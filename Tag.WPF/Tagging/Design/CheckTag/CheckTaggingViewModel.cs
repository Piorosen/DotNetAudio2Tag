using Library;
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

        public Visibility Visible { get => _visible; set { _visible = value; OnPropertyChagend(); } }

        public BitmapSource CoverImage { get => _coverImage; set { _coverImage = value; OnPropertyChagend(); } }

        public ObservableCollection<CheckTagInfoModel> Information { get; set; }
        public ObservableCollection<CheckBrainzModel> BrainzInfo { get; set; }
        public ObservableCollection<CheckUserModel> UserInfo { get; set; }

        private List<TagInfo> BrainzTag;
        private ObservableCollection<TaggingModel> data;
        private Visibility _visible;

        public CheckTaggingViewModel()
        {
            Information = new ObservableCollection<CheckTagInfoModel>();
            BrainzInfo = new ObservableCollection<CheckBrainzModel>();
            UserInfo = new ObservableCollection<CheckUserModel>();
        }

        public void SaveTag()
        {
            int loop = BrainzInfo.Count > UserInfo.Count ? UserInfo.Count : BrainzInfo.Count;

            for (int i = 0; i < loop; i++)
            {
                int index = data.IndexOf(data[UserInfo[i].Id]);

                var temp = data[index];
                {
                    temp.TagInfo.Album = BrainzTag[i].Album;
                    temp.TagInfo.AlbumArtist = BrainzTag[i].AlbumArtist;
                    temp.TagInfo.Artist = BrainzTag[i].Artist;
                    temp.TagInfo.Barcode = BrainzTag[i].Barcode;
                    temp.TagInfo.Comment = BrainzTag[i].Comment;
                    temp.TagInfo.Composer = BrainzTag[i].Composer;
                    temp.TagInfo.Country = BrainzTag[i].Country;
                    temp.TagInfo.DiscNum = BrainzTag[i].DiscNum;
                    temp.TagInfo.Format = BrainzTag[i].Format;
                    temp.TagInfo.Genre = BrainzTag[i].Genre;
                    temp.TagInfo.Identifier = BrainzTag[i].Identifier;
                    temp.TagInfo.Image = BrainzTag[i].Image;
                    temp.TagInfo.Lang = BrainzTag[i].Lang;
                    temp.TagInfo.Publisher = BrainzTag[i].Publisher;
                    temp.TagInfo.TagType = BrainzTag[i].TagType;
                    temp.TagInfo.Title = BrainzTag[i].Title;
                    temp.TagInfo.Track = BrainzTag[i].Track;
                    temp.TagInfo.Year = BrainzTag[i].Year;
                }
                data.RemoveAt(index);
                data.Insert(index, temp);
            }
        }

        public void SetTagValue(List<TagInfo> tag)
        {
            BrainzTag = tag;

            Artst = string.Join(", ", tag[0]?.AlbumArtist);
            Album = tag[0]?.Album;
            Year = tag[0]?.Year;
            Genre = string.Join(", ", tag[0]?.Genre);
            Comment = tag[0]?.Comment;

            Information.Add(new CheckTagInfoModel
            {
                Name = Setting.Global.Language.AlbumArtist,
                Value = string.Join(", ", tag[0]?.AlbumArtist)
            });
            Information.Add(new CheckTagInfoModel
            {
                Name = "포맷",
                Value = string.Join(", ", tag[0].Format)
            });
            Information.Add(new CheckTagInfoModel
            {
                Name = Setting.Global.Language.Year,
                Value = Year
            });
            Information.Add(new CheckTagInfoModel
            {
                Name = "배급사",
                Value = string.Join(", ", tag[0].Publisher)
            });
            Information.Add(new CheckTagInfoModel
            {
                Name = Setting.Global.Language.Track,
                Value = tag[0].Track.Count.ToString()
            });
            Information.Add(new CheckTagInfoModel
            {
                Name = Setting.Global.Language.DiscNum,
                Value = tag[0].DiscNum
            });
            Information.Add(new CheckTagInfoModel
            {
                Name = "바코드",
                Value = tag[0].Barcode
            });

            var tagTemp = tag[0].Image as List<TagLib.IPicture>;

            if (tagTemp.Count > 0)
            {
                var t = new MemoryStream(tagTemp[0].Data.Data);
                
                CoverImage = Imaging.CreateBitmapSourceFromHBitmap(
                     new Bitmap(Image.FromStream(new MemoryStream(tagTemp[0].Data.Data))).GetHbitmap(),
                     IntPtr.Zero,
                     Int32Rect.Empty,
                     BitmapSizeOptions.FromEmptyOptions());
                
                CoverInfo = $"{CoverImage.PixelWidth}x{CoverImage.PixelHeight}, {CapacityManage.Change(new System.Numerics.BigInteger(t.Length))}";
            }

            foreach (var taginfo in tag)
            {
                BrainzInfo.Add(new CheckBrainzModel
                {
                    Track = taginfo.Track[0],
                    Title = taginfo.Title
                });
            }
            Visible = Visibility.Hidden;
        }

        public void SetValue(ObservableCollection<TaggingModel> User)
        {
            data = User;
            Visible = Visibility.Visible;
            int i = 0;
            foreach (var userinfo in User)
            {
                int tracknum = userinfo.TagInfo.Track.Count == 0 ? 0 : (int)userinfo.TagInfo.Track[0];
                UserInfo.Add(new CheckUserModel
                {
                    Length = $"{string.Format("{00}", (int)userinfo.WaveFormat.Length / 60)}:{string.Format("{00}", userinfo.WaveFormat.Length % 60)}",
                    Title = userinfo.TagInfo.Title,
                    Track = tracknum,
                    Id = i
                });
                i++;
            }
        }

        public void UpClick(int index)
        {
            var now = UserInfo[index];

            UserInfo.RemoveAt(index);
            UserInfo.Insert(index - 1, now);
        }
        public void DownClick(int index)
        {
            var now = UserInfo[index];

            UserInfo.RemoveAt(index);
            UserInfo.Insert(index + 1, now);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChagend([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}
