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
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Threading;
using Base_ASQL;
using Extern_ASQL;

namespace Main
{
    public partial class mgmt_pr : Form
    {
        BASQL basql = new BASQL();
        Extern_Sql asql = new Extern_Sql();
        Gadg_maps mps = new Gadg_maps();

        public mgmt_pr()
        {
            this.Icon = Properties.Resources.amdsicnico;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Disposed += new EventHandler(mgmt_pr_Disposed);
            InitializeComponent();
            this.Text = "Amatrix Customer Managment";
            this.Opacity = Properties.Settings.Default.opacity;
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

        void mgmt_pr_Disposed(object sender, EventArgs e)
        {
            cust_dtst.Clear();
            customersTableAdapter.Connection.Close();

            cust_dtst.Dispose();
            customersBindingSource.Dispose();
            customersTableAdapter.Dispose();

            dtptp.Clear();
            dtptp.Dispose();
            dtp_c.Clear();
            dtp_c.Dispose();
            dtp.Clear();
            dtp.Dispose();

            Sync_bttn.Click -= Sync_bttn_Click;
            bkk_Sync.DoWork -= bkk_Sync_DoWork;
            bkk_color.DoWork -= bkk_color_DoWork;
            button4.Click -= button4_Click;
            button3.Click -= button3_Click_1;
            button2.Click -= cbx_enter;
            saveToolStripMenuItem.Click -= svebtn_ButtonClick;
            restr.Click -= rstrt_Click;
            bindingNavigatorDeleteItem.Click -= bindingNavigatorDeleteItem_Click;
            bindingNavigatorDeleteItem.MouseDown -= bindingNavigatorDeleteItem_MouseDown;
            button15.Click -= button15_Click;
            cn.Enter -= cn_Enter;
            pbx_lgu.BackColorChanged -= pbx_lgu_BackgroundImageChanged;
            eml_snd.Click -= eml_snd_Click;
            toolStripButton3.Click -= toolStripButton3_Click;
            printPurchasedProductsInformationToolStripMenuItem.Click -= reps;
            printCustomerInformationToolStripMenuItem.Click -= reps;
            bindingNavigatorPositionItem.TextChanged -= bindingNavigatorPositionItem_TextChanged;
            //toolStripButton1.Click -= toolStripButton1_Click;
            this.Deactivate -= this.mgmt_pr_Deactivate;
            this.Load -= this.mgmt_pr_Load;
            this.Activated -= this.mgmt_pr_Activated;
            this.tbxfned.Leave -= this.tbxfned_Leave;
            this.tbxfned.Enter -= this.tbxfned_Enter;
            this.tbxfned.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tbxfned.MouseLeave -= this.tvtxt1_MouseLeave;
            this.gotoitm.Click -= this.gotoitm_Click;
            this.edt.Click -= this.reps;
            button14.Click -= button14_Click;
            button1.Click -= this.button1_Click;
            this.pnt.Click -= this.reps;
            this.svebtn.Click -= this.svebtn_ButtonClick;
            this.clse.MouseLeave -= this.clse_MouseLeave;
            this.clse.ButtonClick -= this.clse_ButtonClick;
            this.clse.MouseEnter -= this.clse_MouseEnter;
            this.rstrt.Click -= this.rstrt_Click;
            this.connlbl.MouseEnter -= this.connlbl_MouseEnter;
            this.connlbl.MouseLeave -= this.connlbl_MouseLeave;
            this.connlbl.Click -= this.connlbl_Click;
            //this.nwjrn.Click -= this.reps;
            this.tmeclse.Tick -= this.tmeclse_Tick;
            this.decpr.Tick -= this.decpr_Tick;
            this.clsemn.Click -= this.clse_ButtonClick;
            this.cpy.Click -= this.cpy_Click;
            this.ct.Click -= this.ct_Click;
            this.pster.Click -= this.pster_Click;
            this.deletecell.Click -= this.deletecell_Click;
            this.initializeToolStripMenuItem.Click -= this.initializeToolStripMenuItem_Click;
            this.switchDatabaseToolStripMenuItem.Click -= this.switchDatabaseToolStripMenuItem_Click;
            this.rePartitionDataBaseToolStripMenuItem.Click -= this.rePartitionDataBaseToolStripMenuItem_Click;
            this.contentsToolStripMenuItem.Click -= this.contentsToolStripMenuItem_Click;
            this.abtmnu.Click -= this.abtmnu_Click;
            this.customerListToolStripMenuItem.Click -= this.toolStripButton12_Click;
            //this.customerListDataViewToolStripMenuItem.Click -= this.toolStripButton14_Click;
            //this.toolStripButton14.Click -= this.toolStripButton14_Click;
            this.toolStripButton12.Click -= this.toolStripButton12_Click;
            this.bt_cv.Click -= this.bt_cv_Click;
            this.button12.Click -= this.btop_Click;
            this.btop.Click -= this.btop_Click;
            this.button10.Click -= this.reps;
            this.button9.Click -= this.button9_Click;
            this.remv_pic.Click -= this.remv_pic_Click;
            this.pbx_lgu.MouseLeave -= this.pictureBox6_MouseLeave;
            this.pbx_lgu.Click -= this.bt_cv_Click;
            this.pbx_lgu.MouseEnter -= this.pictureBox6_MouseEnter;
            this.tbx_lgu.TextChanged -= this.tbx_lgu_TextChanged;
            this.tbx_lgu.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tbx_lgu.MouseEnter -= this.tvtxt1_MouseEnter;
            this.sa.Click -= this.sa_Click;
            this.bindingNavigatorPositionItem.MouseEnter -= this.tvtxt1_MouseEnter;
            this.bindingNavigatorPositionItem.MouseLeave -= this.tvtxt1_MouseLeave;
            this.go_emp.ButtonClick -= this.go_emp_ButtonClick;
            this.employeeToolStripMenuItem.Click -= this.employeeToolStripMenuItem_CheckedChanged;
            this.allFieldsToolStripMenuItem.Click -= this.employeeToolStripMenuItem_CheckedChanged;
            this.toolStripTextBox2.Leave -= this.tbxfned_Leave;
            this.toolStripTextBox2.Enter -= this.tbxfned_Enter;
            this.toolStripTextBox2.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox2.MouseLeave -= this.tvtxt1_MouseLeave;
            this.prod_box.Leave -= this.prod_box_Leave;
            this.button11.Click -= this.button11_Click;
            this.button18.Click -= this.button18_Click;
            this.button19.Click -= this.cbx_enter;
            this.dataGridView2.CellMouseClick -= this.dataGridView2_CellMouseClick;
            this.button13.Click -= this.button13_Click;
            //this.button17.Click -= this.button17_Click;
            /*this.comboBox4.Enter -= this.cbx_enter;
            this.comboBox4.MouseEnter -= this.tvtxt1_MouseEnter;
            this.comboBox4.MouseLeave -= this.tvtxt1_MouseLeave;
            this.comboBox4.DropDown -= this.Gen;
            this.comboBox4.TextChanged -= this.Gen;*/
            this.textBox5.TextChanged -= this.Gen;
            this.textBox5.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox5.MouseEnter -= this.tvtxt1_MouseEnter;
            this.button16.Click -= this.button16_Click;
            this.textBox23.TextChanged -= this.Gen;
            this.textBox23.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox23.MouseEnter -= this.tvtxt1_MouseEnter;
            //this.textBox20.TextChanged -= this.Gen;
            //this.textBox20.MouseLeave -= this.tvtxt1_MouseLeave;
            //this.textBox20.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox21.TextChanged -= this.Gen;
            this.textBox21.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox21.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox19.TextChanged -= this.Gen;
            this.textBox19.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox19.MouseEnter -= this.tvtxt1_MouseEnter;
            /*this.textBox16.TextChanged -= this.Gen;
            this.textBox16.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox16.MouseEnter -= this.tvtxt1_MouseEnter;*/
            this.textBox17.TextChanged -= this.Gen;
            this.textBox17.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox17.MouseEnter -= this.tvtxt1_MouseEnter;
            /*this.textBox15.TextChanged -= this.Gen;
            this.textBox15.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox15.MouseEnter -= this.tvtxt1_MouseEnter;*/
            this.textBox14.TextChanged -= this.Gen;
            this.textBox14.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox14.MouseEnter -= this.tvtxt1_MouseEnter;
            this.checkBox1.CheckedChanged -= this.Gen;
            this.dateTimePicker2.ValueChanged -= this.Gen;
            this.dateTimePicker2.DropDown -= this.Gen;
            this.dateTimePicker1.ValueChanged -= this.Gen;
            this.dateTimePicker1.DropDown -= this.Gen;
            this.textBox2.TextChanged -= this.Gen;
            this.textBox2.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox2.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox6.TextChanged -= this.Gen;
            this.textBox6.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox6.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox7.TextChanged -= this.Gen;
            this.textBox7.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox7.MouseEnter -= this.tvtxt1_MouseEnter;
            this.cn.TextChanged -= this.Gen;
            this.cn.MouseLeave -= this.tvtxt1_MouseLeave;
            this.cn.MouseEnter -= this.tvtxt1_MouseEnter;
            //this.button2.Click -= this.button2_Click;
            //this.button3.Click -= this.button3_Click;
            this.textBox3.TextChanged -= this.Gen;
            this.textBox3.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox3.MouseEnter -= this.tvtxt1_MouseEnter;
            //this.button4.Click -= this.button2_Click;
            //this.button5.Click -= this.button3_Click;
            this.textBox8.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox8.MouseEnter -= this.tvtxt1_MouseEnter;
            //this.button7.Click -= this.button2_Click;
            //this.button8.Click -= this.button3_Click;
            this.textBox9.TextChanged -= this.Gen;
            this.textBox9.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox9.MouseEnter -= this.tvtxt1_MouseEnter;
            /*this.toolStripButton9.Click -= this.sa_Click;
            this.toolStripSplitButton1.ButtonClick -= this.go_emp_ButtonClick;
            this.toolStripMenuItem1.Click -= this.employeeToolStripMenuItem_CheckedChanged;
            this.toolStripMenuItem2.Click -= this.employeeToolStripMenuItem_CheckedChanged;
            this.toolStripTextBox5.Leave -= this.tbxfned_Leave;
            this.toolStripTextBox5.Enter -= this.tbxfned_Enter;
            this.toolStripTextBox5.TextChanged -= this.toolStripTextBox5_TextChanged;*/
            this.ttp_del.Tick -= this.ttp_del_Tick;
            this.ofd.FileOk -= this.ofd_FileOk;
            ts2.MouseEnter -= ts2_MouseEnter;
            ts2.MouseLeave -= ts2_MouseLeave;
            this.connectToToolStripMenuItem.Click -= this.connectToToolStripMenuItem_Click;
            this.helpToolStripMenuItem1.Click -= this.contentsToolStripMenuItem_Click;
            mps.Dispose();

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
            mps.Visible = false;
            this.Controls.Add(mps);
            th_initdb_strt();
        }

        private Thread th_initdb;
        private delegate void del_initdb();

        private void th_initdb_strt()
        {
            try
            {
                th_initdb = new Thread(new ThreadStart(del_initdb_strt));
                th_initdb.IsBackground = true;
                th_initdb.Start();
            }
            catch (Exception erty) { init_db(); }
        }

        private void del_initdb_strt()
        {
            try
            {
                this.Invoke(new del_initdb(init_db));
            }
            catch (Exception erty) { init_db(); }
        }

        private DataTable dtp = new DataTable();
        private void init_db()
        {
            try
            {
                Last_Query_Used = "Select * From Customers";
                if (Main.Amatrix.mgt == "")
                {
                    cust_dtst.Clear();
                    string ConnString2 = customersTableAdapter.Connection.ConnectionString;
                    string SqlString2 = "Select * From Customers";
                    using (SqlCeConnection conn2 = new SqlCeConnection(ConnString2))
                    {
                        using (SqlCeCommand cmd2 = new SqlCeCommand(SqlString2, conn2))
                        {
                            cmd2.CommandType = CommandType.Text;
                            conn2.Open();
                            SqlCeDataReader reader2 = cmd2.ExecuteReader();
                            using (reader2)
                            {
                                cust_dtst.Load(reader2, LoadOption.PreserveChanges, "Customers");
                                dgv2.DataSource = cust_dtst.Customers;
                            }
                            conn2.Close();
                        }
                    }
                }
                else
                {
                    pbx_lgu.BackgroundImage = Properties.Resources.person;
                    cust_dtst.Clear();
                    dr_univ = Quer("SELECT * FROM Customers");
                    cust_dtst.Load(dr_univ, LoadOption.PreserveChanges, "Customers");
                    if (Main.Amatrix.mgt != "")
                    {
                        if (dgv2[26, 0].Value != DBNull.Value)
                        {
                            byte[] res1 = (byte[])dgv2[26, 0].Value;
                            Image newImage;
                            using (MemoryStream ms = new MemoryStream(res1, 0, res1.Length))
                            {
                                newImage = Bitmap.FromStream(ms, true);
                                ms.Flush();
                                ms.Close();
                                ms.Dispose();
                            }
                            pbx_lgu.BackgroundImage = newImage;
                        }
                        else
                        {
                            pbx_lgu.BackgroundImage = Properties.Resources.person;
                        }
                    }
                }
                conn();
            }
            catch (Exception ertyuu) { /*Am_err ner = new Am_err(); ner.tx("Amatrix Customer Managment was Unable to Start the Specified Data-Base. Error Information : " + ertyuu.Message);*/ }
        }

        public void tx(string Name_)
        {
            tbxfned.Text = Name_;
            EventArgs e = new EventArgs();
            gotoitm_Click(gotoitm, e);
            this.Show();
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

        private void conn()
        {
            bool t = false;
            if (Main.Amatrix.mgt == "")
            {
                try
                {
                    customersTableAdapter.Connection.Open();
                    t = true;
                    customersTableAdapter.Connection.Close();
                }
                catch (Exception erty) { }
                if (t == true)
                {
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Customer Managment Table is Dynamically Connected";
                    //toolStripDropDownButton9.Enabled = true;
                }
                else if (t == false)
                {
                    connlbl.Text = "Customer Managment Table is Not Connected";
                    connlbl.Image = Properties.Resources.connctno;
                }
                else
                {
                    connlbl.Text = "Customer Managment Table Connectivity Error (Reconnect Please)";
                    connlbl.Image = Properties.Resources.conncerr;
                }
                db_info.Text = customersTableAdapter.Connection.Database; srv_inf.Text = customersTableAdapter.Connection.ServerVersion;
                nds_.Text = customersTableAdapter.Connection.DataSource;
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    conn.Open();
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Customer Managment Table is Dynamically Connected";
                }
                catch (Exception erty)
                {
                    connlbl.Text = "Customer Managment Table is Not Connected";
                    connlbl.Image = Properties.Resources.connctno;
                }
            }
        }

        private void mgmt_pr_Load(object sender, EventArgs e)
        {
        }

        private void connlbl_MouseEnter(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = Properties.Resources.bannrrageconv;
        }

        private void connlbl_MouseLeave(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = null;
        }

        private void clse_MouseEnter(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void clse_MouseLeave(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void rstrt_Click(object sender, EventArgs e)
        {
            mgmt_pr prr = new mgmt_pr();
            prr.Show();
            this.Close();
        }

        private void tmeclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void clse_ButtonClick(object sender, EventArgs e)
        {
            tmeclse.Start();
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

        private void mgmt_pr_Activated(object sender, EventArgs e)
        {
            try
            {
                decpr.Stop();
            }
            catch (Exception erty)
            {
            }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void mgmt_pr_Deactivate(object sender, EventArgs e)
        {
            decpr.Start();
        }

        private void svebtn_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (Main.Amatrix.mgt == "")
                {
                    customersBindingSource.EndEdit();
                    customersTableAdapter.Update(cust_dtst);
                }
                else
                {
                    customersBindingSource.EndEdit();
                    asql.Save(cust_dtst.Customers, "Customers", Main.Amatrix.mgt);
                    try
                    {
                        Main.Amatrix.ascl.broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><typ>w</typ><val>0</val><app>" + this.Name + "</app><par>[" + toolStrip1.Name + "]</par><con>Sync_bttn</con>");
                    }
                    catch (Exception erty) { general_mssg("Syncronization is not Set Up", "Sync Error"); }
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A Fatal Error Occured While Saving Your Information. The Operation Was Halted and Your Data Was not Saved."); }
        }

        private void tbx_C_addr_TextChanged(object sender, EventArgs e)
        {

        }


        private string append_as = ""; private bool from_dataorcnt = false;
        private void button3_Click(object sender, EventArgs e)
        {
            /*if (sender.Equals(button3) == true)
            {
                from_dataorcnt = true;
                listView3.Items.Add(textBox3.Text);
                addr_save(button3);
                from_dataorcnt = false;
            }
            else if (sender.Equals(button5) == true)
            {
                from_dataorcnt = true;
                listView4.Items.Add(textBox8.Text);
                addr_save(button5);
                from_dataorcnt = false;
            }
            else if (sender.Equals(button8) == true)
            {
                from_dataorcnt = true;
                listView5.Items.Add(textBox9.Text);
                addr_save(button8);
                from_dataorcnt = false;
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //from_dataorcnt = true;
            //remv_addr(sender);
            //from_dataorcnt = false;
            //addr_save();
        }

        gadg_pics pcs;
        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (pbx_lgu.BackgroundImage != Properties.Resources.person)
                {
                    pcs = new gadg_pics();
                    pcs.fromfle(pbx_lgu.BackgroundImage);
                    pcs.Location = new Point(103, 42);
                    panel2.Controls.Add(pcs);
                    pcs.BringToFront();
                }
            }
            catch (Exception erty) { }
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                pcs.Dispose();
            }
            catch (Exception erty) { }
        }

        private void go_emp_ButtonClick(object sender, EventArgs e)
        {
            oper_save();
            cust_dtst.Clear();
            string ConnString = customersTableAdapter.Connection.ConnectionString;
            string SqlString = "";
            if (employeeToolStripMenuItem.Checked == true)
            {
                SqlString = "Select * FROM Customers WHERE [Corporate Name] LIKE '%" + toolStripTextBox2.Text + "%'";
            }
            else if (allFieldsToolStripMenuItem.Checked == true)
            {
                SqlString = "Select * FROM Customers WHERE [First Name (Individual)] LIKE '%" + toolStripTextBox2.Text + "%' OR [Last Name (Individual)] LIKE '%" + toolStripTextBox2.Text + "%' OR [First Name (Individual)] + ' ' + [Last Name (Individual)] LIKE '%" + toolStripTextBox2.Text + "%' OR [First Name (Individual)] + [Last Name (Individual)] LIKE '%" + toolStripTextBox2.Text + "%'";
            }
            Last_Query_Used = SqlString;
            if (Main.Amatrix.mgt == "")
            {
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            cust_dtst.Load(reader, LoadOption.PreserveChanges, "Customers");
                        }
                    }
                }
            }
            else
            {
                cust_dtst.Clear();
                dr_univ = Quer(SqlString);
                cust_dtst.Customers.Load(dr_univ);
            }
        }

        private void employeeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (sender.Equals(employeeToolStripMenuItem) == true)
            {
                employeeToolStripMenuItem.Checked = true;
                allFieldsToolStripMenuItem.Checked = false;
            }
            else if (sender.Equals(allFieldsToolStripMenuItem) == true)
            {
                employeeToolStripMenuItem.Checked = false;
                allFieldsToolStripMenuItem.Checked = true;
            }
        }
        private TextBox tbx_temp; private ToolStripTextBox tbx_tstemp; ComboBox cbx_tempcol;
        private void tbxfned_Enter(object sender, EventArgs e)
        {
            try
            {
                tbx_temp = (TextBox)sender;
                tbx_temp.ForeColor = Color.DimGray;
            }
            catch (Exception erty)
            {
                try
                {
                    tbx_tstemp = (ToolStripTextBox)sender;
                    tbx_tstemp.ForeColor = Color.DimGray;
                }
                catch (Exception erty7) { cbx_tempcol = (ComboBox)sender; cbx_tempcol.ForeColor = Color.DimGray; }
            }
        }

        private void tbxfned_Leave(object sender, EventArgs e)
        {
            try
            {
                tbx_temp = (TextBox)sender;
                tbx_temp.ForeColor = Color.LightGray;
            }
            catch (Exception erty)
            {
                try
                {
                    tbx_tstemp = (ToolStripTextBox)sender; tbx_tstemp.ForeColor = Color.LightGray;
                }
                catch (Exception erty71) { cbx_tempcol = (ComboBox)sender; cbx_tempcol.ForeColor = Color.LightGray; }
            }
        }

        private void oper_save()
        {
            if (acc_journ_sett.Default.dynam_jrn == true)
            {
                try
                {
                    customersBindingSource.EndEdit();
                    if (Main.Amatrix.mgt == "")
                    {
                        customersTableAdapter.Update(cust_dtst);
                    }
                    else
                    {
                        asql.Save(cust_dtst.Customers, "Customers", Main.Amatrix.mgt);
                    }
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.ToString()); }
            }
        }

        //search
        string Last_Query_Used;
        private void gotoitm_Click(object sender, EventArgs e)
        {
            oper_save();
            try
            {
                int start = 0;
                string SqlString = "Select * From Customers Where";
                foreach (DataColumn dgvc in cust_dtst.Customers.Columns)
                {
                    try
                    {
                        if (dgvc.ColumnName != "Logo2")
                        {
                            if (start == 0)
                            {
                                if (tbxfned.Text.Contains(' ') == true)
                                {
                                    SqlString = SqlString + " [" + dgvc.ColumnName + "] + ' ' + [" + cust_dtst.Customers.Columns[1].ColumnName + "] LIKE '%" + tbxfned.Text + "%' ";
                                }
                                else
                                {
                                    SqlString = SqlString + " [" + dgvc.ColumnName + "] LIKE '%" + tbxfned.Text + "%' ";
                                }
                                start = 1;
                            }
                            else
                            {
                                SqlString = SqlString + " OR [" + dgvc.ColumnName + "] LIKE '%" + tbxfned.Text + "%'";
                            }
                        }
                    }
                    catch (Exception ertty) { Am_err ner = new Am_err(); ner.tx(ertty.Message); }
                }
                Last_Query_Used = SqlString;
                if (Main.Amatrix.mgt == "")
                {
                    cust_dtst.Clear();
                    string ConnString = customersTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                cust_dtst.Load(reader, LoadOption.PreserveChanges, "Customers");
                                dgv2.DataSource = cust_dtst.Customers;
                            }
                        }
                    }
                }
                else
                {
                    cust_dtst.Clear();
                    dr_univ = Quer(SqlString);
                    cust_dtst.Customers.Load(dr_univ);
                    dgv2.DataSource = cust_dtst.Customers;
                }
                if (Main.Amatrix.mgt != "")
                {
                    if (dgv2[26, dgv2.CurrentRow.Index].Value != DBNull.Value)
                    {
                        byte[] res1 = (byte[])dgv2[26, dgv2.CurrentRow.Index].Value;
                        Image newImage;
                        using (MemoryStream ms = new MemoryStream(res1, 0, res1.Length))
                        {
                            newImage = Bitmap.FromStream(ms, true);
                            ms.Flush();
                            ms.Close();
                            ms.Dispose();
                        }
                        pbx_lgu.BackgroundImage = newImage;
                    }
                    else
                    {
                        pbx_lgu.BackgroundImage = Properties.Resources.person;
                    }
                }
            }
            catch (Exception erty) {  }
        }

        private Thread th_shwall;
        private delegate void del_shwall();

        private void th_shwall_strt()
        {
            try
            {
                th_shwall = new Thread(new ThreadStart(del_shhwall_strt));
                th_shwall.IsBackground = true;
                th_shwall.Start();
            }
            catch (Exception erty) { }
        }

        private void del_shhwall_strt()
        {
            try
            {
                this.Invoke(new del_shwall(showall_GUI));
            }
            catch (Exception erty) { general_mssg("Amatrix was Unable to Process the Request", "Process Interrupted"); }
        }

        private void showall_GUI()
        {
            oper_save();
            string SqlString = "Select * From Customers";
            Last_Query_Used = SqlString;
            if (Main.Amatrix.mgt == "")
            {
                cust_dtst.Clear();
                string ConnString = customersTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            cust_dtst.Load(reader, LoadOption.PreserveChanges, "Customers");
                        }
                    }
                }
            }
            else
            {
                cust_dtst.Clear();
                dr_univ = Quer(SqlString);
                cust_dtst.Customers.Load(dr_univ);
                //image
                if (dgv2[26, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value != DBNull.Value)
                {
                    try
                    {
                        byte[] res1 = (byte[])dgv2[26, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value;
                        Image newImage;
                        using (MemoryStream ms = new MemoryStream(res1, 0, res1.Length))
                        {
                            newImage = Bitmap.FromStream(ms, true);
                            ms.Flush();
                            ms.Close();
                            ms.Dispose();
                        }
                        pbx_lgu.BackgroundImage = newImage;
                    }
                    catch (Exception erty)
                    {
                        pbx_lgu.BackgroundImage = (Image)dgv2[26, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value;
                    }
                }
                else
                {
                    pbx_lgu.BackgroundImage = Properties.Resources.person;
                }
            }
        }

        private void sa_Click(object sender, EventArgs e)
        {
            th_shwall_strt();
        }

        private void general_mssg(string text, string cause1)
        {
            err_inf_1.Text = cause1;
            err_inf_2.Text = text;
            err.Visible = true;

            try
            {
                ttp2.Show(text, this, this.Size.Width - 114, ts2.Location.Y - 10, 5000);
            }
            catch (Exception erty) { }

            ttp_del.Start();
        }

        private void ttp_del_Tick(object sender, EventArgs e)
        {
            ttp_del.Stop();
            if (err.DropDown.Visible != true)
            {
                err.Visible = false;
            }
        }

        private void tbx_lgu_TextChanged(object sender, EventArgs e)
        {
            if (bindingNavigatorPositionItem.Text == "0")
            {
                general_mssg("No Values are Within the Employee/Payroll Table Press the + Button to Add One The Button may be Found in the Tool-bar Above.", "No Entries Within Said Table");
            }
            try
            {
                if (Main.Amatrix.mgt == "")
                {
                    if (tbx_lgu.Text == "" || File.Exists(tbx_lgu.Text) == false)
                    {
                        pbx_lgu.BackgroundImage = Properties.Resources.person;
                    }
                    else
                    {
                        pbx_lgu.BackgroundImage = Image.FromFile(tbx_lgu.Text);
                    }
                }
            }
            catch (Exception erty) { general_mssg("Could Not Set The Specified Logo", erty.Message); }
        }

        private void bt_cv_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                pbx_lgu.BackgroundImage = Image.FromFile(ofd.FileName);
                pbx_lgu.BackgroundImageLayout = ImageLayout.Zoom;
            }
            catch (Exception erty) { general_mssg("Could Not Set The Specified Logo", erty.Message); }
            if (Main.Amatrix.mgt == "")
            {
                tbx_lgu.Text = ofd.FileName;
            }
            else
            {
                Image i = Image.FromFile(ofd.FileName);
                byte[] b;
                try
                {
                    ImageConverter converter = new ImageConverter();
                    Bitmap bmp = new Bitmap(i);
                    b = (byte[])converter.ConvertTo(bmp, typeof(byte[]));
                    dgv2[26, dgv2.CurrentRow.Index].Value = b;
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
        }

        private void remv_pic_Click(object sender, EventArgs e)
        {
            pbx_lgu.BackgroundImage = Properties.Resources.person;
            tbx_lgu.Text = "";
            if (Main.Amatrix.mgt != "")
            {
                dgv2[26, dgv2.CurrentRow.Index].Value = DBNull.Value;
            }
        }

        private void btop_Click(object sender, EventArgs e)
        {
            if (sender.Equals(btop) == true)
            {
                try
                {
                    FileInfo fnf = new FileInfo(tbx_lgu.Text);
                    System.Diagnostics.Process.Start(fnf.DirectoryName);
                }
                catch (Exception erty) { }
            }
            else
            {
                try
                {
                    System.Diagnostics.Process.Start(tbx_lgu.Text);
                }
                catch (Exception erty) { }
            }
        }

        bool ch = false;
        private void purchse_txtch(object sender, EventArgs e)
        {
        }

        private void write_to_purchase_tbx()
        {}

        private void button17_Click(object sender, EventArgs e)
        {
            /*try
            {
                if (Main.Amatrix.mgt == "")
                {
                    ch = true;
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                    conn.Open();
                    SqlCeCommand cmd = new SqlCeCommand("UPDATE prod_bulk SET [Sold To] = '" + cn.Text + "', [State] = 'Sold' WHERE [Notes/Information] = '" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                    conn2.Open();

                    SqlCeCommand cmd2 = new SqlCeCommand("UPDATE prod_bulk SET [On The (Date)] = " + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + " WHERE [Notes/Information] = '" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "'", conn2);
                    cmd2.ExecuteNonQuery();
                    ch = false;
                }
                else
                {
                    ch = true;
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE prod_bulk SET [Sold To] = '" + cn.Text + "', [State] = 'Sold' WHERE [Notes/Information] LIKE '%" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "%' AND [Reference Number] LIKE '%" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "%'", conn);
                    cmd.ExecuteNonQuery();
                    SqlConnection conn2 = new SqlConnection(Main.Amatrix.mgt);
                    conn2.Open();

                    SqlCommand cmd2 = new SqlCommand("UPDATE prod_bulk SET [On The (Date)] = " + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + " WHERE [Notes/Information] LIKE '%" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "%' AND [Reference Number] LIKE '%" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "%'", conn2);
                    cmd2.ExecuteNonQuery();
                    ch = false;
                }
                oper_save();
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Could Not Add the Specified Product."); }

            load_bulk();

            mgmt_Linkto_acc mgmy = new mgmt_Linkto_acc();
            mgmy.tx(dataGridView2.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), cn.Text, "customer managment");
            dtp_c.Clear(); dtptp.Clear();
            dataGridView2.DataSource = dtp_c;
            dataGridView1.DataSource = dtptp;*/
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ch = true; DataTable dtp_temp = new DataTable();
            try
            {
                //old
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                    conn.Open();
                    SqlCeCommand cmd = new SqlCeCommand("UPDATE prod_bulk SET [Sold To] = NULL, [State] = 'Resent-Back From[" + cn.Text + "]' WHERE [Notes/Information] = '" + dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dgv_prods[1, dgv_prods.CurrentRow.Index].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();

                    SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                    SqlCeCommand cmd2 = new SqlCeCommand("SELECT * FROM SalesBook WHERE [Buyer] = '" + cn.Text + "' AND [Particulars] = 'Sale of Product[" + dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString() + "\\" + dgv_prods[1, dgv_prods.CurrentRow.Index].Value.ToString() + "]'", conn2);
                    conn2.Open();
                    SqlCeDataReader dr2 = cmd2.ExecuteReader();
                    dtp_temp.Load(dr2);

                    SqlCeConnection conn3 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                    SqlCeCommand cmd3 = new SqlCeCommand("INSERT INTO prod_resent VALUES('" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString() + "','','Us','" + DateTime.Now.ToString() + "', '', '', '" + cn.Text + "', '', '" + dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString() + "', '" + dgv_prods[1, dgv_prods.CurrentRow.Index].Value.ToString() + "')", conn3);
                    conn3.Open();
                    SqlCeDataReader dr3 = cmd3.ExecuteReader();
                    conn3.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE prod_bulk SET [Sold To] = NULL, [State] = 'Resent-Back From[" + cn.Text + "]' WHERE [Notes/Information] = '" + dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dgv_prods[1, dgv_prods.CurrentRow.Index].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                   
                    dtp_temp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM SalesBook WHERE [Buyer] = '" + cn.Text + "' AND [Particulars] = 'Sale of Product[" + dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString() + "\\" + dgv_prods[1, dgv_prods.CurrentRow.Index].Value.ToString() + "]'", "SalesBook", dtp_temp);
                    DataTable dtp_y = new DataTable();
                    basql.Execute(Main.Amatrix.mgt, "INSERT INTO prod_resent VALUES('" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString() + "','','Us','" + DateTime.Now.ToString() + "', '', '', '" + cn.Text + "', '', '" + dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString() + "', '" + dgv_prods[1, dgv_prods.CurrentRow.Index].Value.ToString() + "')", "prod_resent", dtp_y);
                    dtp_y.Clear(); dtp_y.Dispose();
                }
                //?Understand?

                if (dtp_temp.Rows.Count > 0)
                {
                    //if (DialogResult.Yes == MessageBox.Show("Would you Like to Create a Journal Entry for Product as Revoked?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    //{
                        int maxm = 0; double cst = 0;
                        try
                        {
                            if (Main.Amatrix.acc == "")
                            {
                                SqlCeConnection mySqlConnection3 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                                SqlCeCommand mySqlCommand3 = new SqlCeCommand("SELECT MAX([Serial Number]) FROM SalesBook", mySqlConnection3);
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
                                DataTable dtpy = new DataTable(); basql.Execute(Main.Amatrix.acc, "SELECT MAX([Serial Number]) FROM SalesBook", "SalesBook", dtpy);
                                maxm = Convert.ToInt32(dtpy.Rows[0].ItemArray[0]);
                                maxm = maxm + 1;
                                dtpy.Clear(); dtpy.Dispose();
                            }
                        }
                        catch (Exception erty) { maxm = 1; }
                        //getprod

                        //SELECT product cost
                        DataTable dtp_prod_cost = new DataTable(); double tax = 0;
                        if (Main.Amatrix.mgt == "")
                        {
                            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                            SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt WHERE [Product ID Number] = '" + dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString() + "'", conn);
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
                            dtp_prod_cost = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Prod_mgmt WHERE [Product ID Number] = '" + dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString() + "'", "Prod_mgmt", dtp_prod_cost);
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
                        dtp_prod_cost.Clear(); dtp_prod_cost.Dispose();
                        //insert
                        if (Main.Amatrix.acc == "")
                        {
                            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO SalesBook VALUES('" + maxm.ToString() + "', 'Us', '" + DateTime.Now.ToShortDateString() + "', NULL, '', 'Return of Product[" + dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString() + "\\" + dgv_prods[1, dgv_prods.CurrentRow.Index].Value.ToString() + "]', '0', '" + tax.ToString() + "', '-" + cst.ToString() + "', '', '', '-" + (cst + tax).ToString() + "', '')", conn);
                            conn.Open();
                            SqlCeDataReader dr = cmd.ExecuteReader();
                            conn.Close();
                        }
                        else
                        {
                            DataTable dtpy = new DataTable();
                            basql.Execute(Main.Amatrix.acc, "INSERT INTO SalesBook VALUES('" + maxm.ToString() + "', 'Us', '" + DateTime.Now.ToShortDateString() + "', NULL, '', 'Return of Product[" + dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString() + "\\" + dgv_prods[1, dgv_prods.CurrentRow.Index].Value.ToString() + "]', '0', '" + tax.ToString() + "', '-" + cst.ToString() + "', '', '', '-" + (cst + tax).ToString() + "', '')", "SalesBook", dtpy);
                            dtpy.Clear(); dtpy.Dispose();
                        }

                        if (DialogResult.Yes == MessageBox.Show("Edit Resent Values?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            mgmt_supch_resent rst = new mgmt_supch_resent();
                            rst.tx(dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString(), dgv_prods[1, dgv_prods.CurrentRow.Index].Value.ToString());
                        }
                    //}
                }
            }
            catch (Exception erty) { }
            dtp_temp.Clear();
            dtp_temp.Dispose();
            load_bulk();
            ch = false;
        }

        string s;
        private void cpy_Click(object sender, EventArgs e)
        {
            try
            {
                s = this.ActiveControl.Text;
            }
            catch (Exception erty) { }
        }

        private void pster_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl.Text = this.ActiveControl.Text + s;
            }
            catch (Exception erty) { }
        }

        private void deletecell_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl.Text = "";
            }
            catch (Exception erty) { }
        }

        private void ct_Click(object sender, EventArgs e)
        {
            try
            {
                s = this.ActiveControl.Text;
                this.ActiveControl.Text = "";
            }
            catch (Exception erty) { }
        }

        private void connlbl_Click(object sender, EventArgs e)
        {
            Point ptt = new Point();
            ptt.X = Cursor.Position.X - this.Location.X + 50;
            ptt.Y = Cursor.Position.Y - this.Location.Y;
            cmslv.Show(ptt);
        }

        private void initializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            th_initdb_strt();
        }

        //enter textbox
        Color cl = new Color(); ComboBox cbx_tempcol2;
        private void tvtxt1_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                tbx_tstemp = (ToolStripTextBox)sender;
                cl = tbx_tstemp.BackColor;
                tbx_tstemp.BackColor = Color.White;
            }
            catch (Exception erty)
            {
                try
                {
                    tbx_temp = (TextBox)sender; cl = tbx_temp.BackColor; tbx_temp.BackColor = Color.White;
                }
                catch (Exception er2ty)
                {
                    try
                    {
                        cbx_tempcol2 = (ComboBox)sender; cl = cbx_tempcol2.BackColor; cbx_tempcol2.BackColor = Color.White;
                    }
                    catch (Exception erty45) { }
                }
            }
        }

