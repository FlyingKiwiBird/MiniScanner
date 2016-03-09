namespace EveScanner.Evepraisal
{
    using Interfaces;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;

    [DataContract]
    public class EvepraisalJson
    {
        public static EvepraisalJson Resolve(string json)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return EvepraisalJson.Resolve(ms);
            }
        }

        public static EvepraisalJson Resolve(Stream s)
        {
            DataContractJsonSerializer jser = new DataContractJsonSerializer(typeof(EvepraisalJson));
            EvepraisalJson output = (EvepraisalJson)jser.ReadObject(s);
            return output;
        }

        [DataMember(Name = "created")]
        public long Created { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "items")]
        public EvepraisalItem[] Items { get; set; }

        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        [DataMember(Name = "market_id")]
        public int MarketId { get; set; }

        [DataMember(Name = "market_name")]
        public string MarketName { get; set; }

        [DataMember(Name = "totals")]
        public EvepraisalTotals Totals { get; set; }

        public IEnumerable<IItemAppraisal> GetItemAppraisals()
        {
            return this.Items;
        }
    }
}
