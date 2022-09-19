using MimeKit;
using Npgsql;
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

namespace Forms
{
public partial class Code : Form
    {
        #region Connect
        public static SqlConnection conn;
        string connectionString = String.Format($"Server = {DataBank.Server}; Port = {DataBank.Port}; User Id = {DataBank.User_id}; Password = {DataBank.Password}; Database = {DataBank.DB}");
        #endregion
        //Временно
        public void GlobalUpdate()
        {
            update();
        }
        private void update()
        {
            DataSet dataSetMain = new DataSet();
            dataSetMain.Clear();
            DataBank.cmd = new Npgsql.NpgsqlCommand("select * from tbl_users", DataBank.conn);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(DataBank.cmd);
            adapter.Fill(dataSetMain, "table1");
            dataSetMain.Tables[0].Columns[0].ColumnName = "id";
            dataSetMain.Tables[0].Columns[1].ColumnName = "Login";
            dataSetMain.Tables[0].Columns[2].ColumnName = "Password";
            dataSetMain.Tables[0].Columns[3].ColumnName = "Email";
        }
        Recovery rec;
        regnewpass Regnewpass;
        public Code()
        {
            KeyPreview=true;
            KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button5_Click(button5,null); };
            KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button1_Click_1(button1, null); };
            DataBank.conn = new Npgsql.NpgsqlConnection(connectionString);
            DataBank.conn.Open();
            InitializeComponent();
            GlobalUpdate();
            textBox1.BackColor = Color.FromArgb(40, 40, 40);
            label4.Visible = false;
        }

        private void Recovery_Load(object sender, EventArgs e)
        {

            this.BackColor = Color.FromArgb(18, 18, 18);
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

        
        

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Авторизация";
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

        private void Recovery_Load_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Востановить кнопка 2
            if (textBox1.Text == Convert.ToString(DataBank.i)) 
            {
                this.Close();
                Regnewpass = new regnewpass();
                Regnewpass.Show();
            }
            else { label4.Visible = true;}
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            rec = new Recovery();
            rec.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (DataBank.conn.State == ConnectionState.Open)
            {
                DataBank.cmd = new NpgsqlCommand($@"UPDATE tbl_users SET password='{textBox1.Text}' WHERE email like '{DataBank.Mail}'", DataBank.conn);
                DataBank.cmd.ExecuteNonQuery();
                DataBank.conn.Close();
                GlobalUpdate();
                this.Hide();
                Form1 menu;
                this.Close();
                menu = new Form1();
                menu.Show();
            }
            else
            {
                DataBank.conn.Open();
                DataBank.cmd = new NpgsqlCommand($@"UPDATE tbl_users SET password='{textBox1.Text}' WHERE email like '{DataBank.Mail}'", DataBank.conn);
                DataBank.cmd.ExecuteNonQuery();
                DataBank.conn.Close();
                GlobalUpdate();
                this.Hide();
                Form1 menu;
                this.Close();
                menu = new Form1();
                menu.Show();
            }
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button5, "Вернуться на прошлую страницу");
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button3, "Закрыть");
        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox1, "Введите код отправленный вам на почту");
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(label4, "Неправильно введён код");
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button1, "Востановить пароль");
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}