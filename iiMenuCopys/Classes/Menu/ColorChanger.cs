using System;
using UnityEngine;
using UnityEngine.UI;

namespace iiMenu.Classes
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ColorChanger : MonoBehaviour
    {
        public ColorChanger(IntPtr e) : base(e) { }
        public virtual void Start()
        {
            if (colors == null)
            {
                Destroy(this);
                return;
            }

            targetRenderer = GetComponent<Renderer>();

            if (colors.IsFlat())
            {
                Update();
                Destroy(this);
                return;
            }

            Update();
        }

        public virtual void Update()
        {
            targetRenderer.enabled = overrideTransparency ?? !colors.transparent;

            if (colors.transparent)
                return;

            targetRenderer.material.color = colors.GetCurrentColor();
        }

        public Renderer targetRenderer;
        public ExtGradient colors;
        public bool? overrideTransparency;
    }

    [MelonLoader.RegisterTypeInIl2Cpp]
    public class TextColorChanger : MonoBehaviour
    {
        public TextColorChanger(IntPtr e) : base(e) { }
        public virtual void Start()
        {
            if (colors == null)
            {
                Destroy(this);
                return;
            }

            targetText = gameObject.GetComponent<Text>();

            if (colors.IsFlat())
            {
                Update();
                Destroy(this);
                return;
            }

            Update();
        }

        public virtual void Update() =>
            targetText.color = colors.GetCurrentColor();

        public Text targetText;
        public ExtGradient colors;
    }
}