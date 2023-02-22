// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageExtensions.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Extensions
{
    #region

    using System.Drawing;
    using System.IO;

    #endregion

    /// <summary>
    /// The image extensions.
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// The to byte array.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        public static byte[] ToByteArray(this Bitmap image)
        {
            using (var memStream = (Stream)new MemoryStream())
            {
                image.Save(memStream, image.RawFormat);
                return memStream.ToByteArray();
            }
        }
    }
}