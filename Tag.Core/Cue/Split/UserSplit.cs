using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Cue.Split
{
    class UserSplit : ISplit
    {
        public void Setting(string fileName, string format, params object[] data)
        {
            var arg = string.Format(format, data);
            System.Diagnostics.Process.Start(fileName, arg);
        }

        public IEnumerable<int> Execute(CueInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
