using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Zedoronio.Drawing
{
    public class DrawingTools
    {
        public static void DrawRectangle(int x, int y, int width, int height, Brush brush, int depth = 0)
        {
            brush.BeforeDraw();
            GL.Begin(PrimitiveType.Quads);
            brush.StartBegin();
            brush.BeforeVertex(0);
            GL.Vertex3(x, y, depth);
            brush.BeforeVertex(1);
            GL.Vertex3(x + width, y, depth);
            brush.BeforeVertex(2);
            GL.Vertex3(x + width, y + height, depth);
            brush.BeforeVertex(3);
            GL.Vertex3(x, y + height, depth);
            GL.End();
            brush.AfterDraw();
        }
    }
}