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
using System.Drawing;
using BarcodeLib;
using System.Drawing.Printing;

namespace SigknowBarcode
{
    /// <summary>
    /// Interaction logic for PreOperationWindow.xaml
    /// </summary>
    public partial class PreOperationWindow : Window
    {
        System.Windows.Controls.ComboBox cbFactory = new System.Windows.Controls.ComboBox();
        System.Windows.Controls.ComboBox cbYear = new System.Windows.Controls.ComboBox();
        System.Windows.Controls.ComboBox cbMonth = new System.Windows.Controls.ComboBox();
        System.Windows.Controls.ComboBox cbDay = new System.Windows.Controls.ComboBox();
        System.Windows.Controls.TextBox textSN = new System.Windows.Controls.TextBox();
        System.Windows.Controls.TextBox textEN = new System.Windows.Controls.TextBox();
        System.Windows.Controls.Image imgBarcode = new System.Windows.Controls.Image();
        //string imagefilename;

        public PreOperationWindow()
        {
            InitializeComponent();

            //////////////////////////////////////////////////////
            Label lbGenerateBarcode = new Label();
            Label lbFactory = new Label();
            lbGenerateBarcode.Content = "批次列印EzyPro Patch 序號";
            Border bLabel = new Border();
            bLabel.Background = System.Windows.Media.Brushes.Gray;
            bLabel.BorderBrush = System.Windows.Media.Brushes.White;
            bLabel.BorderThickness = new Thickness(1);
            bLabel.Padding = new Thickness(1);
            bLabel.Child = lbGenerateBarcode;
            System.DateTime tmCurrent = DateTime.Now;

            //////////////////////////////////////////////////////
            //ComboBox - cbFactory - 出廠地點 : T - taiwan , C - china
            cbFactory.Width = 40;
            cbFactory.Items.Add("C");
            cbFactory.Items.Add("T");
            cbFactory.TabIndex = 1;
            cbFactory.SelectedItem = "T";
            Border bFactory = new Border();
            bFactory.Background = System.Windows.Media.Brushes.White;
            bFactory.BorderBrush = System.Windows.Media.Brushes.White;
            bFactory.BorderThickness = new Thickness(1);
            bFactory.Child = cbFactory;

            int snum = 1;
            //ComboBox - textYear - 出廠年份 : yyyy
            cbYear.Width = 120;
            for (snum = 2015; snum <= 2015+10; snum++)
            {
                cbYear.Items.Add(snum.ToString().PadLeft(4, '0'));
            }
            cbYear.TabIndex = 2;
            cbYear.SelectedItem = tmCurrent.Year.ToString();
            //MessageBox.Show(tmCurrent.Year.ToString().PadLeft(4, '0'));
            cbYear.SelectedItem = tmCurrent.Year.ToString().PadLeft(4, '0');
            Border bYear = new Border();
            bYear.Background = System.Windows.Media.Brushes.White;
            bYear.BorderBrush = System.Windows.Media.Brushes.White;
            bYear.BorderThickness = new Thickness(1);
            bYear.Padding = new Thickness(1);
            bYear.Child = cbYear;

            //ComboBox - textYear - 出廠年份 
            cbMonth.Width = 100;
            for (snum = 1; snum <= 12; snum++)
            {
                cbMonth.Items.Add(snum.ToString().PadLeft(2, '0'));
            }
            //cbMonth.SelectedItem = "01";
            cbMonth.SelectedItem = tmCurrent.Month.ToString().PadLeft(2, '0');
            cbMonth.TabIndex = 3;
            Border bMonth = new Border();
            bMonth.Background = System.Windows.Media.Brushes.White;
            bMonth.BorderBrush = System.Windows.Media.Brushes.White;
            bMonth.BorderThickness = new Thickness(1);
            bMonth.Padding = new Thickness(1);
            bMonth.Child = cbMonth;

            //ComboBox - textYear - 出廠日 
            cbDay.Width = 100;
            for (snum = 1; snum <= 31; snum++)
            {

                cbDay.Items.Add(snum.ToString().PadLeft(2, '0'));
            }
            cbDay.SelectedItem = "01";
            cbDay.SelectedItem = tmCurrent.Day.ToString().PadLeft(2, '0');
            cbDay.TabIndex = 4;
            Border bDay = new Border();
            bDay.Background = System.Windows.Media.Brushes.White;
            bDay.BorderBrush = System.Windows.Media.Brushes.White;
            bDay.BorderThickness = new Thickness(1);
            bDay.Padding = new Thickness(1);
            bDay.Child = cbDay;
            
            //TextBox - textSN - 起始序號
            textSN.Width = 100;
            textSN.Text = "0001";
            Border bSN = new Border();
            bSN.Background = System.Windows.Media.Brushes.White;
            bSN.BorderBrush = System.Windows.Media.Brushes.White;
            bSN.BorderThickness = new Thickness(1);
            bSN.Padding = new Thickness(1);
            bSN.Child = textSN;

            //TextBox - textEN - 結束號碼
            textEN.Width = 100;
            textEN.Text = "0001";
            Border bEN = new Border();
            bEN.Background = System.Windows.Media.Brushes.White;
            bEN.BorderBrush = System.Windows.Media.Brushes.White;
            bEN.BorderThickness = new Thickness(1);
            bEN.Padding = new Thickness(1);
            bEN.Child = textEN;

            // Add child elements to the DockPanel Children collection
            StackPanel spBarcodeBody = new StackPanel();
            spBarcodeBody.Orientation = System.Windows.Controls.Orientation.Horizontal;
            spBarcodeBody.Margin = new Thickness(10);
            spBarcodeBody.Children.Add(bFactory);
            spBarcodeBody.Children.Add(bYear);
            spBarcodeBody.Children.Add(bMonth);
            spBarcodeBody.Children.Add(bDay);
            spBarcodeBody.Children.Add(bSN);
            spBarcodeBody.Children.Add(bEN);

            Button btnGenBarcode = new Button();
            btnGenBarcode.Width = 60;
            btnGenBarcode.Height = 40;
            btnGenBarcode.Content = "產生條碼";
            btnGenBarcode.Click += btnGenBarcode_Click;

            Border bGenBarcode = new Border();
            bGenBarcode.Background = System.Windows.Media.Brushes.White;
            bGenBarcode.BorderBrush = System.Windows.Media.Brushes.White;
            bGenBarcode.BorderThickness = new Thickness(1);
            bGenBarcode.Padding = new Thickness(1);
            bGenBarcode.Child = btnGenBarcode;

            StackPanel spButtons = new StackPanel();
            spButtons.Children.Add(bGenBarcode);

            StackPanel spMainBody = new StackPanel();
            spMainBody.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            spMainBody.Children.Add(bLabel);
            spMainBody.Children.Add(spBarcodeBody);
            spMainBody.Children.Add(spButtons);

            // Add the parent Canvas as the Content of the Window Object
            this.Content = spMainBody;
            this.Width = 650;
            this.Height = 200;
        }
        private void btnGenBarcode_Click(object sender, RoutedEventArgs e)
        {

            // TODO: generate barcodes
            //MessageBox.Show("Generating Barcode ...");
            Barcode bc = new Barcode();
            int _start = int.Parse(textSN.Text); // starting serial #
            int _end = int.Parse(textEN.Text); // ending serial #
            int serial;
            string textSerialNo = "0000";
            //System.Drawing.Image draw;
            //BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            //BitmapImage bitmapBarcode = new BitmapImage();
            //bitmapBarcode.BeginInit();  // WTY : need to be placed outside the for loop
            for (serial = _start; serial <= _end; serial++)
            {
                textSerialNo = cbFactory.Text + cbYear.Text + cbMonth.Text + cbDay.Text + serial.ToString().PadLeft(4, '0');
                //System.Windows.Forms.PictureBox wtybarcode = new System.Windows.Forms.PictureBox();

                //wtybarcode.Image = b.Encode(BarcodeLib.TYPE.CODE128, textSerialNo);
                //wtybarcode.Image = b.Encode(BarcodeLib.TYPE.CODE128, "01234567");
                //imagefilename = "c:\\Users\\TaiYuan\\Google 雲端硬碟\\" + textSerialNo + ".bmp";
                //wtybarcode.Image.Save(@imagefilename, System.Drawing.Imaging.ImageFormat.Bmp);

                //bitmapBarcode.UriSource = new Uri(@imagefilename, UriKind.RelativeOrAbsolute);
                //bitmapBarcode.DecodePixelWidth = 40;
                //bitmapBarcode.DecodePixelHeight = 10;
                
                //imgBarcode.Source = bitmapBarcode;

                // print
                //PrintDocument printDoc = new PrintDocument();
                //printDoc.PrintPage += new PrintPageEventHandler(printDoc_Print);
                // printDoc.Print();  // WTY: TODO : dont' waste paper yet ~
                //MessageBox.Show(textSerialNo);
                
                //// ---> 4cm x 2cm : TSCLIB_DLL.PrintBarcodeWithText(textSerialNo);
                TSCLIB_DLL.PrintBarcodeWithTextForPreOperation(textSerialNo);
            }
            //bitmapBarcode.EndInit();  // WTY : need to be placed outside the for loop;

            // TODO: print barcodes
            //MessageBox.Show("TODO :Printing Barcode here");
        }
        /*
        private void printDoc_Print(object sender, PrintPageEventArgs ev)
        {
            string _fn;
            try
            {
                _fn = String.Copy(imagefilename);
                System.Drawing.Image img = System.Drawing.Image.FromFile(@_fn);
                PointF pf = new PointF(10, 10);
                //ev.Graphics.DrawImage(img, pf);
                //ev.Graphics.DrawImage(img, 20, 10, 140, 60);

                ///////////////////////////////////////////
                System.Drawing.Point p1 = new System.Drawing.Point(10, 10); // x, y
                System.Drawing.Point p2 = new System.Drawing.Point(50, 10);
                System.Drawing.Point p3 = new System.Drawing.Point(10, 30);
                System.Drawing.Point[] points = new System.Drawing.Point[] {p1, p2, p3};
                System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(10, 10, 140, 60);
                ev.Graphics.DrawImage(img, points, srcRect, System.Drawing.GraphicsUnit.Pixel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        */
    }

}
