using System;
using UnityEngine;
using static iiMenu.Menu.Main;

namespace iiMenu.Classes
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ColorChanger : TimedBehaviour
    {
        public ColorChanger(IntPtr ptr) : base(ptr) { }
        public override void Start()
        {
            base.Start();
            this.gameObjectRenderer = base.GetComponent<Renderer>();
            this.Update();
        }

        public override void Update()
        {
            base.Update();
            if (this.colors != null)
            {
                if (!this.isMonkeColors)
                {
                    if (this.timeBased)
                    {
                        //this.color = this.colors.Evaluate(this.progress);
                        this.color = this.colors.Evaluate((Time.time / 2f) % 1);
                    }
                    if (this.isRainbow)
                    {
                        float h = (Time.frameCount / 180f) % 1f;
                        this.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                    }
                    this.gameObjectRenderer.material.color = this.color;
                }
                else
                {
                    this.gameObjectRenderer.material = GorillaTagger.Instance.myVRRig.mainSkin.material;
                }
            }
        }

        public Renderer gameObjectRenderer;
        public Gradient colors = null;
        public Color32 color;
        public bool timeBased = true;
        public bool isRainbow = false;
        public bool isMonkeColors = false;
    }
}