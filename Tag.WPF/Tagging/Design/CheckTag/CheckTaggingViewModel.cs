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

        public ObservableCollection<CheckTagInfoModel> Information { get; set; }
        public ObservableCollection<CheckBrainzModel> BrainzInfo { get; set; }
        public ObservableCollection<CheckUserModel> UserInfo { get; set; }

        public CheckTaggingViewModel()
        {
            Information = new ObservableCollection<CheckTagInfoModel>();
            BrainzInfo = new ObservableCollection<CheckBrainzModel>();
            UserInfo = new ObservableCollection<CheckUserModel>();
        }


        public void SetValue(List<TagInfo> tag, List<TrackInfo> User)
        {
            Artst = string.Join(", ", tag[0].AlbumArtist);
            Album = tag[0].Album;
            Year = tag[0].Year;
            Genre = string.Join(", ", tag[0].Genre);
            Comment = tag[0].Comment;

            Information.Add(new CheckTagInfoModel
            {
                Name = tag[0].AlbumArtist.GetType().Name,
                Value = string.Join("; ", tag[0].AlbumArtist)
            });

            Information.Add(new CheckTagInfoModel
            {
                Name = tag[0].Format.GetType().Name,
                Value = string.Join(", ", tag[0].Format)
            });
            Information.Add(new CheckTagInfoModel
            {
                Name = tag[0].Year.GetType().Name,
                Value = Year
            });

            Information.Add(new CheckTagInfoModel
            {
                Name = tag[0].Publisher.GetType().Name,
                Value = string.Join(";", tag[0].Publisher)
            });
            Information.Add(new CheckTagInfoModel
            {
                Name = tag[0].Track.GetType().Name,
                Value = tag[0].Track.Count.ToString()
            });
            Information.Add(new CheckTagInfoModel
            {
                Name = tag[0].DiscNum.GetType().Name,
                Value = tag[0].DiscNum
            });
            Information.Add(new CheckTagInfoModel
            {
                Name = tag[0].Barcode.GetType().Name,
                Value = tag[0].Barcode
            });

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
                BrainzInfo.Add(new CheckBrainzModel
                {
                    Track = taginfo.Track[0],
                    Title = taginfo.Title
                });
            }
            foreach (var userinfo in User)
            {
                UserInfo.Add(new CheckUserModel
                {
                    Length = $"{string.Format("{00}", (int)userinfo.DurationMS / 60)}:{string.Format("{00}", userinfo.DurationMS % 60)}",
                    Title = userinfo.Title,
                    Track = userinfo.Track
                });
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChagend([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}
