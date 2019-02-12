using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Tag.Setting;

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

        private void Tag_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.Select == true)
            {
                return;
            }
            if (sender is TextBox)
            {
                var k = (sender as TextBox);
                viewModel.ChangeText(k.Name, k.Text);
            }
        }

        private void TagListView_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = e.OriginalSource as GridViewColumnHeader;
            ListView list = e.Source as ListView;
            var view = list.View as GridView;

            int result = 0;
            for (int i = 0; i < view.Columns.Count; i++)
            {
                if (view.Columns[i].Header == column.Content)
                {
                    result = i;
                    break;
                }
            }

            if (result == 0)
            {
                viewModel.Items.ToList().Sort((a, b) =>
                {
                    return a.FileName.CompareTo(b);
                });
            }
            else if (result == 1)
            {
                viewModel.Items.ToList().Sort((a, b) =>
                {
                    return a.TagInfo.Title.CompareTo(b.TagInfo.Title);
                });
            }
            else if (result == 2)
            {
                viewModel.Items.ToList().Sort((a, b) =>
                {
                    return a.TagInfo.Path.CompareTo(b.TagInfo.Path);
                });
            }
            else if (result == 3)
            {

            }
            else if (result == 4)
            {

            }

            var name = column.Content;
        }
    }
}
