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
using System.Data.SqlServerCe;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Threading;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Main
{
    public partial class acc_trns : Form
    {
        ArrayList ar_click = new ArrayList();
        ArrayList ar_MouseDU = new ArrayList();
        //ArrayList ar_MouseU = new ArrayList();
        //Threading Objects
        private Thread thinit;
        private delegate void delinit();
        

        public acc_trns()
        {
            this.Icon = Properties.Resources.amdsicnico;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            clsejourn.Image = Properties.Resources.extfin;
            this.Text = "Amatrix Report Generator";
            try
            {
                initer.Start();
            }
            catch (Exception erty)
            {
                Init();
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

        string Template, Type; bool Print_all;
        public void tx(string Template2, string Type2, bool Print_all2)
        {
            this.Show();
            Template = Template2;
            Type = Type2;
            Print_all = Print_all2;
        }

        private void txstrt_Tick(object sender, EventArgs e)
        {
            txstrt.Stop();
            if (Print_all == true)
            {
                checkBox3.Checked = true;
            }
            try
            {
                if (Template.ToLower().EndsWith("simple journal"))
                {
                    this.Text = this.Text + " : Journal";
                    th_journsimp_srtr();
                }
                if (Template.ToLower().EndsWith("ledger"))
                {
                    this.Text = this.Text + " : Ledger";
                }
                if (Template.ToLower().EndsWith("Invoice"))
                {
                    this.Text = this.Text + " : Invoices";
                }
            }
            catch (Exception erty) { }
        }

        private Thread th_journsimp;
        private delegate void del_journsimp();

        private void th_journsimp_srtr()
        {
            th_journsimp = new Thread(new ThreadStart(del_journsimp_srtr));
            th_journsimp.IsBackground = true;
            th_journsimp.Start();
        }

        private void del_journsimp_srtr()
        {
            this.Invoke(new del_journsimp(simpjourn));
        }

        private void thinitstrt()
        {
            try
            {
                thinit = new Thread(new ThreadStart(delinitstrt));
                thinit.Start();
            }
            catch (Exception erty)
            {
                Init();
            }
        }

        private void delinitstrt()
        {
            try
            {
                this.Invoke(new delinit(Init));
            }
            catch (Exception erty)
            {
                Init();
            }
        }

        private void Init()
        {
            pnl1.Select();
            if (choicesett.Default.tpmst == true)
            {
                this.TopMost = true;
            }
            else if (choicesett.Default.tpmst == false)
            {
                this.TopMost = false;
            }
            connctdb();
        }

        private void connctdb()
        {
            pnl_working.Dispose();
        }

        private void acc_trns_Load(object sender, EventArgs e)
        {
        }

        private void col_Tick(object sender, EventArgs e)
        {
        }

        private void selctcol_Click(object sender, EventArgs e)
        {
            Main.cols cls = new cols();
            cls.Show();
        }

        private void disbltc(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.lockstat == "Locked")
            {
                this.Enabled = false;
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
            else if (Properties.Settings.Default.lockstat == "none")
            {
                this.Enabled = true;
                this.Visible = true;
                this.ShowInTaskbar = true;
            }
            else
            { }
        }

        private void clsejourn_me(object sender, EventArgs e)
        {
            clsejourn.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void clsejourn_ml(object sender, EventArgs e)
        {
            clsejourn.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void tmeclse_tc(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
        }

        private void clsejournclc(object sender, EventArgs e)
        {
            tmeclse.Start();
        }

        private void rstrt2_Click(object sender, EventArgs e)
        {
            acc_trns tnf = new acc_trns();
            tnf.Show();
            this.Close();
        }

        private void dectns_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectns.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void acc_trns_Activated(object sender, EventArgs e)
        {
            try
            {
                dectns.Stop();
            }
            catch (Exception tex)
            {
            }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void acc_trns_Deactivate(object sender, EventArgs e)
        {
            dectns.Start();
        }

        private void connlblme(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = Properties.Resources.bannrrageconv;
        }

        private void connlblml(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = null;
        }

        private void svebtn_ButtonClick(object sender, EventArgs e)
        {
        }

        private void initer_Tick(object sender, EventArgs e)
        {
            try
            {
                initer.Stop();
                Init();
            }
            catch(Exception erty)
            {
                
            }
        }

        private void show_AllToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void nwjrn_Click(object sender, EventArgs e)
        {
            acc_rep_wiz wrdwr = new acc_rep_wiz();
            wrdwr.Show();
        }

        private void lblcrt_btn_Click(object sender, EventArgs e)
        {
            if (sender.Equals(lblcrt_btn) == true)
            {
                lblcrt();
            }
            if (sender.Equals(new_pic) == true)
            {
                piccrt();
            }
        }

        //control working

        Panel pnl;
        private void lblcrt()
        {
            Label lbl = new Label();
            lbl.Text = "Text Label";
            lbl.ContextMenuStrip = cms;
            lbl.Click += new EventHandler(Click_);
            lbl.MouseDown += new MouseEventHandler(MouseDown_);
            lbl.MouseUp += new MouseEventHandler(MouseUp_);
            ar_MouseDU.Add(lbl);
            ar_click.Add(lbl);

            pnl.Controls.Add(lbl);
            drag.Start();
            cntr_temp = (Control)lbl;
        }
        private void piccrt()
        {
            PictureBox pbx = new PictureBox();
            pbx.BackgroundImage = Properties.Resources.pict;
            pbx.BackgroundImageLayout = ImageLayout.Center;
            pbx.BorderStyle = BorderStyle.FixedSingle;
            pbx.ContextMenuStrip = cms;
            pbx.Click += new EventHandler(Click_);
            pbx.MouseDown += new MouseEventHandler(MouseDown_);
            pbx.MouseUp += new MouseEventHandler(MouseUp_);
            ar_click.Add(pbx);
            ar_MouseDU.Add(pbx);

            pnl.Controls.Add(pbx);
            drag.Start();
            cntr_temp = (Control)pbx;
        }

        private void Click_(object sender, EventArgs e)
        {
            
        }

        void MouseDown_(object sender, MouseEventArgs e)
        {
            cntr_temp = (Control)sender;
            cntr_temp.BringToFront();
            x = Cursor.Position.X;
            y = Cursor.Position.Y;
            drag.Start();
        }

        void MouseUp_(object sender, MouseEventArgs e)
        {
            drag.Stop();
        }

        private void drag_Tick(object sender, EventArgs e)
        {
            if (Cursor.Position.X != x && Cursor.Position.Y != y)
            {
                control_move();
            }
        }

        private Control cntr_temp, cntr_temp2;
        int x, y;
        private void control_move()
        {
            try
            {
                cntr_temp.Location = new Point(((Cursor.Position.X - this.Location.X) - cntr_temp.Size.Width / 2) - pnl.Location.X, ((Cursor.Position.Y - this.Location.Y) - 59) - pnl.Location.Y);
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

        private void pnl1_MouseEnter(object sender, EventArgs e)
        {
            pnl = pnl1;
        }

        private void pnl2_MouseEnter(object sender, EventArgs e)
        {
            pnl = pnl2;
        }

        private void pnl3_MouseEnter(object sender, EventArgs e)
        {
            pnl = pnl3;
        }

        ToolStripMenuItem ts_temp;
        private void rmv_Click(object sender, EventArgs e)
        {
            ts_temp = (ToolStripMenuItem)sender;
            cntr_temp = ts_temp.Owner.Parent;
            pnl1.Controls.Remove(cntr_temp);
        }

        //templates

        private int tot1, tot2;
        private void simpjourn()
        {
            Font fnt = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            Font fnt2 = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            Font fnt3 = new Font("Microsoft Sans Serif", 9.25f, FontStyle.Underline);

            label1.Text = "General Journal";
            label1.Location = new Point((pnl1.Size.Width / 2) - label1.Size.Width / 2, 37);

            PictureBox pbx = new PictureBox();
            pbx.BackColor = Color.Lavender;
            pbx.Location = new Point(60, 140);
            pbx.Size = new Size(pnl1.Size.Width - 120, 340);

            Label lbl_2simp = new Label();
            lbl_2simp.Text = "General Journal Monthly Rendition:";
            lbl_2simp.Size = new Size(225, 17);
            lbl_2simp.BackColor = Color.Lavender;
            lbl_2simp.Font = fnt3;
            lbl_2simp.MouseDown += new MouseEventHandler(MouseDown_);
            lbl_2simp.MouseUp += new MouseEventHandler(MouseUp_);
            lbl_2simp.ContextMenuStrip = cms;
            lbl_2simp.Location = new Point(((pnl1.Size.Width/2) - lbl_2simp.Size.Width/2) + 5, 150);
            ar_MouseDU.Add(lbl_2simp);

            Label lblmnth = new Label();
            lblmnth.Text = "Month:";
            lblmnth.BackColor = Color.Lavender;
            lblmnth.Location = new Point(70, 187);
            lblmnth.Font = fnt2;

            Label lbl_cr = new Label();
            lbl_cr.Text = "Credit";
            lbl_cr.BackColor = Color.Lavender;
            lbl_cr.Location = new Point(240, 187);
            lbl_cr.Font = fnt2;

            Label lbl_dr = new Label();
            lbl_dr.Text = "Debit";
            lbl_dr.BackColor = Color.Lavender;
            lbl_dr.Size = new Size(lbl_dr.Size.Width - 10, lbl_dr.Size.Height);
            lbl_dr.Location = new Point(350, 187);
            lbl_dr.Font = fnt2;

            Label lbl_tot = new Label();
            lbl_tot.Text = "Grand/Month Total";
            lbl_tot.BackColor = Color.Lavender;
            lbl_tot.Location = new Point(440, 187);
            lbl_tot.Size = new Size(lbl_tot.Size.Width + 20, lbl_tot.Size.Height);
            lbl_tot.Font = fnt2;

            Label lbl_yr = new Label();
            lbl_yr.Text = "Year";
            lbl_yr.BackColor = Color.Lavender;
            lbl_yr.Size = new Size(40, 13);
            lbl_yr.Location = new Point(590, 187);
            lbl_yr.Font = fnt2;

            int loc = 215; string Query;
            n = 0; n3 = 0;
            n2 = 0;
            for (int i = 1; i <= 13; i++)
            {
                Label lbll = new Label();
                Label lbll_crd = new Label();
                Label lbll_dbt = new Label();
                Label lbll_tot_mn = new Label();
                Label lbl_yrr = new Label();
                if (i == 1)
                {
                    lbll.Text = "January"; Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(MM, [Date of Transaction]) = 1 AND datepart(YY, [Date of Transaction]) = datepart(YY, GetDate())"; universal_query_builder(Query);
                    lbll_crd.Text = dataGridView1[0, 0].Value.ToString();
                    lbll_dbt.Text = dataGridView1[1, 0].Value.ToString();
                }
                if (i == 2)
                {
                    lbll.Text = "Febuary"; Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(MM, [Date of Transaction]) = 2 AND datepart(YY, [Date of Transaction]) = datepart(YY, GetDate())"; universal_query_builder(Query);
                    lbll_crd.Text = dataGridView1[0, 0].Value.ToString();
                    lbll_dbt.Text = dataGridView1[1, 0].Value.ToString();
                }
                if (i == 3)
                {
                    lbll.Text = "March"; Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(MM, [Date of Transaction]) = 3 AND datepart(YY, [Date of Transaction]) = datepart(YY, GetDate())"; universal_query_builder(Query);
                    lbll_crd.Text = dataGridView1[0, 0].Value.ToString();
                    lbll_dbt.Text = dataGridView1[1, 0].Value.ToString();
                }
                if (i == 4)
                {
                    lbll.Text = "April"; Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(MM, [Date of Transaction]) = 4 AND datepart(YY, [Date of Transaction]) = datepart(YY, GetDate())"; universal_query_builder(Query);
                    lbll_crd.Text = dataGridView1[0, 0].Value.ToString();
                    lbll_dbt.Text = dataGridView1[1, 0].Value.ToString();
                }
                if (i == 5)
                {
                    lbll.Text = "May"; Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(MM, [Date of Transaction]) = 5 AND datepart(YY, [Date of Transaction]) = datepart(YY, GetDate())"; universal_query_builder(Query);
                    lbll_crd.Text = dataGridView1[0, 0].Value.ToString();
                    lbll_dbt.Text = dataGridView1[1, 0].Value.ToString();
                }
                if (i == 6)
                {
                    lbll.Text = "June"; Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(MM, [Date of Transaction]) = 6 AND datepart(YY, [Date of Transaction]) = datepart(YY, GetDate())"; universal_query_builder(Query);
                    lbll_crd.Text = dataGridView1[0, 0].Value.ToString();
                    lbll_dbt.Text = dataGridView1[1, 0].Value.ToString();
                }
                if (i == 7)
                {
                    lbll.Text = "July"; Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(MM, [Date of Transaction]) = 7 AND datepart(YY, [Date of Transaction]) = datepart(YY, GetDate())"; universal_query_builder(Query);
                    lbll_crd.Text = dataGridView1[0, 0].Value.ToString();
                    lbll_dbt.Text = dataGridView1[1, 0].Value.ToString();
                }
                if (i == 8)
                {
                    lbll.Text = "August"; Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(MM, [Date of Transaction]) = 8 AND datepart(YY, [Date of Transaction]) = datepart(YY, GetDate())"; universal_query_builder(Query);
                    lbll_crd.Text = dataGridView1[0, 0].Value.ToString();
                    lbll_dbt.Text = dataGridView1[1, 0].Value.ToString();
                }
                if (i == 9)
                {
                    lbll.Text = "September";  Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(MM, [Date of Transaction]) = 9 AND datepart(YY, [Date of Transaction]) = datepart(YY, GetDate())"; universal_query_builder(Query);
                    lbll_crd.Text = dataGridView1[0, 0].Value.ToString();
                    lbll_dbt.Text = dataGridView1[1, 0].Value.ToString();
                }
                if (i == 10)
                {
                    lbll.Text = "October";  Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(MM, [Date of Transaction]) = 10 AND datepart(YY, [Date of Transaction]) = datepart(YY, GetDate())"; universal_query_builder(Query);
                    lbll_crd.Text = dataGridView1[0, 0].Value.ToString();
                    lbll_dbt.Text = dataGridView1[1, 0].Value.ToString();
                }
                if (i == 11)
                {
                    lbll.Text = "November";  Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(MM, [Date of Transaction]) = 11 AND datepart(YY, [Date of Transaction]) = datepart(YY, GetDate())"; universal_query_builder(Query);
                    lbll_crd.Text = dataGridView1[0, 0].Value.ToString();
                    lbll_dbt.Text = dataGridView1[1, 0].Value.ToString();
                }
                if (i == 12)
                {
                    lbll.Text = "December"; Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(MM, [Date of Transaction]) = 12 AND datepart(YY, [Date of Transaction]) = datepart(YY, GetDate())"; universal_query_builder(Query);
                    lbll_crd.Text = dataGridView1[0, 0].Value.ToString();
                    lbll_dbt.Text = dataGridView1[1, 0].Value.ToString();
                }
                if (i == 13)
                {
                    lbll.Text = "Total : "; lbll.Font = fnt2; 
                    lbll_crd.Text = n.ToString();
                    lbll_dbt.Text = n2.ToString();
                    lbll_tot_mn.Text = n3.ToString();
                }
                if (i != 13)
                {
                    try
                    {
                        tot1 = Convert.ToInt32(dataGridView1[0, 0].Value);
                        tot2 = Convert.ToInt32(dataGridView1[1, 0].Value);
                        tot2 = tot2 - tot1;
                        lbll_tot_mn.Text = tot2.ToString();
                    }
                    catch (Exception erty) { }
                }

                //additions
                try
                {
                    n = n + Convert.ToInt32(dataGridView1[0, 0].Value);
                    n2 = n2 + Convert.ToInt32(dataGridView1[1, 0].Value);
                    n3 = n3 + tot2;
                }
                catch (Exception erty) { }


                lbl_yrr.Text = DateTime.Now.Year.ToString();

                lbll_crd.Location = new Point(240, loc);
                lbll_crd.BackColor = Color.Lavender;
                lbll_crd.ContextMenuStrip = cms;
                lbll_crd.Size = new Size(lbll_crd.Size.Width, 14);

                lbll_dbt.Location = new Point(350, loc);
                lbll_dbt.BackColor = Color.Lavender;
                lbll_dbt.ContextMenuStrip = cms;
                lbll_dbt.Size = new Size(lbll_dbt.Size.Width - 20, 14);

                lbll_tot_mn.Location = new Point(440, loc);
                lbll_tot_mn.BackColor = Color.Lavender;
                lbll_tot_mn.ContextMenuStrip = cms;
                lbll_tot_mn.Size = new Size(lbll_tot_mn.Size.Width, 14);

                lbl_yrr.Size = new Size(35, 14);
                lbl_yrr.BackColor = Color.Lavender;
                lbl_yrr.ContextMenuStrip = cms;
                lbl_yrr.Location = new Point(590, loc);

                lbll.Location = new Point(70, loc);
                lbll.ContextMenuStrip = cms;
                lbll.BackColor = Color.Lavender;
                lbll.MouseDown += new MouseEventHandler(MouseDown_);
                lbll.MouseUp += new MouseEventHandler(MouseUp_);
                ar_MouseDU.Add(lbll);

                lbll.Size = new Size(lbll.Size.Width, 14);
                pnl1.Controls.Add(lbll);
                pnl1.Controls.Add(lbll_crd);
                pnl1.Controls.Add(lbll_dbt);
                pnl1.Controls.Add(lbll_tot_mn);
                pnl1.Controls.Add(lbl_yrr);
                loc = loc + 20;
            }

            //pnl1.Controls.Add(lbl_1simp);
            pnl1.Controls.Add(lbl_2simp);
            pnl1.Controls.Add(lblmnth);
            pnl1.Controls.Add(lbl_cr);
            pnl1.Controls.Add(lbl_dr);
            pnl1.Controls.Add(lbl_tot);
            pnl1.Controls.Add(lbl_yr);
            pnl1.Controls.Add(pbx);

            //yearly rendition
            PictureBox pbx2 = new PictureBox();
            pbx2.BackColor = Color.Lavender;
            pbx2.Location = new Point(60, 510);
            pbx2.Size = new Size(pnl1.Size.Width - 120, 170);

            Label lbl_3simp = new Label();
            lbl_3simp.BackColor = Color.Lavender;
            lbl_3simp.Text = "General Journal Yearly Rendition:";
            lbl_3simp.Font = fnt3;
            lbl_3simp.Location = new Point(((pnl1.Size.Width / 2) - lbl_2simp.Size.Width / 2) + 7, 520);
            lbl_3simp.Size = new Size(225, 17);
            lbl_3simp.ContextMenuStrip = cms;
            lbl_3simp.MouseDown += new MouseEventHandler(MouseDown_);
            lbl_3simp.MouseUp += new MouseEventHandler(MouseUp_);
            ar_MouseDU.Add(lbl_3simp);

            Label lblyear = new Label();
            lblyear.Text = "Year";
            lblyear.BackColor = Color.Lavender;
            lblyear.Location = new Point(70, 557);
            lblyear.Font = fnt2;

            Label lbl_cr_yr = new Label();
            lbl_cr_yr.Text = "Credit";
            lbl_cr_yr.BackColor = Color.Lavender;
            lbl_cr_yr.Location = new Point(240, 557);
            lbl_cr_yr.Font = fnt2;

            Label lbl_dr_yr = new Label();
            lbl_dr_yr.Text = "Debit";
            lbl_dr_yr.BackColor = Color.Lavender;
            lbl_dr_yr.Size = new Size(lbl_dr_yr.Size.Width - 10, lbl_dr.Size.Height);
            lbl_dr_yr.Location = new Point(350, 557);
            lbl_dr_yr.Font = fnt2;

            Label lbl_tot_yr = new Label();
            lbl_tot_yr.Text = "Grand/Year Total";
            lbl_tot_yr.BackColor = Color.Lavender;
            lbl_tot_yr.Location = new Point(440, 557);
            lbl_tot_yr.Size = new Size(lbl_tot_yr.Size.Width + 20, lbl_tot_yr.Size.Height);
            lbl_tot_yr.Font = fnt2;

            int year;
            for (int i = 0; i <= 5; i++)
            {
                year = DateTime.Now.Year;
                year = year - i;
                dataGridView1.Visible = true;
                Query = "SELECT sum(Credit), sum(Debit) FROM journal WHERE datepart(YY, [Date of Transaction]) = '" + year.ToString() + "'";
            }

            pnl1.Controls.Add(lbl_3simp);
            pnl1.Controls.Add(lbl_cr_yr);
            pnl1.Controls.Add(lbl_dr_yr);
            pnl1.Controls.Add(lbl_tot_yr);
            pnl1.Controls.Add(lblyear);
            pnl1.Controls.Add(pbx2);

            pnl_working.Dispose();
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
        private void universal_query_builder(string Query)
        {
            dst_final_public.Clear();
            ConnString = Properties.Settings.Default.AmdtbseConnectionString;
            SqlString = Query;
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
    }
}
