using MaterialDesignThemes.Wpf;
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
using Tag.Setting;

namespace Tag.WPF
{
    /// <summary>
    /// LameTestCode.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LameTestCode : UserControl
    {
        public LameTestCode(string text)
        {
            InitializeComponent();
            TextResult.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Global.DialogIdentifier.LameEnable = true;
            DialogHost.CloseDialogCommand.Execute(true, (sender as Button)?.CommandTarget);
        }
    }
}
