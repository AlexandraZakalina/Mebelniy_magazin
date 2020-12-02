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
    public partial class Form5 : Form
    {
        string connectionstring = @"Data Source=DESKTOP-HHT7APO;Integrated Security=SSPI;Initial Catalog=Mebelniy_magazin";
        public string login_manager = "";
        public Form5(Form2 f2)
        {
            login_manager = f2.login;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query1 = "SELECT max(id_zakaz) FROM Zakaz";
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            SqlCommand command = new SqlCommand(query1, conn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int id = Convert.ToInt32(reader[0]);
            reader.Close();
            id++;
            string query2 = "INSERT INTO Zakaz (id_zakaz, fio_zakazchika, phone, adres, id_tovar, color, kolvo, stoimost, login) VALUES ("+id+",'"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"',"+Convert.ToInt32(comboBox1.SelectedValue)+",'"+textBox4.Text+"',"+textBox5.Text+","+textBox6.Text+",'"+login_manager+"')";
            SqlCommand command1 = new SqlCommand(query2, conn);
            command1.ExecuteNonQuery();
            MessageBox.Show("Заказ оформлен");
            conn.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.Tovar". При необходимости она может быть перемещена или удалена.
            this.tovarTableAdapter.Fill(this.dataSet1.Tovar);

        }
    }
}
