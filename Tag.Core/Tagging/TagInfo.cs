using ATL.CatalogDataReaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Tagging
{
    public class BrainzInfo : TagInfo
    {
        public bool TagInfo = false;
        public int Score = 0;
        public string Type = string.Empty;
    }

    public class TagInfo
    {
        public string Path = string.Empty;
        public string Title = string.Empty;
        public List<string> Artist = new List<string>();
        public string Album = string.Empty;
        public string Year = string.Empty;
        public List<uint> Track = new List<uint>();
        public List<string> Genre = new List<string>();
        public string Comment = string.Empty;
        public List<string> AlbumArtist = new List<string>();
        public List<string> Composer = new List<string>();
        public string DiscNum = string.Empty;
        public List<TagLib.IPicture> Image = new List<TagLib.IPicture>();
        public string Barcode = string.Empty;
        public List<string> Publisher = new List<string>();
        public List<string> Format = new List<string>();
        public string Country = string.Empty;

        public TagInfo(TagLib.Tag value)
        {
            Title = value.Title;
            Artist = value.Performers.ToList();
            Album = value.Album;
            Year = value.Year.ToString();
            Track.Add(value.Track);
            Genre = value.Genres.ToList();
            Comment = value.Comment;
            AlbumArtist = value.AlbumArtists.ToList();
            Composer = value.Composers.ToList();
            Image = value.Pictures.ToList();
            
        }
        public TagInfo()
        {
        }

        public TagInfo(TagInfo value)
        {
            Title = value.Title;
            Artist = value.Artist;
            Album = value.Album;
            Year = value.Year;
            Track = value.Track;
            Genre = value.Genre;
            Comment = value.Comment;
            AlbumArtist = value.AlbumArtist;
            Composer = value.Composer;
            Image = value.Image;
            Barcode = value.Barcode;
        }
    }
}
