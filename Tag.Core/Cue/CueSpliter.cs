using ATL.CatalogDataReaders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Cue;
using Tag.Core.Cue.Split;
using Tag.Core.Tagging;

namespace Tag.Core.Cue
{


    public class CueSpliter : ICore<CueInfo>
    {
        readonly List<CueInfo> CueList = new List<CueInfo>();

        public CueInfo this[int index] => CueList[index];

        public bool AddFile(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);

            string buffer = sr.ReadToEnd();
            sr.Close();

            File.Delete(path);

            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.Write(buffer);
            }

            return AddFile(path
                 , Path.GetDirectoryName(path) + @"\" + Path.GetFileNameWithoutExtension(path)
                 , Path.GetDirectoryName(path) + @"\");
        }

        public bool AddFile(string cuePath, string wavePath, string savePath)
        {
            ICatalogDataReader reader;

            try
            {
                reader = CatalogDataReaderFactory
                            .GetInstance()
                            .GetCatalogDataReader(cuePath);
                switch (reader.Extension)
                {
                    case AudioType.FLAC:
                        wavePath += ".flac";
                        break;
                    case AudioType.WAV:
                        wavePath += ".wav";
                        break;
                    case AudioType.NONE:
                        throw new Exception();
                }
            }
            catch (Exception)
            {
                return false;
            }

            var filereader = new AudioFileReader(wavePath);

            CueInfo info = new CueInfo
            {
                Artist = reader.Artist,
                Barcode = reader.Barcode,
                Path = reader.Path,
                SavePath = savePath,
                WavPath = wavePath,
                Title = reader.Title,
                AudioType = reader.Extension,
                REM = new REM
                {
                    Date = reader.Date,
                    DiscId = reader.DiscId,
                    Genre = reader.Genre
                },
                WaveFormat = filereader.WaveFormat
            };
            double StartPosition = 0.0;
            foreach (var value in reader.Tracks)
            {
                StartPosition += value.TimeOffSet;
                info.Track.Add(new TrackInfo
                {
                    Album = value.Album,
                    Artist = value.Artist,
                    Composer = value.Composer,
                    DurationMS = value.DurationMs < 0 ? filereader.TotalTime.TotalMilliseconds - StartPosition : value.DurationMs,
                    Title = value.Title,
                    Track = value.TrackNumber,
                    StartPosition = StartPosition,
                    TimeOffSet = value.TimeOffSet
                });
                StartPosition += value.DurationMs;
            }
            CueList.Add(info);
            return true;
        }

        public bool AddFile(CueInfo file)
        {
            return AddFile(file.Path, file.WavPath, file.SavePath);
        }

        public bool Delete(int at)
        {
            if (0 <= at && at < CueList.Count)
            {
                CueList.RemoveAt(at);
                return true;
            }
            return false;
        }
        public bool Delete(CueInfo remove)
        {
            return CueList.Remove(remove);
        }

        public IEnumerable<int> Execute()
        {
            int cueCount = CueList.Count;

            for (int index = 0; index < cueCount; index++)
            {
                ISplit user = null;

                if (CueList[index].AudioType == AudioType.WAV)
                {
                    user = new WaveSplit();
                }
                else if (CueList[index].AudioType == AudioType.FLAC)
                {
                    user = new FlacSplit();
                }
                // 커스텀 Spliter 사용함.
                else if (CueList[index].AudioType == AudioType.NONE)
                {
                    user = new UserSplit();
                }

                foreach (var value in CueList[index].Track)
                {
                    char[] chars = Path.GetInvalidFileNameChars();
                    for (int i = 0; i < value.Title.Length; i++)
                    {
                        for (int w = 0; w < chars.Length; w++)
                        {
                            if (value.Title[i] == chars[w])
                            {
                                value.Title = value.Title.Remove(i, 1);
                                break;
                            }
                        }
                    }
                }

                foreach (var value in user.Execute(CueList[index]))
                {
                    yield return (int)((value / (index + 1)) * cueCount);
                }

                AudioTagging tagging = new AudioTagging();
                tagging.CueFile(CueList[index]);
                foreach (var i in tagging.Execute())
                {

                }
            }
            
            yield return 100;
        }

        public List<CueInfo> List()
        {
            return CueList;
        }
    }
}
