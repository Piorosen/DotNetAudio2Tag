using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Tagging;

namespace Tag.WPF
{
    public class CheckTagInfoModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class CheckBrainzModel
    {
        public TagInfo Tag { get; set; }
        public string Track { get; set; }
        public string Title { get; set; }
        public string DiscNo { get; set; }
    }

    public class CheckUserModel
    {
        public int Id = 0;
        public string Length { get; set; }
        public string Title { get; set; }
        public int Track { get; set; }
    }
}
