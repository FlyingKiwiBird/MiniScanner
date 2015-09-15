//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="HistoryWrapper.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using EveScanner.Interfaces;

    /// <summary>
    /// Used to wrap Scan Results for the Scan History view.
    /// </summary>
    public class HistoryWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryWrapper"/> class.
        /// </summary>
        /// <param name="result">Scan Result to Wrap</param>
        public HistoryWrapper(IScanResult result)
        {
            this.Scan = result;
        }

        /// <summary>
        /// Gets the scan we're wrapping.
        /// </summary>
        public IScanResult Scan { get; private set; }

        /// <summary>
        /// Gets a unique identifier for this scan.
        /// </summary>
        public Guid Id
        {
            get
            {
                return this.Scan.Id;
            }
        }

        /// <summary>
        /// Gets the Date and time the scan was taken.
        /// </summary>
        public DateTime ScanDate
        {
            get
            {
                return this.Scan.ScanDate;
            }
        }

        /// <summary>
        /// Gets the Sell/Buy value string associated with the scan.
        /// </summary>
        public string Value
        {
            get
            {
                if (this.Scan.SellValue == 0 && this.Scan.BuyValue == 0 && this.Scan.Stacks == 0 && this.Scan.Volume == 0)
                {
                    return "EMPTY";
                }

                return string.Format(CultureInfo.CurrentCulture, "{0}/{1}", ScanResult.GetISKString(this.Scan.SellValue), ScanResult.GetISKString(this.Scan.BuyValue));
            }
        }

        /// <summary>
        /// Gets the ship type associated with the scan.
        /// </summary>
        public string ShipType
        {
            get
            {
                return this.Scan.ShipType;
            }
        }

        /// <summary>
        /// Gets the location associated with the scan.
        /// </summary>
        public string Location
        {
            get
            {
                return this.Scan.Location;
            }
        }

        /// <summary>
        /// Gets the Character Name associated with the scan
        /// </summary>
        public string CharacterName
        {
            get
            {
                return this.Scan.CharacterName;
            }
        }

        /// <summary>
        /// Gets a formatted string of the special cases for the scan.
        /// </summary>
        public string SpecialCases
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (this.Scan.ImageIndex != null && this.Scan.ImageIndex.Count() > 0)
                {
                    bool first = true;

                    foreach (int i in this.Scan.ImageIndex)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            sb.Append(",");
                        }

                        sb.AppendFormat(" {0}", ConfigHelper.Instance.ImageNames[i.ToString(CultureInfo.InvariantCulture)]);
                    }
                }

                return sb.ToString();
            }
        }
    }
}
