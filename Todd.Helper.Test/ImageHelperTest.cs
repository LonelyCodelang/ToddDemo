using ImageProcessor;
using ImageProcessor.Imaging.Filters.Photo;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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


        [Fact]
        public void GrayImagetest()
        {
            string imagePath = @"D:\test\1\xx.png";

            string saveImagePath = @"D:\test\1\2-3.png";

            // ImageHelper.GrayImage(new Bitmap(imagePath), 1).Save(saveImagePath,ImageFormat.Jpeg);

            MakeGrayscale3(new Bitmap(imagePath)).Save(saveImagePath, ImageFormat.Jpeg);

        }

        //图片黑白
        [Fact]
        public void GrayImagetest1()
        {
            string imagePath = @"D:\test\img\123.jpg";

            string saveImagePath = @"D:\test\img\123N222.jpg";

            // ImageHelper.GrayImage(new Bitmap(imagePath), 1).Save(saveImagePath,ImageFormat.Jpeg);

            ImageHelper.GrayScale(imagePath, saveImagePath);

        }

        [Fact]
        public void GrayImagetest2()
        {
            string imagePath = @"D:\test\img\123.jpg";
            string saveImagePath = @"D:\test\img\123N555.jpg";


            //string imagePath = @"D:\test\123\aaa111111.jpg";
            //string saveImagePath = @"D:\test\img\123N555.jpg";


            //string imagePath = @"D:\test\1\xx.png";

            //string saveImagePath = @"D:\test\1\2-4.png";

            // ImageHelper.GrayImage(new Bitmap(imagePath), 1).Save(saveImagePath,ImageFormat.Jpeg);
            // ImageHelper.GrayScale(imagePath, saveImagePath);
            ImageHelper.GraySelva(imagePath, saveImagePath);

        }

        public Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
               {
         new float[] {.3f, .3f, .3f, 0, 0},
         new float[] {.59f, .59f, .59f, 0, 0},
         new float[] {.11f, .11f, .11f, 0, 0},
         new float[] {0, 0, 0, 1, 0},
         new float[] {0, 0, 0, 0, 1}
               });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }


        [Fact]
        public void ImageProcessorTest1()
        {
            string imagePath = @"D:\test\img\123.jpg";
            //string saveImagePath = @"D:\test\img\123N555.jpg";

            //string imagePath = @"D:\test\1\xx.png";
            string saveImagePath = @"D:\test\1\xx_new3.png";

            byte[] photoBytes = File.ReadAllBytes(imagePath);
            // Format is automatically detected though can be changed.
            ISupportedImageFormat format = new JpegFormat { };
            //Size size = new Size(150, 0);
            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory.Load(inStream)
                                    .Filter(MatrixFilters.GreyScale)
                                    .Format(format)
                                    .Save(saveImagePath);
                    }
                    // Do something with the stream.
                }
            }

        }


        [Fact]
        public void isBlackWhiteTest1()
        {
            string filePath = @"D:\test\1\1.png";
            string filePath1 = @"D:\test\1\ttt\2.png";
            string filePath2 = @"D:\test\1\ttt\3.png";
            //ImageHelper.GrayImage1(filePath, filePath1);
            //int i = 0;
            //Byte[,] xx= ImageHelper.ToBinaryArray(new Bitmap(filePath), BinarizationMethods.Otsu, out i);
            //BinaryArrayToBinaryBitmap(xx).Save(filePath2);


            ChangeGray(new Bitmap(filePath)).Save(filePath2);
        }

        /// <summary>
        /// 将二值化数组转换为二值化图像
        /// </summary>
        /// <param name="binaryArray">二值化数组</param>
        /// <returns>二值化图像</returns>
        public static Bitmap BinaryArrayToBinaryBitmap(Byte[,] binaryArray)
        {   // 将二值化数组转换为二值化数据
            Int32 PixelHeight = binaryArray.GetLength(0);
            Int32 PixelWidth = binaryArray.GetLength(1);
            Int32 Stride = ((PixelWidth + 31) >> 5) << 2;
            Byte[] Pixels = new Byte[PixelHeight * Stride];
            for (Int32 i = 0; i < PixelHeight; i++)
            {
                Int32 Base = i * Stride;
                for (Int32 j = 0; j < PixelWidth; j++)
                {
                    if (binaryArray[i, j] != 0)
                    {
                        Pixels[Base + (j >> 3)] |= Convert.ToByte(0x80 >> (j & 0x7));
                    }
                }
            }

            // 创建黑白图像
            Bitmap BinaryBmp = new Bitmap(PixelWidth, PixelHeight, PixelFormat.Format1bppIndexed);

            // 设置调色表
            ColorPalette cp = BinaryBmp.Palette;
            cp.Entries[0] = Color.Black;    // 黑色
            cp.Entries[1] = Color.White;    // 白色
            BinaryBmp.Palette = cp;

            // 设置位图图像特性
            BitmapData BinaryBmpData = BinaryBmp.LockBits(new Rectangle(0, 0, PixelWidth, PixelHeight), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
            Marshal.Copy(Pixels, 0, BinaryBmpData.Scan0, Pixels.Length);
            BinaryBmp.UnlockBits(BinaryBmpData);

            return BinaryBmp;
        }


        public static Bitmap ChangeGray(Bitmap b)
        {
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite,
             PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;   // 扫描的宽度 

            unsafe
            {
                byte* p = (byte*)bmData.Scan0.ToPointer(); // 获取图像首地址
                int nOffset = stride - b.Width * 3;  // 实际宽度与系统宽度的距离
                byte red, green, blue;
                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        p[0] = p[1] = p[2] = (byte)(.299 * red + .587 * green + .114 * blue); // 转换公式
                        p += 3;  // 跳过3个字节处理下个像素点
                    }
                    p += nOffset; // 加上间隔
                }
            }
            b.UnlockBits(bmData); // 解锁
            return b;
        }

    }
}
