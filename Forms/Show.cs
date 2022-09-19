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
    public partial class Show : Form
    {
        public int ovosh;
        #region Connect
        private NpgsqlCommandBuilder cmdBuilder;
        private NpgsqlConnection conn;
        private NpgsqlDataAdapter dataAdapter;
        private DataSet dataSet = null;
        private bool newRowAdding = false;
        string connectionString = String.Format($"Server = {DataBank.Server}; Port = {DataBank.Port}; User Id = {DataBank.User_id}; Password = {DataBank.Password}; Database = {DataBank.DB}");
        #endregion
        public Show()
        {
            InitializeComponent();
        }
        private void Show_Load(object sender, EventArgs e)
        {
            KeyPreview=true;
            KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button8_Click(button8, null); };
            KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button2_Click(button2, null); };
            button2.Visible=false;
            label2.Visible=false;

            #region ComboBoxs
            comboBox1.Items.Add("LogPass");
            comboBox1.Items.Add("Test");
            comboBox1.Items.Add("Category");
            comboBox1.Items.Add("Compound");
            comboBox1.Items.Add("Menu");
            comboBox1.Items.Add("Sales_food");
            comboBox1.Items.Add("Report");
            comboBox1.Items.Add("Workgroup");
            comboBox1.Items.Add("Peshka");
            comboBox1.Items.Add("Graph_per_day");

            comboBox2.Items.Add("LogPass");
            comboBox2.Items.Add("Test");
            comboBox2.Items.Add("Category");
            comboBox2.Items.Add("Compound");
            comboBox2.Items.Add("Menu");
            comboBox2.Items.Add("sales_food");
            comboBox2.Items.Add("Report");
            comboBox2.Items.Add("Workgroup");
            comboBox2.Items.Add("Peshka");
            comboBox2.Items.Add("Graph_per_day");

            comboBox3.Items.Add("LogPass");
            comboBox3.Items.Add("Test");
            comboBox3.Items.Add("Category");
            comboBox3.Items.Add("Compound");
            comboBox3.Items.Add("Menu");
            comboBox3.Items.Add("Sales_food");
            comboBox3.Items.Add("Report");
            comboBox3.Items.Add("Workgroup");
            comboBox3.Items.Add("Peshka");
            comboBox3.Items.Add("Graph_per_day");

            comboBox4.Items.Add("LogPass");
            comboBox4.Items.Add("Test");
            comboBox4.Items.Add("Category");
            comboBox4.Items.Add("Compound");
            comboBox4.Items.Add("Menu");
            comboBox4.Items.Add("Sales_food");
            comboBox4.Items.Add("Report");
            comboBox4.Items.Add("Workgroup");
            comboBox4.Items.Add("Peshka");
            comboBox4.Items.Add("Graph_per_day");

            comboBox5.Items.Add("LogPass");
            comboBox5.Items.Add("Test");
            comboBox5.Items.Add("Category");
            comboBox5.Items.Add("Compound");
            comboBox5.Items.Add("Menu");
            comboBox5.Items.Add("Sales_food");
            comboBox5.Items.Add("Report");
            comboBox5.Items.Add("Workgroup");
            comboBox5.Items.Add("Peshka");
            comboBox5.Items.Add("Graph_per_day");

            comboBox6.Items.Add("LogPass");
            comboBox6.Items.Add("Test");
            comboBox6.Items.Add("Category");
            comboBox6.Items.Add("Compound");
            comboBox6.Items.Add("Menu");
            comboBox6.Items.Add("Sales_food");
            comboBox6.Items.Add("Report");
            comboBox6.Items.Add("Workgroup");
            comboBox6.Items.Add("Peshka");
            comboBox6.Items.Add("Graph_per_day");

            comboBox7.Items.Add("LogPass");
            comboBox7.Items.Add("Test");
            comboBox7.Items.Add("Category");
            comboBox7.Items.Add("Compound");
            comboBox7.Items.Add("Menu");
            comboBox7.Items.Add("Sales_food");
            comboBox7.Items.Add("Report");
            comboBox7.Items.Add("Workgroup");
            comboBox7.Items.Add("Peshka");
            comboBox7.Items.Add("Graph_per_day");

            comboBox8.Items.Add("LogPass");
            comboBox8.Items.Add("Test");
            comboBox8.Items.Add("Category");
            comboBox8.Items.Add("Compound");
            comboBox8.Items.Add("Menu");
            comboBox8.Items.Add("Sales_food");
            comboBox8.Items.Add("Report");
            comboBox8.Items.Add("Workgroup");
            comboBox8.Items.Add("Peshka");
            comboBox8.Items.Add("Graph_per_day");

            comboBox9.Items.Add("LogPass");
            comboBox9.Items.Add("Test");
            comboBox9.Items.Add("Category");
            comboBox9.Items.Add("Compound");
            comboBox9.Items.Add("Menu");
            comboBox9.Items.Add("Sales_food");
            comboBox9.Items.Add("Report");
            comboBox9.Items.Add("Workgroup");
            comboBox9.Items.Add("Peshka");
            comboBox9.Items.Add("Graph_per_day");

            comboBox10.Items.Add("LogPass");
            comboBox10.Items.Add("Test");
            comboBox10.Items.Add("Category");
            comboBox10.Items.Add("Compound");
            comboBox10.Items.Add("Menu");
            comboBox10.Items.Add("Sales_food");
            comboBox10.Items.Add("Report");
            comboBox10.Items.Add("Workgroup");
            comboBox10.Items.Add("Peshka");
            comboBox10.Items.Add("Graph_per_day");
            #endregion

            conn = new NpgsqlConnection(connectionString);
            conn.Open();
            LoadData();
            this.BackColor = Color.FromArgb(18, 18, 18);
        }
        #region Page1
        private void LoadData()
        {
            try
            {
                button2.Visible=false;
                label2.Visible = false;
                dataAdapter = new NpgsqlDataAdapter("SELECT *, 'Delete' as Command FROM tbl_users", conn); // -
                cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
                cmdBuilder.GetInsertCommand();
                cmdBuilder.GetUpdateCommand();
                cmdBuilder.GetDeleteCommand();
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "tbl_users");// -
                dataGridView1.DataSource = dataSet.Tables["tbl_users"]; //2 -
                //Удалил
                dataGridView1.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
                this.dataGridView1.Columns[0].Visible = false; // Скрыть первый столбец
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReloadData()
        {
            try
            {
                dataSet.Tables["tbl_users"].Clear();// -
                dataAdapter.Fill(dataSet, "tbl_users");// -
                dataGridView1.DataSource = dataSet.Tables["tbl_users"]; //2 -
                dataGridView1.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
                this.dataGridView1.Columns[0].Visible = false; // Скрыть первый столбец
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex==4)// -
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(); // 2 -
                    if (task=="Delete")
                    {
                        if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView1.Rows.RemoveAt(rowIndex); // -
                            dataSet.Tables["tbl_users"].Rows[rowIndex].Delete(); // -
                            dataAdapter.Update(dataSet, "tbl_users"); // -
                        }
                    }
                    else if (task=="Insert")
                    {
                        try
                        {
                            dataGridView1.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
                            int rowIndex = dataGridView1.Rows.Count-2;// -
                            DataRow row = dataSet.Tables["tbl_users"].NewRow();// -

                            row["id"] = dataGridView1.Rows[rowIndex].Cells["id"].Value;//2 -
                            row["username"] = dataGridView1.Rows[rowIndex].Cells["username"].Value;//2 -
                            row["password"] = dataGridView1.Rows[rowIndex].Cells["password"].Value;//2 -
                            row["email"] = dataGridView1.Rows[rowIndex].Cells["email"].Value;//2 -

                            dataSet.Tables["tbl_users"].Rows.Add(row); // -
                            dataSet.Tables["tbl_users"].Rows.RemoveAt(dataSet.Tables["tbl_users"].Rows.Count-1); //2 -
                            dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count-2);//2
                            dataGridView1.Rows[e.RowIndex].Cells[4].Value = "Delete";//2 -

                            dataAdapter.Update(dataSet, "tbl_users"); // -
                            newRowAdding = false;
                        }
                        catch
                        {
                            newRowAdding=false;
                            MessageBox.Show("Заполните все поля", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if (task=="Update")
                    {
                        dataGridView1.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                        int r = e.RowIndex;
                        dataSet.Tables["tbl_users"].Rows[r]["id"]=dataGridView1.Rows[r].Cells["id"].Value;//4 -
                        dataSet.Tables["tbl_users"].Rows[r]["username"]=dataGridView1.Rows[r].Cells["username"].Value;//4 -
                        dataSet.Tables["tbl_users"].Rows[r]["password"]=dataGridView1.Rows[r].Cells["password"].Value;//4 -
                        dataSet.Tables["tbl_users"].Rows[r]["email"]=dataGridView1.Rows[r].Cells["email"].Value;//4 -
                        dataAdapter.Update(dataSet, "tbl_users"); // -
                        dataGridView1.Rows[e.RowIndex].Cells[4].Value = "Delete";//2 -
                    }
                    ReloadData(); // -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

            try
            {
                if (newRowAdding ==false)
                {
                    newRowAdding=true;
                    int lastRow = dataGridView1.Rows.Count-2;// -
                    DataGridViewRow row = dataGridView1.Rows[lastRow];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    row.Cells["Command"].Value = "Insert";
                    dataGridView1.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (newRowAdding==false)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;// -
                    DataGridViewRow editingRow = dataGridView1.Rows[rowIndex];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    editingRow.Cells["Command"].Value = "Update";
                    dataGridView1.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex==0)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button8_Click(button8, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button2_Click(button2, null); };
                tabControl1.SelectedTab=tabPage1;
                LoadData();
                button2.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox1.SelectedIndex==1)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button1_Click_2(button1, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button6_Click(button6, null); };
                tabControl1.SelectedTab=tabPage2;
                LoadData2();
                button6.Visible=false;
                label1.Visible=false;
            }
            else if (comboBox1.SelectedIndex==2)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button14_Click(button14, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button13_Click(button13, null); };
                tabControl1.SelectedTab=tabPage3;
                LoadData3(); // -
                button13.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox1.SelectedIndex==3)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button16_Click(button16, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button9_Click(button9, null); };
                tabControl1.SelectedTab=tabPage4;
                LoadData4(); // -
                label4.Visible=false;
                button9.Visible = false;
            }
            else if (comboBox1.SelectedIndex==4)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button20_Click(button20, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button17_Click(button17, null); };
                tabControl1.SelectedTab=tabPage5;
                LoadData5(); // -
                label5.Visible=false;
                button17.Visible = false;
            }
            else if (comboBox1.SelectedIndex==5)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button24_Click(button24, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button21_Click(button21, null); };
                tabControl1.SelectedTab=tabPage6;
                LoadData6(); // -
                label6.Visible=false;
                button21.Visible = false;
            }
            else if (comboBox1.SelectedIndex==6)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button25_Click(button25, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button26_Click(button26, null); };
                tabControl1.SelectedTab=tabPage7;
                LoadData7();
                label7.Visible=false;
                button26.Visible=false;
            }
            else if (comboBox1.SelectedIndex==7)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button30_Click(button30, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button29_Click(button29, null); };
                tabControl1.SelectedTab=tabPage8;
                LoadData8();
                label8.Visible=false;
                button29.Visible=false;
            }
            else if (comboBox1.SelectedIndex==8)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button34_Click(button34, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button33_Click(button33, null); };
                tabControl1.SelectedTab=tabPage9;
                LoadData9();
                label9.Visible=false;
                button33.Visible=false;
            }
            else if (comboBox1.SelectedIndex==9)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button38_Click(button38, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button37_Click(button37, null); };
                tabControl1.SelectedTab=tabPage10;
                LoadData10();
                label10.Visible=false;
                button37.Visible=false;
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void button8_Click(object sender, EventArgs e)
        {
            label2.Visible = true; // -
            button2.Visible=true; // -
            if (textBox2.TextLength==0) { return; } // -
            dataAdapter = new NpgsqlDataAdapter($"SELECT *, 'Delete' as Command FROM tbl_users where username like '{textBox2.Text}' or password like '{textBox2.Text}' or email like '{textBox2.Text}'", conn); // -
            textBox2.Clear(); // -
            cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
            cmdBuilder.GetInsertCommand();
            cmdBuilder.GetUpdateCommand();
            cmdBuilder.GetDeleteCommand();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "tbl_users");// -
            dataGridView1.DataSource = dataSet.Tables["tbl_users"]; //2 -
                                                                    //Удалил
            dataGridView1.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion
        #region Page2

        private void LoadData2()
        {
            label1.Visible = false;
            try
            {
                dataAdapter = new NpgsqlDataAdapter("SELECT *, 'Delete' as Command FROM normaliz", conn); //
                cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
                cmdBuilder.GetInsertCommand();
                cmdBuilder.GetUpdateCommand();
                cmdBuilder.GetDeleteCommand();
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "normaliz");//
                dataGridView2.DataSource = dataSet.Tables["normaliz"]; //2
                //Удалил
                dataGridView2.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue; //2
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReloadData2()
        {
            try
            {
                dataSet.Tables["normaliz"].Clear();//
                dataAdapter.Fill(dataSet, "normaliz");//
                dataGridView2.DataSource = dataSet.Tables["normaliz"]; //2
                dataGridView2.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue; //2
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex==3)//
                {
                    string task = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString(); // 2
                    if (task=="Delete")
                    {
                        if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView2.Rows.RemoveAt(rowIndex); //
                            dataSet.Tables["normaliz"].Rows[rowIndex].Delete(); //
                            dataAdapter.Update(dataSet, "normaliz"); //
                        }
                    }
                    else if (task=="Insert")
                    {
                        try
                        {
                            dataGridView2.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue; //2
                            int rowIndex = dataGridView2.Rows.Count-2;//
                            DataRow row = dataSet.Tables["normaliz"].NewRow();//

                            row["eda"] = dataGridView2.Rows[rowIndex].Cells["eda"].Value;//2
                            row["name"] = dataGridView2.Rows[rowIndex].Cells["name"].Value;//2
                            row["pass"] = dataGridView2.Rows[rowIndex].Cells["pass"].Value;//2

                            dataSet.Tables["normaliz"].Rows.Add(row); //
                            dataSet.Tables["normaliz"].Rows.RemoveAt(dataSet.Tables["normaliz"].Rows.Count-1); //2
                            dataGridView2.Rows.RemoveAt(dataGridView2.Rows.Count-2);//2
                            dataGridView2.Rows[e.RowIndex].Cells[3].Value = "Delete";//2

                            dataAdapter.Update(dataSet, "normaliz"); //
                            newRowAdding = false;
                        }
                        catch
                        {
                            newRowAdding=false;
                            MessageBox.Show("Заполните все поля", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if (task=="Update")
                    {
                        dataGridView2.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue;//2
                        int r = e.RowIndex;
                        dataSet.Tables["normaliz"].Rows[r]["eda"]=dataGridView2.Rows[r].Cells["eda"].Value;//4
                        dataSet.Tables["normaliz"].Rows[r]["name"]=dataGridView2.Rows[r].Cells["name"].Value;//4
                        dataSet.Tables["normaliz"].Rows[r]["pass"]=dataGridView2.Rows[r].Cells["pass"].Value;//4
                        dataAdapter.Update(dataSet, "normaliz"); //
                        dataGridView2.Rows[e.RowIndex].Cells[3].Value = "Delete";//
                    }
                    ReloadData2(); //
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (newRowAdding ==false)
                {
                    newRowAdding=true;
                    int lastRow = dataGridView2.Rows.Count-2;//
                    DataGridViewRow row = dataGridView2.Rows[lastRow];//
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    row.Cells["Command"].Value = "Insert";
                    dataGridView2.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue;//2

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (newRowAdding==false)
                {
                    int rowIndex = dataGridView2.SelectedCells[0].RowIndex;//
                    DataGridViewRow editingRow = dataGridView2.Rows[rowIndex];//
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    editingRow.Cells["Command"].Value = "Update";
                    dataGridView2.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue;//2
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged_3(object sender, EventArgs e)
        {
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            LoadData2();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            label1.Visible = true;
            button6.Visible=true;
            if (textBox1.TextLength==0) { return; }
            dataAdapter = new NpgsqlDataAdapter($"SELECT *, 'Delete' as Command FROM normaliz where eda like '{textBox1.Text}' or name like '{textBox1.Text}' or pass like '{textBox1.Text}'", conn); //
            textBox1.Clear();
            cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
            cmdBuilder.GetInsertCommand();
            cmdBuilder.GetUpdateCommand();
            cmdBuilder.GetDeleteCommand();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "normaliz");//
            dataGridView2.DataSource = dataSet.Tables["normaliz"]; //2
                                                                   //Удалил
            dataGridView2.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue; //2
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox2.SelectedIndex==0)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button8_Click(button8, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button2_Click(button2, null); };
                tabControl1.SelectedTab=tabPage1;
                LoadData();
                button2.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox2.SelectedIndex==1)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button1_Click_2(button1, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button6_Click(button6, null); };
                tabControl1.SelectedTab=tabPage2;
                LoadData2();
                button6.Visible=false;
                label1.Visible=false;
            }
            else if (comboBox2.SelectedIndex==2)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button14_Click(button14, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button13_Click(button13, null); };
                tabControl1.SelectedTab=tabPage3;
                LoadData3(); // -
                button13.Visible=false;
                label3.Visible=false;
            }
            else if (comboBox2.SelectedIndex==3)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button16_Click(button16, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button9_Click(button9, null); };
                tabControl1.SelectedTab=tabPage4;
                LoadData4(); // -
                label4.Visible=false;
                button9.Visible = false;
            }
            else if (comboBox2.SelectedIndex==4)
            {
                MessageBox.Show("5");
            }
            else if (comboBox2.SelectedIndex==5)
            {
                MessageBox.Show("6");
            }
            else if (comboBox2.SelectedIndex==6)
            {
                MessageBox.Show("7");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click_3(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Visible=false;
            LoadData2();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click_2(object sender, EventArgs e)
        {

        }

        private void label4_Click_3(object sender, EventArgs e)
        {

        }
        #endregion
        #region Category
        private void LoadData3()
        {
            if (ovosh==1)
            {
                ovosh = 0;
                this.dataGridView3.Columns[1].Visible = true;
                this.dataGridView3.Columns[2].Visible = true;
                this.dataGridView3.Columns[3].Visible = true;
                this.dataGridView3.Columns[4].Visible = true;
            }
            else if (ovosh==0)
            {
                label3.Visible = false;
                try
                {
                    dataAdapter = new NpgsqlDataAdapter("SELECT *, 'Delete' as Command FROM category", conn); // -
                    cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
                    cmdBuilder.GetInsertCommand();
                    cmdBuilder.GetUpdateCommand();
                    cmdBuilder.GetDeleteCommand();
                    dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "category");// -
                    dataGridView3.DataSource = dataSet.Tables["category"]; //2 -
                                                                           //this.dataGridView3.Columns[4].Visible = false; ------------
                                                                           //Удалил
                    dataGridView3.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ReloadData3()
        {
            try
            {
                dataSet.Tables["category"].Clear();// -
                dataAdapter.Fill(dataSet, "category");// -
                dataGridView3.DataSource = dataSet.Tables["category"]; //2 -
                dataGridView3.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex==5)// -
                {
                    string task = dataGridView3.Rows[e.RowIndex].Cells[5].Value.ToString(); // 2 -
                    if (task=="Delete")
                    {
                        if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView3.Rows.RemoveAt(rowIndex); // -
                            dataSet.Tables["category"].Rows[rowIndex].Delete(); // -
                            dataAdapter.Update(dataSet, "category"); // -
                        }
                    }
                    else if (task=="Insert")
                    {
                        try
                        {
                            dataGridView3.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
                            int rowIndex = dataGridView3.Rows.Count-2;// -
                            DataRow row = dataSet.Tables["category"].NewRow();// -

                            row["ovosh"] = dataGridView3.Rows[rowIndex].Cells["ovosh"].Value;//2 -
                            row["riba"] = dataGridView3.Rows[rowIndex].Cells["riba"].Value;//2 -
                            row["miaso"] = dataGridView3.Rows[rowIndex].Cells["miaso"].Value;//2 -
                            row["voda"] = dataGridView3.Rows[rowIndex].Cells["voda"].Value;//2 -
                            row["cat_id"] = dataGridView3.Rows[rowIndex].Cells["cat_id"].Value;//2 -

                            dataSet.Tables["category"].Rows.Add(row); // -
                            dataSet.Tables["category"].Rows.RemoveAt(dataSet.Tables["category"].Rows.Count-1); //2 -
                            dataGridView3.Rows.RemoveAt(dataGridView3.Rows.Count-2);//2 -
                            dataGridView3.Rows[e.RowIndex].Cells[5].Value = "Delete";//2 -

                            dataAdapter.Update(dataSet, "category"); // -
                            newRowAdding = false;
                        }
                        catch
                        {
                            newRowAdding=false;
                            MessageBox.Show("Заполните все поля", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if (task=="Update")
                    {
                        dataGridView3.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                        int r = e.RowIndex;
                        dataSet.Tables["category"].Rows[r]["ovosh"]=dataGridView3.Rows[r].Cells["ovosh"].Value;//4 -
                        dataSet.Tables["category"].Rows[r]["riba"]=dataGridView3.Rows[r].Cells["riba"].Value;//4 - 
                        dataSet.Tables["category"].Rows[r]["miaso"]=dataGridView3.Rows[r].Cells["miaso"].Value;//4 -
                        dataSet.Tables["category"].Rows[r]["voda"]=dataGridView3.Rows[r].Cells["voda"].Value;//4 -
                        dataSet.Tables["category"].Rows[r]["cat_id"]=dataGridView3.Rows[r].Cells["cat_id"].Value;//4 -
                        dataAdapter.Update(dataSet, "category"); //
                        dataGridView3.Rows[e.RowIndex].Cells[5].Value = "Delete";//2 -
                    }
                    ReloadData3(); // -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView3_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

            try
            {
                if (newRowAdding ==false)
                {
                    newRowAdding=true;
                    int lastRow = dataGridView3.Rows.Count-2;// -
                    DataGridViewRow row = dataGridView3.Rows[lastRow];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    row.Cells["Command"].Value = "Insert";
                    dataGridView3.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (newRowAdding==false)
                {
                    int rowIndex = dataGridView3.SelectedCells[0].RowIndex;// -
                    DataGridViewRow editingRow = dataGridView3.Rows[rowIndex];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    editingRow.Cells["Command"].Value = "Update";
                    dataGridView3.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            LoadData3();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex==0)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button8_Click(button8, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button2_Click(button2, null); };
                tabControl1.SelectedTab=tabPage1;
                LoadData();
                button2.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox3.SelectedIndex==1)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button1_Click_2(button1, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button6_Click(button6, null); };
                tabControl1.SelectedTab=tabPage2;
                LoadData2();
                button6.Visible=false;
                label1.Visible=false;
            }
            else if (comboBox3.SelectedIndex==2)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button14_Click(button14, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button13_Click(button13, null); };
                tabControl1.SelectedTab=tabPage3;
                LoadData3(); // -
                button13.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox3.SelectedIndex==3)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button16_Click(button16, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button9_Click(button9, null); };
                tabControl1.SelectedTab=tabPage4;
                LoadData4(); // -
                label4.Visible=false;
                button9.Visible = false;
            }
            else if (comboBox3.SelectedIndex==4)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button20_Click(button20, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button17_Click(button17, null); };
                tabControl1.SelectedTab=tabPage5;
                LoadData5(); // -
                label5.Visible=false;
                button17.Visible = false;
            }
            else if (comboBox3.SelectedIndex==5)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button24_Click(button24, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button21_Click(button21, null); };
                tabControl1.SelectedTab=tabPage6;
                LoadData6(); // -
                label6.Visible=false;
                button21.Visible = false;
            }
            else if (comboBox3.SelectedIndex==6)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button25_Click(button25, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button26_Click(button26, null); };
                tabControl1.SelectedTab=tabPage7;
                LoadData7();
                label7.Visible=false;
                button26.Visible=false;
            }
            else if (comboBox3.SelectedIndex==7)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button30_Click(button30, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button29_Click(button29, null); };
                tabControl1.SelectedTab=tabPage8;
                LoadData8();
                label8.Visible=false;
                button29.Visible=false;
            }
            else if (comboBox3.SelectedIndex==8)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button34_Click(button34, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button33_Click(button33, null); };
                tabControl1.SelectedTab=tabPage9;
                LoadData9();
                label9.Visible=false;
                button33.Visible=false;
            }
            else if (comboBox3.SelectedIndex==9)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button38_Click(button38, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button37_Click(button37, null); };
                tabControl1.SelectedTab=tabPage10;
                LoadData10();
                label10.Visible=false;
                button37.Visible=false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Visible = false;
            LoadData3();
        }

        private void button14_Click(object sender, EventArgs e)
        {

            label3.Visible = true; // -
            button13.Visible=true; // -
            if (textBox3.TextLength==0) { return; }
            dataAdapter = new NpgsqlDataAdapter($"SELECT *, 'Delete' as Command FROM category where ovosh like '{textBox3.Text}' or riba like '{textBox3.Text}' or miaso like '{textBox3.Text}' or voda like '{textBox3.Text}' or cat_id = {textBox3.Text}", conn); // -
            textBox3.Clear(); // -
            cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
            cmdBuilder.GetInsertCommand();
            cmdBuilder.GetUpdateCommand();
            cmdBuilder.GetDeleteCommand();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "category");// -
            dataGridView3.DataSource = dataSet.Tables["category"]; //2 -
                                                                   //Удалил
            dataGridView3.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
        }
        private void button41_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            button13.Visible=true;
            ovosh = 1;
            this.dataGridView3.Columns[1].Visible = false;
            this.dataGridView3.Columns[2].Visible = false;
            this.dataGridView3.Columns[3].Visible = false;
            this.dataGridView3.Columns[4].Visible = false;
        }
        private void dataGridView3_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress2);
            if (dataGridView3.CurrentCell.ColumnIndex==4)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox!=null)
                {
                    textBox.KeyPress+= new KeyPressEventHandler(Column_KeyPress2);
                }
            }
        }
        private void Column_KeyPress2(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion
        #region Compount
        private void LoadData4()
        {
            button9.Visible = false;
            label4.Visible = false;
            try
            {
                dataAdapter = new NpgsqlDataAdapter("SELECT *, 'Delete' as Command FROM compound", conn); // -
                cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
                cmdBuilder.GetInsertCommand();
                cmdBuilder.GetUpdateCommand();
                cmdBuilder.GetDeleteCommand();
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "compound");// -
                dataGridView4.DataSource = dataSet.Tables["compound"]; //2 -
                //this.dataGridView4.Columns[0].Visible = false; ---------------------------------
                //this.dataGridView4.Columns[3].Visible = false;
                //Удалил
                dataGridView4.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReloadData4()
        {
            try
            {
                dataSet.Tables["compound"].Clear();// -
                dataAdapter.Fill(dataSet, "compound");// -
                dataGridView4.DataSource = dataSet.Tables["compound"]; //2 -
                dataGridView4.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.ColumnIndex==4)// -
                {
                    string task = dataGridView4.Rows[e.RowIndex].Cells[4].Value.ToString(); // 2 -
                    if (task=="Delete")
                    {
                        if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView4.Rows.RemoveAt(rowIndex); // -
                            dataSet.Tables["compound"].Rows[rowIndex].Delete(); // -
                            dataAdapter.Update(dataSet, "compound"); // -
                        }
                    }
                    else if (task=="Insert")
                    {
                        try
                        {
                            dataGridView4.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
                            int rowIndex = dataGridView4.Rows.Count-2;// -
                            DataRow row = dataSet.Tables["compound"].NewRow();// -

                            row["food_id"] = dataGridView4.Rows[rowIndex].Cells["food_id"].Value;//2 -
                            row["ingridients"] = dataGridView4.Rows[rowIndex].Cells["ingridients"].Value;//2 -
                            row["col_vo_ingrid"] = dataGridView4.Rows[rowIndex].Cells["col_vo_ingrid"].Value;//2 -
                            row["cat_id"] = dataGridView4.Rows[rowIndex].Cells["cat_id"].Value;//2 -

                            dataSet.Tables["compound"].Rows.Add(row); // -
                            dataSet.Tables["compound"].Rows.RemoveAt(dataSet.Tables["compound"].Rows.Count-1); //2 -
                            dataGridView4.Rows.RemoveAt(dataGridView4.Rows.Count-2);//2 -
                            dataGridView4.Rows[e.RowIndex].Cells[4].Value = "Delete";//2 -

                            dataAdapter.Update(dataSet, "compound"); // -
                            newRowAdding = false;
                        }
                        catch
                        {
                            newRowAdding=false;
                            MessageBox.Show("Заполните все поля", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if (task=="Update")
                    {
                        dataGridView4.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                        int r = e.RowIndex;
                        dataSet.Tables["compound"].Rows[r]["food_id"]=dataGridView4.Rows[r].Cells["food_id"].Value;//4  -
                        dataSet.Tables["compound"].Rows[r]["ingridients"]=dataGridView4.Rows[r].Cells["ingridients"].Value;//4-
                        dataSet.Tables["compound"].Rows[r]["col_vo_ingrid"]=dataGridView4.Rows[r].Cells["col_vo_ingrid"].Value;//4 -
                        dataSet.Tables["compound"].Rows[r]["cat_id"]=dataGridView4.Rows[r].Cells["cat_id"].Value;//4 -
                        dataAdapter.Update(dataSet, "compound"); // -
                        dataGridView4.Rows[e.RowIndex].Cells[4].Value = "Delete";// -
                    }
                    ReloadData4(); // -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView4_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

            try
            {
                if (newRowAdding ==false)
                {
                    newRowAdding=true;
                    int lastRow = dataGridView4.Rows.Count-2;// -
                    DataGridViewRow row = dataGridView4.Rows[lastRow];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    row.Cells["Command"].Value = "Insert";
                    dataGridView4.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView4_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (newRowAdding==false)
                {
                    int rowIndex = dataGridView4.SelectedCells[0].RowIndex;// -
                    DataGridViewRow editingRow = dataGridView4.Rows[rowIndex];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    editingRow.Cells["Command"].Value = "Update";
                    dataGridView4.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex==0)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button8_Click(button8, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button2_Click(button2, null); };
                tabControl1.SelectedTab=tabPage1;
                LoadData();
                button2.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox4.SelectedIndex==1)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button1_Click_2(button1, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button6_Click(button6, null); };
                tabControl1.SelectedTab=tabPage2;
                LoadData2();
                button6.Visible=false;
                label1.Visible=false;
            }
            else if (comboBox4.SelectedIndex==2)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button14_Click(button14, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button13_Click(button13, null); };
                tabControl1.SelectedTab=tabPage3;
                LoadData3(); // -
                button13.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox4.SelectedIndex==3)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button16_Click(button16, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button9_Click(button9, null); };
                tabControl1.SelectedTab=tabPage4;
                LoadData4(); // -
                label4.Visible=false;
                button9.Visible = false;
            }
            else if (comboBox4.SelectedIndex==4)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button20_Click(button20, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button17_Click(button17, null); };
                tabControl1.SelectedTab=tabPage5;
                LoadData5(); // -
                label5.Visible=false;
                button17.Visible = false;
            }
            else if (comboBox4.SelectedIndex==5)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button24_Click(button24, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button21_Click(button21, null); };
                tabControl1.SelectedTab=tabPage6;
                LoadData6(); // -
                label6.Visible=false;
                button21.Visible = false;
            }
            else if (comboBox4.SelectedIndex==6)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button25_Click(button25, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button26_Click(button26, null); };
                tabControl1.SelectedTab=tabPage7;
                LoadData7();
                label7.Visible=false;
                button26.Visible=false;
            }
            else if (comboBox4.SelectedIndex==7)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button30_Click(button30, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button29_Click(button29, null); };
                tabControl1.SelectedTab=tabPage8;
                LoadData8();
                label8.Visible=false;
                button29.Visible=false;
            }
            else if (comboBox4.SelectedIndex==8)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button34_Click(button34, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button33_Click(button33, null); };
                tabControl1.SelectedTab=tabPage9;
                LoadData9();
                label9.Visible=false;
                button33.Visible=false;
            }
            else if (comboBox4.SelectedIndex==9)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button38_Click(button38, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button37_Click(button37, null); };
                tabControl1.SelectedTab=tabPage10;
                LoadData10();
                label10.Visible=false;
                button37.Visible=false;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LoadData4();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            button9.Visible = true;
            label4.Visible = true;
            button6.Visible=true;
            if (textBox4.TextLength==0) { return; }
            dataAdapter = new NpgsqlDataAdapter($"SELECT *, 'Delete' as Command FROM compound where food_id ={textBox4.Text} or ingridients like '{textBox4.Text}' or col_vo_ingrid ={textBox4.Text} or cat_id ={textBox4.Text}", conn); // -
            textBox4.Clear();
            cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
            cmdBuilder.GetInsertCommand();
            cmdBuilder.GetUpdateCommand();
            cmdBuilder.GetDeleteCommand();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "compound");// -
            dataGridView4.DataSource = dataSet.Tables["compound"]; //2 -
                                                                   //Удалил
            dataGridView4.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);
            if(dataGridView4.CurrentCell.ColumnIndex==0 || dataGridView4.CurrentCell.ColumnIndex==2 || dataGridView4.CurrentCell.ColumnIndex==3)
            {
                TextBox textBox =  e.Control as TextBox;
                if (textBox!=null)
                {
                    textBox.KeyPress+= new KeyPressEventHandler(Column_KeyPress);
                }
            }
        }
        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            LoadData4();
        }
        #endregion
        #region Menu
        private void tabPage5_Click(object sender, EventArgs e)
        {

        }
        private void LoadData5()
        {
            button17.Visible = false;
            label5.Visible = false;
            try
            {
                dataAdapter = new NpgsqlDataAdapter("SELECT *, 'Delete' as Command FROM menu", conn); // -
                cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
                cmdBuilder.GetInsertCommand();
                cmdBuilder.GetUpdateCommand();
                cmdBuilder.GetDeleteCommand();
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "menu");// -
                dataGridView5.DataSource = dataSet.Tables["menu"]; //2 -
                //this.dataGridView4.Columns[0].Visible = false; ---------------------------------
                //this.dataGridView4.Columns[3].Visible = false;
                //Удалил
                dataGridView5.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReloadData5()
        {
            try
            {
                dataSet.Tables["menu"].Clear();// -
                dataAdapter.Fill(dataSet, "menu");// -
                dataGridView5.DataSource = dataSet.Tables["menu"]; //2 -
                dataGridView5.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex==5)// -
                {
                    string task = dataGridView5.Rows[e.RowIndex].Cells[5].Value.ToString(); // 2 -
                    if (task=="Delete")
                    {
                        if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView5.Rows.RemoveAt(rowIndex); // -
                            dataSet.Tables["menu"].Rows[rowIndex].Delete(); // -
                            dataAdapter.Update(dataSet, "menu"); // -
                        }
                    }
                    else if (task=="Insert")
                    {
                        try
                        {
                            dataGridView5.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
                            int rowIndex = dataGridView5.Rows.Count-2;// -
                            DataRow row = dataSet.Tables["menu"].NewRow();// -

                            row["name"] = dataGridView5.Rows[rowIndex].Cells["name"].Value;//2 -
                            row["weight"] = dataGridView5.Rows[rowIndex].Cells["weight"].Value;//2 -
                            row["price"] = dataGridView5.Rows[rowIndex].Cells["price"].Value;//2 -
                            row["menu_id"] = dataGridView5.Rows[rowIndex].Cells["menu_id"].Value;//2 -
                            row["food_id"] = dataGridView5.Rows[rowIndex].Cells["food_id"].Value;//2 -

                            dataSet.Tables["menu"].Rows.Add(row); // -
                            dataSet.Tables["menu"].Rows.RemoveAt(dataSet.Tables["menu"].Rows.Count-1); //2 -
                            dataGridView5.Rows.RemoveAt(dataGridView5.Rows.Count-2);//2 -
                            dataGridView5.Rows[e.RowIndex].Cells[5].Value = "Delete";//2 -

                            dataAdapter.Update(dataSet, "menu"); // -
                            newRowAdding = false;
                        }
                        catch
                        {
                            newRowAdding=false;
                            MessageBox.Show("Заполните все поля", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if (task=="Update")
                    {
                        dataGridView5.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                        int r = e.RowIndex;
                        dataSet.Tables["menu"].Rows[r]["name"]=dataGridView5.Rows[r].Cells["name"].Value;//4  -
                        dataSet.Tables["menu"].Rows[r]["weight"]=dataGridView5.Rows[r].Cells["weight"].Value;//4-
                        dataSet.Tables["menu"].Rows[r]["price"]=dataGridView5.Rows[r].Cells["price"].Value;//4 -
                        dataSet.Tables["menu"].Rows[r]["menu_id"]=dataGridView5.Rows[r].Cells["menu_id"].Value;//4 -
                        dataSet.Tables["menu"].Rows[r]["food_id"]=dataGridView5.Rows[r].Cells["food_id"].Value;//4 -
                        dataAdapter.Update(dataSet, "menu"); // -
                        dataGridView5.Rows[e.RowIndex].Cells[5].Value = "Delete";//2 -
                    }
                    ReloadData5(); // -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView5_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (newRowAdding ==false)
                {
                    newRowAdding=true;
                    int lastRow = dataGridView5.Rows.Count-2;// -
                    DataGridViewRow row = dataGridView5.Rows[lastRow];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    row.Cells["Command"].Value = "Insert";
                    dataGridView5.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView5_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (newRowAdding==false)
                {
                    int rowIndex = dataGridView5.SelectedCells[0].RowIndex;// -
                    DataGridViewRow editingRow = dataGridView5.Rows[rowIndex];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    editingRow.Cells["Command"].Value = "Update";
                    dataGridView5.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex==0)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button8_Click(button8, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button2_Click(button2, null); };
                tabControl1.SelectedTab=tabPage1;
                LoadData();
                button2.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox5.SelectedIndex==1)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button1_Click_2(button1, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button6_Click(button6, null); };
                tabControl1.SelectedTab=tabPage2;
                LoadData2();
                button6.Visible=false;
                label1.Visible=false;
            }
            else if (comboBox5.SelectedIndex==2)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button14_Click(button14, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button13_Click(button13, null); };
                tabControl1.SelectedTab=tabPage3;
                LoadData3(); // -
                button13.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox5.SelectedIndex==3)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button16_Click(button16, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button9_Click(button9, null); };
                tabControl1.SelectedTab=tabPage4;
                LoadData4(); // -
                label4.Visible=false;
                button9.Visible = false;
            }
            else if (comboBox5.SelectedIndex==4)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button20_Click(button20, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button17_Click(button17, null); };
                tabControl1.SelectedTab=tabPage5;
                LoadData5(); // -
                label5.Visible=false;
                button17.Visible = false;
            }
            else if (comboBox5.SelectedIndex==5)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button24_Click(button24, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button21_Click(button21, null); };
                tabControl1.SelectedTab=tabPage6;
                LoadData6(); // -
                label6.Visible=false;
                button21.Visible = false;
            }
            else if (comboBox5.SelectedIndex==6)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button25_Click(button25, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button26_Click(button26, null); };
                tabControl1.SelectedTab=tabPage7;
                LoadData7();
                label7.Visible=false;
                button26.Visible=false;
            }
            else if (comboBox5.SelectedIndex==7)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button30_Click(button30, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button29_Click(button29, null); };
                tabControl1.SelectedTab=tabPage8;
                LoadData8();
                label8.Visible=false;
                button29.Visible=false;
            }
            else if (comboBox5.SelectedIndex==8)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button34_Click(button34, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button33_Click(button33, null); };
                tabControl1.SelectedTab=tabPage9;
                LoadData9();
                label9.Visible=false;
                button33.Visible=false;
            }
            else if (comboBox5.SelectedIndex==9)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button38_Click(button38, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button37_Click(button37, null); };
                tabControl1.SelectedTab=tabPage10;
                LoadData10();
                label10.Visible=false;
                button37.Visible=false;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            LoadData5();
        }   

        private void button20_Click(object sender, EventArgs e)
        {
            button17.Visible = true;
            label5.Visible = true;
            // Если что удалить Если не чего не делает button6.Visible=true;
            if (textBox5.TextLength==0) { return; }
            dataAdapter = new NpgsqlDataAdapter($"SELECT *, 'Delete' as Command FROM menu where name like '{textBox5.Text}' or weight ={textBox5.Text} or price ={textBox5.Text} or menu_id ={textBox5.Text} or food_id ={textBox5.Text}", conn); // -
            textBox5.Clear();
            cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
            cmdBuilder.GetInsertCommand();
            cmdBuilder.GetUpdateCommand();
            cmdBuilder.GetDeleteCommand();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "menu");// -
            dataGridView5.DataSource = dataSet.Tables["menu"]; //2 -
                                                                   //Удалил
            dataGridView5.Columns[5].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
        }

        private void button19_Click(object sender, EventArgs e)
        {
            LoadData5();
        }

        private void dataGridView5_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress3);
            if (dataGridView5.CurrentCell.ColumnIndex==1 || dataGridView5.CurrentCell.ColumnIndex==2 || dataGridView5.CurrentCell.ColumnIndex==3 || dataGridView5.CurrentCell.ColumnIndex==4)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox!=null)
                {
                    textBox.KeyPress+= new KeyPressEventHandler(Column_KeyPress3);
                }
            }
        }
        private void Column_KeyPress3(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region sales_food
        private void tabPage6_Click(object sender, EventArgs e)
        {

        }
        private void LoadData6()
        {
            label6.Visible=false;
            button21.Visible = false;
            try
            {
                dataAdapter = new NpgsqlDataAdapter("SELECT *, 'Delete' as Command FROM sales_food", conn); // -
                cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
                cmdBuilder.GetInsertCommand();
                cmdBuilder.GetUpdateCommand();
                cmdBuilder.GetDeleteCommand();
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "sales_food");// -
                dataGridView6.DataSource = dataSet.Tables["sales_food"]; //2 -
                //this.dataGridView4.Columns[0].Visible = false; ---------------------------------
                //this.dataGridView4.Columns[3].Visible = false;
                //Удалил
                dataGridView6.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReloadData6()
        {
            try
            {
                dataSet.Tables["sales_food"].Clear();// -
                dataAdapter.Fill(dataSet, "sales_food");// -
                dataGridView6.DataSource = dataSet.Tables["sales_food"]; //2 -
                dataGridView6.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.ColumnIndex==4)// -
                {
                    string task = dataGridView6.Rows[e.RowIndex].Cells[4].Value.ToString(); // 2 -
                    if (task=="Delete")
                    {
                        if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView6.Rows.RemoveAt(rowIndex); // -
                            dataSet.Tables["sales_food"].Rows[rowIndex].Delete(); // -
                            dataAdapter.Update(dataSet, "sales_food"); // -
                        }
                    }
                    else if (task=="Insert")
                    {
                        try
                        {
                            dataGridView6.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
                            int rowIndex = dataGridView6.Rows.Count-2;// -
                            DataRow row = dataSet.Tables["sales_food"].NewRow();// -

                            row["col_vo"] = dataGridView6.Rows[rowIndex].Cells["col_vo"].Value;//2 -
                            row["compound"] = dataGridView6.Rows[rowIndex].Cells["compound"].Value;//2 -
                            row["sales_id"] = dataGridView6.Rows[rowIndex].Cells["sales_id"].Value;//2 -
                            row["menu_id"] = dataGridView6.Rows[rowIndex].Cells["menu_id"].Value;//2 -

                            dataSet.Tables["sales_food"].Rows.Add(row); // -
                            dataSet.Tables["sales_food"].Rows.RemoveAt(dataSet.Tables["sales_food"].Rows.Count-1); //2 -
                            dataGridView6.Rows.RemoveAt(dataGridView6.Rows.Count-2);//2 -
                            dataGridView6.Rows[e.RowIndex].Cells[4].Value = "Delete";//2 -

                            dataAdapter.Update(dataSet, "sales_food"); // -
                            newRowAdding = false;
                        }
                        catch
                        {
                            newRowAdding=false;
                            MessageBox.Show("Заполните все поля", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if (task=="Update")
                    {
                        dataGridView6.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                        int r = e.RowIndex;
                        dataSet.Tables["sales_food"].Rows[r]["col_vo"]=dataGridView6.Rows[r].Cells["col_vo"].Value;//4  -
                        dataSet.Tables["sales_food"].Rows[r]["compound"]=dataGridView6.Rows[r].Cells["compound"].Value;//4-
                        dataSet.Tables["sales_food"].Rows[r]["sales_id"]=dataGridView6.Rows[r].Cells["sales_id"].Value;//4 -
                        dataSet.Tables["sales_food"].Rows[r]["menu_id"]=dataGridView6.Rows[r].Cells["menu_id"].Value;//4 -
                        dataAdapter.Update(dataSet, "sales_food"); // -
                        dataGridView6.Rows[e.RowIndex].Cells[4].Value = "Delete";//2 -
                    }
                    ReloadData6(); // -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView6_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

            try
            {
                if (newRowAdding ==false)
                {
                    newRowAdding=true;
                    int lastRow = dataGridView6.Rows.Count-2;// -
                    DataGridViewRow row = dataGridView6.Rows[lastRow];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    row.Cells["Command"].Value = "Insert";
                    dataGridView6.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView6_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (newRowAdding==false)
                {
                    int rowIndex = dataGridView6.SelectedCells[0].RowIndex;// -
                    DataGridViewRow editingRow = dataGridView6.Rows[rowIndex];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    editingRow.Cells["Command"].Value = "Update";
                    dataGridView6.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox6.SelectedIndex==0)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button8_Click(button8, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button2_Click(button2, null); };
                tabControl1.SelectedTab=tabPage1;
                LoadData();
                button2.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox6.SelectedIndex==1)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button1_Click_2(button1, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button6_Click(button6, null); };
                tabControl1.SelectedTab=tabPage2;
                LoadData2();
                button6.Visible=false;
                label1.Visible=false;
            }
            else if (comboBox6.SelectedIndex==2)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button14_Click(button14, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button13_Click(button13, null); };
                tabControl1.SelectedTab=tabPage3;
                LoadData3(); // -
                button13.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox6.SelectedIndex==3)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button16_Click(button16, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button9_Click(button9, null); };
                tabControl1.SelectedTab=tabPage4;
                LoadData4(); // -
                label4.Visible=false;
                button9.Visible = false;
            }
            else if (comboBox6.SelectedIndex==4)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button20_Click(button20, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button17_Click(button17, null); };
                tabControl1.SelectedTab=tabPage5;
                LoadData5(); // -
                label5.Visible=false;
                button17.Visible = false;
            }
            else if (comboBox6.SelectedIndex==5)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button24_Click(button24, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button21_Click(button21, null); };
                tabControl1.SelectedTab=tabPage6;
                LoadData6(); // -
                label6.Visible=false;
                button21.Visible = false;
            }
            else if (comboBox6.SelectedIndex==6)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button25_Click(button25, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button26_Click(button26, null); };
                tabControl1.SelectedTab=tabPage7;
                LoadData7();
                label7.Visible=false;
                button26.Visible=false;
            }
            else if (comboBox6.SelectedIndex==7)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button30_Click(button30, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button29_Click(button29, null); };
                tabControl1.SelectedTab=tabPage8;
                LoadData8();
                label8.Visible=false;
                button29.Visible=false;
            }
            else if (comboBox6.SelectedIndex==8)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button34_Click(button34, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button33_Click(button33, null); };
                tabControl1.SelectedTab=tabPage9;
                LoadData9();
                label9.Visible=false;
                button33.Visible=false;
            }
            else if (comboBox6.SelectedIndex==9)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button38_Click(button38, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button37_Click(button37, null); };
                tabControl1.SelectedTab=tabPage10;
                LoadData10();
                label10.Visible=false;
                button37.Visible=false;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            LoadData6();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            button21.Visible = true;
            label6.Visible = true;
            if (textBox6.TextLength==0) { return; }
            
            dataAdapter = new NpgsqlDataAdapter($"SELECT *, 'Delete' as Command FROM sales_food where col_vo ={textBox6.Text} or compound like '{textBox6.Text}' or sales_id ={textBox6.Text} or menu_id ={textBox6.Text}", conn); // -
            textBox6.Clear();
            cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
            cmdBuilder.GetInsertCommand();
            cmdBuilder.GetUpdateCommand();
            cmdBuilder.GetDeleteCommand();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "sales_food");// -
            dataGridView6.DataSource = dataSet.Tables["sales_food"]; //2 -
                                                               //Удалил
            dataGridView6.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
        }

        private void button23_Click(object sender, EventArgs e)
        {
            LoadData6();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView6_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress4);
            if (dataGridView6.CurrentCell.ColumnIndex==0 || dataGridView6.CurrentCell.ColumnIndex==2 || dataGridView6.CurrentCell.ColumnIndex==3)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox!=null)
                {
                    textBox.KeyPress+= new KeyPressEventHandler(Column_KeyPress4);
                }
            }
        }
        private void Column_KeyPress4(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion
        #region report

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }
        private void LoadData7()
        {
            button26.Visible = false;
            label7.Visible = false;
            try
            {
                dataAdapter = new NpgsqlDataAdapter("SELECT *, 'Delete' as Command FROM report", conn); // -
                cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
                cmdBuilder.GetInsertCommand();
                cmdBuilder.GetUpdateCommand();
                cmdBuilder.GetDeleteCommand();
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "report");// -
                dataGridView7.DataSource = dataSet.Tables["report"]; //2 -
                //this.dataGridView4.Columns[0].Visible = false; ---------------------------------
                //this.dataGridView4.Columns[3].Visible = false;
                //Удалил
                dataGridView7.Columns[7].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReloadData7()
        {
            try
            {
                dataSet.Tables["report"].Clear();// -
                dataAdapter.Fill(dataSet, "report");// -
                dataGridView7.DataSource = dataSet.Tables["report"]; //2 -
                dataGridView7.Columns[7].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.ColumnIndex==7)// -
                {
                    string task = dataGridView7.Rows[e.RowIndex].Cells[7].Value.ToString(); // 2 -
                    if (task=="Delete")
                    {
                        if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView7.Rows.RemoveAt(rowIndex); // -
                            dataSet.Tables["report"].Rows[rowIndex].Delete(); // -
                            dataAdapter.Update(dataSet, "report"); // -
                        }
                    }
                    else if (task=="Insert")
                    {
                        try
                        {
                            dataGridView7.Columns[7].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
                            int rowIndex = dataGridView7.Rows.Count-2;// -
                            DataRow row = dataSet.Tables["report"].NewRow();// -

                            row["othet_id"] = dataGridView7.Rows[rowIndex].Cells["othet_id"].Value;//2 -
                            row["profit"] = dataGridView7.Rows[rowIndex].Cells["profit"].Value;//2 -
                            row["date"] = dataGridView7.Rows[rowIndex].Cells["date"].Value;//2 -
                            row["food_seles"] = dataGridView7.Rows[rowIndex].Cells["food_seles"].Value;//2 -
                            row["rashod"] = dataGridView7.Rows[rowIndex].Cells["rashod"].Value;//2 -
                            row["sales_id"] = dataGridView7.Rows[rowIndex].Cells["sales_id"].Value;//2 -
                            row["workgroup_id"] = dataGridView7.Rows[rowIndex].Cells["workgroup_id"].Value;//2 -    

                            dataSet.Tables["report"].Rows.Add(row); // -
                            dataSet.Tables["report"].Rows.RemoveAt(dataSet.Tables["report"].Rows.Count-1); //2 -
                            dataGridView7.Rows.RemoveAt(dataGridView7.Rows.Count-2);//2 -
                            dataGridView7.Rows[e.RowIndex].Cells[7].Value = "Delete";//2 -

                            dataAdapter.Update(dataSet, "report"); // -
                            newRowAdding = false;
                        }
                        catch
                        {
                            newRowAdding=false;
                            MessageBox.Show("Заполните все поля", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if (task=="Update")
                    {
                        dataGridView7.Columns[7].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                        int r = e.RowIndex;
                        dataSet.Tables["report"].Rows[r]["othet_id"]=dataGridView7.Rows[r].Cells["othet_id"].Value;//4  -
                        dataSet.Tables["report"].Rows[r]["profit"]=dataGridView7.Rows[r].Cells["profit"].Value;//4-
                        dataSet.Tables["report"].Rows[r]["date"]=dataGridView7.Rows[r].Cells["date"].Value;//4 -
                        dataSet.Tables["report"].Rows[r]["food_seles"]=dataGridView7.Rows[r].Cells["food_seles"].Value;//4 -
                        dataSet.Tables["report"].Rows[r]["rashod"]=dataGridView7.Rows[r].Cells["rashod"].Value;//4  -
                        dataSet.Tables["report"].Rows[r]["sales_id"]=dataGridView7.Rows[r].Cells["sales_id"].Value;//4  -
                        dataSet.Tables["report"].Rows[r]["workgroup_id"]=dataGridView7.Rows[r].Cells["workgroup_id"].Value;//4 -
                        dataAdapter.Update(dataSet, "report"); // -
                        dataGridView7.Rows[e.RowIndex].Cells[7].Value = "Delete";//2 -
                    }
                    ReloadData7(); // -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView7_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

            try
            {
                if (newRowAdding ==false)
                {
                    newRowAdding=true;
                    int lastRow = dataGridView7.Rows.Count-2;// -
                    DataGridViewRow row = dataGridView7.Rows[lastRow];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    row.Cells["Command"].Value = "Insert";
                    dataGridView7.Columns[7].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView7_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (newRowAdding==false)
                {
                    int rowIndex = dataGridView7.SelectedCells[0].RowIndex;// -
                    DataGridViewRow editingRow = dataGridView7.Rows[rowIndex];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    editingRow.Cells["Command"].Value = "Update";
                    dataGridView7.Columns[7].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox7.SelectedIndex==0)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button8_Click(button8, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button2_Click(button2, null); };
                tabControl1.SelectedTab=tabPage1;
                LoadData();
                button2.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox7.SelectedIndex==1)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button1_Click_2(button1, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button6_Click(button6, null); };
                tabControl1.SelectedTab=tabPage2;
                LoadData2();
                button6.Visible=false;
                label1.Visible=false;
            }
            else if (comboBox7.SelectedIndex==2)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button14_Click(button14, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button13_Click(button13, null); };
                tabControl1.SelectedTab=tabPage3;
                LoadData3(); // -
                button13.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox7.SelectedIndex==3)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button16_Click(button16, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button9_Click(button9, null); };
                tabControl1.SelectedTab=tabPage4;
                LoadData4(); // -
                label4.Visible=false;
                button9.Visible = false;
            }
            else if (comboBox7.SelectedIndex==4)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button20_Click(button20, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button17_Click(button17, null); };
                tabControl1.SelectedTab=tabPage5;
                LoadData5(); // -
                label5.Visible=false;
                button17.Visible = false;
            }
            else if (comboBox7.SelectedIndex==5)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button24_Click(button24, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button21_Click(button21, null); };
                tabControl1.SelectedTab=tabPage6;
                LoadData6(); // -
                label6.Visible=false;
                button21.Visible = false;
            }
            else if (comboBox7.SelectedIndex==6)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button25_Click(button25, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button26_Click(button26, null); };
                tabControl1.SelectedTab=tabPage7;
                LoadData7();
                label7.Visible=false;
                button26.Visible=false;
            }
            else if (comboBox7.SelectedIndex==7)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button30_Click(button30, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button29_Click(button29, null); };
                tabControl1.SelectedTab=tabPage8;
                LoadData8();
                label8.Visible=false;
                button29.Visible=false;
            }
            else if (comboBox7.SelectedIndex==8)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button34_Click(button34, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button33_Click(button33, null); };
                tabControl1.SelectedTab=tabPage9;
                LoadData9();
                label9.Visible=false;
                button33.Visible=false;
            }
            else if (comboBox7.SelectedIndex==9)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button38_Click(button38, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button37_Click(button37, null); };
                tabControl1.SelectedTab=tabPage10;
                LoadData10();
                label10.Visible=false;
                button37.Visible=false;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            LoadData7();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            LoadData7();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button25_Click(object sender, EventArgs e)
        {

            label7.Visible = true; // -
            button26.Visible=true; // -
            if (textBox7.TextLength==0) { return; } // -
            dataAdapter = new NpgsqlDataAdapter($"SELECT *, 'Delete' as Command FROM report where othet_id ={textBox7.Text} or profit ={textBox7.Text} or date like '{textBox7.Text}' or food_seles ={textBox7.Text} or rashod ={textBox7.Text} or sales_id ={textBox7.Text} or workgroup_id ={textBox7.Text}", conn); // -
            textBox7.Clear(); // -
            cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
            cmdBuilder.GetInsertCommand();
            cmdBuilder.GetUpdateCommand();
            cmdBuilder.GetDeleteCommand();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "report");// -
            dataGridView7.DataSource = dataSet.Tables["report"]; //2 -
                                                                    //Удалил
            dataGridView7.Columns[7].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView7_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress5);
            if (dataGridView7.CurrentCell.ColumnIndex==0 || dataGridView7.CurrentCell.ColumnIndex==1 || dataGridView7.CurrentCell.ColumnIndex==3 || dataGridView7.CurrentCell.ColumnIndex==4 || dataGridView7.CurrentCell.ColumnIndex==5 || dataGridView7.CurrentCell.ColumnIndex==6)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox!=null)
                {
                    textBox.KeyPress+= new KeyPressEventHandler(Column_KeyPress5);
                }
            }
        }
        private void Column_KeyPress5(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion
        #region workgroup
        private void tabPage8_Click(object sender, EventArgs e)
        {

        }
        private void LoadData8()
        {
            button29.Visible = false;
            label8.Visible = false;
            try
            {
                dataAdapter = new NpgsqlDataAdapter("SELECT *, 'Delete' as Command FROM workgroup", conn); // -
                cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
                cmdBuilder.GetInsertCommand();
                cmdBuilder.GetUpdateCommand();
                cmdBuilder.GetDeleteCommand();
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "workgroup");// -
                dataGridView8.DataSource = dataSet.Tables["workgroup"]; //2 -
                //this.dataGridView4.Columns[0].Visible = false; ---------------------------------
                //this.dataGridView4.Columns[3].Visible = false;
                //Удалил
                dataGridView8.Columns[6].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReloadData8()
        {
            try
            {
                dataSet.Tables["workgroup"].Clear();// -
                dataAdapter.Fill(dataSet, "workgroup");// -
                dataGridView8.DataSource = dataSet.Tables["workgroup"]; //2 -
                dataGridView8.Columns[6].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            try
            {
                if (e.ColumnIndex==6)// -
                {
                    string task = dataGridView8.Rows[e.RowIndex].Cells[6].Value.ToString(); // 2 -
                    if (task=="Delete")
                    {
                        if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView8.Rows.RemoveAt(rowIndex); // -
                            dataSet.Tables["workgroup"].Rows[rowIndex].Delete(); // -
                            dataAdapter.Update(dataSet, "workgroup"); // -
                        }
                    }
                    else if (task=="Insert")
                    {
                        try
                        {
                            dataGridView8.Columns[6].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
                            int rowIndex = dataGridView8.Rows.Count-2;// -
                            DataRow row = dataSet.Tables["workgroup"].NewRow();// -

                            row["workgroup_Id"] = dataGridView8.Rows[rowIndex].Cells["workgroup_Id"].Value;//2 -
                            row["date"] = dataGridView8.Rows[rowIndex].Cells["date"].Value;//2 -
                            row["price"] = dataGridView8.Rows[rowIndex].Cells["price"].Value;//2 -
                            row["graph_id"] = dataGridView8.Rows[rowIndex].Cells["graph_id"].Value;//2 -
                            row["peshka_id"] = dataGridView8.Rows[rowIndex].Cells["peshka_id"].Value;//2 -
                            row["date_id"] = dataGridView8.Rows[rowIndex].Cells["date_id"].Value;//2 - 

                            dataSet.Tables["workgroup"].Rows.Add(row); // -
                            dataSet.Tables["workgroup"].Rows.RemoveAt(dataSet.Tables["workgroup"].Rows.Count-1); //2 -
                            dataGridView8.Rows.RemoveAt(dataGridView8.Rows.Count-2);//2 -
                            dataGridView8.Rows[e.RowIndex].Cells[6].Value = "Delete";//2 -

                            dataAdapter.Update(dataSet, "workgroup"); // -
                            newRowAdding = false;
                        }
                        catch
                        {
                            newRowAdding=false;
                            MessageBox.Show($"Заполните все поля", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if (task=="Update")
                    {
                        dataGridView8.Columns[6].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                        int r = e.RowIndex;
                        dataSet.Tables["workgroup"].Rows[r]["workgroup_Id"]=dataGridView8.Rows[r].Cells["workgroup_Id"].Value;//4  -
                        dataSet.Tables["workgroup"].Rows[r]["date"]=dataGridView8.Rows[r].Cells["date"].Value;//4-
                        dataSet.Tables["workgroup"].Rows[r]["price"]=dataGridView8.Rows[r].Cells["price"].Value;//4 -
                        dataSet.Tables["workgroup"].Rows[r]["graph_id"]=dataGridView8.Rows[r].Cells["graph_id"].Value;//4 -
                        dataSet.Tables["workgroup"].Rows[r]["peshka_id"]=dataGridView8.Rows[r].Cells["peshka_id"].Value;//4  -
                        dataSet.Tables["workgroup"].Rows[r]["date_id"]=dataGridView8.Rows[r].Cells["date_id"].Value;//4  -
                        dataAdapter.Update(dataSet, "workgroup"); // -
                        dataGridView8.Rows[e.RowIndex].Cells[6].Value = "Delete";//2 -
                    }
                    ReloadData8(); // -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView8_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

            try
            {
                if (newRowAdding ==false)
                {
                    newRowAdding=true;
                    int lastRow = dataGridView8.Rows.Count-2;// -
                    DataGridViewRow row = dataGridView8.Rows[lastRow];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    row.Cells["Command"].Value = "Insert";
                    dataGridView8.Columns[6].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView8_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (newRowAdding==false)
                {
                    int rowIndex = dataGridView8.SelectedCells[0].RowIndex;// -
                    DataGridViewRow editingRow = dataGridView8.Rows[rowIndex];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    editingRow.Cells["Command"].Value = "Update";
                    dataGridView8.Columns[6].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox8.SelectedIndex==0)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button8_Click(button8, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button2_Click(button2, null); };
                tabControl1.SelectedTab=tabPage1;
                LoadData();
                button2.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox8.SelectedIndex==1)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button1_Click_2(button1, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button6_Click(button6, null); };
                tabControl1.SelectedTab=tabPage2;
                LoadData2();
                button6.Visible=false;
                label1.Visible=false;
            }
            else if (comboBox8.SelectedIndex==2)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button14_Click(button14, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button13_Click(button13, null); };
                tabControl1.SelectedTab=tabPage3;
                LoadData3(); // -
                button13.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox8.SelectedIndex==3)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button16_Click(button16, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button9_Click(button9, null); };
                tabControl1.SelectedTab=tabPage4;
                LoadData4(); // -
                label4.Visible=false;
                button9.Visible = false;
            }
            else if (comboBox8.SelectedIndex==4)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button20_Click(button20, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button17_Click(button17, null); };
                tabControl1.SelectedTab=tabPage5;
                LoadData5(); // -
                label5.Visible=false;
                button17.Visible = false;
            }
            else if (comboBox8.SelectedIndex==5)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button24_Click(button24, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button21_Click(button21, null); };
                tabControl1.SelectedTab=tabPage6;
                LoadData6(); // -
                label6.Visible=false;
                button21.Visible = false;
            }
            else if (comboBox8.SelectedIndex==6)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button25_Click(button25, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button26_Click(button26, null); };
                tabControl1.SelectedTab=tabPage7;
                LoadData7();
                label7.Visible=false;
                button26.Visible=false;
            }
            else if (comboBox8.SelectedIndex==7)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button30_Click(button30, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button29_Click(button29, null); };
                tabControl1.SelectedTab=tabPage8;
                LoadData8();
                label8.Visible=false;
                button29.Visible=false;
            }
            else if (comboBox8.SelectedIndex==8)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button34_Click(button34, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button33_Click(button33, null); };
                tabControl1.SelectedTab=tabPage9;
                LoadData9();
                label9.Visible=false;
                button33.Visible=false;
            }
            else if (comboBox8.SelectedIndex==9)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button38_Click(button38, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button37_Click(button37, null); };
                tabControl1.SelectedTab=tabPage10;
                LoadData10();
                label10.Visible=false;
                button37.Visible=false;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            LoadData8();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            LoadData8();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button30_Click(object sender, EventArgs e)
        {


            label8.Visible = true; // -
            button29.Visible=true; // -
            if (textBox8.TextLength==0) { return; } // -
            dataAdapter = new NpgsqlDataAdapter($"SELECT *, 'Delete' as Command FROM workgroup where workgroup_id ={textBox8.Text} or date like '{textBox8.Text}' or price ={textBox8.Text} or graph_id ={textBox8.Text} or peshka_id ={textBox8.Text} or date_id ={textBox8.Text}", conn); // -
            textBox8.Clear(); // -
            cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
            cmdBuilder.GetInsertCommand();
            cmdBuilder.GetUpdateCommand();
            cmdBuilder.GetDeleteCommand();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "workgroup");// -
            dataGridView8.DataSource = dataSet.Tables["workgroup"]; //2 -
                                                                    //Удалил
            dataGridView8.Columns[6].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
        }
        private void dataGridView8_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress6);
            if (dataGridView8.CurrentCell.ColumnIndex==0 || dataGridView8.CurrentCell.ColumnIndex==2 || dataGridView8.CurrentCell.ColumnIndex==3 || dataGridView8.CurrentCell.ColumnIndex==4 || dataGridView8.CurrentCell.ColumnIndex==5)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox!=null)
                {
                    textBox.KeyPress+= new KeyPressEventHandler(Column_KeyPress6);
                }
            }
        }
        private void Column_KeyPress6(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion
        #region peshka
        private void tabPage9_Click(object sender, EventArgs e)
        {

        }
        private void LoadData9()
        {
            button33.Visible = false;
            label9.Visible = false;
            try
            {
                dataAdapter = new NpgsqlDataAdapter("SELECT *, 'Delete' as Command FROM peshka", conn); // -
                cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
                cmdBuilder.GetInsertCommand();
                cmdBuilder.GetUpdateCommand();
                cmdBuilder.GetDeleteCommand();
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "peshka");// -
                dataGridView9.DataSource = dataSet.Tables["peshka"]; //2 -
                //this.dataGridView4.Columns[0].Visible = false; ---------------------------------
                //this.dataGridView4.Columns[3].Visible = false;
                //Удалил
                dataGridView9.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReloadData9()
        {
            try
            {
                dataSet.Tables["peshka"].Clear();// -
                dataAdapter.Fill(dataSet, "peshka");// -
                dataGridView9.DataSource = dataSet.Tables["peshka"]; //2 -
                dataGridView9.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView9_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex==4)// -
                {
                    string task = dataGridView9.Rows[e.RowIndex].Cells[4].Value.ToString(); // 2 -
                    if (task=="Delete")
                    {
                        if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView9.Rows.RemoveAt(rowIndex); // -
                            dataSet.Tables["peshka"].Rows[rowIndex].Delete(); // -
                            dataAdapter.Update(dataSet, "peshka"); // -
                        }
                    }
                    else if (task=="Insert")
                    {
                        try
                        {
                            dataGridView9.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
                            int rowIndex = dataGridView9.Rows.Count-2;// -
                            DataRow row = dataSet.Tables["peshka"].NewRow();// -

                            row["f_i_o"] = dataGridView9.Rows[rowIndex].Cells["f_i_o"].Value;//2 -
                            row["pasport"] = dataGridView9.Rows[rowIndex].Cells["pasport"].Value;//2 -
                            row["peshka_Id"] = dataGridView9.Rows[rowIndex].Cells["peshka_Id"].Value;//2 -
                            row["price"] = dataGridView9.Rows[rowIndex].Cells["price"].Value;//2 -

                            dataSet.Tables["peshka"].Rows.Add(row); // -
                            dataSet.Tables["peshka"].Rows.RemoveAt(dataSet.Tables["peshka"].Rows.Count-1); //2 -
                            dataGridView9.Rows.RemoveAt(dataGridView9.Rows.Count-2);//2 -
                            dataGridView9.Rows[e.RowIndex].Cells[4].Value = "Delete";//2 -

                            dataAdapter.Update(dataSet, "peshka"); // -
                            newRowAdding = false;
                        }
                        catch
                        {
                            newRowAdding=false;
                            MessageBox.Show("Заполните все поля", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if (task=="Update")
                    {
                        dataGridView9.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                        int r = e.RowIndex;
                        dataSet.Tables["peshka"].Rows[r]["f_i_o"]=dataGridView9.Rows[r].Cells["f_i_o"].Value;//4  -
                        dataSet.Tables["peshka"].Rows[r]["pasport"]=dataGridView9.Rows[r].Cells["pasport"].Value;//4-
                        dataSet.Tables["peshka"].Rows[r]["peshka_Id"]=dataGridView9.Rows[r].Cells["peshka_Id"].Value;//4 -
                        dataSet.Tables["peshka"].Rows[r]["price"]=dataGridView9.Rows[r].Cells["price"].Value;//4 -
                        dataAdapter.Update(dataSet, "peshka"); // -
                        dataGridView9.Rows[e.RowIndex].Cells[4].Value = "Delete";//2 -
                    }
                    ReloadData9(); // -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView9_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

            try
            {
                if (newRowAdding ==false)
                {
                    newRowAdding=true;
                    int lastRow = dataGridView9.Rows.Count-2;// -
                    DataGridViewRow row = dataGridView9.Rows[lastRow];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    row.Cells["Command"].Value = "Insert";
                    dataGridView9.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView9_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (newRowAdding==false)
                {
                    int rowIndex = dataGridView9.SelectedCells[0].RowIndex;// -
                    DataGridViewRow editingRow = dataGridView9.Rows[rowIndex];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    editingRow.Cells["Command"].Value = "Update";
                    dataGridView9.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox9.SelectedIndex==0)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button8_Click(button8, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button2_Click(button2, null); };
                tabControl1.SelectedTab=tabPage1;
                LoadData();
                button2.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox9.SelectedIndex==1)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button1_Click_2(button1, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button6_Click(button6, null); };
                tabControl1.SelectedTab=tabPage2;
                LoadData2();
                button6.Visible=false;
                label1.Visible=false;
            }
            else if (comboBox9.SelectedIndex==2)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button14_Click(button14, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button13_Click(button13, null); };
                tabControl1.SelectedTab=tabPage3;
                LoadData3(); // -
                button13.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox9.SelectedIndex==3)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button16_Click(button16, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button9_Click(button9, null); };
                tabControl1.SelectedTab=tabPage4;
                LoadData4(); // -
                label4.Visible=false;
                button9.Visible = false;
            }
            else if (comboBox9.SelectedIndex==4)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button20_Click(button20, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button17_Click(button17, null); };
                tabControl1.SelectedTab=tabPage5;
                LoadData5(); // -
                label5.Visible=false;
                button17.Visible = false;
            }
            else if (comboBox9.SelectedIndex==5)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button24_Click(button24, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button21_Click(button21, null); };
                tabControl1.SelectedTab=tabPage6;
                LoadData6(); // -
                label6.Visible=false;
                button21.Visible = false;
            }
            else if (comboBox9.SelectedIndex==6)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button25_Click(button25, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button26_Click(button26, null); };
                tabControl1.SelectedTab=tabPage7;
                LoadData7();
                label7.Visible=false;
                button26.Visible=false;
            }
            else if (comboBox9.SelectedIndex==7)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button30_Click(button30, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button29_Click(button29, null); };
                tabControl1.SelectedTab=tabPage8;
                LoadData8();
                label8.Visible=false;
                button29.Visible=false;
            }
            else if (comboBox9.SelectedIndex==8)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button34_Click(button34, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button33_Click(button33, null); };
                tabControl1.SelectedTab=tabPage9;
                LoadData9();
                label9.Visible=false;
                button33.Visible=false;
            }
            else if (comboBox9.SelectedIndex==9)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button38_Click(button38, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button37_Click(button37, null); };
                tabControl1.SelectedTab=tabPage10;
                LoadData10();
                label10.Visible=false;
                button37.Visible=false;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            LoadData9();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            LoadData9();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button34_Click(object sender, EventArgs e)
        {
            button33.Visible = true;
            label9.Visible = true;
            // Если что удалить Если не чего не делает button6.Visible=true;
            if (textBox9.TextLength==0) { return; }
            dataAdapter = new NpgsqlDataAdapter($"SELECT *, 'Delete' as Command FROM peshka where f_i_o like '{textBox9.Text}' or pasport ={textBox9.Text} or peshka_id ={textBox9.Text} or price ={textBox9.Text}", conn); // -
            textBox9.Clear();
            textBox9.Clear();
            cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
            cmdBuilder.GetInsertCommand();
            cmdBuilder.GetUpdateCommand();
            cmdBuilder.GetDeleteCommand();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "peshka");// -
            dataGridView9.DataSource = dataSet.Tables["peshka"]; //2 -
                                                                 //Удалил
            dataGridView9.Columns[4].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
        }
        private void dataGridView9_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress7);
            if (dataGridView9.CurrentCell.ColumnIndex==1 || dataGridView9.CurrentCell.ColumnIndex==2 || dataGridView9.CurrentCell.ColumnIndex==3)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox!=null)
                {
                    textBox.KeyPress+= new KeyPressEventHandler(Column_KeyPress7);
                }
            }
        }
        private void Column_KeyPress7(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion
        #region graph_per_day
        private void tabPage10_Click(object sender, EventArgs e)
        {

        }
        private void LoadData10()
        {
            button37.Visible = false;
            label10.Visible = false;
            try
            {
                dataAdapter = new NpgsqlDataAdapter("SELECT *, 'Delete' as Command FROM graph_per_day", conn); // -
                cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
                cmdBuilder.GetInsertCommand();
                cmdBuilder.GetUpdateCommand();
                cmdBuilder.GetDeleteCommand();
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "graph_per_day");// -
                dataGridView10.DataSource = dataSet.Tables["graph_per_day"]; //2 -
                //this.dataGridView4.Columns[0].Visible = false; ---------------------------------
                //this.dataGridView4.Columns[3].Visible = false;
                //Удалил
                dataGridView10.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReloadData10()
        {
            try
            {
                dataSet.Tables["graph_per_day"].Clear();// -
                dataAdapter.Fill(dataSet, "graph_per_day");// -
                dataGridView10.DataSource = dataSet.Tables["graph_per_day"]; //2 -
                dataGridView10.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView10_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.ColumnIndex==3)// -
                {
                    string task = dataGridView10.Rows[e.RowIndex].Cells[3].Value.ToString(); // 2 -
                    if (task=="Delete")
                    {
                        if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView10.Rows.RemoveAt(rowIndex); // -
                            dataSet.Tables["graph_per_day"].Rows[rowIndex].Delete(); // -
                            dataAdapter.Update(dataSet, "graph_per_day"); // -
                        }
                    }
                    else if (task=="Insert")
                    {
                        try
                        {
                            dataGridView10.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
                            int rowIndex = dataGridView10.Rows.Count-2;// -
                            DataRow row = dataSet.Tables["graph_per_day"].NewRow();// -

                            row["date"] = dataGridView10.Rows[rowIndex].Cells["date"].Value;//2 -
                            row["graph_id"] = dataGridView10.Rows[rowIndex].Cells["graph_id"].Value;//2 -
                            row["time"] = dataGridView10.Rows[rowIndex].Cells["time"].Value;//2 -

                            dataSet.Tables["graph_per_day"].Rows.Add(row); // -
                            dataSet.Tables["graph_per_day"].Rows.RemoveAt(dataSet.Tables["graph_per_day"].Rows.Count-1); //2 -
                            dataGridView10.Rows.RemoveAt(dataGridView10.Rows.Count-2);//2 -
                            dataGridView10.Rows[e.RowIndex].Cells[3].Value = "Delete";//2 -

                            dataAdapter.Update(dataSet, "graph_per_day"); // -
                            newRowAdding = false;
                        }
                        catch
                        {
                            newRowAdding=false;
                            MessageBox.Show("Заполните все поля", "Ошибка ввода данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if (task=="Update")
                    {
                        dataGridView10.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                        int r = e.RowIndex;
                        dataSet.Tables["graph_per_day"].Rows[r]["date"]=dataGridView10.Rows[r].Cells["date"].Value;//4  -
                        dataSet.Tables["graph_per_day"].Rows[r]["graph_id"]=dataGridView10.Rows[r].Cells["graph_id"].Value;//4-
                        dataSet.Tables["graph_per_day"].Rows[r]["time"]=dataGridView10.Rows[r].Cells["time"].Value;//4 -
                        dataAdapter.Update(dataSet, "graph_per_day"); // -
                        dataGridView10.Rows[e.RowIndex].Cells[3].Value = "Delete";//2 -
                    }
                    ReloadData10(); // -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView10_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

            try
            {
                if (newRowAdding ==false)
                {
                    newRowAdding=true;
                    int lastRow = dataGridView10.Rows.Count-2;// -
                    DataGridViewRow row = dataGridView10.Rows[lastRow];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    row.Cells["Command"].Value = "Insert";
                    dataGridView10.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView10_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (newRowAdding==false)
                {
                    int rowIndex = dataGridView10.SelectedCells[0].RowIndex;// -
                    DataGridViewRow editingRow = dataGridView5.Rows[rowIndex];// -
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    editingRow.Cells["Command"].Value = "Update";
                    dataGridView10.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue;//2 -
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            LoadData10();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            LoadData10();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button38_Click(object sender, EventArgs e)
        {

            button37.Visible = true;
            label10.Visible = true;
            // Если что удалить Если не чего не делает button6.Visible=true;
            if (textBox10.TextLength==0) { return; }
            dataAdapter = new NpgsqlDataAdapter($"SELECT *, 'Delete' as Command FROM graph_per_day where date like '{textBox10.Text}' or graph_id = {textBox10.Text} or time = {textBox10.Text}", conn); // -
            textBox10.Clear();
            cmdBuilder = new NpgsqlCommandBuilder(dataAdapter);
            cmdBuilder.GetInsertCommand();
            cmdBuilder.GetUpdateCommand();
            cmdBuilder.GetDeleteCommand();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "graph_per_day");// -
            dataGridView10.DataSource = dataSet.Tables["graph_per_day"]; //2 -
                                                               //Удалил
            dataGridView10.Columns[3].DefaultCellStyle.ForeColor = Color.SlateBlue; //2 -
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox10.SelectedIndex==0)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button8_Click(button8, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button2_Click(button2, null); };
                tabControl1.SelectedTab=tabPage1;
                LoadData();
                button2.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox10.SelectedIndex==1)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button1_Click_2(button1, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button6_Click(button6, null); };
                tabControl1.SelectedTab=tabPage2;
                LoadData2();
                button6.Visible=false;
                label1.Visible=false;
            }
            else if (comboBox10.SelectedIndex==2)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button14_Click(button14, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button13_Click(button13, null); };
                tabControl1.SelectedTab=tabPage3;
                LoadData3(); // -
                button13.Visible=false;
                label2.Visible=false;
            }
            else if (comboBox10.SelectedIndex==3)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button16_Click(button16, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button9_Click(button9, null); };
                tabControl1.SelectedTab=tabPage4;
                LoadData4(); // -
                label4.Visible=false;
                button9.Visible = false;
            }
            else if (comboBox10.SelectedIndex==4)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button20_Click(button20, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button17_Click(button17, null); };
                tabControl1.SelectedTab=tabPage5;
                LoadData5(); // -
                label5.Visible=false;
                button17.Visible = false;
            }
            else if (comboBox10.SelectedIndex==5)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button24_Click(button24, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button21_Click(button21, null); };
                tabControl1.SelectedTab=tabPage6;
                LoadData6(); // -
                label6.Visible=false;
                button21.Visible = false;
            }
            else if (comboBox10.SelectedIndex==6)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button25_Click(button25, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button26_Click(button26, null); };
                tabControl1.SelectedTab=tabPage7;
                LoadData7();
                label7.Visible=false;
                button26.Visible=false;
            }
            else if (comboBox10.SelectedIndex==7)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button30_Click(button30, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button29_Click(button29, null); };
                tabControl1.SelectedTab=tabPage8;
                LoadData8();
                label8.Visible=false;
                button29.Visible=false;
            }
            else if (comboBox10.SelectedIndex==8)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button34_Click(button34, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button33_Click(button33, null); };
                tabControl1.SelectedTab=tabPage9;
                LoadData9();
                label9.Visible=false;
                button33.Visible=false;
            }
            else if (comboBox10.SelectedIndex==9)
            {
                KeyPreview=true;
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Enter) button38_Click(button38, null); };
                KeyDown+=(s, e) => { if (e.KeyValue==(char)Keys.Escape) button37_Click(button37, null); };
                tabControl1.SelectedTab=tabPage10;
                LoadData10();
                label10.Visible=false;
                button37.Visible=false;
            }
        }
        private void dataGridView10_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress8);
            if (dataGridView10.CurrentCell.ColumnIndex==1 || dataGridView10.CurrentCell.ColumnIndex==2)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox!=null)
                {
                    textBox.KeyPress+= new KeyPressEventHandler(Column_KeyPress8);
                }
            }
        }
        private void Column_KeyPress8(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}