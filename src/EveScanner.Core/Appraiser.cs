//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Appraiser.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EveScanner.Interfaces;
    using EveScanner.Interfaces.Providers;
    using EveScanner.IoC;

    /// <summary>
    /// This class does all the heavy lifting for scan appraisals.
    /// </summary>
    public class Appraiser
    {
        /// <summary>
        /// Gets an Appraisal for a scan using whatever appraiser is already registered and defaulted.
        /// </summary>
        /// <param name="data">Scan Data as Text</param>
        /// <returns>Appraisal Information</returns>
        public IScanResult GetAppraisal(string data)
        {
            return this.GetAppraisal(data, null);
        }

        /// <summary>
        /// Gets an Appraisal using the Appraiser Specified.
        /// </summary>
        /// <param name="data">Scan Data as Text</param>
        /// <param name="appraiserName">Appraiser to use. If none specified, the default is used.</param>
        /// <returns>Appraisal Information</returns>
        public IScanResult GetAppraisal(string data, string appraiserName)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return null;
            }

            IScanResult output = null;

            string url = data;
            url = url.IndexOf(' ') < 0 ? url : url.Substring(0, url.IndexOf(' '));
            url = url.IndexOf('\r') < 0 ? url : url.Substring(0, url.IndexOf('\r'));
            url = url.IndexOf('\n') < 0 ? url : url.Substring(0, url.IndexOf('\n'));
            
            if (Validators.CheckForCargoScan(data))
            {
                // Hold on to these for afterwards...
                IEnumerable<ILineAppraisal> items = this.GetItemsFromText(data);

                IAppraisalService svc = null;

                if (!string.IsNullOrEmpty(appraiserName))
                {
                    svc = Injector.Create<IAppraisalService>();
                }
                else
                {
                    svc = Injector.Create<IAppraisalService>(appraiserName);
                }

                output = svc.GetAppraisalFromScan(items);
            }
            else if (Validators.CheckForUri(url))
            {
                foreach (var v in Injector.GetImplementationsFor<IAppraisalService>())
                {
                    IAppraisalService svc = v.Construct();
                    if (svc.CanRetrieveFromUrl(url))
                    {
                        output = svc.GetAppraisalFromUrl(url);
                        break;
                    }
                }
            }
            else
            {
                throw new Exception("Text was not scan or recognized URL");
            }

            if (output != null)
            {
                IItemPriceProvider ipa = Injector.Create<IItemPriceProvider>();

                if (ipa != null)
                {
                    foreach (var item in output.AppraisedLines.Where(x => x.BuyValue == 0 && x.SellValue == 0))
                    {
                        item.ReappraiseItem(ipa);
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Converts a text block of many lines into a bunch of ScanItem(s).
        /// </summary>
        /// <param name="data">Scan Data as Text</param>
        /// <returns>IEnumerable of ScanItem</returns>
        private IEnumerable<ScanLine> GetItemsFromText(string data)
        {
            foreach (string s in Validators.ExtractLinesFromInput(data))
            {
                yield return Validators.GetScanItemFromLine(s);
            }

            yield break;
        }
    }
}
