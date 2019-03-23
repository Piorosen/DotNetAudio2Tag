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
using Tag.Setting;
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
            if (viewModel.Items.Count == 0)
            {
                Application.notifier.ShowInformation(Global.Language.AutoFail);
                return;
            }
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            int run = GetNum(CheckBoxCueSplit) | GetNum(CheckBoxConv) | GetNum(CheckBoxTagging);
            var path = string.Empty;

            if (System.IO.Path.GetExtension(viewModel.Items[0].Path).ToLower() == ".cue")
            {
                if ((run & (int)AutoModeTag.CueSplit) != (int)AutoModeTag.CueSplit)
                {
                    Application.notifier.ShowError(Global.Language.AutoFail);
                    return;
                }
            }

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
                        path = dialog.SelectedPath + @"\";
                    }
                }
                viewModel.Execute(run, path);
            }
            else
            {
                Application.notifier.ShowError(Global.Language.AutoFail);
            }
        }

        private void ItemDragDrop(object sender, DragEventArgs e)
        {
            viewModel.Items.Clear();
            int cue = 0, other = 0;
            
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var value in items)
            {
                if (System.IO.Path.GetExtension(value).ToLower() == ".cue")
                {
                    cue++;
                }
                else
                {
                    other++;
                }
            }
            if (cue == 0 && other > 0)
            {

            }else if (cue == 1 && other == 0)
            {

            }
            else
            {
                return;
            }

            foreach (var path in items)
            {
                var t = System.IO.Path.GetExtension(path).ToLower();
                viewModel.LabelVisibility = Visibility.Hidden;
                viewModel.AddFile(path);
            }
        }
        private void ItemDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }
    }
}
