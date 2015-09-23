//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="MemberCorporationsRowset.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Represents a Row Set of Member Corporation Rows.
    /// </summary>
    [XmlRoot("rowset")]
    public class MemberCorporationsRowset : EveApiRowset<MemberCorporationRow>
    {
    }
}
