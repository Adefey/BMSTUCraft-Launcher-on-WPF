using System;
using System.IO;

namespace BMSTUCraft_Launcher
{
    internal static class GameInfo
    {
        public static string url = "https://getfile.dokpub.com/yandex/get/https://disk.yandex.ru/d/Yqh8URbu0OwImA";
        public static string roamingFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string minecraftFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/.bmstucraft";
        public static string cfgPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/.bmstucraft/launcheroptions.txt";
        public static bool isInstalled()
        {
            bool res = true;
            if ((!Directory.Exists(minecraftFolder)) || (!Directory.Exists(minecraftFolder + @"/versions/1.12.2-forge-14.23.5.2854"))
                || (!File.Exists(minecraftFolder + @"/mods/pack.txt")))
            {
                res = false;
            }
            return res;
        }
    }
}
