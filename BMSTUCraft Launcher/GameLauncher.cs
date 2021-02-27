using System;
using System.Diagnostics;
using System.IO;
using System.Windows;


namespace BMSTUCraft_Launcher
{
    public class GameLauncher
    {
        public int maxMem = 512 * 2;
        public string nickName = "Steve";
        private string logPath;

        #region launchSettings
        private Process minecraftInstance;

        private string minecraftFolder;
        private string javaPath;
        private string libPath;

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
            minecraftFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/.bmstucraft";
            javaPath = minecraftFolder + @"/Java/jre1.8.0_281/bin/javaw.exe";
            libPath = minecraftFolder + @"/libraries/";
            logPath = minecraftFolder + @"/latest.txt";
        }

        private void FillParams()
        {
            javaParams = $"-Xmx{maxMem}M";
            addParams = "-XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump";
            javaNatives = $"-Djava.library.path={minecraftFolder}/natives/";
            orgNatives = $"-Dorg.lwjgl.librarypath={minecraftFolder}/natives/";
            libraries = $"-cp \"{libPath}com/mojang/patchy/1.1/patchy-1.1.jar;{libPath}oshi-project/oshi-core/1.1/oshi-core-1.1.jar;{libPath}net/java/dev/jna/jna/4.4.0/jna-4.4.0.jar;{libPath}net/java/dev/jna/platform/3.4.0/platform-3.4.0.jar;{libPath}com/ibm/icu/icu4j-core-mojang/51.2/icu4j-core-mojang-51.2.jar;{libPath}net/sf/jopt-simple/jopt-simple/5.0.3/jopt-simple-5.0.3.jar;{libPath}com/paulscode/codecjorbis/20101023/codecjorbis-20101023.jar;{libPath}com/paulscode/codecwav/20101023/codecwav-20101023.jar;{libPath}com/paulscode/libraryjavasound/20101123/libraryjavasound-20101123.jar;{libPath}com/paulscode/librarylwjglopenal/20100824/librarylwjglopenal-20100824.jar;{libPath}com/paulscode/soundsystem/20120107/soundsystem-20120107.jar;{libPath}io/netty/netty-all/4.1.9.Final/netty-all-4.1.9.Final.jar;{libPath}com/google/guava/guava/21.0/guava-21.0.jar;{libPath}org/apache/commons/commons-lang3/3.5/commons-lang3-3.5.jar;{libPath}commons-io/commons-io/2.5/commons-io-2.5.jar;{libPath}commons-codec/commons-codec/1.10/commons-codec-1.10.jar;{libPath}net/java/jinput/jinput/2.0.5/jinput-2.0.5.jar;{libPath}net/java/jutils/jutils/1.0.0/jutils-1.0.0.jar;{libPath}com/google/code/gson/gson/2.8.0/gson-2.8.0.jar;{libPath}com/mojang/authlib/1.5.25/authlib-1.5.25.jar;{libPath}com/mojang/realms/1.10.22/realms-1.10.22.jar;{libPath}org/apache/commons/commons-compress/1.8.1/commons-compress-1.8.1.jar;{libPath}org/apache/httpcomponents/httpclient/4.3.3/httpclient-4.3.3.jar;{libPath}commons-logging/commons-logging/1.1.3/commons-logging-1.1.3.jar;{libPath}org/apache/httpcomponents/httpcore/4.3.2/httpcore-4.3.2.jar;{libPath}it/unimi/dsi/fastutil/7.1.0/fastutil-7.1.0.jar;{libPath}org/apache/logging/log4j/log4j-api/2.8.1/log4j-api-2.8.1.jar;{libPath}org/apache/logging/log4j/log4j-core/2.8.1/log4j-core-2.8.1.jar;{libPath}org/lwjgl/lwjgl/lwjgl/2.9.4-nightly-20150209/lwjgl-2.9.4-nightly-20150209.jar;{libPath}org/lwjgl/lwjgl/lwjgl_util/2.9.4-nightly-20150209/lwjgl_util-2.9.4-nightly-20150209.jar;{libPath}org/lwjgl/lwjgl/lwjgl-platform/2.9.4-nightly-20150209/lwjgl-platform-2.9.4-nightly-20150209.jar;{libPath}org/lwjgl/lwjgl/lwjgl/2.9.2-nightly-20140822/lwjgl-2.9.2-nightly-20140822.jar;{libPath}org/lwjgl/lwjgl/lwjgl_util/2.9.2-nightly-20140822/lwjgl_util-2.9.2-nightly-20140822.jar;{libPath}org/lwjgl/lwjgl/lwjgl-platform/2.9.2-nightly-20140822/lwjgl-platform-2.9.2-nightly-20140822-natives-windows.jar;{libPath}net/java/jinput/jinput-platform/2.0.5/jinput-platform-2.0.5-natives-windows.jar;{libPath}com/mojang/text2speech/1.10.3/text2speech-1.10.3.jar;{libPath}com/mojang/text2speech/1.10.3/text2speech-1.10.3.jar;{libPath}ca/weblite/java-objc-bridge/1.0.0/java-objc-bridge-1.0.0.jar;{libPath}ca/weblite/java-objc-bridge/1.0.0/java-objc-bridge-1.0.0.jar;{minecraftFolder}/versions/1.12.2/1.12.2.jar;{libPath}net/minecraftforge/forge/1.12.2-14.23.5.2854/forge-1.12.2-14.23.5.2854.jar;{libPath}org/ow2/asm/asm-debug-all/5.2/asm-debug-all-5.2.jar;{libPath}com/typesafe/akka/akka-actor_2.11/2.3.3/akka-actor_2.11-2.3.3.jar;{libPath}com/typesafe/config/1.2.1/config-1.2.1.jar;{libPath}org/scala-lang/scala-actors-migration_2.11/1.1.0/scala-actors-migration_2.11-1.1.0.jar;{libPath}org/scala-lang/scala-compiler/2.11.1/scala-compiler-2.11.1.jar;{libPath}org/scala-lang/plugins/scala-continuations-library_2.11/1.0.2/scala-continuations-library_2.11-1.0.2.jar;{libPath}org/scala-lang/plugins/scala-continuations-plugin_2.11.1/1.0.2/scala-continuations-plugin_2.11.1-1.0.2.jar;{libPath}org/scala-lang/scala-library/2.11.1/scala-library-2.11.1.jar;{libPath}org/scala-lang/scala-parser-combinators_2.11/1.0.1/scala-parser-combinators_2.11-1.0.1.jar;{libPath}org/scala-lang/scala-reflect/2.11.1/scala-reflect-2.11.1.jar;{libPath}org/scala-lang/scala-swing_2.11/1.0.1/scala-swing_2.11-1.0.1.jar;{libPath}org/scala-lang/scala-xml_2.11/1.0.2/scala-xml_2.11-1.0.2.jar;{libPath}java3d/vecmath/1.5.2/vecmath-1.5.2.jar;{libPath}net/sf/trove4j/trove4j/3.0.3/trove4j-3.0.3.jar;{libPath}org/apache/maven/maven-artifact/3.5.3/maven-artifact-3.5.3.jar;{libPath}net/minecraftforge/forge/1.12.2-14.23.5.2768/forge-1.12.2-14.23.5.2768.jar;{libPath}net/minecraft/launchwrapper/1.12/launchwrapper-1.12.jar;{libPath}lzma/lzma/0.0.1/lzma-0.0.1.jar;{libPath}net/sf/jopt-simple/jopt-simple/5.0.3/jopt-simple-5.0.3.jar\"";
            JVM = "-d64";
            mainClassName = "net.minecraft.launchwrapper.Launch";
            windowSize = "--width 800 --height 480";
            user = $"--username {nickName}";
            version = "1.12.2-forge1.12.2-14.23.5.2854";
            gameDir = $"--gameDir {minecraftFolder}";
            assetDir = $"--assetsDir {minecraftFolder}/assets/";
            assetIndex = "--assetIndex 1.12";
            UUID = "--uuid 14565";
            accessToken = "--accessToken BMSTU";
            other = "--userType mojang --tweakClass net.minecraftforge.fml.common.launcher.FMLTweaker --versionType Forge";
        }

        public void RunMinecraft()
        {
            FillParams();
            if (File.Exists(logPath))
            {
                File.WriteAllText(logPath, "");
            }
            else
            {
                File.Create(logPath).Dispose();
            }
            ProcessStartInfo processInfo = new ProcessStartInfo()
            {
                FileName = javaPath,
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
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл javaw.exe не найден. Проверьте, установили ли вы Java. Файл должен лежать в Program Files в папке Java и там внутри еще папки и там bin", "Проблема с установкой Java");
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        private void MinecraftInstance_DataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                File.AppendAllText(logPath, e.Data + Environment.NewLine);
            }
            catch (Exception)
            {
            }
        }

        //public void Stop()
        //{
        //    minecraftInstance.Kill();
        //}
    }
}
