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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class First_use : Form
    {
        public First_use()
        {
            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.amdsicnico;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
            this.Text = "Amatrix Employee and Company Information Settings";
            init();
            /*try
            { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text); pwd.Owner = this; }
            catch (Exception erty) { }*/
        }

        private void init()
        {
            textBox1.Text = Properties.Settings.Default.usr;
            try
            {
                pictureBox4.BackgroundImage = Image.FromFile(Properties.Settings.Default.img_usr);
            }
            catch (Exception erty) { pictureBox4.BackgroundImage = Properties.Resources.person; }
            if (textBox1.Text == "")
            {
                textBox1.Text = SystemInformation.UserName + "\\AM-USER\\" + SystemInformation.UserDomainName;
            }
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

        public void tx()
        {
            tabControl1.SelectTab(1);
            this.Show();
        }

        SQLCEBasql.SQLCEBSQ scsql = new SQLCEBasql.SQLCEBSQ();
        private void First_use_Load(object sender, EventArgs e)
        {
            if(Main.Amatrix.doc != "")
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.doc);
                SqlCommand cmd = new SqlCommand("SELECT * FROM co_nfo", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                misc_DBDataSet1.co_nfo.Load(dr);
                conn.Close();

                DataTable drp = new DataTable();
                try
                {
                    drp = scsql.Execute(Properties.Settings.Default.Misc_DBConnectionString, "SELECT * FROM co_nfo");
                    textBox1.Text = drp.Rows[0].ItemArray[5].ToString();
                    textBox3.Text = drp.Rows[0].ItemArray[6].ToString();
                }
                catch (Exception erty) { }
                drp.Clear();
                drp.Dispose();
            }
            else
            {
                DataTable drp = new DataTable();
                try
                {
                    drp = scsql.Execute(Properties.Settings.Default.Misc_DBConnectionString, "SELECT * FROM co_nfo");
                    textBox1.Text = drp.Rows[0].ItemArray[5].ToString();
                    textBox3.Text = drp.Rows[0].ItemArray[6].ToString();
                }
                catch (Exception erty) { }
                drp.Clear();
                drp.Dispose();

                this.Text = this.Text + " (Using Local(Non Server) Data)";
                this.co_nfoTableAdapter.Fill(this.misc_DBDataSet1.co_nfo);
            }
        }

        private void bttopt_Click(object sender, EventArgs e)
        {
            First_use_optn opfrt = new First_use_optn();
            opfrt.Show();
        }

        Button bcs_tmp;
        private void bttopt_MouseEnter(object sender, EventArgs e)
        {
            bcs_tmp = (Button)sender;
            bcs_tmp.BackgroundImage = Properties.Resources.btnsimp2;
        }

        private void bttopt_MouseLeave(object sender, EventArgs e)
        {
            bcs_tmp = (Button)sender;
            bcs_tmp.BackgroundImage = Properties.Resources.btnsim1;
        }

        private void bttopt_MouseDown(object sender, MouseEventArgs e)
        {
            bcs_tmp = (Button)sender;
            bcs_tmp.BackgroundImage = Properties.Resources.btsimp;
        }

        private void bttopt_MouseUp(object sender, MouseEventArgs e)
        {
            bcs_tmp = (Button)sender;
            bcs_tmp.BackgroundImage = Properties.Resources.btnsimp2;
        }

        private void nxt_1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabControl1.SelectedIndex + 1);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                pictureBox4.BackgroundImage = Image.FromFile(ofd.FileName);
                Properties.Settings.Default.img_usr = ofd.FileName;
                Properties.Settings.Default.Save();
            }
            catch (Exception erty) { pictureBox4.BackgroundImage = Properties.Resources.person; }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox4.BackgroundImage = Properties.Resources.person;
            Properties.Settings.Default.img_usr = "";
            Properties.Settings.Default.Save();
        }

        private void First_use_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                scsql.Execute_Insert_Update(Properties.Settings.Default.Misc_DBConnectionString, "DELETE FROM co_nfo");
                scsql.Execute_Insert_Update(Properties.Settings.Default.Misc_DBConnectionString, "INSERT INTO co_nfo VALUES('" + textBox2.Text + "','" + textBox4.Text + "','" + textBox6.Text + "','" + textBox5.Text + "','" + textBox7.Text + "','" + textBox1.Text+ "','" + textBox3.Text + "')");
                
                if (Main.Amatrix.doc != "")
                {
                    Base_ASQL.BASQL b = new Base_ASQL.BASQL();
                    DataTable dtpy = new DataTable();
                    b.Execute(Main.Amatrix.doc, "DELETE FROM co_nfo", "", dtpy);
                    b.Execute(Main.Amatrix.doc, "INSERT INTO co_nfo VALUES('" + textBox2.Text + "', '" + textBox4.Text + "', '" + textBox6.Text + "','', '" + textBox5.Text + "','','','', '" + textBox7.Text + "','','" + "INF-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString() + "')", "", dtpy);
                    dtpy.Clear();
                    dtpy.Dispose();
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Unable to Save Company Information Data. (" + erty.Message + ")"); e.Cancel = true; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = SystemInformation.UserName + "\\AM-USER\\" + SystemInformation.UserDomainName;
        }
    }
}
