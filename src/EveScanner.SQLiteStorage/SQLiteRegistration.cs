using EveScanner.Interfaces;
using EveScanner.Interfaces.Providers;

namespace EveScanner.SQLiteStorage
{
    
    public class SQLiteRegistration : ISelfRegister
    {
        public void SetMeUp(IRegistrationService service)
        {
            service.Register<IInventoryRepackagedProvider>(typeof(SQLiteExtraDataProvider));
            service.Register<IScanHistory>(typeof(SQLiteScanHistory));
            service.Register<IStaticDataExportProvider>(typeof(SQLiteStaticDataProvider));
            service.Register<IInventoryGroupProvider>(typeof(SQLiteStaticDataProvider));
            service.Register<IInventoryTypeProvider>(typeof(SQLiteStaticDataProvider));
        }
    }
}
