//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Injector.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Basic IoC injector
    /// </summary>
    public static class Injector
    {
        /// <summary>
        /// Holds the implementation cache for ALL implementations
        /// </summary>
        private static Dictionary<Type, InjectionType[]> allImplementations = new Dictionary<Type, InjectionType[]>();

        /// <summary>
        /// Holds the implementation cache for default implementations.
        /// </summary>
        private static Dictionary<Type, InjectionType> defaultImplementations = new Dictionary<Type, InjectionType>();

        /// <summary>
        /// Registers an Interface and Implementation Type at Compile Time
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <typeparam name="TImplementationType">Implementation of the Interface</typeparam>
        public static void Register<TInterfaceType, TImplementationType>() where TImplementationType : TInterfaceType
        {
            Injector.Register<TInterfaceType>(typeof(TImplementationType));
        }

        /// <summary>
        /// Registers an Interface and Implementation Type at Run Time
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <param name="implementationType">Implementation of the Interface</param>
        public static void Register<TInterfaceType>(Type implementationType)
        {
            if (implementationType == null)
            {
                throw new ArgumentNullException("implementationType", "Implementation Type cannot be null.");
            }

            InjectionType t = Injector.GetImplementationsFor<TInterfaceType>().Where(
                    x => x.ImplementationType == implementationType
            ).SingleOrDefault();

            if (t == null)
            {
                Injector.Register<TInterfaceType>(implementationType, implementationType.Name);
            }

            Injector.SetDefaultImplementation(
                typeof(TInterfaceType),
                t
            );
        }

        /// <summary>
        /// Registers an Interface and Implementation Type at Run Time
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <param name="implementationType">Implementation of the Interface</param>
        /// <param name="friendlyName">Friendly Name</param>
        public static void Register<TInterfaceType>(Type implementationType, string friendlyName)
        {
            Injector.Register<TInterfaceType>(implementationType, friendlyName, false);
        }

        /// <summary>
        /// Registers an Interface and Implementation Type at Run Time
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <param name="implementationType">Implementation of the Interface</param>
        /// <param name="friendlyName">Friendly Name</param>
        /// <param name="setDefaultImplementation">Set this to Default Implementation</param>
        public static void Register<TInterfaceType>(Type implementationType, string friendlyName, bool setDefaultImplementation)
        {
            if (implementationType == null)
            {
                throw new ArgumentNullException("implementationType", "ImplementationType cannot be null.");
            }

            Type interfaceType = typeof(TInterfaceType);

            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException("InterfaceType must be an Interface");
            }

            if (!interfaceType.IsAssignableFrom(implementationType))
            {
                throw new ArgumentException("ImplementationType must be an implementation of the InterfaceType");
            }

            InjectionType t = Injector.GetImplementationsFor<TInterfaceType>().Where(
                    x => x.ImplementationType == implementationType
            ).FirstOrDefault();

            if (t == null)
            {
                t = new InjectionType<TInterfaceType>(implementationType, friendlyName);
                Injector.allImplementations[interfaceType] = Injector.allImplementations[interfaceType].Union(new[] { t }).ToArray();
            }
            else
            {
                t.FriendlyName = friendlyName;
            }

            if (!Injector.defaultImplementations.ContainsKey(interfaceType) || setDefaultImplementation)
            {
                Injector.SetDefaultImplementation(interfaceType, t);
            }
        }

        /// <summary>
        /// Creates an object for a particular given interface type.
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <returns>Constructed Implementation</returns>
        public static TInterfaceType Create<TInterfaceType>()
        {
            if (!defaultImplementations.ContainsKey(typeof(TInterfaceType)))
            {
                Type t = Injector.FindImplementation<TInterfaceType>();
                if (t == null)
                {
                    return default(TInterfaceType);
                }

                Injector.Register<TInterfaceType>(Injector.FindImplementation<TInterfaceType>());
            }

            return (TInterfaceType)Injector.defaultImplementations[typeof(TInterfaceType)].Construct();
        }

        /// <summary>
        /// Creates an object for a particular given interface type and friendly name.
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <param name="friendlyName">Friendly Name</param>
        /// <returns>Constructed Implementation</returns>
        public static TInterfaceType Create<TInterfaceType>(string friendlyName)
        {
            return (TInterfaceType)Injector.GetImplementationsFor<TInterfaceType>().Where(x => x.FriendlyName == friendlyName).Single().Construct();
        }

        /// <summary>
        /// Creates an object for a particular given interface type and type name.
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <param name="typeName">Type Name</param>
        /// <returns>Constructed Implementation</returns>
        public static TInterfaceType CreateFromTypeName<TInterfaceType>(string typeName)
        {
            Type t = Type.GetType(typeName);

            if (t == null)
            {
                return Create<TInterfaceType>();
            }

            return (TInterfaceType)Activator.CreateInstance(t);
        }

        /// <summary>
        /// Finds an Implementation of Type InterfaceType from the Assembly the Interface is defined in, or, failing that, one from all currently loaded assemblies.
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <returns>Implementation Type</returns>
        public static Type FindImplementation<TInterfaceType>()
        {
            InjectionType<TInterfaceType>[] implementations = Injector.GetImplementationsFor<TInterfaceType>();
            if (implementations == null || implementations.Length == 0)
            {
                return null;
            }

            return implementations[0].ImplementationType;
        }

        /// <summary>
        /// Returns all the objects you could ever want registered for a given interface.
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <returns>Implemented Type</returns>
        public static InjectionType<TInterfaceType>[] GetImplementationsFor<TInterfaceType>()
        {
            Type interfaceType = typeof(TInterfaceType);

            if (!Injector.allImplementations.ContainsKey(interfaceType))
            {
                Injector.FillAllImplementationsDictionary<TInterfaceType>();
            }

            return Injector.allImplementations[interfaceType].Cast<InjectionType<TInterfaceType>>().ToArray();
        }

        public static void SetDefaultImplementation(string interfaceName, string implementationName)
        {
            if (string.IsNullOrWhiteSpace(interfaceName))
            {
                throw new ArgumentNullException("interfaceName");
            }

            if (string.IsNullOrWhiteSpace(implementationName))
            {
                throw new ArgumentNullException("implementationName");
            }

            Type tInterface = Type.GetType(interfaceName);
            if (tInterface == null)
            {
                foreach (Type tKey in Injector.allImplementations.Keys)
                {
                    if (tKey.Name == interfaceName || tKey.FullName == interfaceName || tKey.AssemblyQualifiedName == interfaceName)
                    {
                        tInterface = tKey;
                        break;
                    }
                }

                if (tInterface == null)
                {
                    throw new ArgumentException("Type does not exist.", "interfaceName");
                }
            }

            InjectionType iType = null;

            Type tImplementation = Type.GetType(implementationName);
            if (tImplementation == null)
            {
                iType = Injector.allImplementations[tInterface].Where(x => x.ImplementationType.Name == implementationName || x.ImplementationType.FullName == implementationName || x.ImplementationType.AssemblyQualifiedName == implementationName).FirstOrDefault();
            }
            else
            {
                iType = Injector.allImplementations[tInterface].Where(x => x.ImplementationType == tImplementation).FirstOrDefault();
            }

            if (iType == null)
            {
                throw new ArgumentException("Type does not exist.", "implementationName");
            }

            Injector.SetDefaultImplementation(tInterface, iType);
        }

        /// <summary>
        /// Fills a dictionary with all implementations of a particular interface.
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        private static void FillAllImplementationsDictionary<TInterfaceType>()
        {
            Type interfaceType = typeof(TInterfaceType);

            if (Injector.allImplementations.ContainsKey(interfaceType))
            {
                Injector.allImplementations.Remove(interfaceType);
            }

            Injector.allImplementations.Add(interfaceType,
                AppDomain.CurrentDomain.GetAssemblies().SelectMany(
                    x => x.GetTypes()
                ).Where(
                    y => interfaceType.IsAssignableFrom(y) && interfaceType != y && !y.IsInterface
                ).ToArray().Select(
                    x => new InjectionType<TInterfaceType>(x)
                ).ToArray()
            );
        }

        /// <summary>
        /// Adds an implementation to the mapping, removing the old one if necessary.
        /// </summary>
        /// <param name="t">Type to Add</param>
        /// <param name="x">InjectionType to Map</param>
        private static void SetDefaultImplementation(Type t, InjectionType x)
        {
            if (Injector.defaultImplementations.ContainsKey(t))
            {
                Injector.defaultImplementations.Remove(t);
            }

            Injector.defaultImplementations.Add(t, x);
        }
    }
}
