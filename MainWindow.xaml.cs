using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WindowsEyes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dispatcher uiDispatcher = Dispatcher.CurrentDispatcher;
        Boolean mouceScan = false;
        int x = 1;
        //Rect ect = Rect(1,1,30+x,30+x)
        public MainWindow()
        {
            InitializeComponent();
            MoucePosition();

            //Grid1.Children.Remove(RightEye);


            new Thread(() =>
            {
                while (true)
                {
                    //Logic
                    Point point2 = new Point();
                    point2 = GetMousePosition();
                    //GetCursorPos( ref point2);

                    //Update UI
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Position.Content = point2.X + ", " + point2.Y;
                        Canvas.SetLeft(Oczko, point2.X / 10);
                        Canvas.SetTop(Oczko, point2 .Y / 10);
                        Canvas.SetLeft(OczkoLeft, point2.X / 10);
                        Canvas.SetTop(OczkoLeft, point2.Y / 10);

                    }));
                   

                    Thread.Sleep(25);
                }
            }).Start();

            //new Thread(() =>
            //{
            //    while (true)
            //    {
            //        Move(x);
            //        if (x == 29)
            //        {
            //            x--;
            //        }
            //        if (x == 0)
            //        {
            //            x++;
            //        }
            //        Thread.Sleep(100);
            //        int ii = 77 + x;
                    
            //        //.Margin =new Thickness(ii, 83, 0, 251);

            //        ////uiDispatcher.Invoke(() =>
            //        ////{
            //        ////    Canvas myCanvas = CanvasImage;
            //        ////    //myCanvas.Effect.BeginAnimation()
            //        ////    Ellipse ellipse = RightEye;
                        

            //        ////    ///myCanvas.Children.Add(ellipse);
                        
            //        ////    //myCanvas.
            //        ////});
            //    }
            //}).Start();

        }

        private void Move(int XLeft)
        {
            //x = XLeft;
            //Canvas canas = new Canvas();
            //canas.Height = 100;
            //canas.Width = 100;
            


            //canas.Children.Clear();

            //Grid1.Children.Remove(RightEye);
            //canas.Children.Add(RightEye);

            //canas.Children[0].RenderTransform.TransformBounds(new Rect(1+ XLeft, 1, 30+x, 30+x));
        }

        private void MoucePosition()
        {
            while (mouceScan)
            {
                if (mouceScan)
                {
                    Point point = GetMousePosition();
                    mouceScan = false;
                }
            }
        }
        private void WindowsEyesForm_MouseMove(object sender, MouseEventArgs e)
        {

            var position = Mouse.GetPosition(null);
            // GetMousePos() => WindowsEyesForm.PointToScreen(Mouse.GetPosition(WindowsEyesForm));
            //e.Device.
        }
        public void KeepReportMousePos()
        {
            //Endless Report Mouse position
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    this.Dispatcher.Invoke(
                        DispatcherPriority.SystemIdle,
                        new Action(() =>
                        {
                            GetCursorPos();
                        }));
                }
            });
        }
        public void GetCursorPos()
        {
            //get the mouse position and show on the TextBlock
            Point pointToWindow = Mouse.GetPosition(this);
            Point pointToScreen = PointToScreen(pointToWindow);
            //System.Drawing.Point p = Cursor.;
            //.Text = p.X + " " + p.Y;
        }

        private void MainWindow_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //invoke mouse position detect when wheel the mouse
            KeepReportMousePos();


        }

        private void RightEye_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            KeepReportMousePos();
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        //private void RightEye_KeyDown(object sender, KeyEventArgs e)
        //{
        //    mouceScan = mouceScan ? false :  true;
        //    MoucePosition();
        //}

        private void WindowsEyesForm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouceScan = mouceScan ? false : true;
            MoucePosition();
        }
    }
}