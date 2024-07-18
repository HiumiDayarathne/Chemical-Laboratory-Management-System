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
    public partial class Form1 : Form
    {
        int chemicalId, amount, minAmount;
        String chemicalName, iupacName, state, formula, expDate, hazards, boilingPoint;
        String conString = "Data Source=DESKTOP-2EC93NV\\SQLEXPRESS;Initial Catalog=\"Chemical Laboratory Management System\";Integrated Security=True";
       
    
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Insert Button click
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse input values
                chemicalId = int.Parse(textBox1.Text);
                chemicalName = textBox2.Text;
                iupacName = textBox3.Text;
                formula = textBox4.Text;
                state = textBox5.Text;
                boilingPoint = textBox6.Text;
                expDate = textBox7.Text;
                minAmount = int.Parse(textBox8.Text);
                amount = int.Parse(textBox9.Text);
                hazards = textBox10.Text;

                // Create a SQL connection and command
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    // Use parameterized query to prevent SQL injection
                    string query = "INSERT INTO Chemicals ([Chemical_ID], [Common_Name], [IUPAC_Name], [Formula], [Physical_State], [Boiling_Melting_Point], [Expiry_Date], [Minimum_Required_Amount], [Amount], [Hazards]) VALUES (@ChemicalId, @ChemicalName, @IupacName, @Formula, @State, @BoilingPoint, @ExpDate, @MinAmount, @Amount, @Hazards)";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@ChemicalId", chemicalId);
                        command.Parameters.AddWithValue("@ChemicalName", chemicalName);
                        command.Parameters.AddWithValue("@IupacName", iupacName);
                        command.Parameters.AddWithValue("@Formula", formula);
                        command.Parameters.AddWithValue("@State", state);
                        command.Parameters.AddWithValue("@BoilingPoint", boilingPoint);
                        command.Parameters.AddWithValue("@ExpDate", expDate);
                        command.Parameters.AddWithValue("@MinAmount", minAmount);
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@Hazards", hazards);

                        // Execute the query
                        command.ExecuteNonQuery();
                    }

                    con.Close();
                    BindData();
                    //clear the textboxes after inserting the new record
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox8.Clear();   
                    textBox9.Clear();
                    textBox10.Clear();
                }

                MessageBox.Show("Chemical information inserted successfully.");
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
            SqlCommand command = new SqlCommand("Select * from Chemicals", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable(); 
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }


        //Update button click, Product Id is given by the user 
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                 SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand command = new SqlCommand("update Chemicals set [Common_Name] = '" + textBox2.Text + "', [IUPAC_Name] = '" + textBox3.Text + "', [Formula] = '" + textBox4.Text + "',[Physical_State]= '" + textBox5.Text + "', [Boiling_Melting_Point] = '" + textBox6.Text + "', [Expiry_Date]= '" + textBox7.Text + "', [Minimum_Required_Amount]= '" + int.Parse(textBox8.Text) + "', [Amount]= '" + int.Parse(textBox9.Text) + "', [Hazards]= '" + textBox10.Text + "' where Chemical_ID = '" + int.Parse(textBox1.Text) + "'", con);
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Updated.");
                BindData();

                //clear the textboxes after inserting the new record
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Delete button click, the chemical id is given by the user
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    int chemicalId = int.Parse(textBox1.Text);

                    // Check if the record with the specified Chemical_ID exists
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Open();
                        string checkQuery = "SELECT COUNT(*) FROM Chemicals WHERE Chemical_ID = @Chemical_ID";
                        SqlCommand checkCommand = new SqlCommand(checkQuery, con);
                        checkCommand.Parameters.AddWithValue("@Chemical_ID", chemicalId);

                        int rowCount = (int)checkCommand.ExecuteScalar();

                        if (rowCount == 0)
                        {
                            MessageBox.Show("No record with Chemical ID " + chemicalId + " found.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (MessageBox.Show("This will permanently delete the record from your database. Are you sure?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                SqlCommand deleteCommand = new SqlCommand("DELETE FROM Chemicals WHERE Chemical_ID = @Chemical_ID", con);
                                deleteCommand.Parameters.AddWithValue("@Chemical_ID", chemicalId);
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
                MessageBox.Show("Please enter the Chemical ID");
            }
        }

        //Search a record using the product Id
        private void button4_Click(object sender, EventArgs e)
        {
            try
            { 
                SqlConnection con = new SqlConnection(conString);
                SqlCommand command = new SqlCommand("Select * from Chemicals where [Chemical_ID] = '"+int.Parse(textBox1.Text)+"'" , con);
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

        //Reset button click
        private void button5_Click(object sender, EventArgs e)
        {
            //clear the textboxes after inserting the new record
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
        }

        //log out strip menu
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide(); // Close the main form

            // Show or open the login form
            Form14 loginForm = new Form14();
            loginForm.ShowDialog(); // Show the login form
        }

        //home strip menu
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide(); // Close the main form

            // Show or open the home Admin form
            Form15 homeForm = new Form15();
            homeForm.ShowDialog(); // Show the home form
        }

    }
}
