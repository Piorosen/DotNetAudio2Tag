using NAudio.Lame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Conv;

namespace Tag.WPF
{
    public class PresetModel
    {
        public LAMEPreset Preset { get; set; }
        public ConvMode ConvMode { get; set; }
        public PresetModel(LAMEPreset p, ConvMode mode)
        {
            Preset = p;
            ConvMode = mode;
        }
    }
}
