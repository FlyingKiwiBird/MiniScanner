//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EvepraisalPrices.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Evepraisal
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Holds the Buy, Sell, and Overall Price from Evepraisal
    /// </summary>
    [DataContract]
    public class EvepraisalPrices
    {
        /// <summary>
        /// Gets or sets the Overall Pricing of the Item.
        /// </summary>
        [DataMember(Name = "all")]
        public EvepraisalPrice All { get; set; }

        /// <summary>
        /// Gets or sets Buy Pricing of the Item.
        /// </summary>
        [DataMember(Name = "buy")]
        public EvepraisalPrice Buy { get; set; }

        /// <summary>
        /// Gets or sets Sell Pricing of the Item.
        /// </summary>
        [DataMember(Name = "sell")]
        public EvepraisalPrice Sell { get; set; }
    }
}
