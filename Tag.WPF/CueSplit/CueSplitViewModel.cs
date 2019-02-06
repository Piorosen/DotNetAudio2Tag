using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.WPF
{
    public class CueSplitViewModel
    {
        public ObservableCollection<CueSplitModel> Items { get; set; }

        public CueSplitViewModel()
        {
            Items = new ObservableCollection<CueSplitModel>
            {
                   new CueSplitModel{ Artist= "123123" }
                
            };
        }

        public void AddFile(string filePath)
        {
            Items.Add(new CueSplitModel { Artist = "123123" });
        }
    }
}
