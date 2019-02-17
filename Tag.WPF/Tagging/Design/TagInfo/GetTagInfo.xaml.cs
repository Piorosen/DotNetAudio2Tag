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

        void OpenEvent(object sender, DialogOpenedEventArgs e)
        {
            view.Search(SearchInfo);
        }
        MusicBrainzSearch view;
        private async void MusicBrainz_Search(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(false, null);

            view = new MusicBrainzSearch(userinfo)
            {
                Width = 800,
                Height = 280,

            };
            await Task.Delay(500);

            if (Setting.Global.DialogCheck == false)
            {
                Setting.Global.DialogCheck = true;
                var t = await DialogHost.Show(view, OpenEvent);
                Setting.Global.DialogCheck = false;
            }

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

            if (Setting.Global.DialogCheck == false)
            {
                Setting.Global.DialogCheck = true;
                await DialogHost.Show(view);
                Setting.Global.DialogCheck = false;
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                if (Setting.Global.DialogCheck == true)
                {
                    DialogHost.CloseDialogCommand.Execute(false, null);
                }
            }
        }
    }
}
