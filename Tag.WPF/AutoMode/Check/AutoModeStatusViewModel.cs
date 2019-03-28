using ATL.CatalogDataReaders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tag.Core.Conv;
using Tag.Core.Cue;
using Tag.Core.Tagging;
using Tag.Setting;

namespace Tag.WPF
{
    class AutoModeStatusViewModel : INotifyPropertyChanged
    {
        CueSpliter cue = new CueSpliter();
        AudioConverter audioconv = new AudioConverter();
        AudioTagging audiotag = new AudioTagging();

        Dictionary<int, int> status = new Dictionary<int, int>();
        private string _title;
        private int _value;
        readonly UserControl Control;

        public string Title { get => _title; set { Control?.Dispatcher?.Invoke(() => { _title = value; OnPropertyChanged(); }); } }
        public int Value { get => _value; set { Control?.Dispatcher?.Invoke(() => { _value = value; OnPropertyChanged(); }); } }


        public AutoModeStatusViewModel(UserControl control)
        {
            Control = control;
            audioconv.ChangeExecute += (s, e) =>
            {
                try
                {
                    status[(e / 10000)] = e % 1000;
                    Value = status.Values.Sum() / audioconv.List().Count();
                }
                catch { }
            };
        }
        
        void CueSplit(List<AutoModeModel> data, string resultPath)
        {
            cue.AddFile(data[0].Path);
            foreach (var item in cue.List())
            {
                item.SavePath = resultPath + $"\\{Global.Setting.AutoCueFolder}\\";
            }
            
            foreach (var value in cue.Execute())
            {
                Value = value;
            }

            data.Clear();
            for (int i = 0; i < cue[0].Track.Count; i++)
            {
                string filename = Global.Setting.CueSplitSetting;
                while (filename.IndexOf("%a%") != -1)
                {
                    filename = filename.Replace("%a%", cue[0].Track[i].Artist);
                }
                while (filename.IndexOf("%A%") != -1)
                {
                    filename = filename.Replace("%A%", cue[0].Artist);
                }
                while (filename.IndexOf("%n%") != -1)
                {
                    filename = filename.Replace("%n%", cue[0].Track[i].Title);
                }
                while (filename.IndexOf("%t%") != -1)
                {
                    filename = filename.Replace("%t%", cue[0].Track[i].Track.ToString());
                }
                filename += Path.GetExtension(cue[0].WavPath);
                filename = cue[0].SavePath + filename;
                data.Add(new AutoModeModel(filename, data.Count + 1));
            }
        }

        async Task<bool> Conv(List<AutoModeModel> data, string resultPath, ConvCheckModel preset)
        {
            for (int i = 0; i < data.Count; i++)
            {
                string file = data[i].Path;

                audioconv.AddFile(new ConvInfo
                {
                    FilePath = file,
                    Type = data[i].Type,
                    ResultPath = resultPath + $"\\{Global.Setting.AutoConvFolder}\\",
                    Source = preset.Param.Path,
                    Format = preset.Param.Format
                });
            }


            var result = await audioconv.Execute(preset.preset.ConvMode, 4, resultPath + $"\\{Global.Setting.AutoConvFolder}\\");

            var t = data.ToArray().ToList();
            data.Clear();
            foreach (var value in t)
            {
                string ext = string.Empty;
                if (preset.preset.ConvMode == ConvMode.MYFLAC)
                {
                    ext = ".flac";
                }
                else
                {
                    ext = ".mp3";
                }
                var path = resultPath + $"\\{Global.Setting.AutoConvFolder}\\" + Path.GetFileNameWithoutExtension(value.Tag.Path) + ext;
                data.Add(new AutoModeModel(path, data.Count + 1));
            }


            return result;
        }

        void Tagging(List<AutoModeModel> data, List<TagInfo> tag, bool isCue = false)
        {
            audiotag.List().Clear();
            int count = data.Count > tag.Count ? tag.Count : data.Count;

            var datatmp = data.ToArray().ToList();

            for (int i = 0; i < count; i++)
            {
                audiotag.AddFile(tag[i], data[i].Path);
            }

            foreach (var value in audiotag.Execute())
            {
            }

            if (isCue == true)
            {
                return; 
            }

            for (int i = 0; i < count ; i++)
            {
                string filename = Global.Setting.TagTypeSetting;
                while (filename.IndexOf("%a%") != -1)
                {
                    filename = filename.Replace("%a%", tag[i].Artist.Count != 0
                                                                    ? tag[i].Artist[0]
                                                                    : string.Empty);
                }
                while (filename.IndexOf("%A%") != -1)
                {
                    filename = filename.Replace("%A%", tag[i].AlbumArtist.Count != 0
                                                                    ? tag[i].AlbumArtist[0]
                                                                    : string.Empty);
                }
                while (filename.IndexOf("%n%") != -1)
                {
                    filename = filename.Replace("%n%", tag[i].Title);
                }
                while (filename.IndexOf("%t%") != -1)
                {
                    filename = filename.Replace("%t%", tag[i].Track.Count != 0
                                                                    ? tag[i].Track[0].ToString()
                                                                    : string.Empty);
                }

                while (filename.IndexOf("%y%") != -1)
                {
                    filename = filename.Replace("%y%", tag[i].Year);
                }
                while (filename.IndexOf("%an%") != -1)
                {
                    filename = filename.Replace("%an%", tag[i].Album);
                }
                while (filename.IndexOf("%fn%") != -1)
                {
                    filename = filename.Replace("%fn%", Path.GetFileNameWithoutExtension(data[0].Path));
                }

                data.RemoveAt(0);

                var dir = Path.GetDirectoryName(datatmp[i].Tag.Path);
                var ext = Path.GetExtension(datatmp[i].Path);

                var path = Path.GetFullPath(datatmp[i].Tag.Path);

                char[] chars = Path.GetInvalidFileNameChars();
                for (int q = 0; q < filename.Length; q++)
                {
                    for (int w = 0; w < chars.Length; w++)
                    {
                        if (filename[q] == chars[w])
                        {
                            filename = filename.Remove(q, 1);
                            break;
                        }
                    }
                }
                filename = dir + @"\" + filename + ext;
                try
                {
                    if (Path.GetFullPath(filename) != Path.GetFullPath(path))
                    {
                        if (File.Exists(filename))
                        {
                            File.Delete(filename);
                        }
                        File.Move(path, filename);
                    }
                }
                catch { }
                
                data.Add(new AutoModeModel(filename, data.Count + 1));
            }
        }

        // run 정보와 현재 파일 상태, 태그정보
        public async Task<bool> Execute(int run, string resultPath, List<AutoModeModel> data, List<TagInfo> tag, ConvCheckModel preset)
        {
            await Task.Run(async () =>
            {
                cue.List().Clear();
                audioconv.List().Clear();
                audiotag.List().Clear();

                if ((run & 1) == 1)
                {
                    Title = Global.Language.AutoCueSplit;
                    CueSplit(data, resultPath);
                    if ((run & 4) == 4)
                    {
                        Title = Global.Language.AutoCueTag;
                        Tagging(data, tag, !Global.Setting.CueTagging);
                    }
                }

                if ((run & 2) == 2)
                {
                    Title = Global.Language.AutoAudioConv;
                    var result = await Conv(data, resultPath, preset);
                    if ((run & 4) == 4)
                    {
                        Title = Global.Language.AutoAudioTag;
                        Tagging(data, tag);
                    }
                }

                if (run == 4)
                {
                    Title = Global.Language.AutoTag;
                    Tagging(data, tag);
                }
                Title = Global.Language.AutoComplete;
            });
            return true;
        }




        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

    }
}
