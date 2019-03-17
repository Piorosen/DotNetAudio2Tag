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
using ToastNotifications.Messages;

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
        /// <summary>
        /// View에 보여주는 리스트
        /// Cue이면 Path에 Cue파일 경로, Mp3나 Wav파일이면 해당 파일경로
        /// </summary>
        public ObservableCollection<AutoModeModel> Items { get; set; } = new ObservableCollection<AutoModeModel>();

        public Visibility LabelVisibility { get => _labelVisibility; set { _labelVisibility = value; OnPropertyChanged(); } }
        Visibility _labelVisibility = Visibility.Visible;

        private bool _buttonEnable = true;
        public bool ButtonEnable { get => _buttonEnable; set { _buttonEnable = value; OnPropertyChanged(); } }

        public AutoModeViewModel()
        {
            Global.DialogIdentifier.PropertyChanged += DialogIdentifier_PropertyChanged;
        }

        private void DialogIdentifier_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Global.DialogIdentifier.AutoModeEnable))
            {
                ButtonEnable = Global.DialogIdentifier.AutoModeEnable;
            }
        }

        List<TaggingModel> TagData = new List<TaggingModel>();
        ConvCheckModel ConvPreset = new ConvCheckModel();



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        /// <summary>
        /// 파일 추가
        /// </summary>
        /// <param name="file">해당 파일 경로</param>
        public void AddFile(string file)
        {
            var ext = Path.GetExtension(file).ToLower();
            if (ext == ".cue")
            {
                CueSpliter cue = new CueSpliter();
                cue.AddFile(file);
                foreach (var item in cue[0].Track)
                {
                    Items.Add(new AutoModeModel(item, file));
                }
            }
            else
            {
                Items.Add(new AutoModeModel(file));
            }
        }

        /// <summary>
        /// Path가 Cue파일인지 아닌지 체크
        /// </summary>
        /// <returns>cue인지 아닌지 결과</returns>
        bool CheckCueSplit()
        {
            foreach (var value in Items)
            {
                if (Path.GetExtension(value.Path) != ".cue")
                {
                    return false;
                }
            }
            return true;
        }

        async Task<bool> CheckConv(bool isCue)
        {
            if (isCue)
            {
                return false;
            }
            var check = new ConvCheck();
            var result = await DialogHost.Show(check, Global.DialogIdentifier.AutoModeCodec);

            if (result is ConvCheckModel)
            {
                ConvPreset = result as ConvCheckModel;
                return true;
            }
            else
            {
                return false;
            }
        }

        async Task<bool> CheckTagging(bool isCue)
        {
            if (isCue)
            {
                return false;
            }

            TagData.Clear();

            var tag = new ObservableCollection<TaggingModel>();
            foreach (var value in Items)
            {
                var model = new TaggingModel
                {
                    TagInfo = value.Tag,
                    WaveFormat = new WaveFormatModel
                    {
                        Bitrate = value.Format.SampleRate,
                        Channel = value.Format.Channels,
                        Length = value.DurationMS / 1000
                    }
                };
                TagData.Add(model);
                tag.Add(model);
            }
            var check = new GetTagInfo(Items[0].Tag, tag);
            
            var result = await DialogHost.Show(check, Global.DialogIdentifier.AutoModeTagSelect, (object s, DialogClosingEventArgs e) =>
            {
                for (int i = 0; i < TagData.Count; i++)
                {
                    var data = tag.First((model) => (model.WaveFormat.Length == TagData[i].WaveFormat.Length));
                    TagData[i] = data;
                }
            });

            if (result is bool)
            {
                if ((bool)result == false)
                {
                    return false;
                }
            }
            return true;
        }

        async Task<bool> CheckMode(int run)
        {
            string ext = Path.GetExtension(Items[0].Path).ToLower();

            bool isCue = false;

            if (ext == ".cue")
            {
                isCue = true;
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
                    TagData.Add(model);
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
            if (result && (run & (int)AutoModeTag.CueSplit) == (int)AutoModeTag.CueSplit)
            {
                isCue = false;
                result &= CheckCueSplit();
            }
            if (result && (run & (int)AutoModeTag.Conv) == (int)AutoModeTag.Conv)
            {
                result &= await CheckConv(isCue);
            }
            if (result && (run & (int)AutoModeTag.Tagging) == (int)AutoModeTag.Tagging)
            {
                result &= await CheckTagging(isCue);
            }
            return result;
        }

        
        public async void Execute(int run, string resultPath)
        {
            Global.DialogIdentifier.AutoModeEnable = false;
            Global.IsAutoMode = true;

            var result = await CheckMode(run);
            if (result)
            {
                var list = new List<AutoModeModel>();
                var tag = new List<TagInfo>();
                foreach (var value in TagData)
                {
                    tag.Add(value.TagInfo);
                }

                foreach (var item in Items)
                {
                    list.Add(item);
                }
                // run 정보와 현재 파일 상태, 태그정보
                var check = new AutoModeStatus(run, resultPath, list, tag, ConvPreset);
                
                await DialogHost.Show(check, Global.DialogIdentifier.AutoModeStatus, (object a, DialogOpenedEventArgs b) =>
                {
                    check.Execute();
                });
                Application.notifier.ShowSuccess(Global.Language.AutoSuccess);
            }
            else
            {
                Application.notifier.ShowError(Global.Language.AutoFail);
            }
            Global.DialogIdentifier.AutoModeEnable = true;
        }
    }
}
