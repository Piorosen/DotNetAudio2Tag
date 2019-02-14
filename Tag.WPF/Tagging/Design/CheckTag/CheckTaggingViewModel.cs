using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tag.Core.Cue;
using Tag.Core.Tagging;

namespace Tag.WPF
{
    class CheckTaggingViewModel : INotifyPropertyChanged
    {
        private string _artst;
        private BitmapSource _coverImage;
        private string _album;
        private string _year;
        private string _genre;
        private string _comment;
        private string _coverInfo;


        public string CoverInfo { get => _coverInfo; set { _coverInfo = value; OnPropertyChagend(); } }

        public string Artst { get => _artst; set { _artst = value; OnPropertyChagend(); } }
        public string Album { get => _album; set { _album = value; OnPropertyChagend(); } }
        public string Year { get => _year; set { _year = value; OnPropertyChagend(); } }
        public string Genre { get => _genre; set { _genre = value; OnPropertyChagend(); } }
        public string Comment { get => _comment; set { _comment = value; OnPropertyChagend(); } }

        public BitmapSource CoverImage { get => _coverImage; set { _coverImage = value; OnPropertyChagend(); } }

        public ObservableCollection<(string Name, string Value)> Information { get; set; }
        public ObservableCollection<(uint Track, string Title)> BrainzInfo { get; set; }
        public ObservableCollection<(string Length, string Title, int Track)> UserInfo { get; set; }

        public CheckTaggingViewModel()
        {
            Information = new ObservableCollection<(string Name, string Value)>();
            BrainzInfo = new ObservableCollection<(uint Track, string Title)>();
            UserInfo = new ObservableCollection<(string Length, string Title, int Track)>();
        }


        public void SetValue(List<TagInfo> tag, List<TrackInfo> User)
        {
            Information.Add((tag[0].AlbumArtist.GetType().Name, string.Join("; ", tag[0].AlbumArtist)));
            Information.Add((tag[0].Format.GetType().Name, string.Join(", ", tag[0].Format)));
            Information.Add((tag[0].Year.GetType().Name, Year));
            Information.Add((tag[0].Publisher.GetType().Name, string.Join(";", tag[0].Publisher)));
            Information.Add((tag[0].Track.GetType().Name, tag[0].Track.Count.ToString()));
            Information.Add((tag[0].DiscNum.GetType().Name, tag[0].DiscNum));
            Information.Add((tag[0].Barcode.GetType().Name, tag[0].Barcode));

            var tagTemp = tag[0].Image as List<TagLib.IPicture>;
            
            if (tagTemp.Count > 0)
            {
                CoverImage = Imaging.CreateBitmapSourceFromHBitmap(
                     new Bitmap(Image.FromStream(new MemoryStream(tagTemp[0].Data.Data))).GetHbitmap(),
                     IntPtr.Zero,
                     Int32Rect.Empty,
                     BitmapSizeOptions.FromEmptyOptions());
                CoverInfo = $"{CoverImage.PixelWidth}x{CoverImage.PixelHeight}";
            }

            foreach (var taginfo in tag)
            {
                BrainzInfo.Add((taginfo.Track[0], taginfo.Title));
            }
            foreach (var userinfo in User)
            {
                UserInfo.Add(($"{string.Format("00", userinfo.DurationMS / 60)}" +
                    $":{string.Format("00", userinfo.DurationMS % 60)}"
                    , userinfo.Title, userinfo.Track));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChagend([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}
