using Microsoft.Win32;
using MultiThreadMusicPlayer.Media;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace MultiThreadMusicPlayer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayList PListOne;
        PlayList PListTwo;
        Track SelectedTrackOne;
        Track SelectedTrackTwo;
        MediaPlayer MPlayerOne;
        MediaPlayer MPlayerTwo;
        private bool IsOpened;

        public MainWindow()
        {
            InitializeComponent();
            MPlayerOne = new MediaPlayer();
            MPlayerTwo = new MediaPlayer();
            PListOne = new PlayList();
            PListTwo = new PlayList();
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
            fd.Filter = "Music files |*.mp3";
            bool? result = fd.ShowDialog();
            
            if (result == true)
            {
                PListOne.Add(new Track() { Name = fd.Title, ID = fd.FileName  });
               // MPlayerOne.Open(new Uri(fd.FileName, UriKind.Absolute));
                IsOpened = true;
            }
        }

        private void Play_1(object sender, RoutedEventArgs e)
        {
            if (IsOpened)
                Dispatcher.BeginInvoke(new ParameterizedThreadStart(Play), MPlayerOne, SelectedTrackOne);

        }

        private void Pause_1(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ParameterizedThreadStart(Pause), MPlayerOne, SelectedTrackOne);
        }

        private void Stop_1(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ParameterizedThreadStart(Stop), MPlayerOne, SelectedTrackOne);
        }

        private void Open_Track_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.DefaultExt = ".mp3";
            fd.Filter = "Music files (.mp3)|*.mp3";
            bool? result = fd.ShowDialog();

            if (result == true)
            {
                PListTwo.Add(new Track() { Name = fd.Title, ID = fd.FileName });
               // MPlayerTwo.Open(new Uri(fd.FileName, UriKind.Absolute));
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

