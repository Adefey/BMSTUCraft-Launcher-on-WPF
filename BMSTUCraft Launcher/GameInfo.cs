using System;
using System.IO;

namespace BMSTUCraft_Launcher
{
    internal static class GameInfo
    {
        public static string url = "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/EaE_FOpikzVdjQ";
        public static string roamingFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string minecraftFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/.bmstucraft";
        public static string cfgPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/.bmstucraft/launcheroptions.txt";
        public static string libPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/.bmstucraft/libraries/";
        public static string logPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/.bmstucraft/latestlog.txt";
        public static string javaPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/.bmstucraft/Java/jre1.8.0_281/bin/javaw.exe";
        public static string baseNick = "Steve";
        public static int baseRAM = 4096;
        public static bool IsInstalled()
        {
            bool res = true;
            if ((!Directory.Exists(minecraftFolder)) || (!Directory.Exists(minecraftFolder + @"/versions/1.12.2-forge-14.23.5.2854"))
                || (!File.Exists(minecraftFolder + @"/mods/19032021.txt")))
            {
                res = false;
            }
            return res;
        }
        public static void DeleteGame()
        {
            if (File.Exists(GameInfo.roamingFolder + @"/.bmtucraft.zip"))
            {
                File.Delete(GameInfo.roamingFolder + @"/.bmtucraft.zip");
            }

            if (Directory.Exists(GameInfo.minecraftFolder))
            {
                Directory.Delete(GameInfo.minecraftFolder, true);
            }
        }
    }
}
