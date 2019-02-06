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

        private Tag.Core.Cue.CueSpliter cueSpliter;

        public CueSplitViewModel()
        {
            Items = new ObservableCollection<CueSplitModel>();
            cueSpliter = new Core.Cue.CueSpliter();
        }

        public void AddFile(string filePath)
        {
            cueSpliter.AddFile(filePath);
            
        }
    }
}
