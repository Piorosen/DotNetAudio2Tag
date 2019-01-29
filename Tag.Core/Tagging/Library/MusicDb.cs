using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Cue;

namespace Tag.Core.Tagging.Library
{
    class MusicDb : ITag
    {
        /*
         * Title
         * Catalog Numer
         * Barcode
         * Composer
         * Arragner 
         * Performer
         * Lyicist
         * Publisher
         * Product
         * Track Title
         * Scan Captions
         * Notes
         * Any Field
         */

        private List<TagInfo> Search(CueInfo info)
        {


            return new List<TagInfo>();
        }
        public List<TagInfo> GetTagInfo(CueInfo info)
        {


            return new List<TagInfo>();
        }
    }
}
