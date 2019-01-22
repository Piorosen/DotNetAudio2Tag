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
        
        public void AutoConverting(string cueFile)
        {
            cueSpliter.AddFile(cueFile);
            var list = cueSpliter.List();
            string barcode = string.Empty;

            foreach (var t in cueSpliter.Execute()) ;
            for (int i = 0; i < list.Count; i++)
            {
                for (int k = 0; k < list[i].Track.Count; k++)
                {
                    wav2Mp3.AddFile(list[i].SavePath + $"{k + 1}. " + list[i].Track[k].Title + ".wav");
                }
                foreach (var t in wav2Mp3.Execute()) ;

                var temp = brainz.GetTagInfo(list[i]);
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
                foreach (var t in tagging.Execute()) ;
            }
            
            
            

        }

    }
}
