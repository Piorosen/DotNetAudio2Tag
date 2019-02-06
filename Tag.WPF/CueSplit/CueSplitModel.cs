
using System.Windows.Media;
using Tag.Core.Cue;

namespace Tag.WPF
{
    public class CueSplitModel : TrackInfo
    {
        public int Index { get; set; } = 0;
        public Brush Color => IsSelect ? Brushes.LightGray : Index % 2 == 0 ? Brushes.Beige : Brushes.Transparent;
        public bool IsSelect = false;

        public CueSplitModel()
        {

        }
        public CueSplitModel(TrackInfo info)
        {
            this.Artist = info.Artist;
            this.Composer = info.Composer;
            this.DurationMS = info.DurationMS;
            this.StartPosition = info.StartPosition;
            this.Title = info.Title;
            this.Track = info.Track;
            this.MemberwiseClone();
        }
    }
}
