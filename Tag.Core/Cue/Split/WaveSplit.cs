using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Cue.Split
{
    public class WaveSplit : ISplit
    {
        public IEnumerable<int> Execute(CueInfo info)
        {
            using (WaveFileReader reader = new WaveFileReader(info.WavPath))
            {
                int percent = 0;
                foreach (var trackinfo in info.Track)
                {
                    if (Directory.Exists(info.SavePath))
                    {
                        Directory.CreateDirectory(info.SavePath);
                    }
                    using (WaveFileWriter writer = new WaveFileWriter(info.SavePath + $"{trackinfo.Track}. " + trackinfo.Title + ".wav", reader.WaveFormat))
                    {

                        double BytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000.0;
                        int start = (int)(trackinfo.StartPosition * BytesPerMillisecond);
                        start -= start % reader.WaveFormat.BlockAlign;

                        int end = (int)((trackinfo.StartPosition + trackinfo.DurationMS) * BytesPerMillisecond);
                        end -= end % reader.WaveFormat.BlockAlign;
                        foreach (int value in TrimWavFile(reader, writer, start, end))
                        {
                            yield return (percent + value) / info.Track.Count;
                        }
                    }
                    percent += 100;
                }
            }
            yield return 100;
        }
        
        private IEnumerable<int> TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
            endPos = endPos > reader.Length ? (int)reader.Length : endPos;
            startPos = startPos > 0 ? startPos : 0;

            int percent = 0;

            reader.Position = startPos;

            byte[] buffer = new byte[reader.WaveFormat.BlockAlign * 100];
            while (reader.Position < endPos)
            {
                int bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.Write(buffer, 0, bytesRead);
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
            yield return 100;
        }
    }
}
