using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Tag.Setting.Pattern;
using Tag.Setting.Setting;

namespace Tag.Setting
{
    public class Language : SingleTon<Language>, INotifyPropertyChanged
    {
        private string _fileName = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Load(string filename)
        {
            if (_fileName == filename)
            {
                return;
            }

            this._fileName = filename;

            if (new FileInfo(Global.FilePath.LangPath + filename).Exists)
            {
                Config.Path = Global.FilePath.LangPath + filename;
                foreach (var value in this.GetType().GetProperties())
                {
                    var data = value.GetValue(this);
                    if (data.GetType() == typeof(string))
                    {
                        value.SetValue(this, Config.GetOption("Lang", value.Name));
                    }
                    else
                    {
                        value.SetValue(this, new FontFamily(Config.GetOption("Lang", value.Name)));
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value.Name));
                }
            }
        }

        public FontFamily Font { get; set; } = new FontFamily();

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

        public string ImageNone { get; set; } = string.Empty;
        
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
        public string English { get; set; } = string.Empty;
        public string Romjai { get; set; } = string.Empty;
        public string Japanese { get; set; } = string.Empty;

        public string ConvExt { get; set; } = string.Empty;
        public string Conv320Kb { get; set; } = string.Empty;
        public string Conv256Kb { get; set; } = string.Empty;
        public string Conv128Kb { get; set; } = string.Empty;
        public string ConvFlac { get; set; } = string.Empty;
        public string ConvUser { get; set; } = string.Empty;
        public string ConvFileOpen { get; set; } = string.Empty;
        public string ConvExecute { get; set; } = string.Empty;
        public string ConvDragDrop { get; set; } = string.Empty;

        public string LameExternalPath { get; set; } = string.Empty;
        public string LameEncoding { get; set; } = string.Empty;
        public string LameCodeTest { get; set; } = string.Empty;
        public string LameCancel { get; set; } = string.Empty;
        public string LameSave { get; set; } = string.Empty;

        public string FFMpegExternalPath { get; set; } = string.Empty;
        public string FFMpegEncoding { get; set; } = string.Empty;
        public string FFMpegCodeTest { get; set; } = string.Empty;
        public string FFMpegCancel { get; set; } = string.Empty;
        public string FFMpegSave { get; set; } = string.Empty;

        public string FFMpegFileName { get; set; } = string.Empty;
        public string FFMpegBitrate { get; set; } = string.Empty;
        public string FFMpegRate { get; set; } = string.Empty;
        public string FFMpegOutputFileName { get; set; } = string.Empty;


        public string ConvStatusAllProgress { get; set; } = string.Empty;
        public string ConvStatusClose { get; set; } = string.Empty;

        public string CueDragDrop { get; set; } = string.Empty;
        public string CueFileOpen { get; set; } = string.Empty;
        public string CueFileSplitExecute { get; set; } = string.Empty;
        public string CueAlbum { get; set; } = string.Empty;
        public string CueBarcode { get; set; } = string.Empty;
        public string CueAverage { get; set; } = string.Empty;
        public string CueGenre { get; set; } = string.Empty;
        public string CueSongLength { get; set; } = string.Empty;
        public string CueIndexLength { get; set; } = string.Empty;
        public string CueMin { get; set; } = string.Empty;
        public string CueSecond { get; set; } = string.Empty;
        public string CueMiliSecond { get; set; } = string.Empty;

        public string AutoDragDrop { get; set; } = string.Empty;
        public string AutoCueSplit { get; set; } = string.Empty;
        public string AutoCodecConvert { get; set; } = string.Empty;
        public string AutoTag { get; set; } = string.Empty;
        public string AutoExecute { get; set; } = string.Empty;


        public string AutoCueTag { get; set; } = string.Empty;
        public string AutoAudioConv { get; set; } = string.Empty;
        public string AutoAudioTag { get; set; } = string.Empty;
        public string AutoComplete { get; set; } = string.Empty;

        public string CacheSize { get; set; } = string.Empty;
        public string LangOther { get; set; } = string.Empty;
        public string ProgramLang { get; set; } = string.Empty;
        public string CacheRemove { get; set; } = string.Empty;

        public string CueSplitSetting { get; set; } = string.Empty;
        public string CueFormatType { get; set; } = string.Empty;
        public string SettingAlbumArtist { get; set; } = string.Empty;
        public string SettingTrackArtist { get; set; } = string.Empty;
        public string SettingTrackName { get; set; } = string.Empty;
        public string SettingTrackNum { get; set; } = string.Empty;
        public string SettingAlbumName { get; set; } = string.Empty;
        public string SettingTrackYear { get; set; } = string.Empty;
        public string SettingFileName { get; set; } = string.Empty;

        public string SettingTagSaveFormat { get; set; } = string.Empty;
        public string SettingTagSaveFileFormat { get; set; } = string.Empty;
        public string SettingCueFileNameChnage { get; set; } = string.Empty;


        public string AutoModeSetting { get; set; } = string.Empty;
        public string CueSplitFolder { get; set; } = string.Empty;
        public string ConvFolder { get; set; } = string.Empty;

        public string ConvSuccess { get; set; } = string.Empty;
        public string ConvFail { get; set; } = string.Empty;
        public string CueSuccess { get; set; } = string.Empty;
        public string CueFail { get; set; } = string.Empty;
        public string AutoFail { get; set; } = string.Empty;
        public string AutoSuccess { get; set; } = string.Empty;
        public string TagSuccess { get; set; } = string.Empty;
        public string TagFail { get; set; } = string.Empty;

        public string CueSplit { get; set; } = string.Empty;
        public string Tagging { get; set; } = string.Empty;
        public string Mp3Conv { get; set; } = string.Empty;
        public string AutoMode { get; set; } = string.Empty;
        public string Setting { get; set; } = string.Empty;

        public string Execute { get; set; } = string.Empty;
        

    }
}
