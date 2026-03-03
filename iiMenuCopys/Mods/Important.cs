using ExitGames.Client.Photon;
using GorillaNetworking;
using Photon.Pun;
using UnityEngine;
using static iiMenu.Menu.Main;
using static iiMenu.Mods.Reconnect;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods
{
    internal class Important
    {
        public static void Disconnect()
        {
            if ((GetIndex("Primary Room Mods").enabled && rightPrimary) || !GetIndex("Primary Room Mods").enabled)
            {
                PhotonNetwork.Disconnect();
            }
        }

        public static void Reconnect()
        {
            if ((GetIndex("Primary Room Mods").enabled && rightPrimary) || !GetIndex("Primary Room Mods").enabled)
            {
                rejRoom = PhotonNetwork.CurrentRoom.Name;
                rejDebounce = Time.time + internetFloat;
                PhotonNetwork.Disconnect();
            }
        }

        public static void CancelReconnect()
        {
            rejRoom = null;
            isJoiningRandom = false;
        }

        public static void JoinRandom()
        {
            if ((GetIndex("Primary Room Mods").enabled && rightPrimary) || !GetIndex("Primary Room Mods").enabled)
            {
                if (PhotonNetwork.InRoom)
                {
                    PhotonNetwork.Disconnect();
                    isJoiningRandom = true;
                    jrDebounce = Time.time + internetFloat;
                }
                else
                {
                    GameObject forest = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest");
                    GameObject city = GameObject.Find("Environment Objects/LocalObjects_Prefab/City");
                    GameObject canyons = GameObject.Find("Environment Objects/LocalObjects_Prefab/Canyon");
                    GameObject mountains = GameObject.Find("Environment Objects/LocalObjects_Prefab/Mountain");
                    GameObject beach = GameObject.Find("Environment Objects/LocalObjects_Prefab/Beach");
                    GameObject sky = GameObject.Find("Environment Objects/LocalObjects_Prefab/skyjungle");
                    GameObject basement = GameObject.Find("Environment Objects/LocalObjects_Prefab/Basement");
                    GameObject caves = GameObject.Find("Environment Objects/LocalObjects_Prefab/Cave_Main_Prefab");

                    if (forest.activeSelf == true)
                    {
                        GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                    }
                    if (city.activeSelf == true)
                    {
                        GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City Front").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                    }
                    if (canyons.activeSelf == true)
                    {
                        GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Canyon").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                    }
                    if (mountains.activeSelf == true)
                    {
                        GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Mountain For Computer").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                    }
                    if (beach.activeSelf == true)
                    {
                        GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Beach from Forest").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                    }
                    if (sky.activeSelf == true)
                    {
                        GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Clouds").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                    }
                    if (basement.activeSelf == true)
                    {
                        GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Basement For Computer").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                    }
                    if (caves.activeSelf == true)
                    {
                        GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Cave").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                    }
                }
            }
        }

        public static void CreatePublic()
        {
            Hashtable hash = new Hashtable();
            hash.Add("gameMode", "INFECTION");
            Photon.Realtime.RoomOptions roomOptions = new Photon.Realtime.RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            roomOptions.MaxPlayers = iiMenu.Menu.Main.controller.GetRoomSize(iiMenu.Menu.Main.controller.currentJoinTrigger.gameModeName);
            roomOptions.CustomRoomProperties = hash;
            roomOptions.PublishUserId = true;
            roomOptions.CustomRoomPropertiesForLobby = new string[]
            {
                "gameMode"
            };
            PhotonNetwork.CreateRoom(RandomRoomName(), roomOptions, null, null);
        }

        public static void EnableFPC()
        {
            if (GameObject.Find("Third Person Camera") != null)
            {
                cam = GameObject.Find("Third Person Camera");
            }
            if (GameObject.Find("CameraTablet(Clone)") != null)
            {
                cam = GameObject.Find("CameraTablet(Clone)");
            }

            if (cam != null)
            {
                cam.SetActive(false);
            }
        }

        public static void DisableFPC()
        {
            if (cam != null)
            {
                cam.SetActive(true);
            }
        }

        public static void JoinDiscord()
        {
            Application.OpenURL("https://discord.gg/dtQdz59FJG");
        }

        public static void EnableAntiAFK()
        {
            iiMenu.Menu.Main.controller.disableAFKKick = false;
        }

        public static void DisableAntiAFK()
        {
            iiMenu.Menu.Main.controller.disableAFKKick = true;
        }

        public static void DisableNetworkTriggers()
        {
            GameObject.Find("NetworkTriggers").SetActive(false);
        }

        public static void EnableNetworkTriggers()
        {
            GameObject.Find("NetworkTriggers").SetActive(true);
        }

        public static void PCButtonClick()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    GorillaPressableButton button = Ray.collider.GetComponent<GorillaPressableButton>();
                    if (button != null)
                    {
                        button.ButtonActivation();
                    }

                    GorillaTriggerBox triggerBox = Ray.collider.GetComponent<GorillaTriggerBox>();
                    if (triggerBox != null && triggerBox.triggerBoxOnce)
                    {
                        triggerBox.OnBoxTriggered();
                    }

                    GorillaKeyboardButton keyboardButton = Ray.collider.GetComponent<GorillaKeyboardButton>();
                    if (keyboardButton != null)
                    {
                        keyboardButton.computer.PressButton(keyboardButton);
                    }

                    ModeSelectButton modeSelectButton = Ray.collider.GetComponent<ModeSelectButton>();
                    if (modeSelectButton != null)
                    {
                        modeSelectButton.ButtonActivationWithHand(false);
                    }

                    CheckoutCartButton checkoutCartButton = Ray.collider.GetComponent<CheckoutCartButton>();
                    if (checkoutCartButton != null)
                    {
                        checkoutCartButton.ButtonActivationWithHand(false);
                    }
                    PurchaseItemButton purchaseItemButton = Ray.collider.GetComponent<PurchaseItemButton>();
                    if (purchaseItemButton != null)
                    {
                        purchaseItemButton.ButtonActivationWithHand(false);
                    }
                    CosmeticStand cosmeticStand = Ray.collider.GetComponent<CosmeticStand>();
                    if (cosmeticStand != null)
                    {
                        cosmeticStand.ButtonActivationWithHand(false);
                    }
                    FittingRoomButton fittingRoomButton = Ray.collider.GetComponent<FittingRoomButton>();
                    if (fittingRoomButton != null)
                    {
                        fittingRoomButton.ButtonActivationWithHand(false);
                    }
                }
            }
        }

        public static void DisableQuitBox()
        {
            GameObject.Find("QuitBox").SetActive(false);
        }

        public static void EnableQuitBox()
        {
            GameObject.Find("QuitBox").SetActive(true);
        }

        public static void EnableFPSBoost()
        {
            QualitySettings.masterTextureLimit = 99999;
        }

        public static void DisableFPSBoost()
        {
            QualitySettings.masterTextureLimit = 0;
        }

        public static void ForceLagGame()
        {
            foreach (MeshCollider v in GameObject.FindObjectsOfType<MeshCollider>())
            {
                v.enabled = true;
                v.gameObject.SetActive(true);
            }
        }

        public static void EUServers()
        {
            PhotonNetwork.ConnectToRegion("eu");
        }

        public static void USServers()
        {
            PhotonNetwork.ConnectToRegion("us");
        }

        public static void USWServers()
        {
            PhotonNetwork.ConnectToRegion("usw");
        }

        public static string RandomRoomName()
        {
            string text = "";
            for (int i = 0; i < 4; i++)
            {
                text += iiMenu.Menu.Main.controller.roomCharacters.Substring(Random.Range(0, iiMenu.Menu.Main.controller.roomCharacters.Length), 1);
            }
            if (GorillaComputer.instance.CheckAutoBanListForName(text))
            {
                return text;
            }
            return RandomRoomName();
        }
    }
}
