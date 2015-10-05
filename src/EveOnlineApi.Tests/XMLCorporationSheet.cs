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
    public class XMLCorporationSheet
    {
        public static string SampleXml { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            XMLCorporationSheet.SampleXml = File.ReadAllText("samples\\CorporationSheet.xml");
        }

        [TestMethod]
        public void CorporationSheet_TestSerialization()
        {
            CorporationSheetApi api = XmlSerialization.DeserializeString<CorporationSheetApi>(XMLCorporationSheet.SampleXml);
            Assert.IsNotNull(api);

            Assert.AreEqual(2, api.Version);
            Assert.AreEqual("2015-09-17 13:27:17", api.CurrentTime);
            Assert.AreEqual("2015-09-17 16:46:26", api.CachedUntil);

            Assert.IsNotNull(api.Result);
            Assert.AreEqual(667531913, api.Result.CorporationId);
            Assert.AreEqual("GoonWaffe", api.Result.CorporationName);
            Assert.AreEqual("GEWNS", api.Result.Ticker);
            Assert.AreEqual(443630591, api.Result.CeoId);
            Assert.AreEqual("The Mittani", api.Result.CeoName);
            Assert.AreEqual(60002104, api.Result.StationId);
            Assert.AreEqual("S-U8A4 V - Moon 1 - Ishukone Corporation Factory", api.Result.StationName);
            Assert.AreEqual("FNLN: i killed my moms and my god. j/k. god isn't real and is a human concept. do babys ever think of god? no. because hes fake. baby geniuses was true... i guess.<br>dintlu: babies they cannot talk. if they know of god they cannot communicate of god. in growing they learn about adults needs to control information: Knowledge is Power. the baby genious indulges the moronic parent<br>FNLN: the baby thinks. it looks. ity understands. baby becomes ahigher being. baby is chrsit itself....??? maybe in your world. but you're in our world now.<br>dintlu: god is the ultimate. he is every baby<br>dintlu: also in every baby. he is their past and present. not their future. babys are left by god and become neitchze<br>dintlu: the baby grows and the baby is dead but not dead. only god is dead<br>FNLN: the baby. it always was about the baby. never about me, or her... just. ababy.<br>dintlu: you communicate. you are grown, not a baby, godless adult: resized devil<br>FNLN: holy shit", api.Result.Description);
            Assert.AreEqual(string.Empty, api.Result.Url);
            Assert.AreEqual(1354830081, api.Result.AllianceId);
            Assert.AreEqual(0, api.Result.FactionId);
            Assert.AreEqual("Goonswarm Federation", api.Result.AllianceName);
            Assert.AreEqual(15, api.Result.TaxRate);
            Assert.AreEqual(2830, api.Result.MemberCount);
            Assert.AreEqual(100000, api.Result.Shares);

            Assert.IsNotNull(api.Result.Logo);
            Assert.AreEqual(0, api.Result.Logo.GraphicId);
            Assert.AreEqual(558, api.Result.Logo.Shape1);
            Assert.AreEqual(533, api.Result.Logo.Shape2);
            Assert.AreEqual(533, api.Result.Logo.Shape3);
            Assert.AreEqual(673, api.Result.Logo.Color1);
            Assert.AreEqual(680, api.Result.Logo.Color2);
            Assert.AreEqual(680, api.Result.Logo.Color3);
        }

        [TestMethod]
        public void CorporationSheet_XMLLookup()
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
        }
    }
}