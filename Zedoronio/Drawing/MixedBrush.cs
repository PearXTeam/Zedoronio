namespace Zedoronio.Drawing
{
    public class MixedBrush : Brush
    {
        public Brush Main, Secondary;

        public MixedBrush(Brush main, Brush secondary)
        {
            Main = main;
            Secondary = secondary;
        }

        public override void BeforeDraw()
        {
            Main.BeforeDraw();
            Secondary.BeforeDraw();
        }

        public override void StartBegin()
        {
            Main.StartBegin();
            Secondary.StartBegin();
        }

        public override void BeforeVertex(int vert)
        {
            Main.BeforeVertex(vert);
            Secondary.BeforeVertex(vert);
        }

        public override void AfterDraw()
        {
            Main.AfterDraw();
            Secondary.AfterDraw();
        }
    }
}