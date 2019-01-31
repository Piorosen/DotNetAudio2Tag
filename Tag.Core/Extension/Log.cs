using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Extension
{
    public static class Log /// File, Reg, Internal쪽으로 Log를 출력을 합니다. (Date 클래스 는 언제 로그가 발생했는지를 나타냅니다.)
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
        private static string m_FilePath; // 이 값은 \\temp\\ 으로 끝나야 합니다.
        private static Encoding m_enc = Encoding.UTF8; // 기본값은 Default 입니다.
        
        private static bool m_AES = false;
        
        #endregion

        /*
            <summary>
                Property는 FilePath, Encoding, RegPath를 정의 합니다.
            </summary>
        */
        #region Property
        public static string FilePath
        {
            get
            {
                return m_FilePath;
            }
        }
        public static Encoding Encoding
        {
            set
            {
                m_enc = value;
            }
            get
            {
                return m_enc;
            }
        }
        public static bool AES
        {
            set
            {
                m_AES = value;
            }
            get
            {
                return m_AES;
            }
        }
        #endregion

        /*
            <summary>
                로그 기능을 사용하기 위해 반드시 해야하는 기능입니다.
            </summary>
        */
        

        #region Prepare
        public static bool FilePrepare(string path) // File Log를 사용할 준비를 합니다.
        {
            try
            {
                if (!IsAdmin.Check()) return false; // 값을 수정이 가능한지 불가능한지 판단을 해줍니다.
                new DirectoryInfo(path).Create();
                m_FilePath = path;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();

            String FileName = FilePath + TDate() + ".log";
            StreamWriter mwriter = new StreamWriter(FileName, true, m_enc);
            Data = $"{methodBase.DeclaringType.Name}.{methodBase.Name} : {Data}";

            if (AES)
            {
                Data = new AES256().AESEncrypt256(Data);
            }
            string str = LDate() + " [" + ((int)i) + "] " + Data;
            
            mwriter.Close();
        }
        #endregion
    }
}
