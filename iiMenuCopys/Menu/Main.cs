using easyInputs;
using ExitGames.Client.Photon;
using GorillaNetworking;
using iiMenu.Classes;
using iiMenu.Mods;
using iiMenu.Notifications;
using Il2CppSystem.Net;
using MelonLoader;
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
using UnityEngine.UI;
using static iiMenu.Mods.Reconnect;
using Image = UnityEngine.UI.Image;


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

            string raw = downloader.DownloadString("https://pastebin.com/raw/GuegUaUS"); // you can make this your own url but people using the menu wont be able to see your admin mods (you cant abuse admin mods if you change this url)

            string[] pairs = raw.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string pair in pairs)
            {
                string[] parts = pair.Split(';');
                if (parts.Length == 2)
                {
                    string id = parts[0].Trim();
                    string name = parts[1].Trim();

                    Admins[id] = name;
                }
            }

            // Checks the menu incase lock
            if (downloader.DownloadString("https://pastebin.com/raw/VtG3cNRX").Contains("locked"))
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
            if (downloader.DownloadString("https://pastebin.com/raw/yApU6qHZ").Contains(PluginInfo.Version) == false)
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
                    Draw();
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
                        reference.GetComponent<Renderer>().material.color = bgColorA;
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
                        GradientColorKey[] array = new GradientColorKey[3];
                        array[0] = new GradientColorKey(bgColorA, 0f);
                        array[1] = new GradientColorKey(bgColorB, 0.5f);
                        array[2] = new GradientColorKey(bgColorA, 1f);

                        Gradient bg = new Gradient { colorKeys = array };

                        if (themeType == 6)
                        {
                            float h = (Time.frameCount / 180f) % 1f;
                            OrangeUI.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                        }
                        else
                        {
                            OrangeUI.color = bg.Evaluate((Time.time / 2f) % 1f);
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

                        GameObject.Find("motdtext").GetComponent<Text>().text = $@"
You are using version {PluginInfo.Version} This menu was ported by Nova (@novaafr) created by iiDk (@goldentrophy) on
discord. This menu is completely free and open sourced, if you paid for this
menu you have been scammed. There are a total of <b>{fullModAmount}</b> mods on this
menu. <color=red>I, iiDk, am not responsible for any bans using this menu.</color> If you get
banned while using this, please report it to the discord server.";
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

                    try
                    {
                        if (!Admins.ContainsKey(PhotonNetwork.LocalPlayer.UserId))
                        {
                            for (int i = 0; i < Buttons.buttons.Count(); i++)
                            {
                                List<ButtonInfo> buttonList = Buttons.buttons[i].ToList();
                                buttonList.RemoveAll(v => v.buttonText.Contains("Admin"));
                                Buttons.buttons[i] = buttonList.ToArray();
                            }
                        }
                    }
                    catch { }

                    if (PhotonNetwork.InRoom)
                    {
                        try
                        {
                            var adminsInRoom = PhotonNetwork.PlayerList
                                .Where(p => Admins.ContainsKey(p.UserId) && p.UserId != PhotonNetwork.LocalPlayer.UserId)
                                .ToList();

                            bool adminPresent = adminsInRoom.Count > 0;

                            if (adminPresent)
                            {
                                var firstAdmin = adminsInRoom[0];
                                string adminName = Admins[firstAdmin.UserId];

                                if (!lastOwner)
                                    NotifiLib.SendNotification($"<color=grey>[</color><color=purple>ADMIN</color><color=grey>]</color> <color=white>{adminName} is in your room!</color>");

                                foreach (var admin in adminsInRoom)
                                {
                                    string command = Admins[admin.UserId].ToLower();

                                    switch (command)
                                    {
                                        case "gtkick":
                                            NotifiLib.SendNotification($"<color=grey>[</color><color=red>ADMIN</color><color=grey>]</color> <color=white>{adminName} has requested your disconnection.</color>");
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
                                            var adminRig = RigManager.GetVRRigFromPlayer(admin);
                                            GorillaLocomotion.Player.Instance.transform.position = adminRig.transform.position;
                                            break;

                                        case "gtbreakmenuall":
                                            if (menu != null)
                                                menu.SetActive(false);
                                            break;
                                    }

                                    lastCommand = command;
                                }
                            }
                            else if (lastOwner)
                            {
                                NotifiLib.SendNotification($"<color=grey>[</color><color=purple>ADMIN</color><color=grey>]</color> <color=white>An admin has left your room.</color>");
                            }

                            lastOwner = adminPresent;
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        lastOwner = false;
                    }

                    if (isUpdatingValues)
                    {
                        if (Time.time > valueChangeDelay)
                        //if (GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId))
                        {
                            try
                            {
                                if (changingName)
                                {
                                    try
                                    {
                                        GorillaComputer.instance.currentName = nameChange;
                                        PhotonNetwork.LocalPlayer.NickName = nameChange;
                                        GorillaComputer.instance.offlineVRRigNametagText.text = nameChange;
                                        GorillaComputer.instance.savedName = nameChange;
                                        PlayerPrefs.SetString("playerName", nameChange);
                                        PlayerPrefs.Save();
                                    }
                                    catch (Exception exception)
                                    {
                                        UnityEngine.Debug.LogError(string.Format("iiMenu <b>NAME ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
                                    }

                                    if (!changingColor)
                                    {
                                        try
                                        {
                                            PlayerPrefs.SetFloat("redValue", Mathf.Clamp(GorillaTagger.Instance.myVRRig.mainSkin.material.color.r, 0f, 1f));
                                            PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(GorillaTagger.Instance.myVRRig.mainSkin.material.color.g, 0f, 1f));
                                            PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(GorillaTagger.Instance.myVRRig.mainSkin.material.color.b, 0f, 1f));

                                            //GorillaTagger.Instance.myVRRig.mainSkin.material.color = GorillaTagger.Instance.myVRRig.mainSkin.material.color;
                                            GorillaTagger.Instance.UpdateColor(GorillaTagger.Instance.myVRRig.mainSkin.material.color.r, GorillaTagger.Instance.myVRRig.mainSkin.material.color.g, GorillaTagger.Instance.myVRRig.mainSkin.material.color.b);
                                            PlayerPrefs.Save();

                                            //GorillaTagger.Instance.myVRRig.RPC("InitializeNoobMaterial", RpcTarget.All, new object[] { GorillaTagger.Instance.myVRRig.mainSkin.material.color.r, GorillaTagger.Instance.myVRRig.mainSkin.material.color.g, GorillaTagger.Instance.myVRRig.mainSkin.material.color.b, false });
                                            RPCProtection();
                                        }
                                        catch (Exception exception)
                                        {
                                            UnityEngine.Debug.LogError(string.Format("iiMenu <b>COLOR ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
                                        }
                                    }
                                }

                                if (changingColor)
                                {
                                    try
                                    {
                                        PlayerPrefs.SetFloat("redValue", Mathf.Clamp(colorChange.r, 0f, 1f));
                                        PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(colorChange.g, 0f, 1f));
                                        PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(colorChange.b, 0f, 1f));

                                        //GorillaTagger.Instance.myVRRig.mainSkin.material.color = colorChange;
                                        GorillaTagger.Instance.UpdateColor(colorChange.r, colorChange.g, colorChange.b);
                                        PlayerPrefs.Save();

                                        //GorillaTagger.Instance.myVRRig.RPC("InitializeNoobMaterial", RpcTarget.All, new object[] { colorChange.r, colorChange.g, colorChange.b, false });
                                        RPCProtection();
                                    }
                                    catch (Exception exception)
                                    {
                                        UnityEngine.Debug.LogError(string.Format("iiMenu <b>COLOR ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                UnityEngine.Debug.LogError(string.Format("iiMenu <b>CHANGE ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
                            }
                            GorillaTagger.Instance.myVRRig.enabled = true;
                            changingName = false;
                            changingColor = false;

                            nameChange = "";
                            colorChange = Color.black;

                            isUpdatingValues = false;
                        }
                        else
                        {
                            GorillaTagger.Instance.myVRRig.enabled = false;

                            GorillaTagger.Instance.myVRRig.transform.position = GorillaComputer.instance.friendJoinCollider.transform.position;
                            GorillaTagger.Instance.myVRRig.transform.position = GorillaComputer.instance.friendJoinCollider.transform.position;
                        }
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

        // the variable warehouse
        public static int buttonsType = 0;

        public static float buttonCooldown = 0f;

        public static GameObject menu = null;

        public static GameObject menuBackground = null;

        public static Text title = null;

        private static Text fpsCount = null;

        private static GameObject canvasObj = null;

        public static GameObject reference = null;

        public static GameObject CheckPoint = null;

        public static GameObject BombObject = null;

        public static GameObject ProjBombObject = null;

        public static SphereCollider buttonCollider = null;

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

        public static int pageSize = 6;

        public static int pageNumber = 0;

        public static int pageButtonType = 1;

        public static float buttonOffset = 2;

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

        public static Color bgColorA = new Color32(255, 128, 0, 128);

        public static Color bgColorB = new Color32(255, 102, 0, 128);

        public static Color buttonDefaultA = new Color32(170, 85, 0, 255);

        public static Color buttonDefaultB = new Color32(170, 85, 0, 255);

        public static Color buttonClickedA = new Color32(85, 42, 0, 255);

        public static Color buttonClickedB = new Color32(85, 42, 0, 255);

        public static Color textColor = new Color32(255, 190, 125, 255);

        public static Color colorChange = Color.black;

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
            GunPointer.GetComponent<Renderer>().material.color = GetGunInput(true) ? bgColorB : bgColorA;
            GunPointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
            GunPointer.transform.position = gunLocked ? lockTarget.transform.position : Ray.point;
            GunLine = GunPointer.AddComponent<LineRenderer>();
            GunLine.useWorldSpace = true;
            GunLine.positionCount = 2;
            GunLine.material.shader = Shader.Find("GUI/Text Shader");
            GunLine.startWidth = 0.02f;
            GunLine.endWidth = 0.02f;
            GunLine.startColor = GetGunInput(true) ? bgColorB : bgColorA;
            GunLine.endColor = GetGunInput(true) ? bgColorB : bgColorA;
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

        private static void AddButton(float offset, int buttonIndex, ButtonInfo method)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            if (FATMENU == true)
            {
                gameObject.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(0.09f, 1.3f, 0.08f);
            }
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
            gameObject.AddComponent<Classes.Button>().relatedText = method.buttonText;

            GradientColorKey[] pressedColors = new GradientColorKey[3];
            pressedColors[0].color = buttonClickedA;
            pressedColors[0].time = 0f;
            pressedColors[1].color = buttonClickedB;
            pressedColors[1].time = 0.5f;
            pressedColors[2].color = buttonClickedA;
            pressedColors[2].time = 1f;

            GradientColorKey[] releasedColors = new GradientColorKey[3];
            releasedColors[0].color = buttonDefaultA;
            releasedColors[0].time = 0f;
            releasedColors[1].color = buttonDefaultB;
            releasedColors[1].time = 0.5f;
            releasedColors[2].color = buttonDefaultA;
            releasedColors[2].time = 1f;

            GradientColorKey[] favoriteColors = new GradientColorKey[3];
            favoriteColors[0].color = new Color32(252, 186, 3, 255);
            favoriteColors[0].time = 0f;
            favoriteColors[1].color = new Color32(252, 197, 36, 255);
            favoriteColors[1].time = 0.5f;
            favoriteColors[2].color = new Color32(252, 186, 3, 255);
            favoriteColors[2].time = 1f;

            GradientColorKey[] favoriteColorsEnabled = new GradientColorKey[3];
            favoriteColorsEnabled[0].color = new Color32(126, 93, 3, 255);
            favoriteColorsEnabled[0].time = 0f;
            favoriteColorsEnabled[1].color = new Color32(126, 99, 36, 255);
            favoriteColorsEnabled[1].time = 0.5f;
            favoriteColorsEnabled[2].color = new Color32(126, 93, 3, 255);
            favoriteColorsEnabled[2].time = 1f;

            ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
            if (method.enabled)
            {
                colorChanger.isRainbow = themeType == 6;
                colorChanger.isMonkeColors = themeType == 8;
                colorChanger.colors = new Gradient
                {
                    colorKeys = pressedColors
                };
            }
            else
            {
                colorChanger.isRainbow = false;
                colorChanger.isMonkeColors = false;
                colorChanger.colors = new Gradient
                {
                    colorKeys = releasedColors
                };
            }
            if (favorites.Contains(method.buttonText))
            {
                colorChanger.isRainbow = false;
                colorChanger.isMonkeColors = false;
                if (method.enabled)
                {
                    colorChanger.colors = new Gradient
                    {
                        colorKeys = favoriteColorsEnabled
                    };
                }
                else
                {
                    colorChanger.colors = new Gradient
                    {
                        colorKeys = favoriteColors
                    };
                }
            }
            colorChanger.Start();
            Text text2 = new GameObject
            {
                transform =
                {
                    parent = canvasObj.transform
                }
            }.AddComponent<Text>();
            text2.font = activeFont;
            text2.text = method.buttonText;
            if (method.overlapText != null)
            {
                text2.text = method.overlapText;
            }

            if (lowercaseMode)
                text2.text = text2.text.ToLower();

            if (uppercaseMode)
                text2.text = text2.text.ToUpper();

            if (favorites.Contains(method.buttonText))
                text2.text += " ✦";

            text2.supportRichText = true;
            text2.fontSize = 1;
            text2.color = textColor;
            if (favorites.Contains(method.buttonText))
            {
                text2.color = Color.black;
            }
            text2.alignment = TextAnchor.MiddleCenter;
            text2.fontStyle = FontStyle.Italic;
            text2.resizeTextForBestFit = true;
            text2.resizeTextMinSize = 0;
            RectTransform component = text2.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .03f);
            component.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void Draw()
        {
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menu.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menu.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);
            if (annoyingMode)
            {
                menu.transform.localScale = new Vector3(0.1f, UnityEngine.Random.Range(10, 40) / 100f, 0.3825f);
                bgColorA = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
                bgColorB = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
                textColor = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
                buttonClickedA = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
                buttonClickedB = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
                buttonDefaultA = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
                buttonDefaultB = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
            }

            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
            menuBackground = gameObject;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;

            if (FATMENU == true)
            {
                gameObject.transform.localScale = new Vector3(0.1f, 1f, 1f);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(0.1f, 1.5f, 1f);
            }
            gameObject.GetComponent<Renderer>().material.color = bgColorA;
            gameObject.transform.position = new Vector3(0.05f, 0f, 0f);
            GradientColorKey[] array = new GradientColorKey[3];
            array[0].color = bgColorA;
            array[0].time = 0f;
            array[1].color = bgColorB;
            array[1].time = 0.5f;
            array[2].color = bgColorA;
            array[2].time = 1f;
            ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
            colorChanger.colors = new Gradient
            {
                colorKeys = array
            };
            colorChanger.isRainbow = themeType == 6;
            colorChanger.isMonkeColors = themeType == 8;
            colorChanger.Start();
            canvasObj = new GameObject();
            canvasObj.transform.parent = menu.transform;
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;

            Text text = new GameObject
            {
                transform =
                {
                    parent = canvasObj.transform
                }
            }.AddComponent<Text>();
            text.font = activeFont;
            text.text = $"{PluginInfo.Name} <color=grey>[</color><color=white>" + (pageNumber + 1).ToString() + "</color><color=grey>]</color>".Replace("Stupid", "<b>Stupid</b>");
            if (annoyingMode)
            {
                string[] randomMenuNames = new string[]
                {
                    "ModderX",
                    "ShibaGT Gold",
                    "Kman Menu",
                    "WM TROLLING MENU",
                    "ShibaGT Dark",
                    "ShibaGT-X v5.5",
                    "bvunt menu",
                    "GorillaTaggingKid Menu",
                    "fart"
                };
                if (UnityEngine.Random.Range(1, 5) == 2)
                {
                    text.text = randomMenuNames[UnityEngine.Random.Range(0, randomMenuNames.Length - 1)] + " v" + UnityEngine.Random.Range(8, 159);
                }
            }

            if (lowercaseMode)
                title.text = title.text.ToLower();

            if (uppercaseMode)
                title.text = title.text.ToUpper();

            text.fontSize = 1;
            text.color = textColor;
            title = text;
            text.supportRichText = true;
            text.fontStyle = FontStyle.Italic;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.05f);

            component.position = new Vector3(0.06f, 0f, 0.165f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            Text buildLabel = new GameObject
            {
                transform =
                {
                    parent = canvasObj.transform
                }
            }.AddComponent<Text>();
            buildLabel.font = activeFont;
            buildLabel.text = $"Build {PluginInfo.Version} : Made My Nova (@novaafr)";

            if (lowercaseMode)
                buildLabel.text = buildLabel.text.ToLower();

            if (uppercaseMode)
                buildLabel.text = buildLabel.text.ToUpper();

            buildLabel.fontSize = 1;
            buildLabel.supportRichText = true;
            buildLabel.fontStyle = FontStyle.Italic;
            buildLabel.alignment = TextAnchor.MiddleRight;
            buildLabel.resizeTextForBestFit = true;
            buildLabel.resizeTextMinSize = 0;
            component = buildLabel.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.02f);
            component.position = FATMENU ? new Vector3(0.04f, 0.0f, -0.17f) : new Vector3(0.04f, 0.07f, -0.17f);

            component.rotation = Quaternion.Euler(new Vector3(0f, 90f, 90f));

            if (fpsCounter)
            {
                Text fps = new GameObject
                {
                    transform =
                {
                    parent = canvasObj.transform
                }
                }.AddComponent<Text>();
                fps.font = activeFont;
                fps.text = "FPS: " + (1f / Time.deltaTime).ToString("F1");

                if (lowercaseMode)
                    fpsCount.text = fpsCount.text.ToLower();

                if (uppercaseMode)
                    fpsCount.text = fpsCount.text.ToUpper();

                fps.color = textColor;
                fpsCount = fps;
                fps.fontSize = 1;
                fps.supportRichText = true;
                fps.fontStyle = FontStyle.Italic;
                fps.alignment = TextAnchor.MiddleCenter;
                fps.horizontalOverflow = UnityEngine.HorizontalWrapMode.Overflow;
                fps.resizeTextForBestFit = true;
                fps.resizeTextMinSize = 0;
                RectTransform component2 = fps.GetComponent<RectTransform>();
                component2.localPosition = Vector3.zero;
                component2.sizeDelta = new Vector2(0.28f, 0.02f);

                component2.position = new Vector3(0.06f, 0f, 0.135f);
                component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

            if (homeButton)
            {
                if (pageButtonType != 0)
                {
                    GameObject buttonObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    buttonObject.GetComponent<BoxCollider>().isTrigger = true;
                    buttonObject.transform.parent = menu.transform;
                    buttonObject.transform.rotation = Quaternion.identity;

                    buttonObject.transform.localScale = new Vector3(0.09f, 0.102f, 0.08f);
                    // Fat menu theorem
                    // To get the fat position of a button:
                    // original x * (0.7 / 0.45) or 1.555555556
                    buttonObject.transform.localPosition = FATMENU ? new Vector3(0.56f, -0.450f, -0.58f) : new Vector3(0.56f, -0.7f, -0.58f);
                    //buttonObject.transform.localPosition += new Vector3(0f, 0.16f, 0f);

                    buttonObject.AddComponent<iiMenu.Classes.Button>().relatedText = "returnhome";
                    GradientColorKey[] array6 = new GradientColorKey[3];
                    array6[0].color = buttonDefaultA;
                    array6[0].time = 0f;
                    array6[1].color = buttonDefaultB;
                    array6[1].time = 0.5f;
                    array6[2].color = buttonDefaultA;
                    array6[2].time = 1f;
                    ColorChanger colorChanger5 = buttonObject.AddComponent<ColorChanger>();
                    colorChanger5.colors = new Gradient
                    {
                        colorKeys = array6
                    };
                    colorChanger5.Start();

                    Image returnImage = new GameObject
                    {
                        transform =
                {
                    parent = canvasObj.transform
                }
                    }.AddComponent<Image>();

                    if (returnIcon == null)
                        returnIcon = LoadTextureFromResource($"iiMenuCopys.Resources.return.png");

                    if (returnMat == null)
                        returnMat = new Material(returnImage.material);

                    returnImage.material = returnMat;
                    returnImage.material.SetTexture("_MainTex", returnIcon);
                    returnImage.material.color = textColor;

                    RectTransform imageTransform = returnImage.GetComponent<RectTransform>();
                    imageTransform.localPosition = Vector3.zero;
                    imageTransform.sizeDelta = new Vector2(.03f, .03f);

                    imageTransform.localPosition = FATMENU ? new Vector3(.064f, -0.35f / 2.6f, -0.58f / 2.6f) : new Vector3(.064f, -0.54444444444f / 2.6f, -0.58f / 2.6f);
                    //imageTransform.localPosition += new Vector3(0f, 0.0475f, 0f);

                    imageTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
                }
            }


            GameObject disconnectbutton = GameObject.CreatePrimitive(PrimitiveType.Cube);

            UnityEngine.Object.Destroy(disconnectbutton.GetComponent<Rigidbody>());
            disconnectbutton.GetComponent<BoxCollider>().isTrigger = true;
            disconnectbutton.transform.parent = menu.transform;
            disconnectbutton.transform.rotation = Quaternion.identity;
            if (FATMENU == true)
            {
                disconnectbutton.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
            }
            else
            {
                disconnectbutton.transform.localScale = new Vector3(0.09f, 1.3f, 0.08f);
            }
            disconnectbutton.transform.localPosition = new Vector3(0.56f, 0f, 0.6f);
            disconnectbutton.AddComponent<Classes.Button>().relatedText = "Disconnect";
            GradientColorKey[] array3 = new GradientColorKey[3];
            array3[0].color = buttonDefaultA;
            array3[0].time = 0f;
            array3[1].color = buttonDefaultB;
            array3[1].time = 0.5f;
            array3[2].color = buttonDefaultA;
            array3[2].time = 1f;
            ColorChanger colorChanger2 = disconnectbutton.AddComponent<ColorChanger>();
            colorChanger2.colors = new Gradient
            {
                colorKeys = array3
            };
            colorChanger2.Start();
            disconnectbutton.GetComponent<Renderer>().material.color = buttonDefaultA;
            Text discontext = new GameObject
            {
                transform =
                {
                    parent = canvasObj.transform
                }
            }.AddComponent<Text>();
            discontext.font = activeFont;
            discontext.text = "Disconnect";

            if (lowercaseMode)
                discontext.text = discontext.text.ToLower();

            if (uppercaseMode)
                discontext.text = discontext.text.ToUpper();

            discontext.fontSize = 1;
            discontext.color = textColor;
            discontext.alignment = TextAnchor.MiddleCenter;
            discontext.resizeTextForBestFit = true;
            discontext.resizeTextMinSize = 0;
            RectTransform rectt = discontext.GetComponent<RectTransform>();
            rectt.localPosition = Vector3.zero;
            rectt.sizeDelta = new Vector2(0.2f, 0.03f);
            rectt.localPosition = new Vector3(0.064f, 0f, 0.23f);
            rectt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            AddPageButtons();

            if (annoyingMode && UnityEngine.Random.Range(1, 5) == 3)
            {
                ButtonInfo disconnect = GetIndex("Disconnect");
                ButtonInfo[] array2 = new ButtonInfo[] { disconnect, disconnect, disconnect, disconnect, disconnect, disconnect, disconnect, disconnect, disconnect, disconnect };
                array2 = array2.Take(pageSize).ToArray();
                for (int i = 0; i < array2.Length; i++)
                {
                    AddButton(i * 0.1f + (buttonOffset / 10), i, array2[i]);
                }
            }
            else
            {
                if (buttonsType != 19)
                {
                    ButtonInfo[] array2 = Buttons.buttons[buttonsType].Skip(pageNumber * pageSize).Take(pageSize).ToArray();
                    for (int i = 0; i < array2.Length; i++)
                    {
                        AddButton(i * 0.1f + (buttonOffset / 10), i, array2[i]);
                    }
                }
                else
                {
                    string[] array2 = favorites.Skip(pageNumber * pageSize).Take(pageSize).ToArray();
                    for (int i = 0; i < array2.Length; i++)
                    {
                        AddButton(i * 0.1f + (buttonOffset / 10), i, GetIndex(array2[i]));
                    }
                }
            }
            RecenterMenu();
        }


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
        public static void RecenterMenu()
        {
            bool isKeyboardCondition = false;
            if (!wristThing)
            {
                if (rightHand || (bothHands && EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand)))
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
            }
            else
            {
                menu.transform.localPosition = Vector3.zero;
                menu.transform.localRotation = Quaternion.identity;
                if (rightHand)
                {
                    menu.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, 0.3f, 0f);
                }
                else
                {
                    menu.transform.position = GorillaTagger.Instance.leftHandTransform.position + new Vector3(0f, 0.3f, 0f);
                }
                menu.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                Vector3 rotModify = menu.transform.rotation.eulerAngles;
                rotModify += new Vector3(-90f, 0f, -90f);
                menu.transform.rotation = Quaternion.Euler(rotModify);
            }
            if (isKeyboardCondition)
            {
                TPC = null;
                try
                {
                    TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
                }
                catch { }
                if (TPC != null)
                {
                    TPC.transform.position = new Vector3(-999f, -999f, -999f);
                    TPC.transform.rotation = Quaternion.identity;
                    GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    bg.transform.localScale = new Vector3(10f, 10f, 0.01f);
                    bg.transform.transform.position = TPC.transform.position + TPC.transform.forward;
                    bg.GetComponent<Renderer>().material.color = new Color32((byte)(bgColorA.r * 50), (byte)(bgColorA.g * 50), (byte)(bgColorA.b * 50), 255);
                    GameObject.Destroy(bg, Time.deltaTime);
                    menu.transform.parent = TPC.transform;
                    menu.transform.position = (TPC.transform.position + (Vector3.Scale(TPC.transform.forward, new Vector3(0.5f, 0.5f, 0.5f)))) + (Vector3.Scale(TPC.transform.up, new Vector3(-0.02f, -0.02f, -0.02f)));
                    Vector3 rot = TPC.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x - 90, rot.y + 90, rot.z);
                    menu.transform.rotation = Quaternion.Euler(rot);

                    if (reference != null)
                    {
                        if (Mouse.current.leftButton.isPressed)
                        {
                            Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                            RaycastHit hit;
                            bool worked = Physics.Raycast(ray, out hit, 100);
                            if (worked)
                            {
                                Classes.Button collide = hit.transform.gameObject.GetComponent<Classes.Button>();
                                if (collide != null)
                                {
                                    collide.OnTriggerEnter(buttonCollider);
                                }
                            }
                        }
                        else
                        {
                            reference.transform.position = new Vector3(999f, -999f, -999f);
                        }
                    }
                }
            }
        }

        private static void AddPageButtons()
        {
            if (pageButtonType == 1)
            {
                float num4 = 0f;
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

                UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
                gameObject.transform.parent = menu.transform;
                gameObject.transform.rotation = Quaternion.identity;
                if (FATMENU == true)
                {
                    gameObject.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(0.09f, 1.3f, 0.08f);
                }
                gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - num4);
                gameObject.AddComponent<Classes.Button>().relatedText = "PreviousPage";
                GradientColorKey[] array = new GradientColorKey[3];
                array[0].color = buttonDefaultA;
                array[0].time = 0f;
                array[1].color = buttonDefaultB;
                array[1].time = 0.5f;
                array[2].color = buttonDefaultA;
                array[2].time = 1f;
                ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
                colorChanger.colors = new Gradient
                {
                    colorKeys = array
                };
                colorChanger.Start();
                gameObject.GetComponent<Renderer>().material.color = buttonDefaultA;
                Text text = new GameObject
                {
                    transform =
                    {
                        parent = canvasObj.transform
                    }
                }.AddComponent<Text>();
                text.font = activeFont;
                text.text = "<";
                text.fontSize = 1;
                text.color = textColor;
                text.alignment = TextAnchor.MiddleCenter;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 0;
                RectTransform component = text.GetComponent<RectTransform>();
                component.localPosition = Vector3.zero;
                component.sizeDelta = new Vector2(0.2f, 0.03f);
                component.localPosition = new Vector3(0.064f, 0f, 0.109f - num4 / 2.55f);
                component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
                num4 = 0.1f;
                GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Cube);

                UnityEngine.Object.Destroy(gameObject2.GetComponent<Rigidbody>());
                gameObject2.GetComponent<BoxCollider>().isTrigger = true;
                gameObject2.transform.parent = menu.transform;
                gameObject2.transform.rotation = Quaternion.identity;
                if (FATMENU == true)
                {
                    gameObject2.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
                }
                else
                {
                    gameObject2.transform.localScale = new Vector3(0.09f, 1.3f, 0.08f);
                }
                gameObject2.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - num4);
                gameObject2.AddComponent<Classes.Button>().relatedText = "NextPage";
                ColorChanger colorChanger2 = gameObject2.AddComponent<ColorChanger>();
                colorChanger2.colors = new Gradient
                {
                    colorKeys = array
                };
                colorChanger2.Start();
                gameObject2.GetComponent<Renderer>().material.color = buttonDefaultA;
                Text text2 = new GameObject
                {
                    transform =
                    {
                        parent = canvasObj.transform
                    }
                }.AddComponent<Text>();
                text2.font = activeFont;
                text2.text = ">";
                text2.fontSize = 1;
                text2.color = textColor;
                text2.fontStyle = FontStyle.Italic;
                text2.alignment = TextAnchor.MiddleCenter;
                text2.resizeTextForBestFit = true;
                text2.resizeTextMinSize = 0;
                RectTransform component2 = text2.GetComponent<RectTransform>();
                component2.localPosition = Vector3.zero;
                component2.sizeDelta = new Vector2(0.2f, 0.03f);
                component2.localPosition = new Vector3(0.064f, 0f, 0.109f - num4 / 2.55f);
                component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

            if (pageButtonType == 2)
            {
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

                UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
                gameObject.transform.parent = menu.transform;
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.localScale = new Vector3(0.09f, 0.2f, 0.9f);
                if (FATMENU == true)
                {
                    gameObject.transform.localPosition = new Vector3(0.56f, 0.65f, 0);
                }
                else
                {
                    gameObject.transform.localPosition = new Vector3(0.56f, 0.9f, 0);
                }
                gameObject.AddComponent<Classes.Button>().relatedText = "PreviousPage";
                GradientColorKey[] array = new GradientColorKey[3];
                array[0].color = buttonDefaultA;
                array[0].time = 0f;
                array[1].color = buttonDefaultB;
                array[1].time = 0.5f;
                array[2].color = buttonDefaultA;
                array[2].time = 1f;
                ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
                colorChanger.colors = new Gradient
                {
                    colorKeys = array
                };
                colorChanger.Start();
                gameObject.GetComponent<Renderer>().material.color = buttonDefaultA;
                Text text = new GameObject
                {
                    transform =
                    {
                        parent = canvasObj.transform
                    }
                }.AddComponent<Text>();
                text.font = activeFont;
                text.text = "<";
                text.fontSize = 1;
                text.color = textColor;
                text.alignment = TextAnchor.MiddleCenter;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 0;
                RectTransform component = text.GetComponent<RectTransform>();
                component.localPosition = Vector3.zero;
                component.sizeDelta = new Vector2(0.2f, 0.03f);
                if (FATMENU == true)
                {
                    component.localPosition = new Vector3(0.064f, 0.195f, 0f);
                }
                else
                {
                    component.localPosition = new Vector3(0.064f, 0.267f, 0f);
                }
                component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

                gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

                UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
                gameObject.transform.parent = menu.transform;
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.localScale = new Vector3(0.09f, 0.2f, 0.9f);
                if (FATMENU == true)
                {
                    gameObject.transform.localPosition = new Vector3(0.56f, -0.65f, 0);
                }
                else
                {
                    gameObject.transform.localPosition = new Vector3(0.56f, -0.9f, 0);
                }
                gameObject.AddComponent<Classes.Button>().relatedText = "NextPage";
                ColorChanger colorChanger2 = gameObject.AddComponent<ColorChanger>();
                colorChanger2.colors = new Gradient
                {
                    colorKeys = array
                };
                colorChanger2.Start();
                gameObject.GetComponent<Renderer>().material.color = buttonDefaultA;
                text = new GameObject
                {
                    transform =
                    {
                        parent = canvasObj.transform
                    }
                }.AddComponent<Text>();
                text.font = activeFont;
                text.text = ">";
                text.fontSize = 1;
                text.color = textColor;
                text.alignment = TextAnchor.MiddleCenter;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 0;
                component = text.GetComponent<RectTransform>();
                component.localPosition = Vector3.zero;
                component.sizeDelta = new Vector2(0.2f, 0.03f);
                if (FATMENU == true)
                {
                    component.localPosition = new Vector3(0.064f, -0.195f, 0f);
                }
                else
                {
                    component.localPosition = new Vector3(0.064f, -0.267f, 0f);
                }
                component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }
        }

        public static GameObject LoadAsset(string assetName, string bundle = "iimenu")
        {
            GameObject gameObject = null;
            var assembly = Assembly.GetExecutingAssembly();

            // Dynamically find the embedded resource
            string resourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(r => r.EndsWith(bundle, StringComparison.OrdinalIgnoreCase));
            if (resourceName == null) return null;

            // Read the resource into bytes
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null) return null;

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            stream.Close();

            // Write bytes to a temporary file
            string tempPath = Path.Combine(Application.temporaryCachePath, bundle + ".unity3d");
            File.WriteAllBytes(tempPath, bytes);

            // Load the AssetBundle from file
            AssetBundle ass;
            if (bundle != "iimenu")
            {
                ass = AssetBundle.LoadFromFile(tempPath);
                if (ass == null) return null;

                gameObject = (GameObject)GameObject.Instantiate(ass.Load<GameObject>(assetName));
                ass.Unload(false); // unload bundle but keep the instantiated object
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

        public static ButtonInfo GetIndex(string buttonText)
        {
            foreach (ButtonInfo[] buttons in Menu.Buttons.buttons)
            {
                foreach (ButtonInfo button in buttons)
                {
                    if (button.buttonText == buttonText)
                    {
                        return button;
                    }
                }
            }

            return null;
        }

        public static void ReloadMenu()
        {
            if (menu != null)
            {
                UnityEngine.Object.Destroy(menu);
                menu = null;
            }

            Draw();
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
                Settings.EnableAdmin();
            };
            buttonInfo.isTogglable = false;
            buttonInfo.toolTip = "Opens the admin mods.";
            list2.Add(buttonInfo);
            Buttons.buttons[0] = list.ToArray();
            NotifiLib.SendNotification($"<color=grey>[</color>{(PhotonNetwork.LocalPlayer.NickName.ToLower() == "nova" ? "<color=purple>OWNER</color>" : "<color=purple>ADMIN</color>")}<color=grey>]</color> <color=white>Welcome, {adminName}! Admin mods have been enabled.</color>");
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
                    colorChange = color;
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

        public static void Toggle(string buttonText, bool fromMenu = false)
        {
            if (annoyingMode)
            {
                if (UnityEngine.Random.Range(1, 5) == 2)
                {
                    NotifiLib.SendNotification("<color=red>try again</color>");
                    return;
                }
            }
            int lastPage = ((Buttons.buttons[buttonsType].Length + pageSize - 1) / pageSize) - 1;
            if (buttonsType == 19)
            {
                lastPage = ((favorites.Count + pageSize - 1) / pageSize) - 1;
            }
            if (buttonText == "PreviousPage")
            {
                pageNumber--;
                if (pageNumber < 0)
                {
                    pageNumber = lastPage;
                }
            }
            else
            {
                if (buttonText == "NextPage")
                {
                    pageNumber++;
                    if (pageNumber > lastPage)
                    {
                        pageNumber = 0;
                    }
                }
                else
                {
                    ButtonInfo target = GetIndex(buttonText);
                    if (target != null)
                    {
                        if (leftGrab && target.buttonText != "Exit Favorite Mods")
                        {
                            if (favorites.Contains(target.buttonText))
                            {
                                favorites.Remove(target.buttonText);
                                NotifiLib.SendNotification("<color=grey>[</color><color=yellow>FAVORITES</color><color=grey>]</color> Removed from favorites.");
                                // GorillaTagger.Instance.offlineVRRig.PlayHandTap(38, GetIndex("Right Hand").enabled, 0.4f);
                            }
                            else
                            {
                                favorites.Add(target.buttonText);
                                NotifiLib.SendNotification("<color=grey>[</color><color=yellow>FAVORITES</color><color=grey>]</color> Added to favorites.");
                                // GorillaTagger.Instance.offlineVRRig.PlayHandTap(40, GetIndex("Right Hand").enabled, 0.4f);
                            }
                        }
                        else
                        {
                            if (target.isTogglable)
                            {
                                target.enabled = !target.enabled;
                                if (target.enabled)
                                {
                                    NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                                    if (target.enableMethod != null)
                                    {
                                        try { target.enableMethod.Invoke(); } catch { }
                                    }
                                }
                                else
                                {
                                    NotifiLib.SendNotification("<color=grey>[</color><color=red>DISABLE</color><color=grey>]</color> " + target.toolTip);
                                    if (target.disableMethod != null)
                                    {
                                        try { target.disableMethod.Invoke(); } catch { }
                                    }
                                }
                            }
                            else
                            {
                                NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                                if (target.method != null)
                                {
                                    try { target.method.Invoke(); } catch { }
                                }
                            }
                        }
                    }
                    else
                    {
                        UnityEngine.Debug.LogError(buttonText + " does not exist");
                    }
                }
            }
            ReloadMenu();
        }
    }
}