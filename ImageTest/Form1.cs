using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.Formats;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageTest
{
    public partial class Form1 : Form
    {
        Bitmap img1, img2;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "所有檔案(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img1 = ImageDecoder.DecodeFromFile(openFileDialog1.FileName);
                pictureBox1.Image = img1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            img2 = filter.Apply(img1);
            pictureBox2.Image = img2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Grayscale grayfilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayImg = grayfilter.Apply(img1);

            OtsuThreshold filter = new OtsuThreshold();
            img2 = filter.Apply(grayImg);
            pictureBox2.Image = img2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Grayscale grayfilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap edgeImg = grayfilter.Apply(img1);

            OtsuThreshold filter = new OtsuThreshold();
            filter.ApplyInPlace(edgeImg);

            CannyEdgeDetector filter2 = new CannyEdgeDetector();
            img2 = filter2.Apply(edgeImg);
            pictureBox2.Image = img2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExtractChannel filter = new ExtractChannel(RGB.B);
            img2 = filter.Apply(img1);
            pictureBox2.Image = img2;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = @"imgone.jpg";
        //    saveFileDialog1.Filter = "Bitmap file(*.bmp)|*.bmp";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img2.Save(saveFileDialog1.FileName);
            }
        }
    }
}
