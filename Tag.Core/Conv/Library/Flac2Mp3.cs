using NAudio.Flac;
using NAudio.Lame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Conv.Library
{
    class Flac2Mp3 : IConv
    {
        private IEnumerable<int> Execute(string filePath, string resultPath, LAMEPreset preset = LAMEPreset.ABR_320 | LAMEPreset.V0)
        {
            using (var flac = new FlacReader(filePath))
            {
                using (var mp3 = new LameMP3FileWriter($"{resultPath}", flac.WaveFormat, preset))
                {
                    byte[] buffer = new byte[flac.WaveFormat.BlockAlign * 100];
                    while (flac.Position < flac.Length)
                    {
                        int bytesRequired = (int)(flac.Length - flac.Position);
                        if (bytesRequired > 0)
                        {
                            int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                            int bytesRead = flac.Read(buffer, 0, bytesToRead);
                            if (bytesRead > 0)
                            {
                                mp3.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
           //         flac.CopyTo(mp3);
                }
            }
            yield return 100;
        }

        public IEnumerable<int> Execute(ConvInfo info)
        {
            LAMEPreset preset = new LAMEPreset();

            foreach (var value in info.Parameter)
            {
                if (value is LAMEPreset)
                {
                    preset |= (LAMEPreset)value;
                }
            }

            foreach (var value in Execute(info.FilePath, info.ResultPath, preset))
            {
                yield return value;
            }
        }

    }
}
