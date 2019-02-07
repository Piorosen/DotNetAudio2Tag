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
    }
}
