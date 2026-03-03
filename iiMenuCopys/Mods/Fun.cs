using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using static iiMenu.Classes.RigManager;
using static iiMenu.Menu.Main;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods
{
    internal class Fun
    {
        public static void NightTime()
        {
            BetterDayNightManager.instance.timeOfDay = 0;
        }

        public static void DayTime()
        {
            BetterDayNightManager.instance.timeOfDay = 1;
        }

        public static void FixHead()
        {
            //GorillaTagger.Instance.myVRRig.head.trackingRotationOffset.x = 0f;
            //GorillaTagger.Instance.myVRRig.head.trackingRotationOffset.y = 0f;
            //GorillaTagger.Instance.myVRRig.head.trackingRotationOffset.z = 0f;
        }

        public static void UpsideDownHead()
        {
            //GorillaTagger.Instance.myVRRig.head.trackingRotationOffset.z = 180f;
        }

        public static void SpinHeadX()
        {
           // GorillaTagger.Instance.myVRRig.head.trackingRotationOffset.x += 10f;
        }

        public static void SpinHeadY()
        {
           // GorillaTagger.Instance.myVRRig.head.trackingRotationOffset.y += 10f;
        }

        public static void SpinHeadZ()
        {
           // GorillaTagger.Instance.myVRRig.head.trackingRotationOffset.z =+ GorillaTagger.Instance.myVRRig.head.trackingRotationOffset.z+ 10f;
        }

        public static void FlipHands()
        {
            Vector3 lh = GorillaTagger.Instance.leftHandTransform.position;
            Vector3 rh = GorillaTagger.Instance.rightHandTransform.position;
            Quaternion lhr = GorillaTagger.Instance.leftHandTransform.rotation;
            Quaternion rhr = GorillaTagger.Instance.rightHandTransform.rotation;

            GorillaLocomotion.Player.Instance.rightHandTransform.transform.position = lh;
            GorillaLocomotion.Player.Instance.leftHandTransform.transform.position = rh;

            GorillaLocomotion.Player.Instance.rightHandTransform.transform.rotation = lhr;
            GorillaLocomotion.Player.Instance.leftHandTransform.transform.rotation = rhr;
        }

        public static void FixHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = 0.1f;
        }

        public static void LoudHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = int.MaxValue;
        }

        public static void SilentHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = 0;
        }

        public static void EnableInstantHandTaps()
        {
            GorillaTagger.Instance.tapCoolDown = 0f;
        }

        public static void DisableInstantHandTaps()
        {
            GorillaTagger.Instance.tapCoolDown = 0.33f;
        }

        public static void WaterSplashHands()
        {
            if (rightGrab)
            {
                if (Time.time > splashDel)
                {
                    /*GorillaTagger.Instance.myVRRig.RPC("PlaySplashEffect", RpcTarget.All, new object[]
                    {
                        GorillaTagger.Instance.rightHandTransform.position,
                        GorillaTagger.Instance.rightHandTransform.rotation,
                        4f,
                        100f,
                        true,
                        false
                    });*/
                    RPCProtection();
                    splashDel = Time.time + 0.1f;
                }
            }
            if (leftGrab)
            {
                if (Time.time > splashDel)
                {
                    /*GorillaTagger.Instance.myVRRig.RPC("PlaySplashEffect", RpcTarget.All, new object[]
                    {
                        GorillaTagger.Instance.leftHandTransform.position,
                        GorillaTagger.Instance.leftHandTransform.rotation,
                        4f,
                        100f,
                        true,
                        false
                    });*/
                    RPCProtection();
                    splashDel = Time.time + 0.1f;
                }
            }
        }

        public static void WaterSplashAura()
        {
            if (Time.time > splashDel)
            {
                /*GorillaTagger.Instance.myVRRig.RPC("PlaySplashEffect", RpcTarget.All, new object[]
                {
                    GorillaTagger.Instance.myVRRig.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f,0.5f),UnityEngine.Random.Range(-0.5f,0.5f),UnityEngine.Random.Range(-0.5f,0.5f)),
                    GorillaTagger.Instance.myVRRig.transform.rotation,
                    4f,
                    100f,
                    true,
                    false
                });*/
                RPCProtection();
                splashDel = Time.time + 0.1f;
            }
        }

        public static void WaterSplashGun()
        {

        }

        public static void BugGun()
        {

        }

        public static void BatGun()
        {

        }

        public static void BeachBallGun()
        {

        }

        public static void GrabBug()
        {
            if (rightGrab)
            {
                GameObject.Find("Floating Bug Holdable").transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
        }

        public static void GrabBat()
        {
            if (rightGrab)
            {
                GameObject.Find("Cave Bat Holdable").transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
        }

        public static void GrabBeachBall()
        {
            if (rightGrab)
            {
                GameObject.Find("BeachBall").transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
        }

        public static void DestroyBug()
        {
            GameObject.Find("Floating Bug Holdable").transform.position = new Vector3(99999f, 99999f, 99999f);
        }

        public static void DestroyBat()
        {
            GameObject.Find("Cave Bat Holdable").transform.position = new Vector3(99999f, 99999f, 99999f);
        }

        public static void DestroyBeachBall()
        {
            GameObject.Find("BeachBall").transform.position = new Vector3(99999f, 99999f, 99999f);
        }

        public static void PopAllBalloons()
        {
            foreach (BalloonHoldable balloon in GameObject.FindObjectsOfType<BalloonHoldable>())
            {
                balloon.OwnerPopBalloon();
                balloon.PopBalloon();
            }
        }

        public static void GrabBalloons()
        {
            if (rightGrab)
            {
                foreach (BalloonHoldable balloon in GameObject.FindObjectsOfType<BalloonHoldable>())
                {
                    balloon.gameObject.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                }
            }
        }

        public static void BalloonGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    foreach (BalloonHoldable balloon in GameObject.FindObjectsOfType<BalloonHoldable>())
                    {
                        balloon.gameObject.transform.position = GunPointer.transform.position + new Vector3(0f, 1f, 0f);
                    }
                }
            }
        }

        public static void DestroyBalloons()
        {
            foreach (BalloonHoldable balloon in GameObject.FindObjectsOfType<BalloonHoldable>())
            {
                balloon.gameObject.transform.position = new Vector3(99999f, 99999f, 99999f);
            }
        }

        public static void GrabTrain()
        {
            if (rightGrab)
            {
                GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Holiday2023Forest/Holiday2023Forest_Gameplay/NCTrain_Kit_Prefab").transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
        }

        public static void TrainGun()
        {
            if (rightGrab || Mouse.current.rightButton.isPressed)
            {
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward, out var Ray);
                if (shouldBePC)
                {
                    Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                    Physics.Raycast(ray, out Ray, 100);
                }

                GameObject NewPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                NewPointer.GetComponent<Renderer>().material.color = bgColorA;
                NewPointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                NewPointer.transform.position = Ray.point;
                UnityEngine.Object.Destroy(NewPointer.GetComponent<BoxCollider>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Collider>());
                UnityEngine.Object.Destroy(NewPointer, Time.deltaTime);
                if (rightTrigger > 0.5f || Mouse.current.leftButton.isPressed)
                {
                    GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Holiday2023Forest/Holiday2023Forest_Gameplay/NCTrain_Kit_Prefab").transform.position = NewPointer.transform.position + new Vector3(0f, 1f, 0f);
                }
            }
        }

        public static void DestroyTrain()
        {
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Holiday2023Forest/Holiday2023Forest_Gameplay/NCTrain_Kit_Prefab").transform.position = new Vector3(99999f, 99999f, 99999f);
        }

        public static void SlowTrain()
        {
            GameObject train = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Holiday2023Forest/Holiday2023Forest_Gameplay/NCTrain_Kit_Prefab/NCTrainEngine_Prefab");
            train.GetComponent<PhotonView>().ControllerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            train.GetComponent<PhotonView>().OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            //train.GetComponent<TraverseSpline>().duration = 60f;
        }

        public static void FastTrain()
        {
            GameObject train = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Holiday2023Forest/Holiday2023Forest_Gameplay/NCTrain_Kit_Prefab/NCTrainEngine_Prefab");
            train.GetComponent<PhotonView>().ControllerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            train.GetComponent<PhotonView>().OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            //train.GetComponent<TraverseSpline>().duration = 10f;
        }

        public static void FixTrain()
        {
            GameObject train = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Holiday2023Forest/Holiday2023Forest_Gameplay/NCTrain_Kit_Prefab/NCTrainEngine_Prefab");
            train.GetComponent<PhotonView>().ControllerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            train.GetComponent<PhotonView>().OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            //train.GetComponent<TraverseSpline>().duration = 30f;
        }

        public static void RemoveName()
        {
            ChangeName("________");
        }

        public static void SetNameToSTATUE()
        {
            ChangeName("STATUE");
        }

        public static void SetNameToRUN()
        {
            ChangeName("RUN");
        }

        public static void SetNameToiiOnTop()
        {
            ChangeName("iiOnTop");
        }

        public static void SetNameToBEHINDYOU()
        {
            ChangeName("BEHINDYOU");
        }

        public static void PBBVNameCycle()
        {
            if (Time.time > nameCycleDelay)
            {
                nameCycleIndex++;
                if (nameCycleIndex > 3)
                {
                    nameCycleIndex = 1;
                }

                if (nameCycleIndex == 1)
                {
                    ChangeName("PBBV");
                }
                if (nameCycleIndex == 2)
                {
                    ChangeName("IS");
                }
                if (nameCycleIndex == 3)
                {
                    ChangeName("HERE");
                }

                nameCycleDelay = Time.time + 1f;
            }
        }

        public static void J3VUNameCycle()
        {
            if (Time.time > nameCycleDelay)
            {
                nameCycleIndex++;
                if (nameCycleIndex > 4)
                {
                    nameCycleIndex = 1;
                }

                if (nameCycleIndex == 1)
                {
                    ChangeName("J3VU");
                }
                if (nameCycleIndex == 2)
                {
                    ChangeName("HAS");
                }
                if (nameCycleIndex == 3)
                {
                    ChangeName("BECOME");
                }
                if (nameCycleIndex == 4)
                {
                    ChangeName("HOSTILE");
                }

                nameCycleDelay = Time.time + 1f;
            }
        }

        public static void RunRabbitNameCycle()
        {
            if (Time.time > nameCycleDelay)
            {
                nameCycleIndex++;
                if (nameCycleIndex > 2)
                {
                    nameCycleIndex = 1;
                }

                if (nameCycleIndex == 1)
                {
                    ChangeName("RUN");
                }
                if (nameCycleIndex == 2)
                {
                    ChangeName("RABBIT");
                }

                nameCycleDelay = Time.time + 1f;
            }
        }

        public static void RandomNameCycle()
        {
            if (Time.time > nameCycleDelay)
            {
                string random = "";
                for (int i = 0; i < 8; i++)
                {
                    random += letters[UnityEngine.Random.Range(0, letters.Length - 1)];
                }
                ChangeName(random);

                nameCycleDelay = Time.time + 1f;
            }
        }

        public static void StrobeColor()
        {
            if (Time.time > colorChangerDelay)
            {
                colorChangerDelay = Time.time + 0.1f;
                strobeColor = !strobeColor;
                ChangeColor(new Color(strobeColor ? 1 : 0, strobeColor ? 1 : 0, strobeColor ? 1 : 0));
            }
        }

        public static void RainbowColor()
        {
            if (Time.time > colorChangerDelay)
            {
                colorChangerDelay = Time.time + 0.1f;
                float h = (Time.frameCount / 180f) % 1f;
                ChangeColor(UnityEngine.Color.HSVToRGB(h, 1f, 1f));
            }
        }

        public static void HardRainbowColor()
        {
            if (Time.time > colorChangerDelay)
            {
                colorChangerDelay = Time.time + 1f;
                colorChangeType++;
                if (colorChangeType > 3)
                {
                    colorChangeType = 0;
                }
                Color[] colors = new Color[]
                {
                    Color.red,
                    Color.green,
                    Color.blue,
                    Color.magenta
                };

                ChangeColor(colors[colorChangeType]);
            }
        }

        public static void NegativeColor()
        {
            PlayerPrefs.SetFloat("redValue", -2147483648);
            PlayerPrefs.SetFloat("greenValue", -2147483648);
            PlayerPrefs.SetFloat("blueValue", -2147483648);

            GorillaTagger.Instance.UpdateColor(-2147483648, -2147483648, -2147483648);
            PlayerPrefs.Save();
            //GorillaTagger.Instance.myVRRig.RPC("InitializeNoobMaterial", RpcTarget.All, new object[] { -2147483648, -2147483648, -2147483648, false });
            RPCProtection();
        }

        public static void BecomeGoldentrophy()
        {
            ChangeName("goldentrophy");
            ChangeColor(new Color32(255, 128, 0, 255));
        }

        public static void BecomePBBV()
        {
            ChangeName("PBBV");
            ChangeColor(new Color32(230, 127, 102, 255));
        }

        public static void BecomeECHO()
        {
            ChangeName("ECHO");
            ChangeColor(new Color32(0, 150, 255, 255));
        }

        public static void BecomeDAISY09()
        {
            ChangeName("DAISY09");
            ChangeColor(new Color32(255, 81, 231, 255));
        }

        public static void BecomeHiddenOnLeaderboard()
        {
            ChangeName("________");
            ChangeColor(new Color32(0, 53, 2, 255));
        }

        public static void CopyIdentityGun()
        {
            if (rightGrab || Mouse.current.rightButton.isPressed)
            {
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward, out var Ray);
                if (shouldBePC)
                {
                    Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                    Physics.Raycast(ray, out Ray, 100);
                }

                GameObject NewPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                NewPointer.GetComponent<Renderer>().material.color = bgColorA;
                NewPointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                NewPointer.transform.position = Ray.point;
                UnityEngine.Object.Destroy(NewPointer.GetComponent<BoxCollider>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Collider>());
                UnityEngine.Object.Destroy(NewPointer, Time.deltaTime);
                if ((rightTrigger > 0.5f || Mouse.current.leftButton.isPressed) && Time.time > stealIdentityDelay)
                {
                    VRRig possibly = Ray.collider.GetComponentInParent<VRRig>();
                    if (possibly && possibly != GorillaTagger.Instance.myVRRig)
                    {
                        ChangeName(GetPlayerFromVRRig(possibly).NickName);
                        ChangeColor(possibly.mainSkin.material.GetColor(2));
                        stealIdentityDelay = Time.time + 0.5f;
                    }
                }
            }
        }

        public static void ChangeAccessories()
        {
            if (leftGrab && !lastHitL)
            {
                hat--;
                if (hat < 1)
                {
                    hat = 3;
                }

                if (hat == 1)
                {
                    GameObject.Find("Wardrobe/WardrobeItemButton").GetComponent<GorillaPressableButton>().ButtonActivation();
                }
                if (hat == 2)
                {
                    GameObject.Find("Wardrobe/WardrobeItemButton (1)").GetComponent<GorillaPressableButton>().ButtonActivation();
                }
                if (hat == 3)
                {
                    GameObject.Find("Wardrobe/WardrobeItemButton (2)").GetComponent<GorillaPressableButton>().ButtonActivation();
                }
            }
            lastHitL = leftGrab;
            if (rightGrab && !lastHitR)
            {
                hat++;
                if (hat < 1)
                {
                    hat = 3;
                }

                if (hat == 1)
                {
                    GameObject.Find("Wardrobe/WardrobeItemButton").GetComponent<GorillaPressableButton>().ButtonActivation();
                }
                if (hat == 2)
                {
                    GameObject.Find("Wardrobe/WardrobeItemButton (1)").GetComponent<GorillaPressableButton>().ButtonActivation();
                }
                if (hat == 3)
                {
                    GameObject.Find("Wardrobe/WardrobeItemButton (2)").GetComponent<GorillaPressableButton>().ButtonActivation();
                }
            }
            lastHitR = rightGrab;
            if (leftPrimary && !lastHitLP)
            {
                GameObject.Find("Wardrobe/WardrobeLeftButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
                if (hat == 1)
                {
                    GameObject.Find("Wardrobe/WardrobeItemButton").GetComponent<GorillaPressableButton>().ButtonActivation();
                }
                if (hat == 2)
                {
                    GameObject.Find("Wardrobe/WardrobeItemButton (1)").GetComponent<GorillaPressableButton>().ButtonActivation();
                }
                if (hat == 3)
                {
                    GameObject.Find("Wardrobe/WardrobeItemButton (2)").GetComponent<GorillaPressableButton>().ButtonActivation();
                }
            }
            lastHitLP = leftPrimary;

            if (rightPrimary && !lastHitRP)
            {
                GameObject.Find("Wardrobe/WardrobeRightItem").GetComponent<WardrobeFunctionButton>().ButtonActivation();
                if (hat == 1)
                {
                    GameObject.Find("Wardrobe/WardrobeItemButton").GetComponent<GorillaPressableButton>().ButtonActivation();
                }
                if (hat == 2)
                {
                    GameObject.Find("Wardrobe/WardrobeItemButton (1)").GetComponent<GorillaPressableButton>().ButtonActivation();
                }
                if (hat == 3)
                {
                    GameObject.Find("Wardrobe/WardrobeItemButton (2)").GetComponent<GorillaPressableButton>().ButtonActivation();
                }
            }
            lastHitRP = rightPrimary;

            if (rightSecondary && !lastHitRS)
            {
                accessoryType++;
                if (accessoryType > 4)
                {
                    accessoryType = 1;
                }
                if (accessoryType == 1)
                {
                    GameObject.Find("Wardrobe/WardobeHatButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
                }
                if (accessoryType == 2)
                {
                    GameObject.Find("Wardrobe/WardrobeFaceButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
                }
                if (accessoryType == 3)
                {
                    GameObject.Find("Wardrobe/WardrobeBadgeButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
                }
                if (accessoryType == 4)
                {
                    GameObject.Find("Wardrobe/WardrobeHoldableButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
                }
            }
            lastHitRS = rightSecondary;
        }

        public static void SpazAccessories()
        {
            int rando = UnityEngine.Random.Range(1, 9);

            if (rando == 1)
            {
                GameObject.Find("Wardrobe/WardrobeItemButton").GetComponent<GorillaPressableButton>().ButtonActivation();
            }
            if (rando == 2)
            {
                GameObject.Find("Wardrobe/WardrobeItemButton (1)").GetComponent<GorillaPressableButton>().ButtonActivation();
            }
            if (rando == 3)
            {
                GameObject.Find("Wardrobe/WardrobeItemButton (2)").GetComponent<GorillaPressableButton>().ButtonActivation();
            }

            if (rando == 4)
            {
                GameObject.Find("Wardrobe/WardrobeLeftButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
            }
            if (rando == 5)
            {
                GameObject.Find("Wardrobe/WardrobeRightItem").GetComponent<WardrobeFunctionButton>().ButtonActivation();
            }

            if (rando == 6)
            {
                GameObject.Find("Wardrobe/WardobeHatButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
            }
            if (rando == 7)
            {
                GameObject.Find("Wardrobe/WardrobeFaceButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
            }
            if (rando == 8)
            {
                GameObject.Find("Wardrobe/WardrobeBadgeButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
            }
            if (rando == 9)
            {
                GameObject.Find("Wardrobe/WardrobeHoldableButton").GetComponent<WardrobeFunctionButton>().ButtonActivation();
            }
        }

        public static void EnableCustomSoundOnJoin()
        {
            customSoundOnJoin = true;
        }

        public static void DisableCustomSoundOnJoin()
        {
            customSoundOnJoin = false;
        }
    }
}
