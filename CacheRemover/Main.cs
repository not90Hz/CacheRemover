using System;
using MelonLoader;
using CacheRemover.Functions;
using BuildInfo = CacheRemover.Melon.BuildInfo;
using Main = CacheRemover.Main;

[assembly: MelonInfo(typeof(Main), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author, BuildInfo.DownloadLink)]
[assembly: MelonGame("VRChat", "VRChat")]

namespace CacheRemover
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            try
            {
                Handler.Setup();
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        }

        public override void OnApplicationQuit()
        {
            try
            {
                Handler.RemoveCache();
                Handler.Debug();
            }
            catch(Exception e)
            {
                Logger.Error(e.ToString());
            }
        }
    }
}
