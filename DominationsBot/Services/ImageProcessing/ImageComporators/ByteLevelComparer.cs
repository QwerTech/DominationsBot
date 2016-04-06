using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace DominationsBot.Services.ImageProcessing.ImageComporators
{
    public class ByteLevelComparer : IImageComparer
    {
        public bool Compare(Bitmap one, Bitmap another)
        {
            return GetSimilarityPersent(one, another, 0)==100;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="one"></param>
        /// <param name="another"></param>
        /// <param name="allowedErrorPercent"></param>
        /// <returns>100 - equal
        /// 0 - absolutely differrent</returns>
        public unsafe byte GetSimilarityPersent(Bitmap one, Bitmap another, int allowedErrorPercent = 100)
        {
            if (one.Size != another.Size)
            {
                throw new InvalidOperationException("Разные размеры");
            }
            if (one.PixelFormat != another.PixelFormat)
            {
                throw new InvalidOperationException("Разные PixelFormat");
            }

            var rect = new Rectangle(0, 0, one.Width, one.Height);
            var data1= one.LockBits(rect, ImageLockMode.ReadOnly, one.PixelFormat);
            var data2= another.LockBits(rect, ImageLockMode.ReadOnly, one.PixelFormat);

            var p1 = (byte*) data1.Scan0;
            var p2 = (byte*) data2.Scan0;
            var bytePerPixel = Image.GetPixelFormatSize(one.PixelFormat)/8;
            var byteCount = one.Height*data1.Stride/bytePerPixel;

            var allowedErrorBytes = byteCount*bytePerPixel*allowedErrorPercent/100;
            int errorsCount = 0;
            
            for (var i = 0; i < byteCount; ++i)
            {
                for (var j = 0; j < bytePerPixel; ++j)
                {
                    if (*p1 != *p2)
                    {
                        errorsCount++;
                        if (errorsCount >= allowedErrorBytes)
                        {
                            break;
                        }
                    }
                    p1++;
                    p2++;
                }
            }

            one.UnlockBits(data1);
            another.UnlockBits(data2);

            var similarityPersent =(byte)(100- (byte)(errorsCount * 100 / (byteCount*bytePerPixel)));
            return similarityPersent;
        }

    }
}