using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using Tag.Setting;

namespace Tag.WPF
{
    /// <summary>
    /// CueSplit.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingPage : UserControl
    {
        SettingViewModel  viewModel;

        public SettingPage()
        {
            InitializeComponent();
            DataContext = viewModel = new SettingViewModel();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.ChangeLang((sender as Control).Tag as string);
        }

        private void Update(object sender, MouseEventArgs e)
        {
            viewModel.CapacityUpdate();
        }

        private void CacheRemove(object sender, RoutedEventArgs e)
        {
            viewModel.CacheRemove();
        }

        private void AllSave(object sender, RoutedEventArgs e)
        {
            Global.Setting.Save();
        }
    }
}
