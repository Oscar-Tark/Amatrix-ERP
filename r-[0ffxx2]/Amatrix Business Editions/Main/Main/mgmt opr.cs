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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Data.SqlServerCe;
using System.Text;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using Base_ASQL;
using Extern_ASQL;
using Microsoft.PointOfService;

namespace Main
{
    public partial class mgmt_opr : Form
    {
        private PosExplorer explorer;
        private ArrayList scannerList;
        private Scanner activeScanner;
        BASQL basql = new BASQL();
        Extern_Sql asql = new Extern_Sql();

        public mgmt_opr()
        {
            this.Icon = Properties.Resources.amdsicnico;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            initdb();
            this.Disposed += new EventHandler(mgmt_opr_Disposed);
            this.Text = "Amatrix Logistics Managment";
            Init();
            //th_init_db_strt();
            cnt = new Control();
            conn_main();
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

        private void Init()
        {
            mps.Visible = false;
            this.Controls.Add(mps);
            if (bindingNavigatorPositionItem.Text == "0")
            {
                general_mssg("No Values are Within the Logistics Table Press the + Button to Add One The Button may be Found in the Tool-bar Above.", "No Entries Within Said Table");
            }
        }

        Thread th_init_db;
        delegate void del_init_db();
        private void th_init_db_strt()
        {
            try
            {
                th_init_db = new Thread(new ThreadStart(del_init_db_strt));
                th_init_db.IsBackground = true;
                th_init_db.Start();
            }
            catch (Exception erty) { initdb(); }
        }

        private void del_init_db_strt()
        {
            try
            {
                this.Invoke(new del_init_db(initdb));
            }
            catch (Exception erty) { initdb(); }
        }

        private void initdb()
        {
            try
            {
                Last_Query_Used = "SELECT * FROM Logs_mgmt";
                logstcs_dtst.Logs_mgmt.RowChanged += new DataRowChangeEventHandler(Logs_mgmt_RowChanged);
                if (Main.Amatrix.mgt == "")
                {
                    this.logs_mgmtTableAdapter.Fill(this.logstcs_dtst.Logs_mgmt);
                }
                else
                {
                    logstcs_dtst.Clear();
                    dr_univ = Quer("SELECT * FROM Logs_mgmt");
                    logstcs_dtst.Load(dr_univ, LoadOption.PreserveChanges, "Logs_mgmt");
                }
                toolStripButton2.Enabled = true;
            }
            catch (Exception erty) { }
        }

        void mgmt_opr_Disposed(object sender, EventArgs e)
        {
            logs_prod_dtst.Clear();
            logs_prodTableAdapter.Connection.Close();

            logstcs_dtst.Clear();
            logs_mgmtTableAdapter.Connection.Close();
            dataGridView2.DataSource = null;

            logs_prod_dtst.Dispose();
            logs_prodTableAdapter.Dispose();
            logsprodBindingSource.Dispose();

            p2p_dtst.Clear();
            p2p_logsTableAdapter.Connection.Close();

            p2p_dtst.Dispose();
            p2p_logsTableAdapter.Dispose();
            p2plogsBindingSource.Dispose();

            logstcs_dtst.Dispose();
            logs_mgmtTableAdapter.Dispose();
            logsmgmtBindingSource.Dispose();

            emply_payr_dtst.Clear();
            employ_payrllTableAdapter.Connection.Close();

            emply_payr_dtst.Dispose();
            employ_payrllTableAdapter.Dispose();
            employpayrllBindingSource.Dispose();

            dgv_prods_.DataSource = null; 
            try
            {
                activeScanner.DataEvent -= activeScanner_DataEvent;
                activeScanner.ErrorEvent -= activeScanner_ErrorEvent;
            }
            catch (Exception erty) { }

            logstcs_dtst.Logs_mgmt.RowChanged -= Logs_mgmt_RowChanged;
            log_read.Tick -= log_read_Tick;
            bkk_sync.RunWorkerCompleted -= bkk_sync_RunWorkerCompleted;
            bkk_logread.DoWork -= bkk_logread_DoWork;
            toolStripButton2.Click -= toolStripButton2_Click_1;
            bkk_sync.DoWork -= bkk_sync_DoWork;
            checkBox2.Click -= checkBox2_Click;
            dgv_prods_.DataError -= dgv_prods__DataError;
            dgv_prods_.CellBeginEdit -= dgv_prods__CellBeginEdit;
            toolStripButton4.Click -= toolStripButton2_Click;
            toolStripButton4.MouseDown -= toolStripButton2_MouseDown;
            bindingNavigatorDeleteItem.MouseDown -= bindingNavigatorDeleteItem_MouseDown;
            bindingNavigatorDeleteItem.Click -= bindingNavigatorDeleteItem_Click;
            toolStripButton3.Click -= toolStripButton3_Click;
            printBulkProductInformationToolStripMenuItem.Click -= pnt_Click;
            printLogisticalInformationToolStripMenuItem.Click -= pnt_Click;
            ts2.MouseEnter -= ts2_MouseEnter;
            ts2.MouseLeave -= ts2_MouseLeave;
            button5.Click -= button5_Click;
            svebtn.Click -= svebtn_ButtonClick;
            toolStripButton1.Click -= toolStripButton1_Click;
            this.Deactivate -= this.mgmt_opr_Deactivate;
            this.Load -= this.mgmt_opr_Load;
            this.Activated -= this.mgmt_opr_Activated;
            this.tbxfned.Leave -= this.tbxfned_Leave;
            this.tbxfned.Enter -= this.tbxfned_Enter;
            this.gotoitm.Click -= this.go_emp_Click;
            this.pnt.Click -= this.pnt_Click;
            this.clse.MouseLeave -= this.clse_MouseLeave;
            this.clse.ButtonClick -= this.clse_ButtonClick;
            this.clse.MouseEnter -= this.clse_MouseEnter;
            this.rstrt.Click -= this.rstrt_Click;
            this.connlbl.MouseEnter -= this.connlbl_MouseEnter;
            this.connlbl.MouseLeave -= this.connlbl_MouseLeave;
            this.connlbl.Click -= this.connlbl_Click;
            this.save_inv.Click -= this.svebtn_ButtonClick;
            this.restr.Click -= this.rstrt_Click;
            this.clsemn.Click -= this.clse_ButtonClick;
            this.cpy.Click -= this.cpy_Click;
            this.ct.Click -= this.ct_Click;
            this.pster.Click -= this.pster_Click;
            this.deletecell.Click -= this.deletecell_Click;
            this.sall.Click -= this.selectAllToolStripMenuItem_Click;
            this.initializeToolStripMenuItem.Click -= this.sa_Click;
            this.switchDatabaseToolStripMenuItem.Click -= this.switchDatabaseToolStripMenuItem_Click;
            this.rePartitionDataBaseToolStripMenuItem.Click -= this.rePartitionDataBaseToolStripMenuItem_Click;
            this.contentsToolStripMenuItem.Click -= this.helpToolStripMenuItem1_Click;
            this.abtmnu.Click -= this.abtmnu_Click;
            this.ttp_del.Tick -= this.ttp_del_Tick;
            this.bindingNavigator1.Click -= this.tabPage3_Enter;
            this.sa.Click -= this.sa_Click;
            this.bindingNavigatorPositionItem.TextChanged -= this.bindingNavigatorPositionItem_TextChanged;
            this.textBox8.Enter -= this.tabPage3_Enter;
            this.textBox6.Enter -= this.tabPage3_Enter;
            this.button2.Click -= this.gen_batch_n;
            //this.button4.Enter -= this.tabPage3_Enter;
            this.comboBox1.Enter -= this.tabPage3_Enter;
            this.textBox5.Enter -= this.tabPage3_Enter;
            this.textBox4.Enter -= this.tabPage3_Enter;
            this.textBox3.Enter -= this.tabPage3_Enter;
            this.textBox2.Enter -= this.tabPage3_Enter;
            this.textBox1.Enter -= this.tabPage3_Enter;
            this.dateTimePicker2.Enter -= this.tabPage3_Enter;
            this.dateTimePicker1.Enter -= this.tabPage3_Enter;
           //this.lde_scn.Click -= this.lde_scn_Click;
           //this.lde_scn.Enter -= this.tabPage3_Enter;
            this.brcde_txt.TextChanged -= this.brcde_txt_TextChanged;
            this.cn.TextChanged -= this.id_ch;
            this.cn.Enter -= this.tabPage3_Enter;
            this.bindingNavigator3.Enter -= this.tabPage3_Enter;
            this.button1.Click -= this.button1_Click;
            this.button7.Click -= this.button7_Click;
            this.dgv_prods_.CellMouseLeave -= this.dgv_CellMouseLeave;
            this.dgv_prods_.Enter -= this.tabPage3_Enter;
            this.dgv_prods_.Leave -= this.dataGridView1_Leave;
            this.dgv_prods_.CellMouseEnter -= this.dgv_CellMouseEnter;
            this.dgv_prods_.CellEndEdit -= this.dgv_prods__CellEndEdit;
            this.deleteToolStripMenuItem.Click -= this.deleteToolStripMenuItem_Click;
            this.selectAllToolStripMenuItem.Click -= this.selectAllToolStripMenuItem_Click;
            this.textBox7.TextChanged -= this.prods;
            this.textBox7.Enter -= this.tabPage3_Enter;
            this.ofd_bc.FileOk -= this.ofd_bc_FileOk;
            this.connectToToolStripMenuItem.Click -= this.connectToToolStripMenuItem_Click;
            this.helpToolStripMenuItem1.Click -= this.helpToolStripMenuItem1_Click;
            this.tmeclse.Tick -= this.tmeclse_Tick;
            this.decpr.Tick -= this.decpr_Tick;
            this.button9.Click -= this.button8_Click;
            this.button3.Click -= this.button8_Click;
            this.button8.Click -= this.button8_Click;
            this.dataGridView2.CellMouseClick -= this.empl_selex;
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

        private void conn_p2p()
        {
            bool f = false;
            if (Main.Amatrix.mgt == "")
            {
                try
                {
                    p2p_logsTableAdapter.Connection.Open();
                    f = true;
                    p2p_logsTableAdapter.Connection.Close();
                }
                catch (Exception errtt) { f = false; }

                if (f == true)
                {
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Point to Point Table is Connected";
                }
                else if (f == false)
                {
                    connlbl.Text = "Point to Point Table is Not Connected";
                    connlbl.Image = Properties.Resources.connctno;
                }
                db_info.Text = p2p_logsTableAdapter.Connection.Database; srv_inf.Text = p2p_logsTableAdapter.Connection.ServerVersion;
                nds_.Text = p2p_logsTableAdapter.Connection.DataSource;
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    conn.Open();
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Point to Point Table is Connected";
                }
                catch (Exception ertyt)
                {
                    connlbl.Text = "Point to Point Table is Not Connected";
                    connlbl.Image = Properties.Resources.connctno;
                }
            }
        }

        private void conn_main()
        {
            if (Main.Amatrix.mgt == "")
            {
                if (cnt.Equals(dgv_prods_) == true || cnt.Equals(bindingNavigator3) == true)
                {
                    if (cnt.Equals(dgv_prods_) == true)
                    {
                        cpy.Enabled = false;
                        ct.Enabled = false;
                        pster.Enabled = false;
                        sall.Enabled = true;
                        sall.Text = "Select All";
                    }
                    bool f = false;
                    try
                    {
                        logs_prodTableAdapter.Connection.Open();
                        f = true;
                        logs_prodTableAdapter.Connection.Close();
                    }
                    catch (Exception errtt) { f = false; }

                    if (f == true)
                    {
                        connlbl.Image = Properties.Resources.conncted;
                        connlbl.Text = "Logistical Product Table is Connected";
                    }
                    else if (f == false)
                    {
                        connlbl.Text = "Logistical Product Table is Not Connected";
                        connlbl.Image = Properties.Resources.connctno;
                    }
                    db_info.Text = logs_prodTableAdapter.Connection.Database; srv_inf.Text = logs_prodTableAdapter.Connection.ServerVersion;
                    nds_.Text = logs_prodTableAdapter.Connection.DataSource;
                }
                else
                {
                    bool f = false;
                    try
                    {
                        logs_mgmtTableAdapter.Connection.Open();
                        f = true;
                        logs_mgmtTableAdapter.Connection.Close();
                    }
                    catch (Exception errtt) { f = false; }

                    if (f == true)
                    {
                        connlbl.Image = Properties.Resources.conncted;
                        connlbl.Text = "Logistical Table is Connected";
                    }
                    else if (f == false)
                    {
                        connlbl.Text = "Logistical Table is Not Connected";
                        connlbl.Image = Properties.Resources.connctno;
                    }
                    db_info.Text = logs_mgmtTableAdapter.Connection.Database; srv_inf.Text = logs_mgmtTableAdapter.Connection.ServerVersion;
                    nds_.Text = logs_mgmtTableAdapter.Connection.DataSource;
                }
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    conn.Open();
                    conn.Close();
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Logistical Managment is Connected";
                }
                catch (Exception erty)
                {
                    connlbl.Text = "Logistical Managment is Not Connected";
                    connlbl.Image = Properties.Resources.connctno;
                }
            }
        }

