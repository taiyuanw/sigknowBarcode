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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SigknowBarcode
{
    /// <summary>
    /// Interaction logic for CommandListWindow.xaml
    /// Barcodes for commands to be used in Association.
    /// </summary>
    public partial class CommandListWindow : Window

    {
        DockPanel myDockPanel = new DockPanel();
        Border BorderMenu = new Border();
        Border BorderMainBody = new Border();
        Canvas canvasCommands = new Canvas();

        public CommandListWindow()
        {
            InitializeComponent();
            // Initialize DockPanel
            myDockPanel.LastChildFill = true;

            // Define the child content myTextBlock1
            BorderMenu.Height = 25;
            BorderMenu.Background = Brushes.SkyBlue;
            BorderMenu.BorderThickness = new Thickness(1);
            DockPanel.SetDock(BorderMenu, Dock.Top);

            // Add barcode tray menu items 
            ToolBarTray tbtToolBarTray = new ToolBarTray();
            ToolBar tbToolBar = new ToolBar();

            // Add task buttons
            Button btnPrintCommandList = new Button() { Content = "列印", Background = Brushes.White, Name = "btnPrint" };
            btnPrintCommandList.Click += btnPrintCommandList_Click;
            tbToolBar.Items.Add(btnPrintCommandList);

            DockPanel.SetDock(tbtToolBarTray, Dock.Left);
            tbToolBar.Background = Brushes.Orange;
            tbtToolBarTray.ToolBars.Add(tbToolBar);
            BorderMenu.Child = tbtToolBarTray;


            // Loading Barcode Image (image/textblock --> stackpanel --> border ===> DockPanel ===> Window)
            
            // create image element
            ////////////////////////////////////////////
            Image iPrint1 = new Image();
            iPrint1.Width = 180;
            BitmapImage biPrint1 = new BitmapImage();
            biPrint1.BeginInit();
            biPrint1.UriSource = new Uri(@"c:\888.gif");
            biPrint1.DecodePixelWidth = 400;
            biPrint1.EndInit();
            iPrint1.Source = biPrint1;
            TextBlock tb1 = new TextBlock();
            tb1.Text = "查尋";
            tb1.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            StackPanel sp1 = new StackPanel();
            sp1.Children.Add(iPrint1);
            sp1.Children.Add(tb1);
            sp1.Orientation = System.Windows.Controls.Orientation.Vertical;
            Border bContent1 = new Border();
            bContent1.Height = 200;
            bContent1.Background = Brushes.White;
            bContent1.BorderThickness = new Thickness(1);
            DockPanel.SetDock(bContent1, Dock.Top);
            bContent1.Child = sp1;

            ////////////////////////////////////////////
            Image iPrint2 = new Image();
            iPrint2.Width = 180;
            BitmapImage biPrint2 = new BitmapImage();
            biPrint2.BeginInit();
            biPrint2.UriSource = new Uri(@"c:\888.gif");
            biPrint2.DecodePixelWidth = 400;
            biPrint2.EndInit();
            iPrint2.Source = biPrint2;
            TextBlock tb2 = new TextBlock();
            tb2.Text = "確認";
            tb2.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            StackPanel sp2 = new StackPanel();
            sp2.Children.Add(iPrint2);
            sp2.Children.Add(tb2);
            sp2.Orientation = System.Windows.Controls.Orientation.Vertical;
            Border bContent2 = new Border();
            bContent2.Height = 200;
            bContent2.Background = Brushes.White;
            bContent2.BorderThickness = new Thickness(1);
            DockPanel.SetDock(bContent2, Dock.Top);
            bContent2.Child = sp2;



            ////////////////////////////////////////////
            Image iPrint3 = new Image();
            iPrint3.Width = 180;
            BitmapImage biPrint3 = new BitmapImage();
            biPrint3.BeginInit();
            biPrint3.UriSource = new Uri(@"c:\888.gif");
            biPrint3.DecodePixelWidth = 400;
            biPrint3.EndInit();
            iPrint3.Source = biPrint3;
            TextBlock tb3 = new TextBlock();
            tb3.Text = "重置";
            tb3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            StackPanel sp3 = new StackPanel();
            sp3.Children.Add(iPrint3);
            sp3.Children.Add(tb3);
            sp3.Orientation = System.Windows.Controls.Orientation.Vertical;
            Border bContent3 = new Border();
            bContent3.Height = 200;
            bContent3.Background = Brushes.White;
            bContent3.BorderThickness = new Thickness(1);
            DockPanel.SetDock(bContent3, Dock.Top);
            bContent3.Child = sp3;

            ////////////////////////////////////////////
            Image iPrint4 = new Image();
            iPrint4.Width = 180;
            BitmapImage biPrint4 = new BitmapImage();
            biPrint4.BeginInit();
            biPrint4.UriSource = new Uri(@"c:\888.gif");
            biPrint4.DecodePixelWidth = 400;
            biPrint4.EndInit();
            iPrint4.Source = biPrint4;
            TextBlock tb4 = new TextBlock();
            tb4.Text = "解除關聯";
            tb4.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            StackPanel sp4 = new StackPanel();
            sp4.Children.Add(iPrint4);
            sp4.Children.Add(tb4);
            sp4.Orientation = System.Windows.Controls.Orientation.Vertical;
            Border bContent4 = new Border();
            bContent4.Height = 200;
            bContent4.Background = Brushes.White;
            bContent4.BorderThickness = new Thickness(1);
            DockPanel.SetDock(bContent4, Dock.Top);
            bContent4.Child = sp4;
            
            // put all barcode images into a StackPanel
            StackPanel spCommands = new StackPanel();

            /*
            spCommands.Children.Add(bContent1);
            spCommands.Children.Add(bContent2);
            spCommands.Children.Add(bContent3);
            spCommands.Children.Add(bContent4);*/

            BorderMainBody.Background = Brushes.White;
            BorderMainBody.BorderBrush = Brushes.Black;
            BorderMainBody.BorderThickness = new Thickness(2);
            //BorderMainBody.CornerRadius = new CornerRadius(45);
            BorderMainBody.Padding = new Thickness(1);
            //BorderMainBody.Child = spCommands;

            // load all barcode images to a canvas
            double maxX = bContent1.Width;
            double maxY = bContent1.Height;
            Canvas.SetTop(bContent1, maxY * 0 + 15);
            Canvas.SetTop(bContent2, maxY * 1 + 15);
            Canvas.SetTop(bContent3, maxY * 2 + 15);
            Canvas.SetTop(bContent4, maxY * 3 + 15);

            canvasCommands.Children.Add(bContent1);
            canvasCommands.Children.Add(bContent2);
            canvasCommands.Children.Add(bContent3);
            canvasCommands.Children.Add(bContent4);
            BorderMainBody.Child = canvasCommands;

            // Add child elements to the DockPanel Children collection
            myDockPanel.Children.Add(BorderMenu);
            myDockPanel.Children.Add(BorderMainBody);

            // Add the parent Canvas as the Content of the Window Object
            //this.Content = myDockPanel;
            this.Content = myDockPanel;
            this.Width = 400 ;
            this.Height = 820;
            this.Background = Brushes.White;
        }

        private void CommandListWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void btnPrintCommandList_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dlg = new PrintDialog();

            if (dlg.ShowDialog() == true)
            {
                dlg.PrintVisual(this.canvasCommands as Visual, "Command Lists");
            }
        }
        
        //[DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        public static BitmapSource loadBitmap(System.Drawing.Bitmap source)
        {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
        } 
        
    }
    /*
    public partial class MyCanvas : Canvas
    {
        protected override void OnRender(DrawingContext dc, string filepath)
        {
            base.OnRender(dc);
            BitmapImage img = new BitmapImage(new Uri(filepath));
            dc.DrawImage(img, new Rect(0, 0, img.PixelWidth, img.PixelHeight));
        }

    }*/
}
