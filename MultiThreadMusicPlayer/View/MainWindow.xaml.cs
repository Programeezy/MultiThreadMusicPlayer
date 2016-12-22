using Microsoft.Win32;
using MultiThreadMusicPlayer.Media;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MultiThreadMusicPlayer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayList PListOne
        {
            get; set;
        }
        PlayList PListTwo;
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
            PlayList_1.ItemsSource = PListOne;
            PlayList_2.ItemsSource = PListTwo;
        }

        private void Play(Track selectedTrack)
        {
            if (selectedTrack != null)
                selectedTrack.Open(new Uri(selectedTrack.ID, UriKind.Absolute));
            selectedTrack.Play();
        }

        private void Pause(Track selectedTrack)
        {
            selectedTrack.Pause();

        }

        private void Stop(Track selectedTrack)
        {
            selectedTrack.Stop();
        }

        private void Open_Track_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.DefaultExt = ".mp3";
            fd.Filter = "Music files |*.mp3";
            bool? result = fd.ShowDialog();
            
            if (result == true)
            {
                PListOne.Add(new Track { Name = fd.FileName, ID = fd.FileName, Rating = 1 });
                PlayList_1.ItemsSource = PListOne;
            }
        }

        private void Play_1(object sender, RoutedEventArgs e)
        {
                Dispatcher.BeginInvoke(new Action<Track>(Play), PlayList_1.SelectedItem);

        }

        private void Pause_1(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action<Track>(Pause), PlayList_1.SelectedItem);
        }

        private void Stop_1(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action<Track>(Stop), PlayList_1.SelectedItem);
        }

        private void Open_Track_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.DefaultExt = ".mp3";
            fd.Filter = "Music files (.mp3)|*.mp3";
            bool? result = fd.ShowDialog();

            if (result == true)
            {
                PListTwo.Add(new Track() { Name = fd.FileName, ID = fd.FileName });
               
                IsOpened = true;
            }
        }

        private void Play_2(object sender, RoutedEventArgs e)
        {
            if (IsOpened)
                Dispatcher.BeginInvoke(new Action<Track>(Play), PlayList_2.SelectedItem);

        }

        private void Pause_2(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action<Track>(Pause), PlayList_2.SelectedItem);
        }

        private void Stop_2(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action<Track>(Stop), PlayList_2.SelectedItem);
        }


    }
}

