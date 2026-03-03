using easyInputs;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using GorillaNetworking;
using iiMenu.Classes;
using iiMenu.Classes.Menu;
using iiMenu.Mods;
using iiMenu.Notifications;
using Il2CppSystem.Net;
using MelonLoader;
using Mono.Cecil;
using Mono.CSharp;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;
using UnityEngine.XR;
using static iiMenu.Mods.Reconnect;
using static UnityEngine.UIElements.TextField;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;


/*
 *  HEY SKIDDERS
 *  IGNORE THE SHITTY CODE
 *  IF YOU WANT TO CONTINUE THIS FOR ME PLEASE FORK IT AND PULL REQUEST
 *  
 *  EVERYTHING HERE IS NOT MY CODE EXCEPT FOR SOME STUFF
 *  ALL CODE IS iiDks
*/

[assembly: MelonInfo(typeof(iiMenu.Menu.Main), "iiMenuCopys", "1.0.0", "Nova")]
[assembly: MelonGame()]
namespace iiMenu.Menu
{
    public class Main : MelonMod
    {
        public static PhotonNetworkController controller;

        [Obsolete]
        public override void OnApplicationStart()
        {
            ClassInjector.RegisterTypeInIl2Cpp<RigManager>();
            ClassInjector.RegisterTypeInIl2Cpp<iiMenu.Classes.Button>();

            NotifiLib.LoadNotis();
            foreach (PhotonNetworkController con in GameObject.FindObjectsOfType<PhotonNetworkController>())
            {
                controller = con;
            }

            ServerData data = JsonUtility.FromJson<ServerData>(downloader.DownloadString(PluginInfo.ServerDataEndpoint));

            if (data.admins != null)
            {
                foreach (Admin admin in data.admins)
                {
                    if (!string.IsNullOrEmpty(admin.user_id))
                    {
                        Admins[admin.user_id] = admin.name;
                    }
                }
            }

            // Checks the menu incase lock

            if (data.locked)
            {
                Application.OpenURL("https://pastebin.com/raw/VVGz1pTD");

                GameObject.Destroy(GameObject.Find("GorillaPlayer"));
                GameObject.Destroy(GameObject.Find("Main Camera"));
                GameObject.Destroy(GameObject.Find("Level"));
                GameObject.Destroy(GameObject.Find("lower level"));
                Environment.Exit(0);
                Application.Quit();
            }

            // Version Checker
            if (PluginInfo.Version != data.menu_version)
            {
                NotifiLib.SendNotification("<color=red>[UPDATE]</color> menu needs updated!");
                Application.OpenURL("https://pastebin.com/raw/fxcK9stm");
                PluginInfo.Name = "UPDATE NEEDED";
                Application.Quit();
            }
        }

        public override void OnUpdate()
        {
            try
            {
                bool dropOnRemove = true;
                bool isKeyboardCondition = false;
                bool buttonCondition = EasyInputs.GetSecondaryButtonDown(EasyHand.LeftHand);
                if (rightHand)
                {
                    buttonCondition = EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand);
                }
                if (bothHands)
                {
                    buttonCondition = EasyInputs.GetSecondaryButtonDown(EasyHand.LeftHand) || EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand);
                }
                if (wristThing)
                {
                    bool fuck = Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position - (GorillaTagger.Instance.leftHandTransform.forward * 0.1f), GorillaTagger.Instance.rightHandTransform.position) < 0.1f;
                    if (rightHand)
                    {
                        fuck = Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.rightHandTransform.position - (GorillaTagger.Instance.rightHandTransform.forward * 0.1f)) < 0.1f;
                    }
                    if (fuck && !lastChecker)
                    {
                        wristOpen = !wristOpen;
                    }
                    lastChecker = fuck;

