using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using static iiMenu.Classes.RigManager;
using static iiMenu.Menu.Main;
using static iiMenu.Mods.Spammers.Projectiles;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods
{
    internal class Overpowered
    {
        public static void SetMasterClient()
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            GorillaNot.instance.rpcErrorMax = int.MaxValue;
            GorillaNot.instance.currentMasterClient = PhotonNetwork.LocalPlayer;
            GorillaNot.instance.OnMasterClientSwitched(PhotonNetwork.LocalPlayer);
            GorillaGameManager.instance.currentMasterClient = PhotonNetwork.LocalPlayer;
        }

        public static void BetaSetStatus(int state, Photon.Realtime.Player plr)
        {
            switch (state)
            {
                case 0:
                    GorillaTagger.Instance.myVRRig.photonView.RPC("SetTaggedTime", plr, null);
                    GorillaTagger.Instance.myVRRig.photonView.RPC("SetSlowedTime", plr, null);
                    break;
                case 1:
                    GorillaTagger.Instance.myVRRig.photonView.RPC("SetJoinTaggedTime", plr, null);
                    break;
            }
        }

        public static void SlowGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;

                if (gunLocked && lockTarget != null)
                {
                    PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                    GorillaTagger.Instance.myVRRig.photonView.RPC("SetSlowedTime", lockTarget.photonView.Owner, null);
                }

                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who != null && who != GorillaTagger.Instance.myVRRig)
                    {
                        gunLocked = true;
                        lockTarget = who;
                    }
                }
            }
            else
            {
                if (gunLocked)
                {
                    gunLocked = false;
                }
            }
        }

        public static void SlowAll()
        {
            if (Time.time > kgDebounce)
            {
                foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                {
                    BetaSetStatus(0, player);
                    RPCProtection();
                    kgDebounce = Time.time + 0.5f;
                }
            }
        }

        public static void VibrateGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;

                if (gunLocked && lockTarget != null)
                {
                    PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                    GorillaTagger.Instance.myVRRig.photonView.RPC("SetTaggedTime", lockTarget.photonView.Owner, null);
                }

                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who != null && who != GorillaTagger.Instance.myVRRig)
                    {
                        gunLocked = true;
                        lockTarget = who;
                        BetaSetStatus(1, lockTarget.photonView.Owner);
                    }
                }
            }
            else
            {
                if (gunLocked)
                {
                    gunLocked = false;
                }
            }
        }

        public static void VibrateAll()
        {
            if (Time.time > kgDebounce)
            {
                foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                {
                    BetaSetStatus(1, player);
                    RPCProtection();
                    kgDebounce = Time.time + 0.5f;
                }
            }
        }

        public static void BlindGun()
        {

        }

        public static void CrashAll()
        {
            if (rightTrigger > 0.6f)
            {
                Experimental.SetMaster();
                foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
                {
                    PhotonNetwork.Instantiate("gorillaprefabs/gorilla player networked", iiMenu.Classes.RigManager.GetVRRigFromPlayer(plr).transform.position, Quaternion.identity);
                    PhotonNetwork.Instantiate("gorillaprefabs/gorilla player networked", iiMenu.Classes.RigManager.GetVRRigFromPlayer(plr).transform.position, Quaternion.identity);
                    GorillaTagger.Instance.myVRRig.photonView.RPC("RequestCosmetics", plr, null);
                    GorillaTagger.Instance.myVRRig.photonView.RPC("RequestCosmetics", plr, null);
                    GorillaTagger.Instance.myVRRig.photonView.RPC("RequestCosmetics", plr, null);
                    GorillaTagger.Instance.myVRRig.photonView.RPC("UpdatePlayerCosmetics", plr, null);
                    GorillaTagger.Instance.myVRRig.photonView.RPC("UpdatePlayerCosmetics", plr, null);
                    GorillaTagger.Instance.myVRRig.photonView.RPC("UpdatePlayerCosmetics", plr, null);
                    GorillaTagger.Instance.myVRRig.photonView.RPC("UpdatePlayerCosmetics", plr, null);
                    GorillaTagger.Instance.myVRRig.photonView.RPC("LaunchSlingshotProjectile", plr, null);
                    GorillaTagger.Instance.myVRRig.photonView.RPC("LaunchSlingShotProjectile", plr, null);
                    PhotonNetwork.DestroyPlayerObjects(plr);
                    PhotonNetwork.DestroyPlayerObjects(plr);
                    Hashtable goodCrash = new Hashtable();
                    goodCrash.Add("-1", "false");
                    PhotonNetwork.NetworkingClient.OpRaiseEvent(207, goodCrash, null, SendOptions.SendUnreliable);
                    PhotonNetwork.NetworkingClient.OpRaiseEvent(202, goodCrash, null, SendOptions.SendUnreliable);
                    PhotonNetwork.Instantiate("gorillaprefabs/gorilla player actual", iiMenu.Classes.RigManager.GetVRRigFromPlayer(plr).transform.position, Quaternion.identity);
                    PhotonNetwork.Instantiate("gorillaprefabs/gorilla player actual", iiMenu.Classes.RigManager.GetVRRigFromPlayer(plr).transform.position, Quaternion.identity);
                    for (int i = 0; i < 2500; i++)
                    {
                        PhotonNetwork.RaiseEvent(2, null, new RaiseEventOptions { TargetActors = new int[] { iiMenu.Classes.RigManager.GetVRRigFromPlayer(plr).photonView.Owner.ActorNumber } }, SendOptions.SendUnreliable);
                        PhotonNetwork.RaiseEvent(3, null, new RaiseEventOptions { TargetActors = new int[] { iiMenu.Classes.RigManager.GetVRRigFromPlayer(plr).photonView.Owner.ActorNumber } }, SendOptions.SendUnreliable);
                    }
                }
            }
        }

        public static void BlindAll()
        {
            VRRig randomRig = GetRandomVRRig(false);

            Vector3 startpos = randomRig.headMesh.transform.position + (randomRig.headMesh.transform.forward * 0.5f);
            Quaternion rot = randomRig.headMesh.transform.rotation;

            BetaFireProjectile("STICKABLE TARGET", startpos, rot);
        }

        public static void KickGun()
        {

        }

        public static void KickAll()
        {
            if (PhotonNetwork.InRoom && !PhotonNetwork.CurrentRoom.IsVisible)
            {

            }
        }
        /*
        public static void CrashGun()
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
                    VRRig possibly = Ray.collider.GetComponentInParent<VRRig>();
                    if (possibly && possibly != GorillaTagger.Instance.myVRRig)
                    {
                        isCopying = true;
                        whoCopy = possibly;
                    }
                }

                if (isCopying)
                {
                    Vector3 lagPosition = new Vector3(0f, 0f, 0f);

                    GorillaTagger.Instance.myVRRig.enabled = false;
                    GorillaTagger.Instance.myVRRig.transform.position = lagPosition;
                    GorillaTagger.Instance.myVRRig.transform.position = lagPosition;

                    GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                    l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                    GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                    r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                    l.GetComponent<Renderer>().material.color = bgColorA;
                    r.GetComponent<Renderer>().material.color = bgColorA;

                    UnityEngine.Object.Destroy(l, Time.deltaTime);
                    UnityEngine.Object.Destroy(r, Time.deltaTime);

                    string[] fullProjectileNames = new string[]
                    {
                        "SlingshotProjectile",
                        "SnowballProjectile",
                        "WaterBalloonProjectile",
                        "LavaRockProjectile",
                        "HornsSlingshotProjectile_PrefabV",
                        "CloudSlingshot_Projectile",
                        "CupidArrow_Projectile",
                        "IceSlingshotProjectile_PrefabV Variant",
                        "ElfBow_Projectile",
                        "MoltenRockSlingshot_Projectile",
                        "SpiderBowProjectile Variant",
                        "BucketGift_Cane_Projectile Variant",
                        "BucketGift_Coal_Projectile Variant",
                        "BucketGift_Roll_Projectile Variant",
                        "BucketGift_Round_Projectile Variant",
                        "BucketGift_Square_Projectile Variant"
                    };

                    string[] fullTrailNames = new string[]
                    {
                        "SlingshotProjectileTrail",
                        "HornsSlingshotProjectileTrail_PrefabV",
                        "CloudSlingshot_ProjectileTrailFX",
                        "CupidArrow_ProjectileTrailFX",
                        "IceSlingshotProjectileTrail Variant",
                        "ElfBow_ProjectileTrail",
                        "MoltenRockSlingshotProjectileTrail",
                        "SpiderBowProjectileTrail Variant"
                    };

                    //string projname = fullProjectileNames[UnityEngine.Random.Range(0, 15)];
                    string projname = fullProjectileNames[2];
                    string trailname = fullTrailNames[UnityEngine.Random.Range(0, 7)];

                    for (var i = 0; i < crashAmount; i++)
                    {
                        GameObject projectile = GameObject.Find("Environment Objects/PersistentObjects_Prefab/GlobalObjectPools/" + projname + "(Clone)");

                        GameObject trail = GameObject.Find("Environment Objects/PersistentObjects_Prefab/GlobalObjectPools/" + trailname + "(Clone)");

                        int hasha = PoolUtils.GameObjHashCode(projectile);
                        int hashb = PoolUtils.GameObjHashCode(trail);
                        int hashc = GorillaGameManager.instance.IncrementLocalPlayerProjectileCount();

                        Vector3 startpos = lagPosition - new Vector3(0f, 0.5f, 0f);
                        Vector3 charvel = Vector3.zero;

                        float randa = UnityEngine.Random.Range(0, 255);
                        float randb = UnityEngine.Random.Range(0, 255);
                        float randc = UnityEngine.Random.Range(0, 255);

                        GorillaGameManager.instance.photonView.RPC("LaunchSlingshotProjectile", GetPlayerFromVRRig(whoCopy), new object[]
                        {
                            startpos,
                            charvel,
                            hasha,
                            hashb,
                            false,
                            hashc,
                            true,
                            randa / 255f,
                            randb / 255f,
                            randc / 255f,
                            1f
                        });
                        RPCProtection();
                    }
                }
            }
            else
            {
                if (isCopying)
                {
                    isCopying = false;
                    GorillaTagger.Instance.myVRRig.enabled = true;
                }
            }
        }

        public static void CrashAll()
        {
            if (rightTrigger > 0.5f)
            {
                Vector3 lagPosition = new Vector3(0f, 0f, 0f);

                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.myVRRig.transform.position = lagPosition;
                GorillaTagger.Instance.myVRRig.transform.position = lagPosition;

                GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                l.GetComponent<Renderer>().material.color = bgColorA;
                r.GetComponent<Renderer>().material.color = bgColorA;

                UnityEngine.Object.Destroy(l, Time.deltaTime);
                UnityEngine.Object.Destroy(r, Time.deltaTime);

                string[] fullProjectileNames = new string[]
                {
                    "SlingshotProjectile",
                    "SnowballProjectile",
                    "WaterBalloonProjectile",
                    "LavaRockProjectile",
                    "HornsSlingshotProjectile_PrefabV",
                    "CloudSlingshot_Projectile",
                    "CupidArrow_Projectile",
                    "IceSlingshotProjectile_PrefabV Variant",
                    "ElfBow_Projectile",
                    "MoltenRockSlingshot_Projectile",
                    "SpiderBowProjectile Variant",
                    "BucketGift_Cane_Projectile Variant",
                    "BucketGift_Coal_Projectile Variant",
                    "BucketGift_Roll_Projectile Variant",
                    "BucketGift_Round_Projectile Variant",
                    "BucketGift_Square_Projectile Variant"
                };

                string[] fullTrailNames = new string[]
                {
                    "SlingshotProjectileTrail",
                    "HornsSlingshotProjectileTrail_PrefabV",
                    "CloudSlingshot_ProjectileTrailFX",
                    "CupidArrow_ProjectileTrailFX",
                    "IceSlingshotProjectileTrail Variant",
                    "ElfBow_ProjectileTrail",
                    "MoltenRockSlingshotProjectileTrail",
                    "SpiderBowProjectileTrail Variant"
                };

                //string projname = fullProjectileNames[UnityEngine.Random.Range(0, 15)];
                string projname = fullProjectileNames[2];
                string trailname = fullTrailNames[UnityEngine.Random.Range(0, 7)];

                for (var i = 0; i < crashAmount; i++)
                {
                    GameObject projectile = GameObject.Find("Environment Objects/PersistentObjects_Prefab/GlobalObjectPools/" + projname + "(Clone)");

                    GameObject trail = GameObject.Find("Environment Objects/PersistentObjects_Prefab/GlobalObjectPools/" + trailname + "(Clone)");

                    int hasha = PoolUtils.GameObjHashCode(projectile);
                    int hashb = PoolUtils.GameObjHashCode(trail);
                    int hashc = GorillaGameManager.instance.IncrementLocalPlayerProjectileCount();

                    Vector3 startpos = lagPosition - new Vector3(0f, 0.5f, 0f);
                    Vector3 charvel = Vector3.zero;

                    float randa = UnityEngine.Random.Range(0, 255);
                    float randb = UnityEngine.Random.Range(0, 255);
                    float randc = UnityEngine.Random.Range(0, 255);

                    GorillaGameManager.instance.photonView.RPC("LaunchSlingshotProjectile", RpcTarget.Others, new object[]
                    {
                        startpos,
                        charvel,
                        hasha,
                        hashb,
                        false,
                        hashc,
                        true,
                        randa / 255f,
                        randb / 255f,
                        randc / 255f,
                        1f
                    });
                    RPCProtection();
                }
            } else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
        }
        */

        public static void FlingGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;

                if (gunLocked && lockTarget != null)
                {
                    // Calculate a direction from gun to target
                    Vector3 direction = (lockTarget.transform.position - GunPointer.transform.position).normalized;

                    // Fling vector (upward or away depending on design)
                    Vector3 flingVelocity = direction * 15f + Vector3.up * 10f; // tweak these

                    PhotonMessageInfo fakeInfo = default; // since we’re local
                    lockTarget.AddVelocityToQueue(flingVelocity, fakeInfo);
                }

                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who != null && who != GorillaTagger.Instance.myVRRig)
                    {
                        gunLocked = true;
                        lockTarget = who;
                    }
                }
            }
            else
            {
                if (gunLocked)
                {
                    gunLocked = false;
                }
            }
        }


        public static void CrashGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;

                if (gunLocked && lockTarget != null)
                {
                    PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                    for (int i = 0; i < 2500; i++)
                    {
                        PhotonNetwork.RaiseEvent(2, null, new RaiseEventOptions { TargetActors = new int[] { lockTarget.photonView.Owner.ActorNumber } }, SendOptions.SendUnreliable);
                        PhotonNetwork.RaiseEvent(3, null, new RaiseEventOptions { TargetActors = new int[] { lockTarget.photonView.Owner.ActorNumber } }, SendOptions.SendUnreliable);
                    }
                    PhotonNetwork.DestroyPlayerObjects(lockTarget.photonView.Owner);
                }

                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who != null && who != GorillaTagger.Instance.myVRRig)
                    {
                        gunLocked = true;
                        lockTarget = who;
                    }
                }
            }
            else
            {
                if (gunLocked)
                {
                    gunLocked = false;
                }
            }
        }

        public static void DestroyGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;

                if (gunLocked && lockTarget != null)
                {
                    PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                    Photon.Realtime.Player player = GetPlayerFromVRRig(lockTarget);
                    PhotonNetwork.CurrentRoom.StorePlayer(player);
                    PhotonNetwork.CurrentRoom.Players.Remove(player.ActorNumber);
                    PhotonNetwork.OpRemoveCompleteCacheOfPlayer(player.ActorNumber);
                }

                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who != null && who != GorillaTagger.Instance.myVRRig)
                    {
                        gunLocked = true;
                        lockTarget = who;
                    }
                }
            }
            else
            {
                if (gunLocked)
                {
                    gunLocked = false;
                }
            }
        }

        public static void DestroyAll()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
            {
                PhotonNetwork.CurrentRoom.StorePlayer(player);
                PhotonNetwork.CurrentRoom.Players.Remove(player.ActorNumber);
                PhotonNetwork.OpRemoveCompleteCacheOfPlayer(player.ActorNumber);
            }
        }

        public static void BreakAudioGun()
        {

        }

        public static void BreakAudioAll()
        {
            // GorillaTagger.Instance.myVRRig.RPC("PlayHandTap", RpcTarget.Others, new object[]{
            //    213,
            ////   false,
            //999999f
            //   0.01f
            // });
        }
    }
}