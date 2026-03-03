using ExitGames.Client.Photon;
using iiMenu.Notifications;
using Photon.Pun;
using UnityEngine;
using static iiMenu.Menu.Main;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods
{
    internal class Advantages
    {
        public static void TagSelf()
        {
            /*if (!GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected") && Time.time > delaythinggg)
            {
                PhotonView.Get(GorillaGameManager.instance).RPC("ReportContactWithLavaRPC", RpcTarget.MasterClient, Array.Empty<object>());
                delaythinggg = Time.time + 0.5f;
            }*/
            foreach (GorillaTagManager gorillaTagManager in GameObject.FindObjectsOfType<GorillaTagManager>())
            {
                if (gorillaTagManager.currentInfected.Contains(PhotonNetwork.LocalPlayer))
                {
                    NotifiLib.SendNotification("<color=grey>[</color><color=green>SUCCESS</color><color=grey>]</color> <color=white>You have been tagged!</color>");
                    GorillaTagger.Instance.myVRRig.enabled = true;
                    GetIndex("Tag Self").enabled = false;
                }
                else
                {
                    foreach (VRRig rig in GorillaParent.instance.vrrigs)
                    {
                        if (rig.mainSkin.material.name.Contains("fected"))
                        {
                            GorillaTagger.Instance.myVRRig.enabled = false;
                            GorillaTagger.Instance.myVRRig.transform.position = rig.rightHandTransform.position;
                            GorillaTagger.Instance.myVRRig.transform.position = rig.rightHandTransform.position;
                        }
                    }
                }
            }
        }

        public static void UntagSelf()
        {
            foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
            {
                if (tagman.currentInfected.Contains(PhotonNetwork.LocalPlayer))
                {
                    tagman.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                }
            }
        }

        public static void UntagAll()
        {
            foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
            {
                foreach (Photon.Realtime.Player v in PhotonNetwork.PlayerList)
                {
                    if (tagman.currentInfected.Contains(v))
                    {
                        tagman.currentInfected.Remove(v);
                    }
                }
            }
        }

        public static void SpamTagSelf()
        {
            foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
            {
                if (tagman.currentInfected.Contains(PhotonNetwork.LocalPlayer))
                {
                    tagman.currentInfected.Remove(PhotonNetwork.LocalPlayer);
                }
                else
                {
                    tagman.currentInfected.Add(PhotonNetwork.LocalPlayer);
                }
            }
        }

        public static void SpamTagAll()
        {
            foreach (GorillaTagManager tagman in GameObject.FindObjectsOfType<GorillaTagManager>())
            {
                foreach (Photon.Realtime.Player v in PhotonNetwork.PlayerList)
                {
                    if (tagman.currentInfected.Contains(v))
                    {
                        tagman.currentInfected.Remove(v);
                    }
                    else
                    {
                        tagman.currentInfected.Add(v);
                    }
                }
            }
        }

        public static void PhysicalTagAura()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                Vector3 they = vrrig.transform.position;
                Vector3 notthem = GorillaTagger.Instance.myVRRig.head.rigTarget.position;
                float distance = Vector3.Distance(they, notthem);

                if (GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected") && !vrrig.mainSkin.material.name.Contains("fected") && GorillaLocomotion.Player.Instance.disableMovement == false && distance < 1.667)
                {
                    if (rightHand == true) { GorillaLocomotion.Player.Instance.rightHandTransform.position = they; } else { GorillaLocomotion.Player.Instance.leftHandTransform.position = they; }
                }
            }
        }

        /*public static void RPCTagAura()
        {
            if (GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected"))
            {
                foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
                {
                    VRRig rig = GorillaGameManager.instance.FindPlayerVRRig(player);
                    if (!rig.mainSkin.material.name.Contains("fected"))
                    {
                        if (Time.time > TagAuraDelay)
                        {
                            float distance = Vector3.Distance(GorillaTagger.Instance.myVRRig.transform.position, rig.transform.position);
                            if (distance < GorillaGameManager.instance.tagDistanceThreshold)
                            {
                                PhotonView.Get(GorillaGameManager.instance.GetComponent<GorillaGameManager>()).RPC("ReportTagRPC", RpcTarget.MasterClient, new object[]
                                {
                                                player
                                });
                                RPCProtection();
                            }

                            TagAuraDelay = Time.time + 0.1f;
                        }
                    }
                }
            }
        }*/

        public static void TagGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who != null && who != GorillaTagger.Instance.myVRRig)
                    {
                        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                        GorillaTagger.Instance.myVRRig.photonView.RPC("ReportTagRPC", RpcTarget.MasterClient, new Il2CppSystem.Object[1] { (Il2CppSystem.Object)who.photonView.Owner });
                    }
                }
            }
        }

        public static void FlickTagGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                if (GetGunInput(true))
                {
                    GorillaLocomotion.Player.Instance.rightHandTransform.position = GunPointer.transform.position + new Vector3(0f, 0.3f, 0f);
                }
            }
        }

        public static void TagAll()
        {
            if (!GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected"))
            {
                NotifiLib.SendNotification("<color=grey>[</color><color=red>ERROR</color><color=grey>]</color> <color=white>You must be tagged.</color>");
                GetIndex("Tag All").enabled = false;
            }
            else
            {
                bool isInfectedPlayers = false;
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (!vrrig.mainSkin.material.name.Contains("fected"))
                    {
                        isInfectedPlayers = true;
                        break;
                    }
                }
                if (isInfectedPlayers == true)
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (!vrrig.mainSkin.material.name.Contains("fected"))
                        {
                            if (GorillaTagger.Instance.myVRRig.enabled == true)
                                GorillaTagger.Instance.myVRRig.enabled = false;
                            GorillaTagger.Instance.myVRRig.transform.position = vrrig.transform.position;
                            GorillaTagger.Instance.myVRRig.transform.position = vrrig.transform.position;

                            Vector3 they = vrrig.transform.position;
                            Vector3 notthem = GorillaTagger.Instance.myVRRig.head.rigTarget.position;
                            float distance = Vector3.Distance(they, notthem);

                            if (GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected") && !vrrig.mainSkin.material.name.Contains("fected") && distance < 1.667)
                            {
                                if (rightHand == true) { GorillaLocomotion.Player.Instance.rightHandTransform.position = they; } else { GorillaLocomotion.Player.Instance.leftHandTransform.position = they; }
                            }
                        }
                    }
                }
                else
                {
                    NotifiLib.SendNotification("<color=grey>[</color><color=green>SUCCESS</color><color=grey>]</color> <color=white>Everyone is tagged!</color>");
                    GorillaTagger.Instance.myVRRig.enabled = true;
                    GetIndex("Tag All").enabled = false;
                }
            }
        }

        public static void TagBot()
        {
            if (rightSecondary)
            {
                GetIndex("Tag Bot").enabled = false;
            }
            if (PhotonNetwork.InRoom)
            {
                if (!GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected"))
                {
                    bool isInfectedPlayers = false;
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig.mainSkin.material.name.Contains("fected"))
                        {
                            isInfectedPlayers = true;
                            break;
                        }
                    }
                    if (isInfectedPlayers)
                    {
                        GetIndex("Tag Self").method.Invoke();
                        GetIndex("Tag All").enabled = false;
                    }
                }
                else
                {
                    bool isInfectedPlayers = false;
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (!vrrig.mainSkin.material.name.Contains("fected"))
                        {
                            isInfectedPlayers = true;
                            break;
                        }
                    }
                    if (isInfectedPlayers)
                    {
                        GetIndex("Tag All").enabled = true;
                    }
                }
            }
        }

        public static void NoTagOnJoin()
        {
            PlayerPrefs.SetString("tutorial", "true");
            Hashtable h = new Hashtable();
            h.Add("didTutorial", "true");
            PhotonNetwork.LocalPlayer.SetCustomProperties(h, null, null);
            PlayerPrefs.Save();
        }

        public static void TagOnJoin()
        {
            PlayerPrefs.SetString("tutorial", "false");
            Hashtable h = new Hashtable();
            h.Add("didTutorial", "false");
            PhotonNetwork.LocalPlayer.SetCustomProperties(h, null, null);
            PlayerPrefs.Save();
        }

        public static void EnableRemoveChristmasLights()
        {
            foreach (GameObject g in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (g.activeSelf && g.name.Contains("snow"))
                {
                    g.SetActive(false);
                    lights.Add(g);
                }
            }
        }

        public static void DisableRemoveChristmasLights()
        {
            foreach (GameObject l in lights)
            {
                l.SetActive(true);
            }
            lights.Clear();
        }

        public static void EnableRemoveChristmasDecorations()
        {
            foreach (GameObject g in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (g.activeSelf && g.name.Contains("snow") || g.name.Contains("snowman"))
                {
                    g.SetActive(false);
                    holidayobjects.Add(g);
                }
            }
        }

        public static void DisableRemoveChristmasDecorations()
        {
            foreach (GameObject h in holidayobjects)
            {
                h.SetActive(true);
            }
            holidayobjects.Clear();
        }
    }
}