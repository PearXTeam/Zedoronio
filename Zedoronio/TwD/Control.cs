using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Zedoronio.EventArguments;

namespace Zedoronio.TwD
{
	public class Control
	{
	    private bool _selected;
	    public bool Entered;

		public event EventHandler<FrameEventArgs> RenderFrame;
		public event EventHandler<KeyboardKeyEventArgs> KeyDown;
		public event EventHandler<KeyboardKeyEventArgs> KeyUp;
		public event EventHandler<KeyPressEventArgs> KeyPress;
		public event EventHandler<MouseButtonEventArgs> ButtonDown;
		public event EventHandler<MouseButtonEventArgs> ButtonUp;
		public event EventHandler<MouseMoveEventArgs> MouseMove;
		public event EventHandler MouseEnter;
		public event EventHandler MouseLeave;
		public event EventHandler<MouseWheelEventArgs> MouseWheel;
		public event EventHandler Load;
	    public event EventHandler<SelectionChangedEventArgs> SelectionChanged;
	    public event EventHandler Resize;

		public Scene Scene
		{
			get
			{
				if (this is Scene)
					return (Scene)this;
				return (Scene)GetMainParent(this);
			}
		}

	    public ControlCollection Controls { get; }
	    public Point Location { get; set; }
		public Size Size { get; set; }
	    public bool KeyEventsRequiresSelection { get; set; } = true;
	    public bool ScrollEventRequiresSelection { get; set; } = true;
	    public ControlSelectType SelectType { get; set; } = ControlSelectType.Click;

		public Control Parent { get; set; }

	    public bool Selected
	    {
	        get { return _selected; }
	        private set
	        {
	            _selected = value;
	            OnSelectionChanged(new SelectionChangedEventArgs(value));
	        }
	    }

		public Control(Point loc, Size s)
		{
			Location = loc;
			Size = s;
			Controls = new ControlCollection(this);
		}

		public void Select()
		{
			UnselectAll(GetMainParent(this));
			Selected = true;
		}

		public void Unselect()
		{
			Selected = false;
		}

		public virtual void OnRenderFrame(FrameEventArgs e)
		{
			GL.PushMatrix();
			var p = GetPointOnScene();
			GL.Translate(p.X, p.Y, 0);
			RenderFrame?.Invoke(this, e);
			GL.PopMatrix();
			foreach (var cont in Controls)
			{
				cont.OnRenderFrame(e);
			}
		}

		public virtual void OnKeyDown(KeyboardKeyEventArgs e)
		{
			foreach (var cont in Controls)
				cont.OnKeyDown(e);
			if ((KeyEventsRequiresSelection && Selected) || !KeyEventsRequiresSelection)
				KeyDown?.Invoke(this, e);
		}

		public virtual void OnKeyUp(KeyboardKeyEventArgs e)
		{
			foreach (var cont in Controls)
				cont.OnKeyUp(e);
			if ((KeyEventsRequiresSelection && Selected) || !KeyEventsRequiresSelection)
				KeyUp?.Invoke(this, e);
		}

		public virtual void OnKeyPress(KeyPressEventArgs e)
		{
			foreach (var cont in Controls)
				cont.OnKeyPress(e);
			if ((KeyEventsRequiresSelection && Selected) || !KeyEventsRequiresSelection)
				KeyPress?.Invoke(this, e);
		}

		public virtual void OnButtonDown(MouseButtonEventArgs e)
		{
		    foreach (var cont in Controls)
		    {
		        if (new RectangleF(cont.Location, cont.Size).Contains(e.Position))
		        {
		            cont.OnButtonDown(new MouseButtonEventArgs(e.Position.X - cont.Location.X, e.Position.Y-  cont.Location.Y, e.Button, e.IsPressed));
		            return;
		        }
		    }
		    if(SelectType == ControlSelectType.Click)
		        Select();
			ButtonDown?.Invoke(this, e);
		}

		public virtual void OnButtonUp(MouseButtonEventArgs e)
		{
		    foreach (var cont in Controls)
		    {
		        if (new RectangleF(cont.Location, cont.Size).Contains(e.Position))
		        {
		            cont.OnButtonUp(new MouseButtonEventArgs(e.Position.X - cont.Location.X, e.Position.Y-  cont.Location.Y, e.Button, e.IsPressed));
		            return;
		        }
		    }
		    ButtonUp?.Invoke(this, e);
		}

		public virtual void OnMouseMove(MouseMoveEventArgs e)
		{
		    foreach (var cont in Controls)
		    {
		        if (new RectangleF(cont.Location, cont.Size).Contains(e.Position))
		        {
		            cont.OnMouseMove(new MouseMoveEventArgs(e.Position.X - cont.Location.X, e.Position.Y - cont.Location.Y, e.XDelta, e.YDelta));
		            return;
		        }
		        if (cont.Entered)
		        {
		            cont.Entered = false;
		            cont.OnMouseLeave(new EventArgs());
		        }
		    }
		    if (Parent.Entered)
		    {
		        Parent.Entered = false;
		        Parent.OnMouseLeave(new EventArgs());
		    }
		    if (!Entered)
		    {
		        Entered = true;
		        OnMouseEnter(new EventArgs());
		    }
			MouseMove?.Invoke(this, e);
		}

		public virtual void OnMouseEnter(EventArgs e)
		{
		    if(SelectType == ControlSelectType.MouseEnter)
		        Select();
			MouseEnter?.Invoke(this, e);
		}

		public virtual void OnMouseLeave(EventArgs e)
		{
		    //if(SelectType == ControlSelectType.MouseEnter)
		     //   Unselect();
			MouseLeave?.Invoke(this, e);
		}

		public virtual void OnMouseWheel(MouseWheelEventArgs e)
		{
		    foreach (var cont in Controls)
		        cont.OnMouseWheel(e);
		    if ((ScrollEventRequiresSelection && Selected) || !ScrollEventRequiresSelection)
		    {

		        MouseWheel?.Invoke(this, e);
		    }
		}

		public virtual void OnLoad(EventArgs e)
		{
			Load?.Invoke(this, e);
			foreach (var cont in Controls)
				cont.OnLoad(e);
		}

	    public virtual void OnSelectionChanged(SelectionChangedEventArgs e)
	    {
	        SelectionChanged?.Invoke(this, e);
	    }

	    public virtual void OnResize(EventArgs e)
	    {
	        Resize?.Invoke(this, e);
	    }

		public static void UnselectAll(Control c)
		{
			c.Unselect();
			foreach (var cont in c.Controls)
			{
				UnselectAll(cont);
			}
		}

		public static Control GetMainParent(Control c)
		{
			if (c.Parent != null)
				return GetMainParent(c.Parent);
			return c;
		}

		public Point GetPointOnScene()
		{
			return GetPointOnScene(this, Point.Empty);
		}

		static Point GetPointOnScene(Control c, Point now)
		{
			if (!(c is Scene))
			{
				return GetPointOnScene(c.Parent, new Point(c.Location.X + now.X, c.Location.Y + now.Y));
			}
			return now;
		}
	}
}
