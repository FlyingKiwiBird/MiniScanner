//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CoreRegistration.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi
{
    using EveOnlineApi.Interfaces;
    using EveOnlineApi.Interfaces.Xml;
    using EveScanner.Interfaces;
    using EveScanner.Interfaces.Providers;

    /// <summary>
    /// Registers classes for the Api library.
    /// </summary>
    public class ApiRegistration : ISelfRegister
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

            // Configure Item Prices from CREST
            service.Register<IItemPriceProvider>(typeof(EveOnlineCrestApi));

            // Configure XML API Injections
            service.Register<IAllianceXmlDataProvider>(typeof(FileBackedEveOnlineXmlApi));
            service.Register<ICharacterXmlDataProvider>(typeof(FileBackedEveOnlineXmlApi));
            service.Register<ICorporationXmlDataProvider>(typeof(FileBackedEveOnlineXmlApi));
            service.Register<IContactListXmlDataProvider>(typeof(FileBackedEveOnlineXmlApi));

            // Configure other API Entity Injections
            service.Register<IAllianceDataProvider>(typeof(XmlBackedEveOnlineApi));
            service.Register<ICharacterDataProvider>(typeof(XmlBackedEveOnlineApi));
            service.Register<ICorporationDataProvider>(typeof(XmlBackedEveOnlineApi));
            service.Register<IStandingsDataProvider>(typeof(XmlBackedEveOnlineApi));

            service.Register<IImageDataProvider>(typeof(FileBackedImageDataProvider));
        }
    }
}
