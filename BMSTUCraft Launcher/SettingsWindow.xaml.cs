using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
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
            Close();
        }

        private void RAMSlider_DragEnter(object sender, DragEventArgs e)
        {
            RAMInfoLabel.Content = $"Выбрано {(int)RAMSlider.Value * 512}МБ";
        }
    }
}
