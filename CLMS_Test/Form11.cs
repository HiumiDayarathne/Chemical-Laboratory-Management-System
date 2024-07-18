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
    public partial class Form11 : Form
    {
        int quantity;
        String equipmentName, equipmentId;
        String conString = "Data Source=DESKTOP-2EC93NV\\SQLEXPRESS;Initial Catalog=\"Chemical Laboratory Management System\";Integrated Security=True";
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            //calling method to display dataGridView when form is loading
            BindData();
            comboBox1.SelectedIndex = 0;
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

       

        //Search a record using the product Id or the common name
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // search by chemical ID
                if (comboBox1.SelectedIndex == 1)
                {
                    SqlConnection con = new SqlConnection(conString);
                    SqlCommand command = new SqlCommand("Select * from Equipments where [EquipmentID] = '" + int.Parse(textBox1.Text) + "'", con);
                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    dataGridView1.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No match found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dataGridView1.DataSource = dt;
                    }

                }
                else if (comboBox1.SelectedIndex == 0) //search by common name
                {
                    SqlConnection con = new SqlConnection(conString);
                    SqlCommand command = new SqlCommand("Select * from Equipments where [EquipmentName] = '" + textBox1.Text + "'", con);
                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    dataGridView1.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No match found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No match found" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide(); // Close the main form

            // Show or open the login form
            Form13 loginForm = new Form13();
            loginForm.ShowDialog(); // Show the login form
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide(); // Close the main form

            // Show or open the home Admin form
            Form5 homeForm = new Form5();
            homeForm.ShowDialog(); // Show the home form
        }

        //reset button click
        private void button1_Click(object sender, EventArgs e)
        {
            //call the method to display dataGridView again
            BindData();
            comboBox1.SelectedIndex = 0;
            textBox1.Clear();
        }
    }
}
