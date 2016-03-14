using Puma.Net;
using System;
using System.Drawing;

namespace DominationsBot.Services
{
    public class TextRecognition
    {
        public string ImageToText(Bitmap bitmap)
        {
            string result = "";

            //Распознавание
            PumaPage image = new PumaPage(bitmap);
            using (image)
            {
                image.FileFormat = PumaFileFormat.TxtAscii;
                image.AutoRotateImage = true;
                image.EnableSpeller = false;
                image.RecognizeTables = true;
                image.FontSettings.DetectItalic = true;
                image.Language = PumaLanguage.Russian;

                try
                {
                    result = image.RecognizeToString();
                }
                catch (Exception)
                {
                    image.Dispose();
                }
            }

            return result;
        }
    }
}
