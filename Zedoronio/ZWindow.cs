using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using PearXLib.Maths;

namespace Zedoronio
{
	public class ZWindow : GameWindow
	{
		public Scene Scene;
	    public Size CodeSize { get; set; }

	    public SizeF SizeMultiplier
	    {
	        get
	        {
	            return new SizeF((float) Size.Width / CodeSize.Width, (float) Size.Height / CodeSize.Height);
	        }
	    }

		public ZWindow(Size codeSize)
		{
		    CodeSize = codeSize;
			RenderFrame += ZWindow_UpdateFrame;
			KeyDown += ZWindow_KeyDown;
			KeyUp += ZWindow_KeyUp;
			KeyPress += ZWindow_KeyPress;
			Resize += ZWindow_Resize;
		    MouseDown += OnMouseDown;
		}

	    private void OnMouseDown(object sender, MouseButtonEventArgs e)
	    {
	        e.Position = e.Position.Divide(SizeMultiplier).ToPoint();
	        Scene?.OnButtonDown(e);
	    }

	    void ZWindow_UpdateFrame(object sender, FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			Scene?.OnRenderFrame(e);
			SwapBuffers();
		}

		void ZWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			Scene?.OnKeyDown(e);
		}

		void ZWindow_KeyUp(object sender, KeyboardKeyEventArgs e)
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
			Scene.OnLoad(new EventArgs());
		}
	}
}
