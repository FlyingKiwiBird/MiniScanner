//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="MarketOrders.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Json
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a list of Market Orders from CREST.
    /// </summary>
    [DataContract]
    public class MarketOrders
    {
        /// <summary>
        /// Gets or sets all the Items (actually orders)
        /// </summary>
        [DataMember(Name = "items")]
        public IEnumerable<BuySellOrder> Items { get; set; }

        /// <summary>
        /// Gets or sets the number of pages in the response. This should likely be moved into a more base class.
        /// </summary>
        [DataMember(Name = "pageCount")]
        public int PageCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of items returned.
        /// </summary>
        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }
    }
}
