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
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                    }
                };
                try
                {
                    proc.Start();
                }
                catch { }
            }
            catch
            {
            }

            while (!proc.StandardError.EndOfStream)
            {
                var err = proc.StandardError.ReadLine();
                var l = err.Split('%');
                if (l.Length != 1)
                {
                    var t = l[0].Split('(');
                    if (t.Length != 1)
                    {
                        if (int.TryParse(t[1].Trim(), out int r))
                        {
                            yield return r;
                        }
                    }
                }
            }
            yield return 100;
        }
    }
}
