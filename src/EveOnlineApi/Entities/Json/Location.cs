//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Location.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Json
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a market location from CREST.
    /// </summary>
    [DataContract]
    public class Location
    {
        /// <summary>
        /// Gets or sets the Location Id.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the Location.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
