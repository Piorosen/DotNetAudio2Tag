using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core
{
    interface ITagger
    {
        List<TagInfo> GetTagInfo(Core.CueData info);
    }
}
