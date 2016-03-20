using System;
using System.Drawing;
using AForge;
using AForge.Imaging.Filters;
using Tesseract;

namespace DominationsBot.Services.ImageProcessing
{
    public class TextRecognition:ITextRecognition
    {
        private readonly TesseractEngine _engine;

        public TextRecognition(TesseractEngine engine)
        {
            _engine = engine;
        }

        public string GetText(Bitmap bitmap)
        {
            // create filter
            ColorFiltering filter = new ColorFiltering();
            // set color ranges to keep
            filter.Red = new IntRange(250, 255);
            filter.Green = new IntRange(245, 255);
            filter.Blue = new IntRange(200, 220);
            // apply the filter
            filter.ApplyInPlace(bitmap);
            bitmap.Save("123.png");
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

    public interface ITextRecognition
    {
        T GetText<T>(Bitmap bitmap) where T : struct;
        string GetText(Bitmap bitmap);
    }
}