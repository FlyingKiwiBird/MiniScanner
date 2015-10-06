//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CharacterIdApi.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Base for the EVE Character Id API
    /// </summary>
    [XmlRoot("eveapi")]
    public class CharacterIdApi : EveApi<CharacterIdResult>
    {
    }
}
