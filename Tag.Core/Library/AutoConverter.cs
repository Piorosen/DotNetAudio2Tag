using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core
{
    class AutoConverter
    {
        readonly CueSpliter cueSpliter = new CueSpliter();
        readonly Wav2Mp3Converter wav2Mp3 = new Wav2Mp3Converter();
        readonly Mp3Tagging tagging = new Mp3Tagging();
        
        
        void AutoConverting(string cueFile)
        {


            cueSpliter.AddFile(cueFile);
            var list = cueSpliter.List();
            cueSpliter.Execute();
            for (int i = 0; i < list.Count; i++)
            {
                for (int k = 0; k < list[i].Track.Count; k++)
                {
                    wav2Mp3.AddFile(list[i].SavePath + $"{k + 1}. " + list[i].Track[k].Title + ".wav");
                }
            }
            wav2Mp3.Execute();
            

        }

    }
}
