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
    public class ConvertModel
    {
        public string Title { get; set; } = string.Empty;
        public int Value { get; set; } = 0;
        public ConvInfo Info = new ConvInfo();

    }
}
