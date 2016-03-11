//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="InventoryPricing.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.SQLiteStorage.Entities
{
    using System;
    using EveScanner.IoC.Attributes;

    /// <summary>
    /// Representation of secondary pricing records.
    /// </summary>
    public class InventoryPricing
    {
        /// <summary>
        /// Type Id
        /// </summary>
        private long typeId;

        /// <summary>
        /// Buy Price
        /// </summary>
        private decimal? buyPrice;

        /// <summary>
        /// Sell Price
        /// </summary>
        private decimal? sellPrice;

        /// <summary>
        /// Date Changed
        /// </summary>
        private string dateChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryPricing"/> class.
        /// </summary>
        public InventoryPricing()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryPricing"/> class.
        /// </summary>
        /// <param name="typeId">Type Id</param>
        /// <param name="buyPrice">Buy Price</param>
        /// <param name="sellPrice">Sell Price</param>
        public InventoryPricing(long typeId, decimal? buyPrice, decimal? sellPrice)
        {
            this.typeId = typeId;
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;
            this.dateChanged = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss");
        }

        /// <summary>
        /// Gets the Type Id
        /// </summary>
        [IgnoreMember]
        public int TypeId
        {
            get
            {
                return (int)this.typeId;
            }
        }

        /// <summary>
        /// Gets the Buy Price
        /// </summary>
        [IgnoreMember]
        public double BuyPrice
        {
            get
            {
                return (double)this.buyPrice;
            }
        }

        /// <summary>
        /// Gets the Sell Price
        /// </summary>
        [IgnoreMember]
        public double SellPrice
        {
            get
            {
                return (double)this.sellPrice;
            }
        }

        /// <summary>
        /// Gets the last time the Price was Updated
        /// </summary>
        [IgnoreMember]
        public DateTime DateChanged
        {
            get
            {
                DateTime tmp = DateTime.Parse(this.dateChanged);
                DateTime utctmp = DateTime.SpecifyKind(tmp, DateTimeKind.Utc);
                return utctmp.ToLocalTime();
            }
        }
    }
}
