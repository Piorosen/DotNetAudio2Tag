using ATL.CatalogDataReaders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core
{
    public class TrackInfo
    {
        public string Title = string.Empty;
        public double DurationMS = 0.0;
        
    }
    public class WavFormat
    {
        public double BytesPerMillisecond = 0.0;
        public int BlockAlign = 0;
    }
    public class CueData
    {
        public string Path = string.Empty;
        public string Title = string.Empty;
        public string SavePath = string.Empty;
        public string WavPath = string.Empty;
        public WavFormat Format = null;
        public List<TrackInfo> Track = new List<TrackInfo>();
    }



    public class CueSpliter : ICore<CueData, string>
    {
        readonly List<CueData> CueList = new List<CueData>();

        public bool AddFile(string path)
        {
            var result = AddFile(path
                 , Path.GetDirectoryName(path) + @"\" + Path.GetFileNameWithoutExtension(path) + ".wav"
                 , Path.GetDirectoryName(path) + @"\");
            if (result)
            {
                Extension.Log.FileWrite($"{path} : add", Extension.Error.Success);
            }
            else
            {
                Extension.Log.FileWrite($"{path} : fatal", Extension.Error.Error);
            }
            return result;
        }

        public bool AddFile(string cuePath, string wavePath, string savePath)
        {
            WaveFileReader wfr = null;
            ICatalogDataReader reader;

            try
            {
                reader = CatalogDataReaderFactory
                            .GetInstance()
                            .GetCatalogDataReader(cuePath);

                wfr = new WaveFileReader(wavePath);
                
            }catch (Exception)
            {
                Extension.Log.FileWrite($"{cuePath}, {wavePath} path not found", Extension.Error.IOException);
                return false;
            }
            finally
            {
                wfr.Close();
            }

            CueData data = new CueData
            {
                Path = cuePath,
                WavPath = wavePath,
                SavePath = savePath,
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
                    Title = track.Title
                });
            }

            CueList.Add(data);
            Extension.Log.FileWrite($"CueListAdd", Extension.Error.Success);
            return true;
        }
        
        public bool Delete(int at)
        {
            if (0 <= at && at < CueList.Count)
            {
                CueList.RemoveAt(at);
                Extension.Log.FileWrite($"{at} delete", Extension.Error.Success);
                return true;
            }
            Extension.Log.FileWrite($"{at} Location Error", Extension.Error.IndexException);
            return false;
        }
        public bool Delete(CueData remove)
        {
            var result = CueList.Remove(remove);
            if (result)
            {
                Extension.Log.FileWrite($"{remove.Title} delete", Extension.Error.Success);
            }
            else
            {
                Extension.Log.FileWrite($"{remove.Title} Location Error", Extension.Error.Error);
            }
            return result;
        }

        public IEnumerable<int> Execute()
        {
            int trackCount = 0;
            for (int l = 0; l < CueList.Count; l++)
            {
                trackCount += CueList[l].Track.Count;
            }
            Extension.Log.FileWrite($"Init", Extension.Error.None);

            int count = 0;
            foreach (var list in CueList) 
            {
                using (WaveFileReader reader = new WaveFileReader(list.WavPath))
                {
                    Extension.Log.FileWrite($"File Load", Extension.Error.Success);
                    int position = 0;
                    int num = 0;
                    foreach (var track in list.Track)
                    {
                        using (WaveFileWriter writer = new WaveFileWriter(list.SavePath + $"{num}. " + track.Title + ".wav", reader.WaveFormat))
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
                        Extension.Log.FileWrite($"File Split {(int)((100.0 / trackCount) * count)} / {100}", Extension.Error.Success);
                        yield return (int)((100.0 / trackCount) * count);

                    }
                }
            }
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
