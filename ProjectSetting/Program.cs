using System;
using System.IO;
using System.Reflection;

namespace ProjectSetting
{
    class Program
    {
        static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        static void Main(string[] args)
        {
            var SlnDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;
            var dir = SlnDir.FullName;

            if (Directory.Exists(dir + @"\Tag.WPF\bin\Debug"))
            {
                Directory.Delete(dir + @"\Tag.WPF\bin\Debug", true);
            }
            else
            {
                Directory.CreateDirectory(dir + @"\Tag.WPF\bin\Debug");
            }

            DirectoryCopy(dir + @"\Tool", dir + @"\Tag.WPF\bin\Debug", true);
        }
    }
}