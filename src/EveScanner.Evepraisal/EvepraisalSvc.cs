//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EvepraisalSvc.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Evepraisal
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    using EveScanner.Core;
    using EveScanner.Interfaces;
    using EveScanner.IoC;

    /// <summary>
    /// Provides a method to submit item lists to Evepraisal and receive a scan response.
    /// </summary>
    public class EvepraisalSvc : IAppraisalService
    {
        /// <summary>
        /// Holds the URI for the appraisal service.
        /// </summary>
        private string uri = string.Empty;

        /// <summary>
        /// Holds Scan Data
        /// </summary>
        private string scanData = string.Empty;

        /// <summary>
        /// Holds scan url
        /// </summary>
        private string scanUrl = string.Empty;

        /// <summary>
        /// Holds appraisal response.
        /// </summary>
        private string appraisalResponse = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="EvepraisalSvc"/> class.
        /// </summary>
        public EvepraisalSvc()
            : this("evepraisal.com", false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EvepraisalSvc"/> class. Allows for pointing at another domain.
        /// </summary>
        /// <param name="domain">Domain of the Scanning Service</param>
        /// <param name="https">Whether the service is HTTPS</param>
        public EvepraisalSvc(string domain, bool https)
        {
            this.uri = (https ? "https" : "http") + "://" + domain + "/";
        }

        /// <summary>
        /// Gets a ScanResult for a particular set of data you want to appraise.
        /// </summary>
        /// <param name="data">Items to appraise</param>
        /// <returns>Parsed ScanResult</returns>
        public IScanResult GetAppraisalFromScan(IEnumerable<ILineAppraisal> data)
        {
            this.scanUrl = string.Empty;
            this.scanData = string.Join(Environment.NewLine, data.Select(x => string.Format(CultureInfo.InvariantCulture, "{0} {1}", x.Quantity, x.TypeName)).ToArray());
            this.appraisalResponse = this.GetAppraisalFromScanData();
            ScanResult rs = this.ParseResponse();
            return rs;
        }

        /// <summary>
        /// Gets a ScanResult for a previously submitted appraisal.
        /// </summary>
        /// <param name="url">Previous Appraisal URL</param>
        /// <returns>Parsed ScanResult</returns>
        public IScanResult GetAppraisalFromUrl(string url)
        {
            this.scanUrl = url;
            this.scanData = string.Empty;
            this.appraisalResponse = this.GetPreviousAppraisal();
            ScanResult rs = this.ParseResponse();
            return rs;
        }

        /// <summary>
        /// Indicates if this service can handle a URL presented to it.
        /// </summary>
        /// <param name="url">URL to previously submitted appraisal</param>
        /// <returns>True if the service can handle the URL, false otherwise.</returns>
        public bool CanRetrieveFromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            return url.Replace("https", "http").StartsWith(this.uri.Replace("https", "http"), StringComparison.Ordinal);
        }

        /// <summary>
        /// Parses an Evepraisal HTML document and returns a scan result.
        /// </summary>
        /// <returns>Parsed ScanResult</returns>
        private ScanResult ParseResponse()
        {
            string responseString = this.appraisalResponse;

            try
            {
                // Find the /e/ link
                int scanIx = responseString.IndexOf("<a href=\"/e/", StringComparison.OrdinalIgnoreCase);
                int scanIe = responseString.IndexOf("\"", scanIx + 10, StringComparison.OrdinalIgnoreCase);
                string url = this.uri + responseString.Substring(scanIx + 10, scanIe - scanIx - 10);
                string appraisalUrl = url;

                if (string.IsNullOrEmpty(this.scanData))
                {
                    int rawIa = responseString.IndexOf("<textarea class=\"input-block-level\" rows", StringComparison.OrdinalIgnoreCase);
                    int rawIx = responseString.IndexOf(">", rawIa + 1, StringComparison.OrdinalIgnoreCase);
                    int rawIe = responseString.IndexOf("</textarea>", rawIx + 1, StringComparison.OrdinalIgnoreCase);

                    string rawScan = responseString.Substring(rawIx + 1, rawIe - rawIx - 1);
                    this.scanData = rawScan;
                }

                // And grab the .json instead
                Uri localUri = new Uri(url + ".json");

                using (IWebClient dl = Injector.Create<IWebClient>())
                {
                    string result = dl.GetUriToString(localUri);
                    EvepraisalJson json = EvepraisalJson.Resolve(result);
                    return new ScanResult(
                        Guid.Empty,
                        DateTime.Now,
                        this.scanData,
                        (decimal)json.Totals.Buy,
                        (decimal)json.Totals.Sell,
                        !string.IsNullOrEmpty(this.scanData) ? this.scanData.Split(new string[] { Environment.NewLine, "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Length : json.Items.Count(),
                        (decimal)json.Totals.Volume,
                        appraisalUrl,
                        json.Items);
                }
            }
            catch
            {
                string s = "SCAN-" + DateTime.Now.ToString("yyyyMMddhhmmss", CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(this.scanData))
                {
                    File.WriteAllText(s + ".req.txt", this.scanData);
                }

                if (!string.IsNullOrEmpty(this.scanUrl))
                {
                    File.WriteAllText(s + ".req.txt", this.scanUrl);
                }

                File.WriteAllText(s + ".rsp.txt", this.appraisalResponse);

                Logger.Error("Scan Parsing Failed! Logged scan data to " + s + ".*.txt", true);

                throw;
            }
        }

        /// <summary>
        /// Retrieves the content of the page at the specified url.
        /// </summary>
        /// <returns>Contents of the URL specified</returns>
        private string GetPreviousAppraisal()
        {
            Uri localUri = new Uri(this.scanUrl);

            using (IWebClient dl = Injector.Create<IWebClient>())
            {
                return dl.GetUriToString(localUri);
            }
        }

        /// <summary>
        /// Posts a set of data to Evepraisal for a new appraisal.
        /// </summary>
        /// <returns>Page HTML</returns>
        private string GetAppraisalFromScanData()
        {
            string data = this.scanData;

            try
            {
                Uri localUri = new Uri(this.uri + "estimate");
                string requestString = "raw_paste=" + data + "&market=30000142";
                Logger.Debug("Request String: {0}", requestString);
                byte[] encodedBytes = Encoding.UTF8.GetBytes(requestString);

                using (IWebClient cli = Injector.Create<IWebClient>())
                {
                    string responseFromServer = cli.PostUriToString(localUri, encodedBytes);
                    Logger.Debug("Response Html: {0}", responseFromServer);

                    return responseFromServer;
                }
            }
            catch (Exception ex)
            {
                Logger.Debug("Error", ex.ToString());
                throw;
            }
        }
    }
}
