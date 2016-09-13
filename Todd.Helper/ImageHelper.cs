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
            var graphics = Graphics.FromImage(b1);
            graphics.TranslateTransform(0, 0, MatrixOrder.Prepend);
            graphics.RotateTransform(90);

            graphics.DrawImage(b1, new Point(0, 0));


            b1.RotateFlip(rotateFlipType);



            b1.Save(NewimgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        /// <summary>
        /// 顺时针旋转图片
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

        public static Bitmap RotateImg1(Bitmap bmp, float angle, Color bkColor)
        {
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

            Bitmap tempImg = new Bitmap(w, h, pf);
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
            Bitmap newImg = new Bitmap(Convert.ToInt32(rct.Width), Convert.ToInt32(rct.Height), pf);
            g = Graphics.FromImage(newImg);
            g.Clear(bkColor);
            g.TranslateTransform(-rct.X, -rct.Y);
            g.RotateTransform(angle);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(tempImg, 0, 0);
            g.Dispose();
            tempImg.Dispose();
            return newImg;
        }


        /// <summary>
        /// 将图片转成正图
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
        /// method to rotate an image either clockwise or counter-clockwise
        /// </summary>
        /// <param name="img">the image to be rotated</param>
        /// <param name="rotationAngle">the angle (in degrees).
        /// NOTE: 
        /// Positive values will rotate clockwise
        /// negative values will rotate counter-clockwise
        /// </param>
        /// <returns></returns>
        public static Image RotateImage(string imgPath)
        {

            Image img = Image.FromFile(imgPath);
            var graphics = Graphics.FromImage(img);

            graphics.DrawImage(img, 10, 10, img.Width, img.Height);
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            graphics.DrawImage(img, 160, 10, img.Width, img.Height);

            //Bitmap b1 = new Bitmap(imgPath);
            //var graphics = Graphics.FromImage(b1);
            //graphics.TranslateTransform(0, 0, MatrixOrder.Prepend);
            //graphics.RotateTransform(90);

            //graphics.DrawImage(b1, new Point(0, 0));


            //b1.RotateFlip(RotateFlipType.Rotate90FlipNone);



            // b1.Save(NewimgPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            return img;
        }
    }
}
