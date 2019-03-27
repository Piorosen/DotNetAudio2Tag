using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Setting.Setting
{
    class Config
    {
        public static string Path = string.Empty;

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string Default, StringBuilder result, int size, string path);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string path);

        public static string GetOption(string Section, string Key, string Path = null)
        {
            if (Path == null)
            {
                Path = Config.Path;
            }
            StringBuilder sb = new StringBuilder(256);
            
            GetPrivateProfileString(Section, Key, null, sb, 256, Path);
            var encode = Encoding.UTF8.GetBytes(sb.ToString());
            return Encoding.UTF8.GetString(encode, 0, encode.Length);
        }

        public static long SetOption(string Section, string Key, string Val, string Path = null)
        {
            if (Path == null)
            {
                Path = Config.Path;
            }
            return WritePrivateProfileString(Section, Key, Val, Path);
        }

    }
}
