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
using System.Collections;
using System.ComponentModel;
using System.Data.SqlServerCe;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using Base_ASQL;

namespace Main
{
    public partial class acc_ledg : Form
    {
        private string whtt;
        private string wter;
        private int winwin = 0;

        private ArrayList aund = new ArrayList();
        private ArrayList aundC = new ArrayList();
        private ArrayList aundR = new ArrayList();

        //Recognition
        private int winstatus = 0;

        //Cross Threading Objects
        private Thread thinit;
        private delegate void delinit();

        private Thread thinit2;
        private delegate void delinit2();

        Base_ASQL.BASQL basql = new BASQL();
       
        //dbm classes

        public acc_ledg()
        {
            this.Icon = Properties.Resources.amdsicnico;
            InitializeComponent();
            try
            {
                tmeinit.Start();
            }
            catch (Exception erty) { Init(); }
            connctyes();
            this.Disposed += new EventHandler(acc_ledg_Disposed);
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

        //Initializer Method__________________________________________________________________________________________________________

        //int main
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

        private int srry = 0;
        private void initconn()
        {
            try
            {
                connlbl.Text = "Establishing Hybrid Connection..";
                if (Main.Amatrix.acc == "")
                {
                    ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                    SqlString = "Select * From CashBook";
                    using (conn_public = new SqlCeConnection(ConnString))
                    {
                        using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                        {
                            conn_public.Open();
                        }
                    }
                    conn_public.Close();
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Connected (Hybrid Mode)";
                }
                else
                {
                    connlbl.Text = "Establishing Hybrid Connection..";
                    DataTable dtp = new DataTable();
                    dtp = basql.Execute(Main.Amatrix.acc, "Select * FROM CashBook", "CashBook", dtp);
                    connlbl.Text = "Connected (Hybrid Mode)";
                }
            }
            catch (Exception erty) { connlbl.Text = "Unable to Establish Connection"; connlbl.Image = Properties.Resources.connctno; }
        }

        private void  Init()
        {
            pnl_journvw.Controls.Remove(gadg_resz1);
            gadg_resz1.Dock = DockStyle.Fill;
            clse.Image = Properties.Resources.extfin;
            clse3.Image = Properties.Resources.extfin;
            remv_zz.Image = Properties.Resources.extfin;
            clseall.Image = Properties.Resources.extfin;
            clsettry.Image = Properties.Resources.extfin;
            clsettry2.Image = Properties.Resources.extfin;
            clsemn.Image = Properties.Resources.extfin;

            if (Properties.Settings.Default.defrgb == 1)
            {
                tswin.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);
                tswin3.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);

                tsttl.ForeColor = Color.FromArgb(Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb);
                tsttl2.ForeColor = Color.FromArgb(Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb);
            }

            cnbnn1.AllowTransparency = true;
            cnbnn1.Opacity = 0.90;
            cmsvwslv.AllowTransparency = true;
            cmsvwslv.Opacity = 0.90;
            cmsvwslv.BackgroundImage = Properties.Resources.app;
            cmsvwslv.BackgroundImageLayout = ImageLayout.Stretch;
            cmsvwslv.ForeColor = Color.WhiteSmoke;

            zz_yn(false);

            cnbnn3.AllowTransparency = true;
            cnbnn3.Opacity = 0.90;
            cmslv.AllowTransparency = true;
            cmslv.Opacity = 0.90;
            clsejourn.Text = "Close";
            this.Disposed += new EventHandler(jrndip);

            /*if (choicesett.Default.tpmst == true)
            {
                this.TopMost = true;
            }
            else if (choicesett.Default.tpmst == false)
            {
                this.TopMost = false;
            }*/
            //init2
            x = dgvwin.Size.Width;
            y = dgvwin.Size.Height;

            x3 = summpnl.Size.Width;
            y3 = summpnl.Size.Height;

        }

        private void reprts()
        {
            SqlCeConnection conn;
            if (Main.Amatrix.acc == "")
            {
                conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                SqlCeCommand cmm = new SqlCeCommand("SELECT sum(credit) FROM SalesBook WHERE [Date of Sale] > '09/10/91'", conn);
                using (conn)
                {
                    conn.Open();
                    SqlCeDataReader dr = cmm.ExecuteReader();

                    using (dr)
                    {
                        DataSet dtst = new DataSet();
                        dtst.Load(dr, LoadOption.PreserveChanges, "SalesBook");
                    }
                }
            }
            else
            {
                dttp.Clear();
                dttp = basql.Execute(Main.Amatrix.acc, "SELECT sum(credit) FROM SalesBook WHERE [Date of Sale] > '09/10/91'", "SalesBook", dttp);
            }
        }

        DataTable dttp = new DataTable();
        private Double smm1;
        private Double smm2;
        //private int resjrn;
        private Thread th_sum;
        private delegate void del_sum();
        private void SUMS()
        {
            th_sum = new Thread(new ThreadStart(sums_delstrt));
            th_sum.IsBackground = true;
            th_sum.Start();
        }

        private void sums_delstrt()
        {
            this.Invoke(new del_sum(sumdelgt));
        }

        private void sumdelgt()
        {    
            dst_END_public.Clear();
            if (Main.Amatrix.acc == "")
            {
                ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                SqlString = "Select sum(Credit), sum(Debit) FROM CashBook";
                using (conn_public = new SqlCeConnection(ConnString))
                {
                    using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                    {

                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        using (read_public)
                        {
                            dst_END_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView2.DataSource = dst_END_public;
                        }
                    }
                }
                try
                {
                    smm1 = Convert.ToDouble(dataGridView2[0, 0].Value);
                    smm2 = Convert.ToDouble(dataGridView2[1, 0].Value);
                    smm1 = smm2 - smm1;
                    //jrnrow.Text = smm1.ToString();
                }
                catch (Exception erty) { }
                //pbkk
                //ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                SqlString = "Select sum(Credit) FROM PurchaseBook";
                using (conn_public = new SqlCeConnection(ConnString))
                {
                    using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                    {

                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        using (read_public)
                        {
                            dst_END_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView2.DataSource = dst_END_public;
                        }
                    }
                }
                /*try
                {
                    toolStripLabel39.Text = dataGridView2[0, 1].Value.ToString();
                }
                catch (Exception erty) { }*/

                //slsbkk
                //ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                SqlString = "Select sum(Credit) FROM SalesBook";
                using (conn_public = new SqlCeConnection(ConnString))
                {
                    using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                    {

                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        using (read_public)
                        {
                            dst_END_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView2.DataSource = dst_END_public;
                        }
                    }
                }
                /*try
                {
                   // toolStripLabel14.Text = dataGridView2[0, 2].Value.ToString();
                }
                catch (Exception erty) { }*/
            }
            else
            {
                ConnString = Main.Amatrix.acc;
                dst_END_public = basql.Execute(ConnString, "Select sum(Credit), sum(Debit) FROM CashBook", "CashBook", dst_END_public);
                dst_END_public = basql.Execute(ConnString, "Select sum(Credit) FROM PurchaseBook", "PurchaseBook", dst_END_public);
                dst_END_public = basql.Execute(ConnString, "Select sum(Credit) FROM SalesBook", "SalesBook", dst_END_public);
                dataGridView2.DataSource = dst_END_public;
            }
        }
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        void jrndip(object sender, EventArgs e)
        {
            Properties.Settings.Default.journ_fleloc = "";
            Properties.Settings.Default.Journmod = "";
            Properties.Settings.Default.Save();
        }

        private void acc_ledg_Load(object sender, EventArgs e)
        {
        }

        private void pntclc(object sender, EventArgs e)
        {
            pd1.ShowDialog();
        }

