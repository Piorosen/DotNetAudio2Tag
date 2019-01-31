using ATL.CatalogDataReaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Conv.Library;
using Tag.Core.Tagging;

namespace Tag.Core.Conv
{
    public class AudioConverter : ICore<ConvInfo>
    {
        readonly List<ConvInfo> TagList = new List<ConvInfo>();

        public ConvInfo this[int index] => TagList[index];

        public bool AddFile(ConvInfo file)
        {
            if (file.ResultPath == string.Empty)
            {
                file.ResultPath = Path.Combine(file.Directory, file.FileName);
                file.ResultPath += ".mp3";
            }
            TagList.Add(file);
            return true;
        }

        public bool Delete(int at)
        {
            if (0 <= at && at < TagList.Count)
            {
                TagList.RemoveAt(at);
                return true;
            }
            return false;
        }
        public bool Delete(ConvInfo item)
        {
            return TagList.Remove(item);
        }

        public IEnumerable<int> Execute() => Execute(ConvMode.NORMAL);
        public IEnumerable<int> Execute(ConvMode mode)
        {
            foreach (var value in TagList)
            {
                if (mode == ConvMode.NORMAL)
                {
                    if (value.Type == AudioType.WAV)
                    {
                        Wav2Mp3 conv = new Wav2Mp3();
                        foreach (var status in conv.Execute(value))
                        {

                        }
                    }
                    else if (value.Type == AudioType.FLAC)
                    {
                        Flac2Mp3 conv = new Flac2Mp3();
                        foreach (var status in conv.Execute(value))
                        {

                        }
                    }
                    else if (value.Type == AudioType.NONE)
                    {
                        yield return 0;
                        yield break;
                    }
                }
                else if (mode == ConvMode.USER)
                {
                    User2Mp3 conv = new User2Mp3();
                    conv.Execute(value);
                }
                
            }
            yield return 0;
        }

        public List<ConvInfo> List()
        {
            throw new NotImplementedException();
        }
    }
}
