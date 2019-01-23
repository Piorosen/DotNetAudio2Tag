using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core
{
    public class AutoConverter
    {
        readonly CueSpliter cueSpliter = new CueSpliter();
        readonly Wav2Mp3Converter wav2Mp3 = new Wav2Mp3Converter();
        readonly Mp3Tagging tagging = new Mp3Tagging();
        readonly TagMusicBrainz brainz = new TagMusicBrainz();

        public IEnumerable<object> AutoConverting(string cueFile, string wavFile, string mp3File, string workDir)
        {
            cueSpliter.AddFile($"{workDir}{cueFile}.cue", $"{workDir}{cueFile}.wav", workDir + wavFile);
            var list = cueSpliter.List();
            string barcode = string.Empty;
            
            // 어차피 단일로만 들어 오므로 단일 컨버팅 시작
            foreach (var t1 in cueSpliter.Execute()) ;

            // 만약에 단일이 아닐경우에만..
            for (int i = 0; i < list.Count; i++)
            {
                var temp = brainz.GetTagInfo(list[i]);
                if (temp.Count == 0)
                {
                    List<BrainzInfo> Check = new List<BrainzInfo>();
                    foreach (var track in list[i].Track)
                    {
                        yield return brainz.GetReleaseInfo(new TagInfo { Title = track.Title, Artist = track.Artist.Split(';').ToList() });
                    }
                }
                for (int k = 0; k < list[i].Track.Count; k++)
                {
                    wav2Mp3.AddFile(list[i].SavePath + $"{k + 1}. " + list[i].Track[k].Title + ".wav");
                }
                foreach (var t2 in wav2Mp3.Execute()) ;

                for (int k = 0; k < list[i].Track.Count; k++)
                {
                    var path = list[i].SavePath + $"{k + 1}. " + list[i].Track[k].Title + ".mp3";
                    tagging.AddFile(new TagInfo(TagLib.File.Create(path).Tag) { Path = path });
                    tagging[k].Title = temp[k].Title;
                    tagging[k].Album = temp[k].Album;
                    tagging[k].Track = temp[k].Track;
                    tagging[k].Artist = temp[k].Artist;
                    tagging[k].Composer = temp[k].Composer;
                    tagging[k].Year = temp[k].Year;
                    tagging[k].Image = temp[k].Image;
                }
                foreach (var t3 in tagging.Execute()) ;
            }

            yield break;
        }

    }
}
