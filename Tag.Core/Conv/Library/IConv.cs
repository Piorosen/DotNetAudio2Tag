using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Conv
{
    interface IConv
    {
        IEnumerable<int> Execute(ConvInfo info);

    }
}
