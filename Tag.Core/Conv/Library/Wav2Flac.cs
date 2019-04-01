using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Tag.Setting;

namespace Tag.Core.Conv.Library
{
    class Wav2Flac : IConv
    {
        public IEnumerable<int> Execute(ConvInfo info)
        {
            info.Format = Global.Setting.FFMpegEncode;

            string dummyname = Path.GetDirectoryName(info.FilePath) + "\\" + Path.GetRandomFileName() + info.Extension;
            string resultdummyname = $"{info.ResultPath}{Path.GetRandomFileName()}";

            AudioFileReader afr = new AudioFileReader(info.FilePath);
            afr.Close();
            while (info.Format.IndexOf("%fn%") != -1)
            {
                info.Format = info.Format.Replace("%fn%", dummyname);
            }
            while (info.Format.IndexOf("%bit%") != -1)
            {
                info.Format = info.Format.Replace("%bit%", afr.WaveFormat.BitsPerSample.ToString());
            }
            while (info.Format.IndexOf("%rate%") != -1)
            {
                info.Format = info.Format.Replace("%rate%", afr.WaveFormat.SampleRate.ToString());
            }
            while (info.Format.IndexOf("%outputfn%") != -1)
            {
                info.Format = info.Format.Replace("%outputfn%", resultdummyname);
            }

            if (!Directory.Exists(info.ResultPath))
            {
                Directory.CreateDirectory(info.ResultPath);
            }

            try
            {
                File.Move(info.FilePath, dummyname);
            }
            catch {

            }

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

            string ext = string.Empty;
            int last = 0;
            bool getExt = false;

            while (!proc.StandardError.EndOfStream)
            {
                string[] get = { "" };
                string[] list = { "" };
                var err = proc.StandardError.ReadLine();

                if (getExt == false)
                {
                    get = Regex.Split(err, "Output #0, ");
                    if (get.Length == 2)
                    {
                        ext = get[1].Split(',')[0];
                        getExt = true;
                    }
                }

                list = Regex.Split(err, "time=");
                if (list.Length == 2)
                {
                    try
                    {
                        var time = list[1].Split(' ')[0];
                        int hour = int.Parse(time.Split(':')[0]);
                        int min = int.Parse(time.Split(':')[1]);
                        int second = int.Parse(time.Split(':')[2].Split('.')[0]);
                        int mili = int.Parse(time.Split('.')[1]);
                        TimeSpan data = new TimeSpan(0, hour, min, second, mili);
                        last = (int)(data.TotalMilliseconds / afr.TotalTime.TotalMilliseconds * 100);
                    }
                    catch { }

                }
                yield return last;
            }

            try
            {
                File.Move(dummyname, info.FilePath);
                string result = Path.GetFullPath($"{info.ResultPath}\\{info.FileName}.{ext}");

                if (File.Exists(result))
                {
                    File.Delete(result);
                }
                File.Move($"{resultdummyname}.{ext}", result);
            }
            catch { }
            
            yield return 100;
        }
    }
}
