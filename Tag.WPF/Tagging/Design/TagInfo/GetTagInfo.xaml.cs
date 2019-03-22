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
using Tag.Setting;

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

        void OpenBrainzEvent(object sender, DialogOpenedEventArgs e)
        {
            brainz.Search(SearchInfo);
            this.Width = 0;
            this.Height = 0;
        }
        void OpenVgmEvent(object sender, DialogOpenedEventArgs e)
        {
            vgm.Search(SearchInfo);
            this.Width = 0;
            this.Height = 0;
        }
        async void CloseEvent(object sender, DialogClosingEventArgs e)
        {
            this.Height = 150;
            this.Width = 200;
            if (e.Parameter is bool)
            {
                if ((bool)e.Parameter != false)
                {
                    await Task.Delay(500);
                    Global.DialogIdentifier.TaggingEnable = true;
                    DialogHost.CloseDialogCommand.Execute(e.Parameter, null);
                }
            }
            
        }

        MusicBrainzSearch brainz;
        VgmDbSearch vgm;

        private async void MusicBrainz_Search(object sender, RoutedEventArgs e)
        {
            brainz = new MusicBrainzSearch(userinfo);

            var result = await DialogHost.Show(brainz, Global.IsAutoMode
                ? Global.DialogIdentifier.AutoBrainzSearch
                : Global.DialogIdentifier.BrainzSearch, OpenBrainzEvent, CloseEvent);
            await Task.Delay(500);
        }

        private async void VGMDB_Search(object sender, RoutedEventArgs e)
        {
            vgm = new VgmDbSearch(userinfo)
            {
                Width = 800,
                Height = 280,
            };
            var result = await DialogHost.Show(vgm, Global.IsAutoMode
                ? Global.DialogIdentifier.AutoVgmSearch
                : Global.DialogIdentifier.VgmSearch, OpenVgmEvent, CloseEvent);
            await Task.Delay(500);
            
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                DialogHost.CloseDialogCommand.Execute(false, null);
                Global.DialogIdentifier.TaggingEnable = true;
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(false, null);
            Global.DialogIdentifier.TaggingEnable = true;
        }
    }
}
