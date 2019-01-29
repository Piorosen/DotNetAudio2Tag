using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Cue;

namespace Tag.Core.Tagging.Library
{
    public class MusicDb : ITag
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

        //
        // Referer: https://vgmdb.net/search
        // Origin: https://vgmdb.net
        // Content-Type: application/x-www-form-urlencoded
        // Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
        // User-Agent: Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_2) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.0.2 Safari/605.1.15
        //
        private string RequestWeb(TagInfo info)
        {
            string Url = "https://vgmdb.net/search?do=results";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.Referer = "https://vgmdb.net/search";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_2) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.0.2 Safari/605.1.15";

            using (var stream = request.GetRequestStream())
            {
                byte[] data = Encoding.UTF8.GetBytes("action=advancedsearch" +
                                $"&albumtitles={info.Title}" +
                                $"&catalognum={info.Barcode}" +
                                $"&eanupcjan={string.Empty /* Barcode */}" +
                                $"&dosearch=Search+Albums+Now&pubtype%5B0%5D=1&pubtype%5B1%5D=1&pubtype%5B2%5D=1&distype%5B0%5D=1&distype%5B1%5D=1&distype%5B2%5D=1&distype%5B3%5D=1&distype%5B4%5D=1&distype%5B5%5D=1&distype%5B6%5D=1&distype%5B7%5D=1&distype%5B8%5D=1&category%5B1%5D=0&category%5B2%5D=0&category%5B4%5D=0&category%5B8%5D=0&category%5B16%5D=0" +
                                $"&composer={string.Join(" ", info.Composer)}" +
                                $"&arranger={string.Empty}" +
                                $"&performer={string.Join(" ", info.Artist)}" +
                                $"&lyricist={string.Empty}" +
                                $"&publisher={string.Join(" ", info.Publisher)}" +
                                $"&game={string.Empty /* Product */}" +
                                $"&trackname={string.Empty}" +
                                $"&caption={string.Empty /* Scan Caption */}" +
                                $"&notes={string.Empty}" +
                                $"&anyfield={string.Empty}" +
                                "&classification%5B1%5D=0&classification%5B2%5D=0&classification%5B32%5D=0&classification%5B4%5D=0&classification%5B16%5D=0&classification%5B256%5D=0&classification%5B512%5D=0&classification%5B64%5D=0&classification%5B4096%5D=0&classification%5B8%5D=0&classification%5B128%5D=0&classification%5B1024%5D=0&classification%5B2048%5D=0&releasedatemodifier=is&day=0&month=0&year=0&discsmodifier=is&discs=&pricemodifier=is&price_value=&wishmodifier=0&sellmodifier=0&collectionmodifier=0&tracklistmodifier=is&tracklists=&scanmodifier=is&scans=&albumadded=&albumlastedit=&scanupload=&tracklistadded=&tracklistlastedit=&sortby=albumtitle&orderby=ASC&childmodifier=0");
                stream.Write(data, 0, data.Length);
            }

            string respondText = string.Empty;

            using (var stream = request.GetResponse())
            {
                using (var reader = new StreamReader(stream.GetResponseStream()))
                {
                    respondText = reader.ReadToEnd();
                }
            }
            return respondText;
        }

        private List<TagInfo> Search(TagInfo info)
        {
            string data = RequestWeb(info);
            
            return new List<TagInfo>();
        }


        public List<TagInfo> GetTagInfo(TagInfo info)
        {
            Search(info);

            return new List<TagInfo>();
        }
    }
}
