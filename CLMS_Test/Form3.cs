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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CLMS_Test
{
    public partial class Form3 : Form
    {
        
        String firstName, lastName, email, studentNo;
        
        String conString = "Data Source=DESKTOP-2EC93NV\\SQLEXPRESS;Initial Catalog=\"Chemical Laboratory Management System\";Integrated Security=True";
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            BindData();
        }

        //delete button click
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    studentNo = textBox1.Text;

                    // Check if the record with the specified Chemical_ID exists
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Open();
                        string checkQuery = "SELECT COUNT(*) FROM StudentInformation WHERE StudentNo = @studentNo";
                        SqlCommand checkCommand = new SqlCommand(checkQuery, con);
                        checkCommand.Parameters.AddWithValue("@studentNo", studentNo);

                        int rowCount = (int)checkCommand.ExecuteScalar();

                        if (rowCount == 0)
                        {
                            MessageBox.Show("No record with Student Number " + studentNo + " found.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (MessageBox.Show("This will permanently delete the record from your database. Are you sure?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                SqlCommand deleteCommand = new SqlCommand("DELETE FROM StudentInformation WHERE StudentNo = @studentNo", con);
                                deleteCommand.Parameters.AddWithValue("@studentNo", studentNo);
                                deleteCommand.ExecuteNonQuery();
                                MessageBox.Show("Successfully deleted.");
                                BindData();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter the Student Number");
            }
        }

        //reset button click
        private void button5_Click(object sender, EventArgs e)
        {
            //clear the textboxes after inserting the new record
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
          
        }

        

        //search button click
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string studentNo = textBox1.Text;

                    // Use a parameterized query to prevent SQL injection
                    string query = "SELECT * FROM StudentInformation WHERE StudentNo = @StudentNo";
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@StudentNo", studentNo);

                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Display the result in the DataGridView
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        // No records found with the specified StudentNo
                        MessageBox.Show("No records found for StudentNo: " + studentNo, "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = null; // Clear the DataGridView
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //method to display table in dataGridView
        void BindData()
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand command = new SqlCommand("Select * from StudentInformation", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        //edit student profile infomation
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand command = new SqlCommand("update StudentInformation set [Firstname] = '" + textBox2.Text + "', [Lastname] = '" + textBox3.Text + "',[Email] = '"+textBox4.Text+"'  WHERE [StudentNo] = '"+textBox1.Text+"'", con);
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Updated.");
                BindData();

                //clear the textboxes after inserting the new record
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide(); // Close the main form

            // Show or open the login form
            Form14 loginForm = new Form14();
            loginForm.ShowDialog(); // Show the login form
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide(); // Close the main form

            // Show or open the home Admin form
            Form15 homeForm = new Form15();
            homeForm.ShowDialog(); // Show the home form
        }
    }
}
