using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Conv;
using Tag.Core.Cue;
using Tag.Core.Tagging;
using Tag.Core.Tagging.Library;

namespace Tag.Core
{
    class Wait
    {
        public CueInfo cue = new CueInfo();
        public List<TagInfo> tag = new List<TagInfo>();
        public List<BrainzInfo> info = new List<BrainzInfo>();
    }

    //public class AutoConverter
    //{
    //    readonly CueSpliter cueSpliter = new CueSpliter();
    //    readonly Wav2Mp3Converter wav2Mp3 = new Wav2Mp3Converter();
    //    readonly Mp3Tagging tagging = new Mp3Tagging();
    //    readonly TagMusicBrainz brainz = new TagMusicBrainz();

    //    Wait wait = new Wait();

    //    public List<BrainzInfo> AutoConverting(string cueFile, string wavFile, string mp3File, string workDir)
    //    {
    //        cueSpliter.AddFile($"{workDir}{cueFile}.cue", $"{workDir}{cueFile}.wav", workDir + wavFile);
    //        var list = cueSpliter.List();
    //        try { list.RemoveRange(1, list.Count - 1); } catch (Exception) { }

    //        string barcode = string.Empty;

    //        // 어차피 단일로만 들어 오므로 단일 컨버팅 시작
    //        foreach (var t1 in cueSpliter.Execute()) ;

    //        // 만약에 단일이 아닐경우에만..
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            wait.cue = list[i];
    //            var temp = brainz.GetTagInfo(list[i]);
    //            wait.tag = temp;
    //            if (temp.Count == 0)
    //            {
    //                foreach (var track in list[i].Track)
    ////                {
    ////                    var p = brainz.GetReleaseInfo(new TagInfo { Title = track.Title, Artist = track.Artist.Split(';').ToList() });
    ////                    wait.info = p;
    ////                    return p;
    ////                }
    ////            }
    ////            else
    ////            {
    ////                Execute();
    ////            }
    ////        }
    ////        return new List<BrainzInfo>();
    ////    }

    //    public bool SelectBrainzIndex(int index = -1)
    //    {
    //        if (index == -1)
    //        {
    //            wait = new Wait();
    //        }
    //        if (0 <= index && index < wait.info.Count)
    //        {
    //            wait.tag = brainz.GetTagInfo(new CueInfo { Barcord = wait.info[index].Barcode });
    //            return true;
    //        }
    //        return false;
    //    }

    //    public bool Execute()
    //    {
    //        for (int k = 0; k < wait.cue.Track.Count; k++)
    //        {
    //            wav2Mp3.AddFile(wait.cue.SavePath + $"{k + 1}. " + wait.cue.Track[k].Title + ".wav");
    //        }
    //        foreach (var t2 in wav2Mp3.Execute()) ;

    //        for (int k = 0; k < wait.cue.Track.Count && k < wait.tag.Count; k++)
    //        {
    //            var path = wait.cue.SavePath + $"{k + 1}. " + wait.cue.Track[k].Title + ".mp3";
    //            tagging.AddFile(new TagInfo(TagLib.File.Create(path).Tag) { Path = path });
    //            tagging[k].Title = wait.tag[k].Title;
    //            tagging[k].Album = wait.tag[k].Album;
    //            tagging[k].Track = wait.tag[k].Track;
    //            tagging[k].Artist = wait.tag[k].Artist;
    //            tagging[k].Composer = wait.tag[k].Composer;
    //            tagging[k].Year = wait.tag[k].Year;
    //            tagging[k].Image = wait.tag[k].Image;
    //        }
    //        foreach (var t3 in tagging.Execute()) ;
    //        return true;
    //    }
    
}