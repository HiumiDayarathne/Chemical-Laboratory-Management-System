using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CLMS_Test
{
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        // Log in button click
        private void button1_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-2EC93NV\SQLEXPRESS;Initial Catalog=Chemical Laboratory Management System;Integrated Security=True"))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT * FROM StudentInformation WHERE StudentNo = @studentNo AND Password = @password";
                        SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                        sda.SelectCommand.Parameters.AddWithValue("@studentNo", textBox1.Text.Trim());
                        sda.SelectCommand.Parameters.AddWithValue("@password", textBox2.Text.Trim());

                        DataTable dta = new DataTable();
                        sda.Fill(dta);

                        if (dta.Rows.Count > 0)
                        {
                            // Login successful
                            Form5 form5 = new Form5();
                            this.Hide();
                            form5.Show();
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

        //sign up button click. Load student registration form
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 form8 = new Form8();  
            form8.ShowDialog();
        }

        //Back button click. Load Main page form
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form12 form12 = new Form12();
            form12.ShowDialog();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
