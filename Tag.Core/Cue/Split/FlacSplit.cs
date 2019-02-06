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
                foreach (var trackinfo in info.Track)
                {
                    var config = b.PCM;
                    using (FlakeWriter a = new FlakeWriter(info.SavePath + $"{ trackinfo.Track }. " + trackinfo.Title + ".flac", config))
                    {
                        int start = (int)(trackinfo.StartPosition * BytesPerMillisecond / 4.0);
                        start -= start % config.BlockAlign;
                        int end = (int)((trackinfo.StartPosition + trackinfo.DurationMS) * BytesPerMillisecond / 4.0);
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
                    if (percent == (int)(reader.Position * 100.0 / endPos))
                    {
                        yield return percent;
                    }
                    else
                    {
                        percent = (int)(reader.Position * 100.0 / endPos);
                    }
                }
            }
            yield return 100;
        }
    }
}
