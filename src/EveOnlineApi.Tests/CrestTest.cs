using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EveScanner.IoC;
using EveScanner.Interfaces;
using EveScanner.Core;

namespace EveOnlineApi.Tests
{
    [TestClass]
    public class CrestTest
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Injector.Register<IWebClient>(typeof(WebClient));
        }

        [TestMethod]
        public void GetBuySellOrders()
        {
            EveOnlineCrestApi api = new EveOnlineCrestApi();
            var items = api.GetBuySellOrders(10000002, 40519);
        }
    }
}
