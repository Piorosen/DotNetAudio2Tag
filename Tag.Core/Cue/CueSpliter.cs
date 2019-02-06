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

namespace Tag.Core.Cue
{


    public class CueSpliter : ICore<CueInfo>
    {
        readonly List<CueInfo> CueList = new List<CueInfo>();

        public CueInfo this[int index] => CueList[index];

        public bool AddFile(string path)
        {

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
                WaveFormat = new AudioFileReader(wavePath).WaveFormat
            };
            double StartPosition = 0.0;
            foreach (var value in reader.Tracks)
            {
                info.Track.Add(new TrackInfo
                {
                    Artist = value.Artist,
                    Composer = value.Composer,
                    DurationMS = value.DurationMs,
                    Title = value.Title,
                    Track = value.TrackNumber,
                    StartPosition = StartPosition
                });
                StartPosition += value.DurationMs;
            }
            CueList.Add(info);
            return true;
        }

        public bool AddFile(CueInfo file)
        {
            throw new NotImplementedException();
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
                if (CueList[index].AudioType == AudioType.WAV)
                {
                    WaveSplit wav = new WaveSplit();
                    foreach (var value in wav.Execute(CueList[index]))
                    {
                        yield return (int)((value / (index + 1)) * cueCount);
                    }
                }
                else if (CueList[index].AudioType == AudioType.FLAC)
                {
                    FlacSplit flac = new FlacSplit();
                    foreach (var track in CueList[index].Track)
                    {
                        foreach (var value in flac.Execute(CueList[index]))
                        {
                            yield return (int)((value / (index + 1)) * cueCount);
                        }
                    }
                }
                // 커스텀 Spliter 사용함.
                else if (CueList[index].AudioType == AudioType.NONE)
                {
                    UserSplit user = new UserSplit();
                    foreach (var track in CueList[index].Track)
                    {
                        foreach (var value in user.Execute(CueList[index]))
                        {
                            yield return (int)((value / (index + 1)) * cueCount);
                        }
                    }
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
