using System;
using UnityEngine;

namespace iiMenu.Extensions
{
    public static class TextMeshExtensions
    {
        private static Shader _textShader;

        public static void SafeSetText(this TextMesh textMesh, string text)
        {
            if (textMesh == null)
                return;

            if (textMesh.text != text)
                textMesh.text = text;
        }

        public static void SafeSetFont(this TextMesh textMesh, Font font)
        {
            if (textMesh == null || font == null)
                return;

            if (textMesh.font != font)
            {
                textMesh.font = font;

                MeshRenderer renderer = textMesh.GetComponent<MeshRenderer>();
                if (renderer != null && font.material != null)
                    renderer.sharedMaterial = font.material;
            }
        }

        public static void SafeSetFontSize(this TextMesh textMesh, int size)
        {
            if (textMesh == null)
                return;

            if (textMesh.fontSize != size)
                textMesh.fontSize = size;
        }

        public static void SafeSetFontStyle(this TextMesh textMesh, FontStyle style)
        {
            if (textMesh == null)
                return;

            if (textMesh.fontStyle != style)
                textMesh.fontStyle = style;
        }

        public static void SafeSetCharacterSize(this TextMesh textMesh, float size)
        {
            if (textMesh == null)
                return;

            if (!Mathf.Approximately(textMesh.characterSize, size))
                textMesh.characterSize = size;
        }

        public static Shader TextShader
        {
            get
            {
                if (_textShader == null)
                {
                    _textShader = Shader.Find("GUI/Text Shader");

                    if (_textShader == null)
                        _textShader = Shader.Find("UI/Default");

                    if (_textShader == null)
                        Debug.LogError("[iiMenu] Failed to find a valid TextMesh shader.");
                }

                return _textShader;
            }
        }

        public static void Chams(this TextMesh textMesh)
        {
            if (textMesh == null)
                return;

            MeshRenderer renderer = textMesh.GetComponent<MeshRenderer>();
            if (renderer == null)
                return;

            Shader shader = TextShader;
            if (shader == null)
                return;

            Material sourceMat = renderer.sharedMaterial;
            if (sourceMat == null)
                return;

            if (renderer.sharedMaterial != null && renderer.sharedMaterial.shader == shader)
                return;

            Material newMat = new Material(sourceMat);
            newMat.shader = shader;

            renderer.sharedMaterial = newMat;
        }
    }
}