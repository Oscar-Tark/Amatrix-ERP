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
using System.Linq;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Wizard_sdf : Form
    {
        public Wizard_sdf()
        {
            this.Icon = Properties.Resources.installericon;
            InitializeComponent();
            this.Text = "Amatrix Guide : New Local Connection";
            /*try
            { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text); pwd.Owner = this; }
            catch (Exception erty) { }*/
        }

        private string connection;
        private void Wizard_sdf_Load(object sender, EventArgs e)
        {

        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            OFD.ShowDialog();
        }

        private void bt_nxt_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (sender.Equals(bt_nxt) == true)
                {
                    tabControl1.SelectTab(tabControl1.SelectedIndex + 1);
                    if (tabControl1.SelectedIndex == 2)
                    {
                        test();
                    }
                }
                else
                {
                    tabControl1.SelectTab(tabControl1.SelectedIndex - 1);
                }
            //}
            //catch (Exception erty) { }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void test()
        {
            if (tbx1.Text != "Data-Base Address" || tbx1.Text != "")
            {
                connection = "DataSource='" + tbx1.Text + "'";
                if(pwrd.Text != "Data-Base Password" && pwrd.Text != "")
                {
                    connection = connection + " Password='" + pwrd.Text + "'";
                }

                //testing part1
                SqlCeConnection conn = new SqlCeConnection(connection); 
                
                DataSet dbtDataSet = new DataSet();
                DataTable dt;
                try
                {
                    conn.Open();
                    tst_1.Visible = true;
                    pic_1.Visible = true;
                    progressBar1.Increment(20);
                    lbl_testing.Text = "Connected";
                    pic_1.BackgroundImage = Properties.Resources.tick;
                }
                catch (Exception erty)
                {
                    tst_1.Visible = true;
                    pic_1.Visible = true; 
                    pic_1.BackgroundImage = Properties.Resources.ex; tst_1.Text = "Not Connected"; lbl_testing.Text = "Testing Stopped";
                }

                try
                {
                    lbl_testing.Text = "Testing Tables...";
                    SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES", conn);
                    dataAdapter.Fill(dbtDataSet);
                    progressBar1.Increment(20); dataGridView1.DataSource = dbtDataSet.Tables[0];

                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            cb.Items.Add(dgvr.Cells[2].Value.ToString());
                        }
                        catch (Exception erty34) { }
                    }

                    //end
                    tst_2.Visible = true;
                    pic_2.Visible = true;
                    tst_2.Text = "Found Tables";
                    pic_2.BackgroundImage = Properties.Resources.tick;
                    lbl_testing.Text = "Testing Completed";

                    progressBar1.Increment(60);
                    pic_4.Visible = true;
                    tst_3.Visible = true;
                }
                catch (Exception erty)
                {
                    tst_2.Visible = true;
                    tst_2.Text = "Table Testing Unsuccessfull";
                    pic_2.Visible = true;
                    pic_2.BackgroundImage = Properties.Resources.ex;
                }
            }
            else
            { tabControl1.SelectTab(0); Am_err ner = new Am_err(); ner.tx("A valid Data-Base File has not Been Selected"); }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
        }

        private void OFD_FileOk(object sender, CancelEventArgs e)
        {
            tbx1.Text = OFD.FileName;
        }

        private Button bttn_tmp;
        private void bt_nxt_MouseEnter(object sender, EventArgs e)
        {
            bttn_tmp = (Button)sender;
            bttn_tmp.BackgroundImage = Properties.Resources.btnsimp2;
        }

        private void bt_nxt_MouseLeave(object sender, EventArgs e)
        {
            bttn_tmp = (Button)sender;
            bttn_tmp.BackgroundImage = Properties.Resources.btnsim1;
        }

        private void bt_nxt_MouseDown(object sender, MouseEventArgs e)
        {
            bttn_tmp = (Button)sender;
            bttn_tmp.BackgroundImage = Properties.Resources.btsimp;
        }

        private void bt_nxt_MouseUp(object sender, MouseEventArgs e)
        {
            bttn_tmp = (Button)sender;
            bttn_tmp.BackgroundImage = Properties.Resources.btnsimp2;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
