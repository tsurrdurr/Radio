using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Radio.Object;
using Radio.Workers;

namespace Radio.ViewModels
{
    public class PlaylistsViewModel : ViewModel
    {
        public PlaylistsViewModel()
        {
            var downloader = new PlaylistDownloader(); ;
            Playlists = downloader.LoadPlaylists();
            SelectedPlaylist = Playlists.FirstOrDefault();
            Action calledMethod = LoadIconsFromUIThread;
            Application.Current.Dispatcher.BeginInvoke(calledMethod);
        }

        private Playlist _selectedPlaylist;

        public ObservableCollection<Playlist> Playlists { get; set; }

        public Playlist SelectedPlaylist
        {
            get { return _selectedPlaylist; }
            set
            {
                _selectedPlaylist = value;
                OnPropertyChanged(nameof(SelectedPlaylist));
            }
        }

        private void LoadIconsFromUIThread()
        {
            var downloader = new PlaylistDownloader();
            for (int i = 0; i < Playlists.Count; i++)
            {
                Playlists[i] = downloader.LoadIcon(Playlists[i]);
            }
        }
    }

}
