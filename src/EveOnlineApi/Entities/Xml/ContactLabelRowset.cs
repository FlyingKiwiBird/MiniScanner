//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ContactLabelRowset.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Row set for the Contact List part of the API
    /// </summary>
    [XmlRoot("rowset")]
    public class ContactLabelRowset : EveApiRowset<ContactLabelRow>
    {
    }
}
