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

namespace SigknowBarcode
{
    /// <summary>
    /// Interaction logic for PcbaCopyWindow.xaml
    /// </summary>
    public partial class PcbaCopyWindow : Window
    {
        System.Windows.Controls.TextBox txtPCBASN = new System.Windows.Controls.TextBox();
        System.Windows.Controls.TextBox txtSIGKNOWSN = new System.Windows.Controls.TextBox();

        public PcbaCopyWindow()
        {

            Label lbPCBACopy = new Label();
            Label lbFactory = new Label();
            lbPCBACopy.Content = "PCBA碼復印";
            Border bLabel = new Border();
            bLabel.Background = System.Windows.Media.Brushes.White;
            bLabel.BorderBrush = System.Windows.Media.Brushes.White;
            bLabel.BorderThickness = new Thickness(1);
            bLabel.Padding = new Thickness(1);
            bLabel.Child = lbPCBACopy;


            StackPanel spBarcodeBody = new StackPanel();
            InitializeComponent();

            txtPCBASN.Width = 200;
            //txtPCBASN.Text = "PCBA 條碼讀取";
            txtPCBASN.Text = "";
            Border bPCBA = new Border();
            bPCBA.Background = System.Windows.Media.Brushes.White;
            bPCBA.BorderBrush = System.Windows.Media.Brushes.White;
            bPCBA.BorderThickness = new Thickness(1);
            bPCBA.Padding = new Thickness(1);
            bPCBA.Child = txtPCBASN;
            spBarcodeBody.Children.Add(bPCBA);

            StackPanel spMainBody = new StackPanel();
            spMainBody.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            spMainBody.Children.Add(bLabel);
            spMainBody.Children.Add(spBarcodeBody);

            // Add the parent Canvas as the Content of the Window Object
            this.Content = spMainBody;
            this.Width = 350;
            this.Height = 200;
            txtPCBASN.Focus();
            txtPCBASN.SelectAll();
            txtPCBASN.KeyDown += txtPCBASN_KeyDown;

        }

        private void txtPCBASN_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string theText="";
            //”Return”可用Keys.Enter替代，這樣寫if(e.Key==Keys.Enter)
            //如果要偵測同時按了Alt和Q的話，就用if(e.Alt && e.Key==Keys.Q)
            if (e.Key.ToString() == "Return")
            {
                if (textBox != null)
                {
                    theText = textBox.Text;
                    theText = textBox.GetLineText(0);
                }
                //MessageBox.Show(theText);
                TSCLIB_DLL.PrintPCBAWithText(theText);
                //TSCLIB_DLL.PrintPCBAWithText(theText);
                //textBox.Clear();
                textBox.SelectAll();

            }
        }

    }
}
