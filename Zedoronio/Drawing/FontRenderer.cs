using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Zedoronio.Typography;

namespace Zedoronio.Drawing
{
    public class FontRenderer
    {
        public static void Render(string txt, TypographyAtlas atl, int x, int y)
        {
            int xm = 0;
            int ym = 0;
            float dw = atl.Texture.Size.Width;
            float dh= atl.Texture.Size.Height;
            foreach (var ch in txt)
            {
                if (ch == '\n')
                {
                    ym += atl.Height;
                    xm = 0;
                    continue;
                }
                var entr = atl.Entries[ch];
                var v = new PointF[]
                {
                    new PointF(entr.X / dw, 0),
                    new PointF((entr.X + entr.Width) / dw, 0),
                    new PointF((entr.X + entr.Width) / dw, atl.Height / dh),
                    new PointF(entr.X / dw, atl.Height / dh),
                };
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                DrawingTools.DrawRectangle(x + xm, y + ym, entr.Width, atl.Height, new TextureBrush(atl.Texture, v));
                GL.Disable(EnableCap.Blend);
                xm += entr.Width;
            }
        }
    }
}