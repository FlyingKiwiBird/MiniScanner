namespace EveScanner.Evepraisal
{
    using System.Runtime.Serialization;

    [DataContract]
    public class EveprasisalPrice
    {
        [DataMember(Name = "avg")]
        public double Average { get; set; }

        [DataMember(Name = "max")]
        public double Maximum { get; set; }

        [DataMember(Name = "median")]
        public double Median { get; set; }

        [DataMember(Name = "min")]
        public double Minimum { get; set; }

        [DataMember(Name = "percentile")]
        public double Percentile { get; set; }

        [DataMember(Name = "price")]
        public double Price { get; set; }

        [DataMember(Name = "stddev")]
        public double StdDev { get; set; }

        [DataMember(Name = "volume")]
        public long Volume { get; set; }
    }
}
