/*Amatrix Data Center
    Copyright (C) 2013  Oscar Arjun Singh Tark, Benjamin Jack Johnson

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Application_FILEPOOL : Form
    {
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();

        public Application_FILEPOOL()
        {
            this.Icon = Properties.Resources.amdsicnico;
            InitializeComponent();
            /*try
            { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text); pwd.Owner = this; }
            catch (Exception erty) { }*/
        }

        string Serial, From_App;
        public void tx(string Serial_, string From_App_)
        {
            Serial = Serial_; From_App = From_App_;
            this.Show();
            init();
        }

        private const int CS_DROPSHADOW = 0x00020000;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.ClassStyle |= CS_DROPSHADOW;
                return p;
            }
        }

        private void init()
        {
            if (radioButton2.Checked != true)
            {
                files_dtst.Clear();
                SqlCeConnection conn = new SqlCeConnection(filesTableAdapter.Connection.ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Files WHERE [For App] = '" + From_App + "' AND [For Serial] = '" + Serial + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                files_dtst.Files.Load(dr);
                dataGridView1.DataSource = files_dtst.Files;
                conn.Close();
            }
            else
            {
                try
                {
                    dr_univ = Quer("SELECT * FROM Files WHERE [For App] = '" + From_App + "' AND [For Serial] = '" + Serial + "'");
                    files_dtst.Files.Load(dr_univ);
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("The Server Was Not Found"); radioButton1.Checked = true; radioButton2.Enabled = false; }
            }
        }

        SqlDataReader dr_univ;
        private SqlDataReader Quer(string Query)
        {
            SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
            SqlCommand cmd = new SqlCommand(Query, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
            conn.Close();
        }

        private void Application_FILEPOOL_Load(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fnf = new FileInfo(ofd.FileName);
            DataRow drt = files_dtst.Files.NewRow();
            drt[0] = fnf.Name;
            drt[1] = fnf.Extension;
            drt[2] = ofd.FileName;
            drt[3] = SystemInformation.UserName;
            if (radioButton2.Checked == true)
            {
                byte[] binaryData = File.ReadAllBytes(ofd.FileName);
                drt[4] = binaryData;
            }
            drt[5] = From_App;
            drt[6] = Serial;
            drt[7] = DateTime.Now.ToString() + DateTime.Now.Ticks.ToString();
            files_dtst.Files.Rows.Add(drt);
            dataGridView1.DataSource = files_dtst.Files;
            if (radioButton2.Checked != true)
            {
                filesTableAdapter.Update(files_dtst);
            }
            else
            { asql.Save(files_dtst.Files, "Files", Main.Amatrix.doc); }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == false)
            {
                files_dtst.Clear();
                SqlCeConnection conn = new SqlCeConnection(filesTableAdapter.Connection.ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("DELETE FROM Files WHERE [Serial] = " + dataGridView1[7, dataGridView1.CurrentRow.Index].Value, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                basql.Execute(Main.Amatrix.doc, "DELETE FROM Files WHERE [Serial] = " + dataGridView1[7, dataGridView1.CurrentRow.Index].Value, "Files", files_dtst.Files);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                try
                {
                    string docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    byte[] b = (byte[])dataGridView1[4, dataGridView1.CurrentRow.Index].Value;
                    Directory.CreateDirectory(docs + "\\Amatrix Downloaded Files\\");
                    FileStream fs = new FileStream(docs + "\\Amatrix Downloaded Files\\" + dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString(), FileMode.Create, FileAccess.ReadWrite);
                    fs.Flush(); fs.Close();
                    File.WriteAllBytes(docs + "\\Amatrix Downloaded Files\\" + dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString(), b);

                    try
                    {
                        System.Diagnostics.Process.Start(docs + "\\Amatrix Downloaded Files\\" + dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                    }
                    catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("The File Was Not Found on the Server"); }
            }
            else
            {
                try
                {
                    System.Diagnostics.Process.Start(dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString());
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            init();
        }
    }
}
