using ATL.CatalogDataReaders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Cue;

namespace Tag.Core
{
    

    public class CueSpliter : ICore<CueInfo, string>
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
            WaveFileReader wfr = null;
            
            ICatalogDataReader reader;
            NAudio.Wave.CueList list = new CueList();

            try
            {
                reader = CatalogDataReaderFactory
                            .GetInstance()
                            .GetCatalogDataReader(cuePath);

                wfr = new WaveFileReader(wavePath);
               
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                wfr?.Close();
            }


            CueInfo data = new CueInfo
            {
                Path = cuePath,
                WavPath = wavePath,
                SavePath = savePath,
                Artists = reader.Artist,
                Title = reader.Title,
                Barcord = reader.Barcode,
                Genre = reader.Genre,
                
                Format = new WavFormat
                {
                    BlockAlign = wfr.BlockAlign,
                    BytesPerMillisecond = wfr.WaveFormat.AverageBytesPerSecond / 1000
                }
            };

            foreach (var track in reader.Tracks)
            {
                data.Track.Add(new TrackInfo
                {
                    DurationMS = track.DurationMs,
                    Title = track.Title,
                    Artist = track.Artist
                });
            }
            CueList.Add(data);
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
                using (WaveFileReader reader = new WaveFileReader(list.WavPath))
                {
                    int position = 0;
                    int num = 0;
                    foreach (var track in list.Track)
                    {
                        using (WaveFileWriter writer = new WaveFileWriter(list.SavePath + $"{num + 1}. " + track.Title + ".wav", reader.WaveFormat))
                        {
                            int start = (int)(position * list.Format.BytesPerMillisecond);
                            start -= start % reader.WaveFormat.BlockAlign;

                            int end = (int)((position + track.DurationMS) * list.Format.BytesPerMillisecond);
                            end -= end % reader.WaveFormat.BlockAlign;
                            TrimWavFile(reader, writer, start, end);
                        }
                        position += (int)track.DurationMS;

                        num++;
                        count++;
                        yield return (int)((100.0 / trackCount) * count);
                    }
                }
            }

            yield return 100;
        }

        public List<CueInfo> List()
        {
            return CueList;
        }

        private void TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
            reader.Position = startPos;
            byte[] buffer = new byte[reader.WaveFormat.BlockAlign * 100];
            while (reader.Position < endPos)
            {
                int bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }
    }
}
