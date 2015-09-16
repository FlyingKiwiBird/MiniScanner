//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ShipPicker.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Form to allow quick visual ship picking.
    /// </summary>
    public partial class ShipPicker : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShipPicker"/> class.
        /// </summary>
        public ShipPicker()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a reference to the Parent Form.
        /// </summary>
        public Form1 CallingForm { get; set; }

        /// <summary>
        /// Loads all the images for the form and sets up the location.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void ShipPicker_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = Image.FromFile(@"images\\1-1-PROVI.png");
            this.pictureBox2.Image = Image.FromFile(@"images\\1-2-CHARON.png");
            this.pictureBox3.Image = Image.FromFile(@"images\\1-3-OBELISK.png");
            this.pictureBox4.Image = Image.FromFile(@"images\\1-4-FENRIR.png");
            this.pictureBox5.Image = Image.FromFile(@"images\\2-1-ARK.png");
            this.pictureBox6.Image = Image.FromFile(@"images\\2-2-RHEA.png");
            this.pictureBox7.Image = Image.FromFile(@"images\\2-3-ANSHAR.png");
            this.pictureBox8.Image = Image.FromFile(@"images\\2-4-NOMAD.png");
            this.pictureBox9.Image = Image.FromFile(@"images\\3-1-ORCA.png");
            this.pictureBox10.Image = Image.FromFile(@"images\\3-2-BOWHEAD.png");

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
        /// Sends a message back to the parent form indicating which image was clicked, and closes the form.
        /// </summary>
        /// <param name="sender">Any of the ship images.</param>
        /// <param name="e">Not provided.</param>
        private void Picture_Click(object sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            PictureBox p = sender as PictureBox;
            if (p == null)
            {
                return;
            }

            if (p.Tag == null)
            {
                return;
            }

            string tag = (string)p.Tag;
            if (string.IsNullOrEmpty(tag))
            {
                return;
            }

            this.CallingForm.UpdateShipType(tag);
            this.Close();
        }
    }
}
