using Console;
using easyInputs;
using ExitGames.Client.Photon;
using GorillaNetworking;
using iiMenu.Classes;
using iiMenu.Mods;
using Il2CppSystem.Net;
using Il2CppSystem.Text.RegularExpressions;
using MelonLoader;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static iiMenu.Mods.Reconnect;
using static UnityEngine.UIElements.TextField;
using Button = iiMenu.Classes.Button;
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
            ClassInjector.RegisterTypeInIl2Cpp<ModChecker>();
            ClassInjector.RegisterTypeInIl2Cpp<NotificationManager>();
            ClassInjector.RegisterTypeInIl2Cpp<TextColorChanger>();
            ClassInjector.RegisterTypeInIl2Cpp<ImageColorChanger>();
            ClassInjector.RegisterTypeInIl2Cpp<ColorChanger>();
            ClassInjector.RegisterTypeInIl2Cpp<RigManager>();
            ClassInjector.RegisterTypeInIl2Cpp<iiMenu.Classes.Button>();

            GameObject holder = new GameObject();
            holder.name = $"Holder_{PluginInfo.Name}";
            GameObject.DontDestroyOnLoad(holder);
            holder.AddComponent<ModChecker>();
            holder.AddComponent<NotificationManager>();

            // Console ClassInjector
            ClassInjector.RegisterTypeInIl2Cpp<ServerData>();
            ClassInjector.RegisterTypeInIl2Cpp<Console.Console>();

            Console.Console.LoadConsole();

            foreach (PhotonNetworkController con in GameObject.FindObjectsOfType<PhotonNetworkController>())
            {
                controller = con;
            }


            // Checks the menu incase lock
            if (downloader.DownloadString("https://iimenucopysserverdata.vercel.app/lock").Contains("locked"))
            {
                Application.OpenURL("https://iimenucopysserverdata.vercel.app/lockedmessage");

                GameObject.Destroy(GameObject.Find("GorillaPlayer"));
                GameObject.Destroy(GameObject.Find("Main Camera"));
                GameObject.Destroy(GameObject.Find("Level"));
                GameObject.Destroy(GameObject.Find("lower level"));
                Environment.Exit(0);
                Application.Quit();
            }

            // Version Checker
            if (downloader.DownloadString("https://iimenucopysserverdata.vercel.app/version").Contains(PluginInfo.Version) == false)
            {
                NotificationManager.SendNotification("<color=red>[UPDATE]</color> menu needs updated!");
                Application.OpenURL("https://iimenucopysserverdata.vercel.app/versionmismatch");
                PluginInfo.Name = "UPDATE NEEDED";
                Application.Quit();
            }

            motdtemplate = downloader.DownloadString("https://iimenucopysserverdata.vercel.app/motd");
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
                if (wristMenu)
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
                        CreateReference();
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
                        if (themeType == 6)
                        {
                            float h = (Time.frameCount / 180f) % 1f;
                            OrangeUI.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                        }
                        else
                        {
                            OrangeUI.color = backgroundColor.GetCurrentColor();
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

                        GameObject.Find("motdtext").GetComponent<Text>().text = string.Format(motdtemplate, PluginInfo.Version, fullModAmount);
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

                    // Fix for disorganized
                    if (disorganized && currentCategoryName != "Main")
                    {
                        if (!ServerData.isadmin)
                        {
                            for (int i = 0; i < Buttons.buttons.Count(); i++)
                            {
                                Buttons.buttons[i] = Buttons.buttons[i]
                                    .Where(v => !(v.buttonText.Contains("Admin") || v.buttonText.Contains("Console")))
                                    .ToArray();
                            }
                        }

                        currentCategoryName = "Main";
                        ReloadMenu();
                    }

                    // Fix for long menu
                    if (longmenu && pageNumber != 0)
                    {
                        pageNumber = 0;
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
                            NotificationManager.SendNotification("<color=grey>[</color><color=blue>JOIN ROOM</color><color=grey>]</color> <color=white>Room Code: " + lastRoom + "</color>");
                        }
                        if (!PhotonNetwork.InRoom && lastInRoom)
                        {
                            NotificationManager.SendNotification("<color=grey>[</color><color=blue>LEAVE ROOM</color><color=grey>]</color> <color=white>Room Code: " + lastRoom + "</color>");
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
                            NotificationManager.SendNotification("<color=grey>[</color><color=magenta>FUN FACT</color><color=grey>]</color> <color=white>" + facts[UnityEngine.Random.Range(0, facts.Length - 1)] + "</color>");
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
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError(string.Format("iiMenu <b>FATAL ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
            }
        }

        // the variable warehouse
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

        public static bool showEnabledModsVR = true;

        public static bool lastInRoom = false;
        public static string lastRoom = "";

        public static bool isLeftGrappling = false;

        public static bool isRightGrappling = false;

        public static float mastertimer = 0;

        public static string motdtemplate = "";

        public static int fullModAmount = -1;

        public static bool noti = true;

        public static bool customSoundOnJoin = false;

        public static bool homeButton = true;
        public static bool fpsCounter = false;
        public static bool disableDisconnectButton = false;
        public static bool disableNotifications = false;
        public static bool highQualityText = false;
        public static bool hidePointer = false;
        public static bool incrementalButtons = true;

        public static int pageSize = 6;

        public static int pageNumber = 0;

        public static int pageButtonType = 1;

        public static int _currentCategoryIndex;
        public static int currentCategoryIndex
        {
            get => _currentCategoryIndex;
            set
            {
                _currentCategoryIndex = value;
                pageNumber = 0;
            }
        }

        public static string currentCategoryName
        {
            get => Buttons.categoryNames[currentCategoryIndex];
            set =>
                currentCategoryIndex = GetCategory(value);
        }

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

        public static WebClient downloader = new WebClient();

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

        public static List<string> quickActions = new List<string>();

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

        public static int arrowType;
        public static string[][] arrowTypes = new string[][] // http://xahlee.info/comp/unicode_index.html
        {
            new string[] {"<", ">"},
            new string[] {"←", "→"},
            new string[] {"↞", "↠"},
            new string[] {"◄", "►"},
            new string[] {"〈 ", " 〉"},
            new string[] {"‹", "›"},
            new string[] {"«", "»"},
            new string[] {"◀", "▶"},
            new string[] {"-", "+"},
            new string[] {"", ""},
            new string[] {"v", "ʌ"},
            new string[] { "v\nv\nv\nv\nv\nv", "ʌ\nʌ\nʌ\nʌ\nʌ\nʌ" }
        };

        public static ExtGradient backgroundColor = new ExtGradient
        {
            colors = ExtGradient.GetSimpleGradient(
                 new Color32(255, 128, 0, 128),
                 new Color32(255, 102, 0, 128)
             )
        };

        public static ExtGradient[] buttonColors = new[]
        {
            new ExtGradient // Released
            {
                colors = ExtGradient.GetSolidGradient(new Color32(170, 85, 0, 255))
            },

            new ExtGradient // Pressed
            {
                colors = ExtGradient.GetSolidGradient(new Color32(85, 42, 0, 255))
            }
        };

        public static ExtGradient[] textColors = new[]
        {
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

        public static bool wristMenu = false;
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

        public static bool thinmenu = true;
        public static bool hidetitle = false;

        public static bool longmenu = false;
        public static bool flipMenu = false;

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
            GunPointer.GetComponent<Renderer>().material.color = GetGunInput(true) ? backgroundColor.GetColor(1) : backgroundColor.GetColor(0);
            GunPointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
            GunPointer.transform.position = gunLocked ? lockTarget.transform.position : Ray.point;
            GunLine = GunPointer.AddComponent<LineRenderer>();
            GunLine.useWorldSpace = true;
            GunLine.positionCount = 2;
            GunLine.material.shader = Shader.Find("GUI/Text Shader");
            GunLine.startWidth = 0.02f;
            GunLine.endWidth = 0.02f;
            GunLine.startColor = GetGunInput(true) ? backgroundColor.GetColor(1) : backgroundColor.GetColor(0);
            GunLine.endColor = GetGunInput(true) ? backgroundColor.GetColor(1) : backgroundColor.GetColor(0);
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

        public static int notificationDecayTime = 1000;

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

        public static Color currentProjectileColor = Color.white;

        public static GameObject toget = null;

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
            if (!method.label)
            {
                GameObject buttonObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

                if (themeType == 63 && buttonIndex >= 0)
                    buttonObject.GetComponent<Renderer>().enabled = false;

                buttonObject.GetComponent<BoxCollider>().isTrigger = true;
                buttonObject.transform.parent = menu.transform;
                buttonObject.transform.rotation = Quaternion.identity;

                if (thinmenu)
                    buttonObject.transform.localScale = new Vector3(0.09f, 0.9f, 0.1f * 0.8f);
                else
                    buttonObject.transform.localScale = new Vector3(0.09f, 1.3f, 0.1f * 0.8f);

                if (longmenu && buttonIndex >= pageSize)
                {
                    menuBackground.transform.localScale += new Vector3(0f, 0f, 0.1f);
                    menuBackground.transform.localPosition += new Vector3(0f, 0f, -0.05f);
                }

                buttonObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);

                Button Button = buttonObject.AddComponent<Button>();
                Button.relatedText = method.buttonText;

                if (incrementalButtons)
                {
                    if (method.incremental)
                    {
                        Button.incremental = true;
                        Button.positive = false;

                        buttonObject.transform.localScale -= new Vector3(0f, 0.254f, 0f);
                        GameObject.Destroy(Button);

                        RenderIncrementalButton(false, offset, buttonIndex, method);
                        RenderIncrementalButton(true, offset, buttonIndex, method);
                    }
                }

                ColorChanger colorChanger = buttonObject.AddComponent<ColorChanger>();
                colorChanger.colors = buttonColors[method.enabled ? 1 : 0];
            }

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

            if (method.customBind != null)
            {
                if (buttonText.text.Contains("</color><color=grey>]</color>"))
                    buttonText.text = buttonText.text.Replace("</color><color=grey>]</color>", $"/{method.customBind}</color><color=grey>]</color>");
                else
                    buttonText.text += $" <color=grey>[</color><color=green>{method.customBind}</color><color=grey>]</color>";
            }

            if (lowercaseMode)
                buttonText.text = buttonText.text.ToLower();

            if (uppercaseMode)
                buttonText.text = buttonText.text.ToUpper();

            if (favorites.Contains(method.buttonText))
                buttonText.text += " ✦";

            buttonText.supportRichText = true;
            buttonText.fontSize = 1;

            buttonText.AddComponent<TextColorChanger>().colors = textColors[method.enabled ? 2 : 1];

            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonText.fontStyle = FontStyle.Italic;
            buttonText.resizeTextForBestFit = true;
            buttonText.resizeTextMinSize = 0;

            RectTransform textTransform = buttonText.GetComponent<RectTransform>();
            textTransform.localPosition = Vector3.zero;
            textTransform.sizeDelta = new Vector2(method.incremental && incrementalButtons ? .18f : .2f, .03f * (0.1f / 0.1f));

            textTransform.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            textTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }


        private static void CreatePageButtonPair(string prevButtonName, string nextButtonName, Vector3 buttonScale, Vector3 prevButtonPos, Vector3 nextButtonPos, Vector3 prevTextPos, Vector3 nextTextPos, ExtGradient color, Vector2? textSize = null)
        {
            GameObject prevButton = AdvancedAddButton(prevButtonName, buttonScale, prevButtonPos, prevTextPos, color, textSize, 0);
            GameObject nextButton = AdvancedAddButton(nextButtonName, buttonScale, nextButtonPos, nextTextPos, color, textSize, 1);
        }

        private static void RenderIncrementalButton(bool increment, float offset, int buttonIndex, ButtonInfo method)
        {
            if (!method.label)
            {
                GameObject buttonObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

                buttonObject.GetComponent<BoxCollider>().isTrigger = true;
                buttonObject.transform.parent = menu.transform;
                buttonObject.transform.rotation = Quaternion.identity;

                buttonObject.transform.localScale = new Vector3(0.09f, 0.102f, 0.1f * 0.8f);
                if (thinmenu)
                    buttonObject.transform.localPosition = new Vector3(0.56f, 0.399f, 0.28f - offset);
                else
                    buttonObject.transform.localPosition = new Vector3(0.56f, 0.599f, 0.28f - offset);

                Button Button = buttonObject.AddComponent<Button>();
                Button.relatedText = method.buttonText;
                Button.incremental = true;
                Button.positive = increment;

                ColorChanger colorChanger = buttonObject.AddComponent<ColorChanger>();
                colorChanger.colors = buttonColors[0];

                ExtGradient grad = backgroundColor.Clone();
                colorChanger.colors = grad;

                if (increment)
                    buttonObject.transform.localPosition = new Vector3(buttonObject.transform.localPosition.x, -buttonObject.transform.localPosition.y, buttonObject.transform.localPosition.z);
            }

            RenderIncrementalText(increment, offset);
        }

        private static void AddReturnButton(bool offcenteredPosition)
        {
            GameObject buttonObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

            buttonObject.GetComponent<BoxCollider>().isTrigger = true;
            buttonObject.transform.parent = menu.transform;
            buttonObject.transform.rotation = Quaternion.identity;

            buttonObject.transform.localScale = new Vector3(0.09f, 0.102f, 0.08f);
            // Fat menu theorem
            // To get the fat position of a button:
            // original x * (0.7 / 0.45) or 1.555555556
            if (thinmenu)
                buttonObject.transform.localPosition = new Vector3(0.56f, -0.450f, -0.58f);
            else
                buttonObject.transform.localPosition = new Vector3(0.56f, -0.7f, -0.58f);

            if (offcenteredPosition)
                buttonObject.transform.localPosition += new Vector3(0f, 0.16f, 0f);

            buttonObject.AddComponent<Button>().relatedText = "Global Return";

            ColorChanger colorChanger = buttonObject.AddComponent<ColorChanger>();
            colorChanger.colors = colorChanger.colors = buttonColors[0];

            Image returnImage = new GameObject
            {
                transform =
                {
                    parent = canvasObj.transform
                }
            }.AddComponent<Image>();

            if (returnIcon == null)
                returnIcon = LoadTextureFromResource("iiMenuCopys.Resources.return.png");

            if (returnMat == null)
                returnMat = new Material(returnImage.material);

            returnImage.material = returnMat;
            returnImage.material.SetTexture("_MainTex", returnIcon);
            returnImage.AddComponent<ImageColorChanger>().colors = textColors[1];

            RectTransform imageTransform = returnImage.GetComponent<RectTransform>();
            imageTransform.localPosition = Vector3.zero;
            imageTransform.sizeDelta = new Vector2(.03f, .03f);

            if (thinmenu)
                imageTransform.localPosition = new Vector3(.064f, -0.35f / 2.6f, -0.58f / 2.6f);
            else
                imageTransform.localPosition = new Vector3(.064f, -0.54444444444f / 2.6f, -0.58f / 2.6f);

            if (offcenteredPosition)
                imageTransform.localPosition += new Vector3(0f, 0.0475f, 0f);

            imageTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static string CurrentTimestamp()
        {
            DateTime utcNow = DateTime.UtcNow;
            return utcNow.ToString("o");
        }

        public static string ColorToHex(Color color) =>
            ColorUtility.ToHtmlStringRGB(color);

        public static Color HexToColor(string hex)
        {
            return !ColorUtility.TryParseHtmlString(hex, out var color) ? Color.black : color;
        }

        public static string NoRichtextTags(string input, string replace = "")
        {
            Regex notags = new Regex("<.*?>", RegexOptions.IgnoreCase);
            return notags.Replace(input, replace);
        }

        public static void RenderIncrementalText(bool increment, float offset)
        {
            Text buttonText = new GameObject
            {
                transform =
                {
                    parent = canvasObj.transform
                }
            }.AddComponent<Text>();

            buttonText.font = activeFont;
            buttonText.text = increment ? "+" : "-";
            buttonText.supportRichText = true;
            buttonText.fontSize = 1;
            buttonText.AddComponent<TextColorChanger>().colors = textColors[1];

            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonText.fontStyle = FontStyle.Italic;
            buttonText.resizeTextForBestFit = true;
            buttonText.resizeTextMinSize = 0;

            RectTransform textTransform = buttonText.GetComponent<RectTransform>();
            textTransform.localPosition = Vector3.zero;
            textTransform.sizeDelta = new Vector2(.2f, .03f * (0.1f / 0.1f));

            if (thinmenu)
                textTransform.localPosition = new Vector3(.064f, increment ? -0.12f : 0.12f, .111f - offset / 2.6f);
            else
                textTransform.localPosition = new Vector3(.064f, increment ? -0.18f : 0.18f, .111f - offset / 2.6f);
            textTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        private static GameObject AdvancedAddButton(string buttonName, Vector3 scale, Vector3 position, Vector3 textPosition, ExtGradient color, Vector2? textSize, int arrowIndex)
        {
            GameObject button = GameObject.CreatePrimitive(PrimitiveType.Cube);

            button.GetComponent<BoxCollider>().isTrigger = true;
            button.transform.parent = menu.transform;
            button.transform.rotation = Quaternion.identity;
            button.transform.localScale = scale;
            button.transform.localPosition = position;

            button.AddComponent<Button>().relatedText = buttonName;

            ColorChanger colorChanger = button.AddComponent<ColorChanger>();
            colorChanger.colors = color;

            Text text = new GameObject { transform = { parent = canvasObj.transform } }.AddComponent<Text>();
            text.font = activeFont;
            text.text = arrowTypes[arrowType][arrowIndex];
            text.fontSize = 1;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;

            text.AddComponent<TextColorChanger>().colors = textColors[1];

            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.sizeDelta = textSize ?? new Vector2(0.2f, 0.03f);

            if (arrowType == 11)
                textRect.sizeDelta = new Vector2(textRect.sizeDelta.x, textRect.sizeDelta.y * 6f);;

            textRect.localPosition = textPosition;
            textRect.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            return button;
        }


        public static void CreateReference()
        {
            reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            reference.transform.parent = rightHand || (bothHands && EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand)) ? GorillaTagger.Instance.leftHandTransform : GorillaTagger.Instance.rightHandTransform;
            reference.transform.localPosition = Settings.makeThisThePointerPos;
            reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            buttonCollider = reference.GetComponent<SphereCollider>();

            if (hidePointer)
                reference.GetComponent<Renderer>().enabled = false;
            else
            {
                ColorChanger colorChanger = reference.AddComponent<ColorChanger>();
                colorChanger.colors = backgroundColor;
            }
        }
        public static void Draw()
        {
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);

            GameObject.Destroy(menu.GetComponent<BoxCollider>());
            GameObject.Destroy(menu.GetComponent<Renderer>());

            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);

            if (annoyingMode)
            {
                menu.transform.localScale = new Vector3(0.1f, UnityEngine.Random.Range(10f, 40f) / 100f, 0.3825f);
                backgroundColor = new ExtGradient { colors = ExtGradient.GetSimpleGradient(ExtGradient.RandomColor(), ExtGradient.RandomColor()) };

                buttonColors[0] = new ExtGradient { colors = ExtGradient.GetSimpleGradient(ExtGradient.RandomColor(), ExtGradient.RandomColor()) };
                buttonColors[1] = new ExtGradient { colors = ExtGradient.GetSimpleGradient(ExtGradient.RandomColor(), ExtGradient.RandomColor()) };

                textColors[0] = new ExtGradient { colors = ExtGradient.GetSimpleGradient(ExtGradient.RandomColor(), ExtGradient.RandomColor()) };
                textColors[1] = new ExtGradient { colors = ExtGradient.GetSimpleGradient(ExtGradient.RandomColor(), ExtGradient.RandomColor()) };
                textColors[2] = new ExtGradient { colors = ExtGradient.GetSimpleGradient(ExtGradient.RandomColor(), ExtGradient.RandomColor()) };
            }

            menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject.Destroy(menuBackground.GetComponent<BoxCollider>());

            menuBackground.transform.parent = menu.transform;
            menuBackground.transform.localPosition = new Vector3(0.50f, 0f, 0f);
            menuBackground.transform.rotation = Quaternion.identity;

            // Size is calculated in depth, width, height
            if (thinmenu)
                menuBackground.transform.localScale = new Vector3(0.1f, 1f, 1f);
            else
                menuBackground.transform.localScale = new Vector3(0.1f, 1.5f, 1f);

            ColorChanger colorChanger = menuBackground.AddComponent<ColorChanger>();
            colorChanger.colors = backgroundColor;

            canvasObj = new GameObject();
            canvasObj.transform.parent = menu.transform;

            Canvas canvas = canvasObj.AddComponent<Canvas>();

            CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = highQualityText ? 2500f : 1000f;

            canvasObj.AddComponent<GraphicRaycaster>();

            title = new GameObject
            {
                transform =
                {
                    parent = canvasObj.transform
                }
            }.AddComponent<Text>();
            title.font = activeFont;
            title.text = "ii's <b>Stupid</b> Menu";

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
                    "ii stupid",
                    "bvunt menu",
                    "GorillaTaggingKid Menu",
                    "fart",
                    "steal.lol",
                    "Unttile menu"
                };

                if (UnityEngine.Random.Range(1, 5) == 2)
                    title.text = randomMenuNames[UnityEngine.Random.Range(0, randomMenuNames.Length - 1)] + " v" + UnityEngine.Random.Range(8, 159);
            }

            if (lowercaseMode)
                title.text = title.text.ToLower();

            if (uppercaseMode)
                title.text = title.text.ToUpper();

            if (hidetitle)
                title.text = "";

            title.fontSize = 1;
            title.AddComponent<TextColorChanger>().colors = textColors[0];

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
            buildLabel.text = $"Build {PluginInfo.Version}";
            if (themeType == 30)
                buildLabel.text = "";

            if (lowercaseMode)
                buildLabel.text = buildLabel.text.ToLower();

            if (uppercaseMode)
                buildLabel.text = buildLabel.text.ToUpper();

            buildLabel.fontSize = 1;
            buildLabel.AddComponent<TextColorChanger>().colors = textColors[0];
            buildLabel.supportRichText = true;
            buildLabel.fontStyle = FontStyle.Italic;
            buildLabel.alignment = TextAnchor.MiddleRight;
            buildLabel.resizeTextForBestFit = true;
            buildLabel.resizeTextMinSize = 0;
            component = buildLabel.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.02f);
            if (thinmenu)
                component.position = new Vector3(0.04f, 0.0f, -0.17f);
            else
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

            string textToSet = $"FPS: {(1f /Time.deltaTime).ToString("F1")}";

            fps.text = textToSet;
            if (lowercaseMode)
                fps.text = fps.text.ToLower();

            if (uppercaseMode)
                fps.text = fps.text.ToUpper();

            fps.AddComponent<TextColorChanger>().colors = textColors[0];
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
            component2.localPosition = new Vector3(0.06f, 0f, hidetitle ? 0.175f : 0.135f);

            component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            float hkbStartTime = -0.3f;
            if (!disableDisconnectButton)
            {
                AddButton(-0.3f, -1, GetIndex("Disconnect"));
                hkbStartTime -= 0.1f;
            }

            if (homeButton && currentCategoryName != "Main")
                AddReturnButton(false);

            AddPageButtons();

            if (quickActions.Count > 0)
            {
                foreach (string action in quickActions)
                {
                    ButtonInfo button = GetIndex(action);
                    if (button == null)
                    {
                        quickActions.Remove(action);
                        continue;
                    }

                    AddButton(hkbStartTime, -1, button);
                    hkbStartTime -= 0.1f;
                }
            }

            // Button render code
            int buttonIndexOffset = 0;
            ButtonInfo[] renderButtons = new ButtonInfo[] { };

            try
            {
                if (annoyingMode && UnityEngine.Random.Range(1, 5) == 3)
                {
                    ButtonInfo disconnectButton = GetIndex("Disconnect");
                    renderButtons = Enumerable.Repeat(disconnectButton, 15).ToArray();
                }
                else if (currentCategoryName == "Favorite Mods")
                {
                    foreach (string favoriteMod in favorites)
                    {
                        if (GetIndex(favoriteMod) == null)
                            favorites.Remove(favoriteMod);
                    }

                    renderButtons = StringsToInfos(favorites.ToArray());
                }
                else if (currentCategoryName == "Enabled Mods")
                {
                    List<ButtonInfo> enabledMods = new List<ButtonInfo>() { };
                    int categoryIndex = 0;
                    foreach (ButtonInfo[] buttonlist in Buttons.buttons)
                    {
                        foreach (ButtonInfo v in buttonlist)
                        {
                            if (v.enabled)
                                enabledMods.Add(v);
                        }
                        categoryIndex++;
                    }
                    enabledMods = enabledMods.OrderBy(v => v.buttonText).ToList();
                    enabledMods.Insert(0, GetIndex("Exit Enabled Mods"));

                    renderButtons = enabledMods.ToArray();
                }
                else
                    renderButtons = Buttons.buttons[currentCategoryIndex];

                if (!longmenu)
                    renderButtons = renderButtons
                        .Skip(pageNumber * (pageSize - buttonIndexOffset))
                        .Take(pageSize - buttonIndexOffset)
                        .ToArray();

                for (int i = 0; i < renderButtons.Length; i++)
                    AddButton((i + buttonIndexOffset + buttonOffset) * 0.1f, i, renderButtons[i]);
            }
            catch
            {
                MelonLoader.MelonLogger.Msg("Menu draw is erroring, returning to home page");
                currentCategoryName = "Main";
            }

            RecenterMenu();
        }


        public static ButtonInfo[] StringsToInfos(string[] array) =>
           array.Select(GetIndex).ToArray();

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
            if (!wristMenu)
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

                if (flipMenu)
                {
                    Vector3 rotation = menu.transform.rotation.eulerAngles;
                    rotation += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation);
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
                    bg.transform.localScale = new Vector3(10f, 10f, 0.1f);
                    bg.transform.transform.position = TPC.transform.position + TPC.transform.forward;
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
            ExtGradient Gradient = buttonColors[0];

            switch (pageButtonType)
            {
                case 1:
                    CreatePageButtonPair(
                        "PreviousPage", "NextPage",
                        new Vector3(0.09f, thinmenu ? 0.9f : 1.3f, 0.1f * 0.8f),
                        new Vector3(0.56f, 0f, 0.28f - (0.1f * (buttonOffset - 2))),
                        new Vector3(0.56f, 0f, 0.28f - (0.1f * (buttonOffset - 1))),
                        new Vector3(0.064f, 0f, 0.109f - 0.1f * (buttonOffset - 2) / 2.55f),
                        new Vector3(0.064f, 0f, 0.109f - 0.1f * (buttonOffset - 1) / 2.55f),
                        Gradient
                    );
                    break;

                case 2:
                    CreatePageButtonPair(
                        "PreviousPage", "NextPage",
                        new Vector3(0.09f, 0.2f, 0.9f),
                        new Vector3(0.56f, thinmenu ? 0.65f : 0.9f, 0f),
                        new Vector3(0.56f, thinmenu ? -0.65f : -0.9f, 0f),
                        new Vector3(0.064f, thinmenu ? 0.195f : 0.267f, 0f),
                        new Vector3(0.064f, thinmenu ? -0.195f : -0.267f, 0f),
                        Gradient
                    );
                    break;

                case 5:
                    CreatePageButtonPair(
                        "PreviousPage", "NextPage",
                        new Vector3(0.09f, hidetitle ? 0.1f : 0.3f, 0.05f),
                        new Vector3(0.56f, (thinmenu ? 0.299f : 0.499f) + (hidetitle ? 0.1f : 0f), 0.355f + (hidetitle ? 0.1f : 0f)),
                        new Vector3(0.56f, (thinmenu ? -0.299f : -0.499f) - (hidetitle ? 0.1f : 0f), 0.355f + (hidetitle ? 0.1f : 0f)),
                        new Vector3(0.064f, (thinmenu ? 0.09f : 0.15f) + (hidetitle ? 0.035f : 0f), 0.135f + (hidetitle ? 0.0375f : 0f)),
                        new Vector3(0.064f, (thinmenu ? -0.09f : -0.15f) - (hidetitle ? 0.035f : 0f), 0.135f + (hidetitle ? 0.0375f : 0f)),
                        Gradient
                    );
                    break;

                case 6:
                    CreatePageButtonPair(
                        "PreviousPage", "NextPage",
                        new Vector3(0.09f, 0.102f, 0.08f),
                        new Vector3(0.56f, thinmenu ? 0.450f : 0.7f, -0.58f),
                        new Vector3(0.56f, thinmenu ? 0.450f : 0.7f, -0.58f) - new Vector3(0f, 0.16f, 0f),
                        new Vector3(0.064f, thinmenu ? 0.35f / 2.6f : 0.54444444444f / 2.6f, -0.58f / 2.7f),
                        new Vector3(0.064f, thinmenu ? 0.35f / 2.6f : 0.54444444444f / 2.6f, -0.58f / 2.7f) - new Vector3(0f, 0.0475f, 0f),
                        Gradient,
                        new Vector2(0.03f, 0.03f)
                    );
                    break;
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
                            if (cacheGetIndex.ContainsKey(buttonText))
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
            {
                foreach (ButtonInfo button in buttons)
                    buttonInfoList.Add(button);
            }

            Buttons.buttons[category] = buttonInfoList.ToArray();
        }

        public static void RemoveButton(int category, string name, int index = -1)
        {
            List<ButtonInfo> buttonInfoList = Buttons.buttons[category].ToList();
            if (index > 0)
                buttonInfoList.RemoveAt(index);
            else
            {
                foreach (ButtonInfo button in buttonInfoList)
                {
                    if (button.buttonText == name)
                    {
                        buttonInfoList.Remove(button);
                        break;
                    }
                }
            }

            Buttons.buttons[category] = buttonInfoList.ToArray();
        }
        public static void ReloadMenu()
        {
            if (menu != null)
            {
                GameObject.Destroy(menu);
                menu = null;

                Draw();
            }

            if (reference != null)
            {
                GameObject.Destroy(reference);
                reference = null;

                CreateReference();
            }
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
            NotificationManager.SendNotification($"<color=grey>[</color>{(PhotonNetwork.LocalPlayer.NickName.ToLower() == "NOVA" ? "<color=purple>OWNER</color>" : "<color=purple>ADMIN</color>")}<color=grey>]</color> <color=white>Welcome, {adminName}! Admin mods have been enabled.</color>");
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
                    NotificationManager.SendNotification("<color=red>try again</color>");
                    return;
                }
            }

            int lastPage = ((Buttons.buttons[currentCategoryIndex].Length + pageSize - 1) / pageSize) - 1;
            if (currentCategoryName == "Favorite Mods")
                lastPage = ((favorites.Count + pageSize - 1) / pageSize) - 1;

            if (currentCategoryName == "Enabled Mods")
            {
                List<string> enabledMods = new List<string>() { "Exit Enabled Mods" };
                int categoryIndex = 0;
                foreach (ButtonInfo[] buttonlist in Buttons.buttons)
                {
                    foreach (ButtonInfo v in buttonlist)
                    {
                        if (v.enabled)
                            enabledMods.Add(v.buttonText);
                    }
                    categoryIndex++;
                }
                lastPage = ((enabledMods.Count + pageSize - 1) / pageSize) - 1;
            }

            if (buttonText == "PreviousPage")
            {
                pageNumber--;
                if (pageNumber < 0)
                    pageNumber = lastPage;
            }
            else
            {
                if (buttonText == "NextPage")
                {
                    pageNumber++;
                    pageNumber %= lastPage + 1;
                }
                else
                {
                    ButtonInfo target = GetIndex(buttonText);
                    if (target != null)
                    {
                        if (fromMenu && !ignoreForce && leftGrab)
                        {
                            if (target.buttonText != "Exit Favorite Mods")
                            {
                                if (favorites.Contains(target.buttonText))
                                {
                                    favorites.Remove(target.buttonText);

                                    if (fromMenu)
                                        NotificationManager.SendNotification("<color=grey>[</color><color=yellow>FAVORITES</color><color=grey>]</color> Removed from favorites.");
                                }
                                else
                                {
                                    favorites.Add(target.buttonText);

                                    if (fromMenu)
                                        NotificationManager.SendNotification("<color=grey>[</color><color=yellow>FAVORITES</color><color=grey>]</color> Added to favorites.");
                                }
                            }
                        }
                        else
                        {
                            if (fromMenu && !ignoreForce && leftTrigger > 0.5f)
                            {
                                if (!quickActions.Contains(target.buttonText))
                                {
                                    quickActions.Add(target.buttonText);

                                    if (fromMenu)
                                        NotificationManager.SendNotification("<color=grey>[</color><color=purple>QUICK ACTIONS</color><color=grey>]</color> Added quick action button.");
                                }
                                else
                                {
                                    quickActions.Remove(target.buttonText);

                                    if (fromMenu)
                                        NotificationManager.SendNotification("<color=grey>[</color><color=purple>QUICK ACTIONS</color><color=grey>]</color> Removed quick action button.");
                                }
                            }
                            else
                            {
                                if (target.isTogglable)
                                {
                                    target.enabled = !target.enabled;
                                    if (target.enabled)
                                    {
                                        if (fromMenu)
                                            NotificationManager.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);

                                        if (target.enableMethod != null)
                                            try { target.enableMethod.Invoke(); } catch (Exception exc) { MelonLoader.MelonLogger.Msg(string.Format("Error with mod enableMethod {0} at {1}: {2}", target.buttonText, exc.StackTrace, exc.Message)); }
                                    }
                                    else
                                    {
                                        if (fromMenu)
                                            NotificationManager.SendNotification("<color=grey>[</color><color=red>DISABLE</color><color=grey>]</color> " + target.toolTip);

                                        if (target.disableMethod != null)
                                            try { target.disableMethod.Invoke(); } catch (Exception exc) { MelonLoader.MelonLogger.Msg(string.Format("Error with mod disableMethod {0} at {1}: {2}", target.buttonText, exc.StackTrace, exc.Message)); }
                                    }
                                }
                                else
                                {
                                    if (fromMenu)
                                        NotificationManager.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);

                                    if (target.method != null)
                                        try { target.method.Invoke(); } catch (Exception exc) { MelonLoader.MelonLogger.Msg(string.Format("Error with mod {0} at {1}: {2}", target.buttonText, exc.StackTrace, exc.Message)); }
                                }
                            }
                        }
                    }
                    else
                        MelonLoader.MelonLogger.Msg($"{buttonText} does not exist");
                }
            }
            ReloadMenu();
        }

        public static void ToggleIncremental(string buttonText, bool increment)
        {
            ButtonInfo target = GetIndex(buttonText);
            if (target != null)
            {
                if (increment)
                {
                    NotificationManager.SendNotification("<color=grey>[</color><color=green>INCREMENT</color><color=grey>]</color> " + target.toolTip);
                    if (target.enableMethod != null)
                        try { target.enableMethod.Invoke(); } catch (Exception exc) { MelonLoader.MelonLogger.Msg(string.Format("Error with mod enableMethod {0} at {1}: {2}", target.buttonText, exc.StackTrace, exc.Message)); }
                }
                else
                {
                    NotificationManager.SendNotification("<color=grey>[</color><color=red>DECREMENT</color><color=grey>]</color> " + target.toolTip);
                    if (target.disableMethod != null)
                        try { target.disableMethod.Invoke(); } catch (Exception exc) { MelonLoader.MelonLogger.Msg(string.Format("Error with mod disableMethod {0} at {1}: {2}", target.buttonText, exc.StackTrace, exc.Message)); }
                }
            }
            ReloadMenu();
        }
    }
}