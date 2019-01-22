using System;
namespace Tag.Core
{
    public class MusicBrainzLib
    {
        Core.TagInfo track = new Core.TagInfo();

        public void SetTrack(Core.CueData trackinfo)
        {
            track.Title = trackinfo.Title;
            track.Artist.Add(trackinfo.Artists);
            track.Comment = trackinfo.Comments;

            var list = MusicBrainz.Search.Release(null, null, null, null, null, null, "4935228173068");
            foreach (var data in list.Data)
            {

                Console.WriteLine($"{data.Title} : {data.Country} : {data.Score}");
            }
        }
    }
}
