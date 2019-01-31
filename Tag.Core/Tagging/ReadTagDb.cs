using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Tagging.Library;

namespace Tag.Core.Tagging
{
    public class ReadTagDb
    {
        MusicDb vgmDb = new MusicDb();
        MusicBrain brainz = new MusicBrain();
        
        public List<TagInfo> GetAlbumList(TagInfo info)
        {
            List<TagInfo> result = new List<TagInfo>();

            result.AddRange(vgmDb.GetAlbumInfo(info));
            result.AddRange(brainz.GetAlbumInfo(info));

            return result;
        }

        public List<TagInfo> GetTagInfo(TagInfo info)
        {
            List<TagInfo> result = new List<TagInfo>();

            result.AddRange(vgmDb.GetTrackInfo(info));
            result.AddRange(brainz.GetTrackInfo(info));

            return result;
        }
    }
}
