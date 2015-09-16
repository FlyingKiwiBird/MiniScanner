//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Form1.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.UI
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using EveScanner.Core;
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
        /// Holds the current result being parsed.
        /// </summary>
        private IScanResult result = null;

        /// <summary>
        /// Holds result history. Replaces scans.
        /// </summary>
        private IScanHistory history = null;

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

            this.history = ConfigHelper.GetImplementation<IScanHistory>();
        }
        #endregion Constructors

        #region Helper Functions
        /// <summary>
        /// Changes the colors of a control and all controls below to a darker color scheme.
        /// </summary>
        /// <param name="baseControl">Outer control</param>
        public void ChangeFormColorsNightMode(Control baseControl)
        {
            if (baseControl == null)
            {
                return;
            }

            Color foreground = Color.DarkGray;
            Color background = Color.Black;

            baseControl.ForeColor = foreground;
            baseControl.BackColor = background;

            Button b = baseControl as Button;
            if (b != null)
            {
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 1;
            }

            foreach (Control x in baseControl.Controls)
            {
                this.ChangeFormColorsNightMode(x);
            }
        }

        /// <summary>
        /// Changes the colors of a control and all controls below to a darker color scheme.
        /// </summary>
        /// <param name="baseControl">Outer control</param>
        public void ChangeFormColorsDayMode(Control baseControl)
        {
            if (baseControl == null)
            {
                return;
            }

            Color foreground = SystemColors.ControlText;
            Color background = SystemColors.Control;

            baseControl.ForeColor = foreground;
            baseControl.BackColor = background;

            Button b = baseControl as Button;
            if (b != null)
            {
                b.FlatStyle = FlatStyle.Standard;
                b.FlatAppearance.BorderSize = 1;
            }

            TextBox t = baseControl as TextBox;
            if (t != null)
            {
                t.BackColor = SystemColors.Window;
            }

            ComboBox cb = baseControl as ComboBox;
            if (cb != null)
            {
                cb.BackColor = SystemColors.Window;
            }

            foreach (Control x in baseControl.Controls)
            {
                this.ChangeFormColorsDayMode(x);
            }
        }

        /// <summary>
        /// Updates the Ship Type Dropdown if provided by another form.
        /// </summary>
        /// <param name="shipValue">Ship String key</param>
        public void UpdateShipType(string shipValue)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => this.UpdateShipType(shipValue)));
            }
            else
            {
                foreach (string s in ConfigHelper.Instance.ShipTypes.AllKeys)
                {
                    if (ConfigHelper.Instance.ShipTypes[s] == shipValue)
                    {
                        int index = this.shipTypeDropdown.Items.IndexOf(s);

                        if (index > -1)
                        {
                            this.shipTypeDropdown.SelectedIndex = index;
                            this.shipTypeDropdown.Text = s;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates the Fit String box.
        /// </summary>
        /// <param name="newFitValue">New value for Fit String</param>
        public void UpdateFitType(string newFitValue)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => this.UpdateFitType(newFitValue)));
            }
            else
            {
                this.fitInfoText.Text = newFitValue;

                this.FitInfoText_Leave(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Injects a scan from an external object into the application (like the history form)
        /// </summary>
        /// <param name="scanResult">Scan to inject</param>
        public void InjectExternalScan(IScanResult scanResult)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => this.InjectExternalScan(scanResult)));
            }
            else
            {
                this.AddResultToList(scanResult);
            }
        }

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

            // This is the shittiest way to do drag on click, but, I don't rely on more
            // P/Invoke messaging to do it, so, it's what I'm going to use.
            if (m.Msg == 0x84)
            {
                if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
                {
                    if ((int)m.Result == 0x01)
                    {
                        m.Result = (IntPtr)0x02;
                        return;
                    }
                }
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
                    if (this.submitANYClipboardDataToolStripMenuItem.Checked || this.CheckTextFormat(data) || data.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    {
                        Logger.Scan("Captured scan {0}", data);
                        this.scanText.Text = data;
                        this.SubmitRequestButton_Click(this.submitRequestButton, EventArgs.Empty);
                    }
                }
            }
        }

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
            IScanResult resultToAdd = this.historyDropdown.Items.Cast<IScanResult>().Where(x => x.Id == scanResult.Id).FirstOrDefault();

            if (resultToAdd == null)
            {
                this.history.AddScan(scanResult);
                this.historyDropdown.Items.Insert(0, scanResult);
                this.historyDropdown.SelectedIndex = 0;
                this.scanValueLabel.Text = this.historyDropdown.Items.Count.ToString(CultureInfo.CurrentCulture);
                Logger.Result(scanResult.ToString());
            }
            else
            {
                int index = this.historyDropdown.Items.IndexOf(resultToAdd);

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
            this.stacksValueText.Text = this.result.Stacks.ToString(CultureInfo.CurrentCulture);
            this.volumeValueLabel.Text = string.Format(CultureInfo.CurrentCulture, "{0:n}", this.result.Volume) + " m3";

            // Get URL
            this.resultUrlTextBox.Text = this.result.AppraisalUrl;

            // Get Scan from Object
            this.scanText.Text = this.result.RawScan;

            // Construct / Restore Image
            if (this.pictureBox.Image != null)
            {
                this.pictureBox.Image.Dispose();
                this.pictureBox.Image = null;
            }

            if (this.result.ImageIndex != null)
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
            bool hasLocation = !string.IsNullOrEmpty(this.result.Location);

            if (hasLocation && this.result.Location == this.location1Text.Text)
            {
                this.location1Radio.Checked = true;
            }
            else
            {
                this.location1Radio.Checked = false;
            }

            if (hasLocation && this.result.Location == this.location2Text.Text)
            {
                this.location2Radio.Checked = true;
            }
            else
            {
                this.location2Radio.Checked = false;
            }

            if (hasLocation && this.result.Location == this.location3Text.Text)
            {
                this.location3Radio.Checked = true;
            }
            else
            {
                this.location3Radio.Checked = false;
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
                string[] imageNames = imageList.OrderBy(x => x).Select(x => ConfigHelper.Instance.ImageGroups[x.ToString(CultureInfo.InvariantCulture)]).ToArray();

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
            this.historyDropdown.Items.Insert(selectedIndex, this.result);
            this.historyDropdown.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// Fixes the window height based on the controls on the form.
        /// </summary>
        private void FixWindowHeight()
        {
            int newHeight = 0;
            int containerBottomToFormBottom = this.Height - (this.resultsContainer.Location.Y + this.resultsContainer.Size.Height);

            if (!this.infoContainer.Visible)
            {
                newHeight = this.infoContainer.Location.Y + this.resultsContainer.Height + containerBottomToFormBottom;

                this.infoContainer.Tag = this.infoContainer.Height;
            }
            else
            {
                int containerBottomToContainerTop = this.resultsContainer.Top - (this.locationContainer.Location.Y + this.locationContainer.Size.Height);
                newHeight = this.infoContainer.Location.Y + this.infoContainer.Height + this.locationContainer.Height + (2 * containerBottomToContainerTop) + this.resultsContainer.Height + containerBottomToFormBottom;

                newHeight = newHeight + (int)this.infoContainer.Tag;
            }

            this.MinimumSize = new Size(this.MinimumSize.Width, newHeight);
            this.Height = this.MinimumSize.Height;
        }
        #endregion Helper Functions

        #region Form Events
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

            this.keepLocationBetweenScansToolStripMenuItem.Checked = ConfigHelper.Instance.KeepLocation;

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
            if (string.IsNullOrEmpty(this.scanText.Text))
            {
                return;
            }

            try
            {
                IScanResult iresult = null;

                if (this.scanText.Text.StartsWith("http://evepraisal.com/e/", StringComparison.OrdinalIgnoreCase))
                {
                    Evepraisal ep = new Evepraisal();
                    string url = this.scanText.Text;
                    url = url.IndexOf(' ') < 0 ? url : url.Substring(0, url.IndexOf(' '));
                    url = url.IndexOf('\r') < 0 ? url : url.Substring(0, url.IndexOf('\r'));
                    url = url.IndexOf('\n') < 0 ? url : url.Substring(0, url.IndexOf('\n'));
                    iresult = ep.GetAppraisalFromUrl(url);
                }
                else if (this.scanText.Text.StartsWith("https://goonpraisal.apps.goonswarm.org/e/", StringComparison.OrdinalIgnoreCase))
                {
                    Evepraisal ep = new Evepraisal("goonpraisal.apps.goonswarm.org", true);
                    string url = this.scanText.Text;
                    url = url.IndexOf(' ') < 0 ? url : url.Substring(0, url.IndexOf(' '));
                    url = url.IndexOf('\r') < 0 ? url : url.Substring(0, url.IndexOf('\r'));
                    url = url.IndexOf('\n') < 0 ? url : url.Substring(0, url.IndexOf('\n'));
                    iresult = ep.GetAppraisalFromUrl(url);
                }
                else if (this.submitANYClipboardDataToolStripMenuItem.Checked || this.CheckTextFormat(this.scanText.Text))
                {
                    if (this.evepraisalToolStripMenuItem.Checked)
                    {
                        Evepraisal ep = new Evepraisal();
                        iresult = ep.GetAppraisalFromScan(this.scanText.Text);
                    }

                    if (this.goonmetricsToolStripMenuItem.Checked)
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

                if (ConfigHelper.Instance.KeepLocation && this.result != null && !string.IsNullOrEmpty(this.result.Location))
                {
                    iresult.Location = this.result.Location;
                }

                this.result = iresult;
                this.scanText.Text = this.result.RawScan;
                this.AddResultToList(this.result);
                this.ParseCurrentResult();
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
                this.TopMost = false;
                this.Activate();
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

            if (this.historyDropdown.Items.Count == 0)
            {
                return;
            }

            this.result = (IScanResult)this.historyDropdown.SelectedItem;

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
        private void CharacterNameText_Leave(object sender, EventArgs e)
        {
            if (this.result == null)
            {
                return;
            }

            this.result.CharacterName = this.characterNameText.Text;
            this.UpdateDropdown();
        }

        /// <summary>
        /// Updates the Ship Type Dropdown in the Results Object
        /// </summary>
        /// <param name="sender">Ship Type Dropdown</param>
        /// <param name="e">Not provided</param>
        private void ShipTypeDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.result == null)
            {
                return;
            }

            this.result.ShipType = this.shipTypeDropdown.Text;

            this.historyDropdown.SelectedItem = this.result;
            this.UpdateDropdown();
        }

        /// <summary>
        /// Updates the Fit Info text in the Results Object
        /// </summary>
        /// <param name="sender">Fit Info Box</param>
        /// <param name="e">Not provided</param>
        private void FitInfoText_Leave(object sender, EventArgs e)
        {
            if (this.result == null)
            {
                return;
            }

            this.result.FitInfo = this.fitInfoText.Text;
            this.UpdateDropdown();
        }

        /// <summary>
        /// Updates the Notes in the results object.
        /// </summary>
        /// <param name="sender">Notes box</param>
        /// <param name="e">Not provided</param>
        private void NotesText_Leave(object sender, EventArgs e)
        {
            if (this.result == null)
            {
                return;
            }

            this.result.Notes = this.notesText.Text;
            this.UpdateDropdown();
        }

        /// <summary>
        /// Opens the Ship Picker form as a dialog box.
        /// </summary>
        /// <param name="sender">... button next to ship dropdown.</param>
        /// <param name="e">Not provided.</param>
        private void ShipTypePickerButton_Click(object sender, EventArgs e)
        {
            using (ShipPicker sp = new ShipPicker())
            {
                sp.CallingForm = this;

                sp.ShowDialog(this);
            }
        }

        /// <summary>
        /// Opens the Fit Picker form as a dialog box.
        /// </summary>
        /// <param name="sender">... button next to fit textbox.</param>
        /// <param name="e">Not provided.</param>
        private void FitInfoPickerButton_Click(object sender, EventArgs e)
        {
            using (FitPicker fp = new FitPicker())
            {
                fp.CallingForm = this;

                fp.ShowDialog(this);
            }
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

            if (this.scanContainer.Visible)
            {
                this.infoContainer.Visible = false;
                this.locationContainer.Visible = false;
                this.scanContainer.Visible = false;
            }
            else
            {
                this.infoContainer.Visible = true;
                this.locationContainer.Visible = true;
                this.scanContainer.Visible = true;
            }

            this.FixWindowHeight();

            ConfigHelper.Instance.ShowExtra = this.scanContainer.Visible;
            ConfigHelper.Instance.AppWidth = this.Width;
            ConfigHelper.Instance.AppHeight = this.Height;

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
            this.captureClipboardOnToolStripMenuItem.Checked = ConfigHelper.Instance.CaptureClipboard;
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
        private void SubmitANYClipboardDataToolStripMenuItem_Click(object sender, EventArgs e)
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
            if (sender != null && sender == this.clearToolStripMenuItem)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to clear current data?", "Reset Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
                if (dialogResult == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

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
            this.characterNameText.Text = string.Empty;
            this.notesText.Text = string.Empty;
            this.shipTypeDropdown.Text = string.Empty;
            this.fitInfoText.Text = string.Empty;

            if (this.pictureBox.Image != null)
            {
                this.pictureBox.Image.Dispose();
                this.pictureBox.Image = null;
            }

            this.historyDropdown.Items.Clear();
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

        /// <summary>
        /// Displays the "About" message.
        /// </summary>
        /// <param name="sender">Help -> About</param>
        /// <param name="e">Not provided.</param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = @"©2015 Viktorie Lucilla <viktorie@rifter.ca>

Comments/Suggestions/Complaints can be posted in the appropriate 
thread on the Goonfleet Forums or sent to me via Jabber.
";

            MessageBox.Show(message, "About", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
        }

        /// <summary>
        /// Shows the license message (from License.txt)
        /// </summary>
        /// <param name="sender">Help -> License</param>
        /// <param name="e">Not provided.</param>
        private void LicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = File.ReadAllText("license.txt");

            MessageBox.Show(message, "License", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
        }

        /// <summary>
        /// Sends the user to the source code.
        /// </summary>
        /// <param name="sender">Help -> Source Code</param>
        /// <param name="e">Not Provided.</param>
        private void SourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://bitbucket.org/viktorielucilla/evescanner-net4");
        }

        /// <summary>
        /// Sends the user to the issue tracker.
        /// </summary>
        /// <param name="sender">Help -> Source Code</param>
        /// <param name="e">Not Provided.</param>
        private void IssueTrackerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://bitbucket.org/viktorielucilla/evescanner-net4/issues");
        }

        /// <summary>
        /// Sets up some example scan results so someone can visually inspect and play with things.
        /// </summary>
        /// <param name="sender">Options -> Debug -> Show Example Results</param>
        /// <param name="e">Not provided.</param>
        private void AddExampleResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ClearToolStripMenuItem_Click(null, EventArgs.Empty);

            ScanResult r = new ScanResult(Guid.Empty, DateTime.Now, "1 Dummy Item", 3000000000000, 4123456789012, 1, 1, "http://goonfleet.com/?1", new int[] { 1 }) { CharacterName = "T2 BPO", ShipType = "Providence - Freighter - Amarr", Notes = "Triggers T2 BPO Image", Location = "Perimeter -> Urlen" };
            this.AddResultToList(r);

            ScanResult s = new ScanResult(Guid.Empty, DateTime.Now, string.Empty, 300000000000, 412345678901, 3, 4, "http://goonfleet.com/?2", new int[] { 2 }) { CharacterName = "Plastic Wrap", ShipType = "Charon - Freighter - Caldari", Notes = "Triggers Wrap Image" };
            this.AddResultToList(s);

            ScanResult t = new ScanResult(Guid.Empty, DateTime.Now, string.Empty, 30000000000, 41234567890, 3, 4, "http://goonfleet.com/?3", new int[] { 3 }) { CharacterName = "Container", ShipType = "Obelisk - Freighter - Gallente", Notes = "Triggers Container Image" };
            this.AddResultToList(t);

            ScanResult u = new ScanResult(Guid.Empty, DateTime.Now, string.Empty, 3000000000, 4123456789, 3, 4, "http://goonfleet.com/?4", new int[] { 4 }) { CharacterName = "Isotopes", ShipType = "Fenrir - Freighter - Minmatar", Notes = "Triggers Isotope Image", Location = "Ashab -> Madirmilire" };
            this.AddResultToList(u);

            ScanResult v = new ScanResult(Guid.Empty, DateTime.Now, string.Empty, 300000000, 412345678, 3, 4, "http://goonfleet.com/?5", new int[] { 5 }) { CharacterName = "Valuable BPO", ShipType = "Ark - Jump Freighter - Amarr - Helium", Notes = "Triggers $$$ BPO Image" };
            this.AddResultToList(v);

            ScanResult w = new ScanResult(Guid.Empty, DateTime.Now, string.Empty, 30000000, 41234567, 3, 4, "http://goonfleet.com/?99", new int[] { 99 }) { CharacterName = "Fedo", ShipType = "Rhea - Jump Freighter - Caldari - Nitrogen", Notes = "Triggers Fedo Image" };
            this.AddResultToList(w);

            ScanResult x = new ScanResult(Guid.Empty, DateTime.Now, string.Empty, 3000000, 4123456, 3, 4, "http://goonfleet.com/?23", new int[] { 2, 3 }) { CharacterName = "Mixed 1", ShipType = "Anshar - Jump Freighter - Gallente - Oxygen", Notes = "Triggers Wrap+Container Image", Location = "Hatakani -> Sivala" };
            this.AddResultToList(x);

            ScanResult y = new ScanResult(Guid.Empty, DateTime.Now, string.Empty, 300000, 412345, 3, 4, "http://goonfleet.com/?1234", new int[] { 1, 2, 3, 4 }) { CharacterName = "Mixed 2", ShipType = "Nomad - Jump Freighter - Minmatar - Hydrogen", Notes = "Triggers 4 Combined Image" };
            this.AddResultToList(y);

            ScanResult z = new ScanResult(Guid.Empty, DateTime.Now, string.Empty, 30000, 41234, 3, 4, "http://goonfleet.com/?1234599", new int[] { 1, 2, 3, 4, 5, 99 }) { CharacterName = "Mixed 3", ShipType = string.Empty, Notes = "Triggers 6 Combined Image" };
            this.AddResultToList(z);
        }

        /// <summary>
        /// Hides or restores borders on the form.
        /// </summary>
        /// <param name="sender">Options -> UI -> Hide Borders</param>
        /// <param name="e">Not provided.</param>
        private void HideBordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.Sizable)
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            }
            else
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            }

            this.hideBordersToolStripMenuItem.Checked = !this.hideBordersToolStripMenuItem.Checked;

            this.SuspendLayout();

            this.FixWindowHeight();

            this.ResumeLayout();
        }

        /// <summary>
        /// Makes the colors in the application dark and spooky.
        /// </summary>
        /// <param name="sender">Options -> UI -> Darker Theme</param>
        /// <param name="e">Not provided.</param>
        private void DarkerThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeFormColorsNightMode(this);
        }

        /// <summary>
        /// Makes the colors in the application light and airy.
        /// </summary>
        /// <param name="sender">Options -> UI -> Lighter Theme</param>
        /// <param name="e">Not provided.</param>
        private void LighterThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeFormColorsDayMode(this);
        }

        /// <summary>
        /// Adds an empty scan result to the application.
        /// </summary>
        /// <param name="sender">File -> New Empty Scan</param>
        /// <param name="e">Not provided.</param>
        private void NewEmptyScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanResult rx = new ScanResult(Guid.Empty, DateTime.Now, "1 Empty Cargohold", 0, 0, 0, 0, "http://goonfleet.com/?" + this.scanValueLabel.Text, null);
            this.AddResultToList(rx);
        }

        /// <summary>
        /// Toggles the keeping of locations between scans.
        /// </summary>
        /// <param name="sender">Options -> Keep Location Between Scans</param>
        /// <param name="e">Not provided.</param>
        private void KeepLocationBetweenScansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.keepLocationBetweenScansToolStripMenuItem.Checked = !this.keepLocationBetweenScansToolStripMenuItem.Checked;

            ConfigHelper.Instance.KeepLocation = this.keepLocationBetweenScansToolStripMenuItem.Checked;
        }
        
        /// <summary>
        /// Called when the History item on the menu bar is called. Shows the history form.
        /// </summary>
        /// <param name="sender">History menu item.</param>
        /// <param name="e">This parameter is not used.</param>
        private void HistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ScanHistory hi = new ScanHistory())
            {
                hi.CallingForm = this;

                hi.ShowDialog();
            }
        }
        #endregion Menu Items
    }
}
