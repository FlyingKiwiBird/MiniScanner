using EveScanner.IoC.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveScanner.SQLiteStorage
{
    public class InventoryPricing
    {
        private long typeId;
        private decimal? buyPrice;
        private decimal? sellPrice;
        private string dateChanged;

        public InventoryPricing()
        {
        }

        public InventoryPricing(long typeId, decimal? buyPrice, decimal? sellPrice)
        {
            this.typeId = typeId;
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;
        }

        [IgnoreMember]
        public int TypeId
        {
            get
            {
                return (int)this.typeId;
            }
        }

        [IgnoreMember]
        public double BuyPrice
        {
            get
            {
                return (double)this.buyPrice;
            }
        }

        [IgnoreMember]
        public double SellPrice
        {
            get
            {
                return (double)this.sellPrice;
            }
        }

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
