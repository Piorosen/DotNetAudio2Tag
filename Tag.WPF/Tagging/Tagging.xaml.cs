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
using Tag.Core.Tagging;
using Tag.Setting;
using ToastNotifications.Messages;

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
            if (viewModel.Items.Count == 0)
            {
                Application.notifier.ShowInformation(Global.Language.AutoFail);
                return;
            }
            viewModel.AllTagSave();
        }
        private void GetTagInfo(object sender, RoutedEventArgs e)
        {
            if (TagListView.Items.Count != 0)
            {
                viewModel.GetTagInfo(TagListView.SelectedIndex);
            }
            else
            {
                Application.notifier.ShowInformation(Global.Language.AutoFail);
            }
        }

        private void ItemDragDrop(object sender, DragEventArgs e)
        {
            viewModel.ClearFile();

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
            currentSort = -1;
            ItemsConvert(2);
        }

        private void List_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                var listView = (sender is ListView) ? (sender as ListView) : null;
                
                if (listView != null && listView.SelectedItems.Count != 0)
                {
                    var list = listView.SelectedItems.Cast<object>().ToList();
                    foreach (var value in list)
                    {
                        viewModel.RemoveFile(listView.Items.IndexOf(value));
                    }
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

        int currentSort = -1;
        private void TagListView_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = e.OriginalSource as GridViewColumnHeader;
            ListView list = e.Source as ListView;
            var view = list.View as GridView;

            int result = 0;
            try
            {
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    if (view.Columns[i].Header == column.Content)
                    {
                        result = i;
                        break;
                    }
                }
            }
            catch
            {
                return;
            }
            ItemsConvert(result);
        }

        void ItemsConvert(int result)
        {
            if (result == currentSort)
            {
                currentSort = -1;
            }
            else
            {
                currentSort = result;
            }

            var viewModelItem = viewModel.Items.ToList();


            if (result == 0)
            {
                viewModelItem.Sort((a, b) =>
                {
                    return a.FileName.CompareTo(b.FileName);
                });
            }
            else if (result == 1)
            {
                viewModelItem.Sort((a, b) =>
                {
                    return a?.TagInfo?.Title?.CompareTo(b?.TagInfo?.Title) ?? 1;
                });
            }
            else if (result == 2)
            {
                viewModelItem.Sort((a, b) =>
                {
                    return a?.TagInfo?.Path?.CompareTo(b?.TagInfo?.Path) ?? 1;
                });
            }
            else if (result == 3)
            {

            }
            else if (result == 4)
            {
                viewModelItem.Sort((a, b) =>
                {
                    if (a.TagInfo.Artist.Count != b.TagInfo.Artist.Count)
                    {
                        return a?.TagInfo?.Artist.Count - b?.TagInfo?.Artist?.Count ?? 1;
                    }
                    else
                    {
                        for (int w = 0; w < a.TagInfo.Artist.Count; w++)
                        {
                            var r = a?.TagInfo?.Artist[w]?.CompareTo(b?.TagInfo?.Artist[w]);
                            if (r != 0)
                            {
                                return r ?? 1;
                            }
                        }
                    }
                    return 0;
                });
            }
            else if (result == 5)
            {
                viewModelItem.Sort((a, b) =>
                {
                    if (a.TagInfo.AlbumArtist.Count != b.TagInfo.AlbumArtist.Count)
                    {
                        return a.TagInfo.AlbumArtist.Count - b.TagInfo.AlbumArtist.Count;
                    }
                    else
                    {
                        for (int w = 0; w < a.TagInfo.AlbumArtist.Count; w++)
                        {
                            var r = a.TagInfo.AlbumArtist[w]?.CompareTo(b.TagInfo.AlbumArtist[w]);
                            if (r != 0)
                            {
                                return r ?? 1;
                            }
                        }
                    }
                    return 0;
                });
            }
            else if (result == 6)
            {
                viewModelItem.Sort((a, b) =>
                {
                    return a.TagInfo.Album?.CompareTo(b.TagInfo.Album) ?? 1;
                });
            }
            else if (result == 7)
            {
                viewModelItem.Sort((a, b) =>
                {
                    if (a.TagInfo.Track?.Count == null)
                    {
                        return 1;
                    }
                    if (b.TagInfo.Track?.Count == null)
                    {
                        return 1;
                    }

                    if (a.TagInfo.Track.Count != b.TagInfo.Track.Count)
                    {
                        return a.TagInfo.Track.Count - b.TagInfo.Track.Count;
                    }
                    else
                    {
                        for (int w = 0; w < a.TagInfo.Track.Count; w++)
                        {
                            var r = a.TagInfo.Track[w].CompareTo(b.TagInfo.Track[w]);
                            if (r != 0)
                            {
                                return r;
                            }
                        }
                    }
                    return 0;
                });
            }
            else if (result == 8)
            {
                viewModelItem.Sort((a, b) =>
                {
                    return a.TagInfo.DiscNum?.CompareTo(b.TagInfo.DiscNum) ?? 1;
                });
            }
            else if (result == 9)
            {
                viewModelItem.Sort((a, b) =>
                {
                    return a.TagInfo.Year?.CompareTo(b.TagInfo.Year) ?? 1;
                });
            }
            else if (result == 10)
            {
                viewModelItem.Sort((a, b) =>
                {
                    if (a.TagInfo.Genre.Count != b.TagInfo.Genre.Count)
                    {
                        return a.TagInfo.Genre.Count - b.TagInfo.Genre.Count;
                    }
                    else
                    {
                        for (int w = 0; w < a.TagInfo.Genre.Count; w++)
                        {
                            var r = a.TagInfo.Genre[w]?.CompareTo(b.TagInfo.Genre[w]);
                            if (r != 0)
                            {
                                return r ?? 1;
                            }
                        }
                    }
                    return 0;
                });
            }
            else if (result == 11)
            {
                viewModelItem.Sort((a, b) =>
                {
                    return a.TagInfo.Year?.CompareTo(b.TagInfo.Comment) ?? 1;
                });
            }
            else if (result == 12)
            {
                viewModelItem.Sort((a, b) =>
                {
                    return a.WaveFormat.Bitrate.CompareTo(b.WaveFormat.Bitrate);
                });
            }
            else if (result == 13)
            {
                viewModelItem.Sort((a, b) =>
                {
                    return a.WaveFormat.Length.CompareTo(b.WaveFormat.Length);
                });
            }
            else if (result == 14)
            {
                viewModelItem.Sort((a, b) =>
                {
                    return a.WaveFormat.Channel.CompareTo(b.WaveFormat.Channel);
                });
            }

            viewModel.Items.Clear();

            for (int w = 0; w < viewModelItem.Count; w++)
            {
                viewModel.Items.Add(currentSort == -1
                    ? viewModelItem[viewModelItem.Count - w - 1]
                    : viewModelItem[w]);
            }
        }

        private void ImageDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            Image(items);
        }

        private void ImageChange(object sender, RoutedEventArgs e)
        {
            if (viewModel?.SelectItem?.TagInfo?.Image != null)
            {
                viewModel.SelectItem.TagInfo.Image.Clear();
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
            var tag = viewModel.SelectItem.TagInfo;
            tag.Image.Clear();

            foreach (var item in items)
            {
                try
                {
                    var pic = new TagLib.Picture(item);
                    tag.Image.Add(pic);
                    viewModel.SelectItem.TagInfo = tag;
                }
                catch { }
            }
        }

        private void ImageDelete(object sender, RoutedEventArgs e)
        {
            if (viewModel?.SelectItem?.TagInfo != null)
            {
                var tag = viewModel.SelectItem.TagInfo;
                tag.Image.Clear();
                viewModel.SelectItem.TagInfo = tag;
            }
        }

        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                Global.DialogIdentifier.TaggingEnable = true;
                DialogHost.CloseDialogCommand.Execute(false, null);
            }
        }
    }
}
