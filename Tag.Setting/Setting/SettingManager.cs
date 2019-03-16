using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tag.Setting.Pattern;
using Tag.Setting.Setting;

namespace Tag.Setting
{
    public class SettingManager : SingleTon<SettingManager>, INotifyPropertyChanged
    {
        public string LameEncode { get; set; } = string.Empty;
        public string LamePath { get { return Global.Resource.Lame; } set { Global.Resource.Lame = value; } }
        public string LangFile { get; set; } = string.Empty;
        public string LangIndex { get; set; } = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

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
                        var get = Config.GetOption("Option", value.Name);

                        //if (get.Length > 1 && get[0] == '.' && get[1] == '\\')
                        //{
                        //    get = Application.StartupPath + get;
                        //}
                        value.SetValue(this, get);
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value.Name));
                    }
                }
            }
        }

        public void Save()
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
                        Config.SetOption("Option", value.Name, value.GetValue(this) as String);
                    }
                }
            }
        }
    }
}
