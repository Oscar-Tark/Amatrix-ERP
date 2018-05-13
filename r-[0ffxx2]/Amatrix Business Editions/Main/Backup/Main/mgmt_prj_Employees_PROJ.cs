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
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class mgmt_prj_Employees_PROJ : Form
    {
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();

        public mgmt_prj_Employees_PROJ()
        {
            this.Icon = Properties.Resources.amdsicnico;
            InitializeComponent();
            this.Disposed += new EventHandler(mgmt_prj_Employees_PROJ_Disposed);
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

        void mgmt_prj_Employees_PROJ_Disposed(object sender, EventArgs e)
        {
            prj_employees.Clear();

            prj_mgmt_employeesTableAdapter.Connection.Close();

            prjmgmtemployeesBindingSource.EndEdit();

            prj_employees.Dispose();
            prj_mgmt_employeesTableAdapter.Dispose();
            prjmgmtemployeesBindingSource.Dispose();

            dtp.Clear();
            dgv.DataSource = null;
            dtp.Dispose();

            this.Activated -= mgmt_prj_Employees_PROJ_Activated;
            this.Deactivate -= mgmt_prj_Employees_PROJ_Deactivate;
            dectmeabt.Tick -= dectmeabt_Tick;
            this.bindingNavigatorAddNewItem.Click -= this.bindingNavigatorAddNewItem_Click;
            this.toolStripButton2.Click -= this.toolStripButton2_Click;
            this.toolStripButton1.Click -= this.toolStripButton1_Click;
            this.button2.Click -= this.button2_Click;
            this.bkk.DoWork -= this.bkk_DoWork;
            this.bkk.RunWorkerCompleted -= this.bkk_RunWorkerCompleted;
            this.dgv.CellMouseClick -= this.dgv_CellMouseClick;
            this.dgv.DataError -= this.dataGridView1_DataError;
            this.textBox3.Leave -= this.textBox3_Leave;
            this.textBox3.Enter -= this.textBox3_Enter;
            this.textBox5.Leave -= this.textBox3_Leave;
            this.textBox5.Enter -= this.textBox3_Enter;
            this.button1.Click -= this.button1_Click;
            this.Load -= this.mgmt_prj_Employees_PROJ_Load;
            this.Disposed -= mgmt_prj_Employees_PROJ_Disposed;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void mgmt_prj_Employees_PROJ_Load(object sender, EventArgs e)
        {
        }

        String task, project;
        public void tx(String Task, String ProjectSER)
        {
            project = ProjectSER;
            task = Task;
            prj_employees.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prj_mgmt_employees WHERE [FORPRJ] = '" + Task +"' AND [PRJSER] = '" + ProjectSER + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prj_employees.prj_mgmt_employees.Load(dr);
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prj_mgmt_employees WHERE [FORPRJ] = '" + Task + "' AND [PRJSER] = '" + ProjectSER + "'");
                prj_employees.prj_mgmt_employees.Load(dr_univ);
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

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            textBox2.Text = "EMPL - " + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString();
            textBox1.Text = task;
            textBox6.Text = project;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.StartsWith("EMPL -") == true)
            {
                if (DialogResult.Yes == MessageBox.Show("Would You Like to Continue this Operation? Altering Pin Numbers May Cause Data Instability.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    textBox2.Text = "EMPL - " + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString();
                }
            }
            else
            {
                textBox2.Text = "EMPL - " + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            prjmgmtemployeesBindingSource.EndEdit();
            if (Main.Amatrix.mgt == "")
            {
                prj_mgmt_employeesTableAdapter.Update(prj_employees);
            }
            else
            {
                asql.Save(prj_employees.prj_mgmt_employees, "prj_mgmt_employees", Main.Amatrix.mgt);
            }
        }

        DataTable dtp = new DataTable();
        private void bkk_DoWork(object sender, DoWorkEventArgs e)
        {
            dtp.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Employ_payrll", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp.Load(dr);
                conn.Close();
            }
            else 
            {
                dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Employ_payrll", "Employ_payrll", dtp);
            }
        }

        private void bkk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgv.DataSource = dtp;
            label5.Visible = false;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            prj_employees.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prj_mgmt_employees", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prj_employees.prj_mgmt_employees.Load(dr);
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prj_mgmt_employees");
                prj_employees.prj_mgmt_employees.Load(dr_univ);
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            dtp.Clear();
            dgv.DataSource = null;
            bkk.RunWorkerAsync();
            panel2.Visible = true;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            dtp.Clear();
            dgv.DataSource = null;
        }

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                textBox3.Text = dgv[0, e.RowIndex].Value.ToString();
                textBox5.Text = dgv[1, e.RowIndex].Value.ToString();
            }
            catch (Exception erty) { }
        }

        private void dectmeabt_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmeabt.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void mgmt_prj_Employees_PROJ_Activated(object sender, EventArgs e)
        {
            dectmeabt.Stop();
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void mgmt_prj_Employees_PROJ_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
        }
    }
}
