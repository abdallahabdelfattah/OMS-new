// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Barcode.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Drawing
{
    #region Usings

    using System.Drawing;

    using ZXing;
    using ZXing.QrCode;

    #endregion

    /// <summary>
    ///     The barcode helper.
    /// </summary>
    public static class Barcode
    {
        /// <summary>
        /// The decode from image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string DecodeFromImage(Bitmap image)
        {
            // create a barcode reader instance
            var reader = new BarcodeReader();

            // detect and decode the barcode inside the bitmap
            var result = reader.Decode(image);

            return result?.Text;
        }

        /// <summary>
        /// The generate bar code.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public static byte[] GenerateBarCode(string code, int width, int height)
        {
            var barcodeWriter = new BarcodeWriter
                                    {
                                        Format = BarcodeFormat.CODE_128,
                                        Options = new QrCodeEncodingOptions
                                                      {
                                                          DisableECI = true,
                                                          Height = height,
                                                          Width = width

                                                          // ,Margin = 10
                                                      }
                                    };

            byte[] imageBytes;

            using (var bitmap = barcodeWriter.Write(code))
            {
                var converter = new ImageConverter();
                imageBytes = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            }

            return imageBytes;
        }
    }
}