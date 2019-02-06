using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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
        private void ItemsControl_DragEnter(object sender, System.Windows.DragEventArgs e)
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

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Items.Add(new CueSplitModel { Artist = "123123" });
        }
    }
}
