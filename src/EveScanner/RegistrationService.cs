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
                    Assembly assembly_readonly = Assembly.ReflectionOnlyLoadFrom(fileName);
                        
                    foreach (Type t in assembly_readonly.GetTypes().Where(x => !x.IsInterface && x.IsAssignableFrom(typeof(ISelfRegister))))
                    {
                        ISelfRegister rx = Injector.CreateFromTypeName<ISelfRegister>(t.AssemblyQualifiedName);
                        rx.SetMeUp(this);
                    }
                }
                catch (BadImageFormatException)
                {
                    // Don't care, it's not a managed DLL.
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
