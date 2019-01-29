using ATL.CatalogDataReaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Conv
{
    class ConvInfo
    {
        public AudioType Type = AudioType.NONE;
        public string FilePath = string.Empty;
        public string ResultPath = string.Empty;

        public string FileName => Path.GetFileNameWithoutExtension(FilePath);
        public string Extension => Path.GetExtension(FilePath);
        public string Directory => Path.GetDirectoryName(FilePath);
        
        public List<object> Parameter = new List<object>();
        public string Source = string.Empty;
    }
}
