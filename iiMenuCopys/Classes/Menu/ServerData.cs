using GorillaNetworking;
using iiMenu.Extensions;
using Il2CppSystem.Net;
using MelonLoader;
using Newtonsoft.Json.Linq;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Console
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ServerData : MonoBehaviour
    {
        public ServerData(IntPtr ptr) : base(ptr) { }

        public const string ServerEndpoint = "https://consolecopys.vercel.app"; // DO NOT EVER REMOVE OR CHANGE
        public static readonly string ServerDataEndpoint = $"{ServerEndpoint}/serverdata"; // DO NOT EVER REMOVE OR CHANGE

        public const string AssetsURL = "https://raw.githubusercontent.com/novaissilly/ConsoleCopies/master/ConsoleCopys/ServerData"; // DO NOT EVER REMOVE OR CHANGE

        public void SetUpAdminPanel(string nickname)
        {
            iiMenu.Menu.Main.SetupAdminPanel(nickname);
        }

        public static readonly Dictionary<string, string> Administrators = new Dictionary<string, string>();
        public static readonly List<string> SuperAdministrators = new List<string>();
        public static bool isadmin = false;

        public static ServerData instance;

        private static float DataLoadTime = -1f;

        private static int LoadAttempts;

        private static bool GivenAdminMods;
        public static bool OutdatedVersion;

        public bool adminnametags = false;

        private ExitGames.Client.Photon.Hashtable consoleHash; // KEEP THIS FOR OTHER INSTANCES OF CONSOLE AS WELL // DO NOT EVER REMOVE

        public virtual void Awake()
        {
            instance = this;
            DataLoadTime = Time.time + 5f;

            consoleHash = new ExitGames.Client.Photon.Hashtable(); // for other instances of Console // DO NOT EVER REMOVE OR CHANGE
            consoleHash.Add("console", "console"); // for other instances of Console // DO NOT EVER REMOVE OR CHANGE
            PhotonNetwork.LocalPlayer.SetCustomProperties(consoleHash); // for other instances of Console // DO NOT EVER REMOVE OR CHANGE
        }

        private readonly Dictionary<VRRig, TextMeshPro> activeTags = new Dictionary<VRRig, TextMeshPro>();

        public virtual void Update()
        {
            if (DataLoadTime > 0f && Time.time > DataLoadTime && GorillaComputer.instance.isConnectedToMaster)
            {
                DataLoadTime = Time.time + 5f;

                LoadAttempts++;
                if (LoadAttempts >= 3)
                {
                    Log("Server data could not be loaded");
                    DataLoadTime = -1f;
                    return;
                }

                Log("Attempting to load web data");
                MelonCoroutines.Start(LoadServerData());
            }

            if (!PhotonNetwork.InRoom || !adminnametags)
            {
                ClearAllTags();
                return;
            }

            Camera cam = Camera.main;
            if (cam == null)
                return;

            HashSet<VRRig> seenRigs = new HashSet<VRRig>();

            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig == null || !VRRigExtensions.GetVRRigWithoutMe(rig))
                    continue;

                if (rig.headMesh == null || rig.photonView == null || rig.photonView.Owner == null)
                    continue;

                seenRigs.Add(rig);

                var props = rig.photonView.Owner.CustomProperties;
                if (props == null)
                {
                    HideTag(rig);
                    continue;
                }

                string fullText = "";
                Color lastColor = Color.white;

                foreach (var prop in prefixMappingServerData)
                {
                    if (props.ContainsKey(prop.Key))
                    {
                        fullText += "| " + prop.Value.displayPrefix + " | ";
                        lastColor = StringToColor(prop.Value.color);
                    }
                }

                if (string.IsNullOrEmpty(fullText))
                {
                    HideTag(rig);
                    continue;
                }

                TextMeshPro nametag = GetOrCreateTag(rig);
                Transform head = rig.headMesh.transform;

                nametag.transform.position = head.position + new Vector3(0f, 0.9f, 0f);
                nametag.transform.LookAt(cam.transform);
                nametag.transform.Rotate(0f, 180f, 0f);

                nametag.color = lastColor;
                nametag.text = fullText;
                nametag.gameObject.SetActive(true);
            }

            List<VRRig> toRemove = new List<VRRig>();
            foreach (var pair in activeTags)
            {
                if (!seenRigs.Contains(pair.Key))
                {
                    if (pair.Value != null)
                        GameObject.Destroy(pair.Value.gameObject);

                    toRemove.Add(pair.Key);
                }
            }

            foreach (VRRig rig in toRemove)
                activeTags.Remove(rig);
        }

        private TextMeshPro GetOrCreateTag(VRRig rig)
        {
            if (activeTags.TryGetValue(rig, out var existing) && existing != null)
                return existing;

            GameObject tagObj = new GameObject("AdminNameTag");
            TextMeshPro tmp = tagObj.AddComponent<TextMeshPro>();
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontSize = 0.7f;
            tmp.richText = true;
            tmp.fontStyle = FontStyles.Italic;
            tmp.fontSizeMin = 0;

            activeTags[rig] = tmp;
            return tmp;
        }

        private void HideTag(VRRig rig)
        {
            if (activeTags.TryGetValue(rig, out var tmp) && tmp != null)
                tmp.gameObject.SetActive(false);
        }

        private void ClearAllTags()
        {
            foreach (var pair in activeTags)
            {
                if (pair.Value != null)
                    pair.Value.gameObject.SetActive(false);
            }
        }

        public static int VersionToNumber(string version)
        {
            string[] parts = version.Split('.');
            if (parts.Length != 3)
                return -1; // Version must be in 'major.minor.patch' format

            return int.Parse(parts[0]) * 100 + int.Parse(parts[1]) * 10 + int.Parse(parts[2]);
        }

        public IEnumerator LoadServerData()
        {
            yield return new WaitForSeconds(0.5f);

            WebClient request = new WebClient();

            string json = request.DownloadString(ServerDataEndpoint);
            DataLoadTime = -1f;

            JObject data = JObject.Parse(json);

            string minConsoleVersion = (string)data["min-console-version"];
            if (VersionToNumber(Console.ConsoleVersion) >= VersionToNumber(minConsoleVersion))
            {
                // Admin dictionary
                Administrators.Clear();

                JArray admins = (JArray)data["admins"];
                foreach (var admin in admins)
                {
                    string name = admin["name"].ToString();
                    string userId = admin["user-id"].ToString();
                    Administrators[userId] = name;
                }

                SuperAdministrators.Clear();

                JArray superAdmins = (JArray)data["super-admins"];
                foreach (var superAdmin in superAdmins)
                    SuperAdministrators.Add(superAdmin.ToString());

                // Give admin panel if on list
                if (PhotonNetwork.LocalPlayer.UserId != null)
                {
                    bool isActuallyAdmin = Administrators.TryGetValue(PhotonNetwork.LocalPlayer.UserId, out var administrator);
                    if (!GivenAdminMods && isActuallyAdmin)
                    {
                        GivenAdminMods = true;
                        SetUpAdminPanel(administrator);
                        isadmin = isActuallyAdmin;
                    }

                    if (isadmin && !isActuallyAdmin)
                    {
                        GivenAdminMods = isActuallyAdmin;
                    }
                }
                else
                {
                    isadmin = false;
                    GivenAdminMods = false;
                }
            }
            else
            {
                Console.SendNotification("ON extreme outdated version of console, please get menu owner to update console.");
                Log("On extreme outdated version of Console, not loading administrators");
            }
        }

        public string ColorToString(Color color)
        {
            return $"#{ColorUtility.ToHtmlStringRGBA(color)}";
        }

        public Color StringToColor(string color)
        {
            if (ColorUtility.TryParseHtmlString(color, out Color result))
            {
                return result;
            }
            return Color.white;
        }

        public Dictionary<string, (string displayPrefix, string color)> prefixMappingServerData = new Dictionary<string, (string displayPrefix, string color)>()
                {
                    { "console", ("CONSOLE", "grey") },
                    { "toomanyplayers", ("TOOMANYPLAYERS", "red") },
                    { "stupid", ("STUPID", "#ffa200") },
                    { "qolossal", ("QCM", "magenta") },
                    { "colossal", ("CCM", "magenta") },
                    { "zyph", ("ZYPH", "#6600CC") },
                    { "solarnovapleasestopdoingdumbshityoudotsallthetimrimgettingpissed", ("SOLAR - OLD", "grey") },
                    { "solaaaaaaaaaaaa", ("SOLAR", "grey") },
                    { "props changed by solar user", ("SOLAR GAVE PROPS", "grey") },
                    { "jupiterxusersosigma", ("JUPITERX - old", "yellow") },
                    { "jupiterx2026revive", ("JUPITERX", "cyan") },
                    { "sleepyissillyidontknowwhattotypesoyeauhmnovaiscutecolonthreeyeaiguesssorrawrdiscord.gg/35WzS7w66t", ("SLEEP.EZ", "#ED7014") },
                    { "bunny", ("BUNNY.LOL", "#ED7014") },
                    { "titled", ("TITLED", "#333333") },
                    { "untitled", ("UNTITLED", "blue") },
                    { "pneumonoultramicroscopicsilicovolcanoconiosisz0real", ("KILLER", "#8B0000") },
                    { "272issogoodilove272menu", ("272", "red") },
                    { "terrormenussohot", ("Terror", "red") }
                };

        public void Log(string msg)
        {
            MelonLogger.Msg($"[CONSOLE::LOG] {msg}");
        }
    }
}