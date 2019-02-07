
using NAudio.Lame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.WPF
{
    class ConvertViewModel
    {
        public ObservableCollection<ConvertModel> ConvertExtension { get; set; }

        public ConvertViewModel()
        {
            ConvertExtension = new ObservableCollection<ConvertModel>
            {
                new ConvertModel("변환할 프리셋",
                            new PresetModel("MP3 320Kb", LAMEPreset.ABR_320),
                            new PresetModel("MP3 256Kb", LAMEPreset.ABR_256),
                            new PresetModel("MP3 128Kb", LAMEPreset.ABR_128),
                            new PresetModel("Flac", LAMEPreset.ABR_320))
            };
        }
    }
}
