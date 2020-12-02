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
    public partial class Form8 : Form
    {
        string connectionstring = @"Data Source=DESKTOP-HHT7APO;Integrated Security=SSPI;Initial Catalog=Mebelniy_magazin";
        public Form8()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query1 = "UPDATE Polzovatel SET ";
            if(textBox2.Text != "")
            {
                query1 += "password ='"+textBox2.Text+"',";
            }
            if(Convert.ToString(comboBox1.SelectedValue) != "")
            {
                query1 += "doljnost = '"+comboBox1.SelectedValue+"',";
            }
            if(textBox3.Text != "")
            {
                query1 += "familiya = '"+textBox3.Text+"',";
            }
		if(textBox4.Text != "")
            {
                query1 += "imya = '"+textBox4.Text+"',";
            }
		if(textBox5.Text != "")
            {
                query1 += "otchestvo = '"+textBox5.Text+"',";
            }
            string query11 = "";
            for(int i=0;i<query1.Length-1;i++)
            {
                query11 += query1[i];
            }
            query11+= " WHERE login ='"+textBox1.Text+"'";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            SqlCommand command1 = new SqlCommand(query11,conn);
            command1.ExecuteNonQuery();
            MessageBox.Show("Пользователь изменен!");
            conn.Close();
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

        private void Form8_Load(object sender, EventArgs e)
        {
            load(connectionstring, dataGridView1, "SELECT * FROM Polzovatel");
        }
    }
}