        private DataTable dtp = new DataTable();
        private DataTable dtp_p2p = new DataTable();
        private void mgmt_opr_Load(object sender, EventArgs e)
        {
            //pos
            try
            {
                DeviceInfo di = explorer.GetDevice(DeviceType.Scanner);
                activeScanner = (Scanner)explorer.CreateInstance(di);
                activeScanner.Open();
                activeScanner.Claim(1000);
                activeScanner.DeviceEnabled = true;
                activeScanner.DataEvent += new DataEventHandler(activeScanner_DataEvent);
                activeScanner.ErrorEvent += new DeviceErrorEventHandler(activeScanner_ErrorEvent);
                activeScanner.DecodeData = true;
                activeScanner.DataEventEnabled = true;
            }
            catch (Exception erty) { }
        }

        //POS Events
        void activeScanner_DataEvent(object sender, DataEventArgs e)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            try
            {
                // Display the ASCII encoded label text
                if (DialogResult.Yes == MessageBox.Show("You Have Loaded a BarCode, Would You Like to Insert it in the Bar-Code TextBox For Your Product? To Do So Click on Yes. If Instead You Would like to Insert it into the Search TextBox Click on No.", "Amatrix Bar-Coder", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    brcde_txt.Text = encoder.GetString(activeScanner.ScanDataLabel);
                }
                else
                {
                    tbxfned.Text = encoder.GetString(activeScanner.ScanDataLabel);
                }
                // re-enable the data event for subsequent scans
                activeScanner.DataEventEnabled = true;
            }
            catch (PosControlException)
            {
                // Log any errors
                Am_err ner = new Am_err(); ner.tx("An error Occured during the Bar-Scan");
            }
        }

