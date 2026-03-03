using Photon.Pun;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods
{
    internal class Battle
    {
        public static void BattleStartGame()
        {
            foreach (GorillaBattleManager battle in UnityEngine.Object.FindObjectsOfType<GorillaBattleManager>())
            {
                if (!PhotonView.Get(battle).IsMine)
                {
                    PhotonView.Get(battle).RequestOwnership();
                }
                if (PhotonView.Get(battle).IsMine)
                {
                    battle.StartBattle();
                }
            }
        }

        public static void BattleEndGame()
        {
            foreach (GorillaBattleManager battle in UnityEngine.Object.FindObjectsOfType<GorillaBattleManager>())
            {
                if (!PhotonView.Get(battle).IsMine)
                {
                    PhotonView.Get(battle).RequestOwnership();
                }
                if (PhotonView.Get(battle).IsMine)
                {
                    battle.BattleEnd();
                }
            }
        }

        public static void BattleRestartGame()
        {
            foreach (GorillaBattleManager battle in UnityEngine.Object.FindObjectsOfType<GorillaBattleManager>())
            {
                if (!PhotonView.Get(battle).IsMine)
                {
                    PhotonView.Get(battle).RequestOwnership();
                }
                if (PhotonView.Get(battle).IsMine)
                {
                    battle.BattleEnd();
                    battle.StartBattle();
                }
            }
        }

        public static void BattleBalloonSpam()
        {
            foreach (GorillaBattleManager battle in UnityEngine.Object.FindObjectsOfType<GorillaBattleManager>())
            {
                if (!PhotonView.Get(battle).IsMine)
                {
                    PhotonView.Get(battle).RequestOwnership();
                }
                if (PhotonView.Get(battle).IsMine)
                {
                    foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                    {
                        battle.EndBattleGame();
                        battle.UpdateBattleState();
                        battle.currentState = GorillaBattleManager.BattleState.GameEnd;
                        battle.StartBattle();
                    }
                }
            }
        }
    }
}