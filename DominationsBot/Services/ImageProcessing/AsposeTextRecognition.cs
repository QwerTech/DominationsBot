using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.OCR;
using Tesseract;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace DominationsBot.Services.ImageProcessing
{
    public class AsposeTextRecognition : ITextRecognition
    {
        private readonly OcrEngine _engine;

        public AsposeTextRecognition(OcrEngine engine)
        {
            _engine = engine;
        }

        public string GetText(Bitmap bitmap)
        {

            //Retrieve the OcrConfig of the OcrEngine object
            OCRConfig ocrConfig = _engine.Config;
            _engine.Config.RemoveNonText = true;
            _engine.Config.DetectReadingOrder = true;
            _engine.Config.DetectTextRegions = true;
            //Set the Whitelist property to recognize numbers only
            ocrConfig.Whitelist = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream,ImageFormat.Png);
            _engine.Image = ImageStream.FromStream(memoryStream, ImageStreamFormat.Png);
            _engine.Process();
            var  result= _engine.Text.ToString();
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
