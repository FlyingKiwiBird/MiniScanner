//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IImageDataProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    /// <summary>
    /// Data Provider for Eve Online Image Data
    /// </summary>
    public interface IImageDataProvider
    {
        /// <summary>
        /// Gets image data from the EVE Online Image Servers
        /// </summary>
        /// <param name="imageType">Type of Image</param>
        /// <param name="id">Id for Image</param>
        /// <param name="width">Width of Image</param>
        /// <returns>Image Data</returns>
        byte[] GetImageData(string imageType, int id, int width);
    }
}
