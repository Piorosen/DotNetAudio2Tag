using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Conv;

namespace Tag.Core.Conv
{
    public class Wav2Mp3Converter
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
            yield break;   
        }

        public IEnumerable<int> Execute(string filePath, string resultPath)
        {
            throw new NotImplementedException();
        }
    }
}