        private void tmeclse_tc(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                tmeclse.Dispose();
                this.Close();
            }
        }

        private void clsejournclc(object sender, EventArgs e)
        {
            tmeclse.Start();
        }

        private void decjourn_tc(object sender, EventArgs e)
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

        private void acc_journ_dec(object sender, EventArgs e)
        {
            try
            {
                tmex.Stop();
                dgvwintic.Stop();
            }
            catch (Exception excu)
            { }
            decjourn.Start();
        }

        private void acc_journ_act(object sender, EventArgs e)
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

        private void clsejourn_me(object sender, EventArgs e)
        {
            clsejourn.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void clsejourn_ml(object sender, EventArgs e)
        {
            clsejourn.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void connlblme(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = Properties.Resources.bannrrageconv;
        }

        private void connlblml(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = null;
        }

        private void autsvetck(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.journrecovwhre = wter;
                Properties.Settings.Default.journrecovwht = whtt;
                Properties.Settings.Default.Save();
            }
            catch (Exception excautsvejrn)
            {
            }
        }

        private void gtedat(string wcre)
        {
        }

        private void dtegteelem(string gutternut, int fuck, int buck)
        {
            string accumus = "";
            fuck = fuck;
            while (fuck < buck - 1)
            {
                fuck++;
                accumus = accumus + gutternut[fuck];
            }
            sctnfordte(accumus);
        }

        private void sctnfordte(string tosct)
        {
            int ndx = 0;
            string facm = "";
            foreach (char nigd in tosct)
            {
                if (nigd == '|')
                {
                    ndx++;
                }
                else if (ndx == 1)
                {
                    facm = facm + nigd;
                    if (nigd == '|' && ndx == 1)
                    {
                        ndx++;
                    }
                }
                else if (ndx == 2)
                {
                    break;
                }
                else
                { }
            }
        }

        private int amdorflt = 0;
        
        private string nme = "";
        //open from out
        public void ouot(string glgu, string butr)
        {
        }

        private void setms(string gtefor)
        {
        }

        //open from out end

        //Read Columns

        private void dgvwintc(object sender, EventArgs e)
        {
            dvgpndoc.ToolTipText = "Maximize";
            buho = 0;
            dgvwin.Dock = DockStyle.None;
            dgvwin.Location = new Point((Cursor.Position.X - this.Location.X) - (dgvwin.Size.Width / 2), Cursor.Position.Y - this.Location.Y-60);
        }

        private void saveas2(object sender, EventArgs e)
        {
            saveas.ShowDialog();
        }

        private void svedskclc(object sender, EventArgs e)
        {
            sveacc.ShowDialog();
        }

        private void lv1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Text = "Key pressed ";
        }

        private void lvvs_Click(object sender, EventArgs e)
        {
            Point pms = new Point();
            pms.X = Cursor.Position.X;
            pms.Y = Cursor.Position.Y;
            cmsvwslv.Show(pms);
        }

        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            buho = 0;
            dgvwin.Dock = DockStyle.None;
            dgvwintic.Start();
        }

        private void toolStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            dgvwintic.Stop();
            dgvwintic3.Stop();
        }

        private void pnl_journvw_clc(object sender, EventArgs e)
        {
            try
            {
                tmex3.Stop();
                tmex.Stop();
            }
            catch (Exception ext)
            { }
            try
            {
                dgvwintic.Stop();
                dgvwintic3.Stop();
            }
            catch (Exception ext2)
            { }
        }

        int buho = 0;
        private void dvgpndoc_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dck();
        }

        private void dck()
        {
            if (buho == 0)
            {
                dvgpndoc.ToolTipText = "Window";
                dgvwin.Dock = DockStyle.Fill;
                buho = 1;
            }
            else if (buho == 1)
            {
                dvgpndoc.ToolTipText = "Maximize";
                dgvwin.Dock = DockStyle.None;
                buho = 0;
            }
            else { buho = 0; }
        }

        private void connlbl_Click(object sender, EventArgs e)
        {
            Point ptt = new Point();
            ptt.X = Cursor.Position.X - this.Location.X + 50;
            ptt.Y = Cursor.Position.Y - this.Location.Y;
            cmslv.Show(ptt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buho = 0;
            tmex.Start();
        }

        private void tmex_Tick(object sender, EventArgs e)
        {
            if (dgvwin.Controls.IndexOf(gadg_resz1) == -1)
            {
                dgvwin.Controls.Add(gadg_resz1);
            }
            gadg_resz1.Visible = true;
            gadg_resz1.BringToFront();
            dgvwin.Dock = DockStyle.None;
            dgvwin.Size = new Size(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y);
        }

        private void reszz_ButtonClick(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dvgpndoc.ToolTipText = "Maximize";
            buho = 0;
            tmex.Start();
        }

        private void stwin_Click(object sender, EventArgs e)
        {
            try
            {
                dgvwin.BringToFront();
                dgvwintic3.Stop();
                dgvwintic.Stop();
                tmex.Stop();
                tmex3.Stop();
            }
            catch (Exception exct) { }
        }

        private void tswin_DoubleClick(object sender, EventArgs e)
        {
            dgvwintic.Start();
        }

        private void tswin_Click(object sender, EventArgs e)
        {
            winstatus = 3;
            try
            {
                tmex.Stop();
                tmex3.Stop();
            }
            catch (Exception ett)
            { }
            try
            {
                dgvwintic.Stop();
                dgvwintic3.Stop();
            }
            catch (Exception ert) 
            { }
            winwin = 3;
            dgvwin.BringToFront();
            gadg_resz1.Visible = false;
            if (zz.Visible == true) { zz.BringToFront(); }
        }

        private void mvewin_ButtonClick(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwintic.Start();
        }
        //Window close
        int wozzy = 0;
        int x, y;
        private void clse_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            if (wozzy == 0)
            {
                sepmnu1.Visible = false;
                pos1mnu.Visible = false;
                maxminmnu1.Enabled = false;
                mxmn.Enabled = false;
                bn1jn.Visible = false;
                szejrn.Visible = false;
                reszz.Visible = false;
                mvewin.Visible = false;
                dvgpndoc.Visible = false;
                rszemnu.Enabled = false;
                tsjrnstat.Visible = false;
                x = dgvwin.Size.Width;
                y = dgvwin.Size.Height;
                dgvwin.Dock = DockStyle.None;
                dgvwin.Size = new Size(140, 48);
                wozzy = 1;
            }
            else if (wozzy == 1)
            {
                buho = 0;
                sepmnu1.Visible = true;
                rszemnu.Enabled = true;
                pos1mnu.Visible = true;
                maxminmnu1.Enabled = true;
                mxmn.Enabled = true;
                szejrn.Visible = true;
                reszz.Visible = true;
                tsjrnstat.Visible = true;
                bn1jn.Visible = true;
                mvewin.Visible = true;
                dvgpndoc.Visible = true;
                dgvwin.Size = new Size(x, y);
                wozzy = 0;
            }
        }
        //restart jrn
        private void restr_Click(object sender, EventArgs e)
        {
            acc_ledg ldg = new acc_ledg();
            ldg.Show();
            this.Close();
        }


        //Move Window 1
        private void movtolef_clc(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwin.Dock = DockStyle.Left;
        }

        private void movtor(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwin.Dock = DockStyle.Right;
        }

        private void freesty_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwin.Dock = DockStyle.None;
        }

        private void tobott_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwin.Dock = DockStyle.Bottom;
        }

        private void totop_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwin.Dock = DockStyle.Top;
        }

        private void tswin_MouseDown(object sender, MouseEventArgs e)
        {
            if (Properties.Settings.Default.defrgb == 0)
            {
                tswin.BackColor = Color.Lavender;
            }
            else if (Properties.Settings.Default.defrgb != 0)
            {
                try
                {
                    tswin.BackColor = Color.FromArgb(Properties.Settings.Default.r - 9, Properties.Settings.Default.g - 9, Properties.Settings.Default.b - 9);
                }
                catch (Exception erty) { }
            }
        }

        private void tswin_MouseUp(object sender, MouseEventArgs e)
        {
            if (Properties.Settings.Default.defrgb == 0)
            {
                tswin.BackColor = Color.White;
            }
            else if (Properties.Settings.Default.defrgb != 0)
            {
                try
                {
                    tswin.BackColor = Color.FromArgb(Properties.Settings.Default.r + 10, Properties.Settings.Default.g + 10, Properties.Settings.Default.b + 10);
                }
                catch (Exception erty) { }
            }
        }
        //Win2
        private void tswin2_clc(object sender, EventArgs e)
        {
            winstatus = 2;
            try
            {
                tmex.Stop();
                tmex3.Stop();
                dgvwintic.Stop();
                dgvwintic3.Stop();
            }
            catch (Exception exct)
            {
            }
        }

        private void dgvwin_Click(object sender, EventArgs e)
        {
            try
            {
                tmex.Stop();
                tmex3.Stop();
                dgvwintic.Stop();
                dgvwintic3.Stop();
            }
            catch (Exception ext)
            {
            }
            dgvwin.BringToFront();
        }
        //Win 3
        private void tswin3_Click(object sender, EventArgs e)
        {
            try
            {
                dgvwintic.Stop();
                dgvwintic3.Stop();
                tmex.Stop();
                tmex3.Stop();
            }
            catch (Exception exct) { }
            gadg_resz1.Visible = false;
            summpnl.BringToFront();
        }

        private void summpnl_Click(object sender, EventArgs e)
        {
            try
            {
                dgvwintic.Stop();
                dgvwintic3.Stop();
                tmex.Stop();
                tmex3.Stop();
            }
            catch (Exception tyu) { }
            summpnl.BringToFront();
            gadg_resz1.Visible = false;
        }

        private void sst_Click(object sender, EventArgs e)
        {
            try
            {
                dgvwintic.Stop();
                dgvwintic3.Stop();
                tmex.Stop();
                tmex3.Stop();
            }
            catch (Exception etc) { }
            summpnl.BringToFront();
        }

        private ToolStrip tsn_tmp, tsn_tmp2;
        private void tswin3_MouseEnter(object sender, EventArgs e)
        {
            tsn_tmp = (ToolStrip)sender;
            if (Properties.Settings.Default.defrgb == 0)
            {
                tsn_tmp.BackColor = Color.White;
            }
            else if (Properties.Settings.Default.defrgb != 0)
            {
                try
                {
                    tsn_tmp.BackColor = Color.FromArgb(Properties.Settings.Default.r + 10, Properties.Settings.Default.g + 10, Properties.Settings.Default.b + 10);
                }
                catch (Exception erty) { }
            }
        }

        private void tswin3_MouseLeave(object sender, EventArgs e)
        {
            tsn_tmp2 = (ToolStrip)sender;
            if (Properties.Settings.Default.defrgb == 0)
            {
                tsn_tmp2.BackColor = Color.AliceBlue;
            }
            else if (Properties.Settings.Default.defrgb != 0)
            {
                try
                {
                    tsn_tmp2.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);
                }
                catch (Exception erty) { }
            }
        }

        private void tswin3_MouseDown(object sender, MouseEventArgs e)
        {
            if (Properties.Settings.Default.defrgb == 0)
            {
                tswin3.BackColor = Color.Lavender;
            }
            else if (Properties.Settings.Default.defrgb != 0)
            {
                try
                {
                    tswin3.BackColor = Color.FromArgb(Properties.Settings.Default.r - 9, Properties.Settings.Default.g - 9, Properties.Settings.Default.b - 9);
                }
                catch (Exception erty) { }
            }
        }

        private void tswin3_MouseUp(object sender, MouseEventArgs e)
        {
            if (Properties.Settings.Default.defrgb == 0)
            {
                tswin3.BackColor = Color.White;
            }
            else if (Properties.Settings.Default.defrgb != 0)
            {
                try
                {
                    tswin3.BackColor = Color.FromArgb(Properties.Settings.Default.r + 10, Properties.Settings.Default.g + 10, Properties.Settings.Default.b + 10);
                }
                catch (Exception erty) { }
            }
        }

        int wozzy3 = 0;
        int x3, y3;
        private void clse3_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            if (wozzy3 == 0)
            {
                sepmnu3.Visible = false;
                pos3mnu.Visible = false;
                maxminmnu3.Enabled = false;
                szesumm.Visible = false;
                maxminmnu.Enabled = false;
                rszmnu3.Enabled = false;
                reszz3.Visible = false;
                movwin3.Visible = false;
                dvgpndoc3.Visible = false;
                x3 = summpnl.Size.Width;
                y3 = summpnl.Size.Height;
                summpnl.Dock = DockStyle.None;
                summpnl.Size = new Size(140, 48);
                wozzy3 = 1;
            }
            else if (wozzy3 == 1)
            {
                buho3 = 0;
                maxminmnu3.Enabled = true;
                szesumm.Visible = true;
                rszmnu3.Enabled = true;
                maxminmnu.Enabled = true;
                reszz3.Visible = true;
                movwin3.Visible = true;
                dvgpndoc3.Visible = true;
                summpnl.Size = new Size(x3, y3);
                wozzy3 = 0;
            }
        }

        private void dgvwintic3_Tick(object sender, EventArgs e)
        {
            dvgpndoc3.ToolTipText = "Maximize";
            buho3 = 0;
            summpnl.Dock = DockStyle.None;
            summpnl.Location = new Point(Cursor.Position.X - this.Location.X - (summpnl.Size.Width / 2), Cursor.Position.Y - this.Location.Y - 60);
        }

        private void tswin3_DoubleClick(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            dgvwintic3.Start();
        }

        private void tmex3_Tick(object sender, EventArgs e)
        {
            if (summpnl.Controls.IndexOf(gadg_resz1) == -1)
            {
                summpnl.Controls.Add(gadg_resz1);
            }
            gadg_resz1.Visible = true;
            gadg_resz1.BringToFront();
            summpnl.Dock = DockStyle.None;
            summpnl.Size = new Size(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y);
        }

        private void reszz3_ButtonClick(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            dvgpndoc3.ToolTipText = "Maximize";
            buho3 = 0;
            tmex3.Start();
        }

        int buho3;
        private void dvgpndoc3_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            if (buho3 == 0)
            {
                dvgpndoc3.ToolTipText = "Window";
                summpnl.Dock = DockStyle.Fill;
                buho3 = 1;
            }
            else if (buho3 == 1)
            {
                dvgpndoc3.ToolTipText = "Maximize";
                summpnl.Dock = DockStyle.None;
                buho3 = 0;
            }
            else { buho3 = 0; }
        }

        private void rstrt2_Click(object sender, EventArgs e)
        {
            this.Text = "Restarting..";
            acc_ledg ldg = new acc_ledg();
            ldg.Show();
            this.Close();
        }

        private void str_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            summpnl.Size = new Size(685, 389);
        }

        private void setToDefauljkk_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            summpnl.Dock = DockStyle.None;
            summpnl.Location = new Point(106, 50);
        }

        private void smtrze_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            summpnl.Size = new Size(summpnl.Size.Width, summpnl.Size.Width);
        }

        private void smthtt_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            summpnl.Size = new Size(summpnl.Size.Height, summpnl.Size.Height);
        }

        private void ewv_Click(object sender, EventArgs e)
        {
            pnl_journvw.AutoScroll = true;
            acc_journ_sett.Default.widevw = true;
            acc_journ_sett.Default.Save();
        }

        private void dwv_Click(object sender, EventArgs e)
        {
            pnl_journvw.AutoScroll = false;
            acc_journ_sett.Default.widevw = false;
            acc_journ_sett.Default.Save();
        }

        private void ts2_MouseEnter_1(object sender, EventArgs e)
        {
            ts2.BackColor = Color.AliceBlue;
        }

        private void ts2_MouseLeave_1(object sender, EventArgs e)
        {
            ts2.BackColor = Color.Lavender;
        }

        private void movtolef3_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            summpnl.Dock = DockStyle.Left;
        }

        private void movtrgt_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            summpnl.Dock = DockStyle.Right;
        }

        private void movtbott3_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            summpnl.Dock = DockStyle.Bottom;
        }

        private void movtop3_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            summpnl.Dock = DockStyle.Top;
        }

        private void freesty3_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            summpnl.Dock = DockStyle.None;
        }

        private void clseall_Click(object sender, EventArgs e)
        {
            maxminmnu1.Enabled = false;
            maxminmnu3.Enabled = false;

            rszemnu.Enabled = false;
            rszmnu3.Enabled = false;

            //3
            sepmnu3.Visible = false;
            pos3mnu.Visible = false;
            maxminmnu.Enabled = false;
            szejrn.Visible = false;
            reszz3.Visible = false;
            movwin3.Visible = false;
            dvgpndoc3.Visible = false;
            x3 = summpnl.Size.Width;
            y3 = summpnl.Size.Height;
            summpnl.Dock = DockStyle.None;
            summpnl.Size = new Size(140, 48);
            wozzy3 = 1;

            //1
            sepmnu1.Visible = false;
            pos1mnu.Visible = false;
            mxmn.Enabled = false;
            szesumm.Visible = false;
            reszz.Visible = false;
            mvewin.Visible = false;
            dvgpndoc.Visible = false;
            x = dgvwin.Size.Width;
            y = dgvwin.Size.Height;
            dgvwin.Dock = DockStyle.None;
            dgvwin.Size = new Size(140, 48);
            wozzy = 1;
        }

        private void restawin_Click(object sender, EventArgs e)
        {
            maxminmnu1.Enabled = true;
            maxminmnu3.Enabled = true;

            rszemnu.Enabled = true;
            rszmnu3.Enabled = true;

            //3
            sepmnu3.Visible = true;
            pos3mnu.Visible = true;
            szesumm.Visible = true;
            maxminmnu.Enabled = true;
            reszz3.Visible = true;
            movwin3.Visible = true;
            dvgpndoc3.Visible = true;
            summpnl.Size = new Size(x3, y3);
            wozzy3 = 0;

            //1
            sepmnu1.Visible = true;
            pos1mnu.Visible = true;
            mxmn.Enabled = true;
            szejrn.Visible = true;
            reszz.Visible = true;
            mvewin.Visible = true;
            dvgpndoc.Visible = true;
            dgvwin.Size = new Size(x, y);
            wozzy = 0;
        }

        private void sttodefjrn_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwin.Dock = DockStyle.None;
            dgvwin.Location = new Point(164, 15);
        }

        private void simjrnwth_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwin.Size = new Size(dgvwin.Size.Width, dgvwin.Size.Width);
        }

        private void simjrnhgt_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwin.Size = new Size(dgvwin.Size.Height, dgvwin.Size.Height);
        }

        private void stdflt_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwin.Size = new Size(685, 389);
        }

        //Col.
        private void grylv1col_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Colpct = 1;
            Properties.Settings.Default.Save();
        }

        private void restodef_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.defrgb = 0;
            Properties.Settings.Default.Save();
            tswin3.BackColor = Color.AliceBlue;
            tswin.BackColor = Color.AliceBlue;

            tsttl.ForeColor = Color.Gray;
            tsttl2.ForeColor = Color.Gray;
        }

        private void blulv1col_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Colpct = 2;
            Properties.Settings.Default.Save();
        }

        private void redlv1col_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Colpct = 3;
            Properties.Settings.Default.Save();
        }

        private void cnbnn3_MouseEnter(object sender, EventArgs e)
        {
            cnbnn3.Opacity = 0.90;
        }

        private void cnbnn3_MouseLeave(object sender, EventArgs e)
        {
            cnbnn3.Opacity = 0.85;
        }

        private void cnbnn1_MouseEnter(object sender, EventArgs e)
        {
            cnbnn1.Opacity = 0.90;
        }

        private void cnbnn1_MouseLeave(object sender, EventArgs e)
        {
            cnbnn1.Opacity = 0.85;
        }

        private void jrntofnt_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
        }

        private void smtofnt_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
        }

        private void selwin_MouseEnter(object sender, EventArgs e)
        {
            selwin.Opacity = 0.90;
        }

        private void selwin_MouseLeave(object sender, EventArgs e)
        {
            selwin.Opacity = 0.85;
        }

        private void minwins_Click(object sender, EventArgs e)
        {
            buho = 0;
            buho3 = 0;

            dvgpndoc.ToolTipText = "Maximize";
            dvgpndoc3.ToolTipText = "Maximize";

            summpnl.Dock = DockStyle.None;
            dgvwin.Dock = DockStyle.None;
        }

        private void maxall_Click(object sender, EventArgs e)
        {
            buho = 1;
            buho3 = 1;
            summpnl.Dock = DockStyle.Fill;
            dgvwin.Dock = DockStyle.Fill;
        }

        private void tbcol_Click(object sender, EventArgs e)
        {
            try
            {
                summpnl.BringToFront();
                tmex.Stop();
                tmex3.Stop();

                dgvwintic.Stop();
                dgvwintic3.Stop();
            }
            catch (Exception etr) { }
        }

        private void tbx1_Click(object sender, EventArgs e)
        {
            try
            {
                summpnl.BringToFront();
                tmex.Stop();
                tmex3.Stop();

                dgvwintic.Stop();
                dgvwintic3.Stop();
            }
            catch (Exception etr) { }
        }

        private void tbcsp_Click(object sender, EventArgs e)
        {
            try
            {
                tmex.Stop();
                tmex3.Stop();

                dgvwintic.Stop();
                dgvwintic3.Stop();
            }
            catch (Exception etr) { }
        }

        private void tbxfned_tch(object sender, EventArgs e)
        {
            if (tbxfned.Text == "" || tbxfned.Text == null || tbxfned.Text == "Entry")
            {
                tbxfned.Text = "Entry";
                tbxfned.ForeColor = Color.LightGray;
                using (Font fnt = new Font(tbxfned.Font.FontFamily.GetName(0), tbxfned.Font.Size, FontStyle.Italic))
                {
                    tbxfned.Font = fnt;
                }
            }
            else if (tbxfned.Text != "" || tbxfned.Text != null)
            {
                tbxfned.ForeColor = Color.DimGray;
                using (Font fnt = new Font(tbxfned.Font.FontFamily.GetName(0), tbxfned.Font.Size, FontStyle.Regular))
                {
                    tbxfned.Font = fnt;
                }
            }
        }

        private void slctrgb_Click(object sender, EventArgs e)
        {
            Main.cols cls = new cols();
            cls.Show();
        }

        private void col_Tick(object sender, EventArgs e)
        {
            if (Main.Amatrix.rgb == 1)
            {
                tswin.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);
                tswin3.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);

                tsttl.ForeColor = Color.FromArgb(Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb);
                tsttl2.ForeColor = Color.FromArgb(Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb);
            }
            else if (Main.Amatrix.rgb == 2)
            {
                tswin3.BackColor = Color.AliceBlue;
                tswin.BackColor = Color.AliceBlue;

                tsttl.ForeColor = Color.Gray;
                tsttl2.ForeColor = Color.Gray;
            }
        }

        private void svebtn_ButtonClick(object sender, EventArgs e)
        {
        }

        private void general_mssg(string text, string cause1)
        {

            err_inf_1.Text = cause1;

            err_inf_2.Text = text;

            try
            {
                ttp2.Show(text, this, this.Size.Width - 94, ts2.Location.Y - 34, 5000);
            }
            catch (Exception erty) { }

            ttp_del.Start();
        }

        private void clseconn_Click(object sender, EventArgs e)
        {
            connlbl.Text = "Disconnecting..";
            connlbl.Image = Properties.Resources.conncting;
        }

        private void connct_Click(object sender, EventArgs e)
        {
            connctyes();
        }

        private void connctyes()
        {
            initconn();
        }

        private void tmeinit_Tick(object sender, EventArgs e)
        {
            tmeinit.Stop();
            try
            {
                thinitstrt();
            }
            catch (Exception erty) { Init(); }
        }

        private void enblhc_Click_1(object sender, EventArgs e)
        {
            foreach (Control cn in this.Controls)
            {
                cn.ForeColor = Color.White;
                cn.BackColor = Color.Black;
            }
        }

        private void chbselallinfoc_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void abtmnu_Click(object sender, EventArgs e)
        {
            app_abt bt = new app_abt();
            bt.descr("Amatrix Ledger");
            bt.Show();
        }
        private void undoall_Click(object sender, EventArgs e)
        {
                if (winstatus == 3)
                {
                    try
                    {
                            int x, y;
                            x = Convert.ToInt32(aundC[aundC.Count - 1]);
                            y = Convert.ToInt32(aundR[aundR.Count - 1]);

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
        }

        private void dgv_Sorted(object sender, EventArgs e)
        {
            aund.Clear();
            aundC.Clear();
            aundR.Clear();
        }

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
        private ArrayList copycutpaste = new ArrayList();
        private void cpy_Click(object sender, EventArgs e)
        {
            copycutpaste.Clear();
        }

        private void dgv1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Am_err mer = new Am_err(); mer.tx(e.Exception.Source + e.Exception.Message);
        }

        private void set_quikbox()
        {
            if (Cursor.Position.X - this.Location.X <= this.Size.Width && Cursor.Position.Y - this.Location.Y <= this.Size.Height)
            {
                zz.Location = new Point((Cursor.Position.X - this.Location.X) - zz.Size.Width / 2, Cursor.Position.Y - this.Location.Y);
            }
            else { }
        }

        private void ct_Click(object sender, EventArgs e)
        {
            copycutpaste.Clear();
            try
            {
                if (winstatus == 3)
                {
                }
            }
            catch (Exception erty) { }
        }

        private  ToolStripTextBox tstb_byrw;

        //initialize startup
        private void yeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            acc_journ_sett.Default.db_jrn_strt = true;
            acc_journ_sett.Default.Save();
        }

        private void noToolStripMenuItem_Click(object sender, EventArgs e)
        {
            acc_journ_sett.Default.db_jrn_strt = false;
            acc_journ_sett.Default.Save();
        }

        private void initializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initconn();
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

        private void refrjrn_Click(object sender, EventArgs e)
        {
            SUMS();
        }

        private void zz_MouseEnter(object sender, EventArgs e)
        {
            //clse_zz.Stop();
            zz.BackColor = Color.GhostWhite;
            tmr.Interval = 13000;
            tmr.Start();
        }

        private void zz_MouseLeave(object sender, EventArgs e)
        {
            //clse_zz.Start();
            zz.BackColor = Color.WhiteSmoke;
            tmr.Interval = 13000;
            tmr.Start();
        }

        private void zz_yn(bool shw)
        {
            if (shw == true)
            {
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

        private void clse_zz_Tick(object sender, EventArgs e)
        {
            zz_yn(false);
        }

        private void ttp_del_Tick(object sender, EventArgs e)
        {
            ttp_del.Stop();
            if (err.DropDown.Visible != true)
            {
                err.Visible = false;
            }
        }

        private void remv_zz_Click(object sender, EventArgs e)
        {
            zz_yn(false);
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
                zz_yn(false);
        }

        private void oaapp_Click(object sender, EventArgs e)
        {
            acc_journ jrn = new acc_journ();
            jrn.Show();
        }

        //visual data methods

        //column adder
        Font fnt;

        private void lv_cols()
        {
            //new
            if (tv.SelectedNode.Text == "Years")
            {
                lv.Clear();
                lv.Columns.Clear();
                lv.Items.Clear();

                lv.Columns.Add("Date", "Date", 135, HorizontalAlignment.Left, 3);
                lv.Columns.Add("Credit", 80);
                lv.Columns.Add("Debit", 80);
                lv.Columns.Add("Particulars", 160);
            }
            if (tv.SelectedNode.Text == "Months")
            {
                lv.Clear();
                lv.Columns.Clear();
                lv.Items.Clear();

                lv.Columns.Add("Date", "Date", 135, HorizontalAlignment.Left, 3);
                lv.Columns.Add("Credit", 80);
                lv.Columns.Add("Debit", 80);
                lv.Columns.Add("Particulars", 160);
            }
            if (tv.SelectedNode.Text == "Dates")
            {
                lv.Clear();
                lv.Columns.Clear();
                lv.Items.Clear();

                lv.Columns.Add("Date", "Date", 120, HorizontalAlignment.Left, 3);
                lv.Columns.Add("Credit", 80);
                lv.Columns.Add("Debit", 80);
                lv.Columns.Add("Particulars", 160);
                lv.Columns.Add("Serial Number", 115);
            }
            //old
            if (tv.SelectedNode.Text == "Average Debit/Credit Per Month (Current)")
            {
                lv.Clear();
                lv.Columns.Clear();
                lv.Items.Clear();
                lv.Columns.Add("Month", "Month", 80, HorizontalAlignment.Left, 3);
                lv.Columns.Add("Credit Average For Month", 140);
                lv.Columns.Add("Debit Average For Month", 140);
                lv.Columns.Add("Difference (Dr-Cr)", 120);
                lv.Columns.Add("Difference (Cr-Dr)", 120);
                lv.Columns.Add("Year", 60);

                lv.Items.Add("January", 4);
                lv.Items.Add("Febuary", 4);
                lv.Items.Add("March", 4);
                lv.Items.Add("April", 4);
                lv.Items.Add("May", 4);
                lv.Items.Add("June", 4);
                lv.Items.Add("July", 4);
                lv.Items.Add("August", 4);
                lv.Items.Add("September", 4);
                lv.Items.Add("October", 4);
                lv.Items.Add("November", 4);
                lv.Items.Add("December", 4);
                lv.Items.Add("Total", 4);

                lv.Items[12].BackColor = Color.Lavender;
                fnt = new Font(lv.Font.FontFamily.GetName(0), lv.Font.Size, FontStyle.Bold);
                lv.Items[12].Font = fnt;
            }
            if (tv.SelectedNode.Text == "Average Debit/Credit Per Year (Current)")
            {
                lv.Clear();
                lv.Columns.Clear();
                lv.Items.Clear();
                lv.Columns.Add("Year", "Year", 80, HorizontalAlignment.Left, 3);
                lv.Columns.Add("Average Credit For Year", 140);
                lv.Columns.Add("Average Debit For Year", 140);
                lv.Columns.Add("Difference (Dr-Cr)", 120);
                lv.Columns.Add("Difference (Cr-Dr)", 120);
                
                lv.Items.Add(DateTime.Now.Year.ToString(), 4);
                lv.Items.Add("Total", 4);
                fnt = new Font(lv.Font.FontFamily.GetName(0), lv.Font.Size, FontStyle.Bold);
                lv.Items[1].Font = fnt;
                lv.Items[1].BackColor = Color.Lavender;
            }
            if (tv.SelectedNode.Text == "Debit/Credit Sum Per Month (Current)")
            {
                lv.Clear();
                lv.Columns.Clear();
                lv.Items.Clear();
                lv.Columns.Add("Month", "Month", 80, HorizontalAlignment.Left, 3);
                lv.Columns.Add("Credit Sum For Month", 140);
                lv.Columns.Add("Debit Sum For Month", 140);
                lv.Columns.Add("Difference (Dr-Cr)", 120);
                lv.Columns.Add("Difference (Cr-Dr)", 120);
                lv.Columns.Add("Year", 60);

                lv.Items.Add("January", 4);
                lv.Items.Add("Febuary", 4);
                lv.Items.Add("March", 4);
                lv.Items.Add("April", 4);
                lv.Items.Add("May", 4);
                lv.Items.Add("June", 4);
                lv.Items.Add("July", 4);
                lv.Items.Add("August", 4);
                lv.Items.Add("September", 4);
                lv.Items.Add("October", 4);
                lv.Items.Add("November", 4);
                lv.Items.Add("December", 4);
                lv.Items.Add("Total", 4);

                lv.Items[12].BackColor = Color.Lavender;
                fnt = new Font(lv.Font.FontFamily.GetName(0), lv.Font.Size, FontStyle.Bold);
                lv.Items[12].Font = fnt;
            }
            if (tv.SelectedNode.Text == "Debit/Credit Sum Per Year (Current)")
            {
                lv.Clear();
                lv.Columns.Clear();
                lv.Items.Clear();

                lv.Columns.Add("Year", "Year", 80, HorizontalAlignment.Left, 3);
                lv.Columns.Add("Sum Credit For Year", 140);
                lv.Columns.Add("Sum Debit For Year", 140);
                lv.Columns.Add("Difference (Dr-Cr)", 120);
                lv.Columns.Add("Difference (Cr-Dr)", 120);

                lv.Items.Add(DateTime.Now.Year.ToString(), 4);
                lv.Items.Add("Total", 4);
                fnt = new Font(lv.Font.FontFamily.GetName(0), lv.Font.Size, FontStyle.Bold);
                lv.Items[1].Font = fnt;
                lv.Items[1].BackColor = Color.Lavender;
            }
            if (tv.SelectedNode.Text == "All Months")
            {
                lv.Clear();
                lv.Columns.Clear();
                lv.Items.Clear();

                lv.Columns.Add("Month/Year", "Month/Year", 130, HorizontalAlignment.Left, 3);
                lv.Columns.Add("Sum Credit For Month", 140);
                lv.Columns.Add("Sum Debit For Month", 140);
                lv.Columns.Add("Average Credit For Month", 140);
                lv.Columns.Add("Average Debit For Month", 140);
            }
            if (tv.SelectedNode.Text == "All Years")
            {
                lv.Clear();
                lv.Columns.Clear();
                lv.Items.Clear();
                lv.Columns.Add("Year", "Year", 130, HorizontalAlignment.Left, 3);
                lv.Columns.Add("Sum Credit For Year", 140);
                lv.Columns.Add("Sum Debit For Year", 140);
                lv.Columns.Add("Average Credit For Year", 140);
                lv.Columns.Add("Average Debit For Year", 140);
            }
        }

        private void lv_cols2()
        {            //new
            if (tv2.SelectedNode.Text == "Years")
            {
                lv2.Clear();
                lv2.Columns.Clear();
                lv2.Items.Clear();

                lv2.Columns.Add("Date", "Date", 135, HorizontalAlignment.Left, 3);
                lv2.Columns.Add("Credit", 80);
                lv2.Columns.Add("Debit", 80);
                lv2.Columns.Add("Particulars", 160);
                lv.Columns.Add("From Serial Number", 140);
                lv.Columns.Add("To Serial Number", 140);
            }
            if (tv2.SelectedNode.Text == "Months")
            {
                lv2.Clear();
                lv2.Columns.Clear();
                lv2.Items.Clear();

                lv2.Columns.Add("Date", "Date", 135, HorizontalAlignment.Left, 3);
                lv2.Columns.Add("Credit", 80);
                lv2.Columns.Add("Debit", 80);
                lv2.Columns.Add("Particulars", 160);
                lv.Columns.Add("From Serial Number", 140);
                lv.Columns.Add("To Serial Number", 140);
            }
            if (tv2.SelectedNode.Text == "Dates")
            {
                lv2.Clear();
                lv2.Columns.Clear();
                lv2.Items.Clear();

                lv2.Columns.Add("Date", "Date", 120, HorizontalAlignment.Left, 3);
                lv2.Columns.Add("Credit", 80);
                lv2.Columns.Add("Debit", 80);
                lv2.Columns.Add("Particulars", 160);
            }
            //old
            if (tv2.SelectedNode.Text == "Average Credit Per Month (Current)")
            {
                lv2.Clear();
                lv2.Columns.Clear();
                lv2.Items.Clear();
                lv2.Columns.Add("Month", "Month", 80, HorizontalAlignment.Left, 3);
                lv2.Columns.Add("Credit Average For Month", 140);
                lv2.Columns.Add("Year", 60);

                lv2.Items.Add("January", 4);
                lv2.Items.Add("Febuary", 4);
                lv2.Items.Add("March", 4);
                lv2.Items.Add("April", 4);
                lv2.Items.Add("May", 4);
                lv2.Items.Add("June", 4);
                lv2.Items.Add("July", 4);
                lv2.Items.Add("August", 4);
                lv2.Items.Add("September", 4);
                lv2.Items.Add("October", 4);
                lv2.Items.Add("November", 4);
                lv2.Items.Add("December", 4);
                lv2.Items.Add("Total", 4);

                lv2.Items[12].BackColor = Color.Lavender;
                fnt = new Font(lv2.Font.FontFamily.GetName(0), lv2.Font.Size, FontStyle.Bold);
                lv2.Items[12].Font = fnt;
            }
            if (tv2.SelectedNode.Text == "Average Credit Per Year (Current)")
            {
                lv2.Clear();
                lv2.Columns.Clear();
                lv2.Items.Clear();
                lv2.Columns.Add("Year", "Year", 80, HorizontalAlignment.Left, 3);
                lv2.Columns.Add("Average Credit For Year", 140);

                lv2.Items.Add(DateTime.Now.Year.ToString(), 4);
                lv2.Items.Add("Total", 4);
                fnt = new Font(lv2.Font.FontFamily.GetName(0), lv2.Font.Size, FontStyle.Bold);
                lv2.Items[1].Font = fnt;
                lv2.Items[1].BackColor = Color.Lavender;
            }
            if (tv2.SelectedNode.Text == "Credit Sum Per Month (Current)")
            {
                lv2.Clear();
                lv2.Columns.Clear();
                lv2.Items.Clear();
                lv2.Columns.Add("Month", "Month", 80, HorizontalAlignment.Left, 3);
                lv2.Columns.Add("Credit Sum For Month", 140);
                lv2.Columns.Add("Year", 60);

                lv2.Items.Add("January", 4);
                lv2.Items.Add("Febuary", 4);
                lv2.Items.Add("March", 4);
                lv2.Items.Add("April", 4);
                lv2.Items.Add("May", 4);
                lv2.Items.Add("June", 4);
                lv2.Items.Add("July", 4);
                lv2.Items.Add("August", 4);
                lv2.Items.Add("September", 4);
                lv2.Items.Add("October", 4);
                lv2.Items.Add("November", 4);
                lv2.Items.Add("December", 4);
                lv2.Items.Add("Total", 4);

                lv2.Items[12].BackColor = Color.Lavender;
                fnt = new Font(lv2.Font.FontFamily.GetName(0), lv2.Font.Size, FontStyle.Bold);
                lv2.Items[12].Font = fnt;
            }
            if (tv2.SelectedNode.Text == "Credit Sum Per Year (Current)")
            {
                lv2.Clear();
                lv2.Columns.Clear();
                lv2.Items.Clear();

                lv2.Columns.Add("Year", "Year", 80, HorizontalAlignment.Left, 3);
                lv2.Columns.Add("Sum Credit For Year", 140);

                lv2.Items.Add(DateTime.Now.Year.ToString(), 4);
                lv2.Items.Add("Total", 4);
                fnt = new Font(lv2.Font.FontFamily.GetName(0), lv2.Font.Size, FontStyle.Bold);
                lv2.Items[1].Font = fnt;
                lv2.Items[1].BackColor = Color.Lavender;
            }
            if (tv2.SelectedNode.Text == "All Months")
            {
                lv2.Clear();
                lv2.Columns.Clear();
                lv2.Items.Clear();

                lv2.Columns.Add("Month/Year", "Month/Year", 130, HorizontalAlignment.Left, 3);
                lv2.Columns.Add("Sum Credit For Month", 140);
                lv2.Columns.Add("Average Credit For Month", 140);
            }
            if (tv2.SelectedNode.Text == "All Years")
            {
                lv2.Clear();
                lv2.Columns.Clear();
                lv2.Items.Clear();
                lv2.Columns.Add("Year", "Year", 130, HorizontalAlignment.Left, 3);
                lv2.Columns.Add("Sum Credit For Year", 140);
                lv2.Columns.Add("Average Credit For Year", 140);
            }
        }

        private void lv_cols3()
        {
            //new
            if (tv3.SelectedNode.Text == "Years")
            {
                lv3.Clear();
                lv3.Columns.Clear();
                lv3.Items.Clear();

                lv3.Columns.Add("Date", "Date", 135, HorizontalAlignment.Left, 3);
                lv3.Columns.Add("Credit", 80);
                lv3.Columns.Add("Debit", 80);
                lv3.Columns.Add("Particulars", 160);
            }
            if (tv3.SelectedNode.Text == "Months")
            {
                lv3.Clear();
                lv3.Columns.Clear();
                lv3.Items.Clear();

                lv3.Columns.Add("Date", "Date", 135, HorizontalAlignment.Left, 3);
                lv3.Columns.Add("Credit", 80);
                lv3.Columns.Add("Debit", 80);
                lv3.Columns.Add("Particulars", 160);
            }
            if (tv3.SelectedNode.Text == "Dates")
            {
                lv3.Clear();
                lv3.Columns.Clear();
                lv3.Items.Clear();

                lv3.Columns.Add("Date", "Date", 120, HorizontalAlignment.Left, 3);
                lv3.Columns.Add("Credit", 80);
                lv3.Columns.Add("Debit", 80);
                lv3.Columns.Add("Particulars", 160);
                lv.Columns.Add("Serial Number", 140);
            }
            //old
            if (tv3.SelectedNode.Text == "Average Debit/Credit Per Month (Current)")
            {
                lv3.Clear();
                lv3.Columns.Clear();
                lv3.Items.Clear();
                lv3.Columns.Add("Month", "Month", 80, HorizontalAlignment.Left, 3);
                lv3.Columns.Add("Credit Average For Month", 140);
                lv3.Columns.Add("Debit Average For Month", 140);
                lv3.Columns.Add("Difference (Dr-Cr)", 120);
                lv3.Columns.Add("Difference (Cr-Dr)", 120);
                lv3.Columns.Add("Year", 60);

                lv3.Items.Add("January", 4);
                lv3.Items.Add("Febuary", 4);
                lv3.Items.Add("March", 4);
                lv3.Items.Add("April", 4);
                lv3.Items.Add("May", 4);
                lv3.Items.Add("June", 4);
                lv3.Items.Add("July", 4);
                lv3.Items.Add("August", 4);
                lv3.Items.Add("September", 4);
                lv3.Items.Add("October", 4);
                lv3.Items.Add("November", 4);
                lv3.Items.Add("December", 4);
                lv3.Items.Add("Total", 4);

                lv3.Items[12].BackColor = Color.Lavender;
                fnt = new Font(lv3.Font.FontFamily.GetName(0), lv3.Font.Size, FontStyle.Bold);
                lv3.Items[12].Font = fnt;
            }
            if (tv3.SelectedNode.Text == "Average Debit/Credit Per Year (Current)")
            {
                lv3.Clear();
                lv3.Columns.Clear();
                lv3.Items.Clear();
                lv3.Columns.Add("Year", "Year", 80, HorizontalAlignment.Left, 3);
                lv3.Columns.Add("Average Credit For Year", 140);
                lv3.Columns.Add("Average Debit For Year", 140);
                lv3.Columns.Add("Difference (Dr-Cr)", 120);
                lv3.Columns.Add("Difference (Cr-Dr)", 120);

                lv3.Items.Add(DateTime.Now.Year.ToString(), 4);
                lv3.Items.Add("Total", 4);
                fnt = new Font(lv3.Font.FontFamily.GetName(0), lv3.Font.Size, FontStyle.Bold);
                lv3.Items[1].Font = fnt;
                lv3.Items[1].BackColor = Color.Lavender;
            }
            if (tv3.SelectedNode.Text == "Debit/Credit Sum Per Month (Current)")
            {
                lv3.Clear();
                lv3.Columns.Clear();
                lv3.Items.Clear();
                lv3.Columns.Add("Month", "Month", 80, HorizontalAlignment.Left, 3);
                lv3.Columns.Add("Credit Sum For Month", 140);
                lv3.Columns.Add("Debit Sum For Month", 140);
                lv3.Columns.Add("Difference (Dr-Cr)", 120);
                lv3.Columns.Add("Difference (Cr-Dr)", 120);
                lv3.Columns.Add("Year", 60);

                lv3.Items.Add("January", 4);
                lv3.Items.Add("Febuary", 4);
                lv3.Items.Add("March", 4);
                lv3.Items.Add("April", 4);
                lv3.Items.Add("May", 4);
                lv3.Items.Add("June", 4);
                lv3.Items.Add("July", 4);
                lv3.Items.Add("August", 4);
                lv3.Items.Add("September", 4);
                lv3.Items.Add("October", 4);
                lv3.Items.Add("November", 4);
                lv3.Items.Add("December", 4);
                lv3.Items.Add("Total", 4);

                lv3.Items[12].BackColor = Color.Lavender;
                fnt = new Font(lv3.Font.FontFamily.GetName(0), lv3.Font.Size, FontStyle.Bold);
                lv3.Items[12].Font = fnt;
            }
            if (tv3.SelectedNode.Text == "Debit/Credit Sum Per Year (Current)")
            {
                lv3.Clear();
                lv3.Columns.Clear();
                lv3.Items.Clear();

                lv3.Columns.Add("Year", "Year", 80, HorizontalAlignment.Left, 3);
                lv3.Columns.Add("Sum Credit For Year", 140);
                lv3.Columns.Add("Sum Debit For Year", 140);
                lv3.Columns.Add("Difference (Dr-Cr)", 120);
                lv3.Columns.Add("Difference (Cr-Dr)", 120);
                lv3.Items.Add(DateTime.Now.Year.ToString(), 4);
                lv3.Items.Add("Total", 4);
                fnt = new Font(lv3.Font.FontFamily.GetName(0), lv3.Font.Size, FontStyle.Bold);
                lv3.Items[1].Font = fnt;
                lv3.Items[1].BackColor = Color.Lavender;
            }
            if (tv3.SelectedNode.Text == "All Months")
            {
                lv3.Clear();
                lv3.Columns.Clear();
                lv3.Items.Clear();

                lv3.Columns.Add("Month/Year", "Month/Year", 130, HorizontalAlignment.Left, 3);
                lv3.Columns.Add("Sum Credit For Month", 140);
                lv3.Columns.Add("Average Credit For Month", 140);
                lv3.Columns.Add("Sum Debit For Month", 140);
                lv3.Columns.Add("Average Debit For Month", 140);
            }
            if (tv3.SelectedNode.Text == "All Years")
            {
                lv3.Clear();
                lv3.Columns.Clear();
                lv3.Items.Clear();
                lv3.Columns.Add("Year", "Year", 130, HorizontalAlignment.Left, 3);
                lv3.Columns.Add("Sum Credit For Year", 140);
                lv3.Columns.Add("Average Credit For Year", 140);
                lv3.Columns.Add("Sum Debit For Year", 140);
                lv3.Columns.Add("Average Debit For Year", 140);
            }
        }

        private void lv4_cols()
        {
            //new
            if (tv4.SelectedNode.Text == "Years")
            {
                lv4.Clear();
                lv4.Columns.Clear();
                lv4.Items.Clear();

                lv4.Columns.Add("Date", "Date", 135, HorizontalAlignment.Left, 3);
                lv4.Columns.Add("Credit", 80);
                lv4.Columns.Add("Debit", 80);
                lv4.Columns.Add("Particulars", 160);
            }
            if (tv4.SelectedNode.Text == "Months")
            {
                lv4.Clear();
                lv4.Columns.Clear();
                lv4.Items.Clear();

                lv4.Columns.Add("Date", "Date", 135, HorizontalAlignment.Left, 3);
                lv4.Columns.Add("Credit", 80);
                lv4.Columns.Add("Debit", 80);
                lv4.Columns.Add("Particulars", 160);
            }
            if (tv4.SelectedNode.Text == "Dates" || tv4.SelectedNode.Text == "Asset Account Between Dates" || tv4.SelectedNode.Text == "Liability Account Between Dates" || tv4.SelectedNode.Text == "Revenue Account Between Dates" || tv4.SelectedNode.Text == "Expense Account Between Dates")
            {
                lv4.Clear();
                lv4.Columns.Clear();
                lv4.Items.Clear();

                lv4.Columns.Add("Date", "Date", 120, HorizontalAlignment.Left, 3);
                lv4.Columns.Add("Credit", 80);
                lv4.Columns.Add("Debit", 80);
                lv4.Columns.Add("Particulars", 160);
            }
            //old
            if (tv4.SelectedNode.Text == "Average Debit/Credit Per Month (Current)" || tv4.SelectedNode.Text == "Asset Account By Month (Current)" || tv4.SelectedNode.Text == "Liability Account By Month (Current)" || tv4.SelectedNode.Text == "Revenue Account By Month (Current)" || tv4.SelectedNode.Text == "Expense Account By Month (Current)")
            {
                lv4.Clear();
                lv4.Columns.Clear();
                lv4.Items.Clear();
                lv4.Columns.Add("Month", "Month", 80, HorizontalAlignment.Left, 3);
                if (tv4.SelectedNode.Text == "Average Debit/Credit Per Month (Current)")
                {
                    lv4.Columns.Add("Credit Average For Month", 140);
                    lv4.Columns.Add("Debit Average For Month", 140);
                }
                else if (tv4.SelectedNode.Text != "Average Debit/Credit Per Month (Current)")
                {
                    lv4.Columns.Add("Credit Sum For Month", 140);
                    lv4.Columns.Add("Debit Sum For Month", 140);
                }
                lv4.Columns.Add("Difference (Dr-Cr)", 120);
                lv4.Columns.Add("Difference (Cr-Dr)", 120);
                lv4.Columns.Add("Year", 60);

                lv4.Items.Add("January", 4);
                lv4.Items.Add("Febuary", 4);
                lv4.Items.Add("March", 4);
                lv4.Items.Add("April", 4);
                lv4.Items.Add("May", 4);
                lv4.Items.Add("June", 4);
                lv4.Items.Add("July", 4);
                lv4.Items.Add("August", 4);
                lv4.Items.Add("September", 4);
                lv4.Items.Add("October", 4);
                lv4.Items.Add("November", 4);
                lv4.Items.Add("December", 4);
                lv4.Items.Add("Total", 4);

                lv4.Items[12].BackColor = Color.Lavender;
                fnt = new Font(lv4.Font.FontFamily.GetName(0), lv4.Font.Size, FontStyle.Bold);
                lv4.Items[12].Font = fnt;
            }
            if (tv4.SelectedNode.Text == "Average Debit/Credit Per Year (Current)")
            {
                lv4.Clear();
                lv4.Columns.Clear();
                lv4.Items.Clear();
                lv4.Columns.Add("Year", "Year", 80, HorizontalAlignment.Left, 3);
                lv4.Columns.Add("Average Credit For Year", 140);
                lv4.Columns.Add("Average Debit For Year", 140);
                lv4.Columns.Add("Difference (Dr-Cr)", 120);
                lv4.Columns.Add("Difference (Cr-Dr)", 120);

                lv4.Items.Add(DateTime.Now.Year.ToString(), 4);
                lv4.Items.Add("Total", 4);
                fnt = new Font(lv4.Font.FontFamily.GetName(0), lv4.Font.Size, FontStyle.Bold);
                lv4.Items[1].Font = fnt;
                lv4.Items[1].BackColor = Color.Lavender;
            }
            if (tv4.SelectedNode.Text == "Debit/Credit Sum Per Month (Current)")
            {
                lv4.Clear();
                lv4.Columns.Clear();
                lv4.Items.Clear();
                lv4.Columns.Add("Month", "Month", 80, HorizontalAlignment.Left, 3);
                lv4.Columns.Add("Credit Sum For Month", 140);
                lv4.Columns.Add("Debit Sum For Month", 140);
                lv4.Columns.Add("Difference (Dr-Cr)", 120);
                lv4.Columns.Add("Difference (Cr-Dr)", 120);
                lv4.Columns.Add("Year", 60);

                lv4.Items.Add("January", 4);
                lv4.Items.Add("Febuary", 4);
                lv4.Items.Add("March", 4);
                lv4.Items.Add("April", 4);
                lv4.Items.Add("May", 4);
                lv4.Items.Add("June", 4);
                lv4.Items.Add("July", 4);
                lv4.Items.Add("August", 4);
                lv4.Items.Add("September", 4);
                lv4.Items.Add("October", 4);
                lv4.Items.Add("November", 4);
                lv4.Items.Add("December", 4);
                lv4.Items.Add("Total", 4);

                lv4.Items[12].BackColor = Color.Lavender;
                fnt = new Font(lv4.Font.FontFamily.GetName(0), lv4.Font.Size, FontStyle.Bold);
                lv4.Items[12].Font = fnt;
            }
            if (tv4.SelectedNode.Text == "Debit/Credit Sum Per Year (Current)" || tv4.SelectedNode.Text == "Asset Account By Year (Current)" || tv4.SelectedNode.Text == "Liability Account By Year (Current)" || tv4.SelectedNode.Text == "Revenue Account By Year (Current)" || tv4.SelectedNode.Text == "Expense Account By Year (Current)")
            {
                lv4.Clear();
                lv4.Columns.Clear();
                lv4.Items.Clear();

                lv4.Columns.Add("Year", "Year", 80, HorizontalAlignment.Left, 3);
                lv4.Columns.Add("Sum Credit For Year", 140);
                lv4.Columns.Add("Sum Debit For Year", 140);
                lv4.Columns.Add("Difference (Dr-Cr)", 120);
                lv4.Columns.Add("Difference (Cr-Dr)", 120);

                lv4.Items.Add(DateTime.Now.Year.ToString(), 4);
                lv4.Items.Add("Total", 4);
                fnt = new Font(lv4.Font.FontFamily.GetName(0), lv4.Font.Size, FontStyle.Bold);
                lv4.Items[1].Font = fnt;
                lv4.Items[1].BackColor = Color.Lavender;
            }
            if (tv4.SelectedNode.Text == "All Months" || tv4.SelectedNode.Text == "Asset Account All Months/Years" || tv4.SelectedNode.Text == "Liability Account All Months/Years" || tv4.SelectedNode.Text == "Revenue Account All Months/Years" || tv4.SelectedNode.Text == "Expense Account All Months/Years")
            {
                lv4.Clear();
                lv4.Columns.Clear();
                lv4.Items.Clear();

                lv4.Columns.Add("Month/Year", "Month/Year", 130, HorizontalAlignment.Left, 3);
                lv4.Columns.Add("Sum Credit For Month", 140);
                lv4.Columns.Add("Sum Debit For Month", 140);
                lv4.Columns.Add("Average Credit For Month", 140);
                lv4.Columns.Add("Average Debit For Month", 140);
            }
            if (tv4.SelectedNode.Text == "All Years")
            {
                lv4.Clear();
                lv4.Columns.Clear();
                lv4.Items.Clear();
                lv4.Columns.Add("Year", "Year", 130, HorizontalAlignment.Left, 3);
                lv4.Columns.Add("Sum Credit For Year", 140);
                lv4.Columns.Add("Sum Debit For Year", 140);
                lv4.Columns.Add("Average Credit For Year", 140);
                lv4.Columns.Add("Average Debit For Year", 140);
            }
        }

        //objects
        DataTable dst_final_public = new DataTable();
        DataTable dst_END_public = new DataTable();
        double n = 0; double n2 = 0; double n3 = 0;
        double n4 = 0;
        double tot = 0;
        SqlCeCommand cmd_public;
        SqlCeDataReader read_public;
        SqlCeConnection conn_public;
        string ConnString, SqlString;
        ArrayList al_years = new ArrayList();
        private void by_date(int who)
        {
            try
            {
                n = 0; n3 = 0;
                n2 = 0; n4 = 0;
                dst_final_public.Clear();
                dst_final_public.Columns.Clear();
                dataGridView1.Columns.Clear();
                lv_cols();
                if (who == 1)
                {
                    if (tv.SelectedNode.Text == "Dates")
                    {
                        str_builder = tv.SelectedNode.Text;
                        contextMenuStrip2.Show(tv, 0, 0);
                    }
                    if (tv.SelectedNode.Text == "Months")
                    {
                        str_builder = tv.SelectedNode.Text;
                        contextMenuStrip3.Show(tv, 0, 0);
                    }
                    if (tv.SelectedNode.Text == "Years")
                    {
                        str_builder = tv.SelectedNode.Text;
                        contextMenuStrip4.Show(tv, 0, 0);
                    }
                    if (tv.SelectedNode.Text == "Average Debit/Credit Per Month (Current)")
                    {
                        str_builder = tv.SelectedNode.Text;
                        for (int i = 1; i <= 12; i++)
                        {
                            if (Main.Amatrix.acc == "")
                            {
                                ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                                SqlString = "Select avg(Credit), avg(Debit) FROM CashBook WHERE DatePart(MM, [Date]) = '" + i.ToString() + "' AND DatePart(YY, [Date]) = DatePart(YY, GetDate())";
                                using (conn_public = new SqlCeConnection(ConnString))
                                {
                                    using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                                    {

                                        conn_public.Open();
                                        read_public = cmd_public.ExecuteReader();
                                        using (read_public)
                                        {
                                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                            dataGridView1.DataSource = dst_final_public;
                                            lv.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                                            lv.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[1].Value.ToString());
                                            try
                                            {
                                                n = n + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                                            }
                                            catch (Exception erty) { }
                                            try
                                            {
                                                n2 = n2 + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value);
                                            }
                                            catch (Exception ertyt2) { }
                                            n4 = n4 - n3;
                                            try
                                            {
                                                tot = Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                                                lv.Items[i - 1].SubItems.Add(tot.ToString());
                                            }
                                            catch (Exception erty) { lv.Items[i - 1].SubItems.Add("0"); }

                                            try
                                            {
                                                lv.Items[i - 1].SubItems.Add((Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value)).ToString());
                                            }
                                            catch (Exception erty) { lv.Items[i - 1].SubItems.Add("0"); }
                                            lv.Items[i - 1].SubItems.Add(DateTime.Now.Year.ToString());
                                        }
                                    }
                                }
                            }
                            else
                            {
                                dst_final_public = basql.Execute(Main.Amatrix.acc, "Select avg(Credit), avg(Debit) FROM CashBook WHERE DatePart(MM, [Date]) = '" + i.ToString() + "' AND DatePart(YY, [Date]) = DatePart(YY, GetDate())", "CashBook", dst_final_public); dataGridView1.DataSource = dst_final_public;
                                lv.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                                lv.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[1].Value.ToString());
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value);
                                }
                                catch (Exception ertyt2) { }
                                n4 = n4 - n3;
                                try
                                {
                                    tot = Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                                    lv.Items[i - 1].SubItems.Add(tot.ToString());
                                }
                                catch (Exception erty) { lv.Items[i - 1].SubItems.Add("0"); }
                                lv.Items[i - 1].SubItems.Add(DateTime.Now.Year.ToString());
                            }
                        }
                        lv.Items[12].SubItems.Add(n.ToString());
                        lv.Items[12].SubItems.Add(n2.ToString());
                        lv.Items[12].SubItems.Add((n2 - n).ToString());
                    }
                    else if (tv.SelectedNode.Text == "Average Debit/Credit Per Year (Current)")
                    {
                        str_builder = tv.SelectedNode.Text;
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        if (Main.Amatrix.acc == "")
                        {
                            SqlString = "Select avg(Credit), avg(Debit) FROM CashBook WHERE DatePart(YY, [Date]) = DatePart(YY, GetDate())";
                            using (conn_public = new SqlCeConnection(ConnString))
                            {
                                using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                                {

                                    conn_public.Open();
                                    read_public = cmd_public.ExecuteReader();
                                    using (read_public)
                                    {
                                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                        dataGridView1.DataSource = dst_final_public;
                                        lv.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[0].Value.ToString());
                                        lv.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[1].Value.ToString());
                                        try
                                        {
                                            n = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                }
                            }
                        }
                        else
                        {
                            dst_final_public = basql.Execute(Main.Amatrix.acc, "Select avg(Credit), avg(Debit) FROM CashBook WHERE DatePart(YY, [Date]) = DatePart(YY, GetDate())", "CashBook", dst_final_public);
                            dataGridView1.DataSource = dst_final_public;
                            lv.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[0].Value.ToString());
                            lv.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[1].Value.ToString());
                            try
                            {
                                n = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
                            }
                            catch (Exception erty) { }
                            try
                            {
                                n2 = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                            }
                            catch (Exception erty) { }
                        }
                        lv.Items[1].SubItems.Add(n.ToString());
                        lv.Items[1].SubItems.Add(n2.ToString());
                        lv.Items[1].SubItems.Add((n2 - n).ToString());
                        lv.Items[0].SubItems.Add((n2 - n).ToString());
                        lv.Items[1].SubItems.Add((n - n2).ToString());
                        lv.Items[0].SubItems.Add((n - n2).ToString());
                    }
                    //sum current()
                    if (tv.SelectedNode.Text == "Debit/Credit Sum Per Month (Current)")
                    {
                        str_builder = tv.SelectedNode.Text;
                        for (int i = 1; i <= 12; i++)
                        {
                            SqlString = "Select SUM(Credit), SUM(Debit) FROM CashBook WHERE DatePart(MM, [Date]) = '" + i.ToString() + "' AND DatePart(YY, [Date]) = DatePart(YY, GetDate())";

                            if (Main.Amatrix.acc == "")
                            {
                                ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                            }
                            else
                            {
                                ConnString = Main.Amatrix.acc;
                            }
                            if (Main.Amatrix.acc == "")
                            {
                                using (conn_public = new SqlCeConnection(ConnString))
                                {
                                    using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                                    {

                                        conn_public.Open();
                                        read_public = cmd_public.ExecuteReader();
                                        using (read_public)
                                        {
                                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                            dataGridView1.DataSource = dst_final_public;
                                            lv.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                                            lv.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[1].Value.ToString());
                                            try
                                            {
                                                n = n + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                                            }
                                            catch (Exception erty) { }
                                            try
                                            {
                                                n2 = n2 + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value);
                                            }
                                            catch (Exception ertyt2) { }
                                            //after
                                            lv.Items[i - 1].SubItems.Add((n2 - n).ToString());
                                            lv.Items[i - 1].SubItems.Add((n - n2).ToString());
                                            lv.Items[i - 1].SubItems.Add(DateTime.Now.Year.ToString());
                                        }
                                    }
                                }
                            }
                            else
                            {
                                dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "CashBook", dst_final_public);
                                dataGridView1.DataSource = dst_final_public;
                                lv.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                                lv.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[1].Value.ToString());
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value);
                                }
                                catch (Exception ertyt2) { }
                                //after
                                lv.Items[i - 1].SubItems.Add((n2 - n).ToString());
                                lv.Items[i - 1].SubItems.Add((n - n2).ToString());
                                lv.Items[i - 1].SubItems.Add(DateTime.Now.Year.ToString());
                            }
                        }
                        lv.Items[12].SubItems.Add(n.ToString());
                        lv.Items[12].SubItems.Add(n2.ToString());
                        lv.Items[12].SubItems.Add((n2 - n).ToString());
                        lv.Items[12].SubItems.Add((n - n2).ToString());
                    }
                    else if (tv.SelectedNode.Text == "Debit/Credit Sum Per Year (Current)")
                    {
                        str_builder = tv.SelectedNode.Text;
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select SUM(Credit), SUM(Debit) FROM CashBook WHERE DatePart(YY, [Date]) = DatePart(YY, GetDate())";
                        if (Main.Amatrix.acc == "")
                        {
                            using (conn_public = new SqlCeConnection(ConnString))
                            {
                                using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                                {

                                    conn_public.Open();
                                    read_public = cmd_public.ExecuteReader();
                                    using (read_public)
                                    {
                                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                        dataGridView1.DataSource = dst_final_public;
                                        lv.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[0].Value.ToString());
                                        lv.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[1].Value.ToString());
                                        try
                                        {
                                            n = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                }
                            }
                        }
                        else
                        {
                            dst_final_public = basql.Execute(ConnString, SqlString, "CashBook", dst_final_public);
                            dataGridView1.DataSource = dst_final_public;
                            lv.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[0].Value.ToString());
                            lv.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[1].Value.ToString());
                            try
                            {
                                n = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
                            }
                            catch (Exception erty) { }
                            try
                            {
                                n2 = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                            }
                            catch (Exception erty) { }
                        }
                        lv.Items[1].SubItems.Add(n.ToString());
                        lv.Items[1].SubItems.Add(n2.ToString());
                        lv.Items[1].SubItems.Add((n2 - n).ToString());
                        lv.Items[0].SubItems.Add((n2 - n).ToString());
                        lv.Items[1].SubItems.Add((n - n2).ToString());
                        lv.Items[0].SubItems.Add((n - n2).ToString());
                    }
                    else if (tv.SelectedNode.Text == "All Years")
                    {
                        str_builder = tv.SelectedNode.Text;
                        dst_final_public.Clear();
                        al_years.Clear();
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select DISTINCT DatePart(YY, [Date]) FROM CashBook";
                        if (Main.Amatrix.acc == "")
                        {
                            using (conn_public = new SqlCeConnection(ConnString))
                            {
                                using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                                {
                                    conn_public.Open();
                                    read_public = cmd_public.ExecuteReader();
                                    using (read_public)
                                    {
                                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                        dataGridView1.DataSource = dst_final_public;

                                        int yu = 0;
                                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                                        {
                                            try
                                            {
                                                string str3 = (string)dgvr.Cells[0].Value.ToString();
                                                al_years.Add(str3);
                                            }
                                            catch (Exception erty) { }
                                            yu++;
                                        }
                                    }
                                }
                            }
                            dst_final_public.Clear();
                            foreach (string s in al_years)
                            {
                                str_builder = tv.SelectedNode.Text;
                                lv.Items.Add(s);
                                if (Main.Amatrix.acc == "")
                                {
                                    ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                                }
                                else
                                {
                                    ConnString = Main.Amatrix.acc;
                                }
                                SqlString = "Select sum(Credit), sum(Debit), avg(Debit/Credit), avg(Debit) FROM CashBook WHERE DatePart(YY, [Date]) = '" + s + "'";
                                using (conn_public = new SqlCeConnection(ConnString))
                                {
                                    using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                                    {

                                        conn_public.Open();
                                        read_public = cmd_public.ExecuteReader();
                                        using (read_public)
                                        {
                                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                            dataGridView1.DataSource = dst_final_public;
                                        }
                                    }
                                }
                                try
                                {
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, lv.Items.Count - 1].Value.ToString());
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, lv.Items.Count - 1].Value.ToString());
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, lv.Items.Count - 1].Value.ToString());
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, lv.Items.Count - 1].Value.ToString());

                                    n = n + Convert.ToDouble(dataGridView1[0, lv.Items.Count - 1].Value);
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, lv.Items.Count - 1].Value);
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, lv.Items.Count - 1].Value);
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, lv.Items.Count - 1].Value);
                                }
                                catch (Exception erty) { }
                            }
                        }
                        else
                        {
                            dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "CashBook", dst_final_public);
                            dataGridView1.DataSource = dst_final_public;

                            int yu = 0;
                            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                            {
                                try
                                {
                                    string str3 = (string)dgvr.Cells[0].Value.ToString();
                                    al_years.Add(str3);
                                }
                                catch (Exception erty) { }
                                yu++;
                            }
                            dst_final_public.Clear();
                            foreach (string s in al_years)
                            {
                                str_builder = tv.SelectedNode.Text;
                                lv.Items.Add(s);
                                ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                                SqlString = "Select sum(Credit), sum(Debit), avg(Debit/Credit), avg(Debit) FROM CashBook WHERE DatePart(YY, [Date]) = '" + s + "'";
                                dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "CashBook", dst_final_public);
                                dataGridView1.DataSource = dst_final_public;
                                try
                                {
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, lv.Items.Count - 1].Value.ToString());
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, lv.Items.Count - 1].Value.ToString());
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, lv.Items.Count - 1].Value.ToString());
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, lv.Items.Count - 1].Value.ToString());
                                    n = n + Convert.ToDouble(dataGridView1[0, lv.Items.Count - 1].Value);
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, lv.Items.Count - 1].Value);
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, lv.Items.Count - 1].Value);
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, lv.Items.Count - 1].Value);
                                }
                                catch (Exception erty) { }
                            }
                        }
                        lv.Items.Add("Total");
                        fnt = new Font(lv.Font.FontFamily.GetName(0), lv.Font.Size, FontStyle.Bold);
                        lv.Items[lv.Items.Count - 1].Font = fnt;
                        lv.Items[lv.Items.Count - 1].BackColor = Color.Lavender;
                        lv.Items[lv.Items.Count - 1].SubItems.Add(n.ToString());
                        lv.Items[lv.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv.Items[lv.Items.Count - 1].SubItems.Add(n3.ToString());
                        lv.Items[lv.Items.Count - 1].SubItems.Add(n4.ToString());
                    }
                    else if (tv.SelectedNode.Text == "All Months")
                    {
                        str_builder = tv.SelectedNode.Text;
                        n = 0;
                        n2 = 0;
                        n3 = 0;
                        n4 = 0;
                        dst_final_public.Clear();
                        al_years.Clear();
                        lv.Items.Clear();
                        fnt = new Font(lv.Font.FontFamily.GetName(0), lv.Font.Size, FontStyle.Bold);
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select DISTINCT DatePart(YY, [Date]) FROM CashBook";
                        if (Main.Amatrix.acc == "")
                        {
                            conn_public = new SqlCeConnection(ConnString);
                            cmd_public = new SqlCeCommand(SqlString, conn_public);
                            conn_public.Open();
                            read_public = cmd_public.ExecuteReader();
                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView1.DataSource = dst_final_public;
                        }
                        else
                        {
                            dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "CashBook", dst_final_public);
                            dataGridView1.DataSource = dst_final_public;
                        }

                        int yu = 0;
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                string str3 = (string)dgvr.Cells[0].Value.ToString();
                                al_years.Add(str3);
                            }
                            catch (Exception erty) { }
                            yu++;
                        }

                        foreach (string s in al_years)
                        {
                            dst_final_public.Clear();
                            lv.Items.Add(s);
                            lv.Items[lv.Items.Count - 1].BackColor = Color.Lavender;
                            n = 0;
                            n2 = 0;
                            for (int i = 1; i <= 12; i++)
                            {
                                if (Main.Amatrix.acc == "")
                                {
                                    ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                                }
                                else
                                {
                                    ConnString = Main.Amatrix.acc;
                                }
                                SqlString = "Select sum(Credit), sum(Debit), avg(Credit), avg(Debit) FROM CashBook WHERE DatePart(YY, [Date]) = '" + s + "' AND DatePart(MM, [Date]) = '" + i.ToString() + "'";
                                if (Main.Amatrix.acc == "")
                                {
                                    conn_public = new SqlCeConnection(ConnString);
                                    cmd_public = new SqlCeCommand(SqlString, conn_public);
                                    conn_public.Open();
                                    read_public = cmd_public.ExecuteReader();
                                    dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                    dataGridView1.DataSource = dst_final_public;
                                }
                                else
                                {
                                    dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "CashBook", dst_final_public);
                                    dataGridView1.DataSource = dst_final_public;
                                }
                                if (i == 1)
                                {
                                    lv.Items.Add("January");
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    //additions
                                    try
                                    {
                                        n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                    }
                                    catch (Exception erty) { }
                                }
                                if (i == 2)
                                {
                                    lv.Items.Add("Febuary");
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, 1].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, 1].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, 1].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, 1].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    //additions
                                    try
                                    {
                                        n = n + Convert.ToDouble(dataGridView1[0, 1].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = n2 + Convert.ToDouble(dataGridView1[1, 1].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                    }
                                    catch (Exception erty) { }
                                }
                                if (i == 3)
                                {
                                    lv.Items.Add("March");
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, 2].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, 2].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, 2].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, 2].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    //additions
                                    try
                                    {
                                        n = n + Convert.ToDouble(dataGridView1[0, 2].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = n2 + Convert.ToDouble(dataGridView1[1, 2].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n3 = n3 + Convert.ToDouble(dataGridView1[2, 2].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n4 = n4 + Convert.ToDouble(dataGridView1[3, 2].Value);
                                    }
                                    catch (Exception erty) { }
                                }
                                if (i == 4)
                                {
                                    lv.Items.Add("April");
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, 3].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, 3].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, 3].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, 3].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    //additions
                                    try
                                    {
                                        n = n + Convert.ToDouble(dataGridView1[0, 3].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = n2 + Convert.ToDouble(dataGridView1[1, 3].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n3 = n3 + Convert.ToDouble(dataGridView1[2, 3].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n4 = n4 + Convert.ToDouble(dataGridView1[3, 3].Value);
                                    }
                                    catch (Exception erty) { }
                                }
                                if (i == 5)
                                {
                                    lv.Items.Add("May");
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, 4].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, 4].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, 4].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, 4].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    //additions
                                    try
                                    {
                                        n = n + Convert.ToDouble(dataGridView1[0, 4].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = n2 + Convert.ToDouble(dataGridView1[1, 4].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n3 = n3 + Convert.ToDouble(dataGridView1[2, 4].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n4 = n4 + Convert.ToDouble(dataGridView1[3, 4].Value);
                                    }
                                    catch (Exception erty) { }
                                }
                                if (i == 6)
                                {
                                    lv.Items.Add("June");
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, 5].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, 5].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, 5].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, 5].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    //additions
                                    try
                                    {
                                        n = n + Convert.ToDouble(dataGridView1[0, 5].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = n2 + Convert.ToDouble(dataGridView1[1, 5].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n3 = n3 + Convert.ToDouble(dataGridView1[2, 5].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n4 = n4 + Convert.ToDouble(dataGridView1[3, 5].Value);
                                    }
                                    catch (Exception erty) { }
                                }
                                if (i == 7)
                                {
                                    lv.Items.Add("July");
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, 6].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, 6].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, 6].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, 6].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    //additions
                                    try
                                    {
                                        n = n + Convert.ToDouble(dataGridView1[0, 6].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = n2 + Convert.ToDouble(dataGridView1[1, 6].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n3 = n3 + Convert.ToDouble(dataGridView1[2, 6].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n4 = n4 + Convert.ToDouble(dataGridView1[3, 6].Value);
                                    }
                                    catch (Exception erty) { }
                                }
                                if (i == 8)
                                {
                                    lv.Items.Add("August");
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, 7].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, 7].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, 7].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, 7].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    //additions
                                    try
                                    {
                                        n = n + Convert.ToDouble(dataGridView1[0, 7].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = n2 + Convert.ToDouble(dataGridView1[1, 7].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n3 = n3 + Convert.ToDouble(dataGridView1[2, 7].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n4 = n4 + Convert.ToDouble(dataGridView1[3, 7].Value);
                                    }
                                    catch (Exception erty) { }
                                }
                                if (i == 9)
                                {
                                    lv.Items.Add("September");
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, 8].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, 8].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, 8].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, 8].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    //additions
                                    try
                                    {
                                        n = n + Convert.ToDouble(dataGridView1[0, 8].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = n2 + Convert.ToDouble(dataGridView1[1, 8].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n3 = n3 + Convert.ToDouble(dataGridView1[2, 8].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n4 = n4 + Convert.ToDouble(dataGridView1[3, 8].Value);
                                    }
                                    catch (Exception erty) { }
                                }
                                if (i == 10)
                                {
                                    lv.Items.Add("October");
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, 9].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, 9].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, 9].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, 9].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    //additions
                                    try
                                    {
                                        n = n + Convert.ToDouble(dataGridView1[0, 9].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = n2 + Convert.ToDouble(dataGridView1[1, 9].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n3 = n3 + Convert.ToDouble(dataGridView1[2, 9].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n4 = n4 + Convert.ToDouble(dataGridView1[3, 9].Value);
                                    }
                                    catch (Exception erty) { }
                                }
                                if (i == 11)
                                {
                                    lv.Items.Add("November");
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, 10].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, 10].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, 10].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, 10].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    //additions
                                    try
                                    {
                                        n = n + Convert.ToDouble(dataGridView1[0, 10].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = n2 + Convert.ToDouble(dataGridView1[1, 10].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n3 = n3 + Convert.ToDouble(dataGridView1[2, 10].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n4 = n4 + Convert.ToDouble(dataGridView1[3, 10].Value);
                                    }
                                    catch (Exception erty) { }
                                }
                                if (i == 12)
                                {
                                    lv.Items.Add("December");
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[0, 11].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, 11].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, 11].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, 11].Value.ToString());
                                    }
                                    catch (Exception erty) { }
                                    //additions
                                    try
                                    {
                                        n = n + Convert.ToDouble(dataGridView1[0, 11].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = n2 + Convert.ToDouble(dataGridView1[1, 11].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n3 = n3 + Convert.ToDouble(dataGridView1[2, 11].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n4 = n4 + Convert.ToDouble(dataGridView1[3, 11].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        lv.Items.Add("Yearly Total");
                                        lv.Items[lv.Items.Count - 1].Font = fnt;
                                        lv.Items[lv.Items.Count - 1].BackColor = Color.Lavender;
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(n.ToString());
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(n2.ToString());
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(n3.ToString());
                                        lv.Items[lv.Items.Count - 1].SubItems.Add(n4.ToString());
                                    }
                                    catch (Exception erty) { }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception erty) { }
            }

        private void bydate2()
        {
            try
            {
                n = 0; n3 = 0;
                n2 = 0; n4 = 0;
                dst_final_public.Clear();
                dst_final_public.Columns.Clear();
                dataGridView1.Columns.Clear();

                lv_cols2();
                if (tv2.SelectedNode.Text == "Dates")
                {
                    contextMenuStrip2.Show(tv2, 0, 0);
                }
                if (tv2.SelectedNode.Text == "Months")
                {
                    contextMenuStrip3.Show(tv2, 0, 0);
                }
                if (tv2.SelectedNode.Text == "Years")
                {
                    contextMenuStrip4.Show(tv2, 0, 0);
                }
                if (tv2.SelectedNode.Text == "Average Credit Per Month (Current)")
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        SqlString = "Select avg(Credit) FROM SalesBook WHERE DatePart(MM, [Date of Sale]) = '" + i.ToString() + "' AND DatePart(YY, [Date of Sale]) = DatePart(YY, GetDate())";

                        if (Main.Amatrix.acc == "")
                        {
                            conn_public = new SqlCeConnection(ConnString);
                            cmd_public = new SqlCeCommand(SqlString, conn_public);
                            conn_public.Open();
                            read_public = cmd_public.ExecuteReader();
                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView1.DataSource = dst_final_public;
                        }
                        else
                        {
                            dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "SalesBook", dst_final_public);
                            dataGridView1.DataSource = dst_final_public;
                        }
                        lv2.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                        try
                        {
                            n = n + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                        }
                        catch (Exception erty) { }
                        lv2.Items[i - 1].SubItems.Add(DateTime.Now.Year.ToString());
                    }
                    lv2.Items[12].SubItems.Add(n.ToString());
                }
                else if (tv2.SelectedNode.Text == "Average Credit Per Year (Current)")
                {
                    if (Main.Amatrix.acc == "")
                    {
                        ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                    }
                    else
                    {
                        ConnString = Main.Amatrix.acc;
                    }
                    SqlString = "Select avg(Credit) FROM SalesBook WHERE DatePart(YY, [Date of Sale]) = DatePart(YY, GetDate())";
                    if (Main.Amatrix.acc == "")
                    {
                        conn_public = new SqlCeConnection(ConnString);

                        cmd_public = new SqlCeCommand(SqlString, conn_public);
                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    else
                    {
                        dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "SalesBook", dst_final_public);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    lv2.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[0].Value.ToString());
                    try
                    {
                        n = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
                    }
                    catch (Exception erty) { }
                    lv2.Items[1].SubItems.Add(n.ToString());
                }
                //sum current()
                if (tv2.SelectedNode.Text == "Credit Sum Per Month (Current)")
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select SUM(Credit) FROM SalesBook WHERE DatePart(MM, [Date of Sale]) = '" + i.ToString() + "' AND DatePart(YY, [Date of Sale]) = DatePart(YY, GetDate())";
                        if (Main.Amatrix.acc == "")
                        {
                            conn_public = new SqlCeConnection(ConnString);
                            cmd_public = new SqlCeCommand(SqlString, conn_public);
                            conn_public.Open();
                            read_public = cmd_public.ExecuteReader();
                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView1.DataSource = dst_final_public;
                        }
                        else
                        {
                            dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "SalesBook", dst_final_public);
                            dataGridView1.DataSource = dst_final_public;
                        }
                        lv2.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                        try
                        {
                            n = n + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                        }
                        catch (Exception erty) { }
                        //after
                        lv2.Items[i - 1].SubItems.Add(DateTime.Now.Year.ToString());
                    }
                    lv2.Items[12].SubItems.Add(n.ToString());
                }
                else if (tv2.SelectedNode.Text == "Credit Sum Per Year (Current)")
                {
                    if (Main.Amatrix.acc == "")
                    {
                        ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                    }
                    else
                    {
                        ConnString = Main.Amatrix.acc;
                    }
                    SqlString = "Select SUM(Credit) FROM SalesBook WHERE DatePart(YY, [Date of Sale]) = DatePart(YY, GetDate())";
                    if (Main.Amatrix.acc == "")
                    {
                        conn_public = new SqlCeConnection(ConnString);
                        cmd_public = new SqlCeCommand(SqlString, conn_public);
                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    else
                    {
                        dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "SalesBook", dst_final_public);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    lv2.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[0].Value.ToString());
                    try
                    {
                        n = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
                    }
                    catch (Exception erty) { }


                    lv2.Items[1].SubItems.Add(n.ToString());
                }
                else if (tv2.SelectedNode.Text == "All Years")
                {
                    dst_final_public.Clear();
                    al_years.Clear();
                    if (Main.Amatrix.acc == "")
                    {
                        ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                    }
                    else
                    {
                        ConnString = Main.Amatrix.acc;
                    }
                    SqlString = "Select DISTINCT DatePart(YY, [Date of Sale]) FROM SalesBook";
                    if (Main.Amatrix.acc == "")
                    {
                        conn_public = new SqlCeConnection(ConnString);
                        cmd_public = new SqlCeCommand(SqlString, conn_public);
                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    else
                    {
                        dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "SalesBook", dst_final_public);
                        dataGridView1.DataSource = dst_final_public;
                    }

                    int yu = 0;
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            string str3 = (string)dgvr.Cells[0].Value.ToString();
                            al_years.Add(str3);
                        }
                        catch (Exception erty) { }
                        yu++;
                    }

                    dst_final_public.Clear();
                    foreach (string s in al_years)
                    {
                        lv2.Items.Add(s);
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select sum(Credit), avg(Credit) FROM SalesBook WHERE DatePart(YY, [Date of Sale]) = '" + s + "'";
                        if (Main.Amatrix.acc == "")
                        {
                            conn_public = new SqlCeConnection(ConnString);
                            cmd_public = new SqlCeCommand(SqlString, conn_public);
                            conn_public.Open();
                            read_public = cmd_public.ExecuteReader();
                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView1.DataSource = dst_final_public;
                        }
                        else
                        {
                            dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "SalesBook", dst_final_public);
                            dataGridView1.DataSource = dst_final_public;
                        }
                        try
                        {
                            lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, lv2.Items.Count - 1].Value.ToString());
                            lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, lv2.Items.Count - 1].Value.ToString());
                            n = n + Convert.ToDouble(dataGridView1[0, lv2.Items.Count - 1].Value);
                            n2 = n2 + Convert.ToDouble(dataGridView1[1, lv2.Items.Count - 1].Value);
                        }
                        catch (Exception erty) { }
                    }
                    lv2.Items.Add("Total");
                    fnt = new Font(lv2.Font.FontFamily.GetName(0), lv2.Font.Size, FontStyle.Bold);
                    lv2.Items[lv2.Items.Count - 1].Font = fnt;
                    lv2.Items[lv2.Items.Count - 1].BackColor = Color.Lavender;
                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(n.ToString());
                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(n2.ToString());
                }
                else if (tv2.SelectedNode.Text == "All Months")
                {
                    str_builder = tv2.SelectedNode.Text;
                    n = 0;
                    n2 = 0;
                    n3 = 0;
                    n4 = 0;
                    dst_final_public.Clear();
                    al_years.Clear();
                    lv2.Items.Clear();
                    fnt = new Font(lv2.Font.FontFamily.GetName(0), lv2.Font.Size, FontStyle.Bold);
                    if (Main.Amatrix.acc == "")
                    {
                        ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                    }
                    else
                    {
                        ConnString = Main.Amatrix.acc;
                    }
                    SqlString = "Select DISTINCT DatePart(YY, [Date of Sale]) FROM SalesBook";
                    if (Main.Amatrix.acc == "")
                    {
                        conn_public = new SqlCeConnection(ConnString);
                        cmd_public = new SqlCeCommand(SqlString, conn_public);
                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    else
                    {
                        dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "SalesBook", dst_final_public);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    int yu = 0;
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            string str3 = (string)dgvr.Cells[0].Value.ToString();
                            al_years.Add(str3);
                        }
                        catch (Exception erty) { }
                        yu++;
                    }
                    foreach (string s in al_years)
                    {
                        dst_final_public.Clear();
                        lv2.Items.Add(s);
                        lv2.Items[lv2.Items.Count - 1].BackColor = Color.Lavender;
                        n = 0;
                        n2 = 0;
                        for (int i = 1; i <= 12; i++)
                        {
                            SqlString = "Select sum(Credit), avg(Credit) FROM SalesBook WHERE DatePart(YY, [Date of Sale]) = '" + s + "' AND DatePart(MM, [Date of Sale]) = '" + i.ToString() + "'";
                            //MessageBox.Show(SqlString);
                            if (Main.Amatrix.acc == "")
                            {
                                conn_public = new SqlCeConnection(ConnString);
                                cmd_public = new SqlCeCommand(SqlString, conn_public);
                                conn_public.Open();
                                read_public = cmd_public.ExecuteReader();
                                dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                dataGridView1.DataSource = dst_final_public;
                            }
                            else
                            {
                                dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "SalesBook", dst_final_public);
                                dataGridView1.DataSource = dst_final_public;
                            }
                            if (i == 1)
                            {
                                lv2.Items.Add("January");
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 2)
                            {
                                lv2.Items.Add("Febuary");
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, 1].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, 1].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 1].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 1].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 3)
                            {
                                lv2.Items.Add("March");
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, 2].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, 2].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 2].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 2].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 4)
                            {
                                lv2.Items.Add("April");
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, 3].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, 3].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 3].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 3].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 5)
                            {
                                lv2.Items.Add("May");
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, 4].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, 4].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 4].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 4].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 6)
                            {
                                lv2.Items.Add("June");
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, 5].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, 5].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 5].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 5].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 7)
                            {
                                lv2.Items.Add("July");
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, 6].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, 6].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 6].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 6].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 8)
                            {
                                lv2.Items.Add("August");
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, 7].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, 7].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 7].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 7].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 9)
                            {
                                lv2.Items.Add("September");
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, 8].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, 8].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 8].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 8].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 10)
                            {
                                lv2.Items.Add("October");
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, 9].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, 9].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 9].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 9].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 11)
                            {
                                lv2.Items.Add("November");
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, 10].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, 10].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 10].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 10].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 12)
                            {
                                lv2.Items.Add("December");
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[0, 11].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, 11].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 11].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 11].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items.Add("Yearly Total");
                                    lv2.Items[lv2.Items.Count - 1].Font = fnt;
                                    lv2.Items[lv2.Items.Count - 1].BackColor = Color.Lavender;
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(n.ToString());
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(n2.ToString());
                                }
                                catch (Exception erty) { }
                            }
                        }
                    }
                }
            }
            catch (Exception erty) { }
        }

        private void bydate3()
        {
            try
            {
                n = 0; n3 = 0;
                n2 = 0; n4 = 0;
                dst_final_public.Clear();
                dst_final_public.Columns.Clear();
                dataGridView1.Columns.Clear();

                lv_cols3();
                if (tv3.SelectedNode.Text == "Dates")
                {
                    contextMenuStrip2.Show(tv3, 0, 0);
                }
                if (tv3.SelectedNode.Text == "Months")
                {
                    contextMenuStrip3.Show(tv3, 0, 0);
                }
                if (tv3.SelectedNode.Text == "Years")
                {
                    contextMenuStrip4.Show(tv3, 0, 0);
                }
                if (tv3.SelectedNode.Text == "Average Debit/Credit Per Month (Current)")
                {
                    str_builder = tv3.SelectedNode.Text;
                    for (int i = 1; i <= 12; i++)
                    {
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                            SqlString = "Select avg(Credit), avg(Debit) FROM PurchaseBook WHERE DatePart(MM, [Date of Purchase]) = '" + i.ToString() + "' AND DatePart(YY, [Date of Purchase]) = DatePart(YY, GetDate())";
                            using (conn_public = new SqlCeConnection(ConnString))
                            {
                                using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                                {

                                    conn_public.Open();
                                    read_public = cmd_public.ExecuteReader();
                                    using (read_public)
                                    {
                                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                        dataGridView1.DataSource = dst_final_public;
                                        lv3.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                                        lv3.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[1].Value.ToString());
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value);
                                        }
                                        catch (Exception ertyt2) { }
                                        n4 = n4 - n3;
                                        try
                                        {
                                            tot = Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                                            lv3.Items[i - 1].SubItems.Add(tot.ToString());
                                        }
                                        catch (Exception erty) { lv3.Items[i - 1].SubItems.Add("0"); }
                                        try
                                        {
                                            lv3.Items[i - 1].SubItems.Add((Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value)).ToString());
                                           
                                        }
                                        catch (Exception erty) { lv3.Items[i - 1].SubItems.Add("0"); }
                                        lv3.Items[i - 1].SubItems.Add(DateTime.Now.Year.ToString());
                                    }

                                }
                            }
                        }
                        else
                        {
                            dst_final_public = basql.Execute(Main.Amatrix.acc, "Select avg(Credit), avg(Debit) FROM PurchaseBook WHERE DatePart(MM, [Date of Purchase]) = '" + i.ToString() + "' AND DatePart(YY, [Date of Purchase]) = DatePart(YY, GetDate())", "PurchaseBook", dst_final_public); dataGridView1.DataSource = dst_final_public;
                            lv3.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                            lv3.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[1].Value.ToString());
                            try
                            {
                                n = n + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                            }
                            catch (Exception erty) { }
                            try
                            {
                                n2 = n2 + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value);
                            }
                            catch (Exception ertyt2) { }
                            n4 = n4 - n3;
                            try
                            {
                                tot = Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                                lv3.Items[i - 1].SubItems.Add(tot.ToString());
                            }
                            catch (Exception erty) { lv3.Items[i - 1].SubItems.Add("0"); }
                            try
                            {
                                lv3.Items[i - 1].SubItems.Add((Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value)).ToString());
                            }
                            catch (Exception erty) { lv3.Items[i - 1].SubItems.Add("0"); }
                            lv3.Items[i - 1].SubItems.Add(DateTime.Now.Year.ToString());
                        }
                    }
                    lv3.Items[12].SubItems.Add(n.ToString());
                    lv3.Items[12].SubItems.Add(n2.ToString());
                    lv3.Items[12].SubItems.Add((n2 - n).ToString());
                    lv3.Items[12].SubItems.Add((n - n2).ToString());
                }
                else if (tv3.SelectedNode.Text == "Average Debit/Credit Per Year (Current)")
                {
                    SqlString = "Select avg(Credit), avg(Debit) FROM PurchaseBook WHERE DatePart(YY, [Date of Purchase]) = DatePart(YY, GetDate())";
                    if (Main.Amatrix.acc == "")
                    {
                        conn_public = new SqlCeConnection(ConnString);
                        cmd_public = new SqlCeCommand(SqlString, conn_public);
                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    else
                    {
                        dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "PurchaseBook", dst_final_public);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    lv3.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[0].Value.ToString());
                    lv3.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[1].Value.ToString());
                    try
                    {
                        n = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
                        n2 = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                    }
                    catch (Exception erty) { }
                    lv3.Items[1].SubItems.Add(n.ToString());
                    lv3.Items[1].SubItems.Add(n2.ToString());
                    lv3.Items[1].SubItems.Add((n2 - n).ToString());
                    lv3.Items[0].SubItems.Add((n2 - n).ToString());
                    lv3.Items[1].SubItems.Add((n - n2).ToString());
                    lv3.Items[0].SubItems.Add((n - n2).ToString());
                }
                //sum current()
                if (tv3.SelectedNode.Text == "Debit/Credit Sum Per Month (Current)")
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select SUM(Credit), SUM(Debit) FROM PurchaseBook WHERE DatePart(MM, [Date of Purchase]) = '" + i.ToString() + "' AND DatePart(YY, [Date of Purchase]) = DatePart(YY, GetDate())";
                        if (Main.Amatrix.acc == "")
                        {
                            conn_public = new SqlCeConnection(ConnString);
                            cmd_public = new SqlCeCommand(SqlString, conn_public);
                            conn_public.Open();
                            read_public = cmd_public.ExecuteReader();
                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView1.DataSource = dst_final_public;
                        }
                        else
                        {
                            dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "PurchaseBook", dst_final_public);
                            dataGridView1.DataSource = dst_final_public;
                        }
                        lv3.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                        lv3.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[1].Value.ToString());
                        try
                        {
                            n = n + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                        }
                        catch (Exception erty) { }
                        try
                        {
                            n2 = n2 + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value);
                        }
                        catch (Exception ertyt2) { }
                        //after
                        lv3.Items[i - 1].SubItems.Add((n2 - n).ToString());
                        lv3.Items[i - 1].SubItems.Add((n - n2).ToString());
                        lv3.Items[i - 1].SubItems.Add(DateTime.Now.Year.ToString());
                    }
                    lv3.Items[12].SubItems.Add(n.ToString());
                    lv3.Items[12].SubItems.Add(n2.ToString());
                    lv3.Items[12].SubItems.Add((n2 - n).ToString());
                    lv3.Items[12].SubItems.Add((n - n2).ToString());
                }
                else if (tv3.SelectedNode.Text == "Debit/Credit Sum Per Year (Current)")
                {
                    SqlString = "Select SUM(Credit), SUM(Debit) FROM PurchaseBook WHERE DatePart(YY, [Date of Purchase]) = DatePart(YY, GetDate())";
                    if (Main.Amatrix.acc == "")
                    {
                        conn_public = new SqlCeConnection(ConnString);
                        cmd_public = new SqlCeCommand(SqlString, conn_public);
                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    else
                    {
                        dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "PurchaseBook", dst_final_public);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    lv3.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[0].Value.ToString());
                    lv3.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[1].Value.ToString());
                    try
                    {
                        n = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
                        n2 = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                    }
                    catch (Exception erty) { }
                    lv3.Items[1].SubItems.Add(n.ToString());
                    lv3.Items[1].SubItems.Add(n2.ToString());
                    lv3.Items[1].SubItems.Add((n2 - n).ToString());
                    lv3.Items[0].SubItems.Add((n2 - n).ToString());
                    lv3.Items[1].SubItems.Add((n - n2).ToString());
                    lv3.Items[0].SubItems.Add((n - n2).ToString());
                }
                else if (tv3.SelectedNode.Text == "All Years")
                {
                    dst_final_public.Clear();
                    al_years.Clear();
                    if (Main.Amatrix.acc == "")
                    {
                        ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                    }
                    else
                    {
                        ConnString = Main.Amatrix.acc;
                    }
                    SqlString = "Select DISTINCT DatePart(YY, [Date of Purchase]) FROM PurchaseBook";
                    if (Main.Amatrix.acc == "")
                    {
                        conn_public = new SqlCeConnection(ConnString);
                        cmd_public = new SqlCeCommand(SqlString, conn_public);
                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    else
                    {
                        dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "PurchaseBook", dst_final_public);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    int yu = 0;
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            string str3 = (string)dgvr.Cells[0].Value.ToString();
                            al_years.Add(str3);
                        }
                        catch (Exception erty) { }
                        yu++;
                    }
                    dst_final_public.Clear();
                    foreach (string s in al_years)
                    {
                        lv3.Items.Add(s);
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select sum(Credit), avg(Credit), avg(Debit), avg(Debit) FROM PurchaseBook WHERE DatePart(YY, [Date of Purchase]) = '" + s + "'";
                        if (Main.Amatrix.acc == "")
                        {
                            conn_public = new SqlCeConnection(ConnString);
                            cmd_public = new SqlCeCommand(SqlString, conn_public);
                            conn_public.Open();
                            read_public = cmd_public.ExecuteReader();
                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView1.DataSource = dst_final_public;
                        }
                        else
                        {
                            dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "PurchaseBook", dst_final_public);
                            dataGridView1.DataSource = dst_final_public;
                        }

                        try
                        {
                            lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, lv3.Items.Count - 1].Value.ToString());
                            lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, lv3.Items.Count - 1].Value.ToString());
                            lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, lv3.Items.Count - 1].Value.ToString());
                            lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, lv3.Items.Count - 1].Value.ToString());

                            n = n + Convert.ToDouble(dataGridView1[0, lv3.Items.Count - 1].Value);
                            n2 = n2 + Convert.ToDouble(dataGridView1[1, lv3.Items.Count - 1].Value);
                            n3 = n3 + Convert.ToDouble(dataGridView1[2, lv3.Items.Count - 1].Value);
                            n4 = n4 + Convert.ToDouble(dataGridView1[3, lv3.Items.Count - 1].Value);
                        }
                        catch (Exception erty) { }
                    }
                    lv3.Items.Add("Total");
                    fnt = new Font(lv3.Font.FontFamily.GetName(0), lv3.Font.Size, FontStyle.Bold);
                    lv3.Items[lv3.Items.Count - 1].Font = fnt;
                    lv3.Items[lv3.Items.Count - 1].BackColor = Color.Lavender;
                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(n.ToString());
                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(n2.ToString());
                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(n3.ToString());
                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(n4.ToString());
                }
                else if (tv3.SelectedNode.Text == "All Months")
                {
                    str_builder = tv3.SelectedNode.Text;
                    n = 0;
                    n2 = 0;
                    n3 = 0;
                    n4 = 0;
                    dst_final_public.Clear();
                    al_years.Clear();
                    lv3.Items.Clear();
                    fnt = new Font(lv3.Font.FontFamily.GetName(0), lv3.Font.Size, FontStyle.Bold);
                    if (Main.Amatrix.acc == "")
                    {
                        ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                    }
                    else
                    {
                        ConnString = Main.Amatrix.acc;
                    }
                    SqlString = "Select DISTINCT DatePart(YY, [Date of Purchase]) FROM PurchaseBook";
                    if (Main.Amatrix.acc == "")
                    {
                        conn_public = new SqlCeConnection(ConnString);
                        cmd_public = new SqlCeCommand(SqlString, conn_public);
                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                        dataGridView1.DataSource = dst_final_public;
                    }
                    else
                    {
                        dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "PurchaseBook", dst_final_public);
                        dataGridView1.DataSource = dst_final_public;
                    }

                    int yu = 0;
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        try
                        {
                            string str3 = (string)dgvr.Cells[0].Value.ToString();
                            al_years.Add(str3);
                        }
                        catch (Exception erty) { }
                        yu++;
                    }

                    foreach (string s in al_years)
                    {
                        dst_final_public.Clear();
                        lv3.Items.Add(s);
                        lv3.Items[lv3.Items.Count - 1].BackColor = Color.Lavender;
                        n = 0;
                        n2 = 0;
                        for (int i = 1; i <= 12; i++)
                        {
                            if (Main.Amatrix.acc == "")
                            {
                                ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                            }
                            else
                            {
                                ConnString = Main.Amatrix.acc;
                            }
                            SqlString = "Select sum(Credit), sum(Debit), avg(Credit), avg(Debit) FROM PurchaseBook WHERE DatePart(YY, [Date of Purchase]) = '" + s + "' AND DatePart(MM, [Date of Purchase]) = '" + i.ToString() + "'";
                            if (Main.Amatrix.acc == "")
                            {
                                conn_public = new SqlCeConnection(ConnString);
                                cmd_public = new SqlCeCommand(SqlString, conn_public);
                                conn_public.Open();
                                read_public = cmd_public.ExecuteReader();
                                dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                dataGridView1.DataSource = dst_final_public;
                            }
                            else
                            {
                                dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "PurchaseBook", dst_final_public);
                                dataGridView1.DataSource = dst_final_public;
                            }
                            if (i == 1)
                            {
                                lv3.Items.Add("January");
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 2)
                            {
                                lv3.Items.Add("Febuary");
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, 1].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, 1].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, 1].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, 1].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 1].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 1].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 3)
                            {
                                lv3.Items.Add("March");
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, 2].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, 2].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, 2].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, 2].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 2].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 2].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, 2].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, 2].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 4)
                            {
                                lv3.Items.Add("April");
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, 3].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, 3].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, 3].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, 3].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 3].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 3].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, 3].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, 3].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 5)
                            {
                                lv3.Items.Add("May");
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, 4].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, 4].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, 4].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, 4].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 4].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 4].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, 4].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, 4].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 6)
                            {
                                lv3.Items.Add("June");
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, 5].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, 5].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, 5].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, 5].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 5].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 5].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, 5].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, 5].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 7)
                            {
                                lv3.Items.Add("July");
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, 6].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, 6].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, 6].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, 6].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 6].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 6].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, 6].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, 6].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 8)
                            {
                                lv3.Items.Add("August");
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, 7].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, 7].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, 7].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, 7].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 7].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 7].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, 7].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, 7].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 9)
                            {
                                lv3.Items.Add("September");
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, 8].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, 8].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, 8].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, 8].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 8].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 8].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, 8].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, 8].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 10)
                            {
                                lv3.Items.Add("October");
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, 9].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, 9].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, 9].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, 9].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 9].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 9].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, 9].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, 9].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 11)
                            {
                                lv3.Items.Add("November");
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, 10].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, 10].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, 10].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, 10].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 10].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 10].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, 10].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, 10].Value);
                                }
                                catch (Exception erty) { }
                            }
                            if (i == 12)
                            {
                                lv3.Items.Add("December");
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[0, 11].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, 11].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, 11].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, 11].Value.ToString());
                                }
                                catch (Exception erty) { }
                                //additions
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[0, 11].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, 11].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, 11].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, 11].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items.Add("Yearly Total");
                                    lv3.Items[lv3.Items.Count - 1].Font = fnt;
                                    lv3.Items[lv3.Items.Count - 1].BackColor = Color.Lavender;
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(n.ToString());
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(n2.ToString());
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(n3.ToString());
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(n4.ToString());
                                }
                                catch (Exception erty) { }
                            }
                        }
                    }
                }
            }
            catch (Exception erty) { }
        }

        private void bydate4()
        {
            try
            {
                n = 0; n3 = 0;
                n2 = 0; n4 = 0;

                dst_final_public.Clear();
                dst_final_public.Columns.Clear();
                dataGridView1.Columns.Clear();
                lv4_cols();
                {
                    if (tv4.SelectedNode.Text == "Dates")
                    {
                        str_builder = tv4.SelectedNode.Text;
                        contextMenuStrip2.Show(tv4, 0, 0);
                    }
                    if (tv4.SelectedNode.Text == "Months")
                    {
                        str_builder = tv4.SelectedNode.Text;
                        contextMenuStrip3.Show(tv4, 0, 0);
                    }
                    if (tv4.SelectedNode.Text == "Years")
                    {
                        str_builder = tv4.SelectedNode.Text;
                        contextMenuStrip4.Show(tv4, 0, 0);
                    }
                    if (tv4.SelectedNode.Text == "Asset Account Between Dates" || tv4.SelectedNode.Text == "Liability Account Between Dates" || tv4.SelectedNode.Text == "Revenue Account Between Dates" || tv4.SelectedNode.Text == "Expense Account Between Dates")
                    {
                        str_builder = tv4.SelectedNode.Text;
                        contextMenuStrip2.Show(tv4, 0, 0);
                    }
                    if (tv4.SelectedNode.Text == "Average Debit/Credit Per Month (Current)" || tv4.SelectedNode.Text == "Asset Account By Month (Current)" || tv4.SelectedNode.Text == "Liability Account By Month (Current)" || tv4.SelectedNode.Text == "Revenue Account By Month (Current)" || tv4.SelectedNode.Text == "Expense Account By Month (Current)")
                    {
                        str_builder = tv4.SelectedNode.Text;
                        for (int i = 1; i <= 12; i++)
                        {
                            if (str_builder == "Asset Account By Month (Current)")
                            {
                                SqlString = "Select sum(Credit), sum(Debit) FROM journal WHERE DatePart(MM, [Date of Transaction]) = '" + i.ToString() + "' AND DatePart(YY, [Date of Transaction]) = DatePart(YY, GetDate()) AND [Account Description] = 'Asset Account'";
                            }
                            if (str_builder == "Liability Account By Month (Current)")
                            {
                                SqlString = "Select sum(Credit), sum(Debit) FROM journal WHERE DatePart(MM, [Date of Transaction]) = '" + i.ToString() + "' AND DatePart(YY, [Date of Transaction]) = DatePart(YY, GetDate()) AND [Account Description] = 'Liability Account'";
                            }
                            if (str_builder == "Revenue Account By Month (Current)")
                            {
                                SqlString = "Select sum(Credit), sum(Debit) FROM journal WHERE DatePart(MM, [Date of Transaction]) = '" + i.ToString() + "' AND DatePart(YY, [Date of Transaction]) = DatePart(YY, GetDate()) AND [Account Description] = 'Revenue Account'";
                            }
                            if (str_builder == "Expense Account By Month (Current)")
                            {
                                SqlString = "Select sum(Credit), sum(Debit) FROM journal WHERE DatePart(MM, [Date of Transaction]) = '" + i.ToString() + "' AND DatePart(YY, [Date of Transaction]) = DatePart(YY, GetDate()) AND [Account Description] = 'Expense Account'";
                            }
                            if (str_builder == "Average Debit/Credit Per Month (Current)")
                            {
                                SqlString = "Select sum(Credit), sum(Debit) FROM journal WHERE DatePart(MM, [Date of Transaction]) = '" + i.ToString() + "' AND DatePart(YY, [Date of Transaction]) = DatePart(YY, GetDate())";
                            }

                            if (Main.Amatrix.acc == "")
                            {
                                ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                            }
                            else
                            {
                                ConnString = Main.Amatrix.acc;
                            }
                            if (Main.Amatrix.acc == "")
                            {
                                conn_public = new SqlCeConnection(ConnString);
                                cmd_public = new SqlCeCommand(SqlString, conn_public);
                                conn_public.Open();
                                read_public = cmd_public.ExecuteReader();
                                dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                dataGridView1.DataSource = dst_final_public;
                            }
                            else
                            {
                                dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "journal", dst_final_public);
                                dataGridView1.DataSource = dst_final_public;
                            }
                                        lv4.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                                        lv4.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[1].Value.ToString());
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value);
                                        }
                                        catch (Exception ertyt2) { }
                                        n4 = n4 - n3;
                                        try
                                        {
                                            tot = Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                                            lv4.Items[i - 1].SubItems.Add(tot.ToString());
                                        }
                                        catch (Exception erty) { lv4.Items[i - 1].SubItems.Add("0"); }
                                        try
                                        {
                                            lv4.Items[i - 1].SubItems.Add((Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value) - Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value)).ToString());
                                            
                                        }
                                        catch (Exception erty) { lv4.Items[i - 1].SubItems.Add("0"); }
                                        lv4.Items[i - 1].SubItems.Add(DateTime.Now.Year.ToString());
                                    
                                
                            
                        }
                        lv4.Items[12].SubItems.Add(n.ToString());
                        lv4.Items[12].SubItems.Add(n2.ToString());
                        lv4.Items[12].SubItems.Add((n2 - n).ToString());
                    }
                    else if (tv4.SelectedNode.Text == "Average Debit/Credit Per Year (Current)")
                    {
                        str_builder = tv4.SelectedNode.Text;
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select avg(Credit), avg(Debit) FROM journal WHERE DatePart(YY, [Date of Transaction]) = DatePart(YY, GetDate())";
                        if (Main.Amatrix.acc == "")
                        {
                            conn_public = new SqlCeConnection(ConnString);
                            cmd_public = new SqlCeCommand(SqlString, conn_public);
                            conn_public.Open();
                            read_public = cmd_public.ExecuteReader();
                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView1.DataSource = dst_final_public;
                        }
                        else
                        {
                            dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "journal", dst_final_public);
                            dataGridView1.DataSource = dst_final_public;
                        }
                                    lv4.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[0].Value.ToString());
                                    lv4.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[1].Value.ToString());
                                    try
                                    {
                                        n = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
                                    }
                                    catch (Exception erty) { }
                                    try
                                    {
                                        n2 = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                                    }
                                    catch (Exception erty) { }
                                
                            
                        
                        lv4.Items[1].SubItems.Add(n.ToString());
                        lv4.Items[1].SubItems.Add(n2.ToString());
                        lv4.Items[1].SubItems.Add((n2 - n).ToString());
                        lv4.Items[0].SubItems.Add((n2 - n).ToString());
                        lv4.Items[1].SubItems.Add((n - n2).ToString());
                        lv4.Items[0].SubItems.Add((n - n2).ToString());
                    }
                    //sum current()
                    if (tv4.SelectedNode.Text == "Debit/Credit Sum Per Month (Current)")
                    {
                        str_builder = tv4.SelectedNode.Text;
                        for (int i = 1; i <= 12; i++)
                        {
                            if (Main.Amatrix.acc == "")
                            {
                                ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                            }
                            else
                            {
                                ConnString = Main.Amatrix.acc;
                            }
                            SqlString = "Select SUM(Credit), SUM(Debit) FROM journal WHERE DatePart(MM, [Date of Transaction]) = '" + i.ToString() + "' AND DatePart(YY, [Date of Transaction]) = DatePart(YY, GetDate())";
                            if (Main.Amatrix.acc == "")
                            {
                                conn_public = new SqlCeConnection(ConnString);
                                cmd_public = new SqlCeCommand(SqlString, conn_public);
                                conn_public.Open();
                                read_public = cmd_public.ExecuteReader();
                                dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                dataGridView1.DataSource = dst_final_public;
                            }
                            else
                            {
                                dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "journal", dst_final_public);
                                dataGridView1.DataSource = dst_final_public;
                            }
                                        lv4.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[0].Value.ToString());
                                        lv4.Items[i - 1].SubItems.Add(dataGridView1.Rows[i - 1].Cells[1].Value.ToString());
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1.Rows[i - 1].Cells[1].Value);
                                        }
                                        catch (Exception ertyt2) { }
                                        //after
                                        lv4.Items[i - 1].SubItems.Add((n2 - n).ToString());
                                        lv4.Items[i - 1].SubItems.Add((n - n2).ToString());
                                        lv4.Items[i - 1].SubItems.Add(DateTime.Now.Year.ToString());
                        }
                        lv4.Items[12].SubItems.Add(n.ToString());
                        lv4.Items[12].SubItems.Add(n2.ToString());
                        lv4.Items[12].SubItems.Add((n2 - n).ToString());
                        lv4.Items[12].SubItems.Add((n - n2).ToString());
                    }
                    else if (tv4.SelectedNode.Text == "Debit/Credit Sum Per Year (Current)" || tv4.SelectedNode.Text == "Asset Account By Year (Current)" || tv4.SelectedNode.Text == "Liability Account By Year (Current)" || tv4.SelectedNode.Text == "Revenue Account By Year (Current)" || tv4.SelectedNode.Text == "Expense Account By Year (Current)")
                    {
                        str_builder = tv4.SelectedNode.Text;
                        ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        if (str_builder == "Asset Account By Year (Current)")
                        {
                            universal_query_builder("Select SUM(Credit), SUM(Debit) FROM journal WHERE DatePart(YY, [Date of Transaction]) = DatePart(YY, GetDate()) AND [Account Description] = 'Asset Account'");
                        }
                        if (str_builder == "Liability Account By Year (Current)")
                        {
                            universal_query_builder("Select SUM(Credit), SUM(Debit) FROM journal WHERE DatePart(YY, [Date of Transaction]) = DatePart(YY, GetDate()) AND [Account Description] = 'Liability Account'");
                        }
                        if (str_builder == "Revenue Account By Year (Current)")
                        {
                            universal_query_builder("Select SUM(Credit), SUM(Debit) FROM journal WHERE DatePart(YY, [Date of Transaction]) = DatePart(YY, GetDate()) AND [Account Description] = 'Revenue Account'");
                        }
                        if (str_builder == "Expense Account By Year (Current)")
                        {
                            universal_query_builder("Select SUM(Credit), SUM(Debit) FROM journal WHERE DatePart(YY, [Date of Transaction]) = DatePart(YY, GetDate()) AND [Account Description] = 'Expense Account'");
                        }
                        else
                        {
                            universal_query_builder("Select SUM(Credit), SUM(Debit) FROM journal WHERE DatePart(YY, [Date of Transaction]) = DatePart(YY, GetDate())");
                        }
                        lv4.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[0].Value.ToString());
                        lv4.Items[0].SubItems.Add(dataGridView1.Rows[0].Cells[1].Value.ToString());
                        try
                        {
                            n = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
                        }
                        catch (Exception erty) { }
                        try
                        {
                            n2 = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                        }
                        catch (Exception erty) { }
                        lv4.Items[1].SubItems.Add(n.ToString());
                        lv4.Items[1].SubItems.Add(n2.ToString());
                        lv4.Items[1].SubItems.Add((n2 - n).ToString());
                        lv4.Items[0].SubItems.Add((n2 - n).ToString());
                        lv4.Items[1].SubItems.Add((n - n2).ToString());
                        lv4.Items[0].SubItems.Add((n - n2).ToString());
                    }
                    else if (tv4.SelectedNode.Text == "All Years" || tv4.SelectedNode.Text == "Asset Account All Years" || tv4.SelectedNode.Text == "Liability Account All Years" || tv4.SelectedNode.Text == "Revenue Account All Years" || tv4.SelectedNode.Text == "Expense Account All Years")
                    {
                        str_builder = tv4.SelectedNode.Text;
                        dst_final_public.Clear();
                        al_years.Clear();
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select DISTINCT DatePart(YY, [Date of Transaction]) FROM journal";
                        if (Main.Amatrix.acc == "")
                        {
                            using (conn_public = new SqlCeConnection(ConnString))
                            {
                                using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                                {
                                    conn_public.Open();
                                    read_public = cmd_public.ExecuteReader();
                                    using (read_public)
                                    {
                                        dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                        dataGridView1.DataSource = dst_final_public;

                                        int yu = 0;
                                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                                        {
                                            try
                                            {
                                                string str3 = (string)dgvr.Cells[0].Value.ToString();
                                                al_years.Add(str3);
                                            }
                                            catch (Exception erty) { }
                                            yu++;
                                        }
                                    }
                                }
                            }
                            dst_final_public.Clear();
                            foreach (string s in al_years)
                            {
                                str_builder = tv4.SelectedNode.Text;
                                lv4.Items.Add(s);
                                if (Main.Amatrix.acc == "")
                                {
                                    ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                                }
                                else
                                {
                                    ConnString = Main.Amatrix.acc;
                                }
                                SqlString = "Select sum(Credit), sum(Debit), avg(Credit), avg(Debit) FROM journal WHERE DatePart(YY, [Date of Transaction]) = '" + s + "'";
                                using (conn_public = new SqlCeConnection(ConnString))
                                {
                                    using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                                    {

                                        conn_public.Open();
                                        read_public = cmd_public.ExecuteReader();
                                        using (read_public)
                                        {
                                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                            dataGridView1.DataSource = dst_final_public;
                                        }
                                    }
                                }
                                try
                                {
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, lv4.Items.Count - 1].Value.ToString());
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, lv4.Items.Count - 1].Value.ToString());
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, lv4.Items.Count - 1].Value.ToString());
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, lv4.Items.Count - 1].Value.ToString());

                                    n = n + Convert.ToDouble(dataGridView1[0, lv4.Items.Count - 1].Value);
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, lv4.Items.Count - 1].Value);
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, lv4.Items.Count - 1].Value);
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, lv4.Items.Count - 1].Value);
                                }
                                catch (Exception erty) { }
                            }
                        }
                        else
                        {
                            dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "journal", dst_final_public);
                            dataGridView1.DataSource = dst_final_public;

                            int yu = 0;
                            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                            {
                                try
                                {
                                    string str3 = (string)dgvr.Cells[0].Value.ToString();
                                    al_years.Add(str3);
                                }
                                catch (Exception erty) { }
                                yu++;
                            }
                            dst_final_public.Clear();
                            foreach (string s in al_years)
                            {
                                str_builder = tv4.SelectedNode.Text;
                                lv4.Items.Add(s);
                                ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                                SqlString = "Select sum(Credit), sum(Debit), avg(Credit), avg(Debit) FROM journal WHERE DatePart(YY, [Date of Transaction]) = '" + s + "'";
                                dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "journal", dst_final_public);
                                dataGridView1.DataSource = dst_final_public;
                                try
                                {
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, lv4.Items.Count - 1].Value.ToString());
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, lv4.Items.Count - 1].Value.ToString());
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, lv4.Items.Count - 1].Value.ToString());
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, lv4.Items.Count - 1].Value.ToString());

                                    n = n + Convert.ToDouble(dataGridView1[0, lv4.Items.Count - 1].Value);
                                    n2 = n2 + Convert.ToDouble(dataGridView1[1, lv4.Items.Count - 1].Value);
                                    n3 = n3 + Convert.ToDouble(dataGridView1[2, lv4.Items.Count - 1].Value);
                                    n4 = n4 + Convert.ToDouble(dataGridView1[3, lv4.Items.Count - 1].Value);
                                }
                                catch (Exception erty) { }
                            }
                        }
                        lv4.Items.Add("Total");
                        fnt = new Font(lv4.Font.FontFamily.GetName(0), lv4.Font.Size, FontStyle.Bold);
                        lv4.Items[lv4.Items.Count - 1].Font = fnt;
                        lv4.Items[lv4.Items.Count - 1].BackColor = Color.Lavender;
                        lv4.Items[lv4.Items.Count - 1].SubItems.Add(n.ToString());
                        lv4.Items[lv4.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv4.Items[lv4.Items.Count - 1].SubItems.Add(n3.ToString());
                        lv4.Items[lv4.Items.Count - 1].SubItems.Add(n4.ToString());
                    }
                    else if (tv4.SelectedNode.Text == "All Months" || tv4.SelectedNode.Text == "Asset Account All Months/Years" || tv4.SelectedNode.Text == "Liability Account All Months/Years" || tv4.SelectedNode.Text == "Revenue Account All Months/Years" || tv4.SelectedNode.Text == "Expense Account All Months/Years")
                    {
                        str_builder = tv4.SelectedNode.Text;
                        n = 0;
                        n2 = 0;
                        n3 = 0;
                        n4 = 0;
                        dst_final_public.Clear();
                        al_years.Clear();
                        lv4.Items.Clear();
                        fnt = new Font(lv4.Font.FontFamily.GetName(0), lv4.Font.Size, FontStyle.Bold);

                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select DISTINCT DatePart(YY, [Date of Transaction]) FROM journal";
                        if (Main.Amatrix.acc == "")
                        {
                            conn_public = new SqlCeConnection(ConnString);
                            cmd_public = new SqlCeCommand(SqlString, conn_public);

                            conn_public.Open();
                            read_public = cmd_public.ExecuteReader();
                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView1.DataSource = dst_final_public;
                        }
                        else
                        {
                            dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "journal", dst_final_public);
                            dataGridView1.DataSource = dst_final_public;
                        }
                                    int yu = 0;
                                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                                    {
                                        try
                                        {
                                            string str3 = (string)dgvr.Cells[0].Value.ToString();
                                            al_years.Add(str3);
                                        }
                                        catch (Exception erty) { }
                                        yu++;
                                    }
                        foreach (string s in al_years)
                        {
                            lv4.Items.Add(s);
                            lv4.Items[lv4.Items.Count - 1].BackColor = Color.Lavender;
                            n = 0;
                            n2 = 0;
                            for (int i = 1; i <= 12; i++)
                            {
                                if (tv4.SelectedNode.Text == "All Months")
                                {
                                    universal_query_builder("Select sum(Credit), sum(Debit), avg(Credit), avg(Debit) FROM journal WHERE DatePart(YY, [Date of Transaction]) = '" + s + "' AND DatePart(MM, [Date of Transaction]) = '" + i.ToString() + "'");
                                }{
                                    if (i == 1)
                                    {
                                        lv4.Items.Add("January");
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        //additions
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                    if (i == 2)
                                    {
                                        lv4.Items.Add("Febuary");
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        //additions
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                    if (i == 3)
                                    {
                                        lv4.Items.Add("March");
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        //additions
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                    if (i == 4)
                                    {
                                        lv4.Items.Add("April");
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        //additions
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                    if (i == 5)
                                    {
                                        lv4.Items.Add("May");
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        //additions
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                    if (i == 6)
                                    {
                                        lv4.Items.Add("June");
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        //additions
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                    if (i == 7)
                                    {
                                        lv4.Items.Add("July");
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        //additions
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                    if (i == 8)
                                    {
                                        lv4.Items.Add("August");
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        //additions
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                    if (i == 9)
                                    {
                                        lv4.Items.Add("September");
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        //additions
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                    if (i == 10)
                                    {
                                        lv4.Items.Add("October");
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        //additions
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                    if (i == 11)
                                    {
                                        lv4.Items.Add("November");
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        //additions
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                    }
                                    if (i == 12)
                                    {
                                        lv4.Items.Add("December");
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[0, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, 0].Value.ToString());
                                        }
                                        catch (Exception erty) { }
                                        //additions
                                        try
                                        {
                                            n = n + Convert.ToDouble(dataGridView1[0, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n2 = n2 + Convert.ToDouble(dataGridView1[1, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n3 = n3 + Convert.ToDouble(dataGridView1[2, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            n4 = n4 + Convert.ToDouble(dataGridView1[3, 0].Value);
                                        }
                                        catch (Exception erty) { }
                                        try
                                        {
                                            lv4.Items.Add("Yearly Total");
                                            lv4.Items[lv4.Items.Count - 1].Font = fnt;
                                            lv4.Items[lv4.Items.Count - 1].BackColor = Color.Lavender;
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(n.ToString());
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(n2.ToString());
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(n3.ToString());
                                            lv4.Items[lv4.Items.Count - 1].SubItems.Add(n4.ToString());
                                        }
                                        catch (Exception erty) { }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception erty) { }
        }

        private void universal_query_builder(string Query)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dst_final_public.Clear();
            dst_final_public.Columns.Clear();
            SqlString = Query;
            if (Main.Amatrix.acc == "")
            {
                ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                using (conn_public = new SqlCeConnection(ConnString))
                {
                    using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                    {

                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        using (read_public)
                        {
                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView1.DataSource = dst_final_public;
                        }
                    }
                }
            }
            else
            {
                dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "", dst_final_public);
                dataGridView1.DataSource = dst_final_public;
            }
        }

        private void universal_query_builder_nc(string Query)
        {
            SqlString = Query;
            if (Main.Amatrix.acc == "")
            {
                ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                using (conn_public = new SqlCeConnection(ConnString))
                {
                    using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                    {

                        conn_public.Open();
                        read_public = cmd_public.ExecuteReader();
                        using (read_public)
                        {
                            dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                            dataGridView1.DataSource = dst_final_public;
                        }
                    }
                }
            }
            else
            {
                dst_final_public = basql.Execute(Main.Amatrix.acc, SqlString, "", dst_final_public);
                dataGridView1.DataSource = dst_final_public;
            }
        }

        void acc_ledg_Disposed(object sender, EventArgs e)
        {
            gadg_resz1.Click -= xuy;
            helpToolStripMenuItem1.Click -= contentsToolStripMenuItem_Click;
            gadg_resz1.Dispose();
            pictureBox1.Click -= summpnl_Click;
            label1.Click -= summpnl_Click;
            textBox1.Click -= summpnl_Click;
            printValuesInToolStripMenuItem.Click -= printValuesInToolStripMenuItem_Click;
            printLedgerValuesToolStripMenuItem.Click -= printLedgerValuesToolStripMenuItem_Click;
            this.toolStripTextBox8.MouseEnter -= this.tvtxt12_MouseEnter;
            this.toolStripTextBox8.MouseLeave -= this.tvtxt12_MouseLeave;
            this.toolStripTextBox8.KeyUp -= this.tvtxt202_KeyUp;
            this.toolStripTextBox8.Click -= this.tvtxt202_Click;
            this.toolStripTextBox9.MouseEnter -= this.tvtxt12_MouseEnter;
            this.toolStripTextBox9.MouseLeave -= this.tvtxt12_MouseLeave;
            this.toolStripTextBox9.KeyUp -= this.tvtxt202_KeyUp;
            this.toolStripTextBox9.Click -= this.tvtxt202_Click;
            this.oku_bydte.Click -= this.oku_bydte_Click;
            this.toolStripMenuItem5.Click -= this.oku_bydte_Click;
            this.toolStripMenuItem12.Click -= this.oku_bydte_Click;
            this.connectToToolStripMenuItem.Click -= this.connectToToolStripMenuItem_Click;
            this.svebtn.ButtonClick -= this.svebtn_ButtonClick;
            this.clsejourn.MouseLeave -= this.clsejourn_ml;
            this.clsejourn.ButtonClick -= this.clsejournclc;
            this.clsejourn.MouseEnter -= this.clsejourn_me;
            this.rstrt2.Click -= this.rstrt2_Click;
            this.connlbl.MouseEnter -= this.connlblme;
            this.connlbl.MouseLeave -= this.connlblml;
            this.connlbl.Click -= this.connlbl_Click;
            this.tmeclse.Tick -= this.tmeclse_tc;
            this.decjourn.Tick -= this.decjourn_tc;
            this.pnl_journvw.Click -= this.pnl_journvw_clc;
            this.selwin.MouseEnter -= this.selwin_MouseEnter;
            this.selwin.MouseLeave -= this.selwin_MouseLeave;
            this.jrntofnt.Click -= this.jrntofnt_Click;
            this.smtofnt.Click -= this.smtofnt_Click;
            this.minimizeAllToolStripMenuItem.Click -= this.minwins_Click;
            this.closeAllToTrayToolStripMenuItem.Click -= this.clseall_Click;
            this.restoreAllWindowsToolStripMenuItem.Click -= this.restawin_Click;
            this.summpnl.Click -= this.summpnl_Click;
            this.panel1.Click -= this.summpnl_Click;
            this.pictureBox1.Click -= this.summpnl_Click;
            this.gu.Click -= this.gu_Click;
            this.label1.Click -= this.summpnl_Click;
            this.dataGridView3.Click -= this.summpnl_Click;
            this.sst.Click -= this.sst_Click;
            this.reszz3.ButtonClick -= this.reszz3_ButtonClick;
            this.toolStripMenuItem9.Click -= this.smtrze_Click;
            this.toolStripMenuItem10.Click -= this.smthtt_Click;
            this.movwin3.ButtonClick -= this.tswin3_DoubleClick;
            this.movtolef3.Click -= this.movtolef3_Click;
            this.movtrgt3.Click -= this.movtrgt_Click;
            this.movtbott3.Click -= this.movtbott3_Click;
            this.movtop3.Click -= this.movtop3_Click;
            this.freesty3.Click -= this.freesty3_Click;
            this.tswin3.MouseUp -= this.tswin3_MouseUp;
            this.tswin3.DoubleClick -= this.tswin3_DoubleClick;
            this.tswin3.MouseEnter -= this.tswin3_MouseEnter;
            this.tswin3.MouseDown -= this.tswin3_MouseDown;
            this.tswin3.MouseLeave -= this.tswin3_MouseLeave;
            this.tswin3.Click -= this.tswin3_Click;
            this.cnbnn3.MouseEnter -= this.cnbnn3_MouseEnter;
            this.cnbnn3.MouseLeave -= this.cnbnn3_MouseLeave;
            this.maxminmnu3.Click -= this.dvgpndoc3_Click;
            this.finalAccountToolStripMenuItem.Click -= this.tswin3_DoubleClick;
            this.rszmnu3.Click -= this.reszz3_ButtonClick;
            this.closeToTrayToolStripMenuItem.Click -= this.clse3_Click;
            this.clse3.Click -= this.clse3_Click;
            this.dvgpndoc3.Click -= this.dvgpndoc3_Click;
            this.tsttl2.Click -= this.tswin3_Click;
            this.toolStripButton24.Click -= this.toolStripButton24_Click;
            this.oaapp.Click -= this.oaapp_Click;
            this.zz.MouseLeave -= this.zz_MouseLeave;
            this.zz.MouseEnter -= this.zz_MouseEnter;
            this.toolStrip5.MouseEnter -= this.zz_MouseEnter;
            this.toolStrip5.MouseLeave -= this.zz_MouseLeave;
            this.toolStripButton2.Click -= this.toolStripButton24_Click;
            this.QA_allup.Click -= this.QA_allup_Click;
            this.QA_oneup.Click -= this.QA_allup_Click;
            this.QA_onedown.Click -= this.QA_allup_Click;
            this.QA_alldown.Click -= this.QA_allup_Click;
            this.remv_zz.Click -= this.remv_zz_Click;
            this.dgvwin.Click -= this.dgvwin_Click;
            this.lv4.Enter -= this.lv4_Enter;
            this.oeij.Click -= this.oeij_Click;
            this.tv4.AfterSelect -= this.tv_AfterSelect;
            this.toolStripButton4.Click -= this.dgvupall_Click_1;
            this.toolStripButton10.Click -= this.dgvupall_Click_1;
            this.toolStripButton13.Click -= this.dgvupall_Click_1;
            this.toolStripButton15.Click -= this.dgvupall_Click_1;
            this.lv.Enter -= this.lv4_Enter;
            this.tv.AfterSelect -= this.tv_AfterSelect;
            this.dgvupall.Click -= this.dgvupall_Click_1;
            this.dgvupone.Click -= this.dgvupall_Click_1;
            this.dgvdownone.Click -= this.dgvupall_Click_1;
            this.dgvdownall.Click -= this.dgvupall_Click_1;
            this.lv3.Enter -= this.lv4_Enter;
            this.tv3.AfterSelect -= this.tv_AfterSelect;
            this.toolStripButton141.Click -= this.dgvupall_Click_1;
            this.toolStripSplitButton6.Click -= this.dgvupall_Click_1;
            this.toolStripSplitButton9.Click -= this.dgvupall_Click_1;
            this.toolStripButton144.Click -= this.dgvupall_Click_1;
            this.lv2.Enter -= this.lv4_Enter;
            this.tv2.AfterSelect -= this.tv_AfterSelect;
            this.toolStripButton82.Click -= this.dgvupall_Click_1;
            this.toolStripSplitButton1.Click -= this.dgvupall_Click_1;
            this.toolStripSplitButton4.Click -= this.dgvupall_Click_1;
            this.toolStripButton85.Click -= this.dgvupall_Click_1;
            this.stwin.Click -= this.stwin_Click;
            this.reszz.ButtonClick -= this.reszz_ButtonClick;
            this.mvewin.ButtonClick -= this.mvewin_ButtonClick;
            this.movtolef.Click -= this.movtolef_clc;
            this.toRightToolStripMenuItem.Click -= this.movtor;
            this.tobott.Click -= this.tobott_Click;
            this.totop.Click -= this.totop_Click;
            this.freesty.Click -= this.freesty_Click;
            this.tswin.MouseUp -= this.tswin_MouseUp;
            this.tswin.DoubleClick -= this.tswin_DoubleClick;
            this.tswin.MouseEnter -= this.tswin3_MouseEnter;
            this.tswin.MouseDown -= this.tswin_MouseDown;
            this.tswin.MouseLeave -= this.tswin3_MouseLeave;
            this.tswin.Click -= this.tswin_Click;
            this.cnbnn1.MouseEnter -= this.cnbnn1_MouseEnter;
            this.cnbnn1.MouseLeave -= this.cnbnn1_MouseLeave;
            this.maxminmnu1.Click -= this.dvgpndoc_Click;
            this.toolStripMenuItem4.Click -= this.mvewin_ButtonClick;
            this.rszemnu.Click -= this.reszz_ButtonClick;
            this.toolStripMenuItem6.Click -= this.clse_Click;
            this.clse.Click -= this.clse_Click;
            this.dvgpndoc.Click -= this.dvgpndoc_Click;
            this.tsttl.DoubleClick -= this.tswin_DoubleClick;
            this.tsttl.Click -= this.tswin_Click;
            this.svewin3.Click -= this.svebtn_ButtonClick;
            this.toolStripButton8.Click -= this.toolStripButton24_Click;
            this.autsve.Tick -= this.autsvetck;
            this.dgvwintic.Tick -= this.dgvwintc;
            this.tmex.Tick -= this.tmex_Tick;
            this.restr.Click -= this.restr_Click;
            this.clsemn.Click -= this.clsejournclc;
            this.undoall.Click -= this.undoall_Click;
            this.cpy.Click -= this.cpy_Click;
            this.ct.Click -= this.ct_Click;
            this.connectionToolStripMenuItem.Click -= this.connectionToolStripMenuItem_Click;
            this.contentsToolStripMenuItem.Click -= this.contentsToolStripMenuItem_Click;
            this.abtmnu.Click -= this.abtmnu_Click;
            this.summaryToolStripMenuItem.Click -= this.jrntofnt_Click;
            this.toolStripMenuItem29.Click -= this.reszz_ButtonClick;
            this.simjrnwth.Click -= this.simjrnwth_Click;
            this.simjrnhgt.Click -= this.simjrnhgt_Click;
            this.stdflt.Click -= this.stdflt_Click;
            this.toolStripMenuItem35.Click -= this.mvewin_ButtonClick;
            this.toolStripMenuItem36.Click -= this.movtolef_clc;
            this.toolStripMenuItem37.Click -= this.movtor;
            this.toolStripMenuItem38.Click -= this.tobott_Click;
            this.toolStripMenuItem39.Click -= this.totop_Click;
            this.toolStripMenuItem40.Click -= this.freesty_Click;
            this.sttodefjrn.Click -= this.sttodefjrn_Click;
            this.mxmn.Click -= this.dvgpndoc_Click;
            this.clsettry2.Click -= this.clse_Click;
            this.journalToolStripMenuItem.Click -= this.smtofnt_Click;
            this.rszesumm.Click -= this.reszz3_ButtonClick;
            this.smtrze.Click -= this.smtrze_Click;
            this.smtwdt.Click -= this.smtrze_Click;
            this.smthtt.Click -= this.smthtt_Click;
            this.str.Click -= this.str_Click;
            this.mve.Click -= this.tswin3_DoubleClick;
            this.leftToolStripMenuItem.Click -= this.movtolef3_Click;
            this.rightToolStripMenuItem.Click -= this.movtrgt_Click;
            this.bottomToolStripMenuItem.Click -= this.movtbott3_Click;
            this.topToolStripMenuItem.Click -= this.movtop3_Click;
            this.freeStyleToolStripMenuItem.Click -= this.freesty3_Click;
            this.setToDefaultToolStripMenuItem1.Click -= this.setToDefauljkk_Click;
            this.maxminmnu.Click -= this.dvgpndoc3_Click;
            this.clsettry.Click -= this.clse3_Click;
            //this.minwins.Click -= this.minwins_Click;
            this.clseall.Click -= this.clseall_Click;
            this.restawin.Click -= this.restawin_Click;
            this.toolStripMenuItem15.Click -= this.slctrgb_Click;
            this.toolStripButton14.Click -= this.tswin_Click;
            this.toolStripButton12.Click -= this.tswin3_Click;
            this.dgvwintic3.Tick -= this.dgvwintic3_Tick;
            this.tmex3.Tick -= this.tmex3_Tick;
            this.ts2.MouseEnter -= this.ts2_MouseEnter_1;
            this.ts2.MouseLeave -= this.ts2_MouseLeave_1;
            this.tbxfned.TextChanged -= this.tbxfned_tch;
            this.journalToolStripMenuItem4.Click -= this.generalLedgerSalesBookValesToolStripMenuItem_Click;
            this.slctrgb.Click -= this.slctrgb_Click;
            this.enblhc.Click -= this.enblhc_Click_1;
            this.ewv.Click -= this.ewv_Click;
            this.dwv.Click -= this.dwv_Click;
            this.col.Tick -= this.col_Tick;
            this.tmeinit.Tick -= this.tmeinit_Tick;
            this.ttp_del.Tick -= this.ttp_del_Tick;
            this.tmr.Tick -= this.tmr_Tick;
            this.Deactivate -= this.acc_journ_dec;
            this.Load -= this.acc_ledg_Load;
            this.Activated -= this.acc_journ_act;

            al_years.Clear();

            //new dispose
            try
            {
                fnt.Dispose();
            }
            catch (Exception erty) { }
            try
            {
                dst_final_public.Dispose();
            }
            catch (Exception fuyf) { }
            try
            {
                dst_END_public.Dispose();
            }
            catch (Exception rtd) { }
            try
            {
                cmd_public.Dispose();
            }
            catch (Exception erty) { }
            try
            {
                read_public.Dispose();
            }
            catch (Exception ertybk) { }
            try
            {
                conn_public.Dispose();
            }
            catch (Exception ertyt) { }

            //END -|

            this.BindingContext = null;

            this.Events.Dispose();
            try
            {
                thinit.Abort();
                thinit2.Abort();
                th_sum.Abort();
            }
            catch (Exception erty) { }

            foreach (Control cnt in this.Controls)
            {
                cnt.Dispose();
            }

            this.components.Dispose();
            this.Dispose(true);
            GC.Collect();
        }

        int tab_print = 0;
        //Events
        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (sender.Equals(tv) == true)
            {
                by_date(1);
                tab_print = 0;
            }
            else if (sender.Equals(tv2) == true)
            {
                bydate2();
                tab_print = 1;
            }
            else if (sender.Equals(tv3) == true)
            {
                bydate3();
                tab_print = 2;
            }
            else if (sender.Equals(tv4) == true)
            {
                bydate4();
                tab_print = 3;
            }
        }

        private void dgvupall_Click_1(object sender, EventArgs e)
        {
            try
            {
                //CB
                if (sender.Equals(dgvupall) == true)
                {
                    int fok = lv.FocusedItem.Index;
                    lv.Items[0].Focused = true;
                    lv.Items[0].Selected = true;
                    lv.Items[fok].Selected = false;
                }
                if (sender.Equals(dgvupone) == true)
                {
                    int fok = lv.FocusedItem.Index;
                    lv.Items[lv.FocusedItem.Index - 1].Focused = true;
                    lv.Items[lv.FocusedItem.Index].Selected = true;
                    lv.Items[fok].Selected = false;
                }
                if (sender.Equals(dgvdownall) == true)
                {
                    int fok = lv.FocusedItem.Index;
                    lv.Items[lv.Items.Count - 1].Focused = true;
                    lv.Items[lv.FocusedItem.Index].Selected = true;
                    lv.Items[fok].Selected = false;
                }
                if (sender.Equals(dgvdownone) == true)
                {
                    int fok = lv.FocusedItem.Index;
                    lv.Items[lv.FocusedItem.Index + 1].Focused = true;
                    lv.Items[lv.FocusedItem.Index].Selected = true;
                    lv.Items[fok].Selected = false;
                }
                //PB
                if (sender.Equals(toolStripButton141) == true)
                {
                    int fok = lv3.FocusedItem.Index;
                    lv3.Items[0].Focused = true;
                    lv3.Items[0].Selected = true;
                    lv3.Items[fok].Selected = false;
                }
                if (sender.Equals(toolStripSplitButton6) == true)
                {
                    int fok = lv3.FocusedItem.Index;
                    lv3.Items[lv3.FocusedItem.Index - 1].Focused = true;
                    lv3.Items[lv3.FocusedItem.Index].Selected = true;
                    lv3.Items[fok].Selected = false;
                }
                if (sender.Equals(toolStripButton144) == true)
                {
                    int fok = lv3.FocusedItem.Index;
                    lv3.Items[lv3.Items.Count - 1].Focused = true;
                    lv3.Items[lv3.FocusedItem.Index].Selected = true;
                    lv3.Items[fok].Selected = false;
                }
                if (sender.Equals(toolStripSplitButton9) == true)
                {
                    int fok = lv3.FocusedItem.Index;
                    lv3.Items[lv3.FocusedItem.Index + 1].Focused = true;
                    lv3.Items[lv3.FocusedItem.Index].Selected = true;
                    lv3.Items[fok].Selected = false;
                }
                //GJ
                if (sender.Equals(toolStripButton4) == true)
                {
                    int fok = lv4.FocusedItem.Index;
                    lv4.Items[0].Focused = true;
                    lv4.Items[0].Selected = true;
                    lv4.Items[fok].Selected = false;
                }
                if (sender.Equals(toolStripButton10) == true)
                {
                    int fok = lv4.FocusedItem.Index;
                    lv4.Items[lv4.FocusedItem.Index - 1].Focused = true;
                    lv4.Items[lv4.FocusedItem.Index].Selected = true;
                    lv4.Items[fok].Selected = false;
                }
                if (sender.Equals(toolStripButton15) == true)
                {
                    int fok = lv4.FocusedItem.Index;
                    lv4.Items[lv4.Items.Count - 1].Focused = true;
                    lv4.Items[lv4.FocusedItem.Index].Selected = true;
                    lv4.Items[fok].Selected = false;
                }
                if (sender.Equals(toolStripButton13) == true)
                {
                    int fok = lv4.FocusedItem.Index;
                    lv4.Items[lv4.FocusedItem.Index + 1].Focused = true;
                    lv4.Items[lv4.FocusedItem.Index].Selected = true;
                    lv4.Items[fok].Selected = false;
                }
                //SB
                if (sender.Equals(toolStripButton82) == true)
                {
                    int fok = lv2.FocusedItem.Index;
                    lv2.Items[0].Focused = true;
                    lv2.Items[0].Selected = true;
                    lv2.Items[fok].Selected = false;
                }
                if (sender.Equals(toolStripSplitButton1) == true)
                {
                    int fok = lv2.FocusedItem.Index;
                    lv2.Items[lv2.FocusedItem.Index - 1].Focused = true;
                    lv2.Items[lv2.FocusedItem.Index].Selected = true;
                    lv2.Items[fok].Selected = false;
                }
                if (sender.Equals(toolStripButton85) == true)
                {
                    int fok = lv2.FocusedItem.Index;
                    lv2.Items[lv2.Items.Count - 1].Focused = true;
                    lv2.Items[lv2.FocusedItem.Index].Selected = true;
                    lv2.Items[fok].Selected = false;
                }
                if (sender.Equals(toolStripSplitButton4) == true)
                {
                    int fok = lv2.FocusedItem.Index;
                    lv2.Items[lv2.FocusedItem.Index + 1].Focused = true;
                    lv2.Items[lv2.FocusedItem.Index].Selected = true;
                    lv2.Items[fok].Selected = false;
                }
            }
            catch (Exception erty) { }
        }

        private void oku_bydte_Click(object sender, EventArgs e)
        {
            n = 0;
            n2 = 0;
            n3 = 0;
            n4 = 0;
            try
            {
                pb.Visible = true; 
                if (tabControl2.SelectedIndex == 0)
                {
                    if (sender.Equals(oku_bydte) == true)
                    {
                        if (tv4.SelectedNode.Text == "Asset Account Between Dates")
                        {
                            SqlString = "Select [Date of Transaction], Credit, Debit, Particulars FROM journal WHERE [Date of Transaction] > '" + toolStripTextBox8.Text + "' AND [Date of Transaction] < '" + toolStripTextBox9.Text + "' AND [Account Description] = 'Asset Account'";
                        }
                        if (tv4.SelectedNode.Text == "Liability Account Between Dates")
                        {
                            SqlString = "Select [Date of Transaction], Credit, Debit, Particulars FROM journal WHERE [Date of Transaction] > '" + toolStripTextBox8.Text + "' AND [Date of Transaction] < '" + toolStripTextBox9.Text + "' AND [Account Description] = 'Liability Account'";
                        }
                        if (tv4.SelectedNode.Text == "Revenue Account Between Dates")
                        {
                            SqlString = "Select [Date of Transaction], Credit, Debit, Particulars FROM journal WHERE [Date of Transaction] > '" + toolStripTextBox8.Text + "' AND [Date of Transaction] < '" + toolStripTextBox9.Text + "' AND [Account Description] = 'Revenue Account'";
                        }
                        if (tv4.SelectedNode.Text == "Expense Account Between Dates")
                        {
                            SqlString = "Select [Date of Transaction], Credit, Debit, Particulars FROM journal WHERE [Date of Transaction] > '" + toolStripTextBox8.Text + "' AND [Date of Transaction] < '" + toolStripTextBox9.Text + "' AND [Account Description] = 'Expense Account'";
                        }
                        if (tv4.SelectedNode.Text == "Dates")
                        {
                            SqlString = "Select [Date of Transaction], Credit, Debit, Particulars FROM journal WHERE [Date of Transaction] > '" + toolStripTextBox8.Text + "' AND [Date of Transaction] < '" + toolStripTextBox9.Text + "'";
                        }

                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        universal_query_builder(SqlString);
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                lv4.Items.Add(dataGridView1[0, dgvr.Index].Value.ToString(), 3);
                                try
                                {
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[2, dgvr.Index].Value);
                                }
                                catch (Exception ertr) { }
                            }
                            catch (Exception erty) { }
                        }
                        fnt = new Font(lv4.Font.FontFamily.GetName(0), lv4.Font.Size, FontStyle.Bold);
                        lv4.Items.Add("Total");
                        lv4.Items[lv4.Items.Count - 1].SubItems.Add(n.ToString());
                        lv4.Items[lv4.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv4.Items[lv4.Items.Count - 1].BackColor = Color.Lavender;
                        lv4.Items[lv4.Items.Count - 1].Font = fnt;
                    }
                    if (sender.Equals(toolStripMenuItem5) == true)
                    {
                        n = 0;
                        n2 = 0;
                        string s = "";
                        string s2 = "";
                        s = "01-" + toolStripComboBox1.Text + '-' + toolStripComboBox2.Text;
                        s2 = "01-" + toolStripComboBox3.Text + '-' + toolStripComboBox4.Text;
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select [Date of Transaction], Credit, Debit, Particulars FROM journal WHERE [Date of Transaction] > '" + s + "' AND [Date of Transaction] < '" + s2 + "'";
                        universal_query_builder(SqlString);
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                lv4.Items.Add(dataGridView1[0, dgvr.Index].Value.ToString(), 3);
                                try
                                {
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[2, dgvr.Index].Value);
                                }
                                catch (Exception ertr) { }
                            }
                            catch (Exception erty) { }
                        }
                        fnt = new Font(lv4.Font.FontFamily.GetName(0), lv4.Font.Size, FontStyle.Bold);
                        lv4.Items.Add("Total");
                        lv4.Items[lv4.Items.Count - 1].SubItems.Add(n.ToString());
                        lv4.Items[lv4.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv4.Items[lv4.Items.Count - 1].BackColor = Color.Lavender;
                        lv4.Items[lv4.Items.Count - 1].Font = fnt;
                    }
                    if (sender.Equals(toolStripMenuItem12) == true)
                    {
                        n = 0;
                        n2 = 0;
                        string s = "";
                        string s2 = "";
                        s = "01-01-" + toolStripComboBox5.Text;
                        s2 = "01-01-" + toolStripComboBox6.Text;
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select [Date of Transaction], Credit, Debit, Particulars FROM journal WHERE [Date of Transaction] > '" + s + "' AND [Date of Transaction] < '" + s2 + "'";

                        universal_query_builder(SqlString);
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                lv4.Items.Add(dataGridView1[0, dgvr.Index].Value.ToString(), 3);
                                try
                                {
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[2, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv4.Items[lv4.Items.Count - 1].SubItems.Add(dataGridView1[3, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[2, dgvr.Index].Value);
                                }
                                catch (Exception ertr) { }
                            }
                            catch (Exception erty) { }
                        }
                        fnt = new Font(lv4.Font.FontFamily.GetName(0), lv4.Font.Size, FontStyle.Bold);
                        lv4.Items.Add("Total");
                        lv4.Items[lv4.Items.Count - 1].SubItems.Add(n.ToString());
                        lv4.Items[lv4.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv4.Items[lv4.Items.Count - 1].BackColor = Color.Lavender;
                        lv4.Items[lv4.Items.Count - 1].Font = fnt;
                    }
                }
                if (tabControl2.SelectedIndex == 1)
                {
                    if (sender.Equals(oku_bydte) == true)
                    {
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select [Date], Credit, Debit, Particulars FROM CashBook WHERE [Date] > '" + toolStripTextBox8.Text + "' AND [Date] < '" + toolStripTextBox9.Text + "'";
                        universal_query_builder(SqlString);
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                lv.Items.Add(dataGridView1[0, dgvr.Index].Value.ToString(), 3);
                                try
                                {
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[2, dgvr.Index].Value);
                                }
                                catch (Exception ertr) { }
                            }
                            catch (Exception erty) { }
                        }
                        fnt = new Font(lv.Font.FontFamily.GetName(0), lv.Font.Size, FontStyle.Bold);
                        lv.Items.Add("Total");
                        lv.Items[lv.Items.Count - 1].SubItems.Add(n.ToString());
                        lv.Items[lv.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv.Items[lv.Items.Count - 1].BackColor = Color.Lavender;
                        lv.Items[lv.Items.Count - 1].Font = fnt;
                    }
                    if (sender.Equals(toolStripMenuItem5) == true)
                    {
                        n = 0;
                        n2 = 0;
                        string s = "";
                        string s2 = "";
                        s = "01-" + toolStripComboBox1.Text + '-' + toolStripComboBox2.Text;
                        s2 = "01-" + toolStripComboBox3.Text + '-' + toolStripComboBox4.Text;
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select [Date], Credit, Debit, Particulars FROM CashBook WHERE [Date] > '" + s + "' AND [Date] < '" + s2 + "'";//

                        universal_query_builder(SqlString);
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                lv.Items.Add(dataGridView1[0, dgvr.Index].Value.ToString(), 3);
                                try
                                {
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[1, dgvr.Index].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[2, dgvr.Index].Value);
                                }
                                catch (Exception ertr) { }
                            }
                            catch (Exception erty) { }
                        }
                        fnt = new Font(lv.Font.FontFamily.GetName(0), lv.Font.Size, FontStyle.Bold);
                        lv.Items.Add("Total");
                        lv.Items[lv.Items.Count - 1].SubItems.Add(n.ToString());
                        lv.Items[lv.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv.Items[lv.Items.Count - 1].BackColor = Color.Lavender;
                        lv.Items[lv.Items.Count - 1].Font = fnt;
                    }
                    if (sender.Equals(toolStripMenuItem12) == true)
                    {
                        n = 0;
                        n2 = 0;
                        string s = "";
                        string s2 = "";
                        s = "01-01-" + toolStripComboBox5.Text;
                        s2 = "01-01-" + toolStripComboBox6.Text;
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select [Date], Credit, Debit, Particulars FROM CashBook WHERE [Date] > '" + s + "' AND [Date] < '" + s2 + "'";

                        universal_query_builder(SqlString);
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                lv.Items.Add(dataGridView1[0, dgvr.Index].Value.ToString(), 3);
                                try
                                {
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[2, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv.Items[lv.Items.Count - 1].SubItems.Add(dataGridView1[3, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[2, dgvr.Index].Value);
                                }
                                catch (Exception ertr) { }
                            }
                            catch (Exception erty) { }
                        }
                        fnt = new Font(lv.Font.FontFamily.GetName(0), lv.Font.Size, FontStyle.Bold);
                        lv.Items.Add("Total");
                        lv.Items[lv.Items.Count - 1].SubItems.Add(n.ToString());
                        lv.Items[lv.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv.Items[lv.Items.Count - 1].BackColor = Color.Lavender;
                        lv.Items[lv.Items.Count - 1].Font = fnt;
                    }
                }
                if (tabControl2.SelectedIndex == 2)
                {
                    if (sender.Equals(oku_bydte) == true)
                    {
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select [Date of Purchase], Credit, Debit, Particulars FROM PurchaseBook WHERE [Date of Purchase] > '" + toolStripTextBox8.Text + "' AND [Date of Purchase] < '" + toolStripTextBox9.Text + "'";
                        universal_query_builder(SqlString);
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                lv3.Items.Add(dataGridView1[0, dgvr.Index].Value.ToString(), 3);
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { } 
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[2, dgvr.Index].Value);
                                }
                                catch (Exception ertr) { }
                            }
                            catch (Exception erty) { }
                        }
                        fnt = new Font(lv3.Font.FontFamily.GetName(0), lv3.Font.Size, FontStyle.Bold);
                        lv3.Items.Add("Total");
                        lv3.Items[lv3.Items.Count - 1].SubItems.Add(n.ToString());
                        lv3.Items[lv3.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv3.Items[lv3.Items.Count - 1].BackColor = Color.Lavender;
                        lv3.Items[lv3.Items.Count - 1].Font = fnt;
                    }
                    if (sender.Equals(toolStripMenuItem5) == true)
                    {
                        n = 0;
                        n2 = 0;
                        string s = "";
                        string s2 = "";
                        s = "01-" + toolStripComboBox1.Text + '-' + toolStripComboBox2.Text;
                        s2 = "01-" + toolStripComboBox3.Text + '-' + toolStripComboBox4.Text;
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select [Date of Purchase], Credit, Debit Particulars FROM PurchaseBook WHERE [Date of Purchase] > '" + s + "' AND [Date of Purchase] < '" + s2 + "'";//

                        universal_query_builder(SqlString);
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                lv3.Items.Add(dataGridView1[0, dgvr.Index].Value.ToString(), 3);
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[1, dgvr.Index].Value);
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[2, dgvr.Index].Value);
                                }
                                catch (Exception ertr) { }
                            }
                            catch (Exception erty) { }
                        }
                        fnt = new Font(lv3.Font.FontFamily.GetName(0), lv3.Font.Size, FontStyle.Bold);
                        lv3.Items.Add("Total");
                        lv3.Items[lv3.Items.Count - 1].SubItems.Add(n.ToString());
                        lv3.Items[lv3.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv3.Items[lv3.Items.Count - 1].BackColor = Color.Lavender;
                        lv3.Items[lv3.Items.Count - 1].Font = fnt;
                    }
                    if (sender.Equals(toolStripMenuItem12) == true)
                    {
                        n = 0;
                        n2 = 0;
                        string s = "";
                        string s2 = "";
                        s = "01-01-" + toolStripComboBox5.Text;
                        s2 = "01-01-" + toolStripComboBox6.Text;
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select [Date of Purchase], Credit, Debit, Particulars FROM PurchaseBook WHERE [Date of Purchase] > '" + s + "' AND [Date of Purchase] < '" + s2 + "'";//

                        universal_query_builder(SqlString);
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                lv3.Items.Add(dataGridView1[0, dgvr.Index].Value.ToString(), 3);
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[2, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv3.Items[lv3.Items.Count - 1].SubItems.Add(dataGridView1[3, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[2, dgvr.Index].Value);
                                }
                                catch (Exception ertr) { }
                            }
                            catch (Exception erty) { }
                        }
                        fnt = new Font(lv3.Font.FontFamily.GetName(0), lv3.Font.Size, FontStyle.Bold);
                        lv3.Items.Add("Total");
                        lv3.Items[lv3.Items.Count - 1].SubItems.Add(n.ToString());
                        lv3.Items[lv3.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv3.Items[lv3.Items.Count - 1].BackColor = Color.Lavender;
                        lv3.Items[lv3.Items.Count - 1].Font = fnt;
                    }
                }
                if (tabControl2.SelectedIndex == 3)
                {
                    if (sender.Equals(oku_bydte) == true)
                    {
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select [Date of Sale], Credit, Particulars FROM SalesBook WHERE [Date of Sale] > '" + toolStripTextBox8.Text + "' AND [Date of Sale] < '" + toolStripTextBox9.Text + "'";// AND DatePart(MM, [Date]) > '" + toolStripTextBox4.Text + "' AND DatePart(MM, [Date]) < '" + toolStripComboBox1.Text + "' AND DatePart(DD, [Date]) > '" + toolStripTextBox2.Text + "' AND DatePart(DD, [Date]) < '" + toolStripComboBox2.Text + "'";

                        universal_query_builder(SqlString);
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                lv2.Items.Add(dataGridView1[0, dgvr.Index].Value.ToString(), 3);
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[2, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[3, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[2, dgvr.Index].Value);
                                }
                                catch (Exception ertr) { }
                            }
                            catch (Exception erty) { }
                        }
                        fnt = new Font(lv2.Font.FontFamily.GetName(0), lv2.Font.Size, FontStyle.Bold);
                        lv2.Items.Add("Total");
                        lv2.Items[lv2.Items.Count - 1].SubItems.Add(n.ToString());
                        lv2.Items[lv2.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv2.Items[lv2.Items.Count - 1].BackColor = Color.Lavender;
                        lv2.Items[lv2.Items.Count - 1].Font = fnt;
                    }
                    if (sender.Equals(toolStripMenuItem5) == true)
                    {
                        n = 0;
                        n2 = 0;
                        string s = "";
                        string s2 = "";
                        s = "01-" + toolStripComboBox1.Text + '-' + toolStripComboBox2.Text;
                        s2 = "01-" + toolStripComboBox3.Text + '-' + toolStripComboBox4.Text;
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select [Date of Sale], Credit, Particulars FROM SalesBook WHERE [Date of Sale] > '" + s + "' AND [Date of Sale] < '" + s2 + "'";//
                        /*using (conn_public = new SqlCeConnection(ConnString))
                        {
                            using (cmd_public = new SqlCeCommand(SqlString, conn_public))
                            {

                                conn_public.Open();
                                read_public = cmd_public.ExecuteReader();
                                using (read_public)
                                {
                                    dst_final_public.Load(read_public, LoadOption.PreserveChanges);
                                    dataGridView1.DataSource = dst_final_public;
                                }
                            }
                        }*/
                        universal_query_builder(SqlString);
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                lv2.Items.Add(dataGridView1[0, dgvr.Index].Value.ToString(), 3);
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[2, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[3, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[2, dgvr.Index].Value);
                                }
                                catch (Exception ertr) { }
                            }
                            catch (Exception erty) { }
                        }
                        fnt = new Font(lv2.Font.FontFamily.GetName(0), lv2.Font.Size, FontStyle.Bold);
                        lv2.Items.Add("Total");
                        lv2.Items[lv2.Items.Count - 1].SubItems.Add(n.ToString());
                        lv2.Items[lv2.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv2.Items[lv2.Items.Count - 1].BackColor = Color.Lavender;
                        lv2.Items[lv2.Items.Count - 1].Font = fnt;
                    }
                    if (sender.Equals(toolStripMenuItem12) == true)
                    {
                        n = 0;
                        n2 = 0;
                        string s = "";
                        string s2 = "";
                        s = "01-01-" + toolStripComboBox5.Text;
                        s2 = "01-01-" + toolStripComboBox6.Text;
                        if (Main.Amatrix.acc == "")
                        {
                            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                        }
                        else
                        {
                            ConnString = Main.Amatrix.acc;
                        }
                        SqlString = "Select [Date of Sale], Credit, Particulars FROM SalesBook WHERE [Date of Sale] > '" + s + "' AND [Date of Sale] < '" + s2 + "'";//

                        universal_query_builder(SqlString);
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            try
                            {
                                lv2.Items.Add(dataGridView1[0, dgvr.Index].Value.ToString(), 3);
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[2, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    lv2.Items[lv2.Items.Count - 1].SubItems.Add(dataGridView1[3, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n = n + Convert.ToDouble(dataGridView1[1, dgvr.Index].Value.ToString());
                                }
                                catch (Exception erty) { }
                                try
                                {
                                    n2 = n2 + Convert.ToDouble(dataGridView1[2, dgvr.Index].Value);
                                }
                                catch (Exception ertr) { }
                            }
                            catch (Exception erty) { }
                        }
                        fnt = new Font(lv2.Font.FontFamily.GetName(0), lv2.Font.Size, FontStyle.Bold);
                        lv2.Items.Add("Total");
                        lv2.Items[lv2.Items.Count - 1].SubItems.Add(n.ToString());
                        lv2.Items[lv2.Items.Count - 1].SubItems.Add(n2.ToString());
                        lv2.Items[lv2.Items.Count - 1].BackColor = Color.Lavender;
                        lv2.Items[lv2.Items.Count - 1].Font = fnt;
                    }
                }
                pb.Visible = false;
            }
            catch (Exception erty) { }
        }

        //gui helpers
        private int sel2;
        private void tvtxt202_KeyUp(object sender, KeyEventArgs e)
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

        private void tvtxt12_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                tstt = (ToolStripTextBox)sender;
                tstt.BackColor = Color.Lavender;
            }
            catch (Exception erty) { }
        }

        private void tvtxt12_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                tstt = (ToolStripTextBox)sender;
                tstt.BackColor = Color.White;
            }
            catch (Exception erty) { }
        }

        private void tvtxt202_Click(object sender, EventArgs e)
        {
            tstt = (ToolStripTextBox)sender;
            if (tstt.Text == "Enter a Date (eg. 01-jan-91)" || tstt.Text == "Show Invoice Of" || tstt.Text == "Enter First Date (eg. 01-Jan-91)" || tstt.Text == "Enter Last Date (eg. 02-Feb-92)" || tstt.Text == "Enter a Value" || tstt.Text == "Enter a Value(Piece)" || tstt.Text == "Enter Lower Value" || tstt.Text == "Enter Greater Value")
            {
                tstt.SelectAll();
            }
        }

        private string str_builder, sq_build, temp_str;
        int days_MM, days_MM_fin, year;
        private void oeij_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl2.SelectedIndex == 0)
                {
                    if (str_builder == "Average Debit/Credit Per Month (Current)")
                    {
                        days_MM = DIM(lv.FocusedItem.Text);
                        try
                        {
                            temp_str = lv.FocusedItem.Text.Remove(3);
                        }
                        catch (Exception erty) { }
                        year = Convert.ToInt32(lv.FocusedItem.SubItems[4].Text);
                        days_MM_fin = DateTime.DaysInMonth(year, days_MM);
                        sq_build = "Select * From CashBook Where [Date] > '01-" + temp_str + '-' + lv.FocusedItem.SubItems[4].Text + "' AND [Date] < '" + days_MM_fin.ToString() + '-' + temp_str + '-' + lv.FocusedItem.SubItems[4].Text + "'";
                        acc_journ jrn = new acc_journ();
                        jrn.Show();
                        jrn.Extern(sq_build, "CashBook");
                    }
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Unable to Open Row in Journal. The Row may be Empty."); }
        }

        private int DIM(string Month)
        {
            int retur = 0;
            if (Month.ToLower() == "january")
            {
                retur = 1;
            }
            if (Month.ToLower() == "febuary")
            {
                retur = 2;
            }
            if (Month.ToLower() == "march")
            {
                retur = 3;
            }
            if (Month.ToLower() == "april")
            {
                retur = 4;
            }
            if (Month.ToLower() == "may")
            {
                retur = 5;
            }
            if (Month.ToLower() == "june")
            {
                retur = 6;
            }
            if (Month.ToLower() == "july")
            {
                retur = 7;
            }
            if (Month.ToLower() == "august")
            {
                retur = 8;
            }
            if (Month.ToLower() == "september")
            {
                retur = 9;
            }
            if (Month.ToLower() == "october")
            {
                retur = 10;
            }
            if (Month.ToLower() == "november")
            {
                retur = 11;
            }
            if (Month.ToLower() == "december")
            {
                retur = 12;
            }
            return retur;
        }

        private void lv4_Enter(object sender, EventArgs e)
        {
            zz_yn(true);
        }

        private void QA_allup_Click(object sender, EventArgs e)
        {
            try
            {
                //upall
                if (sender.Equals(QA_allup) == true)
                {
                    if (tabControl2.SelectedIndex == 1)
                    {
                        int fok = lv.FocusedItem.Index;
                        lv.Items[0].Focused = true;
                        lv.Items[0].Selected = true;
                        lv.Items[fok].Selected = false;
                    }
                    if (tabControl2.SelectedIndex == 0)
                    {
                        int fok = lv4.FocusedItem.Index;
                        lv4.Items[0].Focused = true;
                        lv4.Items[0].Selected = true;
                        lv4.Items[fok].Selected = false;
                    }
                    if (tabControl2.SelectedIndex == 2)
                    {
                        int fok = lv3.FocusedItem.Index;
                        lv3.Items[0].Focused = true;
                        lv3.Items[0].Selected = true;
                        lv3.Items[fok].Selected = false;
                    }
                    if (tabControl2.SelectedIndex == 3)
                    {
                        int fok = lv2.FocusedItem.Index;
                        lv2.Items[0].Focused = true;
                        lv2.Items[0].Selected = true;
                        lv2.Items[fok].Selected = false;
                    }
                }
                //upone
                if (sender.Equals(QA_oneup) == true)
                {
                    if (tabControl2.SelectedIndex == 0)
                    {
                        int fok = lv4.FocusedItem.Index;
                        lv4.Items[lv4.FocusedItem.Index - 1].Focused = true;
                        lv4.Items[lv4.FocusedItem.Index].Selected = true;
                        lv4.Items[fok].Selected = false;
                    }
                    if (tabControl2.SelectedIndex == 1)
                    {
                        int fok = lv.FocusedItem.Index;
                        lv.Items[lv.FocusedItem.Index - 1].Focused = true;
                        lv.Items[lv.FocusedItem.Index].Selected = true;
                        lv.Items[fok].Selected = false;
                    }
                    if (tabControl2.SelectedIndex == 2)
                    {
                        int fok = lv3.FocusedItem.Index;
                        lv3.Items[lv3.FocusedItem.Index - 1].Focused = true;
                        lv3.Items[lv3.FocusedItem.Index].Selected = true;
                        lv3.Items[fok].Selected = false;
                    }
                    if (tabControl2.SelectedIndex == 3)
                    {
                        int fok = lv2.FocusedItem.Index;
                        lv2.Items[lv2.FocusedItem.Index - 1].Focused = true;
                        lv2.Items[lv2.FocusedItem.Index].Selected = true;
                        lv2.Items[fok].Selected = false;
                    }
                }
                //down one
                if (sender.Equals(QA_alldown) == true)
                {
                    if (tabControl2.SelectedIndex == 0)
                    {
                        int fok = lv4.FocusedItem.Index;
                        lv4.Items[lv4.Items.Count - 1].Focused = true;
                        lv4.Items[lv4.FocusedItem.Index].Selected = true;
                        lv4.Items[fok].Selected = false;
                    } 
                    if (tabControl2.SelectedIndex == 1)
                    {
                        int fok = lv.FocusedItem.Index;
                        lv.Items[lv.Items.Count - 1].Focused = true;
                        lv.Items[lv.FocusedItem.Index].Selected = true;
                        lv.Items[fok].Selected = false;
                    } 
                    if (tabControl2.SelectedIndex == 2)
                    {
                        int fok = lv3.FocusedItem.Index;
                        lv3.Items[lv3.Items.Count - 1].Focused = true;
                        lv3.Items[lv3.FocusedItem.Index].Selected = true;
                        lv3.Items[fok].Selected = false;
                    } 
                    if (tabControl2.SelectedIndex == 3)
                    {
                        int fok = lv2.FocusedItem.Index;
                        lv2.Items[lv2.Items.Count - 1].Focused = true;
                        lv2.Items[lv2.FocusedItem.Index].Selected = true;
                        lv2.Items[fok].Selected = false;
                    }
                }
                //down all
                if (sender.Equals(QA_onedown) == true)
                {
                    if (tabControl2.SelectedIndex == 0)
                    {
                        int fok = lv4.FocusedItem.Index;
                        lv4.Items[lv4.FocusedItem.Index + 1].Focused = true;
                        lv4.Items[lv4.FocusedItem.Index].Selected = true;
                        lv4.Items[fok].Selected = false;
                    }
                    if (tabControl2.SelectedIndex == 1)
                    {
                        int fok = lv.FocusedItem.Index;
                        lv.Items[lv.FocusedItem.Index + 1].Focused = true;
                        lv.Items[lv.FocusedItem.Index].Selected = true;
                        lv.Items[fok].Selected = false;
                    }
                    if (tabControl2.SelectedIndex == 2)
                    {
                        int fok = lv3.FocusedItem.Index;
                        lv3.Items[lv3.FocusedItem.Index + 1].Focused = true;
                        lv3.Items[lv3.FocusedItem.Index].Selected = true;
                        lv3.Items[fok].Selected = false;
                    }
                    if (tabControl2.SelectedIndex == 3)
                    {
                        int fok = lv2.FocusedItem.Index;
                        lv2.Items[lv2.FocusedItem.Index + 1].Focused = true;
                        lv2.Items[lv2.FocusedItem.Index].Selected = true;
                        lv2.Items[fok].Selected = false;
                    }
                }
            }
            catch (Exception erty) { }
        }

        private void connectToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy lg = new loggy();
            lg.Show();
        }

        private DataTable dtp = new DataTable();
        private void gu_Click(object sender, EventArgs e)
        {
            string SQL_Q = ""; dataGridView3.Columns.Clear();
            dtp = new DataTable();
            if (radioButton7.Checked == true && radioButton1.Checked == true)
            {
                SQL_Q = "SELECT * FROM journal WHERE [Particulars] LIKE '%" + textBox1.Text + "%'";
            }
            if (radioButton5.Checked == true && radioButton1.Checked == true)
            {
                SQL_Q = "SELECT sum(Credit) as [Sum of Credit Values], sum(Debit) as [Sum of Debit Values] FROM journal WHERE [Particulars] LIKE '%" + textBox1.Text + "%'";
            }
            if (radioButton6.Checked == true && radioButton1.Checked == true)
            {
                SQL_Q = "SELECT avg(Credit) as [Average of Credit Values], avg(Debit) as [Average of Debit Values] FROM journal WHERE [Particulars] LIKE '%" + textBox1.Text + "%'";
            }
            //2
            if (radioButton7.Checked == true && radioButton3.Checked == true)
            {
                SQL_Q = "SELECT * FROM CashBook WHERE [Particulars] LIKE '%" + textBox1.Text + "%'";
            }
            if (radioButton5.Checked == true && radioButton3.Checked == true)
            {
                SQL_Q = "SELECT sum(Credit) as [Sum of Credit Values], sum(Debit) as [Sum of Debit Values] FROM CashBook WHERE [Particulars] LIKE '%" + textBox1.Text + "%'";
            }
            if (radioButton6.Checked == true && radioButton3.Checked == true)
            {
                SQL_Q = "SELECT avg(Credit) as [Average of Credit Values], avg(Debit) as [Average of Debit Values] FROM CashBook WHERE [Particulars] LIKE '%" + textBox1.Text + "%'";
            }
            //3
            if (radioButton7.Checked == true && radioButton2.Checked == true)
            {
                SQL_Q = "SELECT * FROM PurchaseBook WHERE [Particulars] LIKE '%" + textBox1.Text + "%'";
            }
            if (radioButton5.Checked == true && radioButton2.Checked == true)
            {
                SQL_Q = "SELECT sum(Credit) as [Sum of Credit Values], sum(Debit) as [Sum of Debit Values] FROM PurchaseBook WHERE [Particulars] LIKE '%" + textBox1.Text + "%'";
            }
            if (radioButton6.Checked == true && radioButton2.Checked == true)
            {
                SQL_Q = "SELECT avg(Credit) as [Average of Credit Values], avg(Debit) as [Average of Debit Values] FROM PurchaseBook WHERE [Particulars] LIKE '%" + textBox1.Text + "%'";
            }
            //4
            if (radioButton7.Checked == true && radioButton4.Checked == true)
            {
                SQL_Q = "SELECT * FROM SalesBook WHERE [Particulars] LIKE '%" + textBox1.Text + "%'";
            }
            if (radioButton5.Checked == true && radioButton4.Checked == true)
            {
                SQL_Q = "SELECT sum(Credit) as [Sum of Credit Values] FROM SalesBook WHERE [Particulars] LIKE '%" + textBox1.Text + "%'";
            }
            if (radioButton6.Checked == true && radioButton4.Checked == true)
            {
                SQL_Q = "SELECT avg(Credit) as [Average of Credit Values] FROM SalesBook WHERE [Particulars] LIKE '%" + textBox1.Text + "%'";
            }

            if (Main.Amatrix.acc == "")
            {
                ConnString = Properties.Settings.Default.AmdtbseConnectionString;
                SqlCeConnection conn = new SqlCeConnection(ConnString);
                SqlCeCommand cmd = new SqlCeCommand(SQL_Q, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp.Load(dr);
                conn.Close();
                dataGridView3.DataSource = dtp;
            }
            else
            {
                dtp = basql.Execute(Main.Amatrix.acc, SQL_Q, "", dtp);
                dataGridView3.DataSource = dtp;
            }
        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            acc_rep_wiz rpwiz = new acc_rep_wiz();
            rpwiz.Show();
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.tx(this.Name);
        }

        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy_adv adv = new loggy_adv();
            adv.Show();
        }

        private void generalLedgerSalesBookValesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            acc_journ jrn = new acc_journ();
            jrn.Show();
        }

        private DataGridView dgv_print = new DataGridView();
        private void printLedgerValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgv_print.Rows.Clear();
            dgv_print.Columns.Clear(); int columns = 0;
            if(tab_print == 0)
            {
                foreach (ColumnHeader ch in lv.Columns)
                {
                    dgv_print.Columns.Add(ch.Text, ch.Text);
                    columns++;
                }
                foreach (ListViewItem lvi in lv.Items)
                {
                    dgv_print.Rows.Add();
                    dgv_print[0, lvi.Index].Value = lvi.Text;
                    for (int i = 0; i <= columns; i++)
                    {
                        try
                        {
                            dgv_print[i, lvi.Index].Value = lvi.SubItems[i].Text;
                        }
                        catch (Exception erty) { try { dgv_print[i, lvi.Index].Value = ""; } catch (Exception ertyl) { } }
                    }
                }
            }
            if (tab_print == 1)
            {
                foreach (ColumnHeader ch in lv2.Columns)
                {
                    dgv_print.Columns.Add(ch.Text, ch.Text);
                    columns++;
                }
                foreach (ListViewItem lvi in lv2.Items)
                {
                    dgv_print.Rows.Add();
                    dgv_print[0, lvi.Index].Value = lvi.Text;
                    for (int i = 0; i <= columns; i++)
                    {
                        try
                        {
                            dgv_print[i, lvi.Index].Value = lvi.SubItems[i].Text;
                        }
                        catch (Exception erty) { try { dgv_print[i, lvi.Index].Value = ""; } catch (Exception ertyl) { } }
                    }
                }
            }
            if (tab_print == 2)
            {
                foreach (ColumnHeader ch in lv3.Columns)
                {
                    dgv_print.Columns.Add(ch.Text, ch.Text);
                    columns++;
                }
                foreach (ListViewItem lvi in lv3.Items)
                {
                    dgv_print.Rows.Add();
                    dgv_print[0, lvi.Index].Value = lvi.Text;
                    for (int i = 0; i <= columns; i++)
                    {
                        try
                        {
                            dgv_print[i, lvi.Index].Value = lvi.SubItems[i].Text;
                        }
                        catch (Exception erty) { try { dgv_print[i, lvi.Index].Value = ""; } catch (Exception ertyl) { } }
                    }
                }
            }
            if (tab_print == 3)
            {
                foreach (ColumnHeader ch in lv4.Columns)
                {
                    dgv_print.Columns.Add(ch.Text, ch.Text);
                    columns++;
                }
                foreach (ListViewItem lvi in lv4.Items)
                {
                    dgv_print.Rows.Add();
                    dgv_print[0, lvi.Index].Value = lvi.Text;
                    for (int i = 0; i <= columns; i++)
                    {
                        try
                        {
                            dgv_print[i, lvi.Index].Value = lvi.SubItems[i].Text;
                        }
                        catch (Exception erty) { try { dgv_print[i, lvi.Index].Value = ""; } catch (Exception ertyl) { } }
                    }
                }
            }
            PrintDataGrid.PrintDGV.Print_DataGridView(dgv_print);
        }

        private void printValuesInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDataGrid.PrintDGV.Print_DataGridView(dataGridView3);
        }

        private void xuy(object sender, EventArgs e)
        {
            try
            {
                dgvwintic.Stop();
                dgvwintic3.Stop();
                tmex.Stop();
                tmex3.Stop();
            }
            catch (Exception exct) { }
            gadg_resz1.Visible = false;
            if (summpnl.Controls.IndexOf(gadg_resz1) != -1)
            {
                summpnl.BringToFront();
            }
            else if (dgvwin.Controls.IndexOf(gadg_resz1) != -1)
            {
                dgvwin.BringToFront();
            }
        }
        //-END-
     }
}