        private void tvtxt1_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                tbx_tstemp = (ToolStripTextBox)sender;
                tbx_tstemp.BackColor = cl;
            }
            catch (Exception erty)
            {
                try
                {
                    tbx_temp = (TextBox)sender; tbx_temp.BackColor = cl;
                }
                catch (Exception er2ty)
                {
                    try
                    {
                        cbx_tempcol2 = (ComboBox)sender;
                        cbx_tempcol2.BackColor = cl;
                    }
                    catch (Exception erty444) { }
                }
            }
        }

        private DataTable dtptptpt = new DataTable();
        private void Gen(object sender, EventArgs e)
        {
            if (bindingNavigatorPositionItem.Text == "0")
            {
                general_mssg("No Values are Within the Employee/Payroll Table Press the + Button to Add One The Button may be Found in the Tool-bar Above.", "No Entries Within Said Table");
            }
            if (cn.Text == "")
            {
                cn.Text = "Click on Generate Batch Number";
            }
            cn.ReadOnly = false;
            load_bulk();
        }

        private void load_bulk()
        {
            if (Main.Amatrix.mgt == "")
            {
                dtptptpt = new DataTable();
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prod_bulk WHERE [Sold To] = '" + cn.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtptptpt.Load(dr);
                dgv_prods.DataSource = dtptptpt;
            }
            else
            {
                dtptptpt = new DataTable();
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM prod_bulk WHERE [Sold To] = '" + cn.Text + "'", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dtptptpt.Load(dr);
                dgv_prods.DataSource = dtptptpt;
            }
            try
            {
                bkk_color.RunWorkerAsync();
            }
            catch (Exception erty)
            {
                bkk_color.CancelAsync();
                th_warr_strt();
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void connectToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy lg = new loggy();
            lg.Show();
        }

        private DataTable dtp_c = new DataTable();
        private void cbx_enter(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dtp_c = new DataTable();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp_c.Load(dr);
                dataGridView2.DataSource = dtp_c;
                conn.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prod_mgmt", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dtp_c.Load(dr);
                dataGridView2.DataSource = dtp_c;
                conn.Close();
            }
            prod_box.Visible = true;
        }

        private void prod_box_Leave(object sender, EventArgs e)
        {
            prod_box.Visible = false;
        }

        DataTable dtptp = new DataTable();
        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dtptp = new DataTable();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prod_bulk WHERE [Notes/Information] LIKE '%" + dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString() + "%' AND [Sold To] IS NULL OR [Sold To] = '' AND [State] <> 'Sold'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtptp.Load(dr);
                dataGridView1.DataSource = dtptp;
                conn.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM prod_bulk WHERE [Notes/Information] LIKE '%" + dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString() + "%' AND [Sold To] IS NULL OR [Sold To] = '' AND [State] <> 'Sold'", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dtptp.Load(dr);
                dataGridView1.DataSource = dtptp;
                conn.Close();
            }

            try
            {
                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                {
                    Application.DoEvents();
                    if (dgvr.Cells[5].Value.ToString() == cn.Text)
                    {
                        try
                        {
                            foreach (DataGridViewCell dgvcc in dgvr.Cells)
                            {
                                Application.DoEvents();
                                try
                                {
                                    dgvcc.Style.BackColor = Color.DarkOrange;
                                }
                                catch (Exception erty) { }
                            }
                        }
                        catch (Exception erty) { }
                    }
                }
            }
            catch (Exception erty) { }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                dtp_c = new DataTable();
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt WHERE [Product Name] LIKE '%" + textBox4.Text + "%'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp_c.Load(dr);
                dataGridView2.DataSource = dtp_c;
                conn.Close();
            }
            else
            {
                dtp_c = new DataTable();
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prod_mgmt WHERE [Product Name] LIKE '%" + textBox4.Text + "%'", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dtp_c.Load(dr);
                dataGridView2.DataSource = dtp_c;
                conn.Close();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            mgmt_supch sh = new mgmt_supch();
            try
            {
                sh.tx(dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString());
            }
            catch (Exception erty) { sh.Dispose(); }
        }

        private void reps(object sender, EventArgs e)
        {
            if (sender.Equals(printCustomerInformationToolStripMenuItem) == true)
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dgv2);
            }
            else
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dgv_prods);
            }
        }

        private void rePartitionDataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reparttn tin = new reparttn();
            tin.Show();
        }

        private void switchDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy_adv adv = new loggy_adv();
            adv.Show();
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.tx(this.Name);
        }

        private void abtmnu_Click(object sender, EventArgs e)
        {
            app_abt abt = new app_abt();
            abt.descr(this.Text);
        }

        private Button btn_mp;
        private void button9_Click(object sender, EventArgs e)
        {
            btn_mp = (Button)sender;
            mps.Visible = true;
            mps.webBrowser1.Navigate("http://maps.google.co.in/maps?hl=en&tab=wl");
            mps.Dock = DockStyle.Fill;
            mps.BringToFront();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(textBox19.Text);
            }
            catch (Exception erty) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            prod_box.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1[5/*dataGridView1.ColumnCount - 5*/, dataGridView1.CurrentRow.Index].Value = cn.Text;
                dataGridView1[6/*dataGridView1.ColumnCount - 2*/, dataGridView1.CurrentRow.Index].Value = DateTime.Now;
                
                //addprodu
                try
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        ch = true;
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        conn.Open();
                        SqlCeCommand cmd = new SqlCeCommand("UPDATE prod_bulk SET [Sold To] = '" + cn.Text + "', [State] = 'Sold' WHERE [Notes/Information] = '" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "'", conn);
                        cmd.ExecuteNonQuery();
                        SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        conn2.Open();

                        SqlCeCommand cmd2 = new SqlCeCommand("UPDATE prod_bulk SET [On The (Date)] = getdate() WHERE [Notes/Information] = '" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "'", conn2);
                        cmd2.ExecuteNonQuery();
                        ch = false;
                    }
                    else
                    {
                        ch = true;
                        SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE prod_bulk SET [Sold To] = '" + cn.Text + "', [State] = 'Sold' WHERE [Notes/Information] = '" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "'", conn);
                        cmd.ExecuteNonQuery();
                        SqlConnection conn2 = new SqlConnection(Main.Amatrix.mgt);
                        conn2.Open();

                        SqlCommand cmd2 = new SqlCommand("UPDATE prod_bulk SET [On The (Date)] = getdate() WHERE [Notes/Information] = '" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "'", conn2);
                        cmd2.ExecuteNonQuery();
                        ch = false;
                    }
                    oper_save();
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Could Not Add the Specified Product."); }

                load_bulk();

                try
                {
                    mgmt_Linkto_acc mgmy = new mgmt_Linkto_acc();
                    mgmy.tx(dataGridView2.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), cn.Text, "customer managment", 0, 0);
                }
                catch (Exception erty) { MessageBox.Show(erty.Message); }
                dtp_c.Clear(); dtptp.Clear();
                dataGridView2.DataSource = dtp_c;
                dataGridView1.DataSource = dtptp;
                prod_box.Visible = false;
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Unable to Verify, Please Select an Inventory Item."); }
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            if (bindingNavigatorPositionItem.Text == "0")
            {
                panel2.Enabled = false;
            }
            else
            { panel2.Enabled = true; }

            if (Main.Amatrix.mgt != "")
            {
                try
                {
                    if (dgv2[26, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value != DBNull.Value)
                    {
                        try
                        {
                            byte[] res1 = (byte[])dgv2[26, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value;
                            Image newImage;
                            using (MemoryStream ms = new MemoryStream(res1, 0, res1.Length))
                            {
                                newImage = Bitmap.FromStream(ms, true);
                                ms.Flush();
                                ms.Close();
                                ms.Dispose();
                            }
                            pbx_lgu.BackgroundImage = newImage;
                        }
                        catch (Exception erty)
                        {
                            pbx_lgu.BackgroundImage = (Image)dgv2[26, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value;
                        }
                    }
                    else
                    {
                        pbx_lgu.BackgroundImage = Properties.Resources.person;
                    }
                }
                catch (Exception erty)
                {
                }
            }
        }

        private ToolStrip tss;
        private void ts2_MouseEnter(object sender, EventArgs e)
        {
            tss = (ToolStrip)sender;
            tss.BackColor = Color.GhostWhite;
        }

        private void ts2_MouseLeave(object sender, EventArgs e)
        {
            tss = (ToolStrip)sender;
            tss.BackColor = Color.Lavender;
        }

        private void eml_snd_Click(object sender, EventArgs e)
        {
            eml_send snd = new eml_send();
            snd.Send_to(textBox23.Text);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Inventory_Searcher src = new Inventory_Searcher(); src.Show();
        }

        private void pbx_lgu_BackgroundImageChanged(object sender, EventArgs e)
        {
            if (pbx_lgu.BackgroundImage.Size.Height < pbx_lgu.Size.Height || pbx_lgu.BackgroundImage.Size.Width < pbx_lgu.Size.Width)
            {
                pbx_lgu.BackgroundImageLayout = ImageLayout.Center;
            }
            else
            {
                pbx_lgu.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void cn_Enter(object sender, EventArgs e)
        {
            if (cn.Text != "" && bindingNavigatorCountItem.Text != "0")
            {
                cn.ReadOnly = true;
            }
            else
            { cn.ReadOnly = false; }
        }
        //new
        private void button15_Click(object sender, EventArgs e)
        {
            if (cn.Text.StartsWith("AM-CUS-") == true)
            {
                if (DialogResult.Yes == MessageBox.Show("Would You Like to Continue this Operation? Altering Logistical Pin Numbers May Cause Data Instability.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    cn.Text = "AM-CUS-" + DateTime.Now.ToString() + "-ID";
                }
            }
            else
            {
                try
                {
                    cn.Text = "AM-CUS-" + DateTime.Now.ToString() + "-ID";
                }
                catch (Exception erty) { cn.Text = "AM-CUS-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString() + "-ID"; }
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DataTable dtpp = new DataTable();
            if (Main.Amatrix.mgt == "")
            {
                /*SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("DELETE FROM Logs_prod WHERE [Package/box Number] = '" + poop + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();*/

                if (DialogResult.Yes == MessageBox.Show("Change the State of Your Sold Products(inventory items) for this Customer from sold to empty?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString); DataGridViewCheckBoxCell l = new DataGridViewCheckBoxCell();
                    SqlCeCommand cmd2 = new SqlCeCommand("UPDATE prod_bulk SET [State] = '' WHERE [Sold To] = '" + poop + "' AND [State] = 'Sold'", conn2);
                    conn2.Open();
                    cmd2.ExecuteNonQuery();
                    conn2.Close();
                }

                SqlCeConnection conn3 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd3 = new SqlCeCommand("UPDATE prod_bulk SET [Sold To] = '' WHERE [Sold To] = '" + poop + "'", conn3);
                conn3.Open();
                cmd3.ExecuteNonQuery();
                conn3.Close();
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("Change the State of Your Sold Products(inventory items) for this Customer from sold to empty?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    basql.Execute(Main.Amatrix.mgt, "UPDATE prod_bulk SET [State] = '' WHERE [Sold To] = '" + poop + "' AND [State] = 'Sold'", "prod_bulk", dtpp);
                }
                dtpp.Clear();
                basql.Execute(Main.Amatrix.mgt, "UPDATE prod_bulk SET [Sold To] = '' WHERE [Sold To] = '" + poop + "'", "prod_bulk", dtpp);
            }
            dtpp.Clear();
            dtpp.Dispose();
        }
        string poop;
        private void bindingNavigatorDeleteItem_MouseDown(object sender, MouseEventArgs e)
        {
            poop = cn.Text;
        }

        string s_location = ""; String our_addr = ""; string Izd = "";
        private void button3_Click_1(object sender, EventArgs e)
        {
            bool abort = false;
            if (DialogResult.Yes == MessageBox.Show("Generate a Logistical Route?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                mgmt_opr_choose_routeinfo tre = new mgmt_opr_choose_routeinfo();
                tre.tx(dtptptpt, s_location, textBox6.Text + " " + textBox2.Text, textBox7.Text, textBox8.Text, textBox9.Text, textBox3.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mgmt_supch_warranties warr = new mgmt_supch_warranties();
            try
            {
                warr.tx(dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString(), dgv_prods[1, dgv_prods.CurrentRow.Index].Value.ToString(), cn.Text);
                warr.Disposed += new EventHandler(warr_Disposed);
            }
            catch (Exception erty) { warr.Close(); Am_err ner = new Am_err(); ner.tx("No Warranty Value Available"); }
        }

        void warr_Disposed(object sender, EventArgs e)
        {
            load_bulk();
        }

        //Warranty Eval

        private Thread th_warr;
        private delegate void del_warr();

        private void th_warr_strt()
        {
            try
            {
                th_warr = new Thread(new ThreadStart(del_warr_strt));
                th_warr.IsBackground = true;
                th_warr.Priority = ThreadPriority.BelowNormal;
                th_warr.Start();
            }
            catch (Exception erty) { }
        }

        private void del_warr_strt()
        {
            try
            {
                this.Invoke(new del_warr(warranty_eval));
            }
            catch (Exception erty) { }
        }

        private DataTable dtp_warr; private DateTime dt1, dt2;
        private void warranty_eval()
        {
            dtp_warr = new DataTable();
            foreach (DataGridViewRow dgvr in dgv_prods.Rows)
            {
                try
                {
                    dtp_warr.Clear(); string SQL = "SELECT * FROM prod_warranties WHERE [For Product] = '" + dgvr.Cells[2].Value.ToString() + "' AND [For Bulk] = '" + dgvr.Cells[1].Value.ToString() + "'";
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand(SQL, conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp_warr.Load(dr);
                    }
                    else
                    {
                        dtp_warr = basql.Execute(Main.Amatrix.mgt, SQL, "prod_warranties", dtp_warr);
                    }

                    //eval
                    if (dtp_warr.Rows[0].ItemArray[6].ToString() == cn.Text && dtp_warr.Rows[0].ItemArray[1].ToString() == "Yes")
                    {
                        dt1 = Convert.ToDateTime(dtp_warr.Rows[0].ItemArray[2].ToString());
                        dt2 = Convert.ToDateTime(dtp_warr.Rows[0].ItemArray[3].ToString());

                        if (dt1 <= DateTime.Now && dt2 >= DateTime.Now)
                        {
                            dgvr.Cells[0].Style.BackColor = Color.DarkOrange;
                        }
                        else
                        {
                            dgvr.Cells[0].Style.BackColor = Color.LightGreen;
                        }
                    }
                }
                catch (Exception erty) { }
            }
            dtp_warr.Clear();
            dtp_warr.Dispose();
        }

        private void bkk_color_DoWork(object sender, DoWorkEventArgs e)
        {
            th_warr_strt();
        }

        //new
        int whereami = 0;
        Thread Sync_th;
        delegate void Sync_del();
        private void Sync_bttn_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Sync_bttn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.Text = this.Text + " [Synchronizing]";
            whereami = Convert.ToInt32(bindingNavigatorPositionItem.Text);
            try
            {
                bkk_Sync.RunWorkerAsync();
            }
            catch (Exception erty) { Sync_bttn.BackColor = Color.DarkOrange; this.Enabled = true; this.Text = this.Text.Replace(" [Synchronizing]", ""); }
        }

        private void bkk_Sync_DoWork(object sender, DoWorkEventArgs e)
        {
            Sync_th = new Thread(new ThreadStart(del_Sync_strt));
            Sync_th.IsBackground = true;
            Sync_th.Start();
        }

        private void del_Sync_strt()
        {
            this.Invoke(new Sync_del(Sync));
        }

        SQLCEBasql.SQLCEBSQ scsql = new SQLCEBasql.SQLCEBSQ();
        private void Sync()
        {
            cust_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                cust_dtst.Customers.Load(dr);
                conn.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand(Last_Query_Used, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                cust_dtst.Customers.Load(dr);
                conn.Close();
            }
            load_bulk();
            customersBindingSource.Position = whereami - 1;
            this.Text = this.Text.Replace(" [Synchronizing]", "");
            this.Enabled = true;
            Sync_bttn.BackColor = Color.Transparent;
        }
    }
}
