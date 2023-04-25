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
    public partial class Customer : Form
    {
        DBconnect DBcon = new DBconnect();
        public void getTable()
        {
            string selectQuery = "select ctId,ctname,ctphone,ctmilk,ctextra from customer";
            SqlCommand command = new SqlCommand(selectQuery, DBcon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_cattle.DataSource = table;
        }

        public bool cusid(string stxt)
        {
            Regex rg = new Regex(@"^[c][u][s]\d+", RegexOptions.IgnoreCase);
            if (!rg.IsMatch(stxt))
            {
                MessageBox.Show("Enter the correct id", "Error_id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public bool phone(string ph)
        {
            Regex rg = new Regex(@"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)");
            if (!rg.IsMatch(ph))
            {
                MessageBox.Show("invalid phone no", "Error_phone", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void clear()
        {
            TextBox_id.Clear();
            TextBox_name.Clear();
            TextBox_phone.Clear();
            TextBox_milk.Clear();
            TextBox_extra.Clear();
            TextBox_address.Clear();
        }
        public Customer()
        {
            InitializeComponent();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!cusid(TextBox_id.Text)) || (!phone(TextBox_phone.Text)) || (!name(TextBox_name.Text)))
                {

                    return;
                }
                string insertQuery = "insert into customer values('" + TextBox_id.Text.Trim() + "','" + TextBox_name.Text.Trim() + "','" + TextBox_phone.Text + "'," + TextBox_milk.Text + "," + TextBox_extra.Text + ",'"+TextBox_address.Text+"')";
                SqlCommand command = new SqlCommand(insertQuery, DBcon.GetCon());
                DBcon.openCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Customer Added Successfully", "Add Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBcon.closeCon();
                getTable();
                clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void Customer_Load(object sender, EventArgs e)
        {
            getTable();
        }

        private void DataGridView_cattle_Click(object sender, EventArgs e)
        {
            TextBox_id.Text = DataGridView_cattle.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_name.Text = DataGridView_cattle.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_phone.Text = DataGridView_cattle.SelectedRows[0].Cells[2].Value.ToString();
            TextBox_milk.Text = DataGridView_cattle.SelectedRows[0].Cells[3].Value.ToString();
            TextBox_extra.Text = DataGridView_cattle.SelectedRows[0].Cells[4].Value.ToString();
            string selectQuery = "select address from customer where ctId = '" + TextBox_id.Text + "'";
            SqlCommand command = new SqlCommand(selectQuery, DBcon.GetCon());
            DBcon.openCon();
            TextBox_address.Text =command.ExecuteScalar().ToString();
            DBcon.closeCon();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!cusid(TextBox_id.Text)) || (!phone(TextBox_phone.Text)) || (!name(TextBox_name.Text)))
                {

                    return;
                }
                if (TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_phone.Text == "" || TextBox_milk.Text == "" || TextBox_extra.Text == "" || TextBox_address.Text == "")
                {
                    MessageBox.Show("Missing Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string updateQuery = "update customer set ctname ='" + TextBox_name.Text.Trim() + "' , ctphone = '" + TextBox_phone.Text + "',ctmilk = " + TextBox_milk.Text + ", ctextra =" + TextBox_extra.Text + ",address ='" + TextBox_address.Text + "'where ctId = '" + TextBox_id.Text + "' ";
                    SqlCommand command = new SqlCommand(updateQuery, DBcon.GetCon());
                    DBcon.openCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Customer Update Successfully", "Update information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string deleteQuery = "delete from customer where ctId = '" + TextBox_id.Text + "' ";
                SqlCommand command = new SqlCommand(deleteQuery, DBcon.GetCon());
                DBcon.openCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Customer delete Successfully", "delete information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBcon.closeCon();
                getTable();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cattle_Click(object sender, EventArgs e)
        {
            staffcontrolpage staff = new staffcontrolpage();
            staff.Show();
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
