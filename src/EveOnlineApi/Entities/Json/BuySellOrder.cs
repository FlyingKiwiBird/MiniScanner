//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="BuySellOrder.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Json
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents either a buy or sell order from CREST.
    /// </summary>
    [DataContract]
    public class BuySellOrder
    {
        /// <summary>
        /// Gets or sets a value indicating whether this is a buy order. If false, this is a sell order.
        /// </summary>
        [DataMember(Name = "buy")]
        public bool BuyOrder { get; set; }

        /// <summary>
        /// Gets or sets a string containing a Date/Time value when the Order was issued.
        /// </summary>
        [DataMember(Name = "issued")]
        public string Issued { get; set; }

        /// <summary>
        /// Gets or sets the price of the order.
        /// </summary>
        [DataMember(Name = "price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the total volume entered for the order at time of creation.
        /// </summary>
        [DataMember(Name = "volumeEntered")]
        public int VolumeEntered { get; set; }

        /// <summary>
        /// Gets or sets the minimum volume required to fulfill this order.
        /// </summary>
        [DataMember(Name = "minVolume")]
        public int MinVolume { get; set; }

        /// <summary>
        /// Gets or sets the remaining volume available on this order.
        /// </summary>
        [DataMember(Name = "volume")]
        public int Volume { get; set; }

        /// <summary>
        /// Gets or sets the range at which this order can be fulfilled. This could be "Station", "System", or a number of jumps.
        /// </summary>
        [DataMember(Name = "range")]
        public string Range { get; set; }

        /// <summary>
        /// Gets or sets the number of days this order is valid.
        /// </summary>
        [DataMember(Name = "duration")]
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets information about the item type.
        /// </summary>
        [DataMember(Name = "type")]
        public EveOnlineApi.Entities.Json.Type Type { get; set; }

        /// <summary>
        /// Gets or sets the Order Id.
        /// </summary>
        [DataMember(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the Location where this order is based.
        /// </summary>
        [DataMember(Name = "location")]
        public Location Location { get; set; }
    }
}
