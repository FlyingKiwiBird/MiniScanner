using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EveOnlineApi.Common;
using EveOnlineApi.Entities;
using EveOnlineApi.Entities.Xml;
using EveOnlineApi.Interfaces.Xml;
using EveScanner.IoC;

namespace EveOnlineApi.Tests
{
    [TestClass]
    public class XMLCharacterId
    {
        public static string SampleXml { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            XMLCharacterId.SampleXml = File.ReadAllText("samples\\CharacterId.xml");
        }

        [TestMethod]
        public void CharacterId_TestSerialization()
        {
            CharacterIdApi api = XmlSerialization.DeserializeString<CharacterIdApi>(XMLCharacterId.SampleXml);
            Assert.IsNotNull(api);

            Assert.AreEqual(2, api.Version);
            Assert.AreEqual("2015-09-17 02:50:49", api.CurrentTime);
            Assert.AreEqual("2015-10-17 02:50:49", api.CachedUntil);

            Assert.IsNotNull(api.Result);
            
            Assert.IsNotNull(api.Result.RowSet);
            Assert.AreEqual("characters", api.Result.RowSet.Name);
            Assert.AreEqual("characterID", api.Result.RowSet.Key);
            Assert.AreEqual("name,characterID", api.Result.RowSet.Columns);

            Assert.IsNotNull(api.Result.RowSet.Rows);

            Assert.AreEqual(1, api.Result.RowSet.Rows.Count);

            Assert.AreEqual(1170031179, api.Result.RowSet.Rows[0].CharacterId);
            Assert.AreEqual("Viktorie Lucilla", api.Result.RowSet.Rows[0].Name);
        }

        [TestMethod]
        public void CharacterId_XMLLookup()
        {
            ICharacterXmlDataProvider cxdp = Injector.Create<ICharacterXmlDataProvider>();
            Assert.IsNotNull(cxdp);

            int cid = cxdp.GetCharacterId("Viktorie Lucilla");
            Assert.AreEqual(1170031179, cid);
        }
    }
}