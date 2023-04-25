using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cattle_FarmingManagement
{
    public partial class staffcontrolpage : Form
        
    {
        DBconnect DBcon = new DBconnect();
        public void getTable()
        {
            string selectQuery = "select cid,cbreed,cmilk,checkup,pregnancy from acattle";
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
            TextBox_check.Clear();
            TextBox_preg.Clear();
        }
        public staffcontrolpage()
        {
            InitializeComponent();
        }

        private void label_logout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void staffcontrolpage_Load(object sender, EventArgs e)
        {
            getTable();
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
            TextBox_check.Text = DataGridView_cattle.SelectedRows[0].Cells[3].Value.ToString();
            TextBox_preg.Text = DataGridView_cattle.SelectedRows[0].Cells[4].Value.ToString();
        }

        //

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
        
        
        public bool preg(string name)
        {
            Regex rg = new Regex(@"\d+");
            if (!rg.IsMatch(name))
            {
                MessageBox.Show("invalid pregnancy detail", "Error_pregnancy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if ( (!milk(TextBox_milk.Text))  || (!preg(TextBox_preg.Text)) || (!name(TextBox_breed.Text)) )
                {

                    return;
                }
                
                if (TextBox_id.Text == "" || TextBox_breed.Text == "" || TextBox_milk.Text == "" )
                {
                    MessageBox.Show("Missing Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string updateQuery = "update acattle set cbreed ='"+TextBox_breed.Text.Trim()+"' , cmilk = " +TextBox_milk.Text+ ",checkup = " + TextBox_check.Text + ", pregnancy =" + TextBox_preg.Text+ " where cid = '" + TextBox_id.Text + "' ";
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

        private void Customer_Click(object sender, EventArgs e)
        {
            Customer cust = new Customer();
            cust.Show();
            this.Hide();
        }

        private void Vietnary_Click(object sender, EventArgs e)
        {
            Vietnary viet = new Vietnary();
            viet.Show();
            this.Hide();
        }

        private void Fooder_Click(object sender, EventArgs e)
        {
            Fooder food = new Fooder();
            food.Show();
            this.Hide();
        }

        private void Supplier_Click(object sender, EventArgs e)
        {
            Supplier sup = new Supplier();
            sup.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bill b = new Bill();
            b.Show();
            this.Hide();
        }
    }
}
