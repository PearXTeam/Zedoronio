using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Zedoronio.Textures;

namespace Zedoronio.Drawing
{
    public class TextureBrush: Brush
    {
        private Texture Tex;
        private PointF[] Points;

        public TextureBrush(Texture tex, PointF[] pts = null)
        {
            if (pts == null)
                Points = new PointF[]
                {
                    new Point(0, 0),
                    new Point(1, 0),
                    new Point(1, 1),
                    new Point(0, 1),
                };
            else
                Points = pts;
            Tex = tex;
        }

        public override void BeforeDraw()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, Tex.Id);
        }

        public override void BeforeVertex(int vert)
        {
            GL.TexCoord2(Points[vert].X, Points[vert].Y);
        }

        public override void AfterDraw()
        {
            GL.Disable(EnableCap.Texture2D);
        }
    }
}