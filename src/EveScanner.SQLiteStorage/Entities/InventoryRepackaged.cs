//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="InventoryRepackaged.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.SQLiteStorage.Entities
{
    using EveScanner.Interfaces.EDE;
    using EveScanner.IoC.Attributes;

    /// <summary>
    /// Representation of repackaged volumes.
    /// </summary>
    public class InventoryRepackaged : IInventoryRepackaged
    {
        /// <summary>
        /// Group Id
        /// </summary>
        private long groupId;

        /// <summary>
        /// Group Volume
        /// </summary>
        private double volume;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryRepackaged"/> class.
        /// </summary>
        public InventoryRepackaged()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryRepackaged"/> class.
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <param name="volume">Group Volume</param>
        public InventoryRepackaged(int groupId, double volume)
        {
            this.groupId = groupId;
            this.volume = volume;
        }

        /// <summary>
        /// Gets the Group Id
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
        /// Gets the Repackaged Volume
        /// </summary>
        [IgnoreMember]
        public double Volume
        {
            get
            {
                return this.volume;
            }
        }
    }
}
