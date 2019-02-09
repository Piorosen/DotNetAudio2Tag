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
    public class AudioTagging : ICore<TagInfo>
    {
        readonly public List<TagInfo> tagList = new List<TagInfo>();

        public bool AddFile(TagInfo file)
        {
            tagList.Add(file);
            return true;
        }

        public bool AddFile(string filePath)
        {
            return AddFile(new TagInfo(TagLib.File.Create(filePath).Tag, filePath));
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
        public List<TagInfo> List()
        {
            return tagList;
        }

        public IEnumerable<int> Execute()
        {
            for (int i = 0; i < tagList.Count; i++)
            {
                Tagging(tagList[i]);
                yield return (int)(100.0 / tagList.Count * (i+1));
            }
        }

        private void Tagging(TagInfo taginfo)
        {
            var mp3File = TagLib.File.Create(taginfo.Path);
            mp3File.Tag.Title = taginfo.Title;
            mp3File.Tag.Performers = taginfo.Artist.ToArray();
            mp3File.Tag.Album = taginfo.Album;
            mp3File.Tag.Year = uint.Parse(taginfo.Year.Split('-')[0]);
            mp3File.Tag.Track = taginfo.Track.Count != 0 ? taginfo.Track[0] : 1;
            mp3File.Tag.TrackCount = taginfo.Track.Aggregate((a, b) => a + b);
            mp3File.Tag.Genres = taginfo.Genre.ToArray();
            mp3File.Tag.Comment = taginfo.Comment;
            mp3File.Tag.AlbumArtists = taginfo.AlbumArtist.ToArray();
            mp3File.Tag.Composers = taginfo.Composer.ToArray();
            mp3File.Tag.MusicBrainzDiscId = taginfo.DiscNum;
            mp3File.Tag.TrackCount = (uint)taginfo.Track.Count;
            mp3File.Tag.Conductor = string.Join(";", taginfo.Publisher);
            mp3File.Tag.Pictures = taginfo.Image.ToArray();
            mp3File.Save();
        }
        
    }
}