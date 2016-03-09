namespace EveScanner.Evepraisal
{
    using Interfaces;
    using System.Runtime.Serialization;
    using System;

    [DataContract]
    public class EvepraisalItem : IItemAppraisal
    {
        [DataMember(Name = "groupID")]
        public int GroupID { get; set; }

        [DataMember(Name = "market")]
        public bool Market { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "prices")]
        public EvepraisalPrices Prices { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        [DataMember(Name = "typeID")]
        public int TypeID { get; set; }

        [DataMember(Name = "typeName")]
        public string TypeName { get; set; }

        [DataMember(Name = "volume")]
        public double Volume { get; set; }

        public decimal BuyValue
        {
            get
            {
                return (decimal)this.Prices.Buy.Price;
            }
        }

        public string ErrorMessage
        {
            get; set;
        }

        public bool IsBlueprintCopy
        {
            get
            {
                return this.Name.IndexOf("(Copy)") > 0;
            }
        }

        public bool IsError
        {
            get; set;
        }

        public double RepackagedVolume
        {
            get
            {
                switch (this.GroupID)
                {
                    case 31:
                        return 500;
                    case 25:
                    case 237:
                    case 324:
                    case 830:
                    case 831:
                    case 834:
                    case 893:
                    case 1283:
                    case 1527:
                        return 2500;
                    case 463:
                    case 543:
                        return 3750;
                    case 420:
                    case 541:
                    case 963:
                    case 1305:
                    case 1534:
                        return 5000;
                    case 26:
                    case 358:
                    case 832:
                    case 833:
                    case 894:
                    case 906:
                        return 10000;
                    case 419:
                    case 540:
                    case 1201:
                        return 15000;
                    case 28:
                    case 380:
                    case 1202:
                        return 20000;
                    case 27:
                    case 898:
                    case 900:
                        return 50000;
                    default:
                        return this.Volume;
                }
            }
        }

        public decimal SellValue
        {
            get
            {
                return (decimal)this.Prices.Sell.Price;
            }
        }
    }
}
