using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Tag.WPF
{
    /// <summary>
    /// Convert.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Convert : UserControl
    {
        ConvertViewModel viewModel;
        public Convert()
        {
            InitializeComponent();
            DataContext = viewModel = new ConvertViewModel();
        }

        private void ItemDragDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path in items)
            {
                var t = Path.GetExtension(path).ToLower();
                switch (t)
                {
                    case ".wav":
                    case ".flac":
                    case ".mp3":
                        var q = new Core.Conv.ConvInfo
                        {
                            FilePath = path
                        };
                        q.ResultPath = q.Directory + "\\";
                        viewModel.ConvInfos.Add(q);
                        break;
                }
            }
        }
        private void ItemDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }



        private async void Execute(object sender, RoutedEventArgs e)
        {
            await viewModel.Execute();
        }

        private void OpenDialog(object sender, RoutedEventArgs e)
        {
            
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.Checked(int.Parse((e.Source as RadioButton).Tag.ToString()));
        }
    }
}
