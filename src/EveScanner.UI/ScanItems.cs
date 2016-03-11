using EveScanner.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EveScanner.UI
{
    public partial class ScanItems : Form
    {
        private IEnumerable<ILineAppraisal> items = null;

        public ScanItems(IEnumerable<ILineAppraisal> items)
        {
            InitializeComponent();

            this.items = items;
        }

        private void ScanItems_Load(object sender, EventArgs e)
        {
            this.scanItemsGrid.DataSource = items.ToList();

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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
