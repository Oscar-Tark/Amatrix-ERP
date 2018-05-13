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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;
using System.Threading;
using Base_ASQL;
using Extern_ASQL;

namespace Main
{
    public partial class mgmt_stratgy : Form
    {
        //objects
        private delegate void delinit();
        private Thread thinit;
        private int Year = DateTime.Now.Year;
        private int howmany;
        private int maxm;
        private int biggest = 100;
        private int smallest = 1;
        private DataGridViewCellEventArgs dgvcea;

        BASQL basql = new BASQL();
        Extern_Sql asql = new Extern_Sql();

        public mgmt_stratgy()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.amdsicnico;
            this.Disposed += new EventHandler(mgmt_stratgy_Disposed);
            InitializeComponent();

            if (Main.Amatrix.mgt != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.mgt); pwd.Owner = this; }
                catch (Exception erty) { }
            }

            this.Opacity = Properties.Settings.Default.opacity;
            this.Text = "Amatrix Project Managment";
            thinitstrt();
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

        void mgmt_stratgy_Disposed(object sender, EventArgs e)
        {
            dgv.DataSource = null;
            dgv3.DataSource = null;

            prjmgmt_dtst.Clear();
            prj_tasks_dtst.Clear();

            dtp_main.Clear();
            dtp_main.Dispose();

            dtp.Clear();
            dtp.Dispose();

            dtp_mater.Clear();
            dtp_mater.Dispose();

            dtptp.Clear(); dtptp.Dispose();

            dgv_prods.DataSource = null;

            prj_mgmt_tsksTableAdapter.Connection.Close();
            prj_mgmtTableAdapter.Connection.Close();
            

            prj_tasks_dtst.Dispose();
            prjmgmt_dtst.Dispose();

            prjmgmttsksBindingSource.Dispose();
            prjmgmtBindingSource.Dispose();
            prj_mgmtTableAdapter.Dispose();

            Sync_bttn.Click -= Sync_bttn_Click;
            bkk_sync.DoWork -= bkk_sync_DoWork;
            bindingNavigatorPositionItem.TextChanged -= bindingNavigatorPositionItem_TextChanged_1;
            label20.TextChanged -= label20_TextChanged;
            toolStripButton6.Click -= toolStripButton6_Click;
            button18.Click -= button18_Click;
            tabControl1.SelectedIndexChanged -= tabControl1_SelectedIndexChanged;
            button13.Click -= button13_Click;
            dataGridView2.CellMouseClick -= dataGridView2_CellMouseClick;
            button19.Click -= button19_Click;
            button14.Click -= button14_Click;
            button3.Click -= button3_Click;
            cn.MouseEnter -= tvtxt1_MouseEnter;
            cn.MouseLeave -= tvtxt1_MouseLeave;
            textBox8.MouseEnter -= tvtxt1_MouseEnter;
            textBox8.MouseLeave -= tvtxt1_MouseLeave;
            textBox1.MouseEnter -= tvtxt1_MouseEnter;
            textBox1.MouseLeave -= tvtxt1_MouseLeave;
            textBox2.MouseEnter -= tvtxt1_MouseEnter;
            textBox2.MouseLeave -= tvtxt1_MouseLeave;
            textBox4.MouseEnter -= tvtxt1_MouseEnter;
            textBox4.MouseLeave -= tvtxt1_MouseLeave;
            textBox5.MouseEnter -= tvtxt1_MouseEnter;
            textBox5.MouseLeave -= tvtxt1_MouseLeave;

            bindingNavigatorDeleteItem.Click -= bindingNavigatorDeleteItem_Click;
            bindingNavigatorDeleteItem.MouseDown -= bindingNavigatorDeleteItem_MouseDown;

            toolStripButton5.Click -= toolStripButton5_Click;
            toolStripButton5.MouseDown -= toolStripButton5_MouseDown;

            trackBar1.ValueChanged -= trackBar1_Scroll;
            trackBar1.Scroll -= trackBar1_Scroll;
            dgv3.CellMouseEnter -= dgv_CellMouseEnter;
            dgv3.CellMouseLeave -= dgv_CellMouseLeave;
            dgv3.CellBeginEdit -= dgv3_CellBeginEdit;
            toolStripButton2.Click -= toolStripButton2_Click;
            toolStripButton3.Click -= toolStripButton3_Click;
            toolStripButton5.Click -= toolStripButton5_Click;
            dgv3.CellClick -= dgv3_CellClick;
            toolStripButton71.Click -= toolStripButton71_Click;
            textBox1.TextChanged -= BAL_CH;
            textBox2.TextChanged -= BAL_CH;
            textBox4.TextChanged -= BAL_CH;
            textBox5.TextChanged -= BAL_CH;
            button1.Click -= BAL_CH;
            cn.TextAlignChanged -= bindingNavigatorPositionItem_TextChanged;
            toolStripButton1.Click -= toolStripButton1_Click;
            bindingNavigatorPositionItem.TextChanged -= bindingNavigatorPositionItem_TextChanged;
            bindingNavigatorAddNewItem.Click -= bindingNavigatorAddNewItem_Click;
            //toolStripButton68.Click -= toolStripButton68_Click;
            this.Deactivate -= this.mgmt_stratgy_Deactivate;
            this.Load -= this.mgmt_stratgy_Load;
            this.Activated -= this.mgmt_stratgy_Activated;
            this.save_inv.Click -= this.sve;
            this.restr.Click -= this.rstrt_Click;
            this.clsemn.Click -= this.clse_ButtonClick;
            this.undoall.Click -= this.undoall_Click;
            this.cpy.Click -= this.cpy_Click;
            this.ct.Click -= this.ct_Click;
            this.pster.Click -= this.pster_Click;
            this.sall.Click -= this.sall_Click;
            this.initializeToolStripMenuItem.Click -= this.initializeToolStripMenuItem_Click;
            this.switchDatabaseToolStripMenuItem.Click -= this.switchDatabaseToolStripMenuItem_Click;
            this.dynayes.Click -= this.dynayes_Click;
            this.dynano.Click -= this.dynano_Click;
            this.rePartitionDataBaseToolStripMenuItem.Click -= this.rePartitionDataBaseToolStripMenuItem_Click;
            this.contentsToolStripMenuItem.Click -= this.contentsToolStripMenuItem_Click;
            this.abtmnu.Click -= this.abtmnu_Click;
            this.svebtn.Click -= this.sve;
            this.clse.MouseLeave -= this.clse_MouseLeave;
            this.clse.ButtonClick -= this.clse_ButtonClick;
            this.clse.MouseEnter -= this.clse_MouseEnter;
            this.rstrt.Click -= this.rstrt_Click;
            this.connlbl.MouseEnter -= this.connlbl_MouseEnter;
            this.connlbl.MouseLeave -= this.connlbl_MouseLeave;
            this.connlbl.Click -= this.connlbl_Click;
            this.selectToolStripMenuItem.Click -= this.undoall_Click;
            this.copyToolStripMenuItem.Click -= this.cpy_Click;
            this.cutToolStripMenuItem.Click -= this.ct_Click;
            this.pasteToolStripMenuItem.Click -= this.pster_Click;
            this.deleteToolStripMenuItem.Click -= this.deletecell_Click;
            this.selectAllToolStripMenuItem.Click -= this.sall_Click;
            this.ts2.MouseEnter -= this.ts2_MouseEnter;
            this.ts2.MouseLeave -= this.ts2_MouseLeave;
            this.tbxfned.Leave -= this.tbxfned_Leave;
            this.tbxfned.Enter -= this.tbxfned_Enter;
            this.gotoitm.Click -= this.gotoitm_ButtonClick;
            this.edt.Click -= this.edt_Click;
            this.pnt.Click -= this.pnt_Click;
            this.rsdbord.Click -= this.rsdbord_Click;
            this.dflt_dgvbord.Click -= this.dflt_dgvbord_Click;
            this.O.Click -= this.IO_Click;
            this.IO.Click -= this.IO_Click;
            this.colreordr.Click -= this.colreordr_Click;
            this.colreordflse.Click -= this.colreordflse_Click;
            this.ascvw.Click -= this.ascvw_Click;
            this.descvw.Click -= this.descvw_Click;
            this.fnt_mnu.DropDownOpened -= this.Virtual_menu;
            this.tbxfntlv.SelectedIndexChanged -= this.Virtual_Combo;
            this.tsc_fnt_sze.SelectedIndexChanged -= this.Virtual_Combo;
            this.toolStripButton4.Click -= this.toolStripButton12_Click;
            this.decprj.Tick -= this.decprj_Tick;
            this.clseprj.Tick -= this.clseprj_Tick;
            this.connectToToolStripMenuItem.Click -= this.connectToToolStripMenuItem_Click;
            this.helpToolStripMenuItem1.Click -= this.contentsToolStripMenuItem_Click;
            this.ttp_del.Tick -= this.ttp_del_Tick_1;
            this.tmr.Tick -= this.tmr_Tick;
            this.bkk_graph.DoWork -= this.bkk_graph_DoWork;
            this.bkk_graph.RunWorkerCompleted -= this.bkk_graph_RunWorkerCompleted;
            this.toolStrip5.MouseEnter -= this.ts2_MouseEnter;
            this.toolStrip5.MouseLeave -= this.ts2_MouseLeave;
            this.QA_cmd.DropDownOpened -= this.QA_cmd_DropDownOpened;
            this.mini_timelife.Click -= this.toolStripButton12_Click;
            this.toolStripButton53.Click -= this.undoall_Click;
            this.toolStripButton54.Click -= this.cpy_Click;
            this.toolStripButton55.Click -= this.ct_Click;
            this.toolStripButton58.Click -= this.pster_Click;
            this.toolStripButton59.Click -= this.sall_Click;
            this.toolStripButton60.Click -= this.deletecell_Click;
            this.remv_zz.Click -= this.remv_zz_Click;
            this.toolStripButton61.Click -= this.toolStripButton12_Click;
            this.toolStripButton63.Click -= this.cpy_Click;
            this.toolStripButton64.Click -= this.ct_Click;
            this.toolStripButton65.Click -= this.pster_Click;
            this.nxt_yr.Click -= this.yr_tl;
            this.prv_yr.Click -= this.yr_tl;
            this.dgv.CellMouseLeave -= this.dgv_CellMouseLeave;
            this.dgv.CellMouseEnter -= this.dgv_CellMouseEnter;
            this.dgv.DataError -= this.dgv_DataError;
            this.toolStripButton1.Click -= this.new_q;
            dgv3.DataError -= dgv3_DataError;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void thinitstrt()
        {
            try
            {
                thinit = new Thread(new ThreadStart(delinitstrt));
                thinit.Start();
            }
            catch (Exception erty) { Init(); }
        }

        private void delinitstrt()
        {
            try
            {
                this.Invoke(new delinit(Init));
            }
            catch (Exception erty) { Init(); }
        }

        private void Init()
        {
            try
            {
                if (choicesett.Default.tpmst == true)
                {
                    this.TopMost = true;
                }
                else if (choicesett.Default.tpmst == false)
                {
                    this.TopMost = false;
                }
            }
            catch (Exception erty) { }

            try
            {
                if (acc_journ_sett.Default.IO == false)
                {
                    dgv.ReadOnly = false;
                    re_only.Visible = false;
                }
                else if (acc_journ_sett.Default.IO == true)
                {
                    dgv.ReadOnly = true;
                    re_only.Visible = true;
                }
            }
            catch (Exception erty) { }

            if (acc_journ_sett.Default.dgvborder == 0)
            {
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            }

            else if (acc_journ_sett.Default.dgvborder == 1)
            {
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            }
            else { acc_journ_sett.Default.dgvborder = 0; acc_journ_sett.Default.Save(); }
            try
            {
                init_db();
            }
            catch (Exception erty) { init_db(); }
            //bkkinit.RunWorkerAsync();
        }

        private DataTable dtp_main = new DataTable();
        private void init_db()
        {
            connlbl.Text = "Initializing Database..";
            prj_tasks_dtst.Clear();
            //if (app_connection_settings.Default.Server_addr.ToLower() == "server address" || app_connection_settings.Default.Server_addr.ToLower() == "internal server")
            //{
            Last_Query_Used = "Select * From prj_mgmt";
                if (Main.Amatrix.mgt == "")
                {
                    try
                    {
                        prj_mgmtTableAdapter.Connection.Open();
                    }
                    catch (Exception erty) { }
                    try
                    {
                        string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                        string SqlString = "Select * From prj_mgmt";
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {
                                cmd.CommandType = CommandType.Text;
                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                                    dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                                }
                            }
                        }
                    }
                    catch (Exception erty) { }
                }
                else
                {
                    dr_univ = Quer("SELECT * FROM prj_mgmt");
                    prjmgmt_dtst.prj_mgmt.Load(dr_univ);
                }
                conn2();
            //}
            //else { }

            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prj_mgmt_tsks WHERE [FORPRJ] = '" + cn.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prj_tasks_dtst.prj_mgmt_tsks.Load(dr);
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prj_mgmt_tsks WHERE [FORPRJ] = '" + cn.Text + "'");
                prj_tasks_dtst.prj_mgmt_tsks.Load(dr_univ);
            }
        }

        private void conn2()
        {
            if (Main.Amatrix.mgt == "")
            {
                if (prj_mgmtTableAdapter.Connection.State == ConnectionState.Open)
                {
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Project Managment Table is Connected";
                }
                else if (prj_mgmtTableAdapter.Connection.State == ConnectionState.Closed)
                {
                    connlbl.Text = "Project Managment Table is Not Connected";
                    connlbl.Image = Properties.Resources.connctno;
                }
                else
                {
                    connlbl.Text = "Project Managment Table Connectivity Error (Reconnect Please)"; connlbl.Image = Properties.Resources.conncerr;
                }
                db_info.Text = "Project Managment Viewer DataBase: " + prj_mgmtTableAdapter.Connection.Database + ", Project Managment Database: " + prj_mgmtTableAdapter.Connection.Database;
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    conn.Open();

                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Project Managment is Connected";
                }
                catch (Exception erty)
                {
                    connlbl.Text = "Project Managment is Not Connected";
                    connlbl.Image = Properties.Resources.connctno;
                }
            }
        }

        private void mgmt_stratgy_Load(object sender, EventArgs e)
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

        private void connlbl_Click(object sender, EventArgs e)
        {
            Point ptt = new Point();
            ptt.X = Cursor.Position.X - this.Location.X + 50;
            ptt.Y = Cursor.Position.Y - this.Location.Y;
            cmslv.Show(ptt);
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
            mgmt_stratgy stgky = new mgmt_stratgy();
            stgky.Show();
            this.Close();
        }

        private void decprj_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                decprj.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void clseprj_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void clse_ButtonClick(object sender, EventArgs e)
        {
            clseprj.Start();
        }

        private void mgmt_stratgy_Deactivate(object sender, EventArgs e)
        {
            try
            {
                decprj.Stop();
            }
            catch(Exception ty){}
            decprj.Start();
        }

        private void mgmt_stratgy_Activated(object sender, EventArgs e)
        {
            try
            {
                decprj.Stop();
            }
            catch (Exception ty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private Color cl_tmp;
        private void dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (visual.Default.Basic == false)
            {
                try
                {
                    cl_tmp = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.AliceBlue;
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
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = cl_tmp;
                }
                catch (Exception erty) { }
            }
            else { }
        }

        //error handling
        private string mssge_ttp;
        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            err.Visible = true;
            if (e.Exception.Message.ToLower() == "input string was not in a correct format.")
            {
                mssge_ttp = "An error Occured : a Value that is not acceptable for the current Column has been Entered";
            }
            else
            {
                mssge_ttp = e.Exception.Message;
            }

            err_inf_1.Text = "Row : [" + e.RowIndex.ToString() + "] Cell : [" + e.ColumnIndex.ToString() + "], Serial Number : " + dgv[0, e.RowIndex].Value.ToString();

            err_inf_2.Text = mssge_ttp;

            try
            {
                ttp2.Show(mssge_ttp, this, this.Size.Width - 114, ts2.Location.Y - 10, 5000);
            }
            catch (Exception erty) { }

            ttp_del.Start();
        }

        private void dgv_Enter(object sender, EventArgs e)
        {
            conn2();
        }

        private void ttp_del_Tick_1(object sender, EventArgs e)
        {
            ttp_del.Stop();
            if (err.DropDown.Visible != true)
            {
                err.Visible = false;
            }
        }

        private void sve(object sender, EventArgs e)
        {
            try
            {
                if (Main.Amatrix.mgt == "")
                {
                    prjmgmtBindingSource.EndEdit();
                    prj_mgmtTableAdapter.Update(prjmgmt_dtst);
                    prj_mgmt_tsksTableAdapter.Update(prj_tasks_dtst);
                }
                else
                {
                    prjmgmtBindingSource.EndEdit();
                    asql.Save(prjmgmt_dtst.prj_mgmt, "prj_mgmt", Main.Amatrix.mgt);
                    asql.Save(prj_tasks_dtst.prj_mgmt_tsks, "prj_mgmt_tsks", Main.Amatrix.mgt);
                    try
                    {
                        Main.Amatrix.ascl.broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><typ>w</typ><val>0</val><app>" + this.Name + "</app><par>[" + toolStrip1.Name + "]</par><con>Sync_bttn</con>");
                    }
                    catch (Exception erty) { general_mssg("Syncronization is not Set Up", "Sync Error"); }
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A Fatal Error Occured While Saving Your Information. The Operation Was Halted and Your Data Was not Saved."); }
        }

        private void deletecell_Click(object sender, EventArgs e)
        {
            this.ActiveControl.Text = "";
        }

        private void oper_save()
        {
            if (acc_journ_sett.Default.dynam_jrn == true)
            {
                try
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        prjmgmtBindingSource.EndEdit();
                        prj_mgmtTableAdapter.Update(prjmgmt_dtst);
                        prj_mgmt_tsksTableAdapter.Update(prj_tasks_dtst);
                    }
                    else
                    {
                        prjmgmtBindingSource.EndEdit();
                        asql.Save(prjmgmt_dtst.prj_mgmt, "prj_mgmt", Main.Amatrix.mgt);
                        asql.Save(prj_tasks_dtst.prj_mgmt_tsks, "prj_mgmt_tsks", Main.Amatrix.mgt);
                    }
                }
                catch (Exception erty) { }
            }
        }

        private void gotoitm_Click(object sender, EventArgs e)
        {
            oper_save(); string SqlString = "Select * FROM prj_mgmt WHERE [Serial Number] LIKE '%" + tbxfned.Text + "%' OR [prj_mgmt Reference Number (ID)] LIKE '%" + tbxfned.Text + "%' OR [Date] LIKE '%" + tbxfned.Text + "%' OR [Due Date] LIKE '%" + tbxfned.Text + "%' OR [Billers Name] LIKE '%" + tbxfned.Text + "%' OR [Shippers Name] LIKE '%" + tbxfned.Text + "%' OR [Bill To (Address)] LIKE '%" + tbxfned.Text + "%' OR [Ship To (Address)] LIKE '%" + tbxfned.Text + "%' OR [Bill To Contact] LIKE '%" + tbxfned.Text + "%' OR [Ship To Contact] LIKE '%" + tbxfned.Text + "%' OR [Bill To Fax] LIKE '%" + tbxfned.Text + "%' OR [Ship To Fax] LIKE '%" + tbxfned.Text + "%' OR [Bill To Email ID] LIKE '%" + tbxfned.Text + "%' OR [Ship To Email ID] LIKE '%" + tbxfned.Text + "%' OR [Item] LIKE '%" + tbxfned.Text + "%' OR [Item ID] LIKE '%" + tbxfned.Text + "%' OR [Item Description] LIKE '%" + tbxfned.Text + "%' OR [Unit Price] LIKE '%" + tbxfned.Text + "%' OR [Units Ordered] LIKE '%" + tbxfned.Text + "%' OR [Units Delivered] LIKE '%" + tbxfned.Text + "%' OR [Cost] LIKE '%" + tbxfned.Text + "%'OR [Profit] LIKE '%" + tbxfned.Text + "%' OR [Owing] LIKE '%" + tbxfned.Text + "%' OR [Owing Amount] LIKE '%" + tbxfned.Text + "%' OR [Paid] LIKE '%" + tbxfned.Text + "%' OR [Paid Amount] LIKE '%" + tbxfned.Text + "%' OR [Vat Rate (%)] LIKE '%" + tbxfned.Text + "%' OR [Vat] LIKE '%" + tbxfned.Text + "%' OR [Other Tax Rate (%)] LIKE '%" + tbxfned.Text + "%' OR [Other Tax] LIKE '%" + tbxfned.Text + "%' OR [Total After Tax] LIKE '%" + tbxfned.Text + "%' OR [Total] LIKE '%" + tbxfned.Text + "%' OR [Notes] LIKE '%" + tbxfned.Text + "%'";
            if (Main.Amatrix.mgt == "")
            {
                prjmgmt_dtst.Clear();
                string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                        }
                    }
                }
            }
            else
            {
                dtp_main = basql.Execute(Main.Amatrix.mgt, SqlString, "prj_mgmt", dtp_main);
                dgv.DataSource = dtp_main;
            }
        }

        private void Showalljourn()
        {
            oper_save();
            Last_Query_Used = "Select * FROM prj_mgmt";
            if (Main.Amatrix.mgt == "")
            {
                prjmgmt_dtst.Clear();
                string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                string SqlString = "Select * FROM prj_mgmt";
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                        }
                    }
                }
            }
            else
            {
                prjmgmt_dtst.Clear();
                dr_univ = Quer("Select * FROM prj_mgmt");
                prjmgmt_dtst.prj_mgmt.Load(dr_univ);
            }

            prj_tasks_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prj_mgmt_tsks WHERE [FORPRJ] = '" + cn.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prj_tasks_dtst.prj_mgmt_tsks.Load(dr);
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prj_mgmt_tsks WHERE [FORPRJ] = '" + cn.Text + "'");
                prj_tasks_dtst.prj_mgmt_tsks.Load(dr_univ);
            }
        }

        //Queries


        //dynam query
        ToolStripButton b_temp;
        ToolStripMenuItem mt, mt2, mt3;
        ToolStripTextBox tbxx, tbxx2; ToolStripComboBox tcxx;
        string who, what1, what2;
        private void eqls(ToolStripButton sender)
        {
            oper_save();
            mt = (ToolStripMenuItem)sender.Owner.Items[0];
            try
            {
                tbxx = (ToolStripTextBox)sender.Owner.Items[1];
                what2 = tbxx.Text;
            }
            catch (Exception erty) { tcxx = (ToolStripComboBox)sender.Owner.Items[1]; what2 = tcxx.Text; }
            mt2 = (ToolStripMenuItem)mt.OwnerItem;
            mt3 = (ToolStripMenuItem)mt2.OwnerItem;
            what1 = mt.Owner.Items[0].Text;

            who = mt3.Text;
            who = who.Remove(who.Length - 3);

            if (who.ToLower() == "start date")
            {
                who = "Sart Date";
            }
            string SqlString = "Select * FROM prj_mgmt WHERE [" + who + "] = '" + what2 + "'";
            if (Main.Amatrix.mgt == "")
            {
                prjmgmt_dtst.Clear();
                string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                        }
                    }
                }
            }
            else
            {
                dtp_main.Clear();
                dtp_main = basql.Execute(Main.Amatrix.mgt, SqlString, "prj_mgmt", dtp_main);
                dgv.DataSource = dtp_main;
            }
        }

        private void nt_eql(ToolStripButton sender)
        {
            oper_save();
            mt = (ToolStripMenuItem)sender.Owner.Items[0];
            try
            {
                tbxx = (ToolStripTextBox)sender.Owner.Items[1];
                what2 = tbxx.Text;
            }
            catch (Exception erty) { tcxx = (ToolStripComboBox)sender.Owner.Items[1]; what2 = tcxx.Text; }
            mt2 = (ToolStripMenuItem)mt.OwnerItem;
            mt3 = (ToolStripMenuItem)mt2.OwnerItem;
            what1 = mt.Owner.Items[0].Text;

            who = mt3.Text;
            who = who.Remove(who.Length - 3);
            if (who.ToLower() == "start date")
            {
                who = "Sart Date";
            }
            string SqlString = "Select * FROM prj_mgmt WHERE [" + who + "] != '" + what2 + "'";

            if (Main.Amatrix.mgt == "")
            {
                prjmgmt_dtst.Clear();
                string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                        }
                    }
                }
            }
            else
            {
                dtp_main.Clear();
                dtp_main = basql.Execute(Main.Amatrix.mgt, SqlString, "prj_mgmt", dtp_main);
                dgv.DataSource = dtp_main;
            }
        }

        //between/between dates
        private void btw(ToolStripButton sender)
        {
            oper_save();
            mt = (ToolStripMenuItem)sender.Owner.Items[0];
            try
            {
                tbxx = (ToolStripTextBox)sender.Owner.Items[1];
                what2 = tbxx.Text;
            }
            catch (Exception erty) { tcxx = (ToolStripComboBox)sender.Owner.Items[1]; what2 = tcxx.Text; }
            tbxx2 = (ToolStripTextBox)sender.Owner.Items[2];
            mt2 = (ToolStripMenuItem)mt.OwnerItem;
            mt3 = (ToolStripMenuItem)mt2.OwnerItem;
            what1 = mt.Owner.Items[0].Text;

            who = mt3.Text;
            who = who.Remove(who.Length - 3);
            if (who.ToLower() == "start date")
            {
                who = "Sart Date";
            }
            string SqlString = "Select * FROM prj_mgmt WHERE [" + who + "] > '" + what2 + "' AND [" + who + "] < '" + tbxx2.Text + "'";
            if (Main.Amatrix.mgt == "")
            {
                prjmgmt_dtst.Clear();
                string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                        }
                    }
                }
            }
            else
            {
                dtp_main.Clear();
                dtp_main = basql.Execute(Main.Amatrix.mgt, SqlString, "prj_mgmt", dtp_main);
                dgv.DataSource = dtp_main;
            }
        }

        //less/before than
        private void lss_thn(ToolStripButton sender)
        {
            oper_save();
            mt = (ToolStripMenuItem)sender.Owner.Items[0];
            try
            {
                tbxx = (ToolStripTextBox)sender.Owner.Items[1];
                what2 = tbxx.Text;
            }
            catch (Exception erty) { tcxx = (ToolStripComboBox)sender.Owner.Items[1]; what2 = tcxx.Text; }
            mt2 = (ToolStripMenuItem)mt.OwnerItem;
            mt3 = (ToolStripMenuItem)mt2.OwnerItem;
            what1 = mt.Owner.Items[0].Text;

            who = mt3.Text;
            who = who.Remove(who.Length - 3);
            if (who.ToLower() == "start date")
            {
                who = "Sart Date";
            }
            string SqlString = "Select * FROM prj_mgmt WHERE [" + who + "] < '" + what2 + "'";

            if (Main.Amatrix.mgt == "")
            {
                prjmgmt_dtst.Clear();
                string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                        }
                    }
                }
            }
            else
            {
                dtp_main.Clear();
                dtp_main = basql.Execute(Main.Amatrix.mgt, SqlString, "prj_mgmt", dtp_main);
                dgv.DataSource = dtp_main;
            }
        }

        //greater/after than
        private void grt_thn(ToolStripButton sender)
        {
            oper_save();
            mt = (ToolStripMenuItem)sender.Owner.Items[0];
            try
            {
                tbxx = (ToolStripTextBox)sender.Owner.Items[1];
                what2 = tbxx.Text;
            }
            catch (Exception erty) { tcxx = (ToolStripComboBox)sender.Owner.Items[1]; what2 = tcxx.Text; }
            mt2 = (ToolStripMenuItem)mt.OwnerItem;
            mt3 = (ToolStripMenuItem)mt2.OwnerItem;
            what1 = mt.Owner.Items[0].Text;

            who = mt3.Text;
            who = who.Remove(who.Length - 3);
            if (who.ToLower() == "start date")
            {
                who = "Sart Date";
            }
            string SqlString = "Select * FROM prj_mgmt WHERE [" + who + "] > '" + what2 + "'";

            if (Main.Amatrix.mgt == "")
            {
                prjmgmt_dtst.Clear();
                string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                        }
                    }
                }
            }
            else
            {
                dtp_main.Clear();
                dtp_main = basql.Execute(Main.Amatrix.mgt, SqlString, "prj_mgmt", dtp_main);
                dgv.DataSource = dtp_main;
            }
        }

        //starting with
        private void str_wth(ToolStripButton sender)
        {
            oper_save();
            mt = (ToolStripMenuItem)sender.Owner.Items[0];
            try
            {
                tbxx = (ToolStripTextBox)sender.Owner.Items[1];
                what2 = tbxx.Text;
            }
            catch (Exception erty) { tcxx = (ToolStripComboBox)sender.Owner.Items[1]; what2 = tcxx.Text; }
            mt2 = (ToolStripMenuItem)mt.OwnerItem;
            mt3 = (ToolStripMenuItem)mt2.OwnerItem;
            what1 = mt.Owner.Items[0].Text;

            who = mt3.Text;
            who = who.Remove(who.Length - 3);
            if (who.ToLower() == "start date")
            {
                who = "Sart Date";
            }
            string SqlString = "Select * FROM prj_mgmt WHERE [" + who + "] LIKE '" + what2 + "%'";
            if (Main.Amatrix.mgt == "")
            {
                prjmgmt_dtst.Clear();
                string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                        }
                    }
                }
            }
            else
            {
                dtp_main.Clear();
                dtp_main = basql.Execute(Main.Amatrix.mgt, SqlString, "prj_mgmt", dtp_main);
                dgv.DataSource = dtp_main;
            }
        }

        //ending with
        private void end_wth(ToolStripButton sender)
        {
            oper_save();
            mt = (ToolStripMenuItem)sender.Owner.Items[0];
            try
            {
                tbxx = (ToolStripTextBox)sender.Owner.Items[1];
                what2 = tbxx.Text;
            }
            catch (Exception erty) { tcxx = (ToolStripComboBox)sender.Owner.Items[1]; what2 = tcxx.Text; }
            mt2 = (ToolStripMenuItem)mt.OwnerItem;
            mt3 = (ToolStripMenuItem)mt2.OwnerItem;
            what1 = mt.Owner.Items[0].Text;

            who = mt3.Text;
            who = who.Remove(who.Length - 3);
            if (who.ToLower() == "start date")
            {
                who = "Sart Date";
            }
            string SqlString = "Select * FROM prj_mgmt WHERE [" + who + "] LIKE '%" + what2 + "'";

            if (Main.Amatrix.mgt == "")
            {
                prjmgmt_dtst.Clear();
                string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                        }
                    }
                }
            }
            else
            {
                dtp_main.Clear();
                dtp_main = basql.Execute(Main.Amatrix.mgt, SqlString, "prj_mgmt", dtp_main);
                dgv.DataSource = dtp_main;
            }
        }

        //find with pieces
        private void piece(ToolStripButton sender)
        {
            oper_save();
            mt = (ToolStripMenuItem)sender.Owner.Items[0];
            try
            {
                tbxx = (ToolStripTextBox)sender.Owner.Items[1];
                what2 = tbxx.Text;
            }
            catch (Exception erty) { tcxx = (ToolStripComboBox)sender.Owner.Items[1]; what2 = tcxx.Text; }
            mt2 = (ToolStripMenuItem)mt.OwnerItem;
            mt3 = (ToolStripMenuItem)mt2.OwnerItem;
            what1 = mt.Owner.Items[0].Text;

            who = mt3.Text;
            who = who.Remove(who.Length - 3);
            if (who.ToLower() == "start date")
            {
                who = "Sart Date";
            }
            string SqlString = "Select * FROM prj_mgmt WHERE [" + who + "] LIKE '%" + what2 + "%'";

            if (Main.Amatrix.mgt == "")
            {
                prjmgmt_dtst.Clear();
                string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                        }
                    }
                }
            }
            else
            {
                dtp_main.Clear();
                dtp_main = basql.Execute(Main.Amatrix.mgt, SqlString, "prj_mgmt", dtp_main);
                dgv.DataSource = dtp_main;
            }
        }
        //dynam query -END-
        //Query Events

        //equal
        private void new_q(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            eqls(b_temp);
        }

        //not equal
        private void tvvbxtnt1_Click(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            nt_eql(b_temp);
        }

        private void btw_event(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            btw(b_temp);
        }

        private void gt_event(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            grt_thn(b_temp);
        }

        private void lss_event(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            lss_thn(b_temp);
        }

        private void stwth_event(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            str_wth(b_temp);
        }

        private void ew_event(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            end_wth(b_temp);
        }

        private void piece_event(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            piece(b_temp);
        }

        private void dbtsumqry_Click(object sender, EventArgs e)
        {
            oper_save(); string SqlString = "Select sum([Serial Number]) [Serial Number], sum([Project Actual Cost]) [Project Actual Cost], sum([Project Actual Revenue]) [Project Actual Revenue] FROM prj_mgmt";
            if (Main.Amatrix.mgt == "")
            {
                prjmgmt_dtst.Clear();
                string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                        }
                    }
                }
            }
            else
            {
                dtp_main.Clear();
                dtp_main = basql.Execute(Main.Amatrix.mgt, SqlString, "prj_mgmt", dtp_main);
                dgv.DataSource = dtp_main;
            }
        }

        private void avgqryjourn_Click(object sender, EventArgs e)
        {
            oper_save(); string SqlString = "Select avg([Serial Number]) [Serial Number], avg([Project Actual Cost]) [Project Actual Cost], avg([Project Actual Revenue]) [Project Actual Revenue] FROM prj_mgmt";
            if (Main.Amatrix.mgt == "")
            {
                prjmgmt_dtst.Clear();
                string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                        }
                    }
                }
            }
            else
            {
                dtp_main.Clear();
                dtp_main = basql.Execute(Main.Amatrix.mgt, SqlString, "prj_mgmt", dtp_main);
                dgv.DataSource = dtp_main;
            }
        }

        //sub-funcs

        private void undoall_Click(object sender, EventArgs e)
        {
        }

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

        private void ct_Click(object sender, EventArgs e)
        {
            try
            {
                s = this.ActiveControl.Text;
                this.ActiveControl.Text = "";
            }
            catch (Exception erty) { }
        }

        private void sall_Click(object sender, EventArgs e)
        {
            dgv.SelectAll();
        }

        private void general_mssg(string text, string cause1)
        {
            err.Visible = true;
            err_inf_1.Text = cause1;
            err_inf_2.Text = text;

            try
            {
                ttp2.Show(text, this, this.Size.Width - 94, ts2.Location.Y - 34, 5000);
            }
            catch (Exception erty) { }

            ttp_del.Start();
        }

        string Last_Query_Used;
        private void gotoitm_ButtonClick(object sender, EventArgs e)
        {
            oper_save();
            try
            {
                int start = 0;
                string SqlString = "SELECT * From prj_mgmt WHERE";
                foreach (DataColumn dgvc in prjmgmt_dtst.prj_mgmt.Columns)
                {
                    if (dgvc.ColumnName != "Completion Status")
                    {
                        if (start == 0)
                        {
                            SqlString = SqlString + " [" + dgvc.ColumnName + "] LIKE '%" + tbxfned.Text + "%' ";
                            start = 1;
                        }
                        else if (start != 0)// && dgvc.ColumnName.Contains("Budget") != true && dgvc.ColumnName.Contains("Cost") != true && dgvc.ColumnName.Contains("Revenue") != true)
                        {
                            SqlString = SqlString + " OR [" + dgvc.ColumnName + "] LIKE '%" + tbxfned.Text + "%'";
                        }
                    }
                }
                Last_Query_Used = SqlString;
                prjmgmt_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                            }
                        }
                    }
                }
                else
                {
                    dr_univ = Quer(SqlString);
                    prjmgmt_dtst.prj_mgmt.Load(dr_univ);
                }
            }
            catch (Exception erty) { }
        }

        private void colreordr_Click(object sender, EventArgs e)
        {
            dgv.AllowUserToOrderColumns = true;
        }

        private void colreordflse_Click(object sender, EventArgs e)
        {
            dgv.AllowUserToOrderColumns = false;
        }

        private void IO_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(IO) == true)
                {
                    acc_journ_sett.Default.IO = false;
                    dgv.ReadOnly = false;
                    re_only.Visible = false;
                }
                if (sender.Equals(O) == true)
                {
                    acc_journ_sett.Default.IO = true;
                    dgv.ReadOnly = true;
                    re_only.Visible = true;
                }
                acc_journ_sett.Default.Save();
            }
            catch (Exception erty) { }
        }

        private void rsdbord_Click(object sender, EventArgs e)
        {

            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            acc_journ_sett.Default.dgvborder = 1;
            acc_journ_sett.Default.Save();
        }

        private void dflt_dgvbord_Click(object sender, EventArgs e)
        {

            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            acc_journ_sett.Default.dgvborder = 0;
            acc_journ_sett.Default.Save();
        }

        private void ascvw_Click(object sender, EventArgs e)
        {
            dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);
        }

        private void descvw_Click(object sender, EventArgs e)
        {
            dgv.Sort(dgv.Columns[0], ListSortDirection.Descending);
        }

        private void Virtual_menu(object sender, EventArgs e)
        {
            if (sender.Equals(fnt_mnu) == true)
            {
                tsc_fnt_sze.Text = dgv.Font.Size.ToString();
            }
        }

        private float ftmp;
        private void Virtual_Combo(object sender, EventArgs e)
        {
            if (sender.Equals(tsc_fnt_sze) == true)
            {
                if (tsc_fnt_sze.Text.ToLower() != "default")
                {
                    try
                    {
                        ftmp = (float)Convert.ToInt32(tsc_fnt_sze.Text);
                        Font fnt = new Font(dgv.Font.FontFamily.GetName(0), ftmp, FontStyle.Regular);
                        dgv.Font = fnt;
                        dgv.AutoResizeRows();
                        dgv.AutoResizeColumns();
                        visual.Default.font = ftmp;
                        Properties.Settings.Default.Save();
                    }
                    catch (Exception erty) { }
                }
                else
                {
                    Font fnt = new Font(dgv.Font.FontFamily.GetName(0), 8.25f, FontStyle.Regular);
                    dgv.Font = fnt;
                    dgv.AutoResizeRows();
                    dgv.AutoResizeColumns();
                    visual.Default.font = 8.25f;
                    Properties.Settings.Default.Save();
                }
            }
            if (sender.Equals(tbxfntlv) == true)
            {
                if (tbxfntlv.Text.ToLower() != "default")
                {
                    try
                    {
                        Font fnt = new Font(tbxfntlv.Text, dgv.Font.Size, FontStyle.Regular);
                        dgv.Font = fnt;
                        dgv.AutoResizeRows();
                        dgv.AutoResizeColumns();
                    }
                    catch (Exception erty) { }
                }
                else
                {
                    Font fnt = new Font("Microsoft Sans Serif", dgv.Font.Size, FontStyle.Regular);
                    dgv.Font = fnt;
                    dgv.AutoResizeRows();
                    dgv.AutoResizeColumns();
                }
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            draw_graph();
        }

        private ToolStripTextBox tstb_byrw;
        private void uptxtdgv_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tstb_byrw = (ToolStripTextBox)sender;
                oper_save(); string SqlString = "Select * FROM prj_mgmt WHERE [Serial Number] = '" + tstb_byrw.Text + "'";
                if (Main.Amatrix.mgt == "")
                {
                    prjmgmt_dtst.Clear();
                    string ConnString = prj_mgmtTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                prjmgmt_dtst.Load(reader, LoadOption.PreserveChanges, "prj_mgmt");
                                dgv.DataSource = prjmgmt_dtst.prj_mgmt;
                            }
                        }
                    }
                }
                else
                {
                    dtp_main.Clear();
                    dtp_main = basql.Execute(Main.Amatrix.mgt, SqlString, "prj_mgmt", dtp_main);
                    dgv.DataSource = dtp_main;
                }
            }
            catch (Exception ery) { }
        }

        private void dgvtxtright_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tstb_byrw = (ToolStripTextBox)sender;
                dgv.CurrentCell = dgv[dgv.CurrentRow.Index, Convert.ToInt32(tstb_byrw.Text)];
            }
            catch (Exception erty) { }
        }

        private void set_quikbox()
        {
            try
            {
                if (Cursor.Position.X - this.Location.X <= this.Size.Width && Cursor.Position.Y - this.Location.Y <= this.Size.Height)
                {
                    zz.Location = new Point((Cursor.Position.X - this.Location.X) - zz.Size.Width / 2, (Cursor.Position.Y - this.Location.Y) - 15);
                }
                else { }
            }
            catch (Exception erty) { }
        }

        private int extern_opn = 0;
        private void zz_yn(bool shw)
        {
            if (extern_opn == 0)
            {
                if (shw == true)
                {
                    try
                    {
                        tmr.Stop();
                    }
                    catch (Exception erty) { }
                    set_quikbox();
                    zz.BringToFront();
                    zz.Visible = true;

                    tmr.Interval = 4500;
                    tmr.Start();
                }
                else if (shw == false)
                {
                    zz.Visible = false;
                }
                else { }
            }
            else
            {
                general_mssg("Some Quick Access Functions Crashed and are Temporarily Unavailable", "Opened from External Application");
            }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            if (QA_cmd.DropDown.Visible == true)
            { }
            else
            {
                if (mini_timelife.Checked == false)
                {
                    zz_yn(false);
                }
            }
        }

        private void remv_zz_Click(object sender, EventArgs e)
        {
            zz_yn(false);
        }

        private void zz_MouseEnter(object sender, EventArgs e)
        {
            //clse_zz.Stop();
            tmr.Interval = 13000;
            tmr.Start();
        }

        private void zz_MouseLeave(object sender, EventArgs e)
        {
            //clse_zz.Start();
            tmr.Interval = 13000;
            tmr.Start();
        }

        private void initializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            init_db();
            tabControl1.SelectTab(0);
        }

        DataGridView dgv2 = new DataGridView();
        DataSet dt_temp = new DataSet();
        ListView lv_temp = new ListView();
        private void draw_graph()
        {
            try
            {
                dt_temp.Clear();
                listView1.Items.Clear();
                lv_temp = listView1;
                dgv2 = new DataGridView();
                bkk_graph.RunWorkerAsync();
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.ToString()); }
        }

        private void bkk_graph_DoWork(object sender, DoWorkEventArgs e)
        {
            do_bkk_LINE();
        }

        private void do_bkk_LINE()
        {
            try
            {
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection cnn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                    SqlCeCommand comm = new SqlCeCommand("SELECT * FROM prj_mgmt WHERE DatePart(yy, [Sart Date]) = "/*DatePart(yy, "*/ + Year.ToString() + /*")*/" OR DatePart(yy, [End Date]) = "/*DatePart(yy, GetDate())*/ + Year.ToString(), cnn);// + "AND [Serial Number] >= 1 AND [Serial Number] <= 100", cnn);
                    SqlCeDataReader dr;
                    cnn.Open();
                    dr = comm.ExecuteReader();
                    dt_temp.Load(dr, LoadOption.PreserveChanges, "prj_mgmt");
                    cnn.Close();
                }
                else
                {
                    SqlConnection cnn = new SqlConnection(Main.Amatrix.mgt);
                    SqlCommand comm = new SqlCommand("SELECT * FROM prj_mgmt WHERE DatePart(yy, [Sart Date]) = "/*DatePart(yy, "*/ + Year.ToString() + /*")*/" OR DatePart(yy, [End Date]) = "/*DatePart(yy, GetDate())*/ + Year.ToString(), cnn);// + "AND [Serial Number] >= 1 AND [Serial Number] <= 100", cnn);
                    SqlDataReader dr;
                    cnn.Open();
                    dr = comm.ExecuteReader();
                    dt_temp.Load(dr, LoadOption.PreserveChanges, "prj_mgmt");
                    cnn.Close();
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.ToString()); }
        }

        private int strt; private int stop; private Color cl = new Color();
        private void bkk_graph_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            grphing_done();
        }

        private void grphing_done()
        {
            lv_temp.Items.Clear();
            prv_yr.Enabled = true;
            nxt_yr.Enabled = true;
            yr_lbl.Text = Year.ToString();
            foreach (DataRow dgvr in dt_temp.Tables[0].Rows)
            {
                lv_temp.Items.Add(dgvr[0].ToString());
                lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[1].ToString(), Color.Black, Color.Lavender, listView1.Font);
                lv_temp.Items[lv_temp.Items.Count - 1].UseItemStyleForSubItems = false;

                DateTime dt = new DateTime();
                try
                {
                    dt = (DateTime)dgvr[3];
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
                if (/*dgvr[3].ToString() == "" || */dt.Year != Year)
                {
                    strt = 1;
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("...", Color.Black, Color.SteelBlue, listView1.Font);
                }
                else if (dgvr[3].ToString().ToLower().Contains("/1/") == true || dgvr[3].ToString().ToLower().Contains("-1-") == true || dgvr[3].ToString().ToLower().Contains("/01/") == true || dgvr[3].ToString().ToLower().Contains("-01-") == true || dgvr[3].ToString().ToLower().Contains("jan") == true && dt.Year == Year)
                {
                    strt = 1;
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[3].ToString().Remove(10), Color.Black, Color.SteelBlue, listView1.Font);
                }
                else if (dgvr[3].ToString().ToLower().Contains("/2/") == true || dgvr[3].ToString().ToLower().Contains("-2-") == true || dgvr[3].ToString().ToLower().Contains("/02/") == true || dgvr[3].ToString().ToLower().Contains("-02-") == true || dgvr[3].ToString().ToLower().Contains("feb") == true && dt.Year == Year)
                {
                    strt = 2;
                    for (int i = 0; i < 1; i++)
                    {
                        lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("");
                    }
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[3].ToString().Remove(10), Color.Black, Color.RoyalBlue, listView1.Font);
                }
                else if (dgvr[3].ToString().ToLower().Contains("/3/") == true || dgvr[3].ToString().ToLower().Contains("-3-") == true || dgvr[3].ToString().ToLower().Contains("/03/") == true || dgvr[3].ToString().ToLower().Contains("-03-") == true || dgvr[3].ToString().ToLower().Contains("mar") == true && dt.Year == Year)
                {
                    strt = 3;
                    for (int i = 0; i < 2; i++)
                    {
                        lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("");
                    }
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[3].ToString().Remove(10), Color.Black, Color.CornflowerBlue, listView1.Font);
                }
                else if (dgvr[3].ToString().ToLower().Contains("/4/") == true || dgvr[3].ToString().ToLower().Contains("-4-") == true || dgvr[3].ToString().ToLower().Contains("/04/") == true || dgvr[3].ToString().ToLower().Contains("-04-") == true || dgvr[3].ToString().ToLower().Contains("apr") == true && dt.Year == Year)
                {
                    strt = 4;
                    for (int i = 0; i < 3; i++)
                    {
                        lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("");
                    }
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[3].ToString().Remove(10), Color.Black, Color.LightSteelBlue, listView1.Font);
                }
                else if (dgvr[3].ToString().ToLower().Contains("/5/") == true || dgvr[3].ToString().ToLower().Contains("-5-") == true || dgvr[3].ToString().ToLower().Contains("/05/") == true || dgvr[3].ToString().ToLower().Contains("-05-") == true || dgvr[3].ToString().ToLower().Contains("may") == true && dt.Year == Year)
                {
                    strt = 5;
                    for (int i = 0; i < 4; i++)
                    {
                        lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("");
                    }
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[3].ToString().Remove(10), Color.Black, Color.LightBlue, listView1.Font);
                }
                else if (dgvr[3].ToString().ToLower().Contains("/6/") == true || dgvr[3].ToString().ToLower().Contains("-6-") == true || dgvr[3].ToString().ToLower().Contains("/06/") == true || dgvr[3].ToString().ToLower().Contains("-06-") == true || dgvr[3].ToString().ToLower().Contains("jun") == true && dt.Year == Year)
                {
                    strt = 6;
                    for (int i = 0; i < 5; i++)
                    {
                        lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("");
                    }
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[3].ToString().Remove(10), Color.Black, Color.Lavender, listView1.Font);
                }
                else if (dgvr[3].ToString().ToLower().Contains("/7/") == true || dgvr[3].ToString().ToLower().Contains("-7-") == true || dgvr[3].ToString().ToLower().Contains("/07/") == true || dgvr[3].ToString().ToLower().Contains("-07-") == true || dgvr[3].ToString().ToLower().Contains("jul") == true && dt.Year == Year)
                {
                    strt = 7;
                    for (int i = 0; i < 6; i++)
                    {
                        lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("");
                    }
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[3].ToString().Remove(10), Color.Black, Color.AliceBlue, listView1.Font);
                }
                else if (dgvr[3].ToString().ToLower().Contains("/8/") == true || dgvr[3].ToString().ToLower().Contains("-8-") == true || dgvr[3].ToString().ToLower().Contains("/08/") == true || dgvr[3].ToString().ToLower().Contains("-08-") == true || dgvr[3].ToString().ToLower().Contains("aug") == true && dt.Year == Year)
                {
                    strt = 8;
                    for (int i = 0; i < 7; i++)
                    {
                        lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("");
                    }
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[3].ToString().Remove(10), Color.Black, Color.MistyRose, listView1.Font);
                }
                else if (dgvr[3].ToString().ToLower().Contains("/9/") == true || dgvr[3].ToString().ToLower().Contains("-9-") == true || dgvr[3].ToString().ToLower().Contains("/09/") == true || dgvr[3].ToString().ToLower().Contains("-09-") == true || dgvr[3].ToString().ToLower().Contains("sep") == true && dt.Year == Year)
                {
                    strt = 9;
                    for (int i = 0; i < 8; i++)
                    {
                        lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("");
                    }
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[3].ToString().Remove(10), Color.Black, Color.Thistle, listView1.Font);
                }
                else if (dgvr[3].ToString().ToLower().Contains("/10/") == true || dgvr[3].ToString().ToLower().Contains("-10-") == true || dgvr[3].ToString().ToLower().Contains("oct") == true && dt.Year == Year)
                {
                    strt = 10;
                    for (int i = 0; i < 9; i++)
                    {
                        lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("");
                    }
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[3].ToString().Remove(10), Color.Black, Color.LightBlue, listView1.Font);
                }
                else if (dgvr[3].ToString().ToLower().Contains("/11/") == true || dgvr[3].ToString().ToLower().Contains("-11-") == true || dgvr[3].ToString().ToLower().Contains("nov") == true && dt.Year == Year)
                {
                    strt = 11;
                    for (int i = 0; i < 10; i++)
                    {
                        lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("");
                    }
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[3].ToString().Remove(10), Color.Black, Color.LightSteelBlue, listView1.Font);
                }
                else if (dgvr[3].ToString().ToLower().Contains("/12/") == true || dgvr[3].ToString().ToLower().Contains("-12-") == true || dgvr[3].ToString().ToLower().Contains("dec") == true && dt.Year == Year)
                {
                    strt = 12;
                    for (int i = 0; i < 11; i++)
                    {
                        lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("");
                    }
                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[3].ToString().Remove(10), Color.Black, Color.SteelBlue, listView1.Font);
                }
                else { strt = 1; }

                DateTime dt2 = new DateTime();
                try
                {
                    dt2 = (DateTime)dgvr[5];
                }
                catch (Exception erty) { }
                if (dt2.Year != Year && dgvr[5].ToString() != "" && dgvr[5] != DBNull.Value)
                {
                    stop = 13;
                }
                else if (dgvr[5].ToString().ToLower().Contains("/1/") == true || dgvr[5].ToString().ToLower().Contains("-1-") == true || dgvr[5].ToString().ToLower().Contains("/01/") == true || dgvr[5].ToString().ToLower().Contains("-01-") == true || dgvr[5].ToString().ToLower().Contains("jan") == true && dt2.Year == Year)
                {
                    stop = 1;
                }
                else if (dgvr[5].ToString().ToLower().Contains("/2/") == true || dgvr[5].ToString().ToLower().Contains("-2-") == true || dgvr[5].ToString().ToLower().Contains("/02/") == true || dgvr[5].ToString().ToLower().Contains("-02-") == true || dgvr[5].ToString().ToLower().Contains("feb") == true && dt2.Year == Year)
                {
                    stop = 2;
                }
                else if (dgvr[5].ToString().ToLower().Contains("/3/") == true || dgvr[5].ToString().ToLower().Contains("-3-") == true || dgvr[5].ToString().ToLower().Contains("/03/") == true || dgvr[5].ToString().ToLower().Contains("-03-") == true || dgvr[5].ToString().ToLower().Contains("mar") == true && dt2.Year == Year)
                {
                    stop = 3;
                }
                else if (dgvr[5].ToString().ToLower().Contains("/4/") == true || dgvr[5].ToString().ToLower().Contains("-4-") == true || dgvr[5].ToString().ToLower().Contains("/04/") == true || dgvr[5].ToString().ToLower().Contains("-04-") == true || dgvr[5].ToString().ToLower().Contains("apr") == true && dt2.Year == Year)
                {
                    stop = 4;
                }
                else if (dgvr[5].ToString().ToLower().Contains("/5/") == true || dgvr[5].ToString().ToLower().Contains("-5-") == true || dgvr[5].ToString().ToLower().Contains("/05/") == true || dgvr[5].ToString().ToLower().Contains("-05-") == true || dgvr[5].ToString().ToLower().Contains("may") == true && dt2.Year == Year)
                {
                    stop = 5;
                }
                else if (dgvr[5].ToString().ToLower().Contains("/6/") == true || dgvr[5].ToString().ToLower().Contains("-6-") == true || dgvr[5].ToString().ToLower().Contains("/06/") == true || dgvr[5].ToString().ToLower().Contains("-06-") == true || dgvr[5].ToString().ToLower().Contains("jun") == true && dt2.Year == Year)
                {
                    stop = 6;
                }
                else if (dgvr[5].ToString().ToLower().Contains("/7/") == true || dgvr[5].ToString().ToLower().Contains("-7-") == true || dgvr[5].ToString().ToLower().Contains("/07/") == true || dgvr[5].ToString().ToLower().Contains("-07-") == true || dgvr[5].ToString().ToLower().Contains("jul") == true && dt2.Year == Year)
                {
                    stop = 7;
                }
                else if (dgvr[5].ToString().ToLower().Contains("/8/") == true || dgvr[5].ToString().ToLower().Contains("-8-") == true || dgvr[5].ToString().ToLower().Contains("/08/") == true || dgvr[5].ToString().ToLower().Contains("-08-") == true || dgvr[5].ToString().ToLower().Contains("aug") == true && dt2.Year == Year)
                {
                    stop = 8;
                }
                else if (dgvr[5].ToString().ToLower().Contains("/9/") == true || dgvr[5].ToString().ToLower().Contains("-9-") == true || dgvr[5].ToString().ToLower().Contains("/09/") == true || dgvr[5].ToString().ToLower().Contains("-09-") == true || dgvr[5].ToString().ToLower().Contains("sep") == true && dt2.Year == Year)
                {
                    stop = 9;
                }
                else if (dgvr[5].ToString().ToLower().Contains("/10/") == true || dgvr[5].ToString().ToLower().Contains("-10-") == true || dgvr[5].ToString().ToLower().Contains("oct") == true && dt2.Year == Year)
                {
                    stop = 10;
                }
                else if (dgvr[5].ToString().ToLower().Contains("/11/") == true || dgvr[5].ToString().ToLower().Contains("-11-") == true || dgvr[5].ToString().ToLower().Contains("nov") == true && dt2.Year == Year)
                {
                    stop = 11;
                }
                else if (dgvr[5].ToString().ToLower().Contains("/12/") == true || dgvr[5].ToString().ToLower().Contains("-12-") == true || dgvr[5].ToString().ToLower().Contains("dec") == true && dt2.Year == Year)
                {
                    stop = 12;
                }
                else { stop = strt; }

                try
                {
                    try
                    {
                        cl = new Color();
                        cl = Color.WhiteSmoke;
                        cl = Color.FromArgb(lv_temp.Items[lv_temp.Items.Count - 1].SubItems[strt + 1].BackColor.R + 30, lv_temp.Items[lv_temp.Items.Count - 1].SubItems[strt + 1].BackColor.G + 30, lv_temp.Items[lv_temp.Items.Count - 1].SubItems[strt + 1].BackColor.B + 30);
                    }
                    catch (Exception wtr)
                    {
                        try
                        {
                            cl = Color.FromArgb(lv_temp.Items[lv_temp.Items.Count - 1].SubItems[strt + 1].BackColor.R - 30, lv_temp.Items[lv_temp.Items.Count - 1].SubItems[strt + 1].BackColor.G - 30, lv_temp.Items[lv_temp.Items.Count - 1].SubItems[strt + 1].BackColor.B - 30);
                        }
                        catch (Exception erty) { }
                    }
                    if (stop > strt)
                    {
                        for (int i = strt + 1; i <= stop; i++)
                        {
                            if (i != 13)
                            {
                                if (i != stop)
                                {
                                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add("", Color.Black, cl, listView1.Font);
                                }
                                if (i == stop)
                                {
                                    lv_temp.Items[lv_temp.Items.Count - 1].SubItems.Add(dgvr[5].ToString().Remove(10), Color.Black, lv_temp.Items[listView1.Items.Count - 1].SubItems[strt + 1].BackColor, listView1.Font);
                                }
                            }
                            else
                            {
                                lv_temp.Items[lv_temp.Items.Count - 1].SubItems[13].Text = "...";
                            }
                        }
                    }
                }
                catch (Exception erty) { }
            }
            listView1 = lv_temp;
        }

        private void yr_tl(object sender, EventArgs e)
        {
            if (sender.Equals(nxt_yr) == true)
            {
                Year = Year + 1;
                draw_graph();
            }
            else if (sender.Equals(prv_yr) == true)
            {
                Year = Year - 1;
                draw_graph();
            }
        }

        private void connectToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy lg = new loggy();
            lg.Show();
        }

        //Query Quik Access

        //Equal to
        /*private void eqlmnuopn_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                tss2.ShowDropDown();
            }
            catch (Exception erty) { }
        }

        //Not Equal to
        //private ToolStripItem tsmi_temp;
        private void nteqlshrt_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[(dgv.CurrentCell.ColumnIndex + 1) - 19];
                ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[2];
                tss2.ShowDropDown();
            }
            catch (Exception erty) { }
        }

        //greater than
        private void grtrthn_Click(object sender, EventArgs e)
        {
                try
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[4];
                    tss2.ShowDropDown();
                }
                catch (Exception erty) { }
        }

        //less than
        private void lessthn_Click(object sender, EventArgs e)
        {
                try
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[5];
                    tss2.ShowDropDown();
                }
                catch (Exception erty) { }
        }

        //Between
        private void btween_Click(object sender, EventArgs e)
        {
                try
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[3];
                    tss2.ShowDropDown();
                }
                catch (Exception erty) { }
        }

        //Starting With
        private void startwid_Click(object sender, EventArgs e)
        {
                try
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[3];
                    tss2.ShowDropDown();
                }
                catch (Exception erty) { }
        }

        private void QA_endwth_Click(object sender, EventArgs e)
        {
                try
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[4];
                    tss2.ShowDropDown();
                }
                catch (Exception erty) { }
        }

        private void QA_bd_Click(object sender, EventArgs e)
        {
                try
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[4];
                    tss2.ShowDropDown();
                }
                catch (Exception erty) { }
        }

        private void QA_ad_Click(object sender, EventArgs e)
        {
                try
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[5];
                    tss2.ShowDropDown();
                }
                catch (Exception erty) { }
        }*/

        //QA open

        private void QA_cmd_DropDownOpened(object sender, EventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 0 || dgv.CurrentCell.ColumnIndex >= 9 && dgv.CurrentCell.ColumnIndex <= 12)
            {//numerical
                QA_grth.Enabled = true;
                QA_lssth.Enabled = true;
                QA_btw.Enabled = true;

                QA_endwth.Enabled = false;
                QA_stwth.Enabled = false;
                QA_ad.Enabled = false;
                QA_bd.Enabled = false;
            }
            else if (dgv.CurrentCell.ColumnIndex >= 4 && dgv.CurrentCell.ColumnIndex <= 7)
            {//date
                QA_grth.Enabled = false;
                QA_lssth.Enabled = false;
                QA_btw.Enabled = true;

                QA_endwth.Enabled = false;
                QA_stwth.Enabled = false;
                QA_ad.Enabled = true;
                QA_bd.Enabled = true;
            }
            else if (dgv.CurrentCell.ColumnIndex >= 1 && dgv.CurrentCell.ColumnIndex <= 3 || dgv.CurrentCell.ColumnIndex == 8)
            {//alpha
                QA_grth.Enabled = false;
                QA_lssth.Enabled = false;
                QA_btw.Enabled = false;

                QA_endwth.Enabled = true;
                QA_stwth.Enabled = true;
                QA_ad.Enabled = false;
                QA_bd.Enabled = false;
            }
            else if (dgv.CurrentCell.ColumnIndex >= 15 && dgv.CurrentCell.ColumnIndex <= 17)
            {//basic
                QA_grth.Enabled = false;
                QA_lssth.Enabled = false;
                QA_btw.Enabled = false;

                QA_endwth.Enabled = false;
                QA_stwth.Enabled = false;
                QA_ad.Enabled = false;
                QA_bd.Enabled = false;
            }
        }

        ToolStripTextBox tbx_tstemp;
        TextBox tbx_temp; ComboBox cbx_tempcol;
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

        private void dynayes_Click(object sender, EventArgs e)
        {
            acc_journ_sett.Default.dynam_jrn = true;
            acc_journ_sett.Default.Save();
        }

        private void dynano_Click(object sender, EventArgs e)
        {
            acc_journ_sett.Default.dynam_jrn = false;
            acc_journ_sett.Default.Save();
        }

        private void abtmnu_Click(object sender, EventArgs e)
        {
            app_abt bt = new app_abt();
            bt.descr(this.Text);
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.tx(this.Name);
        }

        private void edt_Click(object sender, EventArgs e)
        {
            acc_rep_wiz rep = new acc_rep_wiz();
            rep.Show();
        }

        private void pnt_Click(object sender, EventArgs e)
        {
            PrintDataGrid.PrintDGV.Print_DataGridView(dgv);
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

        //sexyness
        private int sel;
        private void tvtxt20_KeyUp(object sender, KeyEventArgs e)
        {
            tstt = (ToolStripTextBox)sender;
            if (e.KeyValue == 8) { }
            else if (tstt.Text.Length == 2 || tstt.Text.Length == 6 && e.KeyValue != 8)
            {
                sel = tstt.SelectionStart + 1;
                tstt.Text = tstt.Text + "-";
                tstt.SelectionStart = sel;
            }

            if (tstt.Text.Length > 9)
            {
                tstt.BackColor = Color.Orange;
            }
            else if (tstt.Text.Length <= 9)
            {
                tstt.BackColor = Color.White;
            }
        }

        private ToolStripTextBox tstt;
        /*private void tvtxt1_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                tstt = (ToolStripTextBox)sender;
                tstt.BackColor = Color.Lavender;
            }
            catch (Exception erty) { }
        }

        private void tvtxt1_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                tstt = (ToolStripTextBox)sender;
                tstt.BackColor = Color.White;
            }
            catch (Exception erty) { }
        }*/

        private void tvtxt20_Click(object sender, EventArgs e)
        {
            tstt = (ToolStripTextBox)sender;
            if (tstt.Text == "Enter a Date (eg. 01-jan-91)" || tstt.Text == "Enter First Date (eg. 01-Jan-91)" || tstt.Text == "Show Invoice Of" || tstt.Text == "Enter Last Date (eg. 02-Feb-92)" || tstt.Text == "Enter a Value" || tstt.Text == "Enter a Value(Piece)" || tstt.Text == "Enter Lower Value" || tstt.Text == "Enter Greater Value")
            {
                tstt.SelectAll();
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

        private bool loc_chnge = false;
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (cn.Text.StartsWith("AM-PRJ-") == true)
            {
                if (DialogResult.Yes == MessageBox.Show("Would You Like to Continue this Operation? Altering Project Pin Numbers May Cause Data Instability.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    cn.Text = "AM-PRJ-" + DateTime.Now.ToString() + "-ID";
                }
            }
            else
            { cn.Text = "AM-PRJ-" + DateTime.Now.ToString() + "-ID"; }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Showalljourn();
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

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            prj_tasks_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prj_mgmt_tsks WHERE [FORPRJ] = '" + cn.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prj_tasks_dtst.prj_mgmt_tsks.Load(dr);
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prj_mgmt_tsks WHERE [FORPRJ] = '" + cn.Text + "'");
                prj_tasks_dtst.prj_mgmt_tsks.Load(dr_univ);
            }
            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            if (textBox4.Text == "")
            {
                textBox4.Text = "0";
            }
            if (textBox5.Text == "")
            {
                textBox5.Text = "0";
            }
            if (cn.Text == "")
            {
                toolStripButton6.Enabled = false;
            }
            else
            { toolStripButton6.Enabled = true; }
        }

        private void dgv3_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dgv3[9, e.RowIndex].Value = DateTime.Now.ToString() + DateTime.Now.Ticks.ToString();
            dgv3[10, e.RowIndex].Value = cn.Text;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            saveprj();
        }

        private void saveprj()
        {
            //prjmgmttsksBindingSource.EndEdit();
            if (Main.Amatrix.mgt == "")
            {
                prj_mgmt_tsksTableAdapter.Update(prj_tasks_dtst);
            }
            else
            {
                asql.Save(prj_tasks_dtst.prj_mgmt_tsks, "prj_mgmt_tsks", Main.Amatrix.mgt);
            } 
            Main.Amatrix.ascl.broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><typ>w</typ><val>0</val><app>" + this.Name + "</app><par>[" + toolStrip1.Name + "]</par><con>Sync_bttn</con>");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            prj_tasks_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prj_mgmt_tsks", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prj_tasks_dtst.prj_mgmt_tsks.Load(dr);
                conn.Close();
            }
            else
            {
                dr_univ = Quer("SELECT * FROM prj_mgmt_tsks");
                prj_tasks_dtst.prj_mgmt_tsks.Load(dr_univ);
            }
        }

        private void dgv3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                try
                {
                    prjmgmttsksBindingSource.EndEdit();
                    saveprj();
                    mgmt_prj_Employees_PROJ prm = new mgmt_prj_Employees_PROJ();
                    prm.tx(dgv3[0, e.RowIndex].Value.ToString(), cn.Text);
                    prm.Show();
                }
                catch (Exception erty) { }
            }
        }

        double d, d2, d3, d4;
        string s;
        private void BAL_CH(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            if (textBox4.Text == "")
            {
                textBox4.Text = "0";
            }
            if (textBox5.Text == "")
            {
                textBox5.Text = "0";
            }
            //tb5-tb2
            try
            {
                d = Convert.ToDouble(textBox5.Text);
                d2 = Convert.ToDouble(textBox2.Text);
                d = d - d2;
                label17.Text = "Actual Balance : " + d.ToString();
            }
            catch (Exception erty)
            {
                label17.Text = "Actual Balance : N.A.";
            }
            try
            {
                d3 = Convert.ToDouble(textBox4.Text);
                d4 = Convert.ToDouble(textBox1.Text);
                d3 = d3 - d4;
                label18.Text = "Budget Balance : " + d3.ToString();
            }
            catch (Exception erty) 
            {
                label18.Text = "Budget Balance : N.A.";
            }
            try
            {
                if (d3 < d)
                {
                    label19.Text = "Profited";
                }
                else if (d < d3)
                {
                    label19.Text = "Not Profited";
                }
                else { label19.Text = "Budgets-Actuals Balanced"; }
            }
            catch (Exception erty)
            {
                label19.Text = "Status N.A.";
            }
        }

        private void toolStripButton71_Click(object sender, EventArgs e)
        {
            try
            {
                mgmt_prj_Employees_PROJ prm = new mgmt_prj_Employees_PROJ();
                prm.tx(dgv3[0, dgv3.CurrentRow.Index].Value.ToString(), cn.Text);
                prm.Show();
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Unable to Open Employees For Tasks, Select a Row in the Tasks Section and try Again."); }
        }

        //enter textbox
        //ToolStripTextBox tbx_tstemp;
        //TextBox tbx_temp;
        //Color cl = new Color(); ComboBox cbx_tempcol;
        private void tvtxt1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void tvtxt1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label14.Text = trackBar1.Value.ToString() + "%";
        }

        //new
        string poop, poop2;
        private void bindingNavigatorDeleteItem_MouseDown(object sender, MouseEventArgs e)
        {
            poop = cn.Text;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("DELETE FROM prj_mgmt_employees WHERE [PRJSER] = '" + poop + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd2 = new SqlCeCommand("DELETE FROM prj_mgmt_tsks WHERE [FORPRJ] = '" + poop + "'", conn2);
                conn2.Open();
                cmd2.ExecuteNonQuery();
                conn2.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("DELETE FROM prj_mgmt_employees WHERE [PRJSER] = '" + poop + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                SqlConnection conn2 = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd2 = new SqlCommand("DELETE FROM prj_mgmt_tsks WHERE [FORPRJ] = '" + poop + "'", conn2);
                conn2.Open();
                cmd2.ExecuteNonQuery();
                conn2.Close();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("DELETE FROM prj_mgmt_employees WHERE [PRJSER] = '" + poop + "' AND [FORPRJ] = '" + poop2 + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                dgv3.Rows.Remove(dgv3.CurrentRow);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("DELETE FROM prj_mgmt_employees WHERE [PRJSER] = '" + poop + "' AND [FORPRJ] = '" + poop2 + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                dgv3.Rows.Remove(dgv3.CurrentRow);
            }
            saveprj();
        }

        private void toolStripButton5_MouseDown(object sender, MouseEventArgs e)
        {
            poop = cn.Text;
            poop2 = dgv3[0, dgv3.CurrentRow.Index].Value.ToString();
        }
        //new
        DataTable dtp = new DataTable();
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            prod_box.Visible = true;
            load_prods();
        }

        private void load_prods()
        {
            dtp.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp.Load(dr);
                dataGridView2.DataSource = dtp;
                conn.Close();
            }
            else
            {
                dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Prod_mgmt", "Prod_mgmt", dtp);
                dataGridView2.DataSource = dtp;
            }
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button19_Click(object sender, EventArgs e)
        {
            load_prods();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO prj_mgmt_materials Values('MAT-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString() + "','" + dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString() + "', '" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "','No','','','','" + cn.Text + "')";
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand(sql, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                conn.Close();

                //prodbulk
                SqlCeConnection conn3 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd3 = new SqlCeCommand("UPDATE prod_bulk SET [Sold To] = 'US@Project(" + cn.Text + ")',[On The (Date)] = getdate(), [State] = 'Sold' WHERE [Notes/Information] = '" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "'", conn3);
                conn3.Open();
                cmd3.ExecuteNonQuery();
                conn3.Close();

                /*SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                conn2.Open();
                SqlCeCommand cmd2 = new SqlCeCommand("UPDATE prod_bulk SET [On The (Date)] = " + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + " WHERE [Notes/Information] = '" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "'", conn2);
                cmd2.ExecuteNonQuery();
                conn2.Close();*/
            }
            else
            {
                DataTable dtpy = new DataTable();
                basql.Execute(Main.Amatrix.mgt, sql, "prj_mgmt_materials", dtpy);
                dtpy.Clear(); dtpy.Dispose();

                //prodbulk
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("UPDATE prod_bulk SET [Sold To] = 'US@Project(" + cn.Text + ")',[On The (Date)] = getdate(), [State] = 'Sold' WHERE [Notes/Information] = '" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

               /* SqlConnection conn2 = new SqlConnection(Main.Amatrix.mgt);
                conn2.Open();
                SqlCommand cmd2 = new SqlCommand("UPDATE prod_bulk SET [On The (Date)] = " + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + " WHERE [Notes/Information] = '" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString() + "'", conn2);
                cmd2.ExecuteNonQuery();
                conn2.Close();*/
            }
            load_materials();
            prod_box.Visible = false;
        }

        private DataTable dtp_mater = new DataTable();
        private void load_materials()
        {
            dtp_mater.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM prj_mgmt_materials WHERE ForPRJ = '" + cn.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp_mater.Load(dr);
            }
            else
            {
                dtp_mater = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM prj_mgmt_materials WHERE ForPRJ = '" + cn.Text + "'", "prj_mgmt_materials", dtp_mater);
            }
            dgv_prods.DataSource = dtp_mater;

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                load_materials();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                //old
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                    conn.Open();
                    SqlCeCommand cmd = new SqlCeCommand("DELETE FROM prj_mgmt_materials WHERE [Key] = '" + dgv_prods.CurrentRow.Cells[0].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();

                    SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                    conn2.Open();
                    SqlCeCommand cmd2 = new SqlCeCommand("UPDATE prod_bulk SET [Sold To] = NULL, [State] = 'Resent-Back From[" + cn.Text + "]' WHERE [Notes/Information] = '" + dgv_prods[1, dgv_prods.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString() + "'", conn2);
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM prj_mgmt_materials WHERE [Key] = '" + dgv_prods.CurrentRow.Cells[0].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();

                    SqlConnection conn2 = new SqlConnection(Main.Amatrix.mgt);
                    conn2.Open();
                    SqlCommand cmd2 = new SqlCommand("UPDATE prod_bulk SET [Sold To] = NULL, [State] = 'Resent-Back From[" + cn.Text + "]' WHERE [Notes/Information] = '" + dgv_prods[1, dgv_prods.CurrentRow.Index].Value.ToString() + "' AND [Reference Number] = '" + dgv_prods[2, dgv_prods.CurrentRow.Index].Value.ToString() + "'", conn2);
                    cmd2.ExecuteNonQuery();
                }
            }
            catch (Exception erty) { }
            dgv_prods.Rows.Remove(dgv_prods.CurrentRow);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                dtp = new DataTable();
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt WHERE [Product Name] LIKE '%" + textBox3.Text + "%'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp.Load(dr);
                dataGridView2.DataSource = dtp;
                conn.Close();
            }
            else
            {
                dtp = new DataTable();
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Prod_mgmt WHERE [Product Name] LIKE '%" + textBox3.Text + "%'", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dtp.Load(dr);
                dataGridView2.DataSource = dtp;
                conn.Close();
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            try
            {
                if (label20.Text != "Yes")
                {
                    if (DialogResult.Yes == MessageBox.Show("Continue Operation?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        label20.Text = "Yes";
                        try
                        {
                            mgmt_Linkto_acc ma = new mgmt_Linkto_acc();
                            ma.tx("Project dr. cr. [" + cn.Text + "]", "Project dr. cr.", cn.Text, "project managment", Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox5.Text));
                        }
                        catch (Exception erty) { }
                        oper_save();
                        Main.Amatrix.ascl.broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><typ>w</typ><val>0</val><app>" + this.Name + "</app><par>[" + toolStrip1.Name + "]</par><con>Sync_bttn</con>");
                    }
                }
                else
                {
                    MessageBox.Show("You May Not Create a Journal Entry for a Project Marked as Final. If one Was Not Created You may Created Manually.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Entries Missing Could not continue. (Make Sure Financial Information is Filled Out and that You have a PIN Number)"); }
        }

        private void label20_TextChanged(object sender, EventArgs e)
        {
            if (label20.Text == "")
            {
                label20.Text = "No";
            }
        }

        private void bindingNavigatorPositionItem_TextChanged_1(object sender, EventArgs e)
        {
            if (bindingNavigatorPositionItem.Text == "0")
            {
                panel2.Enabled = false;
            }
            else
            {
                panel2.Enabled = true;
            }
        }

        private void dgv3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        //new
        int whereami = 0;
        private Thread Sync_th;
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
            prjmgmt_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                prjmgmt_dtst.prj_mgmt.Load(dr);
                conn.Close();
            }
            else
            {
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand(Last_Query_Used, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                prjmgmt_dtst.prj_mgmt.Load(dr);
                conn.Close();
            }
            load_prods();
            load_materials();
            prjmgmtBindingSource.Position = whereami - 1;
            this.Text = this.Text.Replace(" [Synchronizing]", "");
            this.Enabled = true;
            Sync_bttn.BackColor = Color.Transparent;
        }

        //new
        /*Control c;
        private void serv_keyup(object sender, KeyEventArgs e)
        {
            c = (Control)sender;

            string s = "abcdefghijklmnopqrst";

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
            }
            Main.Amatrix.ascl.broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><val>" + c.Text + "</val><app>" + this.Name + "</app><con>" + c.Name + "</con><typ>w</typ><par>" + par + "</par><ndx>" + bindingNavigatorPositionItem.Text + "</ndx>");
        }*/
    }
}
