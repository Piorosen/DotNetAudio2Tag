using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core
{
    interface ICore<T, K>
    {
        bool Delete(int at);
        bool Delete(T remove);

        bool AddFile(K path);

        List<T> List();
        IEnumerable<int> Execute();
    }
}
