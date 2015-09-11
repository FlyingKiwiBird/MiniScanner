//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Evepraisal.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
    using EveScanner.Interfaces;

    /// <summary>
    /// Provides a method to submit item lists to Evepraisal and receive a scan response.
    /// </summary>
    public class Evepraisal : IAppraisalService
    {
        /// <summary>
        /// Holds the URI for the appraisal service.
        /// </summary>
        private string uri = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Evepraisal"/> class.
        /// </summary>
        public Evepraisal()
            : this("evepraisal.com", false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Evepraisal"/> class. Allows for pointing at another domain.
        /// </summary>
        /// <param name="domain">Domain of the Scanning Service</param>
        /// <param name="https">Whether the service is HTTPS</param>
        public Evepraisal(string domain, bool https)
        {
            this.uri = (https ? "https" : "http") + "://" + domain + "/";
        }

        /// <summary>
        /// Gets a ScanResult for a particular set of data you want to appraise.
        /// </summary>
        /// <param name="data">Items to appraise</param>
        /// <returns>Parsed ScanResult</returns>
        public IScanResult GetAppraisalFromScan(string data)
        {
            string appraisal = this.GetAppraisalFromScanData(data);
            ScanResult rs = this.ParseResponse(appraisal);
            return rs;
        }

        /// <summary>
        /// Gets a ScanResult for a previously submitted appraisal.
        /// </summary>
        /// <param name="url">Previous Appraisal URL</param>
        /// <returns>Parsed ScanResult</returns>
        public IScanResult GetAppraisalFromUrl(string url)
        {
            string appraisal = this.GetPreviousAppraisal(url);
            ScanResult rs = this.ParseResponse(appraisal);
            return rs;
        }

        /// <summary>
        /// Parses an Evepraisal HTML document and returns a scan result.
        /// </summary>
        /// <param name="responseString">HTML from Evepraisal</param>
        /// <returns>Parsed ScanResult</returns>
        private ScanResult ParseResponse(string responseString)
        {
            // Find the scan data
            string textArea = "<textarea class=\"input-block-level\" rows=\"10\">";
            int dataIx = responseString.IndexOf(textArea, StringComparison.OrdinalIgnoreCase);
            int dataIe = responseString.IndexOf("</textarea>", dataIx + 1, StringComparison.OrdinalIgnoreCase);
            string rawScan = responseString.Substring(dataIx + textArea.Length, dataIe - dataIx - textArea.Length);
            if (rawScan.IndexOf("\r\n", StringComparison.OrdinalIgnoreCase) == -1)
            {
                rawScan = rawScan.Replace("\n", "\r\n");
            }

            // Find the /e/ link
            int scanIx = responseString.IndexOf("<a href=\"/e/", StringComparison.OrdinalIgnoreCase);
            int scanIe = responseString.IndexOf("\"", scanIx + 10, StringComparison.OrdinalIgnoreCase);
            string url = this.uri + responseString.Substring(scanIx + 10, scanIe - scanIx - 10);
            string appraisalUrl = url;

            // Find the footer with values...
            int footerIx = responseString.IndexOf("<th colspan=\"2\" style=\"text-align:right\">", StringComparison.OrdinalIgnoreCase);
            int footerEnd = responseString.IndexOf("</th>", footerIx, StringComparison.OrdinalIgnoreCase);
            string footer = responseString.Substring(footerIx, footerEnd - footerIx).Replace("\r", string.Empty).Replace("\n", string.Empty);

            // Find "Sell"
            string spanStart = "<span class=\"nowrap\">";
            string spanEnd = "</span>";

            int span1s = footer.IndexOf(spanStart, StringComparison.OrdinalIgnoreCase) + spanStart.Length;
            int span1e = footer.IndexOf(spanEnd, span1s, StringComparison.OrdinalIgnoreCase);

            string sellValueString = footer.Substring(span1s, span1e - span1s);
            decimal sellValue = decimal.Parse(sellValueString, CultureInfo.InvariantCulture);

            // Find "Buy"
            int span2s = footer.IndexOf(spanStart, span1e, StringComparison.OrdinalIgnoreCase) + spanStart.Length;
            int span2e = footer.IndexOf(spanEnd, span2s, StringComparison.OrdinalIgnoreCase);

            string buyValueString = footer.Substring(span2s, span2e - span2s);
            decimal buyValue = decimal.Parse(buyValueString, CultureInfo.InvariantCulture);

            // Find "Volume"
            int span3s = footer.IndexOf(spanStart, span2e, StringComparison.OrdinalIgnoreCase) + spanStart.Length;
            int span3e = footer.IndexOf("m", span2s, StringComparison.OrdinalIgnoreCase);

            string volumeString = footer.Substring(span3s, span3e - span3s);
            decimal volume = decimal.Parse(volumeString, CultureInfo.InvariantCulture);

            // Find "Stacks", and fix items for comparison in images.
            string[] items = rawScan.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = items[i].Substring(items[i].IndexOf(' ') + 1);
            }

            int stacks = items.Length;

            IEnumerable<int> imageIndex = ConfigHelper.Instance.FindImagesToDisplay(items);

            return new ScanResult(rawScan, buyValue, sellValue, stacks, volume, appraisalUrl, imageIndex, string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Retrieves the content of the page at the specified url.
        /// </summary>
        /// <param name="url">Unified Resource Locator</param>
        /// <returns>Contents of the URL specified</returns>
        private string GetPreviousAppraisal(string url)
        {
            WebRequest req = WebRequest.Create(url);
            req.Method = WebRequestMethods.Http.Get;

            string responseFromServer = string.Empty;

            using (HttpWebResponse rsp = (HttpWebResponse)req.GetResponse())
            {
                Logger.Debug("Status {0}({1}), Length: {2}", rsp.StatusCode.ToString(), rsp.StatusDescription, rsp.ContentLength.ToString(CultureInfo.InvariantCulture));
                Stream ds = rsp.GetResponseStream();
                using (StreamReader rdr = new StreamReader(ds))
                {
                    responseFromServer = rdr.ReadToEnd();
                    Logger.Debug("Response Html: {0}", responseFromServer);
                }
            }

            return responseFromServer;
        }

        /// <summary>
        /// Posts a set of data to Evepraisal for a new appraisal.
        /// </summary>
        /// <param name="data">Items to appraise</param>
        /// <returns>Page HTML</returns>
        private string GetAppraisalFromScanData(string data)
        {
            try
            {
                // Let's ask Evepraisal how much the cargo is worth...
                WebRequest req = WebRequest.Create(this.uri + "estimate");
                req.Method = WebRequestMethods.Http.Post;
                req.ContentType = "application/x-www-form-urlencoded";

                // Build the request string, Market is Jita
                string requestString = "raw_paste=" + data + "&market=30000142";
                Logger.Debug("Request String: {0}", requestString);
                byte[] encodedBytes = Encoding.UTF8.GetBytes(requestString);
                req.ContentLength = encodedBytes.Length;

                Stream ds = null;
                using (ds = req.GetRequestStream())
                {
                    ds.Write(encodedBytes, 0, encodedBytes.Length);
                }

                string responseFromServer = string.Empty;

                using (HttpWebResponse rsp = (HttpWebResponse)req.GetResponse())
                {
                    Logger.Debug("Status {0}({1}), Length: {2}", rsp.StatusCode.ToString(), rsp.StatusDescription, rsp.ContentLength.ToString(CultureInfo.InvariantCulture));

                    ds = rsp.GetResponseStream();
                    using (StreamReader rdr = new StreamReader(ds))
                    {
                        responseFromServer = rdr.ReadToEnd();
                        Logger.Debug("Response Html: {0}", responseFromServer);
                    }
                }

                return responseFromServer;
            }
            catch (Exception ex)
            {
                Logger.Debug("GetAppraisalFromScanData error", ex.ToString());
                throw;
            }
        }
    }
}
