namespace EveScanner.SQLiteStorage.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EveScanner.Interfaces.Providers;
    using EveScanner.Interfaces.SDE;
    using EveScanner.IoC;

    [TestClass]
    public class InventoryGroup
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Injector.Register<IInventoryGroupProvider>(typeof(SQLiteStaticDataProvider));
        }

        [TestMethod]
        public void InventoryGroupByIdTest()
        {
            IInventoryGroupProvider iigp = Injector.Create<IInventoryGroupProvider>();
            Assert.IsNotNull(iigp);

            var i = iigp.GetInventoryGroupById(1);
            this.CheckRecord1(i);
        }

        [TestMethod]
        public void InventoryGroupByNameTest()
        {
            IInventoryGroupProvider iigp = Injector.Create<IInventoryGroupProvider>();
            Assert.IsNotNull(iigp);

            var i = iigp.GetInventoryGroupByName("Character");
            this.CheckRecord1(i);

            var j = iigp.GetInventoryGroupByName("Harvestable Cloud");
            this.CheckRecord711(j);
        }

        [TestMethod]
        public void InventoryGroupByCategoryIdTest()
        {
            IInventoryGroupProvider iigp = Injector.Create<IInventoryGroupProvider>();
            Assert.IsNotNull(iigp);

            var i = iigp.GetInventoryGroupsByCategoryId(1);
            Assert.IsNotNull(i);

            var j = i.ToArray();
            Assert.AreEqual(4, j.Length);
            Assert.AreEqual(1, j.Where(x => x.GroupId == 1).Count());
            
            this.CheckRecord1(j.Where(x => x.GroupId == 1).First());
        }

        private void CheckRecord1(IInventoryGroup i)
        {
            Assert.IsNotNull(i);
            Assert.AreEqual(1, i.GroupId);
            Assert.AreEqual(1, i.CategoryId);
            Assert.AreEqual("Character", i.GroupName);
            Assert.IsNull(i.IconId);
            Assert.AreEqual(false, i.UseBasePrice);
            Assert.AreEqual(false, i.Anchored);
            Assert.AreEqual(false, i.Anchorable);
            Assert.AreEqual(false, i.FittableNonSingleton);
            Assert.AreEqual(false, i.Published);
        }

        private void CheckRecord711(IInventoryGroup i)
        {
            Assert.IsNotNull(i);
            Assert.AreEqual(711, i.GroupId);
            Assert.AreEqual(2, i.CategoryId);
            Assert.AreEqual("Harvestable Cloud", i.GroupName);
            Assert.AreEqual(0, i.IconId);
            Assert.AreEqual(false, i.UseBasePrice);
            Assert.AreEqual(false, i.Anchored);
            Assert.AreEqual(false, i.Anchorable);
            Assert.AreEqual(false, i.FittableNonSingleton);
            Assert.AreEqual(true, i.Published);
        }
    }
}
