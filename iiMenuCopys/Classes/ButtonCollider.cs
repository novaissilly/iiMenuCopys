using System;
using MelonLoader;
using UnityEngine;
using Main = iiMenu.Menu.Main;

namespace iiMenu.Classes
{
    [RegisterTypeInIl2Cpp]
    public class Button : MonoBehaviour
    {
        public string relatedText = string.Empty;
        public static float ButtonCooldown;

        public Button(IntPtr ptr) : base(ptr) { }

        public void OnTriggerEnter(Collider collider)
        {
            if (Time.time <= ButtonCooldown)
                return;
            if (collider != Main.buttonCollider)
                return;
            if (Main.menu == null)
                return;
            ButtonCooldown = Time.time + 0.2f;
            Main.Toggle(relatedText, true);
        }
    }
}