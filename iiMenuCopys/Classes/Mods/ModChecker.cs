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
        private bool checkedmenus = false;
        private int menuuserscount = 0;

        public virtual void Awake()
        {
            Instance = this;
            stupodProp = new ExitGames.Client.Photon.Hashtable();
            stupodProp.Add("stupid", "stupid");
            menuuserscount = 0;
            PhotonNetwork.LocalPlayer.SetCustomProperties(stupodProp);
        }

        public virtual void Update()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (VRRigExtensions.GetVRRigWithoutMe(rig))
                    {
                        var props = rig.photonView.Owner.CustomProperties;
                        foreach (var prop in prefixMapping)
                        {
                            if (props.ContainsKey(prop.Key))
                            {
                                GameObject nametagholder = new GameObject();
                                nametagholder.transform.position = rig.headMesh.transform.position + new Vector3(0f, 0.6f, 0f);
                                TextMeshPro nametag = nametagholder.AddComponent<TextMeshPro>();
                                nametag.alignment = TextAlignmentOptions.Center;
                                nametag.fontSize = 0.7f;
                                nametag.richText = true;
                                nametag.fontStyle = FontStyles.Italic;
                                nametag.fontSizeMin = 0;
                                nametag.transform.position = rig.headMesh.transform.position + new Vector3(0f, 0.6f, 0f);
                                nametag.transform.LookAt(Camera.main.transform);
                                nametag.transform.Rotate(new Vector3(0f, 180f, 0f));
                                nametag.color = StringToColor(prop.Value.color);
                                nametag.text = prop.Value.displayPrefix;
                                GameObject.Destroy(nametagholder, Time.deltaTime);
                            }

                            if (admingetmenuusers)
                            {
                                if (!checkedmenus)
                                {
                                    bool prevChecked = checkedmenus;
                                    if (!checkedmenus)
                                        prevChecked = true;
                                    else if (prevChecked)
                                        checkedmenus = true;
                                    else
                                    {
                                        checkedmenus = false;
                                        prevChecked = checkedmenus;
                                    }
                                    if (prevChecked)
                                    {
                                        if (props.ContainsKey("stupid"))
                                        {
                                            menuuserscount += 1;
                                            NotificationManager.SendNotification($"<color={prop.Value.color}>[MENU-USERS]</color> Count: {menuuserscount}");
                                            prevChecked = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void GetMods(Photon.Realtime.Player plr)
        {
            foreach (string pro in prefixMapping.Keys)
            {
                if (plr.CustomProperties.ContainsKey(pro))
                {
                    NotificationManager.SendNotification($"");
                }
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

        public Dictionary<string, (string displayPrefix, string color)> prefixMapping = new Dictionary<string, (string displayPrefix, string color)>()
                {
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
                    { "terrormenussohot", ("Terror", "red") },
                };
    }
}