using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EveScanner;
using EveScanner.Interfaces;

namespace EveScannerTests
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void TestA()
        {
            ScanResult rs = new ScanResult("X", 1, 2, 3, 4, "http://", new int[] { });
            Assert.IsNotNull(rs);

            string output = ScanSerializer.SerializeScan(rs);
            Assert.IsNotNull(output);
            
        }

        [TestMethod]
        public void TestB()
        {
            ScanResult rs = new ScanResult("X", 1, 2, 3, 4, "http://", new int[] { });
            Assert.IsNotNull(rs);

            string output = ScanSerializer.SerializeScan(rs);
            Assert.IsNotNull(output);

            ScanResult rs_b = ScanSerializer.DeserializeScan(output);
            Assert.IsNotNull(rs_b);
        }
    }
}
