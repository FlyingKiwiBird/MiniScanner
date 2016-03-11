//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EveOnlineCrestApi.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;

    using EveOnlineApi.Entities.Json;
    using EveScanner.Interfaces;
    using EveScanner.Interfaces.Providers;
    using EveScanner.IoC;
    
    /// <summary>
    /// Provides an interface to the Eve Online CREST API
    /// </summary>
    public class EveOnlineCrestApi : IItemPriceProvider
    {
        /// <summary>
        /// Holds the base URL for CREST requests.
        /// </summary>
        private string baseUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="EveOnlineCrestApi"/> class.
        /// </summary>
        /// <param name="baseUrl">Base URL for CREST requests.</param>
        public EveOnlineCrestApi(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("Base URL must be provided.", "baseUrl");
            }

            if (baseUrl.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
            }

            this.baseUrl = baseUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EveOnlineCrestApi"/> class.
        /// </summary>
        public EveOnlineCrestApi() : this("https://public-crest.eveonline.com")
        {
        }

        /// <summary>
        /// Gets all of the Buy and Sell orders for an item from the selected region.
        /// </summary>
        /// <param name="regionId">Region Id</param>
        /// <param name="typeId">Item Type Id</param>
        /// <returns>Buy and Sell Orders</returns>
        public IEnumerable<BuySellOrder> GetBuySellOrders(int regionId, int typeId)
        {
            string buyUrl = string.Format(CultureInfo.InvariantCulture, "{0}/market/{1}/orders/buy/?type={0}/types/{2}/", this.baseUrl, regionId, typeId);
            string sellUrl = string.Format(CultureInfo.InvariantCulture, "{0}/market/{1}/orders/sell/?type={0}/types/{2}/", this.baseUrl, regionId, typeId);

            using (var webclient = Injector.Create<IWebClient>())
            {
                string buyContent = webclient.GetUriToString(new Uri(buyUrl));
                MarketOrders buyOrders = JsonResolve<MarketOrders>(buyContent);

                string sellContent = webclient.GetUriToString(new Uri(sellUrl));
                MarketOrders sellOrders = JsonResolve<MarketOrders>(sellContent);

                return buyOrders.Items.Concat(sellOrders.Items);
            }
        }

        /// <summary>
        /// Implements the GetItemPricing method for IItemPriceProvider. Gets the 
        /// buy and sell pricing of an item from "JITA 4-4" in the Forge region.
        /// </summary>
        /// <param name="typeId">Eve Item Type Id</param>
        /// <param name="buyPrice">Returns the Buy Price</param>
        /// <param name="sellPrice">Returns the Sell Price</param>
        public void GetItemPricing(int typeId, out decimal buyPrice, out decimal sellPrice)
        {
            var output = this.GetBuySellOrders(10000002, typeId);

            var buyable = output.Where(x => x.Location.Id == 60003760 && x.BuyOrder).ToArray();
            var sellable = output.Where(x => x.Location.Id == 60003760 && !x.BuyOrder).ToArray();

            buyPrice = buyable.Length == 0 ? 0 : buyable.Max(y => y.Price);
            sellPrice = sellable.Length == 0 ? 0 : sellable.Min(y => y.Price);
        }

        /// <summary>
        /// Internal class for turning a string into a JSON object.
        /// </summary>
        /// <typeparam name="TJsonType">Type of object to serialize</typeparam>
        /// <param name="json">Input JSON text</param>
        /// <returns>Deserialized object</returns>
        private static TJsonType JsonResolve<TJsonType>(string json)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return EveOnlineCrestApi.JsonResolve<TJsonType>(ms);
            }
        }

        /// <summary>
        /// Internal class for turning a string into a JSON object.
        /// </summary>
        /// <typeparam name="TJsonType">Type of object to serialize</typeparam>
        /// <param name="stream">Input JSON stream</param>
        /// <returns>Deserialized object</returns>
        private static TJsonType JsonResolve<TJsonType>(Stream stream)
        {
            DataContractJsonSerializer jser = new DataContractJsonSerializer(typeof(TJsonType));
            TJsonType output = (TJsonType)jser.ReadObject(stream);
            return output;
        }
    }
}
