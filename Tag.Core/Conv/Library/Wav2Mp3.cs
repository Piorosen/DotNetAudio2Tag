using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Cue;

namespace Tag.Core.Conv.Library
{
    class Wav2Mp3 : IConv
    {
        private IEnumerable<int> Execute(string filePath, string resultPath, LAMEPreset preset = LAMEPreset.ABR_320)
        {
            int percent = 0;
            using (var wav = new WaveFileReader(filePath))
            {
                if (Directory.Exists(resultPath))
                {
                    Directory.CreateDirectory(resultPath);
                }
                using (var mp3 = new LameMP3FileWriter($"{resultPath}", wav.WaveFormat, preset))
                {
                    byte[] buffer = new byte[wav.WaveFormat.BlockAlign * 100];
                    while (wav.Position < wav.Length)
                    {
                        int bytesRequired = (int)(wav.Length - wav.Position);
                        if (bytesRequired > 0)
                        {
                            int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                            int bytesRead = wav.Read(buffer, 0, bytesToRead);
                            if (bytesRead > 0)
                            {
                                mp3.Write(buffer, 0, bytesRead);
                            }
                        }

                        var value = (int)(wav.Position * 100.0 / wav.Length);
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
                    // wav.CopyTo(mp3);
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
            
            foreach (var value in Execute(info.FilePath, info.ResultPath, preset == 0 ? LAMEPreset.ABR_320 : preset))
            {
                yield return value;
            }
        }
    }
}
