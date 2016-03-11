//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ICorporation.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    /// <summary>
    /// Represents an EVE Online Corporation
    /// </summary>
    public interface ICorporation
    {
        /// <summary>
        /// Gets the Alliance object associated with the Corporation
        /// </summary>
        IAlliance Alliance { get; }

        /// <summary>
        /// Gets the Corporation Alliance Id
        /// </summary>
        int AllianceId { get; }

        /// <summary>
        /// Gets the Character Object for the CEO.
        /// </summary>
        ICharacter CeoCharacter { get; }

        /// <summary>
        /// Gets the Character Id for the Corporation CEO
        /// </summary>
        int CeoCharacterId { get; }

        /// <summary>
        /// Gets the Corporation Description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the FactionId the Corporation is associated with.
        /// </summary>
        int FactionId { get; }

        /// <summary>
        /// Gets the Station Id for the Corporation Home Station
        /// </summary>
        int HomeStationId { get; }

        /// <summary>
        /// Gets the Corporation Id
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets information used to render the corporation logo.
        /// </summary>
        ICorporationLogo Logo { get; }

        /// <summary>
        /// Gets the number of members in the Corporation;
        /// </summary>
        int MemberCount { get; }

        /// <summary>
        /// Gets the Corporation Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the number of shares assigned from the corporation,
        /// </summary>
        long Shares { get; }

        /// <summary>
        /// Gets the Tax Rate for the Corporation;
        /// </summary>
        double TaxRate { get; }

        /// <summary>
        /// Gets the Corporation Ticket
        /// </summary>
        string Ticker { get; }

        /// <summary>
        /// Gets the Corporation URL
        /// </summary>
        string Url { get; }
    }
}