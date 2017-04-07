using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Zedoronio.Drawing
{
    public class ColorBrush : Brush
    {
        private Color _Color;
        public ColorBrush(Color col)
        {
            _Color = col;
        }

        public override void StartBegin()
        {
            GL.Color3(_Color);
        }
    }
}