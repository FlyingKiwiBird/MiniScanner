//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ICharacter.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the data exposed about an Eve Online character.
    /// </summary>
    public interface ICharacter
    {
        /// <summary>
        /// Gets a value indicating whether we pulled corp/alliance data.
        /// </summary>
        bool AlliancePopulated { get; }

        /// <summary>
        /// Gets the Corporation associated with the Character.
        /// </summary>
        ICorporation Corporation { get; }

        /// <summary>
        /// Gets the Character's Corporation Id.
        /// </summary>
        int CorporationId { get; }

        /// <summary>
        /// Gets the character's employment history.
        /// </summary>
        IEnumerable<IEmploymentHistoryEntry> EmploymentHistory { get; }

        /// <summary>
        /// Gets the Character Id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the Character Name;
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the character's security status.
        /// </summary>
        double SecurityStatus { get; }

        /// <summary>
        /// Gets the Character's Standings.
        /// </summary>
        IStandings Standings { get; }

        /// <summary>
        /// Forces the object to populate data down to the alliance level.
        /// </summary>
        void PopulateCorpAllianceData();

        /// <summary>
        /// Gets the Character's Standings.
        /// </summary>
        void PopulateStandings();

        /// <summary>
        /// Returns a String that represents the current Character.
        /// </summary>
        /// <returns>String describing Character</returns>
        string ToString();
    }
}