//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="FitPicker.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.UI
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Form to allow quick low slot picking.
    /// </summary>
    public partial class FitPicker : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FitPicker"/> class.
        /// </summary>
        public FitPicker()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a reference to the Parent Form.
        /// </summary>
        public Form1 CallingForm { get; set; }

        /// <summary>
        /// Fills in the textbox with the text from the button
        /// </summary>
        /// <param name="button">Button to Source Text From</param>
        /// <param name="textbox">Textbox to fill</param>
        private static void Slot_Click(object button, TextBox textbox)
        {
            if (button == null || textbox == null)
            {
                return;
            }

            Button x = button as Button;

            if (x == null)
            {
                return;
            }

            textbox.Text = x.Text;
        }

        /// <summary>
        /// Called when the form is loaded.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void FitPicker_Load(object sender, EventArgs e)
        {
            if (this.CallingForm.BackColor == Color.Black)
            {
                this.CallingForm.ChangeFormColorsNightMode(this);
            }

            if (this.CallingForm.FormBorderStyle == FormBorderStyle.None)
            {
                this.Text = string.Empty;
                this.ControlBox = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
        }

        /// <summary>
        /// Called when a button for the first text slot is clicked.
        /// </summary>
        /// <param name="sender">Buttons in slot 1</param>
        /// <param name="e">Not provided.</param>
        private void Slot1_Click(object sender, EventArgs e)
        {
            FitPicker.Slot_Click(sender, this.slot1Text);
        }

        /// <summary>
        /// Called when a button for the second text slot is clicked.
        /// </summary>
        /// <param name="sender">Buttons in slot 2</param>
        /// <param name="e">Not provided.</param>
        private void Slot2_Click(object sender, EventArgs e)
        {
            FitPicker.Slot_Click(sender, this.slot2Text);
        }

        /// <summary>
        /// Called when a button for the third text slot is clicked.
        /// </summary>
        /// <param name="sender">Buttons in slot 1</param>
        /// <param name="e">Not provided.</param>
        private void Slot3_Click(object sender, EventArgs e)
        {
            FitPicker.Slot_Click(sender, this.slot3Text);
        }

        /// <summary>
        /// Loads text from the menu items into the 3 textboxes.
        /// </summary>
        /// <param name="sender">Menu Items</param>
        /// <param name="e">Not provided.</param>
        private void MultiSelectMenu_Click(object sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi == null)
            {
                return;
            }

            string textString = tsmi.Text.Substring(3);

            this.slot1Text.Text = textString;
            this.slot2Text.Text = textString;
            this.slot3Text.Text = textString;
        }

        /// <summary>
        /// Called when the OK button is clicked. Groups text items and returns text string to the parent form.
        /// </summary>
        /// <param name="sender">OK Button</param>
        /// <param name="e">Not provided.</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            List<string> output = new List<string>();

            string a = this.slot1Text.Text;
            string b = this.slot2Text.Text;
            string c = this.slot3Text.Text;

            if (a == b && b == c)
            {
                if (!string.IsNullOrEmpty(a))
                {
                    output.Add("3x " + a);
                }
            }
            else if (a == b && a != c)
            {
                if (!string.IsNullOrEmpty(a))
                {
                    output.Add("2x " + a);
                }

                if (!string.IsNullOrEmpty(c))
                {
                    output.Add("1x " + c);
                }
            }
            else if (b == c && a != b)
            {
                if (!string.IsNullOrEmpty(b))
                {
                    output.Add("2x " + a);
                }

                if (!string.IsNullOrEmpty(a))
                {
                    output.Add("1x " + c);
                }
            }
            else if (a == c && b != c)
            {
                if (!string.IsNullOrEmpty(a))
                {
                    output.Add("2x " + a);
                }

                if (!string.IsNullOrEmpty(b))
                {
                    output.Add("1x " + b);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(a))
                {
                    output.Add(a);
                }

                if (!string.IsNullOrEmpty(b))
                {
                    output.Add(b);
                }

                if (!string.IsNullOrEmpty(c))
                {
                    output.Add(c);
                }
            }

            this.CallingForm.UpdateFitType(string.Join(", ", output.OrderBy(x => x).Reverse().ToArray()));

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}