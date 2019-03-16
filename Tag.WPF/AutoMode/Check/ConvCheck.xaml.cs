using MaterialDesignThemes.Wpf;
using NAudio.Lame;
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
using Tag.Core.Conv;
using Tag.Setting;

namespace Tag.WPF
{
    /// <summary>
    /// ConvCheck.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ConvCheck : UserControl
    {
        public ConvCheck()
        {
            InitializeComponent();
            ConvertMode = new List<PresetModel>
            {
                new PresetModel(LAMEPreset.ABR_320, ConvMode.NORMAL),
                new PresetModel(LAMEPreset.ABR_256, ConvMode.NORMAL),
                new PresetModel(LAMEPreset.ABR_128, ConvMode.NORMAL),
                new PresetModel(LAMEPreset.ABR_320, ConvMode.MYFLAC),
                new PresetModel(LAMEPreset.ABR_320, ConvMode.USER)
            };

        }

        public List<PresetModel> ConvertMode { get; set; }
        int Index = 0;
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Index = int.Parse((e.Source as RadioButton).Tag.ToString());
        }

        (string Path, string Format) Param = (Global.Setting.LamePath, $"{Global.Setting.LameEncode} %File% %SaveFile%");

        private async void UserMode_Click(object sender, RoutedEventArgs e)
        {
            var Content = new LameMode
            {
                TextEncode =
                {
                    Text = Param.Format.Replace(" %File% %SaveFile%", string.Empty)
                },
                TextFilePath =
                {
                    Text = Param.Path
                }
            };

            Global.DialogIdentifier.ConvertEnable = false;
            var result = (ValueTuple<string, string>)(await DialogHost.Show(Content, Global.DialogIdentifier.AutoConvertUserMode));

            Param = result == (string.Empty, string.Empty) ? Param : result;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(new ConvCheckModel { preset = ConvertMode[Index], Param = Param }, (sender as Button).CommandTarget);
        }
    }
}
