using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheRemover.Internals
{
    class Struct
    {
        //--%appdata%\LocalLow
        public static string LocalLowPath = null;

        //--%appdata%\LocalLow\VRChat\VRChat\Cache-WindowsPlayer
        public static string VRChatCachePath = @"\VRChat\VRChat\Cache-WindowsPlayer";

        //--Mod config path
        public static string ModConfigPath = Directory.GetCurrentDirectory() + @"\UserData\CacheRemover";

        //--Mod config file
        public static string ModConfigFile = @"\config.txt";

        //--Mod cache file
        public static string ModDebugFile = @"\cache.txt";

        //--Bool that checks if the files should get removed or not
        public static bool RemoveFiles = false;

        public static string DebugText = "";

        public static string LastWorld = null;

        public static string CurrentWorld = null;
    }
}
