using EveScanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace EveScanner.Core
{
    public class ScanRuleEvaluator
    {
        public ScanRuleEvaluator(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(ScanRules));
            object result = null;

            using (TextReader reader = new StreamReader(path))
            {
                result = xs.Deserialize(reader);
            }

            this.Rules = ((ScanRules)result).Rules;
        }

        public IEnumerable<ScanRule> Rules { get; set; }

        public IEnumerable<EvaluationResult> Evaluate(IScanResult result)
        {
            List<ScanRuleResult> output = new List<ScanRuleResult>();

            foreach (ScanRule rule in this.Rules)
            {
                if (rule.Evaluate(result))
                {
                    output.AddRange(rule.Results);
                }
            }

            return output.Select(x => new EvaluationResult() { ResultType = x.ResultType, ResultValue = x.ResultValue }).Distinct();
        }
    }

    public class EvaluationResult
    {
        public string ResultType { get; set; }
        public string ResultValue { get; set; }
         
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            EvaluationResult result = obj as EvaluationResult;

            if (result == null)
            {
                return false;
            }

            return this.ResultType.Equals(result.ResultType, StringComparison.OrdinalIgnoreCase) && this.ResultValue.Equals(result.ResultValue, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int output = 17;

                output = output * 19 + ResultType.GetHashCode();
                output = output * 23 + ResultValue.GetHashCode();

                return output;
            }
        }
    }

    [XmlRoot("rules")]
    public class ScanRules
    {
        /// <summary>
        /// This holds our criteria List so we get around CA2227. Yep.
        /// </summary>
        private readonly Collection<ScanRule> rules = new Collection<ScanRule>();

        [XmlElement("rule")]
        public Collection<ScanRule> Rules
        {
            get
            {
                return this.rules;
            }
        }
    }
    

    [XmlRoot("rule")]
    public class ScanRule
    {
        /// <summary>
        /// This holds our criteria List so we get around CA2227. Yep.
        /// </summary>
        private readonly Collection<ScanRuleCriteria> criteria = new Collection<ScanRuleCriteria>();

        /// <summary>
        /// And the same for results.
        /// </summary>
        private readonly Collection<ScanRuleResult> results = new Collection<ScanRuleResult>();

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("matchType")]
        public string MatchType { get; set; }

        [XmlElement("criteria")]
        public Collection<ScanRuleCriteria> Criteria {
            get
            {
                return this.criteria;
            }
        }

        [XmlElement("result")]
        public Collection<ScanRuleResult> Results
        {
            get
            {
                return this.results;
            }
        }

        public bool Evaluate(IScanResult result)
        {
            bool foundMatch = false;
            bool matchTypeAny = this.MatchType.ToLower(CultureInfo.InvariantCulture) == "any";

            foreach (ScanRuleCriteria criteria in this.Criteria)
            {
                bool interimResult = criteria.Evaluate(result);

                if (matchTypeAny && interimResult)
                {
                    return true;
                }

                else if (!matchTypeAny)
                {
                    if (!interimResult)
                    {
                        return false;
                    }

                    if (!foundMatch && interimResult)
                    {
                        foundMatch = true;
                    }
                }
            }

            return foundMatch;
        }
    }

    [XmlRoot("criteria")]
    public class ScanRuleCriteria
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("ruleId")]
        public int RuleId { get; set; }

        [XmlAttribute("matchProperty")]
        public string MatchProperty { get; set; }

        [XmlAttribute("matchCriteria")]
        public string MatchCriteria { get; set; }

        [XmlAttribute("matchValue")]
        public string MatchValue { get; set; }

        [XmlAttribute("matchQuantity")]
        public string MatchQuantity { get; set; }

        public bool Evaluate(IScanResult result)
        {
            string mpl = this.MatchProperty.ToLower(CultureInfo.InvariantCulture);

            switch (mpl)
            {
                case "buyvalue":
                case "sellvalue":
                case "standings":
                    decimal tempValue = (mpl == "buyvalue") ? result.BuyValue : (mpl == "sellvalue" ? result.SellValue : (result.Character == null || result.Character.Standings == null ? 0 : result.Character.Standings.DerivedStanding));
                    decimal quantityValue = Decimal.Parse(this.MatchQuantity, CultureInfo.InvariantCulture);
                    switch (this.MatchCriteria)
                    {
                        case "eq":
                            if (tempValue == quantityValue) return true;
                            break;
                        case "lt":
                            if (tempValue < quantityValue) return true;
                            break;
                        case "gt":
                            if (tempValue > quantityValue) return true;
                            break;
                        case "le":
                            if (tempValue <= quantityValue) return true;
                            break;
                        case "ge":
                            if (tempValue >= quantityValue) return true;
                            break;
                        default:
                            break;
                    }
                    break;
                case "shiptype":
                case "character":
                case "corporation":
                case "alliance":
                case "corporationname":
                case "alliancename":
                    string tempString = string.Empty;
                    if (mpl == "shiptype") tempString = result.ShipType;
                    else if (mpl == "character") tempString = result.CharacterName;
                    else if (mpl == "alliance") tempString = result.Character.Corporation.Alliance.ShortName;
                    else if (mpl == "corporation") tempString = result.Character.Corporation.Ticker;
                    else if (mpl == "alliancename") tempString = result.Character.Corporation.Alliance.Name;
                    else if (mpl == "corporationname") tempString = result.Character.Corporation.Name;

                    if (tempString == null) tempString = string.Empty;

                    switch (this.MatchCriteria)
                    {
                        case "eq":
                            if (tempString.Equals(this.MatchValue, StringComparison.OrdinalIgnoreCase)) return true;
                            break;
                        case "ne":
                            if (!tempString.Equals(this.MatchValue, StringComparison.OrdinalIgnoreCase)) return true;
                            break;
                        case "ct":
                            if (tempString.IndexOf(this.MatchValue, StringComparison.OrdinalIgnoreCase) > 0) return true;
                            break;
                        case "nf":
                            if (tempString.IndexOf(this.MatchValue, StringComparison.OrdinalIgnoreCase) == 0) return true;
                            break;
                        default:
                            break;
                    }
                    break;
                case "item":
                    int quantityInt = int.Parse(this.MatchQuantity, CultureInfo.InvariantCulture);
                    IEnumerable<ILineAppraisal> matchedLines = result.AppraisedLines.Where(x => x.TypeName == this.MatchValue);

                    switch (this.MatchCriteria)
                    {
                        case "ct":
                            if (matchedLines.Count() > 0) return true;
                            break;
                        case "nf":
                            if (matchedLines.Count() == 0) return true;
                            break;
                        case "eq":
                            if (matchedLines.Any(x => x.Quantity == quantityInt)) return true;
                            if (matchedLines.Count() == 0 && quantityInt == 0) return true;
                            break;
                        case "ne":
                            if (matchedLines.Any(x => x.Quantity != quantityInt)) return true;
                            if (matchedLines.Count() == 0 && quantityInt > 0) return true;
                            break;
                        case "lt":
                            if (matchedLines.Any(x => x.Quantity < quantityInt)) return true;
                            if (matchedLines.Count() == 0 && quantityInt > 0) return true;
                            break;
                        case "gt":
                            if (matchedLines.Any(x => x.Quantity > quantityInt)) return true;
                            break;
                        case "le":
                            if (matchedLines.Any(x => x.Quantity <= quantityInt)) return true;
                            if (matchedLines.Count() == 0 && quantityInt == 0) return true;
                            break;
                        case "ge":
                            if (matchedLines.Any(x => x.Quantity >= quantityInt)) return true;
                            if (matchedLines.Count() == 0 && quantityInt == 0) return true;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            return false;
        }
    }

    [XmlRoot("result")]
    public class ScanRuleResult
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("ruleId")]
        public int RuleId { get; set; }

        [XmlAttribute("resultType")]
        public string ResultType { get; set; }

        [XmlAttribute("resultValue")]
        public string ResultValue { get; set; }
    }

}
