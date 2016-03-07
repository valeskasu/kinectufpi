using System;
using System.Collections.Generic;
using System.Linq;
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
using KinectControls.Test;
using Microsoft.Kinect;
using System.Runtime.InteropServices;
using KinectControls.Test.RegrasMovimentos;

namespace KinectControls.Test
{
    /// <summary>
    /// Interaction logic for PesquisarLocalidade.xaml
    /// </summary>
    public partial class PesquisarLocalidade : Window
    {

        MainWindow map;
        public int countSelectHU = 0;
        public int countSelectCCN = 0;
        public int countSelectSE = 0;
        public int countSelectCCHL = 0;
        public int countSelectCT = 0;
        public int countSelectDCE = 0;
        public int countFecharTela = 0;

        KinectSensor _sensor;
        public PesquisarLocalidade()
        {
            
            InitializeComponent();
            map = new MainWindow(-5.056007, -42.790406, 17);
            //map.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded2(object sender, RoutedEventArgs e)
        {
            _sensor = KinectSensor.KinectSensors.Where(s => s.Status == KinectStatus.Connected).FirstOrDefault();

            _sensor.ColorStream.Enable();
            _sensor.DepthStream.Enable();
            _sensor.SkeletonStream.Enable();

            _sensor.ColorFrameReady += Sensor_ColorFrameReady;
            _sensor.SkeletonFrameReady += Sensor_SkeletonFrameReady;

            _sensor.Start();


        }

        public MainWindow getMap()
        {
            return this.map;

        }

        private void Window_Unloaded2(object sender, RoutedEventArgs e)
        {
            if (_sensor != null)
            {
                this.Hide();

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

                        if (this.IsVisible)
                        {
                            if (RegraFecharTela.ExecutaRegraFecharTela(body))
                            {
                                if (countFecharTela > 15)
                            //        Console.Write("z");
                                this.Close();
                                else
                                    countFecharTela++;

                            }
                            else
                                countFecharTela = 0;

                            if (handLeft.TrackingState != JointTrackingState.NotTracked && handRight.TrackingState != JointTrackingState.NotTracked)
                            {
                                // Select the hand that is closer to the sensor.
                                var activeHand = handRight.Position.Z <= handLeft.Position.Z ? handRight : handLeft;
                                var position = _sensor.CoordinateMapper.MapSkeletonPointToColorPoint(activeHand.Position, ColorImageFormat.RgbResolution640x480Fps30);

                                cursor.Flip(activeHand);
                                cursor.Update(position);
                                

                                if (position.X > 79 && position.X < 195 && position.Y > 97 && position.Y < 137)
                                {

                                    Console.WriteLine("SELECIONAHUU");
                                    countSelectHU++;
                                    Console.WriteLine(countSelectHU);
                                    if(this.IsVisible)
                                        countSelectHU++;
                                    if (countSelectHU >= 45)
                                    {
                                        map.setMap(-5.056007, -42.790406, 17);
                                        map.Visibility = Visibility.Visible;
                                        countSelectHU = 0;
                                        //this.Visibility = Visibility.Collapsed;
                                    }



                                    //if (IsVisible)

                                }
                                else {
                                    countSelectHU = 0;

                                    
                                    
                                    if (position.X > 99 && position.X < 191 && position.Y > 186 && position.Y < 226)
                                    {
                                        countSelectCCN++;
                                        Console.WriteLine("SELECIONACCN");
                                        if (countSelectCCN >= 45)
                                        {
                                            map.setMap(-5.057639, -42.790237, 17);
                                            map.Visibility = Visibility.Visible;
                                            countSelectCCN = 0;
                                            //this.Visibility = Visibility.Collapsed;
                                        }//if (IsVisible)

                                    }
                                    else {
                                        countSelectCCN = 0;

                                        

                                        if (position.X > 99 && position.X < 191 && position.Y > 270 && position.Y < 310)
                                        {

                                            Console.WriteLine("SELECIONASE");
                                            countSelectSE++;
                                            if (countSelectSE >= 45)
                                            {
                                                map.setMap(-5.055871, -42.805336, 17);
                                                map.Visibility = Visibility.Visible;
                                                countSelectSE = 0;
                                                //this.Visibility = Visibility.Hidden;
                                            }



                                            //if (IsVisible)

                                        }
                                        else {
                                            countSelectSE = 0;

                                            
                                            if (position.X > 439 && position.X < 531 && position.Y > 270 && position.Y < 310)
                                            {
                                                countSelectCCHL++;
                                                Console.WriteLine("SELECIONACCHL");
                                                if (countSelectCCHL >= 45)
                                                {
                                                    map.setMap(-5.057872, -42.797144, 17);
                                                    map.Visibility = Visibility.Visible;
                                                    countSelectCCHL = 0;
                                                    //this.Visibility = Visibility.Collapsed;
                                                }//if (IsVisible)

                                            }
                                            else {
                                                countSelectCCHL = 0;

                                                
                                                if (position.X > 439 && position.X < 531 && position.Y > 127 && position.Y < 137)
                                                {

                                                    Console.WriteLine("SELECIONACT");
                                                    countSelectCT++;
                                                    if (countSelectCT >= 45)
                                                    {
                                                        map.setMap(-5.055031, -42.799357, 17);
                                                        map.Visibility = Visibility.Visible;
                                                        countSelectCT = 0;
                                                        //this.Visibility = Visibility.Collapsed;
                                                    }



                                                    //if (IsVisible)

                                                }
                                                else {
                                                    countSelectCT = 0;

                                                    countSelectDCE = 0;
                                                    if (position.X > 439   && position.X < 531 && position.Y > 186 && position.Y < 226)
                                                    {
                                                        Console.WriteLine("SELECIONADCE");
                                                        countSelectDCE++;
                                                        if (countSelectDCE >= 45)
                                                        {
                                                            map.setMap(-5.056593, -42.789486, 17);
                                                            map.Visibility = Visibility.Visible;
                                                            countSelectDCE = 0;
                                                            //this.Visibility = Visibility.Collapsed;
                                                        }//if (IsVisible)

                                                    }
                                                    else
                                                        countSelectDCE = 0;


                                                }

                                            }


                                        }


                                    }


                                }


                            }



                        }
                        else
                        {
                            if (map != null)
                            {
                                if (map.IsVisible)
                                {
                                    this.Visibility = Visibility.Hidden;
                                }
                                
                            }

                        }


                    }




                }

            }
        }

    }
}
