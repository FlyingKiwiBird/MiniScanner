//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IContactListXmlDataProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces.Xml
{
    using EveOnlineApi.Entities.Xml;

    /// <summary>
    /// Defines the interface for retrieving Eve XML API data for Corporations.
    /// </summary>
    public interface IContactListXmlDataProvider
    {
        /// <summary>
        /// Gets the Contact List. This item is not retrieved from Eve API Servers. It is static.
        /// </summary>
        /// <returns>ContactList XML Object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "May take long to execute.")]
        ContactListApi GetContactList();
    }
}
