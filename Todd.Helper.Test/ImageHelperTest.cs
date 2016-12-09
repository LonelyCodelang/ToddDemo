using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Todd.Helper.Test
{
    public class ImageHelperTest
    {
        public ImageHelperTest()
        {

        }

        [Fact]
        public void GetPictureDegreeTest()
        {
            string imagePath = @"D:\test\外采图片\三星\2x1.jpg";
            //string imagePath = @"D:\test\外采图片\1\9b89bcfb-c92e-45bd-81ac-74c8d48b6c70.jpg";
            int Degree = ImageHelper.GetPictureDegree(imagePath);


            int Degree2 = ImageHelper.GetPictureDegree(@"D:\test\外采图片\1\2.jpg");
            int Degree3 = ImageHelper.GetPictureDegree(@"D:\test\外采图片\1\3.jpg");
            int Degree4 = ImageHelper.GetPictureDegree(@"D:\test\外采图片\1\4.jpg");
            int degree5 = ImageHelper.GetPictureDegree(@"C:\Users\Administrator\Documents\Tencent Files\447111964\FileRecv\60a98712-bb59-4fe1-8357-f10c5d6458a4.jpg");
        }

        [Fact]
        public void KiRotateTest()
        {
            try
            {
                string imagePath = @"D:\test\外采图片\三星\2.jpg";
                string saveImagePath = @"D:\test\外采图片\三星\2x1.jpg";
                ImageHelper.RotateImg(imagePath, saveImagePath, RotateFlipType.Rotate90FlipNone);
                // ImageHelper.RotateImg(imagePath, @"D:\test\外采图片\1\2.jpg", RotateFlipType.Rotate90FlipNone);
                //ImageHelper.RotateImg(imagePath, @"D:\test\外采图片\1\3.jpg", RotateFlipType.Rotate180FlipNone);
                //ImageHelper.RotateImg(imagePath, @"D:\test\外采图片\1\4.jpg", RotateFlipType.Rotate270FlipNone);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Fact]
        public void Imagetest()
        {
            string imagePath = @"D:\test\外采图片\三星\2.jpg";
            // string saveImagePath = @"D:\test\外采图片\三星\2x.jpg";
            string saveImagePath = @"D:\test\外采图片\三星\2rx.jpg";
            // Image img = ImageHelper.RotateImage(imagePath);
            // img.Save(saveImagePath, ImageFormat.Jpeg);


            // Image img = ImageHelper.RotateAndSaveImage(Image.FromFile(imagePath), 90);
            // img.Save(saveImagePath, ImageFormat.Jpeg);

            //ImageHelper.RotateAndSaveImage(imagePath, saveImagePath, 90);

            string[] files = Directory.GetFiles(@"D:\test\外采图片\三星\");
            foreach (var item in files)
            {
                string newImg = @"D:\test\外采图片\三星\test\" + Guid.NewGuid().ToString() + ".jpg";
                ImageHelper.RotateImg1(item, newImg, 90, Color.Transparent);
            }


        }
    }
}
