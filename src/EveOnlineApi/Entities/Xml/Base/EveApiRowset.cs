//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EveApiRowset.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml.Base
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents the base of an EVE API Row set. This contains at least 3 attributes, and may contain a set of rows.
    /// </summary>
    [XmlRoot("rowset")]
    public class EveApiRowset
    {
        /// <summary>
        /// Gets or sets the name of the Row Set
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Key Column of the subsequent rows.
        /// </summary>
        [XmlAttribute("key")]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the list of Columns in subsequent rows.
        /// </summary>
        [XmlAttribute("columns")]
        public string Columns { get; set; }
    }

    /// <summary>
    /// Represents an EVE API row set complete with a set of subsequent rows.
    /// </summary>
    /// <typeparam name="V">API Row Type</typeparam>
    [XmlRoot("rowset")]
    public abstract class EveApiRowset<V> : EveApiRowset
        where V : EveRow
    {
        /// <summary>
        /// Gets or sets a list of rows which are subsequent to the Row Set.
        /// </summary>
        [XmlElement("row")]
        public List<V> Rows { get; set; }
    }
}
