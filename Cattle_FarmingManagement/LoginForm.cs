using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient ;

namespace Cattle_FarmingManagement
{
    public partial class LoginForm : Form
    {
        DBconnect DBcon = new DBconnect();
        public LoginForm()
        {
            InitializeComponent();
        }

       

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Goldenrod;
        }

        private void label_clear_MouseEnter(object sender, EventArgs e)
        {
            label_clear.ForeColor = Color.Red;
        }

        private void label_clear_MouseLeave(object sender, EventArgs e)
        {
            label_clear.ForeColor = Color.Goldenrod;
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_clear_Click(object sender, EventArgs e)
        {
            TextBox_username.Clear();
            TextBox_password.Clear();
        }

        private void Button_login_Click(object sender, EventArgs e)
        {
           
            if(TextBox_username.Text == "" || TextBox_password.Text == "") {

                MessageBox.Show("Please Enter The Username And Password", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(comboBox_role.SelectedIndex > -1)
                {
                    if (comboBox_role.SelectedItem.ToString() == "Admin")
                    {
                        if (TextBox_username.Text == "Admin" || TextBox_password.Text == "Admin123")
                        {
                            astaff admin = new astaff();
                            admin.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Admin Please Enter Username or Password", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        string selectQuery = "select * from astaff where  sname = '" + TextBox_username.Text + "' and spassword='" + TextBox_password.Text + "'";
                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, DBcon.GetCon());
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            staffcontrolpage staffcontrol = new staffcontrolpage();
                            staffcontrol.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Please Select Role", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
           
        }
    }

    }

