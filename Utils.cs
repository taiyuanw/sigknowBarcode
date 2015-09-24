using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace SigknowBarcode
{
    class Utils
    {
        // global : only 1 printer is active at a given time frame.
        public static string Printer = null;

        public const string ASSOCIATE = "S001K";
        public const string DISASSOCIATE = "S002K";
        public const string QUERY = "S003K";
        public const string RESET = "S004K";
        public const string VERIFY = "S005K";
        public const string DISVERIFY = "S006K";

        public static void OKBeep()
        {
            Console.Beep(4000, 200);
            //Thread.Sleep(50);
            Console.Beep(5000, 200);
        }
        public static void ErrorBeep()
        {
            Console.Beep(1000, 1000);
            //Console.Beep(1000, 500);
        }

    }
    class MySQLDB
    {
        /* TIP: to clean up ezypatch table, do the following:
       SET SQL_SAFE_UPDATES = 0;
       delete from ezypatch;
       commit;
       select * from ezypatch;
       */

        public static string MySQLserver = "Taiyuan-Lenovo";
        public static string MySQLuser = "mysqladmin";
        public static string MySQLpassword = "0000";
        static MySqlConnection conn = new MySqlConnection();

        private static void connect()
        {
            //MySqlConnection conn = new MySqlConnection();
            string connStr = String.Format("server={0};user={1}; password={2}; database=sigknow; pooling=false",
            MySQLserver, MySQLuser, MySQLpassword);
            conn.ConnectionString = connStr;

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Utils.ErrorBeep();
                MessageBox.Show("連結到資料庫失敗.");
                MessageBox.Show(ex.ToString());
                System.Windows.Application.Current.Shutdown();
            }
        }
        public static void DBconnect()
        {
            connect();
        }
        private static void disconnect()
        {
            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
                Utils.ErrorBeep();
                MessageBox.Show("中斷與資料庫的連結過程中發生問題.");
                MessageBox.Show(ex.ToString());
                System.Windows.Application.Current.Shutdown();
            }
        }
        public static void DBdisconnect()
        {
            disconnect();
        }

        public static void DBcommand(string cmd)
        {
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = cmd;
            try
            {
                connect();
                //MessageBox.Show(cmd);
                command.ExecuteNonQuery();
                disconnect();
                Utils.OKBeep();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("MySQL 指令 \n" + cmd + "\n  執行過程發生錯誤!");
                try
                {
                    disconnect();
                }
                catch
                {
                    Utils.ErrorBeep();
                }
            }
        }

        public static void DBquery(string cmd, ref string pcba, ref string sigknow )
        {
            try
            {
                connect();
                MySqlCommand command = new MySqlCommand(cmd, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pcba = reader.GetString("PCBASN");
                    sigknow = reader.GetString("SIGKNOWSN");
                }
                disconnect();
                Utils.OKBeep();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("MySQL 指令 \n" + cmd + "\n  執行過程發生錯誤!");
                try
                {
                    disconnect();
                }
                catch
                {
                    Utils.ErrorBeep();

                }
            }
        }

    }
}
