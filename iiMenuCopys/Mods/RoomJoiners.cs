using GorillaNetworking;
using UnityEngine;
using static iiMenu.Mods.Reconnect;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods
{
    internal class RoomJoiners
    {
        public static void JoinRoom(string room)
        {
            GameObject.Find("Photon Manager").GetComponent<PhotonNetworkController>().AttemptToJoinSpecificRoom(room);
            rejDebounce = Time.time + 2f;
        }
    }
}