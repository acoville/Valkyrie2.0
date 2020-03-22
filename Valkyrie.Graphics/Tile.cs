using SkiaSharp;

namespace Valkyrie.Graphics
{
    public class Tile : Drawable
    {
        public Tile(string imageSource, SKRect rect, float Z) : base()
        {
            Rectangle = rect;

            float X = Rectangle.Location.X;
            float Y = Rectangle.Location.Y;

            SKPosition = new SKPosition(X, Y, Z);
        }
    }
}
