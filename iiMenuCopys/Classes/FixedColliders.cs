using UnityEngine;

/*
 *  HEY SKIDDERS
 *  leave alone
*/

namespace iiMenu
{
    public class FixedColliders
    {
        public static string relatedText;
        public static GameObject reference;
        public static GameObject button;
        public static void CheckButton()
        {
            float distanceButton = Vector3.Distance(button.transform.position, reference.transform.position);

            if (Time.frameCount >= iiMenu.Menu.Main.framePressCooldown + 30 && distanceButton <= 0.02)
            {
                iiMenu.Menu.Main.Toggle(relatedText);
                iiMenu.Menu.Main.framePressCooldown = Time.frameCount;
            }
        }
    }
}