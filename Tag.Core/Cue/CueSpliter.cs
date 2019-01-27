using ATL.CatalogDataReaders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Cue;

namespace Tag.Core.Cue
{
    

    public class CueSpliter 
    {
        readonly List<CueInfo> CueList = new List<CueInfo>();

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
            catch (Exception)
            {
                return false;
            }

            CueInfo info = new CueInfo
            {
                Artist = reader.Artist,
                Barcord = reader.Barcode,
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

    }
}
