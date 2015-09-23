//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EveRow.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml.Base
{
    using System.Xml.Serialization;

    /// <summary>
    /// Placeholder for an EVE API Row. This may have a number of attributes or sub elements which are defined in the inheriting classes.
    /// </summary>
    [XmlRoot("row")]
    public abstract class EveRow
    {
    }
}