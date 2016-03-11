//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EveApiResult.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml.Base
{
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents an EVI API Result Set. Usually extended with a row set.
    /// </summary>
    [XmlRoot("result")]
    public class EveApiResult
    {
    }

    /// <summary>
    /// Represents an EVI API Result Set. Usually just contains a "Row set" element, but, can contain other items.
    /// </summary>
    /// <typeparam name="TEveApiRowset">API Row Set Type</typeparam>
    [XmlRoot("result")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Small generic class in same file")]
    public abstract class EveApiSingleResult<TEveApiRowset> : EveApiResult
        where TEveApiRowset : EveApiRowset
    {
        /// <summary>
        /// Gets or sets the Row Set
        /// </summary>
        [XmlElement("rowset")]
        public TEveApiRowset RowSet { get; set; }
    }
}
