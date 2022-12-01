using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URALCRM
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
            GraphicsPath gp = new GraphicsPath();
            Graphics g = CreateGraphics();
            // Создадим новый прямоугольник с размерами кнопки 
            Rectangle smallRectangle = button1.ClientRectangle;
            // уменьшим размеры прямоугольника 
            smallRectangle.Inflate(-5, -5);
            // создадим эллипс, используя полученные размеры 
            gp.AddEllipse(smallRectangle);
            button1.Region = new Region(gp);
            // рисуем окантовоку для круглой кнопки 
           /*
            g.DrawEllipse(new Pen(Color.White, 0),
            button1.Left + 1,
            button1.Top + 1,
            button1.Width - 3,
            button1.Height - 3);
            // освобождаем ресурсы 
           */
            g.Dispose();
        }

        private void main_Load(object sender, EventArgs e)
        {
            this.Text = "uralCRM: Version 1.0 " + " User:admin " + DateTime.Now ;
            toolStrip1.Width = 0;
            dataGridView1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            button1.Visible = false;
            timer1.Start();
            timer1.Interval = 5;
            timer1.Tick += timer1_Tick;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (toolStrip1.Width<62)
                toolStrip1.Width++;
            else
        
            timer1.Stop();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            dataGridView1.Visible=true;
            timer2.Start();
            timer2.Interval = 1;
            timer2.Tick += timer2_Tick;
        }
        private void timer2_Tick(object sender, EventArgs e)
        { 
           
            if (toolStrip1.Width > 0)
                toolStrip1.Width--;
            else

                timer2.Stop();

        }

    }

  

}
