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
    class Wav2Mp3 : IMp3Conv
    {
        public IEnumerable<int> Execute(string filePath, string resultPath, LAMEPreset preset)
        {
            string filedata = $"{Path.GetDirectoryName(filePath)}\\{Path.GetFileNameWithoutExtension(filePath)}";

            using (var wav = new NAudio.Wave.WaveFileReader($"{filedata}.wav"))
            {
                using (var mp3 = new NAudio.Lame.LameMP3FileWriter($"{resultPath}", wav.WaveFormat, preset))
                {
                    wav.CopyTo(mp3);
                }
            }

            yield return 100;

        }
    }
}
