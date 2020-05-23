using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Register : Form
    {
        string connectionString = @"Data Source = DESKTOP-HER31GU; Initial Catalog = Registration; Integrated Security=True;";
        public Register()
        {
            InitializeComponent();
            textBox1.Text = "Введите никнейм";
            textBox1.ForeColor = Color.Gray;
            textBox3.Text = "Введите почту";
            textBox3.ForeColor = Color.Gray;
            textBox2.Text = "Введите пароль";
            textBox2.ForeColor = Color.Gray;
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.Show();
        }

        private void Closebutton_MouseEnter(object sender, EventArgs e)
        {
            Closebutton.ForeColor = Color.Red;
        }

        private void Closebutton_MouseLeave(object sender, EventArgs e)
        {
            Closebutton.ForeColor = Color.Coral;
        }

        Point lastpoint;
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Coral;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.DarkSalmon;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Введите никнейм")
                textBox1.Text = "";
            textBox1.ForeColor = Color.Coral;
            if (textBox3.Text == "")
            {
                textBox3.Text = "Введите почту";
                textBox3.ForeColor = Color.Gray;
            }
            if (textBox2.Text == "")
            {
                textBox2.UseSystemPasswordChar = false;
                textBox2.Text = "Введите пароль";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if(textBox3.Text == "Введите почту")
                textBox3.Text = "";
            textBox3.ForeColor = Color.Coral;
            if (textBox1.Text == "")
            {
                textBox1.Text = "Введите никнейм";
                textBox1.ForeColor = Color.Gray;
            }
            if (textBox2.Text == "")
            {
                textBox2.UseSystemPasswordChar = false;
                textBox2.Text = "Введите пароль";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "Введите пароль")
                textBox2.Text = "";
            textBox2.UseSystemPasswordChar = true;
            textBox2.ForeColor = Color.Coral;
            if (textBox1.Text == "")
            {
                textBox1.Text = "Введите никнейм";
                textBox1.ForeColor = Color.Gray;
            }
            if (textBox3.Text == "")
            {
                textBox3.Text = "Введите почту";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                MessageBox.Show("Write something...");
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From tblUser where Username='" + textBox1.Text + "' or Address='" + textBox3.Text + "'", connectionString);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() != "0")
                    {
                        MessageBox.Show("Такие адрес или имя уже существуют!");
                    }
                    else
                    {
                        SqlCommand sqlCmd = new SqlCommand("useraddd", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@Username", textBox1.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Address", textBox3.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Password", textBox2.Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        Clear();
                        this.Hide();
                        Form1 form1 = new Form1();
                        form1.Show();
                    }
                }
            }
        }
        void Clear()
        {
            textBox1.Text = textBox2.Text = "";
        }
    }
}
