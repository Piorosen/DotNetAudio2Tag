using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tag.Setting.Pattern;

namespace Tag.Setting.Setting
{
    public class DialogIdentifier : SingleTon<DialogIdentifier>, INotifyPropertyChanged
    {
        public string Convert = "Convert";
        public string GetTagInfo = "GetTagInfo";
        public string TagSave = "TagSave";
        public string BrainzSearch = "MusicBrainzSearch";
        public string VgmSearch = "VgmDbSearch";
        public string CheckTagInfo = "CheckTagInfo";
        public string ConvertUserMode = "ConvertUserMode";

        private bool _taggingEnable;
        private bool _convertEnable;
        private bool _settingEnable;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnProertyChange([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public bool TaggingEnable { get => _taggingEnable; set { _taggingEnable = value; OnProertyChange(); } }
        public bool ConvertEnable { get => _convertEnable; set { _convertEnable = value; OnProertyChange(); } }
        public bool SettingEnable { get => _settingEnable; set { _settingEnable = value; OnProertyChange(); } }

    }
}
