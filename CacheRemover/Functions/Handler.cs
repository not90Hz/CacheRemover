using MelonLoader;
using System;
using System.IO;
using CacheRemover.Internals;

namespace CacheRemover.Functions
{
    class Handler
    {

        public static void Setup()
        {
            //--Get LocalLow path of your machine
            Struct.LocalLowPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");

            Load();

            Logger.Msg("Initialized!");
        }

        public static void Load()
        {
            //--Create folder if it not exists
            if (!Directory.Exists(Struct.ModConfigPath)) Directory.CreateDirectory(Struct.ModConfigPath);

            //--Check if config file exists and create it if not
            if (!File.Exists(Struct.ModConfigPath + Struct.ModConfigFile))
            {
                File.WriteAllText(Struct.ModConfigPath + Struct.ModConfigFile, "True");
            }

            //--Get bool that has been set in the config
            if (File.Exists(Struct.ModConfigPath + Struct.ModConfigFile)) Struct.RemoveFiles = bool.Parse(File.ReadAllText(Struct.ModConfigPath + Struct.ModConfigFile));
        }

        public static void Save()
        {
            //--Create folder if it not exists
            if (!Directory.Exists(Struct.ModConfigPath)) Directory.CreateDirectory(Struct.ModConfigPath);

            //--Check if config file exists and create it if not
            if (!File.Exists(Struct.ModConfigPath + Struct.ModConfigFile))
            {
                File.WriteAllText(Struct.ModConfigPath + Struct.ModConfigFile, "True");
            }

            //--Set bool that has been set in the local config
            if (File.Exists(Struct.ModConfigPath + Struct.ModConfigFile)) File.WriteAllText(Struct.ModConfigPath + Struct.ModConfigFile, Struct.RemoveFiles.ToString());
        }

        public static void RemoveCache()
        {
            try
            {
                //--Check if the bool is true
                if (Struct.RemoveFiles)
                {
                    int deletedFiles = 0;
                    int deletedFolders = 0;
                    int notdeletedFiles = 0;
                    int notdeletedFolders = 0;
                    DirectoryInfo di = new DirectoryInfo(Struct.LocalLowPath + Struct.VRChatCachePath);
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
                if (!File.Exists(Struct.ModConfigPath + Struct.ModDebugFile)) File.WriteAllText(Struct.ModConfigPath + Struct.ModDebugFile, "Nothing here...");

                if (File.Exists(Struct.ModConfigPath + Struct.ModDebugFile))
                {
                    File.WriteAllText(Struct.ModConfigPath + Struct.ModDebugFile, Struct.DebugText);
                }
            }
            catch{}
        }
    }
}
