using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Cue;
using NAudio.Flac;
using CUETools.Codecs.FLAKE;
using CUETools.Codecs;
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

            while (!proc.StandardError.EndOfStream)
            {
                var err = proc.StandardError.ReadLine();

                var get = Regex.Split(err, "Output #0, ");
                if (get.Length == 2)
                {
                    ext = get[1].Split(',')[0];
                }
                var list = Regex.Split(err, "time=");
                if (list.Length == 2)
                {
                    var time = list[1].Split(' ')[0];
                    int hour = int.Parse(time.Split(':')[0]);
                    int min = int.Parse(time.Split(':')[1]);
                    int second = int.Parse(time.Split(':')[2].Split('.')[0]);
                    int mili = int.Parse(time.Split('.')[1]);
                    TimeSpan data = new TimeSpan(0, hour, min, second, mili);
                    yield return (int)(afr.TotalTime.TotalMilliseconds / data.TotalMilliseconds) * 100;
                }
            }

            try
            {
                File.Move(dummyname, info.FilePath);
                File.Move($"{resultdummyname}.{ext}", Path.GetFullPath($"{info.ResultPath}\\{info.FileName}.{ext}"));
            }
            catch { }

            yield return 100;
        }
    }
}
