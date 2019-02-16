using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Tagging;

namespace Tag.WPF
{
    public class TaggingModel : INotifyPropertyChanged
    {
        private WaveFormatModel _waveFormat = new WaveFormatModel();
        private TagInfo _tagInfo = new TagInfo();

        public string FileName => Path.GetFileName(_tagInfo.Path);
        public WaveFormatModel WaveFormat { get => _waveFormat; set { _waveFormat = value; OnPropertyChanged(); } }
        public TagInfo TagInfo { get => _tagInfo; set { _tagInfo = value; OnPropertyChanged(); } }
        
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}
