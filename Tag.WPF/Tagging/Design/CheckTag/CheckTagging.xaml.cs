using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Tag.Core.Cue;
using Tag.Core.Tagging;
using Tag.Setting;

namespace Tag.WPF
{
    /// <summary>
    /// CheckTagging.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CheckTagging : UserControl
    {
        CheckTaggingViewModel viewModel;
        public CheckTagging(ObservableCollection<TaggingModel> user)
        {
            InitializeComponent();
            DataContext = viewModel = new CheckTaggingViewModel();
            viewModel.SetValue(user);
        }

        public void SetTagValue(Dictionary<string, List<TagInfo>> info)
        {
            viewModel.SetTagValue(info);
        }

        private async void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(false, null);
            await Task.Delay(500);
        }

        private async void Yes_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveTag();
            DialogHost.CloseDialogCommand.Execute(true, null);
            await Task.Delay(500);
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            int index = UserListView.SelectedIndex;
            if (index != -1 && index != 0)
            {
                viewModel.UpClick(index);
                UserListView.SelectedIndex = index - 1;
            }
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            int index = UserListView.SelectedIndex;
            if (index != -1 && index != UserListView.Items.Count - 1)
            {
                viewModel.DownClick(index);
                UserListView.SelectedIndex = index + 1;
            }
        }

        private void ImageDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            Image(items);
        }

        private void ItemDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void ImageChange(object sender, RoutedEventArgs e)
        {
            if (viewModel?.CoverImage != null)
            {
                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog
                {
                    Multiselect = true
                };

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Image(dialog.FileNames);
                }
            }
        }

        void Image(params string[] items)
        {
            var tag = viewModel.CoverImage;
            tag.Clear();

            foreach (var item in items)
            {
                try
                {
                    var pic = new TagLib.Picture(item);
                    tag.Add(pic);
                    viewModel.CoverImage = tag;
                }
                catch { }
            }
        }

        private void ImageDelete(object sender, RoutedEventArgs e)
        {
            if (viewModel?.CoverImage != null)
            {
                var tag = viewModel.CoverImage;
                tag.Clear();
                viewModel.CoverImage = tag;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                var k = (sender as TextBox);
                viewModel.ChangeText(k.Name, k.Text);
            }
        }

        private void Remove_GetTag(object sender, KeyEventArgs e)
        {
            var list = (sender as ListView).SelectedItems;
            if (list == null || list.Count == 0)
            {
                return;
            }
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var items = new List<CheckBrainzModel>();
                foreach (var value in list)
                {
                    items.Add(value as CheckBrainzModel);
                }
                viewModel.RemoveGetTag(items);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            viewModel?.SetTagChange((string)(sender as Control)?.Tag);
        }
    }
}
