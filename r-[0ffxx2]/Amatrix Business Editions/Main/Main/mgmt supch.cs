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
using Microsoft.PointOfService;
using System.Collections;
using System.IO;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Threading;
using System.Windows.Forms;

namespace Main
{
    public partial class mgmt_supch : Form
    {
        private PosExplorer explorer;
        private ArrayList scannerList;
        private Scanner activeScanner;
        private Thread th_init;
        private delegate void del_init();

        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        SQLCEBasql.SQLCEBSQ scsql = new SQLCEBasql.SQLCEBSQ();

        public mgmt_supch()
        {
            this.Icon = Properties.Resources.amdsicnico;
            this.Disposed += new EventHandler(mgmt_supch_Disposed);
            InitializeComponent();
            Init();
            if (Main.Amatrix.mgt != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.mgt); pwd.Owner = this; }
                catch (Exception erty) { }
            }
        }

        void mgmt_supch_Disposed(object sender, EventArgs e)
        {
            dgv2.DataSource = null;
            prodmgmt_dtst.Clear();
            prod_mgmtTableAdapter.Connection.Close();
            dataGridView1.DataSource = null;

            prodmgmt_dtst.Dispose();
            prodmgmtBindingSource.Dispose();
            prod_mgmtTableAdapter.Dispose();

            prod_stock_dtst.Clear();
            prod_stockingTableAdapter.Connection.Close();

            prod_stock_dtst.Dispose();
            prodstockingBindingSource.Dispose();
            prod_stockingTableAdapter.Dispose();

            prod_bulk_dtst.Clear();
            prod_bulkTableAdapter.Connection.Close();

            prod_bulk_dtst.Dispose();
            prodbulkBindingSource.Dispose();
            prod_bulkTableAdapter.Dispose();

            textBox7.Enter -= textBox7_Enter;
            textBox7.KeyUp -= textBox7_KeyUp;
            textBox7.Leave -= textBox7_Leave;
            button15.Click -= button15_Click;
            comboBox2.DropDown -= comboBox2_DropDown;
            //comboBox3.DropDown -= comboBox3_DropDown;
            bkk_prod_tp.DoWork -= bkk_prod_tp_DoWork;
            bkk_prod_tp.RunWorkerCompleted -= bkk_prod_tp_RunWorkerCompleted;
            backgroundWorker1.DoWork -= backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted -= backgroundWorker1_RunWorkerCompleted;
            button14.Click -= button14_Click;
            button11.Click -= button11_Click;
            button13.Click -= button13_Click;
            comboBox3.DropDown -= comboBox3_DropDown;
            button8.Click -= button8_Click;
            button5.Click -= button5_Click_2;
            domainUpDown1.Leave -= changed_levels;
            domainUpDown2.Leave -= changed_levels;
            Sync_bttn.Click -= Sync_bttn_Click;
            bkk_sync.DoWork -= bkk_sync_DoWork;
            textBox16.Enter -= textBox16_Enter;
            textBox16.Leave -= textBox16_Leave;
            bkk_bc_main.DoWork -= bkk_bc_main_DoWork;
            bkk_bc_main.RunWorkerCompleted -= bkk_bc_main_RunWorkerCompleted;
            bkk_color.DoWork -= bkk_color_DoWork;
            bkk_bc.DoWork -= bkk_bc_DoWork;
            bkk_bc.RunWorkerCompleted -= bkk_bc_RunWorkerCompleted;
            dataGridView1.CellBeginEdit -= dataGridView1_CellBeginEdit;
            dataGridView1.CellEndEdit -= dataGridView1_CellEndEdit;
            warrantiesToolStripMenuItem.Click -= warrantiesToolStripMenuItem_Click;
            resentItemsToolStripMenuItem.Click -= resentItemsToolStripMenuItem_Click;
            button4.Click -= button4_Click_2;
            selectedWithLogisticalManagmentToolStripMenuItem.Click -= selectedWithLogisticalManagmentToolStripMenuItem_Click;
            dataGridView1.DataError -= dataGridView1_DataError;
            button10.Click -= button10_Click;
            button7.Click -= button7_Click_1;
            button2.Click -= button5_Click_1;
            button1.Click -= button4_Click_1;
            pbx_lgu.BackgroundImageChanged -= pbx_lgu_BackgroundImageChanged;
            this.Disposed -= mgmt_supch_Disposed;
            //thisMonthToolStripMenuItem.Click -= thisMonthToolStripMenuItem_Click;
            //activeScanner.DataEvent -= activeScanner_DataEvent;
            //activeScanner.ErrorEvent -= activeScanner_ErrorEvent;
            toolStripButton3.Click -= toolStripButton3_Click_1;
            toolStripMenuItem5.Click -= toolStripMenuItem5_Click;
            okViewLossesToolStripMenuItem.Click -= okViewLossesToolStripMenuItem_Click;
            pbx_lgu.MouseEnter -= pbx_lgu_MouseEnter;
            pbx_lgu.MouseLeave -= pbx_lgu_MouseLeave;
            toolStripButton10.Click -= toolStripButton10_Click;
            remv_pic.Click -= remv_pic_Click;
            ts2.MouseEnter -= ts2_MouseEnter;
            ts2.MouseLeave -= ts2_MouseLeave;
            bindingNavigatorPositionItem.TextChanged -= bindingNavigatorPositionItem_TextChanged;
            loosing.Click -= loosing_Click;
            forThisProductToolStripMenuItem.Click -= forThisProductToolStripMenuItem_Click;
            forAllProductsToolStripMenuItem.Click -= forAllProductsToolStripMenuItem_Click;
            forThisProductInTheCurrentMonthToolStripMenuItem.Click -= forThisProductInTheCurrentMonthToolStripMenuItem_Click;
            forAllProductsInTheCurrentMonthToolStripMenuItem.Click -= forAllProductsInTheCurrentMonthToolStripMenuItem_Click;
            printProductInformationToolStripMenuItem.Click -= pnt_Click;
            printInventoryBulkInformationToolStripMenuItem.Click -= pnt_Click;
            this.ts2.MouseEnter -= this.ts2_MouseEnter;
            this.ts2.MouseLeave -= this.ts2_MouseLeave;
            this.tbxfned.Leave -= this.tbxfned_Leave;
            this.tbxfned.Enter -= this.tbxfned_Enter;
            this.tbxfned.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tbxfned.MouseLeave -= this.tvtxt1_MouseLeave;
            this.gotoitm.Click -= this.gotoitm_Click;
            this.edt.Click -= this.pnt_Click;
            this.pnt.Click -= this.pnt_Click;
            this.toolStripButton3.Click -= this.toolStripButton3_Click;
            this.svebtn.Click -= this.svebtn_ButtonClick;
            this.clse.MouseLeave -= this.clse_MouseLeave;
            this.clse.ButtonClick -= this.clse_ButtonClick;
            this.clse.MouseEnter -= this.clse_MouseEnter;
            this.rstrt.Click -= this.restr_Click;
            this.connlbl.MouseEnter -= this.connlbl_MouseEnter;
            this.connlbl.MouseLeave -= this.connlbl_MouseLeave;
            this.connlbl.Click -= this.connlbl_Click;
            this.sa.Click -= this.sa_Click;
            this.bindingNavigatorPositionItem.MouseEnter -= this.tvtxt1_MouseEnter;
            this.bindingNavigatorPositionItem.MouseLeave -= this.tvtxt1_MouseLeave;
            this.bindingNavigatorPositionItem.TextChanged -= this.bindingNavigatorPositionItem_TextChanged;
            this.go_emp.Click -= this.go_emp_ButtonClick;
            this.toolStripTextBox2.Leave -= this.tbxfned_Leave;
            this.toolStripTextBox2.Enter -= this.tbxfned_Enter;
            this.toolStripTextBox2.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox2.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tabControl6.SelectedIndexChanged -= this.tabControl6_SelectedIndexChanged;
            this.comboBox1.DropDown -= this.Gen;
            this.checkBox1.CheckedChanged -= this.Gen;
            this.textBox9.TextChanged -= this.Gen;
            this.textBox9.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox9.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox8.TextChanged -= this.Gen;
            this.textBox8.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox8.MouseEnter -= this.tvtxt1_MouseEnter;
            this.comboBox2.MouseEnter -= this.tvtxt1_MouseEnter;
            this.comboBox2.MouseLeave -= this.tvtxt1_MouseLeave;
            this.comboBox2.DropDown -= this.Gen;
            this.comboBox2.TextChanged -= this.Gen;
            this.textBox6.TextChanged -= this.Gen;
            this.textBox6.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox6.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox5.TextChanged -= this.Gen;
            this.textBox5.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox5.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox4.TextChanged -= this.Gen;
            this.textBox4.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox4.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox3.TextChanged -= this.Gen;
            this.textBox3.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox3.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox2.TextChanged -= this.Gen;
            this.textBox2.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox2.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox1.TextChanged -= this.Gen;
            this.textBox1.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox1.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripButton10.Click -= this.toolStripButton10_Click;
            this.textBox7.TextChanged -= this.Gen;
            this.textBox7.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox7.MouseEnter -= this.tvtxt1_MouseEnter;
            this.cn.TextChanged -= this.cn_TextChanged;
            this.cn.MouseLeave -= this.tvtxt1_MouseLeave;
            this.cn.MouseEnter -= this.tvtxt1_MouseEnter;
            this.button9.Click -= this.button9_Click;
            this.btn_shw.Click -= this.btn_shw_Click;
            this.button3.Click -= this.button3_Click;
            this.textBox17.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox17.MouseEnter -= this.tvtxt1_MouseEnter;
            /*this.button18.Click -= this.button18_Click;
            this.button19.Click -= this.button19_Click;
            this.dataGridView2.CellMouseClick -= this.dataGridView2_CellMouseClick;
            this.button8.Click -= this.button8_Click;*/
            this.button12.Click -= this.button12_Click;
            this.btop.Click -= this.button12_Click;
            this.remv_pic.Click -= this.remv_pic_Click;
            this.tbx_lgu.TextChanged -= this.tbx_lgu_TextChanged;
            this.tbx_lgu.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tbx_lgu.MouseEnter -= this.tvtxt1_MouseEnter;
            this.pbx_lgu.MouseLeave -= this.pbx_lgu_MouseLeave;
            this.pbx_lgu.Click -= this.bt_cv_Click;
            this.pbx_lgu.MouseEnter -= this.pbx_lgu_MouseEnter;
            this.bt_cv.Click -= this.bt_cv_Click;
            //this.button4.Click -= this.button4_Click;
            //this.button7.Click -= this.button7_Click;
            this.textBox16.TextChanged -= this.textBox16_TextChanged;
            /*this.tabControl3.SelectedIndexChanged -= this.button8_Click;
            this.toolStripTextBox4.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox4.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox13.TextChanged -= this.textBox11_TextChanged;
            this.textBox13.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox13.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox12.TextChanged -= this.textBox11_TextChanged;
            this.textBox12.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox12.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox11.TextChanged -= this.textBox11_TextChanged;
            this.textBox11.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox11.MouseEnter -= this.tvtxt1_MouseEnter;
            this.dateTimePicker2.ValueChanged -= this.Gen;
            this.dateTimePicker2.DropDown -= this.Gen;
            this.textBox10.TextChanged -= this.textBox11_TextChanged;
            this.textBox10.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox10.MouseEnter -= this.tvtxt1_MouseEnter;
            this.dateTimePicker1.ValueChanged -= this.dtv_txt;
            this.dateTimePicker1.DropDown -= this.Gen;
            this.button2.Click -= this.button2_Click;
            this.button1.Click -= this.button1_Click;
            this.textBox15.TextChanged -= this.textBox11_TextChanged;
            this.textBox15.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox15.Enter -= this.textBox15_Enter;
            this.textBox15.MouseEnter -= this.tvtxt1_MouseEnter;*/
            this.ofd.FileOk -= this.ofd_FileOk;
            this.save_inv.Click -= this.svebtn_ButtonClick;
            this.restr.Click -= this.restr_Click;
            this.clsemn.Click -= this.clse_ButtonClick;
            this.cpy.Click -= this.cpy_Click;
            this.ct.Click -= this.ct_Click;
            this.pster.Click -= this.pster_Click;
            this.deletecell.Click -= this.deletecell_Click;
            this.initializeToolStripMenuItem.Click -= this.initializeToolStripMenuItem_Click;
            this.switchDatabaseToolStripMenuItem.Click -= this.switchDatabaseToolStripMenuItem_Click;
            this.rePartitionDataBaseToolStripMenuItem.Click -= this.rePartitionDataBaseToolStripMenuItem_Click;
            this.contentsToolStripMenuItem.Click -= this.helpToolStripMenuItem1_Click;
            this.abtmnu.Click -= this.abtmnu_Click;
            this.connectToToolStripMenuItem.Click -= this.connectToToolStripMenuItem_Click;
            this.helpToolStripMenuItem1.Click -= this.helpToolStripMenuItem1_Click;
            this.tmeclse.Tick -= this.tmeclse_Tick;
            this.decpr.Tick -= this.decpr_Tick;
            this.ttp_del.Tick -= this.ttp_del_Tick;
            this.ofd2.FileOk -= this.ofd2_FileOk;
            this.Deactivate -= this.mgmt_supch_Deactivate;
            this.Load -= this.mgmt_supch_Load;
            this.Activated -= this.mgmt_supch_Activated;
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
            this.Opacity = Properties.Settings.Default.opacity;
            /*if (choicesett.Default.tpmst == true)
            {
                this.TopMost = true;
            }
            else if (choicesett.Default.tpmst == false)
            {
                this.TopMost = false;
            }*/
            th_init_strt();
        }

        private void th_init_strt()
        {
            try
            {
                th_init = new Thread(new ThreadStart(del_init_strt));
                th_init.IsBackground = true;
                th_init.Start();
            }
            catch (Exception erty) { init_db(); }
        }

        private void del_init_strt()
        {
            try
            {
                this.Invoke(new del_init(init_db));
            }
            catch (Exception ertyy) { init_db(); }
        }

        string Last_Query_Used;
        private void init_db()
        {
            try
            {
                Last_Query_Used = "Select * From Prod_mgmt";
                try
                {
                    prodmgmt_dtst.Clear();
                }
                catch (Exception ertyg) { }
                if (Main.Amatrix.mgt == "")
                {
                    string ConnString = prod_mgmtTableAdapter.Connection.ConnectionString;
                    string SqlString = "Select * From Prod_mgmt";
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                prodmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "Prod_mgmt");
                            }
                            conn.Close();
                        }
                    }
                }
                else
                {
                    dr_univ = Quer("SELECT * FROM Prod_mgmt");
                    prodmgmt_dtst.Load(dr_univ, LoadOption.PreserveChanges, "Prod_mgmt");
                    if (Main.Amatrix.mgt != "")
                    {
                        if (dgv2[15, 0].Value != DBNull.Value)
                        {
                            byte[] res1 = (byte[])dgv2[15, 0].Value;
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
                            pbx_lgu.BackgroundImage = Properties.Resources.product;
                        }
                    }
                }
            }
            catch (Exception erty) { }
            conn2();
        }
        //access by thread

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

        private Thread conn2_th;
        private delegate void conn2_del();

        private void conn2_th_strt()
        {
            try
            {
                conn2_th = new Thread(new ThreadStart(conn2_del_strt));
                conn2_th.IsBackground = true;
                conn2_th.Start();
            }
            catch (Exception erty) { conn2(); }
        }

        private void conn2_del_strt()
        {
            try
            {
                this.Invoke(new conn2_del(conn2));
            }
            catch (Exception erty) { conn2(); }
        }

        private void conn2()
        {
            bool f = false;
            if (Main.Amatrix.mgt == "")
            {
                try
                {
                    prod_mgmtTableAdapter.Connection.Open();
                    f = true;
                    prod_mgmtTableAdapter.Connection.Close();
                }
                catch (Exception errtt) { f = false; }

                if (f == true)
                {
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Product Table is Connected";
                }
                else if (f == false)
                {
                    connlbl.Text = "Product Table is Not Connected";
                    connlbl.Image = Properties.Resources.connctno;
                }
                db_info.Text = prod_mgmtTableAdapter.Connection.Database; srv_inf.Text = prod_mgmtTableAdapter.Connection.ServerVersion;
                nds_.Text = prod_mgmtTableAdapter.Connection.DataSource;
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    conn.Open();
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Product Table is Connected";
                }
                catch (Exception erty)
                {
                    connlbl.Text = "Product Table is Not Connected";
                    connlbl.Image = Properties.Resources.connctno;
                }
            }
        }

        private void mgmt_supch_Load(object sender, EventArgs e)
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
                if (DialogResult.Yes == MessageBox.Show("You Have Loaded a BarCode, Would You Like to Insert it in the General Bar-Code TextBox For Your Product? To Do So Click on Yes. If Instead You Would like to Insert it into the Search TextBox Click on No.", "Amatrix Bar-Coder", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    textBox16.Text = encoder.GetString(activeScanner.ScanDataLabel);
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

        public void tx(string Name)
        {
            prodmgmt_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(prod_mgmtTableAdapter.Connection.ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt WHERE [Product Name] LIKE '%" + Name + "%'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prodmgmt_dtst.Load(dr, LoadOption.PreserveChanges, "Prod_mgmt");
                conn.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prod_mgmt WHERE [Product Name] LIKE '%" + Name + "%'", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                prodmgmt_dtst.Load(dr, LoadOption.PreserveChanges, "Prod_mgmt");
                conn.Close();
            }
            this.Show();
        }

        private void cn_TextChanged(object sender, EventArgs e)
        {
            if (bindingNavigatorPositionItem.Text == "0")
            {
                general_mssg("No Values are Within the Product List Table Press the + Button to Add One The Button may be Found in the Tool-bar Above.", "No Entries Within Said Table");
            }
            string SqlString2 = "Select * From prod_stocking WHERE [product] = '" + cn.Text + "'";
            if (Main.Amatrix.mgt == "")
            {
                prodstockingBindingSource.EndEdit();
                prod_stockingTableAdapter.Update(prod_stock_dtst);
                prod_stock_dtst.Clear();
                string ConnString2 = prod_stockingTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString2))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString2, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prod_stock_dtst.Load(reader, LoadOption.PreserveChanges, "prod_stocking");
                        }
                    }
                }
            }
            else
            {
                prodstockingBindingSource.EndEdit();
                asql.Save(prod_stock_dtst.prod_stocking, "prod_stocking", Main.Amatrix.mgt);
                prod_stock_dtst.Clear();
                dr_univ = Quer(SqlString2);
                prod_stock_dtst.Load(dr_univ, LoadOption.PreserveChanges, "prod_stocking");
            }
            load_bulk();
        }

        private void svebtn_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (Main.Amatrix.mgt == "")
                {
                    prodstockingBindingSource.EndEdit();
                    prodmgmtBindingSource.EndEdit();
                    prod_stockingTableAdapter.Update(prod_stock_dtst);
                    prod_mgmtTableAdapter.Update(prodmgmt_dtst);
                }
                else
                {
                    prodstockingBindingSource.EndEdit();
                    asql.Save(prod_stock_dtst.prod_stocking, "prod_stocking", Main.Amatrix.mgt);
                    prodmgmtBindingSource.EndEdit();
                    asql.Save(prodmgmt_dtst.Prod_mgmt, "Prod_mgmt", Main.Amatrix.mgt);
                    try
                    {
                        Main.Amatrix.ascl.broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><typ>w</typ><val>0</val><app>" + this.Name + "</app><par>[" + toolStrip1.Name + "]</par><con>Sync_bttn</con>");
                    }
                    catch (Exception erty) { general_mssg("Syncronization is not Set Up", "Sync Error"); }
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A Fatal Error Occured While Saving Your Information. The Operation Was Halted and Your Data Was not Saved."); }
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
            }
            catch (Exception erty) { pbx_lgu.BackgroundImage = Properties.Resources.product; } 
            
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
                    dgv2[15, dgv2.CurrentRow.Index].Value = b;
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(button12) == true)
                {
                    System.Diagnostics.Process.Start(tbx_lgu.Text);
                }
                else
                {
                    FileInfo fnf = new FileInfo(tbx_lgu.Text);
                    System.Diagnostics.Process.Start(fnf.DirectoryName);
                }
            }
            catch (Exception erty) { general_mssg("The Specified File does not Exist", ""); }
        }

        private void tbx_lgu_TextChanged(object sender, EventArgs e)
        {
            if (bindingNavigatorPositionItem.Text == "0")
            {
                general_mssg("No Values are Within the Stock Information Table Press the + Button to Add One The Button may be Found in the Tool-bar Above.", "No Entries Within Said Table");
            }
            try
            {
                pbx_lgu.BackgroundImage = Image.FromFile(tbx_lgu.Text);
            }
            catch (Exception eryt) { pbx_lgu.BackgroundImage = Properties.Resources.product; }
        }

        private void initializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Amatrix Product Managment Does Not Support Data-base Re-initializaion. Restart the application to load the new settings?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                mgmt_supch sh = new mgmt_supch();
                sh.Show();
                this.Close();
            }
            else
            {

            }
            //th_init_strt();
        }

        private void go_emp_ButtonClick(object sender, EventArgs e)
        {
            string SqlString = "";
            SqlString = "Select * FROM Prod_mgmt WHERE [Product Name] LIKE '%" + toolStripTextBox2.Text + "%'";
            Last_Query_Used = SqlString;
            if (Main.Amatrix.mgt == "")
            {
                prodmgmt_dtst.Clear();
                string ConnString = prod_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();
                        using (reader)
                        {
                            prodmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "Prod_mgmt");
                        }
                    }
                }
            }
            else
            {
                prodmgmt_dtst.Clear();
                dr_univ = Quer(SqlString);
                prodmgmt_dtst.Load(dr_univ, LoadOption.PreserveChanges, "Prod_mgmt");
            }
        }

        private void gotoitm_Click(object sender, EventArgs e)
        {
            try
            {
                int start = 0;
                string SqlString = "Select * From Prod_mgmt Where";
                foreach (DataColumn dgvc in prodmgmt_dtst.Prod_mgmt.Columns)
                {
                    try
                    {
                        if (dgvc.ColumnName != "ImagePath2" && dgvc.ColumnName != "B code2")
                        {
                            if (start == 0)
                            {
                                if (tbxfned.Text.Contains(' ') == true)
                                {
                                    SqlString = SqlString + " [" + dgvc.ColumnName + "] + ' ' + [" + prodmgmt_dtst.Prod_mgmt.Columns[1].ColumnName + "] LIKE '%" + tbxfned.Text + "%' ";
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
                    prodmgmt_dtst.Clear();
                    string ConnString = prod_mgmtTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                prodmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "Prod_mgmt");
                            }
                        }
                    }
                }
                else
                {
                    prodmgmt_dtst.Clear();
                    dr_univ = Quer(SqlString);
                    prodmgmt_dtst.Load(dr_univ, LoadOption.PreserveChanges, "Prod_mgmt");
                } 
                
                if (Main.Amatrix.mgt != "")
                {
                    if (dgv2[15, dgv2.CurrentRow.Index].Value != DBNull.Value)
                    {
                        byte[] res1 = (byte[])dgv2[15, dgv2.CurrentRow.Index].Value;
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
                        pbx_lgu.BackgroundImage = Properties.Resources.product;
                    }
                    //2
                    /*if (dgv2[16, dgv2.CurrentRow.Index].Value != DBNull.Value)
                    {
                        byte[] res1 = (byte[])dgv2[16, dgv2.CurrentRow.Index].Value;
                        Image newImage;
                        using (MemoryStream ms = new MemoryStream(res1, 0, res1.Length))
                        {
                            newImage = Bitmap.FromStream(ms, true);
                            ms.Flush();
                            ms.Close();
                            ms.Dispose();
                        }
                        pictureBox2.BackgroundImage = newImage;
                    }
                    else
                    {
                        pictureBox2.BackgroundImage = null;
                    }*/
                }
            }
            catch (Exception erty) {  }   
        }

        private void connlbl_Click(object sender, EventArgs e)
        {
            Point ptt = new Point();
            ptt.X = Cursor.Position.X - this.Location.X + 50;
            ptt.Y = Cursor.Position.Y - this.Location.Y;
            cmslv.Show(ptt);
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
            try
            {
                this.ActiveControl.Text = "";
            }
            catch (Exception erty) { }
        }

        private void connlbl_MouseEnter(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = Properties.Resources.bannrrageconv;
        }

        private void connlbl_MouseLeave(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = null;
        }

        private void sa_Click(object sender, EventArgs e)
        {
            th_init_strt();
        }

        private void clse_MouseEnter(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void clse_MouseLeave(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.Image;
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

        private void abtmnu_Click(object sender, EventArgs e)
        {
            app_abt bt = new app_abt();
            bt.descr(this.Text);
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

        private void restr_Click(object sender, EventArgs e)
        {
            mgmt_supch sp = new mgmt_supch();
            sp.Show();
            this.Close();
        }

        private void mgmt_supch_Activated(object sender, EventArgs e)
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

        private void mgmt_supch_Deactivate(object sender, EventArgs e)
        {
            decpr.Start();
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

        private void Gen(object sender, EventArgs e)
        {
            textBox7.BackColor = Color.GhostWhite;
            if (bindingNavigatorPositionItem.Text == "0")
            {
                general_mssg("No Values are Within the Product List Table Press the + Button to Add One The Button may be Found in the Tool-bar Above.", "No Entries Within Said Table");
            }
            else
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                //get Unit Value
                string ID = "";
                try
                {
                    DataTable dtp_u = new DataTable();
                    dtp_u = scsql.Execute(Properties.Settings.Default.Amdtbse_6ConnectionString, "SELECT * FROM Product_Units WHERE [Product ID] = '" + textBox7.Text + "'");
                    ID = dtp_u.Rows[0].ItemArray[3].ToString();
                }
                catch (Exception erty) { }

                int n = 0;
                SqlCeConnection mySqlConnection = new SqlCeConnection(prod_bulkTableAdapter.Connection.ConnectionString);
                SqlCeCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = "SELECT max([Serial Number]) FROM prod_bulk";
                mySqlConnection.Open();
                try
                {
                    n = Convert.ToInt32(mySqlCommand.ExecuteScalar());
                }
                catch (Exception erty) { n = 0; }
                mySqlConnection.Close();
                DataRow dr = prod_bulk_dtst.prod_bulk.Newprod_bulkRow();
                dr[0] = n + 1;
                dr[1] = textBox17.Text;
                dr[2] = textBox7.Text;
                dr[7] = dateTimePicker3.Value;
                try
                {
                    dr[10] = ID;
                }
                catch (Exception erty) { }
                prod_bulk_dtst.prod_bulk.Rows.Add(dr);
                dataGridView1.DataSource = prod_bulk_dtst.prod_bulk;
                prod_bulkTableAdapter.Update(prod_bulk_dtst);
            }
            else
            {
                try
                {
                    string ID = "";
                    try
                    {
                        DataTable dtp_u = new DataTable();
                        dtp_u = scsql.Execute(Amatrix.mgt, "SELECT * FROM Product_Units WHERE [Product ID] = '" + textBox7.Text + "'");
                        ID = dtp_u.Rows[0].ItemArray[3].ToString();
                    }
                    catch (Exception erty) { }

                    int n = 0;
                    SqlConnection mySqlConnection = new SqlConnection(Main.Amatrix.mgt);
                    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                    mySqlCommand.CommandText = "SELECT max([Serial Number]) FROM prod_bulk";
                    mySqlConnection.Open();
                    try
                    {
                        n = Convert.ToInt32(mySqlCommand.ExecuteScalar());
                    }
                    catch (Exception erty) { n = 0; }
                    mySqlConnection.Close();
                    DataRow dr = prod_bulk_dtst.prod_bulk.Newprod_bulkRow();
                    dr[0] = n + 1;
                    dr[1] = textBox17.Text;
                    dr[2] = textBox7.Text;
                    dr[7] = dateTimePicker3.Value;
                    try
                    {
                        dr[10] = ID;
                    }
                    catch (Exception erty) { }
                    prod_bulk_dtst.prod_bulk.Rows.Add(dr);
                    dataGridView1.DataSource = prod_bulk_dtst.prod_bulk;
                    asql.Save(prod_bulk_dtst.prod_bulk, "prod_bulk", Main.Amatrix.mgt);
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }

            mgmt_Linkto_acc mla = new mgmt_Linkto_acc();
            mla.tx(textBox7.Text, textBox17.Text, cn.Text, "product managment", 0, 0);                                                     
        }

        private void load_bulk()
        {
            prod_bulk_dtst.Clear();
            string SqlString = "Select * From prod_bulk WHERE [Notes/Information] = '" + textBox7.Text + "'";
            if (Main.Amatrix.mgt == "")
            {
                string ConnString = prod_bulkTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prod_bulk_dtst.Load(reader, LoadOption.PreserveChanges, "prod_bulk");
                        }
                        conn.Close();
                    }
                }
            }
            else
            {
                string ConnString = Main.Amatrix.mgt;
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prod_bulk_dtst.Load(reader, LoadOption.PreserveChanges, "prod_bulk");
                        }
                        conn.Close();
                    }
                }
                //dr_univ = Quer(SqlString);
                //prod_bulk_dtst.Load(dr_univ, LoadOption.PreserveChanges, "prod_bulk");
                dataGridView1.DataSource = prod_bulk_dtst.prod_bulk;
            }
            try
            {
                bkk_color.RunWorkerAsync();
            }
            catch (Exception erty) { bkk_color.CancelAsync(); th_mark_strt(); }
        }

        private void connectToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy lg = new loggy();
            lg.Show();
        }
        //bulk
        private void tabControl6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl6.SelectedIndex == 2)
            {
                if (textBox7.Text != "")
                {
                    load_bulk();
                }
                else
                {
                    tabControl6.SelectTab(0);
                    textBox7.BackColor = Color.DarkOrange;
                    Am_err ner = new Am_err();
                    ner.tx("Please Enter a Product ID Number(Highlighted in Orange) before Continuing to this Step.");
                }
            }
            else if (tabControl6.SelectedIndex == 1)
            {
                //markbulk includes the Levels Information needed.
                try
                {
                    bkk_color.RunWorkerAsync();
                }
                catch (Exception erty) { th_mark_strt(); }
            }
        }

        private Thread th_mark;
        private delegate void del_mark();

        private void th_mark_strt()
        {
            try
            {
                th_mark = new Thread(new ThreadStart(del_mark_strt));
                th_mark.IsBackground = true;
                th_mark.Start();
            }
            catch (Exception erty) { general_mssg("Unable to Mark Bulk", "Thread"); }
        }

        private void del_mark_strt()
        {
            try
            {
                this.Invoke(new del_mark(mark_bulk));
            }
            catch (Exception erty) { general_mssg("Unable to Mark Bulk", "Delegate"); }
        }

        private void mark_bulk()
        {
            int sold = 0; int ship = 0;
            int resent = 0; int items = 0;
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {
                try
                {
                    if (dgvr.Cells[3].Value.ToString() == "Sold")
                    {
                        sold++;
                        dgvr.Cells[3].Style.BackColor = Color.LightGreen;
                    }
                    else if (dgvr.Cells[3].Value.ToString() == "Shipping (Logistics)")
                    {
                        ship++;
                        dgvr.Cells[3].Style.BackColor = Color.LightSteelBlue;
                    }
                    else if (dgvr.Cells[3].Value.ToString().Contains("Resent-Back From[") == true)
                    {
                        resent++;
                        dgvr.Cells[3].Style.BackColor = Color.Tomato;
                    }
                    items++;
                }
                catch (Exception erty) { }
            }

            label17.Text = "Stock in Posession : " + items.ToString();
            label16.Text = "Sold : " + sold.ToString();
            label19.Text = "Shipping : " + ship.ToString();
            label22.Text = "Re-Sent : " + resent.ToString();

            try
            {
                if (items <= Convert.ToInt32(domainUpDown1.Text) || items <= Convert.ToInt32(domainUpDown2.Text))
                {
                    label23.ForeColor = Color.DarkOrange;
                    label23.Text = "Status : Low Stock";
                    if (items <= Convert.ToInt32(domainUpDown2.Text))
                    {
                        label23.ForeColor = Color.Tomato;
                        label23.Text = "Status : Critically Low Stock";
                    }
                }
                else
                {
                    int total = items - (sold + ship + resent);
                    label17.Text = "Stock in Posession : " + total.ToString();
                    if (items <= Convert.ToInt32(domainUpDown1.Text) || items <= Convert.ToInt32(domainUpDown2.Text))
                    {
                        label23.ForeColor = Color.DarkOrange;
                        label23.Text = "Status : Low Stock";
                    }
                    else
                    {
                        label23.ForeColor = Color.Black;
                        label23.Text = "Status : Ok";
                    }
                }
            }
            catch (Exception erty) { }
        }

        //bulk -END-
        private void btn_shw_Click(object sender, EventArgs e)
        {
            prod_bulk_dtst.Clear();
            string SqlString = "Select * From prod_bulk";
            if (Main.Amatrix.mgt == "")
            {
                string ConnString = prod_bulkTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prod_bulk_dtst.Load(reader, LoadOption.PreserveChanges, "prod_bulk");
                        }
                        conn.Close();
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                prod_bulk_dtst.Load(dr_univ, LoadOption.PreserveChanges, "prod_bulk");
            }
            dataGridView1.DataSource = prod_bulk_dtst.prod_bulk;
            th_mark_strt();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    foreach (DataGridViewCell dgvc in dataGridView1.SelectedCells)
                    {
                        if (dgvc.ColumnIndex != 0)
                        {
                            dgvc.Value = "";
                        }
                    }
                }
                else
                {
                    //new
                    if (DialogResult.Yes == MessageBox.Show("Would you Like to Create a Journal Entry for Product as Removed?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        int maxm = 0; double cst = 0;
                        try
                        {
                            if (Main.Amatrix.acc == "")
                            {
                                SqlCeConnection mySqlConnection3 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                                SqlCeCommand mySqlCommand3 = new SqlCeCommand("SELECT MAX([Serial Number]) FROM PurchaseBook", mySqlConnection3);
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
                                DataTable dtpy = new DataTable(); basql.Execute(Main.Amatrix.acc, "SELECT MAX([Serial Number]) FROM PurchaseBook", "PurchaseBook", dtpy);
                                maxm = Convert.ToInt32(dtpy.Rows[0].ItemArray[0]);
                                maxm = maxm + 1;
                                dtpy.Clear(); dtpy.Dispose();
                            }
                        }
                        catch (Exception erty) { maxm = 1; }
                        //getprod

                        //SELECT product cost
                        DataTable dtp_prod_cost = new DataTable();
                        double tax = 0;
                        try
                        {
                            tax = Convert.ToDouble(textBox9.Text);
                        }
                        catch (Exception erty) { }
                        try
                        {
                            cst = Convert.ToDouble(textBox8.Text);
                        }
                        catch (Exception erty) { }
                        double summ = tax + cst;
                        //insert
                        if (Main.Amatrix.acc == "")
                        {
                            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                            SqlCeCommand cmd = new SqlCeCommand("INSERT INTO PurchaseBook VALUES('" + maxm.ToString() + "', '', getdate(), '', 'Removal of Product[" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "\\" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "]', '0', '" + tax.ToString() + "', '" + cst.ToString() + "', '0', '" + summ.ToString() + "', '', '" + summ.ToString() + "', '')", conn);
                            conn.Open();
                            SqlCeDataReader dr = cmd.ExecuteReader();
                            conn.Close();
                        }
                        else
                        {
                            DataTable dtpy = new DataTable();
                            basql.Execute(Main.Amatrix.acc, "INSERT INTO PurchaseBook VALUES('" + maxm.ToString() + "', '', getdate(), '', 'Removal of Product[" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "\\" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "]', '0', '" + tax.ToString() + "', '" + cst.ToString() + "', '0', '" + summ.ToString() + "', '', '" + summ.ToString() + "', '')", "PurchaseBook", dtpy);
                            dtpy.Clear(); dtpy.Dispose();
                        }
                    }
                    //new end
                    foreach (DataGridViewRow dgvr in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows.Remove(dgvr);
                    }
                }
                if (Main.Amatrix.mgt == "")
                {
                    prod_bulkTableAdapter.Update(prod_bulk_dtst);
                }
                else
                {
                    asql.Save(prod_bulk_dtst.prod_bulk, "prod_bulk", Main.Amatrix.mgt);
                }
            //}
            //catch (Exception erty) { general_mssg("Unable To Save Deletion Settings", ""); }
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

        private void pnt_Click(object sender, EventArgs e)
        {
            if (sender.Equals(printInventoryBulkInformationToolStripMenuItem) == true)
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dataGridView1);
            }
            else
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dgv2);
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.tx(this.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ofd2.ShowDialog();
        }

        private void ofd2_FileOk(object sender, CancelEventArgs e)
        {
            /*pictureBox2.BackgroundImage = Image.FromFile(ofd2.FileName);
            textBox16.Text = ofd2.FileName;

            if (Main.Amatrix.mgt == "")
            {
                tbx_lgu.Text = ofd2.FileName;
            }
            else
            {
                Image i = Image.FromFile(ofd2.FileName);
                byte[] b;
                try
                {
                    ImageConverter converter = new ImageConverter();
                    Bitmap bmp = new Bitmap(i);
                    b = (byte[])converter.ConvertTo(bmp, typeof(byte[]));
                    dgv2[16, dgv2.CurrentRow.Index].Value = b;
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }*/
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            /*try
            {
                pictureBox2.BackgroundImage = Image.FromFile(textBox16.Text);
            }
            catch (Exception erty) { pictureBox2.BackgroundImage = null; }*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*textBox16.Text = "";
            if (Main.Amatrix.mgt != "")
            {
                dgv2[16, dgv2.CurrentRow.Index].Value = DBNull.Value;
            }
            pictureBox2.BackgroundImage = null;*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //SCANNING---------------------------------------------------------------------> || ||||| ||
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mgmt_opr opr = new mgmt_opr();
            opr.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            allow_change = true;
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
                    if (dgv2[15, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value != DBNull.Value)
                    {
                        try
                        {
                            byte[] res1 = (byte[])dgv2[15, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value;
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
                            pbx_lgu.BackgroundImage = (Image)dgv2[15, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value;
                        }
                    }
                    else
                    {
                        pbx_lgu.BackgroundImage = Properties.Resources.product;
                    }
                }
                catch (Exception erty)
                {
                }

                if (textBox7.Text == "" && tabControl6.SelectedIndex == 2)
                {
                    tabControl6.SelectTab(0);
                    textBox7.BackColor = Color.DarkOrange;
                    Am_err ner = new Am_err();
                    ner.tx("Please Enter a Product ID Number(Highlighted in Orange) before Continuing to this Step.");
                }
                //2
                /*try
                {
                    if (dgv2[16, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value != DBNull.Value)
                    {
                        try
                        {
                            byte[] res1 = (byte[])dgv2[16, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value;
                            Image newImage;
                            using (MemoryStream ms = new MemoryStream(res1, 0, res1.Length))
                            {
                                newImage = Bitmap.FromStream(ms, true);
                                ms.Flush();
                                ms.Close();
                                ms.Dispose();
                            }
                            pictureBox2.BackgroundImage = newImage;
                        }
                        catch (Exception erty)
                        {
                            pbx_lgu.BackgroundImage = (Image)dgv2[16, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value;
                        }
                    }
                    else
                    {
                        pictureBox2.BackgroundImage = null;
                    }
                }
                catch (Exception erty)
                {
                }*/
            }
        }

        private void remv_pic_Click(object sender, EventArgs e)
        {
                tbx_lgu.Text = "";
                pbx_lgu.BackgroundImage = Properties.Resources.product;
                if (Main.Amatrix.mgt != "")
                {
                    dgv2[15, dgv2.CurrentRow.Index].Value = DBNull.Value;
                }
        }

        Gadg_maps mps = new Gadg_maps();
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            mps.Visible = true;
            mps.webBrowser1.Navigate("http://maps.google.co.in/maps?hl=en&tab=wl");
            mps.Dock = DockStyle.Fill;
            mps.BringToFront();
        }

        gadg_pics pcs;
        private void pbx_lgu_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (pbx_lgu.BackgroundImage != Properties.Resources.person)
                {
                    pcs = new gadg_pics();
                    pcs.fromfle(pbx_lgu.BackgroundImage);
                    if (pcs.BackgroundImage.Size.Height < pcs.Size.Height || pcs.BackgroundImage.Size.Width < pcs.Size.Width)
                    {
                        pcs.BackgroundImageLayout = ImageLayout.Center;
                    }
                    else
                    { pcs.BackgroundImageLayout = ImageLayout.Zoom; }
                    pcs.Size = new Size(400, 343);
                    pcs.Location = new Point(184, 56);
                    tabPage1.Controls.Add(pcs);
                    pcs.BringToFront();
                }
            }
            catch (Exception erty) { }
            pcs.BringToFront();
        }

        private void pbx_lgu_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                pcs.Dispose();
            }
            catch (Exception erty) { }
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

        private void loosing_Click(object sender, EventArgs e)
        {
            cms.Show(460, 401);
        }

        private void forThisProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prod_bulk_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prod_bulk WHERE [Notes/Information] = '" + textBox7.Text + "' AND [Sold To] IS NULL", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prod_bulk_dtst.prod_bulk.Load(dr);
                dataGridView1.DataSource = prod_bulk_dtst.prod_bulk;
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prod_bulk WHERE [Notes/Information] = '" + textBox7.Text + "' AND [Sold To] IS NULL");
                prod_bulk_dtst.Load(dr_univ, LoadOption.PreserveChanges, "prod_bulk");
            }
        }

        private void forAllProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prod_bulk_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prod_bulk WHERE [Sold To] IS NULL", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prod_bulk_dtst.prod_bulk.Load(dr);
                dataGridView1.DataSource = prod_bulk_dtst.prod_bulk;
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prod_bulk WHERE [Sold To] IS NULL");
                prod_bulk_dtst.Load(dr_univ, LoadOption.PreserveChanges, "prod_bulk");
            }
        }

        private void forThisProductInTheCurrentMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prod_bulk_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prod_bulk WHERE [Notes/Information] = '" + textBox7.Text + "' AND [Sold To] IS NULL AND datepart(mm, [Bought On Date (By Us)]) = datepart(mm, Getdate()) AND datepart(yy, [Bought On Date (By Us)]) = datepart(yy, Getdate())", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prod_bulk_dtst.prod_bulk.Load(dr);
                dataGridView1.DataSource = prod_bulk_dtst.prod_bulk;
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prod_bulk WHERE [Notes/Information] = '" + textBox7.Text + "' AND [Sold To] IS NULL AND datepart(mm, [Bought On Date (By Us)]) = datepart(mm, Getdate()) AND datepart(yy, [Bought On Date (By Us)]) = datepart(yy, Getdate())");
                prod_bulk_dtst.Load(dr_univ, LoadOption.PreserveChanges, "prod_bulk");
            }
        }

        private void forAllProductsInTheCurrentMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prod_bulk_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prod_bulk WHERE [Sold To] IS NULL AND datepart(mm, [Bought On Date (By Us)]) = datepart(mm, Getdate()) AND datepart(yy, [Bought On Date (By Us)]) = datepart(yy, Getdate())", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prod_bulk_dtst.prod_bulk.Load(dr);
                dataGridView1.DataSource = prod_bulk_dtst.prod_bulk;
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prod_bulk WHERE [Sold To] IS NULL AND datepart(mm, [Bought On Date (By Us)]) = datepart(mm, Getdate()) AND datepart(yy, [Bought On Date (By Us)]) = datepart(yy, Getdate())");
                prod_bulk_dtst.Load(dr_univ, LoadOption.PreserveChanges, "prod_bulk");
            }
        }

        private void okViewLossesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prod_bulk_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prod_bulk WHERE [Sold To] IS NULL AND datepart(mm, [Bought On Date (By Us)]) = '" + toolStripComboBox1.Text + "' AND datepart(yy, [Bought On Date (By Us)]) = '" + toolStripComboBox2.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prod_bulk_dtst.prod_bulk.Load(dr);
                dataGridView1.DataSource = prod_bulk_dtst.prod_bulk;
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prod_bulk WHERE [Sold To] IS NULL AND datepart(mm, [Bought On Date (By Us)]) = '" + toolStripComboBox1.Text + "' AND datepart(yy, [Bought On Date (By Us)]) = '" + toolStripComboBox2.Text + "'");
                prod_bulk_dtst.Load(dr_univ, LoadOption.PreserveChanges, "prod_bulk");
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            prod_bulk_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prod_bulk WHERE [Notes/Information] = '" + textBox7.Text + "' AND [Sold To] IS NULL AND datepart(mm, [Bought On Date (By Us)]) = '" + toolStripComboBox1.Text + "' AND datepart(yy, [Bought On Date (By Us)]) = '" + toolStripComboBox2.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prod_bulk_dtst.prod_bulk.Load(dr);
                dataGridView1.DataSource = prod_bulk_dtst.prod_bulk;
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prod_bulk WHERE [Notes/Information] = '" + textBox7.Text + "' AND [Sold To] IS NULL AND datepart(mm, [Bought On Date (By Us)]) = '" + toolStripComboBox1.Text + "' AND datepart(yy, [Bought On Date (By Us)]) = '" + toolStripComboBox2.Text + "'");
                prod_bulk_dtst.Load(dr_univ, LoadOption.PreserveChanges, "prod_bulk");
            }
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            Inventory_Searcher src = new Inventory_Searcher();
            src.Show();
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            AMG_Fle.Purch_ordr porder = new AMG_Fle.Purch_ordr();
            porder.tx(textBox7.Text, comboBox3.Text);
            porder.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            mgmt_sls_ordr sls = new mgmt_sls_ordr();
            sls.Show();
            sls.tx(textBox7.Text);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                prod_bulkTableAdapter.Update(prod_bulk_dtst);
            }
            else
            {
                asql.Save(prod_bulk_dtst.prod_bulk, "prod_bulk", Main.Amatrix.mgt);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1[4, dataGridView1.CurrentRow.Index].Value != DBNull.Value)
                {
                    cms_open_bulk.Show(button10, 0, button10.Size.Height);
                }
                else
                {
                    Am_err ner = new Am_err(); ner.tx("The Row You have Selected Does Not have a Set Logistical Value.");
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("No 'Open' Menu Available for the Current Row"); }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }
        //new
        private void selectedWithLogisticalManagmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mgmt_opr opr = new mgmt_opr();
            try
            {
                opr.tx(dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString());
            }
            catch (Exception erty) { opr.Close(); }
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            cms_manage_bulk.Show(button4, 0, button4.Size.Height);
        }

        private void resentItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mgmt_supch_resent rst = new mgmt_supch_resent();
            try
            {
                rst.tx(dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString(), dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString());
            }
            catch (Exception erty) { rst.Close(); Am_err ner = new Am_err(); ner.tx("No Items Available"); }
        }

        private void warrantiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mgmt_supch_warranties warr = new mgmt_supch_warranties();
            try
            {
                warr.tx(dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString(), dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString(), "System Admin(" + SystemInformation.UserDomainName + ")");
            }
            catch (Exception erty) { warr.Close(); Am_err ner = new Am_err(); ner.tx("No Items Available"); }
        }

        private void bkk_color_DoWork(object sender, DoWorkEventArgs e)
        {
            th_mark_strt();
        }

        //28-03-2013
        int x_c, y_c;
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            x_c = e.RowIndex;
            y_c = e.ColumnIndex;
            if (e.ColumnIndex == 9)
            {
                try
                {
                    bkk_bc.RunWorkerAsync();
                }
                catch (Exception erty) { check_bc(); }
            }
        }

        private Thread th_bc;
        private delegate void del_bc();
        private void bkk_bc_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                th_bc = new Thread(new ThreadStart(del_bc_strt));
                th_bc.IsBackground = true;
                th_bc.Start();
            }
            catch (Exception erty) { bkk_bc.CancelAsync(); }
        }

        private void del_bc_strt()
        {
            try
            {
                this.Invoke(new del_bc(check_bc));
            }
            catch (Exception erty) { bkk_bc.CancelAsync(); }
        }

        private void check_bc()
        {
            this.Enabled = false;
            DataTable dtpy = new DataTable();
            string s = dataGridView1[y_c, x_c].Value.ToString();
            dataGridView1[y_c, x_c].Value = "Verifying...";
            if (Main.Amatrix.mgt == "")
            {
                dtpy = scsql.Execute(Properties.Settings.Default.Amdtbse_4ConnectionString, "SELECT * FROM Prod_bulk WHERE [Bar Code Extension] = '" + s + "'");
            }
            else
            {
                dtpy = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Prod_bulk WHERE [Bar Code Extension] = '" + s + "'", "", dtpy);
            }
            
            if (dtpy.Rows.Count < 1)
            {
                dataGridView1[y_c, x_c].Value = s;
            }
            else
            {
                dataGridView1[y_c, x_c].Value = before_edit;
                MessageBox.Show("Bar Code Not Available (" + s + ") The Selected Bar Code Already Exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Enabled = true;
            dtpy.Clear();
            dtpy.Dispose();
        }

        private void bkk_bc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true) { check_bc(); }
        }

        string before_edit;
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                try
                {
                    before_edit = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                }
                catch (Exception erty) { before_edit = ""; }
            }
        }
        //chack main bc
        string s_change;
        private void textBox16_Enter(object sender, EventArgs e)
        {
            s_change = textBox16.Text;
        }

        private void textBox16_Leave(object sender, EventArgs e)
        {
            if (s_change != textBox16.Text)
            {
                try
                {
                    bkk_bc_main.RunWorkerAsync();
                }
                catch (Exception erty) { check_bc_main(); }
            }
        }

        private Thread th_bc_main;
        private delegate void del_bc_main();
        private void bkk_bc_main_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                th_bc_main = new Thread(new ThreadStart(del_bc_mn_strt));
                th_bc_main.IsBackground = true;
                th_bc_main.Start();
            }
            catch (Exception erty) { bkk_bc_main.CancelAsync(); }
        }

        private void del_bc_mn_strt()
        {
            try
            {
                this.Invoke(new del_bc_main(check_bc_main));
            }
            catch (Exception erty) { bkk_bc_main.CancelAsync(); }
        }

        private void check_bc_main()
        {
            this.Enabled = false;
            DataTable dtpy = new DataTable();
            string s = textBox16.Text;
            textBox16.Text = "Verifying...";
            if (Main.Amatrix.mgt == "")
            {
                dtpy = scsql.Execute(Properties.Settings.Default.Amdtbse_4ConnectionString, "SELECT * FROM Prod_mgmt WHERE [Supply Chain Data] = '" + s + "'");
            }
            else
            {
                dtpy = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Prod_mgmt WHERE [Supply Chain Data] = '" + s + "'", "", dtpy);
            }

            if (dtpy.Rows.Count < 1)
            {
                textBox16.Text = s;
            }
            else
            {
                textBox16.Text = s_change;
                MessageBox.Show("Bar Code Not Available (" + s + ") The Selected Bar Code Already Exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Enabled = true;
            dtpy.Clear();
            dtpy.Dispose();
        }

        private void bkk_bc_main_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true) { check_bc_main(); }
        }

        int whereami = 0;
        Thread Sync_th;
        delegate void Sync_del();
        private void Sync_bttn_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.Text = this.Text + " [Synchronizing]";
            Sync_bttn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            whereami = Convert.ToInt32(bindingNavigatorPositionItem.Text);
            try
            {
                bkk_sync.RunWorkerAsync();
            }
            catch (Exception erty) { Sync_bttn.BackColor = Color.DarkOrange; this.Enabled = true; this.Text = this.Text.Replace(" [Synchronizing]", ""); }
        }

        private void del_Sync_strt()
        {
            this.Invoke(new Sync_del(Sync));
        }

        private void bkk_sync_DoWork(object sender, DoWorkEventArgs e)
        {
            Sync_th = new Thread(new ThreadStart(del_Sync_strt));
            Sync_th.IsBackground = true;
            Sync_th.Start();
        }

        private void Sync()
        {
            prodmgmt_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prodmgmt_dtst.Prod_mgmt.Load(dr);
                conn.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand(Last_Query_Used, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                prodmgmt_dtst.Prod_mgmt.Load(dr);
                conn.Close();
            }
            load_bulk();
            prodmgmtBindingSource.Position = whereami - 1;
            this.Text = this.Text.Replace(" [Synchronizing]", "");
            this.Enabled = true;
            Sync_bttn.BackColor = Color.Transparent;
        }

        //new
        private void button5_Click_2(object sender, EventArgs e)
        {
            button5.BackColor = Color.WhiteSmoke;
            try
            {
                bkk_color.RunWorkerAsync();
            }
            catch (Exception erty) { th_mark_strt(); }
        }

        private void changed_levels(object sender, EventArgs e)
        {
            button5.BackColor = Color.FromArgb(255, 128, 0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Suppliers sp = new Suppliers();
        }

        //new
        private void button13_Click(object sender, EventArgs e)
        {
            mgmt_purchase_information mpi = new mgmt_purchase_information();
            mpi.tx(textBox7.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(Environment.CurrentDirectory + "\\Connect\\Client\\Amatrix Connect Business Client.exe");
                Main.Amatrix.al_prc.Add(p);
                p.StartInfo = psi;
                p.Start();
            }
            catch (Exception erTY) { }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Product_Type pt = new Product_Type();
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            try
            {
                bkk_prod_tp.RunWorkerAsync();
            }
            catch (Exception erty) { }
        }

        Thread th_tp;
        delegate void del_tp();
        private void bkk_prod_tp_DoWork(object sender, DoWorkEventArgs e)
        {
            th_tp_strt();
        }

        private void th_tp_strt()
        {
            try
            {
                th_tp = new Thread(new ThreadStart(del_tp_strt));
                th_tp.IsBackground = true;
                th_tp.Start();
            }
            catch (Exception erty) { bkk_prod_tp.CancelAsync(); }
        }

        private void del_tp_strt()
        {
            try
            {
                this.Invoke(new del_tp(load_prod_typ));
            }
            catch (Exception erty) { bkk_prod_tp.CancelAsync(); }
        }

        private void load_prod_typ()
        {
            DataTable dtp_types = new DataTable();
            if (Amatrix.mgt == "")
            {
                dtp_types = scsql.Execute(Properties.Settings.Default.Amdtbse_6ConnectionString, "SELECT * FROM Product_Types");
            }
            else
            {
                dtp_types = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Product_Types", "", dtp_types);
            }

            comboBox2.Items.Clear();
            foreach (DataRow dr in dtp_types.Rows)
            {
                try
                {
                    comboBox2.Items.Add(dr.ItemArray[0].ToString());
                }
                catch (Exception erty) { MessageBox.Show(erty.Message); }
            }

            dtp_types.Clear();
            dtp_types.Dispose();
        }

        private void bkk_prod_tp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                load_prod_typ();
            }
        }

        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception erty) { }
        }

        Thread th_supp;
        delegate void del_supp();
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            th_supp_strt();
        }

        private void th_supp_strt()
        {
            try
            {
                th_supp = new Thread(new ThreadStart(del_supp_strt));
                th_supp.IsBackground = true;
                th_supp.Start();
            }
            catch (Exception erty) { backgroundWorker1.CancelAsync(); }
        }

        private void del_supp_strt()
        {
            try
            {
                this.Invoke(new del_supp(load_supp_typ));
            }
            catch (Exception erty) { backgroundWorker1.CancelAsync(); }
        }

        private void load_supp_typ()
        {
            DataTable dtp_supps = new DataTable();
            if (Amatrix.mgt == "")
            {
                dtp_supps = scsql.Execute(Properties.Settings.Default.Amdtbse_6ConnectionString, "SELECT * FROM Suppliers");
            }
            else
            {
                basql.Execute(Amatrix.mgt, "SELECT * FROM Suppliers", "", dtp_supps);
            }

            comboBox3.Items.Clear();
            foreach (DataRow dr in dtp_supps.Rows)
            {
                comboBox3.Items.Add(dr.ItemArray[0].ToString());
            }

            dtp_supps.Clear();
            dtp_supps.Dispose();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                load_supp_typ();
            }
        }

        //new
        private void button15_Click(object sender, EventArgs e)
        {
            mgmt_supch_units uns = new mgmt_supch_units(textBox7.Text, cn.Text);
        }

        string old_id = ""; bool allow_change = true;
        private void textBox7_Leave(object sender, EventArgs e)
        {
            //check for duplicate id numbers
            if (edited == true && textBox7.Text != old_id)
            {
                edited = false;
                DataTable dtp_check = new DataTable();
                if (Amatrix.mgt == "")
                {
                    dtp_check = scsql.Execute(Properties.Settings.Default.Amdtbse_4ConnectionString, "SELECT * FROM Prod_mgmt WHERE [Product ID Number] = '" + textBox7.Text + "'");
                }
                else
                {
                    dtp_check = basql.Execute(Amatrix.mgt, "SELECT * FROM Prod_mgmt WHERE [Product ID Number] = '" + textBox7.Text + "'", "", dtp_check);
                }

                if (dtp_check.Rows.Count > 0 && cn.Text != dtp_check.Rows[0].ItemArray[0].ToString())
                {
                    edited = true;
                    Am_err ner = new Am_err();
                    ner.tx("The Product Identification Number You Just Entered Already Exists, Enter a New One, or your Previous ID Number for this Product.");

                    //textBox7.Text = "";
                    textBox7.SelectAll();
                    textBox7.Focus();
                    svebtn.Enabled = false; save_inv.Enabled = false;

                    foreach (Control c in tabPage6.Controls)
                    {
                        c.Enabled = false;
                    }
                }
                else
                {
                    bindingNavigator1.Enabled = true;
                    tbxfned.Enabled = true;
                    gotoitm.Enabled = true;
                    svebtn.Enabled = true; save_inv.Enabled = true;

                    foreach (Control c in tabPage6.Controls)
                    {
                        c.Enabled = true;
                    }

                    if (DialogResult.Yes == MessageBox.Show("For Safety Reasons, Amatrix will Now Save Your new Product ID, Continue? if not the Old ID will be Reverted", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        svebtn_ButtonClick(svebtn, e);
                        allow_change = true;
                    }
                    else { textBox7.Text = old_id; }
                }
            }
            else
            {
                bindingNavigator1.Enabled = true;
                tbxfned.Enabled = true;
                gotoitm.Enabled = true;
                svebtn.Enabled = true; save_inv.Enabled = true;
            }
        }

        bool edited = false;
        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            edited = true;
            svebtn.Enabled = false; save_inv.Enabled = false;
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            bindingNavigator1.Enabled = false;
            svebtn.Enabled = false; save_inv.Enabled = false;
            tbxfned.Enabled = false;
            gotoitm.Enabled = false;
            
            if (allow_change == true)
            {
                allow_change = false;
                old_id = textBox7.Text;
            }
        }
    }
}