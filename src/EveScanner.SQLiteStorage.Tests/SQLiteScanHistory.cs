namespace EveScanner.SQLiteStorage.Tests
{
    using EveScanner.Core;
    using EveScanner.Interfaces;
    using EveScanner.IoC;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Data.SQLite;
    using System.IO;

    [TestClass]
    public class SQLiteScanHistory
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            string connectionString = ConfigHelper.GetConnectionString("SQLiteScanHistory");
            SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder(connectionString);
            string filename = sb.DataSource;

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            Injector.Register<IScanHistory>(typeof(SQLiteStorage.SQLiteScanHistory));
        }

        [TestMethod]
        public void AddScanToStorage()
        {
            var ish = Injector.Create<IScanHistory>();
            Assert.IsNotNull(ish);

            Guid g = Guid.Empty;

            ScanResult r = new ScanResult(
                    g,
                    DateTime.Now,
                    "",
                    0,
                    0,
                    0,
                    0,
                    string.Empty,
                    null
            );

            ish.AddScan(r);
            Assert.AreNotEqual(Guid.Empty, r.Id);
        }

        [TestMethod]
        public void GetScanFromStorage()
        {
            var ish = Injector.Create<IScanHistory>();
            Assert.IsNotNull(ish);
            
            Guid g = Guid.Empty;

            ScanResult r = new ScanResult(
                    g,
                    DateTime.Now,
                    "",
                    0,
                    0,
                    0,
                    0,
                    string.Empty,
                    null
            );

            ish.AddScan(r);
            Assert.AreNotEqual(Guid.Empty, r.Id);

            var rx = ish.GetResultById(r.Id);
            Assert.IsNotNull(rx);
            Assert.AreEqual(rx.Id, r.Id);
        }
    }
}
