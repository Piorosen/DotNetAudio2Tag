using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    public partial class LameMode : UserControl
    {
        public LameMode()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(false, (sender as Button)?.CommandTarget);
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute((TextFilePath.Text, TextEncode.Text), (sender as Button)?.CommandTarget);
        }

        private void CodeCheck_Click(object sender, RoutedEventArgs e)
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

            proc.Start();
            
            var err = proc.StandardError.ReadToEnd();
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
