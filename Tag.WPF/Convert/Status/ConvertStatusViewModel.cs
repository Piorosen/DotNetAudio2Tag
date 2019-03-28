using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Tag.Core.Conv;
using Tag.Setting;
using ToastNotifications.Messages;

namespace Tag.WPF
{
    public class ConvertStatusViewModel : INotifyPropertyChanged
    {
        public bool Result { get => _result; set { _result = value; OnPropertyChanged();  } }

        public int MultiTask = 4;
        public ObservableCollection<ConvertModel> Items { get; set; }
        public Queue<ConvertModel> ConvertModelQueue = new Queue<ConvertModel>();
        public int TotalStatus { get => _totalStatus; set { _totalStatus = value; OnPropertyChanged(); } }

        AudioConverter converter;
        PresetModel preset;
        UserControl Control;

        int AudioCount = 0;
        List<int> StatusValue = new List<int>();
        private int _totalStatus = 0;
        private bool _result = false;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public async void Execute(PresetModel preset, string resultPath, (string Path, string Format) Param)
        {
            Result = false;
            this.preset = preset;

            int count = (MultiTask > ConvertModelQueue.Count
                ? ConvertModelQueue.Count
                : MultiTask);

            AudioCount = ConvertModelQueue.Count;

            for (int i = 0; i < converter.List().Count; i++)
            {
                converter[i].Format = Param.Format;
                converter[i].Source = Param.Path;
            }

            for (int i = 0; i < AudioCount; i++)
            {
                StatusValue.Add(0);
            }

            for (int i = 0; i < count; i++)
            {
                Items.Add(Dequeue());
            }

            var result = await converter.Execute(preset.ConvMode, MultiTask, resultPath);
            Result = true;
        }

        ConvertModel Dequeue()
        {
            ConvertModel value;

            if (ConvertModelQueue.Count != 0)
            {
                value = ConvertModelQueue.Dequeue();
                value.Info.Parameter.Add(preset.Preset);
            }
            else
            {
                value = null;
            }

            return value;
        }

        private void Converter_ChangeExecute(object sender, int e)
        {
            var data = Items.First((item) => item.Id == e / 10000);
            if (data.Value != e % 10000)
            {
                data.Value = e % 10000;
                StatusValue[data.Id] = data.Value;

                try
                {
                    TotalStatus = StatusValue.Sum() / AudioCount;

                    Control.Dispatcher.Invoke(() =>
                    {
                        Control.UpdateLayout();
                    });
                }
                catch
                {

                }
            }

        }

        private void Converter_CompleteOfIndex(object sender, int e)
        {
            if (ConvertModelQueue.Count != 0)
            {
                Control.Dispatcher.Invoke(() =>
                {
                    Items.Remove(Items.First((item) => item.Id == e));
                    var data = Dequeue();
                    if (data != null)
                    {
                        Items.Add(data);
                    }
                });
            }
        }

        public void Enqueue(ConvertModel info)
        {
            info.Id = converter.List().Count;

            ConvertModelQueue.Enqueue(info);
            converter.AddFile(info.Info);
        }

        public ConvertStatusViewModel(UserControl control)
        {
            Items = new ObservableCollection<ConvertModel>();
            converter = new AudioConverter();
            Control = control;
            converter.ChangeExecute += Converter_ChangeExecute;
            converter.CompleteOfIndex += Converter_CompleteOfIndex;
        }


        public void CloseEvent(IInputElement input)
        {
            DialogHost.CloseDialogCommand.Execute(Result, input);
        }
    }
}
