using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Commons.Framework
{
    public static class CaptchaImageGenerator
    {
        public static CaptchaResult GetImage(int width, int height, string captchaCode)
        {
            using (var baseMap = new Bitmap(width, height))
            using (var graph = Graphics.FromImage(baseMap))
            {
                var rand = new Random();

                graph.Clear(GetBackgroundColor());

                DrawCaptchaCode();
                DrawDisorderLine();

                var ms = new MemoryStream();
                baseMap.Save(ms, ImageFormat.Png);

                return new CaptchaResult
                {
                    CaptchaCode = captchaCode,
                    CaptchaByteData = ms.ToArray(),
                    Timestamp = DateTime.UtcNow
                };

                int GetFontSize(int imageWidth, int captchaCodeCount)
                {
                    var averageSize = imageWidth / captchaCodeCount;
                    return Convert.ToInt32(averageSize);
                }

                Color GetFontColor()
                {
                    return Color.Black;
                }

                Color GetBackgroundColor()
                {
                    return Color.LightGray;
                }

                void DrawCaptchaCode()
                {
                    var fontBrush = new SolidBrush(Color.Black);
                    var fontSize = GetFontSize(width, captchaCode.Length);
                    var font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
                    for (var i = 0; i < captchaCode.Length; i++)
                    {
                        fontBrush.Color = GetFontColor();

                        var shiftPx = fontSize / 6;

                        float x = i * fontSize + rand.Next(-shiftPx, shiftPx) + rand.Next(-shiftPx, shiftPx);
                        var maxY = height - fontSize;
                        if (maxY < 0) maxY = 0;
                        float y = rand.Next(0, maxY);

                        graph.DrawString(captchaCode[i].ToString(), font, fontBrush, x, y);
                    }
                }

                void DrawDisorderLine()
                {
                    var linePen = new Pen(new SolidBrush(Color.DarkGray), 2) { Color = Color.DarkGray };
                    for (var i = 0; i < rand.Next(1, 3); i++)
                    {

                        var startPoint = new Point(rand.Next(0, width), rand.Next(0, height));
                        var endPoint = new Point(rand.Next(0, width), rand.Next(0, height));
                        graph.DrawLine(linePen, startPoint, endPoint);
                    }
                }

            }
        }
    }
}