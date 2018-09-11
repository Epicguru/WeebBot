using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeebBot
{
    public static class BitmapDrawer
    {
        public static Bitmap GetMeme(string text, bool memeText = true)
        {
            if (memeText)
            {
                Random r = new Random();
                StringBuilder str = new StringBuilder(text.Length, text.Length);
                foreach (var c in text.ToCharArray())
                {
                    bool cap = r.NextDouble() >= 0.5f;
                    if (cap)
                    {
                        str.Append(Char.ToUpperInvariant(c));
                    }
                    else
                    {
                        str.Append(c);
                    }
                }

                text = str.ToString();
                str.Clear();
            }

            var memeFormat = Resources.SpongeMeme;

            Graphics g = Graphics.FromImage(memeFormat);

            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            const float FONT_SIZE = 38f;
            const float BORDER_SIZE = 4f;

            Font f = new Font("Impact", FONT_SIZE, FontStyle.Bold);
            Brush b = new SolidBrush(Color.White);
            StringFormat sf = new StringFormat();
            sf.Trimming = StringTrimming.Word;
            sf.Alignment = StringAlignment.Center;

            var imgSize = memeFormat.Size;

            var size = g.MeasureString(text, f, imgSize.Width, sf);

            var pos = new RectangleF(imgSize.Width / 2 - size.Width / 2, 5, size.Width, size.Height + 10);

            //g.DrawString(text, f, b, pos, sf);
            GraphicsPath p = new GraphicsPath();
            p.AddString(
                text,             // text to draw
                f.FontFamily,  // or any other font family
                (int)FontStyle.Bold,      // font style (bold, italic, etc.)
                g.DpiY * FONT_SIZE / 72,       // em size
                pos,              // location where to draw text
                sf);          // set options here (e.g. center alignment)
            g.DrawPath(new Pen(Color.Black, BORDER_SIZE), p);
            g.FillPath(b, p);

            return memeFormat;
        }
    }
}
