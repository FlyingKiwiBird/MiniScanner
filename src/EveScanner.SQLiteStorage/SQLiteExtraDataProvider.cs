using System;
using EveScanner.Core;
using EveScanner.Interfaces.EDE;
using EveScanner.Interfaces.Providers;

namespace EveScanner.SQLiteStorage
{
    public class SQLiteExtraDataProvider : SQLiteQueryable, IInventoryRepackagedProvider
    {
        private const string SQLITE_GetRepackagedVolumeByGroupId = @"SELECT * FROM invRepackaged WHERE groupID = @groupID;";

        private string connectionString;

        public override string ConnectionString { get { return this.connectionString; } }

        public SQLiteExtraDataProvider()
        {
            this.connectionString = ConfigHelper.GetConnectionString("EDEProvider");
        }

        public SQLiteExtraDataProvider(string databaseName)
        {
            this.connectionString = string.Format("Data Source={0};Version=3;", databaseName);
        }

        public IInventoryRepackaged GetRepackagedVolumesForGroup(int groupId)
        {
            return this.RunSingleRecordQuery<InventoryRepackaged>(SQLITE_GetRepackagedVolumeByGroupId, "@groupID", groupId);
        }
    }
}
