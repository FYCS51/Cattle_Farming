using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cattle_FarmingManagement
{
    public partial class Bill : Form
    {
        public Bill()
        {
            InitializeComponent();
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            DBconnect DBcon = new DBconnect();
            if (TextBox_name.Text == "" || TextBox_phone.Text == "")
            {

                MessageBox.Show("Please Enter The Name And Phone", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (comboBox_role.SelectedIndex > -1)
                {
                    if (comboBox_role.SelectedItem.ToString() == "Customer")
                    {
                        string selectQuery = "select ctmilk,ctextra,ctid from customer where ctname = '" + TextBox_name.Text.Trim() + "' and ctphone = '" + TextBox_phone.Text.Trim() + "' ";
                        SqlCommand command = new SqlCommand(selectQuery, DBcon.GetCon());
                        DBcon.openCon();
                        SqlDataReader rdr = command.ExecuteReader();
                        if (rdr.Read())
                        {
                            Double db1= rdr.GetDouble(0);
                            Double db2 = rdr.GetDouble(1);
                            Double final = (db1* 80*31)+db2*80;
                            TextBox_cost.Text = final.ToString();
                            Textbox_id.Text = rdr.GetString(2);
                            DBcon.closeCon();
                            printtext.Clear();
                            printtext.Text += "***********************************************************************************\n";
                            printtext.Text += "                                              FEES RECEIPT                         \n";
                            printtext.Text += "***********************************************************************************\n";
                            printtext.Text += "  Date  : " + DateTime.Now + "\n\n";
                            printtext.Text += "  Name  : " + TextBox_name.Text + "\n\n";
                            printtext.Text += "  Cost  : " + TextBox_cost.Text + "\n\n\n\n\n\n\n\n\n";
                            printtext.Text += "                                                          Signature  :         \n\n\n\n";
                            printtext.Text += "***********************************************************************************\n";
                            printtext.Text += "                                                 Modern Diary                         \n";
                            printtext.Text += "                                      Link Road ,vasai west 401-209                \n";
                            printtext.Text += "                                           Phone no : 9890460253                   \n";
                            printtext.Text += "***********************************************************************************\n";
                        }
                        else
                        {
                            MessageBox.Show("Please Enter the correct Name and Phone number", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            DBcon.closeCon();
                        }


                    }
                    else if(comboBox_role.SelectedItem.ToString() == "Vietnary") 
                    {
                        string selectQuery = "select vcost,vid from vietnary where vname = '" + TextBox_name.Text.Trim() + "' and vphone = '" + TextBox_phone.Text.Trim() + "' ";
                        SqlCommand command = new SqlCommand(selectQuery, DBcon.GetCon());
                        DBcon.openCon();
                        SqlDataReader rdr = command.ExecuteReader();
                        if (rdr.Read())
                        {
                            Double db1 = rdr.GetDouble(0);
                            TextBox_cost.Text =db1.ToString();
                            Textbox_id.Text = rdr.GetString(1);
                            DBcon.closeCon();
                            printtext.Clear();
                            printtext.Text += "***********************************************************************************\n";
                            printtext.Text += "                                              FEES RECEIPT                         \n";
                            printtext.Text += "***********************************************************************************\n";
                            printtext.Text += "  Date  : " + DateTime.Now + "\n\n";
                            printtext.Text += "  Name  : " + TextBox_name.Text + "\n\n";
                            printtext.Text += "  Cost  : " + TextBox_cost.Text + "\n\n\n\n\n\n\n\n\n";
                            printtext.Text += "                                                          Signature  :         \n\n\n\n";
                            printtext.Text += "***********************************************************************************\n";
                            printtext.Text += "                                                  Modern Diary                         \n";
                            printtext.Text += "                                      Link Road ,vasai west 401-209                \n";
                            printtext.Text += "                                           Phone no : 9890460253                   \n";
                            printtext.Text += "***********************************************************************************\n";
                        }
                        else
                        {
                            MessageBox.Show("Please Enter the correct Name and Phone number", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            DBcon.closeCon();
                        }

                    }
                    else
                    {
                        string selectQuery = "select sid,tamt,ramt from supplier where sname = '" + TextBox_name.Text.Trim() + "' and sphone = '" + TextBox_phone.Text.Trim() + "' ";
                        SqlCommand command = new SqlCommand(selectQuery, DBcon.GetCon());
                        DBcon.openCon();
                        SqlDataReader rdr = command.ExecuteReader();
                        if (rdr.Read())
                        {

                            TextBox_cost.Text = (rdr.GetDouble(1) + rdr.GetDouble(2)).ToString();
                            Textbox_id.Text = rdr.GetString(0);
                            DBcon.closeCon();
                            printtext.Clear();
                            printtext.Text += "***********************************************************************************\n";
                            printtext.Text += "                                              FEES RECEIPT                         \n";
                            printtext.Text += "***********************************************************************************\n";
                            printtext.Text += "  Date  : " + DateTime.Now + "\n\n";
                            printtext.Text += "  Name  : " + TextBox_name.Text + "\n\n";
                            printtext.Text += "  Cost  : " + TextBox_cost.Text + "\n\n\n\n\n\n\n\n\n";
                            printtext.Text += "                                                          Signature  :         \n\n\n\n";
                            printtext.Text += "***********************************************************************************\n";
                            printtext.Text += "                                                 Modern Diary                         \n";
                            printtext.Text += "                                      Link Road ,vasai west 401-209                \n";
                            printtext.Text += "                                           Phone no : 9890460253                   \n";
                            printtext.Text += "***********************************************************************************\n";
                        }
                        else
                        {
                            MessageBox.Show("Please Enter the correct Name and Phone number", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            DBcon.closeCon();
                        }
                    }
                  
                }
                else
                {
                    MessageBox.Show("Please Select Role", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            printtext.Clear();
            Textbox_id.Clear();
            TextBox_name.Clear();
            TextBox_phone.Clear();
            TextBox_cost.Clear();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(printtext.Text,new Font("Microsoft San Serif",18,FontStyle.Bold),Brushes.Black,new Point(10,10));

        }

        private void Print_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Goldenrod;
        }

        private void back_Click(object sender, EventArgs e)
        {
            staffcontrolpage pg = new staffcontrolpage();
            pg.Show();
            this.Hide();
        }

        private void Back_Click_1(object sender, EventArgs e)
        {
            staffcontrolpage sc = new staffcontrolpage();
            sc.Show();
            this.Hide();
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
