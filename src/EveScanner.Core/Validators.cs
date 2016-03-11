//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Validators.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Core
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Contains shared validation methods for the application.
    /// </summary>
    public static class Validators
    {
        /// <summary>
        /// This is our cargo scanning validation regex.
        /// </summary>
        private static Regex cargoScanCheck = new Regex(@"^(?<qty>\d*) (?<item>.*?)( )?([(](?<bpind>Original|Copy)[)])?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Converts a line of text into a ScanLine item.
        /// </summary>
        /// <param name="inputText">Text to convert</param>
        /// <returns>Scan Line object</returns>
        public static ScanLine GetScanItemFromLine(string inputText)
        {
            MatchCollection mc = cargoScanCheck.Matches(inputText);
            ScanLine scanItem = new ScanLine();

            if (mc.Count > 0)
            {
                Match m = mc[0];

                scanItem.TypeName = m.Groups["item"].Value;
                scanItem.Quantity = int.Parse(m.Groups["qty"].Value, CultureInfo.InvariantCulture);
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
                scanItem = new ScanLine() { IsError = true, ErrorMessage = inputText };
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

            if (input.IndexOf("\r\n", StringComparison.Ordinal) > -1)
            {
                endofline = "\r\n";
            }
            else if (input.IndexOf("\r", StringComparison.Ordinal) > -1)
            {
                endofline = "\r";
            }
            else if (input.IndexOf("\n", StringComparison.Ordinal) > -1)
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

        /// <summary>
        /// Determines if text is a URI. Looks for // in the URI.
        /// </summary>
        /// <param name="inputText">Text to check</param>
        /// <returns>True if it is a URI, false otherwise.</returns>
        public static bool CheckForUri(string inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText))
            {
                return false;
            }

            if (inputText.IndexOf("//", StringComparison.Ordinal) < 0)
            {
                return false;
            }

            return Uri.IsWellFormedUriString(inputText, UriKind.Absolute);
        }
    }
}
