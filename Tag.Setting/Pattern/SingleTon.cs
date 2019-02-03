using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag.Setting.Pattern
{
    public class SingleTon<T> where T : class, new()
    {
        private static T _instance;
        public static T Insatence
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }
}
