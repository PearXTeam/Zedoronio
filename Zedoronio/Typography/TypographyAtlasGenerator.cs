using System;
using System.Drawing;
using System.Text;
using PearXLib.Maths;

namespace Zedoronio.Typography
{
    public class AtlasFontGenerator
    {
        public static TypographyAtlas Generate(string chars, string fontName, float size, out Bitmap refBm)
        {
            short x = 0;
            byte h = 0;
            TypographyAtlas atl = new TypographyAtlas();
            Bitmap outBmp;
            using (Font f = new Font(fontName, size))
            {
                using (Bitmap bmp = new Bitmap((int) (chars.Length * (size * 0.8)), (int) (size * 1.7)))
                {
                    foreach (var ch in chars)
                    {
                        using (Graphics gfx = Graphics.FromImage(bmp))
                        {
                            var sz = gfx.MeasureString(ch.ToString(), f);
                            gfx.DrawString(ch.ToString(), f, new SolidBrush(Color.Black), x, 0);
                            atl.Entries.Add(ch, new TypographyAtlasEntry{X = x, Width =  (short)sz.Width, Height = (short)sz.Height});
                            x += (short) sz.Width;
                            if (h < sz.Height)
                                h = (byte) sz.Height;
                        }
                    }
                    outBmp = new Bitmap(MathUtils.NearestPowerOf(x, 2), MathUtils.NearestPowerOf(h, 2));
                    using (var gfx = Graphics.FromImage(outBmp))
                        gfx.DrawImage(bmp, 0, 0);
                }
            }
            atl.LoadAtlas(outBmp);
            refBm = outBmp;
            return atl;
        }
    }
}