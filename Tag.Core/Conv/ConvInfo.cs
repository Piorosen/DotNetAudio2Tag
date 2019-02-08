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
        public string FileNamePlus => Path.GetFileName(FilePath);
        public string Directory => Path.GetDirectoryName(FilePath);
        
        /// <summary>
        /// 외부 프로그램 및 컨버팅 파라메타 값
        /// </summary>
        public List<object> Parameter { get; set; } = new List<object>();

        /// <summary>
        /// 외부 프로그램 파라메타 포맷
        /// </summary>
        public string Format { get; set; } = string.Empty;
        /// <summary>
        /// 외부 프로그램 경로
        /// </summary>
        public string Source { get; set; } = string.Empty;
    }
}
