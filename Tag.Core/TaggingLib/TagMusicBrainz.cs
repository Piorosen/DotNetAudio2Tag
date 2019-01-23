using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tag.Core
{
    public class BrainzInfo
    {
        public bool TagInfo = false;
        public string Title = string.Empty;
        public TagInfo Tag = new TagInfo();
        public int Score = 0;
        public string Type = string.Empty;
        public List<string> Artist = new List<string>();
        public string Album = string.Empty;
        public List<int> Track = new List<int>();
        public string Year = string.Empty;
        public string Country = string.Empty;
        public List<string> Format = new List<string>();
        public List<string> Publisher = new List<string>();
        public List<string> CatNo = new List<string>();
        public string Barcode = string.Empty;
    }

    public class TagMusicBrainz : ITagger
    {
        private string Get(string Link, bool check = false)
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
            JObject json = JObject.Parse(Get(Link, true));
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
            catch (Exception)
            {

            }

            return pimage;
        }
        
        public List<BrainzInfo> GetReleaseInfo(TagInfo info)
        {
            var result = new List<BrainzInfo>();

            var data = MusicBrainz.Search.Release(query: info.Title, artist: string.Join(";", info.Artist));

            foreach (var value in data.Data)
            {
                BrainzInfo binfo = new BrainzInfo
                {
                    Score = value.Score,
                    Title = value.Title,
                    TagInfo = false,
                    Country = value.Country,
                    Year = value.Date,
                    Barcode = value.Barcode
                };

                foreach (var i in value.Artistcredit)
                {
                    binfo.Artist.Add(i.Artist.Name);
                }

                foreach (var i in value.Labelinfolist)
                {
                    binfo.Publisher.Add(i.Label.Name);
                    binfo.CatNo.Add(i.Catalognumber);
                }
                foreach (var i in value.Mediumlist.Medium)
                {
                    binfo.Format.Add(i.Format);
                    binfo.Track.Add(int.Parse(i.Tracklist.Count));
                }
                result.Add(binfo);
            }
            return result;
        }

        public List<TagInfo> GetTagInfo(Core.CueData info)
        {
            var result = new List<TagInfo>();
            if (info.Barcord != null)
            {
                var data = MusicBrainz.Search.Release(barcode: info.Barcord);

                var tagging = Get($"http://musicbrainz.org/ws/2/recording/?query=reid:{data.Data[0].Id}");
                XmlDocument xmlreader = new XmlDocument();
                xmlreader.LoadXml(tagging);
                var list = xmlreader["metadata"]["recording-list"].ChildNodes;

                var pimage = GetImage($"http://coverartarchive.org/release/{data.Data[0].Id}");

                for (int i = 0; i < list.Count && i < info.Track.Count; i++)
                {
                    List<string> Artist = new List<string>();
                    Artist.Add(list[i]["artist-credit"]["name-credit"]["artist"]["name"].InnerText);
                    var Composer = new List<string>
                    {
                        list[i]["release-list"]["release"]["artist-credit"]["name-credit"]["artist"]["name"].InnerText
                    };
                    TagInfo ti = new TagInfo
                    {
                        Title = list[i]["title"].InnerText,
                        Album = info.Title,
                        Track = (uint)(i + 1),
                        Artist = Artist,
                        Composer = Composer,
                        Year = uint.Parse(data.Data[0].Date.Split('-')[0])
                    };

                    ti.Image.Add(pimage);

                    result.Add(ti);
                }
            }
            return result;
        }
    }
}
