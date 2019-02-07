using NAudio.Lame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.WPF
{
    

    public class ConvertModel
    {
        public string Title { get; set; }
        public ObservableCollection<PresetModel> Preset { get; set; }

        public ConvertModel(string title, params PresetModel[] models)
        {
            Title = title;
            Preset = new ObservableCollection<PresetModel>(models);
        }
    }
}
