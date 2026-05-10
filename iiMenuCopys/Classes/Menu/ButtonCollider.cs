using System;
using UnityEngine;

namespace ShibaGTGenesis.Classes
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ButtonCollider : MonoBehaviour
    {
        public ButtonCollider(IntPtr e) : base(e) { }
        public string relatedText;
        public float delay;
        public virtual void OnTriggerEnter(Collider other)
        {
            if (Time.time > delay && Menu.Menu.Instance.menu != null && other == Menu.Menu.Instance.referencecollider)
            {
                Menu.Menu.Instance.Toggle(relatedText);
            }
        }
    }
}