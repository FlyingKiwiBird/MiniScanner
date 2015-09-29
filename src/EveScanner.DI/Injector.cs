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
    using System.Linq.Expressions;

    /// <summary>
    /// Basic IoC injector
    /// </summary>
    public static class Injector
    {
        /// <summary>
        /// Holds the implementation cache.
        /// </summary>
        private static Dictionary<Type, Delegate> implementations = new Dictionary<Type, Delegate>();

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
            if (!typeof(TInterfaceType).IsInterface)
            {
                throw new ArgumentException("InterfaceType must be an Interface");
            }

            if (!typeof(TInterfaceType).IsAssignableFrom(implementationType))
            {
                throw new ArgumentException("ImplementationType must be an implementation of the InterfaceType");
            }

            Delegate d = Expression.Lambda(Expression.New(implementationType)).Compile();
            implementations.Add(typeof(TInterfaceType), d);
        }

        /// <summary>
        /// Registers an Interface with a Constructor at Compile Time
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <param name="constructor">Constructor Lambda</param>
        public static void Register<TInterfaceType>(Func<TInterfaceType> constructor)
        {
            implementations.Add(typeof(TInterfaceType), constructor);
        }

        /// <summary>
        /// Creates an object for a particular given interface type.
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <returns>Constructed Implementation</returns>
        public static TInterfaceType Create<TInterfaceType>()
        {
            if (!implementations.ContainsKey(typeof(TInterfaceType)))
            {
                Injector.Register<TInterfaceType>(Injector.FindImplementation<TInterfaceType>());
            }

            Delegate d = Injector.implementations[typeof(TInterfaceType)];
 
            return (TInterfaceType)(d as Func<object>)();
        }

        /// <summary>
        /// Finds an Implementation of Type InterfaceType from the Assembly the Interface is defined in, or, failing that, one from all currently loaded assemblies.
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <returns>Implementation Type</returns>
        public static Type FindImplementation<TInterfaceType>()
        {
            Type output = null;
            
            output = typeof(TInterfaceType).Assembly.GetTypes().Where(x => typeof(TInterfaceType).IsAssignableFrom(x) && x != typeof(TInterfaceType)).FirstOrDefault();

            if (output == null)
            {
                output = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(y => typeof(TInterfaceType).IsAssignableFrom(y) && typeof(TInterfaceType) != y).FirstOrDefault();
            }
            
            return output;
        }
    }
}
