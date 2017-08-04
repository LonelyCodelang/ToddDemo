using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Todd.Helper
{
    /// <summary>
    /// 图片处理
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>  
        /// 获取原图旋转角度(IOS和Android相机拍的照片)  
        /// </summary>  
        /// <param name="path">图片路径</param>  
        /// <returns>图片旋转角度</returns>  
        public static int GetPictureDegree(string path)
        {
            int rotate = 0;
            using (var image = System.Drawing.Image.FromFile(path))
            {
                foreach (var prop in image.PropertyItems)
                {
                    if (prop.Id == 0x112)
                    {
                        string str = System.Text.Encoding.Default.GetString(prop.Value);
                        if (prop.Value[0] == 6)
                            rotate = 90;
                        if (prop.Value[0] == 8)
                            rotate = -90;
                        if (prop.Value[0] == 3)
                            rotate = 180;
                        prop.Value[0] = 1;
                    }
                }
            }
            return rotate;
        }

        /// <summary>
        /// 以逆时针为方向对图像进行旋转(此方法不会修改图片的角度)
        /// </summary>
        /// <param name="imgPath">图片路径</param>
        /// <param name="angle">旋转角度[0,360]</param>
        /// <returns></returns>
        public static void RotateImg(string imgPath, string NewimgPath, RotateFlipType rotateFlipType)
        {
            Bitmap b1 = new Bitmap(imgPath);
            b1.RotateFlip(rotateFlipType);
            b1.Save(NewimgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        /// <summary>
        /// 顺时针旋转图片(部分图片有黑点)
        /// </summary>
        /// <param name="oldBitmap"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Bitmap RotateImageByAngle(System.Drawing.Image oldBitmap, float angle)
        {
            var newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height);
            //Set the resolution for the rotation image
            // rotateImage.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
            //Create a graphics object
            var graphics = Graphics.FromImage(newBitmap);
            graphics.TranslateTransform((float)oldBitmap.Width / 2, (float)oldBitmap.Height / 2);
            graphics.RotateTransform(angle);
            graphics.TranslateTransform(-(float)oldBitmap.Width / 2, -(float)oldBitmap.Height / 2);
            graphics.DrawImage(newBitmap, new System.Drawing.Point(0, 0));
            return newBitmap;
        }

        /// <summary>
        /// 图片旋转
        /// </summary>
        /// <param name="imgPath">图片路径</param>
        /// <param name="newImgPath">旋转后的图片路径</param>
        /// <param name="angle">旋转角度</param>
        /// <param name="bkColor">颜色</param>
        public static void RotateImg1(string imgPath, string newImgPath, float angle, Color bkColor)
        {
            Bitmap bmp = null;
            Bitmap tempImg = null;
            Bitmap newImg = null;
            try
            {
                bmp = new Bitmap(imgPath);
                int w = bmp.Width;
                int h = bmp.Height;
                PixelFormat pf = default(PixelFormat);
                if (bkColor == Color.Transparent)
                {
                    pf = PixelFormat.Format32bppArgb;//Format32bppArgb;
                }
                else
                {
                    pf = bmp.PixelFormat;
                }

                tempImg = new Bitmap(w, h, pf);
                Graphics g = Graphics.FromImage(tempImg);
                g.Clear(bkColor);
                g.DrawImageUnscaled(bmp, 1, 1);
                g.Dispose();

                GraphicsPath path = new GraphicsPath();
                path.AddRectangle(new RectangleF(0f, 0f, w, h));
                Matrix mtrx = new Matrix();
                //Using System.Drawing.Drawing2D.Matrix class 
                mtrx.Rotate(angle);
                RectangleF rct = path.GetBounds(mtrx);
                newImg = new Bitmap(Convert.ToInt32(rct.Width), Convert.ToInt32(rct.Height), pf);
                g = Graphics.FromImage(newImg);
                g.Clear(bkColor);
                g.TranslateTransform(-rct.X, -rct.Y);
                g.RotateTransform(angle);
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g.DrawImageUnscaled(tempImg, 0, 0);
                g.Dispose();
                newImg.Save(newImgPath, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                }
                if (tempImg != null)
                {
                    tempImg.Dispose();
                }
                if (newImg != null)
                {
                    tempImg.Dispose();
                }
            }
        }


        /// <summary>
        /// 将图片转成正图（部分图片转换会出问题，丢失部分图）
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <param name="newImagePath">新的图片保存路径</param>
        public static void SetImageRotate(string imagePath, string newImagePath)
        {
            int rotate = ImageHelper.GetPictureDegree(imagePath);
            Bitmap bit = null;
            Image img = Image.FromFile(imagePath);
            if (rotate == 90)
            {
                bit = ImageHelper.RotateImageByAngle(img, 90);
            }
            else if (rotate == -90)
            {
                bit = ImageHelper.RotateImageByAngle(img, 270);
            }
            else if (rotate == 180)
            {
                bit = ImageHelper.RotateImageByAngle(img, 180);
            }

            bit.Save(newImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        /// <summary>
        /// 图片压缩
        /// </summary>
        /// <param name="inputImage"></param>
        /// <param name="angleDegrees"></param>
        /// <param name="upsizeOk"></param>
        /// <param name="clipOk"></param>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
        public static Bitmap RotateImage(Image inputImage, float angleDegrees, bool upsizeOk,
                                  bool clipOk, Color backgroundColor)
        {
            // Test for zero rotation and return a clone of the input image
            if (angleDegrees == 0f)
                return (Bitmap)inputImage.Clone();

            // Set up old and new image dimensions, assuming upsizing not wanted and clipping OK
            int oldWidth = inputImage.Width;
            int oldHeight = inputImage.Height;
            int newWidth = oldWidth;
            int newHeight = oldHeight;
            float scaleFactor = 1f;

            // If upsizing wanted or clipping not OK calculate the size of the resulting bitmap
            if (upsizeOk || !clipOk)
            {
                double angleRadians = angleDegrees * Math.PI / 180d;

                double cos = Math.Abs(Math.Cos(angleRadians));
                double sin = Math.Abs(Math.Sin(angleRadians));
                newWidth = (int)Math.Round(oldWidth * cos + oldHeight * sin);
                newHeight = (int)Math.Round(oldWidth * sin + oldHeight * cos);
            }

            // If upsizing not wanted and clipping not OK need a scaling factor
            if (!upsizeOk && !clipOk)
            {
                scaleFactor = Math.Min((float)oldWidth / newWidth, (float)oldHeight / newHeight);
                newWidth = oldWidth;
                newHeight = oldHeight;
            }

            // Create the new bitmap object. If background color is transparent it must be 32-bit, 
            //  otherwise 24-bit is good enough.
            Bitmap newBitmap = new Bitmap(newWidth, newHeight, backgroundColor == Color.Transparent ?
                                             PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
            newBitmap.SetResolution(inputImage.HorizontalResolution, inputImage.VerticalResolution);

            // Create the Graphics object that does the work
            using (Graphics graphicsObject = Graphics.FromImage(newBitmap))
            {
                graphicsObject.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsObject.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphicsObject.SmoothingMode = SmoothingMode.HighQuality;

                // Fill in the specified background color if necessary
                if (backgroundColor != Color.Transparent)
                    graphicsObject.Clear(backgroundColor);

                // Set up the built-in transformation matrix to do the rotation and maybe scaling
                graphicsObject.TranslateTransform(newWidth / 2f, newHeight / 2f);

                if (scaleFactor != 1f)
                    graphicsObject.ScaleTransform(scaleFactor, scaleFactor);

                graphicsObject.RotateTransform(angleDegrees);
                graphicsObject.TranslateTransform(-oldWidth / 2f, -oldHeight / 2f);

                // Draw the result 
                graphicsObject.DrawImage(inputImage, 0, 0);
            }

            return newBitmap;
        }


        /// <summary>
        /// 图片黑白处理
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Bitmap GrayImage(Bitmap bmp, int type)
        {
            int height = bmp.Height;
            int width = bmp.Width;
            Bitmap newbmp = new Bitmap(width, height);

            LockBitmap lbmp = new LockBitmap(bmp);
            LockBitmap newlbmp = new LockBitmap(newbmp);
            lbmp.LockBits();
            newlbmp.LockBits();

            Color pixel;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    pixel = lbmp.GetPixel(x, y);
                    int r, g, b, Result = 0;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    switch (type)
                    {
                        case 0://平均值法
                            Result = ((r + g + b) / 3);
                            break;
                        case 1://最大值法
                            Result = r > g ? r : g;
                            Result = Result > b ? Result : b;
                            break;
                        case 2://加权平均值法
                            Result = ((int)(0.3 * r) + (int)(0.59 * g) + (int)(0.11 * b));
                            break;
                    }
                    newlbmp.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                }
            }
            lbmp.UnlockBits();
            newlbmp.UnlockBits();
            return newbmp;
        }


        /// <summary>
        /// 重新设置图片大小
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Bitmap ResizeImage(Bitmap bmp, Size size)
        {
            Bitmap newbmp = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(newbmp))
            {
                g.DrawImage(bmp, new Rectangle(Point.Empty, size));
            }
            return newbmp;
        }


        /// <summary>
        /// 底片效果（反色）
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap NegativeImage(Bitmap bmp)
        {
            int height = bmp.Height;
            int width = bmp.Width;
            Bitmap newbmp = new Bitmap(width, height);

            LockBitmap lbmp = new LockBitmap(bmp);
            LockBitmap newlbmp = new LockBitmap(newbmp);
            lbmp.LockBits();
            newlbmp.LockBits();

            Color pixel;
            for (int x = 1; x < width; x++)
            {
                for (int y = 1; y < height; y++)
                {
                    int r, g, b;
                    pixel = lbmp.GetPixel(x, y);
                    r = 255 - pixel.R;
                    g = 255 - pixel.G;
                    b = 255 - pixel.B;
                    newlbmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            lbmp.UnlockBits();
            newlbmp.UnlockBits();
            return newbmp;
        }

        //浮雕
        public static Bitmap EmbossmentImage(Bitmap bmp)
        {
            int height = bmp.Height;
            int width = bmp.Width;
            Bitmap newbmp = new Bitmap(width, height);

            LockBitmap lbmp = new LockBitmap(bmp);
            LockBitmap newlbmp = new LockBitmap(newbmp);
            lbmp.LockBits();
            newlbmp.LockBits();

            Color pixel1, pixel2;
            for (int x = 0; x < width - 1; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    pixel1 = lbmp.GetPixel(x, y);
                    pixel2 = lbmp.GetPixel(x + 1, y + 1);
                    r = Math.Abs(pixel1.R - pixel2.R + 128);
                    g = Math.Abs(pixel1.G - pixel2.G + 128);
                    b = Math.Abs(pixel1.B - pixel2.B + 128);
                    if (r > 255)
                        r = 255;
                    if (r < 0)
                        r = 0;
                    if (g > 255)
                        g = 255;
                    if (g < 0)
                        g = 0;
                    if (b > 255)
                        b = 255;
                    if (b < 0)
                        b = 0;
                    newlbmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            lbmp.UnlockBits();
            newlbmp.UnlockBits();
            return newbmp;
        }

        /// <summary>
        /// 图片黑白处理（打印出来会有绿色）
        /// </summary>
        /// <param name="imgPath">要处理的图片路径</param>
        /// <param name="newImgPath">处理后的图片存储路径</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static void GrayImage(string imgPath, string newImgPath)
        {
            try
            {
                Bitmap original = new Bitmap(imgPath);
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
                newBitmap.Save(newImgPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 图片黑白处理
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="NewImgPath"></param>
        public static void GrayScale(string imgPath, string NewImgPath)
        {
            try
            {
                Bitmap Bmp = new Bitmap(imgPath);
                int rgb;
                Color c;

                for (int y = 0; y < Bmp.Height; y++)
                    for (int x = 0; x < Bmp.Width; x++)
                    {
                        c = Bmp.GetPixel(x, y);
                        rgb = (int)((c.R + c.G + c.B) / 3);
                        Bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                    }
                Bmp.Save(NewImgPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 图片黑白处理
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="NewImgPath"></param>
        public static void GraySelva(string imgPath, string NewImgPath)
        {
            var originalbmp = new Bitmap(imgPath); // Load the  image
            var newbmp = new Bitmap(imgPath); // New image
            for (int row = 0; row < originalbmp.Width; row++) // Indicates row number
            {
                for (int column = 0; column < originalbmp.Height; column++) // Indicate column number
                {
                    var colorValue = originalbmp.GetPixel(row, column); // Get the color pixel
                    var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 3; // get the average for black and white
                    newbmp.SetPixel(row, column, Color.FromArgb(averageValue, averageValue, averageValue)); // Set the value to new pixel
                }
            }
            newbmp.Save(NewImgPath);
        }

        /// <summary>  
        /// 判断图片是否是黑白  
        /// </summary>  
        /// <param name="filename">图片文件路径</param>  
        /// <returns></returns>  
        public static bool isBlackWhite(string filename)
        {
            Color c = new Color();

            using (Bitmap bmp = new Bitmap(filename))
            {
                //遍历图片的像素点  
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        c = bmp.GetPixel(x, y);

                        //判断像素点的色偏差值Diff  
                        if (GetRGBDiff(c.R, c.G, c.B) > 50)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public static int GetRGBDiff(int r, int g, int b)
        {
            //略，很简单的，就是取r-g,r-b,g-b绝对值的最大值。
             int v1= Math.Max(Math.Abs(r - g), Math.Abs(r - b));

            return Math.Max(v1, Math.Abs(g - b));
        }


        /// <summary>
        /// 图片黑白处理
        /// </summary>
        /// <param name="imgPath">要处理的图片路径</param>
        /// <param name="newImgPath">处理后的图片存储路径</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static void GrayImage1(string imgPath, string newImgPath)
        {
            try
            {
                Bitmap original = new Bitmap(imgPath);
                Bitmap newBitmap = new Bitmap(original.Width, original.Height);

                //get a graphics object from the new image
                Graphics g = Graphics.FromImage(newBitmap);

                //create the grayscale ColorMatrix
                ColorMatrix colorMatrix = new ColorMatrix(
                   new float[][]
                   {
                               new float[] { 0.299f, 0.299f, 0.299f, 0, 0 },
                new float[] { 0.587f, 0.587f, 0.587f, 0, 0 },
                new float[] { 0.114f, 0.114f, 0.114f, 0, 0 },
                new float[] { 0,      0,      0,      1, 0 },
                new float[] { 0,      0,      0,      0, 1 }
                   });

                //create some image attributes
                ImageAttributes attributes = new ImageAttributes();

                //set the color matrix attribute
                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                   0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

                //dispose the Graphics object
                g.Dispose();
                newBitmap.Save(newImgPath);
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// 将位图转换为灰度数组（256级灰度）
        /// </summary>
        /// <param name="bmp">原始位图</param>
        /// <returns>灰度数组</returns>
        public static Byte[,] ToGrayArray(this Bitmap bmp)
        {
            Int32 PixelHeight = bmp.Height; // 图像高度
            Int32 PixelWidth = bmp.Width;   // 图像宽度
            Int32 Stride = ((PixelWidth * 3 + 3) >> 2) << 2;    // 跨距宽度
            Byte[] Pixels = new Byte[PixelHeight * Stride];

            // 锁定位图到系统内存
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, PixelWidth, PixelHeight), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Marshal.Copy(bmpData.Scan0, Pixels, 0, Pixels.Length);  // 从非托管内存拷贝数据到托管内存
            bmp.UnlockBits(bmpData);    // 从系统内存解锁位图

            // 将像素数据转换为灰度数组
            Byte[,] GrayArray = new Byte[PixelHeight, PixelWidth];
            for (Int32 i = 0; i < PixelHeight; i++)
            {
                Int32 Index = i * Stride;
                for (Int32 j = 0; j < PixelWidth; j++)
                {
                    GrayArray[i, j] = Convert.ToByte((Pixels[Index + 2] * 19595 + Pixels[Index + 1] * 38469 + Pixels[Index] * 7471 + 32768) >> 16);
                    Index += 3;
                }
            }

            return GrayArray;
        }

        /// <summary>
        /// 全局阈值图像二值化
        /// </summary>
        /// <param name="bmp">原始图像</param>
        /// <param name="method">二值化方法</param>
        /// <param name="threshold">输出：全局阈值</param>
        /// <returns>二值化后的图像数组</returns>        
        public static Byte[,] ToBinaryArray(this Bitmap bmp, BinarizationMethods method, out Int32 threshold)
        {   // 位图转换为灰度数组
            Byte[,] GrayArray = bmp.ToGrayArray();

            // 计算全局阈值
            if (method == BinarizationMethods.Otsu)
                threshold = OtsuThreshold(GrayArray);
            else
                threshold = IterativeThreshold(GrayArray);

            // 根据阈值进行二值化
            Int32 PixelHeight = bmp.Height;
            Int32 PixelWidth = bmp.Width;
            Byte[,] BinaryArray = new Byte[PixelHeight, PixelWidth];
            for (Int32 i = 0; i < PixelHeight; i++)
            {
                for (Int32 j = 0; j < PixelWidth; j++)
                {
                    BinaryArray[i, j] = Convert.ToByte((GrayArray[i, j] > threshold) ? 255 : 0);
                }
            }

            return BinaryArray;
        }

        /// <summary>
        /// 大津法计算阈值
        /// </summary>
        /// <param name="grayArray">灰度数组</param>
        /// <returns>二值化阈值</returns> 
        public static Int32 OtsuThreshold(Byte[,] grayArray)
        {   // 建立统计直方图
            Int32[] Histogram = new Int32[256];
            Array.Clear(Histogram, 0, 256);     // 初始化
            foreach (Byte b in grayArray)
            {
                Histogram[b]++;                 // 统计直方图
            }

            // 总的质量矩和图像点数
            Int32 SumC = grayArray.Length;    // 总的图像点数
            Double SumU = 0;                  // 双精度避免方差运算中数据溢出
            for (Int32 i = 1; i < 256; i++)
            {
                SumU += i * Histogram[i];     // 总的质量矩                
            }

            // 灰度区间
            Int32 MinGrayLevel = Array.FindIndex(Histogram, NonZero);       // 最小灰度值
            Int32 MaxGrayLevel = Array.FindLastIndex(Histogram, NonZero);   // 最大灰度值

            // 计算最大类间方差
            Int32 Threshold = MinGrayLevel;
            Double MaxVariance = 0.0;       // 初始最大方差
            Double U0 = 0;                  // 初始目标质量矩
            Int32 C0 = 0;                   // 初始目标点数
            for (Int32 i = MinGrayLevel; i < MaxGrayLevel; i++)
            {
                if (Histogram[i] == 0) continue;

                // 目标的质量矩和点数                
                U0 += i * Histogram[i];
                C0 += Histogram[i];

                // 计算目标和背景的类间方差
                Double Diference = U0 * SumC - SumU * C0;
                Double Variance = Diference * Diference / C0 / (SumC - C0); // 方差
                if (Variance > MaxVariance)
                {
                    MaxVariance = Variance;
                    Threshold = i;
                }
            }

            // 返回类间方差最大阈值
            return Threshold;
        }

        /// <summary>
        /// 检测非零值
        /// </summary>
        /// <param name="value">要检测的数值</param>
        /// <returns>
        ///     true：非零
        ///     false：零
        /// </returns>
        private static Boolean NonZero(Int32 value)
        {
            return (value != 0) ? true : false;
        }

        /// <summary>
        /// 迭代法计算阈值
        /// </summary>
        /// <param name="grayArray">灰度数组</param>
        /// <returns>二值化阈值</returns> 
        public static Int32 IterativeThreshold(Byte[,] grayArray)
        {   // 建立统计直方图
            Int32[] Histogram = new Int32[256];
            Array.Clear(Histogram, 0, 256);     // 初始化
            foreach (Byte b in grayArray)
            {
                Histogram[b]++;                 // 统计直方图
            }

            // 总的质量矩和图像点数
            Int32 SumC = grayArray.Length;    // 总的图像点数
            Int32 SumU = 0;
            for (Int32 i = 1; i < 256; i++)
            {
                SumU += i * Histogram[i];     // 总的质量矩                
            }

            // 确定初始阈值
            Int32 MinGrayLevel = Array.FindIndex(Histogram, NonZero);       // 最小灰度值
            Int32 MaxGrayLevel = Array.FindLastIndex(Histogram, NonZero);   // 最大灰度值
            Int32 T0 = (MinGrayLevel + MaxGrayLevel) >> 1;
            if (MinGrayLevel != MaxGrayLevel)
            {
                for (Int32 Iteration = 0; Iteration < 100; Iteration++)
                {   // 计算目标的质量矩和点数
                    Int32 U0 = 0;
                    Int32 C0 = 0;
                    for (Int32 i = MinGrayLevel; i <= T0; i++)
                    {   // 目标的质量矩和点数                
                        U0 += i * Histogram[i];
                        C0 += Histogram[i];
                    }

                    // 目标的平均灰度值和背景的平均灰度值的中心值
                    Int32 T1 = (U0 / C0 + (SumU - U0) / (SumC - C0)) >> 1;
                    if (T0 == T1) break; else T0 = T1;
                }
            }

            // 返回最佳阈值
            return T0;
        }

    }

    /// <summary>
    /// 图像二值化方法：大津法和迭代法
    /// </summary>
    public enum BinarizationMethods
    {
        Otsu,       // 大津法
        Iterative   // 迭代法
    }




}
