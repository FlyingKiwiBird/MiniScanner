using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EveScanner
{
    public class ScanResult
    {
        public string RawScan { get; set; }

        public decimal BuyValue { get; private set; }

        public decimal SellValue { get; private set; }

        public int Stacks { get; private set; }

        public decimal Volume { get; private set; }

        public string EvepraisalUrl { get; private set; }

        public int? ImageIndex { get; private set; }

        public ScanResult(string evepraisalResponse)
        {
            this.ParseResponse(evepraisalResponse);
        }

        private void ParseResponse(string responseString)
        {
            // Find the scan data
            string textArea = "<textarea class=\"input-block-level\" rows=\"10\">";
            int dataIx = responseString.IndexOf(textArea);
            int dataIe = responseString.IndexOf("</textarea>");
            this.RawScan = responseString.Substring(dataIx + textArea.Length, dataIe - dataIx - textArea.Length);

            // Find the /e/ link
            int scanIx = responseString.IndexOf("<a href=\"/e/");
            int scanIe = responseString.IndexOf("\"", scanIx + 7);
            string url = "http://evepraisal.com" + responseString.Substring(scanIx + 9, scanIe - scanIx + 2);
            this.EvepraisalUrl = url;

            // Find the footer with values...
            int footerIx = responseString.IndexOf("<th colspan=\"2\" style=\"text-align:right\">");
            int footerEnd = responseString.IndexOf("</th>", footerIx);
            string footer = responseString.Substring(footerIx, footerEnd - footerIx).Replace("\r", "").Replace("\n", "");

            // Find "Sell"
            string spanStart = "<span class=\"nowrap\">";
            string spanEnd = "</span>";

            int span1s = footer.IndexOf(spanStart) + spanStart.Length;
            int span1e = footer.IndexOf(spanEnd, span1s);

            string sellValue = footer.Substring(span1s, span1e - span1s);
            this.SellValue = decimal.Parse(sellValue);

            // Find "Buy"
            int span2s = footer.IndexOf(spanStart, span1e) + spanStart.Length;
            int span2e = footer.IndexOf(spanEnd, span2s);

            string buyValue = footer.Substring(span2s, span2e - span2s);
            this.BuyValue = decimal.Parse(buyValue);

            // Find "Volume"
            int span3s = footer.IndexOf(spanStart, span2e) + spanStart.Length;
            int span3e = footer.IndexOf("m", span2s);

            string volume = footer.Substring(span3s, span3e - span3s);
            this.Volume = decimal.Parse(volume);

            // Find "Stacks"
            this.Stacks = Regex.Matches(responseString, "<tr class=\"line-item-row").Count;

            string[] items = this.RawScan.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = items[i].Substring(items[i].IndexOf(' ') + 1);
            }

            this.ImageIndex = EveScannerConfig.Instance.FindImageToDisplay(items);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} | {1} | {2} stacks |", ScanResult.GetIskString(this.SellValue), string.Format("{0:n}", this.Volume) + " m3", this.Stacks);
            if (this.ImageIndex != null)
            {
                sb.AppendFormat("{0} |", EveScannerConfig.Instance.ImageNames[this.ImageIndex.ToString()]);
            }
            sb.AppendFormat(" {0}", this.EvepraisalUrl);
            return sb.ToString();
        }

        public static string GetIskString(decimal value)
        {
            string output = string.Empty;

            if (value > 1000000000000)
            {
                output = (value / 1000000000000).ToString("#.00T");
            }
            else if (value > 1000000000)
            {
                output = (value / 1000000000).ToString("#.00B");
            }
            else if (value > 1000000)
            {
                output = (value / 1000000).ToString("#.00M");
            }
            else if (value > 1000)
            {
                output = (value / 1000).ToString("#.00K");
            }
            else
            {
                output = value.ToString();
            }
            return output;
        }
    }
}
