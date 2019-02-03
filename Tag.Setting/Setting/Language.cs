using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Setting.Pattern;

namespace Tag.Setting.Setting
{
    public class Language : SingleTon<Language>
    {
        public void Load(string filename)
        {
            if (new FileInfo(FilePath.SettingPath + filename).Exists)
            {
                Config.Path = FilePath.SettingPath + filename;
                foreach (var value in this.GetType().GetFields())
                {
                    var data = value.GetValue(this);
                    if (data.GetType() == typeof(string))
                    {
                        data = Config.GetOption("Lang", value.Name);
                    }
                }
            }
        }

        public string Title;
        public string Artist;
        public string Album;
        public string Year;
        public string Track;
        public string Gerne;
        public string Comment;
        public string AlbumArtist;
        public string Composer;
        public string DiscNum;
        public string Directory;

        public string CueSplit;
        public string Tagging;
        public string Mp3Conv;
        public string AutoMode;
        public string Setting;

        public string Execute;
    }
}
