using System;
using System.IO;
using System.Linq;
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
            Assert.AreEqual(6, info.Result.RowSet.Rows.Count());

            CharacterEmploymentRow firstJob = info.Result.RowSet.Rows.ElementAtOrDefault(0);
            Assert.IsNotNull(firstJob);

            Assert.AreEqual(19437644, firstJob.RecordId);
            Assert.AreEqual(667531913, firstJob.CorporationId);
            Assert.AreEqual("GoonWaffe", firstJob.CorporationName);
            Assert.AreEqual("2012-04-10 13:05:00", firstJob.StartDate);

            CharacterEmploymentRow secondJob = info.Result.RowSet.Rows.ElementAtOrDefault(1);
            Assert.IsNotNull(secondJob);

            Assert.AreEqual(17668090, secondJob.RecordId);
            Assert.AreEqual(1000080, secondJob.CorporationId);
            Assert.AreEqual("Ministry of War", secondJob.CorporationName);
            Assert.AreEqual("2011-08-18 15:20:00", secondJob.StartDate);

            CharacterEmploymentRow thirdJob = info.Result.RowSet.Rows.ElementAtOrDefault(2);
            Assert.IsNotNull(thirdJob);

            Assert.AreEqual(13108243, thirdJob.RecordId);
            Assert.AreEqual(667531913, thirdJob.CorporationId);
            Assert.AreEqual("GoonWaffe", thirdJob.CorporationName);
            Assert.AreEqual("2010-03-01 02:50:00", thirdJob.StartDate);

            CharacterEmploymentRow fourthJob = info.Result.RowSet.Rows.ElementAtOrDefault(3);
            Assert.IsNotNull(fourthJob);

            Assert.AreEqual(11340919, fourthJob.RecordId);
            Assert.AreEqual(1000080, fourthJob.CorporationId);
            Assert.AreEqual("Ministry of War", fourthJob.CorporationName);
            Assert.AreEqual("2009-08-23 02:20:00", fourthJob.StartDate);

            CharacterEmploymentRow fifthJob = info.Result.RowSet.Rows.ElementAtOrDefault(4);
            Assert.IsNotNull(fifthJob);

            Assert.AreEqual(7089510, fifthJob.RecordId);
            Assert.AreEqual(749147334, fifthJob.CorporationId);
            Assert.AreEqual("GoonFleet", fifthJob.CorporationName);
            Assert.AreEqual("2009-02-06 02:58:00", fifthJob.StartDate);

            CharacterEmploymentRow sixthJob = info.Result.RowSet.Rows.ElementAtOrDefault(5);
            Assert.IsNotNull(sixthJob);

            Assert.AreEqual(7089509, sixthJob.RecordId);
            Assert.AreEqual(1000166, sixthJob.CorporationId);
            Assert.AreEqual("Imperial Academy", sixthJob.CorporationName);
            Assert.AreEqual("2009-02-05 16:57:00", sixthJob.StartDate);
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
