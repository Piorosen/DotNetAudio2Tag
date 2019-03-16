using MaterialDesignThemes.Wpf;
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
    public partial class LameMode : UserControl, INotifyPropertyChanged
    {
        private bool _buttonEnable = true;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        private void DialogIdentifier_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Global.DialogIdentifier.LameEnable))
            {
                ButtonEnableA = Global.DialogIdentifier.LameEnable;
            }
        }

        public bool ButtonEnableA { get => _buttonEnable; set { _buttonEnable = value; OnPropertyChanged(); } }
        public LameMode()
        {
            InitializeComponent();
            TextFilePath.Text = Global.Setting.LamePath;
            TextEncode.Text = Global.Setting.LameEncode;
            Global.DialogIdentifier.PropertyChanged += DialogIdentifier_PropertyChanged;
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Global.DialogIdentifier.ConvertEnable = true;
            DialogHost.CloseDialogCommand.Execute((string.Empty, string.Empty), (sender as Button)?.CommandTarget);
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Global.DialogIdentifier.ConvertEnable = true;
            Global.Setting.LamePath = TextFilePath.Text;
            Global.Setting.LameEncode = TextEncode.Text;
            DialogHost.CloseDialogCommand.Execute((TextFilePath.Text, TextEncode.Text + " %File% %SaveFile%"), (sender as Button)?.CommandTarget);
            Global.Setting.Save();
        }


        private async void CodeCheck_Click(object sender, RoutedEventArgs e)
        {

            var proc = new Process
            {
                StartInfo =
                {
                    FileName = TextFilePath.Text,
                    Arguments = TextEncode.Text + $" {Global.Resource.LameDummy} {Global.FilePath.CachePath}{System.IO.Path.GetRandomFileName()}",
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

            Global.DialogIdentifier.LameEnable = false;
            await DialogHost.Show(new LameTestCode(err), Global.IsAutoMode
                ? Global.DialogIdentifier.AutoLameCodeTest
                : Global.DialogIdentifier.LameCodeTest);

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
                        TextFilePath.Text = dialog.FileName;
                    }
                }
            }
        }
    }
}
