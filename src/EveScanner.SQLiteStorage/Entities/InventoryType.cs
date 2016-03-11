//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="InventoryType.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.SQLiteStorage.Entities
{
    using EveScanner.Interfaces.SDE;
    using EveScanner.IoC.Attributes;

    /// <summary>
    /// SQLite representation of the SDE Inventory Type
    /// </summary>
    public class InventoryType : IInventoryType
    {
        /// <summary>
        /// Type Id
        /// </summary>
        private long typeId;

        /// <summary>
        /// Group Id
        /// </summary>
        private long? groupId;

        /// <summary>
        /// Type Name
        /// </summary>
        private string typeName;

        /// <summary>
        /// Type Description
        /// </summary>
        private string description;

        /// <summary>
        /// Item Mass
        /// </summary>
        private double? mass;

        /// <summary>
        /// Item Volume
        /// </summary>
        private double? volume;

        /// <summary>
        /// Item Capacity
        /// </summary>
        private double? capacity;

        /// <summary>
        /// Portion Size
        /// </summary>
        private long? portionSize;

        /// <summary>
        /// Racial Id
        /// </summary>
        private long? raceId;

        /// <summary>
        /// Base Price
        /// </summary>
        private decimal? basePrice;

        /// <summary>
        /// Publishing Indicator
        /// </summary>
        private long? published;

        /// <summary>
        /// Market Group Id
        /// </summary>
        private long? marketGroupId;

        /// <summary>
        /// Icon Id
        /// </summary>
        private long? iconId;

        /// <summary>
        /// Sound Id
        /// </summary>
        private long? soundId;

        /// <summary>
        /// Graphic Id
        /// </summary>
        private long? graphicId;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryType"/> class.
        /// </summary>
        public InventoryType()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryType"/> class.
        /// </summary>
        /// <param name="typeId">Item Type Id</param>
        /// <param name="groupId">Item Group Id</param>
        /// <param name="typeName">Item Type Name</param>
        /// <param name="description">Item Description</param>
        /// <param name="mass">Item Mass</param>
        /// <param name="volume">Item Volume</param>
        /// <param name="capacity">Item Capacity</param>
        /// <param name="portionSize">Portion Size</param>
        /// <param name="raceId">Racial Id</param>
        /// <param name="basePrice">Base Price</param>
        /// <param name="published">Publishing Indicator</param>
        /// <param name="marketGroupId">Market Group Id</param>
        /// <param name="iconId">Icon ID</param>
        /// <param name="soundId">Sound Id</param>
        /// <param name="graphicId">Graphic Id</param>
        public InventoryType(int typeId, int? groupId, string typeName, string description, double? mass, double? volume, double? capacity, int? portionSize, int? raceId, decimal? basePrice, bool? published, int? marketGroupId, int? iconId, int? soundId, int? graphicId)
        {
            this.typeId = typeId;
            this.groupId = groupId;
            this.typeName = typeName;
            this.description = description;
            this.mass = mass;
            this.volume = volume;
            this.capacity = capacity;
            this.portionSize = portionSize;
            this.raceId = raceId;
            this.basePrice = basePrice;

            if (published.HasValue)
            {
                this.published = published.Value ? 1 : 0;
            }
            else
            {
                this.published = null;
            }

            this.marketGroupId = marketGroupId;
            this.iconId = iconId;
            this.soundId = soundId;
            this.graphicId = graphicId;
        }

        /// <summary>
        /// Gets the Base Price of the Item
        /// </summary>
        [IgnoreMember]
        public decimal? BasePrice
        {
            get
            {
                return this.basePrice;
            }
        }

        /// <summary>
        /// Gets the Cargo Capacity of the Item
        /// </summary>
        [IgnoreMember]
        public double? Capacity
        {
            get
            {
                return this.capacity;
            }
        }

        /// <summary>
        /// Gets the Item Description
        /// </summary>
        [IgnoreMember]
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Gets the Graphic Id
        /// </summary>
        [IgnoreMember]
        public int? GraphicId
        {
            get
            {
                return (int?)this.graphicId;
            }
        }

        /// <summary>
        /// Gets the Item Group Id
        /// </summary>
        [IgnoreMember]
        public int? GroupId
        {
            get
            {
                return (int?)this.groupId;
            }
        }

        /// <summary>
        /// Gets the Icon Id
        /// </summary>
        [IgnoreMember]
        public int? IconId
        {
            get
            {
                return (int?)this.iconId;
            }
        }

        /// <summary>
        /// Gets the Market Group Id
        /// </summary>
        [IgnoreMember]
        public int? MarketGroupId
        {
            get
            {
                return (int?)this.marketGroupId;
            }
        }

        /// <summary>
        /// Gets the Item Mass
        /// </summary>
        [IgnoreMember]
        public double? Mass
        {
            get
            {
                return this.mass;
            }
        }

        /// <summary>
        /// Gets the Portion Size
        /// </summary>
        [IgnoreMember]
        public int? PortionSize
        {
            get
            {
                return (int?)this.portionSize;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this item has been published.
        /// </summary>
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

        /// <summary>
        /// Gets the Race Id
        /// </summary>
        [IgnoreMember]
        public int? RaceId
        {
            get
            {
                return (int?)this.raceId;
            }
        }

        /// <summary>
        /// Gets the Sound Id
        /// </summary>
        [IgnoreMember]
        public int? SoundId
        {
            get
            {
                return (int?)this.soundId;
            }
        }

        /// <summary>
        /// Gets the Item Type Id
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
        /// Gets the Item Type Name
        /// </summary>
        [IgnoreMember]
        public string TypeName
        {
            get
            {
                return this.typeName;
            }
        }

        /// <summary>
        /// Gets the Item Volume
        /// </summary>
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
