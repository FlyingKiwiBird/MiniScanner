//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ILineAppraisal.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces
{
    using EveScanner.Interfaces.Providers;

    /// <summary>
    /// Holds all the data for any line of a scan.
    /// </summary>
    public interface ILineAppraisal
    {
        /// <summary>
        /// Gets or the quantity of the item scanned.
        /// </summary>
        int Quantity { get; }

        /// <summary>
        /// Gets the Type Id of the item scanned.
        /// </summary>
        int TypeId { get; }

        /// <summary>
        /// Gets the Group Id of the item scanned.
        /// </summary>
        int GroupId { get; }

        /// <summary>
        /// Gets the Name of the item scanned.
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// Gets the Volume of the item scanned.
        /// </summary>
        double Volume { get; }

        /// <summary>
        /// Gets the Repackaged Volume of the item scanned.
        /// </summary>
        double RepackagedVolume { get; }

        /// <summary>
        /// Gets the Buy Value of the item scanned.
        /// </summary>
        decimal BuyValue { get; }

        /// <summary>
        /// Gets the Sell Value of the item scanned.
        /// </summary>
        decimal SellValue { get; }

        /// <summary>
        /// Gets a value indicating whether the item scanned was a blueprint copy.
        /// </summary>
        bool IsBlueprintCopy { get; }

        /// <summary>
        /// Gets a value indicating whether this line has an error.
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Gets a value indicating whether this line has been reappraised after the initial appraisal.
        /// </summary>
        bool Reappraised { get; }

        /// <summary>
        /// Reappraises an item with a given pricing provider.
        /// </summary>
        /// <param name="provider">Pricing Provider</param>
        void ReappraiseItem(IItemPriceProvider provider);
    }
}
