//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ScanLine.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Core
{
    using EveScanner.Interfaces;
    using EveScanner.IoC;

    using Interfaces.Providers;
    using Interfaces.SDE;

    /// <summary>
    /// Holds all the data for any line of a scan.
    /// </summary>
    public class ScanLine : ILineAppraisal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScanLine"/> class.
        /// </summary>
        public ScanLine()
        {
            this.IsBlueprintCopy = false;
            this.IsError = false;
            this.ErrorMessage = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanLine"/> class. 
        /// This constructor takes minimal parameters as arguments.
        /// </summary>
        /// <param name="quantity">Number of Units</param>
        /// <param name="typeName">Item Name</param>
        /// <param name="isBlueprintCopy">Blueprint Copy</param>
        public ScanLine(int quantity, string typeName, bool isBlueprintCopy) : this()
        {
            this.Quantity = quantity;
            this.TypeName = typeName;
            this.IsBlueprintCopy = isBlueprintCopy;
        }

        /// <summary>
        /// Gets or sets the quantity of the item scanned.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the Type Id of the item scanned.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets the Group Id of the item scanned.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the Name of the item scanned.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the Volume of the item scanned.
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Gets or sets the Repackaged Volume of the item scanned.
        /// </summary>
        public double RepackagedVolume { get; set; }

        /// <summary>
        /// Gets or sets the Buy Value of the item scanned.
        /// </summary>
        public decimal BuyValue { get; set; }

        /// <summary>
        /// Gets or sets the Sell Value of the item scanned.
        /// </summary>
        public decimal SellValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item scanned was a blueprint copy.
        /// </summary>
        public bool IsBlueprintCopy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this line has an error.
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets a value indicating whether this line has been reappraised after the initial appraisal.
        /// </summary>
        public bool Reappraised { get; private set; }

        /// <summary>
        /// Resolves missing information for a type with minimal info.
        /// </summary>
        public void ResolveMissingInfo()
        {
            if (this.TypeId == default(int))
            {
                if (string.IsNullOrWhiteSpace(this.TypeName))
                {
                    return;
                }

                this.ResolveByTypeName();
            }

            this.ResolveRepackagedInfo();
            this.ResolveItemPricing();
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

                provider.GetItemPricing(this.TypeId, out buyPrice, out sellPrice);

                if (buyPrice > 0 || sellPrice > 0)
                {
                    this.BuyValue = buyPrice;
                    this.SellValue = sellPrice;

                    this.Reappraised = true;
                }
            }
        }

        /// <summary>
        /// Resolves information about an item from the Name
        /// </summary>
        private void ResolveByTypeName()
        {
            IInventoryTypeProvider itprovider = Injector.Create<IInventoryTypeProvider>();
            if (itprovider != null)
            {
                IInventoryType output = itprovider.GetInventoryTypeByTypeName(this.TypeName);
                if (output != null)
                {
                    this.CopyDataFromInventoryType(output);
                }
                else
                {
                    this.IsError = true;
                    this.ErrorMessage = "Could not resolve type by name.";
                }
            }
        }

        /// <summary>
        /// Resolves the Repackaged Size from the Group Id.
        /// </summary>
        private void ResolveRepackagedInfo()
        {
            IExtraDataExportProvider epe = Injector.Create<IExtraDataExportProvider>();
            if (epe != null)
            {
                double rpe = epe.GetRepackagedVolumesForGroup(this.GroupId).Volume;
                if (rpe != default(double) && rpe > 0)
                {
                    this.RepackagedVolume = rpe;
                }
            }
        }

        /// <summary>
        /// Resolves Item Pricing with a Reappraisal of the Item.
        /// </summary>
        private void ResolveItemPricing()
        {
            IItemPriceProvider ipa = Injector.Create<IItemPriceProvider>();
            if (ipa != null)
            {
                this.ReappraiseItem(ipa);
            }
        }

        /// <summary>
        /// Copies data from an Inventory Type into the Scan Line.
        /// </summary>
        /// <param name="sourceType">SDE Inventory Type</param>
        private void CopyDataFromInventoryType(IInventoryType sourceType)
        {
            this.TypeId = sourceType.TypeId;
            this.GroupId = sourceType.GroupId ?? 0;
            this.TypeName = sourceType.TypeName;
            this.Volume = sourceType.Volume ?? 0;
        }
    }
}
