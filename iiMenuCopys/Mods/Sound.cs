using Photon.Pun;
using static iiMenu.Menu.Main;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods.Spammers
{
    internal class Sound
    {
        public static void PlaySound(int SoundID, float Dalay, float volume)
        {
            bool[] array = new bool[] { true, false };
            int num = UnityEngine.Random.Range(0, array.Length);
            GorillaTagger.Instance.leftHandTouching = true;
            GorillaTagger.Instance.rightHandTouching = true;
            if (PhotonNetwork.InRoom)
            {
                GorillaGameManager.instance.stepVolumeMax = 999999f;
            }
            GorillaLocomotion.Player.Instance.wasLeftHandTouching = true;
            GorillaLocomotion.Player.Instance.wasRightHandTouching = true;
            GorillaLocomotion.Player.Instance.IsHandTouching(forLeftHand: array[num]);
            GorillaTagger.Instance.leftHandTouching = false;
            GorillaTagger.Instance.rightHandTouching = false;
            GorillaTagger.Instance.tapCoolDown = Dalay;
            GorillaTagger.Instance.handTapVolume = volume;
            GorillaTagger.Instance.lastLeftTap = 0f;
            GorillaTagger.Instance.lastRightTap = 0f;
            GorillaLocomotion.Player.Instance.inOverlay = false;
            foreach (GorillaSurfaceOverride gorillasurfaceoverride in UnityEngine.Object.FindObjectsOfType<GorillaSurfaceOverride>())
            {
                gorillasurfaceoverride.overrideIndex = SoundID;
                GorillaLocomotion.Player.Instance.leftHandMaterialTouchIndex = gorillasurfaceoverride.overrideIndex;
                GorillaLocomotion.Player.Instance.rightHandMaterialTouchIndex = gorillasurfaceoverride.overrideIndex;
                GorillaLocomotion.Player.Instance.leftHandSurfaceOverride = gorillasurfaceoverride;
                GorillaLocomotion.Player.Instance.rightHandSurfaceOverride = gorillasurfaceoverride;
            }
        }


        public static void RandomSoundSpam()
        {
            if (rightGrab)
            {
                PlaySound(UnityEngine.Random.Range(0, 57), 0.2f, 2f);
                RPCProtection();
            }
        }

        public static void BassSoundSpam()
        {
            if (rightGrab)
            {
                PlaySound(43, 0.2f, 2f);
                RPCProtection();
            }
        }

        public static void WolfSoundSpam()
        {
            if (rightGrab)
            {
                PlaySound(23, 0.2f, 2f);
                RPCProtection();
            }
        }

        public static void EarrapeSoundSpam()
        {
            if (rightGrab)
            {
                PlaySound(18, 0.2f, 2f);
                RPCProtection();
            }
        }

        public static void BigCrystalSoundSpam()
        {
            if (rightGrab)
            {
                PlaySound(16, 0.2f, 2f);
                RPCProtection();
            }
        }

        public static void AK47SoundSpam()
        {
            if (rightGrab)
            {
                PlaySound(3, 0.2f, 2f);
                RPCProtection();
            }
        }

        public static void SqueakSoundSpam()
        {
            if (rightGrab)
            {
                PlaySound(58, 0.2f, 2f);
                RPCProtection();
            }
        }

        public static void SirenSoundSpam()
        {
            if (rightGrab)
            {
                PlaySound(437, 0.2f, 2f);
                RPCProtection();
            }
        }

        public static void DecreaseSoundID()
        {
            soundId = soundId - 1;
            if (soundId < 0)
            {
                soundId = 0;
            }
            GetIndex("Custom Sound Spam").overlapText = "Custom Sound Spam <color=grey>[</color><color=green>" + soundId.ToString() + "</color><color=grey>]</color>";
        }

        public static void IncreaseSoundID()
        {
            soundId++;
            GetIndex("Custom Sound Spam").overlapText = "Custom Sound Spam <color=grey>[</color><color=green>" + soundId.ToString() + "</color><color=grey>]</color>";
        }

        public static void CustomSoundSpam()
        {
            if (rightGrab)
            {
                PlaySound(soundId, 0.2f, 2f);
                RPCProtection();
            }
        }

        public static void CountSoundSpam()
        {
            if (rightGrab)
            {

                RPCProtection();
            }
        }

        public static void BrawlCountSoundSpam()
        {
            if (rightGrab)
            {

                RPCProtection();
            }
        }

        public static void BrawlStartSoundSpam()
        {
            if (rightGrab)
            {

                RPCProtection();
            }
        }

        public static void TagSoundSpam()
        {
            if (rightGrab)
            {

                RPCProtection();
            }
        }

        public static void RoundEndSoundSpam()
        {
            if (rightGrab)
            {
                //GorillaTagger.Instance.myVRRig.RPC("PlayTagSound", RpcTarget.All, new object[]
                //{
                //     2,
                //    999999f
                //});
                RPCProtection();
            }
        }

        public static void BonkSoundSpam()
        {
            if (rightGrab)
            {
                //GorillaTagger.Instance.myVRRig.RPC("PlayTagSound", RpcTarget.All, new object[]
                //{
                //    4,
                //    999999f
                //});
                RPCProtection();
            }
        }
    }
}