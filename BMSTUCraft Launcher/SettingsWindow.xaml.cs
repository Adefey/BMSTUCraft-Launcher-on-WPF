using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace BMSTUCraft_Launcher
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {        
        public string nickName = "Steve";
        public int RAM = 1024;

        public SettingsWindow()
        {
            InitializeComponent();

            if (File.Exists(GameInfo.cfgPath))
            {
                string[] launcherOptions = File.ReadLines(GameInfo.cfgPath).ToArray();
                nickName = launcherOptions[0];
                RAM = Convert.ToInt32(launcherOptions[1]);
                nickTextBox.Text = nickName;
                RAMSlider.Value = RAM / 512;
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void foldButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            nickName = nickTextBox.Text;
            RAM = (int)RAMSlider.Value * 512;
            File.WriteAllText(GameInfo.cfgPath, $"{nickName}\r\n{RAM}");
            Close();
        }

        private void RAMSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RAMInfoLabel.Content = $"Выбрано {(int)RAMSlider.Value * 512}МБ";
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
