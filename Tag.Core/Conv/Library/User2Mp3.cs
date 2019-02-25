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
                info.Format = String.Format(info.Format, info.Parameter);
            }
            catch { }
            try
            {
                info.Format = info.Format.Replace("%File%", $"\"{info.FilePath}\"");
            }
            catch
            {
            }
            try
            {
                info.Format = info.Format.Replace("%SaveFile%", $"\"{info.ResultPath}\"");
            }
            catch { }
            
            Process proc = new Process();
            try
            {
                proc = new Process
                {
                    StartInfo =
                    {
                        FileName = info.Source,
                        Arguments = info.Format,
                        CreateNoWindow = true,
                    }
                };
                proc.Start();
                proc.WaitForExit();
            }
            catch
            {
            }
            yield return 100;
        }
    }
}
