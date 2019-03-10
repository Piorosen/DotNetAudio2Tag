using ATL.CatalogDataReaders;
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
    public class AutoModeModel : TrackInfo, INotifyPropertyChanged
    {
        private string _path;

        public string Path { get => _path; set { _path = value; OnPropertyChanged(); } }
        public TagInfo Tag { get; set; }
        public WaveFormat Format { get; set; }
        public AudioType Type { get; private set; }
        public AutoModeModel(string file)
        {
            using (var tag = TagLib.File.Create(file))
            {
                Tag = new TagInfo(tag.Tag, file);
            }

            using (var audio = new AudioFileReader(file))
            {
                Format = audio.WaveFormat;
                DurationMS = audio.TotalTime.Seconds;
            }
            Title = Tag.Title;

            Artist = string.Join("; ", Tag.Artist);
            Composer = string.Join("; ", Tag.Composer);
            Track = Tag.Track.Count != 0 ? (int)Tag.Track[0] : 1;

            Type = System.IO.Path.GetExtension(file) == ".wav" ? AudioType.WAV : AudioType.FLAC;

            Path = file;
        }

        public AutoModeModel(TrackInfo info, string file)
        {
            this.Artist = info.Artist;
            this.Composer = info.Composer;
            this.DurationMS = info.DurationMS;
            this.StartPosition = info.StartPosition;
            this.Title = info.Title;
            this.Track = info.Track;
            this.TimeOffSet = info.TimeOffSet;
            Format = new WaveFormat(0, 0, 2);
            Tag = new TagInfo
            {
                Title = Title,
            };
            Tag.Artist.Add(info.Artist);
            Tag.Composer.Add(info.Artist);
            Tag.Track.Add((uint)info.Track);
            Tag.Album = info.Album;

            Type = System.IO.Path.GetExtension(file) == ".wav" ? AudioType.WAV : AudioType.FLAC;

            Path = file;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}
