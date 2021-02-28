using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows;

namespace BMSTUCraft_Launcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameLauncher launcher = new GameLauncher();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void foldButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void mainWindow_Activated(object sender, EventArgs e)
        {
            if (!gameInfo.isInstalled())
            {
                infoLabel.Content = "Установка игры";
                try
                {
                    Directory.Delete(gameInfo.minecraftFolder, true);
                }
                catch (DirectoryNotFoundException)
                {
                }

                infoProgressBar.Visibility = Visibility.Visible;
                WebClient webClient = new WebClient();
                try
                {
                    webClient.DownloadFileAsync(new Uri(gameInfo.url), gameInfo.roamingFolder + @"/.bmtucraft.zip");
                    webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                    webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                }
                catch (Exception)
                {
                    infoLabel.Content = "Пробема со скачиванием";
                }
            }
            else
            {
                infoLabel.Content = "Установлено";
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            infoLabel.Content = $"Загрузка {e.ProgressPercentage}%";
            infoProgressBar.Value = e.ProgressPercentage;
        }

        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            infoLabel.Content = "Распаковка";
            ZipFile.ExtractToDirectory(gameInfo.roamingFolder + @"/.bmtucraft.zip", gameInfo.minecraftFolder);
            File.Delete(gameInfo.roamingFolder + @"/.bmtucraft.zip");
            infoLabel.Content = "Установлено";
            infoProgressBar.Visibility = Visibility.Hidden;
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameInfo.isInstalled())
            {
                launcher.RunMinecraft();
                infoLabel.Content = "Запущено";
                WindowState = WindowState.Minimized;
            }
            else
            {
                infoLabel.Content = "Проблема с установкой";
            }
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {

            Visibility = Visibility.Hidden;
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
            settingsWindow.WindowState = WindowState.Normal;
            launcher.nickName = settingsWindow.nickName;
            launcher.maxMem = settingsWindow.RAM;
            Visibility = Visibility.Visible;
        }

        private void mainWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
