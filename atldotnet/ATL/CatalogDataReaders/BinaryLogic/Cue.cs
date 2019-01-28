﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ATL.CatalogDataReaders.BinaryLogic
{
    /// <summary>
    /// Class for cuesheet files reading (extension .cue)
    /// http://wiki.hydrogenaud.io/index.php?title=Cue_sheet
    /// </summary>
    public class Cue : ICatalogDataReader
    {
        private string path = string.Empty;
        private string title = string.Empty;
        private string artist = string.Empty;
        private string comments = string.Empty;
        private string barcode = string.Empty;
        private string genre = string.Empty;
        private string date = string.Empty;
        private string composers = string.Empty;
        private string discid = string.Empty;
        private AudioType extension = AudioType.NONE;

        IList<Track> tracks = new List<Track>();


        public string Path
        {
            get => path;
            set { path = value; }
        }

        public string Artist => artist;

        public string Comments => comments;

        public string Title => title;
        public IList<Track> Tracks => tracks;

        public string Barcode => barcode;

        public string Genre => genre;

        public string Date => date;
        public string Composers => composers;

        public string DiscId => discid;

        public AudioType Extension => extension;

        // ----------------------- Constructor

        public Cue(string path)
        {
            this.path = path;
            read();
        }


        // ----------------------- Specific methods

        private string stripBeginEndQuotes(string s)
        {
            if (s.Length < 2) return s;
            if ((s[0] != '"') || (s[s.Length - 1] != '"')) return s;

            return s.Substring(1, s.Length - 2);
        }

        static private int decodeTimecodeToMs(string timeCode)
        {
            int result = -1;
            bool valid = false;

            int frames = 0;
            int minutes = 0;
            int seconds = 0;

            if (timeCode.Contains(":"))
            {
                valid = true;
                string[] parts = timeCode.Split(':');
                if (parts.Length >= 1) frames = int.Parse(parts[parts.Length - 1]);
                if (parts.Length >= 2) seconds = int.Parse(parts[parts.Length - 2]);
                if (parts.Length >= 3) minutes = int.Parse(parts[parts.Length - 3]);

                result = (int)Math.Round(frames / 75.0); // 75 frames per seconds (CD sectors)
                result += seconds;
                result += minutes * 60;
            }

            if (!valid) result = -1;

            return result * 1000;
        }

        private void read()
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 2048, FileOptions.SequentialScan))
            using (TextReader source = new StreamReader(fs, System.Text.Encoding.UTF8))
            {
                string s = source.ReadLine();
                Track physicalTrack = null;
                string audioFilePath = "";

                Track currentTrack = null;
                Track previousTrack = null;
                int previousTimeOffset = 0;
                int indexRelativePosition = 0;

                while (s != null)
                {
                    s = s.Trim();
                    int firstBlank = s.IndexOf(' ');
                    string firstWord = s.Substring(0, firstBlank);
                    string[] trackInfo = s.Split(' ');


                    if (null == currentTrack)
                    {
                        if ("REM".Equals(firstWord, StringComparison.OrdinalIgnoreCase))
                        {
                            int nextBlank = s.IndexOf(' ', firstBlank + 1);
                            var nextWord = s.Split(' ');

                            if ("GENRE".Equals(nextWord[1], StringComparison.OrdinalIgnoreCase))
                            {
                                genre = stripBeginEndQuotes(s.Substring(nextBlank + 1, s.Length - nextBlank - 1));
                            }
                            else if ("DATE".Equals(nextWord[1], StringComparison.OrdinalIgnoreCase))
                            {
                                date = stripBeginEndQuotes(s.Substring(nextBlank + 1, s.Length - nextBlank - 1));
                            }
                            else if ("DISCID".Equals(nextWord[1], StringComparison.OrdinalIgnoreCase))
                            {
                                discid = stripBeginEndQuotes(s.Substring(nextBlank + 1, s.Length - nextBlank - 1));
                            }
                            else if ("COMMENT".Equals(nextWord[1], StringComparison.OrdinalIgnoreCase))
                            {
                                comments = stripBeginEndQuotes(s.Substring(nextBlank + 1, s.Length - nextBlank - 1));
                            }
                            else if ("COMPOSER".Equals(nextWord[1], StringComparison.OrdinalIgnoreCase))
                            {
                                composers = stripBeginEndQuotes(s.Substring(nextBlank + 1, s.Length - nextBlank - 1));
                            }
                            //if (comments.Length > 0) comments += Settings.InternalValueSeparator;
                            //comments += s.Substring(firstBlank + 1, s.Length - firstBlank - 1);
                        }
                        else if("CATALOG".Equals(firstWord, StringComparison.OrdinalIgnoreCase))
                        {
                            barcode = stripBeginEndQuotes(s.Substring(firstBlank + 1, s.Length - firstBlank - 1));
                        }
                        else if ("PERFORMER".Equals(firstWord, StringComparison.OrdinalIgnoreCase))
                        {
                            artist = stripBeginEndQuotes(s.Substring(firstBlank + 1, s.Length - firstBlank - 1));
                        }
                        else if ("TITLE".Equals(firstWord, StringComparison.OrdinalIgnoreCase))
                        {
                            title = stripBeginEndQuotes(s.Substring(firstBlank + 1, s.Length - firstBlank - 1));
                        }
                        else if ("FILE".Equals(firstWord, StringComparison.OrdinalIgnoreCase))
                        {
                            audioFilePath = s.Substring(firstBlank + 1, s.Length - firstBlank - 1);
                            audioFilePath = audioFilePath.Substring(0, audioFilePath.LastIndexOf(' ')); // Get rid of the last word representing the audio format
                            audioFilePath = stripBeginEndQuotes(audioFilePath);

                            switch (System.IO.Path.GetExtension(audioFilePath).ToLower())
                            {
                                case ".wav":
                                    extension = AudioType.WAV;
                                    break;
                                case ".flac":
                                    extension = AudioType.FLAC;
                                    break;
                                default:
                                    extension = AudioType.NONE;
                                    break;
                            }

                            // Strip the ending word representing the audio format
                            if (!System.IO.Path.IsPathRooted(audioFilePath))
                            {
                                audioFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), audioFilePath);
                            }
                            physicalTrack = new Track(audioFilePath);
                        }
                        else if ("TRACK".Equals(firstWord, StringComparison.OrdinalIgnoreCase))
                        {
                            currentTrack = new Track();
                            if (trackInfo.Length > 0) currentTrack.TrackNumber = byte.Parse(trackInfo[1]);
                            currentTrack.Genre = physicalTrack.Genre;
                            currentTrack.IsVBR = physicalTrack.IsVBR;
                            currentTrack.Bitrate = physicalTrack.Bitrate;
                            currentTrack.CodecFamily = physicalTrack.CodecFamily;
                            currentTrack.Year = physicalTrack.Year;
                            currentTrack.PictureTokens = physicalTrack.PictureTokens;
                            currentTrack.DiscNumber = physicalTrack.DiscNumber;
                            currentTrack.Artist = "";
                            currentTrack.Title = "";
                            currentTrack.Comment = "";
                        }
                    } else
                    {
                        if ("TRACK".Equals(firstWord, StringComparison.OrdinalIgnoreCase))
                        {
                            if (0 == currentTrack.Artist.Length) currentTrack.Artist = artist;
                            if (0 == currentTrack.Artist.Length) currentTrack.Artist = physicalTrack.Artist;
                            if (0 == currentTrack.Title.Length) currentTrack.Title = physicalTrack.Title;
                            if (0 == currentTrack.Comment.Length) currentTrack.Comment = physicalTrack.Comment;
                            if (0 == currentTrack.TrackNumber) currentTrack.TrackNumber = physicalTrack.TrackNumber;
                            currentTrack.Album = title;

                            tracks.Add(currentTrack);

                            previousTrack = currentTrack;
                            currentTrack = new Track();
                            if (trackInfo.Length > 0) currentTrack.TrackNumber = byte.Parse(trackInfo[1]);
                            currentTrack.Genre = physicalTrack.Genre;
                            currentTrack.IsVBR = physicalTrack.IsVBR;
                            currentTrack.Bitrate = physicalTrack.Bitrate;
                            currentTrack.CodecFamily = physicalTrack.CodecFamily;
                            currentTrack.Year = physicalTrack.Year;
                            currentTrack.PictureTokens = physicalTrack.PictureTokens;
                            currentTrack.DiscNumber = physicalTrack.DiscNumber;
                            currentTrack.Artist = "";
                            currentTrack.Title = "";
                            currentTrack.Comment = "";

                            indexRelativePosition = 0;
                        }
                        else if ("REM".Equals(firstWord, StringComparison.OrdinalIgnoreCase))
                        {
                            if (currentTrack.Comment.Length > 0) currentTrack.Comment += Settings.InternalValueSeparator;
                            currentTrack.Comment += s.Substring(firstBlank + 1, s.Length - firstBlank - 1);
                        }
                        //CATALOG 4935228173068
                        else if ("PERFORMER".Equals(firstWord, StringComparison.OrdinalIgnoreCase))
                        {
                            currentTrack.Artist = stripBeginEndQuotes(s.Substring(firstBlank + 1, s.Length - firstBlank - 1));
                        }
                        else if ("TITLE".Equals(firstWord, StringComparison.OrdinalIgnoreCase))
                        {
                            currentTrack.Title = stripBeginEndQuotes(s.Substring(firstBlank + 1, s.Length - firstBlank - 1));
                        }
                        else if (("PREGAP".Equals(firstWord, StringComparison.OrdinalIgnoreCase)) || ("POSTGAP".Equals(firstWord, StringComparison.OrdinalIgnoreCase)))
                        {
                            if (trackInfo.Length > 0) currentTrack.DurationMs += decodeTimecodeToMs(trackInfo[1]);
                        }
                        else if ("INDEX".Equals(firstWord, StringComparison.OrdinalIgnoreCase))
                        {
                            if (trackInfo.Length > 1)
                            {
                                int timeOffset = decodeTimecodeToMs(trackInfo[2]);

                                if (0 == indexRelativePosition && previousTrack != null)
                                {
                                    previousTrack.DurationMs += timeOffset - previousTimeOffset;
                                }
                                else
                                {
                                    currentTrack.DurationMs += timeOffset - previousTimeOffset;
                                }
                                previousTimeOffset = timeOffset;

                                indexRelativePosition++;
                            }
                        }

                    }

                    s = source.ReadLine();
                } // while

                if (currentTrack != null)
                {
                    if (0 == currentTrack.Artist.Length) currentTrack.Artist = artist;
                    if (0 == currentTrack.Artist.Length) currentTrack.Artist = physicalTrack.Artist;
                    if (0 == currentTrack.Title.Length) currentTrack.Title = physicalTrack.Title;
                    if (0 == currentTrack.Comment.Length) currentTrack.Comment = physicalTrack.Comment;
                    if (0 == currentTrack.TrackNumber) currentTrack.TrackNumber = physicalTrack.TrackNumber;
                    currentTrack.Album = title;
                    currentTrack.DurationMs += physicalTrack.DurationMs - previousTimeOffset;

                    tracks.Add(currentTrack);
                }
            } // using

        } // read method

    }
}
