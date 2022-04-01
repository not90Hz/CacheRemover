using System;
using MelonLoader;
using CacheRemover.Functions;
using BuildInfo = CacheRemover.Melon.BuildInfo;
using Main = CacheRemover.Main;
using System.Reflection;
using HarmonyLib;
using VRC.Core;
using Logger = CacheRemover.Internals.Logger;
using System.Linq;
using CacheRemover.Internals;
using System.IO;
using System.Text.RegularExpressions;
using CacheRemover.ReModUI;
using CacheRemover.Utils;

[assembly: MelonInfo(typeof(Main), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author, BuildInfo.DownloadLink)]
[assembly: MelonGame("VRChat", "VRChat")]

namespace CacheRemover
{
    public class Main : MelonMod
    {

        public static HarmonyLib.Harmony Harmony { get; private set; }

        public override void OnApplicationStart()
        {
            try
            {
                Logger.Msg("Initializing...");


                var ourAssembly = Assembly.GetExecutingAssembly();
                var resources = ourAssembly.GetManifestResourceNames();
                foreach (var resource in resources)
                {
                    if (!resource.EndsWith(".png"))
                        continue;

                    var stream = ourAssembly.GetManifestResourceStream(resource);

                    using var ms = new MemoryStream();
                    stream.CopyTo(ms);
                    var resourceName = Regex.Match(resource, @"([a-zA-Z\d\-_]+)\.png").Groups[1].ToString();
                    ResourceManager.LoadSprite("mod", resourceName, ms.ToArray());
                }
                Harmony = MelonHandler.Mods.First(m => m.Info.Name == BuildInfo.Name).HarmonyInstance;
                Harmony.Patch(typeof(RoomManager).GetMethod(nameof(RoomManager.Method_Public_Static_Boolean_ApiWorld_ApiWorldInstance_String_Int32_0)), postfix: GetLocalPatch(nameof(EnterWorldPatch)));

                MelonCoroutines.Start(Events.StartUI());

                Events.OnUiManagerInit += OnUIManagerInit;

                Handler.Setup();
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        }

        //Init UI
        private static void OnUIManagerInit()
        {
            UI.CreateUI();
        }

        //--Got from ReModCE
        private static void EnterWorldPatch(ApiWorld __0, ApiWorldInstance __1)
        {
            try
            {
                //--Got from ReModCE
                if (__0 == null || __1 == null)
                    return;

                if (Struct.CurrentWorld == null && Struct.LastWorld == null)
                {
                    Struct.CurrentWorld = __1.world.name + "#" + __1.name;
                }
                else
                {
                    Struct.LastWorld = Struct.CurrentWorld;
                    Struct.CurrentWorld = __1.world.name + "#" + __1.name;
                }

                if (Struct.LastWorld != Struct.CurrentWorld && Struct.LastWorld != null) Handler.RemoveCache();

                if (Struct.LastWorld != null) Logger.Msg(string.Format("Left {0}", Struct.LastWorld));
                if (Struct.LastWorld != null) Logger.Msg(string.Format("Joined {0}", Struct.CurrentWorld));
            }
            catch{}
        }

        private static HarmonyMethod GetLocalPatch(string name)
        {
            return typeof(Main).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static).ToNewHarmonyMethod();
        }

        public override void OnApplicationQuit()
        {
            try
            {
                Handler.RemoveCache();
                Handler.Debug();
                Handler.Save();
                Logger.Msg("Closing game now!");
            }
            catch(Exception e)
            {
                Logger.Error(e.ToString());
            }
        }
    }
}
