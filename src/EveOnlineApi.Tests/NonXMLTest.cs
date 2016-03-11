using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EveOnlineApi.Common;
using EveOnlineApi.Entities;
using EveOnlineApi.Interfaces;

using EveScanner.IoC;

namespace EveOnlineApi.Tests
{
    [TestClass]
    public class EveCharacter
    {
        [TestMethod]
        public void EveCharacter_FullPath()
        {
            ICharacterDataProvider idp = Injector.Create<ICharacterDataProvider>();
            Assert.IsNotNull(idp);
            
            int characterId = idp.GetCharacterId("Viktorie Lucilla");
            Assert.IsTrue(characterId > 0);

            ICharacter vl = idp.GetCharacterInfo(characterId);
            Assert.IsNotNull(vl);

            Assert.AreEqual("Viktorie Lucilla", vl.Name);
            Assert.AreEqual(characterId, vl.Id);

            Assert.IsNotNull(vl.Corporation);
            Assert.AreEqual(667531913, vl.Corporation.Id);
            Assert.AreEqual("GoonWaffe", vl.Corporation.Name);
            Assert.AreEqual("GEWNS", vl.Corporation.Ticker);

            Assert.IsNotNull(vl.Corporation.CeoCharacter);
            Assert.AreEqual("The Mittani", vl.Corporation.CeoCharacter.Name);

            Assert.IsNotNull(vl.Corporation.Alliance);
            Assert.AreEqual(1354830081, vl.Corporation.Alliance.Id);
            Assert.AreEqual("Goonswarm Federation", vl.Corporation.Alliance.Name);
            Assert.AreEqual("CONDI", vl.Corporation.Alliance.ShortName);

            Assert.IsNotNull(vl.Corporation.Alliance.ExecutorCorporation);
            Assert.AreEqual("DJ's Retirement Fund", vl.Corporation.Alliance.ExecutorCorporation.Name);
            Assert.AreEqual(".FART", vl.Corporation.Alliance.ExecutorCorporation.Ticker);

            Assert.IsNotNull(vl.Corporation.Alliance.ExecutorCorporation.CeoCharacter);
            Assert.AreEqual("Retirement Fund Admin", vl.Corporation.Alliance.ExecutorCorporation.CeoCharacter.Name);
        }
    }
}
