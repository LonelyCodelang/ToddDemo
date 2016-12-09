using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Todd.Helper
{
    /// <summary>
    /// 图片处理
    /// </summary>
    public class ImageHelper
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


    }
}
