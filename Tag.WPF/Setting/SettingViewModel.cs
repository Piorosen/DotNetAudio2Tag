
using Library;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tag.Setting;

namespace Tag.WPF
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        private string _capacity = "0 Bytes";

        public string Capacity { get => _capacity; set
            {
                _capacity = value;
                OnPropertyChange();
            }
        }

        List<string> Index = new List<string>();

        public bool[] bIndex { get; set; } = new bool[4] { false, false, false, false };

        public SettingViewModel()
        {
            Index.Add("en-us.lang");
            Index.Add("ko-kr.lang");
            Index.Add("ja-jp.lang");
            Index.Add("other.lang");

            ChangeLang(Global.Setting.LangFile, true);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChange([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        public void ChangeLang(string Tag, bool First = false)
        {
            if (First == false)
            {
                Global.Language.Load(Tag);
            }
            Global.Setting.LangIndex = Index.IndexOf(Tag).ToString();
            Global.Setting.LangFile = Tag;
            Global.Setting.Save();
            for (int i = 0; i < bIndex.Length; i++)
            {
                bIndex[i] = false;
            }
            bIndex[Index.IndexOf(Tag)] = true;
            OnPropertyChange(nameof(bIndex));
        }

        public void CacheRemove()
        {
            DirectoryInfo dummy = new DirectoryInfo(Global.FilePath.CachePath);
            DirectoryInfo image = new DirectoryInfo(Global.FilePath.CacheImagePath);
            BigInteger num = BigInteger.Zero;

            foreach (var i in dummy.GetFiles())
            {
                i.Delete();
            }
            foreach (var i in image.GetFiles())
            {
                i.Delete();
            }
            CapacityUpdate();
        }

        public void CapacityUpdate()
        {
            DirectoryInfo dummy = new DirectoryInfo(Global.FilePath.CachePath);
            DirectoryInfo image = new DirectoryInfo(Global.FilePath.CacheImagePath);
            BigInteger num = BigInteger.Zero;

            foreach (var i in dummy.GetFiles())
            {
                num += i.Length;
            }
            foreach (var i in image.GetFiles())
            {
                num += i.Length;
            }
            Capacity = CapacityManage.Change(num);
        }
    }
}
