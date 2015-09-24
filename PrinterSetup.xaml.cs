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
using System.Printing;

namespace SigknowBarcode
{
    /// <summary>
    /// Interaction logic for PrinterSetup.xaml
    /// </summary>
    public partial class PrinterSetup : Window
    {
        System.Windows.Controls.ComboBox cbPrinterList = new System.Windows.Controls.ComboBox();

        public PrinterSetup()
        {
            InitializeComponent();

            Label lbPrinterSetup = new Label();
            Label lbFactory = new Label();
            lbPrinterSetup.Content = "列表機設定";
            Border bLabel = new Border();
            bLabel.Background = System.Windows.Media.Brushes.Gray;
            bLabel.BorderBrush = System.Windows.Media.Brushes.White;
            bLabel.BorderThickness = new Thickness(1);
            bLabel.Padding = new Thickness(1);
            bLabel.Child = lbPrinterSetup;

            cbPrinterList.Width = 200;
            cbPrinterList.TabIndex = 2;
            List<string> printlist = listPrinter();
            foreach (string printer in printlist)
            {
                //MessageBox.Show("print found:" + t);
                cbPrinterList.Items.Add(printer);
            }
            Border bPrinters = new Border();
            bPrinters.Background = System.Windows.Media.Brushes.White;
            bPrinters.BorderBrush = System.Windows.Media.Brushes.White;
            bPrinters.BorderThickness = new Thickness(1);
            bPrinters.Padding = new Thickness(1);
            bPrinters.Child = cbPrinterList;

            Button btnSelectPrinter = new Button();
            btnSelectPrinter.Width = 100;
            btnSelectPrinter.Height = 40;
            btnSelectPrinter.Content = "選擇列表機";
            if (Utils.Printer != null)
            {
                cbPrinterList.SelectedItem = Utils.Printer;
            }
            btnSelectPrinter.Click += btnSelectPrinter_Click;

            Border bSelectPrinter = new Border();
            bSelectPrinter.Background = System.Windows.Media.Brushes.White;
            bSelectPrinter.BorderBrush = System.Windows.Media.Brushes.White;
            bSelectPrinter.BorderThickness = new Thickness(1);
            bSelectPrinter.Padding = new Thickness(1);
            bSelectPrinter.Child = btnSelectPrinter;

            // Add child elements to the DockPanel Children collection
            StackPanel spBarcodeBody = new StackPanel();
            spBarcodeBody.Orientation = System.Windows.Controls.Orientation.Horizontal;
            spBarcodeBody.Margin = new Thickness(10);
            spBarcodeBody.Children.Add(bPrinters);
            spBarcodeBody.Children.Add(bSelectPrinter);


            StackPanel spMainBody = new StackPanel();
            spMainBody.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            spMainBody.Children.Add(bLabel);
            spMainBody.Children.Add(spBarcodeBody);

            // Add the parent Canvas as the Content of the Window Object
            this.Content = spMainBody;
            this.Width = 650;
            this.Height = 200;
        }

        /* return a string list of printer names */
        private List<string> listPrinter()
        {
            LocalPrintServer printServer = new LocalPrintServer();
            List<string> printerList = new List<string>();

            PrintQueueCollection printQueuesOnLocalServer = printServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });

            foreach (PrintQueue printer in printQueuesOnLocalServer)
            {

                //MessageBox.Show("\tThe shared printer : " + printer.Name);
                printerList.Add(printer.Name);
            }
            return printerList;
        }
        private void btnSelectPrinter_Click(object sender, RoutedEventArgs e)
        {
            Utils.Printer = (string) cbPrinterList.SelectedItem;
            MessageBox.Show(Utils.Printer + " 已設定.");
            this.Close();
        }
    }
}
