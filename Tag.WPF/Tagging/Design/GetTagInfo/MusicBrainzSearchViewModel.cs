using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Tagging;

namespace Tag.WPF
{
    class MusicBrainzSearchViewModel
    {
        public ObservableCollection<BrainzInfo> Items { get; set; }

        public MusicBrainzSearchViewModel()
        {
            Items = new ObservableCollection<BrainzInfo>();
        }
    }
}
