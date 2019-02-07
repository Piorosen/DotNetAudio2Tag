using NAudio.Lame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.WPF
{
    public class PresetModel
    {
        public string Title { get; set; }
        public LAMEPreset Preset { get; set; }
        public int Index = 0;
        public PresetModel(string t, LAMEPreset p, int index)
        {
            Title = t;
            Preset = p;
            Index = index;
        }
    }
}
