//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="MenuButton.cs">
// Original Code Copyright © Jaex 2014 as per http://stackoverflow.com/a/24087828. License is cc by-sa 3.0.
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.UI
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// This displays a button with a Context Menu for Dropdown.
    /// Shamelessly stolen from here: http://stackoverflow.com/a/24087828
    /// </summary>
    public class MenuButton : Button
    {
        /// <summary>
        /// Gets or sets the Menu we're going to display when the button is pressed.
        /// </summary>
        [DefaultValue(null)]
        public ContextMenuStrip Menu { get; set; }

        /// <summary>
        /// Raised from the OnMouseDown event. Happens when the mouse button is pressed.
        /// </summary>
        /// <param name="mevent">Contains the event data</param>
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent == null)
            {
                return;
            }

            base.OnMouseDown(mevent);

            if (this.Menu != null && mevent.Button == MouseButtons.Left)
            {
                this.Menu.Show(this, mevent.Location);
            }
        }

        /// <summary>
        /// Raised on the OnPaint event. Happens when the control is drawn.
        /// </summary>
        /// <param name="pevent">Contains the event data</param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (pevent == null)
            {
                return;
            }

            base.OnPaint(pevent);

            int arrowX = ClientRectangle.Width - 14;
            int arrowY = (ClientRectangle.Height / 2) - 1;

            Brush brush = Enabled ? SystemBrushes.ControlText : SystemBrushes.ButtonShadow;
            Point[] arrows = new Point[] { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
            pevent.Graphics.FillPolygon(brush, arrows);
        }
    }
}
