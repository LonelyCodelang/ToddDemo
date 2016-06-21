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
        const string helloWorld = "Hello World!";

        static void Main(string[] args)
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
                gRender.WriteToStream(matrix, ImageFormat.Png, stream, new Point(1200, 1200));
            }
        }


    }
}
