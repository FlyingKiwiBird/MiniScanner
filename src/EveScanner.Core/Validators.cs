//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Validators.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Core
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Contains shared validation methods for the application.
    /// </summary>
    public static class Validators
    {
        /// <summary>
        /// Check every line. If one doesn't match the Cargo Scan criteria, return false.
        /// </summary>
        /// <param name="inputText">Scan data</param>
        /// <returns>True if we think this is a cargo scan, false otherwise.</returns>
        public static bool CheckForCargoScan(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                return false;
            }

            foreach (string line in inputText.Split(new string[] { "\r", "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!Regex.IsMatch(line, @"^(?<line>\d+ [0-9a-z\-\. ()/':,]+)\r?$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
