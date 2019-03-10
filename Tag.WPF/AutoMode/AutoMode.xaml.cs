using System;
using System.Collections.Generic;
using System.IO;
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
using ToastNotifications.Messages;

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
        int GetNum(CheckBox box)
        {
            return box.IsChecked == true ? (int)box.Tag : 0;
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            int run = GetNum(CheckBoxCueSplit) | GetNum(CheckBoxConv) | GetNum(CheckBoxTagging);

            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Directory.Exists(dialog.SelectedPath) == false)
                {
                    Execute_Click(sender, e);
                }
                else
                {
                    if (dialog.SelectedPath != string.Empty)
                    {
                        viewModel.Execute(run, dialog.SelectedPath + @"\");
                    }
                }
            }
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
                    viewModel.LabelVisibility = Visibility.Hidden;
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
