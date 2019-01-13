using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Radio.Object;
using Radio.Workers;

namespace Radio.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {


        public ObservableCollection<Playlist> Playlists { get; set; }


        private Playlist selectedPlaylist;
        public Playlist SelectedPlaylist
        {
            get { return selectedPlaylist; }
            set
            {
                selectedPlaylist = value;
                OnPropertyChanged("SelectedPlaylist");
            }
        }

        #region Commands
        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                       (addCommand = new RelayCommand(obj =>
                       {
                           
                          
                       }));
            }
        }
        #endregion
        public MainViewModel()
        {
            Playlists = Worker.LoadPlaylists();
            SelectedPlaylist = Playlists.FirstOrDefault();
            Action calledMethod = UpdateFromUIThread;
            Application.Current.Dispatcher.BeginInvoke(calledMethod);
        }

        private void UpdateFromUIThread()
        {
            for (int i = 0; i < Playlists.Count; i++)
            {
                Playlists[i] = Worker.LoadImages(Playlists[i]);
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
