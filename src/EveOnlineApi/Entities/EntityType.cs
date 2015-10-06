//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EntityType.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an Eve Online Entity Type (which is just an integer) with some custom equality logic.
    /// </summary>
    public class EntityType
    {
        /// <summary>
        /// Represents the Alliance Type Id
        /// </summary>
        private const int AllianceType = 16159;

        /// <summary>
        /// Represents the Corporation Type Id
        /// </summary>
        private const int CorporationType = 2;

        /// <summary>
        /// Represents all the Character Type IDs
        /// </summary>
        private static readonly int[] CharacterTypes = { 1373, 1374, 1375, 1376, 1377, 1378, 1379, 1380, 1381, 1382, 1383, 1384, 1385, 1386 };

        /// <summary>
        /// Holds our initialized Entity Types
        /// </summary>
        private static Dictionary<int, EntityType> cache = new Dictionary<int, EntityType>();

        /// <summary>
        /// Holds our Type Id (the only real data in the object...)
        /// </summary>
        private int typeId;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityType"/> class.
        /// </summary>
        /// <param name="typeId">Integer Type</param>
        private EntityType(int typeId)
        {
            this.typeId = typeId;
        }

        /// <summary>
        /// Gets a Character Entity Type
        /// </summary>
        public static EntityType Character
        {
            get
            {
                return EntityType.GetEntityTypeById(EntityType.CharacterTypes[0]);
            }
        }

        /// <summary>
        /// Gets a Corporation Entity Type
        /// </summary>
        public static EntityType Corporation
        {
            get
            {
                return EntityType.GetEntityTypeById(EntityType.CorporationType);
            }
        }
        
        /// <summary>
        /// Gets an Alliance Entity Type
        /// </summary>
        public static EntityType Alliance
        {
            get
            {
                return EntityType.GetEntityTypeById(EntityType.AllianceType);
            }
        }

        /// <summary>
        /// Gets an Entity Type by Id and caches them as needed.
        /// </summary>
        /// <param name="typeId">Entity Type Integer</param>
        /// <returns>Entity Type Object</returns>
        public static EntityType GetEntityTypeById(int typeId)
        {
            if (!EntityType.cache.ContainsKey(typeId))
            {
                EntityType.cache.Add(typeId, new EntityType(typeId));
            }

            return EntityType.cache[typeId];
        }

        /// <summary>
        /// Implicitly converts an <see cref="EntityType"/> to an integer
        /// </summary>
        /// <param name="t">input <see cref="EntityType"/></param>
        /// <returns>output integer</returns>
        public static implicit operator int(EntityType t)
        {
            return t.typeId;
        }

        /// <summary>
        /// Implicitly converts an integer to an <see cref="EntityType"/>.
        /// </summary>
        /// <param name="i">input integer</param>
        /// <returns>output <see cref="EntityType"/></returns>
        public static implicit operator EntityType(int i)
        {
            return new EntityType(i);
        }

        /// <summary>
        /// Determines whether the specified System.Object is equal to the current <see cref="EntityType"/>.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current <see cref="EntityType"/>.</param>
        /// <returns>true if the specified System.Object is equal to the current <see cref="EntityType"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            EntityType cp = obj as EntityType;

            if (cp.typeId == this.typeId)
            {
                return true;
            }

            if (CharacterTypes.Contains(cp.typeId) && CharacterTypes.Contains(this.typeId))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return this.typeId;
        }
    }
}
