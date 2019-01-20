using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Core.Extension
{
    class IsAdmin
    {
        public static bool Check()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity != null)
                return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
            return false;
        }
    }
}

/*
    이런 방식으로도 이 계정이 현재 어드민 계정인지 판단이 가능함.
    try {
                FileInfo fi = new FileInfo("C:\\Temp");
                if (fi.Exists)
                    fi.Delete();
                fi.CreateText().Write("Test");
                fi.Delete();
                return true;
            }catch (Exception)
            {
                return false;
            }
*/
