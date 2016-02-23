using EveScanner.Interfaces.SDE;
using EveScanner.IoC.Attributes;

namespace EveScanner.Core
{
    public class InventoryType : IInventoryType
    {
        private long typeId;
        private long? groupId;
        private string typeName;
        private string description;
        private double? mass;
        private double? volume;
        private double? capacity;
        private long? portionSize;
        private long? raceId;
        private decimal? basePrice;
        private long? published;
        private long? marketGroupId;
        private long? iconId;
        private long? soundId;
        private long? graphicId;

        public InventoryType()
        {
        }

        [IgnoreMember]
        public decimal? BasePrice
        {
            get
            {
                return this.basePrice;
            }
        }

        [IgnoreMember]
        public double? Capacity
        {
            get
            {
                return this.capacity;
            }
        }

        [IgnoreMember]
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        [IgnoreMember]
        public int? GraphicId
        {
            get
            {
                return (int?)this.graphicId;
            }
        }

        [IgnoreMember]
        public int? GroupId
        {
            get
            {
                return (int?)this.groupId;
            }
        }

        [IgnoreMember]
        public int? IconId
        {
            get
            {
                return (int?)this.iconId;
            }
        }

        [IgnoreMember]
        public int? MarketGroupId
        {
            get
            {
                return (int?)this.marketGroupId;
            }
        }

        [IgnoreMember]
        public double? Mass
        {
            get
            {
                return this.mass;
            }
        }

        [IgnoreMember]
        public int? PortionSize
        {
            get
            {
                return (int?)this.portionSize;
            }
        }

        [IgnoreMember]
        public bool? Published
        {
            get
            {
                if (this.published.HasValue)
                {
                    return this.published.Value == 1;
                }

                return null;
            }
        }

        [IgnoreMember]
        public int? RaceId
        {
            get
            {
                return (int?)this.raceId;
            }
        }

        [IgnoreMember]
        public int? SoundId
        {
            get
            {
                return (int?)this.soundId;
            }
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
        public string TypeName
        {
            get
            {
                return this.typeName;
            }
        }

        [IgnoreMember]
        public double? Volume
        {
            get
            {
                return this.volume;
            }
        }
    }
}
