using iiMenu.Menu;
using System;
using System.Linq;
using UnityEngine;

namespace iiMenu.Classes.Menu
{
    public class ExtGradient
    {
        public static GradientColorKey[] GetSolidGradient(Color color) =>
            new[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) };

        public static GradientColorKey[] GetSimpleGradient(Color a, Color b) =>
            new[] { new GradientColorKey(a, 0f), new GradientColorKey(b, 0.5f), new GradientColorKey(a, 1f) };

        public GradientColorKey[] colors = GetSolidGradient(Color.magenta);

        public Color GetColor(int index)
        {
            if (rainbow)
                return Color.HSVToRGB((Time.time + index / 8) % 1f, 1f, 1f);

            if (pastelRainbow)
                return Color.HSVToRGB(Time.time + index / 8, 0.3f, 1f);

            if (copyRigColor)
                return GorillaTagger.Instance.offlineVRRig.mainSkin.material.color;

            if (transparent)
            {
                Color targetColor = colors[index].color;
                targetColor.a = 0f;

                return targetColor;
            }

            if (customColor != null)
                return customColor?.Invoke() ?? Color.magenta;

            return colors[index].color;
        }

        public void SetColor(int index, Color color, bool setMirror = true)
        {
            rainbow = false;
            pastelRainbow = false;

            copyRigColor = false;

            customColor = null;

            if (colors.Length <= 2)
                colors = GetSimpleGradient(colors[0].color, colors[1].color);

            if (setMirror && index == 0)
            {
                colors[0].color = color;
            }
            else
                colors[index].color = color;
        }

        public void SetColors(Color color)
        {
            rainbow = false;
            pastelRainbow = false;

            copyRigColor = false;

            customColor = null;

            for (int i = 0; i < colors.Length; i++)
                colors[i].color = color;
        }

        public Color GetColorTime(float time)
        {
            if (rainbow)
                return Color.HSVToRGB(time, 1f, 1f);

            if (pastelRainbow)
                return Color.HSVToRGB(time, 0.3f, 1f);

            if (copyRigColor)
                return GorillaTagger.Instance.offlineVRRig.mainSkin.material.color;

            if (transparent)
            {
                Color targetColor = new Gradient { colorKeys = colors }.Evaluate(time);
                targetColor.a = 0f;

                return targetColor;
            }

            if (customColor != null)
                return customColor?.Invoke() ?? Color.magenta;

            return new Gradient { colorKeys = colors }.Evaluate(time);
        }

        public Color GetCurrentColor(float offset = 0f) =>
            GetColorTime((offset + Time.time / (2f)) % 1f);

        public bool IsFlat() =>
            !rainbow && !pastelRainbow && !copyRigColor &&
            colors.Length > 0 && colors.All(key => key.color == colors[0].color);

        public ExtGradient Clone()
        {
            return new ExtGradient
            {
                rainbow = rainbow,
                pastelRainbow = pastelRainbow,
                copyRigColor = copyRigColor,
                customColor = customColor,
                colors = colors.Select(c => new GradientColorKey(c.color, c.time)).ToArray()
            };
        }

        public bool rainbow;
        public bool pastelRainbow;

        public bool copyRigColor;

        public bool transparent;
        public bool epileptic;

        public Func<Color> customColor;
    }
}