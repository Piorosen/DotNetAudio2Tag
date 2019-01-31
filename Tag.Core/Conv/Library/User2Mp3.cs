using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Conv.Library
{
    class User2Mp3 : IConv
    {
        public IEnumerable<int> Execute(ConvInfo info)
        {
            try
            {
                String.Format(info.Format, info.Parameter);
            }
            catch { }

            try
            {
                info.Format.Replace("%File%", info.FilePath);
            }
            catch { }

            try
            {
                info.Format.Replace("%SaveFile%", info.ResultPath);
            }
            catch { }

            bool result = false;
            try
            {
                Process.Start(info.Source, info.Format);
                result = true;
            }
            catch
            {
                result = false;
            }

            yield return result ? 100 : 0;
        }
    }
}
