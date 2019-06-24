using ATL.CatalogDataReaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TagLib;

namespace Tag.Core.Tagging
{
    public class VgmDbInfo : TagInfo
    {
        public Dictionary<string, string> AnothorName { get; set; } = new Dictionary<string, string>();
        public string AnothorNameList
        {
            get
            {
                return string.Join(", ", AnothorName.Keys);
            }
        }
    }
   
    public class BrainzInfo : TagInfo
    {
        public int Score { get; set; } = 0;
    }

    public class TagInfo
    {
        public string Identifier { get; set; } = string.Empty;
        public string Lang { get; set; } = "en";
        
        public string Path { get; private set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<string> Artist { get; set; } = new List<string>();
        public string Album { get; set; } = string.Empty;
        public string Year { get; set; } = "0";
        public List<uint> Track { get; set; } = new List<uint>();
        public List<string> Genre { get; set; } = new List<string>();
        public string Comment { get; set; } = string.Empty;
        public List<string> AlbumArtist { get; set; } = new List<string>();
        public List<string> Composer { get; set; } = new List<string>();
        public string DiscNum { get; set; } = string.Empty;
        public List<IPicture> Image { get; set; } = new List<IPicture>();
        public string Barcode { get; set; } = string.Empty;
        public List<string> Publisher { get; set; } = new List<string>();
        public List<string> Format { get; set; } = new List<string>();
        public string Country { get; set; } = string.Empty;
        public TagTypes TagType { get; set; } = TagTypes.Id3v2;

        public BitmapImage UIImage { get; set; } = null;

        public static void Move(TagLib.Tag A, TagLib.Tag B)
        {
            A.Title = B.Title;
            A.Performers = B.Performers;
            A.Album = B.Album;
            A.Year = B.Year;
            A.Track = B.Track;
            A.Genres = B.Genres;
            A.Comment = B.Comment;
            A.AlbumArtists = B.AlbumArtists;
            A.Composers = B.Composers;
            A.MusicBrainzDiscId = B.MusicBrainzDiscId;
            A.Pictures = B.Pictures;
            A.MusicBrainzReleaseCountry = B.MusicBrainzReleaseCountry;
            A.Conductor = B.Conductor;
        }

        public TagInfo(TagLib.Tag value, string filePath)
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
            DiscNum = value.MusicBrainzDiscId;
            Image = value.Pictures.ToList();
            if (Image?.Count != 0)
            {
                UIImage = new BitmapImage();
                UIImage.BeginInit();
                UIImage.StreamSource = new MemoryStream(value.Pictures[0].Data.Data);
                UIImage.EndInit();
            }
            
            
            Country = value.MusicBrainzReleaseCountry;
            TagType = value.TagTypes;
            Publisher.Add(value.Conductor);
            Path = filePath;
        }

        public TagInfo()
        {
        }
        public TagInfo(string Path)
        {
            this.Path = Path;
        }

        public TagInfo(TagInfo value, string path = "")
        {
            Identifier = value.Identifier;
            Lang = value.Lang;
            Path = value.Path;
            Title = value.Title;
            Artist = value.Artist.ToArray().ToList();
            Album = value.Album;
            Year = value.Year;
            Track = value.Track.ToArray().ToList();
            Genre = value.Genre.ToArray().ToList();
            Comment = value.Comment;
            AlbumArtist = value.AlbumArtist.ToArray().ToList();
            Composer = value.Composer.ToArray().ToList();
            DiscNum = value.DiscNum;
            Image = value.Image.ToArray().ToList();
            Barcode = value.Barcode;
            Publisher = value.Publisher.ToArray().ToList();
            Format = value.Format.ToArray().ToList();
            Country = value.Country;
            TagType = value.TagType;

            if (Image?.Count != 0)
            {
                UIImage = new BitmapImage();
                UIImage.BeginInit();
                UIImage.StreamSource = new MemoryStream(value.Image[0].Data.Data);
                UIImage.EndInit();
            }


            if (path == string.Empty)
            {
                Path = value.Path;
            }
            else
            {
                Path = path;
            }
        }
    }
}
