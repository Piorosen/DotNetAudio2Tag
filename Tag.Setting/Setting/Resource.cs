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
        public string Alert = Global.FilePath.ResourcePath + "Alert.png";
        public string LameDummy = Global.FilePath.ResourcePath + "Test.Dummy";
    }
}
