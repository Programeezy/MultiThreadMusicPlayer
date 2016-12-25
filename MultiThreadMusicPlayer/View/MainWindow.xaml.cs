using Microsoft.Win32;
using MultiThreadMusicPlayer.Media;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

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
     
        public MainWindow()
        {
            InitializeComponent();
           
            PListOne = new PlayList();
            PListTwo = new PlayList();
            SelectedTrackOne = new Track();
            SelectedTrackTwo = new Track();
            PlayList_1.ItemsSource = PListOne;
            PlayList_2.ItemsSource = PListTwo;
        }

        private void timer_Tick_1(object sender, EventArgs e)
        {
            if(SelectedTrackOne.NaturalDuration.HasTimeSpan)
            {
                Thread_1_Slider.Minimum = 0;
                Thread_1_Slider.Maximum = SelectedTrackOne.NaturalDuration.TimeSpan.TotalSeconds;
                Thread_1_Slider.Value = SelectedTrackOne.Position.TotalSeconds;
            }
        }

        private void timer_Tick_2(object sender, EventArgs e)
        {

            if (SelectedTrackTwo.NaturalDuration.HasTimeSpan)
            {
                Thread_2_Slider.Minimum = 0;
                Thread_2_Slider.Maximum = SelectedTrackTwo.NaturalDuration.TimeSpan.TotalSeconds;
                Thread_2_Slider.Value = SelectedTrackTwo.Position.TotalSeconds;
            }
        }

        private void Play(Track selectedTrack, Slider timeSlider)
        {

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);

            if (timeSlider.Equals(Thread_1_Slider))
                timer.Tick += timer_Tick_1;

            else timer.Tick += timer_Tick_2;
           
      
            if (selectedTrack != null)
            {
                if(selectedTrack.Position.Seconds == 0)
                    selectedTrack.Open(new Uri(selectedTrack.ID, UriKind.Absolute));

                selectedTrack.Play();    
                timer.Start();

                
            }
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
            SelectedTrackOne = (Track)PlayList_1.SelectedItem;
            Dispatcher.BeginInvoke(new Action<Track, Slider>(Play), PlayList_1.SelectedItem, Thread_1_Slider);

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

            }
        }

        private void Play_2(object sender, RoutedEventArgs e)
        {
            SelectedTrackTwo = (Track)PlayList_2.SelectedItem;
            Dispatcher.BeginInvoke(new Action<Track, Slider>(Play), PlayList_2.SelectedItem, Thread_2_Slider);

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

