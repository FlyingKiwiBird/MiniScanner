//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EveApi.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml.Base
{
    using System.Xml.Serialization;

    /// <summary>
    /// Base elements present in every Eve Online XML Feed
    /// </summary>
    [XmlRoot("eveapi")]
    public class EveApi
    {
        /// <summary>
        /// Gets or sets the feed version.
        /// </summary>
        [XmlAttribute("version")]
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the current server time in UTC.
        /// </summary>
        [XmlElement("currentTime")]
        public string CurrentTime { get; set; }

        /// <summary>
        /// Gets or sets the time in UTC that the data is cached until.
        /// </summary>
        [XmlElement("cachedUntil")]
        public string CachedUntil { get; set; }
    }

    /// <summary>
    /// Represents the feed data to result sets. These
    /// sets are strongly typed down to the row level.
    /// </summary>
    /// <typeparam name="T">API Result Type</typeparam>
    [XmlRoot("eveapi")]
    public abstract class EveApi<T> : EveApi
        where T : EveApiResult
    {
        /// <summary>
        /// Gets or sets the EVE API Result
        /// </summary>
        [XmlElement("result")]
        public T Result { get; set; }
    }
}
