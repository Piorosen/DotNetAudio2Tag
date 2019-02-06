using ATL.CatalogDataReaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Conv
{
    public class ConvInfo
    {
        public AudioType Type { get; set; } = AudioType.NONE;
        public string FilePath { get; set; } = string.Empty;
        public string ResultPath { get; set; } = string.Empty;

        public string FileName => Path.GetFileNameWithoutExtension(FilePath);
        public string Extension => Path.GetExtension(FilePath);
        public string Directory => Path.GetDirectoryName(FilePath);
        
        public List<object> Parameter { get; set; } = new List<object>();
        public string Format { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
    }
}
