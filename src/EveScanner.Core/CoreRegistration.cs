//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CoreRegistration.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Core
{
    using EveScanner.Interfaces;

    /// <summary>
    /// Registers classes for the Core library.
    /// </summary>
    public class CoreRegistration : ISelfRegister
    {
        /// <summary>
        /// Sets up registrations using a provided service to register.
        /// </summary>
        /// <param name="service">Registration Service</param>
        public void SetMeUp(IRegistrationService service)
        {
            if (service == null)
            {
                return;
            }

            service.Register<IScanHistory>(typeof(ListScanHistory));
            service.Register<ILineAppraisal>(typeof(ScanLine));
            service.Register<IScanResult>(typeof(ScanResult));
            service.Register<IWebClient>(typeof(WebClient));
        }
    }
}
