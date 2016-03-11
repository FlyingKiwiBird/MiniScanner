//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Standings.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities
{
    using EveOnlineApi.Interfaces;
    using EveScanner.Interfaces;
    using EveScanner.IoC;

    /// <summary>
    /// Represents a Standing from person to person/corporation/alliance in Eve Online.
    /// </summary>
    public class Standings : IStandings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Standings"/> class.
        /// </summary>
        /// <param name="psch">Personal Standing to Character</param>
        /// <param name="pscr">Personal Standing to Corporation</param>
        /// <param name="psa">Personal Standing to Alliance</param>
        /// <param name="csch">Corporation Standing to Character</param>
        /// <param name="cscr">Corporation Standing to Corporation</param>
        /// <param name="csa">Corporation Standing to Alliance</param>
        /// <param name="asch">Alliance Standing to Character</param>
        /// <param name="ascr">Alliance Standing to Corporation</param>
        /// <param name="asa">Alliance Standing to Alliance</param>
        public Standings(decimal psch, decimal pscr, decimal psa, decimal csch, decimal cscr, decimal csa, decimal asch, decimal ascr, decimal asa)
        {
            this.PersonalStandingCharacter = psch;
            this.PersonalStandingCorporation = pscr;
            this.PersonalStandingAlliance = psa;

            this.CorporationStandingCharacter = csch;
            this.CorporationStandingCorporation = cscr;
            this.CorporationStandingAlliance = csa;

            this.AllianceStandingCharacter = asch;
            this.AllianceStandingCorporation = ascr;
            this.AllianceStandingAlliance = asa;
        }

        /// <summary>
        /// Gets or sets the Personal Standing to the Character
        /// </summary>
        public decimal PersonalStandingCharacter { get; set; }

        /// <summary>
        /// Gets or sets the Personal Standing to the Corporation
        /// </summary>
        public decimal PersonalStandingCorporation { get; set; }

        /// <summary>
        /// Gets or sets the Personal Standing to the Alliance
        /// </summary>
        public decimal PersonalStandingAlliance { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Standing to the Character
        /// </summary>
        public decimal CorporationStandingCharacter { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Standing to the Corporation
        /// </summary>
        public decimal CorporationStandingCorporation { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Standing to the Alliance
        /// </summary>
        public decimal CorporationStandingAlliance { get; set; }

        /// <summary>
        /// Gets or sets the Alliance Standing to the Character
        /// </summary>
        public decimal AllianceStandingCharacter { get; set; }

        /// <summary>
        /// Gets or sets the Alliance Standing to the Corporation
        /// </summary>
        public decimal AllianceStandingCorporation { get; set; }

        /// <summary>
        /// Gets or sets the Alliance Standing to the Alliance
        /// </summary>
        public decimal AllianceStandingAlliance { get; set; }

        /// <summary>
        /// Gets the derived standings. Personal Standings have priority over Corporation and Alliance Standings.
        /// </summary>
        public decimal DerivedStanding
        {
            get
            {
                decimal[] output = 
                                   {
                                     this.PersonalStandingCharacter, this.PersonalStandingCorporation, this.PersonalStandingAlliance,
                                     this.CorporationStandingCharacter, this.CorporationStandingCorporation, this.CorporationStandingAlliance,
                                     this.AllianceStandingCharacter, this.AllianceStandingCorporation, this.AllianceStandingAlliance
                                   };

                for (int i = 0; i < output.Length; i++)
                {
                    if (output[i] != 0)
                    {
                        return output[i];
                    }
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the calculated standings for a particular entity.
        /// </summary>
        /// <param name="entityName">Entity Name</param>
        /// <param name="entityType">Entity Type</param>
        /// <returns>Standings Object with all Standings</returns>
        public static IStandings GetStandings(string entityName, IEntityType entityType)
        {
            IStandingsDataProvider adp = Injector.Create<IStandingsDataProvider>();
            return adp.GetStandingsInfo(entityName, entityType);
        }
    }
}
