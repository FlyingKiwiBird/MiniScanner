//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EvepraisalTotals.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Evepraisal
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Holds overall pricing for a scan from Evepraisal (and for some reason volume)
    /// </summary>
    [DataContract]
    public class EvepraisalTotals
    {
        /// <summary>
        /// Gets or sets the overall Buy price.
        /// </summary>
        [DataMember(Name = "buy")]
        public double Buy { get; set; }

        /// <summary>
        /// Gets or sets the overall Sell price.
        /// </summary>
        [DataMember(Name = "sell")]
        public double Sell { get; set; }

        /// <summary>
        /// Gets or sets the total Volume for a scan.
        /// </summary>
        [DataMember(Name = "volume")]
        public double Volume { get; set; }
    }
}
