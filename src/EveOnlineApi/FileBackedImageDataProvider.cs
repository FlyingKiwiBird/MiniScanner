//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="FileBackedImageDataProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi
{
    using System.IO;
    using System.Net;
    
    using EveOnlineApi.Interfaces;

    /// <summary>
    /// Provides an image provider backed by the file system for caching.
    /// </summary>
    public class FileBackedImageDataProvider : IImageDataProvider
    {
        /// <summary>
        /// Holds our caching directory.
        /// </summary>
        private string cacheDir = ".\\ImageCache";

        /// <summary>
        /// Initializes a new instance of the <see cref="FileBackedImageDataProvider"/> class.
        /// </summary>
        public FileBackedImageDataProvider()
        {
            if (!Directory.Exists(this.cacheDir))
            {
                Directory.CreateDirectory(this.cacheDir);
            }
        }

        /// <summary>
        /// Gets image data from the EVE Online Image Servers
        /// </summary>
        /// <param name="imageType">Type of Image</param>
        /// <param name="id">Id for Image</param>
        /// <param name="width">Width of Image</param>
        /// <returns>Image Data</returns>
        public byte[] GetImageData(string imageType, int id, int width)
        {
            string localPath = Path.Combine(this.cacheDir, imageType);
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }

            string fileName = Path.Combine(localPath, string.Format("{0}_{1}.png", id, width));

            if (!File.Exists(fileName))
            {
                using (WebClient cli = new WebClient())
                {
                    cli.DownloadFile(string.Format("https://image.eveonline.com/{0}/{1}_{2}.png", imageType, id, width), fileName);
                }
            }

            return File.ReadAllBytes(fileName);
        }
    }
}
