using System;
using System.Collections.Generic;
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

namespace Tag.WPF
{
    /// <summary>
    /// AutoMode.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AutoMode : UserControl
    {
        AutoModeViewModel viewModel;
        public AutoMode()
        {
            InitializeComponent();
            DataContext = viewModel = new AutoModeViewModel();
            
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            int run = (int)CheckBoxCueSplit.Tag | (int)CheckBoxConv.Tag | (int)CheckBoxTagging.Tag;
            viewModel.Execute(run);
        }

        private void ItemDragDrop(object sender, DragEventArgs e)
        {
            viewModel.Items.Clear();
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var path in items)
            {
               // var t = Path.GetExtension(path).ToLower();
               // if (t == ".cue")
                {
                    viewModel.AddFile(path);
                }
            }
        }
        private void ItemDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }
    }
}
