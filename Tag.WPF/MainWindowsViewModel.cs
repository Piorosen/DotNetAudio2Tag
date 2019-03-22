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
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}
