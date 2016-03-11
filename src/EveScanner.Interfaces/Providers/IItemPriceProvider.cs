//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IItemPriceProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces.Providers
{
    /// <summary>
    /// Interface for additional pricing queries.
    /// </summary>
    public interface IItemPriceProvider
    {
        /// <summary>
        /// Retrieves Item Pricing for a given Item Type Id
        /// </summary>
        /// <param name="typeId">Item Type Id</param>
        /// <param name="buyPrice">Output - Buying Price</param>
        /// <param name="sellPrice">Output - Selling Price</param>
        void GetItemPricing(int typeId, out decimal buyPrice, out decimal sellPrice);
    }
}
