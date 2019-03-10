using ATL.CatalogDataReaders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                item.SavePath = resultPath + @"\Cue\";
            }

            foreach (var value in cue.Execute())
            {
                Value = value;
            }
            data.Clear();
            for (int i = 0; i < cue[0].Track.Count; i++)
            {
                data.Add(new AutoModeModel(cue[0].SavePath + $"{cue[0].Track[i].Track}. " + cue[0].Track[i].Title +
                    (cue[0].AudioType == AudioType.WAV ? ".wav" : ".flac")));
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
                    ResultPath = resultPath + @"\Conv\",
                    Source = preset.Param.Path,
                    Format = preset.Param.Format
                });
            }
            return await audioconv.Execute(preset.preset.ConvMode, 4, resultPath + @"\Conv\");
        }
        void Tagging(List<AutoModeModel> data, List<TagInfo> tag)
        {
            for (int i = 0; i < data.Count; i++)
            {
                string file = data[i].Path;
                audiotag.AddFile(tag[i], file);
            }

            foreach (var value in audiotag.Execute())
            {
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
                    CueSplit(data, resultPath);

                    if ((run & 4) == 4)
                    {
                        Tagging(data, tag);
                    }
                }

                if ((run & 2) == 2)
                {
                    var result = await Conv(data, resultPath, preset);

                    if ((run & 4) == 4)
                    {
                        Tagging(data, tag);
                    }
                }

                if (run == 4)
                {
                    Tagging(data, tag);
                }
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
