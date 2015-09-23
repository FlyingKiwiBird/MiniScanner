//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="AllianceListResult.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Result tag for the EVE Alliance List API
    /// </summary>
    [XmlRoot("result")]
    public class AllianceListResult : EveApiResult<AlliancesRowset, AllianceRow>
    {
    }
}
