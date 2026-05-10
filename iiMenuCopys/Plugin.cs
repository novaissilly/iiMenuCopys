using MelonLoader;
using ShibaGTGenesis;
using UnhollowerRuntimeLib;
using UnityEngine;

[assembly: MelonInfo(typeof(Plugin), "ShibaGTGenesis", "1.0.0", "Nova_ShibaGTGenesis")]
[assembly: MelonGame()]
namespace ShibaGTGenesis
{
    public class Plugin : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            ClassInjector.RegisterTypeInIl2Cpp<Menu.Menu>();
            ClassInjector.RegisterTypeInIl2Cpp<NotificationManager>();
            ClassInjector.RegisterTypeInIl2Cpp<Boards>();
            HarmonyLib.Harmony harmony = new HarmonyLib.Harmony("Nova_ShibaGTGenesis");
            harmony.PatchAll();
            GameObject holder_genesis = new GameObject();
            holder_genesis.name = "ShibaGTGenesis_holder";
            holder_genesis.AddComponent<Menu.Menu>();
            holder_genesis.AddComponent<Boards>();
            holder_genesis.AddComponent<NotificationManager>();
        }
    }
}