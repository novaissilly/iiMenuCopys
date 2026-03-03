using Photon.Pun;
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
    }
}