using System;
using System.Drawing;
using System.IO;

namespace UI.Utilities
{
    /// <summary>
    /// Helper class for performing image validation.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Compares two images to check if they are identical.
        /// </summary>
        /// <param name="imagePath1">Path to the first image.</param>
        /// <param name="imagePath2">Path to the second image.</param>
        /// <returns>True if the images are identical, false otherwise.</returns>
        public static bool AreImagesEqual(string imagePath1, string imagePath2)
        {
            try
            {
                if (!File.Exists(imagePath1) || !File.Exists(imagePath2))
                {
                    LoggerHelper.LogError($"Image files not found: {imagePath1}, {imagePath2}");
                    return false;
                }

                using (Bitmap img1 = new Bitmap(imagePath1))
                using (Bitmap img2 = new Bitmap(imagePath2))
                {
                    if (img1.Width != img2.Width || img1.Height != img2.Height)
                    {
                        LoggerHelper.LogError("Image dimensions are different.");
                        return false;
                    }

                    for (int x = 0; x < img1.Width; x++)
                    {
                        for (int y = 0; y < img1.Height; y++)
                        {
                            if (img1.GetPixel(x, y) != img2.GetPixel(x, y))
                            {
                                LoggerHelper.LogError("Pixel mismatch detected between the images.");
                                return false;
                            }
                        }
                    }
                }

                LoggerHelper.LogInfo("Images are identical.");
                return true;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"Error occurred during image comparison: {ex.Message}");
                return false;
            }
        }
    }
}
