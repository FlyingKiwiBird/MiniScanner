using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using EveOnlineApi.Entities.Xml;
using EveOnlineApi.Common;
using EveOnlineApi.Entities;

namespace EveOnlineApi.Tests
{
    [TestClass]
    public class XmlContactList
    {
        public static string SampleXml { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            XmlContactList.SampleXml = File.ReadAllText("samples\\ContactList.xml");
        }

        [TestMethod]
        public void ContactList_TestSerialization()
        {
            ContactListApi api = XmlSerialization.DeserializeString<ContactListApi>(XmlContactList.SampleXml);
            Assert.IsNotNull(api);

            Assert.AreEqual(2, api.Version);
            Assert.AreEqual("2015-10-06 13:37:14", api.CurrentTime);
            Assert.AreEqual("2015-10-06 13:52:10", api.CachedUntil);

            Assert.IsNotNull(api.Result);

            Assert.IsNotNull(api.Result.ContactList);
            Assert.AreEqual("contactList", api.Result.ContactList.Name);
            Assert.AreEqual("contactID", api.Result.ContactList.Key);
            Assert.AreEqual("contactID,contactName,standing,contactTypeID,labelMask,inWatchlist", api.Result.ContactList.Columns);
            Assert.IsNotNull(api.Result.ContactList.Rows);
            Assert.AreEqual(2, api.Result.ContactList.Rows.Count);
            Assert.AreEqual(1, api.Result.ContactList.Rows[0].ContactId);
            Assert.AreEqual("Viktorie Lucilla", api.Result.ContactList.Rows[0].ContactName);
            Assert.AreEqual(10, api.Result.ContactList.Rows[0].Standing);
            Assert.AreEqual(1374, api.Result.ContactList.Rows[0].ContactTypeId);
            Assert.AreEqual(0, api.Result.ContactList.Rows[0].LabelMask);
            Assert.AreEqual("False", api.Result.ContactList.Rows[0].InWatchlist);
            Assert.AreEqual("True", api.Result.ContactList.Rows[1].InWatchlist);

            Assert.IsNotNull(api.Result.ContactLabels);
            Assert.IsNotNull(api.Result.ContactLabels.Rows);
            Assert.AreEqual(0, api.Result.ContactLabels.Rows.Count);
            
            Assert.IsNotNull(api.Result.CorporateContactList);
            Assert.AreEqual("corporateContactList", api.Result.CorporateContactList.Name);
            Assert.AreEqual("contactID", api.Result.CorporateContactList.Key);
            Assert.AreEqual("contactID,contactName,standing,contactTypeID,labelMask", api.Result.CorporateContactList.Columns);
            Assert.IsNotNull(api.Result.CorporateContactList.Rows);
            Assert.IsTrue(api.Result.CorporateContactList.Rows.Count > 0);
            Assert.AreEqual(90171277, api.Result.CorporateContactList.Rows[0].ContactId);
            Assert.AreEqual("Phigmeta", api.Result.CorporateContactList.Rows[0].ContactName);
            Assert.AreEqual(-10, api.Result.CorporateContactList.Rows[0].Standing);
            Assert.AreEqual(1378, api.Result.CorporateContactList.Rows[0].ContactTypeId);
            Assert.AreEqual(0, api.Result.CorporateContactList.Rows[0].LabelMask);

            Assert.IsNotNull(api.Result.CorporateContactLabels);
            Assert.AreEqual(0, api.Result.CorporateContactLabels.Rows.Count);
            
            Assert.IsNotNull(api.Result.AllianceContactList);
            Assert.AreEqual("allianceContactList", api.Result.AllianceContactList.Name);
            Assert.AreEqual("contactID", api.Result.AllianceContactList.Key);
            Assert.AreEqual("contactID,contactName,standing,contactTypeID,labelMask", api.Result.AllianceContactList.Columns);
            Assert.IsNotNull(api.Result.AllianceContactList.Rows);
            Assert.IsTrue(api.Result.AllianceContactList.Rows.Count > 0);

            Assert.IsNotNull(api.Result.AllianceContactLabels);
            Assert.IsNotNull(api.Result.AllianceContactLabels.Rows);
            Assert.AreEqual(4, api.Result.AllianceContactLabels.Rows.Count);
            Assert.AreEqual(1, api.Result.AllianceContactLabels.Rows[0].Id);
            Assert.AreEqual("Q Pirates", api.Result.AllianceContactLabels.Rows[0].Name);
        }

        [TestMethod]
        public void TestStandingsRetrieval()
        {
            Standings s = Standings.GetStandings("Viktorie Lucilla", EntityType.Character);

            Assert.IsNotNull(s);
            Assert.AreEqual(10, s.DerivedStanding);
        }
    }
}
