using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Cue.Split
{
    class WaveSplit : ISplit
    {
        public IEnumerable<int> Execute(string filePath, string resultPath, TrackInfo trackinfo)
        {
            using (WaveFileReader reader = new WaveFileReader(filePath))
            {
                using (WaveFileWriter writer = new WaveFileWriter(resultPath + $"{trackinfo.Track + 1}. " + trackinfo.Title + ".wav", reader.WaveFormat))
                {
                    double BytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000.0;
                    int start = (int)(trackinfo.StartPosition * BytesPerMillisecond);
                    start -= start % reader.WaveFormat.BlockAlign;

                    int end = (int)((trackinfo.StartPosition + trackinfo.DurationMS) * BytesPerMillisecond);
                    end -= end % reader.WaveFormat.BlockAlign;
                    foreach (int value in TrimWavFile(reader, writer, start, end))
                    {
                        yield return value;
                    }
                }
            }
        }
        
        private IEnumerable<int> TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
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
                yield return (int)((reader.Position / (double)(endPos - startPos)) * 100);
            }
            yield return 100;
        }
    }
}
