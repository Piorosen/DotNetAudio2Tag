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
        
        

        public void SetTagValue(List<TagInfo> info)
        {
            viewModel.SetTagValue(info);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(false, null);
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveTag();
            DialogHost.CloseDialogCommand.Execute(true, null);
        }
    }
}
