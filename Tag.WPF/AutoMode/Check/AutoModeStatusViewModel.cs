using ATL.CatalogDataReaders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Conv;
using Tag.Core.Cue;
using Tag.Core.Tagging;

namespace Tag.WPF
{
    class AutoModeStatusViewModel : INotifyPropertyChanged
    {
        CueSpliter cue = new CueSpliter();
        AudioConverter audioconv = new AudioConverter();
        AudioTagging audiotag = new AudioTagging();

        public string Title { get; set; }
        public int Value { get; set; }

        // run 정보와 현재 파일 상태, 태그정보
        public async void Execute(int run, string resultPath, List<AutoModeModel> data, List<TagInfo> tag, ConvCheckModel preset)
        {
            cue.List().Clear();
            audioconv.List().Clear();
            audiotag.List().Clear();

            if ((run & 1) == 1)
            {
                cue.AddFile(data[0].Path);
                foreach (var item in cue.List())
                {
                    item.SavePath = resultPath + @"\Cue\";
                }
                Title = "Cue 분리 중";

                foreach (var value in cue.Execute())
                {
                    Value = value;
                }
            }
            if ((run & 4) == 4)
            {
                Title = "Cue 태깅 중";
                for (int i = 0; i < cue[0].Track.Count; i++)
                {
                    string file = cue[0].SavePath + $"{cue[0].Track[i].Track}. " + cue[0].Track[i].Title;
                    switch (cue[0].AudioType)
                    {
                        case AudioType.WAV:
                            file += ".wav";
                            break;
                        case AudioType.FLAC:
                            file += ".flac";
                            break;
                    }
                    audiotag.AddFile(file);
                }

                for (int i = 0; i < (audiotag.List().Count < tag.Count ? audiotag.List().Count : tag.Count); i++)
                {
                    audiotag.List()[i] = tag[i];
                }
            }

            if ((run & 2) == 2)
            {
                Title = "컨버팅 중";

                for (int i = 0; i < cue[0].Track.Count; i++)
                {
                    string file = cue[0].SavePath + $"{cue[0].Track[i].Track}. " + cue[0].Track[i].Title;
                    switch (cue[i].AudioType)
                    {
                        case AudioType.WAV:
                            file += ".wav";
                            break;
                        case AudioType.FLAC:
                            file += ".flac";
                            break;
                    }
                    audioconv.AddFile(new ConvInfo
                    {
                        FilePath = file,
                        Type = cue[i].AudioType,
                        ResultPath = resultPath + @"\Conv\",
                        Source = preset.Param.Path,
                        Format = preset.Param.Format
                    });
                    Dictionary<int, int> status = new Dictionary<int, int>();
                    audioconv.ChangeExecute += (s, e) =>
                    {
                        status[(e / 10000)] = e % 1000;
                        Value = status.Values.Sum() / audioconv.List().Count;
                    };
                    bool p = await audioconv.Execute(preset.preset.ConvMode, 4, resultPath + @"\Conv\");
                }
            }
            if ((run & 4) == 4)
            {
                Title = "음악 파일 태깅 중";
                audiotag.List().Clear();

                for (int i = 0; i < cue[0].Track.Count; i++)
                {
                    string file = cue[0].SavePath + $"{cue[0].Track[i].Track}. " + cue[0].Track[i].Title;
                    switch (cue[0].AudioType)
                    {
                        case AudioType.WAV:
                            file += ".wav";
                            break;
                        case AudioType.FLAC:
                            file += ".flac";
                            break;
                    }
                    audiotag.AddFile(file);
                }

                for (int i = 0; i < (audiotag.List().Count < tag.Count ? audiotag.List().Count : tag.Count); i++)
                {
                    audiotag.List()[i] = tag[i];
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

    }
}
