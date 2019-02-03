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

        /// <summary>
        /// 검색할 정보를 담습니다. info에 대한 참조를 합시다.
        /// </summary>
        /// <param name="info">요청 가능한 목록 : Title, Artist, DiscNum, Barcode, Composer, Publisher</param>
        /// <returns>검색한 결과를 리턴합니다.</returns>
        public List<TagInfo> GetAlbumList(TagInfo info)
        {
            List<TagInfo> result = new List<TagInfo>();

            result.AddRange(vgmDb.GetAlbumInfo(info));
            result.AddRange(brainz.GetAlbumInfo(info));

            return result;
        }
        /// <summary>
        /// 앨범의 고유 아이디와 검색할 언어를 선택합니다.
        /// </summary>
        /// <param name="tag">Tag : Identifier, Lang = "en"을 꼭 선택하셔야합니다., Tag : Barcode Or Identifier</param>
        /// <returns>앨범의 태그 목록을 가져옵니다. 순차적입니다.</returns>
        public List<TagInfo> GetTagInfo(TagInfo info)
        {
            List<TagInfo> result = new List<TagInfo>();

            result.AddRange(vgmDb.GetTrackInfo(info));
            result.AddRange(brainz.GetTrackInfo(info));

            return result;
        }
    }
}
