using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tag.Core.Conv;

namespace Tag.WPF
{
    public class ConvertStatusViewModel
    {
        public int MultiTask = 4;
        public ObservableCollection<ConvertModel> Items { get; set; }
        public Queue<ConvertModel> ConvertModelQueue = new Queue<ConvertModel>();

        AudioConverter converter;
        PresetModel preset;
        UserControl Control;

        public async void Execute(PresetModel preset)
        {
            this.preset = preset;

            int count = (MultiTask > ConvertModelQueue.Count
                ? ConvertModelQueue.Count
                : MultiTask);

            for (int i = 0; i < count; i++)
            {
                Items.Add(Dequeue());
            }
            
            var result = await converter.Execute(preset.ConvMode, MultiTask);
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
            }
        }

        private void Converter_CompleteOfIndex(object sender, int e)
        {
            Control.Dispatcher.Invoke(new Action(() =>
            {
                Items.Remove(Items.First((item) => item.Id == e));
                var data = Dequeue();
                if (data != null)
                {
                    Items.Add(data);
                }
            }));
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
