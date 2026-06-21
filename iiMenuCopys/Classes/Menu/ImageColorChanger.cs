using System;
using UnityEngine;
using UnityEngine.UI;

namespace iiMenu.Classes
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ImageColorChanger : MonoBehaviour
    {
        public ImageColorChanger(IntPtr e) : base(e) { }

        public virtual void Start()
        {
            if (colors == null)
            {
                Destroy(this);
                return;
            }

            targetImage = gameObject.GetComponent<Image>();

            if (colors.IsFlat())
            {
                Update();
                Destroy(this);
                return;
            }

            Update();
        }

        public virtual void Update() =>
            targetImage.color = colors.GetCurrentColor();

        public Image targetImage;
        public ExtGradient colors;
    }
}