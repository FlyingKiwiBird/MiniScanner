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
    public class EveApiResult
    {
    }

    /// <summary>
    /// Represents an EVI API Result Set. Usually just contains a "Row set" element, but, can contain other items.
    /// </summary>
    /// <typeparam name="U">API Row Set Type</typeparam>
    /// <typeparam name="V">API Row Type</typeparam>
    [XmlRoot("result")]
    public abstract class EveApiResult<U, V> : EveApiResult
        where U : EveApiRowset<V>
        where V : EveRow
    {
        /// <summary>
        /// Gets or sets the Row Set
        /// </summary>
        [XmlElement("rowset")]
        public U RowSet { get; set; }
    }
}
