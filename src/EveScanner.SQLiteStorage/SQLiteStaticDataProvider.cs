namespace EveScanner.SQLiteStorage
{
    using EveScanner.Core;
    using EveScanner.Interfaces.Providers;
    using EveScanner.Interfaces.SDE;
    using System.Collections.Generic;

    public class SQLiteStaticDataProvider : SQLiteQueryable, IStaticDataExportProvider
    {
        private const string SQLITE_GetInventoryGroupById = @"SELECT * FROM invGroups WHERE groupID = @groupID;";
        private const string SQLITE_GetInventoryGroupByName = @"SELECT * FROM invGroups WHERE groupName = @groupName;";
        private const string SQLITE_GetInventoryGroupsByCategoryId = @"SELECT * FROM invGroups WHERE categoryID = @categoryID;";
        private const string SQLITE_GetInventoryTypeByTypeId = @"SELECT * FROM invTypes WHERE typeID = @typeID";
        private const string SQLITE_GetInventoryTypeByTypeName = @"SELECT * FROM invTypes WHERE typeName = @typeName";
        private const string SQLITE_GetInventoryTypesByGroupId = @"SELECT * FROM invTypes WHERE groupID = @groupID";

        private string connectionString;

        public override string ConnectionString { get { return this.connectionString; } }

        public SQLiteStaticDataProvider()
        {
            this.connectionString = ConfigHelper.GetConnectionString("SQLiteStaticDataProvider");
        }

        public SQLiteStaticDataProvider(string databaseName)
        {
            this.connectionString = string.Format("Data Source={0};Version=3;", databaseName);
        }

        public IInventoryGroup GetInventoryGroupById(int groupId)
        {
            return this.RunSingleRecordQuery<InventoryGroup>(SQLITE_GetInventoryGroupById, "@groupID", groupId);
        }

        public IInventoryGroup GetInventoryGroupByName(string groupName)
        {
            return this.RunSingleRecordQuery<InventoryGroup>(SQLITE_GetInventoryGroupByName, "@groupName", groupName);
        }

        public IEnumerable<IInventoryGroup> GetInventoryGroupsByCategoryId(int categoryId)
        {
            return this.RunMultipleRecordQuery<InventoryGroup>(SQLITE_GetInventoryGroupsByCategoryId, "@categoryID", categoryId);
        }

        public IInventoryType GetInventoryTypeByTypeId(int typeId)
        {
            return this.RunSingleRecordQuery<InventoryType>(SQLITE_GetInventoryTypeByTypeId, "@typeID", typeId);
        }

        public IInventoryType GetInventoryTypeByTypeName(string typeName)
        {
            return this.RunSingleRecordQuery<InventoryType>(SQLITE_GetInventoryTypeByTypeName, "@typeName", typeName);
        }

        public IEnumerable<IInventoryType> GetInventoryTypesByGroupId(int groupId)
        {
            return this.RunMultipleRecordQuery<InventoryType>(SQLITE_GetInventoryTypesByGroupId, "@groupID", groupId);
        }
    }
}
