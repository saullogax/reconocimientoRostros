using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using System.Threading;

namespace ReconocimientoDerostros
{
    public partial class Form1 : Form
    {

        //SE DECLARAN VARIABLES DE CAPTURA
        private VideoCapture _capture;
        private Thread _captureThread;
        List<Image> imgList = new List<Image>();
        int i = 0;
        int total = 0;

        private CascadeClassifier _CascadeClassifier;
        private CascadeClassifier _CascadeClassifier1;
        private CascadeClassifier _CascadeClassifier2;
        private CascadeClassifier _CascadeClassifier3;
        public Form1()
        {
            InitializeComponent();
            _CascadeClassifier = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");
            _CascadeClassifier1 = new CascadeClassifier(Application.StartupPath + "/haarcascade_eye.xml");
            _CascadeClassifier2 = new CascadeClassifier(Application.StartupPath + "/haarcascade_eye_tree_eyeglasses.xml");
            _CascadeClassifier3 = new CascadeClassifier(Application.StartupPath + "/haarcascade_smile.xml");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Open = new OpenFileDialog();
            if (Open.ShowDialog() == DialogResult.OK)
            {
                Image<Bgr, Byte> imagen = new Image<Bgr, byte>(Open.FileName);
                //imageBox1.Image = imagen;

                    using (var ImageFrame = imagen)
                    {
                        if (ImageFrame != null)
                        {
                            var grayFrame = ImageFrame.Convert<Gray, Byte>();
                            //var grayFrame1 = ImageFrame.Convert<Gray, Byte>();
                            //var grayFrame2 = ImageFrame.Convert<Gray, Byte>();
                            //var grayFrame3 = ImageFrame.Convert<Gray, Byte>();


                            var faces = _CascadeClassifier.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);
                            var eyes = _CascadeClassifier1.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);
                            var eyesglasses = _CascadeClassifier2.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);
                            var smiles = _CascadeClassifier3.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);

                            foreach (var face in faces)
                            {
                                //Detecta Rostros decimal la imagen
                                ImageFrame.Draw(face, new Bgr(Color.Blue), 3);
                                pictureBox1.Image = ImageFrame.GetSubRect(face).ToBitmap();
                                imgList.Add(ImageFrame.GetSubRect(face).ToBitmap());
                                total = total + 1;

                            }

                        foreach (var eye in eyes)
                        {
                            //Detecta Rostros decimal la imagen
                            ImageFrame.Draw(eye, new Bgr(Color.Red), 3);
                        }
                        foreach (var eyesglasse in eyesglasses)
                        {
                            //Detecta Rostros decimal la imagen
                            ImageFrame.Draw(eyesglasse, new Bgr(Color.Black), 3);
                        }
                        foreach (var smile in smiles)
                        {
                            //Detecta Rostros decimal la imagen
                            ImageFrame.Draw(smile, new Bgr(Color.Brown), 3);
                        }
                    }
                    imageBox1.Image = ImageFrame;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _capture = new VideoCapture();
            _captureThread = new Thread(DisplayWebCam);
            _captureThread.Start();
        }

        private void DisplayWebCam()
        {
            while (_capture.IsOpened)
            {
                Mat frame = _capture.QueryFrame();
                //CvInvoke.Resize(frame, frame, imageBox1.Size);
                imageBox1.Image = frame;

                using (var ImageFrame = frame.ToImage<Bgr, Byte>())
                {
                    if (ImageFrame != null)
                    {
                        var grayFrame = ImageFrame.Convert<Gray, Byte>();
                        var grayFrame1 = ImageFrame.Convert<Gray, Byte>();
                        var grayFrame2 = ImageFrame.Convert<Gray, Byte>();
                        var grayFrame3 = ImageFrame.Convert<Gray, Byte>();
                        
                        var faces = _CascadeClassifier.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);
                        var eyes = _CascadeClassifier1.DetectMultiScale(grayFrame1, 1.1, 10, Size.Empty);
                        var eyesglasses = _CascadeClassifier2.DetectMultiScale(grayFrame2, 1.1, 10, Size.Empty);
                        var smiles = _CascadeClassifier3.DetectMultiScale(grayFrame3, 1.1, 10, Size.Empty);

                        foreach (var face in faces)
                        {
                            //Detecta Rostros decimal la imagen
                            ImageFrame.Draw(face, new Bgr(Color.Red), 3);
                        }
                        
                        foreach (var eye in eyes)
                        {
                            //Detecta Rostros decimal la imagen
                            ImageFrame.Draw(eye, new Bgr(Color.Red), 3);
                        }
                        foreach (var eyesglasse in eyesglasses)
                        {
                            //Detecta Rostros decimal la imagen
                            ImageFrame.Draw(eyesglasse, new Bgr(Color.Black), 3);
                        }
                        foreach (var smile in smiles)
                        {
                            //Detecta Rostros decimal la imagen
                            ImageFrame.Draw(smile, new Bgr(Color.Brown), 3);
                        }
                    }
                    imageBox1.Image = ImageFrame;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
            //SE CARGARA EL VIDEO
            _capture = new VideoCapture(open.FileName);
            _captureThread = new Thread(DisplayWebCam);
            _captureThread.Start();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            --i;
            if (i >= 0)
            {
                pictureBox1.Image = imgList[i];
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            i++;
            if (i <= 0)
            {
                pictureBox1.Image = imgList[i];
            }
        }

    }
}