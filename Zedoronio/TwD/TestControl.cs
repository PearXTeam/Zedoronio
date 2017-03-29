using System.Drawing;
using Zedoronio.Common;
using Zedoronio.TwD;

namespace Zedoronio
{
	public class TestControl : Control
	{
		public TestControl(Point p, Size s) : base(p, s)
		{
			RenderFrame += TestControl_RenderFrame;
		}

		void TestControl_RenderFrame(object sender, OpenTK.FrameEventArgs e)
		{DrawingTools.DrawRectangle(Location.X, Location.Y, ClientSize.Width, ClientSize.Height, Color.AliceBlue);
		}
	}
}
