using System.Drawing;
using System.Security.Cryptography;

namespace DominationsBot.Services.ImageProcessing.ImageComporators
{
    public class HashComparer:IImageComparer
    {

        public bool Compare(Bitmap one, Bitmap another)
        {
            //Test to see if we have the same size of image
            if (one.Size != another.Size)
            
                return false;
            
                //Convert each image to a byte array
                ImageConverter ic =
                       new ImageConverter();
                byte[] btImage1 = new byte[1];
                btImage1 = (byte[])ic.ConvertTo(one, btImage1.GetType());
                byte[] btImage2 = new byte[1];
                btImage2 = (byte[])ic.ConvertTo(another, btImage2.GetType());

                //Compute a hash for each image
                SHA256Managed shaM = new SHA256Managed();
                byte[] hash1 = shaM.ComputeHash(btImage1);
                byte[] hash2 = shaM.ComputeHash(btImage2);

                //Compare the hash values
                for (int i = 0; i < hash1.Length && i < hash2.Length; i++)
                {
                    if (hash1[i] != hash2[i])
                        return false;
                }
            
            return true;
        }
    }
}