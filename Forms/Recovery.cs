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
    public partial class Recovery : Form
    {
        #region Connect
        public static SqlConnection conn;
        string connectionString = String.Format($"Server = {DataBank.Server}; Port = {DataBank.Port}; User Id = {DataBank.User_id}; Password = {DataBank.Password}; Database = {DataBank.DB}");
        #endregion
        Code code;
        Form1 menu;
        public Recovery()
        {
            KeyPreview=true;
            KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button2_Click_1(button2,null); };
            KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button4_Click(button4, null); };

            DataBank.conn = new Npgsql.NpgsqlConnection(connectionString);
            DataBank.conn.Open();
            InitializeComponent();
            GlobalUpdate();
            textBox1.BackColor = Color.FromArgb(40, 40, 40);
            label4.Visible = false;
        }
        public void GlobalUpdate()
        {
            update();
        }
        private void update()
        {
            //show

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

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0) { label4.Visible = true; }
            else
            {
                try
                {
                    if (DataBank.conn.State == ConnectionState.Open)
                    {
                        //if conn open
                        //Проверка на существование mail
                        string zap = $"SELECT * FROM tbl_users WHERE email ='{textBox1.Text}'";
                        DataBank.cmd = new NpgsqlCommand(zap, DataBank.conn);
                        NpgsqlDataReader reader = DataBank.cmd.ExecuteReader();
                        if (reader.Read()==true)
                        {
                            //yes
                            reader.Close();
                            Random rnd = new Random();
                            DataBank.i = rnd.Next(1000, 9999);
                            MimeMessage message = new MimeMessage();
                            message.From.Add(new MailboxAddress("Skeet", $"{DataBank.MyMail}")); //отправитель сообщения
                            message.To.Add(new MailboxAddress("Roma", textBox1.Text)); //адресат сообщения
                            message.Subject = "Востановление пароля"; //тема сообщения
                            message.Body = new BodyBuilder() { HtmlBody = $"<div style=\"color: green;\">Здравствуйте!<br>Это ваш код безопасности для востановления пароля учетной записи:<br>{DataBank.i}<br>Он необходим для подтверждения, что владельцем учетной записи являетесь именно вы.</div>" }.ToMessageBody();

                            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                            {
                                client.Connect("smtp.mail.ru", DataBank.MyPort, true); //либо использум порт 465
                                client.Authenticate($"{DataBank.MyMail}", $"{DataBank.MyPass}"); //логин-пароль от аккаунта
                                client.Send(message);

                                client.Disconnect(true);
                            }
                            DataBank.Mail=textBox1.Text;
                            DataBank.conn.Close();
                            this.Close();
                            code = new Code();
                            code.Show();
                        }
                        else
                        {
                            //nope
                            reader.Close();
                            label4.Visible = true;
                        }
                    }
                    else
                    {
                        // if conn close
                        DataBank.conn.Open();
                        string zap = $"SELECT * FROM tbl_users WHERE email ='{textBox1.Text}'";
                        DataBank.cmd = new NpgsqlCommand(zap, DataBank.conn);
                        NpgsqlDataReader reader = DataBank.cmd.ExecuteReader();
                        if (reader.Read()==true)
                        {
                            //yes
                            reader.Close();
                            Random rnd = new Random();
                            DataBank.i = rnd.Next(1000, 9999);
                            MimeMessage message = new MimeMessage();
                            message.From.Add(new MailboxAddress("Skeet", $"{DataBank.MyMail}")); //отправитель сообщения
                            message.To.Add(new MailboxAddress("Roma", textBox1.Text)); //адресат сообщения
                            message.Subject = "Востановление пароля"; //тема сообщения
                            message.Body = new BodyBuilder() { HtmlBody = $"<div style=\"color: green;\">Здравствуйте!<br>Это ваш код безопасности для востановления пароля учетной записи:<br>{DataBank.i}<br>Он необходим для подтверждения, что владельцем учетной записи являетесь именно вы.</div>" }.ToMessageBody();

                            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                            {
                                client.Connect("smtp.mail.ru", DataBank.MyPort, true); //либо использум порт 465
                                client.Authenticate($"{DataBank.MyMail}", $"{DataBank.MyPass}"); //логин-пароль от аккаунта
                                client.Send(message);

                                client.Disconnect(true);
                            }
                            DataBank.Mail=textBox1.Text;
                            DataBank.conn.Close();
                            this.Close();
                            code = new Code();
                            code.Show();
                        }
                        else
                        {
                            reader.Close();
                            label4.Visible = true;
                        }
                    }
                }
                catch
                {
                    label4.Visible = true;
                }
            }
        }

        
        

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Авторизация";
            button4.Visible = true;
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            menu = new Form1();
            menu.Show();
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

        private void button4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button4, "Отправить код востановления");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox1, "Введите почту");
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button2, "Вернуться на прошлую страницу");
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button3, "Закрыть");
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(label4, "Вы неправильно ввели почту, попробуйте ещё раз");
        }
    }
}