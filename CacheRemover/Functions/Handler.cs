using MelonLoader;
using System;
using System.IO;

namespace CacheRemover.Functions
{
    class Handler
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

        public static void Setup()
        {
            //--Get LocalLow path of your machine
            LocalLowPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");

            Check();

            Logger.Msg("Initialized!");

            Logger.Msg(string.Format("In \"{0}\" you can set the config to True (using) or False (not using) to use the mod functions.", ModConfigPath + ModConfigFile));

        }

        public static void Check()
        {
            //--Create folder if it not exists
            if (!Directory.Exists(ModConfigPath)) Directory.CreateDirectory(ModConfigPath);

            //--Check if config file exists and create it if not
            if (!File.Exists(ModConfigPath + ModConfigFile))
            {
                File.WriteAllText(ModConfigPath + ModConfigFile, "True");
            }

            //--Get bool that has been set in the config
            if (File.Exists(ModConfigPath + ModConfigFile)) RemoveFiles = bool.Parse(File.ReadAllText(ModConfigPath + ModConfigFile));
        }

        public static void RemoveCache(bool close)
        {
            try
            {
                //--Check if the bool is true
                if (RemoveFiles)
                {
                    int deletedFiles = 0;
                    int deletedFolders = 0;
                    int notdeletedFiles = 0;
                    int notdeletedFolders = 0;
                    DirectoryInfo di = new DirectoryInfo(LocalLowPath + VRChatCachePath);
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        foreach (FileInfo file in dir.GetFiles())
                        {
                            try
                            {
                                //--Delete file
                                file.Delete();

                                //--Deleted files amount + 1
                                deletedFiles++;
                            }
                            catch
                            {
                                //--Not deleted files amount + 1
                                notdeletedFiles++;
                            }
                        }
                        try
                        {
                            //--Delete folder
                            dir.Delete(true);

                            //--Deleted folders amount + 1
                            deletedFolders++;
                        }
                        catch
                        {
                            //--Not deleted folders amount + 1
                            notdeletedFolders++;
                        }
                    }
                    Logger.Warning("VRChat cache has been deleted.");
                    Logger.Msg(string.Format("Removed: Files={0} | Folders={1}", deletedFiles.ToString(), deletedFolders.ToString()));
                    Logger.Msg(string.Format("Couldnt Remove: Files={0} | Folders={1}", notdeletedFiles.ToString(), notdeletedFolders.ToString()));
                }
                else
                {
                    Logger.Warning("VRChat cache has not been deleted.");
                    Logger.Msg("Closing game now.");
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        }

        public static void Debug()
        {
            try
            {
                if (!File.Exists(ModConfigPath + ModDebugFile)) File.WriteAllText(ModConfigPath + ModDebugFile, "Nothing here...");

                if (File.Exists(ModConfigPath + ModDebugFile))
                {
                    File.WriteAllText(ModConfigPath + ModDebugFile, DebugText);
                }
            }
            catch{}
        }
    }
}
