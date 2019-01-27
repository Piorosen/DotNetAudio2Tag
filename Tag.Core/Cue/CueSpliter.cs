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
            WaveFileReader wfr = null;
            
            ICatalogDataReader reader;

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
