using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URALCRM
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new Form1());
          // Application.Run(new main());
        }
        
    }
    public static class Conn
    {
        public static OleDbConnection conn = new OleDbConnection($@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Directory.GetCurrentDirectory()}\main.mdb");
    }
}
public static class StaticData
{
    //Буфер данных
    public static String login;
}

