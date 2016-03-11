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
    public class XMLCallList
    {
        public static string SampleXml { get; set; }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            XMLCallList.SampleXml = File.ReadAllText("samples\\CallList.xml");
        }

        [TestMethod]
        public void CallList_TestSerialization()
        {
            CallListApi api = XmlSerialization.DeserializeString<CallListApi>(XMLCallList.SampleXml);
            Assert.IsNotNull(api);

            Assert.AreEqual("2015-09-21 21:38:32", api.CurrentTime);
            Assert.AreEqual("2015-09-22 03:35:32", api.CachedUntil);
            Assert.AreEqual(2, api.Version);

            Assert.IsNotNull(api.Result);

            Assert.IsNotNull(api.Result.CallGroups);
            Assert.AreEqual("callGroups", api.Result.CallGroups.Name);
            Assert.AreEqual("groupID", api.Result.CallGroups.Key);
            Assert.AreEqual("groupID,name,description", api.Result.CallGroups.Columns);
            
            Assert.IsNotNull(api.Result.CallGroups.Rows);
            Assert.AreEqual(7, api.Result.CallGroups.Rows.Count());

            CallGroupRow firstCallGroup = api.Result.CallGroups.Rows.FirstOrDefault();
            Assert.IsNotNull(firstCallGroup);

            Assert.AreEqual(1, firstCallGroup.GroupId);
            Assert.AreEqual("Account and Market", firstCallGroup.Name);
            Assert.AreEqual("Market Orders, account balance and journal history.", firstCallGroup.Description);

            Assert.IsNotNull(api.Result.Calls);
            Assert.AreEqual("calls", api.Result.Calls.Name);
            Assert.AreEqual("accessMask,type", api.Result.Calls.Key);
            Assert.AreEqual("accessMask,type,name,groupID,description", api.Result.Calls.Columns);

            Assert.IsNotNull(api.Result.Calls.Rows);
            Assert.AreEqual(57, api.Result.Calls.Rows.Count());

            CallRow firstCall = api.Result.Calls.Rows.FirstOrDefault();
            Assert.IsNotNull(firstCall);

            Assert.AreEqual(536870912, firstCall.AccessMask);
            Assert.AreEqual("Character", firstCall.CallType);
            Assert.AreEqual("ChatChannels", firstCall.Name);
            Assert.AreEqual(7, firstCall.GroupId);
            Assert.AreEqual("List of all chat channels the character owns or is an operator of.", firstCall.Description);
        }

        [TestMethod]
        public void CallList_XMLLookup()
        {
            ICallListXmlDataProvider xdp = Injector.Create<ICallListXmlDataProvider>();
            Assert.IsNotNull(xdp);

            CallListApi api = xdp.GetCallList();
            Assert.IsNotNull(api);

            Assert.AreEqual(7, api.Result.CallGroups.Rows.Count());
        }
    }
}
