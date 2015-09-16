//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IScanHistory.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for scan history storage.
    /// </summary>
    public interface IScanHistory
    {
        /// <summary>
        /// Adds a scan to storage.
        /// </summary>
        /// <param name="result">Scan Result to add to the Storage</param>
        /// <returns>Unique identifier of the returned row. In the horrifically low chance there's a collision, if the unique identifier returned doesn't match the one you passed in, you should retrieve the record back from the DB.</returns>
        Guid AddScan(IScanResult result);

        /// <summary>
        /// Gets a particular scan from storage.
        /// </summary>
        /// <param name="id">Scan Id</param>
        /// <returns>Scan Data</returns>
        IScanResult GetResultById(Guid id);

        /// <summary>
        /// Gets all scans currently stored in storage.
        /// </summary>
        /// <returns>Collection of all scans</returns>
        IEnumerable<IScanResult> GetAllScans();

        /// <summary>
        /// Gets all scans currently stored in storage for a particular character.
        /// </summary>
        /// <param name="characterName">Character Name</param>
        /// <returns>Collection of all scans</returns>
        IEnumerable<IScanResult> GetScansByCharacterName(string characterName);
    }
}
