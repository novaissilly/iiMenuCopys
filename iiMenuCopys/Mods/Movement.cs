using easyInputs;
using Photon.Pun;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using static iiMenu.Classes.RigManager;
using static iiMenu.Menu.Main;
using static UnityEngine.Object;


/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods
{
    internal class Movement
    {
        public static void DisableJoystick()
        {

        }

        public static void EnableJoystick()
        {

        }

        public static void ChangePlatformType()
        {
            platformMode++;
            if (platformMode > 10)
            {
                platformMode = 0;
            }

            string[] platformNames = new string[] {
                "Normal",
                "Invisible",
                "Rainbow",
                "Random Color",
                "Noclip",
                "Glass",
                "Snowball",
                "Water Balloon",
                "Rock",
                "Present",
                "Mentos"
            };

            GetIndex("Change Platform Type").overlapText = "Change Platform Type <color=grey>[</color><color=green>" + platformNames[platformMode] + "</color><color=grey>]</color>";
        }

        public static void ChangePlatformShape()
        {
            platformShape++;
            if (platformShape > 3)
            {
                platformShape = 0;
            }

            string[] platformShapes = new string[] {
                "Sphere",
                "Cube",
                "Cylinder",
                "Legacy"
            };

            GetIndex("Change Platform Shape").overlapText = "Change Platform Shape <color=grey>[</color><color=green>" + platformShapes[platformShape] + "</color><color=grey>]</color>";
        }

        public static void Platforms()
        {
            if (leftGrab)
            {
                if (leftplat == null)
                {
                    if (platformShape == 0)
                    {
                        leftplat = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        leftplat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                    }
                    if (platformShape == 1)
                    {
                        leftplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        leftplat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                    }
                    if (platformShape == 2)
                    {
                        leftplat = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        leftplat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                    }
                    if (platformShape == 3)
                    {
                        leftplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        leftplat.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    }
                    leftplat.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    leftplat.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                    if (platformMode != 5)
                    {
                        if (themeType == 6)
                        {
                            float h = (Time.frameCount / 180f) % 1f;
                            leftplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                        }
                    }
                    if (GetIndex("Stick Long Arms").enabled)
                    {
                        leftplat.transform.position = GorillaTagger.Instance.leftHandTransform.position + GorillaTagger.Instance.leftHandTransform.forward * 0.333f;
                    }
                    if (platformMode == 1)
                    {
                        leftplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 2)
                    {
                        float h = (Time.frameCount / 180f) % 1f;
                        leftplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                    }
                    if (platformMode == 3)
                    {
                        leftplat.GetComponent<Renderer>().material.color = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 128);
                    }
                    if (platformMode == 4)
                    {
                        foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                        {
                            v.enabled = false;
                        }
                    }
                    if (platformMode == 5)
                    {
                        leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 29;
                        if (glass == null)
                        {
                            glass = new Material(Shader.Find("GUI/Text Shader"));
                            glass.SetFloat("_Mode", 3f);
                            glass.color = new Color32(145, 187, 255, 100);
                        }
                        leftplat.GetComponent<Renderer>().material = glass;
                    }
                    if (platformMode == 6)
                    {
                        leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 32;
                        leftplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 7)
                    {
                        leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 204;
                        leftplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 8)
                    {
                        leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 231;
                        leftplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 9)
                    {
                        leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 240;
                        leftplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 10)
                    {
                        leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 249;
                        leftplat.GetComponent<Renderer>().enabled = false;
                    }
                }
                else
                {
                    if (platformMode != 5)
                    {
                        if (themeType == 6)
                        {
                            float h = (Time.frameCount / 180f) % 1f;
                            leftplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                        }
                    }
                    if (platformMode == 2)
                    {
                        float h = (Time.frameCount / 180f) % 1f;
                        leftplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                    }
                    if (platformMode == 3)
                    {
                        leftplat.GetComponent<Renderer>().material.color = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 128);
                    }
                }
            }
            else
            {
                if (leftplat != null)
                {
                    Destroy(leftplat);
                    leftplat = null;
                    if (platformMode == 4 && rightplat == null)
                    {
                        foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                        {
                            v.enabled = true;
                        }
                    }
                }
            }

            if (rightGrab)
            {
                if (rightplat == null)
                {
                    if (platformShape == 0)
                    {
                        rightplat = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        rightplat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                    }
                    if (platformShape == 1)
                    {
                        rightplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        rightplat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                    }
                    if (platformShape == 2)
                    {
                        rightplat = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        rightplat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                    }
                    if (platformShape == 3)
                    {
                        rightplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        rightplat.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    }
                    rightplat.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    rightplat.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                    if (platformMode != 5)
                    {
                        if (themeType == 6)
                        {
                            float h = (Time.frameCount / 180f) % 1f;
                            rightplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                        }
                    }
                    if (GetIndex("Stick Long Arms").enabled)
                    {
                        rightplat.transform.position = GorillaTagger.Instance.rightHandTransform.position + GorillaTagger.Instance.rightHandTransform.forward * 0.333f;
                    }
                    if (platformMode == 1)
                    {
                        rightplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 2)
                    {
                        float h = (Time.frameCount / 180f) % 1f;
                        rightplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                    }
                    if (platformMode == 3)
                    {
                        rightplat.GetComponent<Renderer>().material.color = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 128);
                    }
                    if (platformMode == 4)
                    {
                        foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                        {
                            v.enabled = false;
                        }
                    }
                    if (platformMode == 5)
                    {
                        rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 29;
                        if (glass == null)
                        {
                            glass = new Material(Shader.Find("GUI/Text Shader"));
                            glass.SetFloat("_Mode", 3f);
                            glass.color = new Color32(145, 187, 255, 100);
                        }
                        rightplat.GetComponent<Renderer>().material = glass;
                    }
                    if (platformMode == 6)
                    {
                        rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 32;
                        rightplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 7)
                    {
                        rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 204;
                        rightplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 8)
                    {
                        rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 231;
                        rightplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 9)
                    {
                        rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 240;
                        rightplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 10)
                    {
                        rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 249;
                        rightplat.GetComponent<Renderer>().enabled = false;
                    }
                }
                else
                {
                    if (platformMode != 5)
                    {
                        if (themeType == 6)
                        {
                            float h = (Time.frameCount / 180f) % 1f;
                            rightplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                        }
                    }
                    if (platformMode == 2)
                    {
                        float h = (Time.frameCount / 180f) % 1f;
                        rightplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                    }
                    if (platformMode == 3)
                    {
                        rightplat.GetComponent<Renderer>().material.color = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 128);
                    }
                }
            }
            else
            {
                if (rightplat != null)
                {
                    Destroy(rightplat);
                    rightplat = null;
                    if (platformMode == 4 && leftplat == null)
                    {
                        foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                        {
                            v.enabled = true;
                        }
                    }
                }
            }
        }

        public static void TriggerPlatforms()
        {
            if (leftTrigger > 0.5f)
            {
                if (leftplat == null)
                {
                    if (platformShape == 0)
                    {
                        leftplat = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        leftplat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                    }
                    if (platformShape == 1)
                    {
                        leftplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        leftplat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                    }
                    if (platformShape == 2)
                    {
                        leftplat = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        leftplat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                    }
                    if (platformShape == 3)
                    {
                        leftplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        leftplat.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    }
                    leftplat.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    leftplat.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                    if (platformMode != 5)
                    {
                        if (themeType == 6)
                        {
                            float h = (Time.frameCount / 180f) % 1f;
                            leftplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                        }
                    }
                    if (GetIndex("Stick Long Arms").enabled)
                    {
                        leftplat.transform.position = GorillaTagger.Instance.leftHandTransform.position + GorillaTagger.Instance.leftHandTransform.forward * 0.333f;
                    }
                    if (platformMode == 1)
                    {
                        leftplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 2)
                    {
                        float h = (Time.frameCount / 180f) % 1f;
                        leftplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                    }
                    if (platformMode == 3)
                    {
                        leftplat.GetComponent<Renderer>().material.color = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 128);
                    }
                    if (platformMode == 4)
                    {
                        foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                        {
                            v.enabled = false;
                        }
                    }
                    if (platformMode == 5)
                    {
                        leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 29;
                        if (glass == null)
                        {
                            glass = new Material(Shader.Find("GUI/Text Shader"));
                            glass.SetFloat("_Mode", 3f);
                            glass.color = new Color32(145, 187, 255, 100);
                        }
                        leftplat.GetComponent<Renderer>().material = glass;
                    }
                    if (platformMode == 6)
                    {
                        leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 32;
                        leftplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 7)
                    {
                        leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 204;
                        leftplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 8)
                    {
                        leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 231;
                        leftplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 9)
                    {
                        leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 240;
                        leftplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 10)
                    {
                        leftplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 249;
                        leftplat.GetComponent<Renderer>().enabled = false;
                    }
                }
                else
                {
                    if (platformMode != 5)
                    {
                        if (themeType == 6)
                        {
                            float h = (Time.frameCount / 180f) % 1f;
                            leftplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                        }
                    }
                    if (platformMode == 2)
                    {
                        float h = (Time.frameCount / 180f) % 1f;
                        leftplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                    }
                    if (platformMode == 3)
                    {
                        leftplat.GetComponent<Renderer>().material.color = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 128);
                    }
                }
            }
            else
            {
                if (leftplat != null)
                {
                    Destroy(leftplat);
                    leftplat = null;
                    if (platformMode == 4 && rightplat == null)
                    {
                        foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                        {
                            v.enabled = true;
                        }
                    }
                }
            }

            if (rightTrigger > 0.5f)
            {
                if (rightplat == null)
                {
                    if (platformShape == 0)
                    {
                        rightplat = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        rightplat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                    }
                    if (platformShape == 1)
                    {
                        rightplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        rightplat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                    }
                    if (platformShape == 2)
                    {
                        rightplat = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        rightplat.transform.localScale = new Vector3(0.333f, 0.333f, 0.333f);
                    }
                    if (platformShape == 3)
                    {
                        rightplat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        rightplat.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    }
                    rightplat.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    rightplat.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                    if (platformMode != 5)
                    {
                        if (themeType == 6)
                        {
                            float h = (Time.frameCount / 180f) % 1f;
                            rightplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                        }
                    }
                    if (GetIndex("Stick Long Arms").enabled)
                    {
                        rightplat.transform.position = GorillaTagger.Instance.rightHandTransform.position + GorillaTagger.Instance.rightHandTransform.forward * 0.333f;
                    }
                    if (platformMode == 1)
                    {
                        rightplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 2)
                    {
                        float h = (Time.frameCount / 180f) % 1f;
                        rightplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                    }
                    if (platformMode == 3)
                    {
                        rightplat.GetComponent<Renderer>().material.color = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 128);
                    }
                    if (platformMode == 4)
                    {
                        foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                        {
                            v.enabled = false;
                        }
                    }
                    if (platformMode == 5)
                    {
                        rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 29;
                        if (glass == null)
                        {
                            glass = new Material(Shader.Find("GUI/Text Shader"));
                            glass.SetFloat("_Mode", 3f);
                            glass.color = new Color32(145, 187, 255, 100);
                        }
                        rightplat.GetComponent<Renderer>().material = glass;
                    }
                    if (platformMode == 6)
                    {
                        rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 32;
                        rightplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 7)
                    {
                        rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 204;
                        rightplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 8)
                    {
                        rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 231;
                        rightplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 9)
                    {
                        rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 240;
                        rightplat.GetComponent<Renderer>().enabled = false;
                    }
                    if (platformMode == 10)
                    {
                        rightplat.AddComponent<GorillaSurfaceOverride>().overrideIndex = 249;
                        rightplat.GetComponent<Renderer>().enabled = false;
                    }
                }
                else
                {
                    if (platformMode != 5)
                    {
                        if (themeType == 6)
                        {
                            float h = (Time.frameCount / 180f) % 1f;
                            rightplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                        }
                    }
                    if (platformMode == 2)
                    {
                        float h = (Time.frameCount / 180f) % 1f;
                        rightplat.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(h, 1f, 1f);
                    }
                    if (platformMode == 3)
                    {
                        rightplat.GetComponent<Renderer>().material.color = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 128);
                    }
                }
            }
            else
            {
                if (rightplat != null)
                {
                    Destroy(rightplat);
                    rightplat = null;
                    if (platformMode == 4 && leftplat == null)
                    {
                        foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                        {
                            v.enabled = true;
                        }
                    }
                }
            }
        }

        public static void Frozone()
        {
            if (leftGrab)
            {
                GameObject lol = GameObject.CreatePrimitive(PrimitiveType.Cube);
                lol.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                lol.transform.localPosition = GorillaTagger.Instance.leftHandTransform.position + new Vector3(0f, -0.05f, 0f);
                lol.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;

                lol.AddComponent<GorillaSurfaceOverride>().overrideIndex = 61;
                UnityEngine.Object.Destroy(lol, 1);
            }

            if (rightGrab)
            {
                GameObject lol = GameObject.CreatePrimitive(PrimitiveType.Cube);
                lol.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                lol.transform.localPosition = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, -0.05f, 0f);
                lol.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;

                lol.AddComponent<GorillaSurfaceOverride>().overrideIndex = 61;
                UnityEngine.Object.Destroy(lol, 1);
            }
        }

        public static void ChangeSpeedBoostAmount()
        {
            speedboostCycle++;
            if (speedboostCycle > 3)
            {
                speedboostCycle = 0;
            }

            float[] jspeedamounts = new float[] { 2f, 7.5f, 9f, 200f };
            jspeed = jspeedamounts[speedboostCycle];

            float[] jmultiamounts = new float[] { 0.5f, 1.5f, 2f, 10f };
            jmulti = jmultiamounts[speedboostCycle];

            string[] speedNames = new string[] { "Slow", "Normal", "Fast", "Ultra Fast" };
            GetIndex("Change Speed Boost Amount").overlapText = "Change Speed Boost Amount <color=grey>[</color><color=green>" + speedNames[speedboostCycle] + "</color><color=grey>]</color>";
        }

        public static void PlatformGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                if (GetGunInput(true))
                {
                    GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    UnityEngine.Object.Destroy(platform.GetComponent<BoxCollider>());
                    platform.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                    platform.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
                    platform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    platform.transform.position = GunPointer.transform.position;
                    platform.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
                    UnityEngine.Object.Destroy(platform, 1f);
                }
            }
        }

        public static void ChangeFlySpeed()
        {
            flySpeedCycle++;
            if (flySpeedCycle > 3)
            {
                flySpeedCycle = 0;
            }

            float[] speedamounts = new float[] { 5f, 10f, 30f, 60f };
            flySpeed = speedamounts[flySpeedCycle];

            string[] speedNames = new string[] { "Slow", "Normal", "Fast", "Extra Fast" };
            GetIndex("Change Fly Speed").overlapText = "Change Fly Speed <color=grey>[</color><color=green>" + speedNames[flySpeedCycle] + "</color><color=grey>]</color>";
        }

        public static void Fly()
        {
            if (rightPrimary)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * flySpeed;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        public static void TriggerFly()
        {
            if (rightTrigger > 0.5f)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * flySpeed;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        public static void JoystickFly()
        {
            Vector2 joy = EasyInputs.GetThumbStick2DAxis(EasyHand.RightHand);

            if (Mathf.Abs(joy.x) > 0.3 || Mathf.Abs(joy.y) > 0.3)
            {
                GorillaLocomotion.Player.Instance.transform.position += (GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * (joy.y * flySpeed)) + (GorillaTagger.Instance.headCollider.transform.right * Time.deltaTime * (joy.x * flySpeed));
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        public static void BarkFly()
        {
            GetIndex("Zero Gravity").enabled = true;

            var rb = GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody;
            Vector2 xz = EasyInputs.GetThumbStick2DAxis(EasyHand.LeftHand);
            float y = EasyInputs.GetThumbStick2DAxis(EasyHand.RightHand).y;

            Vector3 inputDirection = new Vector3(xz.x, y, xz.y);
            var playerForward = GorillaLocomotion.Player.Instance.bodyCollider.transform.forward;
            playerForward.y = 0;
            var playerRight = GorillaLocomotion.Player.Instance.bodyCollider.transform.right;
            playerRight.y = 0;

            var velocity = inputDirection.x * playerRight + y * Vector3.up + inputDirection.z * playerForward;
            velocity *= GorillaLocomotion.Player.Instance.transform.localScale.magnitude * flySpeed;
            rb.velocity = Vector3.Lerp(rb.velocity, velocity, 0.12875f);
        }

        public static void DisableBarkFly()
        {
            GetIndex("Zero Gravity").enabled = false;
        }

        public static void SlingshotFly()
        {
            if (rightPrimary)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * (flySpeed * 2);
            }
        }

        public static void ZeroGravitySlingshotFly()
        {
            if (rightPrimary)
            {
                GetIndex("Zero Gravity").enabled = true;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * flySpeed;
            }
            else
            {
                GetIndex("Zero Gravity").enabled = false;
            }
        }

        public static void WASDFly()
        {
            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0.067f, 0f);

        }

        public static void Drive()
        {
            Vector2 joy = EasyInputs.GetThumbStick2DAxis(EasyHand.RightHand);
            lerpygerpy = Vector2.Lerp(lerpygerpy, joy, 0.05f);

            Vector3 addition = GorillaTagger.Instance.bodyCollider.transform.forward * lerpygerpy.y + GorillaTagger.Instance.bodyCollider.transform.right * lerpygerpy.x;// + new Vector3(0f, -1f, 0f);
            Physics.Raycast(GorillaTagger.Instance.bodyCollider.transform.position - new Vector3(0f, 0.2f, 0f), Vector3.down, out var Ray, 512);

            if (Ray.distance < 0.2f && (Mathf.Abs(lerpygerpy.x) > 0.05f || Mathf.Abs(lerpygerpy.y) > 0.05f))
            {
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity = addition * 10f;
            }
        }

        public static void StickLongArms()
        {
            GorillaLocomotion.Player.Instance.leftHandTransform.transform.position = GorillaTagger.Instance.leftHandTransform.position + (GorillaTagger.Instance.leftHandTransform.forward * 0.333f);
            GorillaLocomotion.Player.Instance.rightHandTransform.transform.position = GorillaTagger.Instance.rightHandTransform.position + (GorillaTagger.Instance.rightHandTransform.forward * 0.333f);
        }

        public static void IronMan()
        {
            if (leftPrimary)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(flySpeed * GorillaTagger.Instance.myVRRig.transform.Find("rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L").right, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 50f * GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
            }
            if (rightPrimary)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(flySpeed * -GorillaTagger.Instance.myVRRig.transform.Find("rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R").right, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 50f * GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);
            }
        }

        public static void SpiderMan()
        {
            if (leftGrab)
            {
                if (!isLeftGrappling)
                {
                    isLeftGrappling = true;
                    RaycastHit lefthit;
                    if (Physics.Raycast(GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.forward, out lefthit, 100f))
                    {
                        leftgrapplePoint = lefthit.point;


                        float leftdistanceFromPoint = Vector3.Distance(GorillaTagger.Instance.bodyCollider.attachedRigidbody.position, leftgrapplePoint);

                        leftjoint.maxDistance = leftdistanceFromPoint * 0.8f;
                        leftjoint.minDistance = leftdistanceFromPoint * 0.25f;

                        leftjoint.spring = 10f;
                        leftjoint.damper = 50f;
                    }
                }

                GameObject line = new GameObject("Line");
                LineRenderer liner = line.AddComponent<LineRenderer>();
                UnityEngine.Color thecolor = backgroundColor.GetCurrentColor();
                liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                liner.SetPosition(0, GorillaTagger.Instance.leftHandTransform.position);
                liner.SetPosition(1, leftgrapplePoint);
                liner.material.shader = Shader.Find("Standard");
                UnityEngine.Object.Destroy(line, Time.deltaTime);
            }
            else
            {
                isLeftGrappling = false;
            }

            if (rightGrab)
            {
                if (!isRightGrappling)
                {
                    isRightGrappling = true;
                    RaycastHit righthit;
                    if (Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward, out righthit, 100f))
                    {
                        rightgrapplePoint = righthit.point;


                        float rightdistanceFromPoint = Vector3.Distance(GorillaTagger.Instance.bodyCollider.attachedRigidbody.position, rightgrapplePoint);

                        rightjoint.maxDistance = rightdistanceFromPoint * 0.8f;
                        rightjoint.minDistance = rightdistanceFromPoint * 0.25f;

                        rightjoint.spring = 10f;
                        rightjoint.damper = 50f;
                    }
                }

                GameObject line = new GameObject("Line");
                LineRenderer liner = line.AddComponent<LineRenderer>();
                UnityEngine.Color thecolor = backgroundColor.GetCurrentColor();
                liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                liner.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                liner.SetPosition(1, rightgrapplePoint);
                liner.material.shader = Shader.Find("Standard");
                UnityEngine.Object.Destroy(line, Time.deltaTime);
            }
            else
            {
                isRightGrappling = false;
            }
        }

        public static void DisableSpiderMan()
        {
            isLeftGrappling = false;
            isRightGrappling = false;
        }

        public static void UpAndDown()
        {
            if (rightTrigger > 0.5f)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += Vector3.up * Time.deltaTime * 45f;
            }

            if (rightGrab)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += Vector3.up * Time.deltaTime * -45f;
            }
        }

        public static void AutoFunnyRun()
        {
            if (rightGrab)
            {
                if (bothHands)
                {
                    float time = Time.frameCount;
                    GorillaTagger.Instance.rightHandTransform.position = GorillaTagger.Instance.headCollider.transform.position + (GorillaTagger.Instance.headCollider.transform.forward * Mathf.Cos(time) / 10) + new Vector3(0, -0.5f - (Mathf.Sin(time) / 7), 0) + (GorillaTagger.Instance.headCollider.transform.right * -0.05f);
                    GorillaTagger.Instance.leftHandTransform.position = GorillaTagger.Instance.headCollider.transform.position + (GorillaTagger.Instance.headCollider.transform.forward * Mathf.Cos(time + 180) / 10) + new Vector3(0, -0.5f - (Mathf.Sin(time + 180) / 7), 0) + (GorillaTagger.Instance.headCollider.transform.right * 0.05f);
                }
                else
                {
                    float time = Time.frameCount;
                    GorillaTagger.Instance.rightHandTransform.position = GorillaTagger.Instance.headCollider.transform.position + (GorillaTagger.Instance.headCollider.transform.forward * Mathf.Cos(time) / 10) + new Vector3(0, -0.5f - (Mathf.Sin(time) / 7), 0);
                }
            }
        }

        public static void ForceTagFreeze()
        {
            GorillaLocomotion.Player.Instance.disableMovement = true;
        }

        public static void NoTagFreeze()
        {
            GorillaLocomotion.Player.Instance.disableMovement = false;
        }

        public static void LowGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (6.66f / Time.deltaTime)), ForceMode.Acceleration);
        }

        public static void ZeroGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (9.81f / Time.deltaTime)), ForceMode.Acceleration);
        }

        public static void HighGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.down * (Time.deltaTime * (6.66f / Time.deltaTime)), ForceMode.Acceleration);
        }

        public static void WallWalk()
        {
            if ((GorillaLocomotion.Player.Instance.wasLeftHandTouching || GorillaLocomotion.Player.Instance.wasRightHandTouching) && rightGrab)
            {
                FieldInfo fieldInfo = typeof(GorillaLocomotion.Player).GetField("lastHitInfoHand", BindingFlags.NonPublic | BindingFlags.Instance);
                RaycastHit ray = (RaycastHit)fieldInfo.GetValue(GorillaLocomotion.Player.Instance);
                walkPos = ray.point;
                walkNormal = ray.normal;
            }

            if (!rightGrab)
            {
                walkPos = Vector3.zero;
                GetIndex("Zero Gravity").enabled = false;
            }

            if (walkPos != Vector3.zero)
            {
                //GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(walkNormal * -10, ForceMode.Acceleration);
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(walkNormal * -9.81f, ForceMode.Acceleration);
                GetIndex("Zero Gravity").enabled = true;
            }
        }

        public static void WeakWallWalk()
        {
            if ((GorillaLocomotion.Player.Instance.wasLeftHandTouching || GorillaLocomotion.Player.Instance.wasRightHandTouching) && rightGrab)
            {
                FieldInfo fieldInfo = typeof(GorillaLocomotion.Player).GetField("lastHitInfoHand", BindingFlags.NonPublic | BindingFlags.Instance);
                RaycastHit ray = (RaycastHit)fieldInfo.GetValue(GorillaLocomotion.Player.Instance);
                walkPos = ray.point;
                walkNormal = ray.normal;
            }

            if (!rightGrab)
            {
                walkPos = Vector3.zero;
                GetIndex("Zero Gravity").enabled = false;
            }

            if (walkPos != Vector3.zero)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(walkNormal * -5, ForceMode.Acceleration);
                GetIndex("Zero Gravity").enabled = true;
            }
        }

        public static void StrongWallWalk()
        {
            if ((GorillaLocomotion.Player.Instance.wasLeftHandTouching || GorillaLocomotion.Player.Instance.wasRightHandTouching) && rightGrab)
            {
                FieldInfo fieldInfo = typeof(GorillaLocomotion.Player).GetField("lastHitInfoHand", BindingFlags.NonPublic | BindingFlags.Instance);
                RaycastHit ray = (RaycastHit)fieldInfo.GetValue(GorillaLocomotion.Player.Instance);
                walkPos = ray.point;
                walkNormal = ray.normal;
            }

            if (!rightGrab)
            {
                walkPos = Vector3.zero;
                GetIndex("Zero Gravity").enabled = false;
            }

            if (walkPos != Vector3.zero)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(walkNormal * -50, ForceMode.Acceleration);
                GetIndex("Zero Gravity").enabled = true;
            }
        }

        public static void TeleportToRandom()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig != GorillaTagger.Instance.myVRRig)
                    {
                        GorillaLocomotion.Player.Instance.transform.position = rig.headMesh.transform.position;
                    }
                }
            }
        }

        public static void TeleportGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                if (GetGunInput(true))
                {
                    if (Time.time > teleDebounce)
                    {
                        MeshCollider[] meshColliders = Resources.FindObjectsOfTypeAll<MeshCollider>();
                        foreach (MeshCollider coll in meshColliders)
                        {
                            coll.enabled = false;
                        }
                        GorillaTagger.Instance.GetComponent<Rigidbody>().transform.position = GunPointer.transform.position + new Vector3(0f, 1f, 0f);
                        teleDebounce = Time.time + 0.5f;
                    }
                }
            }
        }

        public static void Airstrike()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                if (GetGunInput(true))
                {
                    if (Time.time > teleDebounce)
                    {
                        MeshCollider[] meshColliders = Resources.FindObjectsOfTypeAll<MeshCollider>();
                        foreach (MeshCollider coll in meshColliders)
                        {
                            coll.enabled = false;
                        }
                        GorillaTagger.Instance.GetComponent<Rigidbody>().transform.position = GunPointer.transform.position + new Vector3(0f, 30f, 0f);
                        GorillaTagger.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0f, -20f, 0f);
                        teleDebounce = Time.time + 0.5f;
                    }
                }
            }
        }

        public static void Checkpoint()
        {
            if (rightGrab)
            {
                if (CheckPoint == null)
                {
                    CheckPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(CheckPoint.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(CheckPoint.GetComponent<SphereCollider>());
                    CheckPoint.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                CheckPoint.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
            if (CheckPoint != null)
            {
                if (rightPrimary)
                {
                    CheckPoint.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                    GorillaTagger.Instance.GetComponent<Rigidbody>().transform.position = CheckPoint.transform.position;
                    GorillaTagger.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                else
                {
                    CheckPoint.GetComponent<Renderer>().material.color = buttonColors[0].GetCurrentColor();
                }
            }
        }

        public static void DisableCheckpoint()
        {
            if (CheckPoint != null)
            {
                UnityEngine.Object.Destroy(CheckPoint);
                CheckPoint = null;
            }
        }

        public static void Bomb()
        {
            if (rightGrab)
            {
                if (BombObject == null)
                {
                    BombObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(BombObject.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(BombObject.GetComponent<SphereCollider>());
                    BombObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                BombObject.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
            if (BombObject != null)
            {
                if (rightPrimary)
                {
                    Vector3 dir = (GorillaTagger.Instance.bodyCollider.transform.position - BombObject.transform.position);
                    dir.Normalize();
                    MeshCollider[] meshColliders = Resources.FindObjectsOfTypeAll<MeshCollider>();
                    foreach (MeshCollider coll in meshColliders)
                    {
                        coll.enabled = false;
                    }
                    GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += 25f * dir;
                    UnityEngine.Object.Destroy(BombObject);
                    BombObject = null;
                }
                else
                {
                    BombObject.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                }
            }
        }

        public static void DisableBomb()
        {
            if (BombObject != null)
            {
                UnityEngine.Object.Destroy(BombObject);
                BombObject = null;
            }
        }

        public static void SpeedBoost()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = jspeed;
            GorillaLocomotion.Player.Instance.jumpMultiplier = jmulti;
        }

        public static void DisableSpeedBoost()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 6.5f;
            GorillaLocomotion.Player.Instance.jumpMultiplier = 1.1f;
        }

        public static void Noclip()
        {
            if (rightTrigger > 0.5f )
            {
                if (noclip == false)
                {
                    noclip = true;
                    foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        v.enabled = false;
                    }
                }
            }
            else
            {
                if (noclip == true)
                {
                    noclip = false;
                    foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        v.enabled = true;
                    }
                }
            }
        }

        public static void Invisible()
        {
            bool hit = rightSecondary || Mouse.current.rightButton.isPressed;
            if (invisMonke)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.myVRRig.transform.position = new Vector3(34234, 32423, 32423);

                GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                UnityEngine.Object.Destroy(l, Time.deltaTime);
                UnityEngine.Object.Destroy(r, Time.deltaTime);
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
            if (hit == true && lastHit2 == false)
            {
                invisMonke = !invisMonke;
            }
            lastHit2 = hit;
        }

        public static void DisableInvisible()
        {
            GorillaTagger.Instance.myVRRig.enabled = true;
        }

        public static void Ghost()
        {
            bool hit = rightPrimary || Mouse.current.leftButton.isPressed;
            GorillaTagger.Instance.myVRRig.enabled = !ghostMonke;
            if (ghostMonke)
            {
                GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                UnityEngine.Object.Destroy(l, Time.deltaTime);
                UnityEngine.Object.Destroy(r, Time.deltaTime);
            }
            if (hit == true && lastHit == false)
            {
                ghostMonke = !ghostMonke;
            }
            lastHit = hit;
        }

        public static void EnableRig()
        {
            GorillaTagger.Instance.myVRRig.enabled = true;
        }

        public static void RigGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                if (GetGunInput(true))
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;
                    GorillaTagger.Instance.myVRRig.transform.position = GunPointer.transform.position + new Vector3(0, 1, 0);
                    GorillaTagger.Instance.myVRRig.transform.position = GunPointer.transform.position + new Vector3(0, 1, 0);

                    GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                    l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                    GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                    r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                    l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                    r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                    UnityEngine.Object.Destroy(l, Time.deltaTime);
                    UnityEngine.Object.Destroy(r, Time.deltaTime);
                }
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
        }

        public static void GrabRig()
        {
            if (rightGrab)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                UnityEngine.Object.Destroy(l, Time.deltaTime);
                UnityEngine.Object.Destroy(r, Time.deltaTime);
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
        }

        public static void EnableSpazRig()
        {
            offsetLH = GorillaTagger.Instance.myVRRig.leftHand.trackingPositionOffset;
            offsetRH = GorillaTagger.Instance.myVRRig.rightHand.trackingPositionOffset;
            offsetH = GorillaTagger.Instance.myVRRig.head.trackingPositionOffset;
        }

        public static void SpazRig()
        {
            if (rightPrimary)
            {
                float spazAmount = 0.1f;
                GorillaTagger.Instance.myVRRig.leftHand.trackingPositionOffset = offsetLH + new Vector3(UnityEngine.Random.Range(-spazAmount, spazAmount), UnityEngine.Random.Range(-spazAmount, spazAmount), UnityEngine.Random.Range(-spazAmount, spazAmount));
                GorillaTagger.Instance.myVRRig.rightHand.trackingPositionOffset = offsetRH + new Vector3(UnityEngine.Random.Range(-spazAmount, spazAmount), UnityEngine.Random.Range(-spazAmount, spazAmount), UnityEngine.Random.Range(-spazAmount, spazAmount));
                GorillaTagger.Instance.myVRRig.head.trackingPositionOffset = offsetH + new Vector3(UnityEngine.Random.Range(-spazAmount, spazAmount), UnityEngine.Random.Range(-spazAmount, spazAmount), UnityEngine.Random.Range(-spazAmount, spazAmount));

                GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                UnityEngine.Object.Destroy(l, Time.deltaTime);
                UnityEngine.Object.Destroy(r, Time.deltaTime);
            }
            else
            {
                GorillaTagger.Instance.myVRRig.leftHand.trackingPositionOffset = offsetLH;
                GorillaTagger.Instance.myVRRig.rightHand.trackingPositionOffset = offsetRH;
                GorillaTagger.Instance.myVRRig.head.trackingPositionOffset = offsetH;
            }
        }

        public static void DisableSpazRig()
        {
            GorillaTagger.Instance.myVRRig.leftHand.trackingPositionOffset = offsetLH;
            GorillaTagger.Instance.myVRRig.rightHand.trackingPositionOffset = offsetRH;
            GorillaTagger.Instance.myVRRig.head.trackingPositionOffset = offsetH;
        }

        public static void SpazHands()
        {
            if (rightPrimary)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;

                GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f);
                GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f);

                GorillaTagger.Instance.myVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
                GorillaTagger.Instance.myVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;

                GorillaTagger.Instance.myVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;

                float spazAmount = 360f;
                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.rotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0, spazAmount), UnityEngine.Random.Range(0, spazAmount), UnityEngine.Random.Range(0, spazAmount)));
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0, spazAmount), UnityEngine.Random.Range(0, spazAmount), UnityEngine.Random.Range(0, spazAmount)));

                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.leftHandTransform.position + GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.forward * 3f;
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.rightHandTransform.position + GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.forward * 3f;

                GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                UnityEngine.Object.Destroy(l, Time.deltaTime);
                UnityEngine.Object.Destroy(r, Time.deltaTime);
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
        }

        public static void FreezeRigLimbs()
        {
            if (rightPrimary)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;

                GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f);
                GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f);

                GorillaTagger.Instance.myVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
                GorillaTagger.Instance.myVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;

                //GorillaTagger.Instance.myVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;

                GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                UnityEngine.Object.Destroy(l, Time.deltaTime);
                UnityEngine.Object.Destroy(r, Time.deltaTime);
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
        }

        public static void FreezeRigBody()
        {
            if (rightPrimary)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;

                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;

                GorillaTagger.Instance.myVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;

                GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                UnityEngine.Object.Destroy(l, Time.deltaTime);
                UnityEngine.Object.Destroy(r, Time.deltaTime);
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
        }

        public static void AutoDance()
        {
            if (rightPrimary)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;

                Vector3 bodyOffset = (GorillaTagger.Instance.bodyCollider.transform.right * (Mathf.Cos((float)Time.frameCount / 20f) * 0.3f)) + (new Vector3(0f, Mathf.Abs(Mathf.Sin((float)Time.frameCount / 20f) * 0.2f), 0f));
                GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f) + bodyOffset;
                GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f) + bodyOffset;

                GorillaTagger.Instance.myVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
                GorillaTagger.Instance.myVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;

                GorillaTagger.Instance.myVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;

                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.myVRRig.transform.position + GorillaTagger.Instance.myVRRig.transform.forward * 0.2f + GorillaTagger.Instance.myVRRig.transform.right * -0.4f + GorillaTagger.Instance.myVRRig.transform.up * (0.3f + (Mathf.Sin((float)Time.frameCount / 20f) * 0.2f));
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.myVRRig.transform.position + GorillaTagger.Instance.myVRRig.transform.forward * 0.2f + GorillaTagger.Instance.myVRRig.transform.right * 0.4f + GorillaTagger.Instance.myVRRig.transform.up * (0.3f + (Mathf.Sin((float)Time.frameCount / 20f) * -0.2f));

                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.rotation = GorillaTagger.Instance.myVRRig.transform.rotation;
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.myVRRig.transform.rotation;

                GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                UnityEngine.Object.Destroy(l, Time.deltaTime);
                UnityEngine.Object.Destroy(r, Time.deltaTime);
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
        }

        public static void Helicopter()
        {
            if (rightPrimary)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;

                GorillaTagger.Instance.myVRRig.transform.position += new Vector3(0f, 0.05f, 0f);
                GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.myVRRig.transform.position;

                GorillaTagger.Instance.myVRRig.transform.rotation = Quaternion.Euler(GorillaTagger.Instance.myVRRig.transform.rotation.eulerAngles + new Vector3(0f, 10f, 0f));
                GorillaTagger.Instance.myVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;

                GorillaTagger.Instance.myVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;

                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.myVRRig.transform.position + GorillaTagger.Instance.myVRRig.transform.right * -1f + GorillaTagger.Instance.myVRRig.transform.up * 0.3f;
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.myVRRig.transform.position + GorillaTagger.Instance.myVRRig.transform.right * 1f + GorillaTagger.Instance.myVRRig.transform.up * 0.3f;

                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.rotation = GorillaTagger.Instance.myVRRig.transform.rotation;
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.myVRRig.transform.rotation;

                GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                UnityEngine.Object.Destroy(l, Time.deltaTime);
                UnityEngine.Object.Destroy(r, Time.deltaTime);
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
        }

        public static void GhostAnimations()
        {
            GorillaTagger.Instance.myVRRig.enabled = false;

            GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f);
            GorillaTagger.Instance.myVRRig.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, 0.15f, 0f);

            GorillaTagger.Instance.myVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
            GorillaTagger.Instance.myVRRig.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;

            GorillaTagger.Instance.myVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;

            GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
            GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;

            if (rightPrimary)
            {
                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + (GorillaTagger.Instance.bodyCollider.transform.right * -0.25f) + (GorillaTagger.Instance.bodyCollider.transform.up * -1f) + (GorillaTagger.Instance.bodyCollider.transform.forward * Mathf.Sin((float)Time.frameCount / 10f));
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + (GorillaTagger.Instance.bodyCollider.transform.right * 0.25f) + (GorillaTagger.Instance.bodyCollider.transform.up * -1f) + -(GorillaTagger.Instance.bodyCollider.transform.forward * Mathf.Sin((float)Time.frameCount / 10f));
            }
            else
            {
                GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + (GorillaTagger.Instance.bodyCollider.transform.right * -0.25f) + (GorillaTagger.Instance.bodyCollider.transform.up * -1f);
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + (GorillaTagger.Instance.bodyCollider.transform.right * 0.25f) + (GorillaTagger.Instance.bodyCollider.transform.up * -1f);
            }

            if (rightSecondary)
            {
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.bodyCollider.transform.position + (GorillaTagger.Instance.bodyCollider.transform.right * 0.25f) + Vector3.Lerp(GorillaTagger.Instance.rightHandTransform.forward, -GorillaTagger.Instance.rightHandTransform.up, 0.5f) * 2f;
                GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
            }

            GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

            l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

            GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

            r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

            l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
            r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

            UnityEngine.Object.Destroy(l, Time.deltaTime);
            UnityEngine.Object.Destroy(r, Time.deltaTime);
        }

        public static void StareAtNearby()
        {
            GorillaTagger.Instance.myVRRig.headConstraint.LookAt(GetClosestVRRig().headMesh.transform.position);
        }

        public static void EnableFloatingRig()
        {
            offsetH = GorillaTagger.Instance.myVRRig.head.trackingPositionOffset;
        }

        public static void FloatingRig()
        {
            GorillaTagger.Instance.myVRRig.head.trackingPositionOffset = offsetH + new Vector3(0f, 0.65f + (Mathf.Sin((float)Time.frameCount / 40f) * 0.2f), 0f);
        }

        public static void DisableFloatingRig()
        {
            GorillaTagger.Instance.myVRRig.head.trackingPositionOffset = offsetH;
        }

        public static void Bees()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;
                    if (Time.time > beesDelay)
                    {
                        GorillaTagger.Instance.myVRRig.transform.position = rig.headMesh.transform.position + new Vector3(0f, 1f, 0f);
                        GorillaTagger.Instance.myVRRig.transform.position = rig.headMesh.transform.position + new Vector3(0f, 1f, 0f);

                        GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = rig.transform.position;
                        GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = rig.transform.position;

                        beesDelay = Time.time + 0.777f;
                    }
                }
            }
        }

        public static void SizeChanger()
        {
            if (rightPrimary)
            {
                sizeScale = 1f;
            }
            if (rightTrigger > 0.5f)
            {
                sizeScale += 0.05f;
            }
            if (rightGrab)
            {
                sizeScale -= 0.05f;
            }
            if (sizeScale <= 0)
            {
                sizeScale = 0.05f;
            }
            //GorillaLocomotion.Player.Instance.scale = sizeScale;
        }

        public static void EnableSizeChanger()
        {
            sizeScale = 1f;
            //GorillaLocomotion.Player.Instance.scale = 1f;
        }

        public static void EnableSlipperyHands()
        {
            EverythingSlippery = true;
        }

        public static void DisableSlipperyHands()
        {
            EverythingSlippery = false;
        }

        public static void EnableGrippyHands()
        {
            EverythingGrippy = true;
        }

        public static void DisableGrippyHands()
        {
            EverythingGrippy = false;
        }

        public static void EnableSlideControl()
        {
            oldSlide = GorillaLocomotion.Player.Instance.slideControl;
            GorillaLocomotion.Player.Instance.slideControl = 1f;
        }

        public static void EnableWeakSlideControl()
        {
            oldSlide = GorillaLocomotion.Player.Instance.slideControl;
            GorillaLocomotion.Player.Instance.slideControl = oldSlide * 2f;
        }

        public static void DisableSlideControl()
        {
            GorillaLocomotion.Player.Instance.slideControl = oldSlide;
        }

        public static void PunchMod()
        {
            int index = -1;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.myVRRig)
                {
                    index++;

                    Vector3 they = vrrig.rightHandTransform.position;
                    Vector3 notthem = GorillaTagger.Instance.myVRRig.head.rigTarget.position;
                    float distance = Vector3.Distance(they, notthem);

                    if (distance < 0.25)
                    {
                        GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += Vector3.Normalize(vrrig.rightHandTransform.position - lastRight[index]) * 10f;
                    }
                    lastRight[index] = vrrig.rightHandTransform.position;

                    they = vrrig.leftHandTransform.position;
                    distance = Vector3.Distance(they, notthem);

                    if (distance < 0.25)
                    {
                        GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += Vector3.Normalize(vrrig.leftHandTransform.position - lastLeft[index]) * 10f;
                    }
                    lastLeft[index] = vrrig.leftHandTransform.position;
                }
            }
        }

        public static void SolidPlayers()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.myVRRig && Vector3.Distance(vrrig.transform.position, GorillaTagger.Instance.headCollider.transform.position) < 5f)
                {
                    Vector3 pointA = vrrig.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f);
                    Vector3 pointB = vrrig.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f);
                    GameObject bodyCollider = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    UnityEngine.Object.Destroy(bodyCollider.GetComponent<Rigidbody>());
                    bodyCollider.GetComponent<Renderer>().enabled = false;
                    bodyCollider.transform.position = Vector3.Lerp(pointA, pointB, 0.5f);
                    bodyCollider.transform.rotation = vrrig.transform.rotation;
                    bodyCollider.transform.localScale = new Vector3(0.3f, 0.55f, 0.3f);
                    UnityEngine.Object.Destroy(bodyCollider, Time.deltaTime * 2);

                    for (int i = 0; i < bones.Count<int>(); i += 2)
                    {
                        pointA = vrrig.mainSkin.bones[bones[i]].position;
                        pointB = vrrig.mainSkin.bones[bones[i + 1]].position;
                        bodyCollider = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        UnityEngine.Object.Destroy(bodyCollider.GetComponent<Rigidbody>());
                        bodyCollider.GetComponent<Renderer>().enabled = false;
                        bodyCollider.transform.position = Vector3.Lerp(pointA, pointB, 0.5f);
                        bodyCollider.transform.LookAt(pointB);
                        bodyCollider.transform.localScale = new Vector3(0.2f, 0.2f, Vector3.Distance(pointA, pointB));
                        UnityEngine.Object.Destroy(bodyCollider, Time.deltaTime * 2);
                    }
                }
            }
        }

        public static void ThrowControllers()
        {
            if (leftPrimary)
            {
                if (leftThrow != null)
                {
                    GorillaLocomotion.Player.Instance.leftHandTransform.position = leftThrow.transform.position;
                    GorillaLocomotion.Player.Instance.leftHandTransform.rotation = leftThrow.transform.rotation;
                }
                else
                {
                    leftThrow = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    leftThrow.GetComponent<Renderer>().enabled = false;
                    UnityEngine.Object.Destroy(leftThrow.GetComponent<BoxCollider>());
                    UnityEngine.Object.Destroy(leftThrow.GetComponent<Rigidbody>());

                    leftThrow.transform.position = GorillaLocomotion.Player.Instance.leftHandTransform.position;
                    leftThrow.transform.rotation = GorillaLocomotion.Player.Instance.leftHandTransform.rotation;
                    Rigidbody comp = leftThrow.AddComponent<Rigidbody>();
                    comp.velocity = EasyInputs.GetDeviceVelocity(EasyHand.LeftHand);
                }
            }
            else
            {
                if (leftThrow != null)
                {
                    UnityEngine.Object.Destroy(leftThrow);
                    leftThrow = null;
                }
            }

            if (rightPrimary)
            {
                if (rightThrow != null)
                {
                    GorillaLocomotion.Player.Instance.rightHandTransform.position = rightThrow.transform.position;
                    GorillaLocomotion.Player.Instance.rightHandTransform.rotation = rightThrow.transform.rotation;
                }
                else
                {
                    rightThrow = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    rightThrow.GetComponent<Renderer>().enabled = false;
                    UnityEngine.Object.Destroy(rightThrow.GetComponent<BoxCollider>());
                    UnityEngine.Object.Destroy(rightThrow.GetComponent<Rigidbody>());

                    rightThrow.transform.position = GorillaLocomotion.Player.Instance.rightHandTransform.position;
                    rightThrow.transform.rotation = GorillaLocomotion.Player.Instance.rightHandTransform.rotation;
                    Rigidbody comp = rightThrow.AddComponent<Rigidbody>();
                    comp.velocity = EasyInputs.GetDeviceVelocity(EasyHand.RightHand);
                }
            }
            else
            {
                if (rightThrow != null)
                {
                    UnityEngine.Object.Destroy(rightThrow);
                    rightThrow = null;
                }
            }
        }

        public static void EnableSteamLongArms()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }

        public static void DisableSteamLongArms()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public static void FlickJump()
        {
            if (rightPrimary)
            {
                GorillaLocomotion.Player.Instance.rightHandTransform.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, -1.5f, 0f);
            }
        }

        public static void LongJump()
        {
            if (rightPrimary)
            {
                if (longJumpPower == Vector3.zero)
                {
                    longJumpPower = GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity / 50f;
                    longJumpPower.y = 0f;
                }
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.transform.position += longJumpPower;
            }
            else
            {
                longJumpPower = Vector3.zero;
            }
        }

        public static void CopyMovementGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;

                if (gunLocked && lockTarget != null)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;

                    GorillaTagger.Instance.myVRRig.transform.position = lockTarget.transform.position;
                    GorillaTagger.Instance.myVRRig.transform.position = lockTarget.transform.position;
                    GorillaTagger.Instance.myVRRig.transform.rotation = lockTarget.transform.rotation;
                    GorillaTagger.Instance.myVRRig.transform.rotation = lockTarget.transform.rotation;

                    GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = lockTarget.leftHandTransform.position;
                    GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = lockTarget.rightHandTransform.position;

                    GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.rotation = lockTarget.leftHandTransform.rotation;
                    GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = lockTarget.rightHandTransform.rotation;

                    GorillaTagger.Instance.myVRRig.head.rigTarget.transform.rotation = lockTarget.headMesh.transform.rotation;

                    GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                    l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                    GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                    r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                    l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                    r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                    UnityEngine.Object.Destroy(l, Time.deltaTime);
                    UnityEngine.Object.Destroy(r, Time.deltaTime);
                }

                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who != null && who != GorillaTagger.Instance.myVRRig)
                    {
                        lockTarget = who;
                        gunLocked = true;
                    }
                }
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                if (gunLocked)
                {
                    gunLocked = false;
                }
            }
        }

        public static void FollowPlayerGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;

                if (gunLocked && lockTarget != null)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;

                    GorillaTagger.Instance.myVRRig.transform.position = lockTarget.transform.position;
                    GorillaTagger.Instance.myVRRig.transform.position = lockTarget.transform.position;
                    GorillaTagger.Instance.myVRRig.transform.rotation = lockTarget.transform.rotation;
                    GorillaTagger.Instance.myVRRig.transform.rotation = lockTarget.transform.rotation;

                    GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = lockTarget.leftHandTransform.position;
                    GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = lockTarget.rightHandTransform.position;

                    GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.rotation = lockTarget.leftHandTransform.rotation;
                    GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = lockTarget.rightHandTransform.rotation;

                    GorillaTagger.Instance.myVRRig.head.rigTarget.transform.rotation = lockTarget.headMesh.transform.rotation;

                    GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                    l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                    GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                    r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                    l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                    r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                    UnityEngine.Object.Destroy(l, Time.deltaTime);
                    UnityEngine.Object.Destroy(r, Time.deltaTime);
                }

                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who != null && who != GorillaTagger.Instance.myVRRig)
                    {
                        lockTarget = who;
                        gunLocked = true;
                    }
                }
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                if (gunLocked)
                {
                    gunLocked = false;
                }
            }
        }

        public static void OrbitPlayerGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;

                if (gunLocked && lockTarget != null)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;

                    GorillaTagger.Instance.myVRRig.transform.position = lockTarget.transform.position;
                    GorillaTagger.Instance.myVRRig.transform.position = lockTarget.transform.position;
                    GorillaTagger.Instance.myVRRig.transform.rotation = lockTarget.transform.rotation;
                    GorillaTagger.Instance.myVRRig.transform.rotation = lockTarget.transform.rotation;

                    GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = lockTarget.leftHandTransform.position;
                    GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = lockTarget.rightHandTransform.position;

                    GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.rotation = lockTarget.leftHandTransform.rotation;
                    GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = lockTarget.rightHandTransform.rotation;

                    GorillaTagger.Instance.myVRRig.head.rigTarget.transform.rotation = lockTarget.headMesh.transform.rotation;

                    GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                    l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                    GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                    r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                    l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                    r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                    UnityEngine.Object.Destroy(l, Time.deltaTime);
                    UnityEngine.Object.Destroy(r, Time.deltaTime);
                }

                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who != null && who != GorillaTagger.Instance.myVRRig)
                    {
                        lockTarget = who;
                        gunLocked = true;
                    }
                }
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                if (gunLocked)
                {
                    gunLocked = false;
                }
            }
        }

        public static void JumpscareGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;

                if (gunLocked && lockTarget != null)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;

                    GorillaTagger.Instance.myVRRig.transform.position = lockTarget.transform.position;
                    GorillaTagger.Instance.myVRRig.transform.position = lockTarget.transform.position;
                    GorillaTagger.Instance.myVRRig.transform.rotation = lockTarget.transform.rotation;
                    GorillaTagger.Instance.myVRRig.transform.rotation = lockTarget.transform.rotation;

                    GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = lockTarget.leftHandTransform.position;
                    GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = lockTarget.rightHandTransform.position;

                    GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.rotation = lockTarget.leftHandTransform.rotation;
                    GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = lockTarget.rightHandTransform.rotation;

                    GorillaTagger.Instance.myVRRig.head.rigTarget.transform.rotation = lockTarget.headMesh.transform.rotation;

                    GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                    l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                    GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                    r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                    l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                    r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                    UnityEngine.Object.Destroy(l, Time.deltaTime);
                    UnityEngine.Object.Destroy(r, Time.deltaTime);
                }

                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who != null && who != GorillaTagger.Instance.myVRRig)
                    {
                        lockTarget = who;
                        gunLocked = true;
                    }
                }
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                if (gunLocked)
                {
                    gunLocked = false;
                }
            }
        }

        public static void IntercourseGun()
        {
            if (GetGunInput(false))
            {
                var GunData = RenderGun();
                GameObject GunPointer = GunData.GunPointer;
                RaycastHit Ray = GunData.Ray;

                if (gunLocked && lockTarget != null)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;

                    GorillaTagger.Instance.myVRRig.transform.position = lockTarget.transform.position;
                    GorillaTagger.Instance.myVRRig.transform.position = lockTarget.transform.position;
                    GorillaTagger.Instance.myVRRig.transform.rotation = lockTarget.transform.rotation;
                    GorillaTagger.Instance.myVRRig.transform.rotation = lockTarget.transform.rotation;

                    GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.position = lockTarget.leftHandTransform.position;
                    GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.position = lockTarget.rightHandTransform.position;

                    GorillaTagger.Instance.myVRRig.leftHand.rigTarget.transform.rotation = lockTarget.leftHandTransform.rotation;
                    GorillaTagger.Instance.myVRRig.rightHand.rigTarget.transform.rotation = lockTarget.rightHandTransform.rotation;

                    GorillaTagger.Instance.myVRRig.head.rigTarget.transform.rotation = lockTarget.headMesh.transform.rotation;

                    GameObject l = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(l.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(l.GetComponent<SphereCollider>());

                    l.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    l.transform.position = GorillaTagger.Instance.leftHandTransform.position;

                    GameObject r = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(r.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(r.GetComponent<SphereCollider>());

                    r.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    r.transform.position = GorillaTagger.Instance.rightHandTransform.position;

                    l.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
                    r.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();

                    UnityEngine.Object.Destroy(l, Time.deltaTime);
                    UnityEngine.Object.Destroy(r, Time.deltaTime);
                }

                if (GetGunInput(true))
                {
                    VRRig who = Ray.collider.GetComponentInParent<VRRig>();
                    if (who != null && who != GorillaTagger.Instance.myVRRig)
                    {
                        lockTarget = who;
                        gunLocked = true;
                    }
                }
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                if (gunLocked)
                {
                    gunLocked = false;
                }
            }
        }

        public static void RemoveCopy()
        {
            isCopying = false;
            lockTarget = null;
            GorillaTagger.Instance.myVRRig.enabled = true;
        }

        public static void SpazHead()
        {
            //GorillaTagger.Instance.myVRRig.head.trackingRotationOffset.x = UnityEngine.Random.Range(0f, 360f);
            //GorillaTagger.Instance.myVRRig.head.trackingRotationOffset.y = UnityEngine.Random.Range(0f, 360f);
            //GorillaTagger.Instance.myVRRig.head.trackingRotationOffset.z = UnityEngine.Random.Range(0f, 360f);
        }

        public static void LaggyRig()
        {
            if (Time.time > laggyRigDelay)
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                idiotfixthingy = true;
                laggyRigDelay = Time.time + 0.5f;
            }
            else
            {
                if (idiotfixthingy)
                {
                    idiotfixthingy = false;
                }
                else
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;
                }
            }
        }

        public static void UpdateRig()
        {
            if (rightPrimary && !lastprimaryhit)
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                idiotfixthingy = true;
            }
            else
            {
                if (idiotfixthingy)
                {
                    idiotfixthingy = false;
                }
                else
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;
                }

            }
            lastprimaryhit = rightPrimary;
        }

        public static void RandomSpazHead()
        {
            if (headspazType)
            {
                SpazHead();
                if (Time.time > headspazDelay)
                {
                    headspazType = false;
                    headspazDelay = Time.time + UnityEngine.Random.Range(1000f, 4000f) / 1000f;
                }
            }
            else
            {
                Fun.FixHead();
                if (Time.time > headspazDelay)
                {
                    headspazType = true;
                    headspazDelay = Time.time + UnityEngine.Random.Range(200f, 1000f) / 1000f;
                }
            }
        }
    }
}
