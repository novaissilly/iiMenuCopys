using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static iiMenu.Menu.Main;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class NotificationManager : MonoBehaviour
    {
        public NotificationManager(IntPtr e) : base(e) { }
        public static void LoadNotis()
        {
            MainCamera = GameObject.Find("Main Camera");
            HUDObj = new GameObject();
            HUDObj2 = new GameObject();
            HUDObj2.name = "NOTIFICATIONLIB_HUD_OBJ";
            HUDObj.name = "NOTIFICATIONLIB_HUD_OBJ";
            HUDObj.AddComponent<Canvas>();
            HUDObj.AddComponent<CanvasScaler>();
            HUDObj.AddComponent<GraphicRaycaster>();
            HUDObj.GetComponent<Canvas>().enabled = true;
            HUDObj.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            HUDObj.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
            HUDObj.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 5f);
            HUDObj.GetComponent<RectTransform>().position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
            HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z - 4.6f);
            HUDObj.transform.parent = HUDObj2.transform;
            HUDObj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
            Vector3 eulerAngles = HUDObj.GetComponent<RectTransform>().rotation.eulerAngles;
            eulerAngles.y = -270f;
            HUDObj.transform.localScale = new Vector3(1f, 1f, 1f);
            HUDObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(eulerAngles);
            Testtext = new GameObject
            {
                transform =
                {
                    parent = HUDObj.transform
                }
            }.AddComponent<Text>();
            Testtext.text = "";
            Testtext.fontSize = 30;
            Testtext.font = Arial;
            Testtext.rectTransform.sizeDelta = new Vector2(450f, 210f);
            Testtext.alignment = TextAnchor.LowerLeft;
            Testtext.rectTransform.localScale = new Vector3(0.00333333333f, 0.00333333333f, 0.33333333f);
            Testtext.rectTransform.localPosition = new Vector3(-1f, -1f, -0.5f);
            Testtext.material = AlertText;
            NotificationManager.NotifiText = Testtext;
        }

        public virtual void FixedUpdate()
        {
            if (!HasInit && GameObject.Find("Main Camera") != null)
            {
                LoadNotis();
                HasInit = true;
            }
            HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
            HUDObj2.transform.rotation = MainCamera.transform.rotation;
            if (Testtext.text != "")
            {
                NotificationDecayTimeCounter++;
                if (NotificationDecayTimeCounter > NotificationDecayTime)
                {
                    Notifilines = null;
                    newtext = "";
                    NotificationDecayTimeCounter = 0;
                    Notifilines = Enumerable.ToArray<string>(Enumerable.Skip<string>(Testtext.text.Split(Environment.NewLine.ToCharArray()), 1));
                    foreach (string text in Notifilines)
                    {
                        if (text != "")
                        {
                            newtext = newtext + text + "\n";
                        }
                    }
                    Testtext.text = newtext;
                }
            }
            else
            {
                NotificationDecayTimeCounter = 0;
            }
        }

        public static async void SendNotification(string NotificationText, int delay = -1)
        {
            if (disableNotifications) return;

            try
            {
                if (IsEnabled && PreviousNotifi != NotificationText)
                {
                    if (!NotificationText.Contains(Environment.NewLine))
                    {
                        NotificationText += Environment.NewLine;
                    }

                    NotifiText.text += NotificationText;
                    NotifiText.supportRichText = true;
                    PreviousNotifi = NotificationText;

                    if (delay > 0)
                    {
                        await Task.Delay(delay);
                        ClearPastNotifications(1);
                    }
                }
            }
            catch
            {
                UnityEngine.Debug.LogError("Notification failed, object probably nil due to third person ; " + NotificationText);
            }
        }

        public static void ClearAllNotifications()
        {
            //NotifiLib.NotifiText.text = "<color=grey>[</color><color=green>SUCCESS</color><color=grey>]</color> <color=white>Notifications cleared.</color>" + Environment.NewLine;
            NotifiText.text = "";
        }

        public static void ClearPastNotifications(int amount)
        {
            string text = "";
            foreach (string text2 in Enumerable.ToArray<string>(Enumerable.Skip<string>(NotifiText.text.Split(Environment.NewLine.ToCharArray()), amount)))
            {
                if (text2 != "")
                {
                    text = text + text2 + "\n";
                }
            }
            NotifiText.text = text;
        }

        static GameObject HUDObj;
        static GameObject HUDObj2;
        static GameObject MainCamera;
        static Text Testtext;
        static Material AlertText = new Material(Shader.Find("GUI/Text Shader"));
        static int NotificationDecayTime = 144;
        static int NotificationDecayTimeCounter;
        public static int NoticationThreshold = 30;
        static string[] Notifilines;
        static string newtext;
        public static string PreviousNotifi;
        static bool HasInit;
        static Text NotifiText;
        public static bool IsEnabled = true;
    }
}