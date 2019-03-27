using MaterialDesignThemes.Wpf;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tag.Setting;

namespace Tag.WPF
{
    /// <summary>
    /// LameMode.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FFMpegMode : UserControl, INotifyPropertyChanged
    {
        private bool _buttonEnable = true;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        private void DialogIdentifier_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Global.DialogIdentifier.CodecEnable))
            {
                ButtonEnableA = Global.DialogIdentifier.CodecEnable;
            }
        }

        public bool ButtonEnableA { get => _buttonEnable; set { _buttonEnable = value; OnPropertyChanged(); } }

        public FFMpegMode()
        {
            InitializeComponent();
            Global.DialogIdentifier.PropertyChanged += DialogIdentifier_PropertyChanged;
        }
        
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Global.DialogIdentifier.ConvertEnable = true;
            DialogHost.CloseDialogCommand.Execute(false, (sender as Button)?.CommandTarget);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Global.DialogIdentifier.ConvertEnable = true;
            DialogHost.CloseDialogCommand.Execute(true, (sender as Button)?.CommandTarget);
            Global.Setting.Save();
        }


        private async void CodeCheck_Click(object sender, RoutedEventArgs e)
        {
            string arg = Global.Setting.FFMpegEncode;
            AudioFileReader afr = new AudioFileReader(Global.Resource.LameDummy + ".wav");
            afr.Close();
            while (arg.IndexOf("%fn%") != -1)
            {
                arg = arg.Replace("%fn%", Global.Resource.LameDummy + ".wav");
            }
            while (arg.IndexOf("%bit%") != -1)
            {
                arg = arg.Replace("%bit%", afr.WaveFormat.BitsPerSample.ToString());
            }
            while (arg.IndexOf("%rate%") != -1)
            {
                arg = arg.Replace("%rate%", afr.WaveFormat.SampleRate.ToString());
            }
            while (arg.IndexOf("%outputfn%") != -1)
            {
                arg = arg.Replace("%outputfn%", $"{Global.FilePath.CachePath}{System.IO.Path.GetRandomFileName()}");
            }

            var proc = new Process
            {
                StartInfo =
                {
                    FileName = Global.Setting.FFMpegPath,
                    Arguments = arg,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };

            string err = string.Empty;
            try
            {
                proc.Start();
                err = proc.StandardError.ReadToEnd();
            }
            catch { err = e.ToString(); }

            Global.DialogIdentifier.CodecEnable = false;
            await DialogHost.Show(new FFMpegTestCode(err), Global.IsAutoMode
                ? Global.DialogIdentifier.AutoCodecCodeTest
                : Global.DialogIdentifier.CodecCodeTest);

        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "Execute (*.exe)|*.exe",
                Multiselect = false
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(dialog.FileName) == false)
                {
                    Find_Click(sender, e);
                }
                else
                {
                    if (dialog.FileName != string.Empty)
                    {
                        Global.Setting.FFMpegPath = dialog.FileName;
                    }
                }
            }
        }
    }
}
