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

namespace CLMS_Test
{
    public partial class Form8 : Form
    {
        String conString = "Data Source=DESKTOP-2EC93NV\\SQLEXPRESS;Initial Catalog=\"Chemical Laboratory Management System\";Integrated Security=True";
        public Form8()
        {
            InitializeComponent();
        }

        //Register Button Click
        private void button1_Click(object sender, EventArgs e)
        {
            
            // Get user input from textboxes
            string studentNo = textBox1.Text;
            string password = textBox5.Text;
            string firstName = textBox2.Text;
            string lastName = textBox3.Text;
            string email = textBox4.Text;
            string reenterPassword = textBox6.Text;

            // Validate user input 
            if (string.IsNullOrWhiteSpace(studentNo) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check if password match
            if (password != reenterPassword)
            {
                MessageBox.Show("Passwords do not match. Please reenter passwords.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox6.Clear();
                textBox5.Clear();
                return;
            }

            // Create a SQL connection and command
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                // Password validation
                if (!IsPasswordValid(password))
                {
                    MessageBox.Show("Password must be between 8 and 12 characters.", "Password Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool IsPasswordValid(String pwd)
                {
                    // Define a regular expression pattern to match passwords with 8 to 12 characters
                    string pattern = @"^.{8,12}$"; // This pattern matches any character (.) between 8 and 12 times.

                    // Use Regex.IsMatch to check if the password matches the pattern
                    return Regex.IsMatch(pwd, pattern);
                }

                // Check if the studentNo already exists
                string checkQuery = "SELECT COUNT(*) FROM StudentInformation WHERE StudentNo = @StudentNo";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@StudentNo", studentNo);

                int userCount = (int)checkCommand.ExecuteScalar();

                if (userCount > 0)
                {
                    MessageBox.Show("StudentNo already exists. Please choose a different one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Insert the new user into the database
                    string insertQuery = "INSERT INTO StudentInformation (StudentNo, Password, Firstname, Lastname, Email) VALUES (@StudentNo, @Password, @FirstName, @LastName, @Email)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@StudentNo", studentNo);
                    insertCommand.Parameters.AddWithValue("@Password", password);
                    insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                    insertCommand.Parameters.AddWithValue("@LastName", lastName);
                    insertCommand.Parameters.AddWithValue("@Email", email);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear the textboxes after successful registration
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                        textBox6.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            
            }
        }

        // Back Button click: Load Log in interface of the student
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // Close the main form

            // Show or open the login form
            Form13 loginForm = new Form13();
            loginForm.ShowDialog(); // Show the login form
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
