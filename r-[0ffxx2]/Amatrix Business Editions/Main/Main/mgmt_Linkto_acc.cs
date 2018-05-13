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
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class mgmt_Linkto_acc : Form
    {
        public mgmt_Linkto_acc()
        {
            InitializeComponent();
            if (Main.Amatrix.mgt != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.mgt); pwd.Owner = this; }
                catch (Exception erty) { }
            }
            else if (Main.Amatrix.acc != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.acc); pwd.Owner = this; }
                catch (Exception erty) { }
            }
            button1.Select();
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

        private void mgmt_Linkto_acc_Load(object sender, EventArgs e)
        {

        }

        //private DataGridViewRow dgvr = new DataGridViewRow();
        string ID, FROM, PROD, MAINPROD, FORBOOK; Double cost_, revenue_;
        public void tx(string MAINPROD_,string PROD_, string ID_, string FROM_, double cost, double revenue)
        {
            PROD = PROD_; ID = ID_; FROM = FROM_; MAINPROD = MAINPROD_; cost_ = cost; revenue_ = revenue;
            if (FROM_ == "customer managment")
            {
                comboBox1.Items.RemoveAt(1);
                comboBox1.Items.RemoveAt(2);
                comboBox1.Items.RemoveAt(0);
                FORBOOK = "SalesBook";
            }
            else if (FROM_ == "product managment")
            {
                comboBox1.Items.RemoveAt(0);
                comboBox1.Items.RemoveAt(0);
                comboBox1.Items.RemoveAt(0);
                comboBox1.Text = "Purchase Book Entry";
                FORBOOK = "PurchaseBook";
            }
            else if (FROM_ == "managment hr")
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
                comboBox1.Items.RemoveAt(3);
                comboBox1.Items.RemoveAt(2);
                comboBox1.Items.RemoveAt(1);
                comboBox1.Text = "General Journal Entry";
                comboBox2.Text = "Salary Payment";
                FORBOOK = "journal";
            }
            else if (FROM_ == "logistical managment")
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
                comboBox1.Items.RemoveAt(3);
                comboBox1.Items.RemoveAt(2);
                comboBox1.Items.RemoveAt(1);
                comboBox1.Text = "General Journal Entry";
                comboBox2.Text = "Logistics Expenditure";
                FORBOOK = "journal";
            }
            else if (FROM_ == "project managment")
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
                comboBox1.Items.RemoveAt(3);
                comboBox1.Items.RemoveAt(2);
                comboBox1.Items.RemoveAt(1);
                comboBox1.Text = "General Journal Entry";
                comboBox2.Text = "Project dr. cr.";
                FORBOOK = "journal";
                EventArgs e = new EventArgs();
                button1_Click(button1, e);
            }
            this.Show();
        }

        DataTable dtp_prod_cost = new DataTable(); Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        private void button1_Click(object sender, EventArgs e)
        {
            int maxm = 0;
            double cst = 0; double tax = 0; string invce = "";
            if (FROM == "customer managment" || FROM == "product managment" || FROM == "managment hr" || FROM == "logistical managment" || FROM == "project managment")
            {
                //insertintojournal
                if (checkBox2.Checked == true)
                {
                    try
                    {
                        if (Main.Amatrix.acc == "")
                        {
                            SqlCeConnection mySqlConnection3 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                            SqlCeCommand mySqlCommand3 = new SqlCeCommand("SELECT MAX([Serial Number]) FROM " + FORBOOK, mySqlConnection3);
                            //mySqlCommand3.CommandText = "SELECT MAX([Serial Number]) FROM SalesBook";
                            mySqlConnection3.Open();
                            DataTable dtpy = new DataTable();
                            SqlCeDataReader dr = mySqlCommand3.ExecuteReader();
                            dtpy.Load(dr);
                            maxm = Convert.ToInt32(dtpy.Rows[0].ItemArray[0]);
                            maxm = maxm + 1;
                            mySqlConnection3.Close();

                            mySqlCommand3.Dispose();
                            mySqlConnection3.Dispose();
                            dtpy.Clear(); dtpy.Dispose();
                        }
                        else
                        {
                            DataTable dtpy = new DataTable(); basql.Execute(Main.Amatrix.acc, "SELECT MAX([Serial Number]) FROM " + FORBOOK, FORBOOK, dtpy);
                            maxm = Convert.ToInt32(dtpy.Rows[0].ItemArray[0]);
                            maxm = maxm + 1;
                            dtpy.Clear(); dtpy.Dispose();
                        }
                    }
                    catch (Exception ery) { maxm = 1; }
                    //SELECT product cost
                    if (FROM != "managment hr" && FROM != "logistical managment" && FROM != "project managment")
                    {
                        if (Main.Amatrix.mgt == "")
                        {
                            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                            SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt WHERE [Product ID Number] = '" + MAINPROD + "'", conn);
                            conn.Open();
                            SqlCeDataReader dr = cmd.ExecuteReader();
                            dtp_prod_cost.Load(dr);
                            conn.Close();
                            try
                            {
                                cst = Convert.ToDouble(dtp_prod_cost.Rows[0].ItemArray[5]);
                            }
                            catch (Exception erty) { }
                            try
                            {
                                tax = Convert.ToDouble(dtp_prod_cost.Rows[0].ItemArray[6]);
                            }
                            catch (Exception ertty) { }
                        }
                        else
                        {
                            dtp_prod_cost = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Prod_mgmt WHERE [Product ID Number] = '" + MAINPROD + "'", "Prod_mgmt", dtp_prod_cost);
                            try
                            {
                                cst = Convert.ToDouble(dtp_prod_cost.Rows[0].ItemArray[5]);
                            }
                            catch (Exception erty) { }
                            try
                            {
                                tax = Convert.ToDouble(dtp_prod_cost.Rows[0].ItemArray[6]);
                            }
                            catch (Exception ertty) { }
                        }
                    }
                    //insert
                    String SQL = "";
                    if (comboBox1.Text.ToLower() == "general journal entry")
                    {
                        string s = "'Salary Payment'"; string i = "0";
                        if (FROM == "logistical managment")
                        { cst = cost_; s = "'Logistics Expenditure'"; }
                        if (FROM == "project managment")
                        { i = revenue_.ToString(); s = "'Project dr. cr.'"; }
                        SQL = "INSERT INTO journal VALUES('" + maxm.ToString() + "',getdate(), '" + MAINPROD + "', " + s + ",'','','False','" + cost_.ToString() + "','','"+ i +"','')";
                    }
                    if (comboBox1.Text.ToLower() == "sales journal entry")
                    {
                        SQL = "INSERT INTO SalesBook VALUES('" + maxm.ToString() + "', '" + ID + "',getdate(), NULL, '', 'Sale of Product[" + MAINPROD + "\\" + PROD + "]', '0', '"+tax.ToString()+"', '" + cst.ToString() + "', '', '', '" + (cst+tax).ToString() + "', '')";
                    }
                    if (comboBox1.Text.ToLower() == "purchase book entry")
                    {
                        SQL = "INSERT INTO PurchaseBook VALUES('" + maxm.ToString() + "', '',getdate(), '', 'Purchase of Product[" + MAINPROD + "\\" + PROD + "]', '0', '" + tax.ToString() + "', '0', '" + cst.ToString() + "', '" + (cst + tax).ToString() + "', '', '" + (cst + tax).ToString() + "', '')";
                    }
                    //insert do...
                    if (Main.Amatrix.acc == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand(SQL, conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        conn.Close();
                    }
                    else
                    {
                        DataTable dtpy = new DataTable(); basql.Execute(Main.Amatrix.acc, SQL, "", dtpy);
                        dtpy.Clear(); dtpy.Dispose();
                    }
                }
                //invoices
                int maxm2 = 0;
                if (checkBox1.Checked == true)
                {
                    if (Main.Amatrix.acc == "")
                    {
                        SqlCeConnection mySqlConnection3 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                        SqlCeCommand mySqlCommand3 = new SqlCeCommand("SELECT MAX([Serial Number]) FROM invoice", mySqlConnection3);
                        mySqlConnection3.Open();
                        DataTable dtpy = new DataTable();
                        SqlCeDataReader dr = mySqlCommand3.ExecuteReader();
                        dtpy.Load(dr);
                        try
                        {
                            maxm2 = Convert.ToInt32(dtpy.Rows[0].ItemArray[0]);
                            maxm2 = maxm2 + 1;
                        }
                        catch (Exception erty) { maxm2 = 1; }
                        mySqlConnection3.Close();

                        mySqlCommand3.Dispose();
                        mySqlConnection3.Dispose();
                        dtpy.Clear(); dtpy.Dispose();
                    }
                    else
                    {
                        DataTable dtpy = new DataTable(); dtpy = basql.Execute(Main.Amatrix.acc, "SELECT max([Serial Number]) FROM invoice", "invoice", dtpy);
                        try
                        {
                            maxm2 = Convert.ToInt32(dtpy.Rows[0].ItemArray[0]);
                            maxm2 = maxm2 + 1;
                        }
                        catch (Exception erty) { maxm2 = 1; }
                        dtpy.Clear(); dtpy.Dispose();
                    }

                    invce = "INVCE-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString();
                    string sql = "INSERT INTO invoice VALUES('" + maxm2.ToString() + "', '" + invce + "',getdate(), '', '', '', '', '', '', '', '', '', '', '', '" + PROD + "', '" + MAINPROD + "\\" + PROD + "', '', " + cst.ToString() + ", '1', '1', '0', '" + cst.ToString() + "', '0', '0', '', '', '', '', '', '" + tax.ToString() + "', '" + (cst + tax).ToString() + "', '" + (cst +tax).ToString() + "', '','', '')";
                    if (Main.Amatrix.acc == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand(sql, conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        conn.Close();
                    }
                    else
                    {
                        DataTable dtpy = new DataTable();
                        basql.Execute(Main.Amatrix.acc, sql, "invoice", dtpy);
                        dtpy.Clear(); dtpy.Dispose();
                    }
                }
                if (checkBox1.Checked == true && checkBox2.Checked == true)
                {
                    if (comboBox1.Text == "General Journal Entry")
                    {
                        Am_err ner = new Am_err();
                        ner.tx("You Cannot Link General Journal Entries With Invoices");
                    }
                    else
                    {
                        string s = "";
                        if (FORBOOK == "PurchaseBook")
                        {
                            s = "Purchase of Product[" + MAINPROD + "\\" + PROD + "]";
                        }
                        else if (FORBOOK == "SalesBook")
                        { s = "Sale of Product[" + MAINPROD + "\\" + PROD + "]"; }
                        if (Main.Amatrix.acc == "")
                        {
                            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                            SqlCeCommand cmd = new SqlCeCommand("UPDATE " + FORBOOK + " SET [Invoice Number] = '" + invce + "' WHERE [Particulars] = '"+s+"'", conn);
                            conn.Open();
                            SqlCeDataReader dr = cmd.ExecuteReader();
                            SqlCeCommand cmd2 = new SqlCeCommand("UPDATE invoice SET [Binded to Journal] = '" + FORBOOK.ToLower() + "' WHERE [Serial Number] = '" + maxm2.ToString() + "'", conn);
                            dr = cmd2.ExecuteReader();
                            conn.Close();

                            SqlCeConnection conn3 = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                            SqlCeCommand cmd3 = new SqlCeCommand("INSERT INTO acc_linking VALUES('KEY-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString() + "', '" + FORBOOK.ToLower() + "','invoice', '" + maxm + "', '" + invce + "')", conn3);
                            conn3.Open();
                            dr = cmd3.ExecuteReader();
                            conn3.Close();
                        }
                        else
                        {
                            DataTable dtpy = new DataTable();
                            basql.Execute(Main.Amatrix.acc, "UPDATE " + FORBOOK + " SET [Invoice Number] = '" + invce + "' WHERE [Particulars] = '"+s+"'", FORBOOK, dtpy);
                            basql.Execute(Main.Amatrix.acc, "UPDATE invoice SET [Binded to Journal] = '" + FORBOOK.ToLower() + "' WHERE [Serial Number] = '" + maxm2.ToString() + "'", "invoice", dtpy);
                            basql.Execute(Main.Amatrix.acc, "INSERT INTO acc_linking VALUES('KEY-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString() + "', '" + FORBOOK.ToLower() + "','invoice', '" + maxm + "', '" + invce + "')", "acc_linking", dtpy);
                            dtpy.Clear(); dtpy.Dispose();
                        }
                    }
                }
            }
            this.Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "General Journal Entry")
            {
                //comboBox2.Enabled = true;
                if (FROM == "customer managment")
                {
                    checkBox1.Enabled = false;
                    checkBox1.Checked = false;
                }
            }
            else
            {
                comboBox2.Enabled = false;
                if (FROM == "customer managment")
                {
                    checkBox1.Enabled = true;
                    checkBox1.Checked = true;
                }
            }
            if (comboBox1.Text != "General Journal Entry" && comboBox1.Text != "Sales Book Entry" && comboBox1.Text != "Cash Book Entry" && comboBox1.Text != "Purchase Book Entry")
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else { button1.Enabled = true; button2.Enabled = true; }
        }
    }
}
