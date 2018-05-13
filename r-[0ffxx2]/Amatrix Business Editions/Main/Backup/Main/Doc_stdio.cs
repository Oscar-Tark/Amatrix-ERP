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
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Main
{
    public partial class Doc_stdio : Form
    {
        private ArrayList al_pge = new ArrayList();
        private ArrayList al_lbl = new ArrayList();
        ArrayList ak_pge_itms = new ArrayList();

        ArrayList ar_click = new ArrayList();
        ArrayList ar_MouseDU = new ArrayList();

        GUI_TYPES.TEMPLATE_GUI_TYPES tgp = new GUI_TYPES.TEMPLATE_GUI_TYPES();

        public Doc_stdio()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.amdsicnico;
            this.Disposed += new EventHandler(Doc_stdio_Disposed);
            this.FormClosing += new FormClosingEventHandler(Doc_stdio_FormClosing);
            InitializeComponent(); 
           /* try
            { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text); pwd.Owner = this; }
            catch (Exception erty) { }*/
            al_pge.Add(pnl1);
            al_lbl.Add(label2);
            init();
        }

        bool changed = true;
        void Doc_stdio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changed == true && svebtn.Enabled == true)
            {
                e.Cancel = true; tmeclse.Stop();
                if (DialogResult.Yes == MessageBox.Show("Save Changes to the Current Document?", "Ducument Studio", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    EventArgs ew = new EventArgs();
                    svebtn_ButtonClick(this, ew);
                }
                else
                {
                    changed = false;
                    tmeclse.Start();
                }
            }
            else
            {
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

        void Doc_stdio_Disposed(object sender, EventArgs e)
        {
            dtp_logs.Clear(); dtp_prod_info.Clear();
            dtp_logs.Dispose(); dtp_prod_info.Dispose();
            this.FormClosing -= Doc_stdio_FormClosing;
            delitm.Click -= rmv_Click;
            toolStripButton10.Click -= nw_pge_Click;
            toolStripButton9.Click -= bringToFrontToolStripMenuItem_Click;
            toolStripButton8.Click -= sendToBackToolStripMenuItem_Click;
            bringToFrontToolStripMenuItem.Click -= bringToFrontToolStripMenuItem_Click;
            sendToBackToolStripMenuItem.Click -= sendToBackToolStripMenuItem_Click;
            printPreviewToolStripMenuItem.Click -= printPreviewToolStripMenuItem_Click;
            clsemn.Click -= clsejourn_ButtonClick;
            restr.Click -= rstrt2_Click;
            this.toolStripButton7.Click -= toolStripButton7_Click;
            this.pnl_journvw.MouseEnter -= this.pnl_journvw_MouseEnter;
            this.trackBar2.ValueChanged -= this.trackBar1_ValueChanged;
            this.trackBar1.ValueChanged -= this.trackBar1_ValueChanged;
            this.label2.MouseLeave -= this.label2_MouseLeave;
            this.label2.MouseEnter -= this.label2_MouseEnter;
            this.pnl1.Click -= this.pnl_Click;
            this.pnl1.MouseEnter -= this.pnl_MouseEnter;
            this.pasteToolStripMenuItem2.Click -= this.pster_Click;
            this.itm_txt.TextChanged -= this.itm_txt_TextChanged;
            this.lblcrt_btn.Click -= this.lblcrt_btn_Click;
            this.new_pic.Click -= this.lblcrt_btn_Click;
            this.txt_entr.Click -= this.lblcrt_btn_Click;
            this.chkbx_crt.Click -= this.lblcrt_btn_Click;
            this.dtfld.Click -= this.lblcrt_btn_Click;
            this.dtetme.Click -= this.lblcrt_btn_Click;
            this.Colos_drdwn.DropDownOpening -= this.Colos_drdwn_DropDownOpening;
            this.toolStripComboBox5.TextChanged -= this.colos_ch;
            this.toolStripComboBox3.TextChanged -= this.colos_ch;
            this.toolStripComboBox1.TextChanged -= this.Fntch;
            this.toolStripComboBox2.TextChanged -= this.Fntch;
            this.toolStripButton2.Click -= this.Fntch;
            this.toolStripButton3.Click -= this.Fntch;
            this.toolStripButton5.Click -= this.Fntch;
            this.pnt.Click -= this.printPreviewToolStripMenuItem_Click;
            this.copyToolStripMenuItem2.Click -= this.cpy_Click;
            this.cutToolStripMenuItem2.Click -= this.ct_Click;
            this.delitm.Click -= this.rmv_Click;
            this.nw_pge.Click -= this.nw_pge_Click;
            this.nwjrn.Click -= this.nwjrn_Click;
            this.toolStripButton6.Click -= this.toolStripButton6_Click;
            this.svebtn.ButtonClick -= this.svebtn_ButtonClick;
            this.saveAsToolStripMenuItem.Click -= this.svebtn_ButtonClick;
            this.clsejourn.MouseLeave -= this.clse_MouseLeave;
            this.clsejourn.ButtonClick -= this.clsejourn_ButtonClick;
            this.clsejourn.MouseEnter -= this.clse_MouseEnter;
            this.rstrt2.Click -= this.rstrt2_Click;
            this.connlbl.MouseEnter -= this.connlbl_MouseEnter;
            this.connlbl.MouseLeave -= this.connlbl_MouseLeave;
            this.connlbl.Click -= this.connlbl_Click;
            this.newToolStripMenuItem.Click -= this.nwjrn_Click;
            this.openToolStripMenuItem1.Click -= this.toolStripButton6_Click;
            this.saveToolStripMenuItem.Click -= this.svebtn_ButtonClick;
            this.toolStripMenuItem1.Click -= this.printPreviewToolStripMenuItem_Click;
            this.copyToolStripMenuItem.Click -= this.cpy_Click;
            this.cutToolStripMenuItem.Click -= this.ct_Click;
            this.pasteToolStripMenuItem.Click -= this.pster_Click;
            this.contentsToolStripMenuItem.Click -= this.contentsToolStripMenuItem_Click;
            this.aboutToolStripMenuItem.Click -= this.aboutToolStripMenuItem_Click;
            this.pages.DropDownItemClicked -= this.pages_DropDownItemClicked;
            this.cms.Opening -= this.cms_Opening;
            this.selectAnImageToolStripMenuItem.Click -= this.selectAnImageToolStripMenuItem_Click;
            this.copyToolStripMenuItem1.Click -= this.cpy_Click;
            this.cutToolStripMenuItem1.Click -= this.ct_Click;
            this.pasteToolStripMenuItem1.Click -= this.pster_Click;
            this.rmv.Click -= this.rmv_Click;
            this.drag.Tick -= this.drag_Tick;
            this.sfd.FileOk -= this.ofd_FileOk;
            this.ofd.FileOk -= this.ofd_FileOk_1;
            this.pge.Tick -= this.pge_Tick;
            this.cb_usr.CheckedChanged -= this.cb_usr_CheckedChanged;
            this.decjourn.Tick -= this.decjourn_Tick;
            this.p_doc.PrintPage -= this.p_doc_PrintPage;
            this.ofd_img.FileOk -= this.ofd_img_FileOk;
            this.tmeclse.Tick -= this.tmeclse_Tick;
            this.moveToAnotherPageToolStripMenuItem.Click -= this.moveToAnotherPageToolStripMenuItem_Click;
            this.imageToolStripMenuItem.Click -= this.selectAnImageToolStripMenuItem_Click;
            this.changePageOfSelectedItemToolStripMenuItem.Click -= this.moveToAnotherPageToolStripMenuItem_Click;
            this.Deactivate -= this.Doc_stdio_Deactivate;
            this.Load -= this.Doc_stdio_Load;
            this.Activated -= this.Doc_stdio_Activated;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void init()
        {
            this.Opacity = Properties.Settings.Default.opacity;

            load_fonts();
            load_colors();
        }

        private void load_fonts()
        {
            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                toolStripComboBox1.Items.Add(font.Name);
                comboBox1.Items.Add(font.Name);
            }
        }

        private void load_colors()
        {
            ArrayList ColorList = new ArrayList();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static |
                                          BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                toolStripComboBox5.Items.Add(c.Name);
                toolStripComboBox3.Items.Add(c.Name);
                comboBox3.Items.Add(c.Name);
                comboBox4.Items.Add(c.Name);
            }
        }

        public void tx(string template)
        {
            //choose_templ(template);
            this.Show();
        }

        public void tx2(string template_)
        {
            cb_usr.Checked = true; cb_usr.Enabled = false;
            svebtn.Enabled = false; saveToolStripMenuItem.Enabled = false;
            moveToAnotherPageToolStripMenuItem.Enabled = false;
            cutToolStripMenuItem1.Enabled = false;
            /*cms.Enabled = false;*/ cms_pnl.Enabled = false;
            itm_txt.Enabled = true;
            templ = template_;
            a.Read(Environment.CurrentDirectory + "\\Templates\\Sales Order Template.afd");
            open_out(Environment.CurrentDirectory + "\\Templates\\Sales Order Template.afd", false);
        }

        private delegate void d_CPD();
        private Thread th_CPD;
        public void set_P(DataGridView Data_, String Order_Number, String Date_Of_Order, String logs)
        {
            Data = Data_;
            OrderNumber = Order_Number;
            DateofOrder = Date_Of_Order;
            Supplier = logs;
            th_stsrt();
        }

        private void th_stsrt()
        {
            th_CPD = new Thread(new ThreadStart(del_strt));
            th_CPD.IsBackground = true;
            th_CPD.SetApartmentState(ApartmentState.MTA);
            th_CPD.Priority = ThreadPriority.AboveNormal;
            th_CPD.Start();
        }

        private void del_strt()
        {
            this.Invoke(new d_CPD(Compile_Print_Data));
        }

        DataGridView Data = new DataGridView();
        string templ; Base_ASQL.BASQL basql = new Base_ASQL.BASQL(); Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        DataTable dtp_logs = new DataTable(); DataTable dtp_prod_info = new DataTable(); double QTY_TOT = 0; double QTY; double TOT = 0;
        string OrderNumber, Buyer_, DateofOrder, Supplier;// Ship_Date, ShippingMethod, PromsArrivalDate;
        double Price_TOT = 0; double Price_TAX = 0; double Price_CURR; double Tax_CURR; double DIsC_TOT = 0; double Discnt;
        public void Compile_Print_Data(/*DataGridView Data, String Order_Number, String Date_Of_Order, String logs*/)// String Ship_Date_, String Shipping_Method, String Proms_Arrival_Date)
        {
            /*OrderNumber = Order_Number;
            DateofOrder = Date_Of_Order;
            Supplier = logs;*/
            //bkk_nfo.RunWorkerAsync();
            try
            {
                if (/*logs*/Supplier != null)
                {
                    dtp_logs.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Logs_mgmt WHERE [Logistical ID Batch] = '" + Supplier/*logs*/ + "'", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp_logs.Load(dr);
                        conn.Close();
                    }
                    else
                    {
                        dtp_logs = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Logs_mgmt WHERE [Logistical ID Batch] = '" + /*logs*/Supplier + "'", "Logs_mgmt", dtp_logs);
                    }
                }

                pnl_tmt = (Panel)al_pge[0];
                lblcrt(false);
                drag.Stop();
                cntr_temp.Text = "Order Number(Ref No.) : " + OrderNumber;//.
                cntr_temp.Location = new Point(350, 120);

                lblcrt(false);
                drag.Stop();
                cntr_temp.Text = "Date : " + DateofOrder;//.
                cntr_temp.Location = new Point(350, 133);

                txtldcrt(false);
                drag.Stop();
                try
                {
                    cntr_temp.Text = dtp_logs.Rows[0].ItemArray[1].ToString();
                }
                catch (Exception erty)
                {
                    cntr_temp.Text = "Enter Purchase From Info Here...";
                }
                cntr_temp.Size = new Size(150, 90);
                cntr_temp.Location = new Point(157, 235);
                txt_border = (TextBox)cntr_temp;
                txt_border.BorderStyle = BorderStyle.None;
                //cntr_temp.Text = cntr_temp.Text + "\n" + "Dispatch Date: " + dtp_logs.Rows[0].ItemArray[5].ToString() + "\n" + "Delivery Date: ";

                txtldcrt(false);
                drag.Stop();
                try
                {
                    cntr_temp.Text = dtp_logs.Rows[0].ItemArray[2].ToString();
                }
                catch (Exception erty) { cntr_temp.Text = "Enter Delivery To Info Here..."; }
                cntr_temp.Size = new Size(150, 90);
                cntr_temp.Location = new Point(383, 235);
                txt_border = (TextBox)cntr_temp;
                txt_border.BorderStyle = BorderStyle.None;

                lblcrt(false);
                drag.Stop();
                try
                {
                    cntr_temp.Text = "Dispatch Date : " + dtp_logs.Rows[0].ItemArray[5].ToString();
                }
                catch (Exception erty) { cntr_temp.Text = "Dispatch Date : --/--/----"; }
                cntr_temp.Location = new Point(350, 85);

                lblcrt(false);
                drag.Stop();
                try
                {
                    cntr_temp.Text = "Delivery Date : " + dtp_logs.Rows[0].ItemArray[6].ToString();
                }
                catch (Exception erty) { cntr_temp.Text = "Delivery Date : --/--/----"; }
                cntr_temp.Location = new Point(350, 100);
            }
            catch (Exception erty) { MessageBox.Show(erty.Message); }

            int ndx = 0; int y = 435;
            foreach (DataGridViewRow dgvr in Data.Rows)
            {
                try
                {
                    if (dgvr.Index != Data.Rows.Count - 1)
                    {
                        lblcrt(false);
                        drag.Stop();
                        cntr_temp.Text = ndx.ToString();
                        cntr_temp.Location = new Point(68, y);

                        try
                        {
                            lblcrt(false); drag.Stop();
                            cntr_temp.Text = dgvr.Cells[1].Value.ToString();
                            cntr_temp.Location = new Point(159, y);
                        }
                        catch (Exception erty) { }

                        try
                        {
                            lblcrt(false); drag.Stop();
                            cntr_temp.Text = dgvr.Cells[2].Value.ToString();
                            cntr_temp.Location = new Point(291, y);
                        }
                        catch (Exception erty) { }

                        try
                        {
                            if (dgvr.Cells[1].Value != DBNull.Value && dgvr.Index != Data.Rows.Count - 1)
                            {
                                dtp_prod_info.Clear();
                                if (Main.Amatrix.mgt == "")
                                {
                                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                                    SqlCeCommand cmd = new SqlCeCommand("SELECT [Product Consumer Price], [Product Taxation Cost] FROM Prod_mgmt WHERE [Product ID Number] = '" + dgvr.Cells[1].Value.ToString() + "'", conn);
                                    conn.Open();
                                    SqlCeDataReader dr = cmd.ExecuteReader();
                                    dtp_prod_info.Load(dr);
                                    conn.Close();
                                }
                                else
                                {
                                    dtp_prod_info = basql.Execute(Main.Amatrix.mgt, "SELECT [Product Consumer Price], [Product Taxation Cost] FROM Prod_mgmt WHERE [Product ID Number] = '" + dgvr.Cells[1].Value.ToString() + "'", "Prod_mgmt", dtp_prod_info);
                                }

                                Discnt = 0; Price_CURR = 0;
                                Price_TAX = 0;

                                try
                                {
                                    lblcrt(false);
                                    drag.Stop();
                                    Price_CURR = Convert.ToDouble(dtp_prod_info.Rows[0].ItemArray[0]);//Price_TOT;
                                    cntr_temp.Text = dtp_prod_info.Rows[0].ItemArray[0].ToString();
                                    cntr_temp.Location = new Point(463, y);
                                    Price_TOT = Price_TOT + Convert.ToDouble(dtp_prod_info.Rows[0].ItemArray[0]);
                                }
                                catch (Exception erty) { Price_CURR = 0; Price_TOT = Price_TOT + 0; }

                                try
                                {
                                    lblcrt(false);
                                    drag.Stop();
                                    Tax_CURR = Convert.ToDouble(dtp_prod_info.Rows[0].ItemArray[1]);
                                    cntr_temp.Text = dtp_prod_info.Rows[0].ItemArray[1].ToString();
                                    cntr_temp.Location = new Point(555, y);
                                    Price_TAX = Price_TAX + Convert.ToDouble(dtp_prod_info.Rows[0].ItemArray[1]);
                                }
                                catch (Exception erty) { Price_TAX = Price_TAX + 0; Tax_CURR = 0; }

                                try
                                {
                                    lblcrt(false);
                                    drag.Stop();
                                    try
                                    {
                                        cntr_temp.Text = dgvr.Cells[7].Value.ToString();
                                        Discnt = Convert.ToDouble(dgvr.Cells[7].Value);
                                        DIsC_TOT = DIsC_TOT + Discnt;
                                    }
                                    catch (Exception erty)
                                    {
                                        cntr_temp.Text = "0";
                                        DIsC_TOT = DIsC_TOT + 0;
                                        Discnt = 0;
                                    }
                                    cntr_temp.Location = new Point(615, y);
                                }
                                catch (Exception erty) { }

                                try
                                {
                                    txtldcrt(false);
                                    drag.Stop();
                                    try
                                    {
                                        cntr_temp.Text = dgvr.Cells[6].Value.ToString();
                                        QTY = Convert.ToDouble(dgvr.Cells[6].Value.ToString());
                                        QTY_TOT = QTY_TOT + Convert.ToDouble(dgvr.Cells[6].Value);
                                    }
                                    catch (Exception erty)
                                    {
                                        cntr_temp.Text = "1";
                                        QTY = 1;
                                        QTY_TOT = QTY_TOT + 1;
                                    }
                                    cntr_temp.Size = new Size(30, cntr_temp.Size.Height);
                                    cntr_temp.Location = new Point(728, y);
                                }
                                catch (Exception erty) { }

                                lblcrt(false);
                                try
                                {
                                    drag.Stop();
                                    //try
                                    //{
                                    cntr_temp.Text = (((Price_CURR + Tax_CURR) * QTY) - Discnt).ToString();
                                    TOT = TOT + (((Price_CURR + Tax_CURR) * QTY) - Discnt);
                                    /*}
                                    catch (Exception erty)
                                    {
                                        try
                                        {
                                            cntr_temp.Text = ((Price_CURR + Tax_CURR) - Discnt).ToString();
                                            TOT = TOT + ((Price_CURR + Tax_CURR) - Discnt);
                                        }
                                        catch (Exception eryy)
                                        {
                                            cntr_temp.Text = (Price_CURR + Tax_CURR).ToString();
                                            TOT = TOT + (Price_CURR + Tax_CURR);
                                        }
                                    }*/
                                    cntr_temp.Location = new Point(660, y);
                                }
                                catch (Exception erty) { cntr_temp.Text = "N.A."; }
                            }
                        }
                        catch (Exception erty) { }

                        y = y + 25; ndx++;
                        if (y > pnl_tmt.Size.Height - 25)
                        {
                            new_pge();
                            y = 45;
                        }
                    }
                }
                catch (Exception erty) { pnl_tmt.Controls.Remove(cntr_temp); }
            }

            //endbx
            y = y + 5;
            piccrt(false);
            drag.Stop();
            cntr_temp.Location = new Point(41, y);
            cntr_temp.BackColor = Color.Gray;
            cntr_temp.Size = new Size(pnl_tmt.Size.Width - 90, 8);

            y = y + 25;
            if (y > pnl_tmt.Size.Height - 25)
            {
                new_pge();
                y = 45;
            }
            lblcrt(false);
            drag.Stop();
            cntr_temp.Text = Price_TOT.ToString();
            cntr_temp.Location = new Point(463, y);

            lblcrt(false);
            drag.Stop();
            cntr_temp.Text = Price_TAX.ToString();
            cntr_temp.Location = new Point(555, y);

            lblcrt(false);
            drag.Stop();
            cntr_temp.Text = DIsC_TOT.ToString();
            cntr_temp.Location = new Point(615, y);

            lblcrt(false);
            drag.Stop();
            try
            {
                cntr_temp.Text = TOT.ToString();
                //cntr_temp.Text = (((Price_TAX + Price_TOT) * QTY_TOT) - DIsC_TOT).ToString();
            }
            catch (Exception erty)
            {
                /*try
                {
                    cntr_temp.Text = (((Price_TAX + Price_TOT) - DIsC_TOT)).ToString();
                }
                catch (Exception ertyt)
                {
                    cntr_temp.Text = (Price_TAX + Price_TOT).ToString();
                }*/
            }
            
            cntr_temp.Location = new Point(660, y); 
            
            txtldcrt(false);
            drag.Stop();
            try
            {
                cntr_temp.Text = QTY_TOT.ToString();
            }
            catch (Exception erty)
            {
                cntr_temp.Text = "1";
            }
            cntr_temp.Size = new Size(30, cntr_temp.Size.Height);
            cntr_temp.Location = new Point(728, y);

            y = y + 50;

            if (y > pnl_tmt.Size.Height - 25)
            {
                new_pge();
                y = 45;
            }

            txtldcrt(false);
            drag.Stop();
            cntr_temp.Location = new Point(60, y);
            cntr_temp.Size = new Size(pnl_tmt.Size.Width - 120, 70);
            cntr_temp.ForeColor = Color.Gray;
            cntr_temp.Text = "Terms And Conditions:";
            txt_border = (TextBox)cntr_temp;
            txt_border.BorderStyle = BorderStyle.None;
        }

        //To avoid confusion, Compile_Print_data was rendered Seperate for the 2 different orders
        public void Compile_Print_Data2(DataGridView Data, String Order_Number, String Date_Of_Order, String logs, String Buyer)// String Ship_Date_, String Shipping_Method, String Proms_Arrival_Date)
        {
            OrderNumber = Order_Number;
            DateofOrder = Date_Of_Order;
            Supplier = logs;
            Buyer_ = Buyer;

            try
            {
                if (logs != null)
                {
                    dtp_logs.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Logs_mgmt WHERE [Logistical ID Batch] = '" + logs + "'", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp_logs.Load(dr);
                        conn.Close();
                    }
                    else
                    {
                        dtp_logs = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Logs_mgmt WHERE [Logistical ID Batch] = '" + logs + "'", "Logs_mgmt", dtp_logs);
                    }
                }

                pnl_tmt = (Panel)al_pge[0];
                lblcrt(false);
                drag.Stop();
                cntr_temp.Text = "Order Number(Ref No.) : " + Order_Number;
                cntr_temp.Location = new Point(350, 120);

                lblcrt(false);
                drag.Stop();
                cntr_temp.Text = "Date : " + Date_Of_Order;
                cntr_temp.Location = new Point(350, 133);

                txtldcrt(false);
                drag.Stop();
                try
                {
                    cntr_temp.Text = dtp_logs.Rows[0].ItemArray[1].ToString();
                }
                catch (Exception erty)
                {
                    cntr_temp.Text = "Enter Purchase From Info Here...";
                }
                cntr_temp.Size = new Size(150, 90);
                cntr_temp.Location = new Point(157, 235);
                txt_border = (TextBox)cntr_temp;
                txt_border.BorderStyle = BorderStyle.None;
                //cntr_temp.Text = cntr_temp.Text + "\n" + "Dispatch Date: " + dtp_logs.Rows[0].ItemArray[5].ToString() + "\n" + "Delivery Date: ";

                txtldcrt(false);
                drag.Stop();
                try
                {
                    cntr_temp.Text = dtp_logs.Rows[0].ItemArray[2].ToString();
                }
                catch (Exception erty) { cntr_temp.Text = "Enter Delivery To Info Here..."; }
                cntr_temp.Size = new Size(150, 90);
                cntr_temp.Location = new Point(383, 235);
                txt_border = (TextBox)cntr_temp;
                txt_border.BorderStyle = BorderStyle.None;

                lblcrt(false);
                drag.Stop();
                try
                {
                    cntr_temp.Text = "Dispatch Date : " + dtp_logs.Rows[0].ItemArray[5].ToString();
                }
                catch (Exception erty) { cntr_temp.Text = "Dispatch Date : --/--/----"; }
                cntr_temp.Location = new Point(350, 85);

                lblcrt(false);
                drag.Stop();
                try
                {
                    cntr_temp.Text = "Delivery Date : " + dtp_logs.Rows[0].ItemArray[6].ToString();
                }
                catch (Exception erty) { cntr_temp.Text = "Delivery Date : --/--/----"; }
                cntr_temp.Location = new Point(350, 100);

                lblcrt(false);
                drag.Stop(); 
                cntr_temp.Text = Buyer;
                cntr_temp.Location = new Point(50, 143);
            }
            catch (Exception erty) { MessageBox.Show(erty.Message); }

            int ndx = 0; int y = 435;
            foreach (DataGridViewRow dgvr in Data.Rows)
            {
                try
                {
                    if (dgvr.Index != Data.Rows.Count - 1)
                    {
                        lblcrt(false);
                        drag.Stop();
                        cntr_temp.Text = ndx.ToString();
                        cntr_temp.Location = new Point(68, y);

                        try
                        {
                            lblcrt(false); drag.Stop();
                            cntr_temp.Text = dgvr.Cells[1].Value.ToString();
                            cntr_temp.Location = new Point(159, y);
                        }
                        catch (Exception erty) { }

                        try
                        {
                            lblcrt(false); drag.Stop();
                            cntr_temp.Text = dgvr.Cells[2].Value.ToString();
                            cntr_temp.Location = new Point(291, y);
                        }
                        catch (Exception erty) { }

                        if (Main.Amatrix.mgt == "")
                        {
                            try
                            {
                                if (dgvr.Cells[1].Value != DBNull.Value && dgvr.Index != Data.Rows.Count - 1)
                                {
                                    dtp_prod_info.Clear();
                                    if (Main.Amatrix.mgt == "")
                                    {
                                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                                        SqlCeCommand cmd = new SqlCeCommand("SELECT [Product Consumer Price], [Product Taxation Cost] FROM Prod_mgmt WHERE [Product ID Number] = '" + dgvr.Cells[1].Value.ToString() + "'", conn);
                                        conn.Open();
                                        SqlCeDataReader dr = cmd.ExecuteReader();
                                        dtp_prod_info.Load(dr);
                                        conn.Close();
                                    }
                                    else
                                    {
                                        dtp_prod_info = basql.Execute(Main.Amatrix.mgt, "SELECT [Product Consumer Price], [Product Taxation Cost] FROM Prod_mgmt WHERE [Product ID Number] = '" + dgvr.Cells[1].Value.ToString() + "'", "Prod_mgmt", dtp_prod_info);
                                    }

                                    Discnt = 0; Price_CURR = 0;
                                    Price_TAX = 0;

                                    try
                                    {
                                        lblcrt(false);
                                        drag.Stop();
                                        cntr_temp.Text = dtp_prod_info.Rows[0].ItemArray[0].ToString();
                                        cntr_temp.Location = new Point(463, y);
                                        Price_TOT = Price_TOT + Convert.ToDouble(dtp_prod_info.Rows[0].ItemArray[0]);
                                        Price_CURR = Convert.ToDouble(dtp_prod_info.Rows[0].ItemArray[0]);//Price_TOT;
                                    }
                                    catch (Exception erty) { Price_TOT = Price_TOT + 0; Price_CURR = 0; }

                                    try
                                    {
                                        lblcrt(false);
                                        drag.Stop();
                                        cntr_temp.Text = dtp_prod_info.Rows[0].ItemArray[1].ToString();
                                        cntr_temp.Location = new Point(555, y);
                                        Price_TAX = Price_TAX + Convert.ToDouble(dtp_prod_info.Rows[0].ItemArray[1]);
                                        Tax_CURR = Convert.ToDouble(dtp_prod_info.Rows[0].ItemArray[1]);
                                    }
                                    catch (Exception erty) { Price_TAX = Price_TAX + 0;; Tax_CURR = 0; }

                                    try
                                    {
                                        lblcrt(false);
                                        drag.Stop();
                                        try
                                        {
                                            cntr_temp.Text = dgvr.Cells[7].Value.ToString();
                                            Discnt = Convert.ToDouble(dgvr.Cells[7].Value);
                                            DIsC_TOT = DIsC_TOT + Discnt;
                                        }
                                        catch (Exception erty)
                                        {
                                            cntr_temp.Text = "0";
                                            DIsC_TOT = DIsC_TOT + 0;
                                            Discnt = 0;
                                        }
                                        cntr_temp.Location = new Point(615, y);
                                    }
                                    catch (Exception erty) { }

                                    try
                                    {
                                        txtldcrt(false);
                                        drag.Stop();
                                        try
                                        {
                                            cntr_temp.Text = dgvr.Cells[6].Value.ToString();
                                            QTY = Convert.ToDouble(dgvr.Cells[6].Value.ToString());
                                            QTY_TOT = QTY_TOT + Convert.ToDouble(dgvr.Cells[6].Value);
                                        }
                                        catch (Exception erty)
                                        {
                                            cntr_temp.Text = "1";
                                            QTY = 1;
                                            QTY_TOT = QTY_TOT + 1;
                                        }
                                        cntr_temp.Size = new Size(30, cntr_temp.Size.Height);
                                        cntr_temp.Location = new Point(728, y);
                                    }
                                    catch (Exception erty) { }

                                    lblcrt(false);
                                    try
                                    {
                                        drag.Stop();
                                        //try
                                        //{
                                            cntr_temp.Text = (((Price_CURR + Tax_CURR) * QTY) - Discnt).ToString();
                                            TOT = TOT + (((Price_CURR + Tax_CURR) * QTY) - Discnt);
                                        /*}
                                        catch (Exception erty)
                                        {
                                            try
                                            {
                                                cntr_temp.Text = ((Price_CURR + Tax_CURR) - Discnt).ToString();
                                                TOT = TOT + ((Price_CURR + Tax_CURR) - Discnt);
                                            }
                                            catch (Exception eryy)
                                            {
                                                cntr_temp.Text = (Price_CURR + Tax_CURR).ToString();
                                                TOT = TOT + (Price_CURR + Tax_CURR);
                                            }
                                        }*/
                                        cntr_temp.Location = new Point(660, y);
                                    }
                                    catch (Exception erty) { cntr_temp.Text = "N.A."; }
                                }
                            }
                            catch (Exception erty) { }
                        }
                        else { }

                        y = y + 25; ndx++;
                        if (y > pnl_tmt.Size.Height - 25)
                        {
                            new_pge();
                            y = 45;
                        }
                    }
                }
                catch (Exception erty) { pnl_tmt.Controls.Remove(cntr_temp); }
            }

            //endbx
            y = y + 5;
            piccrt(false);
            drag.Stop();
            cntr_temp.Location = new Point(41, y);
            cntr_temp.BackColor = Color.Gray;
            cntr_temp.Size = new Size(pnl_tmt.Size.Width - 90, 8);

            if (y > pnl_tmt.Size.Height)
            {
                new_pge();
            }

            y = y + 25;
            if (y > pnl_tmt.Size.Height - 25)
            {
                new_pge();
                y = 45;
            }
            lblcrt(false);
            drag.Stop();
            cntr_temp.Text = Price_TOT.ToString();
            cntr_temp.Location = new Point(463, y);

            lblcrt(false);
            drag.Stop();
            cntr_temp.Text = Price_TAX.ToString();
            cntr_temp.Location = new Point(555, y);

            lblcrt(false);
            drag.Stop();
            cntr_temp.Text = DIsC_TOT.ToString();
            cntr_temp.Location = new Point(615, y);

            lblcrt(false);
            drag.Stop();
            try
            {
                cntr_temp.Text = TOT.ToString();
                //cntr_temp.Text = (((Price_TAX + Price_TOT) * QTY_TOT) - DIsC_TOT).ToString();
            }
            catch (Exception erty)
            {
                /*try
                {
                    cntr_temp.Text = (((Price_TAX + Price_TOT) - DIsC_TOT)).ToString();
                }
                catch (Exception ertyt)
                {
                    cntr_temp.Text = (Price_TAX + Price_TOT).ToString();
                }*/
            }

            cntr_temp.Location = new Point(660, y);

            txtldcrt(false);
            drag.Stop();
            try
            {
                cntr_temp.Text = QTY_TOT.ToString();
            }
            catch (Exception erty)
            {
                cntr_temp.Text = "1";
            }
            cntr_temp.Size = new Size(30, cntr_temp.Size.Height);
            cntr_temp.Location = new Point(728, y);

            y = y + 50;

            if (y > pnl_tmt.Size.Height - 25)
            {
                new_pge();
                y = 45;
            }

            txtldcrt(false);
            drag.Stop();
            cntr_temp.Location = new Point(60, y);
            cntr_temp.Size = new Size(pnl_tmt.Size.Width - 120, 70);
            cntr_temp.ForeColor = Color.Gray;
            cntr_temp.Text = "Terms And Conditions:";
            txt_border = (TextBox)cntr_temp;
            txt_border.BorderStyle = BorderStyle.None;
        }

        //Templates

        private void general_templ()
        {
            Label lbl_temp2 = new Label();
            lbl_temp2 = tgp.Header_Label();

            lbl_temp2.Click += new EventHandler(Click_);
            lbl_temp2.MouseDown += new MouseEventHandler(MouseDown_);
            lbl_temp2.MouseUp += new MouseEventHandler(MouseUp_);

            PictureBox pbx_temp2 = new PictureBox();
            pbx_temp2 = tgp.Picture_Box();
            pbx_temp2.Click += new EventHandler(Click_);
            pbx_temp2.MouseDown += new MouseEventHandler(MouseDown_);
            pbx_temp2.MouseUp += new MouseEventHandler(MouseUp_);

            pnl1.Controls.Add(pbx_temp2);
            pnl1.Controls.Add(lbl_temp2);
        }

        //Templates END
        string PassWord = "";
        private void Doc_stdio_Load(object sender, EventArgs e)
        {

        }

        Panel pnl_temp;
        private void nw_pge_Click(object sender, EventArgs e)
        {
            new_pge();
        }

        private void new_pge()
        {
            y = 50;
            Panel pnl = new Panel();
            Label lbl = new Label();

            pages.DropDownItems.Add((al_pge.Count + 1).ToString(), Properties.Resources.page1);
            pages.DropDownItems[pages.DropDownItems.Count - 1].Click += new EventHandler(pages_click);

            pnl.BorderStyle = BorderStyle.FixedSingle;
            pnl_temp = (Panel)al_pge[al_pge.Count - 1];
            pnl.Size = new Size(pnl_temp.Size.Width, pnl_temp.Size.Height);
            pnl.Location = new Point(pnl_temp.Location.X, pnl_temp.Location.Y + pnl_temp.Size.Height + 32);
            pnl.BackColor = pnl_temp.BackColor;
            pnl.ContextMenuStrip = cms_pnl;
            pnl.Click += new EventHandler(pnl_Click);
            pnl_journvw.Controls.Add(pnl);
            pnl.MouseEnter += new EventHandler(pnl_MouseEnter);

            lbl.Text = "Page " + (al_lbl.Count + 1).ToString();
            lbl.ForeColor = label2.ForeColor;
            lbl.Font = label2.Font;
            lbl.Size = new Size(lbl.Size.Width, label2.Size.Height);
            lbl.Location = new Point(24, pnl.Location.Y - 25);
            lbl.MouseEnter += new EventHandler(label2_MouseEnter);
            lbl.MouseLeave += new EventHandler(label2_MouseLeave);
            al_lbl.Add(lbl);
            pnl_journvw.Controls.Add(lbl);

            PictureBox pb_shad_up = new PictureBox();
            pb_shad_up.BackgroundImage = Properties.Resources.shadow;
            pb_shad_up.BackgroundImageLayout = ImageLayout.Stretch;
            pb_shad_up.Location = new Point(pnl.Location.X, pnl.Location.Y - 5);
            pb_shad_up.Size = new Size(pnl.Size.Width, 11);
            pnl_journvw.Controls.Add(pb_shad_up);
            pb_shad_up.SendToBack();

            PictureBox pb_shad_dwn = new PictureBox();
            pb_shad_dwn.BackgroundImage = Properties.Resources.shadowdwn;
            pb_shad_dwn.BackgroundImageLayout = ImageLayout.Stretch;
            pb_shad_dwn.Location = new Point(pnl.Location.X, pnl.Location.Y + pnl.Size.Height - 6);
            pb_shad_dwn.Size = new Size(pnl.Size.Width, 11);
            pnl_journvw.Controls.Add(pb_shad_dwn);
            pb_shad_dwn.SendToBack();

            PictureBox pb_shad_rgt = new PictureBox();
            pb_shad_rgt.BackgroundImage = Properties.Resources.shadow___Copy;
            pb_shad_rgt.BackgroundImageLayout = ImageLayout.Stretch;
            pb_shad_rgt.Location = new Point(pnl.Location.X + pnl.Size.Width - 9, pnl.Location.Y);
            pb_shad_rgt.Size = new Size(16, pnl.Size.Height);
            pnl_journvw.Controls.Add(pb_shad_rgt);
            pb_shad_rgt.SendToBack();

            PictureBox pb_shad_lft = new PictureBox();
            pb_shad_lft.BackgroundImage = Properties.Resources.shadow___Copy___Copy;
            pb_shad_lft.BackgroundImageLayout = ImageLayout.Stretch;
            pb_shad_lft.Location = new Point(pnl.Location.X - 7, pnl.Location.Y);
            pb_shad_lft.Size = new Size(16, pnl.Size.Height);
            pnl_journvw.Controls.Add(pb_shad_lft);
            pb_shad_lft.SendToBack();

            lbl.SendToBack();

            /*RichTextBox rtb = new RichTextBox();
            rtb.BorderStyle = BorderStyle.None;
            rtb.Dock = DockStyle.Fill;*/

            //pnl.Controls.Add(rtb);
            ArrayList al_tmp = new ArrayList();
            al_tmp.Add(lbl);
            al_tmp.Add(pb_shad_up);
            al_tmp.Add(pb_shad_rgt);
            al_tmp.Add(pb_shad_lft);
            al_tmp.Add(pb_shad_dwn);

            ak_pge_itms.Add(al_tmp);

            al_pge.Add(pnl);
            pnl.Tag = (Object)(al_pge.Count - 1);
            pnl.Select();
            pnl_tmt = pnl;
        }

        void pnl_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                pnl_tmt = (Panel)sender;
            }
            catch (Exception erty) { RichTextBox rtbb = (RichTextBox)sender; pnl_tmt = (Panel)rtbb.Parent; }
            pnl = pnl_tmt;
            //pnl.Controls.Add(itm_txt);
            pnl.Controls.Add(trackBar1);
            pnl.Controls.Add(trackBar2);
            //pnl.Controls.Add(toolStrip1);
            trackBar1.Visible = false;
            trackBar2.Visible = false;
        }

        Panel pnl_tmt;
        void pnl_Click(object sender, EventArgs e)
        {
            drag.Stop();
        }

        //Panel pnl_tmp;
        void pages_click(object sender, EventArgs e)
        {
            ToolStripDropDownItem tddi = (ToolStripDropDownItem)sender;
            int dint = Convert.ToInt32(tddi.Text);
            pnl_tmt = (Panel)al_pge[dint-1];
            pnl_tmt.Select();
        }

        private Label lbl_temp;
        private void label2_MouseEnter(object sender, EventArgs e)
        {
            lbl_temp = (Label)sender;
            lbl_temp.ForeColor = Color.Silver;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            lbl_temp = (Label)sender;
            lbl_temp.ForeColor = Color.LightGray;
        }

        private void pnl_journvw_MouseEnter(object sender, EventArgs e)
        {
            pnl_journvw.Select();
        }

        ToolStripButton tsbttn_temp_other; ToolStrip ts_temp_other;
        Panel pnl_temp_other;
        private void lblcrt_btn_Click(object sender, EventArgs e)
        {
            tsbttn_temp_other = (ToolStripButton)sender;
            try
            {
                pnl_temp_other = (Panel)tsbttn_temp_other.Owner.Parent;
            }
            catch (Exception erty) { pnl_temp_other = pnl1; }
            if (sender.Equals(lblcrt_btn) == true)
            {
                lblcrt(false);
            }
            if (sender.Equals(new_pic) == true)
            {
                piccrt(false);
            }
            if (sender.Equals(txt_entr) == true)
            {
                txtldcrt(false);
            }
            if (sender.Equals(chkbx_crt) == true)
            {
                chkbxcrt(false);
            }
            if (sender.Equals(dtetme) == true)
            {
                dtetmecrt(false);
            }
        }

        //control working

        private void dtetmecrt(bool From_Cntr)
        {
            DateTimePicker dtp = new DateTimePicker();
            dtp.ContextMenuStrip = cms;
            if (From_Cntr == false)
            {
                dtp.ForeColor = Color.Black;
                dtp.BackColor = Color.White;
                dtp.Font = tgp.Default_Font;
            }
            else
            {
                dtp.ForeColor = cpy_cntrl2.ForeColor;
                dtp.BackColor = cpy_cntrl2.BackColor;
                dtp.Font = cpy_cntrl2.Font;
                dtp.Location = cpy_cntrl2.Location;
                dtp.Size = cpy_cntrl2.Size;
                dtp.Text = cpy_cntrl2.Text;
            }

            dtp.Click += new EventHandler(Click_);
            dtp.MouseDown += new MouseEventHandler(MouseDown_);
            dtp.MouseUp += new MouseEventHandler(MouseUp_);
            dtp.MouseEnter += new EventHandler(lbl_MouseEnter);
            dtp.MouseLeave += new EventHandler(lbl_MouseLeave);
            dtp.SizeChanged += new EventHandler(lbl_SizeChanged);
            dtp.LocationChanged += new EventHandler(lbl_SizeChanged);

            pnl_tmt.Controls.Add(dtp);
            dtp.BringToFront();
            drag.Start();
            cntr_temp = (Control)dtp;
        }

        private void dtfldcrt()
        {
            DataGridView dgvr = new DataGridView();
            dgvr.ContextMenuStrip = cms;
            dgvr.ForeColor = Color.Black;
            dgvr.BackColor = Color.White;
            dgvr.Font = tgp.Default_Font;

            dgvr.Click += new EventHandler(Click_);
            dgvr.MouseDown += new MouseEventHandler(MouseDown_);
            dgvr.MouseUp += new MouseEventHandler(MouseUp_);
            dgvr.MouseEnter += new EventHandler(lbl_MouseEnter);
            dgvr.MouseLeave += new EventHandler(lbl_MouseLeave);
            dgvr.SizeChanged += new EventHandler(lbl_SizeChanged);
            dgvr.LocationChanged += new EventHandler(lbl_SizeChanged);

            pnl_tmt.Controls.Add(dgvr);
            dgvr.BringToFront();
            drag.Start();
            cntr_temp = (Control)dgvr;
        }

        private void chkbxcrt(bool From_Cntr)
        {
            CheckBox cbx = new CheckBox();
            if (From_Cntr == false)
            {
                cbx.ForeColor = Color.Black;
                cbx.BackColor = Color.White;
                cbx.Font = tgp.Default_Font;
            }
            else
            {
                cbx.ForeColor = cpy_cntrl2.ForeColor;
                cbx.BackColor = cpy_cntrl2.BackColor;
                cbx.Font = cpy_cntrl2.Font;
                cbx.Location = cpy_cntrl2.Location;
                cbx.Text = cpy_cntrl2.Text;
            }
            cbx.AutoSize = true;
            cbx.Text = "Enter Text Here";
            cbx.Click += new EventHandler(Click_); 
            cbx.MouseDown += new MouseEventHandler(MouseDown_);
            cbx.MouseEnter += new EventHandler(lbl_MouseEnter);
            cbx.MouseLeave += new EventHandler(lbl_MouseLeave);
            cbx.MouseUp += new MouseEventHandler(MouseUp_);
            cbx.ContextMenuStrip = cms;

            cbx.BackColor = Color.Transparent;

            pnl_tmt.Controls.Add(cbx);
            cbx.BringToFront();
            drag.Start();
            cntr_temp = (Control)cbx;
        }

        void cbx_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void txtldcrt(bool From_Cntr)
        {
            TextBox tbx = new TextBox();
            tbx.BorderStyle = BorderStyle.FixedSingle;
            tbx.Multiline = true;

            if (From_Cntr == false)
            {
                tbx.ForeColor = Color.Black;
                tbx.BackColor = Color.White;
                tbx.Font = tgp.Default_Font;
            }
            else
            {
                tbx.ForeColor = cpy_cntrl2.ForeColor;
                tbx.BackColor = cpy_cntrl2.BackColor;
                tbx.Font = cpy_cntrl2.Font;
                tbx.Location = cpy_cntrl2.Location;
                tbx.Size = cpy_cntrl2.Size;
                tbx.Text = cpy_cntrl2.Text;
            }
            tbx.ContextMenuStrip = cms;
            tbx.Click += new EventHandler(Click_);
            tbx.MouseDown += new MouseEventHandler(MouseDown_);
            tbx.MouseUp += new MouseEventHandler(MouseUp_);
            tbx.MouseEnter += new EventHandler(lbl_MouseEnter);
            tbx.MouseLeave += new EventHandler(lbl_MouseLeave);
            tbx.SizeChanged += new EventHandler(lbl_SizeChanged);
            tbx.LocationChanged += new EventHandler(lbl_SizeChanged);
            pnl_tmt.Controls.Add(tbx);
            tbx.BringToFront();
            drag.Start();
            cntr_temp = (Control)tbx;
        }

        
        Panel pnl;
        private void lblcrt(bool From_Cntr)
        {
            Label lbl = new Label();
            lbl.AutoSize = true;
            lbl.Text = "New Text Label";

            if (From_Cntr == false)
            {
                //lbl.Text = "Enter Your Text Here";
                lbl.ForeColor = Color.Black;
                lbl.BackColor = Color.White;
                lbl.Font = tgp.Default_Font;
            }
            else
            {
                try
                {
                    lbl.ForeColor = cpy_cntrl2.ForeColor;
                    lbl.BackColor = cpy_cntrl2.BackColor;
                    lbl.Font = cpy_cntrl2.Font;
                    lbl.Location = cpy_cntrl2.Location;
                    lbl.Text = cpy_cntrl2.Text;
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
            }

            lbl.ContextMenuStrip = cms;
            lbl.Click += new EventHandler(Click_);
            lbl.MouseDown += new MouseEventHandler(MouseDown_);
            lbl.MouseUp += new MouseEventHandler(MouseUp_);
            lbl.MouseEnter += new EventHandler(lbl_MouseEnter);
            lbl.MouseLeave += new EventHandler(lbl_MouseLeave);
            lbl.SizeChanged += new EventHandler(lbl_SizeChanged);
            lbl.LocationChanged += new EventHandler(lbl_SizeChanged);
            try
            {
                lbl.BackColor = Color.Transparent;
            }
            catch (Exception erty) { }
            
            ar_MouseDU.Add(lbl);
            ar_click.Add(lbl);

            pnl_tmt.Controls.Add(lbl);
            lbl.BringToFront();
            drag.Start();
            cntr_temp = (Control)lbl;
        }

        private Control ctr_temp;
        void lbl_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (user_mode == false)
                {
                    trackBar2.Location = new Point(cntr_temp.Location.X - trackBar2.Size.Width, cntr_temp.Location.Y);
                    trackBar1.Location = new Point(cntr_temp.Location.X, cntr_temp.Location.Y + cntr_temp.Size.Height);
                }
            }
            catch (Exception erty) { }
        }

        void lbl_MouseLeave(object sender, EventArgs e)
        {
        }

        bool informative_ = false; Type tp_temp;
        void lbl_MouseEnter(object sender, EventArgs e)
        {
            cntr_temp = (Control)sender;
            informative_ = true;
            tp_temp = cntr_temp.GetType();
            toolStripComboBox1.Text = cntr_temp.Font.Name;
            toolStripComboBox2.Text = cntr_temp.Font.Size.ToString();

            toolStripButton2.Checked = cntr_temp.Font.Bold;
            toolStripButton3.Checked = cntr_temp.Font.Italic;
            toolStripButton5.Checked = cntr_temp.Font.Underline;

            itm_txt.Text = cntr_temp.Text;

            if (user_mode == false)
            {
                trackBar1.Visible = true; trackBar2.Visible = true;
                trackBar2.Location = new Point(cntr_temp.Location.X - trackBar2.Size.Width, cntr_temp.Location.Y);
                trackBar1.Location = new Point(cntr_temp.Location.X, cntr_temp.Location.Y + cntr_temp.Size.Height);
                trackBar2.Value = cntr_temp.Size.Height;
                trackBar1.Value = cntr_temp.Size.Width;
                trackBar1.BringToFront();
                trackBar2.BringToFront();
            }
            else { trackBar1.Visible = false; trackBar2.Visible = false; }
            informative_ = false;
        }

        private void piccrt(bool From_Cntr)
        {
            Panel pbx = new Panel();

            pbx.BorderStyle = BorderStyle.FixedSingle;
            if (From_Cntr == false)
            {
                pbx.BackgroundImageLayout = ImageLayout.Center;
            }
            else
            {
                pbx.BackgroundImage = cpy_cntrl2.BackgroundImage;
                pbx.BackgroundImageLayout = cpy_cntrl2.BackgroundImageLayout;
                pbx.Location = cpy_cntrl2.Location;
                pbx.Size = cpy_cntrl2.Size;
            }

            try
            {
                pbx.BackColor = Color.Transparent;
            }
            catch (Exception erty) { }
            pbx.ContextMenuStrip = cms;
            pbx.Click += new EventHandler(Click_);
            pbx.MouseDown += new MouseEventHandler(MouseDown_);
            pbx.MouseUp += new MouseEventHandler(MouseUp_);
            pbx.MouseEnter += new EventHandler(lbl_MouseEnter);
            pbx.MouseLeave += new EventHandler(lbl_MouseLeave);
            pbx.SizeChanged += new EventHandler(lbl_SizeChanged);
            pbx.LocationChanged += new EventHandler(lbl_SizeChanged);
            pbx.DoubleClick += new EventHandler(pbx_DoubleClick);
            ar_click.Add(pbx);
            ar_MouseDU.Add(pbx);

            try
            {
                pnl_tmt.Controls.Add(pbx);
            }
            catch (Exception erty) { pnl_tmt = pnl1; }
            drag.Start();
            cntr_temp = (Control)pbx;
            pbx.BringToFront();
        }

        void pbx_DoubleClick(object sender, EventArgs e)
        {
            ofd_img.ShowDialog();
        }

        private void Click_(object sender, EventArgs e)
        {

        }

        void MouseDown_(object sender, MouseEventArgs e)
        {
            if (user_mode == false)
            {
                cntr_temp = (Control)sender;
                cntr_temp.BringToFront();
                x = Cursor.Position.X;
                y = Cursor.Position.Y;
                drag.Start();
            }
        }

        void MouseUp_(object sender, MouseEventArgs e)
        {
            if (user_mode == false && next == false)
            {
                drag.Stop();
                pnl_tmt.Invalidate();
            }
        }

        Thread th_drag;
        public delegate void del_drag();
        private void drag_Tick(object sender, EventArgs e)
        {
            if (Cursor.Position.X != x && Cursor.Position.Y != y)
            {
                control_move();
                check_position();
                if (user_mode == false)
                {
                    trackBar1.Visible = true; trackBar2.Visible = true;
                    trackBar2.Location = new Point(cntr_temp.Location.X - trackBar2.Size.Width, cntr_temp.Location.Y);
                    trackBar1.Location = new Point(cntr_temp.Location.X, cntr_temp.Location.Y + cntr_temp.Size.Height);
                    trackBar2.Value = cntr_temp.Size.Height;
                    trackBar1.Value = cntr_temp.Size.Width;
                    trackBar1.BringToFront();
                    trackBar2.BringToFront();
                }
                else { trackBar1.Visible = false; trackBar2.Visible = false; }
            }
        }

        private void check_drag_th()
        {
            th_drag = new Thread(new ThreadStart(check_drag_del));
            th_drag.IsBackground = true;
            th_drag.Start();
        }

        private void check_drag_del()
        {
            try
            {
                this.Invoke(new del_drag(check_position));
            }
            catch (Exception ertyu) { }
        }

        Graphics g; Control Check = new Control(); Control Check2 = new Control(); int point_cur_x = 0;
        private void check_position()
        {
            foreach (Control c in pnl_tmt.Controls)
            {
                if (c.Location.X == cntr_temp.Location.X && c != cntr_temp)
                { Check = c; }
                else
                { Check = null; }

                //lock to grid
                /*if ((c.Location.X <= cntr_temp.Location.X + 5 && c.Location.X >= cntr_temp.Location.X - 5) && c != cntr_temp)
                {
                    if (c.Location.X > cntr_temp.Location.X)
                    {
                        point_cur_x = c.Location.X - cntr_temp.Location.X;
                    }
                    else { point_cur_x = cntr_temp.Location.X - c.Location.X; }

                    cntr_temp.Location = new Point(c.Location.X, cntr_temp.Location.Y);

                    Cursor.Position = new Point(point_cur_x, Cursor.Position.Y);
                }*/

                if (c.Location.Y == cntr_temp.Location.Y)
                { Check2 = c; }
                else
                { Check2 = null; }
            }

            try
            {
                if (Check.Location.X == cntr_temp.Location.X)
                {
                    using (g = pnl_tmt.CreateGraphics())
                    {
                        g.DrawLine(Pens.Green, Check.Location.X, Check.Location.Y, cntr_temp.Location.X, cntr_temp.Location.Y);
                        pnl_tmt.CreateGraphics();
                    }
                }
            }
            catch (Exception erty)
            {
                if (Check == null && Check2 == null)
                {
                    pnl_tmt.Invalidate();
                }
            }

            try
            {
                if (Check2.Location.Y == cntr_temp.Location.Y)
                {
                    using (g = pnl_tmt.CreateGraphics())
                    {
                        g.DrawLine(Pens.Green, Check2.Location.X, Check2.Location.Y, cntr_temp.Location.X, cntr_temp.Location.Y);
                        pnl_tmt.CreateGraphics();
                    }
                }
            }
            catch (Exception erty)
            {
                if (Check == null && Check2 == null)
                {
                    pnl_tmt.Invalidate();
                }
            }
        }

        private Control cntr_temp; private int current_page = 0;
        int x, y; private bool next = false;
        private void control_move()
        {
            try
            {
                if (pnl_tmt != cntr_temp.Parent)
                {
                    pnl_tmt.Controls.Add(cntr_temp);
                }
                cntr_temp.Location = new Point(((Cursor.Position.X - this.Location.X) - cntr_temp.Size.Width / 2) - pnl.Location.X, ((Cursor.Position.Y - this.Location.Y) - 59) - (pnl.Location.Y + 25));
                if (cntr_temp.Location.X < pnl.Size.Width / 2 && cntr_temp.Location.Y < pnl.Size.Height / 2)
                {
                    cntr_temp.Anchor = AnchorStyles.None;
                    cntr_temp.Anchor |= AnchorStyles.Top;
                    cntr_temp.Anchor |= AnchorStyles.Left;
                }//top-left
                else if (cntr_temp.Location.X > pnl.Size.Width / 2 && cntr_temp.Location.Y < pnl.Size.Height / 2)
                {
                    cntr_temp.Anchor = AnchorStyles.None;
                    cntr_temp.Anchor |= AnchorStyles.Top;
                    cntr_temp.Anchor |= AnchorStyles.Right;
                }//top-right
                else if (cntr_temp.Location.X < pnl.Size.Width / 2 && cntr_temp.Location.Y > pnl.Size.Height / 2)
                {
                    cntr_temp.Anchor = AnchorStyles.None;
                    cntr_temp.Anchor |= AnchorStyles.Bottom;
                    cntr_temp.Anchor |= AnchorStyles.Left;
                }//bottom-left
                else if (cntr_temp.Location.X > pnl.Size.Width / 2 && cntr_temp.Location.Y > pnl.Size.Height / 2)
                {
                    cntr_temp.Anchor = AnchorStyles.None;
                    cntr_temp.Anchor |= AnchorStyles.Bottom;
                    cntr_temp.Anchor |= AnchorStyles.Right;
                }//bottom-right
            }
            catch (Exception erty) { }
        }

        private void rmv_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_tmt.Controls.Remove(cntr_temp);
                trackBar1.Visible = false;
                trackBar2.Visible = false;
            }
            catch (Exception erty_) { }
        }

        private Control cntrl_hold;
        private void cms_Opening(object sender, CancelEventArgs e)
        {
            if (user_mode == false)
            {
                cntrl_hold = cms.SourceControl;
            }
            else { cms.Close(); }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(trackBar1) == true)
            {
                cntr_temp.Size = new Size(trackBar1.Value, cntr_temp.Size.Height);
            }
            else
            {
                cntr_temp.Size = new Size(cntr_temp.Size.Width, trackBar2.Value);
            }
        }

        private void Colos_drdwn_DropDownOpening(object sender, EventArgs e)
        {
            try
            {
                toolStripComboBox5.Text = cntr_temp.BackColor.Name;
                toolStripComboBox3.Text = cntr_temp.ForeColor.Name;
            }
            catch (Exception ertty) { }
        }

        private void colos_ch(object sender, EventArgs e)
        {
            if (sender.Equals(toolStripComboBox5) == true)
            {
                try
                {
                    cntr_temp.BackColor = Color.FromName(toolStripComboBox5.Text.ToLower());
                }
                catch (Exception erty) { }
            }
            if (sender.Equals(toolStripComboBox3) == true)
            {
                try
                {
                    cntr_temp.ForeColor = Color.FromName(toolStripComboBox3.Text.ToLower());
                }
                catch (Exception erty) { }
            }
        }

        private void Fntch(object sender, EventArgs e)
        {
            try
            {
                if (informative_ == false)
                {
                    bool b, i, s;
                    b = cntr_temp.Font.Bold;
                    i = cntr_temp.Font.Italic;
                    s = cntr_temp.Font.Underline;

                    if (toolStripComboBox2.Text.Contains('.') == true)
                    {
                        toolStripComboBox2.Text = toolStripComboBox2.Text.Remove(toolStripComboBox2.Text.IndexOf('.'));
                    }
                    int fluff = Convert.ToInt32(toolStripComboBox2.Text);
                    float fnf = (float)fluff;
                    FontStyle f = new FontStyle();
                    f = FontStyle.Regular;
                    if (toolStripButton2.Checked == true)
                    {
                        f |= FontStyle.Bold;
                    }
                    if (toolStripButton3.Checked == true)
                    {
                        f |= FontStyle.Italic;
                    }
                    if (toolStripButton5.Checked == true)
                    {
                        f |= FontStyle.Underline;
                    }
                    Font fn = new Font(toolStripComboBox1.Text, fnf, f);
                    try
                    {
                        cntr_temp.Font = fn;
                    }
                    catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
                }
            }
            catch (Exception erty) { }
        }

        private void svebtn_ButtonClick(object sender, EventArgs e)
        {
            if (File_addr == "" || File_addr == null || sender.Equals(saveAsToolStripMenuItem) == true)
            {
                sfd.ShowDialog();
            }
            else if (File_addr != "" && File_addr != null)
            {
                sve_doc();
            }
            changed = false;
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
            this.Text = "Amatrix Document Studio Beta* [Build 0325] : " + ofd.FileName;
            re_sex(File_addr);
            File_addr = sfd.FileName;
            sve_doc();
        }

        private void sve_doc()
        {
            pg.Value = 0;
            pg.Visible = true;
            pg.Increment(10);
            int ndx = 1;

            File.Delete(File_addr);
            FileStream fs = new FileStream(File_addr, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sr = new StreamWriter(fs);
            sr.WriteLine("[P:" + al_pge.Count + "/P]");
            Panel pnl_sve;
            pg.Increment(20);
            foreach (Control pnl in pnl_journvw.Controls)
            {
                try
                {
                    pnl_sve = (Panel)pnl;
                    sr.WriteLine("<PAGE : " + ndx.ToString() + ">");
                    sr.WriteLine("<PAGE COL[" + pnl.BackColor.ToString() + "]PAGE COL>");
                    int ndx2 = 1;
                    foreach (Control cntrl in pnl_sve.Controls)
                    {
                        if (cntrl.Name.ToLower() != "trackbar1" && cntrl.Name.ToLower() != "trackbar2" && cntrl.Name.ToLower() != "toolstrip1" && cntrl.Name != itm_txt.Name)
                        {
                            sr.WriteLine("<CONTROL : " + ndx2.ToString() + ">");
                            sr.WriteLine("<TYPE[" + cntrl.GetType().ToString() + "]TYPE>");
                            sr.WriteLine("<SIZE[" + cntrl.Size.Width.ToString() + ", " + cntrl.Size.Height.ToString() + "]SIZE>");
                            sr.WriteLine("<LOCATION[" + cntrl.Location.X.ToString() + ", " + cntrl.Location.Y.ToString() + "]LOCATION>");
                            sr.WriteLine("<TEXT[" + cntrl.Text + "]TEXT>");
                            sr.WriteLine("<FONT[" + cntrl.Font.Name + "]FONT>");
                            sr.WriteLine("<FONT_SIZE[" + cntrl.Font.Size.ToString() + "]FONT_SIZE>");
                            sr.WriteLine("<BIU[" + cntrl.Font.Style.ToString() + "]BIU>");

                            if (cntrl.GetType() == typeof(TextBox))
                            {
                                txt_border = (TextBox)cntrl;
                                sr.WriteLine("<BORDER[" + txt_border.BorderStyle.ToString() + "]BORDER>");
                            }
                            else if (cntrl.GetType() == typeof(Panel))
                            {
                                pictbx_border = (Panel)cntrl;
                                sr.WriteLine("<BORDER[" + pictbx_border.BorderStyle.ToString() + "]BORDER>");
                            }

                            try
                            {
                                String s = "";
                                try
                                {
                                    ImageConverter converter = new ImageConverter();
                                    Bitmap bmp = new Bitmap(cntrl.BackgroundImage);
                                    byte[] b = (byte[])converter.ConvertTo(bmp, typeof(byte[]));
                                    s = Convert.ToBase64String(b);
                                }
                                catch (Exception erty) { }
                                if (s != "" && s != null)
                                {
                                    sr.WriteLine("<IMAGE[" + s + "]IMAGE>");
                                }
                                else { sr.WriteLine("<IMAGE[Noimg]IMAGE>"); }
                            }
                            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.ToString() + ";jjjl"); }
                            sr.WriteLine("<BACKCOLOR[" + cntrl.BackColor.Name + "]BACKCOLOR>");
                            sr.WriteLine("<FORECOLOR[" + cntrl.ForeColor.Name + "]FORECOLOR>");
                            try
                            {
                                CheckBox cbx = new CheckBox();
                                cbx = (CheckBox)cntrl;
                                sr.WriteLine("<CHECK[" + cbx.Checked + "]CHECK>");
                            }
                            catch (Exception erty) { }
                            sr.WriteLine("</CONTROL : " + ndx2.ToString() + ">");
                        }
                        ndx2++;
                    }
                    sr.WriteLine("</PAGE : " + ndx.ToString() + ">");
                    ndx++;
                }
                catch (Exception erty) { }
            }            
            if (PassWord != "")
            {
                sr.WriteLine("~AM-!~!Password!~!(" + PassWord + ")");
            }
            pg.Increment(60);
            sr.Flush();
            fs.Flush();
            sr.Close();
            fs.Close();
            pg.Increment(10);
            pg.Visible = false;
            conn_State(true, 0);
            Ky_A.ASE_Key_Wr a = new Ky_A.ASE_Key_Wr();
            try
            {
                a.Write(File_addr);
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("An Error Occured while Saving Your File \"Encryption Unavailable\""); }
        }

        private void nwjrn_Click(object sender, EventArgs e)
        {
            Doc_stdio std = new Doc_stdio();
            std.Show();
        }

        private void re_sex(string File)
        {
            try
            {
                a.Write(File);
            }
            catch (Exception ert) { }
        }

        Ky_A.ASE_Key_Wr a = new Ky_A.ASE_Key_Wr();
        private void ofd_FileOk_1(object sender, CancelEventArgs e)
        {
            Doc_stdio std = new Doc_stdio();
            a.Read(ofd.FileName);
            std.open_out(ofd.FileName, user_mode);
            this.WindowState = FormWindowState.Minimized;
            std.Show();
        }

        public void remove_Page(int index)
        {
            Panel pnl_remv = (Panel)al_pge[index];
            pnl_journvw.Controls.Remove(pnl_remv);
            al_pge.RemoveAt(index);
        }

        public void open_out(string File_, bool usermde)
        {
            try
            {
                File_addr = File_;
                this.Text = this.Text + " : " + File_addr;
                opn();
                cb_usr.Checked = user_mode;
                if (sex.Contains("~AM-!~!Password!~!("))
                {
                    this.Enabled = false;
                    Doc_stdio_canopner cann = new Doc_stdio_canopner();
                    cann.pass(sex, this);
                }
            }
            catch (Exception erty)
            {
                Am_err ner = new Am_err();
                ner.tx(erty.Message);
            }
            try
            {
                this.BringToFront();
            }
            catch (Exception erty) { }
        }

        public void pass(string Passs)
        {
            PassWord = Passs;
            cb_usr.Checked = true;
            cb_usr.Enabled = false;
            this.Enabled = true;
            if (PassWord == "")
            {
                cb_usr.Enabled = true;
                cb_usr.Checked = false;
            }
            EventArgs e = new EventArgs();
            svebtn_ButtonClick(this, e);
        }

        private void connlbl_MouseEnter(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = Properties.Resources.bannrrageconv;
        }

        private void connlbl_MouseLeave(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = null;
        }

        private void conn_State(bool yn, int type)
        {
            if (yn == true && type == 0)
            {
                connlbl.Image = Properties.Resources.conncted;
                connlbl.Text = "Connected To a File on This Computer";
                nds_.Text = "Connected To '" + File_addr + "'";
            }
        }

        private string File_addr; string sex;
        Panel pge_temp = new Panel();
        private void opn()
        {
            ControlBox = false;
            try
            {
                pg.Visible = true;
                pg.Increment(5);
                FileStream fs = new FileStream(File_addr + ".~bp_dump", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                conn_State(true, 0);
                string Text_ = sr.ReadToEnd();
                sex = Text_;
                fs.Flush();
                sr.Close();
                fs.Close();

                string Final_ = "";
                int ndx = 0;
                int ndx2 = 1;
                int cnt_ndx = 0;
                int cnt_ndx2 = 0;
                int typ_ndx;
                Control C_type;
                pg.Increment(10);

                    int ndx3 = Text_.IndexOf("[P:", 0);
                    int ndx4 = Text_.IndexOf("/P]", ndx3);
                    string s = Text_;

                s = s.Remove(0, ndx3 + 3);
                s = s.Remove(ndx4 - 3);
                int Page = Convert.ToInt32(s);
                int tempy = 1;
                foreach (char c in Text_)
                {
                    try
                    {
                        Final_ = "";
                        try
                        {
                            ndx = Text_.IndexOf("<PAGE", ndx);
                            ndx2 = Text_.IndexOf("</PAGE", ndx2);
                            pge_temp = (Panel)al_pge[al_pge.Count - 1];
                            tempy++;
                            if (tempy <= Page)
                            {
                                new_pge();
                                //Page_Color(ndx, Text_.Length, sex);
                            }
                        }
                        catch (Exception erty) { break; }

                        char[] c_ = new char[Text_.Length];
                        Text_.CopyTo(ndx, c_, 0, ndx2 - ndx);
                        Final_ = new string(c_);
                        cnt_ndx = 0;
                        cnt_ndx2 = 0;
                        foreach (char cn in Final_)
                        {
                            try
                            {
                                cnt_ndx = Final_.IndexOf("<CONTROL", cnt_ndx);
                                cnt_ndx2 = Final_.IndexOf("</CONTROL", cnt_ndx);
                                typ_ndx = Final_.IndexOf("<TYPE[", cnt_ndx);

                                C_type = Type_(typ_ndx, Final_);
                                int lox = X_loc(typ_ndx, Final_);
                                int loy = Y_loc(typ_ndx, Final_);
                                int szx = X_sze(typ_ndx, Final_);
                                int szy = Y_sze(typ_ndx, Final_);

                                if (C_type.GetType() == typeof(TextBox) || C_type.GetType() == typeof(Panel))
                                {
                                    C_type = border(typ_ndx, Final_, C_type);
                                }
                                
                                try
                                {
                                    C_type.Font = Font_(typ_ndx, Final_);
                                }
                                catch (Exception erty2) { }
                                try
                                {
                                    C_type.Font = Font_sze(typ_ndx, Final_, C_type.Font);
                                }
                                catch (Exception erty3) { }
                                try
                                {
                                    C_type.Font = BIU_(typ_ndx, Final_, C_type.Font);
                                }
                                catch (Exception erty4) { }
                                try
                                {
                                    CheckBox c__ = new CheckBox();
                                    CheckBox d;
                                    C_type = (CheckBox)C_type;
                                    c__ = (CheckBox)C_type;
                                    c__.Checked = checker(typ_ndx, Final_);
                                    d = (CheckBox)C_type;
                                    C_type = c__;
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    C_type.BackColor = B_color(typ_ndx, Final_);
                                    C_type.ForeColor = F_color(typ_ndx, Final_);
                                }
                                catch (Exception erty1) { Am_err neri = new Am_err(); neri.tx("An Error Occured While Opening the Document '" + File_addr + "', the Document May Be Corrupted or not Be Compatible With this Version of Amatrix Document Studio."); }
                                try
                                {
                                    C_type.Text = Text_st(typ_ndx, Final_);
                                }
                                catch (Exception erty5) { }
                                try
                                {
                                    C_type.Size = new Size(szx, szy);
                                }
                                catch (Exception erty6) { }
                                try
                                {
                                    C_type.Location = new Point(lox, loy);
                                }
                                catch (Exception erty7) { }
                                try
                                {
                                    C_type.BackgroundImage = img_cnt(typ_ndx, Final_);
                                    C_type.BackgroundImageLayout = ImageLayout.Zoom;
                                }
                                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
                                pge_temp.Controls.Add(C_type);
                                C_type.BringToFront();
                                cnt_ndx++;
                                cnt_ndx2++;
                            }
                            catch (Exception erty)
                            {
                                break;
                            }
                        }
                        ndx = ndx2;
                        ndx2++;
                    }
                    catch (Exception erty) { break; }
                }
                pg.Increment(85);
                pg.Visible = false;
                pg.Value = 0;
                pnl1.Select();
            }
            catch (Exception erty) { MessageBox.Show(erty.Message); }
            try
            {
                File.Delete(File_addr + ".~bp_dump");
            }
            catch (Exception erty) { }
            ControlBox = true;
        }

        private Color Page_Color(int ndx1, int ndx2, string Operate)
        {
            int ndx3 = Operate.IndexOf("PAGE COL", ndx1);
            int ndx4 = Operate.IndexOf("]PAGE COL", ndx1);

            //Operate = Operate.Remove(ndx4);
            //Operate = Operate.Remove(ndx3);
            MessageBox.Show(Operate);
            return Color.Black;
        }

        private Control Type_(int Index, String Operate)
        {
            Control temp_cnt = new Control();
            RichTextBox rt = new RichTextBox();
            temp_cnt = (Control)rt;

            int remove = Operate.IndexOf("TYPE>", Index);
            try
            {
                Operate = Operate.Remove(remove);
                int ndxt = Operate.IndexOf("<TYPE", Index);
                Operate = Operate.Remove(0, ndxt);
            }
            catch (Exception erty) { }
            if (Operate.ToLower().Contains("label"))
            {
                Label r = new Label();
                r.AutoSize = true;
                r.ContextMenuStrip = cms;
                r.Click += new EventHandler(Click_);
                r.MouseDown += new MouseEventHandler(MouseDown_);
                r.MouseUp += new MouseEventHandler(MouseUp_);
                r.MouseEnter += new EventHandler(lbl_MouseEnter);
                r.MouseLeave += new EventHandler(lbl_MouseLeave);
                r.SizeChanged += new EventHandler(lbl_SizeChanged);
                r.LocationChanged += new EventHandler(lbl_SizeChanged);
                temp_cnt = (Control)r;
                return r; 
            }
            if (Operate.ToLower().Contains("datetimepicker"))
            {
                DateTimePicker dtp = new DateTimePicker();
                dtp.ContextMenuStrip = cms;
                dtp.Click += new EventHandler(Click_);
                dtp.MouseDown += new MouseEventHandler(MouseDown_);
                dtp.MouseUp += new MouseEventHandler(MouseUp_);
                dtp.MouseEnter += new EventHandler(lbl_MouseEnter);
                dtp.MouseLeave += new EventHandler(lbl_MouseLeave);
                dtp.SizeChanged += new EventHandler(lbl_SizeChanged);
                dtp.LocationChanged += new EventHandler(lbl_SizeChanged);
                temp_cnt = (Control)dtp;
                return dtp;
            }
            if (Operate.ToLower().Contains("datagridview"))
            {
                DataGridView dgvr = new DataGridView();
                dgvr.ContextMenuStrip = cms;
                dgvr.Click += new EventHandler(Click_);
                dgvr.MouseDown += new MouseEventHandler(MouseDown_);
                dgvr.MouseUp += new MouseEventHandler(MouseUp_);
                dgvr.MouseEnter += new EventHandler(lbl_MouseEnter);
                dgvr.MouseLeave += new EventHandler(lbl_MouseLeave);
                dgvr.SizeChanged += new EventHandler(lbl_SizeChanged);
                dgvr.LocationChanged += new EventHandler(lbl_SizeChanged);
                temp_cnt = (Control)dgvr;
                return dgvr;
            }
            if (Operate.ToLower().Contains("checkbox"))
            {
                CheckBox cbx = new CheckBox();
                cbx.AutoSize = true;
                cbx.Text = "Enter Text Here";
                cbx.Click += new EventHandler(Click_);
                cbx.MouseDown += new MouseEventHandler(MouseDown_);
                cbx.MouseUp += new MouseEventHandler(MouseUp_);
                cbx.MouseEnter += new EventHandler(lbl_MouseEnter);
                cbx.MouseLeave += new EventHandler(lbl_MouseLeave);
                cbx.ContextMenuStrip = cms;
                temp_cnt = (Control)cbx;
                return cbx;
            }
            if (Operate.ToLower().Contains("textbox"))
            {
                TextBox tbx = new TextBox();
                tbx.BorderStyle = BorderStyle.FixedSingle;
                tbx.Multiline = true;
                tbx.ContextMenuStrip = cms;
                tbx.Click += new EventHandler(Click_);
                tbx.MouseDown += new MouseEventHandler(MouseDown_);
                tbx.MouseUp += new MouseEventHandler(MouseUp_);
                tbx.MouseEnter += new EventHandler(lbl_MouseEnter);
                tbx.MouseLeave += new EventHandler(lbl_MouseLeave);
                tbx.SizeChanged += new EventHandler(lbl_SizeChanged);
                tbx.LocationChanged += new EventHandler(lbl_SizeChanged);
                temp_cnt = (Control)tbx;
                return tbx;
            }
            if (Operate.ToLower().Contains("panel"))
            {
                Panel pbx = new Panel();
                pbx.BackgroundImage = Properties.Resources.pict;
                pbx.BackgroundImageLayout = ImageLayout.Center;
                pbx.BorderStyle = BorderStyle.FixedSingle;
                pbx.ContextMenuStrip = cms;
                pbx.Click += new EventHandler(Click_);
                pbx.MouseDown += new MouseEventHandler(MouseDown_);
                pbx.MouseUp += new MouseEventHandler(MouseUp_);
                pbx.MouseEnter += new EventHandler(lbl_MouseEnter);
                pbx.MouseLeave += new EventHandler(lbl_MouseLeave);
                pbx.SizeChanged += new EventHandler(lbl_SizeChanged);
                pbx.LocationChanged += new EventHandler(lbl_SizeChanged);
                temp_cnt = (Control)pbx;
                return pbx;
            }
            return temp_cnt;
        }

        private Control border(int Index, string Operate, Control cunt)
        {
            try
            {
                int remove = Operate.IndexOf("<BORDER[", Index);
                int end = Operate.IndexOf("]BORDER>", Index);
                try
                {
                    Operate = Operate.Remove(end);
                    Operate = Operate.Remove(0, remove + 7);
                }
                catch (Exception erty)
                { }
            }
            catch (Exception erty) { }

            if (cunt.GetType() == typeof(TextBox))
            {
                txt_border = (TextBox)cunt;
                if(Operate.Contains(BorderStyle.FixedSingle.ToString()))
                {
                    txt_border.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (Operate.Contains(BorderStyle.None.ToString()))
                {
                    txt_border.BorderStyle = BorderStyle.None;
                }
                cunt = (Control)txt_border;
            }
            else if (cunt.GetType() == typeof(Panel))
            {
                pictbx_border = (Panel)cunt;
                if (Operate.Contains(BorderStyle.FixedSingle.ToString()))
                {
                    pictbx_border.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (Operate.Contains(BorderStyle.None.ToString()))
                {
                    pictbx_border.BorderStyle = BorderStyle.None;
                }
                cunt = (Control)pictbx_border;
            }
            return cunt;
        }

        private bool checker(int Index, string Operate)
        {
            bool b = false;
            int remove = Operate.IndexOf("<CHECK[", Index);
            int end = Operate.IndexOf("]", remove);
            try
            {
                if (end - remove == 6)
                {
                    Operate = "";
                }
                else
                {
                    Operate = Operate.Remove(end);
                    Operate = Operate.Remove(0, remove + 7);
                }
            }
            catch (Exception erty) { }
            if (Operate.ToLower() == "true")
            {
                b = true;
            }
            else { b = false; }
            return b;
        }

        private Image img_cnt(int Index, string Operate)
        {
            int remove = Operate.IndexOf("<IMAGE[", Index);
            int end = Operate.IndexOf("]IMAGE>", Index);
            try
            {
                Operate = Operate.Remove(end);
                Operate = Operate.Remove(0, remove + 7);
            }
            catch (Exception erty)
            { }
            if (Operate == "Noimg")
            {
                Image n = null;
                return n;
            }

            byte[] res1 = Convert.FromBase64String(Operate);
            Image newImage;
            using (MemoryStream ms = new MemoryStream(res1, 0, res1.Length))
            {
                newImage = Bitmap.FromStream(ms, true);
                ms.Flush();
                ms.Close();
                ms.Dispose();
            }
            return newImage;
        }

        private int X_loc(int Index, String Operate)
        {
            int rt = 0;
            int remove = Operate.IndexOf("<LOCATION", Index);
            int end = Operate.IndexOf(",", remove);
            try
            {
                Operate = Operate.Remove(end);
                Operate = Operate.Remove(0, remove + 10);
            }
            catch (Exception erty) { }
            rt = Convert.ToInt32(Operate);
            return rt;
        }

        
        private int Y_loc(int Index, string Operate)
        {
            int rt = 0;
            int remove = Operate.IndexOf("<LOCATION", Index);
            int middle = Operate.IndexOf("LOCATION>", Index);
            int end = Operate.IndexOf(",", remove);
            try
            {
                Operate = Operate.Remove(middle - 1);
                Operate = Operate.Remove(0, end + 1);
            }
            catch (Exception erty) { }
            rt = Convert.ToInt32(Operate);
            return rt;
        }

        
        private int X_sze(int Index, String Operate)
        {
            int rt = 0;
            int remove = Operate.IndexOf("<SIZE", Index);
            int end = Operate.IndexOf(",", remove);
            try
            {
                Operate = Operate.Remove(end);
                Operate = Operate.Remove(0, remove + 6);
            }
            catch (Exception erty) { }
            rt = Convert.ToInt32(Operate);
            return rt;
        }

        private int Y_sze(int Index, String Operate)
        {
            int rt = 0;
            int remove = Operate.IndexOf("<SIZE", Index);
            int middle = Operate.IndexOf("SIZE>", Index);
            int end = Operate.IndexOf(",", remove);
            try
            {
                Operate = Operate.Remove(middle - 1);
                Operate = Operate.Remove(0, end + 1);
            }
            catch (Exception erty) { }
            rt = Convert.ToInt32(Operate);
            return rt;
        }
        
        private string Text_st(int Index, String Operate)
        {
            int remove = Operate.IndexOf("<TEXT[", Index);
            int end = Operate.IndexOf("]", remove);
            try
            {
                if (end - remove == 6)
                {
                    Operate = "";
                }
                else
                {
                    Operate = Operate.Remove(end);
                    Operate = Operate.Remove(0, remove + 6);
                }
            }
            catch (Exception erty) { }
            return Operate;
        }

        private Font Font_(int Index, String Operate)
        {
            int remove = Operate.IndexOf("<FONT[", Index);
            int end = Operate.IndexOf("]", remove);
            try
            {
                Operate = Operate.Remove(end);
                Operate = Operate.Remove(0, remove + 6);
            }
            catch (Exception erty) { }
            Font fnt = new Font(Operate, 8f);
            return fnt;
        }

        private Font Font_sze(int Index, String Operate, Font f)
        {
            int remove = Operate.IndexOf("<FONT_SIZE[", Index);
            int end = Operate.IndexOf("]", remove);
            try
            {
                Operate = Operate.Remove(end);
                Operate = Operate.Remove(0, remove + 11);
            }
            catch (Exception erty) { }
            Double d = Convert.ToInt32(Operate);
            float fsze = (float)d;
            Font fnt = new Font(f.FontFamily.GetName(0), fsze);
            return fnt;
        }

        private Font BIU_(int Index, String Operate, Font fnn)
        {
            int remove = Operate.IndexOf("<BIU[", Index);
            int end = Operate.IndexOf("]", remove);
            try
            {
                Operate = Operate.Remove(end);
                Operate = Operate.Remove(0, remove + 5);
            }
            catch (Exception erty) { }

            FontStyle f = new FontStyle();
            f = FontStyle.Regular;
            if (Operate.ToLower().Contains("bold") == true)
            {
                f |= FontStyle.Bold;
            }
            if (Operate.ToLower().Contains("italic") == true)
            {
                f |= FontStyle.Italic;
            }
            if (Operate.ToLower().Contains("underline") == true)
            {
                f |= FontStyle.Underline;
            }
            Font fn = new Font(fnn.FontFamily.GetName(0), fnn.Size, f);
            return fn;
        }

        private Color B_color(int Index, String Operate)
        {
            int remove = Operate.IndexOf("<BACKCOLOR[", Index);
            int end = Operate.IndexOf("]", remove);
            try
            {
                Operate = Operate.Remove(end);
                Operate = Operate.Remove(0, remove + 11);
            }
            catch (Exception erty) { }
            Color c = Color.FromName(Operate);
            return c;
        }

        private Color F_color(int Index, String Operate)
        {
            int remove = Operate.IndexOf("<FORECOLOR[", Index);
            int end = Operate.IndexOf("]", remove);
            try
            {
                Operate = Operate.Remove(end);
                Operate = Operate.Remove(0, remove + 11);
            }
            catch (Exception erty) { }
            Color c = Color.FromName(Operate);
            return c;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        private void pge_Tick(object sender, EventArgs e)
        {
            pges.Text = "Pages : " + al_pge.Count.ToString();
        }

        bool user_mode = false;
        private void cb_usr_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_usr.Checked == false)
            {
                user_mode = false;
                toolStrip1.Enabled = true;
                toolStripButton4.Enabled = true;
                toolStripDropDownButton1.Enabled = true;
                editToolStripMenuItem1.Enabled = true;
                panel1.Enabled = true;
                re_only.Visible = false;
                itm_txt.Enabled = true;
                nw_pge.Enabled = true;
            }
            else if (cb_usr.Checked == true)
            {
                user_mode = true;
                toolStrip1.Enabled = false;
                editToolStripMenuItem1.Enabled = false;
                toolStripButton4.Enabled = false;
                nw_pge.Enabled = false;
                panel1.Enabled = false;
                itm_txt.Enabled = false;
                re_only.Visible = true;
                toolStripDropDownButton1.Enabled = false;
            }
        }

        private void itm_txt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cntr_temp.Text = itm_txt.Text;
            }
            catch (Exception erty) { }
        }

        private void pages_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //Edit Menu
        Control cpy_cntrl, cpy_cntrl2;
        private void cpy_Click(object sender, EventArgs e)
        {
            cpy_cntrl2 = (Control)cntr_temp;
            editToolStripMenuItem1.HideDropDown();
        }

        private void pster_Click(object sender, EventArgs e)
        {
            try
            {
                cpy_cntrl = (Label)cpy_cntrl2;
                lblcrt(true);
            }
            catch (Exception erty) { }
            try
            {
                cpy_cntrl = (Panel)cpy_cntrl2;
                piccrt(true);
            }
            catch (Exception erty) { }
            try
            {
                cpy_cntrl = (CheckBox)cpy_cntrl2;
                chkbxcrt(true);
            }
            catch (Exception erty) { }
            try
            {
                cpy_cntrl = (TextBox)cpy_cntrl2;
                txtldcrt(true);
            }
            catch (Exception erty) { }
            try
            {
                cpy_cntrl = (DateTimePicker)cpy_cntrl2;
                dtetmecrt(true);
            }
            catch (Exception erty) { }
            editToolStripMenuItem1.HideDropDown();
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
                cpy_cntrl2 = (Control)cntr_temp;
                cntr_temp.Dispose();
            }
            catch (Exception erty) { }
        }

        private void connlbl_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app_abt bt = new app_abt();
            bt.descr("Document Studio");
        }

        private void decjourn_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                decjourn.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void Doc_stdio_Activated(object sender, EventArgs e)
        {
            try
            {
                decjourn.Stop();
            }
            catch (Exception tex)
            {
            }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void Doc_stdio_Deactivate(object sender, EventArgs e)
        {
            decjourn.Start();
        }

        private void ofd_img_FileOk(object sender, CancelEventArgs e)
        {
            cntr_temp.BackgroundImageLayout = ImageLayout.Zoom;
            cntr_temp.BackgroundImage = Image.FromFile(ofd_img.FileName);
        }

        private void selectAnImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd_img.ShowDialog();
        }

        int ndx_print = 0;
        private void p_doc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if(ndx_print >= pnl_print_no)
            {
                e.HasMorePages = false;
            }
            else
            {
                e.Graphics.DrawImage(Image.FromFile(al[ndx_print].ToString()), 0, 0);
                CreateGraphics();
                e.HasMorePages = true;
                ndx_print++;
            }
            e.Graphics.Dispose();
        }

        string Print_path; int pnl_print_no; ArrayList al = new ArrayList();
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ndx_print = 0;
            pnl_print_no = 0;
            toolStrip1.Visible = false; itm_txt.Visible = false;
            al.Clear();
            DirectoryInfo fnf = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            Print_path = fnf.FullName + "\\Amatrix User Folder\\";
            if (Directory.Exists(Print_path) == false) { Directory.CreateDirectory(Print_path); }
            fnf = new DirectoryInfo(Print_path);
            FileInfo[] fnf_ = fnf.GetFiles();

            foreach (FileInfo f in fnf_)
            {
                f.Delete();
            }

            int i = 0;
            Panel tmp_print_pnl;
            foreach (Control p in pnl_journvw.Controls)
            {
                try
                {
                    tmp_print_pnl = (Panel)p;
                    tmp_print_pnl.BorderStyle = BorderStyle.None;
                    Bitmap bmp = new Bitmap(p.Size.Width, p.Size.Height);
                    p.DrawToBitmap(bmp, new Rectangle(0, 0, p.Size.Width, p.Size.Height));
                    bmp.Save(Print_path + "Page" + i.ToString() + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                    al.Add(Print_path + "Page" + i.ToString() + ".bmp");
                    tmp_print_pnl.BorderStyle = BorderStyle.FixedSingle;
                    i++;
                }
                catch (Exception erty) { }
            }
            pnl_print_no = i;
            if (sender.Equals(printPreviewToolStripMenuItem) == true)
            {
                Print_preview ppv = new Print_preview();
                ppv.tx(p_doc, pnl_print_no);
            }
            else
            {
                p_doc.Print();
            }
            toolStrip1.Visible = true; itm_txt.Visible = true;
        }

        void pp_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                DirectoryInfo fnf = new DirectoryInfo(Print_path);
                FileInfo[] fnf_ = fnf.GetFiles();

                foreach (FileInfo f in fnf_)
                {
                    f.Delete();
                }
            }
            catch (Exception erty) { }
        }

        private void clse_MouseEnter(object sender, EventArgs e)
        {
            clsejourn.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void clse_MouseLeave(object sender, EventArgs e)
        {
            clsejourn.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void clsejourn_ButtonClick(object sender, EventArgs e)
        {
            tmeclse.Start();
        }

        private void tmeclse_Tick(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void rstrt2_Click(object sender, EventArgs e)
        {
            Doc_stdio dc = new Doc_stdio();
            dc.Show();
            this.Close();
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.tx(this.Name);
        }

        private void moveToAnotherPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drag.Start();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Doc_stdio_secs secs = new Doc_stdio_secs();
            secs.tx(PassWord, this);
        }

        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cntr_temp.BringToFront();
            }
            catch (Exception eryty) { }
        }

        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cntr_temp.SendToBack();
            }
            catch (Exception ettyy) { }
        }

        private void deletePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Panel pnl_delpge = (Panel)al_pge[Convert.ToInt32(pnl_tmt.Tag)];
            try
            {
                pnl_delpge.Controls.Remove(trackBar1);
            }
            catch (Exception erty) { }
            try
            {
                pnl_delpge.Controls.Remove(trackBar2);
            }
            catch (Exception erty) { }
            pnl_delpge.Dispose();
            al_pge.RemoveAt(Convert.ToInt32(pnl_tmt.Tag));
        }

        private void page1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnl1.Select();
            pnl_tmt = pnl1;
        }

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_tmt.Controls.Clear();
            }
            catch (Exception erty) { }
        }

        //gc.cleanup();

        TextBox txt_border; Panel pictbx_border;
        private void borderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cntr_temp.GetType() == typeof(TextBox))
            {
                txt_border = (TextBox)cntr_temp;
                txt_border.BorderStyle = BorderStyle.FixedSingle;
            }
            if (cntr_temp.GetType() == typeof(Panel))
            {
                pictbx_border = (Panel)cntr_temp;
                pictbx_border.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void noBorderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cntr_temp.GetType() == typeof(TextBox))
            {
                txt_border = (TextBox)cntr_temp;
                txt_border.BorderStyle = BorderStyle.None;
            }
            if (cntr_temp.GetType() == typeof(Panel))
            {
                pictbx_border = (Panel)cntr_temp;
                pictbx_border.BorderStyle = BorderStyle.None;
            }
        }

        private void oneColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("This Clears the Selected Page and puts the System in User Mode for Text Editing. Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                pge_temp.Controls.Clear();
                if (sender.Equals(oneColumnToolStripMenuItem) == true)
                {
                    txtldcrt(false);
                    drag.Stop();
                    cntr_temp.Location = new Point(100, 80);
                    cntr_temp.Size = new Size(pnl_tmt.Size.Width - 200, pnl_tmt.Size.Height - 160);
                }

                if (sender.Equals(twoColumnsToolStripMenuItem) == true)
                {
                    txtldcrt(false);
                    drag.Stop();
                    cntr_temp.Location = new Point(95, 80);
                    cntr_temp.Size = new Size((pnl_tmt.Size.Width / 2) - 100, pnl_tmt.Size.Height - 160);

                    txtldcrt(false);
                    drag.Stop();
                    cntr_temp.Location = new Point((pnl_tmt.Size.Width / 2) + 5, 80);
                    cntr_temp.Size = new Size((pnl_tmt.Size.Width / 2) - 100, pnl_tmt.Size.Height - 160);
                }

                if (sender.Equals(threeColumnsToolStripMenuItem) == true)
                {
                    txtldcrt(false);
                    drag.Stop();
                    cntr_temp.Location = new Point(95, 80);
                    cntr_temp.Size = new Size((pnl_tmt.Size.Width / 2) - 240, pnl_tmt.Size.Height - 160);

                    txtldcrt(false);
                    drag.Stop();
                    cntr_temp.Location = new Point((pnl_tmt.Size.Width / 2) - 92, 80);
                    cntr_temp.Size = new Size((pnl_tmt.Size.Width / 2) - 230, pnl_tmt.Size.Height - 160);

                    txtldcrt(false);
                    drag.Stop();
                    cntr_temp.Location = new Point((pnl_tmt.Size.Width / 2) + 140, 80);
                    cntr_temp.Size = new Size((pnl_tmt.Size.Width / 2) - 230, pnl_tmt.Size.Height - 160);
                }

                if (sender.Equals(letterLayoutToolStripMenuItem) == true || sender.Equals(pickATemplateToolStripMenuItem) == true)
                {
                    doc_stdio_Page_stle pget = new doc_stdio_Page_stle();
                    pget.Show();
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        string Co_nfo, Co_Tel, Co_Fax, Co_Addr; DataTable dtp = new DataTable();
        private void bkk_nfo_DoWork(object sender, DoWorkEventArgs e)
        {
            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
            SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM co_nfo", conn);
            conn.Open();
            SqlCeDataReader dr = cmd.ExecuteReader();
            dtp.Load(dr);
            conn.Close();

            try
            {
                Co_nfo = dtp.Rows[0].ItemArray[0].ToString();
                if(Co_nfo == "" || Co_nfo == null)
                {
                    Co_nfo = "Company Name";
                }
            }
            catch (Exception ertyt) { }

            try
            {
                Co_Tel = "Tel: " +dtp.Rows[0].ItemArray[2].ToString();
            }
            catch (Exception ertyt) { Co_Tel = "Tel: N.A."; }
        }

        private void bkk_nfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Panel p_temp = (Panel)al_pge[0];
            foreach (Control c in p_temp.Controls)
            {
                if (c.Text == "Company Name")
                {
                    c.Text = Co_nfo;
                }
            }
        }

        //new
        private void changefont(object sender, EventArgs e)
        {
            if (sender.Equals(button2) == true)
            {
                if (toolStripButton2.Checked == true)
                {
                    toolStripButton2.Checked = false;
                }
                else { toolStripButton2.Checked = true; }
            }

            if (sender.Equals(button6) == true)
            {
                if (toolStripButton3.Checked == true)
                {
                    toolStripButton3.Checked = false;
                }
                else { toolStripButton3.Checked = true; }
            }

            if (sender.Equals(button5) == true)
            {
                if (toolStripButton5.Checked == true)
                {
                    toolStripButton5.Checked = false;
                }
                else { toolStripButton5.Checked = true; }
            }

            Fntch(sender, e);
        }

        private void changeit(object sender, EventArgs e)
        {
            toolStripComboBox1.Text = comboBox1.Text;
        }

        private void changeit_size(object sender, EventArgs e)
        {
            toolStripComboBox2.Text = comboBox2.Text;
        }

        //remove a page
        private void button12_Click(object sender, EventArgs e)
        {
            if (pnl_tmt != pnl1)
            {
                int ndx = al_pge.IndexOf(pnl_tmt);

                foreach (Control c in ((ArrayList)ak_pge_itms[ndx - 1]))
                {
                    c.Dispose();
                }

                pnl_journvw.Controls.Remove(pnl_tmt);

                //shift up and change labelling

            }
            else { MessageBox.Show("Deleting the First page is not Permitted, You may However right Click on the page and click on 'Delete All on this Page'", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void toolStripButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripButton2.Checked == false)
            {
                button2.BackColor = Color.White;
            }
            else
            {
                button2.BackColor = Color.Orange;
            }
        }

        private void toolStripButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripButton3.Checked == false)
            {
                button6.BackColor = Color.White;
            }
            else
            {
                button6.BackColor = Color.Orange;
            }
        }

        private void toolStripButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripButton5.Checked == false)
            {
                button5.BackColor = Color.White;
            }
            else
            {
                button5.BackColor = Color.Orange;
            }
        }

        private void comboBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5,
                                rect.Width - 10, rect.Height - 10);
            }
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            toolStripComboBox3.Text = comboBox4.Text;
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            toolStripComboBox5.Text = comboBox3.Text;
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f;
                try
                {
                    f = new Font(n, 9);
                }
                catch (Exception erty)
                {
                    f = new Font("Arial", 9, FontStyle.Regular);
                }
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
            }
        }

        /*public void PDf()
        {
            print();
            string attachment = "attachment; filename=ApplicationForm.pdf";

            Response.ClearContent();

            Response.AddHeader("content-disposition", attachment);

            Response.ContentType = "application/pdf";

            StringWriter stw = new StringWriter();

            HtmlTextWriter htextw = new HtmlTextWriter(stw);

            pnl1.RenderControl(htextw);

            Document document = new Document();

            PdfWriter.GetInstance(document, Response.OutputStream);

            document.Open();

            StringReader str = new StringReader(stw.ToString());

            HTMLWorker htmlworker = new HTMLWorker(document);

            htmlworker.Parse(str);

            document.Close();

            Response.Write(document);

            Response.End();
        }*/
    }
}
