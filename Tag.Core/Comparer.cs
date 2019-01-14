using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core
{
    public class Comparer
    {
        private void TrimWavFile(WaveFileReader reader, WaveFileWriter writer)
        {
            reader.Position = 0;
            byte[] buffer = new byte[reader.WaveFormat.BlockAlign * 100];
            while (reader.Position < reader.Length)
            {
                int bytesRequired = (int)(reader.Length - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

    }
}
