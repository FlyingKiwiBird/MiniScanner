//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ListScanHistory.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EveScanner.Interfaces;

    /// <summary>
    /// Scan History which writes to an in-memory list.
    /// </summary>
    public class ListScanHistory : IScanHistory
    {
        /// <summary>
        /// Holds our results
        /// </summary>
        private static List<IScanResult> results = new List<IScanResult>();

        /// <summary>
        /// Holds an index of Unique identifier -> Integer for the results
        /// </summary>
        private static Dictionary<Guid, int> index = new Dictionary<Guid, int>();

        /// <summary>
        /// Static Locking Object
        /// </summary>
        private static object locker = new object();

        /// <summary>
        /// Adds a scan to storage.
        /// </summary>
        /// <param name="result">Scan Result to add to the Storage</param>
        /// <returns>Unique identifier of the returned row. In the horrifically low chance there's a collision, if the unique identifier returned doesn't match the one you passed in, you should retrieve the record back from the DB.</returns>
        public Guid AddScan(IScanResult result)
        {
            return ListScanHistory.AddListResult(result);
        }

        /// <summary>
        /// Gets a particular scan from storage.
        /// </summary>
        /// <param name="id">Scan Id</param>
        /// <returns>Scan Data</returns>
        public IScanResult GetResultById(Guid id)
        {
            return ListScanHistory.GetListResultById(id);
        }

        /// <summary>
        /// Gets all scans currently stored in storage.
        /// </summary>
        /// <returns>Collection of all scans</returns>
        public IEnumerable<IScanResult> GetAllScans()
        {
            return ListScanHistory.GetListResults();
        }

        /// <summary>
        /// Gets all scans currently stored in storage for a particular character.
        /// </summary>
        /// <param name="characterName">Character Name</param>
        /// <returns>Collection of all scans</returns>
        public IEnumerable<IScanResult> GetScansByCharacterName(string characterName)
        {
            return ListScanHistory.GetListResults().Where(x => x.CharacterName == characterName);
        }

        /// <summary>
        /// Updates the scan in the list.
        /// </summary>
        /// <param name="result">Scan to update</param>
        public void UpdateScan(IScanResult result)
        {
            ListScanHistory.UpdateListScan(result);
        }

        /// <summary>
        /// Gets a Result by Id from the Static List
        /// </summary>
        /// <param name="id">Scan Identifier</param>
        /// <returns>Scan Result</returns>
        private static IScanResult GetListResultById(Guid id)
        {
            lock (locker)
            {
                if (!index.ContainsKey(id))
                {
                    return null;
                }

                return results[index[id]];
            }
        }

        /// <summary>
        /// Adds a result to the Static List
        /// </summary>
        /// <param name="result">Scan Result</param>
        /// <returns>Scan Identifier</returns>
        private static Guid AddListResult(IScanResult result)
        {
            lock (locker)
            {
                results.Add(result);

                int ix = results.Count - 1;

                index.Add(result.Id, ix);

                return result.Id;
            }
        }

        /// <summary>
        /// Updates a scan
        /// </summary>
        /// <param name="result">Scan to update</param>
        private static void UpdateListScan(IScanResult result)
        {
            bool isAdd = false;

            lock (locker)
            {
                if (!index.ContainsKey(result.Id))
                {
                    isAdd = true;
                }
            }

            if (isAdd)
            {
                ListScanHistory.AddListResult(result);
            }
            else
            {
                lock (locker)
                {
                    results[index[result.Id]] = result;
                }
            }
        }

        /// <summary>
        /// Gets all results from the Static List
        /// </summary>
        /// <returns>Scan Results</returns>
        private static IEnumerable<IScanResult> GetListResults()
        {
            lock (locker)
            {
                return results;
            }
        }
    }
}
