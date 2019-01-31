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
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string Default, StringBuilder result, int size, string path);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string path);

        public static string GetOption(string Section, string Key, string Path)
        {
            StringBuilder sb = new StringBuilder(256);
            GetPrivateProfileString(Section, Key, null, sb, 256, Path);
            return sb.ToString();
        }

        public static long GetOption(string Section, string Key, string Val, string Path)
        {
            return WritePrivateProfileString(Section, Key, Val, Path);
        }
    }
}
