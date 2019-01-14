using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core
{
    public class CueSpliter
    {
        public void Test()
        {
            ATL.CatalogDataReaders
                .ICatalogDataReader reader = ATL.CatalogDataReaders
                                                        .CatalogDataReaderFactory
                                                        .GetInstance()
                                                        .GetCatalogDataReader(@"D:\data\Coe\MYTH&ROID - HYDRA.cue");
            
            NAudio.Wave.CueWaveFileReader cue = new NAudio.Wave.CueWaveFileReader(@"D:\data\Coe\MYTH&ROID - HYDRA.wav");
            cue.Close();
            //NAudio.Wave.CueWaveFileWriter data = new NAudio.Wave.CueWaveFileWriter(@"D:\data\Coe\MYTH&ROID - HYDRA.wav"
            //                                                            , new NAudio.Wave.WaveFormat((int)reader.Tracks[0].SampleRate
            //                                                                                       , reader.Tracks[0].Bitrate
            //  
            CueList f = new CueList();
            int s = 0, e = 0;
            for (int i = 0; i < reader.Tracks.Count; i++)
            {
                e += reader.Tracks[i].Duration;
                TrimWavFile(@"D:\data\Coe\MYTH&ROID - HYDRA.wav", $"D:\\data\\Coe\\{i + 1}. {reader.Tracks[i].Title}.wav"
                                   , new TimeSpan(0,0, s), reader.Tracks[i]);
                s += reader.Tracks[i].Duration;
            }
                
        }
        public static void TrimWavFile(string inPath, string outPath, TimeSpan cutFromStart, ATL.Track track)
        {
            using (WaveFileReader reader = new WaveFileReader(inPath))
            {
                using (WaveFileWriter writer = new WaveFileWriter(outPath, reader.WaveFormat))
                {
                    //int bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000;
                    float bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000f;
                    int startPos = (int)(cutFromStart.TotalMilliseconds * bytesPerMillisecond);
                    startPos = startPos - startPos % reader.WaveFormat.BlockAlign;

                    int endBytes = (int)((cutFromStart.TotalMilliseconds + track.DurationMs) * bytesPerMillisecond);
                    endBytes = endBytes - endBytes % reader.WaveFormat.BlockAlign;
                    int endPos = (int)reader.Length - endBytes;

                    TrimWavFile(reader, writer, startPos, endBytes);
                }
            }
        }
        private static void TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
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
