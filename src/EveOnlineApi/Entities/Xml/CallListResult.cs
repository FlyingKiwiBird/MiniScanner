//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CallListResult.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;
    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Represents the Result object which contains two
    /// row sets instead of the usual one for the Call List API
    /// call.
    /// </summary>
    [XmlRoot("result")]
    public class CallListResult : EveApiResult
    {
        /// <summary>
        /// Gets or sets the Call Groups Row Set
        /// </summary>
        [XmlElement("rowset", Order = 1)]
        public CallGroupRowset CallGroups { get; set; }

        /// <summary>
        /// Gets or sets the Call Lists Row Set
        /// </summary>
        [XmlElement("rowset", Order = 2)]
        public CallsRowset Calls { get; set; }
    }
}
