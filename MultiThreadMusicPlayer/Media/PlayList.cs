using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadMusicPlayer.Media
{
    public class PlayList : IEnumerable, INotifyCollectionChanged
    {
        private Track[] _Tracks = new Track[0];

        private string _ID;
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private TimeSpan _Length;
        public TimeSpan Length
        {
            get { return _Length; }
            set { _Length = value; }
        }

        private double _Rating;

        public double Rating
        {
            get { return _Rating; }
        }

        private int _Count;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Count
        {
            get { return _Count; }
        }

        public PlayList()
        {

        }

        public PlayList(params Track[] tracks)
        {
            foreach (var track in tracks)
                Add(track);
        }

        public void Add(Track track)
        {
            if (_Count == _Tracks.Length)
                Array.Resize(ref _Tracks, _Tracks.Length + 8);

            _Tracks[_Count] = track;
            _Count++;
            OnCollectionChanged();
            RefreshRating();

        }

        public bool Remove(Track track)
        {
            for(int i = 0; i < _Count; i++)
            {
                if(_Tracks[i].Equals(track))
                {
                    for (int j = i; j < _Count; j++)
                        _Tracks[j] = _Tracks[j + 1];
                    _Count--;
                    RefreshRating();
                    return true;
                }
            }

            return false;
        }

        private void RefreshRating()
        {
            double rate = 0 ;

            foreach(Track track in this)
            {
                rate += track.Rating;
            }

            _Rating = rate / _Count;
        }

        private void RefreshLength()
        {
            TimeSpan length = new TimeSpan();

            foreach(Track track in this)
            {
                length += track.Length;
            }

            Length = length;

        }

        public void OnCollectionChanged()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _Tracks[i];      
        }
    }
}
