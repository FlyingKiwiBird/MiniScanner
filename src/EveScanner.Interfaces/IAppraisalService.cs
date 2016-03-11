//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IAppraisalService.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the methods exposed through an appraisal service.
    /// </summary>
    public interface IAppraisalService
    {
        /// <summary>
        /// Gets an appraisal from a given scan.
        /// </summary>
        /// <param name="data">Data to send for appraisal</param>
        /// <returns>Parsed appraisal</returns>
        IScanResult GetAppraisalFromScan(IEnumerable<ILineAppraisal> data);

        /// <summary>
        /// Gets a previously submitted appraisal from the service.
        /// </summary>
        /// <param name="url">URL to previously submitted appraisal</param>
        /// <returns>Parsed appraisal</returns>
        IScanResult GetAppraisalFromUrl(string url);

        /// <summary>
        /// Indicates if this service can handle a URL presented to it.
        /// </summary>
        /// <param name="url">URL to previously submitted appraisal</param>
        /// <returns>True if the service can handle the URL, false otherwise.</returns>
        bool CanRetrieveFromUrl(string url);
    }
}
