//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IAlliance.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents an Eve Online Alliance
    /// </summary>
    public interface IAlliance
    {
        /// <summary>
        /// Gets the Executor Corporation as an object.
        /// </summary>
        ICorporation ExecutorCorporation { get; }

        /// <summary>
        /// Gets the Corporation Id of the Alliance's Executor Corporation
        /// </summary>
        int ExecutorCorporationId { get; }

        /// <summary>
        /// Gets the Alliance id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets or sets the Member Corporations in the Alliance.
        /// </summary>
        IEnumerable<IAllianceMemberCorporation> MemberCorporations { get; set; }

        /// <summary>
        /// Gets the count of members in the Alliance.
        /// </summary>
        int MemberCount { get; }

        /// <summary>
        /// Gets the name of the Alliance.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Alliance ticker.
        /// </summary>
        string ShortName { get; }

        /// <summary>
        /// Gets the start date of the Alliance.
        /// </summary>
        DateTime StartDate { get; }
    }
}