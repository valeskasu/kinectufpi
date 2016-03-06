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

        KinectSensor _sensor;
        public PesquisarLocalidade()
        {
            InitializeComponent();
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

                        if (handLeft.TrackingState != JointTrackingState.NotTracked && handRight.TrackingState != JointTrackingState.NotTracked)
                        {
                            // Select the hand that is closer to the sensor.
                            var activeHand = handRight.Position.Z <= handLeft.Position.Z ? handRight : handLeft;
                            var position = _sensor.CoordinateMapper.MapSkeletonPointToColorPoint(activeHand.Position, ColorImageFormat.RgbResolution640x480Fps30);

                            cursor.Flip(activeHand);
                            cursor.Update(position);


                            if (RegraSelecionar.ExecutaRegraSelecionar(body) && position.X > 307 && position.X < 427 && position.Y > 253 && position.Y < 298)
                            {

                            }
                            else {
                                if (RegraSelecionar.ExecutaRegraSelecionar(body) && position.X > 70 && position.X < 200 && position.Y > 253 && position.Y < 298)
                                {


                                }


                            }


                        }
                    }




                }

            }
        }

    }
}
