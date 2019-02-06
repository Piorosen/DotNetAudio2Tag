using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tag.WPF
{
    /// <summary>
    /// CueSplit.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CueSplit : UserControl
    {
        CueSplitViewModel viewModel;

        public CueSplit()
        {
            InitializeComponent();
            DataContext = viewModel = new CueSplitViewModel();
            

        }
        

        private void ItemDragDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path in items)
            {
                var t = Path.GetExtension(path).ToLower();
                if (t == ".cue")
                {
                    viewModel.AddFile(path);
                }
            }
        }
        private void ItemDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void ItemMouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;

        }

        private void ItemMouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 1)
            {
                if (sender is Border)
                {
                    viewModel.Click((int)(sender as Border).Tag - 1);
                }
            }
        }

        private void CueFileOpen(object sender, RoutedEventArgs e)
        {

        }
    }
}
