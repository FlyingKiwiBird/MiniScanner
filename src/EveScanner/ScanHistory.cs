//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ScanHistory.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using EveScanner.Interfaces;

    /// <summary>
    /// Used for displaying Scan History and doing some basic filtering.
    /// </summary>
    public partial class ScanHistory : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScanHistory"/> class.
        /// </summary>
        public ScanHistory()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a reference to the form which called this one.
        /// </summary>
        public Form1 CallingForm { get; set; }

        /// <summary>
        /// Called when the form loads. Sets up Grid with History and Scan Filters.
        /// </summary>
        /// <param name="sender">Form Load</param>
        /// <param name="e">This parameter is not used.</param>
        private void ScanHistory_Load(object sender, EventArgs e)
        {
            IScanHistory history = ConfigHelper.GetImplementation<IScanHistory>();

            List<HistoryWrapper> wrapped = history.GetAllScans().Select(x => new HistoryWrapper(x)).ToList();

            this.scanHistoryGrid.DataSource = wrapped;

            foreach (DataGridViewColumn c in this.scanHistoryGrid.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                c.Resizable = DataGridViewTriState.True;
                c.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            this.scanHistoryGrid.Columns["Id"].Visible = false;
            this.scanHistoryGrid.Columns["Scan"].Visible = false;
            this.scanHistoryGrid.Columns["ShipType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            var source = new AutoCompleteStringCollection();

            foreach (DataGridViewRow row in this.scanHistoryGrid.Rows)
            {
                if (row.Cells["CharacterName"] != null && row.Cells["CharacterName"].Value != null)
                {
                    string cn = row.Cells["CharacterName"].Value.ToString();
                    if (!source.Contains(cn))
                    {
                        source.Add(cn);
                    }
                }
            }

            this.characterName.AutoCompleteCustomSource = source;
            this.characterName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.characterName.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        /// <summary>
        /// Called when you double-click on a row header (to left of left-most cell).
        /// </summary>
        /// <param name="sender">This parameter is not used.</param>
        /// <param name="e">Event Arguments</param>
        private void Grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.ReturnRow(e.RowIndex);
        }

        /// <summary>
        /// Called when you double-click on a cell.
        /// </summary>
        /// <param name="sender">This parameter is not used.</param>
        /// <param name="e">Event Arguments</param>
        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.ReturnRow(e.RowIndex);
        }

        /// <summary>
        /// Called when you click the cancel button, sends you back to the parent form.
        /// </summary>
        /// <param name="sender">Cancel Button</param>
        /// <param name="e">This parameter is not used.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// Called when you click the OK button, sends you back to the parent form after checking out some data.
        /// </summary>
        /// <param name="sender">Copy to App</param>
        /// <param name="e">This parameter is not used.</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            int row = -1;

            if (this.scanHistoryGrid.SelectedRows.Count > 0)
            {
                row = this.scanHistoryGrid.SelectedRows[0].Index;
            }

            if (this.scanHistoryGrid.SelectedCells.Count > 0)
            {
                row = this.scanHistoryGrid.SelectedCells[0].RowIndex;
            }

            if (row == -1)
            {
                return;
            }

            this.ReturnRow(row);
        }

        /// <summary>
        /// Filters the List for Character Name
        /// </summary>
        /// <param name="sender">Character Filter Button</param>
        /// <param name="e">This parameter is not used.</param>
        private void CharacterFilter_Click(object sender, EventArgs e)
        {
            IScanHistory history = ConfigHelper.GetImplementation<IScanHistory>();

            List<HistoryWrapper> wrapped = history.GetScansByCharacterName(this.characterName.Text).Select(x => new HistoryWrapper(x)).ToList();

            this.scanHistoryGrid.DataSource = wrapped;
        }

        /// <summary>
        /// Returns a scan to the calling form.
        /// </summary>
        /// <param name="row">Row number from the Grid control</param>
        private void ReturnRow(int row)
        {
            if (row < 0)
            {
                return;
            }

            HistoryWrapper wrap = (HistoryWrapper)this.scanHistoryGrid.Rows[row].DataBoundItem;

            this.CallingForm.InjectExternalScan(wrap.Scan);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
