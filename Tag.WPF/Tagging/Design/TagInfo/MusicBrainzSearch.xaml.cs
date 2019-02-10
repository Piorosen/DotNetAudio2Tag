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
using Tag.Core.Tagging;

namespace Tag.WPF
{
    /// <summary>
    /// MusicBrainzSearch.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MusicBrainzSearch : UserControl
    {
        MusicBrainzSearchViewModel viewModel;

        public MusicBrainzSearch(TagInfo info)
        {
            InitializeComponent();
            DataContext = viewModel = new MusicBrainzSearchViewModel();
            viewModel.SearchAlbum(info);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (sender is ListView) ? sender as ListView : null;
            Console.WriteLine(sender.GetType());
            if (listView != null && listView?.SelectedIndex != -1)
            {
                viewModel.SelectItem(listView.SelectedIndex, this);
            }
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(true, null);
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(false, null);
        }
    }
}
