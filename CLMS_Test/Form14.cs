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

namespace CLMS_Test
{
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }

        private void Form14_Load(object sender, EventArgs e)
        {

        }

        private bool isValid()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Enter a valid username", "Error");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Enter a valid password", "Error");
                return false;
            }
            return true;
        }

        //log in button click
        private void button1_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-2EC93NV\SQLEXPRESS;Initial Catalog=Chemical Laboratory Management System;Integrated Security=True"))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT * FROM AdminInformation WHERE AdminID = @adminId AND Password = @password";
                        SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                        sda.SelectCommand.Parameters.AddWithValue("@adminId", textBox1.Text.Trim());
                        sda.SelectCommand.Parameters.AddWithValue("@password", textBox2.Text.Trim());

                        DataTable dta = new DataTable();
                        sda.Fill(dta);

                        if (dta.Rows.Count > 0)
                        {
                            // Login successful
                            Form15 form15 = new Form15();
                            this.Hide();
                            form15.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password", "Error");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error");
                    }
                }
            }
        }

        
        private void button2_Click(object sender, EventArgs e)
        {

        }

        //back button click. Go to Main page form
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form12 form12 = new Form12();
            form12.ShowDialog();
        }
    }
}
