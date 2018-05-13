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
using System.Data.SqlServerCe;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class wizard_newconn_bkk_cust : Form
    {
        public wizard_newconn_bkk_cust()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Icon = Properties.Resources.installericon;
            InitializeComponent();
            comboBox25.SelectedIndex = 0;
            if (Main.Amatrix.acc != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.acc); pwd.Owner = this; }
                catch (Exception erty) { }
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

        private void acol_Click(object sender, EventArgs e)
        {
            listView1.Items.Add("");
            if (textBox14.Text.ToLower() != "enter a name")
            {
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(textBox14.Text);
            }
            else
            {
                listView1.Items[listView1.Items.Count - 1].SubItems.Add("Not Assigned");
            }
            listView1.Items[listView1.Items.Count - 1].SubItems.Add(comboBox25.Text);
            if (checkBox12.Checked == true)
            {
                listView1.Items[listView1.Items.Count - 1].SubItems.Add("Yes");
            }
            else
            {
                listView1.Items[listView1.Items.Count - 1].SubItems.Add("No");
            }
        }

        private void dcol_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.FocusedItem.SubItems[3].Text != "Yes")
                {
                    listView1.FocusedItem.Remove();
                }
            }
            catch (Exception erty) { }
        }

        private ListViewItem itm_temp;
        private void comboBox24_TextChanged(object sender, EventArgs e)
        {
        }

        private void bt_nxt_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(bt_nxt) == true)
                {
                    tabControl1.SelectTab(tabControl1.SelectedIndex + 1);
                    if (tabControl1.SelectedIndex == 3)
                    {
                        test();
                    }
                }
                else
                {
                    tabControl1.SelectTab(tabControl1.SelectedIndex - 1);
                }
            }
            catch (Exception erty) { }
        }

        private string connection;
        private void test()
        {
            connection = "";
            if (cbx1.Text != "Data-Base Address" || cbx1.Text != "")
            {
                if (cbx1.Text.ToLower() == "internal data-base")
                {
                    connection = Properties.Settings.Default.UCBConnectionString;
                }
                else if (cbx1.Text.ToLower() != "internal data-base") 
                {
                    connection = "DataSource='" + cbx1.Text + "'";
                }

                if (checkBox2.Checked == true)
                {
                }
                else
                {
                    connection = connection + "; Password='" + pwrd.Text + "'";
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
                    if (erty.Message.Contains("The specified password does not match the database password.") == true)
                    {
                        Am_err ner = new Am_err();
                        ner.tx("The Password Specified is Incorrect Please Input the Correct Password and check all inputted data Before Running this Test.");
                    }
                }

                try
                {
                    lbl_testing.Text = "Testing Tables...";
                    SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES", conn);
                    dataAdapter.Fill(dbtDataSet);
                    progressBar1.Increment(20); dataGridView1.DataSource = dbtDataSet.Tables[0];


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
                } conn.Close(); //conn.Dispose();
            }
            else
            { tabControl1.SelectTab(0); Am_err ner = new Am_err(); ner.tx("A valid Data-Base File has not Been Selected"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OFD.ShowDialog();
        }

        private void OFD_FileOk(object sender, CancelEventArgs e)
        {
            cbx1.Text = OFD.FileName;
        }

        private string columns, connxt;
        private void cb_Click(object sender, EventArgs e)
        {
            columns = "";
            if (cbx1.Text.ToLower() == "internal data-base")
            {
                try
                {
                    SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.UCBConnectionString);
                    SqlCeCommand cmd2 = conn2.CreateCommand();
                    foreach (ListViewItem itm in listView1.Items)
                    {
                        columns = columns + "[" + itm.SubItems[1].Text + "]";
                        if (itm.SubItems[2].Text == "Numerical" && itm.SubItems[1].Text != "Serial Number")
                        {
                            columns = columns + " int";
                        }
                        else if (itm.SubItems[2].Text == "AplhaNumerical Values")
                        {
                            columns = columns + " nvarchar";
                        }
                        else if (itm.SubItems[2].Text == "Money Values")
                        {
                            columns = columns + " money";
                        }
                        else if (itm.SubItems[2].Text == "Date and Time Values")
                        {
                            columns = columns + " datetime";
                        }

                        if (itm.SubItems[1].Text == "Serial Number")
                        {
                            columns = columns + " int NOT NULL PRIMARY KEY, CONSTRAINT [sex] UNIQUE ([Serial Number]), ";
                        }
                        else
                        {
                            if (itm.Index != listView1.Items.Count - 1)
                            {
                                columns = columns + ", ";
                            }
                        }
                    }
                    cmd2.CommandText = "CREATE TABLE [" + tbx1nme.Text + "] (" + columns + ")";
                    conn2.Open();
                    cmd2.ExecuteNonQuery();
                    cmd2.Dispose();
                    conn2.Close();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("An Error Occured While Your Custom Book Was In Creation. '" + erty.Message + "'."); }
            }
            else
            {
                connxt = "DataSource='" + cbx1.Text + "'";
                if (checkBox2.Checked == true)
                {
                }
                else
                {
                    connxt = connxt + "; Password='" + pwrd.Text + "'";
                }
                try
                {
                    SqlCeConnection conn = new SqlCeConnection(connxt);
                    SqlCeCommand cmd = conn.CreateCommand();
                    foreach (ListViewItem itm in listView1.Items)
                    {
                        columns = columns + "[" + itm.SubItems[1].Text + "]";
                        if (itm.SubItems[2].Text == "Numerical Values" && itm.SubItems[1].Text != "Serial Number")
                        {
                            columns = columns + " int";
                        }
                        else if (itm.SubItems[2].Text == "AlphaNumerical Values")
                        {
                            columns = columns + " nvarchar(4000)";
                        }
                        else if (itm.SubItems[2].Text == "Money Values")
                        {
                            columns = columns + " money";
                        }
                        else if (itm.SubItems[2].Text == "Date and Time Values")
                        {
                            columns = columns + " datetime";
                        }
                        if (itm.SubItems[1].Text == "Serial Number")
                        {
                            columns = columns + " int NOT NULL PRIMARY KEY, CONSTRAINT [sex] UNIQUE([Serial Number]),";
                        }
                        else
                        {
                            columns = columns + " NULL";
                            if (itm.Index != listView1.Items.Count - 1)
                            {
                                columns = columns + ", ";
                            }
                        }
                    }
                    cmd.CommandText = "CREATE TABLE [" + tbx1nme.Text + "]("+ columns+")";
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();

                    try
                    {
                        SqlCeConnection conn_tb = new SqlCeConnection(Properties.Settings.Default.Amdtbse_2ConnectionString);
                        SqlCeCommand cmd_tb = conn_tb.CreateCommand();
                        conn_tb.Open();
                        if (checkBox1.Checked == false)
                        {
                            cmd_tb.CommandText = "INSERT [DB_PATH_LOCAL] VALUES ('" + tbx2.Text + "', '" + cbx1.Text + "', '')";
                        }
                        else
                        {
                            cmd_tb.CommandText = "INSERT [DB_PATH_LOCAL] VALUES ('NOT AVAILABLE', '" + cbx1.Text + "', '')";
                        }
                        cmd_tb.ExecuteNonQuery();
                        conn_tb.Close();
                    }
                    catch (Exception ertyyy) { /*tabPage2.Select(); Am_err ner = new Am_err(); ner.tx(ertyyy.Message/*"The Name you have Entered already Exists.");*/ }
                }
                catch (Exception erty) { /*Am_err ner = new Am_err(); ner.tx(columns + erty.Message);*/ }
            }
            this.Close();
        }

        private void cbx1_TextChanged(object sender, EventArgs e)
        {
            if (cbx1.Text.ToLower() == "internal data-base")
            {
                tbx2.Enabled = false;
                checkBox1.Enabled = false;
            }
            else
            {
                tbx2.Enabled = true;
                checkBox1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCeConnection conn_tb = new SqlCeConnection(Properties.Settings.Default.Amdtbse_2ConnectionString);
                SqlCeCommand cmd_tb = conn_tb.CreateCommand();
                conn_tb.Open();
                if (checkBox1.Checked == false)
                {
                    cmd_tb.CommandText = "INSERT [DB_PATH_LOCAL] VALUES ('" + tbx2.Text + "', '" + cbx1.Text + "', '')";
                }
                else
                {
                    cmd_tb.CommandText = "INSERT [DB_PATH_LOCAL] VALUES ('NOT AVAILABLE', '" + cbx1.Text + "', '')";
                }
                cmd_tb.ExecuteNonQuery();
                conn_tb.Close();
                this.Close();
            }
            catch (Exception ertyyy) { }
        }

        private ListViewItem lvt_temp = new ListViewItem();
        private void bttn_dwn_Click(object sender, EventArgs e)
        {

        }

        ListViewItem lv_temp, lv_temp2, lv_temp3;
        private void bttnup_Click(object sender, EventArgs e)
        {
            lv_temp = listView1.Items[listView1.FocusedItem.Index];
            lv_temp3 = lv_temp;
            lv_temp2 = listView1.FocusedItem;
            

            lv_temp = lv_temp2;
            listView1.Items[listView1.FocusedItem.Index - 1] = lv_temp2;
        }

        private void tbx2_TextChanged(object sender, EventArgs e)
        {
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox14.Text = listView1.FocusedItem.SubItems[1].Text;
            comboBox25.Text = listView1.FocusedItem.SubItems[2].Text;
            if (listView1.FocusedItem.SubItems[3].Text == "Yes")
            {
                checkBox12.Checked = true;
            }
            else { checkBox12.Checked = false; }
        }

        private void wizard_newconn_bkk_cust_Load(object sender, EventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
