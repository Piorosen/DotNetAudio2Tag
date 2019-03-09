using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Cue;
using Tag.Core.Tagging;

namespace Tag.WPF
{
    class AutoModeModel : TrackInfo, INotifyPropertyChanged
    {
        private string _path;

        public string Path { get => _path; set { _path = value; OnPropertyChanged(); } }
        public TagInfo Tag { get; set; }
        public WaveFormat Format { get; set; }

        public AutoModeModel(string file)
        {
            Tag = new TagInfo(TagLib.File.Create(file).Tag, file);
            var audio = new AudioFileReader(file);
            Format = audio.WaveFormat;
            DurationMS = audio.TotalTime.Seconds;
            Title = Tag.Title;

            Artist = string.Join("; ", Tag.Artist);
            Composer = string.Join("; ", Tag.Composer);
            Track = Tag.Track.Count != 0 ? (int)Tag.Track[0] : 1;

            Path = file;
        }

        public AutoModeModel(TrackInfo info)
        {
            this.Artist = info.Artist;
            this.Composer = info.Composer;
            this.DurationMS = info.DurationMS;
            this.StartPosition = info.StartPosition;
            this.Title = info.Title;
            this.Track = info.Track;
            this.TimeOffSet = info.TimeOffSet;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}
