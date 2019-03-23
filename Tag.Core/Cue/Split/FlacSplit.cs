using CUETools.Codecs;
using CUETools.Codecs.FLAKE;
using NAudio.Flac;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Setting;

namespace Tag.Core.Cue.Split
{
    public class FlacSplit : ISplit
    {
        public IEnumerable<int> Execute(CueInfo info)
        {
            double BytesPerMillisecond = info.WaveFormat.AverageBytesPerSecond / 1000.0;


            StreamReader stream = new StreamReader(info.WavPath);
            using (FlakeReader b = new FlakeReader(info.WavPath, stream.BaseStream))
            {
                int percent = 0;
                if (!Directory.Exists(info.SavePath))
                {
                    Directory.CreateDirectory(info.SavePath);
                }
                foreach (var trackinfo in info.Track)
                {
                    var config = b.PCM;

                    string filename = Global.Setting.CueSplitSetting;
                    while (filename.IndexOf("%a%") != -1)
                    {
                        filename = filename.Replace("%a", trackinfo.Artist);
                    }
                    while (filename.IndexOf("%A%") != -1)
                    {
                        filename = filename.Replace("%a", info.Artist);
                    }
                    while (filename.IndexOf("%n%") != -1)
                    {
                        filename = filename.Replace("%a", trackinfo.Title);
                    }
                    while (filename.IndexOf("%t%") != -1)
                    {
                        filename = filename.Replace("%a", trackinfo.ToString());
                    }

                    using (FlakeWriter a = new FlakeWriter(info.SavePath + filename + ".flac", config))
                    {
                        int start = (int)(trackinfo.StartPosition * BytesPerMillisecond / 8.0);
                        start -= start % config.BlockAlign;
                        int end = (int)((trackinfo.StartPosition + trackinfo.DurationMS) * BytesPerMillisecond / 8.0);
                        end -= end % config.BlockAlign;

                        foreach (int value in TrimFlacFile(b, a, start, end, config))
                        {
                            yield return (percent + value) / info.Track.Count;
                        }
                    }
                    percent += 100;
                }   
            }
            yield return 100;
        }

        private IEnumerable<int> TrimFlacFile(FlakeReader reader, FlakeWriter writer, int startPos, int endPos, AudioPCMConfig conf)
        {
            endPos = endPos > reader.Length ? (int)reader.Length : endPos;
            startPos = startPos > 0 ? startPos : 0;

            int percent = 0;

            reader.Position = startPos;

            AudioBuffer buffer = new AudioBuffer(conf, conf.BlockAlign * 100);
            while (reader.Position < endPos)
            {
                int bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Size);
                    int bytesRead = reader.Read(buffer, bytesToRead);
                    if (bytesRead > 0)
                    {
                        try
                        {
                            writer.Write(buffer);
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                    var value = (int)((reader.Position - startPos) * 100.0 / (endPos - startPos));
                    if (percent == value)
                    {
                        continue;
                    }
                    if (percent != value)
                    {
                        percent = value;
                        yield return percent;
                    }
                }
            }
            yield return 100;
        }
    }
}
