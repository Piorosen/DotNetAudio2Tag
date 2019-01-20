using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Lame;
using NAudio.Wave;
using TagLib;

using TagType = System.ValueTuple<string, TagLib.Tag>;

namespace Tag.Core
{
    public class Mp3Tagging : ICore<TagType, TagType>
    {
        readonly public List<(string Path, TagLib.Tag Tag)> tagList = new List<(string Path, TagLib.Tag Tag)>();

        public bool AddFile(TagType file)
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
        public bool Delete(TagType remove)
        {
            return tagList.Remove(remove);
        }
        public IEnumerable<int> Execute()
        {
            for (int i = 0; i < tagList.Count; i++)
            {
                Tagging(tagList[i].Path, tagList[i].Tag);
                yield return (int)(100.0 / tagList.Count * (i+1));
            }
        }

        public TagLib.Tag this[int i]
        {
            get
            {
                return tagList[i].Tag;
            }
        }

        public void Tagging(string file, TagLib.Tag taginfo)
        {
            var mp3File = TagLib.File.Create(file);
            mp3File.Tag.Title = taginfo.Title;
            mp3File.Tag.Performers = taginfo.Performers;
            mp3File.Tag.Album = taginfo.Album;
            mp3File.Tag.Year = taginfo.Year;
            mp3File.Tag.Track = taginfo.Track;
            mp3File.Tag.TrackCount = taginfo.TrackCount;
            mp3File.Tag.Genres = taginfo.Genres;
            mp3File.Tag.Comment = taginfo.Comment;
            mp3File.Tag.AlbumArtists = taginfo.AlbumArtists;
            mp3File.Tag.Composers = taginfo.Composers;
            mp3File.Tag.Disc = taginfo.Disc;
            mp3File.Tag.Pictures = taginfo.Pictures;

            mp3File.Save();
        }
        
    }
}