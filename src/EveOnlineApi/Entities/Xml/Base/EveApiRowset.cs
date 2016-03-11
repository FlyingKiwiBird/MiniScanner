//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EveApiRowset.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml.Base
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
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
    /// <typeparam name="TEveRow">API Row Type</typeparam>
    [XmlRoot("rowset")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Small generic class in same file")]
    public abstract class EveApiRowset<TEveRow> : EveApiRowset
        where TEveRow : EveRow
    {
        /// <summary>
        /// This holds our List so we get around CA2227. Yep.
        /// </summary>
        private readonly Collection<TEveRow> rows = new Collection<TEveRow>();

        /// <summary>
        /// Gets or sets a list of rows which are subsequent to the Row Set.
        /// </summary>
        [XmlElement("row")]
        public Collection<TEveRow> Rows
        {
            get
            {
                return this.rows;
            }
        }
    }
}
