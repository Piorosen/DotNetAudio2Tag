using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;


namespace CustomDesign.Core
{
    public class CustomDesign
    {
        public Observe Observe = new Observe();
        readonly Stack<CustomType> TypeStack = new Stack<CustomType>();
        readonly Stack<CustomType> ValueStack = new Stack<CustomType>();

        public bool LoadJson(string data)
        {
            JArray list;
            try
            {
                list = JArray.Parse(data);
            }
            catch (Exception)
            {
                return false;
            }

            for (int i = 0; i < list.Count; i++)
            {
                var Type = Observe[list[i]["Name"].ToString()];
                var Enum = list[i].Children();
                TypeStack.Push(Type);
                foreach (var token in Enum)
                {
                    SelectCode(token);
                }
                TypeStack.Pop();
            }
            return true;
        }

        void SelectCode(JToken token)
        {
            JProperty JpToken = token.ToObject<JProperty>();

            if (JpToken.Name == "Field")
            {
                foreach (var internalField in JpToken.Children().Children())
                {
                    CustomType type = TypeStack.Peek();
                    TypeStack.Push(GetField(internalField, type));
                    foreach (var nextToken in internalField)
                    {
                        SelectCode(nextToken);
                    }
                    TypeStack.Pop();
                }

            }
            else if (JpToken.Name == "Property")
            {
                foreach (var internalProperty in JpToken.Children().Children())
                {
                    CustomType type = TypeStack.Peek();
                    TypeStack.Push(GetProperty(internalProperty, type));
                    foreach (var nextToken in internalProperty)
                    {
                        SelectCode(nextToken);
                    }
                    TypeStack.Pop();
                }
            }
            else if (JpToken.Name == "Value" || JpToken.Name == "Constructor")
            {
                var customType = TypeStack.Pop();
                var type = customType.Value.GetType();
                object value = null;
                if (JpToken.Name == "Value")
                {
                    value = Convert.ChangeType(JpToken.Value, type);
                }
                else
                {
                    var list = GetParsing(JpToken);
                    ConstructorInfo constructor = type.GetConstructor(list.Select((data) => data.type).ToArray());
                    value = constructor?.Invoke(list.Select((data) => data.value).ToArray());
                }

                customType.SetValue(TypeStack.Peek().Value, value);
                TypeStack.Push(customType);
            }
            else if (JpToken.Name == "CallFunc")
            {
                var q = JpToken.Value["Param"];
                var customType = TypeStack.Peek();
                var list = GetParsing(q);
                var type = list.Select((data) => data.type).ToArray();
                var value = list.Select((data) => data.value).ToArray();
                MethodInfo t = customType.Type.GetMethod(JpToken.Value["Name"].ToString());
                t?.Invoke(customType.Value, value.Length == 0 ? null : value);
            }
            else if (JpToken.Name == "EventHandler")
            {
                var customType = TypeStack.Pop();

                EventInfo info = customType.Type.GetEvent(JpToken.Value["Name"].ToString());
                MethodInfo minfo = TypeStack.Peek().Type.GetMethod(JpToken.Value["Method"].ToString());
                Delegate method = Delegate.CreateDelegate(info.EventHandlerType, TypeStack.Peek().Value, minfo);
                info.AddEventHandler(customType.Value, method);
                TypeStack.Push(customType);
            }
        }

        public CustomType GetFunction(JToken token, CustomType type)
        {

            return new CustomType(null);
        }

        public List<(Type type, object value)> GetParsing(JToken token)
        {
            var ConType = new List<Type>();

            List<(Type type, object value)> list = new List<(Type, object)>();
            
            foreach (var item in token.Children().Children())
            {
                var internalProperty = item.ToObject<JProperty>();
                var types = Type.GetType(internalProperty.Name.Split('$')[0]);

                list.Add((types, Convert.ChangeType(internalProperty.Value, types)));
            }
            return list;
           
        }

        public CustomType GetField(JToken token, CustomType type)
        {
            var name = token["Name"].ToString();
            var tmp = Observe.GetField(type, name, BindingFlags.NonPublic | BindingFlags.Instance);
            if (tmp.Type == null)
            {
                tmp = Observe.GetField(type, name, BindingFlags.Public | BindingFlags.Instance);
            }
            CustomType w = new CustomType(tmp.Field, tmp.Type.Value);
            foreach (var t in token.Children())
            {
                return w;
            }
            return w;
        }

        public CustomType GetProperty(JToken token, CustomType type)
        {
            var name = token["Name"].ToString();
            var tmp = Observe.GetProperty(type, name, BindingFlags.NonPublic | BindingFlags.Instance);
            if (tmp.Type == null)
            {
                tmp = Observe.GetProperty(type, name, BindingFlags.Public | BindingFlags.Instance);
            }
            CustomType w = new CustomType(tmp.Property, tmp.Type.Value);
            foreach (var t in token.Children())
            {
                return w;
            }
            return w;
        }
    }
}
