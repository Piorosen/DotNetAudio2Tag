using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.WPF
{
    public class CheckTagInfoModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class CheckBrainzModel
    {
        public uint Track { get; set; }
        public string Title { get; set; }
    }

    public class CheckUserModel
    {
        public int Id = 0;
        public string Length { get; set; }
        public string Title { get; set; }
        public int Track { get; set; }
    }
}
