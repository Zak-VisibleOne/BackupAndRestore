using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnsource_Click(object sender, EventArgs e)
        {
            if (radiobackup.Checked == true)
            {
                SaveFileDialog fbd = new SaveFileDialog();
                fbd.Filter = "Backup File (*.bak)| *.bak";
                fbd.FileName = "NayMyoSat" + DateTime.Now.Date.ToString("dd-MM-yyyy");
                fbd.ShowDialog();
                tbsource.Text = fbd.FileName.ToString();
            }
            else if (radiorestore.Checked == true)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Backup File (*.bak)| *.bak";
                ofd.ShowDialog();
                tbsource.Text = ofd.FileName;
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            this.Enabled = false; SqlConnectionStringBuilder builder;

            if (tbsource.Text != "")
            {
                if (radiobackup.Checked)
                {

                    DBConnection.ExecSQL("Backup database NayMyoSat to disk = '" + tbsource.Text.ToString() + " '");
                    MessageBox.Show("Backup database Successfully!");
                    this.Close();
                }
                else if (radiorestore.Checked)
                {

                    string database = ""; int tmp = 0;
                    database = DBConnection.m_cnn.Database;
                    builder = new SqlConnectionStringBuilder(DBConnection.ActiveConnection.ConnectionString.ToString());
                    builder["database"] = "master";
                    builder["password"] = "27042005";
                    DBConnection.m_cnn = new SqlConnection(builder.ConnectionString);
                    DBConnection.m_cnn.Open();
                    SqlCommand smc = new SqlCommand();
                    //int.TryParse(DBConnection.ExecuteScalar("select spid from  sysprocesses where dbid = DB_ID('" + database + "')").ToString(), out tmp);
                    //DBConnection.ExecuteNonQuery("Kill " + tmp.ToString());
                    DBConnection.ExecuteNonQuery("RESTORE DATABASE NayMyoSat  FROM DISK = '" + tbsource.Text + "' WITH MOVE 'NayMyoSat' TO 'C:\\MinnNandar\\Data\\NayMyoSat.MDF', MOVE '" + "NayMyoSat_LOG' TO 'C:\\MinnNandar\\Data\\NayMyoSat_LOG.LDF',Replace");
                    MessageBox.Show("Restore Successfully \nNeed to Restart Application");
                    Application.Restart();
                    //DBConnection.ExecSQL("RESTORE DATABASE VMGStock  FROM DISK = '" + tbsource.Text + "' WITH MOVE 'VMGStock' TO 'C:\\MinnNandar\\Data\\VMGStock.MDF', MOVE '" + "VMGStock_LOG' TO 'C:\\MinnNandar\\Data\\VMGStock_LOG.LDF',Replace");                    
                    MessageBox.Show("Restore database Successfully!");
                }

            }
            else
            {
                MessageBox.Show("Please Choose Path that you will do!");
            }
        }
    }
}
