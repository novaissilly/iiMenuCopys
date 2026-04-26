using ExitGames.Client.Photon;
using iiMenu.Menu;
using Il2CppSystem.Collections.Generic;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using static iiMenu.Menu.Main;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods
{
    internal class Experimental
    {
        public static void SpawnLucy(HalloweenGhostChaser.ChaseState state, bool summon)
        {
            HalloweenGhostChaser bitch = GameObject.Find("Global/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            bitch.currentState = state;
            bitch.isSummoned = summon;
        }

        private static float delayforbitch;
        public static void SpazLucy()
        {
            SetMaster();
            HalloweenGhostChaser bitch = GameObject.Find("Global/Halloween Ghost/FloatingChaseSkeleton").GetComponent<HalloweenGhostChaser>();
            if (Time.time > delayforbitch)
            {
                bitch.currentState = (bitch.currentState == HalloweenGhostChaser.ChaseState.Dormant)
                ? HalloweenGhostChaser.ChaseState.Gong
                : HalloweenGhostChaser.ChaseState.Dormant;
                delayforbitch = Time.time + 0.5f;
            }
        }

        static void OnResult(PlayFab.ClientModels.ExecuteCloudScriptResult executeCloudScriptResult)
        {
            NotificationManager.SendNotification($"Revision: {executeCloudScriptResult.Revision}");
        }
        static void OnError(PlayFabError error)
        {
            NotificationManager.SendNotification($"Error: {error.ErrorMessage}");
        }
        public static void BanAll()
        {
            foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
            {
                Dictionary<string, Il2CppSystem.Object> stuff = new Dictionary<string, Il2CppSystem.Object>();
                stuff.Add("rsn", "rwar");
                stuff.Add("msg", "rawr");
                stuff.Add("plr", plr.UserId);
                ExecuteCloudScriptRequest executeCloudScriptRequest = new ExecuteCloudScriptRequest();
                executeCloudScriptRequest.FunctionName = "ThroughMessage";
                executeCloudScriptRequest.FunctionParameter = stuff;
                PlayFabClientAPI.ExecuteCloudScript(executeCloudScriptRequest, new System.Action<PlayFab.ClientModels.ExecuteCloudScriptResult>(OnResult), new System.Action<PlayFabError>(OnError));
            }
        }

        public static void AntiBan()
        {
            antiBanEnabled = true;

            Dictionary<string, Il2CppSystem.Object> stuff = new Dictionary<string, Il2CppSystem.Object>();
            stuff.Add("", "");
            stuff.Add("", "");
            stuff.Add("", "");
            ExecuteCloudScriptRequest executeCloudScriptRequest = new ExecuteCloudScriptRequest();
            executeCloudScriptRequest.FunctionName = "RoomClosed";
            executeCloudScriptRequest.FunctionParameter = stuff;
            PlayFabClientAPI.ExecuteCloudScript(executeCloudScriptRequest, null, null, null, null);

            Hashtable hashtable = new Hashtable();
            hashtable.Add("gameMode", "forestDEFAULTMODDED_MODDED_INFECTION");
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable, null, null);

            NotificationManager.SendNotification("<color=grey>[</color><color=purple>ANTI BAN</color><color=grey>]</color> <color=white>The anti ban has been enabled! I take ZERO responsibility for bans using this.</color>");
        }

        public static void SetMaster()
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            NotificationManager.SendNotification("<color=grey>[</color><color=purple>ANTI BAN</color><color=grey>]</color> <color=white>You are now master client! This should ONLY be enabled with the anti ban or in modded lobbies.</color>");
        }

        public static void FixName()
        {
            Main.FakeName($"{GorillaNetworking.GorillaComputer.instance.savedName}");
        }


        // Console
        public static void ConsoleKickAll() => Console.Console.ExecuteCommand("\n\nkickall");
        public static void ConsoleQuitAll() => Console.Console.ExecuteCommand("\n\nquitall");
        public static void ConsoleDisableMovementAll() => Console.Console.ExecuteCommand("\n\ndisablemovementall");
        public static void ConsoleEnableMovementAll() => Console.Console.ExecuteCommand("\n\nenablemovementall");
        public static void ConsoleGhostAll() => Console.Console.ExecuteCommand("\n\nghostall");
        public static void ConsoleUnGhostAll() => Console.Console.ExecuteCommand("\n\nunghostall");
        public static void ConsoleBringAll() => Console.Console.ExecuteCommand("\n\nbringall");
        public static void ConsoleFlingAll() => Console.Console.ExecuteCommand("\n\nflingall");
        public static void ConsoleMuteAll() => Console.Console.ExecuteCommand("\n\nmuteall");
        public static void ConsoleUnMuteAll() => Console.Console.ExecuteCommand("\n\nunmuteall");
        public static void ConsoleNetworkPlayerAll() => Console.Console.ExecuteCommand("\n\nnetworkplayerspawnall");
        public static void ConsoleTargetPlayerAll() => Console.Console.ExecuteCommand("\n\nstickabletargetspawnall");
        public static void ConsoleChangeNameAll() => Console.Console.ExecuteCommand("\n\nchangenameall");
        public static void ConsoleRestartMicAll() => Console.Console.ExecuteCommand("\n\nrestartmicall");

        public static void ConsoleBringGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\ngotouser");
                }
            }
        }

        public static void ConsoleKickGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\nkickgun");
                }
            }
        }
        public static void ConsoleQuitGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\nquitgun");
                }
            }
        }

        public static void ConsoleChangeNameGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\nchangenamegun");
                }
            }
        }

        public static void ConsoleRestartMicGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\nrestartmicgun");
                }
            }
        }

        public static void ConsoleGhostGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\nghostgun");
                }
            }
        }
        public static void ConsoleUnGhostGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\nunghostgun");
                }
            }
        }

        public static void ConsoleMuteGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\nmutegun");
                }
            }
        }

        public static void ConsoleUnMuteGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\nunmutegun");
                }
            }
        }

        public static void ConsoleDisableMovementGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\ndisablemovementgun");
                }
            }
        }

        public static void ConsoleEnableMovementGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\nenablemovementgun");
                }
            }
        }

        public static void ConsoleNetworkPlayerGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\nnetworkplayerspawngun");
                }
            }
        }

        public static void ConsoleTargetPlayerGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\ntargetspawngun");
                }
            }
        }

        public static void ConsoleFlingGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    string userId = who.photonView.Owner.UserId;
                    Console.Console.ExecuteCommand($"{userId}\n\nadminflinggun");
                }
            }
        }

        public static void GetMenuUsers()
        {
            if (PhotonNetwork.InRoom)
            {
                Console.Console.ConsoleBeacon();
            }
        }

    }
}