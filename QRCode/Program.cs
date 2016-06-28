using Gma.QrCodeNet;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCode
{
    class Program
    {


        static void Main(string[] args)
        {
            GenerateLogoQr();
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        public static void GenerateQr()
        {
            QrEncoder encoder = new QrEncoder();


            QrCode qrCode;
            // byte[] byteArray = new byte[] { 34, 54, 90, 200 };
            string url = "http://www.yunfangdata.com";
            if (!encoder.TryEncode(url, out qrCode))
                return;

            //GraphicsRenderer gRender = new GraphicsRenderer(new FixedModuleSize(30, QuietZoneModules.Four));
            var gRender = new GraphicsRenderer(new FixedModuleSize(12, QuietZoneModules.Two));
            BitMatrix matrix = qrCode.Matrix;
            using (FileStream stream = new FileStream(@"D:\test\11111.jpg", FileMode.Create))
            {
                gRender.WriteToStream(matrix, ImageFormat.Png, stream, new Point(600, 600));
            }
        }

        /// <summary>
        /// 生成带logo的二维码
        /// </summary>
        public static void GenerateLogoQr()
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = qrEncoder.Encode("http://www.yunfangdata.com");
            //保存成png文件
            string filename = System.Environment.CurrentDirectory + "\\二维码.png"; //@"H:\桌面\截图\logo.png";
            GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(10, QuietZoneModules.Two), Brushes.Black, Brushes.White);

            DrawingSize dSize = render.SizeCalculator.GetSize(qrCode.Matrix.Width);
            Bitmap map = new Bitmap(dSize.CodeWidth, dSize.CodeWidth);
            Graphics g = Graphics.FromImage(map);
            render.Draw(g, qrCode.Matrix);
            //追加Logo图片 ,注意控制Logo图片大小和二维码大小的比例
            Image img = Image.FromFile(System.Environment.CurrentDirectory + "\\img\\logo2.png");

            Point imgPoint = new Point((map.Width - img.Width) / 2, (map.Height - img.Height) / 2);
            g.DrawImage(img, imgPoint.X, imgPoint.Y, img.Width, img.Height);
            map.Save(filename, ImageFormat.Png);
        }
    }
}
