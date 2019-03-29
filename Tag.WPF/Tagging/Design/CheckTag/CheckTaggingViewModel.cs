using Library;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
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
using Tag.Core;
using Tag.Core.Cue;
using Tag.Core.Tagging;
using Tag.Setting;

namespace Tag.WPF
{
    class CheckTaggingViewModel : INotifyPropertyChanged
    {
        private string _artist;
        private List<TagLib.IPicture> _coverImage;
        private string _album;
        private string _year;
        private string _genre;
        private string _comment;
        private string _coverInfo;

        public string CoverInfo { get => _coverInfo; set { _coverInfo = value; OnPropertyChagend(); } }

        public string Artist { get => _artist; set { _artist = value; OnPropertyChagend(); } }
        public string Album { get => _album; set { _album = value; OnPropertyChagend(); } }
        public string Year { get => _year; set { _year = value; OnPropertyChagend(); } }
        public string Genre { get => _genre; set { _genre = value; OnPropertyChagend(); } }
        public string Comment { get => _comment; set { _comment = value; OnPropertyChagend(); } }


        public bool CheckButtonEnable { get => _checkButtonEnable; set { _checkButtonEnable = value; OnPropertyChagend(); } }

        public Visibility Visible
        {
            get => _visible;
            set
            {
                if (value == Visibility.Visible) { CheckButtonEnable = false; }
                else { CheckButtonEnable = true; }
                _visible = value; OnPropertyChagend();
            }
        }

        public List<TagLib.IPicture> CoverImage { get => _coverImage; set { _coverImage = value; OnPropertyChagend(); } }

        public ObservableCollection<CheckTagInfoModel> Information { get; set; }
        public ObservableCollection<CheckBrainzModel> BrainzInfo { get; set; }
        public ObservableCollection<CheckUserModel> UserInfo { get; set; }

        public bool[] LangMode { get; set; } = new bool[3] { false, false, false };
        public bool[] CheckLang { get; set; } = new bool[3] { false, false, false };

        Dictionary<string, List<TagInfo>> SetTagInfoList = new Dictionary<string, List<TagInfo>>();


        private List<TagInfo> BrainzTag = new List<TagInfo>();

