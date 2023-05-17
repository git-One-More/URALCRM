using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace URALCRM
{
    public partial class Form1 : Form
    {
        int chose=0;
        int ind;
        int io=0;
    protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            groupBox1.Location = new Point((ClientSize.Width - groupBox1.Width) / 2, (ClientSize.Height - groupBox1.Height) / 2);
        }
        public Form1()
        {
            InitializeComponent();
            {
                
              
            }
            
        }


        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Black;
            button1.ForeColor = Color.White;
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
        }
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.Black;
            button3.ForeColor = Color.White;
        }
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            Conn.conn.Open();          
            OleDbCommand thisCommand = Conn.conn.CreateCommand();           
            thisCommand.CommandText = "SELECT * FROM users WHERE email = :1";           
            thisCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = textBox2.Text;
                OleDbDataReader thisReader = thisCommand.ExecuteReader();
            string log = string.Empty;
            string pas = string.Empty;           
            while (thisReader.Read())
            {
                StaticData.login+= thisReader["fio"];
                    log+= thisReader["email"];
                pas += thisReader["password"];
            }
            thisReader.Close();
            Conn.conn.Close();
           if (textBox2.Text == log && textBox3.Text == pas){  
            main main = new main();
             main.Show();
             this.Hide();
              
        }
        else
            {
                chose += 1;
                const string caption = "Ошибка №1";
                var result = MessageBox.Show("Вы не верно ввели логин или пароль! попробуйте еще раз", caption,
                                    MessageBoxButtons.OK,
                 MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                   
                    textBox3.Text = "";
                }
                if (chose ==4)
                {
                    
                    var result1 = MessageBox.Show("Вы не верно ввели логин или пароль! попробуйте еще раз", "Смени пароль!",
                                        MessageBoxButtons.OKCancel,
                     MessageBoxIcon.Error);
                    if (result1 == DialogResult.OK)
                    {
                        //Добавить форму востановления пароля
                        
                    }

                }
            }
        }
        private void button1_Focus(object sender, EventArgs e)
        {   
          
        }
        private void textBox2_Focus(object sender, EventArgs e)
        {
          
           
            textBox2.ForeColor = Color.Black;
        }


        private void textBox3_Focus(object sender, EventArgs e)
        {
        
            textBox3.ForeColor = Color.Black;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = true;
            Conn.conn.Open();
            OleDbCommand PUK = Conn.conn.CreateCommand();
            PUK.CommandText = "SELECT COUNT (FIO) FROM USERS";
            OleDbDataReader seqreadr = PUK.ExecuteReader();
            string sequence = string.Empty;
            while (seqreadr.Read())
            {
                sequence += seqreadr[0];
            }
            int g;
            g = Convert.ToInt32(sequence);
            if (g == 1)
            {
                button3.Visible = false;
                chose = 1;
                OleDbCommand zapom = Conn.conn.CreateCommand();
                zapom.CommandText = "SELECT DONT_EXIT, email FROM USERS";
                OleDbDataReader zapreader = zapom.ExecuteReader();
                string zap = string.Empty;
                string zapem = string.Empty;
                while (zapreader.Read())
                {
                    zap += zapreader[0];
                    zapem += zapreader[1];
                }
             ind = Convert.ToInt32(zap);
                if (ind == 1)
                {
                    textBox2.Text = zapem;
                    textBox2.ForeColor = Color.Black;
                    radioButton1.Checked = true;
                    textBox3.Focus();
                }
            }
            else
            {
                textBox2.Visible = false;
                textBox3.Visible = false;
                button2.Visible = false;
                button1.Visible = false;
                radioButton1.Visible = false;

            }
            Conn.conn.Close();
            seqreadr.Close();

            if (textBox3.Text == "Пароль")
            {
                textBox3.UseSystemPasswordChar = false;
                io = 1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.UseSystemPasswordChar == true)
            {
                textBox3.UseSystemPasswordChar = false;
                button2.BackgroundImage = URALCRM.Properties.Resources.hide_icon_153458;


            }
            else if (textBox3.UseSystemPasswordChar == false)
            {
                textBox3.UseSystemPasswordChar = true;
                button2.BackgroundImage = button2.BackgroundImage = URALCRM.Properties.Resources.eye_show;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (io==1)
            {
                textBox3.UseSystemPasswordChar = true;
                io = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 reg = new Form3();
            reg.Show();
            this.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked==true && textBox2.Text!=""&& ind!=1)
            {
                Conn.conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.UpdateCommand = new OleDbCommand("UPDATE USERS SET DONT_EXIT = 1 Where email=:1", Conn.conn);
                adapter.UpdateCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = textBox2.Text;
                adapter.UpdateCommand.ExecuteNonQuery();
                adapter.Dispose();
                Conn.conn.Close();
            }
            if (radioButton1.Checked == false && textBox2.Text != "" && ind == 1)
            {
                Conn.conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.UpdateCommand = new OleDbCommand("UPDATE USERS SET DONT_EXIT = 0 Where email=:1", Conn.conn);
                adapter.UpdateCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = textBox2.Text;
                adapter.UpdateCommand.ExecuteNonQuery();
                adapter.Dispose();
                Conn.conn.Close();
            }
        }
    }
    }

