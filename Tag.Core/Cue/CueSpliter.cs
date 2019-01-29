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
    

    public class CueSpliter :ICore<CueInfo>
    {
        readonly List<CueInfo> CueList = new List<CueInfo>();

        public CueInfo this[int index] => CueList[index];

        public bool AddFile(string path)
        {
            return AddFile(path
                 , Path.GetDirectoryName(path) + @"\" + Path.GetFileNameWithoutExtension(path) + ".wav"
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
            }
            catch (Exception e)
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
                }
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
            return true;
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
            int trackCount = 0;
            for (int l = 0; l < CueList.Count; l++)
            {
                trackCount += CueList[l].Track.Count;
            }
            
            int count = 0;
            foreach (var list in CueList)
            {
                if (list.AudioType == AudioType.WAV)
                {
                    WaveSplit wav = new WaveSplit();
                    foreach (var track in list.Track)
                    {
                        wav.Execute(list.WavPath, list.SavePath, track);
                        yield return (int)((100.0 / trackCount) * count);
                    }
                }
                else if (list.AudioType == AudioType.FLAC)
                {
                    FlacSplit flac = new FlacSplit();
                    foreach (var track in list.Track)
                    {
                        flac.Execute(list.WavPath, list.SavePath, track);
                        yield return (int)((100.0 / trackCount) * count);
                    }
                }
                // 커스텀 Spliter 사용함.
                else if (list.AudioType == AudioType.NONE)
                {
                    UserSplit user = new UserSplit();
                    foreach (var track in list.Track)
                    {
                        user.Execute(list.WavPath, list.SavePath, track);
                        yield return (int)((100.0 / trackCount) * count);
                    }
                }
                count++;
            }

            yield return 100;
        }


    }
}
