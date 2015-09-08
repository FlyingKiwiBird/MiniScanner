//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ImageCombiner.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Combines images to form larger image.
    /// </summary>
    public static class ImageCombiner
    {
        /// <summary>
        /// This combines a bunch of images into one single image. This currently assumes the single image is square.
        /// </summary>
        /// <param name="outerWidth">Width of containing image</param>
        /// <param name="outerHeight">Height of containing image</param>
        /// <param name="imagePaths">Paths to image files</param>
        /// <returns>Combined Image</returns>
        public static Image CombineImages(int outerWidth, int outerHeight, string[] imagePaths)
        {
            Image output = null;

            int numberOfImagesPerRow = (int)Math.Ceiling(Math.Sqrt(imagePaths.Length));

            int imageWidth = (int)Math.Floor((double)outerWidth / (double)numberOfImagesPerRow);
            int imageHeight = (int)Math.Floor((double)outerHeight / (double)numberOfImagesPerRow);

            using (Bitmap bmp = new Bitmap(outerWidth, outerHeight))
            {
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    for (int i = 0; i < imagePaths.Length; i++)
                    {
                        using (Image img = Image.FromFile(imagePaths[i]))
                        {
                            gfx.DrawImage(img, new Rectangle((i % numberOfImagesPerRow) * imageWidth, (i - (i % numberOfImagesPerRow)) / numberOfImagesPerRow * imageHeight, imageWidth, imageHeight), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
                        }
                    }

                    output = (Bitmap)bmp.Clone();
                }
            }

            return output;
        }
    }
}
