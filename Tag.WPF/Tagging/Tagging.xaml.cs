using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace System.Collections.Generic
{
    public static class ExtenList
    {
        
    }
}
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
        


        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
            Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");
        }

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {

        }

        private async void TagAllSave(object sender, RoutedEventArgs e)
        {
            var view = new TagAllSave();
            view.Width = 300;
            view.Height = 100;
            var result = await DialogHost.Show(view, ExtendedOpenedEventHandler, ExtendedClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
        private async void GetTagInfo(object sender, RoutedEventArgs e)
        {
            var view = new GetTagInfo
            {

            };
            var result = await DialogHost.Show(view, ExtendedOpenedEventHandler, ExtendedClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
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
            var listView = (sender is ListView) ? sender as ListView : null ;
            Console.WriteLine(sender.GetType());
            if (listView != null && listView?.SelectedIndex != -1)
            {
                viewModel.SelectModel(listView.SelectedIndex);
            }
        }
    }
}
