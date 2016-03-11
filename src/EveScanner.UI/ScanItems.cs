//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ScanItems.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using EveScanner.Interfaces;

    /// <summary>
    /// Form for showing items which have been scanned along with values.
    /// </summary>
    public partial class ScanItems : Form
    {
        /// <summary>
        /// Holds the Scanned Items
        /// </summary>
        private IEnumerable<ILineAppraisal> items = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanItems"/> class.
        /// </summary>
        /// <param name="items">Scanned Items</param>
        public ScanItems(IEnumerable<ILineAppraisal> items)
        {
            this.InitializeComponent();

            this.items = items;
        }

        /// <summary>
        /// Run when the form is loaded and shown to the user.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void ScanItems_Load(object sender, EventArgs e)
        {
            this.scanItemsGrid.DataSource = this.items.ToList();

            this.scanItemsGrid.Columns["TypeId"].Visible = false;
            this.scanItemsGrid.Columns["GroupId"].Visible = false;
            this.scanItemsGrid.Columns["IsBlueprintCopy"].Visible = false;
            this.scanItemsGrid.Columns["IsError"].Visible = false;
            this.scanItemsGrid.Columns["ErrorMessage"].Visible = false;

            foreach (DataGridViewColumn c in this.scanItemsGrid.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                c.Resizable = DataGridViewTriState.True;
                c.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        /// <summary>
        /// Called when you click the cancel button, sends you back to the parent form.
        /// </summary>
        /// <param name="sender">Cancel Button</param>
        /// <param name="e">This parameter is not used.</param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
