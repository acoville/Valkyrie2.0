using SkiaSharp;

namespace Valkyrie.Graphics
{
    public interface IDrawable
    {
        SKBitmap DisplayImage { get; set; }
        SKPosition SKPosition { get; set; }
        
        void Mirror();
        //void Scale();
        //void Filter();
    }
}
