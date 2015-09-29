//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EmploymentHistory.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.UI
{
    using System;
    using System.Windows.Forms;

    using EveOnlineApi.Entities;

    /// <summary>
    /// Shows the Employment History for a Given Character
    /// </summary>
    public partial class EmploymentHistory : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmploymentHistory"/> class.
        /// </summary>
        /// <param name="ch">Character to display employment history for</param>
        public EmploymentHistory(Character ch)
        {
            this.InitializeComponent();
            this.PopulateEmploymentHistory(ch);
        }

        /// <summary>
        /// Populates the Employment History list box with the Character's Employment History
        /// </summary>
        /// <param name="ch">Character Entity</param>
        private void PopulateEmploymentHistory(Character ch)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => this.PopulateEmploymentHistory(ch)));
                return;
            }

            foreach (var em in ch.EmploymentHistory)
            {
                this.employmentHistoryListbox1.Items.Add(em);
            }
        }
    }
}
