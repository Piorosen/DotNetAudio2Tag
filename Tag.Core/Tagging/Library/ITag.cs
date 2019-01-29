using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Cue;

namespace Tag.Core.Tagging.Library
{
    interface ITag
    {
        List<TagInfo> GetTagInfo(CueInfo info);
    }
}
