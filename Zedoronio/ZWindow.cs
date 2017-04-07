using System;
using System.Drawing;
using System.Timers;
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

	    public SizeF SizeMultiplier => new SizeF((float) Size.Width / CodeSize.Width, (float) Size.Height / CodeSize.Height);

	    protected Timer TimerFps = new Timer(1000);
	    private int RenderCalled = 0;
	    public int Fps { get; set; }

	    public bool FpsCounter
	    {
	        get { return TimerFps.Enabled; }
	        set { TimerFps.Enabled = value; }
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
		    MouseUp += OnMouseUp;
		    MouseMove += OnMouseMove;
		    MouseWheel += OnMouseWheel;
		    Size = new Size((int)(CodeSize.Width * 0.8), (int)(CodeSize.Height * 0.8));
		    Location = new Point((DisplayDevice.Default.Width - Width) / 2, (DisplayDevice.Default.Height - Height) / 2);
	        Load += OnLoad;

		    TimerFps.Elapsed += (sender, args) =>
		    {
		        Fps = RenderCalled;
		        RenderCalled = 0;
		    };
		}

	    private void OnLoad(object o, EventArgs eventArgs)
	    {
	        GL.ClearColor(Color.White);
	    }

	    private void OnMouseWheel(object sender, MouseWheelEventArgs e)
	    {
	        Scene?.OnMouseWheel(e);
	    }

	    private void OnMouseMove(object sender, MouseMoveEventArgs e)
	    {
	        var pos = e.Position.Divide(SizeMultiplier).ToPoint();
	       Scene?.OnMouseMove(new MouseMoveEventArgs(pos.X, pos.Y, (int)Math.Round(e.XDelta / SizeMultiplier.Width), (int)Math.Round(e.YDelta / SizeMultiplier.Height)));
	    }

	    private void OnMouseUp(object sender, MouseButtonEventArgs e)
	    {
	        var pos = e.Position.Divide(SizeMultiplier).ToPoint();
	        Scene?.OnButtonUp(new MouseButtonEventArgs(pos.X, pos.Y, e.Button, e.IsPressed));
	    }

	    private void OnMouseDown(object sender, MouseButtonEventArgs e)
	    {
	        var pos = e.Position.Divide(SizeMultiplier).ToPoint();
	        Scene?.OnButtonDown(new MouseButtonEventArgs(pos.X, pos.Y, e.Button, e.IsPressed));
	    }

	    private void ZWindow_UpdateFrame(object sender, FrameEventArgs e)
	    {
	        RenderCalled++;
			GL.Clear(ClearBufferMask.ColorBufferBit);
			Scene?.OnRenderFrame(e);
			SwapBuffers();
		}

		void ZWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
		{
			Scene?.OnKeyDown(e);
		}

	    private void ZWindow_KeyUp(object sender, KeyboardKeyEventArgs e)
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

	    private void ZWindow_KeyPress(object sender, KeyPressEventArgs e)
		{
			Scene?.OnKeyPress(e);
		}

	    private void ZWindow_Resize(object sender, EventArgs e)
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
