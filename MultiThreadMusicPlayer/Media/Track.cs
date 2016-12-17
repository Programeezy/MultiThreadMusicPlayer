using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadMusicPlayer.Media
{
    public class Track
    {
        public enum Genres { Rock, Rap, Classic, Electro};

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

        private string _Author;

        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }

        private Genres _Genre;

        public Genres Genre
        {
            get { return _Genre; }
            set { _Genre = value; }
        }

        private int _Rating;

        public int Rating
        {
            get
            {
                return _Rating;
            }

            set
            {
                if (value > 10 || value < 0)
                {
                    throw new IndexOutOfRangeException("Rating is out of range");
                }

                _Rating = value;
            }
        }
    }
}
