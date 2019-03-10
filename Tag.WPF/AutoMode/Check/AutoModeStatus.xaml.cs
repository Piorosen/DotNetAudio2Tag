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
using Tag.Core.Tagging;

namespace Tag.WPF
{
    /// <summary>
    /// AutoModeStatus.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AutoModeStatus : UserControl
    {
        AutoModeStatusViewModel viewModel;
        List<TagInfo> tag = new List<TagInfo>();
        List<AutoModeModel> data;
        string result;
        int run;
        ConvCheckModel preset;

        public AutoModeStatus(int run, string result, List<AutoModeModel> data, List<TagInfo> tag, ConvCheckModel preset)
        {
            InitializeComponent();
            DataContext = viewModel = new AutoModeStatusViewModel();
            this.data = data;
            this.run = run;
            this.result = result;
            this.preset = preset;
        }
        public void Execute()
        {
            viewModel.Execute(run, result,  data, tag, preset);
        }


    }
}
