//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EvepraisalItem.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Evepraisal
{
    using System;
    using System.Runtime.Serialization;

    using EveScanner.Interfaces;
    using EveScanner.Interfaces.Providers;
    using EveScanner.IoC;
    
    /// <summary>
    /// A line passed to Evepraisal for Appraisal
    /// </summary>
    [DataContract]
    public class EvepraisalItem : ILineAppraisal
    {
        /// <summary>
        /// Gets or sets the Group Id of the item scanned.
        /// </summary>
        [DataMember(Name = "groupID")]
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is available on the market.
        /// </summary>
        [DataMember(Name = "market")]
        public bool Market { get; set; }

        /// <summary>
        /// Gets or sets the name of the item. Unknown why this is separate from Type Name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Prices for the Appraisal.
        /// </summary>
        [DataMember(Name = "prices")]
        public EvepraisalPrices Prices { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the item scanned.
        /// </summary>
        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the Type Id of the item scanned.
        /// </summary>
        [DataMember(Name = "typeID")]
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets the Name of the item scanned.
        /// </summary>
        [DataMember(Name = "typeName")]
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the Volume of the item scanned.
        /// </summary>
        [DataMember(Name = "volume")]
        public double Volume { get; set; }

        /// <summary>
        /// Gets a value indicating whether this line has been reappraised after the initial appraisal.
        /// </summary>
        public bool Reappraised { get; private set; }

        /// <summary>
        /// Gets the Buy Value of the item scanned.
        /// </summary>
        public decimal BuyValue
        {
            get
            {
                if (this.Prices != null && this.Prices.Buy != null)
                {
                    return (decimal)this.Prices.Buy.Price;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage
        {
            get; set;
        }

        /// <summary>
        /// Gets a value indicating whether the item scanned was a blueprint copy.
        /// </summary>
        public bool IsBlueprintCopy
        {
            get
            {
                return this.Name.IndexOf("(Copy)", StringComparison.OrdinalIgnoreCase) > 0;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this line has an error.
        /// </summary>
        public bool IsError
        {
            get; set;
        }

        /// <summary>
        /// Gets the Repackaged Volume of the item scanned.
        /// </summary>
        public double RepackagedVolume
        {
            get
            {
                switch (this.GroupId)
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

        /// <summary>
        /// Gets the Sell Value of the item scanned.
        /// </summary>
        public decimal SellValue
        {
            get
            {
                if (this.Prices != null && this.Prices.Sell != null)
                {
                    return (decimal)this.Prices.Sell.Price;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Reappraises an item with a given pricing provider.
        /// </summary>
        /// <param name="provider">Pricing Provider</param>
        public void ReappraiseItem(IItemPriceProvider provider)
        {
            if (provider != null)
            {
                decimal buyPrice;
                decimal sellPrice;

                if (this.TypeId == 0)
                {
                    IInventoryTypeProvider itp = Injector.Create<IInventoryTypeProvider>();
                    if (itp != null)
                    {
                        var type = itp.GetInventoryTypeByTypeName(this.TypeName);
                        this.TypeId = type.TypeId;
                        this.GroupId = type.GroupId.Value;
                    }
                }

                provider.GetItemPricing(this.TypeId, out buyPrice, out sellPrice);

                if (buyPrice > 0 || sellPrice > 0)
                {
                    this.Prices = new EvepraisalPrices()
                    {
                        Buy = new EvepraisalPrice()
                        {
                            Price = (double)buyPrice
                        },

                        Sell = new EvepraisalPrice()
                        {
                            Price = (double)sellPrice
                        }
                    };

                    this.Reappraised = true;
                }
            }
        }
    }
}
