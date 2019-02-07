using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.WPF
{
    class ConvertStatusViewModel
    {

        public ObservableCollection<ConvertStatusModel> Items { get; set; }

        public ConvertStatusViewModel()
        {
            Items = new ObservableCollection<ConvertStatusModel>();
            Items.Add(new ConvertStatusModel
            {
                Index = 0,
                Title = "123",
                Value = 0
            }); Items.Add(new ConvertStatusModel
            {
                Index = 0,
                Title = "122233",
                Value = 75
            }); Items.Add(new ConvertStatusModel
            {
                Index = 0,
                Title = "1212343",
                Value = 25
            }); Items.Add(new ConvertStatusModel
            {
                Index = 0,
                Title = "1231111",
                Value = 100
            });
        }

    }
}
