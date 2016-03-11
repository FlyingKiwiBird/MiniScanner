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

            Assert.IsTrue(api.Result.RowSet.Rows.Count() > 0);

            AllianceRow firstAlliance = api.Result.RowSet.Rows.FirstOrDefault();
            Assert.IsNotNull(firstAlliance);

            Assert.AreEqual("Goonswarm Federation", firstAlliance.Name);
            Assert.AreEqual("CONDI", firstAlliance.ShortName);
            Assert.AreEqual(1354830081, firstAlliance.AllianceId);
            Assert.AreEqual(1344654522, firstAlliance.ExecutorCorpId);
            Assert.AreEqual(14514, firstAlliance.MemberCount);
            Assert.AreEqual("2010-06-01 05:36:00", firstAlliance.StartDate);

            Assert.IsNotNull(firstAlliance.MemberCorporations);
            Assert.AreEqual("memberCorporations", firstAlliance.MemberCorporations.Name);
            Assert.AreEqual("corporationID", firstAlliance.MemberCorporations.Key);
            Assert.AreEqual("corporationID,startDate", firstAlliance.MemberCorporations.Columns);

            Assert.IsTrue(firstAlliance.MemberCorporations.Rows.Count() > 0);

            MemberCorporationRow firstMember = firstAlliance.MemberCorporations.Rows.FirstOrDefault();
            Assert.IsNotNull(firstMember);

            Assert.AreEqual(98002506, firstMember.CorporationId);
            Assert.AreEqual("2014-11-12 22:20:00", firstMember.StartDate);
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