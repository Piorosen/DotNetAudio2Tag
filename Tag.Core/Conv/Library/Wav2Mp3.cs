using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Cue;

namespace Tag.Core.Conv.Library
{
    class Wav2Mp3 : IMp3Conv
    {
        public IEnumerable<int> Execute(string filePath, string resultPath)
        {
            List<string> copyPath = new List<string>();
            for (int size = 0; size < filePath.Count; size++)
            {
                string data = string.Empty;
                for (int length = 0; length < filePath[size].Length; length++)
                {
                    data += filePath[size][length];
                }
                copyPath.Add(data);
            }
            Extension.Log.FileWrite($"Init", Extension.Error.None);

            for (int i = 0; i < filePath.Count; i++)
            {
                Extension.Log.FileWrite($"File Load", Extension.Error.Success);
                string filedata = $"{Path.GetDirectoryName(copyPath[i])}\\{Path.GetFileNameWithoutExtension(copyPath[i])}";
                using (var wav = new NAudio.Wave.WaveFileReader($"{filedata}.wav"))
                {
                    using (var mp3 = new NAudio.Lame.LameMP3FileWriter($"{filedata}.mp3", wav.WaveFormat, 320))
                    {
                        wav.CopyTo(mp3);
                    }
                }
                Extension.Log.FileWrite($"File Split {(int)filePath.Count * (i + 1)} / {100}", Extension.Error.Success);
                yield return (int)(100.0 / filePath.Count * (i + 1));
            }
        }
    }
}
