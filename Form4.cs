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
    public partial class Form4 : Form
    {
        string connectionstring = @"Data Source=DESKTOP-HHT7APO;Integrated Security=SSPI;Initial Catalog=Mebelniy_magazin";
        public string login_manager = "";
        public Form4(Form2 f2)
        {
            login_manager = f2.login;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "")
            {
                string query = "SELECT kolvo FROM Tovar WHERE id_tovar="+comboBox1.SelectedValue;
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int kolvo = Convert.ToInt32(reader[0]);
                reader.Close();
                if(kolvo<Convert.ToInt32(textBox2.Text))
                {
                    MessageBox.Show("Товара в необходимом количестве на складе нет");
                }
                else
                {
                    kolvo-=Convert.ToInt32(textBox2.Text);
                    MessageBox.Show(kolvo.ToString());
                    string query1 = "UPDATE Tovar SET kolvo ="+kolvo+" WHERE id_tovar="+Convert.ToInt32(comboBox1.SelectedValue);
                    SqlCommand command1 = new SqlCommand(query1, conn);
                    command1.ExecuteNonQuery();
                    MessageBox.Show("Товар реализован!");
                    string query2 = "SELECT max(id_realizatsiya) FROM Realizatsiya";
                    SqlCommand command2 = new SqlCommand(query2, conn);
                    SqlDataReader reader1 = command2.ExecuteReader();
                    reader1.Read();
                    int count = Convert.ToInt32(reader1[0]);
                    reader1.Close();
                    count++;
                    string query3 = "SELECT stoimost FROM Tovar WHERE id_tovar=" + comboBox1.SelectedValue;
                    SqlCommand command3 = new SqlCommand(query3, conn);
                    SqlDataReader reader2 = command3.ExecuteReader();
                    reader2.Read();
                    int sum = Convert.ToInt32(reader2[0]);
                    reader2.Close();
                    string query4 = "INSERT INTO Realizatsiya (id_realizatsiya, id_tovar, kolvo, login, sum) VALUES ("+count+","+comboBox1.SelectedValue+","+Convert.ToInt32(textBox2.Text)+",'"+login_manager+"',"+(Convert.ToInt32(textBox2.Text)*sum)+")";
                    SqlCommand command4 = new SqlCommand(query4, conn);
                    command4.ExecuteNonQuery();
                    MessageBox.Show("Информация о реализации сохранена!");
                }
                conn.Close();
                load(connectionstring, dataGridView1);
            }
            else
            {
                MessageBox.Show("Количество товаров не указано!");
            }
        }
        public void load(string connectionstring, DataGridView dataGrid)
        {
            string query = "SELECT * FROM Realizatsiya ORDER BY id_realizatsiya";
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGrid.DataSource = table;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Tovar". При необходимости она может быть перемещена или удалена.
            this.tovarTableAdapter.Fill(this.dataSet1.Tovar);
            load(connectionstring, dataGridView1);
        }
    }
}
