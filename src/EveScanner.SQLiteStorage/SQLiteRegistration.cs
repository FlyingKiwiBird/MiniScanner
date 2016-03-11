//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="SQLiteRegistration.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.SQLiteStorage
{
    using EveScanner.Interfaces;
    using EveScanner.Interfaces.Providers;

    /// <summary>
    /// Registers classes for the SQLite library.
    /// </summary>
    public class SQLiteRegistration : ISelfRegister
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
            
            service.Register<IItemPriceProvider>(typeof(SQLiteExtraDataProvider));
            service.Register<IInventoryRepackagedProvider>(typeof(SQLiteExtraDataProvider));
            // service.Register<IScanHistory>(typeof(SQLiteScanHistory)); // Don't register this one until fixed.
            service.Register<IStaticDataExportProvider>(typeof(SQLiteStaticDataProvider));
            service.Register<IInventoryGroupProvider>(typeof(SQLiteStaticDataProvider));
            service.Register<IInventoryTypeProvider>(typeof(SQLiteStaticDataProvider));
        }
    }
}
