//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Injector.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using EveOnlineApi.Interfaces.Xml;

    /// <summary>
    /// Used to dynamically resolve types.
    /// </summary>
    public static class Injector
    {
        /// <summary>
        /// Holds our list of implementations.
        /// </summary>
        private static Dictionary<Type, Type> implementations = new Dictionary<Type, Type>();

        /// <summary>
        /// Initializes static members of the <see cref="Injector"/> class.
        /// </summary>
        static Injector()
        {
            RegisterTypeForInjection(typeof(IAllianceXmlDataProvider), typeof(FileBackedEveOnlineXmlApi));
            RegisterTypeForInjection(typeof(ICallListXmlDataProvider), typeof(FileBackedEveOnlineXmlApi));
            RegisterTypeForInjection(typeof(ICharacterXmlDataProvider), typeof(FileBackedEveOnlineXmlApi));
            RegisterTypeForInjection(typeof(ICorporationXmlDataProvider), typeof(FileBackedEveOnlineXmlApi));
        }

        /// <summary>
        /// Registers a type to be injected later.
        /// </summary>
        /// <param name="interfaceType">Interface Type</param>
        /// <param name="implementationType">Implementation Type</param>
        public static void RegisterTypeForInjection(Type interfaceType, Type implementationType)
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException("interfaceType");
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException("implementationType");
            }

            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException("The type provided was not an interface type.", "interfaceType");
            }

            if (implementationType.IsInterface)
            {
                throw new ArgumentException("The type provided was an interface.", "implementationType");
            }

            if (!interfaceType.IsAssignableFrom(implementationType))
            {
                throw new ArgumentException("The implementation type cannot be cast to the interface type.", "implementationType");
            }

            if (implementations.ContainsKey(interfaceType))
            {
                implementations.Remove(interfaceType);
            }

            implementations.Add(interfaceType, implementationType);
        }

        /// <summary>
        /// Resolves an interface type to the implementation type and calls the default constructor. This is slow, and will be optimized later.
        /// </summary>
        /// <typeparam name="T">Interface Type</typeparam>
        /// <returns>Initialized Object</returns>
        public static T Resolve<T>()
        {
            Type targetType = null;

            if (!implementations.ContainsKey(typeof(T)))
            {
                Type x = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(T) != t && typeof(T).IsAssignableFrom(t)).FirstOrDefault();
                if (x != null)
                {
                    implementations[typeof(T)] = x;
                }
            }

            targetType = implementations[typeof(T)];

            return (T)Activator.CreateInstance(targetType);
        }
    }
}
