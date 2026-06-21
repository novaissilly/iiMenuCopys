using iiMenu.Extensions;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace iiMenu.Classes
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ModChecker : MonoBehaviour
    {
        public ModChecker(IntPtr e) : base(e) { }
        public static volatile ModChecker Instance;
        public ExitGames.Client.Photon.Hashtable stupodProp = null;
        public bool admingetmenuusers = false;

        public virtual void Awake()
        {
            Instance = this;
            stupodProp = new ExitGames.Client.Photon.Hashtable();
            stupodProp.Add("stupid", "stupid");
            PhotonNetwork.LocalPlayer.SetCustomProperties(stupodProp);
        }

        public virtual void Update()
        {
            // Add mod checker here
        }

        public void GetMods(Photon.Realtime.Player plr)
        {
            foreach (var prop in plr.CustomProperties.Values)
            {
                if (prop == null) continue;
                string value = prop.ToString().ToLower();
                foreach (var kvp in prefixMappingModChecker)
                {
                    if (value.Contains(kvp.Key))
                    {
                        string prefix = kvp.Value.displayPrefix;
                        string color = kvp.Value.color;
                        NotificationManager.SendNotification($"<color={color}>[MOD CHECK]</color> {plr.NickName} has {prefix}");
                    }
                }
            }
        }

        public Dictionary<string, (string displayPrefix, string color)> prefixMappingModChecker = new Dictionary<string, (string displayPrefix, string color)>()
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
    }
}