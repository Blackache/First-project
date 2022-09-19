using MimeKit;
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

using System.IO;

namespace Forms
{
    public partial class TestFormsN1 : Form
    {
        Form1 menu;
        #region Connect
        string connectionString = String.Format($"Server = {DataBank.Server}; Port = {DataBank.Port}; User Id = {DataBank.User_id}; Password = {DataBank.Password}; Database = {DataBank.DB}");
        #endregion
        public TestFormsN1()
        {
            KeyPreview=true;
            KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button3_Click(button3, null); };
            KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button6_Click(button6, null); };

            DataBank.conn = new Npgsql.NpgsqlConnection(connectionString);
            DataBank.conn.Open();
            InitializeComponent();
            textBox3.BackColor = Color.FromArgb(40, 40, 40);
        }
        public void Mail()
        {
            update();
        }
        private void update()
        {
            try
            {
                MimeMessage message = new MimeMessage(); 
                message.From.Add(new MailboxAddress("Skeet", $"{DataBank.MyMail}"));
                message.To.Add(new MailboxAddress("Roma", textBox3.Text));
                message.Subject = "Вы успешно прошли регистрацию на проекте Skeet";
                message.Body = new BodyBuilder() { HtmlBody = $"<div style=\"color: green;\">Здравствуйте!<br>Поздравляем вас с успешной регистрацией на нашем проекте Skeet<br>Мы будем очень рады если вы поможете нам в улучшении качества нашего продукта оставляя честные отзывы на нашем сайте pornhub.com<br>Мы будем стараться для вас с уважением rosa </div>" }.ToMessageBody();

                using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.mail.ru", DataBank.MyPort, true);
                    client.Authenticate($"{DataBank.MyMail}", $"{DataBank.MyPass}");
                    client.Send(message);

                    client.Disconnect(true);
                }
                DataBank.cmd = new NpgsqlCommand("insert into tbl_users(username,password,email) values(:_username,:_password,:_email)", DataBank.conn);
                DataBank.cmd.Parameters.Add(new NpgsqlParameter("_username", NpgsqlDbType.Varchar));
                DataBank.cmd.Parameters.Add(new NpgsqlParameter("_password", NpgsqlDbType.Varchar));
                DataBank.cmd.Parameters.Add(new NpgsqlParameter("_email", NpgsqlDbType.Varchar));
                DataBank.cmd.Parameters[0].Value = textBox1.Text;
                DataBank.cmd.Parameters[1].Value = textBox2.Text;
                DataBank.cmd.Parameters[2].Value = textBox3.Text;
                DataBank.cmd.ExecuteScalar();
                DataBank.conn.Close();
                this.Close();
                Form1 avt = new Form1();
                avt.Show();
            }
            catch
            {
                label4.Visible= true;
            }
        }

        private void TestFormsN1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(18, 18, 18);
            textBox1.BackColor = Color.FromArgb(40, 40, 40);
            textBox2.BackColor = Color.FromArgb(40, 40, 40);
            label4.Visible = false;
            button1.Visible = false;
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
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0)
            {
                label4.Visible = true;
            }
            else if (textBox2.Text.Length <= 0)
            {
                label4.Visible = true;
            }
            else if (textBox3.Text.Length <= 0)
            {
                label4.Visible = true;
            }
            else
            {
                try
                {
                    if(DataBank.conn.State == ConnectionState.Closed) { DataBank.conn.Open(); }
                    string zap = $"SELECT * FROM tbl_users WHERE email ='{textBox3.Text}'";
                    DataBank.cmd = new NpgsqlCommand(zap, DataBank.conn);
                    NpgsqlDataReader reader = DataBank.cmd.ExecuteReader();

                    if (reader.Read()==true)
                    {
                        reader.Close();
                        label4.Location = new Point(106, 215);
                        label4.Text = "Почта уже зарегистрирована";
                        label4.Visible= true;
                        DataBank.conn.Close();
                    }
                    else
                    {
                        reader.Close();
                        string zap3 = $"SELECT * FROM tbl_users WHERE username ='{textBox1.Text}'";
                        DataBank.cmd = new NpgsqlCommand(zap3, DataBank.conn);
                        NpgsqlDataReader reader3 = DataBank.cmd.ExecuteReader();
                        if (reader3.Read()==false)
                        {
                            reader3.Close();
                            Mail();
                        }
                        else { reader3.Close(); }
                    }
                    string zap2 = $"SELECT * FROM tbl_users WHERE username ='{textBox1.Text}'";
                    DataBank.cmd = new NpgsqlCommand(zap2, DataBank.conn);
                    NpgsqlDataReader reader2 = DataBank.cmd.ExecuteReader();

                    if (reader2.Read()==true)
                    {
                        reader2.Close();
                        label4.Location = new Point(106, 215);
                        label4.Text = "Логин уже зарегистрирован";
                        label4.Visible= true;
                        DataBank.conn.Close();
                    }
                    else
                    {
                        reader2.Close();
                        string zap4 = $"SELECT * FROM tbl_users WHERE email ='{textBox3.Text}'";
                        DataBank.cmd = new NpgsqlCommand(zap4, DataBank.conn);
                        NpgsqlDataReader reader4 = DataBank.cmd.ExecuteReader();
                        if (reader4.Read()==false)
                        {
                            reader4.Close();
                            Mail();
                        }
                        else { reader4.Close(); }
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
            button6.Visible = false;
            button1.Visible = false;
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
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu = new Form1();
            menu.Show();
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

        private void TestFormsN1_Load_1(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(18, 18, 18);
            textBox1.BackColor = Color.FromArgb(40, 40, 40);
            textBox2.BackColor = Color.FromArgb(40, 40, 40);
            label4.Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            tooltp.SetToolTip(button3, "Закрыть");
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            tooltp.SetToolTip(button1, "Вернуться на прошлую страницу");
        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            tooltp.SetToolTip(textBox1, "Придумайте новый логин");
        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            tooltp.SetToolTip(textBox2, "Придумайте новый пароль");
        }

        private void textBox3_MouseHover(object sender, EventArgs e)
        {
            tooltp.SetToolTip(textBox3, "Введите вашу почту");
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            tooltp.SetToolTip(button6, "Данные вашего аккаунта будут внесены в базу");
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            tooltp.SetToolTip(label4, "Проверьте правильность написания данных");
        }
    }
}
