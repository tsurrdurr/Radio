using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Radio.Object
{
    class Playlist
    {
        private string name;
        private string url;
        private string imagePath;
        private string backgroundImagePath;
        private List<string> musicList;
        private List<string> gifList;
       

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                OnPropertyChanged("Url");
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }
        public string BackgroundImagePath
        {
            get { return backgroundImagePath; }
            set
            {
                backgroundImagePath = value;
                OnPropertyChanged("BackgroundImagePath");
            }
        }
        public List<string> MusicList
        {
            get { return musicList; }
            set
            {
                musicList = value;
                OnPropertyChanged("MusicList");
            }
        }
        public List<string> GifList
        {
            get { return gifList; }
            set
            {
                gifList = value;
                OnPropertyChanged("GifList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

