using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tag.Core.Cue;

namespace Tag.Core.Tagging.Library
{
    public class MusicBrain : ITag<BrainzInfo>
    {
        private string RequestWeb(string Link, bool check = false)
        {
            try
            {
                if (check == false)
                {
                    var result = string.Empty;
                    HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(Link);
                    hwr.Method = "GET";
                    hwr.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                    hwr.Host = "musicbrainz.org";
                    hwr.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36";
                    hwr.Headers.Add("If-None-Match", "Y2U3ZTgwMDAwMDAwMDAwU29scg==");
                    var response = (HttpWebResponse)hwr.GetResponse();
                    result = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    return result;
                }
                else
                {
                    WebClient wc = new WebClient();
                    var result = wc.DownloadString(Link);
                    return result;
                }
            } catch (Exception)
            {

            }
            return null;
        }

        private TagLib.Picture GetImage(string Link)
        {
            try
            {
                JObject json = JObject.Parse(RequestWeb(Link, true));
                var test = json.Children();
                string adress = string.Empty;

                foreach (var t1 in test)
                {
                    foreach (var t2 in t1.Children().Children().Children())
                    {
                        JProperty p = t2.ToObject<JProperty>();
                        if (p.Name == "image")
                        {
                            adress = p.Value.ToString();
                            break;
                        }
                    }
                    break;
                }

                WebClient wc = new WebClient();
                var nameImage = Path.GetRandomFileName();
                wc.DownloadFile(adress, nameImage);

                var pimage = new TagLib.Picture(nameImage); ;
                try
                {
                    new FileInfo(nameImage).Delete();
                }
                catch
                {
                    return null;
                }

                return pimage;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 검색할 정보를 담습니다.
        /// </summary>
        /// <param name="info">요청 가능한 목록 : Title, Artist</param>
        /// <returns>검색한 결과를 리턴합니다.</returns>
        public List<BrainzInfo> GetAlbumInfo(TagInfo info)
        {
            var result = new List<BrainzInfo>();

            string Title = info.Title;
            string Artist = string.Join(" ", info.Artist);
            Artist = Artist == string.Empty ? null : Artist;
            Title = Title == string.Empty ? null : Title;

            var data = MusicBrainz.Search.Release(query: Title, artist: Artist);

            if (data == null)
            {
                return result;
            }

            foreach (var value in data.Data)
            {
                BrainzInfo binfo = new BrainzInfo
                {
                    Score = value.Score,
                    Title = value.Title,
                    Country = value.Country,
                    Year = value.Date,
                    Barcode = value.Barcode,
                    Identifier = value.Id,
                    Album = value.Title,

                };
                binfo.AlbumArtist.Add(value.Artistcredit[0].Artist.Name);
                binfo.Composer.Add(value.Artistcredit[0].Artist.Name);

                foreach (var i in value.Artistcredit)
                {
                    binfo.Artist.Add(i.Artist.Name);
                }

                foreach (var i in value.Labelinfolist)
                {
                    binfo.Publisher.Add(i.Label.Name);
                    binfo.DiscNum += i.Catalognumber + " ";
                }
                foreach (var i in value.Mediumlist.Medium)
                {
                    binfo.Format.Add(i.Format);
                    binfo.Track.Add(uint.Parse(i.Tracklist.Count));
                }
                result.Add(binfo);
            }
            return result;
        }

        /// <summary>
        /// 앨범의 고유 아이디와 검색할 언어를 선택합니다.
        /// </summary>
        /// <param name="tag">Tag : Barcode Or Identifier</param>
        /// <returns>앨범의 태그 목록을 가져옵니다. 순차적입니다.</returns>
        public List<TagInfo> GetTrackInfo(TagInfo info)
        {
            var result = new List<TagInfo>();
            if (info.Barcode != string.Empty || info.Identifier != string.Empty)
            {
                var data = MusicBrainz.Search.Release(barcode: info.Barcode, reid: info.Identifier);

                if (data.Data.Count != 0)
                {
                    var value = data.Data[0];

                    var tagging = RequestWeb($"http://musicbrainz.org/ws/2/recording/?query=reid:{data.Data[0].Id}");
                    XmlDocument xmlreader = new XmlDocument();
                    xmlreader.LoadXml(tagging);
                    var list = xmlreader["metadata"]["recording-list"].ChildNodes;

                    var pimage = GetImage($"http://coverartarchive.org/release/{data.Data[0].Id}");

                    for (int i = 0; i < list.Count; i++)
                    {
                        List<string> Artist = new List<string>
                        {
                            list[i]["artist-credit"]["name-credit"]["artist"]["name"].InnerText
                        };
                        var Composer = new List<string>
                        {
                            list[i]["release-list"]["release"]["artist-credit"]["name-credit"]["artist"]["name"].InnerText
                        };
                        var AlubmArtist = new List<string>
                        { 
                            list[0]["artist-credit"]["name-credit"]["artist"]["name"].InnerText
                        };

                        TagInfo ti = new TagInfo
                        {
                            Title = list[i]["title"].InnerText,
                            Album = info.Album,

                            Artist = Artist,
                            AlbumArtist = info.AlbumArtist,
                            Composer = Composer,

                            Year = value.Date,
                            Country = value.Country,
                            Barcode = value.Barcode
                        };

                        foreach (var w in value.Labelinfolist)
                        {
                            ti.Publisher.Add(w.Label.Name);
                            ti.DiscNum += w.Catalognumber + " ";
                        }
                        foreach (var w in value.Mediumlist.Medium)
                        {
                            ti.Format.Add(w.Format);
                        }
                        ti.Track.Add((uint)i + 1);
                        ti.Image.Add(pimage);

                        result.Add(ti);
                    }
                }

            }
            return result;
        }
    }
}
