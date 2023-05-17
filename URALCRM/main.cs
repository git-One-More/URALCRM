using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace URALCRM
{
    public partial class main : Form
    {
        int par = 1;
        int pk = 0;
        DateTime start = new DateTime();
        DateTime finish = new DateTime();
        DateTime daystart = new DateTime();
        DateTime dayfinish = new DateTime();

     
        public int ms = 0, sec = 0, min = 0, hrs=0;
       public int oms = 0, osec = 0, omin = 0, ohrs = 0;
        public main()
        {
            InitializeComponent();
            
        }

        private void main_Load(object sender, EventArgs e)
        {
            toolStripButton5.Visible = false;
            this.Text = "uralCRM: Version 1.0/ ";
            toolStripLabel1.Text =  " User: "+StaticData.login;
           panel1.Visible = false;
            groupBox1.Visible = false;
          
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
        }


       

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.Dock = DockStyle.None;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox4.Text = string.Empty;
            dataGridView1.Visible=true;
            dataGridView2.Visible = false;
            if (par == 0)
            {
                toolStripButton5.Visible = false;
                toolStripButton5.Items.Clear();
                par = 1;
            }
            panel1.Visible = true;
            groupBox1.Visible = true;
            //data_in_datagridview
            {
                Conn.conn.Open();
                DataSet dataSet = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = new OleDbCommand("SELECT * FROM TASK", Conn.conn);
                adapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
                dataSet.Dispose();
                Conn.conn.Close();
                adapter.Dispose();
            }
            //data_in_textbox
            {
                Conn.conn.Open();
                OleDbCommand thisCommand = Conn.conn.CreateCommand();
                thisCommand.CommandText = "SELECT * FROM users WHERE fio = :1";
                thisCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = StaticData.login;
                OleDbDataReader thisReader = thisCommand.ExecuteReader();
                textBox1.Text = StaticData.login;
                while (thisReader.Read())
                {
                    textBox4.Text += thisReader[1];
                    textBox2.Text += thisReader[4];
                    textBox3.Text += thisReader[5];
                }
                thisReader.Close();
                Conn.conn.Close();
                return;
            }
        }
   

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            const string caption = "Закрытие приложения";
            var result = MessageBox.Show("Вы уверены, что хотите выйти?", caption,
                                MessageBoxButtons.OKCancel,
             MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = true;
            dataGridView1.Visible = false;
            panel1.Visible = false;
           if (par == 1)
         {
                toolStripButton5.Visible = true;
                string path = Directory.GetCurrentDirectory();
                DirectoryInfo d = new DirectoryInfo($@"{path}");
                FileInfo[] Files = d.GetFiles("*.dsql"); 
                foreach (FileInfo file in Files)
                {

                   string connamearr = Path.GetFileNameWithoutExtension(file.ToString());
                    toolStripButton5.Items.Add(connamearr);

                }
                par = 0;
                return;
           }
           if (par == 0|dataGridView2.Visible==false)
          {
                toolStripButton5.Visible = false;
             toolStripButton5.Items.Clear();
                par = 1;
          }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form sql = new sql();
            sql.Show();
            this.Hide();

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (toolStripButton5.SelectedText != "")
            {
                string path = Directory.GetCurrentDirectory();
                string sqlname = toolStripButton5.SelectedItem.ToString();


                string filetext = File.ReadAllText($@"{path}\{sqlname}.dsql");
                string comandtext = filetext.Substring(0, filetext.IndexOf('|'));

                string sqlcon = filetext.Substring(filetext.LastIndexOf('|', filetext.LastIndexOf("")));
                sqlcon = sqlcon.Substring(1);

                OleDbConnection savesqlcon = new OleDbConnection(sqlcon);
                savesqlcon.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = new OleDbCommand(comandtext, savesqlcon);
                adapter.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
                ds.Dispose();
                adapter.Dispose();
                savesqlcon.Close();
                const string caption = "Внимание!";
                var result = MessageBox.Show("Сохранить данные хочешь, или чисто посмотреть зашел?", caption,
                                    MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    MessageBox.Show("ну типо сохранил... нет)");
                }
            }

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            osec += 1;
            if (osec == 60)
            {
               osec = 00;
                min += 1;
            }
            if (omin == 60)
                omin = 00;
            timeN.Text = ohrs + ":" +omin + ":" + osec;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            start = DateTime.Now;

            if (statusN.Text == "На работе")
            {
                if (TaskName.Text == "Название задачи")
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {

                        TaskName.Text = "№" + row.Cells[0].Value.ToString() + ": " + row.Cells[1].Value.ToString();
                        Conn.conn.Open();
                        OleDbDataAdapter adapter = new OleDbDataAdapter();
                        OleDbCommand thisCommand = Conn.conn.CreateCommand();
                        thisCommand.CommandText = "SELECT MAX(PK) FROM PLAN_FACT";
                        OleDbDataReader thisReader = thisCommand.ExecuteReader();

                        while (thisReader.Read())
                        {
                            pk = Convert.ToInt32(thisReader[0]);
                        }
                        pk++;
                        adapter.InsertCommand = new OleDbCommand("INSERT INTO PLAN_FACT VALUES (:1,:2,:3,:4,:5,:6) ", Conn.conn);
                        adapter.InsertCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = pk;
                        adapter.InsertCommand.Parameters.Add(":2", OleDbType.VarChar, 128).Value = row.Cells[1].Value.ToString();
                        adapter.InsertCommand.Parameters.Add(":3", OleDbType.VarChar, 128).Value = start.ToString();
                        adapter.InsertCommand.Parameters.Add(":4", OleDbType.VarChar, 128).Value = DateTime.MaxValue.ToString();
                        adapter.InsertCommand.Parameters.Add(":5", OleDbType.VarChar, 128).Value = (DateTime.MaxValue.ToLocalTime() - start.ToLocalTime()).ToString(@"hh\:mm\:ss");
                        adapter.InsertCommand.Parameters.Add(":6", OleDbType.VarChar, 128).Value = "USER_TASK";
                        adapter.InsertCommand.ExecuteNonQuery();
                        thisReader.Close();
                        Conn.conn.Close();
                    }
                }
                else
                {
                    const string caption = "Внимание";
                    var result = MessageBox.Show("У вас уже запущена задача " + TaskName.Text + ". Хотите переключиться на другую?", caption,
                                        MessageBoxButtons.OKCancel,
                     MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        finish = DateTime.Now;
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            TaskName.Text = "№" + row.Cells[0].Value.ToString() + ": " + row.Cells[1].Value.ToString();
                            Conn.conn.Open();
                            OleDbDataAdapter adapter = new OleDbDataAdapter();
                            OleDbCommand thisCommand = Conn.conn.CreateCommand();
                            thisCommand.CommandText = $"SELECT DATE_START FROM PLAN_FACT WHERE PLAN_FACT.PK={pk}";
                            OleDbDataReader thisReader = thisCommand.ExecuteReader();

                            while (thisReader.Read())
                            {
                                start = Convert.ToDateTime(thisReader[0]);
                            }
                            adapter.UpdateCommand = new OleDbCommand("UPDATE PLAN_FACT SET PLAN_FACT.DATE_FINISH = :1, PLAN_FACT.TIME = :2  WHERE PLAN_FACT.PK=:3;", Conn.conn);
                            adapter.UpdateCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = finish.ToString();
                            adapter.UpdateCommand.Parameters.Add(":2", OleDbType.VarChar, 128).Value = (finish - start).ToString(@"hh\:mm\:ss");
                            adapter.UpdateCommand.Parameters.Add(":3", OleDbType.VarChar, 128).Value = pk;
                            adapter.UpdateCommand.ExecuteNonQuery();
                            pk++;
                            start = DateTime.Now;
                            adapter.InsertCommand = new OleDbCommand("INSERT INTO PLAN_FACT VALUES (:1,:2,:3,:4,:5,:6) ", Conn.conn);
                            adapter.InsertCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = pk;
                            adapter.InsertCommand.Parameters.Add(":2", OleDbType.VarChar, 128).Value = row.Cells[1].Value.ToString();
                            adapter.InsertCommand.Parameters.Add(":3", OleDbType.VarChar, 128).Value = start.ToString();
                            adapter.InsertCommand.Parameters.Add(":4", OleDbType.VarChar, 128).Value = DateTime.MaxValue.ToString();
                            adapter.InsertCommand.Parameters.Add(":5", OleDbType.VarChar, 128).Value = (DateTime.MaxValue - start).ToString(@"hh\:mm\:ss");
                            adapter.InsertCommand.Parameters.Add(":6", OleDbType.VarChar, 128).Value = "USER_TASK";
                            adapter.InsertCommand.ExecuteNonQuery();
                            thisReader.Close();
                            Conn.conn.Close();
                        }

                    }
                }
            }
       else
            {
                const string caption = "Ошибка №2";
                var result = MessageBox.Show("Вы не начали работу!", caption,
                                    MessageBoxButtons.OK,
                 MessageBoxIcon.Error);
              
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            timer2.Stop();
           
        if (statusN.Text != "Перерыв")
            {
                daystart = DateTime.Now;
                statusN.Text = "На работе";
                timer1.Start();
                timer1.Interval = 1000;
                toolStripButton8.ToolTipText = toolStripButton8.Text + "/ " + ohrs + ":" + omin + ":" + osec;
            }
           
            if (statusN.Text == "Перерыв")
            {
                finish = DateTime.Now;
                statusN.Text = "На работе";
                timer1.Start();
                timer1.Interval = 1000;
                toolStripButton8.ToolTipText = toolStripButton8.Text + "/ " + ohrs + ":" + omin + ":" + osec;
                Conn.conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand thisCommand = Conn.conn.CreateCommand();
                thisCommand.CommandText = $"SELECT DATE_START FROM PLAN_FACT WHERE PLAN_FACT.PK={pk}";
                OleDbDataReader thisReader = thisCommand.ExecuteReader();

                while (thisReader.Read())
                {
                    start = Convert.ToDateTime(thisReader[0]);
                }
                adapter.UpdateCommand = new OleDbCommand("UPDATE PLAN_FACT SET PLAN_FACT.DATE_FINISH = :1, PLAN_FACT.TIME = :2  WHERE PLAN_FACT.PK=:3;", Conn.conn);
                adapter.UpdateCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = finish.ToString();
                adapter.UpdateCommand.Parameters.Add(":2", OleDbType.VarChar, 128).Value = (finish - start).ToString(@"hh\:mm\:ss");
                adapter.UpdateCommand.Parameters.Add(":3", OleDbType.VarChar, 128).Value = pk;
                adapter.UpdateCommand.ExecuteNonQuery();
                thisReader.Close();
                Conn.conn.Close();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {   if (statusN.Text == "На работе")
            {
                finish = DateTime.Now;
                statusN.Text = "Перерыв";
                timer1.Stop();
                ms = 0;
                timer2.Start();
                timer2.Interval = 1000;
                TaskName.Text = "Название задачи";
                Conn.conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand thisCommand = Conn.conn.CreateCommand();
                thisCommand.CommandText = $"SELECT DATE_START FROM PLAN_FACT WHERE PLAN_FACT.PK={pk}";
                OleDbDataReader thisReader = thisCommand.ExecuteReader();

                while (thisReader.Read())
                {
                    start = Convert.ToDateTime(thisReader[0]);
                }
                adapter.UpdateCommand = new OleDbCommand("UPDATE PLAN_FACT SET PLAN_FACT.DATE_FINISH = :1, PLAN_FACT.TIME = :2  WHERE PLAN_FACT.PK=:3;", Conn.conn);
                adapter.UpdateCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = finish.ToString();
                adapter.UpdateCommand.Parameters.Add(":2", OleDbType.VarChar, 128).Value = (finish - start).ToString(@"hh\:mm\:ss");
                adapter.UpdateCommand.Parameters.Add(":3", OleDbType.VarChar, 128).Value = pk;
                adapter.UpdateCommand.ExecuteNonQuery();
                pk++;
                start = DateTime.Now;
                adapter.InsertCommand = new OleDbCommand("INSERT INTO PLAN_FACT VALUES (:1,:2,:3,:4,:5,:6) ", Conn.conn);
                adapter.InsertCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = pk;
                adapter.InsertCommand.Parameters.Add(":2", OleDbType.VarChar, 128).Value = "Перерыв";
                adapter.InsertCommand.Parameters.Add(":3", OleDbType.VarChar, 128).Value = start.ToString();
                adapter.InsertCommand.Parameters.Add(":4", OleDbType.VarChar, 128).Value = DateTime.MaxValue.ToString();
                adapter.InsertCommand.Parameters.Add(":5", OleDbType.VarChar, 128).Value = (DateTime.MaxValue - start).ToString(@"hh\:mm\:ss");
                adapter.InsertCommand.Parameters.Add(":6", OleDbType.VarChar, 128).Value = "BREAK_TIME";
                adapter.InsertCommand.ExecuteNonQuery();
                thisReader.Close();
                Conn.conn.Close();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (statusN.Text != "Статус")
            {
                dayfinish = DateTime.Now;
                finish = DateTime.Now;
                TimeSpan raz = (dayfinish.ToLocalTime() - daystart.ToLocalTime());
                
                statusN.Text = "Завершен";
                timer1.Stop();
                timer2.Stop();
                sec = 0; min = 0; hrs = 0;
                osec = 0; min = 0; hrs = 0;
                timeN.Text = "00:00:00";

                TaskName.Text = "Название задачи";
                Conn.conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand thisCommand = Conn.conn.CreateCommand();
                OleDbCommand thisCommand1 = Conn.conn.CreateCommand();
                thisCommand.CommandText = $"SELECT DATE_START FROM PLAN_FACT WHERE PLAN_FACT.PK={pk}";
                //thisCommand1.CommandText = "";
                OleDbDataReader thisReader = thisCommand.ExecuteReader();

                while (thisReader.Read())
                {
                    start = Convert.ToDateTime(thisReader[0]);
                }
                adapter.UpdateCommand = new OleDbCommand("UPDATE PLAN_FACT SET PLAN_FACT.DATE_FINISH = :1, PLAN_FACT.TIME = :2  WHERE PLAN_FACT.PK=:3;", Conn.conn);
                adapter.UpdateCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = finish.ToString();
                adapter.UpdateCommand.Parameters.Add(":2", OleDbType.VarChar, 128).Value = (finish - start).ToString(@"hh\:mm\:ss");
                adapter.UpdateCommand.Parameters.Add(":3", OleDbType.VarChar, 128).Value = pk;
                adapter.UpdateCommand.ExecuteNonQuery();
                pk++;
                adapter.InsertCommand = new OleDbCommand("INSERT INTO PLAN_FACT VALUES (:1,:2,:3,:4,:5,:6) ", Conn.conn);
                adapter.InsertCommand.Parameters.Add(":1", OleDbType.VarChar, 128).Value = pk;
                adapter.InsertCommand.Parameters.Add(":2", OleDbType.VarChar, 128).Value = "Рабочий день";
                adapter.InsertCommand.Parameters.Add(":3", OleDbType.VarChar, 128).Value = daystart.ToString();
                adapter.InsertCommand.Parameters.Add(":4", OleDbType.VarChar, 128).Value = dayfinish.ToString();
                adapter.InsertCommand.Parameters.Add(":5", OleDbType.VarChar, 128).Value = raz.ToString(@"hh\:mm\:ss");
                adapter.InsertCommand.Parameters.Add(":6", OleDbType.VarChar, 128).Value = "WORK_TIME";
                adapter.InsertCommand.ExecuteNonQuery();
                thisReader.Close();
                Conn.conn.Close();
            } 
        }

  

     


        private void timer1_Tick(object sender, EventArgs e)
        {
            sec += 1;
            if (sec == 60)
            {
                sec = 0;
                min += 1;
            }
            if (min == 60)
                min = 0;
            timeN.Text =  hrs + ":" + min + ":" + sec;
        }

       

       
    }

  

}
