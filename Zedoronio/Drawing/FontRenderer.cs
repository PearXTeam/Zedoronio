using System.Drawing;
using Zedoronio.Typography;

namespace Zedoronio.Drawing
{
    public class FontRenderer
    {
        public static void Render(string txt, TypographyAtlas atl, int x, int y)
        {
            int xm = 0;
            foreach (var ch in txt)
            {
                var entr = atl.Entries[ch];
                DrawingTools.DrawRectangle(x + xm, y, entr.Width, entr.Height, new ColorBrush(Color.Red));
                xm += entr.Width;
            }
        }
    }
}