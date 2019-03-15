using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tag.Setting.Pattern;

namespace Tag.Setting.Setting
{
    public class FilePath : SingleTon<FilePath>
    {
        public string LangPath = Application.StartupPath + @"\Setting\Lang\";
        public string SettingPath = Application.StartupPath + @"\Setting\\";
        public string ResourcePath = Application.StartupPath + @"\Setting\Resource\";
        public string CacheImagePath = Application.StartupPath + @"\Setting\Cache\Image\";
        public string CachePath = Application.StartupPath + @"\Setting\Cache\Dummy\";
    }
}
