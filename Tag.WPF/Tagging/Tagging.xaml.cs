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
    /// Tagging.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Tagging : UserControl
    {
        TaggingViewModel viewModel;

        public Tagging()
        {
            InitializeComponent();
            DataContext = viewModel = new TaggingViewModel();
        }

        private void TagAllSave(object sender, RoutedEventArgs e)
        {
            viewModel.AllTagSave();
        }
        private void GetTagInfo(object sender, RoutedEventArgs e)
        {
            if (TagListView.Items.Count != 0)
            {
                viewModel.GetTagInfo(TagListView.SelectedIndex);
            }
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
                        viewModel.AddModel(path);
                        break;
                }
            }
        }
        private void ItemDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void ListView_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (TagListView.SelectedIndex != -1)
            {
                viewModel.SelectModel(TagListView.SelectedIndex);
            }
        }
    }
}
