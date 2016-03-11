//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="XmlBackedEveOnlineApi.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi
{
    using System.Linq;

    using EveOnlineApi.Entities;
    using EveOnlineApi.Entities.Xml;
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
    public class XmlBackedEveOnlineApi : ICharacterDataProvider, ICorporationDataProvider, IAllianceDataProvider, IStandingsDataProvider
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
        public ICharacter GetCharacterInfo(int characterId)
        {
            ICharacterXmlDataProvider xdp = Injector.Create<ICharacterXmlDataProvider>();
            return new Character(xdp.GetCharacterInfo(characterId));
        }

        /// <summary>
        /// Gets the Corporation Information for a Particular Corporation.
        /// </summary>
        /// <param name="corporationId">Corporation Id</param>
        /// <returns>Corporation Object</returns>
        public ICorporation GetCorporationInfo(int corporationId)
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
        public IAlliance GetAllianceInfo(int allianceId)
        {
            return this.GetAllianceInfo(allianceId, true);
        }

        /// <summary>
        /// Gets the Alliance Information for a particular Alliance.
        /// </summary>
        /// <param name="allianceId">Id of the Alliance to retrieve</param>
        /// <param name="suppressMemberCorps">Whether the member corp data should be suppressed</param>
        /// <returns>Alliance Object</returns>
        public IAlliance GetAllianceInfo(int allianceId, bool suppressMemberCorps)
        {
            IAllianceXmlDataProvider xdp = Injector.Create<IAllianceXmlDataProvider>();
            return new Alliance(xdp.GetAllianceData(allianceId, suppressMemberCorps));
        }

        /// <summary>
        /// Gets Standings for a given Entity
        /// </summary>
        /// <param name="entityName">Name of Entity</param>
        /// <param name="entityType">Type of Entity</param>
        /// <returns>Standings Information</returns>
        public IStandings GetStandingsInfo(string entityName, IEntityType entityType)
        {
            int entityId = this.GetCharacterId(entityName);

            // This is a little complicated...
            ICharacter ch = null;
            ICorporation cp = null;
            IAlliance al = null;

            if (entityType == EntityType.Character)
            {
                ch = this.GetCharacterInfo(entityId);
            }

            if (entityType == EntityType.Corporation)
            {
                cp = this.GetCorporationInfo(entityId);
            }

            if (entityType == EntityType.Alliance)
            {
                al = this.GetAllianceInfo(entityId);
            }

            if (ch != null && ch.CorporationId > 0)
            {
                cp = ch.Corporation;
            }

            if (cp != null && cp.AllianceId > 0)
            {
                al = cp.Alliance;
            }

            IContactListXmlDataProvider xdp = Injector.Create<IContactListXmlDataProvider>();

            ContactListApi api = xdp.GetContactList();

            decimal psch = 0, pscr = 0, psa = 0, csch = 0, cscr = 0, csa = 0, asch = 0, ascr = 0, asa = 0;

            if (al != null)
            {
                if (api.Result.ContactList != null)
                {
                    var a1 = api.Result.ContactList.Rows.Where(x => x.ContactName == al.Name && x.ContactTypeId == EntityType.Alliance).FirstOrDefault();
                    if (a1 != null)
                    {
                        psa = a1.Standing;
                    }
                }

                if (api.Result.CorporateContactList != null)
                {
                    var a2 = api.Result.CorporateContactList.Rows.Where(x => x.ContactName == al.Name && x.ContactTypeId == EntityType.Alliance).FirstOrDefault();
                    if (a2 != null)
                    {
                        csa = a2.Standing;
                    }
                }

                if (api.Result.AllianceContactList != null)
                {
                    var a3 = api.Result.AllianceContactList.Rows.Where(x => x.ContactName == al.Name && x.ContactTypeId == EntityType.Alliance).FirstOrDefault();
                    if (a3 != null)
                    {
                        asa = a3.Standing;
                    }
                }
            }

            if (cp != null)
            {
                if (api.Result.ContactList != null)
                {
                    var c1 = api.Result.ContactList.Rows.Where(x => x.ContactName == cp.Name && x.ContactTypeId == EntityType.Corporation).FirstOrDefault();
                    if (c1 != null)
                    {
                        pscr = c1.Standing;
                    }
                }

                if (api.Result.CorporateContactList != null)
                {
                    var c2 = api.Result.CorporateContactList.Rows.Where(x => x.ContactName == cp.Name && x.ContactTypeId == EntityType.Corporation).FirstOrDefault();
                    if (c2 != null)
                    {
                        cscr = c2.Standing;
                    }
                }

                if (api.Result.AllianceContactList != null)
                {
                    var c3 = api.Result.AllianceContactList.Rows.Where(x => x.ContactName == cp.Name && x.ContactTypeId == EntityType.Corporation).FirstOrDefault();
                    if (c3 != null)
                    {
                        ascr = c3.Standing;
                    }
                }
            }

            if (ch != null)
            {
                if (api.Result.ContactList != null)
                {
                    var h1 = api.Result.ContactList.Rows.Where(x => x.ContactName == ch.Name && x.ContactTypeId == EntityType.Character).FirstOrDefault();
                    if (h1 != null)
                    {
                        psch = h1.Standing;
                    }
                }

                if (api.Result.CorporateContactList != null)
                {
                    var h2 = api.Result.CorporateContactList.Rows.Where(x => x.ContactName == ch.Name && x.ContactTypeId == EntityType.Character).FirstOrDefault();
                    if (h2 != null)
                    {
                        csch = h2.Standing;
                    }
                }

                if (api.Result.AllianceContactList != null)
                {
                    var h3 = api.Result.AllianceContactList.Rows.Where(x => x.ContactName == ch.Name && x.ContactTypeId == EntityType.Character).FirstOrDefault();
                    if (h3 != null)
                    {
                        asch = h3.Standing;
                    }
                }
            }

            return new Standings(psch, pscr, psa, csch, cscr, csa, asch, ascr, asa);
        }
    }
}
