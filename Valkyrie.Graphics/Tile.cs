using SkiaSharp;
using Valkryie.GL;

namespace Valkyrie.Graphics
{
    public class Tile : Drawable
    {
        //=========================================================

        /*-----------------------------
         * 
         * 
         * 
         * --------------------------*/

        //=========================================================

        public Tile(string imageSource, SKRect rect)
        {
            Rectangle = rect;
            ImageSource = imageSource;
        }

        //========================================================

        /*----------------------------
         * 
         * Translate
         * moves the tile by an 
         * X and Y offset
         * 
         * --------------------------*/

        public void Translate(float deltaX, float deltaY)
        {
            SKPoint origin = SkiaOrigin;

            origin.X += deltaX;
            origin.Y += deltaY;

            SkiaOrigin = origin;
        }

        //===========================================================

        /*---------------------------
         * 
         * Move
         * moves to a given SK coordinate
         * 
         * -------------------------*/

        public void Move(SKPoint target)
        {
            SkiaOrigin = target;
            float height = Rectangle.Height;
            float width = Rectangle.Width;

            Rectangle = new SKRect(
                SkiaOrigin.X,
                SkiaOrigin.X + width,
                SkiaOrigin.Y,
                SkiaOrigin.Y + height);
        }
    }
}
