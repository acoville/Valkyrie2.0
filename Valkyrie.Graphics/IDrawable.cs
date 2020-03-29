/*===========================================================
 * 
 * 
 * 
 * 
 * ========================================================*/

using SkiaSharp;

namespace Valkyrie.Graphics
{
    public interface IDrawable
    {
        SKBitmap DisplayImage { get; set; }
        SKPosition SKPosition { get; set; }
        
        void Mirror();
        //void Scale();
        void Move(SKPosition target);
        void Filter(SKColor maskColor);
        void Filter(byte R, byte B, byte G, byte Alpha);
    }
}
