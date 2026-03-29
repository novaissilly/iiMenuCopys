using System;
using TMPro;
using UnityEngine;

namespace iiMenu.Extensions
{
    public static class TextMeshProExtensions
    {
        private static Shader _tmpShader;
        private static Material _fallbackMaterial;

        public static void SafeSetText(this TMP_Text tmp, string text)
        {
            if (tmp == null)
                return;

            if (tmp.text != text)
                tmp.text = text;
        }

        public static void SafeSetFont(this TMP_Text tmp, TMP_FontAsset font)
        {
            if (tmp == null || font == null)
                return;

            if (tmp.font != font)
                tmp.font = font;
        }

        public static void SafeSetFontSize(this TMP_Text tmp, float size)
        {
            if (tmp == null)
                return;

            if (Math.Abs(tmp.fontSize - size) > 0.01f)
                tmp.fontSize = size;
        }

        public static void SafeSetFontStyle(this TMP_Text tmp, FontStyles style)
        {
            if (tmp == null)
                return;

            if (tmp.fontStyle != style)
                tmp.fontStyle = style;
        }

        public static void SafeSetCharacterSpacing(this TMP_Text tmp, float targetSpacing)
        {
            if (tmp == null)
                return;

            if (!Mathf.Approximately(tmp.characterSpacing, targetSpacing))
                tmp.characterSpacing = targetSpacing;
        }

        public static Shader TmpShader
        {
            get
            {
                if (_tmpShader == null)
                {
                    _tmpShader = Shader.Find("TextMeshPro/Distance Field");

                    if (_tmpShader == null)
                        _tmpShader = Shader.Find("TextMeshPro/Mobile/Distance Field");

                    if (_tmpShader == null)
                        _tmpShader = Shader.Find("GUI/Text Shader");

                    if (_tmpShader == null)
                        Debug.LogError("[iiMenu] Failed to find a valid TMP shader.");
                }

                return _tmpShader;
            }
        }

        public static void Chams(this TMP_Text tmp)
        {
            if (tmp == null || tmp.font == null)
                return;

            Shader shader = TmpShader;
            if (shader == null)
                return;

            Material sourceMat = tmp.fontSharedMaterial != null
                ? tmp.fontSharedMaterial
                : tmp.font.material;

            if (sourceMat == null)
                return;

            if (tmp.fontMaterial != null && tmp.fontMaterial.shader == shader)
                return;

            Material newMat = new Material(sourceMat);
            newMat.shader = shader;

            tmp.fontSharedMaterial = newMat;
            tmp.fontMaterial = newMat;
            tmp.UpdateMeshPadding();
        }
    }
}