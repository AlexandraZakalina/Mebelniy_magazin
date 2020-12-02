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
    public partial class Form3 : Form
    {
        string connectionstring = @"Data Source=DESKTOP-HHT7APO;Integrated Security=SSPI;Initial Catalog=Mebelniy_magazin";
        public string login_manager = "";
        
        public Form3(Form2 f2)
        {
            login_manager = f2.login;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Tovar". При необходимости она может быть перемещена или удалена.
            this.tovarTableAdapter.Fill(this.dataSet1.Tovar);
            load(connectionstring, dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != "")
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                string query = "SELECT count(*) FROM Prihod";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int count = Convert.ToInt32(reader[0]);
                reader.Close();
                count++;
                connection.Close();
                string query1 = "INSERT INTO Prihod (id_prihod, id_tovar, postavshik, kolvo, login) VALUES (" + count + "," + Convert.ToInt32(comboBox1.SelectedValue) + ",'" + textBox1.Text + "'," + textBox2.Text + ",'"+login_manager+"')";
                string query3 = "SELECT kolvo FROM Tovar where id_tovar="+Convert.ToInt32(comboBox1.SelectedValue);
                
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    conn.Open();
                    SqlCommand command1 = new SqlCommand(query1, conn);
                    command1.ExecuteNonQuery();
                    MessageBox.Show("Запись прошла успешно!");
                    load(connectionstring, dataGridView1);
                    conn.Close();

                    conn.Open();
                    SqlCommand command2 = new SqlCommand(query3, conn);
                    SqlDataReader reader1 = command2.ExecuteReader();
                    reader1.Read();
                    int kolvo = Convert.ToInt32(reader1[0]);
                    reader1.Close();
                    conn.Close();

                    conn.Open();
                    string query2 = "UPDATE Tovar SET kolvo ="+(kolvo+Convert.ToInt32(textBox2.Text))+" WHERE id_tovar="+Convert.ToInt32(comboBox1.SelectedValue);
                    SqlCommand command3 = new SqlCommand(query2, conn);
                    command3.ExecuteNonQuery();
                    MessageBox.Show("Кол-во товаров на складе изменено!");
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Не все поля были заполнены!");
            }
        }
        public void load(string connectionstring, DataGridView dataGrid)
        {
            string query = "SELECT * FROM Prihod ORDER BY id_prihod";
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGrid.DataSource = table;
        }
    }
}
