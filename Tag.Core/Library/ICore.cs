using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core
{
    interface ICore<T>
    {
        bool AddFile(T file);
        bool Delete(int at);
        bool Delete(T item);

        List<T> List();

        IEnumerable<int> Execute();


    }
}
