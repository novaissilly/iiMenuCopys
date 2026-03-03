using iiMenu.Classes;
using iiMenu.Menu;
using iiMenu.Notifications;
using Il2CppSystem.Net;
using Photon.Voice.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace iiMenu.Mods
{
    internal class SoundBoard
    {
        private static bool SoundLoaded = false;
        private static AudioClip downloadedSound = null;
        public static bool AudioIsPlaying = false;
        public static float RecoverTime = -1f;
        public static bool LoopAudio = false;
        public static string Subdirectory = "";
        public static Dictionary<string, AudioClip> audioFilePool = new Dictionary<string, AudioClip> { };
        private static GameObject audiomgr = null;
        // Volume control and embedded resources are unchanged (remove if unused)
        public static string[] VolumeNames = { "Normal", "Loud", "Quiet" };
        public static int Volumeint = 0;
        public static float Volume = 0.5f;

        public static void LoadSoundboard()
        {
            Main.pageNumber = 0;
            Main.buttonsType = 20;
            // Change light to your menu name
            string basePath = Path.Combine("iisStupidMenu", "Sounds", Subdirectory.TrimStart('/').Replace("\\", "/"));

            if (!Directory.Exists("iisStupidMenu")) // Change light to your menu name
                Directory.CreateDirectory("iisStupidMenu"); // Change light to your menu name

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            List<string> enabledSounds = new List<string>();
            foreach (ButtonInfo binfo in Buttons.buttons[27])
            {
                if (binfo.enabled)
                    enabledSounds.Add(binfo.overlapText);
            }

            List<ButtonInfo> soundbuttons = new List<ButtonInfo>
            {
                new ButtonInfo { buttonText = "<color=red>Exit Soundboard</color>", method = () => Settings.ReturnToMain(), isTogglable = false, toolTip = "Returns you to main menu." }
            };

            int index = 0;

            string[] folders = Directory.GetDirectories(basePath);
            foreach (string folder in folders)
            {
                index++;
                string folderName = Path.GetFileName(folder);
                string relativePath = Path.Combine(Subdirectory.TrimStart('/'), folderName).Replace("\\", "/");

                soundbuttons.Add(new ButtonInfo
                {
                    buttonText = $"SoundboardFolder{index}",
                    overlapText = $"▶ {folderName}",
                    method = () => OpenFolder(relativePath),
                    isTogglable = false,
                    toolTip = $"Opens the {folderName} folder."
                });
            }

            index = 0;
            string[] files = Directory.GetFiles(basePath);
            foreach (string file in files)
            {
                index++;
                string fileName = Path.GetFileName(file);
                string cleanName = RemoveFileExtension(fileName).Replace("_", " ");
                bool isEnabled = enabledSounds.Contains(cleanName);
                string relativePath = Path.Combine("iisStupidMenu", "Sounds", Subdirectory.TrimStart('/'), fileName).Replace("\\", "/");
                // Change light to your menu name
                if (LoopAudio)
                {
                    soundbuttons.Add(new ButtonInfo
                    {
                        buttonText = $"SoundboardSound{index}",
                        overlapText = cleanName,
                        enableMethod = () => PlaySoundFile(relativePath),
                        disableMethod = () => StopAllSounds(),
                        enabled = isEnabled,
                        toolTip = $"Plays \"{cleanName}\" through your microphone."
                    });
                }
                else
                {
                    soundbuttons.Add(new ButtonInfo
                    {
                        buttonText = $"SoundboardSound{index}",
                        overlapText = cleanName,
                        method = () => PlaySoundFile(relativePath),
                        isTogglable = false,
                        toolTip = $"Plays \"{cleanName}\" through your microphone."
                    });
                }
            }

            soundbuttons.Add(new ButtonInfo { buttonText = "Stop All Sounds", method = () => StopAllSounds(), isTogglable = false, toolTip = "Stops all currently playing sounds." });
            soundbuttons.Add(new ButtonInfo { buttonText = "Reload Sounds", method = () => LoadSoundboard(), isTogglable = false, toolTip = "Reloads all of your sounds." });
            soundbuttons.Add(new ButtonInfo { buttonText = "Loop Audio", enableMethod = LoopAudioToggleOn, disableMethod = LoopAudioToggleOff, isTogglable = true, toolTip = "Loop the audio." });
            //soundbuttons.Add(new ButtonInfo { buttonText = "Get More Sounds", method = LoadSoundLibrary, isTogglable = false, toolTip = "Opens a public audio library, where you can download your own sounds." });
            Buttons.buttons[20] = soundbuttons.ToArray(); // Make this your new buttoninfo[] {} number
        }

        public static void Play2DAudio(AudioClip sound, float volume)
        {
            if (audiomgr == null)
            {
                audiomgr = new GameObject("2DAudioMgr");
                AudioSource temp = audiomgr.AddComponent<AudioSource>();
                temp.spatialBlend = 0f;
            }
            AudioSource ausrc = audiomgr.GetComponent<AudioSource>();
            ausrc.volume = volume;
            ausrc.PlayOneShot(sound);
        }

        public static string GetFileExtension(string fileName)
        {
            return fileName.ToLower().Split('.')[fileName.Split('.').Length - 1];
        }

        public static string RemoveLastDirectory(string directory)
        {
            return directory == "" || directory.LastIndexOf('/') <= 0 ? "" : directory.Substring(0, directory.LastIndexOf('/'));
        }

        public static string[] AlphabetizeNoSkip(string[] array)
        {
            if (array.Length <= 1)
                return array;

            string first = array[0];
            string[] others = array.OrderBy(s => s).ToArray();
            return others.ToArray();
        }

        static void LoopAudioToggleOn()
        {
            LoopAudio = true;
        }

        static void LoopAudioToggleOff()
        {
            LoopAudio = false;
        }

        public static void OpenFolder(string folder)
        {
            Subdirectory = "/" + folder.Trim('/');
            LoadSoundboard();
        }

        public static string RemoveFileExtension(string file)
        {
            int index = 0;
            string output = "";
            string[] split = file.Split('.');
            foreach (string part in split)
            {
                index++;
                if (index != split.Length)
                {
                    if (index > 1)
                        output += ".";
                    output += part;
                }
            }
            return output;
        }

        public static void LoadAndPlaySound(string soundpath)
        {
            if (!System.IO.File.Exists(soundpath))
            {
                NotifiLib.SendNotification($"File not found: {soundpath}");
                return;
            }

            string extension = Path.GetExtension(soundpath).ToLowerInvariant();
            if (extension != ".wav")
            {
                NotifiLib.SendNotification($"Unsupported file format: {extension}");
                return;
            }

            byte[] soundData = System.IO.File.ReadAllBytes(soundpath);
            AudioClip clip = CreateAudioClipFromWav(soundData, Path.GetFileNameWithoutExtension(soundpath));
            if (clip != null)
                PlayAudioThroughMicrophone(clip);
            else
                NotifiLib.SendNotification($"AudioClip is null after WAV conversion.");
        }

        private static AudioClip CreateAudioClipFromWav(byte[] wavData, string clipName)
        {
            try
            {
                if (wavData.Length < 44) return null;

                int channels = BitConverter.ToInt16(wavData, 22);
                int sampleRate = BitConverter.ToInt32(wavData, 24);
                int bitsPerSample = BitConverter.ToInt16(wavData, 34);
                int dataSize = wavData.Length - 44;
                int sampleCount = dataSize / (channels * (bitsPerSample / 8));

                AudioClip audioClip = AudioClip.Create(clipName, sampleCount, channels, sampleRate, false);
                float[] samples = new float[sampleCount * channels];

                if (bitsPerSample == 16)
                {
                    for (int i = 0; i < sampleCount * channels; i++)
                    {
                        short sample = BitConverter.ToInt16(wavData, 44 + i * 2);
                        samples[i] = sample / 32768f;
                    }
                }
                else if (bitsPerSample == 8)
                {
                    for (int i = 0; i < sampleCount * channels; i++)
                    {
                        byte sample = wavData[44 + i];
                        samples[i] = (sample - 128) / 128f;
                    }
                }

                audioClip.SetData(samples, 0);
                return audioClip;
            }
            catch (Exception ex)
            {
                NotifiLib.SendNotification("Error " + ex.Message);
                return null;
            }
        }

        private static void PlayAudioThroughMicrophone(AudioClip clip)
        {
            if (clip == null)
            {
                NotifiLib.SendNotification("AudioClip is null.");
                return;
            }

            try
            {
                Recorder component = GameObject.Find("NetworkVoice")?.GetComponent<Recorder>() ?? GameObject.Find("Photon Manager")?.GetComponent<Recorder>();
                if (component == null)
                {
                    // NotifiLib.SendNotification("red", "Soundboard", "Recorder not found on 'NetworkVoice'.");
                    return;
                }

                component.SourceType = Recorder.InputSourceType.AudioClip;
                component.AudioClip = clip;

                typeof(Recorder).GetMethod("RestartRecording")?.Invoke(component, new object[] { LoopAudio });
                typeof(Recorder).GetProperty("DebugEchoMode")?.SetValue(component, true);

                AudioIsPlaying = true;
                RecoverTime = Time.time + clip.length + 0.4f;

                // NotifiLib.SendNotification("green", "Soundboard", $"Playing: {clip.name} ({clip.length:F2}s)");
            }
            catch (Exception ex)
            {
                NotifiLib.SendNotification("Play failed: " + ex.Message);
            }
        }

        public static void RestoreMicrophone()
        {
            try
            {
                Recorder component = GameObject.Find("NetworkVoice")?.GetComponent<Recorder>() ?? GameObject.Find("Photon Manager")?.GetComponent<Recorder>();
                if (component != null)
                {
                    component.SourceType = Recorder.InputSourceType.Microphone;
                    component.AudioClip = null;

                    typeof(Recorder).GetMethod("RestartRecording")?.Invoke(component, new object[] { true });
                    typeof(Recorder).GetProperty("DebugEchoMode")?.SetValue(component, false);

                    AudioIsPlaying = false;
                    RecoverTime = -1f;
                }
            }
            catch { }
        }

        public static void StopAllSounds()
        {
            RestoreMicrophone();
        }

        public static void PlaySoundFile(string soundpath)
        {
            LoadAndPlaySound(soundpath);
        }

        public static void Update()
        {
            if (AudioIsPlaying && RecoverTime > 0 && Time.time >= RecoverTime)
                RestoreMicrophone();
        }

        public static void ChangeVolume()
        {
            switch (Volumeint)
            {
                case 0: Volume = 0.5f; break;
                case 1: Volume = 1.0f; break;
                case 2: Volume = 0.2f; break;
            }
        }

        public static byte[] LoadSoundFromResource(string soundFileName)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string resourcePath = $"iisStupidMenu.Sounds.{soundFileName}";
                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                {
                    if (stream != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        stream.CopyTo(ms);
                        return ms.ToArray();
                    }
                    else
                    {
                        NotifiLib.SendNotification($"Resource not found: {resourcePath}");
                    }
                }
            }
            catch (Exception)
            {
                //NotifiLib.SendNotification("red", "[Soundboard]", $"Resource load error: {ex.Message}");
            }
            return null;
        }

        public static void PlayResourceSound(string soundFileName)
        {
            try
            {
                byte[] soundData = LoadSoundFromResource(soundFileName);
                if (soundData != null && soundData.Length > 0)
                {
                    AudioClip clip = CreateAudioClipFromWav(soundData, Path.GetFileNameWithoutExtension(soundFileName));
                    if (clip != null)
                        PlayAudioThroughMicrophone(clip);
                }
            }
            catch (Exception)
            {
                //NotifiLib.SendNotification("red", "[Soundboard]", $"Error playing sound: {ex.Message}");
            }
        }

        public static void PlayLoadedSound()
        {
            if (downloadedSound != null && SoundLoaded)
                PlayAudioThroughMicrophone(downloadedSound);
        }

        public static void ResetLoadedSound()
        {
            SoundLoaded = false;
            downloadedSound = null;
        }

        public static void Thing()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string[] allResources = assembly.GetManifestResourceNames();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== ALL EMBEDDED RESOURCES ===");
            foreach (string resource in allResources)
                sb.AppendLine(resource);
            NotifiLib.SendNotification($"Resouces {sb.ToString()}");
        }
    }
}