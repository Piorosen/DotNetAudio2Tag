using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tag.WPF
{
    class MainWindowsViewModel
    {
        public string CustomizeMode => Setting.Global.Language.AutoMode;
        public string CueSplit => Setting.Global.Language.CueSplit;
        public string Tagging => Setting.Global.Language.Tagging;
        public string Converting => Setting.Global.Language.Mp3Conv;
        public string Option => Setting.Global.Language.Setting;


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}
