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
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public partial class GetTagInfo : UserControl
    {
        public GetTagInfo(TagInfo SearchInfo, ObservableCollection<TaggingModel> user)
        {
            InitializeComponent();
            this.SearchInfo = SearchInfo;
            this.userinfo = user;
        }

        TagInfo SearchInfo;
        ObservableCollection<TaggingModel> userinfo;

        private async void MusicBrainz_Search(object sender, RoutedEventArgs e)
        {
            var view = new MusicBrainzSearch(SearchInfo, userinfo)
            {
                Width=400,
                Height=250,
                
            };

            DialogHost.CloseDialogCommand.Execute(false, null);
            await Task.Delay(500);
            await DialogHost.Show(view);
        }

        private async void VGMDB_Search(object sender, RoutedEventArgs e)
        {
            var view = new VgmDbSearch
            {
                Width = 100,
                Height = 100
            };

            DialogHost.CloseDialogCommand.Execute(false, null);
            await Task.Delay(500);
            await DialogHost.Show(view);
        }
    }
}
