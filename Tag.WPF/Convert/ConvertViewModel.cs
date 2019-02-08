
using MaterialDesignThemes.Wpf;
using NAudio.Lame;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Conv;

namespace Tag.WPF
{
    class ConvertViewModel
    {
        public List<PresetModel> ConvertMode { get; set; }
        public ObservableCollection<ConvInfo> ConvInfos { get; set; }
        int Index = 0;

        public void Checked(int index)
        {
            Index = index;
        }
        public ConvertViewModel()
        {
            ConvertMode = new List<PresetModel>
            {
                new PresetModel(LAMEPreset.ABR_320, ConvMode.MYWAV),
                new PresetModel(LAMEPreset.ABR_256, ConvMode.MYFLAC),
                new PresetModel(LAMEPreset.ABR_128, ConvMode.MYFLAC),
                new PresetModel(LAMEPreset.ABR_320, ConvMode.MYFLAC),
                new PresetModel(LAMEPreset.ABR_320, ConvMode.USER)
            };

            ConvInfos = new ObservableCollection<ConvInfo>();

        }

        DialogSession Session = null;
        void OpenEventHandler(object sender, DialogOpenedEventArgs envetArgs)
        {
            Session = envetArgs.Session;
        }

        private void CloseEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (bool.Parse(eventArgs.Parameter.ToString()) == false) return;

            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            if (!Session.IsEnded)
            {
                Session.Close();
                Session = null;
            }

        }

        public async Task<object> Execute()
        {
            var Content = new ConvertStatus
            {
                Width = 450,
                Height = 400,
            };
            foreach (var value in ConvInfos)
            {
                Content.Enqueue(new ConvertModel
                {
                    Info = value,
                    Title = value.FileNamePlus,
                    Value = 0
                });
            }
            Content.Execute(ConvertMode[Index]);
            var result = await DialogHost.Show(Content, OpenEventHandler, CloseEventHandler);

            return await Task.FromResult(result);
        }
    }
}
