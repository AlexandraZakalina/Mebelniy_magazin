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
    
    public partial class Form7 : Form
    {
        string query1 = "SELECT * FROM Tovar";
        string connectionstring = @"Data Source=DESKTOP-HHT7APO;Integrated Security=SSPI;Initial Catalog=Mebelniy_magazin";
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet11.Kategoriya". При необходимости она может быть перемещена или удалена.
            this.kategoriyaTableAdapter.Fill(this.dataSet11.Kategoriya);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Tovar". При необходимости она может быть перемещена или удалена.
            this.tovarTableAdapter.Fill(this.dataSet1.Tovar);
            load(connectionstring, dataGridView1, query1);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query2 = "UPDATE Tovar SET ";
            if(textBox1.Text != "")
            {
                query2 += "name = '" + textBox1.Text + "',";
            }
            if(Convert.ToString(comboBox2.SelectedValue) != "")
            {
                query2 += "kategoriya = " + Convert.ToInt32(comboBox2.SelectedValue) + ",";
            }
            if(textBox2.Text != "")
            {
                query2 += "kolvo = "+Convert.ToInt32(textBox2.Text)+",";
            }
            if(textBox3.Text != "")
            {
                query2 += "stoimost = "+Convert.ToInt32(textBox3.Text)+",";
            }
            string query22 = "";
            for(int i=0; i<query2.Length-1;i++)
            {
                query22 += query2[i];
            }
            query22 += " WHERE id_tovar = "+Convert.ToInt32(textBox4.Text);
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            SqlCommand command = new SqlCommand(query22, conn);
            command.ExecuteNonQuery();
            MessageBox.Show("Запись изменена!");
            conn.Close();
            load(connectionstring, dataGridView1, query1);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
             query1 = "SELECT * FROM Tovar WHERE kategoriya=" + Convert.ToInt32(comboBox1.SelectedValue);
            load(connectionstring, dataGridView1, query1);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            string query2 = "SELECT max(id_tovar) FROM Tovar";
            int id = 0;
            conn.Open();
            SqlCommand command1 = new SqlCommand(query2,conn);
            SqlDataReader reader1 = command1.ExecuteReader();
            reader1.Read();
            id = Convert.ToInt32(reader1[0]);
            reader1.Close();
            id++;
	        string query3 = "INSERT INTO Tovar(id_tovar, name, kategoriya, kolvo, stoimost) VALUES ("+id+",'"+textBox8.Text+"',"+comboBox3.SelectedValue+","+textBox7.Text+","+textBox6.Text+")";
            SqlCommand command2 = new SqlCommand(query3, conn);
            command2.ExecuteNonQuery();
            MessageBox.Show("Товар записан");
            conn.Close();
            load(connectionstring,dataGridView1,query1);
        }
    }
}
