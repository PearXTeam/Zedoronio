using System;
using System.Drawing;
using OpenTK;
using OpenTK.Input;
using PearXLib;
using Zedoronio.Drawing;

namespace Zedoronio.TwD.Controls
{
    public class ColoredQuad : Control
    {
        public ColoredQuad(Point loc, Size s) : base(loc, s)
        {
           RenderFrame += OnRenderFrame;
            ButtonDown += OnButtonDown;
        }

        private void OnButtonDown(object sender, MouseButtonEventArgs args)
        {
            Console.WriteLine(args.Position);
        }

        private void OnRenderFrame(object sender, FrameEventArgs frameEventArgs)
        {
            DrawingTools.DrawRectangle(0, 0, Size.Width, Size.Height, new ColorBrush(new Random().NextColor()));
        }
    }
}