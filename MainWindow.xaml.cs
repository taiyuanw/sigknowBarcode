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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Initialize DockPanel
            DockPanel myDockPanel = new DockPanel();
            myDockPanel.LastChildFill = true;
            // Define the child content myTextBlock1
            Border BorderMenu = new Border();
            BorderMenu.Height = 25;
            BorderMenu.Background = Brushes.SkyBlue;
            BorderMenu.BorderThickness = new Thickness(1);
            DockPanel.SetDock(BorderMenu, Dock.Top);

            // Add barcode tray menu items 
            ToolBarTray tbtToolBarTray = new ToolBarTray();
            ToolBar tbToolBar = new ToolBar();

            // Add task buttons
            Button btnPrinterSetup = new Button() { Content = "列表機設定", Background = Brushes.White, Name = "btnPrinterSetting" };
            btnPrinterSetup.Click += btnPrinterSetup_click;
            tbToolBar.Items.Add(btnPrinterSetup);

            Button btnPrintCommand = new Button() { Content = "列印指令", Background = Brushes.White, Name = "btnPrintCommand" };
            btnPrintCommand.Click += btnPrintCommand_click;
            tbToolBar.Items.Add(btnPrintCommand);

            Button btnPreOperation = new Button() { Content = "前置作業", Background = Brushes.White, Name = "btnPreOperation" };
            btnPreOperation.Click += btnPreOperation_click;
            tbToolBar.Items.Add(btnPreOperation);

            Button btnPcbaCopy = new Button() { Content = "列印PCB條碼", Background = Brushes.White, Name = "btnPcbaCopy" };
            btnPcbaCopy.Click += btnPcbaCopy_click;
            tbToolBar.Items.Add(btnPcbaCopy);
            
            //Button btnAssociation = new Button() { Content = "序號關聯", Background = Brushes.White, Name = "btnAssociation" };
            //btnAssociation.Click += btnAssociation_click;
            //tbToolBar.Items.Add(btnAssociation);

            Button btnPackaging = new Button() { Content = "列印包裝條碼", Background = Brushes.White, Name = "btnPackaging" };
            btnPackaging.Click += btnPackaging_click;
            tbToolBar.Items.Add(btnPackaging);
            
            DockPanel.SetDock(tbtToolBarTray, Dock.Left);
            tbToolBar.Background = Brushes.Orange;
            tbtToolBarTray.ToolBars.Add(tbToolBar);
            BorderMenu.Child = tbtToolBarTray;

            Border BorderMainBody = new Border();
            BorderMainBody.Background = Brushes.White;
            BorderMainBody.BorderBrush = Brushes.Black;
            BorderMainBody.BorderThickness = new Thickness(2);
            //BorderMainBody.CornerRadius = new CornerRadius(45);
            BorderMainBody.Padding = new Thickness(1);
            // Add child elements to the DockPanel Children collection
            myDockPanel.Children.Add(BorderMenu);
            // myDockPanel.Children.Add(myBorder2);
            myDockPanel.Children.Add(BorderMainBody);

            // Add the parent Canvas as the Content of the Window Object
            this.Content = myDockPanel;
            //Utils.OKBeep();
            Utils.ErrorBeep();
            //Utils.ErrorBeep();

        }

        private void btnPrintCommand_click(object sender, RoutedEventArgs e)
        {
            PrintCommandWindow printcommand_window = new PrintCommandWindow();
            printcommand_window.ShowDialog();
        }

        private void btnPreOperation_click(object sender, RoutedEventArgs e)
        {
            PreOperationWindow preoperation_window = new PreOperationWindow();
            preoperation_window.ShowDialog();
        }

        private void btnAssociation_click(object sender, RoutedEventArgs e)
        {
            AssociationWindow association_window = new AssociationWindow();
            association_window.ShowDialog();
        }

        private void btnPackaging_click(object sender, RoutedEventArgs e)
        {
            PackagingWindow packaging_window = new PackagingWindow();
            packaging_window.ShowDialog();
        }
        private void btnPcbaCopy_click(object sender, RoutedEventArgs e)
        {
            PcbaCopyWindow pcbacopy_window = new PcbaCopyWindow();
            pcbacopy_window.ShowDialog();
        }
        private void btnPrinterSetup_click(object sender, RoutedEventArgs e)
        {
            PrinterSetup printersetup_window = new PrinterSetup();
            printersetup_window.ShowDialog();
        }  

    }
    
}
