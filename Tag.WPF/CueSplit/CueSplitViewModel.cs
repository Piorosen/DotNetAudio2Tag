
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tag.Setting;

namespace Tag.WPF
{
    public class CueSplitViewModel : INotifyPropertyChanged
    {
        public string AlbumTitle { get => Global.Language.CueAlbum + albumTitle; private set { albumTitle = value; OnPropertyChanged(); } }
        public string Barcode { get => Global.Language.CueBarcode + barcode; private set { barcode = value; OnPropertyChanged(); } }
        public string AvgBytePerSecond { get => Global.Language.CueAverage + avgBytePerSecond; private set { avgBytePerSecond = value; OnPropertyChanged(); } }
        public string Genre { get => Global.Language.CueGenre + genre; private set { genre = value; OnPropertyChanged(); } }

        public Visibility LabelVisibility { get => _labelVisibility; set { _labelVisibility = value; OnPropertyChanged(); } }


        int _TaskPercent = 0;
        public int TaskPercent
        {
            get { return _TaskPercent; }
            set
            {
                _TaskPercent = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CueSplitModel> Items { get; set; }

        private readonly Tag.Core.Cue.CueSpliter cueSpliter;
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        

        public CueSplitViewModel()
        {
            Items = new ObservableCollection<CueSplitModel>();
            cueSpliter = new Core.Cue.CueSpliter();
            Items.Add(new CueSplitModel
            {
                Title = Global.Language.Title,
                Artist = Global.Language.Artist,
                DurationMS = -1,
                TimeOffSet = -1
            });
            Global.DialogIdentifier.PropertyChanged += DialogIdentifier_PropertyChanged;
        }

        private void DialogIdentifier_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Global.DialogIdentifier.LangChange))
            {
                Items.RemoveAt(0);
                Items.Insert(0, new CueSplitModel
                {
                    Title = Global.Language.Title,
                    Artist = Global.Language.Artist,
                    DurationMS = -1,
                    TimeOffSet = -1
                });

                OnPropertyChanged(nameof(AlbumTitle));
                OnPropertyChanged(nameof(Barcode));
                OnPropertyChanged(nameof(AvgBytePerSecond));
                OnPropertyChanged(nameof(Genre));
            }
        }

        void Change(int index, CueSplitModel newdata)
        {
            Items.RemoveAt(index);
            Items.Insert(index, newdata);


        }

        public void Click(int index)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                var data = Items[i];
                if (i == index)
                {
                    data.IsSelect = true;
                }
                else
                {
                    data = Items[i];
                    data.IsSelect = false;
                }
                Change(i, data);
            }
        }

        public void AddFile(string filePath)
        {
            Items.Clear();
            cueSpliter.List().Clear();
            cueSpliter.AddFile(filePath);
            int index = 1;

            AlbumTitle = cueSpliter[0].Track[0].Album;
            Barcode = cueSpliter[0].Barcode ?? cueSpliter[0].REM.DiscId;
            AvgBytePerSecond = cueSpliter[0].WaveFormat.AverageBytesPerSecond.ToString();
            Genre = cueSpliter[0].REM.Genre;

            Items.Add(new CueSplitModel
            {
                Title = Global.Language.Title,
                Artist = Global.Language.Artist,
                DurationMS = -1,
                TimeOffSet = -1
            });

            foreach (var value in cueSpliter[0].Track)
            {
                CueSplitModel model = new CueSplitModel(value)
                {
                    Index = index
                };
                Items.Add(model);
                index++;
            }
            TaskPercent = 0;
            LabelVisibility = Visibility.Hidden;
        }

        delegate bool Test();
        bool isTask = false;
        private string albumTitle;
        private string barcode;
        private string avgBytePerSecond;
        private string genre;
        private Visibility _labelVisibility;

        public async Task Execute(Control control, string SavePath = null)
        {
            await Task.Factory.StartNew(() =>
            {
                control.Dispatcher.Invoke(() =>
                {
                    Global.Setting.ExecuteProgram = false;
                });
                if (isTask == false)
                {
                    if (SavePath != null)
                    {
                        foreach (var value in cueSpliter.List())
                        {
                            value.SavePath = SavePath;
                        }
                    }

                    TaskPercent = 0;
                    isTask = true;
                    foreach (var value in cueSpliter.Execute())
                    {
                        if (TaskPercent < value)
                        {
                            TaskPercent = value;
                            control.Dispatcher.Invoke(() =>
                            {
                                control.UpdateLayout();
                            });
                        }
                    }
                    isTask = false;
                }
                control.Dispatcher.Invoke(() =>
                {
                    Global.Setting.ExecuteProgram = true;
                });
            }).ConfigureAwait(true);

        }
    }
}
