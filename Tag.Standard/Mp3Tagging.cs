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
    public class Mp3Tagging : ICore<string>
    {
        public bool AddFile(string path)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int at)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string remove)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> Execute()
        {
            throw new NotImplementedException();
        }

        public void Tagging(string wave, string outFileName)
        {
            TagLib.Id3v2.Tag taginfo = new TagLib.Id3v2.Tag();

            
            
        }
    }
}