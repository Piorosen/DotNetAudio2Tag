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

            if (new FileInfo(Global.FilePath.SettingPath + filename).Exists)
            {
                Config.Path = Global.FilePath.SettingPath + filename;
                foreach (var value in this.GetType().GetProperties())
                {
                    var data = value.GetValue(this);
                    if (data.GetType() == typeof(string))
                    {
                        value.SetValue(this, Config.GetOption("Lang", value.Name));
                    }
                }
            }
        }

        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Track { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string AlbumArtist { get; set; } = string.Empty;
        public string Composer { get; set; } = string.Empty;
        public string DiscNum { get; set; } = string.Empty;
        public string AlbumArt { get; set; } = string.Empty;
        public string Directory { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;

        public string ImageChange { get; set; } = string.Empty;
        public string ImageDelete { get; set; } = string.Empty;

        public string TagDragDrop { get; set; } = string.Empty;
        public string TagAllSave { get; set; } = string.Empty;
        public string TagGetInfo { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public string Codec { get; set; } = string.Empty;
        public string Bitrate { get; set; } = string.Empty;
        public string Length { get; set; } = string.Empty;
        public string Fixed { get; set; } = string.Empty;
        public string Channel { get; set; } = string.Empty;

        public string MusicBrainzSearch { get; set; } = string.Empty;
        public string VGMDBSearch { get; set; } = string.Empty;

        public string SelectInfoAlbum { get; set; } = string.Empty;
        public string Score { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ManualSearch { get; set; } = string.Empty;
        public string OK { get; set; } = string.Empty;
        public string Cancel { get; set; } = string.Empty;

        public string Lang { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;

        public string TASYes { get; set; } = string.Empty;
        public string TASNO { get; set; } = string.Empty;
        public string TASCancel { get; set; } = string.Empty;
        public string TASCheck { get; set; } = string.Empty;

        public string AlbumInfo { get; set; } = string.Empty;
        public string ExtensionInfo { get; set; } = string.Empty;
        public string TrackInfo { get; set; } = string.Empty;
        public string MyFile { get; set; } = string.Empty;
        public string UpMove { get; set; } = string.Empty;
        public string DownMove { get; set; } = string.Empty;
        public string ExtenName { get; set; } = string.Empty;
        public string ExtenValue { get; set; } = string.Empty;



        public string CueSplit { get; set; } = string.Empty;
        public string Tagging { get; set; } = string.Empty;
        public string Mp3Conv { get; set; } = string.Empty;
        public string AutoMode { get; set; } = string.Empty;
        public string Setting { get; set; } = string.Empty;

        public string Execute { get; set; } = string.Empty;

        public string CueFileOpen { get; set; } = string.Empty;
        public string CueFileSplitExecute { get; set; } = string.Empty;

    }
}
