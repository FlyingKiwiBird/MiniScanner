//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CallGroupRowset.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;
    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Represents the Row Set which contains Call Groups from
    /// the EVE Call List API XML call.
    /// </summary>
    [XmlRoot("rowset")]
    public class CallGroupRowset : EveApiRowset<CallGroupRow>
    {
    }
}
