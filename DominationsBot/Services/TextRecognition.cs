using System;
using System.Drawing;
using Tesseract;

namespace DominationsBot.Services
{
    public class TextRecognition
    {
        private readonly TesseractEngine _engine;

        public TextRecognition(TesseractEngine engine)
        {
            _engine = engine;
        }

        public string GetText(Bitmap bitmap)
        {
            string result;
            using (var page = _engine.Process(bitmap, PageSegMode.SingleWord))
            {
                result = page.GetText().Trim();
            }
            return result;
        }

        public T GetText<T>(Bitmap bitmap) where T : struct
        {
            var imageToText = GetText(bitmap);
            var result = Convert.ChangeType(imageToText, typeof(T));
            return (T)result;
        }
    }
}