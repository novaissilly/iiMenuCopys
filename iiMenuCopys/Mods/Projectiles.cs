using easyInputs;
using Photon.Pun;
using UnhollowerBaseLib;
using UnityEngine;
using static iiMenu.Menu.Main;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods.Spammers
{
    internal class Projectiles
    {
        public static Vector3 rHandPos = GorillaTagger.Instance.rightHandTransform.position;
        public static Quaternion rHandRot = GorillaTagger.Instance.rightHandTransform.rotation;

        public static Vector3 lHandPos = GorillaTagger.Instance.leftHandTransform.position;
        public static Quaternion lHandRot = GorillaTagger.Instance.leftHandTransform.rotation;
        public static void BetaFireProjectile(string projectileName, Vector3 position, Quaternion velocity)
        {
            PhotonNetwork.Instantiate(projectileName, position, velocity);
        }

        public static string CurrentProjectile = "bulletPrefab";
        private static int ProjIndex = 0;
        private static string[] FullProjectileNamesPrefab = { "bulletPrefab", "STICKABLE TARGET", "Network Player", "gorillaprefabs/gorillaenemy" };
        private static string[] FullProjectileNames = { "Cube", "Target", "Network Player", "Enemy" };
        public static void ChangeProjectile()
        {
            ProjIndex++;
            if (ProjIndex == FullProjectileNames.Length)
            {
                ProjIndex = 0;
            }
            switch (ProjIndex)
            {
                case 0: CurrentProjectile = FullProjectileNamesPrefab[0]; break;
                case 1: CurrentProjectile = FullProjectileNamesPrefab[1]; break;
                case 2: CurrentProjectile = FullProjectileNamesPrefab[2]; break;
                case 3: CurrentProjectile = FullProjectileNamesPrefab[3]; break;
            }

            GetIndex("Change Projectile").overlapText = $"Change Projectile <color=grey>[</color><color=green>{FullProjectileNames[ProjIndex]}</color>]<color=grey></color>";
        }

        public static void ProjectileSpam()
        {
            if (rightGrab)
            {
                BetaFireProjectile(CurrentProjectile, rHandPos, rHandRot);
            }
            if (leftGrab)
            {
                BetaFireProjectile(CurrentProjectile, lHandPos, lHandRot);
            }
        }

        public static void Aura()
        {
            BetaFireProjectile(CurrentProjectile, GorillaTagger.Instance.myVRRig.headMesh.transform.position, Quaternion.Euler(40f, 2f, 40f));
        }

        public static void GiveProjectileSpamGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;

                if (gunLocked && lockTarget != null)
                {
                    if (lockTarget.rightMiddle.calcT > 0.5f && Time.time > projDebounce)
                    {
                        BetaFireProjectile(CurrentProjectile, lockTarget.rightHandTransform.position, lockTarget.rightHandTransform.rotation);
                    }
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

        public static void Slingshot()
        {
            if (EasyInputs.GetGripButtonDown(EasyHand.RightHand))
            {
                SendCSProjectile(-820530352, GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.up + GorillaTagger.Instance.rightHandTransform.forward * 15f);
            }
            if (EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
            {
                SendCSProjectile(-820530352, GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.up + GorillaTagger.Instance.leftHandTransform.forward * 15f);
            }
        }

        public static void Snowball()
        {
            if (EasyInputs.GetGripButtonDown(EasyHand.RightHand))
            {
                SendCSProjectile(-675036877, GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.up + GorillaTagger.Instance.rightHandTransform.forward * 15f);
            }
            if (EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
            {
                SendCSProjectile(-675036877, GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.up + GorillaTagger.Instance.leftHandTransform.forward * 15f);
            }
        }

        public static void Cloud()
        {
            if (EasyInputs.GetGripButtonDown(EasyHand.RightHand))
            {
                SendCSProjectile(1511318966, GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.up + GorillaTagger.Instance.rightHandTransform.forward * 15f);
            }
            if (EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
            {
                SendCSProjectile(1511318966, GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.up + GorillaTagger.Instance.leftHandTransform.forward * 15f);
            }
        }

        public static void Cupid()
        {
            if (EasyInputs.GetGripButtonDown(EasyHand.RightHand))
            {
                SendCSProjectile(825718363, GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.up + GorillaTagger.Instance.rightHandTransform.forward * 15f);
            }
            if (EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
            {
                SendCSProjectile(825718363, GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.up + GorillaTagger.Instance.leftHandTransform.forward * 15f);
            }
        }
        public static void Deadshot()
        {
            if (EasyInputs.GetGripButtonDown(EasyHand.RightHand))
            {
                SendCSProjectile(693334698, GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.up + GorillaTagger.Instance.rightHandTransform.forward * 15f);
            }
            if (EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
            {
                SendCSProjectile(693334698, GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.up + GorillaTagger.Instance.leftHandTransform.forward * 15f);
            }
        }


        public static void SendCSProjectile(int projHash, Vector3 position, Vector3 velocity)
        {
            GameObject sling = ObjectPools.instance.Instantiate(projHash);
            SlingshotProjectile sling2 = sling.gameObject.GetComponent<SlingshotProjectile>();
            Rigidbody rb = sling2.GetComponent<Rigidbody>();
            sling2.transform.position = position;
            rb.velocity = velocity;
            GameObject.Destroy(sling, 10f);
            GameObject.Destroy(sling2.gameObject, 10f);
        }
    }
}