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
        public List<string> Index = new List<string>();

        public string ISRC = string.Empty;
        public double DurationMS = 0.0;
    }
    public class AudioFormat
    {
        public double BytesPerMillisecond = 0.0;
        public int BlockAlign = 0;
    }
    public class REM
    {

        public string Genre = String.Empty;
        public string Date = string.Empty;
        public string DiscId = string.Empty;
        public int? DiscNumber = null;
        public int? TotalDiscs = null;

    }

    public class CueInfo
    {
        public string Path = string.Empty;
        public string SavePath = string.Empty;
        public string WavPath = string.Empty;

        public string Title = string.Empty;
        public string Artist = String.Empty;
        public string Barcord = String.Empty;

        public REM REM = new REM();
        public AudioFormat Format = null;
        public List<TrackInfo> Track = new List<TrackInfo>();
    }
}
