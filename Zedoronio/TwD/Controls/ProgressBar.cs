using System;
using System.Drawing;
using OpenTK;
using Zedoronio.Drawing;
using Zedoronio.Typography;

namespace Zedoronio.TwD.Controls
{
    public class ProgressBar : Control
    {
        private int max = 100;
        private int val;

        public int MaxValue
        {
            get { return max; }
            set
            {
                if (Value > value)
                    Value = value;
                max = value;
            }
        }

        public int Value
        {
            get { return val; }
            set
            {
                if (value > MaxValue)
                    value = MaxValue;
                val = value;
            }
        }
        public string Text { get; set; }
        public TypographyAtlas Atlas;

        public Color BackgroudColor { get; set; } = Color.LightGray;
        public Color ForeColor { get; set; } = Color.DodgerBlue;
        public Color TextColor { get; set; } = Color.White;

        public ProgressBar(Point loc, Size s) : base(loc, s)
        {
            RenderFrame += OnRenderFrame;
        }

        private void OnRenderFrame(object sender, FrameEventArgs frameEventArgs)
        {
            DrawingTools.DrawRectangle(0, 0, Size.Width, Size.Height, new ColorBrush(BackgroudColor));
            DrawingTools.DrawRectangle(5, 5, (int)((float)(Size.Width - 10) / MaxValue * Value), Size.Height - 10, new ColorBrush(ForeColor));
            if (!string.IsNullOrWhiteSpace(Text))
            {
                var sz = FontRenderer.Measure(Text, Atlas);
                FontRenderer.Render(Text, Atlas, (Size.Width - sz.Width) / 2, (Size.Height - sz.Height) / 2,
                    new ColorBrush(TextColor));
            }
        }
    }
}