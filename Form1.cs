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
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    using EveScanner.Interfaces;

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
        private List<IScanResult> scans = new List<IScanResult>();

        /// <summary>
        /// Holds the current result being parsed.
        /// </summary>
        private IScanResult result = null;

        /// <summary>
        /// Holds a value indicating if we're running on windows, which is necessary for clipboard setup/teardown.
        /// </summary>
        private bool runningOnWindows = true;

        /// <summary>
        /// Holds a value indicating if the clipboard event has fired yet.
        /// </summary>
        private bool firstFire = true;

        /// <summary>
        /// Holds the last clipboard copy to prevent copying twice.
        /// </summary>
        private string lastCopy = string.Empty;
        #endregion Private Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class. This method
        /// initializes the component, and moves it to the last saved location.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();

            if (ConfigHelper.Instance.WindowPositionX == -1 && ConfigHelper.Instance.WindowPositionY == -1)
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(ConfigHelper.Instance.WindowPositionX, ConfigHelper.Instance.WindowPositionY);
            }
        }
        #endregion Constructors

        #region Form Events
        /// <summary>
        /// This processes window messages such as clipboard events.
        /// </summary>
        /// <param name="m">Window Message</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (!ConfigHelper.Instance.CaptureClipboard)
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

                    if (data == this.lastCopy)
                    {
                        Logger.Debug("Captured same data twice, skipping.");
                        return;
                    }

                    this.lastCopy = data;

                    Logger.Debug("Captured scan {0}", data);
                    if (this.submitANYClipboardDataToolStripMenuItem.Checked || this.CheckTextFormat(data) || data.StartsWith("http"))
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
                this.clipboardViewerNext = NativeMethods.SetClipboardViewer(this.Handle);      // Adds our form to the chain of clipboard viewers.
            }

            this.captureClipboardOnToolStripMenuItem.Checked = ConfigHelper.Instance.CaptureClipboard;
            this.toggleAlwaysOnTopToolStripMenuItem.Checked = ConfigHelper.Instance.AlwaysOnTop;
            if (ConfigHelper.Instance.AlwaysOnTop)
            {
                this.TopMost = true;
            }

            if (!ConfigHelper.Instance.ShowExtra)
            {
                this.ShowHideExtraOptionsToolStripMenuItem_Click(null, EventArgs.Empty);
            }

            this.Width = ConfigHelper.Instance.AppWidth;
            this.Height = ConfigHelper.Instance.AppHeight;

            for (int i = 0; i < this.loggingToolStripMenuItem.DropDownItems.Count; i++)
            {
                ToolStripItem c = this.loggingToolStripMenuItem.DropDownItems[i];
                if ((string)c.Tag == ConfigHelper.Instance.DebugLevel)
                {
                    this.DebugLevelStripMenu_Click(c, EventArgs.Empty);
                }
            }

            for (int i = 0; i < this.scanSourceToolStripMenuItem.DropDownItems.Count; i++)
            {
                ToolStripItem c = this.scanSourceToolStripMenuItem.DropDownItems[i];
                if ((string)c.Tag == ConfigHelper.Instance.ScanSource)
                {
                    this.ScanSourceStripMenu_Click(c, EventArgs.Empty);
                }
            }

            this.shipTypeDropdown.Items.Clear();
            foreach (string desc in ConfigHelper.Instance.ShipTypes.Keys)
            {
                this.shipTypeDropdown.Items.Add(desc);
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
                NativeMethods.ChangeClipboardChain(this.Handle, this.clipboardViewerNext);        // Removes our from the chain of clipboard viewers when the form closes.
            }

            Logger.Debug("Saving Config");
            ConfigHelper.Instance.Save();
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

            try
            {
                IScanResult iresult = null;

                if (scanText.Text.StartsWith("http://evepraisal.com/e/"))
                {
                    Evepraisal ep = new Evepraisal();
                    iresult = ep.GetAppraisalFromUrl(this.scanText.Text);
                }
                else if (scanText.Text.StartsWith("https://goonpraisal.apps.goonswarm.org/e/"))
                {
                    Evepraisal ep = new Evepraisal("goonpraisal.apps.goonswarm.org", true);
                    iresult = ep.GetAppraisalFromUrl(this.scanText.Text);
                }
                else if (this.submitANYClipboardDataToolStripMenuItem.Checked || this.CheckTextFormat(scanText.Text))
                {
                    if (evepraisalToolStripMenuItem.Checked)
                    {
                        Evepraisal ep = new Evepraisal();
                        iresult = ep.GetAppraisalFromScan(this.scanText.Text);
                    }

                    if (goonmetricsToolStripMenuItem.Checked)
                    {
                        Evepraisal ep = new Evepraisal("goonpraisal.apps.goonswarm.org", true);
                        iresult = ep.GetAppraisalFromScan(this.scanText.Text);
                    }
                }
                else
                {
                    return;
                }

                if (iresult == null)
                {
                    return;
                }

                this.result = iresult;
                this.scanText.Text = this.result.RawScan;
                this.AddResultToList(this.result);
                this.ParseCurrentResult();

                this.location1Radio.Checked = true;
                this.location1Radio.Checked = false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }

            // If we're minimized, fix that.
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
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

            sb.Append(this.result.ToString());

            bool prevClipboard = ConfigHelper.Instance.CaptureClipboard;

            ConfigHelper.Instance.CaptureClipboard = false;

            Clipboard.SetText(sb.ToString());

            ConfigHelper.Instance.CaptureClipboard = prevClipboard;
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
        /// This calls the click routine and updates locations on the result object.
        /// </summary>
        /// <param name="sender">Location Panel Radio Buttons</param>
        /// <param name="e">None provided</param>
        private void LocationButton_Click(object sender, EventArgs e)
        {
            this.RadioButton_Click(sender, e);

            if (this.result != null)
            {
                this.result.Location = this.GetLocation();
            }

            this.UpdateDropdown();
        }

        /// <summary>
        /// This calls the click routine and updates ship type on the result object.
        /// </summary>
        /// <param name="sender">Ship Panel Radio Buttons</param>
        /// <param name="e">None provided</param>
        private void ShipButton_Click(object sender, EventArgs e)
        {
            this.RadioButton_Click(sender, e);

            if (this.result != null)
            {
                this.result.ShipType = this.GetShipName();
            }

            this.UpdateDropdown();
        }

        /// <summary>
        /// This clears the "checked" state when clicking a radio button while holding shift or control.
        /// </summary>
        /// <param name="sender">Many radio button controls</param>
        /// <param name="e">Not provided</param>
        private void RadioButton_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) != Keys.None)
            {
                RadioButton btn = sender as RadioButton;
                if (btn != null)
                {
                    btn.Checked = false;
                }
            }
        }

        /// <summary>
        /// Updates the character name into the results object.
        /// </summary>
        /// <param name="sender">Character Name text field</param>
        /// <param name="e">Not provided.</param>
        private void characterNameText_Leave(object sender, EventArgs e)
        {
            if (this.result == null)
            {
                return;
            }

            this.result.CharacterName = this.characterNameText.Text;
        }

        /// <summary>
        /// Updates the Ship Type Dropdown in the Results Object
        /// </summary>
        /// <param name="sender">Ship Type Dropdown</param>
        /// <param name="e">Not provided</param>
        private void shipTypeDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.result == null)
            {
                return;
            }

            this.result.ShipType = this.shipTypeDropdown.Text;
        }

        /// <summary>
        /// Updates the Fit Info text in the Results Object
        /// </summary>
        /// <param name="sender">Fit Info Box</param>
        /// <param name="e">Not provided</param>
        private void fitInfoText_Leave(object sender, EventArgs e)
        {
            if (this.result == null)
            {
                return;
            }

            this.result.FitInfo = this.fitInfoText.Text;
        }

        /// <summary>
        /// Updates the Notes in the results object.
        /// </summary>
        /// <param name="sender">Notes box</param>
        /// <param name="e">Not provided</param>
        private void notesText_Leave(object sender, EventArgs e)
        {
            if (this.result == null)
            {
                return;
            }

            this.result.Notes = this.notesText.Text;
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

            if (infoContainer.Visible)
            {
                infoContainer.Visible = false;
                locationContainer.Visible = false;
                scanContainer.Visible = false;

                newHeight = infoContainer.Location.Y + resultsContainer.Height + containerBottomToFormBottom;
            }
            else
            {
                infoContainer.Visible = true;
                locationContainer.Visible = true;
                scanContainer.Visible = true;

                int containerBottomToContainerTop = locationContainer.Top - (infoContainer.Location.Y + infoContainer.Size.Height);
                newHeight = infoContainer.Location.Y + infoContainer.Height + locationContainer.Height + (2 * containerBottomToContainerTop) + resultsContainer.Height + containerBottomToFormBottom;
            }

            ConfigHelper.Instance.ShowExtra = scanContainer.Visible;
            ConfigHelper.Instance.AppWidth = this.Width;
            ConfigHelper.Instance.AppHeight = this.Height;

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
            ConfigHelper.Instance.CaptureClipboard = !ConfigHelper.Instance.CaptureClipboard;
            captureClipboardOnToolStripMenuItem.Checked = ConfigHelper.Instance.CaptureClipboard;
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
            ConfigHelper.Instance.AlwaysOnTop = this.TopMost;
        }

        /// <summary>
        /// Indicates that ANY clipboard text captured should be submitted to the appraisal.
        /// </summary>
        /// <param name="sender">Options -> Submit ANY Clipboard Data</param>
        /// <param name="e">Not provided.</param>
        private void submitANYClipboardDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.submitANYClipboardDataToolStripMenuItem.Checked = !this.submitANYClipboardDataToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Clears all the inputs and labels back to their original values.
        /// </summary>
        /// <param name="sender">File -> Reset Data</param>
        /// <param name="e">Not provided</param>
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Clearing application values!");

            this.sellValueLabel.Text = "---";
            this.buyValueLabel.Text = "---";
            this.volumeValueLabel.Text = "---";
            this.stacksValueText.Text = "---";

            this.location1Radio.Checked = false;
            this.location2Radio.Checked = false;
            this.location3Radio.Checked = false;

            this.location1Text.Text = "Perimeter -> Urlen";
            this.location2Text.Text = "Ashab -> Madirmilire";
            this.location3Text.Text = "Hatakani -> Sivala";

            this.resultUrlTextBox.Text = string.Empty;
            this.scanText.Text = string.Empty;

            this.historyDropdown.Items.Clear();
            this.scans.Clear();
            this.scanValueLabel.Text = "0";
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

            ConfigHelper.Instance.DebugLevel = (string)tsmi.Tag;
        }

        /// <summary>
        /// This sets up the checkmarks properly when clicking an item on the scan source menu.
        /// </summary>
        /// <param name="sender">Options -> Debug</param>
        /// <param name="e">Not provided.</param>
        private void ScanSourceStripMenu_Click(object sender, EventArgs e)
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

            RadioButton radio = this.GetCheckedRadioButton(this.infoContainer);
            if (radio != null)
            {
                output = radio.Text;

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
        /// <param name="scanResult">Result to add</param>
        private void AddResultToList(IScanResult scanResult)
        {
            IScanResult result = this.scans.Where(x => x.AppraisalUrl == scanResult.AppraisalUrl).FirstOrDefault();

            if (result == null)
            {
                this.scans.Add(scanResult);
                this.historyDropdown.Items.Insert(0, scanResult.ToString());
                this.historyDropdown.SelectedIndex = 0;
                this.scanValueLabel.Text = this.scans.Count.ToString();
                Logger.Result(scanResult.ToString());
            }
            else
            {
                int index = this.scans.IndexOf(result);

                this.historyDropdown.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Parses the currently selected result and fills in form fields.
        /// </summary>
        private void ParseCurrentResult()
        {
            // Top Labels
            this.buyValueLabel.Text = ScanResult.GetISKString(this.result.BuyValue) + " ISK";
            this.sellValueLabel.Text = ScanResult.GetISKString(this.result.SellValue) + " ISK";
            this.stacksValueText.Text = this.result.Stacks.ToString();
            this.volumeValueLabel.Text = string.Format("{0:n}", this.result.Volume) + " m3";

            // Get URL
            this.resultUrlTextBox.Text = this.result.AppraisalUrl;

            // Get Scan from Object
            this.scanText.Text = this.result.RawScan;

            // Construct / Restore Image
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
                this.ConstructAndDisplayImages(this.result.ImageIndex);
            }

            // Restore Character Name
            this.characterNameText.Text = string.Empty;

            if (!string.IsNullOrEmpty(this.result.CharacterName))
            {
                this.characterNameText.Text = this.result.CharacterName;
            }

            // Restore Ship Type
            this.shipTypeDropdown.Text = string.Empty;

            if (!string.IsNullOrEmpty(this.result.ShipType))
            {
                int index = this.shipTypeDropdown.Items.IndexOf(this.result.ShipType);

                this.shipTypeDropdown.SuspendLayout();
                if (index > -1)
                {
                    this.shipTypeDropdown.SelectedIndex = index;
                    this.shipTypeDropdown.Text = this.result.ShipType;
                }
                else
                {
                    this.shipTypeDropdown.Text = this.result.ShipType;
                }
                this.shipTypeDropdown.ResumeLayout();
            }

            // Restore Fit Into
            this.fitInfoText.Text = string.Empty;

            if (!string.IsNullOrEmpty(this.result.FitInfo))
            {
                this.fitInfoText.Text = this.result.FitInfo;
            }

            // Restore Notes
            this.notesText.Text = string.Empty;

            if (!string.IsNullOrEmpty(this.result.Notes))
            {
                this.notesText.Text = this.result.Notes;
            }

            // Restore Location
            if (!string.IsNullOrEmpty(this.result.Location))
            {
                if (this.result.Location == location1Text.Text)
                {
                    location1Radio.Checked = true;
                }
                else
                {
                    location1Radio.Checked = false;
                }

                if (this.result.Location == location2Text.Text)
                {
                    location2Radio.Checked = true;
                }
                else
                {
                    location2Radio.Checked = false;
                }

                if (this.result.Location == location3Text.Text)
                {
                    location3Radio.Checked = true;
                }
                else
                {
                    location3Radio.Checked = false;
                }
            }
        }

        /// <summary>
        /// Constructs a composite image and loads it into the image box.
        /// </summary>
        /// <param name="imageList">List of image index</param>
        private void ConstructAndDisplayImages(IEnumerable<int> imageList)
        {
            if (imageList.Count() == 0)
            {
                return;
            }

            try
            {
                string[] imageNames = imageList.OrderBy(x => x).Select(x => ConfigHelper.Instance.ImageGroups[x - 1]).ToArray();

                this.pictureBox.Image = ImageCombiner.CombineImages(this.pictureBox.Width, this.pictureBox.Height, imageNames);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }

        /// <summary>
        /// This method checks that the input text is in the form of a scan we recognize.
        /// </summary>
        /// <param name="inputText">Supposed cargo scan.</param>
        /// <returns>True if the text is determined to be a cargo scan, false otherwise.</returns>
        private bool CheckTextFormat(string inputText)
        {
            if (this.CheckForCargoScan(inputText))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check every line. If one doesn't match the Cargo Scan criteria, return false.
        /// </summary>
        /// <param name="inputText">Scan data</param>
        /// <returns>True if we think this is a cargo scan, false otherwise.</returns>
        private bool CheckForCargoScan(string inputText)
        {
            return Validators.CheckForCargoScan(inputText);
        }

        /// <summary>
        /// Updates the dropdown text with the current result data (updated if you changed ship type/location)
        /// </summary>
        private void UpdateDropdown()
        {
            if (this.result == null)
            {
                return;
            }

            int selectedIndex = this.historyDropdown.SelectedIndex;
            this.historyDropdown.Items.RemoveAt(selectedIndex);
            this.historyDropdown.Items.Insert(selectedIndex, this.result.ToString());
            this.historyDropdown.SelectedIndex = selectedIndex;
        }
        #endregion Helper Functions
    }
}
