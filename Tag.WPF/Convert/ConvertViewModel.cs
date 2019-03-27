
using MaterialDesignThemes.Wpf;
using NAudio.Lame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tag.Core.Conv;
using Tag.Setting;
using ToastNotifications.Messages;

namespace Tag.WPF
{
    class ConvertViewModel : INotifyPropertyChanged
    {
        public List<PresetModel> ConvertMode { get; set; }
        public ObservableCollection<ConvInfo> ConvInfos { get; set; }
        int Index = 0;

        public Visibility LabelVisibility { get => _labelVisibility; set { _labelVisibility = value; OnPropertyChanged(); } }
        public bool ButtonEnable { get => _buttonEnable; set { _buttonEnable = value; OnPropertyChanged(); } }
        

        public void Checked(int index)
        {
            Index = index;
        }
        public ConvertViewModel()
        {
            ConvertMode = new List<PresetModel>
            {
                new PresetModel(LAMEPreset.ABR_320, ConvMode.NORMAL),
                new PresetModel(LAMEPreset.ABR_256, ConvMode.NORMAL),
                new PresetModel(LAMEPreset.ABR_128, ConvMode.NORMAL),
                new PresetModel(LAMEPreset.ABR_320, ConvMode.MYFLAC),
                new PresetModel(LAMEPreset.ABR_320, ConvMode.USER)
            };

            ConvInfos = new ObservableCollection<ConvInfo>();
            Global.DialogIdentifier.PropertyChanged += DialogIdentifier_PropertyChanged;
        }

        private void DialogIdentifier_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Global.DialogIdentifier.ConvertEnable))
            {
                ButtonEnable = Global.DialogIdentifier.ConvertEnable;
            }
        }

        private Visibility _labelVisibility;
        private bool _buttonEnable = true;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public void AddFile(ConvInfo info)
        {
            ConvInfos.Add(info);
            LabelVisibility = Visibility.Hidden;
        }
        public bool RemoveFile(int index)
        {
            if (0 <= index && index < ConvInfos.Count)
            {
                ConvInfos.RemoveAt(index);
                if (ConvInfos.Count == 0)
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

        private void CloseEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (bool.Parse(eventArgs.Parameter.ToString()) == true)
            {
                Application.notifier.ShowInformation(Global.Language.ConvSuccess);
            }
            else
            {
                Application.notifier.ShowInformation(Global.Language.ConvFail);
            }
        }

        public async void Execute(string resultPath)
        {
            var Content = new ConvertStatus
            {
                Width = 450,
                Height = 280,
            };
            foreach (var value in ConvInfos)
            {
                Content.Enqueue(new ConvertModel
                {
                    Info = value,
                    Title = value.FileNamePlus,
                    Value = 0
                });
            }

            Content.Execute(ConvertMode[Index], resultPath);

            Global.DialogIdentifier.ConvertEnable = false;
            var result = await DialogHost.Show(Content, Global.DialogIdentifier.Convert, CloseEventHandler);
            Global.DialogIdentifier.ConvertEnable = true;
        }
        

        public async void LameSetting()
        {
            var Content = new LameMode
            {
            };

            Global.DialogIdentifier.ConvertEnable = false;
            var result = (ValueTuple<string, string>)(await DialogHost.Show(Content, Global.DialogIdentifier.ConvertUserMode));
            
        }

        public async void FFMpegSetting()
        {
            var Content = new LameMode
            {
            };

            Global.DialogIdentifier.ConvertEnable = false;
            var result = (ValueTuple<string, string>)(await DialogHost.Show(Content, Global.DialogIdentifier.ConvertUserMode));
            
        }
    }
}
