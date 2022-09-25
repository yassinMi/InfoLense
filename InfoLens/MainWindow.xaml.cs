using Mi.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InfoLens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Config.Instance;
            CommandManager.RegisterClassCommandBinding(typeof(MainWindow),
                new CommandBinding(SystemCommands.CloseWindowCommand, ExecutedCloseWindowCommand));
            SystemCommands.CloseWindowCommand.InputGestures.Add(new KeyGesture(Key.Escape));
            gapThumb.DragDelta += HndlMovingThumbDelta;
            this.LostFocus += (s, e) =>
            {
                this.IsTabStop = false;
                this.IsTabStop = true;
            };
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            Topmost = true;
            this.Left = 0;
            this.Top = 0;
            
            
            resizeGrip.DragDelta += HndlResizeGripDragDelta;
            topSplitter.DragStarted += HndleTopSplitterDragStarted;
            topSplitter.DragDelta += HndlTopSplitterDragDelta;
            LeftSplitter.DragDelta+= HndlLeftSplitterDragDelta;
            RightSplitter.DragDelta += HndlRightSplitterDragDelta;
            bottomSplitter.DragDelta += HndlbottomSplitterDragDelta;

            Activated += (s, e) =>
            {
                //this.Topmost = true;
            };
            Deactivated += (s, e) =>
            {
                Topmost = true;
                //Activate();
            };

            CoverBrush = new SolidColorBrush(new Color
            {
                A = (byte)(Config.Instance.Opacity * 255 / 100),
                R = Config.Instance.CoverColor.R,
                G = Config.Instance.CoverColor.G,
                B = Config.Instance.CoverColor.B
            }
                );
            Debug.WriteLine($"opc:{Config.Instance.Opacity}");
            c0.Width = new GridLength(Config.Instance.c0Width);
            c1.Width = new GridLength(Config.Instance.c1Width);
            r0.Height = new GridLength(Config.Instance.r0Height);
            r1.Height = new GridLength(Config.Instance.r1Height);
            var c2_new = this.Width - Config.Instance.c1Width - Config.Instance.c0Width;
            var r2_new = this.Height - Config.Instance.r1Height - Config.Instance.r0Height;
            c2.Width = new GridLength(Math.Max(0, c2_new));
            r2.Height = new GridLength(Math.Max (0,r2_new));
            // this.Effect  = new System.Windows.Media.Effects.BlurEffect() { Radius = 3 };

        }

        private void HndlbottomSplitterDragDelta(object sender, DragDeltaEventArgs e)
        {
            var r1_new = r1.ActualHeight + e.VerticalChange;
            var r2_new = host.ActualHeight - r1_new - r0.ActualHeight;
            if ((r1_new >= 0) && (r2_new >= 0))
            {
                r1.Height = new GridLength(r1_new);
                r2.Height = new GridLength(r2_new);
            }
        }

        private void HndlRightSplitterDragDelta(object sender, DragDeltaEventArgs e)
        {
            var c1_new = c1.ActualWidth + e.HorizontalChange;
            var c2_new = host.ActualWidth - c1_new - c0.ActualWidth;
            if ((c1_new >= 0) && (c2_new >= 0))
            {
                c1.Width = new GridLength(c1_new);
                c2.Width = new GridLength(c2_new);
            }
        }

        private void HndlLeftSplitterDragDelta(object sender, DragDeltaEventArgs e)
        {
            var c1_new = c1.ActualWidth - e.HorizontalChange;
            var c0_new = host.ActualWidth - c1_new - c2.ActualWidth;
            if ((c1_new >= 0) && (c0_new >= 0))
            {
                c1.Width = new GridLength(c1_new);
                c0.Width = new GridLength(c0_new);
            }
        }

        private void HndlTopSplitterDragDelta(object sender, DragDeltaEventArgs e)
        {
            var r1_new = r1.ActualHeight - e.VerticalChange;
            var r0_new = host.ActualHeight - r1_new-r2.ActualHeight;
            if ((r1_new >= 0) && (r0_new >= 0))
            {
                r1.Height = new GridLength(r1_new);
                r0.Height = new GridLength(r0_new);
            }
        }

        private void HndleTopSplitterDragStarted(object sender, DragStartedEventArgs e)
        {
            
        }

        private void HndlResizeGripDragDelta(object sender, DragDeltaEventArgs e)
        {
            
            var c1_new = c1.ActualWidth + e.HorizontalChange;
            var c2_new = host.ActualWidth - c1_new-c0.ActualWidth;
            if ((c1_new >= 0) && (c2_new >= 0))
            {
                c1.Width = new GridLength(c1_new);
                c2.Width = new GridLength(c2_new);
            }
            var r1_new = r1.ActualHeight + e.VerticalChange;
            var r2_new = host.ActualHeight - r1_new-r0.ActualHeight;
            if ((r1_new >= 0) && (r2_new >= 0))
            {
                r1.Height = new GridLength(r1_new);
                r2.Height = new GridLength(r2_new);
            }

        }

        private void HndlMovingThumbDelta(object sender, DragDeltaEventArgs e)
        {
            
        }

        private void ExecutedCloseWindowCommand(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // DragMove();
        }





        public Brush CoverBrush
        {
            get { return (Brush)GetValue(CoverBrushProperty); }
            set { SetValue(CoverBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CoverBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CoverBrushProperty =
            DependencyProperty.Register("CoverBrush", typeof(Brush), typeof(MainWindow), new PropertyMetadata(Brushes.Purple));

        private void handle_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var c0_new = c0.ActualWidth + e.HorizontalChange;
            var c2_new = host.ActualWidth- c0_new-c1.ActualWidth;
            if((c0_new>=0 )&& (c2_new >= 0))
            {
                c0.Width = new GridLength(c0_new);
                c2.Width = new GridLength(c2_new);
            }
            var r0_new = r0.ActualHeight + e.VerticalChange;
            var r2_new = host.ActualHeight - r0_new-r1.ActualHeight;
            if ((r0_new >= 0) && (r2_new >= 0))
            {
                r0.Height = new GridLength(r0_new);
                r2.Height = new GridLength(r2_new);
            }

        }

        private void handle_DragStarted(object sender, DragStartedEventArgs e)
        {

        }

        private void PopupBox_Closed(object sender, RoutedEventArgs e)
        {
            Config.Instance.Save();
        }
    }
}
