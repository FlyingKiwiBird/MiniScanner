//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EvepraisalJson.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Evepraisal
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;

    /// <summary>
    /// Root element for the JSON objects which come back from Evepraisal.
    /// </summary>
    [DataContract]
    public class EvepraisalJson
    {
        /// <summary>
        /// Holds our item collection.
        /// </summary>
        private Collection<EvepraisalItem> items = null;

        /// <summary>
        /// Gets or sets the creation time in seconds since the epoch.
        /// </summary>
        [DataMember(Name = "created")]
        public long Created { get; set; }

        /// <summary>
        /// Gets or sets the Evepraisal Scan Id
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets the collection of Items in the scan.
        /// </summary>
        [DataMember(Name = "items")]
        public Collection<EvepraisalItem> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new Collection<EvepraisalItem>();
                }

                return this.items;
            }
        }

        /// <summary>
        /// Gets or sets a value stating what kind of scan this is.
        /// </summary>
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets the Market Id prices were obtained from.
        /// </summary>
        [DataMember(Name = "market_id")]
        public int MarketId { get; set; }

        /// <summary>
        /// Gets or sets the Name of the Market prices were obtained from.
        /// </summary>
        [DataMember(Name = "market_name")]
        public string MarketName { get; set; }

        /// <summary>
        /// Gets or sets Total Values for the scan.
        /// </summary>
        [DataMember(Name = "totals")]
        public EvepraisalTotals Totals { get; set; }

        /// <summary>
        /// Resolves a JSON string into an Evepraisal JSON object.
        /// </summary>
        /// <param name="json">JSON Formatted String</param>
        /// <returns>Evepraisal JSON Object</returns>
        public static EvepraisalJson Resolve(string json)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return EvepraisalJson.Resolve(ms);
            }
        }

        /// <summary>
        /// Resolves a JSON stream into an Evepraisal JSON object.
        /// </summary>
        /// <param name="stream">JSON Formatted Stream</param>
        /// <returns>Evepraisal JSON Object</returns>
        public static EvepraisalJson Resolve(Stream stream)
        {
            DataContractJsonSerializer jser = new DataContractJsonSerializer(typeof(EvepraisalJson));
            EvepraisalJson output = (EvepraisalJson)jser.ReadObject(stream);
            foreach (EvepraisalItem item in output.Items)
            {
                if (string.IsNullOrEmpty(item.TypeName) && !string.IsNullOrEmpty(item.Name))
                {
                    item.TypeName = item.Name;
                }
            }

            return output;
        }
    }
}
