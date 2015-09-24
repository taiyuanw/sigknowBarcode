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
    /// Interaction logic for Packaging.xaml
    /// </summary>
    public partial class PackagingWindow : Window
    {
        System.Windows.Controls.TextBox txtEzyPatchSN = new System.Windows.Controls.TextBox();

        public PackagingWindow()
        {

            Label lbPackaging = new Label();
            Label lbFactory = new Label();
            lbPackaging.Content = "包裝列印(條碼五張 + 使用期限一張)";
            Border bLabel = new Border();
            bLabel.Background = System.Windows.Media.Brushes.White;
            bLabel.BorderBrush = System.Windows.Media.Brushes.White;
            bLabel.BorderThickness = new Thickness(1);
            bLabel.Padding = new Thickness(1);
            bLabel.Child = lbPackaging;


            StackPanel spBarcodeBody = new StackPanel();
            InitializeComponent();

            txtEzyPatchSN.Width = 200;
            txtEzyPatchSN.Text = "<輸入產品條碼>";
            Border bPCBA = new Border();
            bPCBA.Background = System.Windows.Media.Brushes.White;
            bPCBA.BorderBrush = System.Windows.Media.Brushes.White;
            bPCBA.BorderThickness = new Thickness(1);
            bPCBA.Padding = new Thickness(1);
            bPCBA.Child = txtEzyPatchSN;
            spBarcodeBody.Children.Add(bPCBA);

            StackPanel spMainBody = new StackPanel();
            spMainBody.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            spMainBody.Children.Add(bLabel);
            spMainBody.Children.Add(spBarcodeBody);

            // Add the parent Canvas as the Content of the Window Object
            this.Content = spMainBody;
            this.Width = 350;
            this.Height = 200;
            txtEzyPatchSN.Focus();
            txtEzyPatchSN.SelectAll();
            txtEzyPatchSN.KeyDown += txtEzyPatchSN_KeyDown;

        }

        private void txtEzyPatchSN_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string theText = "";
            if (e.Key.ToString() == "Return")
            {
                if (textBox != null)
                {
                    theText = textBox.Text;
                    theText = textBox.GetLineText(0);
                }
                try
                {
                    
                    for (int i = 0; i < 5; i++)
                        TSCLIB_DLL.PrintBarcodeWithText(theText);
                    TSCLIB_DLL.PrintSNAndExpDate(theText, GetExpireDate(theText));
                    
                    //TSCLIB_DLL.PrintBarcodeWithText(theText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    this.Close();
                }
                textBox.SelectAll();

            }
        }
        private string GetExpireDate(string SN)
        {
            // SN format : T20150512001
            return (int.Parse(SN.Substring(1,4) )+ 1).ToString() + "-" + SN.Substring(5,2) + "-" + SN.Substring(7,2);
        }
    }


}
