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
    public partial class Reports : Form
    {
        private Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        private Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        private DataTable dtp = new DataTable();
        private DataTable dtp2 = new DataTable();

        public Reports()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.amdsicnico;
            InitializeComponent();
            this.Disposed += new EventHandler(Reports_Disposed);
            Init();
            if (Main.Amatrix.mgt != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.mgt); pwd.Owner = this; }
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

        void Reports_Disposed(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            printDialog1.Document = null;
            printPreviewDialog1.Document = null;
            printPreviewDialog1.Dispose();
            printDialog1.Dispose();
            printDocument1.Dispose();

            dtp.Clear();
            dtp.Columns.Clear();
            dtp2.Clear();
            dtp2.Columns.Clear();

            dtp.Dispose();
            dtp2.Dispose();

            this.Disposed -= Reports_Disposed;
            this.tv.NodeMouseClick -= this.tv_mc;
            this.button1.Click -= this.button1_Click;
            this.printDocument1.PrintPage -= this.printDocument1_PrintPage;
            this.printDocument1.BeginPrint -= this.printDocument1_BeginPrint;
            this.button2.Click -= this.button2_Click;
            this.button3.Click -= this.button3_Click;
            this.Load -= this.Reports_Load;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void Init()
        {
            tv.ExpandAll();
        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }

        private void tv_mc(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Logistical MGMT
            try
            {
                if (e.Node.Text.ToLower() == "logisitcal entry(ies) count")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT count([Logistical ID Batch]) FROM Logs_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;

                        printhead("Logistical Transactions Count");
                        richTextBox1.Text = richTextBox1.Text + "\n\n" + "Count: " + dataGridView1[0, 0].Value.ToString() + "\n";
                        printend();
                    }
                    else
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        printhead("Logistical Transactions Count");
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT count([Logistical ID Batch]) FROM Logs_mgmt", "Logs_mgmt", dtp);
                        dataGridView1.DataSource = dtp;
                        richTextBox1.Text = richTextBox1.Text + "\n\n" + "Count:" + dataGridView1[0, 0].Value.ToString() + "\n";
                        printend();
                    }
                }
                else if (e.Node.Text.ToLower() == "logisitcal over-all costs")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT sum([Logisical Costs]) FROM Logs_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;

                        printhead("Logistical Costs (Total of All Transactions)");
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "Credit:" + "\n" + dataGridView1[0, 0].Value.ToString();
                        }
                        catch (Exception erty) { }
                        printend();
                    }
                    else
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        printhead("Logistical Costs (Total of All Transactions)");
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT sum([Logisical Costs]) FROM Logs_mgmt", "Logs_mgmt", dtp);
                        dataGridView1.DataSource = dtp;
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "Credit:" + "\n" + dataGridView1[0, 0].Value.ToString();
                        }
                        catch (Exception erty) { }
                        printend();
                    }
                }
                else if (e.Node.Text.ToLower() == "logistical costs per transportation")
                {
                    dtp.Clear(); dtp.Columns.Clear(); dtp2.Clear(); dtp2.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT [Logistical ID Batch], [Logisical Costs] FROM Logs_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;

                        SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd2 = new SqlCeCommand("SELECT sum([Logisical Costs]) FROM Logs_mgmt", conn2);
                        conn2.Open();
                        SqlCeDataReader dr2 = cmd2.ExecuteReader();
                        dtp2.Load(dr2);
                        conn2.Close();

                        printhead("Logistical Costs (Total of All Transactions)");
                    }
                    else
                    {
                        dtp.Clear(); dtp.Columns.Clear(); dtp2.Clear(); dtp2.Columns.Clear();
                        printhead("Logistical Costs (Total of All Transactions)");
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT [Logistical ID Batch], [Logisical Costs] FROM Logs_mgmt", "Logs_mgmt", dtp);
                        dtp2 = basql.Execute(Main.Amatrix.mgt, "SELECT sum([Logisical Costs]) FROM Logs_mgmt", "Logs_mgmt", dtp2);
                        dataGridView1.DataSource = dtp;
                    }
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "For Batch Number : " + dgvr.Cells[0].Value.ToString() + "  |  The Costs Are : " + dgvr.Cells[1].Value.ToString();
                        }
                        catch (Exception erty) { }
                    }
                    try
                    {
                        richTextBox1.Text = richTextBox1.Text + "\n\n" + "_______________________________________________" + "\n\n" + "Total Costs : " + dtp2.Rows[0].ItemArray[0].ToString() + "\n" + "_______________________________________________";
                    }
                    catch (Exception erty) { }
                    printend();
                }
                else if (e.Node.Text.ToLower() == "goods transported (count)")
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT count([Product Serial Number]) FROM Logs_prod", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;

                        printhead("Count of Goods Transported");

                        richTextBox1.Text = richTextBox1.Text + "\n\n" + "Logistically Moved Goods : " + dataGridView1[0, 0].Value.ToString() + "\n";
                        printend();
                    }
                    else
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        printhead("Count of Goods Transported");
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT count([Product Serial Number]) FROM Logs_prod", "Logs_prod", dtp);
                        dataGridView1.DataSource = dtp;
                        richTextBox1.Text = richTextBox1.Text + "\n\n" + "Logistically Moved Goods : " + dataGridView1[0, 0].Value.ToString() + "\n";
                        printend();
                    }
                }
                else if (e.Node.Text.ToLower() == "goods transported (list)")
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Logs_prod", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;

                        printhead("Goods Transported (List)");
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "For Product (By Reference Number) : " + dgvr.Cells[1].Value.ToString() + "  |  Transportation Reference : " + dgvr.Cells[2].Value.ToString() + "  |  Product (By Name) : " + dgvr.Cells[3].Value.ToString();
                            }
                            catch (Exception erty) { }
                        }
                        richTextBox1.Text = richTextBox1.Text + "\n";
                        printend();
                    }
                    else
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        printhead("Goods Transported (List)");
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Logs_prod", "Logs_prod", dtp);
                        dataGridView1.DataSource = dtp;
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "For Product (By Reference Number) : " + dgvr.Cells[1].Value.ToString() + "  |  Transportation Reference : " + dgvr.Cells[2].Value.ToString() + "  |  Product (By Name) : " + dgvr.Cells[3].Value.ToString();
                            }
                            catch (Exception erty) { }
                        }
                        richTextBox1.Text = richTextBox1.Text + "\n";
                        printend();
                    }
                }
                else if (e.Node.Text.ToLower() == "projects debit report")
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT count([PIN (Project Identification Number]), sum([Project Actual Revenue]), sum([Project Budgeted Revenue]) FROM prj_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;

                        printhead("Project Managment Debit Report");
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "Number of Projects : " + dataGridView1[0, 0].Value.ToString() + "\n-----------------\n" + "Actual Debit : " + dataGridView1[1, 0].Value.ToString() + "\n-----------------\n" + "Budgeted Debit : " + dataGridView1[2, 0].Value.ToString();
                        }
                        catch (Exception erty) { }
                    }
                    else
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT count([PIN (Project Identification Number]), sum([Project Actual Revenue]), sum([Project Budgeted Revenue]) FROM prj_mgmt", "prj_mgmt", dtp);
                        dataGridView1.DataSource = dtp;

                        printhead("Project Managment Debit Report");
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "Number of Projects : " + dataGridView1[0, 0].Value.ToString() + "\n-----------------\n" + "Actual Debit : " + dataGridView1[1, 0].Value.ToString() + "\n-----------------\n" + "Budgeted Debit : " + dataGridView1[2, 0].Value.ToString();
                        }
                        catch (Exception erty) { }
                    }
                }
                else if (e.Node.Text.ToLower() == "projects debit report")
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT count([PIN (Project Identification Number]), sum([Project Actual Revenue]), sum([Project Budgeted Revenue]) FROM prj_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;

                        printhead("Project Managment Debit Report");
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "Number of Projects : " + dataGridView1[0, 0].Value.ToString() + "\n-----------------\n" + "Actual Debit : " + dataGridView1[1, 0].Value.ToString() + "\n-----------------\n" + "Budgeted Debit : " + dataGridView1[2, 0].Value.ToString();
                        }
                        catch (Exception erty) { }
                        printend();
                    }
                    else
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT count([PIN (Project Identification Number]), sum([Project Actual Revenue]), sum([Project Budgeted Revenue]) FROM prj_mgmt", "prj_mgmt", dtp);
                        dataGridView1.DataSource = dtp;
                        printhead("Project Managment Debit Report");
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "Number of Projects : " + dataGridView1[0, 0].Value.ToString() + "\n-----------------\n" + "Actual Debit : " + dataGridView1[1, 0].Value.ToString() + "\n-----------------\n" + "Budgeted Debit : " + dataGridView1[2, 0].Value.ToString();
                        }
                        catch (Exception erty) { }
                        printend();
                    }
                }
                else if (e.Node.Text.ToLower() == "projects credit report")
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT count([PIN (Project Identification Number]), sum([Project Actual Cost]) FROM prj_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;

                        printhead("Project Managment Credit Report");
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "Number of Projects : " + dataGridView1[0, 0].Value.ToString() + "\n-----------------\n" + "Actual Credit : " + dataGridView1[1, 0].Value.ToString();// +"\n-----------------\n" + "Budgeted Debit : " + dataGridView1[2, 0].Value.ToString();
                        }
                        catch (Exception erty) { }
                        printend();
                    }
                    else
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT count([PIN (Project Identification Number]), sum([Project Actual Cost]) FROM prj_mgmt", "prj_mgmt", dtp);
                        dataGridView1.DataSource = dtp;
                        printhead("Project Managment Credit Report");
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "Number of Projects : " + dataGridView1[0, 0].Value.ToString() + "\n-----------------\n" + "Actual Credit : " + dataGridView1[1, 0].Value.ToString();// +"\n-----------------\n" + "Budgeted Debit : " + dataGridView1[2, 0].Value.ToString();
                        }
                        catch (Exception erty) { }
                        printend();
                    }
                }
                else if (e.Node.Text.ToLower() == "project count")
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT count([PIN (Project Identification Number]) FROM prj_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;

                        printhead("Project Count");
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "Number of Projects : " + dataGridView1[0, 0].Value.ToString() + "\n";//-----------------\n" + "Actual Credit : " + dataGridView1[1, 0].Value.ToString();// +"\n-----------------\n" + "Budgeted Debit : " + dataGridView1[2, 0].Value.ToString();
                        }
                        catch (Exception erty) { }
                        printend();
                    }
                    else
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT count([PIN (Project Identification Number]) FROM prj_mgmt", "prj_mgmt", dtp);
                        dataGridView1.DataSource = dtp;

                        printhead("Project Count");
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "Number of Projects : " + dataGridView1[0, 0].Value.ToString() + "\n";//-----------------\n" + "Actual Credit : " + dataGridView1[1, 0].Value.ToString();// +"\n-----------------\n" + "Budgeted Debit : " + dataGridView1[2, 0].Value.ToString();
                        }
                        catch (Exception erty) { }
                        printend();
                    }
                }
                else if (e.Node.Text == "Staff Report For Every Project")
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        DataTable dtp2 = new DataTable();
                        dtp.Clear(); dtp.Columns.Clear();
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT [Task], [FORPRJ] FROM prj_mgmt_tsks", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;

                        printhead("Staff Report For Project Managment");
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "__________TASK START__________" + "\n\n" + "TASK : [" + dgvr.Cells[0].Value.ToString() + "] FOR PROJECT [" + dgvr.Cells[1].Value.ToString() + "] \n\n" + "----------EMPLOYEES----------" + "\n\n";
                                dtp2.Clear();
                                SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                                SqlCeCommand cmd2 = new SqlCeCommand("SELECT * FROM prj_mgmt_employees WHERE [FORPRJ] = '" + dgvr.Cells[0].Value.ToString() + "' AND [PRJSER] = '" + dgvr.Cells[1].Value.ToString() + "'", conn2);
                                conn2.Open();
                                SqlCeDataReader dr2 = cmd2.ExecuteReader();
                                dtp2.Load(dr2);
                                foreach (DataRow drri in dtp2.Rows)
                                {
                                    try
                                    {
                                        richTextBox1.Text = richTextBox1.Text + "\n" + "> " + drri.ItemArray[1].ToString() + " " + drri.ItemArray[2].ToString();
                                    }
                                    catch (Exception erty) { }
                                }
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "----------END EMPLOYEES----------" + "\n\n" + "__________END TASK__________" + "\n\n";
                            }
                            catch (Exception erty) { }
                        }
                        printend();
                    }
                    else
                    {
                        DataTable dtp2 = new DataTable();
                        dtp.Clear(); dtp.Columns.Clear();
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT [Task], [FORPRJ] FROM prj_mgmt_tsks", "prj_mgmt_tsks", dtp);
                        dataGridView1.DataSource = dtp;

                        printhead("Staff Report For Project Managment");
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "__________TASK START__________" + "\n\n" + "TASK : [" + dgvr.Cells[0].Value.ToString() + "] FOR PROJECT [" + dgvr.Cells[1].Value.ToString() + "] \n\n" + "----------EMPLOYEES----------" + "\n\n";
                                dtp2.Clear();
                                dtp2 = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM prj_mgmt_employees WHERE [FORPRJ] = '" + dgvr.Cells[0].Value.ToString() + "' AND [PRJSER] = '" + dgvr.Cells[1].Value.ToString() + "'", "prj_mgmt_employees", dtp2);
                                foreach (DataRow drri in dtp2.Rows)
                                {
                                    try
                                    {
                                        richTextBox1.Text = richTextBox1.Text + "\n" + "> " + drri.ItemArray[1].ToString() + " " + drri.ItemArray[2].ToString();
                                    }
                                    catch (Exception erty) { }
                                }
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "----------END EMPLOYEES----------" + "\n\n" + "__________END TASK__________" + "\n\n";
                            }
                            catch (Exception erty) { }
                        }
                        printend();
                    }
                }
                else if (e.Node.Text == "Delayed, Completed, Cancelled Report")
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT [Delayed], [Cancelled], [Finished], [Project Name], [PIN (Project Identification Number] FROM prj_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;

                        printhead("Project Status Report");
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "Project : [" + dgvr.Cells[3].Value.ToString() + "] Project PIN(" + dgvr.Cells[4].Value.ToString() + ") | STATUS : Finished [" + dgvr.Cells[2].Value.ToString() + "] , Cancelled [" + dgvr.Cells[1].Value.ToString() + "] , Delayed [" + dgvr.Cells[0].Value.ToString() + "]" + "\n\n";
                            }
                            catch (Exception erty) { }
                        }
                    }
                    else
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT [Delayed], [Cancelled], [Finished], [Project Name], [PIN (Project Identification Number] FROM prj_mgmt", "prj_mgmt", dtp);
                        dataGridView1.DataSource = dtp;
                        printhead("Project Status Report");
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "Project : [" + dgvr.Cells[3].Value.ToString() + "] Project PIN(" + dgvr.Cells[4].Value.ToString() + ") | STATUS : Finished [" + dgvr.Cells[2].Value.ToString() + "] , Cancelled [" + dgvr.Cells[1].Value.ToString() + "] , Delayed [" + dgvr.Cells[0].Value.ToString() + "]" + "\n\n";
                            }
                            catch (Exception erty) { }
                        }
                    }
                }
                else if (e.Node.Text == "HR Count")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT count([Employee First Name]) FROM Employ_payrll", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;

                        printhead("HR Count");
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "HR Count : " + dataGridView1[0, 0].Value.ToString() + "\n";//-----------------\n" + "Actual Credit : " + dataGridView1[1, 0].Value.ToString();// +"\n-----------------\n" + "Budgeted Debit : " + dataGridView1[2, 0].Value.ToString();
                        }
                        catch (Exception erty) { }
                        printend();
                    }
                    else
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT count([Employee First Name]) FROM Employ_payrll", "Employ_payrll", dtp);
                        dataGridView1.DataSource = dtp;

                        printhead("HR Count");
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "HR Count : " + dataGridView1[0, 0].Value.ToString() + "\n";//-----------------\n" + "Actual Credit : " + dataGridView1[1, 0].Value.ToString();// +"\n-----------------\n" + "Budgeted Debit : " + dataGridView1[2, 0].Value.ToString();
                        }
                        catch (Exception erty) { }
                        printend();
                    }
                }
                else if (e.Node.Text == "HR Credit Report")
                {
                    System.Collections.ArrayList al_SUM_dy = new System.Collections.ArrayList();
                    System.Collections.ArrayList al_SUM_mnth = new System.Collections.ArrayList();
                    System.Collections.ArrayList al_SUM_YR = new System.Collections.ArrayList();

                    double dy = 0; double mnth = 0; double yr = 0; double benefts = 0; double tax = 0; double bonus = 0; double overhr = 0;
                    double curr_dy, curr_mnth, curr_yr; double overhr_SUM = 0; double tottax = 0;
                    dtp.Clear(); dtp.Columns.Clear();

                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Employ_payrll", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp.Clear(); dtp.Columns.Clear();
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Employ_payrll", "Employ_payrll", dtp);
                        dataGridView1.DataSource = dtp;
                    }

                    dtp2.Clear(); dtp2.Columns.Clear();
                    printhead("HR Credit Report Per Employee");
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        dy = 0; mnth = 0; yr = 0;
                        curr_dy = 0; curr_mnth = 0; curr_yr = 0; tax = 0; overhr = 0;
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n\n" + "For Employee : " + dgvr.Cells[0].Value.ToString() + " " + dgvr.Cells[1].Value.ToString() + "(" + dgvr.Cells[5].Value.ToString() + ")" + "\n\n" + "______FINANCIAL INFORMATION______" + "\n\n";
                            try
                            {
                                overhr = Convert.ToDouble(dgvr.Cells[45].Value) * Convert.ToDouble(dgvr.Cells[46].Value);
                                overhr_SUM = overhr_SUM + overhr;
                            }
                            catch (Exception ertyu) { }
                            try
                            {
                                try
                                {
                                    tax = tax + Convert.ToDouble(dgvr.Cells[50].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    tax = tax + Convert.ToDouble(dgvr.Cells[51].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    tax = tax + Convert.ToDouble(dgvr.Cells[52].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    tax = tax + Convert.ToDouble(dgvr.Cells[53].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    tax = tax + Convert.ToDouble(dgvr.Cells[54].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    tax = tax + Convert.ToDouble(dgvr.Cells[55].Value);
                                }
                                catch (Exception erty) { }
                                tottax = tottax + tax;
                            }
                            catch (Exception erty) { MessageBox.Show(erty.Message); }
                            richTextBox1.Text = richTextBox1.Text + "Pay Per (Hour) [" + dgvr.Cells[14].Value.ToString() + "] , Pay Per (Day) [" + dgvr.Cells[41].Value.ToString() + "] , Pay Per (Month) [" + dgvr.Cells[68].Value.ToString() + "] , Bonuses [" + dgvr.Cells[48].Value.ToString() + "] , Benefits [" + dgvr.Cells[49].Value.ToString() + "] , Over-Hours [" + overhr.ToString() + "] , Taxation (For Employee) [" + tax.ToString() + "]" + "\n\n";

                            curr_dy = Convert.ToDouble(dgvr.Cells[14].Value);
                            curr_mnth = Convert.ToDouble(dgvr.Cells[41].Value);
                            curr_yr = Convert.ToDouble(dgvr.Cells[68].Value);
                            bonus = bonus + Convert.ToDouble(dgvr.Cells[48].Value);
                            benefts = benefts + Convert.ToDouble(dgvr.Cells[49].Value);
                        }
                        catch (Exception ertyu) { }
                        try
                        {
                            if (Main.Amatrix.mgt == "")
                            {
                                SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                                SqlCeCommand cmd2 = new SqlCeCommand("SELECT * FROM payedon WHERE [Pay To] = '" + dgvr.Cells[0].Value.ToString() + " " + dgvr.Cells[1].Value.ToString() + " [" + dgvr.Cells[5].Value.ToString() + "]'", conn2);
                                conn2.Open();
                                SqlCeDataReader dr2 = cmd2.ExecuteReader();
                                dtp2.Load(dr2);
                                conn2.Close();
                            }
                            else
                            {
                                dtp2 = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM payedon WHERE [Pay To] = '" + dgvr.Cells[0].Value.ToString() + " " + dgvr.Cells[1].Value.ToString() + " [" + dgvr.Cells[5].Value.ToString() + "]'", "payedon", dtp2);
                            }

                            foreach (DataRow dri in dtp2.Rows)
                            {
                                try
                                {
                                    richTextBox1.Text = richTextBox1.Text + "> Payed On : [" + dri.ItemArray[1].ToString() + "] Payed By : [" + dri.ItemArray[2].ToString() + "] Payment Number : [" + dri.ItemArray[3].ToString() + "] Payed To : [" + dri.ItemArray[4].ToString() + "]" + "\n";
                                    dy = dy + curr_dy;
                                    mnth = mnth + curr_mnth;
                                    yr = yr + curr_yr;
                                }
                                catch (Exception ertyy) { }
                            }
                            al_SUM_dy.Add(dy);
                            al_SUM_mnth.Add(mnth);
                            al_SUM_YR.Add(yr);
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "____FINANCIAL INFORMATION END____";
                        }
                        catch (Exception erty) { }
                    }
                    dy = 0; mnth = 0; yr = 0;
                    foreach (double d in al_SUM_dy)
                    {
                        try
                        {
                            dy = dy + d;
                        }
                        catch (Exception eryty) { }
                    }
                    foreach (double d in al_SUM_mnth)
                    {
                        try
                        {
                            mnth = mnth + d;
                        }
                        catch (Exception eryty) { }
                    }
                    foreach (double d in al_SUM_YR)
                    {
                        try
                        {
                            yr = yr + d;
                        }
                        catch (Exception eryty) { }
                    }
                    richTextBox1.Text = richTextBox1.Text + "\n\n\n" + "-------------------------------------------------" + "\n" + "TOTALS (UN-TAXED, UN BONUSED, UN-BENEFITED) : " + "\n\n" + "> Per Hour : [" + dy.ToString() + "]" + "\n" + "> Per Day : [" + mnth.ToString() + "]" + "\n" + "> Per Month : [" + yr.ToString() + "]" + "\n" + "> Over-Hours : [" + overhr_SUM.ToString() + "] (Money)" + "\n" + "> Bonuses : [" + bonus.ToString() + "]" + "\n" + "> Benefits [" + benefts.ToString() + "]" + "\n" + "> Summed Taxes (Summed Individually) [" + tottax.ToString() + "]";
                    printend();
                }
                else if (e.Node.Text == "Out-Sourced Credit Report")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM outsrce", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM outsrce", "outsrce", dtp);
                        dataGridView1.DataSource = dtp;
                    }
                    printhead("Out-Sourced Credit Report"); string try_er;
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "Office : " + dgvr.Cells[0].Value.ToString() + " , Head Office : " + dgvr.Cells[1].Value.ToString() + "\n\n" + "> Date of Out-Sourcing Started [" + dgvr.Cells[7].Value.ToString() + "]" + "\n\n" + "> Date of Out-Sourcing Ended [" + dgvr.Cells[8].Value.ToString() + "]" + "\n\n" + "------------------------" + "\n\n" + "> Credit [" + dgvr.Cells[6].Value.ToString() + "]" + "\n\n" + "------------------------";
                            
                            //try
                            //{
                                /*dtp2.Clear(); dtp2.Columns.Clear();
                                try_er = dataGridView1[1, dgvr.Index].Value.ToString();
                                if (Main.Amatrix.mgt == "")
                                {
                                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                                    SqlCeCommand cmd = new SqlCeCommand("SELECT [Employee First Name], [Employees Last Name], [Employee ID] FROM Employ_payrll WHERE [outsourced to office] = '" + dataGridView1[1, dgvr.Index].Value.ToString() + "' AND [is outsourced] = 'Yes'", conn);
                                    conn.Open();
                                    SqlCeDataReader dr = cmd.ExecuteReader();
                                    dtp2.Load(dr);
                                    conn.Close();
                                }
                                else
                                {
                                    dtp2 = basql.Execute(Main.Amatrix.mgt, "SELECT [Employee First Name], [Employees Last Name], [Employee ID] FROM Employ_payrll WHERE [outsourced to office] = '" + dataGridView1[1, dgvr.Index].Value.ToString() + "' AND [is outsourced] = 'Yes'", "Employ_payrll", dtp2);
                                }
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "Employees Involved >>>" + "\n\n";
                                foreach (DataRow dr in dtp2.Rows)
                                {
                                    richTextBox1.Text = richTextBox1.Text + "> [" + dr.ItemArray[0].ToString() + " " + dr.ItemArray[1].ToString() + "] ID [" + dr.ItemArray[2].ToString() + "]" + "\n";
                                }
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "Employees Involved END<<<";*/
                            //}
                            //catch (Exception erty) { }

                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "_______END Office_______" + "\n\n";
                        }
                        catch (Exception erty) { }
                    }


                    dtp2.Clear(); dtp2.Columns.Clear();

                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT sum(Loss) FROM outsrce", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp2.Load(dr);
                        conn.Close();
                    }
                    else
                    {
                        dtp2 = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM outsrce", "outsrce", dtp2);
                    }

                    try
                    {
                        richTextBox1.Text = richTextBox1.Text + "\n" + "> Total Credit [" + dtp2.Rows[0].ItemArray[0].ToString() + "]";
                    }
                    catch (Exception erty)
                    { }
                    printend();
                }
                else if (e.Node.Text == "Out-Sourced Debit Report")
                {
                    dtp.Clear(); dtp.Columns.Clear();

                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM outsrce", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM outsrce", "outsrce", dtp);
                        dataGridView1.DataSource = dtp;
                    }

                    printhead("Out-Sourced Debit Report"); string try_er;
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "Office : " + dgvr.Cells[0].Value.ToString() + " , Head Office : " + dgvr.Cells[1].Value.ToString() + "\n\n" + "> Date of Out-Sourcing Started [" + dgvr.Cells[7].Value.ToString() + "]" + "\n\n" + "> Date of Out-Sourcing Ended [" + dgvr.Cells[8].Value.ToString() + "]" + "\n\n" + "------------------------" + "\n\n" + "> Debit [" + dgvr.Cells[5].Value.ToString() + "]" + "\n\n" + "------------------------";

                            /*try
                            {
                                dtp2.Clear(); dtp2.Columns.Clear();
                                try_er = dataGridView1[1, dgvr.Index].Value.ToString();
                                if (Main.Amatrix.mgt == "")
                                {
                                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                                    SqlCeCommand cmd = new SqlCeCommand("SELECT [Employee First Name], [Employees Last Name], [Employee ID] FROM Employ_payrll WHERE [outsourced to office] = '" + dataGridView1[1, dgvr.Index].Value.ToString() + "' AND [is outsourced] = 'Yes'", conn);
                                    conn.Open();
                                    SqlCeDataReader dr = cmd.ExecuteReader();
                                    dtp2.Load(dr);
                                    conn.Close();
                                }
                                else
                                {
                                    dtp2 = basql.Execute(Main.Amatrix.mgt, "SELECT [Employee First Name], [Employees Last Name], [Employee ID] FROM Employ_payrll WHERE [outsourced to office] = '" + dataGridView1[1, dgvr.Index].Value.ToString() + "' AND [is outsourced] = 'Yes'", "Employ_payrll", dtp2);
                                }
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "Employees Involved >>>" + "\n\n";
                                foreach (DataRow dr in dtp2.Rows)
                                {
                                    richTextBox1.Text = richTextBox1.Text + "> [" + dr.ItemArray[0].ToString() + " " + dr.ItemArray[1].ToString() + "] ID [" + dr.ItemArray[2].ToString() + "]" + "\n";
                                }
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "Employees Involved END<<<";
                            }
                            catch (Exception erty) { }*/
                            
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "_______END Office_______" + "\n\n";
                        }
                        catch (Exception erty) { }
                    }


                    dtp2.Clear(); dtp2.Columns.Clear();

                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT sum(Revenue) FROM outsrce", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp2.Load(dr);
                        conn.Close();
                    }
                    else
                    {
                        dtp2 = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM outsrce", "outsrce", dtp2);
                    }

                    try
                    {
                        richTextBox1.Text = richTextBox1.Text + "\n" + "> Total Credit [" + dtp2.Rows[0].ItemArray[0].ToString() + "]";
                    }
                    catch (Exception erty)
                    { }
                    printend();
                }
                else if (e.Node.Text == "Potential Recruits List")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Empl_selec", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Empl_selec", "Empl_selec", dtp);
                        dataGridView1.DataSource = dtp;
                    }
                    printhead("Selection and Recruitment (Potential Recruits List)");
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "> Name : " + dgvr.Cells[0].Value.ToString() + " " + dgvr.Cells[1].Value.ToString() + " , Rank : " + dgvr.Cells[10].Value.ToString() + " , Applying For : " + dgvr.Cells[9].Value.ToString();
                        }
                        catch (Exception erty) { }
                    }
                    printend();
                }
                else if (e.Node.Text == "Recruitment Status List (Per Employee)")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Empl_selec WHERE [Rank] = '1st Selection'", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Empl_selec WHERE [Rank] = '1st Selection'", "Empl_selec", dtp);
                        dataGridView1.DataSource = dtp;
                    }

                    printhead("Recruitment Status List (Per Employee)");
                    richTextBox1.Text = richTextBox1.Text + "\n\n\n\n" + "_______Rank 1 (First Phase of Selection)_______" + "\n\n";
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n" + "> Name : " + dgvr.Cells[0].Value.ToString() + " " + dgvr.Cells[1].Value.ToString() + " , Rank : " + dgvr.Cells[10].Value.ToString() + " , Applying For : " + dgvr.Cells[9].Value.ToString();
                        }
                        catch (Exception erty) { }
                    }
                    //2
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Empl_selec WHERE [Rank] = '2nd Selection'", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Empl_selec WHERE [Rank] = '2nd Selection'", "Empl_selec", dtp);
                        dataGridView1.DataSource = dtp;
                    }
                    richTextBox1.Text = richTextBox1.Text + "\n\n\n\n" + "_______Rank 2 (Second Phase of Selection)_______" + "\n\n";
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n" + "> Name : " + dgvr.Cells[0].Value.ToString() + " " + dgvr.Cells[1].Value.ToString() + " , Rank : " + dgvr.Cells[10].Value.ToString() + " , Applying For : " + dgvr.Cells[9].Value.ToString();
                        }
                        catch (Exception erty) { }
                    }
                    //3
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Empl_selec WHERE [Rank] = '3rd Selection'", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Empl_selec WHERE [Rank] = '3rd Selection'", "Empl_selec", dtp);
                        dataGridView1.DataSource = dtp;
                    }

                    richTextBox1.Text = richTextBox1.Text + "\n\n\n\n" + "_______Rank 3 (Third Phase of Selection)_______" + "\n\n";
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n" + "> Name : " + dgvr.Cells[0].Value.ToString() + " " + dgvr.Cells[1].Value.ToString() + " , Rank : " + dgvr.Cells[10].Value.ToString() + " , Applying For : " + dgvr.Cells[9].Value.ToString();
                        }
                        catch (Exception erty) { }
                    }
                    //Final
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Empl_selec WHERE [Rank] = 'Final Selection'", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Empl_selec WHERE [Rank] = 'Final Selection'", "Empl_selec", dtp);
                        dataGridView1.DataSource = dtp;
                    }

                    richTextBox1.Text = richTextBox1.Text + "\n\n\n\n" + "_______Final Rank (Final Phase of Selection)_______" + "\n\n";
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n" + "> Name : " + dgvr.Cells[0].Value.ToString() + " " + dgvr.Cells[1].Value.ToString() + " , Rank : " + dgvr.Cells[10].Value.ToString() + " , Applying For : " + dgvr.Cells[9].Value.ToString();
                        }
                        catch (Exception erty) { }
                    }
                    printend();
                }
                else if (e.Node.Text == "Client List")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Customers", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Customers", "Empl_selec", dtp);
                        dataGridView1.DataSource = dtp;
                    }

                    printhead("Customer List (Report)");
                    richTextBox1.Text = richTextBox1.Text + "\n"; int i = 0;
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "> Name : " + dgvr.Cells[1].Value.ToString() + " " + dgvr.Cells[2].Value.ToString() + " , Is Current : " + dgvr.Cells[3].Value.ToString() + " , Date Added : " + dgvr.Cells[4].Value.ToString() + " , Mobile Contact Number : " + dgvr.Cells[6].Value.ToString() + " , Fixed Contact Number : " + dgvr.Cells[7].Value.ToString() + " , Email : " + dgvr.Cells[9].Value.ToString() + " , Official Address : " + dgvr.Cells[12].Value.ToString();
                            i++;
                        }
                        catch (Exception erty) { }
                    }
                    richTextBox1.Text = richTextBox1.Text + "\n\n\n" + "-------------------------------------------------" + "\n\n" + "Customer Count : " + i.ToString();
                    printend();
                }
                else if (e.Node.Text == "Purchased Products (Per Customer)")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Customers", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Customers", "Empl_selec", dtp);
                        dataGridView1.DataSource = dtp;
                    }
                    printhead("Purchased Products (Per Customer)");
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            dtp2.Clear(); dtp2.Columns.Clear();
                            s = "";
                            try
                            {
                                s = dgvr.Cells[0].Value.ToString();
                            }
                            catch (Exception erty) { }
                            if (Main.Amatrix.mgt == "")
                            {
                                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prod_bulk WHERE [Sold To] = '" + s + "'", conn);
                                conn.Open();
                                SqlCeDataReader dr = cmd.ExecuteReader();
                                dtp2.Load(dr);
                                conn.Close();
                            }
                            else
                            {
                                dtp2 = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM prod_bulk WHERE [Sold To] = '" + s + "'", "prod_bulk", dtp2);
                            }
                            try
                            {
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "Customer : [" + dgvr.Cells[0].Value.ToString() + "] First Name : (" + dgvr.Cells[1].Value.ToString() + ") Last Name (" + dgvr.Cells[2].Value.ToString() + ")" + "\n\n";
                                foreach (DataRow drr in dtp2.Rows)
                                {
                                    try
                                    {
                                        richTextBox1.Text = richTextBox1.Text + "\n" + "> Product Inventory : " + drr.ItemArray[1].ToString() + " , For Product {" + drr.ItemArray[2].ToString() + "} , State {" + drr.ItemArray[3].ToString() + "} , Bought On {" + drr.ItemArray[7].ToString() + "} , Bar Code Extension {" + drr.ItemArray[9].ToString() + "}" + "\n";
                                    }
                                    catch (Exception erty) { }
                                }
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "--------------------------------";
                            }
                            catch (Exception erty) { }
                        }
                        catch (Exception erty) { }
                    }
                    richTextBox1.Text = richTextBox1.Text + "\n\n" + "Report Completed at : " + DateTime.Now.ToString();
                    printend();
                }
                else if (e.Node.Text == "Product List")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Prod_mgmt", "Prod_mgmt", dtp);
                        dataGridView1.DataSource = dtp;
                    }
                    int i = 0;
                    printhead("Product List Report");
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "> Product : [" + dgvr.Cells[0].Value.ToString() + "] , Product ID Number : {" + dgvr.Cells[1].Value.ToString() + "} Product Price : {" + dgvr.Cells[5].Value.ToString() + "} Product Taxation : {" + dgvr.Cells[6].Value.ToString() + "} Location : {" + dgvr.Cells[8].Value.ToString() + " (Ware House Address)} Ware-House : {" + dgvr.Cells[7].Value.ToString() + "}";
                            i++;
                        }
                        catch (Exception erty) { }
                    }
                    richTextBox1.Text = richTextBox1.Text + "\n\n\n" + "-------------------------------------------------" + "\n\n" + "Product Count : " + i.ToString();
                    printend();
                }
                else if (e.Node.Text == "Product and Inventory List")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Prod_mgmt", "Prod_mgmt", dtp);
                        dataGridView1.DataSource = dtp;
                    }
                    printhead("Products and Inventory Report");
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            dtp2.Clear(); dtp2.Columns.Clear();
                            s = "";
                            try
                            {
                                s = dgvr.Cells[1].Value.ToString();
                            }
                            catch (Exception erty) { }
                            if (Main.Amatrix.mgt == "")
                            {
                                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prod_bulk WHERE [Notes/Information] = '" + s + "'", conn);
                                conn.Open();
                                SqlCeDataReader dr = cmd.ExecuteReader();
                                dtp2.Load(dr);
                                conn.Close();
                            }
                            else
                            {
                                dtp2 = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM prod_bulk WHERE [Notes/Information] = '" + s + "'", "prod_bulk", dtp2);
                            }
                            try
                            {
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "Product : [" + dgvr.Cells[0].Value.ToString() + "] Product ID : (" + dgvr.Cells[1].Value.ToString() + ")" + "\n\n";
                                foreach (DataRow drr in dtp2.Rows)
                                {
                                    try
                                    {
                                        richTextBox1.Text = richTextBox1.Text + "\n" + "> Product Inventory : " + drr.ItemArray[1].ToString() + " , For Product {" + drr.ItemArray[2].ToString() + "} , State {" + drr.ItemArray[3].ToString() + "} , Bought On {" + drr.ItemArray[7].ToString() + "} , Bar Code Extension {" + drr.ItemArray[9].ToString() + "} , Bought By(if) {" + drr.ItemArray[5].ToString() + "} , Logistical Batch(if) {" + drr.ItemArray[4].ToString() + "} , Delivered(Logistics)(if) {" + drr.ItemArray[8].ToString() + "}" + "\n";
                                    }
                                    catch (Exception erty) { }
                                }
                                richTextBox1.Text = richTextBox1.Text + "\n\n" + "--------------------------------";
                            }
                            catch (Exception erty) { }
                        }
                        catch (Exception erty) { }
                    }
                    richTextBox1.Text = richTextBox1.Text + "\n\n" + "Report Completed at : " + DateTime.Now.ToString();
                    printend();
                }
                else if (e.Node.Text == "All Tasks (Report)")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse5ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Tasks", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Tasks", "Prod_mgmt", dtp);
                        dataGridView1.DataSource = dtp;
                    }

                    printhead("Task List (Report)"); int i = 0;
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "> Task : " + dgvr.Cells[1].Value.ToString() + " , Time Start [" + dgvr.Cells[2].Value.ToString() + "] , Time End [" + dgvr.Cells[3].Value.ToString() + "] , Pay (if Part Time) [" + dgvr.Cells[4].Value.ToString() + "] , Staff Involved {" + dgvr.Cells[5].Value.ToString() + "}";
                            i++;
                        }
                        catch (Exception erty) { }
                    }
                    richTextBox1.Text = richTextBox1.Text + "\n\n\n" + "-------------------------------------------------" + "\n\n" + "Task Count : " + i.ToString();
                    printend();
                }
                else if (e.Node.Text == "Tasks This Month")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse5ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Tasks WHERE datepart(mm, [Time Start]) <= datepart(mm, getdate()) AND datepart(yy, [Time Start]) <= datepart(yy, getdate()) AND datepart(mm, [Time End]) >= datepart(mm, getdate()) AND datepart(yy, [Time End]) >= datepart(yy, getdate())", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Tasks WHERE datepart(mm, [Time Start]) <= datepart(mm, getdate()) AND datepart(yy, [Time Start]) <= datepart(yy, getdate()) AND datepart(mm, [Time End]) >= datepart(mm, getdate()) AND datepart(yy, [Time End]) >= datepart(yy, getdate())", "Tasks", dtp);
                        dataGridView1.DataSource = dtp;
                    }

                    printhead("This Month's Tasks (Report)");
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "> Task : " + dgvr.Cells[1].Value.ToString() + " , Time Start [" + dgvr.Cells[2].Value.ToString() + "] , Time End [" + dgvr.Cells[3].Value.ToString() + "] , Pay (if Part Time) [" + dgvr.Cells[4].Value.ToString() + "] , Staff Involved {" + dgvr.Cells[5].Value.ToString() + "}";
                        }
                        catch (Exception erty) { }
                    }
                    printend();
                }
                else if (e.Node.Text == "Tasks For Today (Starting/Ending Today)")
                {
                    dtp.Clear(); dtp.Columns.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse5ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Tasks WHERE datepart(dd, [Time Start]) = datepart(dd, getdate()) AND datepart(mm, [Time Start]) = datepart(mm, getdate()) AND datepart(yy, [Time Start]) = datepart(yy, getdate()) AND datepart(dd, [Time End]) = datepart(dd, getdate()) AND datepart(mm, [Time End]) = datepart(mm, getdate()) AND datepart(yy, [Time End]) = datepart(yy, getdate())", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp.Load(dr);
                        conn.Close();
                        dataGridView1.DataSource = dtp;
                    }
                    else
                    {
                        dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Tasks WHERE datepart(dd, [Time Start]) = datepart(dd, getdate()) datepart(mm, [Time Start]) = datepart(mm, getdate()) AND datepart(yy, [Time Start]) = datepart(yy, getdate()) AND datepart(dd, [Time End]) = datepart(mm, getdate()) datepart(mm, [Time End]) = datepart(mm, getdate()) AND datepart(yy, [Time End]) = datepart(yy, getdate())", "Tasks", dtp);
                        dataGridView1.DataSource = dtp;
                    }

                    printhead("This Month's Tasks (Report)");
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n\n" + "> Task : " + dgvr.Cells[1].Value.ToString() + " , Time Start [" + dgvr.Cells[2].Value.ToString() + "] , Time End [" + dgvr.Cells[3].Value.ToString() + "] , Pay (if Part Time) [" + dgvr.Cells[4].Value.ToString() + "] , Staff Involved {" + dgvr.Cells[5].Value.ToString() + "}";
                        }
                        catch (Exception erty) { }
                    }
                    printend();
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("An Error Occured, Amatrix was Unable to Process Your Request."); }
        }

        string s = "";
        private void printhead(string Head_Text)
        {
            richTextBox1.Text = Head_Text;
            richTextBox1.Text = richTextBox1.Text + "\n" + "-------------------------------------------------" + "\n";
        }

        private void printend()
        {
            richTextBox1.Text = richTextBox1.Text + "\n\n" + "-------------------------------------------------" + "\n\n" + "Report End";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.Text = richTextBox1.Text;
            richTextBoxPrintCtrl1.Font = richTextBox1.Font;
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            checkPrint = richTextBoxPrintCtrl1.Print(checkPrint, richTextBoxPrintCtrl1.TextLength, e);

            // Check for more pages
            if (checkPrint < richTextBoxPrintCtrl1.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        //printing
        private int checkPrint;
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            checkPrint = 0;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.Text = richTextBox1.Text;
            richTextBoxPrintCtrl1.Font = richTextBox1.Font;
            richTextBoxPrintCtrl1.RightToLeft = richTextBox1.RightToLeft;
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        int ndx_src = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ndx_src = richTextBox1.Text.IndexOf(textBox1.Text, ndx_src);
                richTextBox1.Select(ndx_src, textBox1.Text.Length);
                ndx_src++;
            }
            catch (Exception erty) { ndx_src = 0; }
            richTextBox1.Select();
        }
    }
}
