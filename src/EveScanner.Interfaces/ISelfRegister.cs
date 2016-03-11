//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ISelfRegister.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces
{
    /// <summary>
    /// Interface for Library Injection Self Registration
    /// </summary>
    public interface ISelfRegister
    {
        /// <summary>
        /// Sets up registrations using a provided service to register.
        /// </summary>
        /// <param name="service">Registration Service</param>
        void SetMeUp(IRegistrationService service);
    }
}
