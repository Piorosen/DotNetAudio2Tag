using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag.Setting.Pattern;

namespace Tag.Setting.Setting
{
    public class Resource : SingleTon<Resource>
    {
        public string Alert = FilePath.ResourcePath + "Alert.png";
    }
}
