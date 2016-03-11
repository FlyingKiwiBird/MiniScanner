//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Type.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Json
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an EVE Item Type from CREST.
    /// </summary>
    [DataContract]
    public class Type
    {
        /// <summary>
        /// Gets or sets the cargo capacity of members of this type.
        /// </summary>
        [DataMember(Name = "capacity")]
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the description of the type.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the CREST URL for the Type.
        /// </summary>
        [DataMember(Name = "href")]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the Icon Id.
        /// </summary>
        [DataMember(Name = "iconID")]
        public int IconId { get; set; }

        /// <summary>
        /// Gets or sets the Portion Size.
        /// </summary>
        [DataMember(Name = "portionSize")]
        public int PortionSize { get; set; }

        /// <summary>
        /// Gets or sets the Type Id.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Mass of objects of this type.
        /// </summary>
        [DataMember(Name = "mass")]
        public int Mass { get; set; }

        /// <summary>
        /// Gets or sets the type name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CCP has made this item public.
        /// </summary>
        [DataMember(Name = "published")]
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the radius of the type.
        /// </summary>
        [DataMember(Name = "radius")]
        public int Radius { get; set; }

        /// <summary>
        /// Gets or sets the volume of the type in m3.
        /// </summary>
        [DataMember(Name = "volume")]
        public decimal Volume { get; set; }
    }
}
