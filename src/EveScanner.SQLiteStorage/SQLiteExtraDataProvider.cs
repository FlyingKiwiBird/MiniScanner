//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="SQLiteExtraDataProvider.cs">
// Copyright © Viktorie Lucilla 2015-2016. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.SQLiteStorage
{
    using System;
    using System.Data.SQLite;
    using System.IO;

    using EveScanner.Core;
    using EveScanner.Interfaces.EDE;
    using EveScanner.Interfaces.Providers;
    using EveScanner.IoC;
    using EveScanner.SQLiteStorage.Entities;
    using System.Collections.Generic;
    /// <summary>
    /// Implements several providers for SQLite Data
    /// </summary>
    public class SQLiteExtraDataProvider : SQLiteQueryable, IInventoryRepackagedProvider, IItemPriceProvider
    {
        /// <summary>
        /// Repackaged Volume Query
        /// </summary>
        private const string SQLITEGetRepackagedVolumeByGroupId = @"SELECT * FROM invRepackaged WHERE groupID = @groupID;";

        /// <summary>
        /// Pricing by Type Id Query
        /// </summary>
        private const string SQLITEGetPricingByTypeId = @"SELECT * FROM invPricing WHERE typeID = @typeID;";

        /// <summary>
        /// SQLite Connection String
        /// </summary>
        private string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteExtraDataProvider"/> class.
        /// </summary>
        public SQLiteExtraDataProvider() : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteExtraDataProvider"/> class.
        /// </summary>
        /// <param name="databasePath">Path to Database. If not provided, will find connection string from config.</param>
        public SQLiteExtraDataProvider(string databasePath)
        {
            string cs;

            if (!string.IsNullOrWhiteSpace(databasePath))
            {
                cs = string.Format("Data Source={0};Version=3;", databasePath);
            }
            else
            {
                cs = ConfigHelper.GetConnectionString("SQLiteExtraDataProvider");
                SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder(cs);
                databasePath = sb.DataSource;
            }

            this.connectionString = cs;

            if (!File.Exists(databasePath))
            {
                this.CreateDatabase();
            }
        }

        /// <summary>
        /// Gets the Connection String
        /// </summary>
        public override string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        /// <summary>
        /// Returns the Repackaged Volume for items in a specified Group
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <returns>Repackaged Volume</returns>
        public IInventoryRepackaged GetRepackagedVolumesForGroup(int groupId)
        {
            return this.RunSingleRecordQuery<InventoryRepackaged>(SQLITEGetRepackagedVolumeByGroupId, "@groupID", groupId);
        }

        /// <summary>
        /// Gets secondary sourced pricing data from another provider, cache it in the database, and return data.
        /// </summary>
        /// <param name="typeId">Type Id</param>
        /// <param name="buyPrice">Output - Buying Price</param>
        /// <param name="sellPrice">Output - Selling Price</param>
        public void GetItemPricing(int typeId, out decimal buyPrice, out decimal sellPrice)
        {
            InventoryPricing price = this.RunSingleRecordQuery<InventoryPricing>(SQLITEGetPricingByTypeId, "@typeID", typeId);

            if (price == null || price.DateChanged < DateTime.Now.AddHours(-1))
            {
                // Find a different pricing provider to cache data from.
                IItemPriceProvider pricingProvider = null;
                foreach (var v in Injector.GetImplementationsFor<IItemPriceProvider>())
                {
                    if (v.ImplementationType != this.GetType())
                    {
                        pricingProvider = v.Construct();
                        break;
                    }
                }

                if (pricingProvider != null)
                {
                    decimal outbuy = 0, outsell = 0;

                    pricingProvider.GetItemPricing(typeId, out outbuy, out outsell);
                    if (outbuy > 0 || outsell > 0)
                    {
                        this.ExecuteNonQuery("DELETE FROM invPricing WHERE typeID = @typeID;", "@typeID", typeId);
                        this.ExecuteNonQuery("INSERT INTO invPricing (TypeId, BuyPrice, SellPrice) VALUES (@typeID, @buyPrice, @sellPrice);", "@typeID", typeId, "@buyPrice", outbuy, "@sellPrice", outsell);
                    }
                }
            }

            price = this.RunSingleRecordQuery<InventoryPricing>(SQLITEGetPricingByTypeId, "@typeID", typeId);

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

        /// <summary>
        /// Creates the necessary tables in the SQLite database.
        /// </summary>
        private void CreateDatabase()
        {
            #region Create Table SQL
            string sql = @"
CREATE TABLE IF NOT EXISTS `invPricing` (
	`TypeId`	INTEGER NOT NULL,
	`BuyPrice`	NUMERIC NOT NULL,
	`SellPrice`	NUMERIC NOT NULL,
	`DateChanged`	TEXT NOT NULL DEFAULT (DATETIME('now')),
	PRIMARY KEY(TypeId)
);

CREATE TABLE IF NOT EXISTS `invRepackaged`
(
	`groupId`	INTEGER NOT NULL,
	`volume`	DOUBLE DEFAULT NULL,
    PRIMARY KEY(groupId)
);
            ";
            #endregion Create Table SQL

            this.ExecuteNonQuery(sql);
            this.PopulateInitialRepackagedTable();
        }

        /// <summary>
        /// Populates the Repackaged Volumes table with Initial Data when the DB is created.
        /// </summary>
        private void PopulateInitialRepackagedTable()
        {
            Dictionary<int, double> initialValues = new Dictionary<int, double>
            {
                { 31, 500 },
                { 25, 2500 }, { 237, 2500 }, { 324, 2500 }, { 830, 2500 }, { 831, 2500 }, { 834, 2500 }, { 893, 2500 }, { 1283, 2500 }, { 1527, 2500 },
                { 463, 3750 }, { 543, 3750 },
                { 420, 5000 }, { 541, 5000 }, { 963, 5000 }, { 1305, 5000 }, { 1534, 5000 },
                { 26, 10000 }, { 358, 10000 }, { 832, 10000 }, { 833, 10000 }, { 894, 10000 }, { 906, 10000 },
                { 419, 15000 }, { 540, 15000 }, { 1201, 15000 },
                { 28, 20000 }, { 380, 20000 }, { 1202, 20000 },
                { 27, 50000 }, { 898, 50000 }, { 900, 50000 }
            };

            foreach (int i in initialValues.Keys)
            {
                this.ExecuteNonQuery("INSERT INTO invRepackaged (groupId, volume) VALUES(@groupId, @volume);", "@groupId", i, "@volume", initialValues[i]);
            }
        }
    
    }
}
