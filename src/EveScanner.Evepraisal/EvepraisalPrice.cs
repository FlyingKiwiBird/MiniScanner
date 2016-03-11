//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EvepraisalPrice.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Evepraisal
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Pricing Statistics from Evepraisal
    /// </summary>
    [DataContract]
    public class EvepraisalPrice
    {
        /// <summary>
        /// Gets or sets the Average price of the Item
        /// </summary>
        [DataMember(Name = "avg")]
        public double Average { get; set; }

        /// <summary>
        /// Gets or sets the Maximum price of the Item
        /// </summary>
        [DataMember(Name = "max")]
        public double Maximum { get; set; }

        /// <summary>
        /// Gets or sets the Median price of the Item.
        /// </summary>
        [DataMember(Name = "median")]
        public double Median { get; set; }

        /// <summary>
        /// Gets or sets the Minimum price of the Item.
        /// </summary>
        [DataMember(Name = "min")]
        public double Minimum { get; set; }

        /// <summary>
        /// Gets or sets the Percentile of the Item.
        /// </summary>
        [DataMember(Name = "percentile")]
        public double Percentile { get; set; }

        /// <summary>
        /// Gets or sets the Price of the Item.
        /// </summary>
        [DataMember(Name = "price")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the Standard Deviation of Pricing of the Item.
        /// </summary>
        [DataMember(Name = "stddev")]
        public double StdDev { get; set; }

        /// <summary>
        /// Gets or sets the Volume the Item occupies.
        /// </summary>
        [DataMember(Name = "volume")]
        public long Volume { get; set; }
    }
}
