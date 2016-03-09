using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EveScanner.Evepraisal.Tests
{
    [TestClass]
    public class EvepraisalJson
    {
        [TestMethod]
        public void TestSerializer()
        {
            string data = File.ReadAllText("sample.json");
            var v = Evepraisal.EvepraisalJson.Resolve(data);
            Assert.IsNotNull(v);
        }

        [TestMethod]
        public void CheckProperties()
        {
            string data = File.ReadAllText("sample.json");
            var v = Evepraisal.EvepraisalJson.Resolve(data);
            Assert.IsNotNull(v);

            Assert.AreEqual(1439952792, v.Created);
            Assert.AreEqual(7111070, v.Id);

            Assert.AreEqual(4, v.Items.Length);

            Assert.AreEqual(530, v.Items[0].GroupID);
            Assert.AreEqual(true, v.Items[0].Market);
            Assert.AreEqual("Construction Alloy", v.Items[0].Name);

            Assert.AreEqual(74.4, v.Items[0].Prices.All.Average);
            Assert.AreEqual(145, v.Items[0].Prices.All.Maximum);
            Assert.AreEqual(119.25, v.Items[0].Prices.All.Median);
            Assert.AreEqual(1, v.Items[0].Prices.All.Minimum);
            Assert.AreEqual(1, v.Items[0].Prices.All.Percentile);
            Assert.AreEqual(1, v.Items[0].Prices.All.Price);
            Assert.AreEqual(58.82, v.Items[0].Prices.All.StdDev);
            Assert.AreEqual(1518954, v.Items[0].Prices.All.Volume);

            Assert.AreEqual(74.4, v.Items[0].Prices.Buy.Average);
            Assert.AreEqual(145, v.Items[0].Prices.Buy.Maximum);
            Assert.AreEqual(60.74, v.Items[0].Prices.Buy.Median);
            Assert.AreEqual(1, v.Items[0].Prices.Buy.Minimum);
            Assert.AreEqual(145, v.Items[0].Prices.Buy.Percentile);
            Assert.AreEqual(145, v.Items[0].Prices.Buy.Price);
            Assert.AreEqual(58.82, v.Items[0].Prices.Buy.StdDev);
            Assert.AreEqual(1518954, v.Items[0].Prices.Buy.Volume);

            Assert.AreEqual(1100.38, v.Items[0].Prices.Sell.Average);
            Assert.AreEqual(1500, v.Items[0].Prices.Sell.Maximum);
            Assert.AreEqual(1099.89, v.Items[0].Prices.Sell.Median);
            Assert.AreEqual(1098.99, v.Items[0].Prices.Sell.Minimum);
            Assert.AreEqual(1098.99, v.Items[0].Prices.Sell.Percentile);
            Assert.AreEqual(1098.99, v.Items[0].Prices.Sell.Price);
            Assert.AreEqual(132.4, v.Items[0].Prices.Sell.StdDev);
            Assert.AreEqual(37068, v.Items[0].Prices.Sell.Volume);

            Assert.AreEqual(48, v.Items[0].Quantity);
            Assert.AreEqual(21595, v.Items[0].TypeID);
            Assert.AreEqual("Construction Alloy", v.Items[0].TypeName);
            Assert.AreEqual(0.1, v.Items[0].Volume);

            Assert.AreEqual("cargo_scan", v.Kind);
            Assert.AreEqual(30000142, v.MarketId);
            Assert.AreEqual("Jita", v.MarketName);

            Assert.AreEqual(17912.63, v.Totals.Buy);
            Assert.AreEqual(88931.09000000001, v.Totals.Sell);
            Assert.AreEqual(15.72, v.Totals.Volume);
        }
    }
}
