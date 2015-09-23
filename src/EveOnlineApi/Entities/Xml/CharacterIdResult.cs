//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CharacterIdResult.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Result tag for the EVE Character Id API
    /// </summary>
    [XmlRoot("result")]
    public class CharacterIdResult : EveApiResult<CharacterIdRowset, CharacterIdRow>
    {
    }
}
