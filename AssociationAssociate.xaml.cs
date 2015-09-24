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
using System.Threading;

namespace SigknowBarcode
{
    /// <summary>
    /// Interaction logic for AssociationAssociate.xaml
    /// </summary>
    public partial class AssociationAssociate : Window
    {
        System.Windows.Controls.TextBox txtboxPCBASN = new System.Windows.Controls.TextBox();
        System.Windows.Controls.TextBox txtboxPATCHSN = new System.Windows.Controls.TextBox();
        //System.Windows.Controls.Label lblResult = new System.Windows.Controls.Label();

        public AssociationAssociate()
        {
            InitializeComponent();
            associate();

        }
        private void associate()
        {
            StackPanel spAssociationBody = new StackPanel();

            TextBox txtAssociation = new TextBox();
            Label lbAssociation = new Label();
            txtAssociation.Text = "先掃描 <PCBA碼> , 再掃描 <產品條碼> ";
            Border bLabel = new Border();
            bLabel.Background = System.Windows.Media.Brushes.White;
            bLabel.BorderBrush = System.Windows.Media.Brushes.White;
            bLabel.BorderThickness = new Thickness(1);
            bLabel.Padding = new Thickness(1);
            bLabel.Child = txtAssociation;


            txtboxPCBASN.Width = 200;
            txtboxPCBASN.Text = "<掃描PCBA條碼>";
            Border bPCBASN = new Border();
            bPCBASN.Background = System.Windows.Media.Brushes.White;
            bPCBASN.BorderBrush = System.Windows.Media.Brushes.White;
            bPCBASN.BorderThickness = new Thickness(1);
            bPCBASN.Padding = new Thickness(1);
            bPCBASN.Child = txtboxPCBASN;
            spAssociationBody.Children.Add(bPCBASN);

            txtboxPATCHSN.Width = 200;
            txtboxPATCHSN.Text = "<掃描產品條碼>";
            Border bPATCHSN = new Border();
            bPATCHSN.Background = System.Windows.Media.Brushes.White;
            bPATCHSN.BorderBrush = System.Windows.Media.Brushes.White;
            bPATCHSN.BorderThickness = new Thickness(1);
            bPATCHSN.Padding = new Thickness(1);
            bPATCHSN.Child = txtboxPATCHSN;
            spAssociationBody.Children.Add(bPATCHSN);


            StackPanel spWindow = new StackPanel();
            spWindow.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            spWindow.Children.Add(bLabel);
            spWindow.Children.Add(spAssociationBody);

            this.Content = spWindow;
            this.Width = 350;
            this.Height = 200;
            txtboxPCBASN.Focus();
            txtboxPCBASN.SelectAll();
            txtboxPCBASN.KeyDown +=txtboxPCBASN_KeyDown;
            txtboxPATCHSN.KeyDown += txtboxPATCHSN_KeyDown;
        }
        private void validate_pcbasn(string pcbasn)
        {
            if (!pcbasn.Substring(0, 1).ToUpper().Contains("B"))
            {
                throw new Exception("invalid format detected.");
            }
        }
        private void validate_patchsn(string patchsn)
        {
            if (!patchsn.Substring(0, 1).ToUpper().Contains("T") && !patchsn.Substring(0, 1).ToUpper().Contains("C"))
            {
                throw new Exception("invalid format detected.");
            }
        }
        private void dbinsert()
        {
            string _pcbsn = String.Copy(txtboxPCBASN.Text);
            string _patchsn = String.Copy(txtboxPATCHSN.Text);

            if ((_pcbsn.Length == 0) || (_patchsn.Length == 0))
            {
                return;
            }
            else
            {
                //MessageBox.Show(_pcbsn.Length.ToString() + " and " + _patchsn.Length.ToString());
            }
            string cmd = "insert into ezypatch (PCBASN, SIGKNOWSN) values ('" + _pcbsn + "', '" + _patchsn + "')";
            MySQLDB.DBcommand(cmd);

        }
        private void txtboxPCBASN_KeyDown(object sender, KeyEventArgs e)
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
                    //MessageBox.Show(theText);
                    validate_pcbasn(theText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("PCBA 格式不符. 請再掃描一次.");
                    txtboxPCBASN.Clear();
                    txtboxPCBASN.Focus();
                }

                if (txtboxPCBASN.Text != "")
                {
                    txtboxPATCHSN.Focus();
                    txtboxPATCHSN.SelectAll();
                }
            }
        }
        private void txtboxPATCHSN_KeyDown(object sender, KeyEventArgs e)
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
                    //MessageBox.Show(theText);
                    validate_patchsn(theText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("產品序號 格式不符. 請再掃描一次.");
                    txtboxPATCHSN.Clear();
                    txtboxPATCHSN.Focus();
                }

                if (txtboxPATCHSN.Text != "")
                {
                    dbinsert();
                    this.Close();
                }
            }
        }
    }
}
