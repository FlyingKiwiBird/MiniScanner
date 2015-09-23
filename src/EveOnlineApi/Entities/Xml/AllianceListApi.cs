//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="AllianceListApi.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Base for the EVE Alliance List API
    /// </summary>
    [XmlRoot("eveapi")]
    public class AllianceListApi : EveApi<AllianceListResult, AlliancesRowset, AllianceRow>
    {
    }
}
