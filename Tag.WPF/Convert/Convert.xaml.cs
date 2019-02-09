using ATL.CatalogDataReaders;
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
            DataContext = viewModel = new ConvertViewModel();
            InitializeComponent();
        }

        AudioType GetAudioType(string extension)
        {
            AudioType Type = AudioType.NONE;

            switch (extension)
            {
                case ".wav":
                    Type = AudioType.WAV;
                    break;
                case ".flac":
                    Type = AudioType.FLAC;
                    break;
                case ".mp3":
                    Type = AudioType.NONE;
                    break;
                default:
                    Type = AudioType.NONE;
                    break;
            }

            return Type;
        }

        private void ItemDragDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path in items)
            {
                var q = new Core.Conv.ConvInfo
                {
                    FilePath = path
                };
                q.Type = GetAudioType(q.Extension);

                if (q.Type != AudioType.NONE)
                {
                    q.ResultPath = q.Directory + "\\" + q.FileName + ".mp3";
                    viewModel.ConvInfos.Add(q);
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
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "Wave (*.wav)|*.wav|Flac (*.flac)|*.flac",
                Multiselect = true
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var name in dialog.FileNames)
                {
                    if (name != null || name != string.Empty)
                    {
                        var q = new Core.Conv.ConvInfo
                        {
                            FilePath = name
                        };
                        q.Type = GetAudioType(q.Extension);
                        if (q.Type != AudioType.NONE)
                        {
                            q.ResultPath = q.Directory + "\\" + q.FileName + ".mp3";
                            viewModel.ConvInfos.Add(q);
                        }
                    }
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.Checked(int.Parse((e.Source as RadioButton).Tag.ToString()));
        }

        private void List_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                var listView = (sender is ListView) ? null : (sender as ListView);

                if (listView != null || listView.SelectedIndex != -1)
                {
                    viewModel.ConvInfos.RemoveAt(listView.SelectedIndex);
                }
            }
        }
    }
}
