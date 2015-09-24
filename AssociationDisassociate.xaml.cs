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
    /// Interaction logic for AssociationDisassociate.xaml
    /// </summary>
    public partial class AssociationDisassociate : Window
    {
        System.Windows.Controls.TextBox txtboxSN = new System.Windows.Controls.TextBox();
        string SNtype = "";

        public AssociationDisassociate()
        {
            InitializeComponent();
            disassociate();
        }
        private void disassociate()
        {
            StackPanel spAssociationBody = new StackPanel();

            TextBox txtLabel = new TextBox();
            Label lbAssociation = new Label();
            txtLabel.Text = "掃描 <PCBA碼> 或者是 <產品條碼> ";
            Border bLabel = new Border();
            bLabel.Background = System.Windows.Media.Brushes.White;
            bLabel.BorderBrush = System.Windows.Media.Brushes.White;
            bLabel.BorderThickness = new Thickness(1);
            bLabel.Padding = new Thickness(1);
            bLabel.Child = txtLabel;


            txtboxSN.Width = 200;
            txtboxSN.Text = "<掃描條碼>";
            Border bSN = new Border();
            bSN.Background = System.Windows.Media.Brushes.White;
            bSN.BorderBrush = System.Windows.Media.Brushes.White;
            bSN.BorderThickness = new Thickness(1);
            bSN.Padding = new Thickness(1);
            bSN.Child = txtboxSN;
            spAssociationBody.Children.Add(bSN);

            StackPanel spWindow = new StackPanel();
            spWindow.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            spWindow.Children.Add(bLabel);
            spWindow.Children.Add(spAssociationBody);

            this.Content = spWindow;
            this.Width = 350;
            this.Height = 200;
            txtboxSN.Focus();
            txtboxSN.SelectAll();
            txtboxSN.KeyDown += txtboxSN_KeyDown;
        }
        private void txtboxSN_KeyDown(object sender, KeyEventArgs e)
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
                    //TODO 
                    validatesn(theText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SN 格式不符. 請再掃描一次.");
                    txtboxSN.Clear();
                    txtboxSN.Focus();
                    Utils.ErrorBeep();
                }

                try
                {
                    if (txtboxSN.Text != "")
                    {
                        dbdelete(SNtype, theText);
                        //Utils.OKBeep();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ErrorBeep();
                }
            }
        }
        private void validatesn(string sn)
        {
            if (!sn.Substring(0, 1).ToUpper().Contains("T") && !sn.Substring(0, 1).ToUpper().Contains("C") && !sn.Substring(0, 1).ToUpper().Contains("B"))
            {
                throw new Exception("invalid format detected.");
            }
            else if (sn.Substring(0, 1).ToUpper().Contains("T") || sn.Substring(0, 1).ToUpper().Contains("C"))
            {
                SNtype = "SIGKNOWSN";
            }
            else if (sn.Substring(0, 1).ToUpper().Contains("B"))
            {
                SNtype = "PCBASN";
            }


        }
        private void dbdelete(string col, string sn)
        {
            if ((col.Length == 0) || (sn.Length == 0))
            {
                return;
            }
            else
            {
                //MessageBox.Show(_pcbsn.Length.ToString() + " and " + _patchsn.Length.ToString());
            }
            string cmd = "set SQL_SAFE_UPDATES = 0; delete from ezypatch where " + col + " = '" + sn + "'";
            MySQLDB.DBcommand(cmd);

        }
    }
}
