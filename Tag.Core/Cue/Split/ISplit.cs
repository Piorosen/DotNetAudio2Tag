using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Cue.Split
{
    interface ISplit
    {
        IEnumerable<int> Execute(CueInfo info);
    }
}
