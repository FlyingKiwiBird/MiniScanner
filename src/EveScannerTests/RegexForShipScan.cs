using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using EveScanner.Core;

namespace EveScannerTests
{
    [TestClass]
    public class RegexForShipScan
    {
        private bool TestMethod(string sample)
        {
            return Validators.CheckForCargoScan(sample);
        }

        [TestMethod]
        public void SingleLineSuccess()
        {
            string sample = @"109 Energy Cells";
            Assert.IsTrue(this.TestMethod(sample));
        }

        [TestMethod]
        public void MultiLineSuccess()
        {
            string sample = @"109 Energy Cells
1 High-Tech Small Arms
1 High-Tech Manufacturing Tools
48 Construction Alloy";
            Assert.IsTrue(this.TestMethod(sample));
        }

        [TestMethod]
        public void LongComplicatedSuccess()
        {
            string sample = @"45000 Photonic Metamaterials
1 Laser Focusing Crystals Blueprint (Original)
1 Oscillator Capacitor Unit Blueprint (Original)
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
1 Plasma Thruster Blueprint (Original)
913500 Tungsten Carbide
1 Plasma Thruster Blueprint (Original)
1 Plasma Thruster Blueprint (Original)
1 Oscillator Capacitor Unit Blueprint (Original)";
            Assert.IsTrue(this.TestMethod(sample));
        }

        [TestMethod]
        public void Failure_CopyLine()
        {
            string sample = @"88.93K | 15.72 m3 | 4 stacks | http://evepraisal.com/e/7111070 | Ashab -> Madirmilire";
            Assert.IsFalse(this.TestMethod(sample));
        }

        [TestMethod]
        public void Failure_PatternMiss()
        {
            string sample = @"3 pies
x
4 goats";
            Assert.IsFalse(this.TestMethod(sample));
        }

    }
}