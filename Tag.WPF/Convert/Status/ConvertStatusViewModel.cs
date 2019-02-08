using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Tag.Core.Conv;
using ToastNotifications.Messages;

namespace Tag.WPF
{
    public class ConvertStatusViewModel : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public async void Execute(PresetModel preset)
        {
            this.preset = preset;

            int count = (MultiTask > ConvertModelQueue.Count
                ? ConvertModelQueue.Count
                : MultiTask);

            AudioCount = ConvertModelQueue.Count;

            for (int i = 0; i < AudioCount; i++)
            {
                StatusValue.Add(0);
            }

            for (int i = 0; i < count; i++)
            {
                Items.Add(Dequeue());
            }

            var result = await converter.Execute(preset.ConvMode, MultiTask);

            await Task.Delay(1000);
            DialogHost.CloseDialogCommand.Execute(result, null);
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

                TotalStatus = StatusValue.Sum() / AudioCount;

                Control.Dispatcher.Invoke(() =>
                {
                    Control.UpdateLayout();
                });
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

    }
}
