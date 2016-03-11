//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="RegistrationService.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using EveScanner.Interfaces;
    using EveScanner.IoC;
    using Core;
    /// <summary>
    /// Provides a Registration Service allowing Libraries to self-register.
    /// </summary>
    public class RegistrationService : IRegistrationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationService"/> class.
        /// </summary>
        public RegistrationService()
        {
        }

        /// <summary>
        /// Registers an implementation for an interface.
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <param name="implementationType">Implementation Type</param>
        public void Register<TInterfaceType>(Type implementationType)
        {
            Injector.Register<TInterfaceType>(implementationType);
        }

        /// <summary>
        /// Registers an implementation for an interface.
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <param name="implementationType">Implementation Type</param>
        /// <param name="friendlyName">Friendly Name</param>
        public void Register<TInterfaceType>(Type implementationType, string friendlyName)
        {
            Injector.Register<TInterfaceType>(implementationType, friendlyName);
        }

        /// <summary>
        /// Scans the current folder for libraries that implement ISelfRegister.
        /// </summary>
        public void ScanFolder()
        {
            this.ScanFolder(Environment.CurrentDirectory);
        }

        /// <summary>
        /// Scans a provided folder for libraries that implement ISelfRegister.
        /// </summary>
        /// <param name="path">Folder to scan</param>
        public void ScanFolder(string path)
        {
            foreach (string fileName in Directory.GetFiles(path, "*.dll"))
            {
                try
                {
                    AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
                    Assembly assembly_readonly = Assembly.ReflectionOnlyLoadFrom(fileName);

                    // You can't compare types from a ReflectionOnlyAssembly to a live assembly, so, we do it by name instead.
                    foreach (Type t in assembly_readonly.GetTypes().Where(
                        x =>    !x.IsInterface && 
                                x.GetInterfaces().Count(y => y.AssemblyQualifiedName == typeof(ISelfRegister).AssemblyQualifiedName) > 0
                        )
                    )
                    {
                        ISelfRegister rx = Injector.CreateFromTypeName<ISelfRegister>(t.AssemblyQualifiedName);
                        rx.SetMeUp(this);
                    }
                }
                catch (BadImageFormatException)
                {
                    // Don't care, it's not a managed DLL.
                }
                catch (ReflectionTypeLoadException rex)
                {
                    Logger.Fatal("Reflection Load Error occurred while loading assemblies.", string.Join(Environment.NewLine, rex.LoaderExceptions.Select(x => x.ToString()).ToArray()));
                    throw;
                }
                catch (Exception ex)
                {
                    Logger.Fatal("Unknown error occurred while loading assemblies.", ex.ToString());
                    throw;
                }
            }
        }

        public void SetDefaultImplementations()
        {
            foreach (string key in ConfigHelper.Instance.Implementations.Keys)
            {
                Injector.SetDefaultImplementation(key, ConfigHelper.Instance.Implementations[key]);
            }
        }

        private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.ReflectionOnlyLoad(args.Name);
        }
    }
}
