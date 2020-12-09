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
    public partial class Form1 : Form
    {
        string connectionstring = @"Data Source=DESKTOP-HHT7APO;Integrated Security=SSPI;Initial Catalog=Mebelniy_magazin";
        public string login = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login = textBox1.Text;
            string password = textBox2.Text;
            string query = "SELECT count(*) FROM Polzovatel WHERE login='" + login + "' AND password='" + password + "'";

            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            reader.Read();
            int count = Convert.ToInt32(reader[0]);
            reader.Close();
            if(count>0)
            {
                string query1 = "SELECT doljnost FROM Polzovatel WHERE login='" + login + "' AND password='" + password + "'";
                SqlCommand command1 = new SqlCommand(query1, conn);
                SqlDataReader reader1 = command1.ExecuteReader();
                List<string[]> data1 = new List<string[]>();
                reader1.Read();
                string output1 = Convert.ToString(reader1[0]);
                reader1.Close();
                string output = "";
                foreach (char a in output1)
                {
                    if(a!=' ')
                    {
                        output += a;
                    }
                }
                MessageBox.Show("Вы вошли как "+login+". Добро пожаловать!");
                if(output=="manager")
                {
                    Form2 f2 = new Form2(this);
                    f2.Show();
                }
                else
                {
                    if (output == "administrator")
                    {
                        Form6 f6 = new Form6();
                        f6.Show();
                    }
                    else
                    {
                        if(output=="director")
                        {
                            Form9 f9 = new Form9();
                            f9.Show();
                        }
                        else
                            MessageBox.Show("Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Неверно введен логин или пароль");
            }
        }
    }
}
