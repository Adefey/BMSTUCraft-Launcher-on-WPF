using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BMSTUCraft_Launcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameLauncher launcher = new GameLauncher();

        public MainWindow()
        {        
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

            private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
            {
                infoLabel.Text = "Распаковка";
                ZipFile.ExtractToDirectory(gameInfo.roamingFolder + @"/.bmtucraft.zip", gameInfo.minecraftFolder);
                File.Delete(gameInfo.roamingFolder + @"/.bmtucraft.zip");
                infoLabel.Text = "Установлено";
                installProgressBar.Visible = false;
            }


            private void WebClient_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
            {
                infoLabel.Text = $"Загрузка {e.ProgressPercentage}%";
                installProgressBar.Value = e.ProgressPercentage;
            }
        }
    }
}
