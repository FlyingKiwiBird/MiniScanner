//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EvepraisalRegistration.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Evepraisal
{
    using EveScanner.Interfaces;

    /// <summary>
    /// Registers classes for the Evepraisal library.
    /// </summary>
    public class EvepraisalRegistration : ISelfRegister
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

            service.Register<ILineAppraisal>(typeof(EvepraisalItem));
            service.Register<IAppraisalService>(typeof(EvepraisalSvc));
            service.Register<IAppraisalService>(typeof(GoonpraisalSvc));
        }
    }
}
