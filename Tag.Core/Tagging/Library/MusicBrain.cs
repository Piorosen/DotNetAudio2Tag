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
using Tag.Setting;
using Tag.Setting.Setting;

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
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    return null;
                }

            }
            return string.Empty;
        }

        public TagLib.Picture GetImage(string Link, string id)
        {

            var nameImage = Global.FilePath.CacheImagePath + id + ".jpg";
            if (File.Exists(nameImage) == true)
            {
                return new TagLib.Picture(nameImage);
            }
            


            var dataa = string.Empty;
            while (dataa == string.Empty)
            {
                dataa = RequestWeb(Link, true);
                if (dataa == null)
                {
                    return null;
                }
            }

            JObject json = JObject.Parse(dataa);
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


            
            if (File.Exists(nameImage) == false)
            {
                new WebClient().DownloadFile(adress, nameImage);
            }

            var pimage = new TagLib.Picture(nameImage);

            if (Global.CacheImageDelete)
            {
                try
                {
                    new FileInfo(nameImage).Delete();
                }
                catch { }
            }

            return pimage;
        }

        /// <summary>
        /// 검색할 정보를 담습니다.
        /// </summary>
        /// <param name="info">요청 가능한 목록 : Title, Artist</param>
        /// <returns>검색한 결과를 리턴합니다.</returns>
        public List<BrainzInfo> GetAlbumInfo(TagInfo info)
        {
            var result = new List<BrainzInfo>();

            string Title = info.Title == string.Empty ? null : info.Title;
            string Artist = string.Join(" ", info.Artist);
            Artist = Artist == string.Empty ? null : Artist;
            string barcode = info.Barcode == string.Empty ? null : info.Barcode;
            string Album = info.Album == string.Empty ? null : info.Album;
            
            var data = MusicBrainz.Search.Release(barcode: barcode);

            data = MusicBrainz.Search.Release(query: Title, release: Album, artist: Artist, barcode: barcode);
            
            if (data == null)
            {
                data = MusicBrainz.Search.Release(Album)
                    ?? MusicBrainz.Search.Release(Title)
                    ?? MusicBrainz.Search.Release(artist: Artist);
            }
            if (data == null || data.Count == 0)
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
                info.Barcode = info.Barcode == string.Empty ? null : info.Barcode;
                info.Identifier = info.Identifier == string.Empty ? null : info.Identifier;

                MusicBrainz.Data.Release data = null;
                while (data == null)
                {
                    data = MusicBrainz.Search.Release(barcode: info.Barcode, reid: info.Identifier, limit: 50);
                }


                if (data.Data.Count != 0)
                {
                    var value = data.Data[0];
                    var tagging = RequestWeb($"http://musicbrainz.org/ws/2/recording/?query=reid:{data.Data[0].Id}&limit=100");
                   // var tagging = RequestWeb($"https://musicbrainz.org/ws/2/release/{data.Data[0].Id}?inc=aliases&artist-credits+discids&labels&recordings");
                    XmlDocument xmlreader = new XmlDocument();  
                    xmlreader.LoadXml(tagging);
                    var list = xmlreader["metadata"]["recording-list"].ChildNodes;

                    var pimage = GetImage($"http://coverartarchive.org/release/{data.Data[0].Id}", info.Identifier);
                    // https://musicbrainz.org/ws/2/release/038d43b9-8484-4f0c-812b-d03db0f7ba60?inc=aliases+artist-credits+discids+labels+recordings
                    // https://musicbrainz.org/ws/2/release/038d43b9-8484-4f0c-812b-d03db0f7ba60?inc=aliases+artist-credits+discids+labels+recordings

                    for (int i = 0; i < list.Count; i++)
                    {
                        List<string> Artist = new List<string>();
                        var Composer = new List<string>();
                        var AlubmArtist = new List<string>();
                        uint track = 0;
                        uint num = 0;
                        try
                        {
                            Artist = new List<string>
                            {
                                list[i]["artist-credit"]["name-credit"]["artist"]["name"].InnerText
                            };
                        }
                        catch { }
                        try
                        {
                            Composer = new List<string>
                            {
                                list[i]["release-list"]["release"]["artist-credit"]["name-credit"]["artist"]["name"].InnerText
                            };
                        }
                        catch { }

                        try
                        {
                            AlubmArtist = new List<string>
                            {
                                list[0]["artist-credit"]["name-credit"]["artist"]["name"].InnerText
                            };
                        }
                        catch { }

                        try
                        {
                            track = uint.Parse(list[i]["release-list"]
                                ["release"]["medium-list"]
                                ["medium"]["track-list"]
                                ["track"]["number"]
                                .InnerText);
                        }
                        catch { }

                        // 디스크 번호
                        try
                        {
                            num = uint.Parse(list[i]["release-list"]["release"]["medium-list"]["medium"]["position"].InnerText);
                        }
                        catch { }
                        
                        TagInfo ti = new TagInfo
                        {
                            Title = list[i]["title"].InnerText,
                            Album = info.Album,

                            Artist = Artist,
                            AlbumArtist = info.AlbumArtist,
                            Composer = Composer,

                            Year = value.Date,
                            Country = value.Country,
                            Barcode = value.Barcode,
                        };

                        ti.Track.Add(track);
                        ti.Track.Add(num);
                        foreach (var w in value.Labelinfolist)
                        {
                            ti.Publisher.Add(w.Label.Name);
                            ti.DiscNum += w.Catalognumber + " ";
                        }
                        foreach (var w in value.Mediumlist.Medium)
                        {
                            ti.Format.Add(w.Format);
                        }
                        // ti.Track.Add((uint)i + 1);
                        
                        if (pimage != null)
                        {
                            ti.Image.Add(pimage);
                        }

                        result.Add(ti);
                    }
                }

            }
            return result;
        }
    }
}
