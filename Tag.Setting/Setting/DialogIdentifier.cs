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
        public readonly string Convert = "Convert";
        public readonly string GetTagInfo = "GetTagInfo";
        public readonly string TagSave = "TagSave";
        public readonly string BrainzSearch = "MusicBrainzSearch";
        public readonly string VgmSearch = "VgmDbSearch";
        public readonly string CheckTagInfo = "CheckTagInfo";
        public readonly string ConvertUserMode = "ConvertUserMode";
        public readonly string CodecCodeTest = "CodecCodeTest";

        public readonly string AutoModeStatus = "AutoModeStatus";
        public readonly string AutoModeCodec = "AutoModeCodec";
        public readonly string AutoModeTagSelect = "AutoModeTagSelect";

        public readonly string AutoConvertUserMode = "AutoConvertUserMode";
        public readonly string AutoBrainzSearch = "AutoBrainzSearch";
        public readonly string AutoVgmSearch = "AutoVgmSearch";
        public readonly string AutoCheckTagInfo = "AutoCheckTagInfo";

        public readonly string AutoCodecCodeTest = "AutoCodecCodeTest";


        private bool _taggingEnable;
        private bool _convertEnable;
        private bool _settingEnable;
        private bool _lameEnable;
        private bool _autoModeEnable;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnProertyChange([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
        
        public bool AutoModeEnable { get => _autoModeEnable; set { _autoModeEnable = value; OnProertyChange(); } }
        public bool TaggingEnable { get => _taggingEnable; set { _taggingEnable = value; OnProertyChange(); } }
        public bool ConvertEnable { get => _convertEnable; set { _convertEnable = value; OnProertyChange(); } }
        public bool SettingEnable { get => _settingEnable; set { _settingEnable = value; OnProertyChange(); } }
        public bool CodecEnable { get => _lameEnable; set { _lameEnable = value; OnProertyChange(); } }

        public bool LangChange { set { OnProertyChange(); } }
    }
}
