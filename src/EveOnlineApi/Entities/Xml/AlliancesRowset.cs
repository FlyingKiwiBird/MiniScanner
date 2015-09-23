//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="AlliancesRowset.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Row set for the Alliances part of the API
    /// </summary>
    [XmlRoot("rowset")]
    public class AlliancesRowset : EveApiRowset<AllianceRow>
    {
    }
}
