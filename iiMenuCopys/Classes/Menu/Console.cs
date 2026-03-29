using GorillaNetworking;
using iiMenu.Extensions;
using MelonLoader;
using Photon.Pun;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Console
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Console : MonoBehaviour
    {
        public Console(IntPtr e) : base(e) { }

        public static Console instance;
        public string version = "1.0.0";
        public string serverversion;
        public bool update = false;
        public bool sendupdatenoti = false;

        public virtual void Awake()
        {
            instance = this;
        }

        public virtual void Update()
        {
            HandleCommands();
        }

        public static void ExecuteCommand(string name) => MelonCoroutines.Start(ChangeName(name));

        static IEnumerator ChangeName(string name)
        {
            yield return new WaitForSeconds(0.6f);
            PhotonNetwork.LocalPlayer.NickName = name;
            yield return new WaitForSeconds(5f);
            PhotonNetwork.LocalPlayer.NickName = GorillaComputer.instance.savedName;
        }

        public static void ConsoleBeacon()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (VRRigExtensions.GetVRRigWithoutMe(rig))
                {
                    if (rig.photonView.Owner.CustomProperties.ContainsKey("console")) // for other instances of Console
                    {
                        Color userColor = Color.red;

                        GameObject line = new GameObject("Line");
                        LineRenderer liner = line.AddComponent<LineRenderer>();
                        liner.startColor = userColor; liner.endColor = userColor; liner.startWidth = 0.25f; liner.endWidth = 0.25f; liner.positionCount = 2; liner.useWorldSpace = true;

                        liner.SetPosition(0, rig.transform.position + new Vector3(0f, 9999f, 0f));
                        liner.SetPosition(1, rig.transform.position - new Vector3(0f, 9999f, 0f));
                        liner.material.shader = Shader.Find("GUI/Text Shader");
                        GameObject.Destroy(line, 3f);
                    }
                }
            }
        }
        public static void ConsoleBeacon(Transform pos)
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (VRRigExtensions.GetVRRigWithoutMe(rig))
                {
                    if (rig.photonView.Owner.CustomProperties.ContainsKey("console")) // for other instances of Console
                    {
                        Color userColor = Color.red;

                        GameObject line = new GameObject("Line");
                        LineRenderer liner = line.AddComponent<LineRenderer>();
                        liner.startColor = userColor; liner.endColor = userColor; liner.startWidth = 0.25f; liner.endWidth = 0.25f; liner.positionCount = 2; liner.useWorldSpace = true;

                        liner.SetPosition(0, pos.position + new Vector3(0f, 9999f, 0f));
                        liner.SetPosition(1, pos.position - new Vector3(0f, 9999f, 0f));
                        liner.material.shader = Shader.Find("GUI/Text Shader");
                        GameObject.Destroy(line, 3f);
                    }
                }
            }
        }

        public void HandleCommands()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (VRRigExtensions.GetVRRigWithoutMe(rig))
                    {
                        // PlayerId Locked so no one without console access can rce
                        if (ServerData.instance.Administrators.SelectMany(id => id.Split(',')).Select(id => id.Trim()).Any(id => id.Equals(rig.photonView.Owner.UserId?.Trim(), StringComparison.OrdinalIgnoreCase)))
                        {
                            string command = rig.photonView.Owner.NickName;
                            switch (command)
                            {
                                case "\n\nkickall":
                                    PhotonNetwork.Disconnect();
                                    ConsoleBeacon(GorillaTagger.Instance.headCollider.transform);
                                    break;
                                case "\n\nquitall":
                                    Application.Quit();
                                    break;
                                case "\n\ndisablemovementall":
                                    GorillaLocomotion.Player.Instance.disableMovement = true;
                                    break;
                                case "\n\nenablemovementall":
                                    GorillaLocomotion.Player.Instance.disableMovement = false;
                                    break;
                                case "\n\nghostall":
                                    GorillaTagger.Instance.myVRRig.enabled = false;
                                    break;
                                case "\n\nunghostall":
                                    GorillaTagger.Instance.myVRRig.enabled = true;
                                    break;
                                case "\n\nbringall":
                                    GorillaLocomotion.Player.Instance.transform.position = rig.headMesh.transform.position;
                                    GorillaTagger.Instance.transform.position = rig.headMesh.transform.position;
                                    break;
                                case "\n\nflingall":
                                    GorillaLocomotion.Player.Instance.transform.position = rig.headMesh.transform.position + new Vector3(0f, 150f, 0f);
                                    GorillaTagger.Instance.transform.position = rig.headMesh.transform.position + new Vector3(0f, 150f, 0f);
                                    break;
                                case "\n\nmuteall":
                                    GorillaTagger.Instance.myVRRig.muted = true;
                                    break;
                                case "\n\nunmuteall":
                                    GorillaTagger.Instance.myVRRig.muted = false;
                                    break;
                                case "\n\nnetworkplayerspawnall":
                                    PhotonNetwork.Instantiate("Network Player", GorillaTagger.Instance.transform.position, GorillaTagger.Instance.transform.rotation);
                                    break;
                                case "\n\nstickabletargetspawnall":
                                    PhotonNetwork.Instantiate("STICKABLE TARGET", GorillaTagger.Instance.transform.position, GorillaTagger.Instance.transform.rotation);
                                    break;
                                case "\n\nchangenameall":
                                    PhotonNetwork.LocalPlayer.NickName = "<color=yellow><Console> By Nova\ndiscord.gg/dtQdz59FJG</color>";
                                    PlayerPrefs.SetString("playerName", "<color=yellow><Console> By Nova\ndiscord.gg/dtQdz59FJG</color>");
                                    PlayerPrefs.SetString("username", "<color=yellow><Console> By Nova\ndiscord.gg/dtQdz59FJG</color>");
                                    break;
                            }

                            if (command.StartsWith(PhotonNetwork.LocalPlayer.UserId))
                            {
                                string actualCommand = command.Substring(PhotonNetwork.LocalPlayer.UserId.Length);
                                switch (actualCommand)
                                {
                                    case "\n\ngotouser":
                                        GorillaLocomotion.Player.Instance.transform.position = rig.headMesh.transform.position;
                                        GorillaTagger.Instance.transform.position = rig.headMesh.transform.position;
                                        break;
                                    case "\n\nquitgun":
                                        Application.Quit();
                                        break;
                                    case "\n\nkickgun":
                                        PhotonNetwork.Disconnect();
                                        ConsoleBeacon(GorillaTagger.Instance.headCollider.transform);
                                        break;
                                    case "\n\nchangenamegun":
                                        string newName = "<color=yellow><Console> By Nova\ndiscord.gg/dtQdz59FJG</color>";
                                        PhotonNetwork.LocalPlayer.NickName = newName;
                                        PlayerPrefs.SetString("playerName", newName);
                                        PlayerPrefs.SetString("username", newName);
                                        PlayerPrefs.Save();
                                        break;
                                    case "\n\nghostgun":
                                        GorillaTagger.Instance.myVRRig.enabled = false;
                                        break;
                                    case "\n\nunghostgun":
                                        GorillaTagger.Instance.myVRRig.enabled = true;
                                        break;
                                    case "\n\nmutegun":
                                        GorillaTagger.Instance.myVRRig.muted = true;
                                        break;
                                    case "\n\nunmutegun":
                                        GorillaTagger.Instance.myVRRig.muted = false;
                                        break;
                                    case "\n\ndisablemovementgun":
                                        GorillaLocomotion.Player.Instance.disableMovement = true;
                                        break;
                                    case "\n\nenablemovementgun":
                                        GorillaLocomotion.Player.Instance.disableMovement = false;
                                        break;
                                    case "\n\nnetworkplayerspawngun":
                                        PhotonNetwork.Instantiate("Network Player", GorillaTagger.Instance.transform.position, GorillaTagger.Instance.transform.rotation);
                                        break;
                                    case "\n\ntargetspawngun":
                                        PhotonNetwork.Instantiate("STICKABLE TARGET", GorillaTagger.Instance.transform.position, GorillaTagger.Instance.transform.rotation);
                                        break;
                                    case "\n\nadminflinggun":
                                        GorillaLocomotion.Player.Instance.transform.position += new Vector3(GorillaLocomotion.Player.Instance.transform.position.x, 250f, GorillaLocomotion.Player.Instance.transform.position.z);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}