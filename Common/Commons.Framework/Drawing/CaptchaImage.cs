// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CaptchaImage.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// <summary>
//   The captcha image.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Drawing
{
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    #endregion

    /// <summary>
    ///     The captcha image.
    /// </summary>
    [DefaultProperty("Text")]
    public class CaptchaImage
    {
        /// <summary>
        ///     The _text style.
        /// </summary>
        public HatchStyle _textStyle = HatchStyle.Horizontal;

        /// <summary>
        ///     The back style.
        /// </summary>
        public HatchStyle BackStyle = HatchStyle.DottedGrid;

        /// <summary>
        ///     The _random.
        /// </summary>
        private readonly Random random = new Random();

        /// <summary>
        ///     The char set.
        /// </summary>
        private string charSet = "ابتثجحخدذرزسشصضطظعغفقكلمنهوي1234567890";

        private int height = 60;

        private int length = 5;

        private int lines = 100;

        private int noise = 100;

        private string text = "";

        private bool unique = true;

        private int warp = 100;

        private int width = 100;

        public CaptchaImage()
        {
            this.Text = "";
        }

        [Description("Hatch Brush Background Background Color")]
        [Category("Hatch Brush Background")]
        public Color BackgroundBackColor { get; set; } = Color.White;

        [Category("Hatch Brush Background")]
        [Description("Hatch Brush Background Fore Color")]
        public Color BackgroundForeColor { get; set; } = Color.LightGray;

        [Description("Hatch Brush Background Style")]
        [Category("Hatch Brush Background")]
        public HatchStyle BackgroundStyle
        {
            get => this.BackStyle;
            set => this.BackStyle = value;
        }

        [Category("Text")]
        [Description("Character set for auto-text generation")]
        public string CharacterSet
        {
            get => this.charSet;
            set
            {
                if (value == null || value.Length < 2) throw new Exception("Minimum character set size is 2");
                this.charSet = value;
                this.Text = "";
            }
        }

        [Description("Font Name")]
        [TypeConverter(typeof(FontConverter.FontNameConverter))]
        [Category("Font")]
        [Editor("System.Drawing.Design.FontNameEditor", typeof(UITypeEditor))]
        public string FontName { get; set; } = "Tahoma";

        [Description("Font Size")]
        [Category("Font")]
        public int FontSize { get; set; } = 20;

        [Category("Font")]
        [Description("Font Style")]
        public FontStyle FontStyle { get; set; } = FontStyle.Regular;

        [Description("Image Height")]
        [Category("Bitmap")]
        public int Height
        {
            get => this.height;
            set
            {
                if (value < 10 || value > 1000) throw new Exception("Image Height must be between 10 and 1000");
                this.height = value;
            }
        }

        [Description("Generated image")]
        [Browsable(false)]
        [Category("Bitmap")]
        public Bitmap Image
        {
            get
            {
                var bitmap = new Bitmap(this.width, this.height, PixelFormat.Format32bppPArgb);
                Graphics graphics = null;
                Font font = null;
                HatchBrush hatchBrush1 = null;
                HatchBrush hatchBrush2 = null;
                try
                {
                    graphics = Graphics.FromImage(bitmap);
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    var rectangle = new Rectangle(0, 0, this.width, this.height);
                    hatchBrush1 = new HatchBrush(this._textStyle, this.TextForeColor, this.TextBackColor);
                    hatchBrush2 = new HatchBrush(this.BackStyle, this.BackgroundForeColor, this.BackgroundBackColor);
                    graphics.FillRectangle(hatchBrush2, rectangle);
                    var format = new StringFormat
                                     {
                                         Alignment = StringAlignment.Center,
                                         LineAlignment = StringAlignment.Center
                                     };
                    font = new Font(this.FontName, this.FontSize, this.FontStyle);
                    var path = new GraphicsPath();
                    path.AddString(this.text, font.FontFamily, (int)font.Style, font.Size, rectangle, format);
                    if (this.warp > 0)
                    {
                        var matrix = new Matrix();
                        float num = 10 - this.warp * 6 / 100;
                        var destPoints = new PointF[4]
                                             {
                                                 new PointF(
                                                     this.random.Next(rectangle.Width) / num,
                                                     this.random.Next(rectangle.Height) / num),
                                                 new PointF(
                                                     rectangle.Width - this.random.Next(rectangle.Width) / num,
                                                     this.random.Next(rectangle.Height) / num),
                                                 new PointF(
                                                     this.random.Next(rectangle.Width) / num,
                                                     rectangle.Height - this.random.Next(rectangle.Height) / num),
                                                 new PointF(
                                                     rectangle.Width - this.random.Next(rectangle.Width) / num,
                                                     rectangle.Height - this.random.Next(rectangle.Height) / num)
                                             };
                        path.Warp(destPoints, rectangle, matrix, WarpMode.Perspective, 0.0f);
                    }
                    graphics.FillPath(hatchBrush1, path);
                    if (this.noise > 0)
                    {
                        var maxValue = Math.Max(rectangle.Width, rectangle.Height) * this.noise / 3000;
                        var num = rectangle.Width * rectangle.Height / (100 - (this.noise >= 90 ? 90 : this.noise));
                        for (var index = 0; index < num; ++index)
                            graphics.FillEllipse(
                                hatchBrush1,
                                this.random.Next(rectangle.Width),
                                this.random.Next(rectangle.Height),
                                this.random.Next(maxValue),
                                this.random.Next(maxValue));
                    }
                    if (this.lines > 0)
                    {
                        var num = this.lines / 30 + 1;
                        using (var pen = new Pen(hatchBrush1, 1f))
                        {
                            for (var index1 = 0; index1 < num; ++index1)
                            {
                                var points = new PointF[num > 2 ? num - 1 : 2];
                                for (var index2 = 0; index2 < points.Length; ++index2)
                                    points[index2] = new PointF(
                                        this.random.Next(rectangle.Width),
                                        this.random.Next(rectangle.Height));
                                graphics.DrawCurve(pen, points, 1.75f);
                            }
                        }
                    }
                }
                finally
                {
                    graphics?.Dispose();
                    font?.Dispose();
                    hatchBrush1?.Dispose();
                    hatchBrush2?.Dispose();
                }

                return bitmap;
            }
        }

        [Description("Text Length")]
        [Category("Text")]
        public int Length
        {
            get => this.length;
            set
            {
                if (value < 2 || value > 10) throw new Exception("Text Length must be between 2 and 10");
                this.length = value;
                this.Text = "";
            }
        }

        [Description("Lines Factor")]
        [Category("Effects")]
        public int LinesFactor
        {
            get => this.lines;
            set
            {
                if (value < 0 || value > 100) throw new Exception("Lines Factor must be between 0 and 100");
                this.lines = value;
            }
        }

        [Description("Noise Factor")]
        [Category("Effects")]
        public int NoiseFactor
        {
            get => this.noise;
            set
            {
                if (value < 0 || value > 100) throw new Exception("Noise Factor must be between 0 and 100");
                this.noise = value;
            }
        }

        [Description("Text string. Empty to auto-generate random string.")]
        [Category("Text")]
        public string Text
        {
            get => this.text;
            set
            {
                if (value == null || value.Length != 0 && value.Length < 2 || value.Length > 10)
                    throw new Exception("Text Length must be between 2 and 10");
                this.text = value;
                if (this.text.Length == 0)
                    for (var index = 0; index < this.length; ++index)
                    {
                        char ch;
                        do
                        {
                            ch = this.charSet[this.random.Next(0, this.charSet.Length)];
                        }
                        while (this.unique && this.length <= this.charSet.Length && this.text.IndexOf(ch) >= 0);
                        this.text += ch.ToString();
                    }
                this.length = this.text.Length;
            }
        }

        [Description("Hatch Brush Text Background Color")]
        [Category("Hatch Brush Text")]
        public Color TextBackColor { get; set; } = Color.Black;

        [Category("Hatch Brush Text")]
        [Description("Hatch Brush Text Fore Color")]
        public Color TextForeColor { get; set; } = Color.Black;

        [Description("Hatch Brush Text Style")]
        [Category("Hatch Brush Text")]
        public HatchStyle TextStyle
        {
            get => this._textStyle;
            set => this._textStyle = value;
        }

        [Category("Text")]
        [Description("Unique characters or not")]
        public bool Unique
        {
            get => this.unique;
            set
            {
                this.unique = value;
                this.Text = "";
            }
        }

        [Description("Warp Factor")]
        [Category("Effects")]
        public int WarpFactor
        {
            get => this.warp;
            set
            {
                if (value < 0 || value > 100) throw new Exception("Warp Factor must be between 0 and 100");
                this.warp = value;
            }
        }

        [Description("Image Width")]
        [Category("Bitmap")]
        public int Width
        {
            get => this.width;
            set
            {
                if (value < 10 || value > 1000) throw new Exception("Image Width must be between 10 and 1000");
                this.width = value;
            }
        }
    }
}