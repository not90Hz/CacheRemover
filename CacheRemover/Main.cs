using System;
using MelonLoader;
using CacheRemover.Functions;
using BuildInfo = CacheRemover.Melon.BuildInfo;
using Main = CacheRemover.Main;
using System.Reflection;
using HarmonyLib;
using VRC.Core;
using Logger = CacheRemover.Functions.Logger;
using System.Linq;

[assembly: MelonInfo(typeof(Main), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author, BuildInfo.DownloadLink)]
[assembly: MelonGame("VRChat", "VRChat")]

namespace CacheRemover
{
    public class Main : MelonMod
    {

        //--Got from ReModCE
        public static HarmonyLib.Harmony Harmony { get; private set; }

        public override void OnApplicationStart()
        {
            try
            {
                Logger.Msg("Initializing...");

                //--Setting up events
                //--Got from ReModCE
                Harmony = MelonHandler.Mods.First(m => m.Info.Name == "CacheRemover").HarmonyInstance;
                //--Got from ReModCE
                Harmony.Patch(typeof(RoomManager).GetMethod(nameof(RoomManager.Method_Public_Static_Boolean_ApiWorld_ApiWorldInstance_String_Int32_0)), postfix: GetLocalPatch(nameof(EnterWorldPatch)));


                Handler.Setup();
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        }
        
        //--Got from ReModCE
        private static void EnterWorldPatch(ApiWorld __0, ApiWorldInstance __1)
        {
            try
            {
                //--Got from ReModCE
                if (__0 == null || __1 == null)
                    return;

                if (Handler.CurrentWorld == null && Handler.LastWorld == null)
                {
                    Handler.CurrentWorld = __1.world.name + "#" + __1.name;
                }
                else
                {
                    Handler.LastWorld = Handler.CurrentWorld;
                    Handler.CurrentWorld = __1.world.name + "#" + __1.name;
                }

                if (Handler.LastWorld != Handler.CurrentWorld && Handler.LastWorld != null) Handler.RemoveCache(false);

                if (Handler.LastWorld != null) Logger.Msg(string.Format("Left {0}", Handler.LastWorld));
                if (Handler.LastWorld != null) Logger.Msg(string.Format("Joined {0}", Handler.CurrentWorld));
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
                Handler.RemoveCache(true);
                Handler.Debug();
                Logger.Msg("Closing game now!");
            }
            catch(Exception e)
            {
                Logger.Error(e.ToString());
            }
        }
    }
}
