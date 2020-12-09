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
    public partial class Form13 : Form
    {
        string connectionstring = @"Data Source=DESKTOP-HHT7APO;Integrated Security=SSPI;Initial Catalog=Mebelniy_magazin";
        public Form13()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query1 = "INSERT INTO Polzovatel (login, password, familiya, imya, otchestvo, doljnost) VALUES ('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+textBox4.Text+"','"+textBox5.Text+"','"+comboBox1.SelectedValue+"')";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            SqlCommand command = new SqlCommand(query1, conn);
            command.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Пользователь добавлен!");
            load(connectionstring, dataGridView1, "SELECT * FROM Polzovatel");
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            load(connectionstring, dataGridView1, "SELECT * FROM Polzovatel");
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

        private void button2_Click(object sender, EventArgs e)
        {
            string query1 = "DELETE FROM Polzovatel WHERE login='"+textBox10.Text+"'";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            SqlCommand command = new SqlCommand(query1, conn);
            command.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Пользователь удален!");
            load(connectionstring, dataGridView1, "SELECT * FROM Polzovatel");
        }
    }
}
