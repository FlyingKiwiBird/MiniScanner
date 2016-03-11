//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IRegistrationService.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces
{
    using System;

    /// <summary>
    /// Interface for Injection Registration
    /// </summary>
    public interface IRegistrationService
    {
        /// <summary>
        /// Registers an implementation for an interface.
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <param name="implementationType">Implementation Type</param>
        void Register<TInterfaceType>(Type implementationType);

        /// <summary>
        /// Registers an implementation for an interface.
        /// </summary>
        /// <typeparam name="TInterfaceType">Interface Type</typeparam>
        /// <param name="implementationType">Implementation Type</param>
        /// <param name="friendlyName">Friendly Name</param>
        void Register<TInterfaceType>(Type implementationType, string friendlyName);
    }
}
