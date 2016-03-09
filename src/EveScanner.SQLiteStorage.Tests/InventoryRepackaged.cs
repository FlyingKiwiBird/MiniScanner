namespace EveScanner.SQLiteStorage.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EveScanner.Interfaces.Providers;
    using EveScanner.IoC;

    [TestClass]
    public class InventoryRepackaged
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Injector.Register<IInventoryRepackagedProvider>(typeof(SQLiteExtraDataProvider));
        }

        [TestMethod]
        public void InventoryRepackagedByGroupId()
        {
            var irp = Injector.Create<IInventoryRepackagedProvider>();
            Assert.IsNotNull(irp);

            var pv25 = irp.GetRepackagedVolumesForGroup(25);
            Assert.IsNotNull(pv25);

            Assert.AreEqual(25, pv25.GroupId);
            Assert.AreEqual(2500.0, pv25.Volume);
        }
    }
}
