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

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SqlConnection conn = new SqlConnection();
        private string conString = "Data Source=DMGM0349997\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
        private SqlCommand cmd;



        private void Form1_Load(object sender, EventArgs e)
        {
            refreshData();
        }
        private void handleExeption(Exception ex)
        {

            string msg = ex.Message.ToString();
            string caption = "error";

            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void refreshData()
        {


            conn.ConnectionString = conString;
            cmd= conn.CreateCommand();

            try
            {
                string query = "select * from Shippers";
                cmd.CommandText = query;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
                cmbSelect.DisplayMember = "CompanyName";
                cmbSelect.ValueMember = "ShipperID";
                cmbSelect.DataSource = dt;


                reader.Close();

                
            }

            catch(Exception ex)
            {
                handleExeption(ex);
            }

            finally
            {
                cmd.Dispose();
                conn.Close();
            }

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            string CompanyName = tbAirplane.Text;
            string Phone = tbPhone.Text;
           

            if ((CompanyName == "") || (Phone == "")  )
            {
                string msg = "No textbox can be empty";
                string caption = "Error!!";
                MessageBox.Show(msg,caption,MessageBoxButtons.OK,MessageBoxIcon.Error);

            }

            else
            {
                conn.ConnectionString = conString;
                cmd = conn.CreateCommand();
                try
                {
                    string query = "Insert into Shippers values('" + CompanyName + "','" + Phone + "');";


                    cmd.CommandText = query;
                    conn.Open();
                    cmd.ExecuteScalar();

                    MessageBox.Show("Added successfully");

                }

                catch (Exception ex)
                {
                    handleExeption(ex);
                }

                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // delete from shippers  where ID=???

            int flID = Convert.ToInt32(cmbSelect.SelectedValue);
            conn.ConnectionString = conString;
            cmd = conn.CreateCommand();
            try
            {
                string query = "Delete from Shippers where ShipperID="+flID;
                cmd.CommandText = query;
                conn.Open();
                cmd.ExecuteScalar();

            }
            catch(Exception ex)
            {
                handleExeption(ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }
}
