﻿//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ScanResult.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using EveScanner.Interfaces;

    /// <summary>
    /// Provides a Scan Result implementation.
    /// </summary>
    public class ScanResult : IScanResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScanResult"/> class.
        /// </summary>
        /// <param name="rawScan">Raw scan submitted to the engine.</param>
        /// <param name="buyValue">Value of the scan from a "Buy" perspective.</param>
        /// <param name="sellValue">Value of the scan from a "Sell" perspective.</param>
        /// <param name="stacks">Number of stacks in the scan.</param>
        /// <param name="volume">Volume of items in the scan.</param>
        /// <param name="appraisalUrl">URL to the Appraisal</param>
        /// <param name="imageIndex">Image Index for the Appraisal</param>
        /// <param name="shipType">Ship type scanned</param>
        /// <param name="location">Location of the scan.</param>
        /// <param name="characterName">Character name scanned.</param>
        public ScanResult(string rawScan, decimal buyValue, decimal sellValue, int stacks, decimal volume, string appraisalUrl, IEnumerable<int> imageIndex, string shipType, string location, string characterName)
        {
            this.RawScan = rawScan;
            this.BuyValue = buyValue;
            this.SellValue = sellValue;
            this.Stacks = stacks;
            this.Volume = volume;
            this.AppraisalUrl = appraisalUrl;
            this.ImageIndex = imageIndex;
            this.ShipType = shipType;
            this.Location = location;
            this.CharacterName = characterName;
        }

        /// <summary>
        /// Gets the Raw Scan submitted to the engine.
        /// </summary>
        public string RawScan { get; private set; }

        /// <summary>
        /// Gets the value of the scan from a "Buy" perspective.
        /// </summary>
        public decimal BuyValue { get; private set; }

        /// <summary>
        /// Gets the value of the scan from a "Sell" perspective.
        /// </summary>
        public decimal SellValue { get; private set; }

        /// <summary>
        /// Gets the number of stacks in the scan.
        /// </summary>
        public int Stacks { get; private set; }

        /// <summary>
        /// Gets the volume of items in the scan.
        /// </summary>
        public decimal Volume { get; private set; }

        /// <summary>
        /// Gets the URL to the Appraisal
        /// </summary>
        public string AppraisalUrl { get; private set; }

        /// <summary>
        /// Gets the Image Index for the Appraisal
        /// </summary>
        public IEnumerable<int> ImageIndex { get; private set; }

        /// <summary>
        /// Gets or sets the ship type scanned
        /// </summary>
        public string ShipType { get; set; }

        /// <summary>
        /// Gets or sets the location of the scan.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the character name scanned.
        /// </summary>
        public string CharacterName { get; set; }

        public string FitInfo { get; set; }

        public string Notes { get; set; }

        /// <summary>
        /// Formats an ISK value as a string. Does not double the ISK. Returns up to 2 decimal places with an amount identifier.
        /// </summary>
        /// <param name="value">ISK value</param>
        /// <returns>Formatted string.</returns>
        public static string GetISKString(decimal value)
        {
            string output = string.Empty;

            if (value > 1000000000000000)
            {
                output = (value / 1000000000000000).ToString("#.00Q");
            }
            else if (value > 1000000000000)
            {
                output = (value / 1000000000000).ToString("#.00T");
            }
            else if (value > 1000000000)
            {
                output = (value / 1000000000).ToString("#.00B");
            }
            else if (value > 1000000)
            {
                output = (value / 1000000).ToString("#.00M");
            }
            else if (value > 1000)
            {
                output = (value / 1000).ToString("#.00K");
            }
            else
            {
                output = value.ToString();
            }

            return output;
        }

        /// <summary>
        /// Returns an interpreted version of the object as a string in a format convenient for pasting elsewhere.
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(this.ShipType))
            {
                sb.AppendFormat("{0} | ", this.ShipType);
            }

            sb.AppendFormat("{0} | {1} | {2} stacks |", ScanResult.GetISKString(this.SellValue), string.Format("{0:n}", this.Volume) + " m3", this.Stacks);
            if (this.ImageIndex != null && this.ImageIndex.Count() > 0)
            {
                bool first = true;

                foreach (int i in this.ImageIndex)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        sb.Append(",");
                    }

                    sb.AppendFormat(" {0}", ConfigHelper.Instance.ImageNames[i.ToString()]);
                }

                sb.Append(" |");
            }

            sb.AppendFormat(" {0}", this.AppraisalUrl);
            if (!string.IsNullOrEmpty(this.Location))
            {
                sb.AppendFormat(" | {0}", this.Location);
            }

            return sb.ToString();
        }
    }
}