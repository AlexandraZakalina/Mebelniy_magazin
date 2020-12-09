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

namespace IS17ZakalinaKPR
{
    public partial class Form11 : Form
    {
        string connectionstring = @"Data Source=DESKTOP-HHT7APO;Integrated Security=SSPI;Initial Catalog=Mebelniy_magazin";
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            load(connectionstring, dataGridView1, "SELECT * FROM Prihod");
        }

        public void load(string connectionstring, DataGridView dataGrid, string query)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGrid.DataSource = table;
        }
    }
}
