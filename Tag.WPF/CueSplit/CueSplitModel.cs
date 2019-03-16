
using System.Windows.Media;
using Tag.Core.Cue;
using Tag.Setting;

namespace Tag.WPF
{
    public class CueSplitModel : TrackInfo
    {
        public int Index { get; set; } = 0;
        public string IndexString => Index != 0 ? Index.ToString() : string.Empty;

        public Brush Color => Index != 0 ? Index % 2 == 0 ? Brushes.Beige : Brushes.Transparent : (Brush)Application.Current.FindResource("MaterialDesignSelection");
        public bool IsSelect { get; set; } = false;

        public string Duration => DurationMS == -1 ? Global.Language.CueSongLength : $"{(int)(DurationMS / 60 / 1000)}{Global.Language.CueSongLength} : {(int)(DurationMS / 1000 % 60)}{Global.Language.CueSecond} : {(int)(DurationMS % 1000)}{Global.Language.CueMiliSecond}";
        public string OffSet => TimeOffSet == -1 ? Global.Language.CueIndexLength : $"{(int)(TimeOffSet / 1000 % 60)}{Global.Language.CueSecond} : {(int)(TimeOffSet % 1000)}{Global.Language.CueMiliSecond}";

        public string Path = string.Empty;

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
