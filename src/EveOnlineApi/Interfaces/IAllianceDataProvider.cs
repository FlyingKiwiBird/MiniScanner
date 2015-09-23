//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IAllianceDataProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    using EveOnlineApi.Entities;

    /// <summary>
    /// Defines the interface for retrieving Alliance data.
    /// </summary>
    public interface IAllianceDataProvider
    {
        /// <summary>
        /// Gets the Alliance Information for a particular Alliance without
        /// Member Corporation data. This information is much smaller than
        /// the full data set.
        /// </summary>
        /// <param name="allianceId">Id of the Alliance to retrieve</param>
        /// <returns>Alliance Object</returns>
        Alliance GetAllianceInfo(int allianceId);

        /// <summary>
        /// Gets the Alliance Information for a particular Alliance.
        /// </summary>
        /// <param name="allianceId">Id of the Alliance to retrieve</param>
        /// <param name="suppressMemberCorps">Whether the member corp data should be suppressed</param>
        /// <returns>Alliance Object</returns>
        Alliance GetAllianceInfo(int allianceId, bool suppressMemberCorps);
    }
}
