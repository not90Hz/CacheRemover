using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheRemover.Functions
{
    class Logger
    {
        public static void Msg(string text)
        {
            MelonLogger.Msg(text);
            Handler.DebugText = Handler.DebugText + "\n  [Log] " + text + "\n";
        }

        public static void Warning(string text)
        {
            MelonLogger.Warning(text);
            Handler.DebugText = Handler.DebugText + "\n [Warning] " + text + "\n";
        }

        public static void Error(string text)
        {
            MelonLogger.Error(text);
            Handler.DebugText = Handler.DebugText + "\n [Error] " + text + "\n";
        }
    }
}
