using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EveOnlineApi.Common;
using EveOnlineApi.Entities.Xml;
using EveOnlineApi.Interfaces.Xml;
using EveScanner.IoC;

namespace EveOnlineApi.Tests
{
    [TestClass]
    public class XMLCharacterInfo
    {
        public static string SampleXml { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            XMLCharacterInfo.SampleXml = File.ReadAllText("samples\\CharacterInfo.xml");
        }

        [TestMethod]
        public void CharacterInfo_TestSerialization()
        {
            CharacterInfoApi info = XmlSerialization.DeserializeString<CharacterInfoApi>(XMLCharacterInfo.SampleXml);
            Assert.IsNotNull(info);

            Assert.AreEqual(2, info.Version);
            Assert.AreEqual("2015-09-17 02:51:43", info.CurrentTime);
            Assert.AreEqual("2015-09-17 03:46:12", info.CachedUntil);

            Assert.IsNotNull(info.Result);
            Assert.AreEqual(1170031179, info.Result.CharacterId);
            Assert.AreEqual("Viktorie Lucilla", info.Result.CharacterName);
            Assert.AreEqual("Amarr", info.Result.Race);
            Assert.AreEqual(13, info.Result.BloodlineId);
            Assert.AreEqual("Khanid", info.Result.Bloodline);
            Assert.AreEqual(37, info.Result.AncestryId);
            Assert.AreEqual("Cyber Knights", info.Result.Ancestry);
            Assert.AreEqual(667531913, info.Result.CorporationId);
            Assert.AreEqual("GoonWaffe", info.Result.Corporation);
            Assert.AreEqual("2012-04-10 13:05:00", info.Result.CorporationDate);
            Assert.AreEqual(1354830081, info.Result.AllianceId);
            Assert.AreEqual("Goonswarm Federation", info.Result.Alliance);
            Assert.AreEqual("2010-06-04 00:55:00", info.Result.AllianceDate);
            Assert.AreEqual(-1.2521440165047, info.Result.SecurityStatus);

            Assert.IsNotNull(info.Result.RowSet);
            Assert.AreEqual("employmentHistory", info.Result.RowSet.Name);
            Assert.AreEqual("recordID", info.Result.RowSet.Key);
            Assert.AreEqual("recordID,corporationID,corporationName,startDate", info.Result.RowSet.Columns);

            Assert.IsNotNull(info.Result.RowSet.Rows);
            Assert.AreEqual(6, info.Result.RowSet.Rows.Count);
            
            Assert.AreEqual(19437644, info.Result.RowSet.Rows[0].RecordId);
            Assert.AreEqual(667531913, info.Result.RowSet.Rows[0].CorporationId);
            Assert.AreEqual("GoonWaffe", info.Result.RowSet.Rows[0].CorporationName);
            Assert.AreEqual("2012-04-10 13:05:00", info.Result.RowSet.Rows[0].StartDate);

            Assert.AreEqual(17668090, info.Result.RowSet.Rows[1].RecordId);
            Assert.AreEqual(1000080, info.Result.RowSet.Rows[1].CorporationId);
            Assert.AreEqual("Ministry of War", info.Result.RowSet.Rows[1].CorporationName);
            Assert.AreEqual("2011-08-18 15:20:00", info.Result.RowSet.Rows[1].StartDate);

            Assert.AreEqual(13108243, info.Result.RowSet.Rows[2].RecordId);
            Assert.AreEqual(667531913, info.Result.RowSet.Rows[2].CorporationId);
            Assert.AreEqual("GoonWaffe", info.Result.RowSet.Rows[2].CorporationName);
            Assert.AreEqual("2010-03-01 02:50:00", info.Result.RowSet.Rows[2].StartDate);

            Assert.AreEqual(11340919, info.Result.RowSet.Rows[3].RecordId);
            Assert.AreEqual(1000080, info.Result.RowSet.Rows[3].CorporationId);
            Assert.AreEqual("Ministry of War", info.Result.RowSet.Rows[3].CorporationName);
            Assert.AreEqual("2009-08-23 02:20:00", info.Result.RowSet.Rows[3].StartDate);

            Assert.AreEqual(7089510, info.Result.RowSet.Rows[4].RecordId);
            Assert.AreEqual(749147334, info.Result.RowSet.Rows[4].CorporationId);
            Assert.AreEqual("GoonFleet", info.Result.RowSet.Rows[4].CorporationName);
            Assert.AreEqual("2009-02-06 02:58:00", info.Result.RowSet.Rows[4].StartDate);

            Assert.AreEqual(7089509, info.Result.RowSet.Rows[5].RecordId);
            Assert.AreEqual(1000166, info.Result.RowSet.Rows[5].CorporationId);
            Assert.AreEqual("Imperial Academy", info.Result.RowSet.Rows[5].CorporationName);
            Assert.AreEqual("2009-02-05 16:57:00", info.Result.RowSet.Rows[5].StartDate);
        }

        [TestMethod]
        public void CharacterInfo_XMLLookup()
        {
            ICharacterXmlDataProvider cxdp = Injector.Create<ICharacterXmlDataProvider>();
            Assert.IsNotNull(cxdp);

            int cid = cxdp.GetCharacterId("Viktorie Lucilla");
            Assert.AreEqual(1170031179, cid);

            CharacterInfoApi info = cxdp.GetCharacterInfo(cid);
            Assert.IsNotNull(info);

            Assert.IsNotNull(info.Result);
            Assert.AreEqual(1170031179, info.Result.CharacterId);
            Assert.AreEqual("Viktorie Lucilla", info.Result.CharacterName);
        }
    }
}
