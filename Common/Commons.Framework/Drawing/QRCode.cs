// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QRCode.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Drawing
{
    #region

    using System.Drawing;

    using ZXing;
    using ZXing.Common;
    using ZXing.QrCode;

    #endregion

    /// <summary>
    ///     The QR code util.
    /// </summary>
    public class QRCode
    {
        /// <summary>
        /// The generate simple string qr code.
        /// </summary>
        /// <param name="stringToEncode">
        /// The string to encode.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public static byte[] GenerateSimpleStringQrCode(string stringToEncode, int height, int width,int margin)
        {
            // instantiate a writer object
            var barcodeWriter = new BarcodeWriter
                                    {
                                        Format = BarcodeFormat.QR_CODE,
                                        Options =
                                            new QrCodeEncodingOptions
                                                {
                                                    DisableECI = true,
                                                    CharacterSet = "UTF-8",
                                                    Height = height,
                                                    Width = width,
                                                    Margin = margin
                                            }
                                    };

            byte[] imageBytes;

            using (var bitmap = barcodeWriter.Write(stringToEncode))
            {
                var converter = new ImageConverter();
                imageBytes = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            }

            return imageBytes;
        }

        /// <summary>
        /// The generate vcard qr code.
        /// </summary>
        /// <param name="cardInfo">
        /// The card info.
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
        public static byte[] GenerateVcardQRCode(VCard cardInfo, int width, int height)
        {
            // Sample VCard

            // BEGIN:VCARD
            // VERSION:2.1
            // FN:Usama Nada
            // N:Nada;Usama
            // TITLE:Lead Software Engineer
            // TEL;CELL:0566254284
            // EMAIL;HOME;INTERNET:usama_nada@hotmail.com
            // EMAIL;WORK;INTERNET:unada@sure.com.sa
            // URL:http://linkedin.com/in/usamanada
            // ADR:;;Al-Hayat Center, Building B, I;Riyadh;;11372;Saudi Arabia
            // ORG:Sure Technology & Consulting
            // END:VCARD
            var qrValue = $@"BEGIN:VCARD
VERSION:2.1
FN:{cardInfo.FullName}
N:{cardInfo.FirstName};{cardInfo.LastName}
TITLE:{cardInfo.Title}
TEL;CELL:{cardInfo.Mobile}
EMAIL;HOME;INTERNET:{cardInfo.EmailPersonal}
EMAIL;WORK;INTERNET:{cardInfo.EmailWork}
URL:{cardInfo.Url}
ADR:;;{cardInfo.StreetAddress};{cardInfo.City};;{cardInfo.PostalCode};{cardInfo.Country}
ORG:{cardInfo.Company}
END:VCARD";

            // 12
            var barcodeWriter = new BarcodeWriter
                                    {
                                        Format = BarcodeFormat.QR_CODE,
                                        Options =
                                            new QrCodeEncodingOptions
                                                {
                                                    DisableECI = true,
                                                    CharacterSet = "UTF-8",
                                                    Height = height,
                                                    Width = width,
                                                    Margin = 5
                                                }
                                    };

            byte[] imageBytes;

            using (var bitmap = barcodeWriter.Write(qrValue))
            {
                var converter = new ImageConverter();
                imageBytes = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            }

            return imageBytes;
        }

        /// <summary>
        /// The decode qr code.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string DecodeQrCode(Bitmap image)
        {
            var source = new BitmapLuminanceSource(image);
            var bitmap = new BinaryBitmap(new HybridBinarizer(source));
            var result = new MultiFormatReader().decode(bitmap);
            return result?.Text;
        }
    }
}