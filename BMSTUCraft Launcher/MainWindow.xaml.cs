using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace BMSTUCraft_Launcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameLauncher launcher = new GameLauncher();
        private WebClient webClient = new WebClient();

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
            if ((GameInfo.IsInstalled())&&(!webClient.IsBusy))
            {
                infoLabel.Content = "Установлено";
            }
            else
            {
                infoLabel.Content = "Не установлено";
            }
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameInfo.IsInstalled())
            {
                launcher.RunMinecraft();
                infoLabel.Content = "Запущено";
                WindowState = WindowState.Minimized;
            }
            else
            {
                if (!webClient.IsBusy)
                {
                    InstallGame();
                }
                else
                {
                    infoLabel.Content = "Устанавливается";
                }
            }
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameInfo.IsInstalled())
            {
                Visibility = Visibility.Hidden;
                SettingsWindow settingsWindow = new SettingsWindow();
                settingsWindow.SendMainWindowInstance(this);
                settingsWindow.ShowDialog();
                settingsWindow.WindowState = WindowState.Normal;
                launcher.nickName = settingsWindow.nickName;
                launcher.maxMem = settingsWindow.RAM;
                Visibility = Visibility.Visible;
            }
            else
            {
                infoLabel.Content = "Проблема с установкой";
            }
        }

        private void mainWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        public void InstallGame()
        {
            infoLabel.Content = "Установка игры";

            GameInfo.DeleteGame();

            infoProgressBar.Value = 0;
            infoProgressBar.Visibility = Visibility.Visible;
            try
            {
                webClient.DownloadFileAsync(new Uri(GameInfo.url), GameInfo.roamingFolder + @"/.bmtucraft.zip");
                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
            }
            catch (Exception)
            {
                infoLabel.Content = "Пробема со скачиванием";
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            infoLabel.Content = $"Загрузка {e.ProgressPercentage}%";
            infoProgressBar.Value = e.ProgressPercentage;
        }

        private async void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            infoLabel.Content = "Распаковка";
            await Task.Run(() => //костыль с асинхом чтобы не зависало
            {
                ZipFile.ExtractToDirectory(GameInfo.roamingFolder + @"/.bmtucraft.zip", GameInfo.minecraftFolder);
            });
            File.Delete(GameInfo.roamingFolder + @"/.bmtucraft.zip");
            infoLabel.Content = "Установлено";
            infoProgressBar.Visibility = Visibility.Hidden;
        }
    }
}