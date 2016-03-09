namespace EveScanner.SQLiteStorage.Tests
{
    using EveScanner.Interfaces.Providers;
    using EveScanner.IoC;
    using EveScanner.SQLiteStorage;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    [TestClass]
    public class ItemAppraisalData
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Injector.Register<IItemAppraisalDataProvider>(typeof(SQLiteItemAppraisalProvider));
            Injector.Register<IInventoryTypeProvider>(typeof(SQLiteStaticDataProvider));
            Injector.Register<IInventoryRepackagedProvider>(typeof(SQLiteExtraDataProvider));
        }

        [TestMethod]
        public void ItemAppraisalByTypeId()
        {
            var iadp = Injector.Create<IItemAppraisalDataProvider>();
            Assert.IsNotNull(iadp);

            var item = iadp.GetItemAppraisalByTypeId(587);
            Assert.IsNotNull(item);

            Assert.AreEqual(587, item.TypeID);
            Assert.AreEqual("Rifter", item.TypeName);
            Assert.AreEqual(27289, item.Volume);
            Assert.AreEqual(2500, item.RepackagedVolume);
        }

        [TestMethod]
        public void ItemAppraisalByTypeName()
        {
            var iadp = Injector.Create<IItemAppraisalDataProvider>();
            Assert.IsNotNull(iadp);

            var item = iadp.GetItemAppraisalByTypeName("Rifter");
            Assert.IsNotNull(item);

            Assert.AreEqual(587, item.TypeID);
            Assert.AreEqual("Rifter", item.TypeName);
            Assert.AreEqual(27289, item.Volume);
            Assert.AreEqual(2500, item.RepackagedVolume);
        }

        [TestMethod]
        public void ItemAppraisalByTypeNameFailure()
        {
            var iadp = Injector.Create<IItemAppraisalDataProvider>();
            Assert.IsNotNull(iadp);

            var item = iadp.GetItemAppraisalByTypeName("NotARifter");
            Assert.IsNull(item);
        }

        [TestMethod]
        public void GetItemAppraisalsForItemList()
        {
            var iadp = Injector.Create<IItemAppraisalDataProvider>();
            Assert.IsNotNull(iadp);

            string input = @"45000 Photonic Metamaterials
1 Laser Focusing Crystals Blueprint (Original)
1 Oscillator Capacitor Unit Blueprint (Copy)
1 Oscillator Capacitor Unit Blueprint (Original)
1 Plasma Thruster Blueprint (Original)
1 Plasma Thruster Blueprint (Original)
54000 Plasmonic Metamaterials
24000 Hypersynaptic Fibers
1 Electrolytic Capacitor Unit Blueprint (Original)
1 Oscillator Capacitor Unit Blueprint (Original)
1 Electrolytic Capacitor Unit Blueprint (Original)
1 Plasma Thruster Blueprint (Original)
1 Laser Focusing Crystals Blueprint (Original)
1 Laser Focusing Crystals Blueprint (Original)
1 Linear Shield Emitter Blueprint (Original)
693000 Fullerides
60000 Nanotransistors
1 Plasma Thruster Blueprint (Original)
1413000 Fernite Carbide
1 Plasma Thruster Blueprint (Original)
75000 Ferrogel
1 Plasma Thruster Blueprint (Original)
1 Electrolytic Capacitor Unit Blueprint (Original)
1 Electrolytic Capacitor Unit Blueprint (Original)
607500 Crystalline Carbonide
135000 Phenolic Composites
202500 Sylramic Fibers
1 Oscillator Capacitor Unit Blueprint (Original)
1 Linear Shield Emitter Blueprint (Original)
1 Ladar Sensor Cluster Blueprint (Original)
1 Linear Shield Emitter Blueprint (Original)
1 Electrolytic Capacitor Unit Blueprint (Original)
1 Linear Shield Emitter Blueprint (Original)
1 Linear Shield Emitter Blueprint (Original)
1 Plasma Thruster Blueprint (Original)
1 Electrolytic Capacitor Unit Blueprint (Original)
1q Plasma Thruster Blueprint (Original)
913500 Tungsten Carbidess
1 Plasma Thruster Blueprint (Original)
1 Plasma Thruster Blueprint (Original)
1 Oscillator Capacitor Unit Blueprint (Orixginal)";

            var output = iadp.GetItemAppraisalsForItemList(input);
            Assert.IsNotNull(output);

            var outputArray = output.ToArray();
            Assert.IsNotNull(outputArray);

            Assert.AreEqual(41, outputArray.Length);

            Assert.AreEqual(38, outputArray.Where(x => !x.IsError).Count());

            Assert.AreEqual(false, outputArray[1].IsBlueprintCopy);
            Assert.AreEqual(true, outputArray[2].IsBlueprintCopy);
            Assert.AreEqual(true, outputArray[36].IsError);
            Assert.AreEqual(true, outputArray[37].IsError);
            Assert.AreEqual(true, outputArray[40].IsError);
        }
    }
}
