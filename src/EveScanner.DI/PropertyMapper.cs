namespace EveScanner.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using EveScanner.IoC.Attributes;
    using EveScanner.IoC.Extensions;

    public class PropertyMapper<TOutputType> where TOutputType : class
    {
        private static PropertyMapper<TOutputType> instance = null;

        private Dictionary<string, Func<TOutputType, object>> getMethods = new Dictionary<string, Func<TOutputType, object>>(StringComparer.InvariantCultureIgnoreCase);
        private Dictionary<string, Action<TOutputType, object>> setMethods = new Dictionary<string, Action<TOutputType, object>>(StringComparer.InvariantCultureIgnoreCase);
        public List<string> Properties = new List<string>();

        public static PropertyMapper<TOutputType> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PropertyMapper<TOutputType>();
                }

                return instance;
            }
        }

        private PropertyMapper()
        {
            Type t = typeof(TOutputType);
            foreach (PropertyInfo pi in t.GetProperties())
            {
                IgnoreMemberAttribute dnmattr = (IgnoreMemberAttribute)Attribute.GetCustomAttribute(pi, typeof(IgnoreMemberAttribute));

                if (dnmattr == null)
                {
                    string name = pi.Name;
                    MapFromAttribute mapattr = (MapFromAttribute)Attribute.GetCustomAttribute(pi, typeof(MapFromAttribute));
                    if (mapattr != null)
                    {
                        name = ((MapFromAttribute)mapattr).Name;
                    }

                    getMethods.Add(name, pi.GetValueGetter<TOutputType>());
                    if (pi.CanWrite)
                    {
                        setMethods.Add(name, pi.GetValueSetter<TOutputType>());
                    }
                    Properties.Add(name);
                }
            }

            foreach (FieldInfo fi in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                IgnoreMemberAttribute dnmattr = (IgnoreMemberAttribute)Attribute.GetCustomAttribute(fi, typeof(IgnoreMemberAttribute));

                if (dnmattr == null)
                {
                    string name = fi.Name;
                    if (!getMethods.ContainsKey(name))
                    {
                        MapFromAttribute mapattr = (MapFromAttribute)Attribute.GetCustomAttribute(fi, typeof(MapFromAttribute));
                        if (mapattr != null)
                        {
                            name = ((MapFromAttribute)mapattr).Name;
                        }

                        getMethods.Add(name, fi.GetValueGetter<TOutputType>());
                        setMethods.Add(name, fi.GetValueSetter<TOutputType>());
                        Properties.Add(name);
                    }
                }
            }
        }

        public void SetValue(TOutputType mapObject, string propertyName, object value)
        {
            if (this.setMethods.ContainsKey(propertyName))
            {
                this.setMethods[propertyName](mapObject, value);
            }
        }

        public object GetValue(TOutputType mapObject, string propertyName)
        {
            if (this.getMethods.ContainsKey(propertyName))
            {
                return this.getMethods[propertyName](mapObject);
            }

            return null;
        }
    }
}
