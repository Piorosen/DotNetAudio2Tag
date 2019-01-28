using ATL.CatalogDataReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Cue
{
    public class TrackInfo
    {
        public string Title = string.Empty;
        public string Artist = string.Empty;
        public string Composer = string.Empty;

        public double StartPosition = 0.0;
        public double DurationMS = 0.0;
        public int Track = 0;
    }
    public class REM
    {

        public string Genre = String.Empty;
        public string Date = string.Empty;
        public string DiscId = string.Empty;
    
    }

    public class CueInfo
    {
        public string Path = string.Empty;
        public string SavePath = string.Empty;
        public string WavPath = string.Empty;

        public string Title = string.Empty;
        public string Artist = String.Empty;
        public string Barcode = String.Empty;
        public AudioType AudioType = AudioType.NONE;
        public REM REM = new REM();
        public List<TrackInfo> Track = new List<TrackInfo>();
    }
}
