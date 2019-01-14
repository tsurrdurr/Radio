using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace Radio.Workers
{
    public class MusicPlayer
    {
        private static int _stream;
        private static string _played;

        public void StartPlay(string url)
        {
            Bass.BASS_ChannelStop(_stream);
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            _stream = Bass.BASS_StreamCreateURL(url, 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);
            if (_stream != 0)
            {
                Bass.BASS_ChannelPlay(_stream, false);
            }
            
        }

        public void Play()
        {
            if (_stream != 0)
            {
                Bass.BASS_ChannelPlay(_stream, false);
            }
        }

        public void Pause()
        {
            if (_stream != 0)
            {
                Bass.BASS_ChannelPause(_stream);
            }
        }

        public void Stop()
        {
            if (_stream != 0)
            {
                Bass.BASS_ChannelStop(_stream);
            }
        }

        public void PlayOrPause(string url)
        {
            if (url!= _played)
            {
                _played = url;
                StartPlay(url);
                return;
            }
            switch (Bass.BASS_ChannelIsActive(_stream))
            {
                case BASSActive.BASS_ACTIVE_PLAYING:
                    Pause();
                    break;
                case BASSActive.BASS_ACTIVE_PAUSED:
                    Play();
                    break;
                case BASSActive.BASS_ACTIVE_STOPPED:
                    _played = url;
                    StartPlay(url);
                    break;
            }
        }

        public void ChangeValue(float value)
        {
            Bass.BASS_ChannelSetAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, value);
        }

    }
}
