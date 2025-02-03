using System.IO;

namespace DanfeSharp
{
    public static class ImagemSuportada
    {
        public static bool IsSuportada(Stream imageStream)
        {
            imageStream.Position = 0;
            try
            {
                var img = org.pdfclown.documents.contents.entities.Image.Get(imageStream);
                return img != null;
            }
            catch { }
            finally
            {
                imageStream.Position = 0;
            }

            return false;
        }
    }
}