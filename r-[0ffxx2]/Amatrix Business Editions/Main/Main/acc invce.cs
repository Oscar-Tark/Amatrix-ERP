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
using System.Data;
using System.Drawing;
using System.Data.SqlServerCe;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Extern_ASQL;
using Base_ASQL;

namespace Main
{
    public partial class acc_invce : Form
    {
        BASQL basql = new BASQL();
        Extern_Sql asql = new Extern_Sql();
        
        //Cross Threading Objects
        private Thread thinit;
        private delegate void delinit();
        private Thread thinitdb;
        private delegate void delinitdb();
        private int howmany;
        private int maxm;
        private int biggest = 100;
        private int smallest = 1;
        private string LAST_QUERY_USED;
        
        private ArrayList aund = new ArrayList();
        private ArrayList aundC = new ArrayList();
        private ArrayList aundR = new ArrayList();


        public acc_invce()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.amdsicnico;
            InitializeComponent();
            thinitstrt();
            this.Disposed += new EventHandler(acc_invce_Disposed);
            if (acc_journ_sett.Default.db_jrn_strt == true)
            {
                th_strt_db();
            }
            bkkinit.RunWorkerAsync();
            if (Main.Amatrix.acc != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.acc); pwd.Owner = this; }
                catch { }
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

        private void th_strt_db()
        {
            try
            {
                new Thread(new ThreadStart(del_init_db)).Start();
            }
            catch { del_init_db(); }
        }

        private void del_init_db()
        {
            try
            {
                this.Invoke(new delinitdb(init_db));
            }
            catch { init_db(); }
        }

        //int main
        private void thinitstrt()
        {
            try
            {
                thinit = new Thread(new ThreadStart(delinitstrt));
                thinit.Start();
            }
            catch { Init(); }
        }

        private void delinitstrt()
        {
            try
            {
                this.Invoke(new delinit(Init));
            }
            catch { Init(); }
        }

        private void acc_invce_Load(object sender, EventArgs e)
        {
        }

        private void Init()
        {
            if (Properties.Settings.Default.defrgb == 1)
            {
                tswin.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);
                tswin3.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);

