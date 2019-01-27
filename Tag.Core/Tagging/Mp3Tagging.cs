using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Lame;
using NAudio.Wave;
using TagLib;

namespace Tag.Core.Tagging
{
    public class Mp3Tagging
    {
        readonly public List<TagInfo> tagList = new List<TagInfo>();

        public bool AddFile(TagInfo file)
        {
            tagList.Add(file);
            
            return true;
        }
        public bool Delete(int at)
        {
            if (0 <= at && at < tagList.Count)
            {
                tagList.RemoveAt(at);
                return true;
            }
            return false;
        }
        public bool Delete(TagInfo remove)
        {
            return tagList.Remove(remove);
        }
        public IEnumerable<int> Execute()
        {
            for (int i = 0; i < tagList.Count; i++)
            {
                Tagging(tagList[i].Path, tagList[i]);
                yield return (int)(100.0 / tagList.Count * (i+1));
            }
        }
        public List<TagInfo> List()
        {
            return tagList;
        }

        public TagInfo this[int i]
        {
            get
            {
                return tagList[i];
            }
        }

        public void Tagging(string file, TagInfo taginfo)
        {
            var mp3File = TagLib.File.Create(file);
            mp3File.Tag.Title = taginfo.Title;
            mp3File.Tag.Performers = taginfo.Artist.ToArray();
            mp3File.Tag.Album = taginfo.Album;
            mp3File.Tag.Year = taginfo.Year;
            mp3File.Tag.Track = taginfo.Track;
            mp3File.Tag.TrackCount = taginfo.Track;
            mp3File.Tag.Genres = taginfo.Genre.ToArray();
            mp3File.Tag.Comment = taginfo.Comment;
            mp3File.Tag.AlbumArtists = taginfo.AlbumArtist.ToArray();
            mp3File.Tag.Composers = taginfo.Composer.ToArray();
            // mp3File.Tag.Disc = taginfo.DiscNum;
            mp3File.Tag.Pictures = taginfo.Image.ToArray();
            mp3File.Save();
        }
        
    }
}