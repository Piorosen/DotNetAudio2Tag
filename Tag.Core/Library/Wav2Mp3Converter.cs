using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core
{
    public class Wav2Mp3Converter : ICore<string, string>
    {
        readonly List<string> filePath = new List<string>();

        public bool AddFile(string path)
        {
            if (Path.GetExtension(path) == ".wav")
            {
                filePath.Add(path);
                return true;
            }
            return false;
        }

        public bool Delete(int at)
        {
            if (0 <= at && at < filePath.Count)
            {
                filePath.RemoveAt(at);
                return true;
            }
            return false;
        }
        public bool Delete(string remove)
        {
            return filePath.Remove(remove);
        }


        public IEnumerable<int> Execute()
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

            for (int i = 0; i < filePath.Count; i++)
            {
                string filedata = $"{Path.GetDirectoryName(copyPath[i])}\\{Path.GetFileNameWithoutExtension(copyPath[i])}";
                using (var wav = new NAudio.Wave.WaveFileReader($"{filedata}.wav"))
                {
                    using (var mp3 = new NAudio.Lame.LameMP3FileWriter($"{filedata}.mp3", wav.WaveFormat, 320))
                    {
                        wav.CopyTo(mp3);
                    }
                }
                yield return (int)(100.0 / filePath.Count * (i + 1));
            }
        }
    }
}
