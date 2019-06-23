using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATL.CatalogDataReaders;
using NAudio.Lame;
using NAudio.Wave;
using Tag.Core.Cue;
using Tag.Setting;
using TagLib;

namespace Tag.Core.Tagging
{
    public class AudioTagging : ICore<TagInfo>
    {
        readonly public List<TagInfo> tagList = new List<TagInfo>();
            
        public bool CueFile(CueInfo cue)
        {
            for(int i = 0; i < cue.Track.Count; i++)
            {
                string filename = Global.Setting.CueSplitSetting;
                while (filename.IndexOf("%a%") != -1)
                {
                    filename = filename.Replace("%a%", cue.Track[i].Artist);
                }
                while (filename.IndexOf("%A%") != -1)
                {
                    filename = filename.Replace("%A%", cue.Artist);
                }
                while (filename.IndexOf("%n%") != -1)
                {
                    filename = filename.Replace("%n%", cue.Track[i].Title);
                }
                while (filename.IndexOf("%t%") != -1)
                {
                    filename = filename.Replace("%t%", cue.Track[i].Track.ToString());
                }
                string file = cue.SavePath + filename;
                switch (cue.AudioType)
                {
                    case AudioType.WAV:
                        file += ".wav";
                        break;
                    case AudioType.FLAC:
                        file += ".flac";
                        break;
                }
                AddFile(file);
                {
                    TagInfo tag = tagList[i];
                    tag.Album = cue.Title;
                    tag.AlbumArtist = cue.Artist?.Split(';').ToList();
                    tag.Barcode = cue.Barcode;
                    tag.DiscNum = cue.REM.DiscId;
                    tag.Composer = cue.Track[i].Composer?.Split(';').ToList();
                    tag.Genre = cue.REM.Genre?.Split(';').ToList();

                    tag.Artist = cue.Track[i].Artist?.Split(';').ToList();
                    tag.Title = cue.Track[i].Title;
                    tag.Track.RemoveRange(0, tag.Track.Count);
                    tag.Track.Add((uint)cue.Track[i].Track);
                }
            }
            return true;
        }


        public bool AddFile(TagInfo file)
        {
            tagList.Add(file);
            return true;
        }
        public bool AddFile(TagInfo file, string path)
        {
            tagList.Add(new TagInfo(file, path));
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
            using (var mp3File = TagLib.File.Create(taginfo.Path))
            {
                mp3File.Tag.Title = taginfo.Title;
                mp3File.Tag.Performers = taginfo.Artist?.ToArray();
                mp3File.Tag.Album = taginfo.Album;
                if (uint.TryParse(taginfo.Year?.Split('-')[0], out uint result))
                {
                    mp3File.Tag.Year = result;
                }
                mp3File.Tag.Track = taginfo.Track.Count != 0 ? taginfo.Track[0] : 1;
                if (taginfo.Track.Count > 0)
                {
                    mp3File.Tag.TrackCount = taginfo.Track.Aggregate((a, b) => a + b);
                }

                mp3File.Tag.Genres = taginfo.Genre?.ToArray();
                mp3File.Tag.Comment = taginfo.Comment;
                mp3File.Tag.AlbumArtists = taginfo.AlbumArtist?.ToArray();
                mp3File.Tag.Composers = taginfo.Composer?.ToArray();
                mp3File.Tag.MusicBrainzDiscId = taginfo.DiscNum;
                mp3File.Tag.TrackCount = (uint)taginfo.Track.Count;
                mp3File.Tag.Conductor = string.Join(";", taginfo.Publisher);

                mp3File.Tag.Pictures = taginfo.Image?.ToArray();

                mp3File.Save();
            }
        }
    }
}