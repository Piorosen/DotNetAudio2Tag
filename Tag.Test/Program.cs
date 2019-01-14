using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Cue 파일 경로 : ");
            string cue = Console.ReadLine().Trim('\"');
            Console.Write("Wav 파일 경로 : ");
            string wav = Console.ReadLine().Trim('\"'); ;
            Console.Write("저장할 경로 : ");
            string save = Console.ReadLine().Trim('\"');
            
            Tag.Core.CueSpliter cueSplit = new Core.CueSpliter();
            cueSplit.Test(cue, wav, save);
        }
    }
}
