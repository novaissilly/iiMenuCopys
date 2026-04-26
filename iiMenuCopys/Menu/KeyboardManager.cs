using easyInputs;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace iiMenu
{
    public class KeyboardManagerButtonInfo
    {
        public string Text;
        public Action Action;
        public GameObject Object;
        public Renderer Renderer;
    }

    // THIS IS A SHITCODE BUT WORKS SO
    public class KeyboardManager
    {
        public static GameObject Root;
        public static TextMeshPro TitleText;

        private static List<KeyboardManagerButtonInfo> keys = new();

        private static GameObject leftSphere;
        private static GameObject rightSphere;

        public static bool KeyboardEnabled;
        private static bool moveMode;
        private static bool inputdelay = false;

        private static readonly Vector3 Hidden = Vector3.zero;

        private static readonly Vector3 Visible = Vector3.one * 0.55f;

        private const int Layer = 30;
        private const float KeyDepth = -0.02f;

        public static void Search()
        {
            if (!System.IO.File.Exists(Path.Combine(Application.persistentDataPath, "iisStupidMenu/Usekeyboardbefore.txt")))
            {
                NotificationManager.SendNotification("Make sure to use left joystick to toggle the keyboard in place and to mvoe it.");
                System.IO.File.WriteAllText("iisStupidMenu" + "Usekeyboardbefore.txt", "Make sure to use left joystick to toggle the keyboard in place and to mvoe it.");
            }

            KeyboardEnabled = !KeyboardEnabled;

            iiMenu.Menu.Main.pageNumber = 0;
            iiMenu.Menu.Main.keyboardInput = "";

            if (KeyboardEnabled)
                Load();
            else
                DestroyKeyboard();
        }

        public static void DestroyKeyboard()
        {
            if (Root != null)
                GameObject.Destroy(Root);
            if (leftSphere != null)
                GameObject.Destroy(leftSphere);
            if (rightSphere != null)
                GameObject.Destroy(rightSphere);
        }

        public static void Load()
        {
            if (Camera.main == null) return;
            Root = new GameObject("iiMenuKeyboard");
            Root.layer = Layer;
            CreateBackground();
            CreateTitle();
            CreateHandSpheres();
            BuildLayout();
            SpawnKeys();
            Root.transform.localScale = Vector3.zero;
        }

        private static void BuildLayout()
        {
            keys.Clear();
            AddRow("1", "2", "3", "4", "5", "6", "7", "8", "9", "0");
            AddRow("Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P");
            AddRow("A", "S", "D", "F", "G", "H", "J", "K", "L");
            AddRow("Z", "X", "C", "V", "B", "N", "M");
            AddSpecial("DELETE");
            AddSpecial("SPACE");
            AddSpecial("ENTER");
        }

        private static void AddRow(params string[] row)
        {
            foreach (string key in row)
            {
                string captured = key;
                keys.Add(new KeyboardManagerButtonInfo
                {
                    Text = captured,
                    Action = () =>
                    {
                        TitleText.text += captured;
                        iiMenu.Menu.Main.keyboardInput = TitleText.text;
                    }
                });
            }
        }

        private static void AddSpecial(string name)
        {
            keys.Add(new KeyboardManagerButtonInfo
            {
                Text = name,
                Action = () =>
                {
                    switch (name)
                    {
                        case "DELETE":
                            if (TitleText.text.Length > 0)
                                TitleText.text =
                                    TitleText.text.Substring(0, TitleText.text.Length - 1);
                            break;
                        case "SPACE":
                            TitleText.text += " ";
                            break;
                        case "ENTER":
                            iiMenu.Menu.Main.keyboardInput = TitleText.text;
                            break;
                    }
                }
            });
        }

        private static void SpawnKeys()
        {
            float spacingX = 0.09f;
            float spacingY = 0.11f;
            float startY = 0.18f;
            int index = 0;
            int[] rowCounts = { 10, 10, 9, 7 };
            for (int row = 0; row < rowCounts.Length; row++)
            {
                int count = rowCounts[row];
                float startX = -(count - 1) * spacingX * 0.5f;
                for (int col = 0; col < count; col++)
                {
                    if (index >= keys.Count) return;

                    Vector3 pos = new(startX + col * spacingX, startY - row * spacingY, KeyDepth);
                    CreateKey(keys[index++], pos);
                }
            }
            float specialY = -0.32f;
            CreateKey(GetKey("DELETE"), new Vector3(-0.35f, specialY, KeyDepth), 0.15f);
            CreateKey(GetKey("SPACE"), new Vector3(0.00f, specialY, KeyDepth), 0.28f);
            CreateKey(GetKey("ENTER"), new Vector3(0.35f, specialY, KeyDepth), 0.15f);
        }

        private static KeyboardManagerButtonInfo GetKey(string name)
        {
            foreach (var k in keys)
                if (k.Text == name)
                    return k;
            return null;
        }
        private static void CreateKey(KeyboardManagerButtonInfo info, Vector3 pos, float width = 0.08f)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.SetParent(Root.transform, false);
            obj.transform.localPosition = pos;
            obj.transform.localScale = new Vector3(width, 0.08f, 0.04f);

            Renderer r = obj.GetComponent<Renderer>();
            r.material = new Material(Shader.Find("Standard"));
            r.material.color = Menu.Main.buttonColors[1].GetCurrentColor();
            BoxCollider col = obj.GetComponent<BoxCollider>();
            col.isTrigger = true;
            GameObject textObj = new("Text");
            textObj.transform.SetParent(obj.transform);
            textObj.transform.localPosition = new Vector3(0, 0.07f, -0.57f);
            textObj.transform.localRotation = Quaternion.Euler(20f, 0, 0);
            TextMeshPro tmp = textObj.AddComponent<TextMeshPro>();
            tmp.text = info.Text;
            tmp.fontSize = 0.45f;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = Color.white;
            info.Object = obj;
            info.Renderer = r;
            SphereKeyTrigger trigger = obj.AddComponent<SphereKeyTrigger>();
            trigger.Info = info;
        }

        private static void CreateBackground()
        {
            GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Cube);
            bg.transform.SetParent(Root.transform);
            bg.transform.localScale = new Vector3(1.0f, 0.75f, 0.03f);
            bg.transform.localPosition = new Vector3(0, -0.12f, 0f);
            var r = bg.GetComponent<Renderer>();
            r.material = new Material(Shader.Find("Standard"));
            r.material.color = new Color(0.08f, 0.08f, 0.08f);
            UnityEngine.Object.Destroy(bg.GetComponent<Collider>());
        }

        private static void CreateTitle()
        {
            GameObject t = new("Title");
            t.transform.SetParent(Root.transform);
            t.transform.localPosition = new Vector3(0, 0.38f, KeyDepth);
            t.transform.localScale = Vector3.one * 0.12f;
            TitleText = t.AddComponent<TextMeshPro>();
            TitleText.alignment = TextAlignmentOptions.Center;
            TitleText.fontSize = 7;
            TitleText.color = Color.white;
        }

        private static void CreateHandSpheres()
        {
            rightSphere = CreateSphere(GorillaTagger.Instance.rightHandTransform);
            leftSphere = CreateSphere(GorillaTagger.Instance.leftHandTransform);
        }

        private static GameObject CreateSphere(Transform parent)
        {
            GameObject s = new GameObject("KeyboardSphere");
            s.transform.SetParent(parent);
            s.transform.localPosition = new Vector3(0.013f, -0.025f, 0.1f);
            s.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            MeshRenderer mr = s.AddComponent<MeshRenderer>();
            mr.material = new Material(Shader.Find("Unlit/Color"));
            mr.material.color = Color.gray;
            SphereCollider sc = s.AddComponent<SphereCollider>();
            sc.isTrigger = true;
            sc.radius = 0.5f;
            return s;
        }

        public static void Update()
        {
            if (Root == null || Camera.main == null) return;
            if (EasyInputs.GetThumbStickButtonDown(EasyHand.LeftHand) && !inputdelay)
            {
                inputdelay = true;
                moveMode = !moveMode;
            }
            if (!EasyInputs.GetThumbStickButtonDown(EasyHand.LeftHand) && inputdelay)
                inputdelay = false;
            if (leftSphere != null)
                leftSphere.SetActive(KeyboardEnabled);
            if (rightSphere != null)
                rightSphere.SetActive(KeyboardEnabled);
            if (!moveMode)
            {
                Vector3 target = Camera.main.transform.position + Camera.main.transform.forward * 0.6f + Camera.main.transform.up * -0.08f; 
                Root.transform.position = Vector3.Lerp(Root.transform.position, target, Time.deltaTime * 10f);
                Root.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Vector3.up);
            }
            Root.transform.localScale = Vector3.Lerp(Root.transform.localScale, KeyboardEnabled ? Visible : Hidden, Time.deltaTime * 8f);
        }
    }

    [MelonLoader.RegisterTypeInIl2Cpp]
    public class SphereKeyTrigger : MonoBehaviour
    {
        public SphereKeyTrigger(IntPtr ptr) : base(ptr) { }

        public KeyboardManagerButtonInfo Info;
        private static float lastPress;
        private const float Delay = 0.2f;

        public void OnTriggerStay(Collider other)
        {
            if (!other.name.Contains("KeyboardSphere")) return;
            if (Time.time - lastPress < Delay) return;
            lastPress = Time.time;
            Press();
        }
        private void Press()
        {
            if (Info == null) return;

            Info.Renderer.material.color = Color.cyan;
            Info.Action?.Invoke();
            MelonLoader.MelonCoroutines.Start(Reset(Info.Renderer));
        }
        private System.Collections.IEnumerator Reset(Renderer r)
        {
            yield return new WaitForSeconds(0.15f);
            r.material.color = new Color(0.12f, 0.25f, 0.55f);
        }
    }
}