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
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source = DESKTOP-HER31GU; Initial Catalog = Registration; Integrated Security=True;";
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "Введите почту";
            textBox1.ForeColor = Color.Gray;
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
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register reg= new Register();
            reg.Show();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Coral;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.DarkSalmon;
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Введите почту")
                textBox1.Text = "";
            textBox1.ForeColor = Color.Coral;
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
                textBox1.Text = "Введите почту";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From tblUser where Address='" + textBox1.Text + "' and Password='" + textBox2.Text + "'", connectionString);
            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-HER31GU; Initial Catalog = Registration; Integrated Security=True;");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Username from tblUser where Address=@Address", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                Main main = new Main();
                Register register = new Register();
                main.label2.Hide();
                main.label1.Hide();
                main.label3.Show();
                main.label4.Show();
                cmd.Parameters.AddWithValue("@Address", textBox1.Text);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                { main.label4.Text = da.GetValue(0).ToString(); }
                main.Show();
            }
            else
            {
                MessageBox.Show("Please check your Adress and Password");
            }
        }
    }
}
