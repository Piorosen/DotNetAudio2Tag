using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core
{
    interface ICore<T>
    {

        bool Delete(int at);
        bool Delete(T item);

        IEnumerable<int> Execute();

    }
}
