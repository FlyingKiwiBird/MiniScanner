//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IScanResult.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces
{
    using System;
    using System.Collections.Generic;

    using EveOnlineApi.Interfaces;

    /// <summary>
    /// Interface representing the properties required by the application from a scan result.
    /// </summary>
    public interface IScanResult
    {
        /// <summary>
        /// Gets the unique value for the scan id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the Date that the scan was taken.
        /// </summary>
        DateTime ScanDate { get; }
        
        /// <summary>
        /// Gets the Raw Scan submitted to the engine.
        /// </summary>
        string RawScan { get; }

        /// <summary>
        /// Gets the value of the scan from a "Buy" perspective.
        /// </summary>
        decimal BuyValue { get; }

        /// <summary>
        /// Gets the value of the scan from a "Sell" perspective.
        /// </summary>
        decimal SellValue { get; }

        /// <summary>
        /// Gets the number of stacks in the scan.
        /// </summary>
        int Stacks { get; }

        /// <summary>
        /// Gets the volume of items in the scan.
        /// </summary>
        decimal Volume { get; }

        /// <summary>
        /// Gets the repackaged volume of items in the scan (which could be less than the full volume)
        /// </summary>
        decimal RepackagedVolume { get; }

        /// <summary>
        /// Gets a list of all the items in the appraisal and their values.
        /// </summary>
        IEnumerable<ILineAppraisal> AppraisedLines { get; }

        /// <summary>
        /// Gets the URL to the Appraisal
        /// </summary>
        string AppraisalUrl { get; }

        /// <summary>
        /// Gets the Image Index for the Appraisal
        /// </summary>
        IEnumerable<int> ImageIndex { get; }

        /// <summary>
        /// Gets or sets the ship type scanned
        /// </summary>
        string ShipType { get; set; }

        /// <summary>
        /// Gets or sets the location of the scan.
        /// </summary>
        string Location { get; set; }

        /// <summary>
        /// Gets or sets the character name scanned.
        /// </summary>
        string CharacterName { get; set; }

        /// <summary>
        /// Gets or sets some fitting information
        /// </summary>
        string FitInfo { get; set; }

        /// <summary>
        /// Gets or sets additional user entered notes on the scan.
        /// </summary>
        string Notes { get; set; }

        /// <summary>
        /// Gets or sets additional Character data.
        /// </summary>
        ICharacter Character { get; set; }

        /// <summary>
        /// Gets a value indicating whether some items in the scan obtained prices from a secondary source.
        /// </summary>
        bool ItemsReappraised { get; }
    }
}
