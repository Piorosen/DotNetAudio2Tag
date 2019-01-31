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
        private IEnumerable<int> Execute(string filePath, string resultPath, LAMEPreset preset = LAMEPreset.ABR_320 | LAMEPreset.V0)
        {
            using (var wav = new NAudio.Wave.WaveFileReader(filePath))
            {
                using (var mp3 = new NAudio.Lame.LameMP3FileWriter($"{resultPath}", wav.WaveFormat, preset))
                {
                    wav.CopyTo(mp3);
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
