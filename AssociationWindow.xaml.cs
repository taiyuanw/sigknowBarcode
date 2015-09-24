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
using MySql;
using MySql.Data.MySqlClient;

namespace SigknowBarcode
{
    /// <summary>
    /// Interaction logic for AssociationWindow.xaml
    /// </summary>
    public partial class AssociationWindow : Window
    {
        System.Windows.Controls.TextBox txtboxAction = new System.Windows.Controls.TextBox();
        System.Windows.Controls.TextBox txtboxPCBASN = new System.Windows.Controls.TextBox();
        System.Windows.Controls.TextBox txtboxSIGKNOWSN = new System.Windows.Controls.TextBox();
        System.Windows.Controls.Label lblResult = new System.Windows.Controls.Label();


        public AssociationWindow()
        {
            InitializeComponent();

            //TextBox txtAssociation = new TextBox();
            Label lbAssociationWindow = new Label();
            Label lbAssociation = new Label();
            lbAssociationWindow.Content = "掃描動作條碼: 'A-建立關聯' , 'B-解除關聯' 或 'C-查尋關聯' : ";
            Border bLabel = new Border();
            bLabel.Background = System.Windows.Media.Brushes.White;
            bLabel.BorderBrush = System.Windows.Media.Brushes.White;
            bLabel.BorderThickness = new Thickness(1);
            bLabel.Padding = new Thickness(1);
            bLabel.Child = lbAssociationWindow;

            
            StackPanel spAssociationBody = new StackPanel();
            txtboxAction.Width = 150;
            txtboxAction.Text = "";
            Label lbAction = new Label();
            lbAction.Content = "指令:";
            StackPanel spAction = new StackPanel();
            spAction.Children.Add(lbAction);
            spAction.Children.Add(txtboxAction);
            spAction.Orientation = System.Windows.Controls.Orientation.Horizontal;

            Border bAction = new Border();
            bAction.Background = System.Windows.Media.Brushes.White;
            bAction.BorderBrush = System.Windows.Media.Brushes.White;
            bAction.BorderThickness = new Thickness(1);
            bAction.Padding = new Thickness(1);
            bAction.Child = spAction;
            spAssociationBody.Children.Add(bAction);

            StackPanel spWindow = new StackPanel();
            spWindow.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            spWindow.Children.Add(bLabel);
            spWindow.Children.Add(spAssociationBody);

            this.Content = spWindow;
            txtboxAction.Focus();
            txtboxAction.SelectAll();
            txtboxAction.KeyDown += txtboxAction_KeyDown;

        }

        private void txtboxAction_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string theText = "";
            //”Return”可用Keys.Enter替代，這樣寫if(e.Key==Keys.Enter)
            //如果要偵測同時按了Alt和Q的話，就用if(e.Alt && e.Key==Keys.Q)
            if (e.Key.ToString() == "Return")
            {
                if (textBox != null)
                {
                    theText = textBox.Text;
                    theText = textBox.GetLineText(0);
                }

                if (theText == Utils.ASSOCIATE)
                {
                    //MessageBox.Show("Let there be ...   Ass!");
                    txtboxAction.Text = "建立關聯";
                    txtboxAction.SelectAll();
                    AssociationAssociate AA_window = new AssociationAssociate();
                    AA_window.ShowDialog();
                }
                else if (theText == Utils.DISASSOCIATE)
                {
                    txtboxAction.Text = "解除關聯";
                    txtboxAction.SelectAll();
                    AssociationDisassociate AD_window = new AssociationDisassociate();
                    AD_window.ShowDialog();
                }
                else if (theText == Utils.QUERY)
                {
                    txtboxAction.Text = "查詢關聯";
                    txtboxAction.SelectAll();
                    AssociationQuery AQ_window = new AssociationQuery();
                    AQ_window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("不正確的條碼! 請擇一掃描 : \n'建立關聯 (ASSOCIATE) ' , '解除關聯 (DISASSOCIATE) ', 或是 '查尋關聯 (QUERY) '.");
                    txtboxAction.Focus();
                    txtboxAction.Clear();
                }
            }
        }

    }
}