                    buttonCondition = wristOpen;
                }
                buttonCondition = buttonCondition || isKeyboardCondition;
                if (buttonCondition && menu == null)
                {
                    ReloadMenu();
                    if (reference == null)
                    {
                        reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        if (rightHand || (bothHands && EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand)))
                        {
                            reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
                        }
                        else
                        {
                            reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
                        }
                        reference.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                        reference.transform.localPosition = Settings.makeThisThePointerPos;
                        reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                        buttonCollider = reference.GetComponent<SphereCollider>();
                    }
                }
                else
                {
                    if (!buttonCondition && menu != null)
                    {
                        if (dropOnRemove)
                        {
                            Rigidbody comp = menu.AddComponent<Rigidbody>();
                            if (rightHand || (bothHands && EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand)))
                            {
                                comp.velocity = EasyInputs.GetDeviceVelocity(EasyHand.RightHand);
                            }
                            else
                            {
                                comp.velocity = EasyInputs.GetDeviceVelocity(EasyHand.LeftHand);
                            }
                            if (annoyingMode)
                            {
                                comp.velocity = new Vector3(UnityEngine.Random.Range(-33, 33), UnityEngine.Random.Range(-33, 33), UnityEngine.Random.Range(-33, 33));
                            }

                            UnityEngine.Object.Destroy(menu, 2);
                            menu = null;
                            UnityEngine.Object.Destroy(reference);
                            reference = null;
                        }
                        else
                        {
                            UnityEngine.Object.Destroy(menu);
                            menu = null;
                            UnityEngine.Object.Destroy(reference);
                            reference = null;
                        }
                    }
                }
                if (buttonCondition && menu != null)
                {
                    RecenterMenu();
                }
                {
                    hasLoaded = true;
                    hasRemovedThisFrame = false;

                    //  try
                    //  {
                    //     
                    // }
                    // catch (Exception exception)
                    // {
                    //    UnityEngine.Debug.LogError(string.Format("iiMenu <b>COLOR ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
                    //  }

                    try
                    {
                        OrangeUI.color = backgroundColor.GetCurrentColor();
                        if (themeType == 6)
                        {
                            float h = (Time.frameCount / 180f) % 1f;
                            OrangeUI.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                        }
                        GameObject.Find("motd").GetComponent<Text>().supportRichText = true;
                        GameObject.Find("motdtext").GetComponent<Text>().supportRichText = true;
                        GameObject.Find("motd").GetComponent<Text>().text = "Thanks for using ii's <b>Stupid</b> Menu";

                        if (lowercaseMode) GameObject.Find("motd").GetComponent<Text>().text = GameObject.Find("motd").GetComponent<Text>().text.ToLower();
                        if (lowercaseMode) GameObject.Find("motdtext").GetComponent<Text>().text = GameObject.Find("motd").GetComponent<Text>().text.ToLower();

                        if (uppercaseMode) GameObject.Find("motd").GetComponent<Text>().text = GameObject.Find("motdtext").GetComponent<Text>().text.ToUpper();
                        if (uppercaseMode) GameObject.Find("motdtext").GetComponent<Text>().text = GameObject.Find("motdtext").GetComponent<Text>().text.ToUpper();

                        if (fullModAmount < 0)
                        {
                            fullModAmount = 0;
                            foreach (ButtonInfo[] buttons in Buttons.buttons)
                                fullModAmount += buttons.Length;
                        }

                        GameObject.Find("motdtext").GetComponent<Text>().text = string.Format(motdTemplate, PluginInfo.Version, fullModAmount, PluginInfo.BetaBuild ? "Beta" : "Release", PluginInfo.BuildTimestamp);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("MOTD Setup Error: " + ex);
                    }


                    //Camera TPC = null;
                    /*try
                    {
                        //TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
                    }
                    catch { }*/

                    if (fpsCount != null)
                    {
                        fpsCount.text = "FPS: " + (1f / Time.deltaTime).ToString("F1");
                    }

                    if (menuBackground != null && reference != null)
                    {
                        reference.GetComponent<Renderer>().material.color = menuBackground.GetComponent<Renderer>().material.color;
                    }

                    if (disorganized && buttonsType != 0)
                    {
                        buttonsType = 0;
                        ReloadMenu();
                    }

                    try
                    {
                        if (PhotonNetwork.InRoom)
                        {
                            lastRoom = PhotonNetwork.CurrentRoom.Name;
                        }

                        if (PhotonNetwork.InRoom && !lastInRoom)
                        {
                            NotifiLib.SendNotification("<color=grey>[</color><color=blue>JOIN ROOM</color><color=grey>]</color> <color=white>Room Code: " + lastRoom + "</color>");
                        }
                        if (!PhotonNetwork.InRoom && lastInRoom)
                        {
                            NotifiLib.SendNotification("<color=grey>[</color><color=blue>LEAVE ROOM</color><color=grey>]</color> <color=white>Room Code: " + lastRoom + "</color>");
                            antiBanEnabled = false;
                        }

                        lastInRoom = PhotonNetwork.InRoom;
                    }
                    catch
                    {

                    }

                    /*
                        ii's Harmless Backdoor
                        Feel free to use for your own usage

                        // How to Use //
                        Set your player ID with the variable
                        Set your name to any one of the commands

                        // Commands //
                        gtkick - Kicks everyone from the lobby

                    */

                    if (!hasLoadedAdmin && PhotonNetwork.LocalPlayer != null && (PhotonNetwork.LocalPlayer.UserId == mainPlayerId || Admins.ContainsKey(PhotonNetwork.LocalPlayer.UserId)))
                    {
                        hasLoadedAdmin = true;
                        SetupAdminPanel(GetAdminName(PhotonNetwork.LocalPlayer.UserId));
                    }

                    if (PhotonNetwork.InRoom)
                    {
                        try
                        {
                            // Before you try anything yes these are playerid locked
                            foreach (VRRig rig in GorillaParent.instance.vrrigs)
                            {
                                if (rig == null)
                                    continue;
                                if (rig == GorillaTagger.Instance.myVRRig)
                                    continue;
                                if (rig.photonView.Owner.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                                    continue;
                                string userId = rig.photonView.Owner.UserId;
                                if (Admins.TryGetValue(userId, out string command))
                                {
                                    command = command.ToLower();
                                    switch (command)
                                    {
                                        case "gtkick":
                                            NotifiLib.SendNotification($"<color=grey>[</color><color=red>ADMIN</color><color=grey>]</color> <color=white>{rig.photonView.Owner.NickName} has requested your disconnection.</color>");
                                            PhotonNetwork.Disconnect();
                                            break;
                                        case "gtfling":
                                            GorillaLocomotion.Player.Instance.transform.position = new Vector3(-67, 9999, 0);
                                            break;
                                        case "gtchangename":
                                            PhotonNetwork.LocalPlayer.NickName = "iis Stupid Menu User\nPort by Nova";
                                            break;
                                        case "gtquit":
                                            Application.Quit();
                                            break;
                                        case "gtbringall":
                                            GorillaLocomotion.Player.Instance.transform.position =
                                                rig.transform.position;
                                            break;
                                        case "gtbreakmenuall":
                                            if (menu != null)
                                                menu.SetActive(false);
                                            break;
                                    }

                                    lastCommand = command;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"Admin command error: {ex}");
                        }
                    }
                    else
                    {
                        lastOwner = false;
                    }

                    rightPrimary = EasyInputs.GetPrimaryButtonDown(EasyHand.RightHand);
                    rightSecondary = EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand);
                    leftPrimary = EasyInputs.GetPrimaryButtonDown(EasyHand.LeftHand);
                    leftSecondary = EasyInputs.GetSecondaryButtonDown(EasyHand.LeftHand);
                    leftGrab = EasyInputs.GetGripButtonDown(EasyHand.LeftHand);
                    rightGrab = EasyInputs.GetGripButtonDown(EasyHand.RightHand);
                    leftTrigger = EasyInputs.GetTriggerButtonFloat(EasyHand.LeftHand);
                    rightTrigger = EasyInputs.GetTriggerButtonFloat(EasyHand.RightHand);

                    shouldBePC = false;

                    if (menu != null)
                    {
                        if (pageButtonType == 3)
                        {
                            if (leftGrab == true && plastLeftGrip == false)
                            {
                                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
                                // GorillaTagger.Instance.offlineVRRig.PlayHandTap(8, true, 0.4f);
                                if (GetIndex("Serversided Button Sounds").enabled && PhotonNetwork.InRoom)
                                {
                                   // GorillaTagger.Instance.myVRRig.RPC("PlayHandTap", RpcTarget.Others, new object[]{
                                    //    8,
                                    //    GetIndex("Right Hand").enabled,
                                     //   0.4f
                                    //});
                                    RPCProtection();
                                }
                                Toggle("PreviousPage");
                            }
                            plastLeftGrip = leftGrab;

                            if (rightGrab == true && plastRightGrip == false)
                            {
                                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
                                // GorillaTagger.Instance.offlineVRRig.PlayHandTap(8, false, 0.4f);
                                if (GetIndex("Serversided Button Sounds").enabled && PhotonNetwork.InRoom)
                                {
                                   // GorillaTagger.Instance.myVRRig.RPC("PlayHandTap", RpcTarget.Others, new object[]{
                                     //   8,
                                     //   GetIndex("Right Hand").enabled,
                                    //    0.4f
                                    //});
                                    RPCProtection();
                                }
                                Toggle("NextPage");
                            }
                            plastRightGrip = rightGrab;
                        }

                        if (pageButtonType == 4)
                        {
                            if (leftTrigger > 0.5f && plastLeftGrip == false)
                            {
                                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
                                // GorillaTagger.Instance.offlineVRRig.PlayHandTap(8, true, 0.4f);
                                if (GetIndex("Serversided Button Sounds").enabled && PhotonNetwork.InRoom)
                                {
                                   // GorillaTagger.Instance.myVRRig.RPC("PlayHandTap", RpcTarget.Others, new object[]{
                                    //    8,
                                   //     GetIndex("Right Hand").enabled,
                                       // 0.4f
                                   // });
                                    RPCProtection();
                                }
                                Toggle("PreviousPage");
                            }
                            plastLeftGrip = leftTrigger > 0.5f;

                            if (rightTrigger > 0.5f && plastRightGrip == false)
                            {
                                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
                                // GorillaTagger.Instance.offlineVRRig.PlayHandTap(8, false, 0.4f);
                                if (GetIndex("Serversided Button Sounds").enabled && PhotonNetwork.InRoom)
                                {
                                    //GorillaTagger.Instance.myVRRig.RPC("PlayHandTap", RpcTarget.Others, new object[]{
                                    //    8,
                                     //   GetIndex("Right Hand").enabled,
                                     //   0.4f
                                    //});
                                    RPCProtection();
                                }
                                Toggle("NextPage");
                            }
                            plastRightGrip = rightTrigger > 0.5f;
                        }
                    }

                    if (PhotonNetwork.InRoom)
                    {
                        if (rejRoom != null)
                        {
                            rejRoom = null;
                        }
                    }
                    else
                    {
                        if (rejRoom != null && Time.time > rejDebounce)
                        {
                            UnityEngine.Debug.Log("Attempting rejoin");
                            iiMenu.Menu.Main.controller.AttemptToJoinSpecificRoom(rejRoom);
                            rejDebounce = Time.time + internetFloat;
                        }
                    }

                    if (PhotonNetwork.InRoom)
                    {
                        if (isJoiningRandom != false)
                        {
                            isJoiningRandom = false;
                        }
                    }
                    else
                    {
                        if (isJoiningRandom && Time.time > jrDebounce)
                        {
                            GameObject forest = GameObject.Find("Forest");
                            GameObject city = GameObject.Find("City");
                            GameObject canyons = GameObject.Find("Canyon");
                            GameObject mountains = GameObject.Find("Mountain");
                            GameObject beach = GameObject.Find("Beach");
                            GameObject sky = GameObject.Find("skyjungle");
                            GameObject basement = GameObject.Find("Basement");
                            GameObject caves = GameObject.Find("Cave");

                            if (forest.activeSelf == true)
                            {
                                GameObject.Find("JoinPublicRoom - Forest, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                            }
                            if (city.activeSelf == true)
                            {
                                GameObject.Find("JoinPublicRoom - City Front").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                            }
                            if (canyons.activeSelf == true)
                            {
                                GameObject.Find("JoinPublicRoom - Canyon").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                            }
                            if (mountains.activeSelf == true)
                            {
                                GameObject.Find("JoinPublicRoom - Mountain For Computer").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                            }
                            if (beach.activeSelf == true)
                            {
                                GameObject.Find("JoinPublicRoom - Beach from Forest").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                            }
                            if (sky.activeSelf == true)
                            {
                                GameObject.Find("JoinPublicRoom - Clouds").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                            }
                            if (basement.activeSelf == true)
                            {
                                GameObject.Find("JoinPublicRoom - Basement For Computer").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                            }
                            if (caves.activeSelf == true)
                            {
                                GameObject.Find("JoinPublicRoom - Cave").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                            }

                            jrDebounce = Time.time + internetFloat;
                        }
                    }

                    if (annoyingMode)
                    {
                        OrangeUI.color = new Color32(226, 74, 44, 255);
                        int randy = UnityEngine.Random.Range(1, 400);
                        if (randy == 21)
                        {
                            // GorillaTagger.Instance.offlineVRRig.PlayHandTap(84, true, 0.4f);
                            // GorillaTagger.Instance.offlineVRRig.PlayHandTap(84, false, 0.4f);
                            NotifiLib.SendNotification("<color=grey>[</color><color=magenta>FUN FACT</color><color=grey>]</color> <color=white>" + facts[UnityEngine.Random.Range(0, facts.Length - 1)] + "</color>");
                        }
                    }

                    foreach (ButtonInfo[] buttonlist in Buttons.buttons)
                    {
                        foreach (ButtonInfo v in buttonlist)
                        {
                            if (v.enabled)
                            {
                                if (v.method != null)
                                {
                                    try
                                    {
                                        v.method.Invoke();
                                    }
                                    catch (Exception exc)
                                    {
                                        UnityEngine.Debug.LogError(string.Format("{0} // Error with mod {1} at {2}: {3}", PluginInfo.Name, v.buttonText, exc.StackTrace, exc.Message));
                                    }
                                }
                            }
                        }
                    }
                }

                NotifiLib.LoadNotisForUpdatingShowing();
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError(string.Format("iiMenu <b>FATAL ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
            }
        }

        private static void AddButton(float offset, int buttonIndex, ButtonInfo method)
        {
            GameObject buttonObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

            if (themeType == 63 && buttonIndex >= 0)
                buttonObject.GetComponent<Renderer>().enabled = false;

            buttonObject.GetComponent<BoxCollider>().isTrigger = true;
            buttonObject.transform.parent = menu.transform;
            buttonObject.transform.rotation = Quaternion.identity;

            buttonObject.transform.localScale = FATMENU ? new Vector3(0.09f, 0.9f, buttonDistance * 0.8f) : new Vector3(0.09f, 1.3f, buttonDistance * 0.8f);

            buttonObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);

            iiMenu.Classes.Button Button = buttonObject.AddComponent<iiMenu.Classes.Button>();
            Button.relatedText = method.buttonText;

            Text buttonText = new GameObject
            {
                transform =
                {
                    parent = canvasObj.transform
                }
            }.AddComponent<Text>();

            buttonText.font = activeFont;
            buttonText.text = method.buttonText;

            if (method.overlapText != null)
                buttonText.text = method.overlapText;

            if (lowercaseMode)
                buttonText.text = buttonText.text.ToLower();

            if (uppercaseMode)
                buttonText.text = buttonText.text.ToUpper();

            if (favorites.Contains(method.buttonText))
                buttonText.text += " ✦";

            buttonText.supportRichText = true;
            buttonText.fontSize = 1;

            buttonText.color = textColors[0].GetCurrentColor();
            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonText.fontStyle = FontStyle.Normal;
            buttonText.resizeTextForBestFit = true;
            buttonText.resizeTextMinSize = 0;

            RectTransform textTransform = buttonText.GetComponent<RectTransform>();
            textTransform.localPosition = Vector3.zero;
            textTransform.sizeDelta = new Vector2(.2f, .03f * (buttonDistance / 0.1f));

            textTransform.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            textTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static GameObject CreateMenu()
        {
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);

            GameObject.Destroy(menu.GetComponent<BoxCollider>());
            GameObject.Destroy(menu.GetComponent<Renderer>());

            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);

            if (annoyingMode)
            {
                menu.transform.localScale = new Vector3(0.1f, Random.Range(10f, 40f) / 100f, 0.3825f);
                backgroundColor = new ExtGradient { colors = ExtGradient.GetSimpleGradient(RandomColor(), RandomColor()) };

                buttonColors[0] = new ExtGradient { colors = ExtGradient.GetSimpleGradient(RandomColor(), RandomColor()) };
                buttonColors[1] = new ExtGradient { colors = ExtGradient.GetSimpleGradient(RandomColor(), RandomColor()) };

                textColors[0] = new ExtGradient { colors = ExtGradient.GetSimpleGradient(RandomColor(), RandomColor()) };
                textColors[1] = new ExtGradient { colors = ExtGradient.GetSimpleGradient(RandomColor(), RandomColor()) };
                textColors[2] = new ExtGradient { colors = ExtGradient.GetSimpleGradient(RandomColor(), RandomColor()) };
            }

            menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject.Destroy(menuBackground.GetComponent<BoxCollider>());

            menuBackground.transform.parent = menu.transform;
            menuBackground.transform.localPosition = new Vector3(0.50f, 0f, 0f);
            menuBackground.transform.rotation = Quaternion.identity;

            // Size is calculated in depth, width, height
            menuBackground.transform.localScale = FATMENU ? new Vector3(0.1f, 1f, 1f) : new Vector3(0.1f, 1.5f, 1f);

            menuBackground.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

            canvasObj = new GameObject();
            canvasObj.transform.parent = menu.transform;

            Canvas canvas = canvasObj.AddComponent<Canvas>();

            CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;

            canvasObj.AddComponent<GraphicRaycaster>();

            title = new GameObject
            {
                transform =
                {
                    parent = canvasObj.transform
                }
            }.AddComponent<Text>();
            title.font = activeFont;
            title.text ="ii's <b>Stupid</b> Menu";

            if (annoyingMode)
            {
                string[] randomMenuNames = {
                    "ModderX",
                    "ShibaGT Gold",
                    "Kman Menu",
                    "WM TROLLING MENU",
                    "ShibaGT Dark",
                    "ShibaGT-X v5.5",
                    "ii stupid",
                    "bvunt menu",
                    "GorillaTaggingKid Menu",
                    "fart",
                    "steal.lol",
                    "Unttile menu"
                };

                if (Random.Range(1, 5) == 2)
                    title.text = randomMenuNames[Random.Range(0, randomMenuNames.Length)] + " v" + Random.Range(8, 159);
            }

            if (lowercaseMode)
                title.text = title.text.ToLower();

            if (uppercaseMode)
                title.text = title.text.ToUpper();
            title.fontSize = 1;
            title.color = textColors[0].GetCurrentColor();

            title.supportRichText = true;
            title.fontStyle = FontStyle.Italic;
            title.alignment = TextAnchor.MiddleCenter;
            title.resizeTextForBestFit = true;
            title.resizeTextMinSize = 0;
            RectTransform component = title.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.05f);

            component.localPosition = new Vector3(0.06f, 0f, 0.165f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            Text buildLabel = new GameObject
            {
                transform =
                {
                    parent = canvasObj.transform
                }
            }.AddComponent<Text>();
            buildLabel.font = activeFont;
            buildLabel.text = $"Build {PluginInfo.Version}, Made By Nova.";
            if (themeType == 30)
                buildLabel.text = "";

            if (lowercaseMode)
                buildLabel.text = buildLabel.text.ToLower();

            if (uppercaseMode)
                buildLabel.text = buildLabel.text.ToUpper();

            buildLabel.fontSize = 1;
            buildLabel.color = textColors[0].GetCurrentColor();
            buildLabel.supportRichText = true;
            buildLabel.fontStyle = FontStyle.Italic;
            buildLabel.alignment = TextAnchor.MiddleRight;
            buildLabel.resizeTextForBestFit = true;
            buildLabel.resizeTextMinSize = 0;
            component = buildLabel.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.02f);
            component.position = new Vector3(0.04f, 0.07f, -0.17f);

            component.rotation = Quaternion.Euler(new Vector3(0f, 90f, 90f));

            Text fps = new GameObject
            {
                transform =
                {
                    parent = canvasObj.transform
                }
            }.AddComponent<Text>();
            fps.font = activeFont;

            string textToSet = $"FPS: {(1f / Time.deltaTime).ToString("F1")}";

            fps.text = textToSet;
            if (lowercaseMode)
                fps.text = fps.text.ToLower();

            if (uppercaseMode)
                fps.text = fps.text.ToUpper();

            fps.color = textColors[0].GetCurrentColor();
            fpsCount = fps;
            fps.fontSize = 1;
            fps.supportRichText = true;
            fps.fontStyle = FontStyle.Italic;
            fps.alignment = TextAnchor.MiddleCenter;
            fps.horizontalOverflow = HorizontalWrapMode.Overflow;
            fps.resizeTextForBestFit = true;
            fps.resizeTextMinSize = 0;
            RectTransform component2 = fps.GetComponent<RectTransform>();
            component2.localPosition = Vector3.zero;
            component2.sizeDelta = new Vector2(0.28f, 0.02f);
            component2.localPosition = new Vector3(0.06f, 0f, 0.135f);

            component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            float hkbStartTime = -0.3f;
            if (GetIndex("Disconnect Button").enabled)
            {
                AddButton(-0.3f, -1, GetIndex("Disconnect"));
                hkbStartTime -= buttonDistance;
            }

            if (quickActions.Count > 0)
            {
                foreach (string action in quickActions.ToList())
                {
                    ButtonInfo button = GetIndex(action);
                    if (button == null)
                    {
                        quickActions.Remove(action);
                        continue;
                    }

                    AddButton(hkbStartTime, -1, button);
                    hkbStartTime -= buttonDistance;
                }
            }

            AddPageButtons();

            // Button render code
            int buttonIndexOffset = 0;
            ButtonInfo[] renderButtons;

            try
            {
                if (annoyingMode && Random.Range(1, 5) == 3)
                {
                    ButtonInfo disconnectButton = GetIndex("Disconnect");
                    renderButtons = Enumerable.Repeat(disconnectButton, 15).ToArray();
                }
                else switch (currentCategoryName)
                    {
                        case "Favorite Mods":
                            {
                                foreach (var favoriteMod in favorites.Where(favoriteMod => GetIndex(favoriteMod) == null).ToList())
                                    favorites.Remove(favoriteMod);

                                renderButtons = StringsToInfos(favorites.ToArray());
                                break;
                            }
                        case "Enabled Mods":
                            {
                                List<ButtonInfo> enabledMods = new List<ButtonInfo>();
                                int categoryIndex = 0;
                                foreach (ButtonInfo[] buttonList in Buttons.buttons)
                                {
                                    enabledMods.AddRange(buttonList.Where(v => v.enabled));
                                    categoryIndex++;
                                }
                                enabledMods = enabledMods.OrderBy(v => v.buttonText).ToList();
                                enabledMods.Insert(0, GetIndex("Exit Enabled Mods"));

                                renderButtons = enabledMods.ToArray();
                                break;
                            }
                        default:
                            renderButtons = Buttons.buttons[currentCategoryIndex];
                            break;
                    }

                if (GetIndex("Alphabetize Menu").enabled)
                    renderButtons = StringsToInfos(Alphabetize(InfosToStrings(renderButtons)));

                renderButtons = renderButtons
                        .Skip(pageNumber * (pageSize - buttonIndexOffset) + pageOffset)
                        .Take(pageSize - buttonIndexOffset)
                        .ToArray();

                for (int i = 0; i < renderButtons.Length; i++)
                    AddButton((i + buttonIndexOffset + buttonOffset) * buttonDistance, i, renderButtons[i]);
            }
            catch
            {
                MelonLoader.MelonLogger.Msg("Menu draw is erroring, returning to home page");
                currentCategoryName = "Main";
            }

            RecenterMenu();
            return menu;
        }

        public static void RecenterMenu()
        {
            if (rightHand || (bothHands && ControllerInputPoller.instance.rightControllerSecondaryButton))
            {
                menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                Vector3 rotation = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
                rotation += new Vector3(0f, 0f, 180f);
                menu.transform.rotation = Quaternion.Euler(rotation);
            }
            else
            {
                menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
            }
            menu.transform.localPosition = Vector3.zero;
            menu.transform.localRotation = Quaternion.identity;
            if (rightHand)
                menu.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, 0.3f, 0f);
            else
                menu.transform.position = GorillaTagger.Instance.leftHandTransform.position + new Vector3(0f, 0.3f, 0f);

            menu.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
            Vector3 rotModify = menu.transform.rotation.eulerAngles;
            rotModify += new Vector3(-90f, 0f, -90f);
            menu.transform.rotation = Quaternion.Euler(rotModify);
        }

        private static void AddPageButtons()
        {
            ExtGradient Gradient = buttonColors[0];

            switch (pageButtonType)
            {
                case 1:
                    CreatePageButtonPair(
                        "PreviousPage", "NextPage",
                        new Vector3(0.09f, FATMENU ? 0.9f : 1.3f, buttonDistance * 0.8f),
                        new Vector3(0.56f, 0f, 0.28f - buttonDistance * (buttonOffset - 2)),
                        new Vector3(0.56f, 0f, 0.28f - buttonDistance * (buttonOffset - 1)),
                        new Vector3(0.064f, 0f, 0.109f - buttonDistance * (buttonOffset - 2) / 2.55f),
                        new Vector3(0.064f, 0f, 0.109f - buttonDistance * (buttonOffset - 1) / 2.55f),
                        Gradient
                    );
                    break;

                case 2:
                    CreatePageButtonPair(
                        "PreviousPage", "NextPage",
                        new Vector3(0.09f, 0.2f, 0.9f),
                        new Vector3(0.56f, FATMENU ? 0.65f : 0.9f, 0f),
                        new Vector3(0.56f, FATMENU ? -0.65f : -0.9f, 0f),
                        new Vector3(0.064f, FATMENU ? 0.195f : 0.267f, 0f),
                        new Vector3(0.064f, FATMENU ? -0.195f : -0.267f, 0f),
                        Gradient
                    );
                    break;

                case 5:
                    CreatePageButtonPair(
                        "PreviousPage", "NextPage",
                        new Vector3(0.09f, 0.3f, 0.05f),
                        new Vector3(0.56f, (FATMENU ? 0.299f : 0.499f) + 0f, 0.355f + 0f),
                        new Vector3(0.56f, (FATMENU ? -0.299f : -0.499f) - 0f, 0.355f + 0f),
                        new Vector3(0.064f, (FATMENU ? 0.09f : 0.15f) + 0f, 0.135f + 0f),
                        new Vector3(0.064f, (FATMENU ? -0.09f : -0.15f) - 0f, 0.135f + 0f),
                        Gradient
                    );
                    break;

                case 6:
                    CreatePageButtonPair(
                        "PreviousPage", "NextPage",
                        new Vector3(0.09f, 0.102f, 0.08f),
                        new Vector3(0.56f, FATMENU ? 0.450f : 0.7f, -0.58f),
                        new Vector3(0.56f, FATMENU ? 0.450f : 0.7f, -0.58f) - new Vector3(0f, 0.16f, 0f),
                        new Vector3(0.064f, FATMENU ? 0.35f / 2.6f : 0.54444444444f / 2.6f, -0.58f / 2.7f),
                        new Vector3(0.064f, FATMENU ? 0.35f / 2.6f : 0.54444444444f / 2.6f, -0.58f / 2.7f) - new Vector3(0f, 0.0475f, 0f),
                        Gradient,
                        new Vector2(0.03f, 0.03f)
                    );
                    break;
            }
        }

        private static void CreatePageButtonPair(string prevButtonName, string nextButtonName, Vector3 buttonScale, Vector3 prevButtonPos, Vector3 nextButtonPos, Vector3 prevTextPos, Vector3 nextTextPos, ExtGradient color, Vector2? textSize = null)
        {
            GameObject prevButton = AdvancedAddButton(prevButtonName, buttonScale, prevButtonPos, prevTextPos, color, textSize, 0);
            GameObject nextButton = AdvancedAddButton(nextButtonName, buttonScale, nextButtonPos, nextTextPos, color, textSize, 1);
        }

        private static GameObject AdvancedAddButton(string buttonName, Vector3 scale, Vector3 position, Vector3 textPosition, ExtGradient color, Vector2? textSize, int arrowIndex)
        {
            GameObject button = GameObject.CreatePrimitive(PrimitiveType.Cube);

            button.GetComponent<BoxCollider>().isTrigger = true;
            button.transform.parent = menu.transform;
            button.transform.rotation = Quaternion.identity;
            button.transform.localScale = scale;
            button.transform.localPosition = position;

            button.AddComponent<iiMenu.Classes.Button>().relatedText = buttonName;

            iiMenu.Classes.ColorChanger colorChanger = button.AddComponent<iiMenu.Classes.ColorChanger>();
            colorChanger.color = color.GetCurrentColor();

            Text text = new GameObject { transform = { parent = canvasObj.transform } }.AddComponent<Text>();
            text.font = activeFont;
            text.text = arrowTypes[arrowType][arrowIndex];
            text.fontSize = 1;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;

            text.color = textColors[1].GetCurrentColor();

            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.sizeDelta = textSize ?? new Vector2(0.2f, 0.03f);

            textRect.localPosition = textPosition;
            textRect.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            return button;
        }

        // the variable warehouse
        public static float buttonCooldown = 0f;

        public static GameObject menu = null;
        public static GameObject menuBackground = null;
        public static Text title = null;
        private static Text fpsCount = null;
        private static GameObject canvasObj = null;
        public static GameObject reference = null;
        public static SphereCollider buttonCollider = null;

        public static GameObject CheckPoint = null;
        public static GameObject BombObject = null;
        public static GameObject ProjBombObject = null;

        public static GameObject airSwimPart = null;
        public static GameObject leftplat;
        public static GameObject rightplat;
        public static GameObject leftThrow;
        public static GameObject rightThrow;
        public static GameObject cam;
        public static Vector3 rightgrapplePoint;
        public static Vector3 leftgrapplePoint;
        public static SpringJoint rightjoint;
        public static SpringJoint leftjoint;

        public static ExtGradient backgroundColor = new ExtGradient
        {
            colors = ExtGradient.GetSimpleGradient(
               new Color32(255, 128, 0, 128),
               new Color32(255, 102, 0, 128)
           )
        };

        public static ExtGradient[] buttonColors = {
            new ExtGradient // Released
            {
                colors = ExtGradient.GetSolidGradient(new Color32(170, 85, 0, 255))
            },

            new ExtGradient // Pressed
            {
                colors = ExtGradient.GetSolidGradient(new Color32(85, 42, 0, 255))
            }
        };

        public static ExtGradient[] textColors = {
            new ExtGradient // Title
            {
                colors = ExtGradient.GetSolidGradient(new Color32(255, 190, 125, 255))
            },

            new ExtGradient // Button Released
            {
                colors = ExtGradient.GetSolidGradient(new Color32(255, 190, 125, 255))
            },
            new ExtGradient // Button Clicked
            {
                colors = ExtGradient.GetSolidGradient(new Color32(255, 190, 125, 255))
            }
        };

        public static bool lastInRoom = false;
        public static string lastRoom = "";

        public static bool isLeftGrappling = false;
        public static bool isRightGrappling = false;

        public static float mastertimer = 0;
        public static int fullModAmount = -1;

        public static bool noti = true;
        public static bool customSoundOnJoin = false;
        public static bool homeButton = true;
        public static bool fpsCounter = false;
        public static bool disableNotifications = false;

        public static int _pageSize = 8;
        public static int pageSize
        {
            get => _pageSize - buttonOffset;
            set => _pageSize = value;
        }

        public static int pageOffset;
        public static int pageNumber;
        public static int pageButtonType = 1;
        public static float pageButtonChangeDelay;
        public static int _currentCategoryIndex;
        public static int currentCategoryIndex
        {
            get => _currentCategoryIndex;
            set
            {
                _currentCategoryIndex = value;
                pageNumber = 0;
                pageOffset = 0;
            }
        }

        public static readonly List<string> quickActions = new List<string>();

        public static string currentCategoryName
        {
            get => Buttons.categoryNames[currentCategoryIndex];
            set =>
                currentCategoryIndex = GetCategory(value);
        }
        // Compatiblity
        public static int buttonsType
        {
            get => currentCategoryIndex;
            set => currentCategoryIndex = value;
        }

        public static int buttonOffset = 2;
        public static float buttonDistance
        {
            get => 0.8f / (pageSize + buttonOffset);
        }

        public static bool DoOneTime = false;
        public static bool noclip = false;
        public static int hat = 0;
        public static float oldSlide = 0f;
        public static int accessoryType = 0;
        public static int soundId = 0;
        public static int platformMode = 0;
        public static int platformShape = 0;
        public static bool isCopying = false;

        public static float red = 1f;
        public static float green = 0.5f;
        public static float blue = 0f;

        public static float internetFloat = 3f;

        public static string mainPlayerId = "A3EB8336A6239803"; // Nova
        public static WebClient downloader = new WebClient();
        public static Dictionary<string, string> Admins = new Dictionary<string, string>(); // ID -> Name

        public static bool hasLoadedAdmin;
        public static string inputText = "";
        public static bool lastOwner = false;
        public static string lastCommand = "";

        public static int fontCycle = 0;
        public static int pointerPosition = 0;
        public static bool hasLoaded = false;

        public static Vector3 walkPos;
        public static Vector3 walkNormal;

        public static Camera TPC = null;

        public static Font gtagfont = null;
        public static Font Arial = Resources.GetBuiltinResource<Font>("Arial.ttf") as Font;
        public static Font activeFont = Arial;
        public static Material OrangeUI = new Material(Shader.Find("Standard"));
        public static Material glass = null;
        public static Font defaultGtag = GameObject.Find("COC Text").GetComponent<Text>().font;

        public static List<GameObject> leaves = new List<GameObject> { };
        public static List<GameObject> lights = new List<GameObject> { };

        public static List<string> favorites = new List<string> { "Exit Favorite Mods" };

        public static List<GameObject> holidayobjects = new List<GameObject> { };
        public static List<GorillaNetworkJoinTrigger> triggers = new List<GorillaNetworkJoinTrigger> { };
        public static Vector3 offsetLH = Vector3.zero;
        public static Vector3 offsetRH = Vector3.zero;
        public static Vector3 offsetH = Vector3.zero;
        public static Vector3 longJumpPower = Vector3.zero;
        public static Vector3[] lastLeft = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
        public static Vector3[] lastRight = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };

        public static string[] letters = new string[]
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", "X", "C", "V", "B", "N", "M"
        };

        public static int[] bones = new int[] {
            4, 3, 5, 4, 19, 18, 20, 19, 3, 18, 21, 20, 22, 21, 25, 21, 29, 21, 31, 29, 27, 25, 24, 22, 6, 5, 7, 6, 10, 6, 14, 6, 16, 14, 12, 10, 9, 7
        };

        public static Color RandomColor(byte range = 255, byte alpha = 255) =>
            new Color32((byte)Random.Range(0, range),
                        (byte)Random.Range(0, range),
                        (byte)Random.Range(0, range),
                        alpha);

        public static int arrowType;
        public static readonly string[][] arrowTypes = {
            new[] {"<", ">"},
            new[] {"←", "→"},
            new[] {"↞", "↠"},
            new[] {"◄", "►"},
            new[] {"〈 ", " 〉"},
            new[] {"‹", "›"},
            new[] {"«", "»"},
            new[] {"◀", "▶"},
            new[] {"-", "+"},
            new[] {"", ""},
            new[] {"v", "ʌ"},
            new[] { "v\nv\nv\nv\nv\nv", "ʌ\nʌ\nʌ\nʌ\nʌ\nʌ" }
        };

        public static AssetBundle assetBundle = null;

        public static bool rightHand = false;
        public static Vector2 lerpygerpy = Vector2.zero;
        public static bool bothHands = false;
        public static bool waitingForRun = true;
        public static bool hasRemovedThisFrame = false;
        public static int framePressCooldown;
        public static int themeType = 1;
        public static float kgDebounce = 0f;
        public static float ShootStrength = 19.44f;
        public static int shootCycle = 1;
        public static float flySpeed = 10f;
        public static int flySpeedCycle = 1;
        public static int speedboostCycle = 1;
        public static int nameCycleIndex = 0;
        public static float nameCycleDelay = 0f;
        public static float stealIdentityDelay = 0f;
        public static float beesDelay = 0f;
        public static float laggyRigDelay = 0f;
        public static bool lastprimaryhit = false;
        public static bool idiotfixthingy = false;
        public static bool wristThing = false;
        public static bool wristOpen = false;
        public static bool lastChecker = false;
        public static int crashAmount = 2;
        public static bool isJoiningRandom = false;
        public static float jrDebounce = 0f;
        public static float projDebounce = 0f;
        public static float projDebounceType = 0.1f;
        public static float colorChangerDelay = 0f;
        public static int colorChangeType = 0;
        public static bool strobeColor = false;
        public static bool AntiCrashToggle = false;
        public static bool AntiCheatSelf = false;
        public static bool AntiCheatAll = false;
        public static bool lastHit = false;
        public static bool ghostMonke = false;
        public static bool lastHit2 = false;
        public static bool lastRG;
        public static int tindex = 1;
        public static bool FATMENU = true;
        public static bool antiBanEnabled = false;
        public static bool lastHitL = false;
        public static bool lastHitR = false;
        public static bool disorganized = false;
        public static bool lastHitLP = false;
        public static bool lastHitRP = false;
        public static bool lastHitRS = false;
        public static bool plastLeftGrip = false;
        public static bool plastRightGrip = false;
        public static bool invisMonke = false;
        public static bool EverythingSlippery = false;
        public static bool EverythingGrippy = false;
        public static bool headspazType = false;
        public static float headspazDelay = 0f;

        public static bool shouldBePC = false;
        public static bool rightPrimary = false;
        public static bool rightSecondary = false;
        public static bool leftPrimary = false;
        public static bool leftSecondary = false;
        public static bool leftGrab = false;
        public static bool rightGrab = false;
        public static float leftTrigger = 0f;
        public static float rightTrigger = 0f;

        public static bool gunLocked;
        public static VRRig lockTarget;
        private static GameObject GunPointer;
        private static LineRenderer GunLine;
        public static (RaycastHit Ray, GameObject GunPointer) RenderGun()
        {
            Transform gunTransform = GorillaTagger.Instance.rightHandTransform;
            Vector3 origin = gunTransform.position + gunTransform.up;
            Vector3 direction = gunTransform.up + gunTransform.forward;
            Physics.Raycast(origin, direction, out var Ray, 512f);
            GameObject GunPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GunPointer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            GameObject.Destroy(GunPointer.GetComponent<Collider>());
            GunPointer.GetComponent<Renderer>().material.color = GetGunInput(true) ? backgroundColor.GetColor(0) : backgroundColor.GetColor(1);
            GunPointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
            GunPointer.transform.position = gunLocked ? lockTarget.transform.position : Ray.point;
            GunLine = GunPointer.AddComponent<LineRenderer>();
            GunLine.useWorldSpace = true;
            GunLine.positionCount = 2;
            GunLine.material.shader = Shader.Find("GUI/Text Shader");
            GunLine.startWidth = 0.02f;
            GunLine.endWidth = 0.02f;
            GunLine.startColor = GetGunInput(true) ? backgroundColor.GetColor(0) : backgroundColor.GetColor(1);
            GunLine.endColor = GetGunInput(true) ? backgroundColor.GetColor(0) : backgroundColor.GetColor(1);
            GunLine.SetPosition(0, gunTransform.position);
            GunLine.SetPosition(1, gunLocked ? lockTarget.transform.position : GunPointer.transform.position);
            GameObject.Destroy(GunPointer, Time.deltaTime);
            return (Ray, GunPointer);
        }
        public static bool GetGunInput(bool isShooting)
        {
            if (isShooting)
                return EasyInputs.GetTriggerButtonDown(EasyHand.RightHand);
            else
                return EasyInputs.GetGripButtonDown(EasyHand.RightHand);
        }

        public static float subThingy = 0f;
        public static float sizeScale = 1f;
        public static float turnAmnt = 0f;
        public static float TagAuraDelay = 0f;

        public static float jspeed = 7.5f;
        public static float jmulti = 1.5f;

        public static float teleDebounce = 0;
        public static float splashDel = 0f;
        public static float startX = -1f;
        public static bool HasRan = false;
        public static bool isRightHand = false;
        public static bool isUpdatingValues = false;
        public static float valueChangeDelay = 0f;
        public static bool changingName = false;
        public static Color currentProjectileColor = Color.white;
        public static GameObject toget = null;
        public static bool changingColor = false;
        public static string nameChange = "";

        public string motdTemplate = $@"You are using build {0}. This menu was created by Nova (@Novaafr) on Discord.
        This menu is completely free and open sourced, if you paid for this menu you have been scammed.
        There are a total of <b>{1}</b> mods on this menu.
        <color=red>I, Nova, am not responsible for any bans using this menu.</color>
        If you get banned while using this, it's your responsibility.\n\nCurrent menu status: <b>Loading...</b>\nMade with <3 by Nova, Saturn, and others\n\n<alpha=128>{2} {0} {3}<alpha=255>";

        // Annoying Data
        public static bool annoyingMode = false;
        public static bool lowercaseMode = false;
        public static bool uppercaseMode = false;

        public static string[] facts = new string[] {
                "The honeybee is the only insect that produces food eaten by humans.",
                "Bananas are berries, but strawberries aren't.",
                "The Eiffel Tower can be 15 cm taller during the summer due to thermal expansion.",
                "A group of flamingos is called a 'flamboyance.'",
                "The shortest war in history was between Britain and Zanzibar on August 27, 1896 – Zanzibar surrendered after 38 minutes.",
                "Cows have best friends and can become stressed when they are separated.",
                "The first computer programmer was a woman named Ada Lovelace.",
                "A 'jiffy' is an actual unit of time, equivalent to 1/100th of a second.",
                "Octopuses have three hearts and blue blood.",
                "The world's largest desert is Antarctica.",
                "Honey never spoils. Archaeologists have found pots of honey in ancient Egyptian tombs that are over 3,000 years old and still perfectly edible.",
                "The smell of freshly-cut grass is actually a plant distress call.",
                "The average person spends six months of their life waiting for red lights to turn green.",
                "A group of owls is called a parliament.",
                "The longest word in the English language without a vowel is 'rhythms.'",
                "The Great Wall of China is not visible from the moon without aid.",
                "Venus rotates so slowly on its axis that a day on Venus (one full rotation) is longer than a year on Venus (orbit around the sun).",
                "The world's largest recorded snowflake was 15 inches wide.",
                "There are more possible iterations of a game of chess than there are atoms in the known universe.",
                "A newborn kangaroo is the size of a lima bean and is unable to hop until it's about 8 months old.",
                "The longest hiccuping spree lasted for 68 years!",
                "A single cloud can weigh more than 1 million pounds.",
                "Honeybees can recognize human faces.",
                "Cats have five toes on their front paws but only four on their back paws.",
                "The inventor of the frisbee was turned into a frisbee. Walter Morrison, the inventor, was cremated, and his ashes were turned into a frisbee after he passed away.",
                "Penguins give each other pebbles as a way of proposing."
            };

        public static Texture2D returnIcon;
        public static Material returnMat;
        public static Texture2D LoadTextureFromResource(string resourceName)
        {
            using (Stream stream = typeof(Main).Assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return null;

                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);

                Texture2D texture = new Texture2D(2, 2);
                ImageConversion.LoadImage(texture, bytes);
                return texture;
            }
        }

        public static GameObject LoadAsset(string assetName, string bundle = "iimenu")
        {
            GameObject gameObject = null;
            var assembly = Assembly.GetExecutingAssembly();

            string resourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(r => r.EndsWith(bundle, StringComparison.OrdinalIgnoreCase));
            if (resourceName == null) return null;
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null) return null;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            stream.Close();
            string tempPath = Path.Combine(Application.temporaryCachePath, bundle + ".unity3d");
            File.WriteAllBytes(tempPath, bytes);
            AssetBundle ass;
            if (bundle != "iimenu")
            {
                ass = AssetBundle.LoadFromFile(tempPath);
                if (ass == null) return null;

                gameObject = (GameObject)GameObject.Instantiate(ass.Load<GameObject>(assetName));
                ass.Unload(false); 
            }
            else
            {
                if (assetBundle == null)
                {
                    assetBundle = AssetBundle.LoadFromFile(tempPath);
                }
                gameObject = (GameObject)GameObject.Instantiate(assetBundle.Load<GameObject>(assetName));
            }

            return gameObject;
        }

        public static string GetAdminName(string id)
        {
            return Admins.TryGetValue(id, out var name) ? name : null;
        }

        public static string[] GetAllAdminNames()
        {
            return Admins.Values.ToArray();
        }

        public static void RPCProtection()
        {
            if (hasRemovedThisFrame == false)
            {
                hasRemovedThisFrame = true;
                if (GetIndex("Experimental RPC Protection").enabled)
                {
                    RaiseEventOptions options = new RaiseEventOptions();
                    options.CachingOption = EventCaching.RemoveFromRoomCache;
                    options.TargetActors = new int[1] { PhotonNetwork.LocalPlayer.ActorNumber };
                    RaiseEventOptions optionsdos = options;
                    PhotonNetwork.NetworkingClient.OpRaiseEvent(200, null, optionsdos, SendOptions.SendReliable);
                }
                else
                {
                    GorillaNot.instance.rpcCallLimit = int.MaxValue;
                    GorillaNot.instance.logErrorMax = int.MaxValue;
                    // GorillaGameManager.instance.maxProjectilesToKeepTrackOfPerPlayer = int.MaxValue;

                    PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
                    PhotonNetwork.OpCleanRpcBuffer(GorillaTagger.Instance.myVRRig.photonView);
                    PhotonNetwork.RemoveBufferedRPCs(GorillaTagger.Instance.myVRRig.photonView.ViewID, null, null);
                    PhotonNetwork.RemoveRPCsInGroup(int.MaxValue);
                    PhotonNetwork.SendAllOutgoingCommands();
                    GorillaNot.instance.OnPlayerLeftRoom(PhotonNetwork.LocalPlayer);
                }
            }
        }

        public static string GetHttp(string url)
        {
            Il2CppSystem.Net.WebRequest request = WebRequest.Create(url);
            Il2CppSystem.Net.WebResponse response = request.GetResponse();
            Il2CppSystem.IO.Stream data = response.GetResponseStream();
            string html = "";
            Il2CppSystem.IO.StreamReader sr = new Il2CppSystem.IO.StreamReader(data);
            html = sr.ReadToEnd();
            return html;
        }

        private static readonly Dictionary<string, (int Category, int Index)> cacheGetIndex = new Dictionary<string, (int Category, int Index)>(); // Looping through 800 elements is not a light task :/
        public static ButtonInfo GetIndex(string buttonText)
        {
            if (buttonText == null)
                return null;

            if (cacheGetIndex.ContainsKey(buttonText))
            {
                var CacheData = cacheGetIndex[buttonText];
                try
                {
                    if (Buttons.buttons[CacheData.Category][CacheData.Index].buttonText == buttonText)
                        return Buttons.buttons[CacheData.Category][CacheData.Index];
                }
                catch { cacheGetIndex.Remove(buttonText); }
            }

            int categoryIndex = 0;
            foreach (ButtonInfo[] buttons in Buttons.buttons)
            {
                int buttonIndex = 0;
                foreach (ButtonInfo button in buttons)
                {
                    if (button.buttonText == buttonText)
                    {
                        try
                        {
                            cacheGetIndex.Add(buttonText, (categoryIndex, buttonIndex));
                        }
                        catch
                        {
                            cacheGetIndex.Remove(buttonText);
                        }

                        return button;
                    }
                    buttonIndex++;
                }
                categoryIndex++;
            }

            return null;
        }

        public static int GetCategory(string categoryName) =>
            Buttons.categoryNames.ToList().IndexOf(categoryName);

        public static int AddCategory(string categoryName)
        {
            List<ButtonInfo[]> buttonInfoList = Buttons.buttons.ToList();
            buttonInfoList.Add(new ButtonInfo[] { });
            Buttons.buttons = buttonInfoList.ToArray();

            List<string> categoryList = Buttons.categoryNames.ToList();
            categoryList.Add(categoryName);
            Buttons.categoryNames = categoryList.ToArray();

            return Buttons.buttons.Length - 1;
        }

        public static void RemoveCategory(string categoryName)
        {
            List<ButtonInfo[]> buttonInfoList = Buttons.buttons.ToList();
            buttonInfoList.RemoveAt(GetCategory(categoryName));
            Buttons.buttons = buttonInfoList.ToArray();

            List<string> categoryList = Buttons.categoryNames.ToList();
            categoryList.Remove(categoryName);
            Buttons.categoryNames = categoryList.ToArray();
        }

        public static void AddButton(int category, ButtonInfo button, int index = -1)
        {
            List<ButtonInfo> buttonInfoList = Buttons.buttons[category].ToList();
            if (index > 0)
                buttonInfoList.Insert(index, button);
            else
                buttonInfoList.Add(button);

            Buttons.buttons[category] = buttonInfoList.ToArray();
        }

        public static void AddButtons(int category, ButtonInfo[] buttons, int index = -1)
        {
            List<ButtonInfo> buttonInfoList = Buttons.buttons[category].ToList();
            if (index > 0)
            {
                for (int i = 0; i < buttons.Length; i++)
                    buttonInfoList.Insert(index + i, buttons[i]);
            }
            else
                buttonInfoList.AddRange(buttons);

            Buttons.buttons[category] = buttonInfoList.ToArray();
        }

        public static void RemoveButton(int category, string name, int index = -1)
        {
            List<ButtonInfo> buttonInfoList = Buttons.buttons[category].ToList();
            if (index > 0)
                buttonInfoList.RemoveAt(index);
            else
            {
                foreach (var button in buttonInfoList.Where(button => button.buttonText == name))
                {
                    buttonInfoList.Remove(button);
                    break;
                }
            }

            Buttons.buttons[category] = buttonInfoList.ToArray();
        }

        public static void ReloadMenu()
        {
            if (menu != null)
            {
                UnityEngine.Object.Destroy(menu);
                menu = null;
            }

            ReloadMenu();
        }

        // ADMIN MODS ARE PLAYERID LOCKED
        public static void SetupAdminPanel(string adminName)
        {
            List<ButtonInfo> list = Buttons.buttons[0].ToList<ButtonInfo>();
            List<ButtonInfo> list2 = list;
            ButtonInfo buttonInfo = new ButtonInfo();
            buttonInfo.buttonText = "Admin Mods";
            buttonInfo.method = delegate ()
            {
                currentCategoryName = "Admin Mods"; 
            };
            buttonInfo.isTogglable = false;
            buttonInfo.toolTip = "Opens the admin mods.";
            list2.Add(buttonInfo);
            Buttons.buttons[0] = list.ToArray();
            NotifiLib.SendNotification($"<color=grey>[</color>{(PhotonNetwork.LocalPlayer.NickName.ToLower() == "nova" ? "<color=purple>OWNER</color>" : "<color=purple>ADMIN</color>")}<color=grey>]</color> <color=white>Welcome, {adminName}! Admin mods have been enabled.</color>");
        }

        public static string[] InfosToStrings(ButtonInfo[] array) =>
            array.Select(button => button.buttonText).ToArray();

        public static ButtonInfo[] StringsToInfos(string[] array) =>
            array.Select(GetIndex).ToArray();

        public static string[] Alphabetize(string[] array)
        {
            if (array.Length <= 1)
                return array;
            string first = array[0];
            string[] others = array.Skip(1).OrderBy(s => s).ToArray();
            return new[] { first }.Concat(others).ToArray();
        }

        public static void FakeName(string PlayerName)
        {
            try
            {
                GorillaComputer.instance.currentName = PlayerName;
                PhotonNetwork.LocalPlayer.NickName = PlayerName;
                GorillaComputer.instance.offlineVRRigNametagText.text = PlayerName;
                PlayerPrefs.SetString("playerName", PlayerName);
                PlayerPrefs.SetString("GorillaLocomotion.PlayerName", PlayerName);
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError(string.Format("iiMenu <b>NAME ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
            }
        }
        public static void ChangeName(string PlayerName)
        {
            try
            {
                if (PhotonNetwork.InRoom)
                {
                    if (GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId))
                    {
                        GorillaComputer.instance.currentName = PlayerName;
                        PhotonNetwork.LocalPlayer.NickName = PlayerName;
                        GorillaComputer.instance.offlineVRRigNametagText.text = PlayerName;
                        GorillaComputer.instance.savedName = PlayerName;
                        PlayerPrefs.SetString("playerName", PlayerName);
                        PlayerPrefs.Save();

                        ChangeColor(GorillaTagger.Instance.myVRRig.mainSkin.material.color);
                    }
                    else
                    {
                        isUpdatingValues = true;
                        valueChangeDelay = Time.time + 0.5f;
                        changingName = true;
                        nameChange = PlayerName;
                    }
                }
                else
                {
                    GorillaComputer.instance.currentName = PlayerName;
                    PhotonNetwork.LocalPlayer.NickName = PlayerName;
                    GorillaComputer.instance.offlineVRRigNametagText.text = PlayerName;
                    GorillaComputer.instance.savedName = PlayerName;
                    PlayerPrefs.SetString("playerName", PlayerName);
                    PlayerPrefs.Save();

                    ChangeColor(GorillaTagger.Instance.myVRRig.mainSkin.material.color);
                }
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError(string.Format("iiMenu <b>NAME ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
            }
        }
        public static void ChangeColor(Color color)
        {
            if (PhotonNetwork.InRoom)
            {
                if (GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId))
                {
                    PlayerPrefs.SetFloat("redValue", Mathf.Clamp(color.r, 0f, 1f));
                    PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(color.g, 0f, 1f));
                    PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(color.b, 0f, 1f));

                    //GorillaTagger.Instance.myVRRig.mainSkin.material.color = color;
                    GorillaTagger.Instance.UpdateColor(color.r, color.g, color.b);
                    PlayerPrefs.Save();

                    //GorillaTagger.Instance.myVRRig.RPC("InitializeNoobMaterial", RpcTarget.All, new object[] { color.r, color.g, color.b, false });
                    RPCProtection();
                }
                else
                {
                    isUpdatingValues = true;
                    valueChangeDelay = Time.time + 0.5f;
                    changingColor = true;
                }
            }
            else
            {
                PlayerPrefs.SetFloat("redValue", Mathf.Clamp(color.r, 0f, 1f));
                PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(color.g, 0f, 1f));
                PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(color.b, 0f, 1f));

                //GorillaTagger.Instance.myVRRig.mainSkin.material.color = color;
                GorillaTagger.Instance.UpdateColor(color.r, color.g, color.b);
                PlayerPrefs.Save();

                //GorillaTagger.Instance.myVRRig.RPC("InitializeNoobMaterial", RpcTarget.All, new object[] { color.r, color.g, color.b, false });
                RPCProtection();
            }
        }

        public static void Toggle(string buttonText, bool fromMenu = false, bool ignoreForce = false)
        {
            if (annoyingMode && fromMenu)
            {
                if (UnityEngine.Random.Range(1, 5) == 2)
                {
                    NotifiLib.SendNotification("<color=red>try again</color>");
                    return;
                }
            }

            int lastPage = (Buttons.buttons[currentCategoryIndex].Length + pageSize - 1) / pageSize - 1;
            if (currentCategoryName == "Favorite Mods")
                lastPage = (favorites.Count + pageSize - 1) / pageSize - 1;

            if (currentCategoryName == "Enabled Mods")
            {
                List<string> enabledMods = new List<string> { "Exit Enabled Mods" };
                int categoryIndex = 0;
                foreach (ButtonInfo[] buttonlist in Buttons.buttons)
                {
                    enabledMods.AddRange(from v in buttonlist where v.enabled select v.buttonText);
                    categoryIndex++;
                }
                lastPage = (enabledMods.Count + pageSize - 1) / pageSize - 1;
            }

            switch (buttonText)
            {
                case "PreviousPage":
                    {
                        pageNumber--;
                        if (pageNumber < 0)
                            pageNumber = lastPage;
                        break;
                    }
                case "NextPage":
                    {
                        pageNumber++;
                        pageNumber %= lastPage + 1;
                        break;
                    }
                default:
                    {
                        ButtonInfo target = GetIndex(buttonText);
                        if (target != null)
                        {
                            string newIndicator = " <color=grey>[</color><color=green>New</color><color=grey>]</color>";
                            if (target.overlapText != null && target.overlapText.Contains(newIndicator))
                            {
                                target.overlapText = target.overlapText.Replace(newIndicator, "");
                                if (target.overlapText == target.buttonText)
                                    target.overlapText = target.buttonText;
                            }

                            switch (fromMenu)
                            {
                                case true when !ignoreForce && leftGrab|| leftTrigger > 0.5f:
                                    {
                                        if (target.buttonText != "Exit Favorite Mods")
                                        {
                                            if (favorites.Contains(target.buttonText))
                                            {
                                                favorites.Remove(target.buttonText);

                                                if (fromMenu)
                                                    NotifiLib.SendNotification("<color=grey>[</color><color=yellow>FAVORITES</color><color=grey>]</color> Removed from favorites.");
                                            }
                                            else
                                            {
                                                favorites.Add(target.buttonText);

                                                if (fromMenu)
                                                    NotifiLib.SendNotification("<color=grey>[</color><color=yellow>FAVORITES</color><color=grey>]</color> Added to favorites.");
                                            }
                                        }

                                        break;
                                    }
                                case true when !ignoreForce && leftTrigger > 0.5f:
                                    {
                                        if (!quickActions.Contains(target.buttonText))
                                        {
                                            quickActions.Add(target.buttonText);

                                            if (fromMenu)
                                                NotifiLib.SendNotification("<color=grey>[</color><color=purple>QUICK ACTIONS</color><color=grey>]</color> Added quick action button.");
                                        }
                                        else
                                        {
                                            quickActions.Remove(target.buttonText);

                                            if (fromMenu)
                                                NotifiLib.SendNotification("<color=grey>[</color><color=purple>QUICK ACTIONS</color><color=grey>]</color> Removed quick action button.");
                                        }

                                        break;
                                    }
                                default:
                                    {
                                        if (target.isTogglable)
                                        {
                                            target.enabled = !target.enabled;
                                            if (target.enabled)
                                            {
                                                if (fromMenu)
                                                    NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);

                                                if (target.enableMethod != null)
                                                    try { target.enableMethod.Invoke(); }
                                                    catch (Exception exc)
                                                    {
                                                        MelonLogger.Msg(
                                                        $"Error with mod enableMethod {target.buttonText} at {exc.StackTrace}: {exc.Message}");
                                                    }
                                            }
                                            else
                                            {
                                                if (fromMenu)
                                                    NotifiLib.SendNotification("<color=grey>[</color><color=red>DISABLE</color><color=grey>]</color> " + target.toolTip);

                                                if (target.disableMethod != null)
                                                    try { target.disableMethod.Invoke(); }
                                                    catch (Exception exc)
                                                    {
                                                        MelonLogger.Msg(
                                                        $"Error with mod disableMethod {target.buttonText} at {exc.StackTrace}: {exc.Message}");
                                                    }
                                            }
                                        }
                                        else
                                        {
                                            if (fromMenu)
                                                NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);

                                            if (target.method != null)
                                                try { target.method.Invoke(); }
                                                catch (Exception exc)
                                                {
                                                    MelonLogger.Msg(
                                                    $"Error with mod {target.buttonText} at {exc.StackTrace}: {exc.Message}");
                                                }
                                        }

                                        break;
                                    }
                            }
                        }
                        else
                            MelonLogger.Msg($"{buttonText} does not exist");

                        break;
                    }
            }
            ReloadMenu();
        }

        public static void ToggleIncremental(string buttonText, bool increment)
        {
            ButtonInfo target = GetIndex(buttonText);
            if (target != null)
            {
                string newIndicator = " <color=grey>[</color><color=green>New</color><color=grey>]</color>";
                if (target.overlapText != null && target.overlapText.Contains(newIndicator))
                {
                    target.overlapText = target.overlapText.Replace(newIndicator, "");
                    if (target.overlapText == target.buttonText)
                        target.overlapText = target.buttonText;
                }

                if (increment)
                {
                    NotifiLib.SendNotification("<color=grey>[</color><color=green>INCREMENT</color><color=grey>]</color> " + target.toolTip);
                    if (target.enableMethod != null)
                        try { target.enableMethod.Invoke(); }
                        catch (Exception exc)
                        {
                            MelonLoader.MelonLogger.Msg(
                            $"Error with mod enableMethod {target.buttonText} at {exc.StackTrace}: {exc.Message}");
                        }
                }
                else
                {
                    NotifiLib.SendNotification("<color=grey>[</color><color=red>DECREMENT</color><color=grey>]</color> " + target.toolTip);
                    if (target.disableMethod != null)
                        try { target.disableMethod.Invoke(); }
                        catch (Exception exc)
                        {
                            MelonLoader.MelonLogger.Msg(
                            $"Error with mod disableMethod {target.buttonText} at {exc.StackTrace}: {exc.Message}");
                        }
                }
            }
            ReloadMenu();
        }
    }
}