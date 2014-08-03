using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// jasons
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NumberKill
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // good tutorial - http://tech.pro/tutorial/799/wpf-tutorial-using-wpf-in-winforms

            WPF_Test_1 wpf_element = new WPF_Test_1();
            elementHost1.Child = wpf_element;


            //create shape
            System.Diagnostics.Debug.WriteLine("Creating shape");

            
            Point p = new Point(10, 10);

            drawPoint(p, wpf_element.rootCanvas  );
            
          
        }

        private void drawPoint(Point p, Canvas testCanvas)
        {
            //create shape
            System.Diagnostics.Debug.WriteLine("Creating shape");
            Shape userShape;

            Shape shape = new Ellipse();
            shape.SetValue(Canvas.LeftProperty, p.X);
            shape.SetValue(Canvas.TopProperty, p.Y);
            //shape.HorizontalAlignment = HorizontalAlignment.Left;
            //shape.VerticalAlignment = VerticalAlignment.Center;
            shape.Width = 4;
            shape.Height = 4;
            shape.Stroke = new SolidColorBrush(Colors.Black);
            shape.StrokeThickness = 3.0;

            GradientBrush gb = new LinearGradientBrush();
            gb.GradientStops = new GradientStopCollection();
            GradientStop g1 = new GradientStop();
            g1.Color = Colors.Red;
            gb.GradientStops.Add(g1);
            g1 = new GradientStop();
            g1.Color = Colors.Blue;
            g1.Offset = 2;
            gb.GradientStops.Add(g1);

            shape.Fill = gb;

            shape.Visibility = System.Windows.Visibility.Visible;
            shape.Opacity = 0.5;

            testCanvas.Children.Add(shape);

        }
    }
}
