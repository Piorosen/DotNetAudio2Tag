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

        Tag.Core.Conv.AudioConverter converter;

        public bool AddFile(ConvInfo info)
        {
            if (converter.AddFile(info))
            {
               // ConvertModelQueue.Add(info);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Execute(PresetModel preset)
        {
            for (int i = 0; i < (MultiTask > ConvertModelQueue.Count
                ? ConvertModelQueue.Count
                : MultiTask); i++)
            {
                Items.Add(ConvertModelQueue.Dequeue());
            }
        }

        public ConvertStatusViewModel()
        {
            Items = new ObservableCollection<ConvertModel>();
        }

    }
}
