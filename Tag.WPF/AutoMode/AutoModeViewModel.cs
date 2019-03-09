using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Setting;

namespace Tag.WPF
{
    enum AutoModeTag : int
    {
        CueSplit = 1 << 0,
        Conv = 1 << 1,
        Tagging = 1 << 2
    }
    class AutoModeViewModel
    {
        public ObservableCollection<CueSplitModel> Items { get; set; } = new ObservableCollection<CueSplitModel>();

        public void AddFile(string file)
        {

        }

        bool CheckCueSplit()
        {
            return true;
        }

        async Task<bool> CheckConv()
        {
            var check = new ConvCheck();
            var result = await DialogHost.Show(check, Global.DialogIdentifier.AutoModeCodec);

            return false;
        }

        bool CheckTagging()
        {
            return false;
        }

        async Task<bool> CheckMode(int run)
        {
            bool result = true;
            if ((run & (int)AutoModeTag.CueSplit) == 1)
            {
                result &= CheckCueSplit();
            }
            if ((run & (int)AutoModeTag.Conv) == 1)
            {
                result &= await CheckConv();
            }
            if ((run & (int)AutoModeTag.Tagging) == 1)
            {
                result &= CheckTagging();
            }
            return true;
        }

        

        public async void Execute(int run)
        {
            if (await CheckMode(run))
            {

            }
        }
    }
}
