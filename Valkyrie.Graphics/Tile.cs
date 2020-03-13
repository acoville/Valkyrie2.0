using SkiaSharp;

namespace Valkyrie.Graphics
{
    public class Tile : Drawable
    {
        public Tile(string imageSource, SKRect rect) : base()
        {
            Rectangle = rect;
            SKPosition = new SKPosition(Rectangle.Location);
        }
    }
}
