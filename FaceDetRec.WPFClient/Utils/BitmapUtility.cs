using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace FaceDetRec.WPFClient.Utils
{
    public class BitmapUtility
    {
        public static Bitmap CropImage(Bitmap sourceBitmap, Rectangle sectionRectangle)
        {
            // An empty bitmap which will hold the cropped image
            var bitmap = new Bitmap(sectionRectangle.Width, sectionRectangle.Height);
            var graphics = Graphics.FromImage(bitmap);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            graphics.DrawImage(sourceBitmap, 0, 0, sectionRectangle, GraphicsUnit.Pixel);

            return bitmap;
        }

        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            var stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Bmp);

            return stream.GetBuffer();
        }
    }
}
