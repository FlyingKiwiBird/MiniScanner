//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EveApiResult.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml.Base
{
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
    /// <typeparam name="U">API Row Set Type</typeparam>
    [XmlRoot("result")]
    public abstract class EveApiSingleResult<U> : EveApiResult
        where U : EveApiRowset
    {
        /// <summary>
        /// Gets or sets the Row Set
        /// </summary>
        [XmlElement("rowset")]
        public U RowSet { get; set; }
    }
}
