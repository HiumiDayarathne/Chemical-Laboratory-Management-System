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
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CLMS_Test
{
    public partial class Form2 : Form
    {
        int equipmentId, quantity;
        String equipmentName;
        String conString = "Data Source=DESKTOP-2EC93NV\\SQLEXPRESS;Initial Catalog=\"Chemical Laboratory Management System\";Integrated Security=True";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            BindData();
        }


        //method to display table in dataGridView
        void BindData()
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand command = new SqlCommand("Select * from Equipments", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        

        //Insert button click
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse input values
                equipmentId = int.Parse(textBox1.Text);
                equipmentName = textBox2.Text;
                quantity = int.Parse(textBox3.Text);
                

                // Create a SQL connection and command
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    // Use parameterized query to prevent SQL injection
                    string query = "INSERT INTO Equipments ([EquipmentID], [EquipmentName], [Quantity]) VALUES (@EquipmentId, @EquipmentName, @Quantity)";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@EquipmentId", equipmentId);
                        command.Parameters.AddWithValue("@EquipmentName", equipmentName);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        
                        // Execute the query
                        command.ExecuteNonQuery();
                    }

                    con.Close();
                    BindData();
                    //clear the textboxes after inserting the new record
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                  
                }

                MessageBox.Show("Equipment information inserted successfully.");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        //update button click
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand command = new SqlCommand("update Equipments set [EquipmentName] = '" + textBox2.Text + "', [Quantity]= '" + int.Parse(textBox3.Text) + "' where EquipmentID = '" + int.Parse(textBox1.Text) + "'", con);
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Updated.");
                BindData();

                //clear the textboxes after inserting the new record
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //delete button click
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    int equipmentId = int.Parse(textBox1.Text);

                    // Check if the record with the specified EquipmentID exists
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Open();
                        string checkQuery = "SELECT COUNT(*) FROM Equipments WHERE EquipmentID = @EquipmentID";
                        SqlCommand checkCommand = new SqlCommand(checkQuery, con);
                        checkCommand.Parameters.AddWithValue("@EquipmentID", equipmentId);

                        int rowCount = (int)checkCommand.ExecuteScalar();

                        if (rowCount == 0)
                        {
                            MessageBox.Show("No record with Equipment ID " + equipmentId + " found.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (MessageBox.Show("This will permanently delete the record from your database. Are you sure?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                SqlCommand deleteCommand = new SqlCommand("DELETE FROM Equipments WHERE EquipmentID = @EquipmentID", con);
                                deleteCommand.Parameters.AddWithValue("@EquipmentID", equipmentId);
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
                MessageBox.Show("Please enter the Equipment ID");
            }
        }


        //Search a record using the equipment Id
        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                SqlCommand command = new SqlCommand("Select * from Equipments where [EquipmentID] = '" + int.Parse(textBox1.Text) + "'", con);
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide(); // Close the main form

            // Show or open the home Admin form
            Form15 homeForm = new Form15();
            homeForm.ShowDialog(); // Show the home form
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide(); // Close the main form

            // Show or open the login form
            Form14 loginForm = new Form14();
            loginForm.ShowDialog(); // Show the login form
        }

        //Reset button click
        private void button5_Click_1(object sender, EventArgs e)
        {
            //clear the textboxes after inserting the new record
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }


    }
}
