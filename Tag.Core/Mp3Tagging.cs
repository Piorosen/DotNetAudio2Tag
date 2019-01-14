using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Lame;
using NAudio.Wave;

namespace Tag.Core
{
    public class Mp3Tagging
    {

        public void b(string wave, string outFileName)
        {
            using (var reader = new NAudio.Wave.WaveFileReader(wave))
            {
                using (var writeras = new NAudio.Lame.LameMP3FileWriter(outFileName, reader.WaveFormat, 320))
                {
                    reader.CopyTo(writeras);
                }
           }


        }
    }
}