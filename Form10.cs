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
    public partial class Form10 : Form
    {
        string connectionstring = @"Data Source=DESKTOP-HHT7APO;Integrated Security=SSPI;Initial Catalog=Mebelniy_magazin";
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Kategoriya". При необходимости она может быть перемещена или удалена.
            this.kategoriyaTableAdapter.Fill(this.dataSet1.Kategoriya);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Tovar". При необходимости она может быть перемещена или удалена.
            this.tovarTableAdapter.Fill(this.dataSet1.Tovar);
            load(connectionstring, dataGridView1, "SELECT * FROM Tovar");
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

        private void button1_Click(object sender, EventArgs e)
        {
            load(connectionstring, dataGridView1, "SELECT * FROM Tovar");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load(connectionstring, dataGridView1, "SELECT * FROM Tovar WHERE kategoriya=" + comboBox1.SelectedValue);
        }
    }
}
