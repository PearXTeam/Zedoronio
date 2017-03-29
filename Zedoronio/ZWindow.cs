using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Zedoronio
{
	public class ZWindow : GameWindow
	{
		public Scene Scene;

		public ZWindow()
		{
			RenderFrame += ZWindow_UpdateFrame;
			KeyDown += ZWindow_KeyDown;
			KeyUp += ZWindow_KeyUp;
			KeyPress += ZWindow_KeyPress;
			Resize += ZWindow_Resize;
		}

		void ZWindow_UpdateFrame(object sender, FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			Scene?.OnRenderFrame(e);
			SwapBuffers();
		}

		void ZWindow_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{
			Scene?.OnKeyDown(e);
		}

		void ZWindow_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.F11)
			{
				if (WindowState == WindowState.Fullscreen)
					WindowState = WindowState.Normal;
				else
					WindowState = WindowState.Fullscreen;
			}
				
			Scene?.OnKeyUp(e);
		}

		void ZWindow_KeyPress(object sender, KeyPressEventArgs e)
		{
			Scene?.OnKeyPress(e);
		}

		void ZWindow_Resize(object sender, EventArgs e)
		{
			GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
			Scene?.OnResize(new EventArgs());
		}

		public void ChangeScene(Scene sc)
		{
			Scene = sc;
			Scene.Window = this;
			Scene.Location = Location;
			Scene.OnLoad(new EventArgs());
		}
	}
}
