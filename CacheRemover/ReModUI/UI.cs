using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheRemover.Functions;
using CacheRemover.Internals;
using CacheRemover.Melon;
using ReMod.Core;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using ResourceManager = CacheRemover.Internals.ResourceManager;

namespace CacheRemover.ReModUI
{
    class UI
    {
        public static UiManager ui;
        public static IButtonPage mainmenu;

        public static ReMenuToggle enable;
        public static ReMenuButton clear;
        public static ReMenuButton save;
        public static ReMenuButton load;

        //Ingame QuickMenu Tabbuttonpage
        public static void CreateUI()
        {
            ui = new UiManager(BuildInfo.Name, ResourceManager.GetSprite("mod.tabbutton"), false);
            mainmenu = ui.MainMenu;

            enable = mainmenu.AddToggle("Enable", "Enable removing cache", (Use) => Struct.RemoveFiles = Use, Struct.RemoveFiles);
            clear = mainmenu.AddButton("Clear", "Clear cache", () =>
            {
                Handler.RemoveCache();
            });

            //Spacer
            mainmenu.AddSpacer();
            mainmenu.AddSpacer();
            mainmenu.AddSpacer();
            mainmenu.AddSpacer();
            mainmenu.AddSpacer();
            mainmenu.AddSpacer();
            mainmenu.AddSpacer();
            mainmenu.AddSpacer();
            mainmenu.AddSpacer();
            mainmenu.AddSpacer();
            //


            save = mainmenu.AddButton("Save", "Save Settings!", () =>
            {
                Handler.Save();
            });

            //Spacer
            mainmenu.AddSpacer();
            mainmenu.AddSpacer();
            //

            load = mainmenu.AddButton("Load", "Load Settings!", () =>
            {
                Handler.Load();
            });
        }
    }
}
