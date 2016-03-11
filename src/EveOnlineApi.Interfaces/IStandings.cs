//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IStandings.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    /// <summary>
    /// Represents a Standing from person to person/corporation/alliance in Eve Online.
    /// </summary>
    public interface IStandings
    {
        /// <summary>
        /// Gets or sets the Alliance Standing to the Alliance
        /// </summary>
        decimal AllianceStandingAlliance { get; set; }

        /// <summary>
        /// Gets or sets the Alliance Standing to the Character
        /// </summary>
        decimal AllianceStandingCharacter { get; set; }

        /// <summary>
        /// Gets or sets the Alliance Standing to the Corporation
        /// </summary>
        decimal AllianceStandingCorporation { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Standing to the Alliance
        /// </summary>
        decimal CorporationStandingAlliance { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Standing to the Character
        /// </summary>
        decimal CorporationStandingCharacter { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Standing to the Corporation
        /// </summary>
        decimal CorporationStandingCorporation { get; set; }

        /// <summary>
        /// Gets the derived standings. Personal Standings have priority over Corporation and Alliance Standings.
        /// </summary>
        decimal DerivedStanding { get; }

        /// <summary>
        /// Gets or sets the Personal Standing to the Alliance
        /// </summary>
        decimal PersonalStandingAlliance { get; set; }

        /// <summary>
        /// Gets or sets the Personal Standing to the Character
        /// </summary>
        decimal PersonalStandingCharacter { get; set; }

        /// <summary>
        /// Gets or sets the Personal Standing to the Corporation
        /// </summary>
        decimal PersonalStandingCorporation { get; set; }
    }
}