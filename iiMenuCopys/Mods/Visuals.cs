using iiMenu.Classes;
using System.Linq;
using UnityEngine;
using static iiMenu.Menu.Main;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods
{
    internal class Visuals
    {
        public static void FixRigColors()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                vrrig.mainSkin.material.color = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
            }
        }

        public static void EnableRemoveLeaves()
        {
            foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>())
            {
                if (g.activeSelf && g.name.Contains("smallleaves"))
                {
                    g.SetActive(false);
                    leaves.Add(g);
                }
            }
        }

        public static void DisableRemoveLeaves()
        {
            foreach (GameObject l in leaves)
            {
                l.SetActive(true);
            }
            leaves.Clear();
        }

        public static void CasualTracers()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.myVRRig)
                {
                    GameObject line = new GameObject("Line");
                    LineRenderer liner = line.AddComponent<LineRenderer>();
                    UnityEngine.Color thecolor = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
                    liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                    liner.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                    liner.SetPosition(1, vrrig.headMesh.transform.position);
                    liner.material.shader = Shader.Find("GUI/Text Shader");
                    UnityEngine.Object.Destroy(line, Time.deltaTime);
                }
            }
        }

        public static void InfectionTracers()
        {
            bool isInfectedPlayers = false;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig.mainSkin.material.name.Contains("fected"))
                {
                    isInfectedPlayers = true;
                    break;
                }
            }
            if (isInfectedPlayers)
            {
                if (!GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected"))
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig.mainSkin.material.name.Contains("fected") && vrrig != GorillaTagger.Instance.myVRRig)
                        {
                            GameObject line = new GameObject("Line");
                            LineRenderer liner = line.AddComponent<LineRenderer>();
                            UnityEngine.Color thecolor = new Color32(255, 111, 0, 255);
                            liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                            liner.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                            liner.SetPosition(1, vrrig.headMesh.transform.position);
                            liner.material.shader = Shader.Find("GUI/Text Shader");
                            UnityEngine.Object.Destroy(line, Time.deltaTime);
                        }
                    }
                }
                else
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (!vrrig.mainSkin.material.name.Contains("fected") && vrrig != GorillaTagger.Instance.myVRRig)
                        {
                            GameObject line = new GameObject("Line");
                            LineRenderer liner = line.AddComponent<LineRenderer>();
                            UnityEngine.Color thecolor = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
                            liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                            liner.SetPosition(0, GorillaLocomotion.Player.Instance.rightHandTransform.position);
                            liner.SetPosition(1, vrrig.transform.position);
                            liner.material.shader = Shader.Find("GUI/Text Shader");
                            UnityEngine.Object.Destroy(line, Time.deltaTime);
                        }
                    }
                }
            }
            else
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.myVRRig)
                    {
                        GameObject line = new GameObject("Line");
                        LineRenderer liner = line.AddComponent<LineRenderer>();
                        UnityEngine.Color thecolor = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
                        liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                        liner.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                        liner.SetPosition(1, vrrig.transform.position);
                        liner.material.shader = Shader.Find("GUI/Text Shader");
                        UnityEngine.Object.Destroy(line, Time.deltaTime);
                    }
                }
            }
        }

        public static void CasualBoneESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                UnityEngine.Color thecolor = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
                if (vrrig != GorillaTagger.Instance.myVRRig)
                {
                    LineRenderer liner = vrrig.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                    liner.startWidth = 0.025f;
                    liner.endWidth = 0.025f;

                    liner.startColor = thecolor;
                    liner.endColor = thecolor;

                    liner.material.shader = Shader.Find("GUI/Text Shader");

                    liner.SetPosition(0, vrrig.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
                    liner.SetPosition(1, vrrig.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));

                    UnityEngine.Object.Destroy(liner, Time.deltaTime);
                    for (int i = 0; i < bones.Count<int>(); i += 2)
                    {
                        liner = vrrig.mainSkin.bones[bones[i]].gameObject.AddComponent<LineRenderer>();

                        liner.startWidth = 0.025f;
                        liner.endWidth = 0.025f;

                        liner.startColor = thecolor;
                        liner.endColor = thecolor;

                        liner.material.shader = Shader.Find("GUI/Text Shader");

                        liner.SetPosition(0, vrrig.mainSkin.bones[bones[i]].position);
                        liner.SetPosition(1, vrrig.mainSkin.bones[bones[i + 1]].position);

                        UnityEngine.Object.Destroy(liner, Time.deltaTime);
                    }
                }
            }
        }

        public static void InfectionBoneESP()
        {
            bool isInfectedPlayers = false;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig.mainSkin.material.name.Contains("fected"))
                {
                    isInfectedPlayers = true;
                    break;
                }
            }
            if (isInfectedPlayers)
            {
                if (!GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected"))
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        UnityEngine.Color thecolor = new Color32(255, 111, 0, 255);
                        if (vrrig.mainSkin.material.name.Contains("fected") && vrrig != GorillaTagger.Instance.myVRRig)
                        {
                            LineRenderer liner = vrrig.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                            liner.startWidth = 0.025f;
                            liner.endWidth = 0.025f;

                            liner.startColor = thecolor;
                            liner.endColor = thecolor;

                            liner.material.shader = Shader.Find("GUI/Text Shader");

                            liner.SetPosition(0, vrrig.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
                            liner.SetPosition(1, vrrig.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));

                            UnityEngine.Object.Destroy(liner, Time.deltaTime);
                            for (int i = 0; i < bones.Count<int>(); i += 2)
                            {
                                liner = vrrig.mainSkin.bones[bones[i]].gameObject.AddComponent<LineRenderer>();

                                liner.startWidth = 0.025f;
                                liner.endWidth = 0.025f;

                                liner.startColor = thecolor;
                                liner.endColor = thecolor;

                                liner.material.shader = Shader.Find("GUI/Text Shader");

                                liner.SetPosition(0, vrrig.mainSkin.bones[bones[i]].position);
                                liner.SetPosition(1, vrrig.mainSkin.bones[bones[i + 1]].position);

                                UnityEngine.Object.Destroy(liner, Time.deltaTime);
                            }
                        }
                    }
                }
                else
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        UnityEngine.Color thecolor = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
                        if (!vrrig.mainSkin.material.name.Contains("fected") && vrrig != GorillaTagger.Instance.myVRRig)
                        {
                            LineRenderer liner = vrrig.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                            liner.startWidth = 0.025f;
                            liner.endWidth = 0.025f;

                            liner.startColor = thecolor;
                            liner.endColor = thecolor;

                            liner.material.shader = Shader.Find("GUI/Text Shader");

                            liner.SetPosition(0, vrrig.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
                            liner.SetPosition(1, vrrig.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));

                            UnityEngine.Object.Destroy(liner, Time.deltaTime);
                            for (int i = 0; i < bones.Count<int>(); i += 2)
                            {
                                liner = vrrig.mainSkin.bones[bones[i]].gameObject.AddComponent<LineRenderer>();

                                liner.startWidth = 0.025f;
                                liner.endWidth = 0.025f;

                                liner.startColor = thecolor;
                                liner.endColor = thecolor;

                                liner.material.shader = Shader.Find("GUI/Text Shader");

                                liner.SetPosition(0, vrrig.mainSkin.bones[bones[i]].position);
                                liner.SetPosition(1, vrrig.mainSkin.bones[bones[i + 1]].position);

                                UnityEngine.Object.Destroy(liner, Time.deltaTime);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    UnityEngine.Color thecolor = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
                    if (vrrig != GorillaTagger.Instance.myVRRig)
                    {
                        LineRenderer liner = vrrig.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                        liner.startWidth = 0.025f;
                        liner.endWidth = 0.025f;

                        liner.startColor = thecolor;
                        liner.endColor = thecolor;

                        liner.material.shader = Shader.Find("GUI/Text Shader");

                        liner.SetPosition(0, vrrig.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
                        liner.SetPosition(1, vrrig.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));

                        UnityEngine.Object.Destroy(liner, Time.deltaTime);
                        for (int i = 0; i < bones.Count<int>(); i += 2)
                        {
                            liner = vrrig.mainSkin.bones[bones[i]].gameObject.AddComponent<LineRenderer>();

                            liner.startWidth = 0.025f;
                            liner.endWidth = 0.025f;

                            liner.startColor = thecolor;
                            liner.endColor = thecolor;

                            liner.material.shader = Shader.Find("GUI/Text Shader");

                            liner.SetPosition(0, vrrig.mainSkin.bones[bones[i]].position);
                            liner.SetPosition(1, vrrig.mainSkin.bones[bones[i + 1]].position);

                            UnityEngine.Object.Destroy(liner, Time.deltaTime);
                        }
                    }
                }
            }
        }

        public static void CasualChams()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.myVRRig)
                {
                    vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                    vrrig.mainSkin.material.color = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
                }
            }
        }

        public static void InfectionChams()
        {
            bool isInfectedPlayers = false;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig.mainSkin.material.name.Contains("fected"))
                {
                    isInfectedPlayers = true;
                    break;
                }
            }
            if (isInfectedPlayers)
            {
                if (!GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected"))
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig.mainSkin.material.name.Contains("fected") && vrrig != GorillaTagger.Instance.myVRRig)
                        {
                            vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                            vrrig.mainSkin.material.color = new Color32(255, 111, 0, 255);
                        }
                        else
                        {
                            vrrig.mainSkin.material.shader = Shader.Find("Standard");
                        }
                    }
                }
                else
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (!vrrig.mainSkin.material.name.Contains("fected") && vrrig != GorillaTagger.Instance.myVRRig)
                        {
                            vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                            vrrig.mainSkin.material.color = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
                        }
                    }
                }
            }
            else
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.myVRRig)
                    {
                        vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                        vrrig.mainSkin.material.color = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
                    }
                }
            }
        }

        public static void DisableChams()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.myVRRig)
                {
                    vrrig.mainSkin.material.shader = Shader.Find("Standard");
                }
            }
        }

        public static void CasualBeacons()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.myVRRig)
                {
                    GameObject line = new GameObject("Line");
                    LineRenderer liner = line.AddComponent<LineRenderer>();
                    UnityEngine.Color thecolor = Color.cyan;
                    liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                    liner.SetPosition(0, vrrig.transform.position + new Vector3(0f, 9999f, 0f));
                    liner.SetPosition(1, vrrig.transform.position - new Vector3(0f, 9999f, 0f));
                    liner.material.shader = Shader.Find("GUI/Text Shader");
                    UnityEngine.Object.Destroy(line, Time.deltaTime);
                }
            }
        }

        public static void InfectionBeacons()
        {
            bool isInfectedPlayers = false;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig.mainSkin.material.name.Contains("fected"))
                {
                    isInfectedPlayers = true;
                    break;
                }
            }
            if (isInfectedPlayers)
            {
                if (!GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected"))
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig.mainSkin.material.name.Contains("fected") && vrrig != GorillaTagger.Instance.myVRRig)
                        {
                            GameObject line = new GameObject("Line");
                            LineRenderer liner = line.AddComponent<LineRenderer>();
                            UnityEngine.Color thecolor = new Color32(255, 111, 0, 255);
                            liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                            liner.SetPosition(0, vrrig.transform.position + new Vector3(0f, 9999f, 0f));
                            liner.SetPosition(1, vrrig.transform.position - new Vector3(0f, 9999f, 0f));
                            liner.material.shader = Shader.Find("GUI/Text Shader");
                            UnityEngine.Object.Destroy(line, Time.deltaTime);
                        }
                    }
                }
                else
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (!vrrig.mainSkin.material.name.Contains("fected") && vrrig != GorillaTagger.Instance.myVRRig)
                        {
                            GameObject line = new GameObject("Line");
                            LineRenderer liner = line.AddComponent<LineRenderer>();
                            UnityEngine.Color thecolor = Color.cyan;
                            liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                            liner.SetPosition(0, vrrig.transform.position + new Vector3(0f, 9999f, 0f));
                            liner.SetPosition(1, vrrig.transform.position - new Vector3(0f, 9999f, 0f));
                            liner.material.shader = Shader.Find("GUI/Text Shader");
                            UnityEngine.Object.Destroy(line, Time.deltaTime);
                        }
                    }
                }
            }
            else
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.myVRRig)
                    {
                        GameObject line = new GameObject("Line");
                        LineRenderer liner = line.AddComponent<LineRenderer>();
                        UnityEngine.Color thecolor = Color.cyan;
                        liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                        liner.SetPosition(0, vrrig.transform.position + new Vector3(0f, 9999f, 0f));
                        liner.SetPosition(1, vrrig.transform.position - new Vector3(0f, 9999f, 0f));
                        liner.material.shader = Shader.Find("GUI/Text Shader");
                        UnityEngine.Object.Destroy(line, Time.deltaTime);
                    }
                }
            }
        }

        public static void CasualBoxESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.myVRRig)
                {
                    UnityEngine.Color thecolor = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
                    GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    box.transform.position = vrrig.transform.position;
                    box.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                    box.GetComponent<Renderer>().material.color = thecolor;
                    UnityEngine.Object.Destroy(box, Time.deltaTime);
                }
            }
        }

        public static void InfectionBoxESP()
        {
            bool isInfectedPlayers = false;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig.mainSkin.material.name.Contains("fected"))
                {
                    isInfectedPlayers = true;
                    break;
                }
            }
            if (isInfectedPlayers)
            {
                if (!GorillaTagger.Instance.myVRRig.mainSkin.material.name.Contains("fected"))
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (vrrig.mainSkin.material.name.Contains("fected") && vrrig != GorillaTagger.Instance.myVRRig)
                        {
                            UnityEngine.Color thecolor = new Color32(255, 111, 0, 255);
                            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            box.transform.position = vrrig.transform.position;
                            box.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                            box.GetComponent<Renderer>().material.color = thecolor;
                            UnityEngine.Object.Destroy(box, Time.deltaTime);
                        }
                    }
                }
                else
                {
                    foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    {
                        if (!vrrig.mainSkin.material.name.Contains("fected") && vrrig != GorillaTagger.Instance.myVRRig)
                        {
                            UnityEngine.Color thecolor = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
                            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            box.transform.position = vrrig.transform.position;
                            box.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                            box.GetComponent<Renderer>().material.color = thecolor;
                            UnityEngine.Object.Destroy(box, Time.deltaTime);
                        }
                    }
                }
            }
            else
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.myVRRig)
                    {
                        UnityEngine.Color thecolor = new Color(vrrig.mainSkin.material.color.r * 9f, vrrig.mainSkin.material.color.g * 9f, vrrig.mainSkin.material.color.b * 9f);
                        GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        box.transform.position = vrrig.transform.position;
                        box.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                        box.GetComponent<Renderer>().material.color = thecolor;
                        UnityEngine.Object.Destroy(box, Time.deltaTime);
                    }
                }
            }
        }

        public static void ShowButtonColliders()
        {
            GameObject left = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            left.transform.parent = GorillaTagger.Instance.leftHandTransform;
            left.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
            left.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            left.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            UnityEngine.Object.Destroy(left.GetComponent<SphereCollider>());
            UnityEngine.Object.Destroy(left, Time.deltaTime);

            GameObject right = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            right.transform.parent = GorillaTagger.Instance.rightHandTransform;
            right.GetComponent<Renderer>().material.color = backgroundColor.GetCurrentColor();
            right.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            right.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            UnityEngine.Object.Destroy(right.GetComponent<SphereCollider>());
            UnityEngine.Object.Destroy(right, Time.deltaTime);
        }

        public static void ConsoleBeacon(string id, string version, string menuName)
        {
            Photon.Realtime.Player sender = RigManager.GetPlayerFromID(id);
            VRRig vrrig = RigManager.GetVRRigFromPlayer(sender);

            Color userColor = Color.red;

            NotificationManager.SendNotification("<color=grey>[</color><color=purple>ADMIN</color><color=grey>]</color> " + sender.NickName + " is using " + menuName + " version " + version + ".", 3000);
            
            GameObject line = new GameObject("Line");
            LineRenderer liner = line.AddComponent<LineRenderer>();
            liner.startColor = userColor; liner.endColor = userColor; liner.startWidth = 0.25f; liner.endWidth = 0.25f; liner.positionCount = 2; liner.useWorldSpace = true;

            liner.SetPosition(0, vrrig.transform.position + new Vector3(0f, 9999f, 0f));
            liner.SetPosition(1, vrrig.transform.position - new Vector3(0f, 9999f, 0f));
            liner.material.shader = Shader.Find("GUI/Text Shader");
            Object.Destroy(line, 3f);
        }
    }
}