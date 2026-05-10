using GorillaNetworking;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShibaGTGenesis
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Boards : MonoBehaviour
    {
        public Boards(IntPtr e) : base(e) { }

        public GorillaLevelScreen[] cachedScreens;
        public Text coctext, coc, motdtext, motd;
        public Material boardmat;
        public virtual void Start()
        {
            cachedScreens = GorillaComputer.instance.levelScreens;
            coctext = GameObject.Find("COC Text").GetComponent<Text>();
            coc = GameObject.Find("CodeOfConduct").GetComponent<Text>();
            motdtext = GameObject.Find("motdtext").GetComponent<Text>();
            motd = GameObject.Find("motd").GetComponent<Text>();
            boardmat = new Material(Shader.Find("Unlit/Color"));
            boardmat.color = Color.black;
        }

        public virtual void Update()
        {
            bool objectsActive = GameObject.Find("motd").activeSelf;
            if (objectsActive)
            {
                motd.text = "<color=blue>GENESIS NEWS</color>";
                motdtext.text = "GENESIS";
                coc.text = "<color=blue>GENESIS</color>";
                coctext.text = "idk yet";
            }

            foreach (var screen in cachedScreens)
            {
                screen.goodMaterial = boardmat;
                screen.badMaterial = boardmat;
            }
        }
    }
}