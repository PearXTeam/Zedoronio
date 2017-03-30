using System;
using System.Drawing;
using OpenTK;
using OpenTK.Input;
using Zedoronio.Drawing;


//todo: high; Remove this piece of shit
namespace Zedoronio.TwD.Controls
{
    public class ColoredQuad : Control
    {
        private Random rand = new Random();
        private Color col = Color.Black;
        public ColoredQuad(Point loc, Size s) : base(loc, s)
        {
           RenderFrame += OnRenderFrame;
            SelectType = ControlSelectType.MouseEnter;
           MouseWheel += OnMouseWheel;
            MouseEnter += OnMouseEnter;
        }

        private void OnMouseEnter(object sender, EventArgs eventArgs)
        {
            col = Color.White;
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Console.WriteLine(e.Delta);
            col = Color.FromArgb(col.R + e.Delta, col.G + e.Delta, col.B + e.Delta);
        }

        private void OnRenderFrame(object sender, FrameEventArgs frameEventArgs)
        {
            DrawingTools.DrawRectangle(0, 0, Size.Width, Size.Height, new ColorBrush(col));
        }
    }
}