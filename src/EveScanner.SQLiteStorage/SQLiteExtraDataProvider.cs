using System;
using EveScanner.Core;
using EveScanner.Interfaces.EDE;
using EveScanner.Interfaces.Providers;
using EveScanner.IoC;

namespace EveScanner.SQLiteStorage
{
    public class SQLiteExtraDataProvider : SQLiteQueryable, IInventoryRepackagedProvider, IItemPriceProvider
    {
        private const string SQLITE_GetRepackagedVolumeByGroupId = @"SELECT * FROM invRepackaged WHERE groupID = @groupID;";
        private const string SQLITE_GetPricingByTypeId = @"SELECT * FROM invPricing WHERE typeID = @typeID;";

        private string connectionString;

        public override string ConnectionString { get { return this.connectionString; } }

        public SQLiteExtraDataProvider()
        {
            this.connectionString = ConfigHelper.GetConnectionString("SQLiteExtraDataProvider");
        }

        public SQLiteExtraDataProvider(string databaseName)
        {
            this.connectionString = string.Format("Data Source={0};Version=3;", databaseName);
        }

        public IInventoryRepackaged GetRepackagedVolumesForGroup(int groupId)
        {
            return this.RunSingleRecordQuery<InventoryRepackaged>(SQLITE_GetRepackagedVolumeByGroupId, "@groupID", groupId);
        }

        public void GetItemPricing(int typeId, out decimal buyPrice, out decimal sellPrice)
        {
            InventoryPricing price = this.RunSingleRecordQuery<InventoryPricing>(SQLITE_GetPricingByTypeId, "@typeID", typeId);

            if (price == null || price.DateChanged < DateTime.Now.AddHours(-1))
            {
                var crestPricingProvider = Injector.Create<IItemPriceProvider>("EveOnlineCrestApi");

                if (crestPricingProvider != null)
                {
                    decimal outbuy = 0, outsell = 0;

                    crestPricingProvider.GetItemPricing(typeId, out outbuy, out outsell);
                    if (outbuy > 0 || outsell > 0)
                    {
                        this.ExecuteNonQuery("DELETE FROM invPricing WHERE typeID = @typeID;", "@typeID", typeId);
                        this.ExecuteNonQuery("INSERT invPricing (TypeId, BuyPrice, SellPrice) VALUES (@typeID, @buyPrice, @sellPrice);", "@typeID", typeId, "@buyPrice", outbuy, "@sellPrice", outsell);
                    }
                }
            }

            price = this.RunSingleRecordQuery<InventoryPricing>(SQLITE_GetPricingByTypeId, "@typeID", typeId);

            if (price != null)
            {
                buyPrice = (decimal)price.BuyPrice;
                sellPrice = (decimal)price.SellPrice;
            }
            else
            {
                buyPrice = 0;
                sellPrice = 0;
            }
        }
    }
}
