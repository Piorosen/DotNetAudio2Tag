using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Core.Cue;
using NAudio.Flac;
using CUETools.Codecs.FLAKE;
using CUETools.Codecs;

namespace Tag.Core.Conv.Library
{
    class Wav2Flac : IConv
    {
        private IEnumerable<int> Execute(string filePath, string resultPath, LAMEPreset preset = LAMEPreset.ABR_320)
        {
            int percent = 0;
            using (var wav = new WaveFileReader(filePath))
            {
                var config = new AudioPCMConfig(wav.WaveFormat.BitsPerSample, wav.WaveFormat.Channels, wav.WaveFormat.SampleRate);
                if (!Directory.Exists(resultPath))
                {
                    Directory.CreateDirectory(resultPath);
                }
                using (var flac = new FlakeWriter($"{resultPath}", config))
                {
                    var wavbuffer = new byte[wav.WaveFormat.BlockAlign * 100];

                    while (wav.Position < wav.Length)
                    {
                        int bytesRequired = (int)(wav.Length - wav.Position);
                        if (bytesRequired > 0)
                        {
                            int bytesToRead = Math.Min(bytesRequired, wavbuffer.Length);
                            int bytesRead = wav.Read(wavbuffer, 0, bytesToRead);
                            if (bytesRead > 0)
                            {
                                var buffer = new AudioBuffer(config, wavbuffer, wav.WaveFormat.BlockAlign * 100);
                                flac.Write(buffer);
                            }
                        }

                        var value = (int)(wav.Position * 100.0 / wav.Length);
                        if (percent == value)
                        {
                            continue;
                        }
                        if (percent != value)
                        {
                            percent = value;
                            yield return percent;
                        }
                    }
                    // wav.CopyTo(mp3);
                }
            }

            yield return 100;
        }

        public IEnumerable<int> Execute(ConvInfo info)
        {
            LAMEPreset preset = new LAMEPreset();

            foreach (var value in info.Parameter)
            {
                if (value is LAMEPreset)
                {
                    preset |= (LAMEPreset)value;
                }
            }
            
            foreach (var value in Execute(info.FilePath, info.ResultPath, preset == 0 ? LAMEPreset.ABR_320 : preset))
            {
                yield return value;
            }
        }
    }
}
