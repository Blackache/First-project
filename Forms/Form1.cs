using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace Forms
{
public partial class Form1 : Form
    {
        TestFormsN1 reg;
        Recovery rec;
        public Form1()
        {
            KeyPreview=true;
            KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button5_Click(button5, null); };
            InitializeComponent();
        }
        #region Connect
        private NpgsqlConnection conn;
        string connectionString = String.Format($"Server = {DataBank.Server}; Port = {DataBank.Port}; User Id = {DataBank.User_id}; Password = {DataBank.Password}; Database = {DataBank.DB}");
        private NpgsqlCommand cmd;
        private string sql = null;
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip tooltp= new ToolTip();
            tooltp.SetToolTip(button5, "Войти");
            tooltp.SetToolTip(button1, "Востановление пароля");
            tooltp.SetToolTip(button4, "Регистрация");
            tooltp.SetToolTip(button3, "Закрыть");
            tooltp.SetToolTip(textBox1, "Введите логин");
            tooltp.SetToolTip(textBox2, "Введите пароль");



            conn = new NpgsqlConnection(connectionString);
            this.BackColor = Color.FromArgb(18, 18, 18);
            textBox1.BackColor = Color.FromArgb(40, 40, 40);
            textBox2.BackColor = Color.FromArgb(40, 40, 40);
            label4.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            reg = new TestFormsN1();
            reg.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Проверка на логин, пароль
            try
            {
                conn.Open();
                sql = @"select * from u_login(:_username,:_password)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_username", textBox1.Text);
                cmd.Parameters.AddWithValue("_password", textBox2.Text); 
                int result = (int)cmd.ExecuteScalar();
                conn.Close();
                if (result == 1)
                {
                    this.Hide();
                    Show sh = new Show();
                    sh.Show();
                }
                else
                {
                    label4.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Авторизация";
            button4.Visible = true;
            button5.Visible = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            rec = new Recovery();
            rec.Show();
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Show sh = new Show();
            sh.Show();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void button4_MouseHover(object sender, EventArgs e)
        {

        }

        private void button3_MouseHover(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }
    }
}
