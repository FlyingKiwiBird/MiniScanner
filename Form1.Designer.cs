﻿namespace EveScanner
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.otherShipText = new System.Windows.Forms.TextBox();
            this.otherShipRadioButton = new System.Windows.Forms.RadioButton();
            this.location1Text = new System.Windows.Forms.TextBox();
            this.location1Radio = new System.Windows.Forms.RadioButton();
            this.location2Radio = new System.Windows.Forms.RadioButton();
            this.location3Radio = new System.Windows.Forms.RadioButton();
            this.location2Text = new System.Windows.Forms.TextBox();
            this.location3Text = new System.Windows.Forms.TextBox();
            this.shipContainer = new System.Windows.Forms.GroupBox();
            this.locationContainer = new System.Windows.Forms.GroupBox();
            this.scanContainer = new System.Windows.Forms.GroupBox();
            this.submitRequestButton = new System.Windows.Forms.Button();
            this.scanText = new System.Windows.Forms.TextBox();
            this.resultsContainer = new System.Windows.Forms.GroupBox();
            this.historyDropdown = new System.Windows.Forms.ComboBox();
            this.scanValueLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.resultUrlTextBox = new System.Windows.Forms.TextBox();
            this.copySummaryButton = new System.Windows.Forms.Button();
            this.sellValueLabel = new System.Windows.Forms.Label();
            this.buyValueLabel = new System.Windows.Forms.Label();
            this.volumeValueLabel = new System.Windows.Forms.Label();
            this.stacksValueText = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideExtraOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.captureClipboardOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleAlwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scansAndResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.shipContainer.SuspendLayout();
            this.locationContainer.SuspendLayout();
            this.scanContainer.SuspendLayout();
            this.resultsContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sell:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 37);
            this.label2.TabIndex = 1;
            this.label2.Text = "Buy:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Volume:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Stacks:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(79, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.Text = "Providence";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(97, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "Charon";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(188, 19);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(60, 17);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.Text = "Obelisk";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(279, 19);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(51, 17);
            this.radioButton4.TabIndex = 5;
            this.radioButton4.Text = "Fenrir";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(6, 42);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(41, 17);
            this.radioButton5.TabIndex = 6;
            this.radioButton5.Text = "Ark";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(97, 42);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(51, 17);
            this.radioButton6.TabIndex = 7;
            this.radioButton6.Text = "Rhea";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(188, 42);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(58, 17);
            this.radioButton7.TabIndex = 8;
            this.radioButton7.Text = "Anshar";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new System.Drawing.Point(279, 42);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(59, 17);
            this.radioButton8.TabIndex = 9;
            this.radioButton8.Text = "Nomad";
            this.radioButton8.UseVisualStyleBackColor = true;
            this.radioButton8.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Location = new System.Drawing.Point(6, 65);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(48, 17);
            this.radioButton9.TabIndex = 10;
            this.radioButton9.Text = "Orca";
            this.radioButton9.UseVisualStyleBackColor = true;
            this.radioButton9.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Location = new System.Drawing.Point(97, 65);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(70, 17);
            this.radioButton10.TabIndex = 11;
            this.radioButton10.Text = "Bowhead";
            this.radioButton10.UseVisualStyleBackColor = true;
            this.radioButton10.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // otherShipText
            // 
            this.otherShipText.Location = new System.Drawing.Point(188, 88);
            this.otherShipText.Name = "otherShipText";
            this.otherShipText.Size = new System.Drawing.Size(150, 20);
            this.otherShipText.TabIndex = 1;
            // 
            // otherShipRadioButton
            // 
            this.otherShipRadioButton.AutoSize = true;
            this.otherShipRadioButton.Checked = true;
            this.otherShipRadioButton.Location = new System.Drawing.Point(188, 65);
            this.otherShipRadioButton.Name = "otherShipRadioButton";
            this.otherShipRadioButton.Size = new System.Drawing.Size(51, 17);
            this.otherShipRadioButton.TabIndex = 0;
            this.otherShipRadioButton.TabStop = true;
            this.otherShipRadioButton.Text = "Other";
            this.otherShipRadioButton.UseVisualStyleBackColor = true;
            this.otherShipRadioButton.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // location1Text
            // 
            this.location1Text.Location = new System.Drawing.Point(103, 19);
            this.location1Text.Name = "location1Text";
            this.location1Text.Size = new System.Drawing.Size(235, 20);
            this.location1Text.TabIndex = 15;
            this.location1Text.Text = "Perimeter -> Urlen";
            // 
            // location1Radio
            // 
            this.location1Radio.AutoSize = true;
            this.location1Radio.Location = new System.Drawing.Point(6, 20);
            this.location1Radio.Name = "location1Radio";
            this.location1Radio.Size = new System.Drawing.Size(75, 17);
            this.location1Radio.TabIndex = 12;
            this.location1Radio.TabStop = true;
            this.location1Radio.Text = "Location 1";
            this.location1Radio.UseVisualStyleBackColor = true;
            this.location1Radio.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // location2Radio
            // 
            this.location2Radio.AutoSize = true;
            this.location2Radio.Location = new System.Drawing.Point(6, 46);
            this.location2Radio.Name = "location2Radio";
            this.location2Radio.Size = new System.Drawing.Size(75, 17);
            this.location2Radio.TabIndex = 13;
            this.location2Radio.TabStop = true;
            this.location2Radio.Text = "Location 2";
            this.location2Radio.UseVisualStyleBackColor = true;
            this.location2Radio.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // location3Radio
            // 
            this.location3Radio.AutoSize = true;
            this.location3Radio.Location = new System.Drawing.Point(6, 72);
            this.location3Radio.Name = "location3Radio";
            this.location3Radio.Size = new System.Drawing.Size(75, 17);
            this.location3Radio.TabIndex = 14;
            this.location3Radio.TabStop = true;
            this.location3Radio.Text = "Location 3";
            this.location3Radio.UseVisualStyleBackColor = true;
            this.location3Radio.Click += new System.EventHandler(this.radioButton_Click);
            // 
            // location2Text
            // 
            this.location2Text.Location = new System.Drawing.Point(103, 45);
            this.location2Text.Name = "location2Text";
            this.location2Text.Size = new System.Drawing.Size(235, 20);
            this.location2Text.TabIndex = 16;
            this.location2Text.Text = "Ashab -> Madirmilire";
            // 
            // location3Text
            // 
            this.location3Text.Location = new System.Drawing.Point(103, 71);
            this.location3Text.Name = "location3Text";
            this.location3Text.Size = new System.Drawing.Size(235, 20);
            this.location3Text.TabIndex = 17;
            this.location3Text.Text = "Hatakani -> Sivala";
            // 
            // shipContainer
            // 
            this.shipContainer.Controls.Add(this.radioButton1);
            this.shipContainer.Controls.Add(this.radioButton2);
            this.shipContainer.Controls.Add(this.radioButton3);
            this.shipContainer.Controls.Add(this.radioButton4);
            this.shipContainer.Controls.Add(this.radioButton5);
            this.shipContainer.Controls.Add(this.radioButton6);
            this.shipContainer.Controls.Add(this.radioButton7);
            this.shipContainer.Controls.Add(this.radioButton8);
            this.shipContainer.Controls.Add(this.otherShipRadioButton);
            this.shipContainer.Controls.Add(this.radioButton9);
            this.shipContainer.Controls.Add(this.otherShipText);
            this.shipContainer.Controls.Add(this.radioButton10);
            this.shipContainer.Location = new System.Drawing.Point(21, 165);
            this.shipContainer.Name = "shipContainer";
            this.shipContainer.Size = new System.Drawing.Size(350, 120);
            this.shipContainer.TabIndex = 28;
            this.shipContainer.TabStop = false;
            this.shipContainer.Text = "Ship";
            // 
            // locationContainer
            // 
            this.locationContainer.Controls.Add(this.location1Radio);
            this.locationContainer.Controls.Add(this.location1Text);
            this.locationContainer.Controls.Add(this.location3Text);
            this.locationContainer.Controls.Add(this.location2Text);
            this.locationContainer.Controls.Add(this.location2Radio);
            this.locationContainer.Controls.Add(this.location3Radio);
            this.locationContainer.Location = new System.Drawing.Point(21, 291);
            this.locationContainer.Name = "locationContainer";
            this.locationContainer.Size = new System.Drawing.Size(350, 104);
            this.locationContainer.TabIndex = 29;
            this.locationContainer.TabStop = false;
            this.locationContainer.Text = "Location";
            // 
            // scanContainer
            // 
            this.scanContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scanContainer.Controls.Add(this.submitRequestButton);
            this.scanContainer.Controls.Add(this.scanText);
            this.scanContainer.Location = new System.Drawing.Point(377, 165);
            this.scanContainer.Name = "scanContainer";
            this.scanContainer.Size = new System.Drawing.Size(277, 230);
            this.scanContainer.TabIndex = 30;
            this.scanContainer.TabStop = false;
            this.scanContainer.Text = "Raw Scan Data";
            // 
            // submitRequestButton
            // 
            this.submitRequestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.submitRequestButton.Location = new System.Drawing.Point(171, 201);
            this.submitRequestButton.Name = "submitRequestButton";
            this.submitRequestButton.Size = new System.Drawing.Size(100, 23);
            this.submitRequestButton.TabIndex = 22;
            this.submitRequestButton.Text = "Manually Submit";
            this.submitRequestButton.UseVisualStyleBackColor = true;
            this.submitRequestButton.Click += new System.EventHandler(this.submitRequestButton_Click);
            // 
            // scanText
            // 
            this.scanText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scanText.Location = new System.Drawing.Point(6, 19);
            this.scanText.Multiline = true;
            this.scanText.Name = "scanText";
            this.scanText.Size = new System.Drawing.Size(265, 176);
            this.scanText.TabIndex = 21;
            // 
            // resultsContainer
            // 
            this.resultsContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsContainer.Controls.Add(this.historyDropdown);
            this.resultsContainer.Controls.Add(this.scanValueLabel);
            this.resultsContainer.Controls.Add(this.label5);
            this.resultsContainer.Controls.Add(this.resultUrlTextBox);
            this.resultsContainer.Controls.Add(this.copySummaryButton);
            this.resultsContainer.Location = new System.Drawing.Point(21, 401);
            this.resultsContainer.Name = "resultsContainer";
            this.resultsContainer.Size = new System.Drawing.Size(633, 75);
            this.resultsContainer.TabIndex = 31;
            this.resultsContainer.TabStop = false;
            this.resultsContainer.Text = "Results";
            // 
            // historyDropdown
            // 
            this.historyDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.historyDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.historyDropdown.FormattingEnabled = true;
            this.historyDropdown.Location = new System.Drawing.Point(6, 47);
            this.historyDropdown.Name = "historyDropdown";
            this.historyDropdown.Size = new System.Drawing.Size(462, 21);
            this.historyDropdown.TabIndex = 21;
            this.historyDropdown.SelectedIndexChanged += new System.EventHandler(this.historyDropdown_SelectedIndexChanged);
            // 
            // scanValueLabel
            // 
            this.scanValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scanValueLabel.Location = new System.Drawing.Point(527, 45);
            this.scanValueLabel.Name = "scanValueLabel";
            this.scanValueLabel.Size = new System.Drawing.Size(100, 23);
            this.scanValueLabel.TabIndex = 20;
            this.scanValueLabel.Text = "0";
            this.scanValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(481, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Scans:";
            // 
            // resultUrlTextBox
            // 
            this.resultUrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultUrlTextBox.Location = new System.Drawing.Point(173, 21);
            this.resultUrlTextBox.Name = "resultUrlTextBox";
            this.resultUrlTextBox.Size = new System.Drawing.Size(454, 20);
            this.resultUrlTextBox.TabIndex = 19;
            // 
            // copySummaryButton
            // 
            this.copySummaryButton.Location = new System.Drawing.Point(6, 19);
            this.copySummaryButton.Name = "copySummaryButton";
            this.copySummaryButton.Size = new System.Drawing.Size(161, 23);
            this.copySummaryButton.TabIndex = 18;
            this.copySummaryButton.Text = "Copy Summary to Clipboard";
            this.copySummaryButton.UseVisualStyleBackColor = true;
            this.copySummaryButton.Click += new System.EventHandler(this.copySummaryButton_Click);
            // 
            // sellValueLabel
            // 
            this.sellValueLabel.AutoSize = true;
            this.sellValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellValueLabel.Location = new System.Drawing.Point(125, 24);
            this.sellValueLabel.Name = "sellValueLabel";
            this.sellValueLabel.Size = new System.Drawing.Size(64, 51);
            this.sellValueLabel.TabIndex = 32;
            this.sellValueLabel.Text = "---";
            // 
            // buyValueLabel
            // 
            this.buyValueLabel.AutoSize = true;
            this.buyValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buyValueLabel.Location = new System.Drawing.Point(127, 75);
            this.buyValueLabel.Name = "buyValueLabel";
            this.buyValueLabel.Size = new System.Drawing.Size(47, 37);
            this.buyValueLabel.TabIndex = 33;
            this.buyValueLabel.Text = "---";
            // 
            // volumeValueLabel
            // 
            this.volumeValueLabel.AutoSize = true;
            this.volumeValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.volumeValueLabel.Location = new System.Drawing.Point(129, 112);
            this.volumeValueLabel.Name = "volumeValueLabel";
            this.volumeValueLabel.Size = new System.Drawing.Size(33, 25);
            this.volumeValueLabel.TabIndex = 34;
            this.volumeValueLabel.Text = "---";
            // 
            // stacksValueText
            // 
            this.stacksValueText.AutoSize = true;
            this.stacksValueText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stacksValueText.Location = new System.Drawing.Point(129, 137);
            this.stacksValueText.Name = "stacksValueText";
            this.stacksValueText.Size = new System.Drawing.Size(33, 25);
            this.stacksValueText.TabIndex = 35;
            this.stacksValueText.Text = "---";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.showHideExtraOptionsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(664, 24);
            this.menuStrip1.TabIndex = 36;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.clearToolStripMenuItem.Text = "&Reset Data";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // showHideExtraOptionsToolStripMenuItem
            // 
            this.showHideExtraOptionsToolStripMenuItem.Name = "showHideExtraOptionsToolStripMenuItem";
            this.showHideExtraOptionsToolStripMenuItem.Size = new System.Drawing.Size(150, 20);
            this.showHideExtraOptionsToolStripMenuItem.Text = "Show/Hide Extra &Buttons";
            this.showHideExtraOptionsToolStripMenuItem.Click += new System.EventHandler(this.showHideExtraOptionsToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.captureClipboardOnToolStripMenuItem,
            this.toggleAlwaysOnTopToolStripMenuItem,
            this.loggingToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // captureClipboardOnToolStripMenuItem
            // 
            this.captureClipboardOnToolStripMenuItem.Name = "captureClipboardOnToolStripMenuItem";
            this.captureClipboardOnToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.captureClipboardOnToolStripMenuItem.Text = "&Capture Clipboard";
            this.captureClipboardOnToolStripMenuItem.Click += new System.EventHandler(this.captureClipboardOnToolStripMenuItem_Click);
            // 
            // toggleAlwaysOnTopToolStripMenuItem
            // 
            this.toggleAlwaysOnTopToolStripMenuItem.Name = "toggleAlwaysOnTopToolStripMenuItem";
            this.toggleAlwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.toggleAlwaysOnTopToolStripMenuItem.Text = "&Toggle Always on Top";
            this.toggleAlwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.toggleAlwaysOnTopToolStripMenuItem_Click);
            // 
            // loggingToolStripMenuItem
            // 
            this.loggingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.resultsToolStripMenuItem,
            this.scansAndResultsToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.loggingToolStripMenuItem.Name = "loggingToolStripMenuItem";
            this.loggingToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.loggingToolStripMenuItem.Text = "&Logging";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Checked = true;
            this.noneToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.noneToolStripMenuItem.Tag = "none";
            this.noneToolStripMenuItem.Text = "&None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.debugLevelStripMenu_Click);
            // 
            // resultsToolStripMenuItem
            // 
            this.resultsToolStripMenuItem.Name = "resultsToolStripMenuItem";
            this.resultsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.resultsToolStripMenuItem.Tag = "results";
            this.resultsToolStripMenuItem.Text = "&Results";
            this.resultsToolStripMenuItem.Click += new System.EventHandler(this.debugLevelStripMenu_Click);
            // 
            // scansAndResultsToolStripMenuItem
            // 
            this.scansAndResultsToolStripMenuItem.Name = "scansAndResultsToolStripMenuItem";
            this.scansAndResultsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.scansAndResultsToolStripMenuItem.Tag = "scans";
            this.scansAndResultsToolStripMenuItem.Text = "&Scans and Results";
            this.scansAndResultsToolStripMenuItem.Click += new System.EventHandler(this.debugLevelStripMenu_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.debugToolStripMenuItem.Tag = "debug";
            this.debugToolStripMenuItem.Text = "&Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugLevelStripMenu_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(505, 24);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(149, 135);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 37;
            this.pictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 481);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.stacksValueText);
            this.Controls.Add(this.volumeValueLabel);
            this.Controls.Add(this.buyValueLabel);
            this.Controls.Add(this.sellValueLabel);
            this.Controls.Add(this.resultsContainer);
            this.Controls.Add(this.scanContainer);
            this.Controls.Add(this.locationContainer);
            this.Controls.Add(this.shipContainer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(590, 520);
            this.Name = "Form1";
            this.Text = "I LOVE SCANNING";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.shipContainer.ResumeLayout(false);
            this.shipContainer.PerformLayout();
            this.locationContainer.ResumeLayout(false);
            this.locationContainer.PerformLayout();
            this.scanContainer.ResumeLayout(false);
            this.scanContainer.PerformLayout();
            this.resultsContainer.ResumeLayout(false);
            this.resultsContainer.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.TextBox otherShipText;
        private System.Windows.Forms.RadioButton otherShipRadioButton;
        private System.Windows.Forms.TextBox location1Text;
        private System.Windows.Forms.RadioButton location1Radio;
        private System.Windows.Forms.RadioButton location2Radio;
        private System.Windows.Forms.RadioButton location3Radio;
        private System.Windows.Forms.TextBox location2Text;
        private System.Windows.Forms.TextBox location3Text;
        private System.Windows.Forms.GroupBox shipContainer;
        private System.Windows.Forms.GroupBox locationContainer;
        private System.Windows.Forms.GroupBox scanContainer;
        private System.Windows.Forms.TextBox scanText;
        private System.Windows.Forms.GroupBox resultsContainer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox resultUrlTextBox;
        private System.Windows.Forms.Button copySummaryButton;
        private System.Windows.Forms.Label sellValueLabel;
        private System.Windows.Forms.Label buyValueLabel;
        private System.Windows.Forms.Label volumeValueLabel;
        private System.Windows.Forms.Label stacksValueText;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideExtraOptionsToolStripMenuItem;
        private System.Windows.Forms.Button submitRequestButton;
        private System.Windows.Forms.ToolStripMenuItem toggleAlwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.Label scanValueLabel;
        private System.Windows.Forms.ComboBox historyDropdown;
        private System.Windows.Forms.ToolStripMenuItem captureClipboardOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scansAndResultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}

