using Library;
using MaterialDesignThemes.Wpf;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Tag.Core.Cue;
using Tag.Core.Tagging;
using Tag.Setting;
using Tag.WPF.Properties;
using TagLib;
using ToastNotifications.Messages;

namespace Tag.WPF
{
    public class TaggingViewModel : INotifyPropertyChanged
    {
        AudioTagging audioTagging;
        private List<TaggingModel> _selectItem;

        public ObservableCollection<TaggingModel> Items { get => _items; set { _items = value; OnPropertyChanged(); } }
        public List<TaggingModel> SelectItem { get => _selectItem; set { _selectItem = value; OnPropertyChanged(); } }

        public Visibility LabelVisibility { get => _LabelVisibility; set { _LabelVisibility = value; OnPropertyChanged(); } }
        private Visibility _LabelVisibility = Visibility.Visible;

        public bool ButtonEnable { get => _buttonEnable; set { _buttonEnable = value; OnPropertyChanged(); } }
        public string FileSize { get => _fileSize; set { _fileSize = value; OnPropertyChanged(); } }
        public string IsTagSave { get => _isTagSave; set { _isTagSave = value; OnPropertyChanged(); } }

        public bool RemoveFile(int index)
        {
            if (0 <= index && index < Items.Count)
            {
                SelectItem.Remove(Items[index]);
                Items.RemoveAt(index);
                if (Items.Count == 0)
                {
                    LabelVisibility = Visibility.Visible;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ClearFile()
        {
            Items.Clear();
            LabelVisibility = Visibility.Visible;
        }

        private void DialogIdentifier_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Global.DialogIdentifier.TaggingEnable))
            {
                ButtonEnable = Global.DialogIdentifier.TaggingEnable;
            }
        }

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
            LabelVisibility = Visibility.Hidden;
        }
        public void ChangeText(string Name, string Value)
        {
            if (SelectItem == null && SelectItem.Count == 0)
            {
                return;
            }

            foreach (var value in SelectItem)
            {
                foreach (var p in value.TagInfo.GetType().GetProperties())
                {
                    if (p.Name == Name)
                    {
                        var check = p.GetValue(value.TagInfo);
                        if (check == null)
                        {
                            return;
                        }

                        if (check is List<string>)
                        {
                            p.SetValue(value.TagInfo, Value.Split(';').ToList());
                        }
                        else if (check is List<uint>)
                        {
                            try
                            {
                                p.SetValue(value.TagInfo, Value.Split(',')
                                    .ToList()
                                    .Select((s) => uint.Parse(s))
                                    .ToList());
                            }
                            catch
                            {

                            }
                        }
                        else
                        {
                            p.SetValue(value.TagInfo, Value);
                        }
                        value.TagInfo = value.TagInfo;
                        IsTagSave = "태그를 저장하시오.";
                    }
                }
            }
        }



        TagInfo copyFile = new TagInfo();

        public void CopyFile(TaggingModel taggingModel)
        {
            copyFile = new TagInfo(taggingModel.TagInfo);
        }
        public void CutFile(TaggingModel taggingModel)
        {
            copyFile = new TagInfo(taggingModel.TagInfo);
            taggingModel.TagInfo = new TagInfo(copyFile.Path);
        }
        public void RemoveFile(TaggingModel taggingModel)
        {
            copyFile = new TagInfo(taggingModel.TagInfo);
            taggingModel.TagInfo = new TagInfo(copyFile.Path);
            copyFile = new TagInfo();
        }

        public void PasteFile(TaggingModel taggingModel)
        {
            taggingModel.TagInfo = new TagInfo(copyFile, taggingModel.TagInfo.Path);
        }

        public bool Select = false;
        private ObservableCollection<TaggingModel> _items;
        private bool _buttonEnable = true;
        private string _fileSize;
        private string _isTagSave;

        public void SelectModel(List<TaggingModel> items)
        {
            Select = true;
            SelectItem = items;
            if (SelectItem.Count != 0 && SelectItem[0].TagInfo.UIImage != null)
            {
                //var data = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                //     image.GetHbitmap(),
                //     IntPtr.Zero,
                //     Int32Rect.Empty,
                //     BitmapSizeOptions.FromEmptyOptions());
                FileSize = $"{SelectItem[0].TagInfo.UIImage.Width} x {SelectItem[0].TagInfo.UIImage.Height}"; // , {CapacityManage.Change(new System.Numerics.BigInteger(image.))}";
            }
            else
            {
                FileSize = Global.Language.ImageNone;
            }
            Select = false;
        }



        public void SaveOne()
        {

        }

