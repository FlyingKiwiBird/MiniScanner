namespace EveScanner.Evepraisal
{
    using System.Runtime.Serialization;

    [DataContract]
    public class EvepraisalTotals
    {
        [DataMember(Name = "buy")]
        public double Buy { get; set; }

        [DataMember(Name = "sell")]
        public double Sell { get; set; }

        [DataMember(Name = "volume")]
        public double Volume { get; set; }
    }
}
