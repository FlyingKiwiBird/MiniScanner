//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IAllianceMemberCorporation.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    using System;

    /// <summary>
    /// Represents an Eve Online Alliance Member Corporation
    /// </summary>
    public interface IAllianceMemberCorporation
    {
        /// <summary>
        /// Gets the Corporation object..
        /// </summary>
        ICorporation Corporation { get; }

        /// <summary>
        /// Gets the Corporation Id
        /// </summary>
        int CorporationId { get; }

        /// <summary>
        /// Gets the time the corporation joined the Alliance
        /// </summary>
        DateTime StartTime { get; }
    }
}