using System;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace BMSTUCraft_Launcher
{
    public class GameLauncher
    {
        private Process minecraftInstance;
        public int maxMem = GameInfo.baseRAM;
        public string nickName = GameInfo.baseNick;
        private MainWindow mainWindow;

        #region launchSettings      
        private string javaParams;
        private string addParams;
        private string javaNatives;
        private string orgNatives;
        private string libraries;
        private string JVM;
        private string mainClassName;
        private string windowSize;
        private string user;
        private string version;
        private string gameDir;
        private string assetDir;
        private string assetIndex;
        private string UUID;
        private string accessToken;
        private string other;
        #endregion

        public GameLauncher()
        {
            if (File.Exists(GameInfo.cfgPath))
            {
                string[] launcherOptions = File.ReadLines(GameInfo.cfgPath).ToArray();
                nickName = launcherOptions[0];
                maxMem = Convert.ToInt32(launcherOptions[1]);
            }
        }

        public void SendMainWindowInstance(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        private void FillLibPaths(string path)
        {
            if (Directory.GetDirectories(path).Length == 0)
            {
                foreach (string fileName in Directory.GetFiles(path))
                {
                    libraries += $"{fileName};";
                }
            }
            else
            {
                foreach (string dirName in Directory.GetDirectories(path))
                {
                    FillLibPaths(dirName);
                }
            }
        }

        private void FillParams()
        {
            javaParams = $"-Xmx{maxMem}M -XX:+OptimizeFill -XX:+OptimizeStringConcat -XX:+UseFastAccessorMethods";
            addParams = "-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump";
            javaNatives = $"-Djava.library.path={GameInfo.minecraftFolder}/natives/";
            orgNatives = $"-Dorg.lwjgl.librarypath={GameInfo.minecraftFolder}/natives/";
            libraries = @"-cp \";
            FillLibPaths(GameInfo.libPath);
            libraries += $"{GameInfo.minecraftFolder}/versions/1.12.2/1.12.2.jar;";
            JVM = "-d64";
            mainClassName = "net.minecraft.launchwrapper.Launch";
            windowSize = "--width 800 --height 480";
            user = $"--username {nickName}";
            version = "1.12.2-forge1.12.2-14.23.5.2854";
            gameDir = $"--gameDir {GameInfo.minecraftFolder}";
            assetDir = $"--assetsDir {GameInfo.minecraftFolder}/assets/";
            assetIndex = "--assetIndex 1.12";
            UUID = "--uuid 14565";
            accessToken = "--accessToken BMSTU";
            other = "--userType mojang --tweakClass net.minecraftforge.fml.common.launcher.FMLTweaker --versionType Forge";
        }

        public void RunMinecraft()
        {
            FillParams();
            if (File.Exists(GameInfo.logPath))
            {
                File.WriteAllText(GameInfo.logPath, "");
            }
            else
            {
                File.Create(GameInfo.logPath).Dispose();
            }
            ProcessStartInfo processInfo = new ProcessStartInfo()
            {
                FileName = GameInfo.javaPath,
                Arguments = $"{javaParams} {addParams} {javaNatives} {orgNatives} " +
                $"{libraries} {JVM} {mainClassName} {windowSize} {user} {version} {gameDir} {assetDir} " +
                $"{assetIndex} {UUID} {accessToken} {other}",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };
            minecraftInstance = new Process
            {
                StartInfo = processInfo // = Process.Start(processInfo);
            };
            try
            {
                minecraftInstance.Start();
                minecraftInstance.BeginOutputReadLine();
                minecraftInstance.BeginErrorReadLine();
                minecraftInstance.OutputDataReceived += MinecraftInstance_DataReceived;
                minecraftInstance.ErrorDataReceived += MinecraftInstance_DataReceived;
                minecraftInstance.Exited += MinecraftInstance_Exited;
            }
            catch (FileNotFoundException)
            {
                mainWindow.infoLabel.Content = "Файл javaw.exe не найден. Переустановите игру";
            }
            catch (Exception)
            {
                mainWindow.infoLabel.Content = "Непредвиденная ошибка";
            }
        }

        private void MinecraftInstance_DataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                File.AppendAllText(GameInfo.logPath, e.Data + Environment.NewLine);
            }
            catch (Exception)
            {
            }
        }

        private void MinecraftInstance_Exited(object sender, EventArgs e)
        {
            mainWindow.infoLabel.Content = "Процесс завершен";
        }

        //public void Stop()
        //{
        //    minecraftInstance.Kill();
        //}
    }
}
