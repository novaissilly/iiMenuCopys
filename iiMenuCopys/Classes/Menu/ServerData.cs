using iiMenu.Extensions;
using MelonLoader;
using Photon.Pun;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Console
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ServerData : MonoBehaviour
    {
        public ServerData(IntPtr ptr) : base(ptr) { }

        public static ServerData instance;

        public readonly List<string> Administrators = new List<string>();
        public bool isAdmin = false;
        public bool checkedForAdmin = false;
        public bool adminnametags = false;

        public void SetUpAdminPanel(string nickname)
        {
            iiMenu.Menu.Main.SetupAdminPanel(nickname);
        }

        private ExitGames.Client.Photon.Hashtable consoleHash; // KEEP THIS FOR OTHER INSTANCES OF CONSOLE AS WELL

        public virtual void Awake()
        {
            instance = this;

            consoleHash = new ExitGames.Client.Photon.Hashtable(); // for other instances of Console
            consoleHash.Add("console", "console"); // for other instances of Console
            PhotonNetwork.LocalPlayer.SetCustomProperties(consoleHash); // for other instances of Console
        }

        public virtual void Update()
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                if (!checkedForAdmin)
                {
                    if (Administrators.Contains(PhotonNetwork.LocalPlayer.UserId))
                    {
                        SetUpAdminPanel(PhotonNetwork.LocalPlayer.NickName);
                        isAdmin = true;
                    }
                    else
                    {
                        isAdmin = false;
                    }
                    checkedForAdmin = true;
                }
            }

            if (PhotonNetwork.InRoom)
            {
                if (adminnametags)
                {
                    foreach (VRRig rig in GorillaParent.instance.vrrigs)
                    {
                        if (VRRigExtensions.GetVRRigWithoutMe(rig))
                        {
                            var props = rig.photonView.Owner.CustomProperties;

                            string fullText = "";
                            Color lastColor = Color.white;
                            foreach (var prop in prefixMapping)
                            {
                                if (props.ContainsKey(prop.Key))
                                {
                                    fullText += "| " + prop.Value.displayPrefix + " | ";
                                    lastColor = StringToColor(prop.Value.color);
                                }
                            }
                            if (!string.IsNullOrEmpty(fullText))
                            {
                                GameObject nametagholder = new GameObject();
                                nametagholder.transform.position = rig.headMesh.transform.position + new Vector3(0f, 0.9f, 0f);
                                TextMeshPro nametag = nametagholder.AddComponent<TextMeshPro>();
                                nametag.alignment = TextAlignmentOptions.Center;
                                nametag.fontSize = 0.7f;
                                nametag.richText = true;
                                nametag.fontStyle = FontStyles.Italic;
                                nametag.fontSizeMin = 0;
                                nametag.transform.position = rig.headMesh.transform.position + new Vector3(0f, 0.9f, 0f);
                                nametag.transform.LookAt(Camera.main.transform);
                                nametag.transform.Rotate(new Vector3(0f, 180f, 0f));
                                nametag.color = lastColor;
                                nametag.text = fullText;
                                GameObject.Destroy(nametagholder, Time.deltaTime);
                            }
                        }
                    }
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

        public void Log(string msg)
        {
            MelonLogger.Msg($"[CONSOLE::LOG] {msg}");
        }
    }
}