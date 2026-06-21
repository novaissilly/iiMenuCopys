using UnityEngine;
using static iiMenu.Classes.RigManager;
using static iiMenu.Menu.Main;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods
{
    internal class Miscellaneous
    {
        public static void CopyIDGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;
                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who != null && who != GorillaTagger.Instance.myVRRig)
                    {
                        GUIUtility.systemCopyBuffer = GetPlayerFromVRRig(who).UserId;
                    }
                }
            }
        }

        public static void GrabPlayerInfo()
        {

        }
    }
}