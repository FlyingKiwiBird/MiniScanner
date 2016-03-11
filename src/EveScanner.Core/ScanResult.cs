//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ScanResult.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using EveOnlineApi.Interfaces;
    using EveScanner.Interfaces;
    using IoC.Attributes;

    /// <summary>
    /// Provides a Scan Result implementation.
    /// </summary>
    public class ScanResult : IScanResult
    {
        /// <summary>
        /// Holds the Scan Id. This is to identify copied instances.
        /// </summary>
        private string id;

        /// <summary>
        /// Holds the scan date and time.
        /// </summary>
        private string scanDate;

        /// <summary>
        /// Holds the number of stacks, which may be different than the number of items in the appraisal.
        /// </summary>
        private long stacks;

        /// <summary>
        /// Holds the returned buy value for the result.
        /// </summary>
        private decimal buyValue;

        /// <summary>
        /// Holds the returned sell value for the result.
        /// </summary>
        private decimal sellValue;

        /// <summary>
        /// Holds the character object so we can do other things with it.
        /// </summary>
        private ICharacter character = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanResult"/> class.
        /// </summary>
        public ScanResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanResult"/> class.
        /// </summary>
        /// <param name="id">Unique Identifier for the Scan</param>
        /// <param name="scanDate">Date / Time the scan was taken.</param>
        /// <param name="rawScan">Raw scan submitted to the engine.</param>
        /// <param name="buyValue">Value of the scan from a "Buy" perspective.</param>
        /// <param name="sellValue">Value of the scan from a "Sell" perspective.</param>
        /// <param name="stacks">Number of stacks in the scan.</param>
        /// <param name="volume">Volume of items in the scan.</param>
        /// <param name="appraisalUrl">URL to the Appraisal</param>
        /// <param name="appraisedLines">Appraised Items</param>
        public ScanResult(Guid id, DateTime scanDate, string rawScan, decimal buyValue, decimal sellValue, int stacks, decimal volume, string appraisalUrl, IEnumerable<ILineAppraisal> appraisedLines)
        {
            if (id == null || id == Guid.Empty)
            {
                this.Id = Guid.NewGuid();
            }
            else
            {
                this.Id = id;
            }

            if (scanDate == null || scanDate == DateTime.MinValue || scanDate == DateTime.MaxValue)
            {
                this.ScanDate = DateTime.Now;
            }
            else
            {
                this.ScanDate = scanDate;
            }

            this.RawScan = rawScan;
            this.BuyValue = buyValue;
            this.SellValue = sellValue;
            this.Stacks = stacks;
            this.Volume = volume;
            this.AppraisalUrl = appraisalUrl;
            this.AppraisedLines = appraisedLines;
        }

        /// <summary>
        /// Gets the unique value for the scan id.
        /// </summary>
        [IgnoreMember]
        public Guid Id
        {
            get
            {
                return Guid.Parse(this.id);
            }

            private set
            {
                this.id = value.ToString();
            }
        }

        /// <summary>
        /// Gets the Date that the scan was taken.
        /// </summary>
        [IgnoreMember]
        public DateTime ScanDate
        {
            get
            {
                return DateTime.Parse(this.scanDate, CultureInfo.InvariantCulture);
            }

            private set
            {
                this.scanDate = value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the Raw Scan submitted to the engine.
        /// </summary>
        public string RawScan { get; private set; }

        /// <summary>
        /// Gets the value of the scan from a "Buy" perspective.
        /// </summary>
        public decimal BuyValue
        {
            get
            {
                if (this.ItemsReappraised)
                {
                    return this.AppraisedLines.Sum(x => x.BuyValue * x.Quantity);
                }

                return this.buyValue;
            }

            private set
            {
                this.buyValue = value;
            }
        }

        /// <summary>
        /// Gets the value of the scan from a "Sell" perspective.
        /// </summary>
        public decimal SellValue
        {
            get
            {
                if (this.ItemsReappraised)
                {
                    return this.AppraisedLines.Sum(x => x.SellValue * x.Quantity);
                }

                return this.sellValue;
            }

            private set
            {
                this.sellValue = value;
            }
        }

        /// <summary>
        /// Gets the number of stacks in the scan.
        /// </summary>
        [IgnoreMember]
        public int Stacks
        {
            get
            {
                return (int)this.stacks;
            }

            private set
            {
                this.stacks = value;
            }
        }

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

        /// <summary>
        /// Gets or sets some fitting information
        /// </summary>
        public string FitInfo { get; set; }

        /// <summary>
        /// Gets or sets additional user entered notes on the scan.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets additional Character data.
        /// </summary>
        public ICharacter Character
        {
            get
            {
                return this.character;
            }

            set
            {
                this.character = value;

                List<int> integers = null;
                if (this.ImageIndex == null)
                {
                    integers = new List<int>();
                }
                else
                {
                    integers = new List<int>(this.ImageIndex);
                }

                foreach (int i in new int[] { 190, 195, 205, 210 })
                {
                    if (integers.Contains(i))
                    {
                        integers.Remove(i);
                    }
                }

                if (this.character.Standings != null)
                {
                    if (this.Character.Standings.DerivedStanding < -5)
                    {
                        integers.Add(190);
                    }
                    else if (this.Character.Standings.DerivedStanding < 0)
                    {
                        integers.Add(195);
                    }
                    else if (this.Character.Standings.DerivedStanding > 5)
                    {
                        integers.Add(210);
                    }
                    else if (this.Character.Standings.DerivedStanding > 0)
                    {
                        integers.Add(205);
                    }
                }

                this.ImageIndex = integers;
            }
        }

        /// <summary>
        /// Gets the repackaged volume of items in the appraisal.
        /// </summary>
        public decimal RepackagedVolume
        {
            get
            {
                if (this.AppraisedLines == null || this.AppraisedLines.Count() == 0)
                {
                    return 0;
                }

                return (decimal)this.AppraisedLines.Sum(x => x.RepackagedVolume * x.Quantity);
            }
        }

        /// <summary>
        /// Gets the appraised lines from the scan.
        /// </summary>
        public IEnumerable<ILineAppraisal> AppraisedLines
        {
            get; private set;
        }

        /// <summary>
        /// Gets a value indicating whether some items in the scan obtained prices from a secondary source.
        /// </summary>
        public bool ItemsReappraised
        {
            get
            {
                if (this.AppraisedLines == null || this.AppraisedLines.Count() == 0)
                {
                    return false;
                }

                return this.AppraisedLines.Where(x => x.Reappraised).Count() > 0;
            }
        }

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
                output = (value / 1000000000000000).ToString("#.00Q", CultureInfo.InvariantCulture);
            }
            else if (value > 1000000000000)
            {
                output = (value / 1000000000000).ToString("#.00T", CultureInfo.InvariantCulture);
            }
            else if (value > 1000000000)
            {
                output = (value / 1000000000).ToString("#.00B", CultureInfo.InvariantCulture);
            }
            else if (value > 1000000)
            {
                output = (value / 1000000).ToString("#.00M", CultureInfo.InvariantCulture);
            }
            else if (value > 1000)
            {
                output = (value / 1000).ToString("#.00K", CultureInfo.InvariantCulture);
            }
            else
            {
                output = value.ToString(CultureInfo.InvariantCulture);
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

            if (this.character != null && this.character.Standings != null)
            {
                decimal st = this.character.Standings.DerivedStanding;

                if (st < -5)
                {
                    sb.Append("RED ");
                }
                else if (st < 0)
                {
                    sb.Append("ORANGE ");
                }
                else if (st > 0)
                {
                    sb.Append("BLUE ");
                }
            }

            if (!string.IsNullOrEmpty(this.ShipType))
            {
                if (ConfigHelper.Instance.ShipTypes[this.ShipType] != null)
                {
                    sb.AppendFormat("{0} | ", ConfigHelper.Instance.ShipTypes[this.ShipType]);
                }
                else
                {
                    sb.AppendFormat("{0} | ", this.ShipType);
                }
            }
            else if (!string.IsNullOrEmpty(sb.ToString()))
            {
                sb.Append("| ");
            }

            if (this.SellValue == 0 && this.BuyValue == 0 && this.Volume == 0 && this.Stacks == 0)
            {
                sb.Append("EMPTY |");
            }
            else
            {
                sb.AppendFormat(CultureInfo.CurrentCulture, "{0}/{1} | {2} | {3} stacks |", ScanResult.GetISKString(this.SellValue), ScanResult.GetISKString(this.BuyValue), string.Format(CultureInfo.InvariantCulture, "{0:n}", this.Volume) + " m3", this.Stacks);
            }

            if (this.ImageIndex != null && this.ImageIndex.Count() > 0)
            {
                bool first = true;
                foreach (int i in this.ImageIndex.Where(x => !(new int[] { 190, 195, 205, 210 }).Contains(x)))
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

                if (!first)
                {
                    sb.Append(" |");
                }
            }

            sb.AppendFormat(" {0}", this.AppraisalUrl);
            if (!string.IsNullOrEmpty(this.Location))
            {
                sb.AppendFormat(" | {0}", this.Location);
            }

            if (!string.IsNullOrEmpty(this.FitInfo))
            {
                sb.AppendFormat(" | {0}", this.FitInfo);
            }

            if (!string.IsNullOrEmpty(this.CharacterName))
            {
                if (this.Character != null)
                {
                    sb.AppendFormat(" | {0}", this.Character.ToString());
                }
                else
                {
                    sb.AppendFormat(" | {0}", this.CharacterName);
                }
            }

            if (this.ItemsReappraised)
            {
                sb.Append(" | *Secondary Pricing*");
            }

            return sb.ToString();
        }
    }
}
