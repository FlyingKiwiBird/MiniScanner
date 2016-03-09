namespace EveScanner.Evepraisal
{
    using System.Runtime.Serialization;

    [DataContract]
    public class EvepraisalPrices
    {
        [DataMember(Name = "all")]
        public EveprasisalPrice All { get; set; }

        [DataMember(Name = "buy")]
        public EveprasisalPrice Buy { get; set; }

        [DataMember(Name = "sell")]
        public EveprasisalPrice Sell { get; set; }
    }
}
