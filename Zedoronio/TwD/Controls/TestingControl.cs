using System;
using System.Drawing;
using OpenTK;
using OpenTK.Input;
using Zedoronio.Drawing;
using Zedoronio.Textures;
using TextureBrush = Zedoronio.Drawing.TextureBrush;


//todo: high; Remove this piece of shit
namespace Zedoronio.TwD.Controls
{
    public class ColoredQuad : Control
    {
        private Random rand = new Random();
        private Texture Tex;
        public ColoredQuad(Point loc, Size s, Texture tex) : base(loc, s)
        {
           RenderFrame += OnRenderFrame;
            SelectType = ControlSelectType.MouseEnter;
           MouseWheel += OnMouseWheel;
            MouseEnter += OnMouseEnter;
            Tex = tex;
        }

        private void OnMouseEnter(object sender, EventArgs eventArgs)
        {
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Console.WriteLine(e.Delta);
        }

        private void OnRenderFrame(object sender, FrameEventArgs frameEventArgs)
        {
            DrawingTools.DrawRectangle(0, 0, Size.Width, Size.Height, new ColorBrush(Color.White));
        }
    }
}