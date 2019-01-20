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
                Extension.Log.FileWrite($"{path} : add", Extension.Error.Success);
                return true;
            }
            Extension.Log.FileWrite($"{path} : not wav file", Extension.Error.Error);
            return false;
        }

        public bool Delete(int at)
        {
            if (0 <= at && at < filePath.Count)
            {
                filePath.RemoveAt(at);
                Extension.Log.FileWrite($"{at} delete", Extension.Error.Success);
                return true;
            }
            Extension.Log.FileWrite($"{at} Location Error", Extension.Error.IndexException);
            return false;
        }
        public bool Delete(string remove)
        {
            var result = filePath.Remove(remove);
            if (result)
            {
                Extension.Log.FileWrite($"{remove} delete", Extension.Error.Success);
            }
            else
            {
                Extension.Log.FileWrite($"{remove} Location Error", Extension.Error.Error);
            }
            return result;
        }
        public List<string> List()
        {
            return filePath;
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
