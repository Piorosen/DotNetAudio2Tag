using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Setting.Pattern;
using Tag.Setting.Setting;

namespace Tag.Setting
{
    public class SettingManager : SingleTon<SettingManager>
    {
        public string LameEncode { get; set; } = string.Empty;
        public string LamePath { get { return Global.Resource.Lame; } set { Global.Resource.Lame = value; } }
        public string LangIndex { get; set; } = string.Empty;
        public string LangPath { get; set; } = string.Empty;
        
        public void Load()
        {
            string filename = "Setting.ini";
            if (new FileInfo(Global.FilePath.SettingPath + filename).Exists)
            {
                Config.Path = Global.FilePath.SettingPath + filename;
                foreach (var value in this.GetType().GetProperties())
                {
                    var data = value.GetValue(this);
                    if (data.GetType() == typeof(string))
                    {
                        value.SetValue(this, Config.GetOption("Option", value.Name));
                    }
                }
            }
        }
    }
}
