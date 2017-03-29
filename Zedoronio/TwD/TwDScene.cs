using System;
using OpenTK.Graphics.OpenGL;

namespace Zedoronio.TwD
{
	public class TwDScene : Scene
	{
		public TwDScene()
		{
			Resize += TwDScene_Resize;
		}

		void TwDScene_Resize(object sender, EventArgs e)
		{
			GL.MatrixMode(MatrixMode.Projection);
		    GL.LoadIdentity();
			GL.Ortho(0, Size.Width, Size.Height, 0, 0, 1);
		}
	}
}
