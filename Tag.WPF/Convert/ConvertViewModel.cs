
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
using ToastNotifications.Messages;

namespace Tag.WPF
{
    class ConvertViewModel : INotifyPropertyChanged
    {
        public List<PresetModel> ConvertMode { get; set; }
        public ObservableCollection<ConvInfo> ConvInfos { get; set; }
        int Index = 0;

        public Visibility LabelVisibility { get => _labelVisibility; set { _labelVisibility = value; OnPropertyChanged(); } }

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
                new PresetModel(LAMEPreset.ABR_320, ConvMode.NORMAL),
                new PresetModel(LAMEPreset.ABR_320, ConvMode.USER)
            };

            ConvInfos = new ObservableCollection<ConvInfo>();

        }

        DialogSession Session = null;
        private Visibility _labelVisibility;

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

        void OpenEventHandler(object sender, DialogOpenedEventArgs envetArgs)
        {
            Session = envetArgs.Session;
        }

        private void CloseEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (bool.Parse(eventArgs.Parameter.ToString()) == true)
            {
                Application.notifier.ShowInformation("성공적으로 변환을 하였습니다.");
            }
            else
            {
                Application.notifier.ShowInformation("변환을 하는데 실패 하였습니다.");
            }

            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            if (!Session.IsEnded)
            {
                Session.Close();
                Session = null;
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
            if (Setting.Global.DialogCheck == false)
            {
                Setting.Global.DialogCheck = true;
                var result = await DialogHost.Show(Content, OpenEventHandler, CloseEventHandler);
                Setting.Global.DialogCheck = false;
            }
        }
    }
}
