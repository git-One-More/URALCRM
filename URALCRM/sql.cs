using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace URALCRM
{
    public partial class sql : Form
    {
       string connamearr;
        string activeconn;
        string constr;
        string path = Directory.GetCurrentDirectory();
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
           
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            textBox1.Visible = false;
            button3.Visible=false;
            button4.Visible=false;
            button5.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;
            dataGridView1.Visible = false;
            richTextBox1.Visible = false;
            менюToolStripMenuItem.Visible = false;
            выполнитьToolStripMenuItem.Visible = false;

           

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
           

        }

     

        private void button1_Click_1(object sender, EventArgs e)
        {
            comboBox1.Items.Add("ACCES");
            comboBox2.Items.Add("Jet.OLEDB.4.0");
            comboBox2.Items.Add("ACE.OLEDB.12.0");
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
            label3.Text = "Создание подключения";
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
            if (comboBox1.Visible == true)
            {
                comboBox2.Text = ""; 
                comboBox1.Text = "";

                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
            }
            if (comboBox1.Visible == false)
            {
                comboBox2.Text = "";
              comboBox2.Items.Clear();
            }
            comboBox1.Visible = false;
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
            //Загрузка имен подключений
            DirectoryInfo d = new DirectoryInfo($@"{path}");
            FileInfo[] Files = d.GetFiles("*.constr"); //Getting Text files
            foreach (FileInfo file in Files)
            {

                connamearr = Path.GetFileNameWithoutExtension(file.ToString());
                аToolStripMenuItem.Items.Add(connamearr);
            }
            //Создание config файла для подключения
            if (comboBox1.Visible == true)
            {
                const string caption = "Внимание!";
                var result = MessageBox.Show("Вы хотите создать новое подключение с именем: " + textBox2.Text + ". Продолжить?", caption,
                                    MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                   

                    FileStream fs = new FileStream($@"{path}\{textBox2.Text}.constr", FileMode.CreateNew);
                    StreamWriter sw = new StreamWriter(fs);
                    string writer= "Provider = Microsoft." + comboBox2.SelectedItem.ToString() + "; Data Source =" + textBox1.Text;
                    sw.WriteLine(writer);
                  activeconn = textBox2.Text;
                    constr = writer;
                    sw.Close();
                    fs.Close();
                 
                    аToolStripMenuItem.Items.Add(textBox2.Text);
                   
                }

                if (result == DialogResult.Cancel) return;
            }

            // Существующее подключение
            if (comboBox1.Visible==false)
            {
               
                activeconn = comboBox2.SelectedItem.ToString();
                StreamReader reader = new StreamReader($@"{path}/{activeconn}.constr");
                
                constr = reader.ReadToEnd();
                reader.Close();

            }
            toolStripTextBox1.Text = "подключено к: "+activeconn;
            dataGridView1.Visible = true;
            richTextBox1.Visible = true;
            менюToolStripMenuItem.Visible = true;
            toolStripMenuItem1.Visible = false;
            выполнитьToolStripMenuItem.Visible = true;
            groupBox1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {   //Настройк видимости
            comboBox1.Visible = false;
            comboBox2.Visible = true;
            textBox1.Visible = false;
            button3.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            button4.Visible = true;
            button5.Visible = true;
            label3.Visible = true;
            textBox2.Visible = false;
            label3.Text = "Выбор подключения";
            //Загрузка имен подключений в комбобокс2
            DirectoryInfo d = new DirectoryInfo($@"{path}");
                        FileInfo[] Files = d.GetFiles("*.constr");
            foreach (FileInfo file in Files)
            {

                connamearr = Path.GetFileNameWithoutExtension(file.ToString());
                comboBox2.Items.Add(connamearr);

            }

           

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form main = new main();
            main.Show();
            this.Hide();
        }

     
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void выполнитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OleDbConnection sqlcon = new OleDbConnection(constr);
            sqlcon.Open();
            DataSet dataSet = new DataSet();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = new OleDbCommand(richTextBox1.Text, sqlcon);
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            dataSet.Dispose();
            sqlcon.Close();
            adapter.Dispose();
        }

        private void наГлавнюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form main = new main();
            this.Hide();
            main.Show();
        }

        private void новоеПодключениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Add("ACCES");
            comboBox2.Items.Add("Jet.OLEDB.4.0");
            comboBox2.Items.Add("ACE.OLEDB.12.0");
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
            label3.Text = "Создание подключения";
        }

      

        private void аToolStripMenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeconn = аToolStripMenuItem.SelectedItem.ToString();
            StreamReader reader = new StreamReader($@"{path}/{activeconn}.constr");
            constr = reader.ReadToEnd();
            reader.Close();
            toolStripTextBox1.Text = "подключено к: " + activeconn;
        }

        private void экспортЗапросаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
         
            saveFileDialog.Filter = "SQL запрос (*.sql)|*.sql|Все файлы (*.*)| *.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.FileName);
                string savetext = richTextBox1.Text;
                sw.WriteLine(savetext);
                sw.Close();

                string fileName = System.IO.Path.GetFileNameWithoutExtension(saveFileDialog.FileName);

                string pathd = Directory.GetCurrentDirectory();
                FileStream fs = new FileStream($@"{pathd}\{fileName}.dsql", FileMode.CreateNew);
                StreamWriter sw1 = new StreamWriter(fs);
                sw1.WriteLine(savetext);
                sw1.WriteLine(constr);
                sw1.Close();
                fs.Close();

                MessageBox.Show("Сохранение прошло успешно", "Сообщение №2");
            
            
            
            }
        }

        private void имортЗапросаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "SQL запрос (*.sql)|*.sql|Все файлы (*.*)| *.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog.FileName);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void текстовыйДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == -1)
            {
                MessageBox.Show("Нет данных для сохранения", "ERROR №12", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)| *.*";
                
                saveDialog.ShowDialog();
                IDataObject objectSave = Clipboard.GetDataObject();
                dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                dataGridView1.SelectAll();
                Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                string pattern = @"^;(.*)$";
                string str = (Clipboard.GetText(TextDataFormat.Text)).Replace(" ", " ");
                str = Regex.Replace(str, pattern, "$1", RegexOptions.Multiline);
                File.WriteAllText(saveDialog.FileName, str, Encoding.UTF8);

                if (objectSave != null)
                {
                    Clipboard.SetDataObject(objectSave);
                    MessageBox.Show("Данные сохранены в "+saveDialog.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }
        private void ecxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == -1)
            {
                MessageBox.Show("Нет данных для сохранения", "ERROR №12", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    Excel.Application excelapp = new Excel.Application();
                    excelapp.Visible = false;
                    excelapp.SheetsInNewWorkbook = 1;
                    excelapp.Workbooks.Add(Type.Missing);
                    excelapp.Worksheets.Add(Type.Missing);
                    excelapp.Columns.ColumnWidth = 30;
                    excelapp.Rows.RowHeight = 30;
                    excelapp.DisplayAlerts = true;
                    Excel.Workbook workbook = excelapp.Workbooks.Add();
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    var workbook1 = (Excel.Worksheet)workbook.Worksheets[1];
                    worksheet.Cells.NumberFormatLocal = "@";
                    try
                    {
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                        }
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView1.Columns.Count; j++)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                }
                                else
                                {
                                    worksheet.Cells[i + 2, j + 1] = "";
                                }
                            }
                        }


                        SaveFileDialog saveDialog = new SaveFileDialog();
                        saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                        saveDialog.FilterIndex = 1;

                        if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            workbook.SaveAs(saveDialog.FileName);
                            MessageBox.Show("Export Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    finally
                    {
                        excelapp.DisplayAlerts = false;
                        workbook.Close(false);
                        excelapp.Quit();
                        excelapp.DisplayAlerts = true;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

            }

        }
    }
}