        private void CloseEvent(object sender, DialogClosingEventArgs e)
        {
            audioTagging.tagList.Clear();


            if ((bool)e.Parameter == true)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    audioTagging.AddFile(Items[i].TagInfo.Path);
                    audioTagging.List()[i] = Items[i].TagInfo;

                }
                foreach (var value in audioTagging.Execute())
                {
                    Console.WriteLine(value);
                }
                Application.notifier.ShowInformation(Global.Language.TagSuccess);
            }
            else
            {
                Application.notifier.ShowInformation(Global.Language.TagFail);
                return;
            }
        }

        public async void AllTagSave()
        {
            var view = new TagAllSave
            {
                Width = 300,
                Height = 100
            };

            Global.DialogIdentifier.TaggingEnable = false;
            // DialogHost.OpenDialogCommand.Execute(view, new System.Windows.Controls.Button().CommandTarget);
            bool result = (bool)(await DialogHost.Show(view, Global.DialogIdentifier.TagSave, CloseEvent));
            if (result == true)
            {
                IsTagSave = "";
            }
        }

        public async void GetTagInfo(int index)
        {
            var view = new GetTagInfo(Items[index == -1 ? 0 : index].TagInfo, Items);
            try
            {
                Global.DialogIdentifier.TaggingEnable = false;
                var result = await DialogHost.Show(view, Global.DialogIdentifier.GetTagInfo);

                if (result is bool)
                {
                    if ((bool)result == true)
                    {
                        CloseEvent(this, new DialogClosingEventArgs(null, true));

                        List<string> t = new List<string>();

                        foreach (var value in Items)
                        {
                            string filename = Global.Setting.TagTypeSetting;
                            while (filename.IndexOf("%a%") != -1)
                            {
                                filename = filename.Replace("%a%", value.TagInfo.Artist.Count != 0
                                                                                ? value.TagInfo.Artist[0]
                                                                                : string.Empty);
                            }
                            while (filename.IndexOf("%A%") != -1)
                            {
                                filename = filename.Replace("%A%", value.TagInfo.AlbumArtist.Count != 0
                                                                                ? value.TagInfo.AlbumArtist[0]
                                                                                : string.Empty);
                            }
                            while (filename.IndexOf("%n%") != -1)
                            {
                                filename = filename.Replace("%n%", value.TagInfo.Title);
                            }
                            while (filename.IndexOf("%t%") != -1)
                            {
                                filename = filename.Replace("%t%", value.TagInfo.Track.Count != 0
                                                                                ? value.TagInfo.Track[0].ToString()
                                                                                : string.Empty);
                            }
                            while (filename.IndexOf("%y%") != -1)
                            {
                                filename = filename.Replace("%y%", value.TagInfo.Year);
                            }
                            while (filename.IndexOf("%an%") != -1)
                            {
                                filename = filename.Replace("%an%", value.TagInfo.Album);
                            }
                            while (filename.IndexOf("%fn%") != -1)
                            {
                                filename = filename.Replace("%fn%", Path.GetFileNameWithoutExtension(value.TagInfo.Path));
                            }

                            var dir = Path.GetDirectoryName(value.TagInfo.Path);
                            var ext = Path.GetExtension(value.FileName);

                            char[] chars = Path.GetInvalidFileNameChars();
                            for (int i = 0; i < filename.Length; i++)
                            {
                                for (int w = 0; w < chars.Length; w++)
                                {
                                    if (filename[i] == chars[w])
                                    {
                                        filename = filename.Remove(i, 1);
                                        break;
                                    }
                                }
                            }
                            filename = dir + @"\" + filename + ext;
                            try
                            {
                                if (Path.GetFullPath(filename) != Path.GetFullPath(value.TagInfo.Path))
                                {
                                    if (System.IO.File.Exists(filename))
                                    {
                                        System.IO.File.Delete(filename);
                                    }
                                    System.IO.File.Move(value.TagInfo.Path, filename);
                                }
                            }
                            catch { }
                            t.Add(filename);
                            //if (filename + ext != value.TagInfo.Path)
                            //{
                            //    if (System.IO.File.Exists(filename + ext) == true)
                            //    {
                            //        int num = 1;
                            //        for (; System.IO.File.Exists($"{filename} ({num}){ext}"); num++)
                            //        {
                            //        }
                            //        filename += $" ({num}){ext}";
                            //    }
                            //    else
                            //    {
                            //        filename += ext;
                            //    }
                            //    System.IO.File.Move(value.TagInfo.Path, filename);
                            //}

                        }
                        ClearFile();
                        foreach (var value in t)
                        {
                            AddModel(value);
                        }

                    }
                }
            }
            catch { }
        }

        public void RemoveModel(int index)
        {
            Items.RemoveAt(index);
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
            SelectItem = new List<TaggingModel>();
            Global.DialogIdentifier.PropertyChanged += DialogIdentifier_PropertyChanged;
        }
    }
}
