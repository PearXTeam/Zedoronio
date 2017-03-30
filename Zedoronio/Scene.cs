using System.Drawing;
using Zedoronio.TwD;

namespace Zedoronio
{
	public class Scene : Control
	{
		public ZWindow Window { get; set; }

		public Scene() : base(Point.Empty, new Size(1280, 720))
		{

		}
	}
}
