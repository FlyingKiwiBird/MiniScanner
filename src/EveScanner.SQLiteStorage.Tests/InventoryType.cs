namespace EveScanner.SQLiteStorage.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EveScanner.Interfaces.Providers;
    using EveScanner.Interfaces.SDE;
    using EveScanner.IoC;

    [TestClass]
    public class InventoryType
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Injector.Register<IInventoryTypeProvider>(typeof(SQLiteStaticDataProvider));
        }

        [TestMethod]
        public void InventoryTypeByTypeName()
        {
            var iitp = Injector.Create<IInventoryTypeProvider>();
            Assert.IsNotNull(iitp);

            var rifter = iitp.GetInventoryTypeByTypeName("Rifter");
            this.CheckRifter(rifter);
        }

        [TestMethod]
        public void InventoryTypeByTypeId()
        {
            var iitp = Injector.Create<IInventoryTypeProvider>();
            Assert.IsNotNull(iitp);

            var rifter = iitp.GetInventoryTypeByTypeId(587);
            this.CheckRifter(rifter);
        }

        [TestMethod]
        public void InventoryTypesByGroupId()
        {
            var iitp = Injector.Create<IInventoryTypeProvider>();
            Assert.IsNotNull(iitp);

            var output = iitp.GetInventoryTypesByGroupId(25);
            Assert.IsNotNull(output);

            var array = output.ToArray();
            Assert.AreEqual(76, array.Length);

            this.CheckRifter(array.Where(x => x.TypeId == 587).First());
        }

        private void CheckRifter(IInventoryType itype)
        {
            Assert.IsNotNull(itype);
            Assert.AreEqual(587, itype.TypeId);
            Assert.AreEqual(25, itype.GroupId);
            Assert.AreEqual("Rifter", itype.TypeName);
            Assert.AreEqual("The Rifter is a very powerful combat frigate and can easily tackle the best frigates out there. It has gone through many radical design phases since its inauguration during the Minmatar Rebellion. The Rifter has a wide variety of offensive capabilities, making it an unpredictable and deadly adversary.", itype.Description);
            Assert.AreEqual(1067000.0, itype.Mass);
            Assert.AreEqual(27289.0, itype.Volume);
            Assert.AreEqual(140.0, itype.Capacity);
            Assert.AreEqual(1, itype.PortionSize);
            Assert.AreEqual(2, itype.RaceId);
            Assert.AreEqual(null, itype.BasePrice);
            Assert.AreEqual(true, itype.Published);
            Assert.AreEqual(64, itype.MarketGroupId);
            Assert.AreEqual(null, itype.IconId);
            Assert.AreEqual(20078, itype.SoundId);
            Assert.AreEqual(46, itype.GraphicId);
        }
    }
}
