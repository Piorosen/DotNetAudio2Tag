using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Setting.Pattern;
using Tag.Setting.Setting;

namespace Tag.Setting
{
    public static class Global
    {
        public static Language Language => Language.Insatence;
        public static Resource Resource => Resource.Insatence;
        public static FilePath FilePath => FilePath.Insatence;
        public static DialogIdentifier DialogIdentifier => DialogIdentifier.Insatence;

        public static bool CacheImageDelete = false;
        
    }
}
