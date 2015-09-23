//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EveOnlineCacheable.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities
{
    using System;
    
    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Represents the base 3 fields in every EVE Online Data response.
    /// </summary>
    public abstract class EveOnlineCacheable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EveOnlineCacheable"/> class.
        /// </summary>
        /// <param name="apiResult">XML API Result</param>
        internal EveOnlineCacheable(EveApi apiResult) 
            : this(apiResult.Version, apiResult.CurrentTime, apiResult.CachedUntil)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EveOnlineCacheable"/> class.
        /// </summary>
        /// <param name="version">EVE API Version</param>
        /// <param name="currentTime">Current Time UTC</param>
        /// <param name="cachedUntil">Cached Until UTC</param>
        internal EveOnlineCacheable(int version, string currentTime, string cachedUntil)
        {
            this.Version = version;
            this.CurrentTime = DateTime.Parse(currentTime + "Z").ToUniversalTime();
            this.CachedUntil = DateTime.Parse(cachedUntil + "Z").ToUniversalTime();
        }

        /// <summary>
        /// Gets or sets the version of the EVE API this data was retrieved from.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the Current Time in UTC at the time the data was retrieved.
        /// </summary>
        public DateTime CurrentTime { get; set; }

        /// <summary>
        /// Gets or sets the Time in UTC where this data is no longer valid.
        /// </summary>
        public DateTime CachedUntil { get; set; }
    }
}