        void activeScanner_ErrorEvent(object sender, DeviceErrorEventArgs e)
        {
            try
            {
                // re-enable the data event for subsequent scans
                activeScanner.DataEventEnabled = true;
            }
            catch (PosControlException)
            { }
        }

        SqlDataReader dr_univ;
        private SqlDataReader Quer(string Query)
        {
            oper_save();
            SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
            SqlCommand cmd = new SqlCommand(Query, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
            conn.Close();
        }

        private void clse_ButtonClick(object sender, EventArgs e)
        {
            tmeclse.Start();
        }

        private void univ_txtch(object sender, EventArgs e)
        {
        }
        //error messaging

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

        private void general_mssg(string text, string cause1, int Interval)
        {
            err_inf_1.Text = cause1;
            err_inf_2.Text = text;
            err.Visible = true;

            try
            {
                ttp2.ToolTipTitle = "Informator";
                ttp2.Show(text, this, this.Size.Width - 114, ts2.Location.Y - 10, Interval);
            }
            catch (Exception erty) { }

            ttp_del.Interval = Interval;
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

        private void p2p_txtch(object sender, EventArgs e)
        {
            /*if (textBox13.Text == "")
            {
                load_max();
                textBox13.Text = cn.Text;
            }*/
        }

        private void load_max()
        {
            oper_save();
            DataTable dtp = new DataTable();
            int f = 0;
            if (Main.Amatrix.mgt == "")
            {
                string ConnString = p2p_logsTableAdapter.Connection.ConnectionString;
                string SqlString = "Select max([Serial Number]) From p2p_logs";
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            dtp.Load(reader);
                        }
                        conn.Close();
                    }
                }
            }
            else
            {
                dtp.Clear();
                dr_univ = Quer("Select max([Serial Number]) From p2p_logs");
                dtp.Load(dr_univ);
            }
            f = Convert.ToInt32(dtp.Rows[0].ItemArray[0].ToString());
            f = f + 1;
            //textBox12.Text = f.ToString();
        }

