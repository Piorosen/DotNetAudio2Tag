using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Conv;

namespace Tag.WPF
{
    public class ConvertStatusViewModel
    {
        public int MultiTask = 4;
        public ObservableCollection<ConvertModel> Items { get; set; }
        public Queue<ConvertModel> ConvertModelQueue = new Queue<ConvertModel>();

        AudioConverter converter;

        public void Execute(PresetModel preset)
        {
            int count = (MultiTask > ConvertModelQueue.Count
                ? ConvertModelQueue.Count
                : MultiTask);

            for (int i = 0; i < count; i++)
            {
                var value = ConvertModelQueue.Dequeue();
                value.Info.Parameter.Add(preset.Preset);
                Items.Add(value);
            }
            converter.ChangeExecute += Converter_ChangeExecute;
            foreach (var value in converter.Execute(preset.ConvMode, MultiTask))
            {

            }

        }

        private void Converter_ChangeExecute(object sender, int e)
        {
            Console.WriteLine(e);
        }

        public void Enqueue(ConvertModel info)
        {   
            ConvertModelQueue.Enqueue(info);
            converter.AddFile(info.Info);
        }

        public ConvertStatusViewModel()
        {
            Items = new ObservableCollection<ConvertModel>();
            converter = new AudioConverter();
        }

    }
}
