using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApplication1
{
    public class DBConnection
    {
        public const string EncryptKey = "27042005";
        public static string st_Conn = string.Empty;
        private static SqlConnection m_master = new SqlConnection();
        public static SqlConnection m_cnn = new SqlConnection();

        public DBConnection()
        {

        }

        public static SqlConnection ActiveConnection
        {
            get
            {
                if (m_cnn.State != ConnectionState.Open)
                {
                    Open();
                }
                return m_cnn;
            }
            set { m_cnn = value; }
        }

        public static void Open()
        {
            CheckDB();
            if (m_cnn.State == System.Data.ConnectionState.Closed)
            {
                m_cnn.Open();
            }
        }

        public static void Close()
        {
            if (m_cnn.State == System.Data.ConnectionState.Open)
            {
                m_cnn.Close();
            }
        }

        private static void Decrypt(string st)
        {
            RC4Engine myRC4Engine = new RC4Engine();
            myRC4Engine.EncryptionKey = EncryptKey;
            myRC4Engine.CryptedText = st;
            myRC4Engine.Decrypt();
            st_Conn = myRC4Engine.InClearText;
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private static void CheckDB()
        {
            string st_Attach = string.Empty;
            SqlConnectionStringBuilder builder;
            if (m_cnn.ConnectionString.Length == 0)
            {
                string tmp = string.Empty;
                using (FileStream fs = File.OpenRead("DBConnection.ini"))
                {
                    byte[] b = new byte[fs.Length];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        tmp += temp.GetString(b);
                    }
                }
                Decrypt(tmp);
                builder = new SqlConnectionStringBuilder(st_Conn);
                m_cnn = new SqlConnection(builder.ConnectionString);
                builder["database"] = "master";
                m_master = new SqlConnection(builder.ConnectionString);
                SqlCommand cmd = new SqlCommand("select rec=count(*) from sysdatabases where name = '" +
                    m_cnn.Database + "'", m_master);
                m_master.Open();
                if ((int)cmd.ExecuteScalar() != 1)
                {
                    st_Attach = @"exec sp_attach_db N'" + m_cnn.Database +
                        @"', N'" + Environment.CurrentDirectory + @"\Data\" + m_cnn.Database + @".mdf', N'" +
                        Environment.CurrentDirectory + @"\Data\" + m_cnn.Database + @"_log.LDF'";
                    SqlCommand MyCommand = new SqlCommand(st_Attach, m_master);
                    MyCommand.ExecuteNonQuery();
                    MyCommand.Dispose();
                }
                cmd.Dispose();
                m_master.Close();
            }
        }

        public static string rExecSQL(string cmd)
        {
            string ret = string.Empty;
            SqlCommand sqlcmd = new SqlCommand(cmd, ActiveConnection);
            ret = (string)sqlcmd.ExecuteScalar();
            sqlcmd.Dispose();
            Close();
            return ret;
        }
      
        public static object roExecSQL(string cmd)
        {
            object ret;
            SqlCommand sqlcmd = new SqlCommand(cmd, ActiveConnection);
            ret = sqlcmd.ExecuteScalar();
            sqlcmd.Dispose();
            Close();
            return ret;
        }
               
        public static void ExecSQL(string cmd)
        {
            SqlCommand sqlcmd = new SqlCommand(cmd, ActiveConnection);
            sqlcmd.ExecuteNonQuery();
            sqlcmd.Dispose();
            DBConnection.Close();
        }


        public static object ExecuteNonQuery(string cmdString)
        {
            SqlCommand cmd = new SqlCommand(cmdString);
            cmd.Connection = m_cnn;
            cmd.CommandType = CommandType.Text;
            return cmd.ExecuteNonQuery();
        }

        public static void ExecSP(string spName, SqlParameter[] para)
        {
            SqlCommand cmd = new SqlCommand(spName, DBConnection.ActiveConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 3600;
            cmd.Parameters.AddRange(para);
            cmd.ExecuteNonQuery();
        }

        public static DataTable GetSQLTable(string cmd)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(cmd, ActiveConnection);
            adp.Fill(dt);
            adp.Dispose();
            Close();
            return dt;
        }

        public static DataRow[] GetDatarow(string  tableName, int ID)
        {
            string st;
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            if (ID == 0)
            {
                st = string.Format("select * from {0} ", tableName);
            }
            else
            {
                st = string.Format("select * from {0} where ID = {1} ", tableName, ID);
            }
            adp = new SqlDataAdapter(st, ActiveConnection);
            adp.Fill(dt);
            adp.Dispose();
            return dt.Select(); 
        }

        public static int GetTemporaryID()
        {
            int ID = 0;
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = ActiveConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = @"GetLastID";
            sqlcmd.Parameters.Add("@ID", SqlDbType.Int);
            sqlcmd.Parameters[0].Direction = ParameterDirection.Output;
            sqlcmd.ExecuteNonQuery();
            ID = (int)sqlcmd.Parameters[0].Value;
            sqlcmd.Dispose();
            Close();
            return ID;
        }
      
        public static bool IsAccountExists(string code)
        {
            bool ret = false;
            int record = 0;
            string st = code;
            int.TryParse(DBConnection.RExecSQL("Select count(*) from Account where Code = '" + st + "'"), out record);
            if (record > 0)
            {
                ret = true;
            }
            return ret;
        }

        public static string[] GetCode(string Code)
        {
            string[] ret = new string[6];
            if (IsAccountExists(Code))
            {
                ret[0] = DBConnection.RExecSQL("Select ID from Account where Code = '" + Code + "'");
                ret[1] = DBConnection.RExecSQL("Select Code from Account where Code = '" + Code + "'");
                ret[2] = DBConnection.RExecSQL("Select Description from Account where Code = '" + Code + "'");
                ret[3] = DBConnection.RExecSQL("Select Income from Account where Code = '" + Code + "'");
                ret[4] = DBConnection.RExecSQL("Select Expense from Account where Code = '" + Code + "'");
                ret[5] = DBConnection.RExecSQL("Select AG.Description from Account a join AccountGroup Ag on a.AccountGroupID = ag.id where a.Code = '" + Code + "'");

            }
            return ret;
        }

        public static string RExecSQL(string cmd)
        {
            object o;
            string ret = string.Empty;
            SqlCommand sqlcmd = new SqlCommand(cmd, ActiveConnection);
            o = sqlcmd.ExecuteScalar();
            if (o != null)
            {
                ret = o.ToString();
            }
            else
            {
                ret = "";
            }
            sqlcmd.Dispose();
            Close();
            return ret;
        }
        
        public static DataTable GetTable(string TableName, string Condition)
        {
            string st;
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            if (Condition == string.Empty)
            {
                st = string.Format("select * from {0}", TableName);
            }
            else
            {
                st = string.Format("select * from {0} where {1}", TableName, Condition);
            }
            adp = new SqlDataAdapter(st, ActiveConnection);
            adp.Fill(dt);
            return dt;
        }

        public static DataTable GetTable(string TableName,string FieldName,int Condition)
        {
            string st;
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            if (FieldName == string.Empty)
            {
                st = string.Format("select * from {0} ", TableName);
            }
            else
            {
                st = string.Format("select * from {0} where {1} = {2}", TableName,FieldName, Condition);
            }
            adp = new SqlDataAdapter(st, ActiveConnection);
            adp.Fill(dt);
            return dt;
        }

        public static DataTable GetTable(string tableName, int ID)
        {
            string st;
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            if (ID == 0)
            {
                st = string.Format("select * from {0} " ,tableName);
            }
            else
            {
                st = string.Format("select * from {0} where ID = {1} " ,tableName, ID );
            }
            adp = new SqlDataAdapter(st, ActiveConnection);
            adp.Fill(dt);
            adp.Dispose();
            return dt;
        }
        
        public static object ExecuteScalar(string cmdString)
        {
            SqlCommand cmd = new SqlCommand(cmdString,ActiveConnection);
            cmd.Connection = m_cnn;
            cmd.CommandType = CommandType.Text;
            return cmd.ExecuteScalar();
        }

        public static DataTable Table(string Query, string Condition)
        {
            string st;
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            if (Condition == string.Empty)
            {
                st = string.Format (Query);
            }
            else
            {
                st = string.Format(Query, Condition);
            }
            adp = new SqlDataAdapter(st, ActiveConnection);
            adp.Fill(dt);
            return dt;
        }
    }
}
