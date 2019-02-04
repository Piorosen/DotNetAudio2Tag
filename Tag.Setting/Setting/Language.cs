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
        private string _fileName = string.Empty;

        public void Load(string filename)
        {
            if (_fileName == filename)
            {
                return;
            }

            this._fileName = filename;

            if (new FileInfo(FilePath.SettingPath + filename).Exists)
            {
                Config.Path = FilePath.SettingPath + filename;
                foreach (var value in this.GetType().GetFields())
                {
                    var data = value.GetValue(this);
                    if (data.GetType() == typeof(string))
                    {
                        value.SetValue(this, Config.GetOption("Lang", value.Name));
                    }
                }
            }
        }

        public string Title = string.Empty;
        public string Artist = string.Empty;
        public string Album = string.Empty;
        public string Year = string.Empty;
        public string Track = string.Empty;
        public string Genre = string.Empty;
        public string Comment = string.Empty;
        public string AlbumArtist = string.Empty;
        public string Composer = string.Empty;
        public string DiscNum = string.Empty;
        public string AlbumArt = string.Empty;
        public string Directory = string.Empty;

        public string FileName = string.Empty;
        public string Path = string.Empty;
        public string Tag = string.Empty;
        public string Codec = string.Empty;
        public string Bitrate = string.Empty;
        public string Length = string.Empty;
        public string Fixed = string.Empty;


        public string CueSplit = string.Empty;
        public string Tagging = string.Empty;
        public string Mp3Conv = string.Empty;
        public string AutoMode = string.Empty;
        public string Setting = string.Empty;

        public string Execute = string.Empty;


    }
}
