//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IEmploymentHistoryEntry.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    using System;

    /// <summary>
    /// Defines a record of Employment for an Eve Online character.
    /// </summary>
    public interface IEmploymentHistoryEntry
    {
        /// <summary>
        /// Gets the Corporation Object
        /// </summary>
        ICorporation Corporation { get; }

        /// <summary>
        /// Gets or sets the Corporation Id
        /// </summary>
        int CorporationId { get; set; }

        /// <summary>
        /// Gets or sets the End of Employment in UTC.
        /// </summary>
        DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the Employment Record Id
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the Start of Employment in UTC.
        /// </summary>
        DateTime StartDate { get; set; }
    }
}