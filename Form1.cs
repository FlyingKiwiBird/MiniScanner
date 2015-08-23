//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Form1.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    /// <summary>
    /// Main Form for the Application
    /// </summary>
    public partial class Form1 : Form
    {
        #region Private Fields
        /// <summary>
        /// Constant for the WM_DRAWCLIPBOARD message.
        /// </summary>
        private const int WM_DRAWCLIPBOARD = 0x0308;        // WM_DRAWCLIPBOARD message

        /// <summary>
        /// Holds the Handle to the next clipboard viewer in the chain.
        /// </summary>
        private IntPtr clipboardViewerNext;                // Our variable that will hold the value to identify the next window in the clipboard viewer chain

        /// <summary>
        /// Holds a list of all results which have been parsed.
        /// </summary>
        private List<ScanResult> scans = new List<ScanResult>();
        
        /// <summary>
        /// Holds the current result being parsed.
        /// </summary>
        private ScanResult result = null;
        
        /// <summary>
        /// Holds a value indicating if we're running on windows, which is necessary for clipboard setup/teardown.
        /// </summary>
        private bool runningOnWindows = true;

        /// <summary>
        /// Holds a value indicating if the clipboard event has fired yet.
        /// </summary>
        private bool firstFire = true;
        #endregion Private Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class. This method
        /// initializes the component, and moves it to the last saved location.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();

            if (EveScannerConfig.Instance.WindowPosX == -1 && EveScannerConfig.Instance.WindowPosY == -1)
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(EveScannerConfig.Instance.WindowPosX, EveScannerConfig.Instance.WindowPosY);
            }
        }
        #endregion Constructors

        #region P/Invoked Methods
        /// <summary>
        /// Adds a clipboard viewer to the current chain of clipboard registrees.
        /// </summary>
        /// <param name="hWndNewViewer">Handle to current process</param>
        /// <returns>Handle of next process in chain</returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        /// <summary>
        /// Removes a clipboard viewer from the chain of clipboard registrees, adding one back in its place.
        /// </summary>
        /// <param name="hWndRemove">Handle to remove from chain.</param>
        /// <param name="hWndNewNext">Handle to replace with in chain.</param>
        /// <returns>True if successful, false otherwise.</returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
        #endregion P/Invoked Methods

        #region Form Events
        /// <summary>
        /// This processes window messages such as clipboard events.
        /// </summary>
        /// <param name="m">Window Message</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (!EveScannerConfig.Instance.CaptureClipboard)
            {
                return;
            }

            if (m.Msg == WM_DRAWCLIPBOARD)
            {
                if (this.firstFire)
                {
                    Logger.Debug("First clipboard event captured, skipping.");
                    this.firstFire = false;
                    return;
                }

                IDataObject obj = Clipboard.GetDataObject();

                string format = string.Empty;
                if (obj.GetDataPresent(DataFormats.OemText))
                {
                    format = DataFormats.OemText;
                }

                if (obj.GetDataPresent(DataFormats.Text))
                {
                    format = DataFormats.Text;
                }

                if (obj.GetDataPresent(DataFormats.UnicodeText))
                {
                    format = DataFormats.UnicodeText;
                }

                if (!string.IsNullOrEmpty(format))
                {
                    string data = (string)obj.GetData(format);
                    Logger.Debug("Captured scan {0}", data);
                    if (this.CheckTextFormat(data))
                    {
                        Logger.Scan("Captured scan {0}", data);
                        this.scanText.Text = data;
                        this.SubmitRequestButton_Click(this.submitRequestButton, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Called when the form loads, sets up clipboard, always on top, etc.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not provided.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            var pl = Environment.OSVersion.Platform;
            this.runningOnWindows = pl == PlatformID.Win32NT;

            if (this.runningOnWindows)
            {
                Logger.Debug("Attaching Clipboard Handler");
                this.clipboardViewerNext = SetClipboardViewer(this.Handle);      // Adds our form to the chain of clipboard viewers.
            }

            this.captureClipboardOnToolStripMenuItem.Checked = EveScannerConfig.Instance.CaptureClipboard;
            this.toggleAlwaysOnTopToolStripMenuItem.Checked = EveScannerConfig.Instance.AlwaysOnTop;
            if (EveScannerConfig.Instance.AlwaysOnTop)
            {
                this.TopMost = true;
            }

            if (!EveScannerConfig.Instance.ShowExtra)
            {
                this.ShowHideExtraOptionsToolStripMenuItem_Click(null, EventArgs.Empty);
            }

            this.Width = EveScannerConfig.Instance.AppWidth;
            this.Height = EveScannerConfig.Instance.AppHeight;

            for (int i = 0; i < loggingToolStripMenuItem.DropDownItems.Count; i++)
            {
                ToolStripItem c = loggingToolStripMenuItem.DropDownItems[i];
                if ((string)c.Tag == EveScannerConfig.Instance.DebugLevel)
                {
                    this.DebugLevelStripMenu_Click(c, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Called when the form is closing.
        /// </summary>
        /// <param name="sender">The Closing Form</param>
        /// <param name="e">Events for Closing</param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.runningOnWindows)
            {
                Logger.Debug("Detaching Clipboard Handler");
                ChangeClipboardChain(this.Handle, this.clipboardViewerNext);        // Removes our from the chain of clipboard viewers when the form closes.
            }

            Logger.Debug("Saving Config");
            EveScannerConfig.Instance.Save();
            Logger.Debug("Config Saved");
        }
        #endregion Form Events

        #region Form Controls
        /// <summary>
        /// Called when the application File->Exit is called.
        /// </summary>
        /// <param name="sender">File -> Exit</param>
        /// <param name="e">Not provided.</param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Exiting application");
            Application.Exit();
        }

        /// <summary>
        /// This method submits the scan text from the text box to Evepraisal. This method is called by the
        /// clipboard handler as well to do the submission process.
        /// </summary>
        /// <param name="sender">Raw Scan Data -> Manually Submit</param>
        /// <param name="e">Not provided.</param>
        private void SubmitRequestButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(scanText.Text))
            {
                return;
            }

            if (!this.CheckTextFormat(scanText.Text))
            {
                return;
            }

            try
            {
                Evepraisal ep = new Evepraisal();
                string appraisal = ep.GetAppraisal(this.scanText.Text);
                this.result = new ScanResult(appraisal);
                this.scanText.Text = this.result.RawScan;
                this.AddResultToList(this.result);
                this.ParseCurrentResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }

            // If this isn't set to Always on Top, pop the screen up.
            if (!this.TopMost)
            {
                this.TopMost = true;
                this.Activate();
                this.TopMost = false;
            }
        }

        /// <summary>
        /// Copies a summary of the current selected result to the clipboard.
        /// </summary>
        /// <param name="sender">Results Pane -> Copy Summary to Clipboard</param>
        /// <param name="e">Not provided</param>
        private void CopySummaryButton_Click(object sender, EventArgs e)
        {
            if (this.result == null)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();

            string shipname = string.Empty;
            if ((shipname = this.GetShipName()) != string.Empty)
            {
                sb.Append(shipname + " | ");
            }

            sb.Append(this.result.ToString());

            string location = string.Empty;
            if ((location = this.GetLocation()) != string.Empty)
            {
                sb.Append(" | " + location);
            }

            Clipboard.SetText(sb.ToString());
        }

        /// <summary>
        /// Retrieves an historical scan from the dropdown.
        /// </summary>
        /// <param name="sender">Results Pane -> Dropdown</param>
        /// <param name="e">Not provided</param>
        private void HistoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb == null)
            {
                return;
            }

            if (this.scans.Count == 0)
            {
                return;
            }

            int scanIndex = this.scans.Count - cb.SelectedIndex - 1;

            this.result = this.scans[scanIndex];

            this.ParseCurrentResult();
        }

        /// <summary>
        /// This clears the "checked" state when clicking a radio button while holding shift or control.
        /// </summary>
        /// <param name="sender">Many radio button controls</param>
        /// <param name="e">Not provided</param>
        private void RadioButton_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) > 0)
            {
                RadioButton btn = sender as RadioButton;
                if (btn != null)
                {
                    btn.Checked = false;
                }
            }
        }

        /// <summary>
        /// This sets up the checkmarks properly when clicking an item on the debug menu.
        /// </summary>
        /// <param name="sender">Options -> Debug</param>
        /// <param name="e">Not provided.</param>
        private void DebugLevelStripMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi == null)
            {
                return;
            }

            foreach (ToolStripMenuItem tsmx in tsmi.GetCurrentParent().Items)
            {
                if (tsmx == tsmi)
                {
                    tsmx.Checked = true;
                }
                else
                {
                    tsmx.Checked = false;
                }
            }

            EveScannerConfig.Instance.DebugLevel = (string)tsmi.Tag;
        }
        #endregion Form Controls

        #region Menuitems
        /// <summary>
        /// Shows or hides the extra menu items. This method is also called when the app starts if they
        /// need to be hidden.
        /// </summary>
        /// <param name="sender">Show/Hide Extra Buttons</param>
        /// <param name="e">Not provided</param>
        private void ShowHideExtraOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();

            int containerBottomToFormBottom = this.Height - (resultsContainer.Location.Y + resultsContainer.Size.Height);
            int newHeight = 0;

            if (shipContainer.Visible)
            {
                shipContainer.Visible = false;
                locationContainer.Visible = false;
                scanContainer.Visible = false;

                newHeight = shipContainer.Location.Y + resultsContainer.Height + containerBottomToFormBottom;
            }
            else
            {
                shipContainer.Visible = true;
                locationContainer.Visible = true;
                scanContainer.Visible = true;

                int containerBottomToContainerTop = locationContainer.Top - (shipContainer.Location.Y + shipContainer.Size.Height);
                newHeight = shipContainer.Location.Y + shipContainer.Height + locationContainer.Height + (2 * containerBottomToContainerTop) + resultsContainer.Height + containerBottomToFormBottom;
            }

            EveScannerConfig.Instance.ShowExtra = shipContainer.Visible;
            EveScannerConfig.Instance.AppWidth = this.Width;
            EveScannerConfig.Instance.AppHeight = this.Height;

            this.MinimumSize = new Size(this.MinimumSize.Width, newHeight);
            this.Height = this.MinimumSize.Height;

            this.ResumeLayout();
        }

        /// <summary>
        /// Configures whether clipboard data is automatically captured.
        /// </summary>
        /// <param name="sender">Options -> Capture Clipboard</param>
        /// <param name="e">Not provided</param>
        private void CaptureClipboardOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EveScannerConfig.Instance.CaptureClipboard = !EveScannerConfig.Instance.CaptureClipboard;
            captureClipboardOnToolStripMenuItem.Checked = EveScannerConfig.Instance.CaptureClipboard;
        }

        /// <summary>
        /// Configures whether the window is always on top of all others
        /// </summary>
        /// <param name="sender">Options -> Toggle Always on Top</param>
        /// <param name="e">Not provided</param>
        private void ToggleAlwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            this.toggleAlwaysOnTopToolStripMenuItem.Checked = this.TopMost;
            EveScannerConfig.Instance.AlwaysOnTop = this.TopMost;
        }

        /// <summary>
        /// Clears all the inputs and labels back to their original values.
        /// </summary>
        /// <param name="sender">File -> Reset Data</param>
        /// <param name="e">Not provided</param>
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Clearing application values!");

            sellValueLabel.Text = "---";
            buyValueLabel.Text = "---";
            volumeValueLabel.Text = "---";
            stacksValueText.Text = "---";
            otherShipRadioButton.Checked = true;
            
            location1Radio.Checked = false;
            location2Radio.Checked = false;
            location3Radio.Checked = false;

            location1Text.Text = "Perimeter -> Urlen";
            location2Text.Text = "Ashab -> Madirmilire";
            location3Text.Text = "Hatakani -> Sivala";

            resultUrlTextBox.Text = string.Empty;
            scanText.Text = string.Empty;

            historyDropdown.Items.Clear();
            this.scans.Clear();
            scanValueLabel.Text = "0";
        }
        #endregion Menu Items

        #region Helper Functions
        /// <summary>
        /// Gets the selected radio button from a container.
        /// </summary>
        /// <param name="container">Container to find radio button in.</param>
        /// <returns>Selected radio button, or null if none found.</returns>
        private RadioButton GetCheckedRadioButton(Control container)
        {
            foreach (var control in container.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null)
                {
                    if (radio.Checked)
                    {
                        return radio;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the Ship Name from either the selected radio button, or, the textbox if "Other" is selected.
        /// </summary>
        /// <returns>Ship Name</returns>
        private string GetShipName()
        {
            string output = string.Empty;

            RadioButton radio = this.GetCheckedRadioButton(this.shipContainer);
            if (radio != null)
            {
                output = radio.Text;

                if (output == "Other")
                {
                    output = otherShipText.Text;
                }
            }

            return output;
        }

        /// <summary>
        /// Gets the text associated with the currently selected location radio button.
        /// </summary>
        /// <returns>Location to associate</returns>
        private string GetLocation()
        {
            string output = string.Empty;

            if (this.location1Radio.Checked)
            {
                return this.location1Text.Text;
            }

            if (this.location2Radio.Checked)
            {
                return this.location2Text.Text;
            }

            if (this.location3Radio.Checked)
            {
                return this.location3Text.Text;
            }

            return output;
        }

        /// <summary>
        /// Adds a provided result to the list and makes it the top entry in the dropdown
        /// </summary>
        /// <param name="result">Result to add</param>
        private void AddResultToList(ScanResult result)
        {
            this.scans.Add(result);
            this.historyDropdown.Items.Insert(0, result.ToString());
            this.historyDropdown.SelectedIndex = 0;
            this.scanValueLabel.Text = this.scans.Count.ToString();
            Logger.Result(result.ToString());
        }

        /// <summary>
        /// Parses the currently selected result and fills in form fields.
        /// </summary>
        private void ParseCurrentResult()
        {
            this.buyValueLabel.Text = ScanResult.GetIskString(this.result.BuyValue) + " ISK";
            this.sellValueLabel.Text = ScanResult.GetIskString(this.result.SellValue) + " ISK";
            this.stacksValueText.Text = this.result.Stacks.ToString();
            this.volumeValueLabel.Text = string.Format("{0:n}", this.result.Volume) + " m3";

            this.resultUrlTextBox.Text = this.result.EvepraisalUrl;

            this.scanText.Text = this.result.RawScan;

            if (this.result.ImageIndex == null)
            {
                if (this.pictureBox.Image != null)
                {
                    this.pictureBox.Image.Dispose();
                }

                this.pictureBox.Image = null;
            }
            else
            {
                this.pictureBox.Image = Bitmap.FromFile(EveScannerConfig.Instance.ImageGroups[this.result.ImageIndex.ToString()]);
            }
        }

        /// <summary>
        /// This method checks that the input text is in the form of a cargo scan.
        /// </summary>
        /// <param name="inputText">Supposed cargo scan.</param>
        /// <returns>True if the text is determined to be a cargo scan, false otherwise.</returns>
        private bool CheckTextFormat(string inputText)
        {
            string strRegex = @"^(?<line>\d+ [A-Za-z0-9,()'/\-]+( +[A-Za-z0-9,()'/\-]+)*)$";
            string output = Regex.Replace(inputText, strRegex, string.Empty, RegexOptions.Multiline | RegexOptions.ExplicitCapture).Replace("\n", string.Empty);
            if (string.IsNullOrEmpty(output))
            {
                return true;
            }

            return false;
        }
        #endregion Helper Functions
    }
}
