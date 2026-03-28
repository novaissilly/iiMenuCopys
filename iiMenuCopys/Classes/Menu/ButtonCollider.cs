using System;
using UnityEngine;
using static iiMenu.Menu.Main;

namespace iiMenu.Classes
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Button : MonoBehaviour
    {
        public Button(IntPtr e) : base(e) { }
        public string relatedText;

        public bool incremental;
        public bool positive;

        public void OnTriggerEnter(Collider collider)
        {
            if (Time.time > buttonCooldown && (collider == buttonCollider) && menu != null)
            {
                buttonCooldown = Time.time + 0.2f;

                if (incremental)
                    ToggleIncremental(relatedText, positive);
                else
                    Toggle(relatedText, true);
            }
        }
    }
}