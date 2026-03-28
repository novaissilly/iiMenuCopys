using Photon.Pun;
using System.IO;
using UnityEngine;
using static iiMenu.Classes.RigManager;
using static iiMenu.Menu.Main;
using static iiMenu.Mods.Reconnect;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods
{
    internal class Safety
    {
        public static void AntiCrashEnabled()
        {
            GameObject.Find("GlobalObjectPools").SetActive(false);
        }

        public static void AntiCrashDisabled()
        {
            GameObject.Find("GlobalObjectPools").SetActive(true);
        }

        public static void AntiReportDisconnect()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (GorillaPlayerScoreboardLine line in GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>())
                {
                    foreach (VRRig rig in GorillaParent.instance.vrrigs)
                    {
                        Transform reportBtn = line.reportButton.gameObject.transform;
                        if (rig != GorillaTagger.Instance.myVRRig)
                        {
                            float R = Vector3.Distance(reportBtn.position, rig.rightHandTransform.position);
                            float L = Vector3.Distance(reportBtn.position, rig.leftHandTransform.position);
                            if (R < 0.45f || L < 0.45f)
                            {
                                PhotonNetwork.Disconnect();
                                NotificationManager.SendNotification("<color=grey>[</color><color=purple>ANTI-REPORT</color><color=grey>]</color> <color=white>Someone attempted to report you, you have been disconnected.</color>");
                            }
                        }
                    }
                }
            }
        }

        public static void AntiReportReconnect()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (GorillaPlayerScoreboardLine line in GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>())
                {
                    foreach (VRRig rig in GorillaParent.instance.vrrigs)
                    {
                        Transform reportBtn = line.reportButton.gameObject.transform;
                        if (rig != GorillaTagger.Instance.myVRRig)
                        {
                            float R = Vector3.Distance(reportBtn.position, rig.rightHandTransform.position);
                            float L = Vector3.Distance(reportBtn.position, rig.leftHandTransform.position);
                            if (R < 0.45f || L < 0.45f)
                            {
                                rejRoom = PhotonNetwork.CurrentRoom.Name;
                                rejDebounce = Time.time + 2f;
                                PhotonNetwork.Disconnect();
                                NotificationManager.SendNotification("<color=grey>[</color><color=purple>ANTI-REPORT</color><color=grey>]</color> <color=white>Someone attempted to report you, you have been disconnected and will be reconnected shortly.</color>");
                            }
                        }
                    }
                }
            }
        }

        public static void AntiReportJoinRandom()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (GorillaPlayerScoreboardLine line in GameObject.FindObjectsOfType<GorillaPlayerScoreboardLine>())
                {
                    foreach (VRRig rig in GorillaParent.instance.vrrigs)
                    {
                        Transform reportBtn = line.reportButton.gameObject.transform;
                        if (rig != GorillaTagger.Instance.myVRRig)
                        {
                            float R = Vector3.Distance(reportBtn.position, rig.rightHandTransform.position);
                            float L = Vector3.Distance(reportBtn.position, rig.leftHandTransform.position);
                            if (R < 0.45f || L < 0.45f)
                            {
                                PhotonNetwork.Disconnect();
                                isJoiningRandom = true;
                                jrDebounce = Time.time + internetFloat;
                                NotificationManager.SendNotification("<color=grey>[</color><color=purple>ANTI-REPORT</color><color=grey>]</color> <color=white>Someone attempted to report you, you have been disconnected and will be connected to a random lobby shortly.</color>");
                            }
                        }
                    }
                }
            }
        }

        public static void AntiModerator()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (!vrrig.isOfflineVRRig && vrrig.concatStringOfCosmeticsAllowed.Contains("LBAAK"))
                {
                    try
                    {
                        VRRig plr = vrrig;
                        Photon.Realtime.Player player = GetPlayerFromVRRig(plr);
                        if (player != null)
                        {
                            string text = "Room: " + PhotonNetwork.CurrentRoom.Name;
                            float r = 0f;
                            float g = 0f;
                            float b = 0f;
                            try
                            {


                            }
                            catch { UnityEngine.Debug.Log("Failed to log colors, rig most likely nonexistent"); }

                            try
                            {
                                text += "\n====================================\n";
                                text += string.Concat(new string[]
                                {
                                    "Player Name: \"",
                                    player.NickName,
                                    "\", Player ID: \"",
                                    player.UserId,
                                    "\", Player Color: (R: ",
                                    r.ToString(),
                                    ", G: ",
                                    g.ToString(),
                                    ", B: ",
                                    b.ToString(),
                                    ")"
                                });
                            }
                            catch { UnityEngine.Debug.Log("Failed to log player"); }

                            text += "\n====================================\n";
                            text += "Text file generated with ii's Stupid Menu";
                            string fileName = "iisStupidMenu/" + player.NickName + " - Anti Moderator.txt";
                            if (!Directory.Exists("iisStupidMenu"))
                            {
                                Directory.CreateDirectory("iisStupidMenu");
                            }
                            File.WriteAllText(fileName, text);
                        }
                    }
                    catch { }
                    PhotonNetwork.Disconnect();
                    NotificationManager.SendNotification("<color=grey>[</color><color=purple>ANTI-REPORT</color><color=grey>]</color> <color=white>There was a moderator in your lobby, you have been disconnected. Their Player ID and Room Code have been saved to a file.</color>");
                }
            }
        }

        public static void EnableACReportSelf()
        {
            AntiCheatSelf = true;
        }

        public static void DisableACReportSelf()
        {
            AntiCheatSelf = false;
        }

        public static void EnableACReportAll()
        {
            AntiCheatAll = true;
        }

        public static void DisableACReportAll()
        {
            AntiCheatAll = false;
        }

        public static void ChangeIdentity()
        {
            string randomName = "GORILLA";
            for (var i = 0; i < 4; i++)
            {
                randomName = randomName + UnityEngine.Random.Range(0, 9).ToString();
            }

            ChangeName(randomName);

            byte randA = (byte)UnityEngine.Random.Range(0, 255);
            byte randB = (byte)UnityEngine.Random.Range(0, 255);
            byte randC = (byte)UnityEngine.Random.Range(0, 255);
            ChangeColor(new Color32(randA, randB, randC, 255));
        }
    }
}