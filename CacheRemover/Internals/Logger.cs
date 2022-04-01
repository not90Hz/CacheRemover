using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheRemover.Internals
{
    class Logger
    {
        public static void Msg(string text)
        {
            MelonLogger.Msg(text);
            Struct.DebugText = Struct.DebugText + "\n  [Log] " + text + "\n";
        }

        public static void Warning(string text)
        {
            MelonLogger.Warning(text);
            Struct.DebugText = Struct.DebugText + "\n [Warning] " + text + "\n";
        }

        public static void Error(string text)
        {
            MelonLogger.Error(text);
            Struct.DebugText = Struct.DebugText + "\n [Error] " + text + "\n";
        }
    }
}
