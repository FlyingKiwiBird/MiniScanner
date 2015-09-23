//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CharacterIdRowset.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Row Set for the Character Id Lookup
    /// </summary>
    [XmlRoot("rowset")]
    public class CharacterIdRowset : EveApiRowset<CharacterIdRow>
    {
    }
}
