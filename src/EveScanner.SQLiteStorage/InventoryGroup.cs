using EveScanner.IoC.Attributes;
using EveScanner.Interfaces.SDE;

namespace EveScanner.Core
{
    public class InventoryGroup : IInventoryGroup
    {
        private long groupId;
        private long? categoryId;
        private string groupName;
        private long? iconId;
        private long? useBasePrice;
        private long? anchored;
        private long? anchorable;
        private long? fittableNonSingleton;
        private long? published;
        
        public InventoryGroup()
        {
        }

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

        [IgnoreMember]
        public int? CategoryId
        {
            get
            {
                return (int?)this.categoryId;
            }
        }

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

        [IgnoreMember]
        public int GroupId
        {
            get
            {
                return (int)this.groupId;
            }
        }

        [IgnoreMember]
        public string GroupName
        {
            get
            {
                return this.groupName;
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
