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
    public class XMLAllianceList
    {
        public static string SampleXml { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            XMLAllianceList.SampleXml = File.ReadAllText("samples\\AllianceList.xml");
        }

        [TestMethod]
        public void AllianceList_TestSerialization()
        {
            AllianceListApi api = XmlSerialization.DeserializeString<AllianceListApi>(XMLAllianceList.SampleXml);
            Assert.IsNotNull(api);

            Assert.AreEqual(2, api.Version);
            Assert.AreEqual("2015-09-17 13:49:09", api.CurrentTime);
            Assert.AreEqual("2015-09-17 14:20:19", api.CachedUntil);

            Assert.IsNotNull(api.Result);
            
            Assert.IsNotNull(api.Result.RowSet);
            Assert.AreEqual("alliances", api.Result.RowSet.Name);
            Assert.AreEqual("allianceID", api.Result.RowSet.Key);
            Assert.AreEqual("name,shortName,allianceID,executorCorpID,memberCount,startDate", api.Result.RowSet.Columns);

            Assert.IsNotNull(api.Result.RowSet.Rows);

            Assert.IsTrue(api.Result.RowSet.Rows.Count > 0);

            Assert.AreEqual("Goonswarm Federation", api.Result.RowSet.Rows[0].Name);
            Assert.AreEqual("CONDI", api.Result.RowSet.Rows[0].ShortName);
            Assert.AreEqual(1354830081, api.Result.RowSet.Rows[0].AllianceId);
            Assert.AreEqual(1344654522, api.Result.RowSet.Rows[0].ExecutorCorpId);
            Assert.AreEqual(14514, api.Result.RowSet.Rows[0].MemberCount);
            Assert.AreEqual("2010-06-01 05:36:00", api.Result.RowSet.Rows[0].StartDate);

            Assert.IsNotNull(api.Result.RowSet.Rows[0].MemberCorporations);
            Assert.AreEqual("memberCorporations", api.Result.RowSet.Rows[0].MemberCorporations.Name);
            Assert.AreEqual("corporationID", api.Result.RowSet.Rows[0].MemberCorporations.Key);
            Assert.AreEqual("corporationID,startDate", api.Result.RowSet.Rows[0].MemberCorporations.Columns);

            Assert.IsTrue(api.Result.RowSet.Rows[0].MemberCorporations.Rows.Count > 0);

            Assert.AreEqual(98002506, api.Result.RowSet.Rows[0].MemberCorporations.Rows[0].CorporationId);
            Assert.AreEqual("2014-11-12 22:20:00", api.Result.RowSet.Rows[0].MemberCorporations.Rows[0].StartDate);
        }

        [TestMethod]
        public void AllianceList_XMLLookup()
        {
            ICharacterXmlDataProvider cxdp = Injector.Create<ICharacterXmlDataProvider>();
            Assert.IsNotNull(cxdp);

            int cid = cxdp.GetCharacterId("Viktorie Lucilla");
            Assert.AreEqual(1170031179, cid);

            CharacterInfoApi info = cxdp.GetCharacterInfo(cid);
            Assert.IsNotNull(info);

            Assert.IsNotNull(info.Result);

            ICorporationXmlDataProvider xxdp = Injector.Create<ICorporationXmlDataProvider>();
            Assert.IsNotNull(xxdp);

            CorporationSheetApi corp = xxdp.GetCorporationInfo(info.Result.CorporationId);
            Assert.IsNotNull(corp);

            Assert.AreEqual(info.Result.Corporation, corp.Result.CorporationName);

            IAllianceXmlDataProvider axdp = Injector.Create<IAllianceXmlDataProvider>();
            Assert.IsNotNull(axdp);

            AllianceRow alli = axdp.GetAllianceData(corp.Result.AllianceId);
            Assert.IsNotNull(alli);

            Assert.AreEqual(corp.Result.AllianceName, alli.Name);
        }
    }
}