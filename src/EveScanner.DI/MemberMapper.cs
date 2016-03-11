//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="MemberMapper.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;

    using EveScanner.IoC.Attributes;
    using EveScanner.IoC.Extensions;

    /// <summary>
    /// Exposes the Members (Fields and Properties) of a class allowing them to be get and set as needed.
    /// </summary>
    /// <typeparam name="TOutputType">Type to map</typeparam>
    public class MemberMapper<TOutputType> where TOutputType : class
    {
        /// <summary>
        /// Holds the one true instance of this singleton.
        /// </summary>
        private static MemberMapper<TOutputType> instance = new MemberMapper<TOutputType>();

        /// <summary>
        /// Holds the compiled Get methods.
        /// </summary>
        private Dictionary<string, Func<TOutputType, object>> getMethods;

        /// <summary>
        /// Holds the compiled Set methods.
        /// </summary>
        private Dictionary<string, Action<TOutputType, object>> setMethods;

        /// <summary>
        /// Holds a collection of all the property names.
        /// </summary>
        private Collection<string> members;

        /// <summary>
        /// Prevents a default instance of the <see cref="MemberMapper{TOutputType}"/> class from being created.
        /// </summary>
        private MemberMapper()
        {
            // This method does all the reflection digging, setting up getters and setters as required.
            this.members = new Collection<string>();
            this.getMethods = new Dictionary<string, Func<TOutputType, object>>(StringComparer.OrdinalIgnoreCase);
            this.setMethods = new Dictionary<string, Action<TOutputType, object>>(StringComparer.OrdinalIgnoreCase);

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

                    this.getMethods.Add(name, pi.GetValueGetter<TOutputType>());
                    if (pi.CanWrite)
                    {
                        this.setMethods.Add(name, pi.GetValueSetter<TOutputType>());
                    }

                    this.Members.Add(name);
                }
            }

            foreach (FieldInfo fi in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                IgnoreMemberAttribute dnmattr = (IgnoreMemberAttribute)Attribute.GetCustomAttribute(fi, typeof(IgnoreMemberAttribute));

                if (dnmattr == null)
                {
                    string name = fi.Name;
                    if (!this.getMethods.ContainsKey(name))
                    {
                        MapFromAttribute mapattr = (MapFromAttribute)Attribute.GetCustomAttribute(fi, typeof(MapFromAttribute));
                        if (mapattr != null)
                        {
                            name = ((MapFromAttribute)mapattr).Name;
                        }

                        this.getMethods.Add(name, fi.GetValueGetter<TOutputType>());
                        this.setMethods.Add(name, fi.GetValueSetter<TOutputType>());
                        this.Members.Add(name);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the one true instance.
        /// </summary>
        public static MemberMapper<TOutputType> Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Gets the collection of all member names.
        /// </summary>
        public Collection<string> Members
        {
            get
            {
                return this.members;
            }
        }

        /// <summary>
        /// Sets the value of a property or field on an object.
        /// </summary>
        /// <param name="mapObject">Object to apply value to</param>
        /// <param name="memberName">Name of field or property</param>
        /// <param name="value">Value to apply</param>
        public void SetValue(TOutputType mapObject, string memberName, object value)
        {
            if (this.setMethods.ContainsKey(memberName))
            {
                this.setMethods[memberName](mapObject, value);
            }
        }

        /// <summary>
        /// Gets the value of a property or field from an object
        /// </summary>
        /// <param name="mapObject">Object to get value from</param>
        /// <param name="memberName">Name of field or property</param>
        /// <returns>Value of field or property</returns>
        public object GetValue(TOutputType mapObject, string memberName)
        {
            if (this.getMethods.ContainsKey(memberName))
            {
                return this.getMethods[memberName](mapObject);
            }

            return null;
        }
    }
}
