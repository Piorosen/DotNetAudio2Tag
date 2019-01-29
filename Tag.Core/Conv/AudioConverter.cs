using ATL.CatalogDataReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Conv.Library;
using Tag.Core.Tagging;

namespace Tag.Core.Conv
{
    class AudioConverter : ICore<TagInfo>
    {
        readonly List<TagInfo> TagList = new List<TagInfo>();

        public bool Delete(int at)
        {
            if (0 <= at && at < TagList.Count)
            {
                TagList.RemoveAt(at);
                return true;
            }
            return false;
        }

        public bool Delete(TagInfo item)
        {
            return TagList.Remove(item);
        }

        public IEnumerable<int> Execute()
        {
            foreach (var value in TagList)
            {
                if (value.AudioType == AudioType.WAV)
                {
                    Wav2Mp3 conv = new Wav2Mp3();
                    conv.Execute()
                }

            yield return 0;
        }
    }
}
