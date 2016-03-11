//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="StandingsForm.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.UI
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;

    using EveOnlineApi.Entities;
    using EveOnlineApi.Interfaces;
    using EveScanner.Interfaces;

    /// <summary>
    /// Used to display standings for a character.
    /// </summary>
    public partial class StandingsForm : Form
    {
        /// <summary>
        /// Holds the standings object.
        /// </summary>
        private IStandings standings;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandingsForm"/> class.
        /// </summary>
        /// <param name="standings">Standings to Display</param>
        public StandingsForm(IStandings standings)
        {
            this.InitializeComponent();

            this.standings = standings;
        }

        /// <summary>
        /// Runs when the Standings Form loads
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void Standings_Load(object sender, EventArgs e)
        {
            this.derivedValue.Text = StandingsForm.DecimalFormat(this.standings.DerivedStanding);

            this.personalCharacterValue.Text = StandingsForm.DecimalFormat(this.standings.PersonalStandingCharacter);
            this.personalCorporationValue.Text = StandingsForm.DecimalFormat(this.standings.PersonalStandingCorporation);
            this.personalAllianceValue.Text = StandingsForm.DecimalFormat(this.standings.PersonalStandingAlliance);

            this.corporationCharacterValue.Text = StandingsForm.DecimalFormat(this.standings.CorporationStandingCharacter);
            this.corporationCorporationValue.Text = StandingsForm.DecimalFormat(this.standings.CorporationStandingCorporation);
            this.corporationAllianceValue.Text = StandingsForm.DecimalFormat(this.standings.CorporationStandingAlliance);

            this.allianceCharacterValue.Text = StandingsForm.DecimalFormat(this.standings.AllianceStandingCharacter);
            this.allianceCorporationValue.Text = StandingsForm.DecimalFormat(this.standings.AllianceStandingCorporation);
            this.allianceAllianceValue.Text = StandingsForm.DecimalFormat(this.standings.AllianceStandingAlliance);
        }

        /// <summary>
        /// Formats a decimal number with two places after the decimal.
        /// </summary>
        /// <param name="d">Decimal to format</param>
        /// <returns>Formatted string</returns>
        private static string DecimalFormat(decimal d)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:0.00}", d);
        }
    }
}
