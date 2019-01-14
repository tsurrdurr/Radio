using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Radio.Object;
using Radio.ViewModels;
using Un4seen.Bass;

namespace Radio.Workers
{
    class Worker
    {
        public static ObservableCollection<Playlist> LoadPlaylists()
        {
            ObservableCollection<Playlist> playlists = new ObservableCollection<Playlist>();
            string site = "http://www.theradio.ml";
            string getplaysite = site + "/playlists";
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(getplaysite);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            string responce = string.Empty;
            using (StreamReader stream = new StreamReader(
                resp.GetResponseStream(), Encoding.UTF8))
            {
                responce = stream.ReadToEnd();
            }
           var result = JsonConvert.DeserializeObject<string[]>(responce);
            foreach (var elem in result)
            {
                Playlist playlist = new Playlist();
                playlist.Name = elem;
                playlist.Url = $"{site}/{elem}/";
                playlist.GifList = LoadPlaylistContent(playlist.Url+ "gifs");
                playlist.MusicList = LoadPlaylistContent(playlist.Url + "music");
                playlists.Add(playlist);
            }
            return playlists;
        }

        private static List<string> LoadPlaylistContent(string url)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            string responce = string.Empty;
            using (StreamReader stream = new StreamReader(
                resp.GetResponseStream(), Encoding.UTF8))
            {
                responce = stream.ReadToEnd();
            }
            var result = JsonConvert.DeserializeObject<List<string>>(responce);
            return result;
        }

        public static Playlist LoadImages(Playlist playlist)
        {
            DirectoryInfo directory = new DirectoryInfo("Data");
            FileInfo filebackground = new FileInfo($"Data\\{playlist.Name}-background.gif");
            FileInfo filecover = new FileInfo($"Data\\{playlist.Name}-cover.gif");
            playlist.BackgroundImagePath = $"{playlist.Url}background.gif";
            playlist.ImagePath = $"{playlist.Url}cover.gif";
            if (!directory.Exists)
            {
               Directory.CreateDirectory("Data"); 
            }
            if (!filebackground.Exists)
            {
                DownoloadFile(playlist.BackgroundImagePath, filebackground);
                playlist.BackgroundImagePath = filebackground.FullName;
            }
            else
            {
                playlist.BackgroundImagePath = filebackground.FullName;
            }
            if (!filecover.Exists)
            {
                DownoloadFile(playlist.ImagePath, filecover);
                playlist.ImagePath = filecover.FullName;
            }
            else
            {
                playlist.BackgroundImagePath = filebackground.FullName;
            }

            return playlist;
        }

        private static void DownoloadFile(string url, FileInfo path)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile(url,path.FullName);
        }

        public static void PlayAudio(string url)
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            int _stream = Bass.BASS_StreamCreateURL(url, 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);
            if (_stream != 0)
            {
                Bass.BASS_ChannelPlay(_stream, false);
            }
        }
       
    }
}
