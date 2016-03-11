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
    using EveScanner.Interfaces;
    using EveOnlineApi.Interfaces;
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
            this.derivedValue.Text = this.DecimalFormat(this.standings.DerivedStanding);

            this.personalCharacterValue.Text = this.DecimalFormat(this.standings.PersonalStandingCharacter);
            this.personalCorporationValue.Text = this.DecimalFormat(this.standings.PersonalStandingCorporation);
            this.personalAllianceValue.Text = this.DecimalFormat(this.standings.PersonalStandingAlliance);

            this.corporationCharacterValue.Text = this.DecimalFormat(this.standings.CorporationStandingCharacter);
            this.corporationCorporationValue.Text = this.DecimalFormat(this.standings.CorporationStandingCorporation);
            this.corporationAllianceValue.Text = this.DecimalFormat(this.standings.CorporationStandingAlliance);

            this.allianceCharacterValue.Text = this.DecimalFormat(this.standings.AllianceStandingCharacter);
            this.allianceCorporationValue.Text = this.DecimalFormat(this.standings.AllianceStandingCorporation);
            this.allianceAllianceValue.Text = this.DecimalFormat(this.standings.AllianceStandingAlliance);
        }

        /// <summary>
        /// Formats a decimal number with two places after the decimal.
        /// </summary>
        /// <param name="d">Decimal to format</param>
        /// <returns>Formatted string</returns>
        private string DecimalFormat(decimal d)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:0.00}", d);
        }
    }
}
