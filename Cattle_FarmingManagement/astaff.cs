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
    public partial class astaff : Form
    {
        DBconnect DBcon = new DBconnect();
        public astaff()
        {
            InitializeComponent();
        }
        public bool cid(string stxt)
        {
            Regex rg = new Regex(@"^[s]\d+", RegexOptions.IgnoreCase);
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
        public void getTable()
        {
            string selectQuery = "select * from astaff";
            SqlCommand command = new SqlCommand(selectQuery, DBcon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_staff.DataSource = table;
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {

                if ((!cid(TextBox_id.Text)) || (!phone(TextBox_phone.Text)) || (!name(TextBox_name.Text)) )
                {
                   
                    return;
                }
                string insertQuery = "insert into astaff values('"+TextBox_id.Text.Trim()+"','"+TextBox_name.Text.Trim()+"','"+TextBox_password.Text+"','" +TextBox_dob.Text+ "',"+TextBox_phone.Text+")";
                SqlCommand command = new SqlCommand(insertQuery, DBcon.GetCon());
                DBcon.openCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Staff Added Successfully","Add Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                DBcon.closeCon();
                getTable();
                clear();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void astaff_Load(object sender, EventArgs e)
        {
            getTable();
        }

       private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!cid(TextBox_id.Text)) || (!phone(TextBox_phone.Text)) || (!name(TextBox_name.Text)))
                {
                    return;
                }
                if (TextBox_id.Text == "" || TextBox_name.Text == "" || TextBox_password.Text == "" || TextBox_dob.Text == "" || TextBox_phone.Text == "")
                {
                    MessageBox.Show("Missing Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string updateQuery = "update astaff set sname ='" + TextBox_name.Text.Trim() + "' , spassword = '" + TextBox_password.Text + "',sdob = '" + TextBox_dob.Text + "', sphone ='" + TextBox_phone.Text + "' where sid = '" + TextBox_id.Text + "' ";
                    SqlCommand command = new SqlCommand(updateQuery, DBcon.GetCon());
                    DBcon.openCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Staff Update Successfully", "Update information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void DataGridView_staff_Click(object sender, EventArgs e)
        {
            TextBox_id.Text = DataGridView_staff.SelectedRows[0].Cells[0].Value.ToString();
            TextBox_name.Text = DataGridView_staff.SelectedRows[0].Cells[1].Value.ToString();
            TextBox_password.Text = DataGridView_staff.SelectedRows[0].Cells[2].Value.ToString();
            TextBox_dob.Text = DataGridView_staff.SelectedRows[0].Cells[3].Value.ToString();
            TextBox_phone.Text = DataGridView_staff.SelectedRows[0].Cells[4].Value.ToString();
        }
        private void clear()
        {
            TextBox_id.Clear();
            TextBox_name.Clear();
            TextBox_password.Clear();
            TextBox_dob.Clear();
            TextBox_phone.Clear();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                string deleteQuery = "delete from astaff where sid = '" +TextBox_id.Text+ "' " ;
                SqlCommand command = new SqlCommand(deleteQuery, DBcon.GetCon());
                DBcon.openCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Staff delete Successfully", "delete information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBcon.closeCon();
                getTable();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Goldenrod;
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

        private void cattle_Click(object sender, EventArgs e)
        {
            acattle cattle = new acattle();
            cattle.Show();
            this.Hide();
        }
    }
}
