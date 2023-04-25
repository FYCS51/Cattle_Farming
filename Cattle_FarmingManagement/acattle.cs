using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Cattle_FarmingManagement
{
    public partial class acattle : Form
    {
        DBconnect DBcon = new DBconnect();
        public acattle()
        {
            InitializeComponent();
        }
        public void getTable()
        {
            string selectQuery = "select cid,cbreed,cmilk,cprice,cdob from acattle";
            SqlCommand command = new SqlCommand(selectQuery, DBcon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_cattle.DataSource = table;
        }
        private void clear()
        {
            TextBox_id.Clear();
            TextBox_breed.Clear();
            TextBox_milk.Clear();
            TextBox_dob.Clear();
            TextBox_price.Clear();
        }


        private void acattle_Load(object sender, EventArgs e)
        {
            getTable();
        }
        public bool cid(string ctxt)
        {
            Regex rg = new Regex(@"^[cb]\d+",RegexOptions.IgnoreCase);
            if (!rg.IsMatch(ctxt))
            {
                MessageBox.Show("Enter the correct id", "Error_id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public bool milk(string gty)
        {
            Regex rg = new Regex(@"\d+");
            if (!rg.IsMatch(gty))
            {
                MessageBox.Show("Enter the correct milk detail", "Error_id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
      
        }
        public bool name(string name)
        {
            Regex rg = new Regex(@"^[a-z]{3,15}$", RegexOptions.IgnoreCase);
            if (!rg.IsMatch(name))
            {
                MessageBox.Show("invalid name", "Error_name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                if ( (!cid(TextBox_id.Text)) || (!milk(TextBox_milk.Text)) || (!name(TextBox_breed.Text)))
                {
                   
                    return;
                }
                //TextBox_breed.Text = Regex.Replace(TextBox_breed.Text, @"\s+","");
                string insertQuery = "insert into acattle values('" + TextBox_id.Text.Trim() + "','" + TextBox_breed.Text.Trim() + "'," + TextBox_milk.Text + "," + TextBox_price.Text + ",'" + TextBox_dob.Text + "',null,null)";
                SqlCommand command = new SqlCommand(insertQuery, DBcon.GetCon());
                DBcon.openCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Cattle Added Successfully", "Add Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBcon.closeCon();
                getTable();
                clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!cid(TextBox_id.Text)) || (!milk(TextBox_milk.Text)) || (!name(TextBox_breed.Text)))
                {
                   
                    return;
                }
                if (TextBox_id.Text == "" || TextBox_breed.Text == "" || TextBox_milk.Text == "" || TextBox_dob.Text == "" || TextBox_price.Text == "")
                {
                    MessageBox.Show("Missing Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    TextBox_breed.Text = Regex.Replace(TextBox_breed.Text, @"\s+", "");
                    string updateQuery = "update acattle set cbreed ='"+TextBox_breed.Text.Trim()+"' , cmilk = " + TextBox_milk.Text + ",cprice = " + TextBox_price.Text + ", cdob ='" + TextBox_dob.Text + "' where cid = '" + TextBox_id.Text + "' ";
                    SqlCommand command = new SqlCommand(updateQuery, DBcon.GetCon());
                    DBcon.openCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Cattle Update Successfully", "Update information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DBcon.closeCon();
                    getTable();
                    clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                string deleteQuery = "delete from acattle where cid = '" + TextBox_id.Text + "' ";
                SqlCommand command = new SqlCommand(deleteQuery, DBcon.GetCon());
                DBcon.openCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Cattle delete Successfully", "delete information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBcon.closeCon();
                getTable();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void staff_Click(object sender, EventArgs e)
        {
            astaff staff = new astaff();
            staff.Show();
            this.Hide();
        }

        private void label_logout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void label_logout_MouseEnter(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Red;

        }

        private void label_logout_MouseLeave(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Goldenrod;
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Goldenrod;
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DataGridView_cattle_Click(object sender, EventArgs e)
        {
            TextBox_id.Text = DataGridView_cattle.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_breed.Text = DataGridView_cattle.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_milk.Text = DataGridView_cattle.SelectedRows[0].Cells[2].Value.ToString();
            TextBox_price.Text = DataGridView_cattle.SelectedRows[0].Cells[3].Value.ToString();
            TextBox_dob.Text = DataGridView_cattle.SelectedRows[0].Cells[4].Value.ToString();
        }
    }
}

