using Microsoft.Reporting.WinForms;
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
    public partial class Form6 : Form
    {
        String conString = "Data Source=DESKTOP-2EC93NV\\SQLEXPRESS;Initial Catalog=\"Chemical Laboratory Management System\";Integrated Security=True";
        public Form6()
        {
            InitializeComponent();
        }

        //View report button click
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    string query = "SELECT * FROM Chemicals WHERE Amount <= Minimum_Required_Amount";

                    SqlCommand command = new SqlCommand(query, con);

                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);

                    // Clear the reportViewer initially
                    reportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource source = new ReportDataSource("DataSet1", dt);
                    reportViewer1.LocalReport.ReportPath = "Report2.rdlc";
                    reportViewer1.LocalReport.DataSources.Add(source);
                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
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
