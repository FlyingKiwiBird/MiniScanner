//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ICallListXmlDataProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces.Xml
{
    using System.Collections.Generic;
    using EveOnlineApi.Entities.Xml;

    /// <summary>
    /// Defines the interface for retrieving Eve Call List for API Access.
    /// </summary>
    public interface ICallListXmlDataProvider
    {
        /// <summary>
        /// Gets the Call List with Permissions Listed as Well
        /// </summary>
        /// <returns>CallList XML Object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "May take long to execute.")]
        CallListApi GetCallList();
    }
}
