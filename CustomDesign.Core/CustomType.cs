using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CustomDesign.Core
{
    public class CustomType
    {
        public CustomType(Object Info, Object value)
        {
            if (Info is PropertyInfo)
            {
                Property = (PropertyInfo)Info;
            }
            else if (Info is FieldInfo)
            {
                Field = (FieldInfo)Info;
            }
            Value = value;
        }
        public CustomType(Object value, [CallerMemberName] string Name = "")
        {
            Value = value;
            this.Name = Name;
        }
        public String Name = null;
        public Type Type => Value?.GetType();
        public Object Value { get; set; }

        public PropertyInfo Property = null;
        public FieldInfo Field = null;

        public bool SetValue(object obj, object value)
        {
            if (Field == null && Property == null)
            {
                return false;
            }

            Field?.SetValue(obj, value);
            Property?.SetValue(obj, value);
            return true;
        }
    }
}
