using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace EveScanner.Core.Tests
{
    [TestClass]
    public class ScanRuleTests
    {
        [TestMethod]
        public void TestScanRuleSerialization()
        {
            ScanRuleEvaluator eve = this.GetRulesFile();
            Assert.IsNotNull(eve);

            Assert.AreEqual(3, eve.Rules.Count());

            ScanRule rule1 = eve.Rules.ElementAtOrDefault(0);
            Assert.IsNotNull(rule1);

            Assert.AreEqual(1, rule1.Id);
            Assert.AreEqual("Sample Rule Any", rule1.Name);
            Assert.AreEqual("any", rule1.MatchType);

            Assert.AreEqual(2, rule1.Criteria.Count());

            ScanRuleCriteria crit1 = rule1.Criteria.ElementAtOrDefault(0);
            Assert.IsNotNull(crit1);

            Assert.AreEqual(1, crit1.Id);
            Assert.AreEqual(rule1.Id, crit1.RuleId);
            Assert.AreEqual("item", crit1.MatchProperty);
            Assert.AreEqual("Dummy Item", crit1.MatchValue);
            Assert.AreEqual("gt", crit1.MatchCriteria);
            Assert.AreEqual("0", crit1.MatchQuantity);

            ScanRuleCriteria crit2 = rule1.Criteria.ElementAtOrDefault(1);
            Assert.IsNotNull(crit2);

            Assert.AreEqual(2, crit2.Id);
            Assert.AreEqual(rule1.Id, crit2.RuleId);
            Assert.AreEqual("item", crit2.MatchProperty);
            Assert.AreEqual("Dummy Item 2", crit2.MatchValue);
            Assert.AreEqual("lt", crit2.MatchCriteria);
            Assert.AreEqual("5", crit2.MatchQuantity);
        }

        [TestMethod]
        public void TestAnyDetection()
        {
            ScanResult r = new ScanResult(Guid.Empty, DateTime.Now, "1 Dummy Item", 3000000000000, 4123456789012, 1, 1, "http://goonfleet.com/?1", new[] { new ScanLine(1, "Dummy Item", false) }) { CharacterName = "T2 BPO", ShipType = "Providence - Freighter - Amarr", Notes = "Triggers T2 BPO Image", Location = "Perimeter -> Urlen" };
            Assert.IsNotNull(r);

            ScanRuleEvaluator eve = this.GetRulesFile();
            Assert.IsNotNull(eve);

            IEnumerable<EvaluationResult> output = eve.Evaluate(r);
            Assert.IsNotNull(output);

            Assert.AreEqual(2, output.Count());

            EvaluationResult result = output.ElementAtOrDefault(0);
            Assert.IsNotNull(result);

            Assert.AreEqual("tag", result.ResultType);
            Assert.AreEqual("Dummy Item any", result.ResultValue);
        }

        [TestMethod]
        public void TestAllDetection1()
        {
            ScanResult r = new ScanResult(Guid.Empty, DateTime.Now, "1 Dummy Item\r\n1 Dummy Item 2", 3000000000000, 4123456789012, 1, 1, "http://goonfleet.com/?1", new[] { new ScanLine(1, "Dummy Item", false), new ScanLine(1, "Dummy Item 2", false) }) { CharacterName = "T2 BPO", ShipType = "Providence - Freighter - Amarr", Notes = "Triggers T2 BPO Image", Location = "Perimeter -> Urlen" };
            Assert.IsNotNull(r);

            ScanRuleEvaluator eve = this.GetRulesFile();
            Assert.IsNotNull(eve);

            IEnumerable<EvaluationResult> output = eve.Evaluate(r);
            Assert.IsNotNull(output);

            Assert.AreEqual(2, output.Count());

            EvaluationResult result = output.ElementAtOrDefault(0);
            Assert.IsNotNull(result);

            Assert.AreEqual("tag", result.ResultType);
            Assert.AreEqual("Dummy Item any", result.ResultValue);

            result = output.ElementAtOrDefault(1);
            Assert.IsNotNull(result);

            Assert.AreEqual("tag", result.ResultType);
            Assert.AreEqual("Dummy Items All", result.ResultValue);
        }

        [TestMethod]
        public void TestAllDetection2()
        {
            ScanResult r = new ScanResult(Guid.Empty, DateTime.Now, "1 Dummy Item\r\n1 Dummy Item 2", 3000000000000, 4123456789012, 1, 1, "http://goonfleet.com/?1", new[] { new ScanLine(1, "Dummy Item", false), new ScanLine(69, "Exotic Dancers", false) }) { CharacterName = "Viktorie Lucilla", ShipType = "Providence - Freighter - Amarr", Notes = "Triggers T2 BPO Image", Location = "Perimeter -> Urlen" };
            Assert.IsNotNull(r);

            ScanRuleEvaluator eve = this.GetRulesFile();
            Assert.IsNotNull(eve);

            IEnumerable<EvaluationResult> output = eve.Evaluate(r);
            Assert.IsNotNull(output);

            Assert.AreEqual(4, output.Count());
        }

        private ScanRuleEvaluator GetRulesFile()
        {
            ScanRuleEvaluator eve = new ScanRuleEvaluator("rules.xml");
            Assert.IsNotNull(eve);
            return eve;
        }
    }
}
