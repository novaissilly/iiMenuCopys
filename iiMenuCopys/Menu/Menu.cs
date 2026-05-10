using easyInputs;
using NUnit.Framework.Internal;
using ShibaGTGenesis.Classes;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace ShibaGTGenesis.Menu
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Menu : MonoBehaviour
    {
        public Menu(IntPtr e) : base(e) { }

        public virtual void Awake()
        {
            Instance = this;
        }

        public virtual void Update()
        {
            bool open = rightHanded ? EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand) : EasyInputs.GetSecondaryButtonDown(EasyHand.LeftHand);

            fps = (1f / Time.deltaTime).ToString("F0");

            if (menu == null)
            {
                if (open)
                {
                    Draw();

                    RecenterMenu();
                    CreateReference();
                }
            }
            else
            {
                if (open)
                {
                    RecenterMenu();
                }
                else
                {
                    GameObject.Destroy(menu);
                    menu = null;

                    GameObject.Destroy(reference);
                    reference = null;
                }
            }
        }

        public void Draw()
        {
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject.Destroy(menu.GetComponent<BoxCollider>());
            GameObject.Destroy(menu.GetComponent<Rigidbody>());
            GameObject.Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.4f);

            menubackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menubackground.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menubackground.GetComponent<BoxCollider>());
            menubackground.transform.parent = menu.transform;
            menubackground.transform.rotation = Quaternion.identity;
            menubackground.transform.localScale = new Vector3(0.1f, 1f, 1f);
            menubackground.transform.position = new Vector3(0.05f, 0f, 0f);
            menubackground.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
            menubackground.GetComponent<Renderer>().material.color = Color.black;

            canvasObject = new GameObject();
            canvasObject.transform.parent = menu.transform;
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1500f;

            Text title = new GameObject()
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            title.color = Color.white;
            title.text = "ShibaGT Genesis V1.0 - Fps: " + fps;
            title.supportRichText = true;
            title.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            title.fontSize = 1;
            title.resizeTextMinSize = 0;
            title.resizeTextForBestFit = true;
            title.alignment = TextAnchor.MiddleCenter;
            RectTransform component = title.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.05f);
            component.position = new Vector3(0.06f, 0f, 0.165f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            ButtonInfo[] activeButtons = Buttons.buttons[buttonType].Skip(pageNumber * buttonsPerPage).Take(buttonsPerPage).ToArray();
            for (int i = 0; i < activeButtons.Length; i++)
                AddButton(i * 0.1f, i, activeButtons[i]);
        }

        public void AddButton(float offset, int buttonIndex, ButtonInfo method)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
            gameObject.AddComponent<Classes.ButtonCollider>().relatedText = method.buttonText;
            gameObject.GetComponent<Renderer>().material.color = Color.grey;

            Text text = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = method.buttonText;
            if (method.overlapText != null)
            {
                text.text = method.overlapText;
            }
            text.supportRichText = true;
            text.fontSize = 1;
            if (method.enabled)
            {
                text.color = Color.white;
            }
            else
            {
                text.color = Color.black;
            }
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.Normal;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .03f);
            component.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));


            if (method.toolTip != "")
            {
                Text tooltip = new GameObject()
                {
                    transform =
                {
                    parent = canvasObject.transform
                }
                }.AddComponent<Text>();
                tooltip.color = Color.white;
                tooltip.text = method.toolTip;
                tooltip.supportRichText = true;
                tooltip.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                tooltip.fontSize = 1;
                tooltip.resizeTextMinSize = 0;
                tooltip.resizeTextForBestFit = true;
                tooltip.alignment = TextAnchor.MiddleCenter;
                RectTransform component2 = tooltip.GetComponent<RectTransform>();
                component2.localPosition = Vector3.zero;
                component2.sizeDelta = new Vector2(0.24f, 0.03f);
                component2.position = new Vector3(0.06f, 0f, -0.165f);
                component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }
        }

        public void CreateReference()
        {
            reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            reference.transform.parent = rightHanded ? GorillaTagger.Instance.leftHandTransform : GorillaTagger.Instance.rightHandTransform;
            reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            reference.GetComponent<Renderer>().material.color = Color.white;
            referencecollider = reference.GetComponent<SphereCollider>();
        }

        public void RecenterMenu()
        {
            if (rightHanded)
            {
                menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                Vector3 rotation = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
                rotation += new Vector3(0f, 0f, 180f);
                menu.transform.rotation = Quaternion.Euler(rotation);
            }
            else
            {
                menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
            }
        }

        public void ReloadMenu()
        {
            if (menu != null)
            {
                GameObject.Destroy(menu);
                menu = null;
                Draw();
            }
            if (reference != null)
            {
                GameObject.Destroy(reference);
                reference = null;
                CreateReference();
            }
        }

        public void Toggle(string buttontext)
        {

        }

        public static Menu Instance;

        public GameObject menu;
        public GameObject menubackground;
        public GameObject canvasObject;

        public GameObject reference;
        public SphereCollider referencecollider;

        public bool rightHanded = false;

        public int buttonType = 0;
        public int pageNumber = 0;

        public string fps;

        public int buttonsPerPage = 6;
    }
}