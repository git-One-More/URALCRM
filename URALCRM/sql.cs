using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace URALCRM
{
    public partial class sql : Form
    {
        public sql()
        {
            InitializeComponent();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            groupBox1.Location = new Point((ClientSize.Width - groupBox1.Width) / 2, (ClientSize.Height - groupBox1.Height) / 2);
        }
        private void sql_Load(object sender, EventArgs e)
        {
            this.Text = "uralCRM: Version 1.0/ sqldev";
            comboBox1.Items.Add("ACCES");
            comboBox2.Items.Add("Jet.OLEDB.4.0");
            comboBox2.Items.Add("ACE.OLEDB.12.0");
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            textBox1.Visible = false;
            button3.Visible=false;
            button4.Visible=false;
            button5.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
           

        }

     

        private void button1_Click_1(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            textBox1.Visible = true;
            button3.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            button4.Visible = true;
            button5.Visible = true;
            label3.Visible = true;
            textBox2.Visible = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "база данных ACCES(*.mdb)|*.mdb|Все файлы (*.*)| *.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            textBox1.Visible = false;
            button3.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            textBox1.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form main = new main();
            main.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Создание config файла для подключения
            const string caption = "Внимание!";
            var result = MessageBox.Show("Вы хотите создать новое подключение с именем: "+textBox2.Text+". Продолжить?", caption,
                                MessageBoxButtons.OKCancel,
             MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                groupBox1.Visible = false;
                string path = Directory.GetCurrentDirectory();
                FileStream fs = new FileStream($@"{path}\{textBox2.Text}.config", FileMode.CreateNew);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("Provider = Microsoft."+comboBox2.SelectedItem.ToString()+"; Data Source ="+textBox1.Text);
                sw.Close();
                fs.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }
    }
}
