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
    public partial class Fooder : Form
    {
        DBconnect DBcon = new DBconnect();
        public bool fooder(string stxt)
        {
            Regex rg = new Regex(@"^[f]\d+", RegexOptions.IgnoreCase);
            if (!rg.IsMatch(stxt))
            {
                MessageBox.Show("Enter the correct id", "Error_id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
       
        public bool name(string name)
        {
            Regex rg = new Regex(@"^[A-Za-z]{3,15}$");
            if (!rg.IsMatch(name))
            {
                MessageBox.Show("invalid name", "Error_name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public void getTable()
        {
            string selectQuery = "select * from fooder";
            SqlCommand command = new SqlCommand(selectQuery, DBcon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_cattle.DataSource = table;
        }
        private void clear()
        {
            TextBox_id.Clear();
            TextBox_name.Clear();
            TextBox_cost.Clear();
            TextBox_qty.Clear();
            TextBox_remain.Clear();
        }
        public Fooder()
        {
            InitializeComponent();
        }

        private void Fooder_Load(object sender, EventArgs e)
        {
            getTable();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!fooder(TextBox_id.Text))  || (!name(TextBox_name.Text)))
                {

                    return;
                }
                string insertQuery = "insert into fooder values('" + TextBox_id.Text + "','" + TextBox_name.Text + "'," + TextBox_cost.Text + "," + TextBox_qty.Text + "," + TextBox_remain.Text + ")";
                SqlCommand command = new SqlCommand(insertQuery, DBcon.GetCon());
                DBcon.openCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Fooder Added Successfully", "Add Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_cost.Text == "" || TextBox_qty.Text == "" || TextBox_remain.Text == "" )
                {
                    MessageBox.Show("Missing Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string updateQuery = "update fooder set fname ='" + TextBox_name.Text + "' , fcost = " + TextBox_cost.Text + ",fqty= " + TextBox_qty.Text + ", frmn =" + TextBox_remain.Text + " where fid = '" + TextBox_id.Text + "' ";
                    SqlCommand command = new SqlCommand(updateQuery, DBcon.GetCon());
                    DBcon.openCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Fooder Update Successfully", "Update information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string deleteQuery = "delete from fooder where fid = '" + TextBox_id.Text + "' ";
                SqlCommand command = new SqlCommand(deleteQuery, DBcon.GetCon());
                DBcon.openCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Fooder delete Successfully", "delete information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBcon.closeCon();
                getTable();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridView_cattle_Click(object sender, EventArgs e)
        {
            TextBox_id.Text = DataGridView_cattle.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_name.Text = DataGridView_cattle.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_cost.Text = DataGridView_cattle.SelectedRows[0].Cells[2].Value.ToString();
            TextBox_qty.Text = DataGridView_cattle.SelectedRows[0].Cells[3].Value.ToString();
            TextBox_remain.Text = DataGridView_cattle.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void label_logout_MouseEnter(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Red;
        }

        private void label_logout_MouseLeave(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Goldenrod;
        }

        private void label_logout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
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

        private void Cattle_Click(object sender, EventArgs e)
        {

        }

        private void Vietnary_Click(object sender, EventArgs e)
        {
            Vietnary viet = new Vietnary();
            viet.Show();
            this.Hide();
        }


        private void Supplier_Click(object sender, EventArgs e)
        {
            Supplier sup = new Supplier();
            sup.Show();
            this.Hide();
        }

        private void Customer_Click(object sender, EventArgs e)
        {
            Customer cust = new Customer();
            cust.Show();
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
