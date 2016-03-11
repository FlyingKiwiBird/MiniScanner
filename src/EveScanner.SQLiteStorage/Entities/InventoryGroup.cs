//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="InventoryGroup.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.SQLiteStorage.Entities
{
    using EveScanner.Interfaces.SDE;
    using EveScanner.IoC.Attributes;

    /// <summary>
    /// SQLite representation of the SDE Inventory Group
    /// </summary>
    public class InventoryGroup : IInventoryGroup
    {
        /// <summary>
        /// Inventory Group Id
        /// </summary>
        private long groupId;

        /// <summary>
        /// Category Id
        /// </summary>
        private long? categoryId;

        /// <summary>
        /// Inventory Group Name
        /// </summary>
        private string groupName;

        /// <summary>
        /// Icon Id
        /// </summary>
        private long? iconId;

        /// <summary>
        /// Use Base Price
        /// </summary>
        private long? useBasePrice;
        
        /// <summary>
        /// Anchored Flag
        /// </summary>
        private long? anchored;

        /// <summary>
        /// Anchorable Flag
        /// </summary>
        private long? anchorable;

        /// <summary>
        /// Fittable Non Singleton
        /// </summary>
        private long? fittableNonSingleton;

        /// <summary>
        /// Publishing Indicator
        /// </summary>
        private long? published;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryGroup"/> class.
        /// </summary>
        public InventoryGroup()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryGroup"/> class.
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <param name="categoryId">Category Id</param>
        /// <param name="groupName">Group Name</param>
        /// <param name="iconId">Icon Id</param>
        /// <param name="useBasePrice">Do items in this group use the base price?</param>
        /// <param name="anchored">Are items in this group anchored?</param>
        /// <param name="anchorable">Are items in this group anchorable?</param>
        /// <param name="fittableNonSingleton">Are items in the group Fittable Non Singletons?</param>
        /// <param name="published">Is this group published?</param>
        public InventoryGroup(int groupId, int categoryId, string groupName, int? iconId, bool useBasePrice, bool anchored, bool anchorable, bool fittableNonSingleton, bool published)
        {
            this.groupId = groupId;
            this.categoryId = categoryId;
            this.groupName = groupName;
            this.iconId = iconId;
            this.useBasePrice = useBasePrice ? 1 : 0;
            this.anchored = anchored ? 1 : 0;
            this.anchorable = anchorable ? 1 : 0;
            this.fittableNonSingleton = fittableNonSingleton ? 1 : 0;
            this.published = published ? 1 : 0;
        }

        /// <summary>
        /// Gets a value indicating whether the group is anchorable.
        /// </summary>
        [IgnoreMember]
        public bool? Anchorable
        {
            get
            {
                if (this.anchorable.HasValue)
                {
                    return this.anchorable == 1;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the group in anchored.
        /// </summary>
        [IgnoreMember]
        public bool? Anchored
        {
            get
            {
                if (this.anchored.HasValue)
                {
                    return this.anchored == 1;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the Category Id.
        /// </summary>
        [IgnoreMember]
        public int? CategoryId
        {
            get
            {
                return (int?)this.categoryId;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the group is a Fittable Non Singleton.
        /// </summary>
        [IgnoreMember]
        public bool? FittableNonSingleton
        {
            get
            {
                if (this.fittableNonSingleton.HasValue)
                {
                    return this.fittableNonSingleton == 1;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the Group Id.
        /// </summary>
        [IgnoreMember]
        public int GroupId
        {
            get
            {
                return (int)this.groupId;
            }
        }

        /// <summary>
        /// Gets the Group Name.
        /// </summary>
        [IgnoreMember]
        public string GroupName
        {
            get
            {
                return this.groupName;
            }
        }

        /// <summary>
        /// Gets the Icon Id.
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
        /// Gets a value indicating whether the group is published.
        /// </summary>
        [IgnoreMember]
        public bool? Published
        {
            get
            {
                if (this.published.HasValue)
                {
                    return this.published == 1;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the group uses the base item price.
        /// </summary>
        [IgnoreMember]
        public bool? UseBasePrice
        {
            get
            {
                if (this.useBasePrice.HasValue)
                {
                    return this.useBasePrice == 1;
                }

                return null;
            }
        }
    }
}
