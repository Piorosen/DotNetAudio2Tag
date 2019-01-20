using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core
{
    public class TagInfo
    {
        private TagLib.Tag File;

        public string Path = string.Empty;
        public string Title = string.Empty;
        public List<string> Artist = new List<string>();
        public string Album = string.Empty;
        public uint Year = 0;
        public uint Track = 0;
        public List<string> Genre = new List<string>();
        public string Comment = string.Empty;
        public List<string> AlbumArtist = new List<string>();
        public List<string> Composer = new List<string>();
        public string DiscNum = string.Empty;
        public List<TagLib.IPicture> Image = new List<TagLib.IPicture>();
       
        public TagInfo(TagLib.Tag value)
        {
            File = value;

            Title = value.Title;
            Artist = value.Performers.ToList();
            Album = value.Album;
            Year = value.Year;
            Track = value.Track;
            Genre = value.Genres.ToList();
            Comment = value.Comment;
            AlbumArtist = value.AlbumArtists.ToList();
            Composer = value.Composers.ToList();
            Image = value.Pictures.ToList();
        }

        public TagLib.Tag ToTagLib()
        {
            File.Title = Title;
            File.Performers = Artist.ToArray();
            File.Album = Album;
            File.Year = Year;
            File.Track = Track;
            File.Genres = Genre.ToArray();
            File.Comment = Comment;
            File.AlbumArtists = AlbumArtist.ToArray();
            File.Composers = Composer.ToArray();
            File.Pictures = Image.ToArray();

            return File;
        }
    }
}
