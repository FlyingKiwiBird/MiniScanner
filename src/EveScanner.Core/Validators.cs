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
        private static Regex cargoScanCheck = new Regex(@"^(?<qty>\d*) (?<item>.*?)( )?([(](?<bpind>Original|Copy)[)])?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        public static ScanItem GetScanItemFromLine(string inputText)
        {
            MatchCollection mc = cargoScanCheck.Matches(inputText);
            ScanItem scanItem = new ScanItem();

            if (mc.Count > 0)
            {
                Match m = mc[0];

                scanItem.TypeName = m.Groups["item"].Value;
                scanItem.Quantity = int.Parse(m.Groups["qty"].Value);
                if (m.Groups["bpind"] != null)
                {
                    if (m.Groups["bpind"].Value == "Copy")
                    {
                        scanItem.IsBlueprintCopy = true;
                    }
                }
            }

            if (scanItem == null)
            {
                scanItem = new ScanItem() { IsError = true, ErrorMessage = inputText };
            }

            return scanItem;
        }
        
        /// <summary>
        /// Splits a text string into multiple lines.
        /// </summary>
        /// <param name="input">Text String</param>
        /// <returns>Array of individual lines</returns>
        public static string[] ExtractLinesFromInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new string[] { input };
            }

            string endofline = null;

            if (input.IndexOf("\r\n") > -1)
            {
                endofline = "\r\n";
            }
            else if (input.IndexOf("\r") > -1)
            {
                endofline = "\r";
            }
            else if (input.IndexOf("\n") > -1)
            {
                endofline = "\n";
            }

            return input.Split(new string[] { endofline }, StringSplitOptions.RemoveEmptyEntries);
        }

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

            foreach (string line in Validators.ExtractLinesFromInput(inputText))
            {
                if (!Validators.cargoScanCheck.IsMatch(line))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
