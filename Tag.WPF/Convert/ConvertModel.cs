using NAudio.Lame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Conv;

namespace Tag.WPF
{
    public class ConvertModel : INotifyPropertyChanged
    {
        public string Title { get => _title; set { _title = value; OnPropertyChnage(); } }
        public int Value { get => _value; set { _value = value; OnPropertyChnage(); } }

        public ConvInfo Info = new ConvInfo();
        public int Id = 0;

        private int _value = 0;
        private string _title = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChnage([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}
