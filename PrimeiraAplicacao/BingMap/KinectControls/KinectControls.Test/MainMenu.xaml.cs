using KinectControls.Test.RegrasMovimentos;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KinectControls.Test
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public KinectSensor _sensor1;
        public int countSelect2 = 0;
        public int countSelect1 = 0;
        public int countSelect3= 0;
        public int countFecharTela = 0;
        MainWindow map;
        PesquisarLocalidade menuLocal;
        KinectManual manual;
        bool flag = false;


        public MainMenu()
        {
            InitializeComponent();
        }

        private void Window_Loaded1(object sender, RoutedEventArgs e)
        {
            _sensor1 = KinectSensor.KinectSensors.Where(s => s.Status == KinectStatus.Connected).FirstOrDefault();

            _sensor1.ColorStream.Enable();
            _sensor1.DepthStream.Enable();
            _sensor1.SkeletonStream.Enable();

            _sensor1.ColorFrameReady += Sensor_ColorFrameReady;
            _sensor1.SkeletonFrameReady += Sensor_SkeletonFrameReady;

            _sensor1.Start();
        }

        private void Window_Unloaded1(object sender, RoutedEventArgs e)
        {
            if (_sensor1 != null)
            {
                _sensor1.Stop();
            }
        }

        void Sensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (var frame = e.OpenColorImageFrame())
            {
                if (frame != null)
                {
                    #region Draw color frame

                    byte[] pixels = new byte[frame.PixelDataLength];
                    frame.CopyPixelDataTo(pixels);

                    WriteableBitmap bitmap = new WriteableBitmap(frame.Width, frame.Height, 96.0, 96.0, PixelFormats.Bgr32, null);
                    bitmap.Lock();
                    Marshal.Copy(pixels, 0, bitmap.BackBuffer, pixels.Length);
                    bitmap.AddDirtyRect(new Int32Rect(0, 0, frame.Width, frame.Height));
                    bitmap.Unlock();

                    //                    camera.ImageSource = bitmap;

                    #endregion
                }
            }
        }

        public MainMenu GetWindow()
        {
            return this;
            throw new NotImplementedException();
        }

        void Sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {

                    Skeleton[] bodies = new Skeleton[frame.SkeletonArrayLength];

                    frame.CopySkeletonDataTo(bodies);

                    var body = bodies.Where(b => b.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();

                    if (body != null)
                    {

                        Joint handLeft = body.Joints[JointType.HandLeft];

                        Joint handRight = body.Joints[JointType.HandRight];

                        

                        if (handLeft.TrackingState != JointTrackingState.NotTracked && handRight.TrackingState != JointTrackingState.NotTracked)
                        {
                            // Select the hand that is closer to the sensor.
                            var activeHand = handRight.Position.Z <= handLeft.Position.Z ? handRight : handLeft;
                            var position = _sensor1.CoordinateMapper.MapSkeletonPointToColorPoint(activeHand.Position, ColorImageFormat.RgbResolution640x480Fps30);

                            cursor.Flip(activeHand);
                            cursor.Update(position);

                            if (this.IsVisible)
                            {
                                if (RegraFecharTela.ExecutaRegraFecharTela(body))
                                {
                                    if (countFecharTela > 15)
                                        this.Close();
                                    else
                                        countFecharTela++;

                                }
                                else
                                    countFecharTela = 0;

                                if (position.X > 307 && position.X < 427 && position.Y > 253 && position.Y < 298)
                                {

                                    Console.WriteLine("SELECIONA");
                                    countSelect2++;
                                    Console.WriteLine(countSelect2);
                                    if (map == null && countSelect2 >= 45)
                                    {
                                        map = new MainWindow(-5.062080, -42.794552, 17);
                                        map.Show();
                                        countSelect2 = 0;
                                    }



                                    //if (IsVisible)

                                }
                                else {
                                    countSelect2 = 0;
                                    if (position.X > 70 && position.X < 200 && position.Y > 253 && position.Y < 298)
                                    {
                                        countSelect1++;
                                        Console.WriteLine("SELECIONA");
                                        if (menuLocal == null && countSelect1 >= 45)
                                        {
                                            menuLocal = new PesquisarLocalidade();
                                            menuLocal.Show();
                                            countSelect1 = 0;
                                        }//if (IsVisible)

                                    }
                                    else {
                                        countSelect1 = 0;

                                        if (position.X > 166 && position.X < 337 && position.Y > 365 && position.Y < 385)
                                        {

                                            Console.WriteLine("SELECIONAMENU");
                                            countSelect3++;
                                            Console.WriteLine(countSelect3);
                                            if (manual == null && countSelect3 >= 45)
                                            {
                                                manual = new KinectManual();
                                                manual.Show();
                                                countSelect3 = 0;
                                            }



                                            //if (IsVisible)

                                        }
                                        else
                                        {
                                            countSelect3 = 0;
                                        }
                                    }

                                   
                                }
                            }

                            if (map != null)
                            {
                                if (map.IsVisible)
                                {
                                    this.Visibility = Visibility.Collapsed;
                                    countSelect1 = 0;
                                }
                                else {
                                    this.Show();
                                    if (map != null)
                                    {

                                        map.Close();
                                        map = null;
                                    }
                                }
                            }

                            if (menuLocal != null) {
                                if (menuLocal.IsVisible)
                                {
                                    this.Visibility = Visibility.Collapsed;
                                    countSelect2 = 0;
                                }
                                else {
                                    if(!menuLocal.getMap().IsVisible)
                                        this.Show();
                                    if (menuLocal != null)
                                    {

                                        menuLocal.Close();
                                        menuLocal = null;
                                    }
                                }

                                
                            }

                            if (manual != null)
                            {
                                if (manual.IsVisible)
                                {
                                    this.Visibility = Visibility.Collapsed;
                                    countSelect3 = 0;
                                }
                                else {
                                    this.Show();
                                    if (map != null)
                                    {

                                        map.Close();
                                        map = null;
                                    }
                                }
                            }






                        }

                    }
                }
            }
        }
        
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //_sensor1.Stop();
            
            MainWindow map = new MainWindow(-5.062080, -42.794552, 17);
            map.Show();
         
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            KinectManual manual = new KinectManual();
            manual.Show();
        }
    }
}
