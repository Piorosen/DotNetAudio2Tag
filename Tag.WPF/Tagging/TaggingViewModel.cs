using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Tag.Core.Tagging;

namespace Tag.WPF
{
    public class TaggingViewModel : INotifyPropertyChanged
    {
        public string Title => Setting.Global.Language.Title;
        public string Artist => Setting.Global.Language.Artist;
        public string Album => Setting.Global.Language.Album;
        public string Comment => Setting.Global.Language.Comment;
        public string Genre => Setting.Global.Language.Genre;
        public string Year => Setting.Global.Language.Year;
        public string Track => Setting.Global.Language.Track;
        public string AlbumArtist => Setting.Global.Language.AlbumArtist;
        public string Composer => Setting.Global.Language.Composer;
        public string DiscNum => Setting.Global.Language.DiscNum;
        public string Publisher => "제작사";
        public string AlbumArt => Setting.Global.Language.AlbumArt;

        public string FileName => Setting.Global.Language.FileName;
        public string Directory => Setting.Global.Language.Path;
        public string TagType => Setting.Global.Language.Tag;
        public string Bitrate => Setting.Global.Language.Bitrate;
        public string Length => Setting.Global.Language.Length;

        AudioTagging audioTagging;
        private TaggingModel _selectItem;

        public ObservableCollection<TaggingModel> Items { get; set; }
        public TaggingModel SelectItem { get => _selectItem; set { _selectItem = value; OnPropertyChanged(); } }

        
        public void AddModel(string filePath)
        {
            audioTagging.AddFile(filePath);

            using (AudioFileReader reader = new AudioFileReader(filePath))
            {
                Items.Add(new TaggingModel
                {
                    TagInfo = audioTagging.List()[audioTagging.List().Count - 1],
                    WaveFormat = new WaveFormatModel
                    {
                        Bitrate = reader.WaveFormat.SampleRate,
                        Channel = reader.WaveFormat.Channels,
                        Length = reader.TotalTime.TotalSeconds
                    }
                });
            }
        }

        public void RemoveModel(int index)
        {
            Items.RemoveAt(index);
        }
        public void SelectModel(int index)
        {
            SelectItem = Items[index];
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }


        public TaggingViewModel()
        {
            Items = new ObservableCollection<TaggingModel>();

            audioTagging = new AudioTagging();

        }
    }
}
