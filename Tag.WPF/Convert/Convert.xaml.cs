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

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is PresetModel)
            {
                viewModel.Change((e.NewValue as PresetModel).Index);
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
                        var q = new Core.Conv.ConvInfo
                        {
                            FilePath = path
                        };
                        q.ResultPath = q.Directory + "\\";
                        viewModel.AddFile(q);
                        break;
                }
            }
        }
        private void ItemDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        DialogSession Session = null;
        void OpenEventHandler(object sender, DialogOpenedEventArgs envetArgs)
        {
            Session = envetArgs.Session;
        }

        private void CloseEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (bool.Parse(eventArgs.Parameter.ToString()) == false) return;

            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler
            
            if (!Session.IsEnded)
            {
                Session.Close();
                Session = null;
            }

        }

        private async void Execute(object sender, RoutedEventArgs e)
        {

            var result = await DialogHost.Show(new ConvertStatus
            {
                Width=450,
                Height=400
            }, OpenEventHandler, CloseEventHandler);
        }

        private void OpenDialog(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
