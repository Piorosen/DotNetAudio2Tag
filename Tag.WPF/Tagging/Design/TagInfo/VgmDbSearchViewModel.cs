using Library;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Tag.Core.Cue;
using Tag.Core.Tagging;
using Tag.Core.Tagging.Library;
using Tag.Setting;

namespace Tag.WPF
{
    class VgmDbSearchViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<VgmDbInfo> Items { get; private set; }
        public ImageSource ImageSource { get => _image; private set { _image = value; OnPropertyChanged(); } }
        public string ImageInfo { get => _imageInfo; set { _imageInfo = value; OnPropertyChanged(); } }
        public Visibility Visible { get => _visible; set { _visible = value; OnPropertyChanged(); } }
        public Visibility Searching { get => _searching; set { _searching = value; OnPropertyChanged(); } }


        ObservableCollection<TaggingModel> user;


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public VgmDbSearchViewModel(ObservableCollection<TaggingModel> users)
        {
            Items = new ObservableCollection<VgmDbInfo>();
            search = new MusicDb();
            this.user = users;
            Visible = Visibility.Hidden;
        }

        MusicDb search;
        private ImageSource _image;

        public async void SearchAlbum(TagInfo SearchInfo, UserControl c)
        {
            Items.Clear();
            Searching = Visibility.Visible;
            await Task.Run(() =>
            {
                var searchresult = search.GetAlbumInfo(SearchInfo);
                c.Dispatcher.Invoke(() =>
                {
                    foreach (var value in searchresult)
                    {
                        Items.Add(value);
                    }
                    Searching = Visibility.Hidden;
                });
            });
        }


        async void OpenDialog(object sender, DialogOpenedEventArgs e)
        {
            List<TagInfo> searchResult = new List<TagInfo>();
            await Task.Run(() =>
            {
                searchResult = search.GetTrackInfo(Items[index]);
            });
            view.SetTagValue(searchResult);
        }

        int index = 0;
        CheckTagging view;

        public async void Yes_Click(int index)
        {
            this.index = index;

            DialogHost.CloseDialogCommand.Execute(true, null);

            view = new CheckTagging(user)
            {
                Height = 500,
                Width = 1100
            };
            await DialogHost.Show(view, Global.DialogIdentifier.CheckTagInfo, OpenDialog);
        }



        int TaskIdentified = 0;
        private Visibility _visible = Visibility.Hidden;
        private string _imageInfo;
        private Visibility _searching;

        public async void SelectItem(int index, Control control)
        {

            Visible = Visibility.Visible;
            await Task.Run(() =>
            {

                TaskIdentified++;
                int id = TaskIdentified;
                TagLib.Picture tmp = null;
                if (File.Exists(Global.FilePath.CacheImagePath + Items[index].Identifier + ".jpg") == false)
                {
                    var t = search.GetTrackInfo(Items[index]);
                    if (t.Count != 0 && t[0].Image.Count != 0)
                    {
                        tmp = t[0].Image[0] as TagLib.Picture;
                    }
                }
                else
                {
                    try
                    {
                        tmp = new TagLib.Picture(Global.FilePath.CacheImagePath + Items[index].Identifier + ".jpg");
                    }
                    catch {  }
                }

                if (tmp == null)
                {
                    ImageSource = null;
                    ImageInfo = "이미지 정보가 없음";
                }
                else
                {
                    var bin = tmp.Data.Data;
                    if (id == TaskIdentified)
                    {
                        control?.Dispatcher?.Invoke(() =>
                        {
                            var stream = new MemoryStream(bin);
                            var image = new Bitmap(System.Drawing.Image.FromStream(stream));
                            var data = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                 image.GetHbitmap(),
                                 IntPtr.Zero,
                                 Int32Rect.Empty,
                                 BitmapSizeOptions.FromEmptyOptions());
                            ImageInfo = $"{image.Width} x {image.Height}, {CapacityManage.Change(new System.Numerics.BigInteger(stream.Length))}";
                            ImageSource = data;
                        });
                    }
                }
                Visible = Visibility.Hidden;
            });
        }

    }
}
