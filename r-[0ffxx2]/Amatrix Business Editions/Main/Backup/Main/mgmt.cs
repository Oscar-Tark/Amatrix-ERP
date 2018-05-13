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
using System.Collections;
using System.Data.SqlServerCe;
using System.Linq;
using System.IO;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class mgmt : Form
    {
        private ArrayList aund = new ArrayList();
        private ArrayList aundC = new ArrayList();
        private ArrayList aundR = new ArrayList();
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        public mgmt()
        {
            this.Icon = Properties.Resources.amdsicnico;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.Text = "Amatrix Task Managment";
            this.Disposed += new EventHandler(mgmt_Disposed);
            init();
            if (Main.Amatrix.mgt != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text,Main.Amatrix.mgt); pwd.Owner = this; }
                catch (Exception erty) { }
            }
        }

        void mgmt_Disposed(object sender, EventArgs e)
        {
            tasks_DataSet.Clear();
            tasksTableAdapter.Connection.Close();

            tasksBindingSource.Dispose();
            tasksTableAdapter.Dispose();
            tasks_DataSet.Dispose();

            emply_payr_dtst.Clear();
            employ_payrllTableAdapter.Connection.Close();

            employ_payrllTableAdapter.Dispose();
            employpayrllBindingSource.Dispose();
            emply_payr_dtst.Dispose();

            connlbl.MouseEnter -= connlbl_MouseEnter;
            connlbl.MouseLeave -= connlbl_MouseLeave;
            this.button18.Click -= this.button18_Click;
            this.button19.Click -= this.button19_Click;
            this.dataGridView2.CellMouseClick -= this.dataGridView2_CellMouseClick;
            this.button8.Click -= this.button8_Click;
            this.button2.Click -= this.button2_Click;
            this.button1.Click -= this.button1_Click;
            this.dataGridView1.CellBeginEdit -= this.dataGridView1_CellBeginEdit;
            this.sa.Click -= this.sa_Click;
            this.save_inv.Click -= this.sa_Click;
            this.restr.Click -= this.rstrt_Click;
            this.clsemn.Click -= this.clse_ButtonClick;
            this.undoall.Click -= this.undoall_Click;
            this.cpy.Click -= this.cpy_Click;
            this.ct.Click -= this.ct_Click;
            this.pster.Click -= this.pster_Click;
            this.deletecell.Click -= this.deletecell_Click;
            this.sall.Click -= this.sall_Click;
            this.switchDatabaseToolStripMenuItem.Click -= this.switchDatabaseToolStripMenuItem_Click;
            this.rePartitionDataBaseToolStripMenuItem.Click -= this.rePartitionDataBaseToolStripMenuItem_Click;
            this.contentsToolStripMenuItem.Click -= this.contentsToolStripMenuItem_Click;
            this.abtmnu.Click -= this.abtmnu_Click;
            this.pnt.Click -= this.pnt_Click;
            this.svebtn.Click -= this.svebtn_Click;
            this.clse.MouseLeave -= this.clse_MouseEnter;
            this.clse.ButtonClick -= this.clse_ButtonClick;
            this.clse.MouseEnter -= this.clse_MouseEnter;
            this.rstrt.Click -= this.rstrt_Click;
            this.decpr.Tick -= this.decpr_Tick;
            this.tmeclse.Tick -= this.tmeclse_Tick;
            this.Disposed -= mgmt_Disposed;
            this.Deactivate -= this.mgmt_Deactivate;
            this.Load -= this.mgmt_Load;
            this.Activated -= this.mgmt_Activated;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
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
            
        }

        private void mgmt_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tasks_DataSet.Clear();
            if (checkBox1.CheckState == CheckState.Unchecked)
            {
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(tasksTableAdapter.Connection.ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Tasks WHERE [Staff] = '" + cn.Text + "'", conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    tasks_DataSet.Tasks.Load(dr);
                    dataGridView1.DataSource = tasks_DataSet.Tasks;
                    conn.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Tasks WHERE [Staff] = '" + cn.Text + "'", conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    tasks_DataSet.Tasks.Load(dr);
                    dataGridView1.DataSource = tasks_DataSet.Tasks;
                    conn.Close();
                }
            }
            else
            {
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(tasksTableAdapter.Connection.ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Tasks WHERE [Staff] LIKE '%" + cn.Text + "%'", conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    tasks_DataSet.Tasks.Load(dr);
                    dataGridView1.DataSource = tasks_DataSet.Tasks;
                    conn.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Tasks WHERE [Staff] LIKE '%" + cn.Text + "%'", conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    tasks_DataSet.Tasks.Load(dr);
                    dataGridView1.DataSource = tasks_DataSet.Tasks;
                    conn.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (prod_box.Visible == false)
            {
                prod_box.Visible = true;
                showall_d();
            }
            else
            {
                prod_box.Visible = false;
            }
        }

        private void showall_d()
        {
            emply_payr_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Employ_payrll", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                emply_payr_dtst.Employ_payrll.Load(dr);
                conn.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employ_payrll", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                emply_payr_dtst.Employ_payrll.Load(dr);
                conn.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            prod_box.Visible = false;
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dataGridView1[1, dataGridView1.CurrentRow.Index].Value = DateTime.Now;
                try
                {
                    dataGridView1[0, dataGridView1.CurrentRow.Index].Value = dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString() + " " + dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString();
                }
                catch (Exception erty) { dataGridView1[0, dataGridView1.CurrentRow.Index].Value = dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString(); }
                try
                {
                    dataGridView1[0, dataGridView1.CurrentRow.Index].Value = dataGridView1[0, dataGridView1.CurrentRow.Index].Value + " - " + dataGridView2[5, dataGridView2.CurrentRow.Index].Value;
                }
                catch (Exception erty) { MessageBox.Show("You Have Not Entered an Employee ID Code, Searches and Tasks May Fragment into Duplicate Names(Where people Have the Same Name)."); }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Please Select a Task Row."); }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Arraylist addition
            aund.Add(dataGridView1[e.ColumnIndex, e.RowIndex].Value);
            aundC.Add(e.ColumnIndex);
            aundR.Add(e.RowIndex);
            dataGridView1[1, dataGridView1.CurrentRow.Index].Value = DateTime.Now;
        }

        private void svebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Main.Amatrix.mgt == "")
                {
                    tasksTableAdapter.Update(tasks_DataSet);
                }
                else
                {
                    asql.Save(tasks_DataSet.Tasks, "Tasks", Main.Amatrix.mgt);
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Amatrix was Unable to Save Your Task Data."); }
        }

        private void mgmt_Activated(object sender, EventArgs e)
        {
            decpr.Stop();
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void decpr_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                decpr.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void mgmt_Deactivate(object sender, EventArgs e)
        {
            decpr.Start();
        }

        private void tmeclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void rstrt_Click(object sender, EventArgs e)
        {
            mgmt mg = new mgmt();
            mg.Show();
            this.Close();
        }

        private void clse_ButtonClick(object sender, EventArgs e)
        {
            tmeclse.Start();
        }

        private void clse_MouseEnter(object sender, EventArgs e)
        {
            if (clse.DisplayStyle == ToolStripItemDisplayStyle.Image)
            {
                clse.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            }
            else
            {
                clse.DisplayStyle = ToolStripItemDisplayStyle.Image;
            }
        }

        private void pnt_Click(object sender, EventArgs e)
        {
            PrintDataGrid.PrintDGV.Print_DataGridView(dataGridView1);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            showall_d();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            emply_payr_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(employ_payrllTableAdapter.Connection.ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Employ_payrll WHERE [Employee First Name] LIKE '%" + textBox18.Text + "%' OR [Employee Last Name] LIKE '%" + textBox18.Text + "%' OR [Employee ID] LIKE '%" + textBox18.Text + "%'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                emply_payr_dtst.Employ_payrll.Load(dr);
                conn.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employ_payrll WHERE [Employee First Name] LIKE '%" + textBox18.Text + "%' OR [Employee Last Name] LIKE '%" + textBox18.Text + "%' OR [Employee ID] LIKE '%" + textBox18.Text + "%'", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                emply_payr_dtst.Employ_payrll.Load(dr);
                conn.Close();
            }
        }

        private void sa_Click(object sender, EventArgs e)
        {
            tasks_DataSet.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(tasksTableAdapter.Connection.ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Tasks", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                tasks_DataSet.Tasks.Load(dr);
                dataGridView1.DataSource = tasks_DataSet.Tasks;
                conn.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Tasks", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                tasks_DataSet.Tasks.Load(dr);
                dataGridView1.DataSource = tasks_DataSet.Tasks;
                conn.Close();
            }
        }

        private void abtmnu_Click(object sender, EventArgs e)
        {
            app_abt abtat = new app_abt();
            abtat.descr(this.Text);
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.tx(this.Name);
        }

        private void switchDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy_adv adv = new loggy_adv();
            adv.Show();
        }

        private void rePartitionDataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reparttn tn = new reparttn();
            tn.Show();
        }

        private void sall_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
        }

        private void deletecell_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.CurrentCell.Value = DBNull.Value;
            }
            catch (Exception erty)
            {
                try
                {
                    dataGridView1.CurrentCell.Value = null;
                }
                catch (Exception erty1) { }
            }
            if (dataGridView1.SelectedRows.Count > 1)
            {
                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                {
                    dataGridView1.Rows.Remove(dgvr);
                }
            }
        }
        private void undoall_Click(object sender, EventArgs e)
        {
            try
            {
                int x, y;
                x = Convert.ToInt32(aundC[aundC.Count - 1]);
                y = Convert.ToInt32(aundR[aundR.Count - 1]);
                dataGridView1[x, y].Value = aund[aund.Count - 1];

                //remove from undo
                aund.RemoveAt(aund.Count - 1);
                aundC.RemoveAt(aundC.Count - 1);
                aundR.RemoveAt(aundR.Count - 1);
            }
            catch (Exception erty)
            {
                try
                {
                    aund.RemoveAt(aund.Count - 1);
                    aundC.RemoveAt(aundC.Count - 1);
                    aundR.RemoveAt(aundR.Count - 1);
                }
                catch (Exception ett) { }
            }
        }

        private ArrayList copycutpaste = new ArrayList();
        private void cpy_Click(object sender, EventArgs e)
        {
            copycutpaste.Clear();
            try
            {
                copycutpaste.Add(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex].Value);
            }
            catch (Exception erty) { }
        }

        private void pster_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex].Value = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex].Value.ToString() + copycutpaste[0].ToString();
            }
            catch (Exception erty) { Am_err mer = new Am_err(); mer.tx(erty.Message); }
        }

        private void ct_Click(object sender, EventArgs e)
        {
            copycutpaste.Clear();
            try
            {
                copycutpaste.Add(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex].Value);
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex].Value = null;
            }
            catch (Exception erty) { }
        }

        private void ts2_MouseEnter(object sender, EventArgs e)
        {
            ts2.BackColor = Color.AliceBlue;
        }

        private void ts2_MouseLeave(object sender, EventArgs e)
        {
            ts2.BackColor = Color.Lavender;
        }

        private void connlbl_MouseEnter(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = Properties.Resources.bannrrageconv;
        }

        private void connlbl_MouseLeave(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = null;
        }

    }
}
