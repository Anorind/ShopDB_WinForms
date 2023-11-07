using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace ShopDB_WinForms
{

    public partial class Form1 : Form
    {
        static string conn = ConfigurationManager.ConnectionStrings["ShopConn"].ConnectionString;
        private DataSet dataSet = new DataSet();

        public Form1()
        {
            InitializeComponent();
            LoadTableNames();
        }

        private void LoadTableNames()
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                DataTable schema = connection.GetSchema("Tables");
                foreach (DataRow row in schema.Rows)
                {
                    comboBox1.Items.Add(row[2].ToString());
                }
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            string tableName = comboBox1.SelectedItem.ToString();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                string query = "SELECT * FROM " + tableName;
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                if (dataSet.Tables.Contains(tableName))
                {
                    dataSet.Tables[tableName].Clear();
                }
                adapter.Fill(dataSet, tableName);
            }
            dataGridView1.DataSource = dataSet.Tables[tableName];
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
