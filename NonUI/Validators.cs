using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EveScanner
{
    public static class Validators
    {
        /// <summary>
        /// Check every line. If one doesn't match the Cargo Scan criteria, return false.
        /// </summary>
        /// <param name="inputText">Scan data</param>
        /// <returns>True if we think this is a cargo scan, false otherwise.</returns>
        public static bool CheckForCargoScan(string inputText)
        {
            foreach (string line in inputText.Split(new string[] { "\r", "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!Regex.IsMatch(line, @"^(?<line>\d+ [0-9a-z\-\. ()/]+)\r?$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
