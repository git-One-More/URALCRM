using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URALCRM
{
    public partial class Form1 : Form
    {         
    protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            groupBox1.Location = new Point((ClientSize.Width - groupBox1.Width) / 2, (ClientSize.Height - groupBox1.Height) / 2);
        }
        public Form1()
        {
            InitializeComponent();
            {
                
                button2.BackgroundImage = URALCRM.Properties.Resources.eye_show_filled_icon_200617;
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
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Focus(object sender, EventArgs e)
        {   
          
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

        private void Form1_Load(object sender, EventArgs e)
        {

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

        
    }
    }

