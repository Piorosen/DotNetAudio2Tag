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
using ToastNotifications.Messages;

namespace Tag.WPF
{
    /// <summary>
    /// ConvertStatus.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ConvertStatus : UserControl
    {
        ConvertStatusViewModel viewModel;

        public ConvertStatus()
        {
            InitializeComponent();
            DataContext = viewModel = new ConvertStatusViewModel(this);
            
        }
        public void Execute(PresetModel preset)
        {
            viewModel.Execute(preset);
        }
        public void Enqueue(ConvertModel info)
        {
            viewModel.Enqueue(info);
        }
    }
}
