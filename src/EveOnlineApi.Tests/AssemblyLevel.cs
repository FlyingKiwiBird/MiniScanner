using EveOnlineApi.Interfaces;
using EveOnlineApi.Interfaces.Xml;
using EveScanner.IoC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EveOnlineApi.Tests
{
    [TestClass]
    public class AssemblyLevel
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            // Configure XML Handling
            Injector.Register<IAllianceXmlDataProvider>(typeof(FileBackedEveOnlineXmlApi));
            Injector.Register<ICharacterXmlDataProvider>(typeof(FileBackedEveOnlineXmlApi));
            Injector.Register<ICorporationXmlDataProvider>(typeof(FileBackedEveOnlineXmlApi));

            // Configure other API Entity Injections
            Injector.Register<IAllianceDataProvider>(typeof(XmlBackedEveOnlineApi));
            Injector.Register<ICharacterDataProvider>(typeof(XmlBackedEveOnlineApi));
            Injector.Register<ICorporationDataProvider>(typeof(XmlBackedEveOnlineApi));        }
    }
}
