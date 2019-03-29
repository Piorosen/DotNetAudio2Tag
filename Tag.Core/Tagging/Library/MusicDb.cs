using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tag.Core.Cue;
using Tag.Setting;

namespace Tag.Core.Tagging.Library
{
    public class MusicDb : ITag<VgmDbInfo>
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
        #region GetAlbumInfo Libaray
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
                var title = info.Album == string.Empty ? info.Title : info.Album;
                byte[] data = Encoding.UTF8.GetBytes("action=advancedsearch" +
                                $"&albumtitles={title}" +
                                $"&catalognum={string.Empty/*info.DiscNum*/}" +
                                $"&eanupcjan={string.Empty/*info.Barcode*/}" +
                                $"&dosearch=Search+Albums+Now&pubtype%5B0%5D=1&pubtype%5B1%5D=1&pubtype%5B2%5D=1&distype%5B0%5D=1&distype%5B1%5D=1&distype%5B2%5D=1&distype%5B3%5D=1&distype%5B4%5D=1&distype%5B5%5D=1&distype%5B6%5D=1&distype%5B7%5D=1&distype%5B8%5D=1&category%5B1%5D=0&category%5B2%5D=0&category%5B4%5D=0&category%5B8%5D=0&category%5B16%5D=0" +
                                $"&composer={string.Empty/*string.Join(" ", info.Composer)*/}" +
                                $"&arranger={string.Empty}" +
                                $"&performer={string.Join(" ", info.Artist)}" +
                                $"&lyricist={string.Empty}" +
                                $"&publisher={string.Empty/*string.Join(" ", info.Publisher)*/}" +
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
        private List<string> SplitWeb(string Web)
        {
            var result = new List<string>();

            var next = Regex.Split(Web, "Release Date</a></td>")[1];
            next = Regex.Split(next, "</table>")[0];

            var list = Regex.Split(next, "<tr>");

            for (int i = 2; i < list.Length; i++)
            {
                result.Add(list[i]);
            }

            return result;
        }
        private VgmDbInfo ParsingWeb(string parse)
        {
            VgmDbInfo tag = new VgmDbInfo();
            var next = Regex.Split(parse, "<span class=\"catalog album-")[1];
            tag.Genre.Add(next.Split('\"')[0]);


            tag.DiscNum = next.Split('>')[1].Split('<')[0];

            string tmp = Regex.Split(next, "href=\"")[2];

            next = Regex.Split(next, "href=\"")[1];
            tag.Identifier = next.Split('\"')[0].Split('/')[4];


            next = Regex.Split(next, "title=\'")[1];
            tag.Title = Regex.Split(next, "\'>")[0];


            var langlist = Regex.Split(next, "<span class");
            for (int i = 1; i < langlist.Length; i++)
            {
                var next2 = Regex.Split(langlist[i], "lang=\"")[1];
                var lang = next2.Split('\"')[0];
                string title = string.Empty;
                try
                {
                    next2 = Regex.Split(next2, "</em>")[1];
                    title = Regex.Split(next2, "</span>")[0];
                }
                catch
                {
                    title = next2.Split('>')[1];
                    title = Regex.Split(title, "</span>")[0];
                }
                title.Trim('\r', '\t', '\n', ' ', '/', '\\', '\"', '\'');
                tag.AnothorName[lang] = title;
            }


            next = tmp.Split('#')[1].Split('\"')[0];
            tag.Year = next.Split('\"')[0].Insert(6, "-").Insert(4, "-");


            return tag;
        }
        private List<VgmDbInfo> SearchAlbum(TagInfo info)
        {
            try
            {
                var result = new List<VgmDbInfo>();
                string data = RequestWeb(info);
                var split = SplitWeb(data);

                foreach (var parse in split)
                {
                    result.Add(ParsingWeb(parse));
                }
                return result;
            }
            catch
            {
                return new List<VgmDbInfo>();
            }
            
        }
        /// <summary>
        /// 검색할 정보를 담습니다.
        /// </summary>
        /// <param name="info">요청 가능한 목록 : Title, DiscNum, Barcode, Composer, Artist, Publisher</param>
        /// <returns>검색한 결과를 리턴합니다.</returns>
        public List<VgmDbInfo> GetAlbumInfo(TagInfo info)
        {
            return SearchAlbum(info);
        }
        #endregion

        #region GetTrackInfo Library
        private string RequestTrackWeb(string identifier)
        {
            WebClient wc = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            var data = wc.DownloadString($"https://vgmdb.net/album/{identifier}");
            
            return data;
        }
        public TagLib.Picture GetImage(string Link, string id)
        {
            WebClient wc = new WebClient();
            var nameImage = id + ".jpg";
            if (File.Exists(nameImage) == false)
            {
                wc.DownloadFile(Link, Global.FilePath.CacheImagePath + nameImage);
            }

            var pimage = new TagLib.Picture(Global.FilePath.CacheImagePath + nameImage);

            if (Global.CacheImageDelete)
            {
                try
                {
                    new FileInfo(Global.FilePath.CacheImagePath + nameImage).Delete();
                }
                catch { }
            }

            return pimage;
        }
        private List<TagInfo> SplitTrackWeb(string web, string lang, string identifier)
        {
            List<TagInfo> result = new List<TagInfo>();
            TagInfo basic = new TagInfo();

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(web);

            var Stat = doc.DocumentNode.SelectNodes("//body//div[@class='page']")[1];
            Stat = Stat.SelectNodes("./table/tr")[0];

            #region Genre
            var Genre = Stat.SelectSingleNode("./td[@id='rightcolumn']");
            Genre = Genre.SelectNodes("./div")[1];
            Genre = Genre.SelectNodes("./div")[0];
            Genre = Genre.SelectNodes("./div")[3];
            var text = Genre.InnerText;
            text = text.Remove(0, "Category".Length + 1);

            basic.Genre.Add(text.Trim());
            #endregion

            Stat = Stat.SelectSingleNode("./td/div");

            #region Title
            var Title = Stat.SelectNodes("./h1/span");
            foreach (var data in Title)
            {
                if (data.Attributes["lang"].Value == lang)
                {
                    basic.Album = data.InnerText.TrimStart(' ', '\\', '/', ';');
                    basic.Album = basic.Album.Replace("&amp;", "&");
                    break;
                }
            }
            #endregion
            #region Image
            var Image = Stat.SelectSingleNode("./div/div[@id='leftfloat']");
            Image = Image.SelectSingleNode("./div/table/tr/td/div[@id='coverart']");
            var imagePath = Image.Attributes["style"].Value.Split('\'')[1];

            basic.Image.Add(GetImage(imagePath, identifier));
            #endregion

            #region Tag
            var tag = Stat.SelectSingleNode("./div/div[@id='rightfloat']");
            var tagcollection = tag.SelectNodes("./div/div/table[@id='album_infobit_large']/tr");
            try
            {
                basic.DiscNum = tagcollection[0].SelectNodes("./td")[1].InnerText.Trim();
            }
            catch { }
            try
            {
                basic.Year = tagcollection[1].SelectSingleNode("./td/a").Attributes["href"].Value.Split('#')[1].Split('\"')[0].Insert(6, "-").Insert(4, "-");
            }
            catch { }

            try
            {
                foreach (var data in tagcollection[4].SelectNodes("./td")[1].InnerText.Split('+'))
                {
                    basic.Format.Add(data.Trim());
                }
            }
            catch { }

            try
            {
                foreach (var data in tagcollection[6].SelectNodes("./td/a/span[@class='productname']"))
                {
                    if (data.Attributes["lang"].Value == lang)
                    {
                        basic.Publisher.Add(data.InnerText.Replace("&amp;", "&"));

                        break;
                    }
                }
            }
            catch { }

            try
            {
                foreach (var data in tagcollection[7].SelectNodes("./td/a"))
                {
                    if (data.Attributes["lang"] == null)
                    {
                        basic.Composer.Add(data.InnerText.Replace("&amp;", "&"));
                    }
                    else if (data.Attributes["lang"].Value == lang)
                    {
                        basic.Composer.Add(data.SelectSingleNode("./span").InnerText.Replace("&amp;", "&"));
                    }
                }
            }
            catch { }

            try
            {
                foreach (var data in tagcollection[8].SelectNodes("./td/a"))
                {
                    if (data.Attributes["lang"] == null)
                    {
                        basic.AlbumArtist.Add(data.InnerText.Replace("&amp;", "&"));
                    }
                    else if (data.Attributes["lang"].Value == lang)
                    {
                        basic.AlbumArtist.Add(data.SelectSingleNode("./span").InnerText.Replace("&amp;", "&"));
                    }
                }
            }
            catch { }

            try
            {
                foreach (var data in tagcollection[9].SelectNodes("./td/a"))
                {
                    if (data.Attributes["lang"] == null)
                    {
                        basic.Artist.Add(data.InnerText.Replace("&amp;", "&"));
                    }
                    else if (data.Attributes["lang"].Value == lang)
                    {
                        basic.Artist.Add(data.SelectSingleNode("./span").InnerText.Replace("&amp;", "&"));
                    }
                }
            }
            catch { }
            
            #endregion

            var tracklang = Stat.SelectNodes("./div/div/ul[@id='tlnav']/li");
            var tracklist = Stat.SelectNodes("./div/div/div[@id='tracklist']/span");
            int count = 0;
            
            // 언어 갯수 만큼 반복
            foreach (var data in tracklang)
            {
                var convertLang = string.Empty;
                switch (data.SelectSingleNode("./a").InnerText)
                {
                    case "English":
                        convertLang = VGMLang.English;
                        break;
                    case "Romjai":
                        convertLang = VGMLang.Romjai;
                        break;
                    case "Japanese":
                        convertLang = VGMLang.Japanese;
                        break;
                }

                if (lang == convertLang)
                {
                    break;
                }
                count++;

                if (count == tracklang.Count)
                {
                    count = 0;
                    break;
                }
            }

            try
            {
                foreach (var list in tracklist[count].SelectNodes("./table"))
                {
                    foreach (var data in list.SelectNodes("./tr"))
                    {
                        TagInfo value = new TagInfo(basic);
                        value.Track.Add(uint.Parse(data.SelectSingleNode("./td/span[@class='label']").InnerText));
                        value.Track.Add((uint)count + 1);

                        value.Title = data.SelectNodes("./td")[1].InnerText.Replace("&amp;", "&");

                        result.Add(value);
                    }
                }
            }
            catch { }
            

            return result;
        }
        string lastweb = string.Empty;
        string iden = string.Empty;

        /// <summary>
        /// 앨범의 고유 아이디와 검색할 언어를 선택합니다.
        /// </summary>
        /// <param name="tag">Tag : Identifier, Lang = "en"을 꼭 선택하셔야합니다.</param>
        /// <returns></returns>
        [Obsolete()]
        public List<TagInfo> GetTrackInfo(TagInfo tag)
        {
            try
            {
                var web = RequestTrackWeb(tag.Identifier);

                lastweb = web;
                iden = tag.Identifier;

                return SplitTrackWeb(web, tag.Lang, tag.Identifier);
            }
            catch
            {
                return new List<TagInfo>();
            }
        }
        /// <summary>
        /// 앨범의 고유 아이디와 검색할 언어를 선택합니다.
        /// </summary>
        /// <param name="tag">Tag : Identifier, Lang = "en"을 꼭 선택하셔야합니다.</param>
        /// <returns></returns>
        public Dictionary<string, List<TagInfo>> GetTrackInfoList(List<string> lang, string iden)
        {
            var result = new Dictionary<string, List<TagInfo>>();

            try
            {
                var web = RequestTrackWeb(iden);
                
                foreach (var value in lang)
                {
                    result.Add(value, SplitTrackWeb(web, value, iden));
                }
                return result;
            }
            catch
            {
                return new Dictionary<string, List<TagInfo>>();
            }
        }
        #endregion

    }
}
