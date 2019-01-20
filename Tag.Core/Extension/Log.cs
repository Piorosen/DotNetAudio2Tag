using Library.Secure;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Extension
{
    static class Log /// File, Reg, Internal쪽으로 Log를 출력을 합니다. (Date 클래스 는 언제 로그가 발생했는지를 나타냅니다.)
    {
        /*
            <summary>
                본 라이브러리르는 
                정적으로 Reg값 추가,
                정적으로 파일 내용 수정이 가능합니다.
                모든 것이 정적으로 처리가 가능합니다.
                암호화 처리 기능도 가능합니다.
                암호화 처리는 Library.AES256 에 있습니다.
            </summary>
        */

        /*
            <summray>
                이 클래스에 사용되는 변수를 정의합니다.
            </summary>
        */
        #region Varience
        private static string FilePath; // 이 값은 \\temp\\ 으로 끝나야 합니다.
        private static string RegPath; // 기본적인 레지는 CurrentUser 위치 합니다.
        private static Encoding enc = Encoding.Default; // 기본값은 Default 입니다.
        private static StreamWriter writer;
        private static bool AES = false;


        #endregion

        /*
            <summary>
                Property는 FilePath, Encoding, RegPath를 정의 합니다.
            </summary>
        */
        #region Property
        public static string m_FilePath
        {
            set
            {
                FilePath = value;
            }
        }

        public static string m_RegPath
        {
            set
            {
                RegPath = value;
            }
        }

        public static Encoding m_Encoding
        {
            set
            {
                enc = value;
            }
        }
        public static bool m_AES
        {
            set
            {
                AES = value;
            }
        }
        #endregion

        /*
            <summary>
                로그 기능을 사용하기 위해 반드시 해야하는 기능입니다.
            </summary>
        */
        #region Prepare

        public static bool Prepare() // File 과 Reg 값 2개를 시작할 준비를 합니다.
        {
            /*
                <summary>
                    전체 준비를 합니다.
                </summary>
            */
            if (FilePrepare() && RegPrepare()) return true;
            return false;
        }
        public static bool FilePrepare() // File Log를 사용할 준비를 합니다.
        {
            try
            {
                if (!IsAdmin.Check()) return false; // 값을 수정이 가능한지 불가능한지 판단을 해줍니다.
                new DirectoryInfo(FilePath).Create();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool RegPrepare() // Reg Log를 사용할 준비를 합니다.
        {
            if (!IsAdmin.Check()) return false; // 값을 수정이 가능한지 불가능한지 판단을 해줍니다.
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey(RegPath);
            if (rkey == null)
            {
                rkey = Registry.CurrentUser.CreateSubKey(RegPath);
                if (rkey == null) return false;
            }
            return true;
        }

        #endregion

        /*
            <summary>
                현재 날짜 값을 가져옵니다.
                이 값은 Date Class와 연동됩니다.
            </summary>
        */
        #region Date Class
        public static class TitleDate
        {
            public static string Name = "";
            public static bool Year = true;
            public static bool Month = true;
            public static bool Day = true;
            public static bool Hour = false;
            public static bool Min = false;
            public static bool Second = false;
        }
        public static class LogDate
        {
            public static bool Year = true;
            public static bool Month = true;
            public static bool Day = true;
            public static bool Hour = true;
            public static bool Min = true;
            public static bool Second = true;
        }
        #endregion
        #region GetDate
        private static string TDate()
        {
            String T = "[";
            if (TitleDate.Year || TitleDate.Month || TitleDate.Day)
            {
                T += " ";
            }

            if (TitleDate.Year == true)
            {
                T += DateTime.Now.Year.ToString();
            }
            if (TitleDate.Month == true)
            {
                T += DateTime.Now.Month.ToString();
            }
            if (TitleDate.Day == true)
            {
                T += DateTime.Now.Day.ToString();
            }

            if (TitleDate.Hour || TitleDate.Min || TitleDate.Second)
            {
                T += " ";
            }

            if (TitleDate.Hour == true)
            {
                T += DateTime.Now.Hour.ToString();
            }
            if (TitleDate.Min == true)
            {
                T += DateTime.Now.Minute.ToString();
            }
            if (TitleDate.Second == true)
            {
                T += DateTime.Now.Second.ToString();
            }

            T += " ]";
            return T;
        }
        private static string LDate()
        {
            String T = "[";
            if (LogDate.Year || LogDate.Month || LogDate.Day)
            {
                T += " ";
            }

            if (LogDate.Year == true)
            {
                T += DateTime.Now.Year.ToString();
            }
            if (LogDate.Month == true)
            {
                T += DateTime.Now.Month.ToString();
            }
            if (LogDate.Day == true)
            {
                T += DateTime.Now.Day.ToString();
            }

            if (LogDate.Hour || LogDate.Min || LogDate.Second)
            {
                T += " ";
            }

            if (LogDate.Hour == true)
            {
                T += DateTime.Now.Hour.ToString();
            }
            if (LogDate.Min == true)
            {
                T += DateTime.Now.Minute.ToString();
            }
            if (LogDate.Second == true)
            {
                T += DateTime.Now.Second.ToString();
            }

            T += " ]";
            return T;
        }
        #endregion

        /*
            <summary>
                로그 기록을 작성을 합니다.
                FileWrite : 파일에 출력을 합니다.    
                RegWrite : 레지에 출력을 합니다.
            </summary>
        */
        #region Write
        public static void FileWrite(string Data, Error i) // FilePath값이 \\값으로 끝나야합니다.
        {
            String FileName = FilePath + TDate() + ".log";

            if (AES)
            {
                Data = new AES256().AESEncrypt256(Data);
            }
            string str = LDate() + " [" + ((int)i) + "] " + Data;

            writer = new StreamWriter(FileName, true, enc);

            writer.WriteLine(str);
            writer.Close();
        }
        public static void RegWrite(string Data, Error i)
        {
            if (AES)
            {
                Data = new AES256().AESEncrypt256(Data);
            }
            string str = "[" + ((int)i) + "] " + Data;
            if (AES)
            {
                str = new AES256().AESEncrypt256(str);
            }
            Registry.CurrentUser.CreateSubKey(RegPath).SetValue(LDate(), str);
        }
        #endregion
    }
}
