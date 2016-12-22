using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MultiThreadMusicPlayer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        MediaPlayer MPlayerOne;
        MediaPlayer MPlayerTwo;
        private bool IsOpened;

        public MainWindow()
        {
            InitializeComponent();
            MPlayerOne = new MediaPlayer();
            MPlayerTwo = new MediaPlayer();
        }

        private void Play(object player)
        {
            MediaPlayer Player = player as MediaPlayer;

            Player.Play();
        }

        private void Pause(object player)
        {
            var Player = player as MediaPlayer;
            Player.Pause();

        }

        private void Stop(object player)
        {
            var Player = player as MediaPlayer;
            Player.Stop();
        }

        private void Open_Track_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.DefaultExt = ".mp3";
            fd.Filter = "Music files (.mp3)|*.mp3";
            bool? result = fd.ShowDialog();

            if (result == true)
            {
                MPlayerOne.Open(new Uri(fd.FileName, UriKind.Absolute));
                IsOpened = true;
            }
        }

        private void Play_1(object sender, RoutedEventArgs e)
        {
            if (IsOpened)
                Dispatcher.BeginInvoke(new ParameterizedThreadStart(Play), MPlayerOne);

        }

        private void Pause_1(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ParameterizedThreadStart(Pause), MPlayerOne);
        }

        private void Stop_1(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ParameterizedThreadStart(Stop), MPlayerOne);
        }

        private void Open_Track_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.DefaultExt = ".mp3";
            fd.Filter = "Music files (.mp3)|*.mp3";
            bool? result = fd.ShowDialog();

            if (result == true)
            {
                MPlayerTwo.Open(new Uri(fd.FileName, UriKind.Absolute));
                IsOpened = true;
            }
        }

        private void Play_2(object sender, RoutedEventArgs e)
        {
            if (IsOpened)
                Dispatcher.BeginInvoke(new ParameterizedThreadStart(Play), MPlayerTwo);

        }

        private void Pause_2(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ParameterizedThreadStart(Pause), MPlayerTwo);
        }

        private void Stop_2(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ParameterizedThreadStart(Stop), MPlayerTwo);
        }


    }
}

