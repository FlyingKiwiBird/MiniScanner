//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CorporationSheetRowset.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// This class is unused.
    /// </summary>
    [XmlRoot("rowset")]
    public class CorporationSheetRowset : EveApiRowset<CorporationSheetRow>
    {
    }
}
