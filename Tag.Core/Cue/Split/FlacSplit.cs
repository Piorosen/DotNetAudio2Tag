using CUETools.Codecs;
using CUETools.Codecs.FLAKE;
using NAudio.Flac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Cue.Split
{
    public class FlacSplit : ISplit
    {
        public IEnumerable<int> Execute(string filePath, string resultPath, TrackInfo trackinfo)
        {
            NAudio.Flac.FlacReader f = new NAudio.Flac.FlacReader(filePath);
            double BytesPerMillisecond = f.WaveFormat.AverageBytesPerSecond / 1000.0;
            f.Close();

            StreamReader stream = new StreamReader(filePath);
            using (FlakeReader b = new FlakeReader(filePath, stream.BaseStream))
            {
                var config = b.PCM;
                using (FlakeWriter a = new FlakeWriter(resultPath + $"{ trackinfo.Track }. " + trackinfo.Title + ".flac", config))
                {
                    int start = (int)(trackinfo.StartPosition * BytesPerMillisecond / 4.0) ;
                    start -= start % config.BlockAlign;
                    int end = (int)((trackinfo.StartPosition + trackinfo.DurationMS) * BytesPerMillisecond / 4.0);
                    end -= end % config.BlockAlign;

                    foreach (int value in TrimFlacFile(b, a, start, end, config))
                    {
                        yield return value;
                    }
                }
            }
            yield return 100;
        }

        private IEnumerable<int> TrimFlacFile(FlakeReader reader, FlakeWriter writer, int startPos, int endPos, AudioPCMConfig conf)
        {
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
                }
            }
            yield return 100;
        }
    }
}
