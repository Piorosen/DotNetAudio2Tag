using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tag.Core.Cue;
using Tag.Core.Tagging;
using Tag.Setting;

namespace Tag.WPF
{
    enum AutoModeTag : int
    {
        CueSplit = 1,
        Conv = 2,
        Tagging = 4
    }
    class AutoModeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AutoModeModel> Items { get; set; } = new ObservableCollection<AutoModeModel>();
        public Visibility LabelVisibility { get => _labelVisibility; set { _labelVisibility = value; OnPropertyChanged(); } }
        Visibility _labelVisibility = Visibility.Visible;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public void AddFile(string file)
        {
            var ext = Path.GetExtension(file).ToLower();

            if (ext == ".cue")
            {
                CueSpliter cue = new CueSpliter();
                cue.AddFile(file);
                foreach (var item in cue[0].Track)
                {
                    Items.Add(new AutoModeModel(item)
                    {
                        Path = file
                    });
                }
            }
            else
            {
                Items.Add(new AutoModeModel(file));
            }
            
        }

        bool CheckCueSplit()
        {
            return true;
        }

        async Task<bool> CheckConv()
        {
            var check = new ConvCheck();
            var result = await DialogHost.Show(check, Global.DialogIdentifier.AutoModeCodec);

            return false;
        }

        async Task<bool> CheckTagging(ObservableCollection<TaggingModel> data)
        {
            var check = new GetTagInfo(data[0].TagInfo, data);

            var result = await DialogHost.Show(check, Global.DialogIdentifier.AutoModeTagSelect);

            return true;
        }

        async Task<bool> CheckMode(int run)
        {
            string ext = Path.GetExtension(Items[0].Path).ToLower();
            ObservableCollection<TaggingModel> list = new ObservableCollection<TaggingModel>();

            if (ext == ".cue")
            {
                CueSpliter cue = new CueSpliter();
                cue.AddFile(Items[0].Path);

                foreach (var item in cue.List()[0].Track)
                {
                    var model = new TaggingModel
                    {
                        TagInfo = new TagInfo
                        {
                            Album = item.Album,
                            Title = item.Title,
                        },
                        WaveFormat = new WaveFormatModel
                        {
                            Length = item.DurationMS / 1000.0
                        }
                    };
                    model.TagInfo.Track.Add((uint)item.Track);
                    model.TagInfo.Artist.Add(item.Artist);
                    model.TagInfo.Composer.Add(item.Composer);
                    list.Add(model);
                }
            }
            else
            {
                foreach (var item in Items)
                {
                    var model = new TaggingModel
                    {
                        TagInfo = item.Tag,
                        WaveFormat = new WaveFormatModel
                        {
                            Bitrate = item.Format.SampleRate,
                            Channel = item.Format.Channels,
                            Length = item.DurationMS
                        }
                    };
                }
            }

            bool result = true;
            if ((run & (int)AutoModeTag.CueSplit) == (int)AutoModeTag.CueSplit)
            {
                result &= CheckCueSplit();
            }
            if ((run & (int)AutoModeTag.Conv) == (int)AutoModeTag.Conv)
            {
                result &= await CheckConv();
            }
            if ((run & (int)AutoModeTag.Tagging) == (int)AutoModeTag.Tagging)
            {
                result &= await CheckTagging(list);
            }
            return true;
        }

        

        public async void Execute(int run)
        {
            Global.IsAutoMode = true;
            var result = await CheckMode(run);
            if (result)
            {

            }
            
        }
    }
}
