using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URALCRM
{
    public partial class Form3 : Form
    {
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            groupBox1.Location = new Point((ClientSize.Width - groupBox1.Width) / 2, (ClientSize.Height - groupBox1.Height) / 2);
        }
        public Form3()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Focus(object sender, EventArgs e)
        {

            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }
        private void textBox2_Focus(object sender, EventArgs e)
        {

            textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
        }
        private void textBox3_Focus(object sender, EventArgs e)
        {

            textBox3.Text = "";
            textBox3.ForeColor = Color.Black;
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

        private void Form3_Load(object sender, EventArgs e)
        {
            button2.BackgroundImage = URALCRM.Properties.Resources.eye_show_filled_icon_200617;
            textBox3.UseSystemPasswordChar = true;
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
                button2.BackgroundImage = URALCRM.Properties.Resources.eye_show_filled_icon_200617;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != null && textBox3.Text != null)
            {
                Conn.conn.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.InsertCommand = new OleDbCommand("insert into users VALUES(:1,:2,:3, 0)", Conn.conn);
                adapter.InsertCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = textBox1.Text;
                adapter.InsertCommand.Parameters.Add(":2", OleDbType.VarChar, 128).Value = textBox2.Text;
                adapter.InsertCommand.Parameters.Add(":3", OleDbType.VarChar, 128).Value = textBox3.Text;
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Пользователь зарегистрирован", "Регистрация",
                        MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                ds.Dispose();
                adapter.Dispose();
                Conn.conn.Close();
                Form1 aut = new Form1();
                aut.Show();
                this.Hide();
            }
        }
    }
}

