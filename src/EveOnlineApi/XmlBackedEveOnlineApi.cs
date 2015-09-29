//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="XmlBackedEveOnlineApi.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi
{
    using EveOnlineApi.Common;
    using EveOnlineApi.Entities;
    using EveOnlineApi.Interfaces;
    using EveOnlineApi.Interfaces.Xml;

    using EveScanner.IoC;

    /// <summary>
    /// This is an un-cached interface to whatever XML API provider is registered
    /// to be used by the application. Hopefully that API provider is cached, since
    /// if it isn't, you're likely to be blocked by CCP at some point. This provider
    /// provides more a more friendly interface to the EVE data, complete with lazy
    /// loaded properties where applicable.
    /// </summary>
    public class XmlBackedEveOnlineApi : ICharacterDataProvider, ICorporationDataProvider, IAllianceDataProvider
    {
        /// <summary>
        /// Gets the Character Id for a particular Character Name
        /// </summary>
        /// <param name="characterName">Character Name</param>
        /// <returns>Character Id</returns>
        public int GetCharacterId(string characterName)
        {
            ICharacterXmlDataProvider xdp = Injector.Create<ICharacterXmlDataProvider>();
            return xdp.GetCharacterId(characterName);
        }

        /// <summary>
        /// Gets the Character Information for a particular Character Id.
        /// </summary>
        /// <param name="characterId">Character Id</param>
        /// <returns>Character Info Object</returns>
        public Character GetCharacterInfo(int characterId)
        {
            ICharacterXmlDataProvider xdp = Injector.Create<ICharacterXmlDataProvider>();
            return new Character(xdp.GetCharacterInfo(characterId));
        }

        /// <summary>
        /// Gets the Corporation Information for a Particular Corporation.
        /// </summary>
        /// <param name="corporationId">Corporation Id</param>
        /// <returns>Corporation Object</returns>
        public Corporation GetCorporationInfo(int corporationId)
        {
            ICorporationXmlDataProvider xdp = Injector.Create<ICorporationXmlDataProvider>();
            return new Corporation(xdp.GetCorporationInfo(corporationId));
        }

        /// <summary>
        /// Gets the Alliance Information for a particular Alliance without
        /// Member Corporation data. This information is much smaller than
        /// the full data set.
        /// </summary>
        /// <param name="allianceId">Id of the Alliance to retrieve</param>
        /// <returns>Alliance Object</returns>
        public Alliance GetAllianceInfo(int allianceId)
        {
            return this.GetAllianceInfo(allianceId, true);
        }

        /// <summary>
        /// Gets the Alliance Information for a particular Alliance.
        /// </summary>
        /// <param name="allianceId">Id of the Alliance to retrieve</param>
        /// <param name="suppressMemberCorps">Whether the member corp data should be suppressed</param>
        /// <returns>Alliance Object</returns>
        public Alliance GetAllianceInfo(int allianceId, bool suppressMemberCorps)
        {
            IAllianceXmlDataProvider xdp = Injector.Create<IAllianceXmlDataProvider>();
            return new Alliance(xdp.GetAllianceData(allianceId, suppressMemberCorps));
        }
    }
}