        private void load_p2p()
        {
            oper_save();
            try
            {
                p2p_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    string ConnString = p2p_logsTableAdapter.Connection.ConnectionString;
                    string SqlString = "Select * From p2p_logs WHERE [Reference ID] = '" + cn.Text + "'";
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                p2p_dtst.Load(reader, LoadOption.PreserveChanges, "p2p_logs");
                            }
                            conn.Close();
                        }
                    }
                }
                else
                {
                    dr_univ = Quer("Select max([Serial Number]) From p2p_logs");
                    p2p_dtst.Load(dr_univ, LoadOption.PreserveChanges, "p2p_logs");
                }
            }
            catch (Exception erty) { general_mssg("Unable To Load your Point to Point Data.", ""); }
        }

        private void id_ch(object sender, EventArgs e)
        {
            if (cn.Text == "")
            {
                cn.Text = "Click on Generate Batch Number";
            }
            p2plogsBindingSource.EndEdit();
            p2p_logsTableAdapter.Update(p2p_dtst);
            load_p2p();
            load_prods();
        }

        private void oper_save()
        {
            if (acc_journ_sett.Default.autosave == true)
            {
                try
                {
                    logsmgmtBindingSource.EndEdit();
                    if (Main.Amatrix.mgt == "")
                    {
                        logs_mgmtTableAdapter.Update(logstcs_dtst);
                        logs_prodTableAdapter.Update(logs_prod_dtst);
                        p2p_logsTableAdapter.Update(p2p_dtst);
                    }
                    else
                    {
                        asql.Save(logstcs_dtst.Logs_mgmt, logstcs_dtst.Logs_mgmt.TableName, Main.Amatrix.mgt);
                        asql.Save(logs_prod_dtst.Logs_prod, logs_prod_dtst.Logs_prod.TableName, Main.Amatrix.mgt);
                        asql.Save(p2p_dtst.p2p_logs, p2p_dtst.p2p_logs.TableName, Main.Amatrix.mgt);
                    }
                }
                catch (Exception erty) { }
            }
        }

        private void svebtn_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (Main.Amatrix.mgt == "")
                {
                    cn.Text = cn.Text;
                    logsmgmtBindingSource.EndEdit();
                    logs_mgmtTableAdapter.Update(logstcs_dtst);
                }
                else
                {
                    cn.Text = cn.Text;
                    logsmgmtBindingSource.EndEdit();
                    asql.Save(logstcs_dtst.Logs_mgmt, logstcs_dtst.Logs_mgmt.TableName, Main.Amatrix.mgt);

                    try
                    {
                        Main.Amatrix.ascl.broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><typ>w</typ><val>0</val><app>" + this.Name + "</app><par>[" + toolStrip1.Name + "]</par><con>toolStripButton2</con>");
                    }
                    catch (Exception erty) { general_mssg("Syncronization is not Set Up", "Sync Error"); }
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A Fatal Error Occured While Saving Your Information. The Operation Was Halted and Your Data Was not Saved."); }
        }

        private void bindingNavigatorPositionItem1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Main.Amatrix.mgt == "")
                {
                    p2plogsBindingSource.EndEdit();
                    p2p_logsTableAdapter.Update(p2p_dtst);
                }
                else
                {
                    p2plogsBindingSource.EndEdit();
                    asql.Save(p2p_dtst.p2p_logs, p2p_dtst.p2p_logs.TableName, Main.Amatrix.mgt);
                }
            }
            catch (Exception erty) { }
        }

        public void glue(string prod_nme)
        {
            load_prods();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (cn.Text == "")
            {
                Am_err ner = new Am_err();
                ner.tx("Please add a Product First by Clicking on the + Button, if You did So you may Want to check and see if Logistical ID Batch on the Basic Information Tab is not Empty.");
            }
            else
            {
                logs_pick_prods prr = new logs_pick_prods();
                prr.tx(this, cn.Text);
            }
        }

        private void load_prods()
        {
            oper_save();
            try
            {
                logs_prod_dtst.Clear();
                string SqlString = "Select * From Logs_prod WHERE [Package/box Number] = '" + cn.Text + "'";
                if (Main.Amatrix.mgt == "")
                {
                    string ConnString = logs_prodTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                logs_prod_dtst.Load(reader, LoadOption.PreserveChanges, "Logs_prod");
                                dgv_prods_.DataSource = logs_prod_dtst.Logs_prod;
                            }
                            conn.Close();
                        }
                    }
                }
                else
                {
                    dr_univ = Quer(SqlString);
                    logs_prod_dtst.Load(dr_univ, LoadOption.PreserveChanges, "Logs_prod");
                }
            }
            catch (Exception erty) { general_mssg("Unable To Load your Product Data.", erty.Message); }
        }

        private void prods(object sender, EventArgs e)
        {
            load_prods();
        }

        private void gen_batch_n(object sender, EventArgs e)
        {
            if (cn.Text.StartsWith("AM-LOD-") == true)
            {
                if (DialogResult.Yes == MessageBox.Show("Would You Like to Continue this Operation? Altering Logistical Pin Numbers May Cause Data Instability.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    cn.Text = "AM-LOD-" + DateTime.Now.ToString() + "-ID";
                }
            }
            else
            {
                try
                {
                    cn.Text = "AM-LOD-" + DateTime.Now.ToString() + "-ID";
                }
                catch (Exception erty)
                {
                    cn.Text = "AM-LOD-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString() + "-ID";
                }
            }
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            if (bindingNavigatorPositionItem.Text == "0")
            {
                panel2.Enabled = false;
            }
            else
            {
                panel2.Enabled = true;
            }

            if (cn.Text == "")
            {
                cn.Text = "AM-LOD-" + DateTime.Now.ToString() + "-ID";
            }
        }

        private void lde_scn_Click(object sender, EventArgs e)
        {
            ofd_bc.ShowDialog();
        }

        private void brcde_txt_TextChanged(object sender, EventArgs e)
        {
            if (brcde_txt.Text == "")
            {
                img.BackgroundImage = Properties.Resources.br_cde;
            }
            else
            {
                try
                {
                    img.BackgroundImage = Image.FromFile(brcde_txt.Text);
                }
                catch (Exception erty) { img.BackgroundImage = Properties.Resources.br_cde; }
            }
        }

        private void ofd_bc_FileOk(object sender, CancelEventArgs e)
        {
            img.BackgroundImage = Image.FromFile(ofd_bc.FileName);
            if (Main.Amatrix.mgt == "")
            {
                brcde_txt.Text = ofd_bc.FileName;
            }
            else
            {
                Image i = Image.FromFile(ofd_bc.FileName);
                byte[] b;
                try
                {
                    ImageConverter converter = new ImageConverter();
                    Bitmap bmp = new Bitmap(i);
                    b = (byte[])converter.ConvertTo(bmp, typeof(byte[]));
                    dgv2[18, dgv2.CurrentRow.Index].Value = b;
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
        }

        //enter textbox
        ToolStripTextBox tbx_tstemp;
        TextBox tbx_temp;
        Color cl = new Color(); ComboBox cbx_tempcol;
        /*private void tvtxt1_MouseEnter(object sender, EventArgs e)
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
                        cbx_tempcol = (ComboBox)sender; cl = cbx_tempcol.BackColor; cbx_tempcol.BackColor = Color.White;
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
                        cbx_tempcol = (ComboBox)sender;
                        cbx_tempcol.BackColor = cl;
                    }
                    catch (Exception erty444) { }
                }
            }
        }*/

        string s;
        private void cpy_Click(object sender, EventArgs e)
        {
            try
            {
                s = this.ActiveControl.Text;
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
            if (this.ActiveControl == dgv_prods_)
            {
                foreach (DataGridViewRow dgvr in dgv_prods_.SelectedRows)
                {
                    dgv_prods_.Rows.Remove(dgvr);
                }
            }
            else
            {
            }
        }

        Control cnt;
        private void tabPage3_Enter(object sender, EventArgs e)
        {
            cnt = (Control)sender;
            conn_main();
            if (sender.Equals(textBox4) == true)
            {
                emply_payr_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(employ_payrllTableAdapter.Connection.ConnectionString);
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
                prod_box.Visible = true;
            }
            else { prod_box.Visible = false; }
        }

        private void p2p_entr(object sender, EventArgs e)
        {
            conn_p2p();
        }

        string Last_Query_Used = "";
        private void go_emp_Click(object sender, EventArgs e)
        {
            oper_save();
            try
            {
                int start = 0;
                string SqlString = "Select * From Logs_mgmt Where";
                foreach (DataColumn dgvc in logstcs_dtst.Logs_mgmt.Columns)
                {
                    if (dgvc.ColumnName != "Bar Code2")
                    {
                        try
                        {
                            if (start == 0)
                            {
                                SqlString = SqlString + " [" + dgvc.ColumnName + "] LIKE '%" + tbxfned.Text + "%' ";
                                start = 1;
                            }
                            else
                            {
                                SqlString = SqlString + " OR [" + dgvc.ColumnName + "] LIKE '%" + tbxfned.Text + "%'";
                            }
                        }
                        catch (Exception ertty) { Am_err ner = new Am_err(); ner.tx(ertty.Message); }
                    }
                }
                Last_Query_Used = SqlString;
                logstcs_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    string ConnString = logs_mgmtTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                logstcs_dtst.Load(reader, LoadOption.PreserveChanges, "Logs_mgmt");
                            }
                        }
                    }
                }
                else
                {
                    dr_univ = Quer(SqlString);
                    logstcs_dtst.Load(dr_univ, LoadOption.PreserveChanges, "Logs_mgmt");
                }
                //img
                if (dgv2[18, dgv2.CurrentRow.Index].Value != DBNull.Value)
                {
                    byte[] res1 = (byte[])dgv2[18, dgv2.CurrentRow.Index].Value;
                    Image newImage;
                    using (MemoryStream ms = new MemoryStream(res1, 0, res1.Length))
                    {
                        newImage = Bitmap.FromStream(ms, true);
                        ms.Flush();
                        ms.Close();
                        ms.Dispose();
                    }
                    img.BackgroundImage = newImage;
                }
                else
                {
                    img.BackgroundImage = null;
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
        }

        private void clse_MouseEnter(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void clse_MouseLeave(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void sa_Click(object sender, EventArgs e)
        {
            oper_save();
            logstcs_dtst.Clear();
            string SqlString = "SELECT * FROM Logs_mgmt";
            Last_Query_Used = SqlString;
            if (Main.Amatrix.mgt == "")
            {
                string ConnString = logs_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            logstcs_dtst.Load(reader, LoadOption.PreserveChanges, "Logs_mgmt");
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                logstcs_dtst.Load(dr_univ, LoadOption.PreserveChanges, "Logs_mgmt");
            }
        }

        private void rstrt_Click(object sender, EventArgs e)
        {
            mgmt_opr opr = new mgmt_opr();
            opr.Show();
            this.Close();
        }

        private void abtmnu_Click(object sender, EventArgs e)
        {
            app_abt bt = new app_abt();
            bt.descr(this.Text);
        }

        private void connlbl_Click(object sender, EventArgs e)
        {
            Point ptt = new Point();
            ptt.X = Cursor.Position.X - this.Location.X + 50;
            ptt.Y = Cursor.Position.Y - this.Location.Y;
            cmslv.Show(ptt);
        }

        private void connlbl_MouseEnter(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = Properties.Resources.bannrrageconv;
        }

        private void connlbl_MouseLeave(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = null;
        }

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

        private Color cl_tmp;
        private void dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (visual.Default.Basic == false)
            {
                try
                {
                    cl_tmp = dgv_prods_.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                    dgv_prods_.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.AliceBlue;
                }
                catch (Exception erty) { }
            }
            else { }
        }

        private void dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (visual.Default.Basic == false)
            {
                try
                {
                    dgv_prods_.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = cl_tmp;
                }
                catch (Exception erty) { }
            }
            else { }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgv_prods_.SelectedRows)
            {
                dgv_prods_.Rows.Remove(dgvr);
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgv_prods_.SelectAll();
            if (Main.Amatrix.mgt == "")
            {
                logs_prodTableAdapter.Update(logs_prod_dtst);
            }
            else
            {
                asql.Save(logstcs_dtst.Logs_mgmt, "Logs_mgmt", Main.Amatrix.mgt);
            }
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
            cpy.Enabled = true;
            ct.Enabled = true;
            pster.Enabled = true;
            sall.Enabled = false;
            sall.Text = "Select All (Right Click on Control For this Option)";
        }

        private void connectToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy lg = new loggy();
            lg.Show();
        }

        private void dgv_prods__CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                logs_prodTableAdapter.Update(logs_prod_dtst);
            }
            else
            {
                asql.Save(logs_prod_dtst.Logs_prod, "Logs_prod", Main.Amatrix.mgt);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mgmt_supch sh = new mgmt_supch();
            sh.tx(dgv_prods_.CurrentRow.Cells[3].Value.ToString());
        }

        private void tmeclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
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

        private void mgmt_opr_Activated(object sender, EventArgs e)
        {
            try
            {
                decpr.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void mgmt_opr_Deactivate(object sender, EventArgs e)
        {
            decpr.Start();
        }

        private void empl_selex(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString() != "" && dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString() != "")
                {
                    if (textBox4.Text == "")
                    {
                    }
                    else if (textBox4.Text != "" && checkBox1.Checked == true) { textBox4.Text = textBox4.Text + ", "; }
                    textBox4.Text = textBox4.Text + dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString() + " " + dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString();
                    if (textBox5.Text == "")
                    { }
                    else if (textBox5.Text != "" && checkBox1.Checked == true) { textBox5.Text = textBox5.Text + ", "; }
                    textBox5.Text = textBox5.Text + dataGridView2[5, dataGridView2.CurrentRow.Index].Value.ToString();
                }
            }
            else
            {
                if (dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString() != "" && dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString() != "")
                {
                    textBox4.Text = dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString() + " " + dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString();
                    textBox5.Text = dataGridView2[5, dataGridView2.CurrentRow.Index].Value.ToString();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (sender.Equals(button8) == true)
            {
                emply_payr_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(employ_payrllTableAdapter.Connection.ConnectionString);
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
            else if (sender.Equals(button3) == true)
            {
                emply_payr_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(employ_payrllTableAdapter.Connection.ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Employ_payrll WHERE [Employee First Name] LIKE '%" + textBox11.Text + "%' OR [Employee Last Name] LIKE '%" + textBox11.Text + "%' OR [Employee ID] LIKE '%" + textBox11.Text + "%'", conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    emply_payr_dtst.Employ_payrll.Load(dr);
                    conn.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Employ_payrll WHERE [Employee First Name] LIKE '%" + textBox11.Text + "%' OR [Employee Last Name] LIKE '%" + textBox11.Text + "%' OR [Employee ID] LIKE '%" + textBox11.Text + "%'", conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    emply_payr_dtst.Employ_payrll.Load(dr);
                    conn.Close();
                }
            }
            else if (sender.Equals(button9) == true)
            {
                mgmt_hr hr = new mgmt_hr();
                hr.tx(dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString(), dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString());
            }
        }

        private void pnt_Click(object sender, EventArgs e)
        {
            if (sender.Equals(printLogisticalInformationToolStripMenuItem) == true)
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dgv2);
            }
            else
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dgv_prods_);
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            brcde_txt.Text = "";
        }

        Gadg_maps mps = new Gadg_maps();
        private void button5_Click(object sender, EventArgs e)
        {
            mps.Visible = true;
            mps.webBrowser1.Navigate("http://maps.google.co.in/maps?hl=en&tab=wl");
            mps.Dock = DockStyle.Fill;
            mps.BringToFront();
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            mgmt_supch sh = new mgmt_supch();
            sh.Show();
        }

        public void tx(String Batch)
        {
            try
            {
                //bkk_init.CancelAsync();
                logstcs_dtst.Clear();
                string sql = "SELECT * FROM Logs_mgmt WHERE [Logistical ID Batch] = '" + Batch + "'";
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand(sql, conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    logstcs_dtst.Logs_mgmt.Load(dr);
                    conn.Close();
                }
                else
                {
                    dr_univ = Quer(sql);
                    logstcs_dtst.Logs_mgmt.Load(dr_univ);
                }
            }
            catch (Exception erty) { }
            this.Show();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DataTable dtpp = new DataTable();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("DELETE FROM Logs_prod WHERE [Package/box Number] = '" + poop + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                if (DialogResult.Yes == MessageBox.Show("Change the State of Your Transported Products(inventory items) from Shipping (Logistics) to empty?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString); DataGridViewCheckBoxCell l = new DataGridViewCheckBoxCell();
                    SqlCeCommand cmd2 = new SqlCeCommand("UPDATE prod_bulk SET [State] = '' WHERE [Logistical Batch] = '" + poop + "' AND [State] = 'Shipping (Logistics)'", conn2);
                    conn2.Open();
                    cmd2.ExecuteNonQuery();
                    conn2.Close();
                }

                SqlCeConnection conn3 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd3 = new SqlCeCommand("UPDATE prod_bulk SET [Logistical Batch] = '' WHERE [Logistical Batch] = '" + poop + "'", conn3);
                conn3.Open();
                cmd3.ExecuteNonQuery();
                conn3.Close();
            }
            else
            {
                basql.Execute(Main.Amatrix.mgt, "DELETE FROM Logs_prod WHERE [Package/box Number] = '" + poop + "'", "Logs_prod", dtpp);
                dtpp.Clear();
                if (DialogResult.Yes == MessageBox.Show("Change the State of Your Transported Products(inventory items) from Shipping (Logistics) to empty?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    basql.Execute(Main.Amatrix.mgt, "UPDATE prod_bulk SET [State] = '' WHERE [Logistical Batch] = '" + poop + "' AND [State] = 'Shipping (Logistics)'", "prod_bulk", dtpp);
                    dtpp.Clear();
                }
                basql.Execute(Main.Amatrix.mgt, "UPDATE prod_bulk SET [Logistical Batch] = '' WHERE [Logistical Batch] = '" + poop + "'", "prod_bulk", dtpp);
            }
            //logs_prod_dtst.Clear();
            dtpp.Clear();
            dtpp.Dispose();
        }
        //new
        string poop;
        private void bindingNavigatorDeleteItem_MouseDown(object sender, MouseEventArgs e)
        {
            poop = cn.Text;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (poop2 != null || poop2 != "" && dgv_prods_[1, dgv_prods_.CurrentRow.Index].Value != DBNull.Value)
                {
                    DataTable dtpp = new DataTable();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString); DataGridViewCheckBoxCell l = new DataGridViewCheckBoxCell();
                        SqlCeCommand cmd2 = new SqlCeCommand("UPDATE prod_bulk SET [State] = '' WHERE [Logistical Batch] = '" + cn.Text + "' AND [State] = 'Shipping (Logistics)' AND [Reference Number] = '" + dgv_prods_[1, dgv_prods_.CurrentRow.Index].Value.ToString() + "' AND [Notes/Information] = '" + dgv_prods_[4, dgv_prods_.CurrentRow.Index].Value.ToString() + "'", conn2);
                        conn2.Open();
                        cmd2.ExecuteNonQuery();
                        conn2.Close();

                        if (DialogResult.Yes == MessageBox.Show("Change the State of Your Sold Products(inventory items) for this Customer from sold to empty?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            SqlCeConnection conn3 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                            SqlCeCommand cmd3 = new SqlCeCommand("UPDATE prod_bulk SET [Logistical Batch] = '' WHERE [Logistical Batch] = '" + cn.Text + "' AND [Reference Number] = '" + dgv_prods_[1, dgv_prods_.CurrentRow.Index].Value.ToString() + "' AND [Notes/Information] = '" + dgv_prods_[4, dgv_prods_.CurrentRow.Index].Value.ToString() + "'", conn3);
                            conn3.Open();
                            cmd3.ExecuteNonQuery();
                            conn3.Close();
                        }

                        SqlCeConnection conn4 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd4 = new SqlCeCommand("DELETE FROM Logs_prod WHERE [Product Serial Number] = '" + dgv_prods_[0, dgv_prods_.CurrentRow.Index].Value.ToString() + "'", conn4);
                        conn4.Open();
                        cmd4.ExecuteNonQuery();
                        conn4.Close();

                        dgv_prods_.Rows.Remove(dgv_prods_.CurrentRow);
                        logs_prod_dtst.GetChanges();
                        logs_prod_dtst.AcceptChanges();
                        logs_prodTableAdapter.Update(logs_prod_dtst);
                    }
                    else
                    {
                        basql.Execute(Main.Amatrix.mgt, "UPDATE prod_bulk SET [State] = '' WHERE [Logistical Batch] = '" + cn.Text + "' AND [State] = 'Shipping (Logistics)' AND [Reference Number] = '" + dgv_prods_[1, dgv_prods_.CurrentRow.Index].Value.ToString() + "' AND [Notes/Information] = '" + dgv_prods_[4, dgv_prods_.CurrentRow.Index].Value.ToString() + "'", "prod_bulk", dtpp);
                        dtpp.Clear();
                        if (DialogResult.Yes == MessageBox.Show("Change the State of Your Sold Products(inventory items) for this Customer from sold to empty?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            basql.Execute(Main.Amatrix.mgt, "UPDATE prod_bulk SET [Logistical Batch] = '' WHERE [Logistical Batch] = '" + cn.Text + "' AND [Reference Number] = '" + dgv_prods_[1, dgv_prods_.CurrentRow.Index].Value.ToString() + "' AND [Notes/Information] = '" + dgv_prods_[4, dgv_prods_.CurrentRow.Index].Value.ToString() + "'", "prod_bulk", dtpp);
                            dtpp.Clear();
                        }
                        basql.Execute(Main.Amatrix.mgt, "DELETE FROM Logs_prod WHERE [Product Serial Number] = '" + dgv_prods_[0, dgv_prods_.CurrentRow.Index].Value.ToString() + "'", "Logs_prod", dtpp);
                        dgv_prods_.Rows.Remove(dgv_prods_.CurrentRow);
                        asql.Save(logs_prod_dtst.Logs_prod, "prod_bulk", Main.Amatrix.mgt);
                    }
                    dtpp.Clear();
                    dtpp.Dispose();
                } Main.Amatrix.ascl.broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><typ>w</typ><val>0</val><app>" + this.Name + "</app><par>[" + toolStrip1.Name + "]</par><con>toolStripButton2</con>");
                
            //}
            //catch (Exception erty) { MessageBox.Show(erty.Message); Am_err ner = new Am_err(); ner.tx("An Error Occured, Operation Aborted."); }
            poop2 = "";
        }

        string poop2, poop3;
        private void toolStripButton2_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                poop2 = dgv_prods_[1, dgv_prods_.CurrentRow.Index].Value.ToString();
            }
            catch (Exception erty) { }
        }

        private void dgv_prods__CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Altering Cell Values may Have Adverse Effects on the Row's Entities, Would you Like to Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
            }
            else
            {
                MessageBox.Show("Operation Aborted", "Informator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgv_prods_.Rows)
            {
                try
                {
                    string s = dgvr.Cells[1].Value.ToString();
                    string b = "No";
                    if (dgvr.Cells[1].Value != DBNull.Value)
                    {
                        if (Main.Amatrix.mgt == "")
                        {
                            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString); DataGridViewCheckBoxCell l = new DataGridViewCheckBoxCell();
                            SqlCeCommand cmd = new SqlCeCommand("UPDATE prod_bulk SET [Delivered (Logistical Managment)] = @p1 WHERE [Reference Number] = '" + dgvr.Cells[1].Value.ToString() + "'", conn);
                            if (checkBox2.CheckState == CheckState.Checked)
                            {
                                b = "Yes";
                            }
                            else { b = "No"; }
                            cmd.Parameters.AddWithValue("@p1", b.ToString());
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {
                            SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                            SqlCommand cmd = new SqlCommand("UPDATE prod_bulk SET [Delivered (Logistical Managment)] = @p1 WHERE [Reference Number] = '" + dgvr.Cells[1].Value.ToString() + "'", conn);
                            if (checkBox2.CheckState == CheckState.Checked)
                            {
                                b = "Yes";
                            }
                            else { b = "No"; }
                            cmd.Parameters.AddWithValue("@p1", b);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                catch (Exception erty) { }
            }
            if (checkBox2.Checked == true)
            {
                try
                {
                    mgmt_Linkto_acc acc = new mgmt_Linkto_acc();
                    acc.tx("Logistical Transaction Payment For[" + cn.Text + "]", "", cn.Text, "logistical managment", Convert.ToDouble(textBox3.Text), 0);
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("There Where Empty Fields Detected. You Cannot Create a Journal Entry For this Batch. (Check if the Costs field is Empty)"); checkBox2.Checked = false; }
                oper_save();
            }
            if (checkBox2.Checked == false && textBox3.Text != "")
            {
                if (DialogResult.Yes == MessageBox.Show("Make the Associated Journal Value Defunct? (if any)(also SETS CREDIT VALUE TO DEBIT)", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string sql = "UPDATE journal SET [Particulars] = 'Logistical Transaction Payment For[" + cn.Text + "](Defunct)', [Credit] = '0', [Debit] = '" + textBox3.Text + "' WHERE [Particulars] = 'Logistical Transaction Payment For[" + cn.Text + "]'";
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
                        basql.Execute(Main.Amatrix.acc, sql, "journal", dtpy);
                        dtpy.Clear(); dtpy.Dispose();
                    }
                }
            }
        }

        private void dgv_prods__DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        int whereami = 0; 
        Thread Sync_th;
        delegate void Sync_del();
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            this.Enabled = false;
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.Text = this.Text + " [Synchronizing]";
            whereami = Convert.ToInt32(bindingNavigatorPositionItem.Text);
            try
            {
                bkk_sync.RunWorkerAsync();
            }
            catch (Exception erty) { toolStripButton2.BackColor = Color.DarkOrange; this.Enabled = true; this.Text = this.Text.Replace(" [Synchronizing]", ""); }
        }

        private void bkk_sync_DoWork(object sender, DoWorkEventArgs e)
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
            logstcs_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                logstcs_dtst.Logs_mgmt.Load(dr);
                conn.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand(Last_Query_Used, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                logstcs_dtst.Logs_mgmt.Load(dr);
                conn.Close();
            }
            load_p2p();
            load_prods();
            logsmgmtBindingSource.Position = whereami - 1;
            this.Text = this.Text.Replace(" [Synchronizing]", "");
            this.Enabled = true;
            toolStripButton2.BackColor = Color.Transparent;
        }

        //newer
        string time = DateTime.Now.ToString();
        private void log_read_Tick(object sender, EventArgs e)
        {
            try
            {
                bkk_logread.RunWorkerAsync();
            }
            catch (Exception erty) { }
        }

        Thread th_log;
        delegate void del_log();
        private void bkk_logread_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                th_log = new Thread(new ThreadStart(del_log_strt));
                th_log.IsBackground = true;
                th_log.Start();
            }
            catch (Exception erty) { }
        }

        private void del_log_strt()
        {
            try
            {
                this.Invoke(new del_log(read_log));
            }
            catch (Exception erty) { }
        }

        private void read_log()
        {
            try
            {
                DataTable dtpy = new DataTable();
                dtpy = basql.Execute(Main.Amatrix.mgt.Replace("Amdtbse", "Isync_DB"), "SELECT * FROM Isync WHERE [DateTime] > [" + time + "]", "", dtpy);

                if (dtpy.Rows.Count > 0)
                {
                    time = dtpy.Rows[dtpy.Rows.Count - 1].ToString();
                    toolStripButton2.BackColor = Color.DarkOrange;
                }

                dtpy.Clear();
                dtpy.Dispose();
            }
            catch (Exception erty) { MessageBox.Show(erty.Message); }
        }

        //new
        //Control c; string LOG = "";
        /*private void serv_keyup(object sender, KeyEventArgs e)
        {
            //c = (Control)sender;
            /*string s = "abcdefghijklmnopqrst";

            Control parents = c; string par = ""; ArrayList al_par = new ArrayList();
            foreach (char cs in s)
            {
                try
                {
                    parents = parents.Parent;
                    if (parents.Name != c.Name && parents.Name != this.Name)
                    {
                        al_par.Add("[" + parents.Name + "]");
                    }
                }
                catch (Exception erty) { break; }
            }
            for (int i = al_par.Count - 1; i >= 0; i--)
            {
                par = par + al_par[i].ToString();
            }*/
            //Main.Amatrix.ascl.broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><val>" + c.Text + "</val><app>" + this.Name + "</app><con>" + c.Name + "</con><typ>w</typ><par>" + par + "</par><ndx>" + bindingNavigatorPositionItem.Text + "</ndx>");
        //}*/

        bool ischanged = false;
        void Logs_mgmt_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            ischanged = true;
        }

        //new
        private void dgv2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void bkk_sync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }
    }
}
