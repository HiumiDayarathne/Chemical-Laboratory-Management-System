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
    public partial class Form10 : Form
    {
        String conString = "Data Source=DESKTOP-2EC93NV\\SQLEXPRESS;Initial Catalog=\"Chemical Laboratory Management System\";Integrated Security=True";
        public Form10()
        {
            InitializeComponent();
        }

        //update button click
        private void button1_Click(object sender, EventArgs e)
        {
            // Get user input from textboxes
            string studentNo = textBox1.Text;
            string firstName = textBox2.Text;
            string lastName = textBox3.Text;
            string email = textBox4.Text;


            // Validate user input 
            if (string.IsNullOrWhiteSpace(studentNo) || string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a SQL connection and command
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                try
                {
                    string checkQuery = "SELECT COUNT(*) FROM StudentInformation WHERE AdminID = @studentNo";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@studentNo", studentNo);
                    // Insert the new user into the database
                    string insertQuery = "UPDATE StudentInformation (StudentNo, Firstname, Lastname, Email) VALUES (@studentNo, @FirstName, @LastName, @Email)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@studentNo", studentNo);
                    insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                    insertCommand.Parameters.AddWithValue("@LastName", lastName);
                    insertCommand.Parameters.AddWithValue("@Email", email);

                    MessageBox.Show("Update successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear the textboxes after successful update
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();

                }
                catch
                {
                    MessageBox.Show("Update failed. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


        }

        //back button click. show home student form
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // Close the main form

            // Show or open the home Admin form
            Form5 homeForm = new Form5();
            homeForm.ShowDialog(); // Show the home form
        }
    }
}
