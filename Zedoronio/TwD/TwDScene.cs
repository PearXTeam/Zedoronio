using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Zedoronio.Drawing;

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
			GL.Ortho(0, Size.Width, Size.Height, 0, -50, 50);
		}
	}
}
