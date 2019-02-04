using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Tagging;

namespace Tag.WPF
{
    public class TaggingViewModel
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
        public string AlbumArt => Setting.Global.Language.AlbumArt;

        public string FileName => Setting.Global.Language.FileName;
        public string Directory => Setting.Global.Language.Path;
        public string TagType => Setting.Global.Language.Tag;
        public string Codec => Setting.Global.Language.Codec;
        public string Length => Setting.Global.Language.Length;
        public string Fixed => Setting.Global.Language.Fixed;


        public ObservableCollection<TaggingModel> Items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public TaggingViewModel()
        {
            Items = new ObservableCollection<TaggingModel>
            {
                new TaggingModel { N = "123" }
            };
        }
    }
}
