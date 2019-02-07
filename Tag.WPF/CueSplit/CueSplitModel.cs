
using System.Windows.Media;
using Tag.Core.Cue;

namespace Tag.WPF
{
    public class CueSplitModel : TrackInfo
    {
        public int Index { get; set; } = 0;
        public Brush Color => Index % 2 == 0 ? Brushes.Beige : Brushes.Transparent;
        public bool IsSelect { get; set; } = false;

        public string Duration => DurationMS == -1 ? "곡의 길이" : $"{(int)(DurationMS / 60 / 1000)}분 : {(int)(DurationMS / 1000 % 60)}초 : {(int)(DurationMS % 1000)}ms";
        public string OffSet => TimeOffSet == -1 ? "인덱스 길이" : $"{(int)(TimeOffSet / 1000 % 60)}초 : {(int)(TimeOffSet % 1000)}ms";

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
            this.TimeOffSet = info.TimeOffSet;
        }
    }
}
