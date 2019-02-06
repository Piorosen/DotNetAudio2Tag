using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tag.WPF
{
    public class CueSplitViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CueSplitModel> Items { get; set; }

        private Tag.Core.Cue.CueSpliter cueSpliter;
        
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public CueSplitViewModel()
        {
            Items = new ObservableCollection<CueSplitModel>();
            cueSpliter = new Core.Cue.CueSpliter();
        }
        
        public void Change(int index, CueSplitModel newdata)
        {
            Items.RemoveAt(index);
            Items.Insert(index, newdata);
        }

        public void Click(int index)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                var data = Items[i];
                if (i == index)
                {
                    data.IsSelect = true;
                }
                else
                {
                    data = Items[i];
                    data.IsSelect = false;
                }
                Change(i, data);
            }
        }
        
        public void AddFile(string filePath)
        {
            Items.Clear();
            cueSpliter.List().Clear();
            cueSpliter.AddFile(filePath);
            int index = 1;
            foreach (var value in cueSpliter[0].Track)
            {
                CueSplitModel model = new CueSplitModel(value)
                {
                    Index = index
                };
                Items.Add(model);
                index++;
            }
        }
    }
}
