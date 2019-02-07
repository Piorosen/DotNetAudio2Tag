
using NAudio.Lame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Conv;

namespace Tag.WPF
{
    class ConvertViewModel
    {
        Tag.Core.Conv.AudioConverter converter;

        public ObservableCollection<ConvertModel> ConvertExtension { get; set; }
        public ObservableCollection<ConvInfo> ConvInfos { get; set; } 

        public void Change(int index)
        {
            var data = ConvertExtension[0].Preset[index];
            ConvertExtension[0].Preset.RemoveAt(index);
            ConvertExtension[0].Preset.Add(data);

            for (int i = 0; i < ConvertExtension[0].Preset.Count; i++)
            {
                ConvertExtension[0].Preset[i].Index = i;
            }
        }

        public bool AddFile(ConvInfo info)
        {
            if (converter.AddFile(info))
            {
                ConvInfos.Add(info);
                return true;
            }
            else
            {
                return false;
            }
        }

        public ConvertViewModel()
        {
            ConvertExtension = new ObservableCollection<ConvertModel>
            {
                new ConvertModel("변환할 프리셋",
                new PresetModel("Flac", LAMEPreset.ABR_320,0),
                new PresetModel("MP3 128Kb", LAMEPreset.ABR_128,1),
                new PresetModel("MP3 256Kb", LAMEPreset.ABR_256,2),
                new PresetModel("MP3 320Kb", LAMEPreset.ABR_320,3))
            };
            ConvInfos = new ObservableCollection<ConvInfo>();
            converter = new Core.Conv.AudioConverter();
            
        }
    }
}
