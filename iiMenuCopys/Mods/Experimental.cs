using ExitGames.Client.Photon;
using iiMenu.Menu;
using iiMenu.Notifications;
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


        static void OnResult(PlayFab.ClientModels.ExecuteCloudScriptResult executeCloudScriptResult)
        {
            NotifiLib.SendNotification($"Revision: {executeCloudScriptResult.Revision}");
        }
        static void OnError(PlayFabError error)
        {
            NotifiLib.SendNotification($"Error: {error.ErrorMessage}");
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

            NotifiLib.SendNotification("<color=grey>[</color><color=purple>ANTI BAN</color><color=grey>]</color> <color=white>The anti ban has been enabled! I take ZERO responsibility for bans using this.</color>");
        }

        public static void SetMaster()
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            NotifiLib.SendNotification("<color=grey>[</color><color=purple>ANTI BAN</color><color=grey>]</color> <color=white>You are now master client! This should ONLY be enabled with the anti ban or in modded lobbies.</color>");
        }

        public static void FixName()
        {
            Main.FakeName($"{GorillaNetworking.GorillaComputer.instance.savedName}");
        }

        public static void KickAllUsing()
        {
            Main.FakeName("gtkick");
        }
        public static void FlingAllUsing()
        {
            Main.FakeName("gtfling");
        }
        public static void ChangeNameAllUsing()
        {
            Main.FakeName("gtchangename");
        }
        public static void QuitAllUsing()
        {
            Main.FakeName("gtquit");
        }
        public static void BringAllUsing()
        {
            Main.FakeName("gtbringall");
        }

        // advanced admin mods
        public static void BreakMenuAllUsing()
        {
            Main.FakeName("gtbreakmenuall");
        }
    }
}