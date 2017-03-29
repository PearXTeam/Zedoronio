using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Zedoronio.Drawing
{
    public class GradientBrush : Brush
    {
        private Color[] colors;

        public GradientBrush(Color[] colors)
        {
            this.colors = colors;
        }

        public override void BeforeVertex(int vert)
        {
            GL.Color3(colors[vert]);
        }
    }
}