                tsttl.ForeColor = Color.FromArgb(Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb);
                tsttl2.ForeColor = Color.FromArgb(Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb);
            }

            cnbnn1.AllowTransparency = true;
            cnbnn1.Opacity = 0.90;
            cnbnn3.AllowTransparency = true;
            cnbnn3.Opacity = 0.90;
            selwin.AllowTransparency = true;
            selwin.Opacity = 0.90;

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
            catch { }

            try
            {
                if (visual.Default.font != 8.25f)
                {
                    Font fnt = new Font(dgv.Font.FontFamily.GetName(0), visual.Default.font, FontStyle.Regular);
                    dgv.Font = fnt;
                    dgv.AutoResizeRows();
                    dgv.AutoResizeColumns();
                    visual.Default.font = ftmp;
                    visual.Default.Save();
                }
            }
            catch { }

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
        }

        private void init_db()
        {
            connlbl.Text = "Initializing Database..";
            LAST_QUERY_USED = "Select * From invoice Where [Serial Number] >= 1 AND [Serial Number] <= 100";
            if (Main.Amatrix.acc == "")
            {
                try
                {
                    invoiceTableAdapter.Connection.Open();
                }
                catch { }
                try
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    string SqlString = "Select * From invoice Where [Serial Number] >= 1 AND [Serial Number] <= 100";
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                catch { }
            }
            else
            {
                dtp.Clear();
                dtp = asql.Get_all(Main.Amatrix.acc, dtp, "invoice", true);
                dgv.DataSource = dtp;
            }
            conn2();
            return;
        }

        private DataTable dtp = new DataTable();
        private void conn2()
        {
            if (Main.Amatrix.acc == "")
            {
                try
                {
                    if (invoiceTableAdapter.Connection.State == ConnectionState.Open)
                    {
                        connlbl.Image = Properties.Resources.conncted;
                        connlbl.Text = "Invoice Table is Connected";
                    }
                    else if (invoiceTableAdapter.Connection.State == ConnectionState.Closed)
                    {
                        connlbl.Text = "Invoice Table is Not Connected";
                        connlbl.Image = Properties.Resources.connctno;
                    }
                    else
                    {
                        connlbl.Text = "Invoice Table Connectivity Error (Reconnect Please)"; connlbl.Image = Properties.Resources.conncerr;
                    }
                    db_info.Text = "DataBase: " + invoiceTableAdapter.Connection.Database;
                }
                catch { }
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.acc);
                    conn.Open();
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Connected";
                }
                catch
                {
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Connection Not Available";
                }
            }
            return;
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
                szejrn.Visible = false;
                reszz.Visible = false;
                mvewin.Visible = false;
                rszmnu.Enabled = false;
                dvgpndoc.Visible = false;
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
                pos1mnu.Visible = true;
                rszmnu.Enabled = true;
                maxminmnu1.Enabled = true;
                mxmn.Enabled = true;
                szejrn.Visible = true;
                reszz.Visible = true;
                mvewin.Visible = true;
                dvgpndoc.Visible = true;
                dgvwin.Size = new Size(x, y);
                wozzy = 0;
            }
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

        private void selwin_MouseEnter(object sender, EventArgs e)
        {
            selwin.Opacity = 0.90;
        }

        private void selwin_MouseLeave(object sender, EventArgs e)
        {
            selwin.Opacity = 0.85;
        }

        private void cnbnn1_MouseEnter(object sender, EventArgs e)
        {
            cnbnn1.Opacity = 0.90;
        }

        private void cnbnn1_MouseLeave(object sender, EventArgs e)
        {
            cnbnn1.Opacity = 0.85;
        }

        private void cnbnn3_MouseEnter(object sender, EventArgs e)
        {
            cnbnn3.Opacity = 0.90;
        }

        private void cnbnn3_MouseLeave(object sender, EventArgs e)
        {
            cnbnn3.Opacity = 0.85;
        }

        private void tswin_MouseEnter(object sender, EventArgs e)
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

        private void tswin_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.defrgb == 0)
            {
                tswin.BackColor = Color.AliceBlue;
            }
            else if (Properties.Settings.Default.defrgb != 0)
            {
                try
                {
                    tswin.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);
                }
                catch (Exception erty) { }
            }
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
                catch (Exception ertty) { }
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

        //stop tick
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

        //movto Win 1       
        
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

        //simmetrize Win1

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

        //Tickers

        private void dgvwintc(object sender, EventArgs e)
        {
            dvgpndoc.ToolTipText = "Maximize";
            buho = 0;
            dgvwin.Dock = DockStyle.None;
            dgvwin.Location = new Point((Cursor.Position.X - this.Location.X) - (dgvwin.Size.Width / 2), Cursor.Position.Y - this.Location.Y - 60);
        }

        private void dgvwintic3_Tick(object sender, EventArgs e)
        {
            dvgpndoc3.ToolTipText = "Maximize";
            buho3 = 0;
            summpnl.Dock = DockStyle.None;
            summpnl.Location = new Point(Cursor.Position.X - this.Location.X - (summpnl.Size.Width / 2), Cursor.Position.Y - this.Location.Y - 60);
        }

        private void tmex_Tick(object sender, EventArgs e)
        {
            dgvwin.Dock = DockStyle.None;
            dgvwin.Size = new Size(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y);
        }

        private void tmex3_Tick(object sender, EventArgs e)
        {
            summpnl.Dock = DockStyle.None;
            summpnl.Size = new Size(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y);
        }

        //To Tick Trigger

        private void reszz_ButtonClick(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dvgpndoc.ToolTipText = "Maximize";
            buho = 0;
            tmex.Start();
        }

        private void mvewin_ButtonClick(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwintic.Start();
        }

        private void tswin_Click(object sender, EventArgs e)
        {
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
            //winwin = 3;
            dgvwin.BringToFront();
        }

        //win3

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

        private void rstrt_Click(object sender, EventArgs e)
        {
            acc_invce vnce = new acc_invce();
            vnce.Show();
            this.Close();
        }

        private void tswin3_Click(object sender, EventArgs e)
        {
            try
            {
                dgvwintic.Stop();
                dgvwintic3.Stop();
                tmex.Stop();
                tmex3.Stop();
            }
            catch (Exception exct)
            { }
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
                tabControl2.Visible = false;
                movwin3.Visible = false;
                t_v.Visible = false;
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
                tabControl2.Visible = true;
                t_v.Visible = true;
                dvgpndoc3.Visible = true;
                summpnl.Size = new Size(x3, y3);
                wozzy3 = 0;
            }
        }

        private void tswin3_MouseEnter(object sender, EventArgs e)
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

        private void tswin3_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.defrgb == 0)
            {
                tswin3.BackColor = Color.AliceBlue;
            }
            else if (Properties.Settings.Default.defrgb != 0)
            {
                try
                {
                    tswin3.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);
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

        private void tswin3_DoubleClick(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            dgvwintic3.Start();
        }

        private void reszz3_ButtonClick(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            dvgpndoc3.ToolTipText = "Maximize";
            buho3 = 0;
            tmex3.Start();
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

        private void stdflt_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwin.Size = new Size(751, 467);
        }

        private void sttodefjrn_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
            dgvwin.Dock = DockStyle.None;
            dgvwin.Location = new Point(204, 0);
        }

        private void jrntofnt_Click(object sender, EventArgs e)
        {
            dgvwin.BringToFront();
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

        private void clseall_Click(object sender, EventArgs e)
        {
            maxminmnu1.Enabled = false;
            maxminmnu3.Enabled = false;

            rszmnu.Enabled = false;
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

            rszmnu.Enabled = true;
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

        private void str_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            summpnl.Size = new Size(204, 467);
        }

        private void setToDefauljkk_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            summpnl.Dock = DockStyle.None;
            summpnl.Location = new Point(0, 0);
        }

        private void smtofnt_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
        }

        private void clse_MouseEnter(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void clse_MouseLeave(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.Image;
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

        private void tmeclse_tc(object sender, EventArgs e)
        {
            this.Opacity = this.Opacity - 0.05;
            if (this.Opacity == 0.00)
            {
                this.Close();
            }
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

        private void clsejournclc(object sender, EventArgs e)
        {
            tmeclse.Start();
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

        private void stdtodefall_Click(object sender, EventArgs e)
        {
            summpnl.BringToFront();
            summpnl.Dock = DockStyle.None;
            summpnl.Location = new Point(0, 0);

            summpnl.BringToFront();
            summpnl.Size = new Size(204, 467);

            dgvwin.BringToFront();
            dgvwin.Dock = DockStyle.None;
            dgvwin.Location = new Point(204, 0);

            dgvwin.BringToFront();
            dgvwin.Size = new Size(751, 467);
        }

        private void connlblme(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = Properties.Resources.bannrrageconv;
        }

        private void connlblml(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = null;
        }

        private void col_Tick(object sender, EventArgs e)
        {
            if (Main.Amatrix.rgb == 1)
            {
                tswin.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);
                //tswin2.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);
                tswin3.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);

                tsttl.ForeColor = Color.FromArgb(Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb);
                //tsttl1.ForeColor = Color.FromArgb(Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb);
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

        private void slctrgb_Click(object sender, EventArgs e)
        {
            Main.cols cls = new cols();
            cls.Show();
        }

        private void dgvupall_Click(object sender, EventArgs e)
        {
            if (sender.Equals(dgvupall) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[0].Cells[dgv.CurrentCell.ColumnIndex];
                }
                catch (Exception erty) { }
            }
            else if (sender.Equals(dgvupone) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex - 1].Cells[dgv.CurrentCell.ColumnIndex];
                }
                catch (Exception erty2) { }
            }
            else if (sender.Equals(dgvleftall) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0];
                }
                catch (Exception erty3) { }
            }
            else if (sender.Equals(dgvleftone) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex - 1];
                }
                catch (Exception erty4) { }
            }
            else if (sender.Equals(dgvrightone) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex + 1];
                }
                catch (Exception erty5) { }
            }
            else if (sender.Equals(dgvrightall) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.ColumnCount - 1];
                }
                catch (Exception erty6) { }
            }
            else if (sender.Equals(dgvdownone) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentRow.Index + 1].Cells[dgv.CurrentCell.ColumnIndex];
                }
                catch (Exception erty7) { }
            }
            else if (sender.Equals(dgvdownall) == true)
            {
                try
                {
                    dgv.CurrentCell = dgv.Rows[dgv.RowCount - 1].Cells[dgv.CurrentCell.ColumnIndex];
                }
                catch (Exception erty8) { }
            }
        }

        private void bkkinit_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Main.Amatrix.acc == "")
                {
                    SqlCeConnection mySqlConnection = new SqlCeConnection(invoiceTableAdapter.Connection.ConnectionString);
                    SqlCeCommand mySqlCommand = mySqlConnection.CreateCommand();
                    mySqlCommand.CommandText = "SELECT COUNT([Serial Number]) FROM invoice";
                    mySqlConnection.Open();
                    howmany = (int)mySqlCommand.ExecuteScalar();
                    mySqlConnection.Close();
                }
                else
                {
                    SqlConnection mySqlConnection = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                    mySqlCommand.CommandText = "SELECT COUNT([Serial Number]) FROM invoice";
                    mySqlConnection.Open();
                    howmany = (int)mySqlCommand.ExecuteScalar();
                    mySqlConnection.Close();
                }

                //max
                if (Main.Amatrix.acc == "")
                {
                    SqlCeConnection mySqlConnection3 = new SqlCeConnection(invoiceTableAdapter.Connection.ConnectionString);
                    SqlCeCommand mySqlCommand3 = mySqlConnection3.CreateCommand();
                    mySqlCommand3.CommandText = "SELECT MAX([Serial Number]) FROM invoice";
                    mySqlConnection3.Open();
                    maxm = (int)mySqlCommand3.ExecuteScalar();
                    mySqlConnection3.Close();
                }
                else
                {
                    SqlConnection mySqlConnection3 = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand mySqlCommand3 = mySqlConnection3.CreateCommand();
                    mySqlCommand3.CommandText = "SELECT MAX([Serial Number]) FROM invoice";
                    mySqlConnection3.Open();
                    maxm = (int)mySqlCommand3.ExecuteScalar();
                    mySqlConnection3.Close();
                }

                try
                {
                    numbrw.Text = howmany.ToString();
                }
                catch (Exception erty) { }

                try
                {
                    huntme.Text = dgv.Rows[0].Cells[0].Value.ToString() + "-" + dgv.Rows[dgv.RowCount - 2].Cells[0].Value.ToString();
                }
                catch (Exception erty) { huntme.Text = smallest.ToString() + " -" + biggest.ToString() + " "; }
            }
            catch (Exception erty) { }
        }

        private void save_inv_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.IsCurrentCellDirty)
                {
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
                if (Main.Amatrix.acc == "")
                {
                    invoiceTableAdapter.Update(invoice_dataset);
                }
                else
                {
                    asql.Save(dtp, "invoice", Main.Amatrix.acc);
                    try
                    {
                        Main.Amatrix.ascl.broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><typ>w</typ><val>0</val><app>" + this.Name + "</app><par>[" + toolStrip1.Name + "]</par><con>toolStripButton4</con>");
                    }
                    catch (Exception erty) { general_mssg("Syncronization is not Set Up", "Sync Error"); }
                }
            }
            catch (Exception erty) { general_mssg("An Error Occured While Saving Invoices to the DataBase, Invoices has Not been Saved", "Save"); }
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

        private void go_to_Click(object sender, EventArgs e)
        {
            oper_save();
            LAST_QUERY_USED = "Select * From invoice Where [Billers Name] LIKE '%" + show_invc_of.Text + "%'";
            try
            {
                if (Main.Amatrix.acc == "")
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    string SqlString = LAST_QUERY_USED;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    dtp.Clear();
                    dtp = basql.Execute(Main.Amatrix.acc, "Select * From invoice Where [Billers Name] LIKE '%" + show_invc_of.Text + "%'", "invoice", dtp);
                    dgv.DataSource = dtp;
                }
            }
            catch (Exception ertty) { Am_err ner = new Am_err(); ner.tx("Could Not Complete Search as a Fatal Error Occured."); }
        }

        private void delete_itms(object sender, EventArgs e)
        {
            if (sender.Equals(delete) == true)
            {
                if (Convert.ToInt32(dgv.CurrentRow.Cells[0].Value) == 1)
                {
                }
                else
                {
                    if(DialogResult.Yes == MessageBox.Show("To Create Clarity Within your Journal Entries Without Dataloss all Journal Entries with the specified Invoice Reference will be set as 'invoice number (defunct)' for Entry Clarity, would you like to do so?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        if (dgv.SelectedCells.Count > 0)
                        {
                            foreach (DataGridViewCell dgvcll in dgv.SelectedCells)
                            {
                                        string s = "";
                                        if (dgv[34, dgvcll.RowIndex].Value.ToString() == "salesbook") { s = "SalesBook"; }
                                        else { s = "PurchaseBook"; }
                                if (Main.Amatrix.acc == "")
                                {
                                    if (dgv[34, dgvcll.RowIndex].Value == DBNull.Value || dgv[34, dgvcll.RowIndex].Value == null)
                                    { }
                                    else if (dgv[34, dgvcll.RowIndex].Value.ToString() == "salesbook" || dgv[34, dgvcll.RowIndex].Value.ToString() == "purchasebook")
                                    {
                                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                                        SqlCeCommand cmd = new SqlCeCommand("UPDATE " + s + " SET [Invoice Number] = '" + dgvcll.OwningRow.Cells[1].Value.ToString() + " (defunct)' WHERE [Invoice Number] = '" + dgvcll.OwningRow.Cells[1].Value.ToString() + "'", conn);
                                        conn.Open();
                                        SqlCeDataReader dr = cmd.ExecuteReader();
                                        conn.Close();
                                    }
                                }
                                else
                                {
                                    DataTable dtpy = new DataTable();
                                    basql.Execute(Main.Amatrix.acc, "UPDATE " + s + " SET [Invoice Number] = '" + dgvcll.OwningRow.Cells[1].Value.ToString() + " (defunct)' WHERE [Invoice Number] = '" + dgvcll.OwningRow.Cells[1].Value.ToString() + "'", s, dtpy);
                                    dtpy.Clear();
                                    dtpy.Dispose();
                                }
                            }
                        }
                        else
                        {
                            string s = "";
                            if (dgv[34, dgv.CurrentRow.Index].Value.ToString() == "salesbook") { s = "SalesBook"; }
                            else { s = "PurchaseBook"; }

                            if (Main.Amatrix.acc == "")
                            {
                                if (dgv[34, dgv.CurrentRow.Index].Value == DBNull.Value || dgv[34, dgv.CurrentRow.Index].Value == null)
                                { }
                                else if (dgv[34, dgv.CurrentRow.Index].Value.ToString() == "salesbook" || dgv[34, dgv.CurrentRow.Index].Value.ToString() == "purchasebook")
                                {
                                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                                    SqlCeCommand cmd = new SqlCeCommand("UPDATE " + s + " SET [Invoice Number] = '" + dgv[1, dgv.CurrentRow.Index].Value.ToString() + " (defunct)' WHERE [Invoice Number] = '" + dgv.CurrentRow.Cells[1].Value.ToString() + "'", conn);
                                    conn.Open();
                                    SqlCeDataReader dr = cmd.ExecuteReader();
                                    conn.Close();
                                }
                            }
                            else
                            {
                                DataTable dtpy = new DataTable();
                                basql.Execute(Main.Amatrix.acc, "UPDATE " + s + " SET [Invoice Number] = '" + dgv[1, dgv.CurrentRow.Index].Value.ToString() + " (defunct)' WHERE [Invoice Number] = '" + dgv.CurrentRow.Cells[1].Value.ToString() + "'", s, dtpy);
                                dtpy.Clear();
                                dtpy.Dispose();
                            }
                        }
                    }
                    if (dgv.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow dgvr in dgv.SelectedRows)
                        {
                            if (Convert.ToInt32(dgvr.Cells[0].Value) == 1) { }
                            else
                            {
                                howmany = howmany - 1;
                                numbrw.Text = howmany.ToString();
                                dgv.Rows.Remove(dgvr);
                            }
                        }
                    }
                    else
                    {
                        if (dgv.SelectedCells.Count > 1)
                        {
                            foreach (DataGridViewCell dgvc in dgv.SelectedCells)
                            {
                                if (Convert.ToInt32(dgvc.OwningRow.Cells[0].Value) == 1)
                                { }
                                else
                                {
                                    try
                                    {
                                        dgv.Rows.RemoveAt(dgvc.RowIndex);
                                    }
                                    catch (Exception erty) { }
                                }
                            }
                        }
                        else
                        {
                            dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
                            howmany = howmany - 1;
                            numbrw.Text = howmany.ToString();
                        }
                    }
                }
            }

            if (sender.Equals(del_all) == true)
            {
                if (Main.Amatrix.acc == "")
                {
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    string SqlString = "DELETE FROM " + invoice_dataset.invoice.TableName;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.OverwriteChanges, "invoice");
                                dgv.DataSource = null;
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                    invoice_dataset.invoice.Rows.Clear();
                    invoice_dataset.invoice.AcceptChanges();
                    maxm = 0;
                }
                else
                {
                    dtp = asql.Remove_All_From(dtp, "invoice", Main.Amatrix.acc);
                    dgv.DataSource = dtp;
                    maxm = 0;
                }
            }
        }

        private void deletecell_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.CurrentCell.ReadOnly == false)
                {
                    dgv.CurrentCell.Value = DBNull.Value;
                }
            }
            catch (Exception erty)
            {
                try
                {
                    if (dgv.CurrentCell.ReadOnly == false)
                    {
                        dgv.CurrentCell.Value = null;
                    }
                }
                catch (Exception erty1) { }
            }
            if (dgv.SelectedCells.Count > 1)
            {
                foreach (DataGridViewCell dgvr in dgv.SelectedCells)
                {
                    if (dgvr.ReadOnly == false)
                    {
                        try
                        {
                            dgvr.Value = DBNull.Value;
                        }
                        catch (Exception erty) { }
                    }
                }
            }
        }

        private void nxtst_Click(object sender, EventArgs e)
        {
            oper_save();
            smallest = biggest;
            biggest = biggest + 100;
            string SqlString = "Select * From invoice Where [Serial Number] >= '" + smallest + "' AND [Serial Number] <= '" + biggest + "'";
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.Clear();
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp.Clear();
                dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                dgv.DataSource = dtp;
            }
            try
            {
                huntme.Text = dgv.Rows[0].Cells[0].Value.ToString() + "-" + dgv.Rows[dgv.RowCount - 2].Cells[0].Value.ToString();
            }
            catch (Exception erty) { huntme.Text = smallest.ToString() + "-" + biggest.ToString(); }
            LAST_QUERY_USED = SqlString;
        }

        private void prev_ButtonClick(object sender, EventArgs e)
        {
            oper_save();string SqlString = "";
            try
            {
                if (Convert.ToInt32(dgv.Rows[0].Cells[0].Value) == 1)
                {
                }
                else
                {
                    SqlString = "Select * From invoice Where [Serial Number] >= '" + smallest + "' AND [Serial Number] <= '" + biggest + "'";
                    
                    biggest = (int)dgv.Rows[0].Cells[0].Value;
                    smallest = biggest - 100;

                    if (Main.Amatrix.acc == "")
                    {
                        invoice_dataset.Clear();
                        string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                    dgv.DataSource = invoice_dataset.invoice;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtp = asql.Go_To_Position(Main.Amatrix.acc, dtp, "invoice", true, smallest, biggest);
                        dgv.DataSource = dtp;
                    }
                }
            }
            catch (Exception erty)
            {
                smallest = smallest - 100;
                biggest = biggest - 100;
                SqlString = "Select * From invoice Where [Serial Number] >= '" + smallest + "' AND [Serial Number] <= '" + biggest + "'";
                if (Main.Amatrix.acc == "")
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    dtp = asql.Go_To_Position(Main.Amatrix.acc, dtp, "invoice", true, smallest, biggest);
                    dgv.DataSource = dtp;
                }
            }
            try
            {
                huntme.Text = dgv.Rows[0].Cells[0].Value.ToString() + "-" + dgv.Rows[dgv.RowCount - 2].Cells[0].Value.ToString();
            }
            catch (Exception erty) { huntme.Text = smallest.ToString() + "-" + biggest.ToString(); }
            LAST_QUERY_USED = SqlString;
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

        private void undoall_Click(object sender, EventArgs e)
        {
                try
                {
                    int x, y;
                    x = Convert.ToInt32(aundC[aundC.Count - 1]);
                    y = Convert.ToInt32(aundR[aundR.Count - 1]);
                    dgv[x, y].Value = aund[aund.Count - 1];

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
                    copycutpaste.Add(dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value);
            }
            catch (Exception erty) { }
        }

        private void pster_Click(object sender, EventArgs e)
        {
            try
            {
                    dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value.ToString() + copycutpaste[0].ToString();
            }
            catch (Exception erty) { Am_err mer = new Am_err(); mer.tx(erty.Message); }
        }

        private void ct_Click(object sender, EventArgs e)
        {
            copycutpaste.Clear();
            try
            {
                    copycutpaste.Add(dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value);
                    dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value = null;
            }
            catch (Exception erty) { }
        }

        private void sall_Click(object sender, EventArgs e)
        {
            dgv.SelectAll();
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

        private void nw_rw_Click(object sender, EventArgs e)
        {
            LAST_QUERY_USED = "ignore";
            DataRow row;
            DataTable tb = new DataTable();
            row = invoice_dataset.invoice.NewRow();
            row[1] = maxm;
            tb = invoice_dataset.invoice;
            tb.Rows.Add(row);
            dgv.DataSource = tb;
        }

        private void connct_Click(object sender, EventArgs e)
        {
            connctyes();
        }

        private void connctyes()
        {
            try
            {
                if (Main.Amatrix.acc == "")
                {
                    connlbl.Text = "Connecting..";
                    connlbl.Image = Properties.Resources.conncting;
                    invoiceTableAdapter.Connection.Open();
                }
                else
                {
                }
            }
            catch (Exception erty) { }
            conn2();
        }

        private void clseconn_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.acc == "")
            {
                connlbl.Text = "Disconnecting..";
                connlbl.Image = Properties.Resources.conncting;
                invoiceTableAdapter.Connection.Close();
            }
            else
            {
                connlbl.Text = "Connected, Disconnecting to this DataBase is Un-Allowed";
                connlbl.Image = Properties.Resources.conncting;
                invoiceTableAdapter.Connection.Close();
            }
            conn2();
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
            Properties.Settings.Default.Save();
        }

        private void Virtual_menu(object sender, EventArgs e)
        {
            if (sender.Equals(fnt_mnu) == true)
            {
                tsc_fnt_sze.Text = dgv.Font.Size.ToString();
            }
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

        private void set_quikbox()
        {
            try
            {
                if (Cursor.Position.X - this.Location.X <= this.Size.Width && Cursor.Position.Y - this.Location.Y <= this.Size.Height)
                {
                    zz.Location = new Point((Cursor.Position.X - this.Location.X) - zz.Size.Width / 2, Cursor.Position.Y - this.Location.Y);
                }
                else { }
            }
            catch (Exception erty) { }
        }

        private void zz_yn(bool shw)
        {
            if (shw == true)
            {
                set_quikbox();
                zz.Visible = true;
                zz.BringToFront();
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

        private DataTable dtp_cust = new DataTable();
        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            zz_yn(true);
            try
            {
                if (e.ColumnIndex == 5)
                {
                    cust_dtst.Clear();
                    dataGridView1.ReadOnly = false;
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(customersTableAdapter.Connection.ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Customers", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        cust_dtst.Load(dr, LoadOption.PreserveChanges, "Customers");
                        conn.Close();
                    }
                    else
                    {
                        try
                        {
                            SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                            SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", conn);
                            conn.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            cust_dtst.Load(dr, LoadOption.PreserveChanges, "Customers");
                            conn.Close();
                        }
                        catch (Exception erty) { general_mssg(erty.Message, ""); }
                    }
                    cst_box.Visible = true;
                    cst_box.BringToFront();
                    cst_box.Location = new Point((Cursor.Position.X - this.Location.X) - cst_box.Size.Width / 2, Cursor.Position.Y - this.Location.Y);
                }
                else { cst_box.Visible = false; }
                if (e.ColumnIndex == 14)
                {
                    prodmgmt_dtst.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(prod_mgmtTableAdapter.Connection.ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        prodmgmt_dtst.Load(dr, LoadOption.PreserveChanges, "Prod_mgmt");
                    }
                    else
                    {
                        SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                        SqlCommand cmd = new SqlCommand("SELECT * FROM Prod_mgmt", conn);
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        prodmgmt_dtst.Load(dr, LoadOption.PreserveChanges, "Prod_mgmt");
                    }
                    prod_box.Visible = true;
                    prod_box.BringToFront();
                    prod_box.Location = new Point((Cursor.Position.X - this.Location.X) - prod_box.Size.Width / 2, Cursor.Position.Y - this.Location.Y);
                }
                else { prod_box.Visible = false; }
                try
                {
                    tmr.Stop();
                    dgvwintic.Stop();
                    tmex3.Stop();
                }
                catch (Exception erty) { }
                zz_yn(true);
            }
            catch (Exception erty) { }
        }

        private void remv_zz_Click(object sender, EventArgs e)
        {
            zz_yn(false);
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Arraylist addition
            aund.Add(dgv[e.ColumnIndex, e.RowIndex].Value);
            aundC.Add(e.ColumnIndex);
            aundR.Add(e.RowIndex);

            if (dgv.Rows[e.RowIndex].Cells[0].Value == DBNull.Value || dgv.Rows[e.RowIndex].Cells[0].Value == null)
            {
                maxm = maxm + 1;
                dgv.Rows[e.RowIndex].Cells[0].Value = maxm;
                howmany = howmany + 1;
            }
        }

        private void dgv_Sorted(object sender, EventArgs e)
        {
            aund.Clear();
            aundC.Clear();
            aundR.Clear();
        }

        private void dgv_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 18)
            {
                Showalljourn();
            }
            this.Text = e.KeyCode.ToString();
        }

        private void oper_save()
        {
            if (acc_journ_sett.Default.dynam_jrn == true)
            {
                try
                {
                    if (Main.Amatrix.acc == "")
                    {
                        invoiceTableAdapter.Update(invoice_dataset);
                    }
                    else
                    {
                        EventArgs e = new EventArgs();
                        save_inv_Click(svebtn, e);
                    }
                }
                catch (Exception erty) { }
            }
        }

        private void Showalljourn()
        {
            LAST_QUERY_USED = "SELECT * FROM invoice";
            oper_save();
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.Clear();
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                string SqlString = "Select * FROM invoice";
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp = asql.Get_all(Main.Amatrix.acc, dtp, "invoice", true);
                dgv.DataSource = dtp;
            }
        }

        private void show_AllToolStripButton_Click_1(object sender, EventArgs e)
        {
            oper_save();
            if (sender.Equals(showAllToolStripMenuItem1) == true ||  sender.Equals(nopeascdesc) == true)
            {
                Showalljourn();
            }
            if (sender.Equals(shw_here) == true || sender.Equals(shw_here2) == true)
            {
                if (Main.Amatrix.acc == "")
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    string SqlString = "Select * From invoice Where [Serial Number] >= '" + smallest + "' AND [Serial Number] <= '" + biggest + "'";
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    dtp = asql.Go_To_Position(Main.Amatrix.acc, dtp, "invoice", true, smallest, biggest);
                    dgv.DataSource = dtp;
                }
            }
            LAST_QUERY_USED = "Select * From invoice Where [Serial Number] >= '" + smallest + "' AND [Serial Number] <= '" + biggest + "'";
        }

        private void dbtsumqry_Click(object sender, EventArgs e)
        {
            oper_save();
            string SqlString = "Select sum(Debit) Debit, sum(Credit) Credit, sum([Serial Number]) [Serial Number], sum([Unit Price]) Unit Price, sum([Units Ordered]) Units Ordered, sum([Units Delivered]) Units Delivered, sum(Cost) Cost, sum(Profit) Profit, sum(Owing) Owing, sum(Paid) Paid, sum(Total Balance) Total Balance FROM invoice";
            LAST_QUERY_USED = SqlString;    
            if (Main.Amatrix.acc == "")
            {
                //opinvoice_dataseter_save();
                invoice_dataset.Clear();
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();//)

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                dgv.DataSource = dtp;
            }
        }

        private void avgqryjourn_Click(object sender, EventArgs e)
        {
            oper_save(); 
            string SqlString = "Select avg(Debit) Debit, avg(Credit) Credit, avg([Serial Number]) [Serial Number] FROM invoice";
            LAST_QUERY_USED = SqlString;
    
            if (Main.Amatrix.acc == "")
            {
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                dgv.DataSource = dtp;
            }
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvcea = e;
            try
            {
                if (Convert.ToInt32(dgv[e.ColumnIndex, e.RowIndex].Value) == 0)
                {
                    maxm = maxm + 1;
                    dgv.Rows[e.RowIndex].Cells[0].Value = maxm;
                }
                if (acc_journ_sett.Default.dynam_jrn == true)
                {
                    thsum1strt();
                    thinf_strt();
                }
            }
            catch (Exception erty) { }
        }


        private Thread thsum1;
        private delegate void delsum1();

        private void thsum1strt()
        {
            thsum1 = new Thread(new ThreadStart(delsum1strt));
            thsum1.Start();
        }

        private void delsum1strt()
        {
            try
            {
                this.Invoke(new delsum1(sumgo));
            }
            catch (Exception erty) { }
        }

        private void sumgo()
        {
            if (acc_journ_sett.Default.dynam_jrn == true)
            {
                SUMS();
            }
            else { }
        }

        private double smm1;
        private double smm2, smm3;
        private double resjrn;
        private void SUMS()
        {
            try
            {
                if (Main.Amatrix.acc == "")
                {
                    smm1 = Convert.ToDouble(invoice_dataset.invoice.Compute("sum(Profit)", ""));
                }
                else
                {
                    smm1 = Convert.ToDouble(dtp.Compute("sum(Profit)", ""));
                }
            }
            catch (Exception erty) { }

            try
            {
                if (Main.Amatrix.acc == "")
                {
                    smm2 = Convert.ToDouble(invoice_dataset.invoice.Compute("sum(Cost)", ""));
                }
                else
                {
                    smm2 = Convert.ToDouble(dtp.Compute("sum(Cost)", ""));
                }
            }
            catch (Exception erty2) { }

            try
            {
                if (Main.Amatrix.acc == "")
                {
                    smm3 = Convert.ToDouble(invoice_dataset.invoice.Compute("count(Serial Number)", ""));
                }
                else
                {
                    smm3 = Convert.ToDouble(dtp.Compute("count(Serial Number)", ""));
                }
            }
            catch (Exception erty2) { }

            try
            {
                ttcst_inf.Text = smm2.ToString();
                ttprof_inf.Text = smm1.ToString();
                ttinvc_inf.Text = smm3.ToString();
            }
            catch (Exception erty3) { }
        }

        private DataGridViewCellEventArgs dgvcea;
        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvcea = e;
            if (acc_journ_sett.Default.dynam_jrn == true)
            {
                try
                {
                    thinf_strt();
                }
                catch (Exception erty) { }
            }
        }

        Thread th_inf;
        private delegate void del_inf();

        private void thinf_strt()
        {
            try
            {
                th_inf = new Thread(new ThreadStart(delinf_strt));
                th_inf.IsBackground = true;
                th_inf.Start();
            }
            catch (Exception erty) { }
        }

        private void delinf_strt()
        {
            try
            {
                this.Invoke(new del_inf(info));
            }
            catch (Exception erty) { }
        }

        private void info()
        {
            try
            {
                inf_1.Text = "Invoice Number/ID : " + dgv[1, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty) { }
            try
            {
                inf_2.Text = "Date of Invoice : " + dgv[2, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty2) { }
            try
            {
                inf_3.Text = "Due Date of Invoice : " + dgv[3, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty3) { }
            try
            {
                inf_4.Text = "Units Ordered : " + dgv[18, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty4) { }
            try
            {
                inf_5.Text = "Units Shipped : " + dgv[19, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty5) { }
            try
            {
                inf_6.Text = "Unit Price : " + dgv[9, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty6) { }
            try
            {
                inf_8.Text = "Total Profit : " + dgv[21, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty7) { }
            try
            {
                inf_7.Text = "Paid : " + dgv[24, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty8) { }
            try
            {
                sh_inf1.Text = "Shippers Name : " + dgv[5, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty9) { }
            try
            {
                sh_inf2.Text = "Shippers Address : " + dgv[7, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty10) { }
            try
            {
                sh_inf3.Text = "Shippers Contact Number : " + dgv[9, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty11) { }
            try
            {
                sh_inf4.Text = "Shippers Email Address : " + dgv[13, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty12) { }

            try
            {
                bill_inf1.Text = "Billers Name : " + dgv[4, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty13) { }
            try
            {
                bill_inf2.Text = "Billers Address : " + dgv[6, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty14) { }
            try
            {
                bill_inf3.Text = "Billers Contact Number : " + dgv[8, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty15) { }
            try
            {
                bill_inf4.Text = "Billers Email Address" + dgv[12, dgvcea.RowIndex].Value.ToString();
            }
            catch (Exception erty16) { }
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
            init_db();
        }

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

        private void gotoitm_Click(object sender, EventArgs e)
        {
            oper_save(); 
            string SqlString = "Select * FROM invoice WHERE [Serial Number] LIKE '%" + tbxfned.Text + "%' OR [Invoice Reference Number (ID)] LIKE '%" + tbxfned.Text + "%' OR [Date] LIKE '%" + tbxfned.Text + "%' OR [Due Date] LIKE '%" + tbxfned.Text + "%' OR [Billers Name] LIKE '%" + tbxfned.Text + "%' OR [Shippers Name] LIKE '%" + tbxfned.Text + "%' OR [Bill To (Address)] LIKE '%" + tbxfned.Text + "%' OR [Ship To (Address)] LIKE '%" + tbxfned.Text + "%' OR [Bill To Contact] LIKE '%" + tbxfned.Text + "%' OR [Ship To Contact] LIKE '%" + tbxfned.Text + "%' OR [Bill To Fax] LIKE '%" + tbxfned.Text + "%' OR [Ship To Fax] LIKE '%" + tbxfned.Text + "%' OR [Bill To Email ID] LIKE '%" + tbxfned.Text + "%' OR [Ship To Email ID] LIKE '%" + tbxfned.Text + "%' OR [Item] LIKE '%" + tbxfned.Text + "%' OR [Item ID] LIKE '%" + tbxfned.Text + "%' OR [Item Description] LIKE '%" + tbxfned.Text + "%' OR [Unit Price] LIKE '%" + tbxfned.Text + "%' OR [Units Ordered] LIKE '%" + tbxfned.Text + "%' OR [Units Delivered] LIKE '%" + tbxfned.Text + "%' OR [Cost] LIKE '%" + tbxfned.Text + "%'OR [Profit] LIKE '%" + tbxfned.Text + "%' OR [Owing] LIKE '%" + tbxfned.Text + "%' OR [Owing Amount] LIKE '%" + tbxfned.Text + "%' OR [Paid] LIKE '%" + tbxfned.Text + "%' OR [Paid Amount] LIKE '%" + tbxfned.Text + "%' OR [Vat Rate (%)] LIKE '%" + tbxfned.Text + "%' OR [Vat] LIKE '%" + tbxfned.Text + "%' OR [Other Tax Rate (%)] LIKE '%" + tbxfned.Text + "%' OR [Other Tax] LIKE '%" + tbxfned.Text + "%' OR [Total After Tax] LIKE '%" + tbxfned.Text + "%' OR [Total] LIKE '%" + tbxfned.Text + "%' OR [Notes] LIKE '%" + tbxfned.Text + "%'";
            LAST_QUERY_USED = SqlString;    
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.Clear();
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp.Clear();
                dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                dgv.DataSource = dtp;
            }
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


        private void uptxtdgv2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(uptxtdgv) == true)
                {
                    dgv.CurrentCell = dgv.Rows[Convert.ToInt32(uptxtdgv.Text)].Cells[dgv.CurrentCell.ColumnIndex];
                }
                else if (sender.Equals(leftxtdgv) == true)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentRow.Index].Cells[Convert.ToInt32(leftxtdgv.Text)];
                }
                else if (sender.Equals(dgvtxtright) == true)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentRow.Index].Cells[Convert.ToInt32(dgvtxtright.Text)];
                }
                else if (sender.Equals(dgvtxtleft) == true)
                {
                    dgv.CurrentCell = dgv.Rows[Convert.ToInt32(dgvtxtleft.Text)].Cells[dgv.CurrentCell.ColumnIndex];
                }
            }
            catch (Exception erty) { }
        }

        TreeNode tn;
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            tn = (TreeNode)sender;
            if (tn.Text == "Show All")
            {
                this.Text = tn.Text;
            }
            else if (tn.Text == "All")
            {
                this.Text = tn.Text;
            }
        }

        private void currmnth_Click(object sender, EventArgs e)
        {
            oper_save();
            if (acc_journ_sett.Default.shw_extra_date == false)
            {
                invoice_dataset.Clear();
                string SqlString = "Select * FROM invoice WHERE DATEPART(mm, [Date]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Date]) = DATEPART(yy, GETDATE())";
                if (Main.Amatrix.acc == "")
                {
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    LAST_QUERY_USED = "Select * FROM invoice WHERE DATEPART(mm, [Date]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Date]) = DATEPART(yy, GETDATE())"; 
                    dtp.Clear();
                    dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                    dgv.DataSource = dtp;
                }
            }
            else
            {
                invoice_dataset.Clear();
                string SqlString = "Select * FROM invoice WHERE DATEPART(mm, [Date]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Date]) = DATEPART(yy, GETDATE()) OR DATEPART(mm, [Due Date]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Due Date]) = DATEPART(yy, GETDATE())";
                if (Main.Amatrix.acc == "")
                {
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    LAST_QUERY_USED = "Select * FROM invoice WHERE DATEPART(mm, [Date]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Date]) = DATEPART(yy, GETDATE()) OR DATEPART(mm, [Due Date]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Due Date]) = DATEPART(yy, GETDATE())";
                    dtp.Clear();
                    dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                    dgv.DataSource = dtp;
                }
            }
        }

        private void tvvbtxt13_Click(object sender, EventArgs e)
        {
            oper_save();
            if (acc_journ_sett.Default.shw_extra_date == false)
            {
                try
                {
                    invoice_dataset.Clear();
                    string SqlString = "Select * FROM invoice WHERE DATEPART(dd, [Date]) = " + tvtxt15.Text;
                    if (Main.Amatrix.acc == "")
                    {
                        string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                    dgv.DataSource = invoice_dataset.invoice;
                                }
                            }
                        }
                    }
                    else
                    {
                        LAST_QUERY_USED = "Select * FROM invoice WHERE DATEPART(dd, [Date]) = " + tvtxt15.Text;
                        dtp.Clear();
                        dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                        dgv.DataSource = dtp;
                    }
                }
                catch (Exception erty) { }
            }
            else
            {
                try
                {
                    string SqlString = "Select * FROM invoice WHERE DATEPART(dd, [Date]) = '" + tvtxt15.Text + "' OR DATEPART(dd, [Due Date]) = '" + tvtxt15.Text + "' ";
                    LAST_QUERY_USED = SqlString;
                    if (Main.Amatrix.acc == "")
                    {
                        invoice_dataset.Clear();
                        string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                    dgv.DataSource = invoice_dataset.invoice;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtp.Clear();
                        dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                        dgv.DataSource = dtp;
                    }
                }
                catch (Exception erty) { }
            }
        }

        private void tvvbtxt14_Click(object sender, EventArgs e)
        {
            oper_save();
            if (acc_journ_sett.Default.shw_extra_date == false)
            {
                try
                {
                    invoice_dataset.Clear();
                    string SqlString = "Select * FROM invoice WHERE DATEPART(mm, [Date]) = " + tvtxt16.Text;
                    LAST_QUERY_USED = SqlString;
                    if (Main.Amatrix.acc == "")
                    {
                        string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                    dgv.DataSource = invoice_dataset.invoice;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtp.Clear();
                        dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                        dgv.DataSource = dtp;
                    }
                }
                catch (Exception erty) { }
            }
            else
            {
                try
                {
                    invoice_dataset.Clear();
                    string SqlString = "Select * FROM invoice WHERE DATEPART(mm, [Date]) = '" + tvtxt16.Text + "' OR DATEPART(mm, [Due Date]) = '" + tvtxt16.Text + "'";
                    LAST_QUERY_USED = SqlString;
                    if (Main.Amatrix.acc == "")
                    {
                        string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                    dgv.DataSource = invoice_dataset.invoice;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtp.Clear();
                        dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                        dgv.DataSource = dtp;
                    }
                }
                catch (Exception erty) { }
            }
        }

        private void tvvbtxt15_Click_1(object sender, EventArgs e)
        {
            oper_save();
            if (acc_journ_sett.Default.shw_extra_date == false)
            {
                try
                {
                    invoice_dataset.Clear();
                    string SqlString = "Select * FROM invoice WHERE DATEPART(yy, [Date]) = " + tvtxt17.Text;
                    LAST_QUERY_USED = SqlString;
                    if (Main.Amatrix.acc == "")
                    {
                        string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                    dgv.DataSource = invoice_dataset.invoice;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtp.Clear();
                        dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                        dgv.DataSource = dtp;
                    }
                }
                catch (Exception erty) { }
            }
            else
            {
                try
                {
                    invoice_dataset.Clear();
                    string SqlString = "Select * FROM invoice WHERE DATEPART(yy, [Date]) = '" + tvtxt17.Text + "' OR DATEPART(yy, [Due Date]) = '" + tvtxt17.Text + "'";
                    LAST_QUERY_USED = SqlString;
                    if (Main.Amatrix.acc == "")
                    {
                        string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                    dgv.DataSource = invoice_dataset.invoice;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtp.Clear();
                        dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                        dgv.DataSource = dtp;
                    }
                }
                catch (Exception erty) { }
            }
        }

        private void tvvbtxt15_Click(object sender, EventArgs e)
        {
            oper_save();
            if (acc_journ_sett.Default.shw_extra_date == false)
            {
                try
                {
                    invoice_dataset.Clear();
                    string SqlString = "Select * FROM invoice WHERE [Date] > '" + tvtxt18.Text + "' AND [Date] < '" + tvtxt19.Text + "'";
                    LAST_QUERY_USED = SqlString;
                    if (Main.Amatrix.acc == "")
                    {
                        string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {
                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();
                                using (reader)
                                {
                                    invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                    dgv.DataSource = invoice_dataset.invoice;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtp.Clear();
                        dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                        dgv.DataSource = dtp;
                    }
                }
                catch (Exception errty)
                {
                    Am_err mer = new Am_err(); mer.tx("!Enter String!");
                }
            }
            else
            {
                try
                {
                    invoice_dataset.Clear();
                    string SqlString = "Select * FROM invoice WHERE [Date] > '" + tvtxt18.Text + "' AND [Date] < '" + tvtxt19.Text + "' OR [Due Date] > '" + tvtxt18.Text + "' AND [Due Date] < '" + tvtxt19.Text + "'";
                    LAST_QUERY_USED = SqlString;
                    if (Main.Amatrix.acc == "")
                    {
                        string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                    dgv.DataSource = invoice_dataset.invoice;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtp.Clear();
                        dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                        dgv.DataSource = dtp;
                    }
                }
                catch (Exception errty)
                {
                    Am_err mer = new Am_err(); mer.tx("!Enter String!");
                }
            }
        }

        private void toolStripComboBox56_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox56.Text == "Yes")
            {
                acc_journ_sett.Default.shw_extra_date = true;
            }
            else if (toolStripComboBox56.Text == "no")
            {
                acc_journ_sett.Default.shw_extra_date = false;
            }
            else { acc_journ_sett.Default.shw_extra_date = false; }
            acc_journ_sett.Default.Save();
        }

        private void showDueDateValuesAlsoToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            if (acc_journ_sett.Default.shw_extra_date == true)
            {
                toolStripMenuItem398.Text = "Show Due Date Values Also (Currently : Yes)";
            }
            else
            {
                toolStripMenuItem398.Text = "Show Due Date Values Also (Currently : No)";
            }
        }

        //command GUI helpers


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
        private void tvtxt1_MouseEnter(object sender, EventArgs e)
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
        }

        private void tvtxt20_Click(object sender, EventArgs e)
        {
            tstt = (ToolStripTextBox)sender;
            if (tstt.Text == "Enter a Date (eg. 01-jan-91)" || tstt.Text == "Enter First Date (eg. 01-Jan-91)" || tstt.Text == "Show Invoice Of" || tstt.Text == "Enter Last Date (eg. 02-Feb-92)" || tstt.Text == "Enter a Value" || tstt.Text == "Enter a Value(Piece)" || tstt.Text == "Enter First Value" || tstt.Text == "Enter Last Value" || tstt.Text == "Enter a Year (eg. 1991)")
            {
                tstt.SelectAll();
            }
        }

        private void t_v_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            oper_save();
            if (e.Node.Text == "Show At Current Position")
            {
                string SqlString = "Select * From invoice Where [Serial Number] >= '" + smallest + "' AND [Serial Number] <= '" + biggest + "'";
                LAST_QUERY_USED = SqlString;
                if (Main.Amatrix.acc == "")
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    dtp.Clear();
                    dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                    dgv.DataSource = dtp;
                }
            }
            if (e.Node.Text == "Show All")
            {
                Showalljourn();
            }
            else if (e.Node.Text == "All")
            {
                if (e.Node.Parent.Text == "Show Information")
                {
                    tabControl1.SelectTab(0);
                }
                if (e.Node.Parent.Text == "Show Paid") 
                {
                    invoice_dataset.Clear();
                    //if (sender.Equals(toolStripButton97) == true)
                    //{
                        string SqlString = "Select * FROM invoice WHERE [Paid] = 'Yes'";
                        LAST_QUERY_USED = SqlString;
                        if (Main.Amatrix.acc == "")
                        {
                            string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                            using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                            {
                                using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                                {

                                    conn.Open();
                                    SqlCeDataReader reader = cmd.ExecuteReader();

                                    using (reader)
                                    {
                                        invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                        dgv.DataSource = invoice_dataset.invoice;
                                    }
                                }
                            }
                        }
                        else
                        {
                            dtp.Clear();
                            dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                            dgv.DataSource = dtp;
                        }
                    //}
                }
                else if (e.Node.Parent.Text == "Show Owing")
                {
                    string SqlString = "Select * FROM invoice WHERE [Owing] = 'Yes'";
                    LAST_QUERY_USED = SqlString;
                    if (Main.Amatrix.acc == "")
                    {
                        invoice_dataset.Clear();
                        string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                    dgv.DataSource = invoice_dataset.invoice;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtp.Clear();
                        dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                        dgv.DataSource = dtp;
                    }
                }
            }
            else if (e.Node.Text == "This Month")
            {
                if (e.Node.Parent.Text == "Show Paid")
                {
                    string SqlString = "Select * FROM invoice WHERE [Paid] LIKE 'Yes' AND DATEPART(mm, [Date]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Date]) = DATEPART(yy, GETDATE())";
                    LAST_QUERY_USED = SqlString;
                    if (Main.Amatrix.acc == "")
                    {
                        invoice_dataset.Clear();
                        string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                    dgv.DataSource = invoice_dataset.invoice;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtp.Clear();
                        dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                        dgv.DataSource = dtp;
                    }
                }
                else if (e.Node.Parent.Text == "Show Owing")
                {
                    string SqlString = "Select * FROM invoice WHERE [Owing] LIKE 'Yes' AND DATEPART(mm, [Date]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Date]) = DATEPART(yy, GETDATE())";
                    LAST_QUERY_USED = SqlString;
                    if (Main.Amatrix.acc == "")
                    {
                        invoice_dataset.Clear();
                        string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                    dgv.DataSource = invoice_dataset.invoice;
                                }
                            }
                        }
                    }
                    else
                    {
                        dtp.Clear();
                        dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                        dgv.DataSource = dtp;
                    }
                }
            }
            else if (e.Node.Text == "Shipping")
            {
                tabControl1.SelectTab(1);
            }
            else if (e.Node.Text == "Billing")
            {
                tabControl1.SelectTab(2);
            }
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

        private void colreordr_Click(object sender, EventArgs e)
        {
            dgv.AllowUserToOrderColumns = true;
        }

        private void colreordflse_Click(object sender, EventArgs e)
        {
            dgv.AllowUserToOrderColumns = false;
        }

        private void ascvw_Click(object sender, EventArgs e)
        {
            dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);
        }

        private void descvw_Click(object sender, EventArgs e)
        {
            dgv.Sort(dgv.Columns[0], ListSortDirection.Descending);
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

        private void ttp_del_Tick(object sender, EventArgs e)
        {
            ttp_del.Stop();
            if (err.DropDown.Visible != true)
            {
                err.Visible = false;
            }
        }

        private void ts2_MouseEnter_1(object sender, EventArgs e)
        {
            ts2.BackColor = Color.AliceBlue;
        }

        private void ts2_MouseLeave_1(object sender, EventArgs e)
        {
            ts2.BackColor = Color.Lavender;
        }

        //disposal
        void acc_invce_Disposed(object sender, EventArgs e)
        {
            invoiceTableAdapter.Connection.Close();
            invoice_dataset.Clear();
            dgv.DataSource = null;

            //local db dispose()
            invoice_dataset.Dispose();
            invoiceBindingSource.Dispose();
            invoiceTableAdapter.Dispose();

            customersTableAdapter.Connection.Close();
            cust_dtst.Clear();
            dataGridView1.DataSource = null;

            cust_dtst.Dispose();
            customersBindingSource.Dispose();
            customersTableAdapter.Dispose();

            prod_mgmtTableAdapter.Connection.Close();
            prodmgmt_dtst.Clear();
            dataGridView2.DataSource = null;

            prodmgmt_dtst.Dispose();
            prodmgmtBindingSource.Dispose();
            prod_mgmtTableAdapter.Dispose();

            dtp.Clear();
            dtp.Dispose();
            dtp_cust.Clear();
            dtp_cust.Dispose();

            toolStripButton4.Click -= toolStripButton4_Click;
            button8.Click -= button4_Click;
            button7.Click -= button5_Click;
            upinvce.Click -= upinvce_Click;
            tvtxt18.Click -= tvtxt20_Click;
            tvtxt18.KeyUp -= tvtxt20_KeyUp;
            tvtxt18.MouseEnter -= tvtxt1_MouseEnter;
            tvtxt18.MouseLeave -= tvtxt1_MouseLeave;
            tvtxt19.Click -= tvtxt20_Click;
            tvtxt19.KeyUp -= tvtxt20_KeyUp;
            tvtxt19.MouseEnter -= tvtxt1_MouseEnter;
            tvtxt19.MouseLeave -= tvtxt1_MouseLeave;
            tvtxt17.Click -= tvtxt20_Click;
            tvtxt17.KeyUp -= tvtxt20_KeyUp;
            tvtxt17.MouseEnter -= tvtxt1_MouseEnter;
            tvtxt17.MouseLeave -= tvtxt1_MouseLeave;
            toolStrip3.Click -= dgvwin_Click;
            tabControl1.Click -= dgvwin_Click;
            tabControl2.Click -= sst_Click;
            button11.Click -= button11_Click;
            t_v.Click -= sst_Click;
            contentsToolStripMenuItem.Click -= helpToolStripMenuItem1_Click;
            abtmnu.Click -= abtmnu_Click;
            rePartitionDataBaseToolStripMenuItem.Click -= rePartitionDataBaseToolStripMenuItem_Click;

            //toolStripButton138.Click -= toolStripButton138_Click;
            this.Deactivate -= this.acc_journ_dec;
            this.Load -= this.acc_invce_Load;
            this.Activated -= this.acc_journ_act;
            this.helpToolStripMenuItem1.Click -= helpToolStripMenuItem1_Click;
            this.switchDatabaseToolStripMenuItem.Click -= this.switchDatabaseToolStripMenuItem_Click;
            this.pnl_journvw.Click -= this.dgvwin_Click;
            this.selwin.MouseEnter -= this.selwin_MouseEnter;
            this.selwin.MouseLeave -= this.selwin_MouseLeave;
            this.jrntofnt.Click -= this.jrntofnt_Click;
            this.smtofnt.Click -= this.smtofnt_Click;
            this.minimizeAllToolStripMenuItem.Click -= this.minwins_Click;
            this.closeAllToTrayToolStripMenuItem.Click -= this.clseall_Click;
            this.restoreAllWindowsToolStripMenuItem.Click -= this.restawin_Click;
            this.summpnl.Click -= this.summpnl_Click;
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
            this.button6.Click -= this.button6_Click;
            this.button3.Click -= this.button3_Click;
            this.t_v.AfterCheck -= this.treeView1_AfterCheck;
            this.t_v.NodeMouseClick -= this.t_v_NodeMouseClick;
            this.t_v.Click -= this.summpnl_Click;
            this.sst.Click -= this.sst_Click;
            this.reszz3.ButtonClick -= this.reszz3_ButtonClick;
            this.toolStripMenuItem9.Click -= this.smtrze_Click;
            this.toolStripMenuItem10.Click -= this.smthtt_Click;
            this.movwin3.ButtonClick -= this.tswin3_DoubleClick;
            this.toolStripMenuItem12.Click -= this.movtolef3_Click;
            this.toolStripMenuItem13.Click -= this.movtrgt_Click;
            this.toolStripMenuItem14.Click -= this.movtbott3_Click;
            this.toolStripMenuItem15.Click -= this.movtop3_Click;
            this.toolStripMenuItem16.Click -= this.freesty3_Click;
            this.dgvwin.Click -= this.dgvwin_Click;
            this.tbc1.Click -= this.dgvwin_Click;
            this.tabPage7.Click -= this.dgvwin_Click;
            this.toolStripButton3.Click -= this.nxtst_Click;
            this.shw_here2.Click -= this.show_AllToolStripButton_Click_1;
            this.toolStripButton2.Click -= this.prev_ButtonClick;
            this.shw_here.Click -= this.show_AllToolStripButton_Click_1;
            this.showAllToolStripMenuItem1.Click -= this.show_AllToolStripButton_Click_1;
            this.dbtsumqry.Click -= this.dbtsumqry_Click_1;
            this.avgqryjourn.Click -= this.avgqryjourn_Click_1;
            this.toolStripTextBox183.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox183.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox183.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox183.Click -= this.tvtxt20_Click;
            this.toolStripTextBox184.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox184.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox184.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox184.Click -= this.tvtxt20_Click;
            this.toolStripButton261.Click -= this.dbtsumqry_Click_1;
            this.toolStripTextBox185.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox185.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox185.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox185.Click -= this.tvtxt20_Click;
            this.toolStripTextBox186.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox186.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox186.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox186.Click -= this.tvtxt20_Click;
            this.toolStripButton262.Click -= this.avgqryjourn_Click_1;
            this.toolStripTextBox89.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox89.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox89.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox89.Click -= this.tvtxt20_Click;
            this.toolStripTextBox90.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox90.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox90.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox90.Click -= this.tvtxt20_Click;
            this.toolStripTextBox87.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox87.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox87.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox87.Click -= this.tvtxt20_Click;
            this.toolStripTextBox88.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox88.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox88.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox88.Click -= this.tvtxt20_Click;
            this.toolStripTextBox93.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox93.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox93.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox93.Click -= this.tvtxt20_Click;
            this.toolStripTextBox94.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox94.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox94.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox94.Click -= this.tvtxt20_Click;
            this.toolStripTextBox91.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox91.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox91.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox91.Click -= this.tvtxt20_Click;
            this.toolStripTextBox92.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox92.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox92.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox92.Click -= this.tvtxt20_Click;
            this.currmnth.Click -= this.currmnth_Click;
            this.tvvbtxt13.Click -= this.tvvbtxt13_Click;
            this.tvvbtxt14.Click -= this.tvvbtxt14_Click;
            this.tvvbtxt15.Click -= this.tvvbtxt15_Click_1;
            this.tvvbtxt16.Click -= this.tvvbtxt15_Click;
            this.showDueDateValuesAlsoToolStripMenuItem.DropDownOpened -= this.showDueDateValuesAlsoToolStripMenuItem_DropDownOpened;
            this.toolStripComboBox56.SelectedIndexChanged -= this.toolStripComboBox56_SelectedIndexChanged;
            this.delete.ButtonClick -= this.delete_itms;
            this.del_all.Click -= this.delete_itms;
            this.new_rw.Click -= this.deletecell_Click;
            this.dgvupall.Click -= this.dgvupall_Click;
            this.dgvupone.Click -= this.dgvupall_Click;
            this.dgvleftall.Click -= this.dgvupall_Click;
            this.dgvleftone.Click -= this.dgvupall_Click;
            this.leftxtdgv.TextChanged -= this.uptxtdgv2_TextChanged;
            this.dgvrightone.Click -= this.dgvupall_Click;
            this.dgvtxtright.TextChanged -= this.uptxtdgv2_TextChanged;
            this.dgvrightall.Click -= this.dgvupall_Click;
            this.dgvdownone.Click -= this.dgvupall_Click;
            this.dgvdownall.Click -= this.dgvupall_Click;
            this.go_to.Click -= this.go_to_Click;
            this.show_invc_of.MouseEnter -= this.tvtxt1_MouseEnter;
            this.show_invc_of.MouseLeave -= this.tvtxt1_MouseLeave;
            this.show_invc_of.Click -= this.tvtxt20_Click;
            this.inf_8.MouseEnter -= this.inf_1_MouseEnter;
            this.inf_7.MouseEnter -= this.inf_1_MouseEnter;
            this.inf_5.MouseEnter -= this.inf_1_MouseEnter;
            this.inf_4.MouseEnter -= this.inf_1_MouseEnter;
            this.inf_6.MouseEnter -= this.inf_1_MouseEnter;
            this.inf_3.MouseEnter -= this.inf_1_MouseEnter;
            this.button2.Click -= this.button1_Click;
            this.button1.Click -= this.button1_Click;
            this.inf_2.MouseEnter -= this.inf_1_MouseEnter;
            this.inf_1.MouseEnter -= this.inf_1_MouseEnter;
            this.sh_inf4.MouseEnter -= this.inf_1_MouseEnter;
            this.sh_inf3.MouseEnter -= this.inf_1_MouseEnter;
            this.sh_inf2.MouseEnter -= this.inf_1_MouseEnter;
            this.sh_inf1.MouseEnter -= this.inf_1_MouseEnter;
            this.label11.MouseEnter -= this.inf_1_MouseEnter;
            this.bill_inf4.MouseEnter -= this.inf_1_MouseEnter;
            this.bill_inf3.MouseEnter -= this.inf_1_MouseEnter;
            this.bill_inf2.MouseEnter -= this.inf_1_MouseEnter;
            this.bill_inf1.MouseEnter -= this.inf_1_MouseEnter;
            this.dgv.CellBeginEdit -= this.dgv_CellBeginEdit;
            this.dgv.Sorted -= this.dgv_Sorted;
            this.dgv.UserAddedRow -= this.dgv_UserAddedRow;
            this.dgv.CellMouseLeave -= this.dgv_CellMouseLeave;
            this.dgv.Enter -= this.dgv_Enter;
            this.dgv.RowEnter -= this.dgv_RowEnter;
            this.dgv.CellMouseEnter -= this.dgv_CellMouseEnter;
            this.dgv.CellEndEdit -= this.dgv_CellEndEdit;
            this.dgv.DataError -= this.dgv_DataError;
            this.dgv.CellEnter -= this.dgv_CellEnter;
            this.dgv.Click -= this.tswin_Click;
            this.undoToolStripMenuItem.Click -= this.undoall_Click;
            this.copyToolStripMenuItem.Click -= this.cpy_Click;
            this.cutToolStripMenuItem.Click -= this.ct_Click;
            this.pasteToolStripMenuItem.Click -= this.pster_Click;
            this.deleteToolStripMenuItem.Click -= this.deletecell_Click;
            this.selectAllToolStripMenuItem.Click -= this.sall_Click;
            this.stwin.Click -= this.dgvwin_Click;
            this.reszz.ButtonClick -= this.reszz_ButtonClick;
            this.onlyWidthToolStripMenuItem.Click -= this.simjrnwth_Click;
            this.onlyHeightToolStripMenuItem.Click -= this.simjrnhgt_Click;
            this.mvewin.ButtonClick -= this.mvewin_ButtonClick;
            this.movtolef.Click -= this.movtolef_clc;
            this.toRightToolStripMenuItem.Click -= this.movtor;
            this.tobott.Click -= this.tobott_Click;
            this.totop.Click -= this.totop_Click;
            this.freesty.Click -= this.freesty_Click;
            this.tswin.MouseUp -= this.tswin_MouseUp;
            this.tswin.DoubleClick -= this.mvewin_ButtonClick;
            this.tswin.MouseEnter -= this.tswin_MouseEnter;
            this.tswin.MouseDown -= this.tswin_MouseDown;
            this.tswin.MouseLeave -= this.tswin_MouseLeave;
            this.tswin.Click -= this.tswin_Click;
            this.cnbnn1.MouseEnter -= this.cnbnn1_MouseEnter;
            this.cnbnn1.MouseLeave -= this.cnbnn1_MouseLeave;
            this.maxminmnu1.Click -= this.dvgpndoc_Click;
            this.toolStripMenuItem4.Click -= this.mvewin_ButtonClick;
            this.rszmnu.Click -= this.reszz_ButtonClick;
            this.toolStripMenuItem6.Click -= this.clse_Click;
            this.clse1.Click -= this.clse_Click;
            this.dvgpndoc.Click -= this.dvgpndoc_Click;
            this.svewin3.Click -= this.save_inv_Click;
            this.toolStripButton8.Click -= this.save_inv_Click;
            this.ts2.MouseEnter -= this.ts2_MouseEnter_1;
            this.ts2.MouseLeave -= this.ts2_MouseLeave_1;
            this.tbxfned.Leave -= this.tbxfned_Leave;
            this.tbxfned.Enter -= this.tbxfned_Enter;
            this.tbxfned.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tbxfned.MouseLeave -= this.tvtxt1_MouseLeave;
            this.gotoitm.Click -= this.gotoitm_Click;
            //this.edt.Click -= this.edt_Click;
            this.pnt.Click -= this.pnt_Click;
            this.rsdbord.Click -= this.rsdbord_Click;
            this.dflt_dgvbord.Click -= this.dflt_dgvbord_Click;
            this.O.Click -= this.IO_Click;
            this.IO.Click -= this.IO_Click;
            this.colreordr.Click -= this.colreordr_Click;
            this.colreordflse.Click -= this.colreordflse_Click;
            this.ascvw.Click -= this.ascvw_Click;
            this.descvw.Click -= this.descvw_Click;
            this.nopeascdesc.Click -= this.show_AllToolStripButton_Click_1;
            this.slctrgb.Click -= this.slctrgb_Click;
            this.fnt_mnu.DropDownOpened -= this.Virtual_menu;
            this.tbxfntlv.SelectedIndexChanged -= this.Virtual_Combo;
            this.tsc_fnt_sze.SelectedIndexChanged -= this.Virtual_Combo;
            this.toolStripMenuItem24.Click -= this.ewv_Click;
            this.toolStripMenuItem25.Click -= this.dwv_Click;
            this.save_inv.Click -= this.save_inv_Click;
            this.restr.Click -= this.rstrt_Click;
            this.clsemn.Click -= this.clsejournclc;
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
            this.stdtodefall.Click -= this.stdtodefall_Click;
            this.toolStripMenuItem28.Click -= this.slctrgb_Click;
            this.toolStripButton14.Click -= this.jrntofnt_Click;
            this.toolStripButton12.Click -= this.smtofnt_Click;
            this.undoall.Click -= this.undoall_Click;
            this.cpy.Click -= this.cpy_Click;
            this.ct.Click -= this.ct_Click;
            this.pster.Click -= this.pster_Click;
            this.deletecell.Click -= this.deletecell_Click;
            this.sall.Click -= this.sall_Click;
            this.initializeToolStripMenuItem.Click -= this.initializeToolStripMenuItem_Click;
            this.dynayes.Click -= this.dynayes_Click;
            this.dynano.Click -= this.dynano_Click;
            this.ys_autsve.Click -= this.ys_autsve_Click;
            this.no_autsve.Click -= this.ys_autsve_Click;
            this.tmex.Tick -= this.tmex_Tick;
            this.dgvwintic.Tick -= this.dgvwintc;
            this.decjourn.Tick -= this.decjourn_tc;
            this.tmeclse.Tick -= this.tmeclse_tc;
            this.col.Tick -= this.col_Tick;
            this.tmex3.Tick -= this.tmex3_Tick;
            this.dgvwintic3.Tick -= this.dgvwintic3_Tick;
            this.svebtn.Click -= this.save_inv_Click;
            this.clse.MouseLeave -= this.clse_MouseLeave;
            this.clse.ButtonClick -= this.clsejournclc;
            this.clse.MouseEnter -= this.clse_MouseEnter;
            this.rstrt.Click -= this.rstrt_Click;
            this.connlbl.MouseEnter -= this.connlblme;
            this.connlbl.MouseLeave -= this.connlblml;
            this.connlbl.Click -= this.connlbl_Click;
            this.bkkinit.DoWork -= this.bkkinit_DoWork;
            this.edtmnu.Click -= this.edtmnu_Click;
            this.zz.MouseLeave -= this.zz_MouseLeave;
            this.zz.MouseEnter -= this.zz_MouseEnter;
            this.toolStrip5.MouseEnter -= this.zz_MouseEnter;
            this.toolStrip5.MouseLeave -= this.zz_MouseLeave;
            this.toolStripButton5.Click -= this.undoall_Click;
            this.toolStripButton26.Click -= this.cpy_Click;
            this.toolStripButton6.Click -= this.ct_Click;
            this.toolStripButton9.Click -= this.pster_Click;
            this.toolStripButton10.Click -= this.sall_Click;
            this.toolStripButton13.Click -= this.deletecell_Click;
            this.remv_zz.Click -= this.remv_zz_Click;
            this.tmr.Tick -= this.clse_zz_Tick;
            this.ttp_del.Tick -= this.ttp_del_Tick;
            this.button10.Click -= this.button10_Click;
            this.radioButton6.Click -= this.radioButton6_Click;
            this.radioButton5.Click -= this.radioButton6_Click;
            this.radioButton4.Click -= this.radioButton6_Click;
            this.radioButton7.Click -= this.radioButton7_Click;
            this.radioButton8.Click -= this.radioButton7_Click;
            this.button5.Click -= this.button5_Click;
            this.button4.Click -= this.button4_Click;
            this.dataGridView1.CellMouseClick -= this.dataGridView1_CellMouseClick;
            this.radioButton2.Click -= this.radioButton2_CheckedChanged;
            this.radioButton1.Click -= this.radioButton2_CheckedChanged;
            this.dataGridView2.CellMouseClick -= this.dataGridView2_CellMouseClick;
            this.Disposed -= acc_invce_Disposed;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void dgv_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            numbrw.Text = howmany.ToString();
        }

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

            if (who == "Customer (Shipping To)")
            {
                who = "Shippers Name";
            }

            string SqlString = "Select * FROM invoice WHERE [" + who + "] = '" + what2 + "'";
            LAST_QUERY_USED = SqlString;
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.Clear();
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp.Clear();
                dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                dgv.DataSource = dtp;
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

            if (who == "Customer (Shipping To)")
            {
                who = "Shippers Name";
            }

            string SqlString = "Select * FROM invoice WHERE [" + who + "] != '" + what2 + "'";
            LAST_QUERY_USED = SqlString;
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.Clear();
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp.Clear();
                dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                dgv.DataSource = dtp;
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

            if (who == "Customer (Shipping To)")
            {
                who = "Shippers Name";
            }

            string SqlString = "Select * FROM invoice WHERE [" + who + "] > '" + what2 + "' AND [" + who + "] < '" + tbxx2.Text + "'";
            LAST_QUERY_USED = SqlString;
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.Clear();
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp.Clear();
                dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                dgv.DataSource = dtp;
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

            if (who == "Customer (Shipping To)")
            {
                who = "Shippers Name";
            }

            string SqlString = "Select * FROM invoice WHERE [" + who + "] < '" + what2 + "'";
            LAST_QUERY_USED = SqlString;
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.Clear();
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp.Clear();
                dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                dgv.DataSource = dtp;
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

            if (who == "Customer (Shipping To)")
            {
                who = "Shippers Name";
            }

            string SqlString = "Select * FROM invoice WHERE [" + who + "] > '" + what2 + "'";
            LAST_QUERY_USED = SqlString;
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.Clear();
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp.Clear();
                dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                dgv.DataSource = dtp;
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

            if (who == "Customer (Shipping To)")
            {
                who = "Shippers Name";
            }

            string SqlString = "Select * FROM invoice WHERE [" + who + "] LIKE '" + what2 + "%'";
            LAST_QUERY_USED = SqlString;
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.Clear();
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp.Clear();
                dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                dgv.DataSource = dtp;
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

            if (who == "Customer (Shipping To)")
            {
                who = "Shippers Name";
            }

            string SqlString = "Select * FROM invoice WHERE [" + who + "] LIKE '%" + what2 + "'";
            LAST_QUERY_USED = SqlString;
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.Clear();
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp.Clear();
                dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                dgv.DataSource = dtp;
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

            if (who == "Customer (Shipping To)")
            {
                who = "Shippers Name";
            }

            string SqlString = "Select * FROM invoice WHERE [" + who + "] LIKE '%" + what2 + "%'";
            LAST_QUERY_USED = SqlString;
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.Clear();
                string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                            dgv.DataSource = invoice_dataset.invoice;
                        }
                    }
                }
            }
            else
            {
                dtp.Clear();
                dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                dgv.DataSource = dtp;
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

        private void dgv_Enter(object sender, EventArgs e)
        {
            conn2();
        }

        private void ys_autsve_Click(object sender, EventArgs e)
        {
            if (sender.Equals(ys_autsve) == true)
            {
                acc_journ_sett.Default.autosave = true;
            }
            else if (sender.Equals(no_autsve) == true)
            {
                acc_journ_sett.Default.autosave = false;
            }
            acc_journ_sett.Default.Save();
        }

        private void dbtsumqry_Click_1(object sender, EventArgs e)
        {
            oper_save();
            invoice_dataset.Clear();
            if (sender.Equals(toolStripButton261) == true)
            {
                string SqlString = "Select sum([Serial Number]), sum([Unit Price]), sum([Units Ordered]), sum([Units Delivered]), sum([Cost]), sum([Profit]), sum([Owing Amount]), sum([Paid Amount]), sum([Vat]), sum([Other Tax]), sum([Total After Tax]), sum([Total]) FROM invoice WHERE [Date] > '" + toolStripTextBox183.Text + "' AND [Date] < '" + toolStripTextBox184.Text + "'";
                LAST_QUERY_USED = SqlString;
                if (Main.Amatrix.acc == "")
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    dtp.Clear();
                    dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                    dgv.DataSource = dtp;
                }
            }
            else if (sender.Equals(toolStripButton140) == true)
            {
                string SqlString = "Select sum([Serial Number]), sum([Unit Price]), sum([Units Ordered]), sum([Units Delivered]), sum([Cost]), sum([Profit]), sum([Owing Amount]), sum([Paid Amount]), sum([Vat]), sum([Other Tax]), sum([Total After Tax]), sum([Total]) FROM invoice WHERE [Billers Name] = '" + dgv[4, dgv.CurrentRow.Index].Value.ToString() + "' AND [Date] > '" + toolStripTextBox89.Text + "' AND [Date] < '" + toolStripTextBox90.Text + "'";
                LAST_QUERY_USED = SqlString; 
                if (Main.Amatrix.acc == "")
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    dtp.Clear();
                    dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                    dgv.DataSource = dtp;
                }
            }
            else if (sender.Equals(toolStripButton142) == true)
            {
                string SqlString = "Select sum([Serial Number]), sum([Unit Price]), sum([Units Ordered]), sum([Units Delivered]), sum([Cost]), sum([Profit]), sum([Owing Amount]), sum([Paid Amount]), sum([Vat]), sum([Other Tax]), sum([Total After Tax]), sum([Total]) FROM invoice WHERE [Shippers Name] = '" + dgv[5, dgv.CurrentRow.Index].Value.ToString() + "' AND [Date] > '" + toolStripTextBox93.Text + "' AND [Date] < '" + toolStripTextBox94.Text + "'";
                LAST_QUERY_USED = SqlString;
                if (Main.Amatrix.acc == "")
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    dtp.Clear();
                    dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                    dgv.DataSource = dtp;
                }
            }
            else
            {
                string SqlString = "Select sum([Serial Number]), sum([Unit Price]), sum([Units Ordered]), sum([Units Delivered]), sum([Cost]), sum([Profit]), sum([Owing Amount]), sum([Paid Amount]), sum([Vat]), sum([Other Tax]), sum([Total After Tax]), sum([Total]) FROM invoice";// WHERE [" + who + "] LIKE '%" + what2 + "%'";
                LAST_QUERY_USED = SqlString;
                if (Main.Amatrix.acc == "")
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    dtp.Clear();
                    dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                    dgv.DataSource = dtp;
                }
            }
        }

        private void avgqryjourn_Click_1(object sender, EventArgs e)
        {
            if (sender.Equals(toolStripButton262) == true)
            {
                string SqlString = "Select avg([Serial Number]), avg([Unit Price]), avg([Units Ordered]), avg([Units Delivered]), avg([Cost]), avg([Profit]), avg([Owing Amount]), avg([Paid Amount]), avg([Vat]), avg([Other Tax]), avg([Total After Tax]), avg([Total]) FROM invoice WHERE [Date] > '" + toolStripTextBox185.Text + "' AND [Date] < '" + toolStripTextBox186.Text + "'";
                LAST_QUERY_USED = SqlString;
                if (Main.Amatrix.acc == "")
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    dtp.Clear();
                    dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                    dgv.DataSource = dtp;
                }
            }
            else if (sender.Equals(toolStripButton139) == true)
            {
                string SqlString = "Select avg([Serial Number]), avg([Unit Price]), avg([Units Ordered]), avg([Units Delivered]), avg([Cost]), avg([Profit]), avg([Owing Amount]), avg([Paid Amount]), avg([Vat]), avg([Other Tax]), avg([Total After Tax]), avg([Total]) FROM invoice WHERE [Billers Name] = '" + dgv[4, dgv.CurrentRow.Index].Value.ToString() + "' AND [Date] > '" + toolStripTextBox87.Text + "' AND [Date] < '" + toolStripTextBox88.Text + "'";
                LAST_QUERY_USED = SqlString;
                if (Main.Amatrix.acc == "")
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    dtp.Clear();
                    dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                    dgv.DataSource = dtp;
                }
            }
            else if (sender.Equals(toolStripButton141) == true)
            {
                string SqlString = "Select avg([Serial Number]), avg([Unit Price]), avg([Units Ordered]), avg([Units Delivered]), avg([Cost]), avg([Profit]), avg([Owing Amount]), avg([Paid Amount]), avg([Vat]), avg([Other Tax]), avg([Total After Tax]), avg([Total]) FROM invoice WHERE [Shippers Name] = '" + dgv[5, dgv.CurrentRow.Index].Value.ToString() + "' AND [Date] > '" + toolStripTextBox91.Text + "' AND [Date] < '" + toolStripTextBox92.Text + "'";
                LAST_QUERY_USED = SqlString;
                if (Main.Amatrix.acc == "")
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    dtp.Clear();
                    dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                    dgv.DataSource = dtp;
                }
            }
            else
            {
                string SqlString = "Select avg([Serial Number]), avg([Unit Price]), avg([Units Ordered]), avg([Units Delivered]), avg([Cost]), avg([Profit]), avg([Owing Amount]), avg([Paid Amount]), avg([Vat]), avg([Other Tax]), avg([Total After Tax]), avg([Total]) FROM invoice WHERE [" + who + "] LIKE '%" + what2 + "%'";
                LAST_QUERY_USED = SqlString;
                if (Main.Amatrix.acc == "")
                {
                    invoice_dataset.Clear();
                    string ConnString = invoiceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                invoice_dataset.Load(reader, LoadOption.PreserveChanges, "invoice");
                                dgv.DataSource = invoice_dataset.invoice;
                            }
                        }
                    }
                }
                else
                {
                    dtp.Clear();
                    dtp = basql.Execute(Main.Amatrix.acc, SqlString, "invoice", dtp);
                    dgv.DataSource = dtp;
                }
            }
        }

        private void edtmnu_Click(object sender, EventArgs e)
        {
            loggy lg = new loggy();
            lg.Show();
        }

        private void pnt_Click(object sender, EventArgs e)
        {
            PrintDataGrid.PrintDGV.Print_DataGridView(dgv);
        }

        ToolStripTextBox tbx_tstemp;
        TextBox tbx_temp;
        Color cl = new Color(); ComboBox cbx_tempcol;
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

        TextBox tbx_tmp;
        private void inf_1_MouseEnter(object sender, EventArgs e)
        {
            tbx_temp = (TextBox)sender;
            button2.Location = new Point(tbx_temp.Location.X - (button2.Size.Width + 3), tbx_temp.Location.Y - 2);
            button1.Location = new Point(tbx_temp.Location.X + tbx_temp.Size.Width, tbx_temp.Location.Y - 2);
            tbx_temp.Parent.Controls.Add(button1);
            tbx_temp.Parent.Controls.Add(button2);
            if (button1.Visible == false)
            {
                button1.Visible = true; button2.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(button1) == true)
                {
                    tbx_temp.Select();
                    tbx_temp.SelectionStart = tbx_temp.Text.Length;
                }
                if (sender.Equals(button2) == true)
                {
                    tbx_temp.Select();
                    tbx_temp.SelectionStart = 0;
                }
            }
            catch (Exception erty) { }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dgv[5, dgv.CurrentRow.Index].Value = dataGridView1[0, e.RowIndex].Value.ToString();
            }
            catch (Exception erty) { }
            //phone
            if (dataGridView1[6, e.RowIndex].Value != DBNull.Value && dataGridView1[7, e.RowIndex].Value != DBNull.Value)
            {
                radioButton8.Enabled = true;
                radioButton7.Enabled = true;
                if (radioButton8.Checked == true)
                {
                    dgv[9, dgv.CurrentRow.Index].Value = "Mobile : " + dataGridView1[6, e.RowIndex].Value;
                }
                else if (radioButton7.Checked == true) 
                {
                    dgv[9, dgv.CurrentRow.Index].Value = "Fixed : " + dataGridView1[7, e.RowIndex].Value;
                }
            }
            else 
            { 
                radioButton7.Enabled = false; radioButton8.Enabled = false;
                if (dataGridView1[6, e.RowIndex].Value != DBNull.Value)
                {
                    dgv[9, dgv.CurrentRow.Index].Value = "Mobile : " + dataGridView1[6, e.RowIndex].Value;
                }
                else if (dataGridView1[7, e.RowIndex].Value != DBNull.Value)
                {
                    dgv[9, dgv.CurrentRow.Index].Value = "Fixed : " + dataGridView1[7, e.RowIndex].Value;
                }
            }

            //address
            if (dataGridView1[12, e.RowIndex].Value != DBNull.Value && dataGridView1[13, e.RowIndex].Value != DBNull.Value && dataGridView1[14, e.RowIndex].Value != DBNull.Value)
            {
                panel3.Enabled = true;
                if (radioButton6.Checked == true) { dgv[7, dgv.CurrentRow.Index].Value = "Official Address : " + dataGridView1[12, e.RowIndex].Value; }
                else if (radioButton5.Checked == true) { dgv[7, dgv.CurrentRow.Index].Value = "Ware-House Address : " + dataGridView1[13, e.RowIndex].Value; }
                else if (radioButton4.Checked == true) { dgv[7, dgv.CurrentRow.Index].Value = "Logistical Address : " + dataGridView1[14, e.RowIndex].Value; }
            }
            else if (dataGridView1[12, e.RowIndex].Value == DBNull.Value && dataGridView1[13, e.RowIndex].Value == DBNull.Value && dataGridView1[14, e.RowIndex].Value == DBNull.Value)
            {
                panel3.Enabled = false;
            }
            else
            {
                panel3.Enabled = true;
                if (dataGridView1[12, e.RowIndex].Value != DBNull.Value)
                {
                    dgv[7, dgv.CurrentRow.Index].Value = "Official Address : " + dataGridView1[12, e.RowIndex].Value;
                }
                else if (dataGridView1[13, e.RowIndex].Value != DBNull.Value)
                {
                    dgv[7, dgv.CurrentRow.Index].Value = "Ware-House Address : " + dataGridView1[13, e.RowIndex].Value;
                }
                else if (dataGridView1[14, e.RowIndex].Value != DBNull.Value)
                {
                    dgv[7, dgv.CurrentRow.Index].Value = "Logistical Address : " + dataGridView1[14, e.RowIndex].Value;
                }

                if (dataGridView1[12, e.RowIndex].Value == DBNull.Value) { radioButton6.Enabled = false; } else { radioButton6.Enabled = true; }
                if (dataGridView1[13, e.RowIndex].Value == DBNull.Value) { radioButton5.Enabled = false; } else { radioButton5.Enabled = true; }
                if (dataGridView1[14, e.RowIndex].Value == DBNull.Value) { radioButton4.Enabled = false; } else { radioButton4.Enabled = true; }
            }

            if (panel3.Enabled == false && radioButton8.Enabled == false && radioButton7.Enabled == false)
            {
                cst_box.Size = new Size(247, 175);
            }
            else { cst_box.Size = new Size(247, 305); }

            try
            {
                dgv[11, dgv.CurrentRow.Index].Value = dataGridView1[8, dataGridView1.CurrentRow.Index].Value;
            }
            catch (Exception ertyu) { }
            try
            {
                dgv[13, dgv.CurrentRow.Index].Value = dataGridView1[9, dataGridView1.CurrentRow.Index].Value.ToString() + "@" + dataGridView1[10, dataGridView1.CurrentRow.Index].Value.ToString() + "." + dataGridView1[11, dataGridView1.CurrentRow.Index].Value.ToString();
            }
            catch (Exception erty) { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (sender.Equals(button4) == true)
            {
                cust_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(customersTableAdapter.Connection.ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Customers", conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    cust_dtst.Load(dr, LoadOption.PreserveChanges, "Customers");
                    conn.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    cust_dtst.Load(dr, LoadOption.PreserveChanges, "Customers");
                    conn.Close();
                }
            }
            else
            {
                prodmgmt_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(customersTableAdapter.Connection.ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt", conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    prodmgmt_dtst.Load(dr, LoadOption.PreserveChanges, "Prod_mgmt");
                    conn.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Prod_mgmt", conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    prodmgmt_dtst.Load(dr, LoadOption.PreserveChanges, "Prod_mgmt");
                    conn.Close();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (sender.Equals(button5) == true)
            {
                cust_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(customersTableAdapter.Connection.ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Customers WHERE [Corporate Name] LIKE '%" + textBox11.Text + "%'", conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    cust_dtst.Load(dr, LoadOption.PreserveChanges, "Customers");
                    conn.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE [Corporate Name] LIKE '%" + textBox11.Text + "%'", conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    cust_dtst.Load(dr, LoadOption.PreserveChanges, "Customers");
                    conn.Close();
                }
            }
            else
            {
                prodmgmt_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(customersTableAdapter.Connection.ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt WHERE [Product Name] LIKE '%" + textBox11.Text + "%'", conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    prodmgmt_dtst.Load(dr, LoadOption.PreserveChanges, "Prod_mgmt");
                    conn.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Prod_mgmt WHERE [Product Name] LIKE '%" + textBox11.Text + "%'", conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    cust_dtst.Load(dr, LoadOption.PreserveChanges, "Prod_mgmt");
                    conn.Close();
                }
            }
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                customersBindingSource.EndEdit();
                customersTableAdapter.Update(cust_dtst);
            }
            else
            {
                asql.Save(cust_dtst.Customers, "Customers", Main.Amatrix.mgt);
            }
        }

        private double dbl1, dbl2;
        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgv[14, dgv.CurrentRow.Index].Value = dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString();
            dgv[15, dgv.CurrentRow.Index].Value = dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString();
            dgv[16, dgv.CurrentRow.Index].Value = dataGridView2[2, dataGridView2.CurrentRow.Index].Value.ToString();

            try
            {
                if (dataGridView2[6, e.RowIndex].Value != DBNull.Value)
                {
                    prod_box.Size = new Size(247, 286);
                    if (radioButton1.Checked == true)
                    {
                        try
                        {
                            dgv[17, dgv.CurrentRow.Index].Value = dataGridView2[5, dataGridView2.CurrentRow.Index].Value.ToString();
                        }
                        catch (Exception erty) { }
                    }
                    else if (radioButton2.Checked == true)
                    {
                        dbl1 = Convert.ToDouble(dataGridView2[5, e.RowIndex].Value);
                        dbl2 = Convert.ToDouble(dataGridView2[6, e.RowIndex].Value);
                        dbl1 = dbl1 + dbl2;
                        dgv[17, dgv.CurrentRow.Index].Value = dbl1;
                    }
                }
                else if (dataGridView2[6, e.RowIndex].Value == DBNull.Value) { prod_box.Size = new Size(247, 175); }
            }
            catch (Exception erty) { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mgmt_supch sh = new mgmt_supch();
            sh.tx(dgv[14, dgv.CurrentRow.Index].Value.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            App_Shoppe shp = new App_Shoppe();
            shp.Show();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    try
                    {
                        dgv[17, dgv.CurrentRow.Index].Value = dataGridView2[5, dataGridView2.CurrentRow.Index].Value.ToString();
                    }
                    catch (Exception erty) { }
                }
                else if (radioButton2.Checked == true)
                {
                    dbl1 = Convert.ToDouble(dataGridView2[5, dataGridView2.CurrentRow.Index].Value);
                    dbl2 = Convert.ToDouble(dataGridView2[6, dataGridView2.CurrentRow.Index].Value);
                    dbl1 = dbl1 + dbl2;
                    dgv[17, dgv.CurrentRow.Index].Value = dbl1;
                }
            }
            catch (Exception erty) { }
        }

        private void radioButton7_Click(object sender, EventArgs e)
        {
            if (radioButton8.Checked == true)
            {
                dgv[9, dgv.CurrentRow.Index].Value = "Mobile : " + dataGridView1[6, dataGridView1.CurrentRow.Index].Value;
            }
            else if (radioButton7.Checked == true)
            {
                dgv[9, dgv.CurrentRow.Index].Value = "Fixed : " + dataGridView1[7, dataGridView1.CurrentRow.Index].Value;
            }
        }

        private void radioButton6_Click(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
            {
                dgv[7, dgv.CurrentRow.Index].Value = "Official Address : " + dataGridView1[12, dataGridView1.CurrentRow.Index].Value;
            }
            else if (radioButton5.Checked == true)
            {
                dgv[7, dgv.CurrentRow.Index].Value = "Ware-House Address : " + dataGridView1[12, dataGridView1.CurrentRow.Index].Value;
            }
            else if (radioButton4.Checked == true)
            {
                dgv[7, dgv.CurrentRow.Index].Value = "Logistical Address : " + dataGridView1[12, dataGridView1.CurrentRow.Index].Value;
            }
            try
            {
                dgv[11, dgv.CurrentRow.Index].Value = dataGridView1[8, dataGridView1.CurrentRow.Index].Value;
            }
            catch (Exception ertyu) { }
            try
            {
                dgv[13, dgv.CurrentRow.Index].Value = dataGridView1[9, dataGridView1.CurrentRow.Index].Value.ToString() + "@" + dataGridView1[10, dataGridView1.CurrentRow.Index].Value.ToString() + "." + dataGridView1[11, dataGridView1.CurrentRow.Index].Value.ToString();
            }
            catch (Exception erty) { }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (sender.Equals(button10) == true)
            {
                mgmt_pr pr = new mgmt_pr();
                pr.Show();
            }
            else { mgmt_supch sh = new mgmt_supch(); sh.Show(); }
        }

        private void switchDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy_adv adv = new loggy_adv();
            adv.Show();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.tx(this.Name);
        }

        private void abtmnu_Click(object sender, EventArgs e)
        {
            app_abt abt = new app_abt();
            abt.descr(this.Text);
        }

        private void rePartitionDataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reparttn tn = new reparttn();
            tn.Show();
        }

        private void upinvce_Click(object sender, EventArgs e)
        {
            Application_FILEPOOL flepl = new Application_FILEPOOL();
            flepl.tx(dgv[0, dgv.CurrentRow.Index].Value.ToString(), "invoices");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                mgmt_pr prr = new mgmt_pr();
                prr.tx(dgv[5, dgv.CurrentRow.Index].Value.ToString());
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Unable to Complete and Open Transaction."); }
        }

        public void tx_out(string INV_NO)
        {
            thinitdb.Abort();
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.Clear();
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM invoice WHERE [Invoice Reference Number (ID)] = '" + INV_NO + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                invoice_dataset.invoice.Load(dr);
                conn.Close();
            }
            else
            {
                dtp.Clear();
                LAST_QUERY_USED = "SELECT * FROM invoice WHERE [Invoice Reference Number (ID)] = '" + INV_NO + "'";
                dtp = basql.Execute(Main.Amatrix.acc, "SELECT * FROM invoice WHERE [Invoice Reference Number (ID)] = '" + INV_NO + "'", "invoice", dtp);
                dgv.DataSource = dtp;
            }
        }

        Isync.isync_start asnc;
        private void tms_Tick(object sender, EventArgs e)
        {
            asnc = new Isync.isync_start(Main.Amatrix.acc, dtp, LAST_QUERY_USED);
            asnc.bkk.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkk_RunWorkerCompleted);
            asnc.call();
        }

        void bkk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dtp.Merge(asnc.dtp_main);
            dtp.AcceptChanges();
            dgv.AllowUserToAddRows = true;
            GC.Collect();
        }

        bool cancel = false;
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            oper_save();
            dgv.Enabled = false;
            int x, y;
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            x = dgv.CurrentCell.ColumnIndex;
            y = dgv.CurrentRow.Index;
            if (Main.Amatrix.acc == "")
            {
                invoice_dataset.invoice.Clear();
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                SqlCeCommand cmd = new SqlCeCommand(LAST_QUERY_USED, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                invoice_dataset.invoice.Load(dr);
                conn.Close();
            }
            else
            {
                dtp.Clear();
                SqlConnection conn = new SqlConnection(Main.Amatrix.acc);
                SqlCommand cmd = new SqlCommand(LAST_QUERY_USED, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dtp.Load(dr);
                conn.Close();
            }
            dgv.CurrentCell = dgv[x, y];
        }
    }
}