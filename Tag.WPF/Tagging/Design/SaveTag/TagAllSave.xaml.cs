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

namespace Tag.WPF
{
    /// <summary>
    /// TagAllSave.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TagAllSave : UserControl
    {
        public TagAllSave()
        {
            InitializeComponent();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Y)
            {
                DialogHost.CloseDialogCommand.Execute(true, null);
            }
            else if (e.Key == Key.N)
            {
                DialogHost.CloseDialogCommand.Execute(false, null);
            }
            else if (e.Key == Key.C)
            {
                DialogHost.CloseDialogCommand.Execute(false, null);
            }
        }
    }
}