        private ObservableCollection<TaggingModel> data;
        private Visibility _visible;
        private bool _checkButtonEnable;

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
                    temp.TagInfo.Album = Album ?? String.Empty;
                    temp.TagInfo.AlbumArtist = BrainzTag[i].AlbumArtist ?? new List<string>();
                    temp.TagInfo.Artist = Artist != null ? Artist.Split(',').ToList() : new List<string>();
                    temp.TagInfo.Barcode = BrainzTag[i].Barcode;
                    temp.TagInfo.Comment = Comment;
                    temp.TagInfo.Composer = BrainzTag[i]?.Composer;
                    temp.TagInfo.Country = BrainzTag[i]?.Country;
                    temp.TagInfo.DiscNum = BrainzTag[i]?.DiscNum;
                    temp.TagInfo.Format = BrainzTag[i]?.Format;
                    temp.TagInfo.Genre = Genre?.Split(',').ToList();
                    temp.TagInfo.Identifier = BrainzTag[i]?.Identifier;
                    temp.TagInfo.Image = CoverImage?.ToArray().ToList();
                    temp.TagInfo.Lang = BrainzTag[i]?.Lang;
                    temp.TagInfo.Publisher = BrainzTag[i]?.Publisher;
                    temp.TagInfo.TagType = BrainzTag[i].TagType;
                    temp.TagInfo.Title = BrainzTag[i]?.Title;
                    temp.TagInfo.Track.Clear();
                    temp.TagInfo.Track.Add(BrainzTag[i].Track.Count != 0 ? BrainzTag[i].Track[0] : 1);
                    temp.TagInfo.Year = Year;
                }
                data.RemoveAt(index);
                data.Insert(index, temp);
            }
           
        }

        public void SetTagChange(string Tag)
        {
            SetValue(SetTagInfoList[Tag]);
        }

        public async void SetValue(List<TagInfo> tag)
        {
            if(tag == null)
            {
                DialogHost.CloseDialogCommand.Execute(false, null);
                await Task.Delay(500);
                return;
            }
            BrainzInfo.Clear();
            Information.Clear();

            BrainzTag = tag;

            Artist = string.Join(", ", tag[0]?.AlbumArtist);
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
                Name = Global.Language.Format,
                Value = string.Join(", ", tag[0].Format)
            });
            Information.Add(new CheckTagInfoModel
            {
                Name = Setting.Global.Language.Year,
                Value = Year
            });
            Information.Add(new CheckTagInfoModel
            {
                Name = Global.Language.Publisher,
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
                Name = Global.Language.Barcode,
                Value = tag[0].Barcode
            });

            CoverImage = tag[0].Image;
            if (CoverImage == null || CoverImage.Count == 0)
            {
                CoverInfo = Global.Language.ImageNone;
            }
            else
            {
                var stream = new MemoryStream(CoverImage[0].Data.Data);
                var image = new Bitmap(System.Drawing.Image.FromStream(stream));
                var data = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                     image.GetHbitmap(),
                     IntPtr.Zero,
                     Int32Rect.Empty,
                     BitmapSizeOptions.FromEmptyOptions());

                CoverInfo = $"{image.Width} x {image.Height}, {CapacityManage.Change(new System.Numerics.BigInteger(stream.Length))}";
            }

            tag.Sort((a, b) =>
            {
                var disc = (int)a.Track[1] - (int)b.Track[1];
                if (disc == 0)
                {
                    return (int)a.Track[0] - (int)b.Track[0];
                }
                else
                {
                    return disc;
                }
            });

            foreach (var taginfo in tag)
            {
                int index = (int)taginfo.Track[1];
                bool check = false;
                for (int i = tag.IndexOf(taginfo); i < tag.Count; i++)
                {
                    if (index != tag[i].Track[1])
                    {
                        index = (int)tag[i - 1].Track[0];
                        check = true;
                        break;
                    }
                }
                if (check == false)
                {
                    index = (int)tag[tag.Count - 1].Track[0];
                }
                BrainzInfo.Add(new CheckBrainzModel
                {
                    Track = $"{taginfo.Track[0]} / {index}",
                    Title = taginfo.Title,
                    DiscNo = $"{taginfo.Track[1]} / {tag[tag.Count - 1].Track[1]}",
                });
            }
            Visible = Visibility.Hidden;
        }

        public void SetTagValue(Dictionary<string, List<TagInfo>> tags)
        {
            SetTagInfoList = tags;

            List<TagInfo> tag = null;
            if (tags.ContainsKey("MusicBrainz") == true)
            {
                tag = tags["MusicBrainz"];
                for (int i = 0; i < 3; i++)
                {
                    LangMode[i] = false;
                }
            }
            else
            {
                if (tags.ContainsKey(VGMLang.Japanese))
                {
                    LangMode[2] = true;

                    CheckLang[0] = false;
                    CheckLang[1] = false;
                    CheckLang[2] = true;
                    tag = tags[VGMLang.Japanese];
                }
                if (tags.ContainsKey(VGMLang.Romjai))
                {
                    LangMode[1] = true;

                    CheckLang[0] = false;
                    CheckLang[1] = true;
                    CheckLang[2] = false;
                    tag = tags[VGMLang.Romjai];
                }
                if (tags.ContainsKey(VGMLang.English))
                {
                    LangMode[0] = true;

                    CheckLang[0] = true;
                    CheckLang[1] = false;
                    CheckLang[2] = false;
                    tag = tags[VGMLang.English];
                }
            }
            OnPropertyChagend(nameof(LangMode));
            OnPropertyChagend(nameof(CheckLang));
            SetValue(tag);
            
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
                    Length = $"{string.Format("{0}", (int)userinfo.WaveFormat.Length / 60)}{Global.Language.CueMin} : {string.Format("{0}", ((int)userinfo.WaveFormat.Length % 60))}{Global.Language.CueSecond}",
                    Title = userinfo.TagInfo.Path != String.Empty ? Path.GetFileName(userinfo.TagInfo.Path)
                                                        : userinfo.TagInfo.Title,
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
    
        public void RemoveGetTag(List<CheckBrainzModel> list)
        {
            foreach (var value in list)
            {
                BrainzInfo.Remove(value as CheckBrainzModel);
            }
        }
        public void ChangeText(string Name, string Value)
        {
            this.GetType().GetProperty(Name).SetValue(this, Value);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChagend([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}
