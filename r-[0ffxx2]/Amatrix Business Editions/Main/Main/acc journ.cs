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
using System.Data.Sql;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Extern_ASQL;
using Base_ASQL;

namespace Main
{
    public partial class acc_journ : Form
    {
        private string whtt;
        private string wter;
        private int winwin = 0;
        private int howmany;
        private int maxm;
        private int biggest = 100;
        private int smallest = 1;

        private int biggest2 = 100;
        private int smallest2 = 1;
        private int howmany2;
        private int maxm2;

        private int biggest3 = 100;
        private int smallest3 = 1;
        private int howmany3;
        private int maxm3;

        private int biggest4 = 100;
        private int smallest4 = 1;
        private int howmany4;
        private int maxm4;

        private ArrayList aund = new ArrayList();
        private ArrayList aundC = new ArrayList();
        private ArrayList aundR = new ArrayList();

        private ArrayList aund2 = new ArrayList();
        private ArrayList aundC2 = new ArrayList();
        private ArrayList aundR2 = new ArrayList();

        private ArrayList aund3 = new ArrayList();
        private ArrayList aundC3 = new ArrayList();
        private ArrayList aundR3 = new ArrayList();

        private ArrayList aund4 = new ArrayList();
        private ArrayList aundC4 = new ArrayList();
        private ArrayList aundR4 = new ArrayList();

        private DataTable Datatb = new DataTable();
        private DataTable Datatb2 = new DataTable();
        private DataTable Datatb3 = new DataTable();
        private DataTable Datatb4 = new DataTable();
        //Recognition
        private int winstatus = 0;

        //Cross Threading Objects
        private Thread thinit;
        private delegate void delinit();

        private Thread thinit2;
        private delegate void delinit2();

        //dbm classes
        Extern_Sql asql = new Extern_Sql();
        BASQL basql = new BASQL();

        public acc_journ()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Icon = Properties.Resources.amdsicnico;
            this.DoubleBuffered = true;
            this.Disposed += new EventHandler(acc_journ_Disposed);
            InitializeComponent();
            this.Text = "Amatrix Books";
            try
            {
                tmeinit.Start();
            }
            catch (Exception erty) { Init(); }

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

        private void thinit2strt()
        {
            thinit2 = new Thread(new ThreadStart(delinit2strt));
            thinit2.Start();
        }

        private void delinit2strt()
        {
            try
            {
                this.Invoke(new delinit2(settset));
            }
            catch (Exception erty) { settset(); }
        }

        private void settset()
        {
            if (acc_journ_sett.Default.widevw == false)
            {
                pnl_journvw.AutoScroll = false;
            }
            else if (acc_journ_sett.Default.widevw == true)
            {
                pnl_journvw.AutoScroll = true;
            }

            if (acc_journ_sett.Default.dgvborder == 0)
            {
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;

                dgv2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv2.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                
                dgv3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv3.CellBorderStyle = DataGridViewCellBorderStyle.Single;

                dgv4.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv4.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            }

            else if (acc_journ_sett.Default.dgvborder == 1)
            {
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.Raised;

                dgv2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv2.CellBorderStyle = DataGridViewCellBorderStyle.Raised;

                dgv3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv3.CellBorderStyle = DataGridViewCellBorderStyle.Raised;

                dgv4.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv4.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            }
            else { acc_journ_sett.Default.dgvborder = 0; acc_journ_sett.Default.Save(); }
            SUMS();
            thinit2.Abort();
        }

        //int main
        private void thinitstrt()
        {
            try
            {
                thinit = new Thread(new ThreadStart(delinitstrt));
                thinit.IsBackground = true;
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

        private void initconn1_DoWork(object sender, DoWorkEventArgs e)
        {
            initconn();
        }

        private void initconn1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Main.Amatrix.acc == "")
            {
                dgv.DataSource = journ_dtst.journal;
            }
            else
            {
                dgv.DataSource = Datatb;
            }
        }

        private void init_conn2_DoWork(object sender, DoWorkEventArgs e)
        {
            initconn2();
        }

        private void init_conn2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Main.Amatrix.acc == "")
            {
                dgv2.DataSource = cashbook_dtst.CashBook;
            }
            else
            {
                dgv2.DataSource = Datatb2;
            }
        }

        private void init_conn3_DoWork(object sender, DoWorkEventArgs e)
        {
            initconn3();
        }

        private void init_conn3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Main.Amatrix.acc == "")
            {
                dgv3.DataSource = purchaseBook_dtst.PurchaseBook;
            }
            else
            {
                dgv3.DataSource = Datatb3;
            }
        }

        private void init_conn4_DoWork(object sender, DoWorkEventArgs e)
        {
            initconn4();
        }

        private void init_conn4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Main.Amatrix.acc == "")
            {
                dgv4.DataSource = salesBook_dtst.SalesBook;
            }
            else
            {
                dgv4.DataSource = Datatb4;
            }
        }

        private int srry = 0;
        private void initconn()
        {
            Last_Query_Used = "Select * From journal Where [Serial Number] >= 1 AND [Serial Number] <= 100";
            journ_dtst.Clear(); Datatb.Clear();
            if (Main.Amatrix.acc == "")
            {
                try
                {
                    journalTableAdapter.Connection.Open();
                }
                catch (Exception erty) { }
                try
                {
                    string ConnString = journalTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand("Select * From journal Where [Serial Number] >= 1 AND [Serial Number] <= 100", conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                            }
                            cmd.Dispose();
                            reader.Dispose();
                        }
                        conn.Dispose();
                    }
                }
                catch (Exception erty) { }
            }
            else
            {
                try
                {
                    Datatb = asql.Go_To_Position(Main.Amatrix.acc, Datatb, "journal", true, 0, 101);
                }
                catch (Exception ertyy) { Am_err ner = new Am_err(); ner.tx(ertyy.Message); }
            }
        }

        string Last_Query_Used;
        private void initconn2()
        {
            //Cashbook
            cashbook_dtst.Clear(); Datatb2.Clear();
            Last_Query_Used_cb = "Select * From CashBook Where [Serial Number] >= 1 AND [Serial Number] <= 100";
            if (Main.Amatrix.acc == "")
            {
                try
                {
                    cashBookTableAdapter.Connection.Open();
                }
                catch (Exception erty) { } 
                try
                {
                    if (extern_opn == 0)
                    {
                        string ConnString2 = cashBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn2 = new SqlCeConnection(ConnString2))
                        {
                            using (SqlCeCommand cmd2 = new SqlCeCommand("Select * From CashBook Where [Serial Number] >= 1 AND [Serial Number] <= 100", conn2))
                            {
                                cmd2.CommandType = CommandType.Text;

                                conn2.Open();
                                SqlCeDataReader reader2 = cmd2.ExecuteReader();

                                using (reader2)
                                {
                                    cashbook_dtst.Load(reader2, LoadOption.PreserveChanges, "CashBook");
                                }
                                cmd2.Dispose();
                                reader2.Dispose();
                            }
                            conn2.Dispose();
                        }
                    }
                }
                catch (Exception erty) { }
            }
            else
            {
                try
                {
                    Datatb2 = asql.Go_To_Position(Main.Amatrix.acc, Datatb2, "CashBook", true, 0, 101);
                }
                catch (Exception ertyy) { Am_err ner = new Am_err(); ner.tx(ertyy.Message); }
            }
        }

        private void initconn3()
        {
            //purchase book
            Datatb3.Clear(); purchaseBook_dtst.Clear();
            Last_Query_Used_pb = "Select * From PurchaseBook Where [Serial Number] >= 1 AND [Serial Number] <= 100";
            if (Main.Amatrix.acc == "")
            {
                try
                {
                    purchaseBookTableAdapter.Connection.Open();
                }
                catch (Exception erty) { }
                try
                {
                    string ConnString2 = purchaseBookTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn2 = new SqlCeConnection(ConnString2))
                    {
                        using (SqlCeCommand cmd2 = new SqlCeCommand("Select * From PurchaseBook Where [Serial Number] >= 1 AND [Serial Number] <= 100", conn2))
                        {
                            cmd2.CommandType = CommandType.Text;
                            conn2.Open();
                            SqlCeDataReader reader2 = cmd2.ExecuteReader();

                            using (reader2)
                            {
                                purchaseBook_dtst.Load(reader2, LoadOption.PreserveChanges, "PurchaseBook");
                                
                            }
                            cmd2.Dispose();
                            reader2.Dispose();
                        }
                        conn2.Dispose();
                    }
                }
                catch (Exception erty) { }
            }
            else
            {
                try
                {
                    Datatb3 = asql.Go_To_Position(Main.Amatrix.acc, Datatb3, "PurchaseBook", true, 0, 101);
                }
                catch (Exception ertyy) { Am_err ner = new Am_err(); ner.tx(ertyy.Message); }
            }
        }

        private void initconn4()
        {
            Datatb4.Clear(); salesBook_dtst.Clear();
            Last_Query_Used_sb = "Select * From SalesBook Where [Serial Number] >= 1 AND [Serial Number] <= 100";
            if (Main.Amatrix.acc == "")
            {
                try
                {
                    salesBookTableAdapter.Connection.Open();
                }
                catch (Exception erty) { }
                try
                {
                    string ConnString2 = salesBookTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn2 = new SqlCeConnection(ConnString2))
                    {
                        using (SqlCeCommand cmd2 = new SqlCeCommand("Select * From SalesBook Where [Serial Number] >= 1 AND [Serial Number] <= 100", conn2))
                        {
                            cmd2.CommandType = CommandType.Text;

                            conn2.Open();
                            SqlCeDataReader reader2 = cmd2.ExecuteReader();

                            using (reader2)
                            {
                                salesBook_dtst.Load(reader2, LoadOption.PreserveChanges, "SalesBook");
                            }
                            reader2.Dispose();
                            cmd2.Dispose();
                        }
                        conn2.Dispose();
                    }
                }
                catch (Exception erty) { }
            }
            else
            {
                try
                {
                    Datatb4 = asql.Go_To_Position(Main.Amatrix.acc, Datatb4, "SalesBook", true, 0, 101);
                }
                catch (Exception ertyy) { Am_err ner = new Am_err(); ner.tx(ertyy.Message); }
            }
        }

        private void getmaxs()
        {
            try
            {
                if (Main.Amatrix.acc == "")
                {
                    SqlCeConnection mySqlConnection = new SqlCeConnection(journalTableAdapter.Connection.ConnectionString);
                    SqlCeCommand mySqlCommand = mySqlConnection.CreateCommand();

                    mySqlCommand.CommandText = "SELECT COUNT([Serial Number]) FROM journal";
                    mySqlConnection.Open();

                    howmany = (int)mySqlCommand.ExecuteScalar();
                    mySqlConnection.Close();

                    //max
                    SqlCeConnection mySqlConnection3 = new SqlCeConnection(journalTableAdapter.Connection.ConnectionString);
                    SqlCeCommand mySqlCommand3 = mySqlConnection3.CreateCommand();

                    mySqlCommand3.CommandText = "SELECT MAX([Serial Number]) FROM journal";
                    mySqlConnection3.Open();

                    maxm = (int)mySqlCommand3.ExecuteScalar();
                    mySqlConnection3.Close();

                    mySqlCommand.Dispose();
                    mySqlCommand3.Dispose();
                    mySqlConnection.Dispose();
                    mySqlConnection3.Dispose();
                }
                else
                {
                    SqlConnection mySqlConnection = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                    mySqlCommand.CommandText = "SELECT COUNT([Serial Number]) FROM journal";
                    mySqlConnection.Open();

                    howmany = (int)mySqlCommand.ExecuteScalar();
                    mySqlConnection.Close();

                    //max
                    SqlConnection mySqlConnection3 = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand mySqlCommand3 = mySqlConnection3.CreateCommand();

                    mySqlCommand3.CommandText = "SELECT MAX([Serial Number]) FROM journal";
                    mySqlConnection3.Open();

                    maxm = (int)mySqlCommand3.ExecuteScalar();
                    mySqlConnection3.Close();

                    mySqlCommand.Dispose();
                    mySqlCommand3.Dispose();
                    mySqlConnection.Dispose();
                    mySqlConnection3.Dispose();
                }
                numbrw.Text = howmany.ToString();
            }
            catch (Exception erty) { }
        }

        private void getmaxs2()
        {
            try
            {
                if (Main.Amatrix.acc == "")
                {
                    SqlCeConnection mySqlConnection = new SqlCeConnection(cashBookTableAdapter.Connection.ConnectionString);
                    SqlCeCommand mySqlCommand = mySqlConnection.CreateCommand();

                    mySqlCommand.CommandText = "SELECT COUNT([Serial Number]) FROM CashBook";
                    mySqlConnection.Open();

                    howmany2 = (int)mySqlCommand.ExecuteScalar();
                    mySqlConnection.Close();

                    //max
                    SqlCeConnection mySqlConnection3 = new SqlCeConnection(cashBookTableAdapter.Connection.ConnectionString);
                    SqlCeCommand mySqlCommand3 = mySqlConnection3.CreateCommand();

                    mySqlCommand3.CommandText = "SELECT MAX([Serial Number]) FROM CashBook";
                    mySqlConnection3.Open();

                    maxm2 = (int)mySqlCommand3.ExecuteScalar();
                    mySqlConnection3.Close();

                    mySqlCommand.Dispose();
                    mySqlCommand3.Dispose();
                    mySqlConnection.Dispose();
                    mySqlConnection3.Dispose();
                }
                else
                {
                    SqlConnection mySqlConnection = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                    mySqlCommand.CommandText = "SELECT COUNT([Serial Number]) FROM CashBook";
                    mySqlConnection.Open();

                    howmany2 = (int)mySqlCommand.ExecuteScalar();
                    mySqlConnection.Close();

                    //max
                    SqlConnection mySqlConnection3 = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand mySqlCommand3 = mySqlConnection3.CreateCommand();

                    mySqlCommand3.CommandText = "SELECT MAX([Serial Number]) FROM CashBook";
                    mySqlConnection3.Open();

                    maxm2 = (int)mySqlCommand3.ExecuteScalar();
                    mySqlConnection3.Close();

                    mySqlCommand.Dispose();
                    mySqlCommand3.Dispose();
                    mySqlConnection.Dispose();
                    mySqlConnection3.Dispose();
                }
                numbrw2.Text = howmany2.ToString();
            }
            catch (Exception erty) { }
        }

        private void getmaxs3()
        {
            try
            {
                if (Main.Amatrix.acc == "")
                {
                    SqlCeConnection mySqlConnection = new SqlCeConnection(purchaseBookTableAdapter.Connection.ConnectionString);
                    SqlCeCommand mySqlCommand = mySqlConnection.CreateCommand();

                    mySqlCommand.CommandText = "SELECT COUNT([Serial Number]) FROM PurchaseBook";
                    mySqlConnection.Open();

                    howmany3 = (int)mySqlCommand.ExecuteScalar();
                    mySqlConnection.Close();

                    //max
                    SqlCeConnection mySqlConnection3 = new SqlCeConnection(purchaseBookTableAdapter.Connection.ConnectionString);
                    SqlCeCommand mySqlCommand3 = mySqlConnection3.CreateCommand();

                    mySqlCommand3.CommandText = "SELECT MAX([Serial Number]) FROM PurchaseBook";
                    mySqlConnection3.Open();

                    maxm3 = (int)mySqlCommand3.ExecuteScalar();
                    mySqlConnection3.Close();

                    mySqlCommand.Dispose();
                    mySqlCommand3.Dispose();
                    mySqlConnection.Dispose();
                    mySqlConnection3.Dispose();
                }
                else
                {
                    SqlConnection mySqlConnection = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                    mySqlCommand.CommandText = "SELECT COUNT([Serial Number]) FROM PurchaseBook";
                    mySqlConnection.Open();

                    howmany3 = (int)mySqlCommand.ExecuteScalar();
                    mySqlConnection.Close();

                    //max
                    SqlConnection mySqlConnection3 = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand mySqlCommand3 = mySqlConnection3.CreateCommand();

                    mySqlCommand3.CommandText = "SELECT MAX([Serial Number]) FROM PurchaseBook";
                    mySqlConnection3.Open();

                    maxm3 = (int)mySqlCommand3.ExecuteScalar();
                    mySqlConnection3.Close();

                    mySqlCommand.Dispose();
                    mySqlCommand3.Dispose();
                    mySqlConnection.Dispose();
                    mySqlConnection3.Dispose();
                }
                numbrw3.Text = howmany3.ToString();
            }
            catch (Exception erty2) { }
        }

        private void getmaxs4()
        {
            try
            {
                if (Main.Amatrix.acc == "")
                {
                    SqlCeConnection mySqlConnection = new SqlCeConnection(salesBookTableAdapter.Connection.ConnectionString);
                    SqlCeCommand mySqlCommand = mySqlConnection.CreateCommand();

                    mySqlCommand.CommandText = "SELECT COUNT([Serial Number]) FROM SalesBook";
                    mySqlConnection.Open();

                    howmany4 = (int)mySqlCommand.ExecuteScalar();
                    mySqlConnection.Close();

                    //max
                    SqlCeConnection mySqlConnection3 = new SqlCeConnection(salesBookTableAdapter.Connection.ConnectionString);
                    SqlCeCommand mySqlCommand3 = mySqlConnection3.CreateCommand();

                    mySqlCommand3.CommandText = "SELECT MAX([Serial Number]) FROM SalesBook";
                    mySqlConnection3.Open();

                    maxm4 = (int)mySqlCommand3.ExecuteScalar();
                    mySqlConnection3.Close();

                    mySqlCommand.Dispose();
                    mySqlCommand3.Dispose();
                    mySqlConnection.Dispose();
                    mySqlConnection3.Dispose();
                }
                else
                {
                    SqlConnection mySqlConnection = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                    mySqlCommand.CommandText = "SELECT COUNT([Serial Number]) FROM SalesBook";
                    mySqlConnection.Open();

                    howmany4 = (int)mySqlCommand.ExecuteScalar();
                    mySqlConnection.Close();

                    //max
                    SqlConnection mySqlConnection3 = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand mySqlCommand3 = mySqlConnection3.CreateCommand();

                    mySqlCommand3.CommandText = "SELECT MAX([Serial Number]) FROM SalesBook";
                    mySqlConnection3.Open();

                    maxm4 = (int)mySqlCommand3.ExecuteScalar();
                    mySqlConnection3.Close();

                    mySqlCommand.Dispose();
                    mySqlCommand3.Dispose();
                    mySqlConnection.Dispose();
                    mySqlConnection3.Dispose();
                }
                numbrw4.Text = howmany4.ToString();
            }
            catch (Exception erty2) { }
        }

        private void bkkinit_DoWork(object sender, DoWorkEventArgs e)
        {
            getmaxs();
        }

        private void bkk_init2_DoWork(object sender, DoWorkEventArgs e)
        {
            getmaxs2();
        }

        private void bkk_init3_DoWork(object sender, DoWorkEventArgs e)
        {
            getmaxs3();
        }

        private void bkk_init4_DoWork(object sender, DoWorkEventArgs e)
        {
            getmaxs4();
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

            if (acc_journ_sett.Default.db_jrn_strt == true)
            {
                try
                {
                    initconn1.RunWorkerAsync();
                    init_conn2.RunWorkerAsync();
                    init_conn3.RunWorkerAsync();
                    init_conn4.RunWorkerAsync();
                    bkkinit.RunWorkerAsync();
                    bkk_init2.RunWorkerAsync();
                    bkk_init3.RunWorkerAsync();
                    bkk_init4.RunWorkerAsync();
                }
                catch (Exception erty) { }
                if (dgv.RowCount == 1 || dgv.RowCount == 0 || dgv2.RowCount == 1 || dgv2.RowCount == 0) { srry = 0; }
                else
                {
                    srry = 1;
                }

                this.Text = "Amatrix Journal"; tsttl.Text = "Journal";
            }
            else { tsttl.Text = tsttl.Text + " (Not Initialized)"; tsttl2.Text = tsttl2.Text + " (Not Initialized)"; }

            if (Properties.Settings.Default.defrgb == 1)
            {
                tswin.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);
                tswin3.BackColor = Color.FromArgb(Properties.Settings.Default.r, Properties.Settings.Default.g, Properties.Settings.Default.b);

                tsttl.ForeColor = Color.FromArgb(Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb);
                tsttl2.ForeColor = Color.FromArgb(Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb, Properties.Settings.Default.txtrgb);
            }
            else if (Properties.Settings.Default.defrgb != 1)
            {
            }

            connlbl.Image = Properties.Resources.connctno;

            cnbnn1.AllowTransparency = true;
            cnbnn1.Opacity = 0.90;
            cmsvwslv.AllowTransparency = true;
            cmsvwslv.Opacity = 0.90;
            cmsvwslv.BackgroundImage = Properties.Resources.app;
            cmsvwslv.BackgroundImageLayout = ImageLayout.Stretch;
            cmsvwslv.ForeColor = Color.WhiteSmoke;

            try
            {
                if (extern_opn == 0)
                {
                    zz_yn(false);
                }
            }
            catch (Exception erty) { }

            cnbnn3.AllowTransparency = true;
            cnbnn3.Opacity = 0.90;
            cmslv.AllowTransparency = true;
            cmslv.Opacity = 0.90;
            clsejourn.Text = "Close";
            this.Disposed += new EventHandler(jrndip);

            try
            {
                if (visual.Default.font == 8.25f)
                { }
                else
                {
                    Font fnt = new Font(dgv.Font.FontFamily.GetName(0), visual.Default.font, FontStyle.Regular);
                    dgv.Font = fnt;
                    dgv.AutoResizeRows();
                    dgv.AutoResizeColumns();
                    dgv2.Font = fnt;
                    dgv2.AutoResizeRows();
                    dgv2.AutoResizeColumns();
                    dgv3.Font = fnt;
                    dgv3.AutoResizeRows();
                    dgv3.AutoResizeColumns();
                    dgv4.Font = fnt;
                    dgv4.AutoResizeRows();
                    dgv4.AutoResizeColumns();
                    visual.Default.font = ftmp;
                    visual.Default.Save();
                }
            }
            catch (Exception erty) { }

            if (acc_journ_sett.Default.IO == false)
            {
                dgv.ReadOnly = false;
                dgv2.ReadOnly = false;
                dgv3.ReadOnly = false;
                dgv4.ReadOnly = false;
                re_only.Visible = false;
            }
            else if (acc_journ_sett.Default.IO == true)
            {
                dgv.ReadOnly = true;
                dgv2.ReadOnly = true;
                dgv3.ReadOnly = true;
                dgv4.ReadOnly = true;
                re_only.Visible = true;
            }
            //init2
            x = dgvwin.Size.Width;
            y = dgvwin.Size.Height;
            x3 = summpnl.Size.Width;
            y3 = summpnl.Size.Height;
        }

        private Double smm1;
        private Double smm2;
        private Double resjrn;
        private void SUMS()
        {
            if (Main.Amatrix.acc == "")
            {
                try
                {
                    smm1 = Convert.ToDouble(journ_dtst.journal.Compute("sum(Credit)", ""));
                }
                catch (Exception erty) { }
                try
                {
                    smm2 = Convert.ToDouble(journ_dtst.journal.Compute("sum(Debit)", ""));
                }
                catch (Exception erty2) { }
                try
                {
                    ttlcrdtjrn.Text = smm1.ToString();
                    ttldbtjrn.Text = smm2.ToString();
                    resjrn = smm2 - smm1;
                    ttljrn.Text = resjrn.ToString();
                    ttl_opp_1.Text = (smm1 - smm2).ToString();
                }
                catch (Exception erty3) { }
            }
            else
            {
                try
                {
                    smm1 = Convert.ToDouble(Datatb.Compute("sum(Credit)", ""));
                }
                catch (Exception erty) { }
                try
                {
                    smm2 = Convert.ToDouble(Datatb.Compute("sum(Debit)", ""));
                }
                catch (Exception erty2) { }
                try
                {
                    ttlcrdtjrn.Text = smm1.ToString();
                    ttldbtjrn.Text = smm2.ToString();
                    resjrn = smm2 - smm1;
                    ttljrn.Text = resjrn.ToString();
                }
                catch (Exception erty3) { }
            }
        }

        private Thread th_sum2;
        private delegate void del_sum2();

        private void th_sum2_strt()
        {
            try
            {
                th_sum2 = new Thread(new ThreadStart(del_sum1_strt));
                th_sum2.IsBackground = true;
                th_sum2.Start();
            }
            catch (Exception erty) { sum_midway(); }
        }

        private void del_sum1_strt()
        {
            try
            {
                this.Invoke(new del_sum2(sum_midway));
            }
            catch (Exception erty) { sum_midway(); }
        }

        private void sum_midway()
        {
            if (acc_journ_sett.Default.dynam_jrn == true)
            {
                SUMS2();
            }
            else { }
        }

        private void SUMS2()
        {
            if (Main.Amatrix.acc == "")
            {
                try
                {
                    smm1 = Convert.ToDouble(cashbook_dtst.CashBook.Compute("sum(Credit)", ""));
                }
                catch (Exception erty) { }

                try
                {
                    smm2 = Convert.ToDouble(cashbook_dtst.CashBook.Compute("sum(Debit)", ""));
                }
                catch (Exception erty2) { }

                try
                {
                    ttlcrd2.Text = smm1.ToString();
                    ttldbt2.Text = smm2.ToString();
                    resjrn = smm2 - smm1;
                    ttl2.Text = resjrn.ToString();
                    ttl_opp_2.Text = (smm1 - smm2).ToString();
                }
                catch (Exception erty3) { }
            }
            else
            {
                try
                {
                    smm1 = Convert.ToDouble(Datatb2.Compute("sum(Credit)", ""));
                }
                catch (Exception erty) { }

                try
                {
                    smm2 = Convert.ToDouble(Datatb2.Compute("sum(Debit)", ""));
                }
                catch (Exception erty2) { }

                try
                {
                    ttlcrd2.Text = smm1.ToString();
                    ttldbt2.Text = smm2.ToString();
                    resjrn = smm2 - smm1;
                    ttl2.Text = resjrn.ToString();
                }
                catch (Exception erty3) { }
            }
        }

        private Thread th_sum3;
        private delegate void del_sum3();

        private void th_sum3_strt()
        {
            if (acc_journ_sett.Default.dynam_jrn == true)
            {
                th_sum3 = new Thread(new ThreadStart(del_sum_strt3));
                th_sum3.IsBackground = true;
                th_sum3.Start();
            }
        }

        private void del_sum_strt3()
        {
            this.Invoke(new del_sum3(SUMS3));
        }

        private void SUMS3()
        {
            if (Main.Amatrix.acc == "")
            {
                try
                {
                    smm1 = Convert.ToDouble(purchaseBook_dtst.PurchaseBook.Compute("sum(Credit)", ""));
                }
                catch (Exception erty) { }

                try
                {
                    smm2 = Convert.ToDouble(purchaseBook_dtst.PurchaseBook.Compute("sum(Debit)", ""));
                }
                catch (Exception erty2) { }

                try
                {
                    ttlcrd3.Text = smm1.ToString();
                    ttldbt3.Text = smm2.ToString();
                    resjrn = smm2 - smm1;
                    ttl3.Text = resjrn.ToString();
                    ttl_opp_3.Text = (smm1 - smm2).ToString();
                }
                catch (Exception erty3) { }
            }
            else
            {
                try
                {
                    smm1 = Convert.ToDouble(Datatb3.Compute("sum(Credit)", ""));
                }
                catch (Exception erty) { }

                try
                {
                    smm2 = Convert.ToDouble(Datatb3.Compute("sum(Debit)", ""));
                }
                catch (Exception erty2) { }

                try
                {
                    ttlcrd3.Text = smm1.ToString();
                    ttldbt3.Text = smm2.ToString();
                    resjrn = smm2 - smm1;
                    ttl3.Text = resjrn.ToString();
                }
                catch (Exception erty3) { }
            }
        }

        private void conn()
        {
            if (Main.Amatrix.acc == "")
            {
                if (journalTableAdapter.Connection.State == ConnectionState.Open/* && journ_CSHbkkTableAdapter.Connection.State == ConnectionState.Open && journ_SLSbkkTableAdapter.Connection.State == ConnectionState.Open && journ_SLSbkk_ClientnfoTableAdapter.Connection.State == ConnectionState.Open && journ_PURbkkTableAdapter.Connection.State == ConnectionState.Open && journ_PURbkknfoTableAdapter.Connection.State == ConnectionState.Open*/)
                {
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Connected";
                }
                else if (journalTableAdapter.Connection.State == ConnectionState.Closed/* && journ_CSHbkkTableAdapter.Connection.State == ConnectionState.Closed && journ_SLSbkkTableAdapter.Connection.State == ConnectionState.Closed && journ_SLSbkk_ClientnfoTableAdapter.Connection.State == ConnectionState.Closed && journ_PURbkkTableAdapter.Connection.State == ConnectionState.Closed && journ_PURbkknfoTableAdapter.Connection.State == ConnectionState.Closed*/)
                {
                    connlbl.Text = "Not Connected";
                    connlbl.Image = Properties.Resources.connctno;
                }
                else
                {
                    connlbl.Text = "Connectivity Error (Reconnect Please)"; connlbl.Image = Properties.Resources.conncerr;
                }
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
                catch (Exception erty)
                {
                    connlbl.Image = Properties.Resources.conncted;
                    connlbl.Text = "Connection Not Available";
                }
            }
        }

        void jrndip(object sender, EventArgs e)
        {
            Properties.Settings.Default.journ_fleloc = "";
            Properties.Settings.Default.Journmod = "";
            Properties.Settings.Default.Save();
        }

        private void acc_journ_Load(object sender, EventArgs e)
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
                this.Close();
                this.Dispose(true);
                GC.Collect();
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
            rewr.rerom gug = new rewr.rerom();
            string nwloc = gug.crcopy(wcre, "journ");
            string huh = gug.jrnrder(nwloc);
            Properties.Settings.Default.journrecovwht = huh;
            Properties.Settings.Default.journrecovwhre = wcre;
            gug.recov(huh, wcre);
            File.Delete(nwloc);
            Properties.Settings.Default.Journmod = huh;
            Properties.Settings.Default.Save();
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

        //Save to file
        private void saveasfok(object sender, CancelEventArgs e)
        {
            try
            {
                if (saveas.FilterIndex == 1)
                {
                    FileStream fssvasjrn = new FileStream(saveas.FileName.ToString() + ".journ", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter srsvasjrn = new StreamWriter(fssvasjrn);
                    srsvasjrn.Write(Properties.Settings.Default.Journmod.ToString());
                    srsvasjrn.Flush();
                    fssvasjrn.Flush();
                    srsvasjrn.Close();
                    fssvasjrn.Close();
                    Properties.Settings.Default.journ_fleloc = saveas.FileName.ToString();
                    this.Text = "Amatrix Journal : " + saveas.FileName.ToString();
                    //cols();
                    gtedat(saveas.FileName.ToString()+".journ");
                }
                else if (saveas.FilterIndex == 0)
                {
                    FileStream fssvasjrn = new FileStream(saveas.FileName.ToString() + ".amds", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter srsvasjrn = new StreamWriter(fssvasjrn);
                    srsvasjrn.Write(Properties.Settings.Default.Journmod.ToString());
                    srsvasjrn.Flush();
                    fssvasjrn.Flush();
                    srsvasjrn.Close();
                    fssvasjrn.Close(); 
                    Properties.Settings.Default.journ_fleloc = saveas.FileName.ToString();
                    this.Text = "Amatrix Journal : " + saveas.FileName.ToString();
                    //cols();
                    gtedat(saveas.FileName.ToString()+".amds");
                }
                else
                {
                    Am_err errnofleloc = new Am_err();
                    errnofleloc.tx("An error occurred while writing to the file '" + saveas.FileName + "'");
                }

            }
            catch (Exception excnosavas) { }
        }

        private void svedskclc(object sender, EventArgs e)
        {
            sveacc.ShowDialog();
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
            toolStripButton13.Enabled = false;
            calculateValueAfterVatAndCSTToolStripMenuItem.Enabled = false;
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
            acc_journ njrn = new acc_journ();
            njrn.Show();
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
            catch (Exception exct)
            { }
            summpnl.BringToFront();

            if (tbcol.SelectedIndex == 0)
            {
                try
                {
                    winstatus = 2;
                    winwin = 2;
                    dgvwintic.Stop();
                    dgvwintic3.Stop();
                    tmex.Stop();
                    tmex3.Stop();
                }
                catch (Exception exct)
                { }
                calculateValueAfterVatAndCSTToolStripMenuItem.Enabled = false;
            }
            else if (tbcol.SelectedIndex == 1)
            {
                try
                {
                    winstatus = 4;
                    winwin = 4;
                    dgvwintic.Stop();
                    dgvwintic3.Stop();
                    tmex.Stop();
                    tmex3.Stop();
                }
                catch (Exception exct)
                { }
                calculateValueAfterVatAndCSTToolStripMenuItem.Enabled = true;
            }
            else if (tbcol.SelectedIndex == 2)
            {
                try
                {
                    winstatus = 5;
                    winwin = 5;
                    dgvwintic.Stop();
                    dgvwintic3.Stop();
                    tmex.Stop();
                    tmex3.Stop();
                }
                catch (Exception exct) { }
                calculateValueAfterVatAndCSTToolStripMenuItem.Enabled = true;
            }
            gadg_resz1.Visible = false;
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
            Main.acc_journ nm = new acc_journ();
            nm.Show();
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

        private void sall_Click(object sender, EventArgs e)
        {
            if (winstatus == 3)
            {
                dgv.SelectAll();
            }
            if (winstatus == 2)
            {
                dgv2.SelectAll();
            }
            if (winstatus == 4)
            {
                dgv3.SelectAll();
            }
            if (winstatus == 5)
            {
                dgv4.SelectAll();
            }
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

        private void show_AllToolStripButton_Click_1(object sender, EventArgs e)
        {
            oper_save();
            if (sender.Equals(showAllToolStripMenuItem1) == true)
            {
                Showalljourn();
            }
            if (sender.Equals(shw_here) == true || sender.Equals(shw_here2) == true || sender.Equals(nopeascdesc) == true)
            {
                Last_Query_Used = "Select * From journal Where [Serial Number] >= '" + smallest + "' AND [Serial Number] <= '" + biggest + "'";
                if (Main.Amatrix.acc == "")
                {
                    try
                    {
                        journ_dtst.Clear();
                        string ConnString = journalTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                    dgv.DataSource = journ_dtst.journal;
                                }
                            }
                        }
                    }
                    catch (Exception erty) { }
                }
                else
                {
                    Datatb = asql.Go_To_Position(Main.Amatrix.acc, Datatb, "journal", true, smallest, biggest);
                    dgv.DataSource = Datatb;
                }
            }
            //dgv2
            if (sender.Equals(shwhere2) == true || sender.Equals(toolStripMenuItem112) == true)
            {
                Last_Query_Used_cb = "Select * From CashBook Where [Serial Number] >= '" + smallest2 + "' AND [Serial Number] <= '" + biggest2 + "'";
                if (Main.Amatrix.acc == "")
                {
                    try
                    {
                        cashbook_dtst.Clear();
                        string ConnString = cashBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_cb, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    cashbook_dtst.Load(reader, LoadOption.PreserveChanges, "CashBook");
                                    dgv2.DataSource = cashbook_dtst.CashBook;
                                }
                            }
                        }
                    }
                    catch (Exception erty) { }
                }
                else
                {
                    Datatb2 = asql.Go_To_Position(Main.Amatrix.acc, Datatb2, "CashBook", true, smallest2, biggest2);
                    dgv2.DataSource = Datatb2;
                }
            }
            if (sender.Equals(showall2) == true)
            {
                Last_Query_Used_cb = "Select * From CashBook";
                if (Main.Amatrix.acc == "")
                {
                    try
                    {
                        cashbook_dtst.Clear();
                        string ConnString = cashBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_cb, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    cashbook_dtst.Load(reader, LoadOption.PreserveChanges, "CashBook");
                                    dgv2.DataSource = cashbook_dtst.CashBook;
                                }
                            }
                        }
                    }
                    catch (Exception erty) { }
                }
                else
                {
                    Datatb2 = asql.Get_all(Main.Amatrix.acc, Datatb2, "CashBook", false);
                    dgv2.DataSource = Datatb2;
                }
            }
            //dgv3
            if (sender.Equals(shwhere3) == true || sender.Equals(toolStripMenuItem259) == true)
            {
                Last_Query_Used_pb = "Select * From PurchaseBook Where [Serial Number] >= '" + smallest3 + "' AND [Serial Number] <= '" + biggest3 + "'";
                if (Main.Amatrix.acc == "")
                {
                    try
                    {
                        purchaseBook_dtst.Clear();
                        string ConnString = purchaseBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_pb, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    purchaseBook_dtst.Load(reader, LoadOption.PreserveChanges, "PurchaseBook");
                                    dgv3.DataSource = purchaseBook_dtst.PurchaseBook;
                                }
                            }
                        }
                    }
                    catch (Exception erty) { }
                }
                else
                {
                    Datatb3 = asql.Go_To_Position(Main.Amatrix.acc, Datatb3, "PurchaseBook", true, smallest3, biggest3);
                    dgv3.DataSource = Datatb3;
                }
            }
            if (sender.Equals(showall3) == true)
            {
                Last_Query_Used_pb = "Select * From PurchaseBook";
                if (Main.Amatrix.acc == "")
                {
                    try
                    {
                        purchaseBook_dtst.Clear();
                        string ConnString = purchaseBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_pb, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    purchaseBook_dtst.Load(reader, LoadOption.PreserveChanges, "PurchaseBook");
                                    dgv3.DataSource = purchaseBook_dtst.PurchaseBook;
                                }
                            }
                        }
                    }
                    catch (Exception erty) { }
                }
                else
                {
                    Datatb3 = asql.Get_all(Main.Amatrix.acc, Datatb3, "PurchaseBook", false);
                    dgv3.DataSource = Datatb3;
                }
            }
            //dgv4
            if (sender.Equals(toolStripButton127) == true || sender.Equals(toolStripMenuItem385) == true)
            {
                Last_Query_Used_sb = "Select * From SalesBook Where [Serial Number] >= '" + smallest4 + "' AND [Serial Number] <= '" + biggest4 + "'";
                if (Main.Amatrix.acc == "")
                {
                    try
                    {
                        salesBook_dtst.Clear();
                        string ConnString = salesBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_sb, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    salesBook_dtst.Load(reader, LoadOption.PreserveChanges, "SalesBook");
                                    dgv4.DataSource = salesBook_dtst.SalesBook;
                                }
                            }
                        }
                    }
                    catch (Exception erty) { }
                }
                else
                {
                    Datatb4 = asql.Go_To_Position(Main.Amatrix.acc, Datatb4, "SalesBook", false, smallest4, biggest4);
                    dgv4.DataSource = Datatb4;
                }
            }
            if (sender.Equals(toolStripMenuItem386) == true)
            {
                Last_Query_Used_sb = "Select * From SalesBook";
                if (Main.Amatrix.acc == "")
                {
                    try
                    {
                        salesBook_dtst.Clear();
                        string ConnString = salesBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_sb, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    salesBook_dtst.Load(reader, LoadOption.PreserveChanges, "SalesBook");
                                    dgv4.DataSource = salesBook_dtst.SalesBook;
                                }
                            }
                        }
                    }
                    catch (Exception erty) { }
                }
                else
                {
                    Datatb4 = asql.Get_all(Main.Amatrix.acc, Datatb4, "SalesBook", true);
                    dgv4.DataSource = Datatb4;
                }
            }
        }

        private void Showalljourn()
        {
            oper_save();
            Last_Query_Used = "Select * FROM journal";
            if (Main.Amatrix.acc == "")
            {
                journ_dtst.Clear();
                string ConnString = journalTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                            dgv.DataSource = journ_dtst.journal;
                        }
                    }
                }
            }
            else
            {
                Datatb = asql.Get_all(Main.Amatrix.acc, Datatb, "journal", true);
                dgv.DataSource = Datatb;
            }
        }

        private void Showallcash()
        {
            oper_save();
            Last_Query_Used_cb = "Select * FROM CashBook";
            if (Main.Amatrix.acc == "")
            {
                cashbook_dtst.Clear();
                string ConnString = cashBookTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_cb, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            cashbook_dtst.Load(reader, LoadOption.PreserveChanges, "CashBook");
                            dgv2.DataSource = cashbook_dtst.CashBook;
                        }
                    }
                }
            }
            else
            {
                Datatb2 = asql.Get_all(Main.Amatrix.acc, Datatb2, "CashBook", true);
                dgv2.DataSource = Datatb2;
            }
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

            if (tbcol.SelectedIndex == 0)
            {
                try
                {
                    winstatus = 2;
                    winwin = 2;
                    dgvwintic.Stop();
                    dgvwintic3.Stop();
                    tmex.Stop();
                    tmex3.Stop();
                }
                catch (Exception exct)
                { }
                calculateValueAfterVatAndCSTToolStripMenuItem.Enabled = false;
            }
            else if (tbcol.SelectedIndex == 1)
            {
                try
                {
                    winstatus = 4;
                    winwin = 4;
                    dgvwintic.Stop();
                    dgvwintic3.Stop();
                    tmex.Stop();
                    tmex3.Stop();
                }
                catch (Exception exct)
                { }
                calculateValueAfterVatAndCSTToolStripMenuItem.Enabled = true;
            }
            else if (tbcol.SelectedIndex == 2)
            {
                try
                {
                    winstatus = 5;
                    winwin = 5;
                    dgvwintic.Stop();
                    dgvwintic3.Stop();
                    tmex.Stop();
                    tmex3.Stop();
                }
                catch (Exception exct) { }
                calculateValueAfterVatAndCSTToolStripMenuItem.Enabled = true;
            }
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

        private Thread th_save;
        private delegate void delsave();
        private void svebtn_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                //save_all();
                th_save = new Thread(new ThreadStart(del_sve));
                th_save.IsBackground = true;
                th_save.Start(); 
            }
            catch (Exception erty) { save_all(); general_mssg("Saved Using Native Saver", "Save"); }
        }

        private void del_sve()
        {
            try
            {
                this.Invoke(new delsave(save_all));
            }
            catch (Exception erty) { save_all(); general_mssg("Saved Using Native Saver", "Save"); }
        }

        private void save_all()
        {
            if (Main.Amatrix.acc == "")
            {
                journalBindingSource.EndEdit();
                salesBookBindingSource.EndEdit();
                cashBookBindingSource.EndEdit();
                purchaseBookBindingSource.EndEdit();

                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dgv2.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dgv3.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dgv4.CommitEdit(DataGridViewDataErrorContexts.Commit);
                try
                {
                    journalTableAdapter.Update(journ_dtst);
                }
                catch (Exception erty) { general_mssg("An Error Occured While Saving Journal to the DataBase, Journal has Not been Saved", "Save"); }

                try
                {
                    cashBookTableAdapter.Update(cashbook_dtst);
                }
                catch (Exception erty) { general_mssg("An Error Occured While Saving Cash Book to the DataBase, Cash Book has Not been Saved", "Save"); }

                try
                {
                    purchaseBookTableAdapter.Update(purchaseBook_dtst);
                }
                catch (Exception erty) { general_mssg("An Error Occured While Saving Purchase Book to the DataBase, Purchase Book has Not been Saved", "Save"); }

                try
                {
                    salesBookTableAdapter.Update(salesBook_dtst);
                }
                catch (Exception erty) { general_mssg("An Error Occured While Saving Purchase Book to the DataBase, Purchase Book has Not been Saved", "Save"); }
            }
            else
            {
                try
                {
                    asql.Save(Datatb, "journal", Main.Amatrix.acc);
                    asql.Save(Datatb2, "CashBook", Main.Amatrix.acc);
                    asql.Save(Datatb3, "PurchaseBook", Main.Amatrix.acc);
                    asql.Save(Datatb4, "SalesBook", Main.Amatrix.acc);
                    try
                    {
                        Main.Amatrix.ascl.broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><typ>w</typ><val>0</val><app>" + this.Name + "</app><par>[" + ts1.Name + "]</par><con>rfrsh_dta</con>");
                    }
                    catch (Exception erty) { general_mssg("Syncronization is not Set Up", "Sync Error"); }
                }
                catch (Exception erty) { general_mssg("An Error Occured While Saving Purchase Book to the DataBase, Purchase Book has Not been Saved", "Save"); }
            }
            try
            {
                bkkinit.RunWorkerAsync();
            }
            catch (Exception erty) { }
            try
            {
                bkk_init2.RunWorkerAsync();
            }
            catch (Exception ertty) { }
            try
            {
                bkk_init3.RunWorkerAsync();
            }
            catch (Exception erty) { }
            try
            {
                bkk_init4.RunWorkerAsync();
            }
            catch (Exception erty) { }
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
            if (Main.Amatrix.acc == "")
            {
                connlbl.Text = "Disconnecting..";
                connlbl.Image = Properties.Resources.conncting;
                journalTableAdapter.Connection.Close();
                cashBookTableAdapter.Connection.Close();
                purchaseBookTableAdapter.Connection.Close();
                salesBookTableAdapter.Connection.Close();
                conn();
            }
        }

        private void tmeinit_Tick(object sender, EventArgs e)
        {
            tmeinit.Stop();
            try
            {
                thinitstrt();
            }
            catch (Exception erty) { Init(); }
            thinit2strt();
        }

        private void enblhc_Click_1(object sender, EventArgs e)
        {
            foreach (Control cn in this.Controls)
            {
                cn.ForeColor = Color.White;
                cn.BackColor = Color.Black;
            }
        }

        private void abtmnu_Click(object sender, EventArgs e)
        {
            app_abt bt = new app_abt();
            bt.descr("Amatrix Journal");
            bt.Show();
        }

        private void ascvw_Click(object sender, EventArgs e)
        {
            if (winstatus == 3)
            {
                dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);
            }
            if (winstatus == 2)
            {
                dgv2.Sort(dgv2.Columns[0], ListSortDirection.Ascending);
            }
            if (winstatus == 4)
            {
                dgv3.Sort(dgv3.Columns[0], ListSortDirection.Ascending);
            }
            if (winstatus == 5)
            {
                dgv4.Sort(dgv4.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void descvw_Click(object sender, EventArgs e)
        {
            if (winstatus == 3)
            {
                dgv.Sort(dgv.Columns[0], ListSortDirection.Descending);
            } 
            if (winstatus == 2)
            {
                dgv2.Sort(dgv2.Columns[0], ListSortDirection.Descending);
            }
            if (winstatus == 4)
            {
                dgv3.Sort(dgv3.Columns[0], ListSortDirection.Descending);
            }
            if (winstatus == 5)
            {
                dgv4.Sort(dgv4.Columns[0], ListSortDirection.Descending);
            }
        }

        private void rsdbord_Click(object sender, EventArgs e)
        {

            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Raised;

            dgv2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv2.CellBorderStyle = DataGridViewCellBorderStyle.Raised;

            dgv3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv3.CellBorderStyle = DataGridViewCellBorderStyle.Raised;

            dgv4.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv4.CellBorderStyle = DataGridViewCellBorderStyle.Raised;

            acc_journ_sett.Default.dgvborder = 1;
            acc_journ_sett.Default.Save();
        }

        private void dflt_dgvbord_Click(object sender, EventArgs e)
        {

            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            dgv2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv2.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            dgv4.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv4.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            dgv3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv3.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            acc_journ_sett.Default.dgvborder = 0;
            acc_journ_sett.Default.Save();
        }

        private void colreordr_Click(object sender, EventArgs e)
        {
            dgv.AllowUserToOrderColumns = true;
            dgv2.AllowUserToOrderColumns = true;
            dgv3.AllowUserToOrderColumns = true;
            dgv4.AllowUserToOrderColumns = true;
        }

        private void colreordflse_Click(object sender, EventArgs e)
        {
            dgv.AllowUserToOrderColumns = false;
            dgv2.AllowUserToOrderColumns = false;
            dgv3.AllowUserToOrderColumns = false;
            dgv4.AllowUserToOrderColumns = true;
        }

        private void delitm_Click(object sender, EventArgs e)
        {
            try
            {
                journ_dtst.journal.Rows.RemoveAt(dgv.CurrentRow.Index);
            }
            catch (Exception erty)
            {
                dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SUMS();
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (sender.Equals(dgv) == true)
                {
                    if (Convert.ToInt32(dgv[e.ColumnIndex, e.RowIndex].Value) == 0)
                    {
                        maxm = maxm + 1;
                        dgv.Rows[e.RowIndex].Cells[0].Value = maxm;
                    }
                    if (acc_journ_sett.Default.dynam_jrn == true)
                    {
                        thsum1strt();
                    }
                    if (e.ColumnIndex == 11 || e.ColumnIndex == 12)
                    {
                        dgv[13, e.RowIndex].Value = (Convert.ToDouble(dgv[11, e.RowIndex].Value) - Convert.ToDouble(dgv[12, e.RowIndex].Value)).ToString();
                        //dgv[13, e.RowIndex].Value = (Convert.ToDouble(dgv[12, e.RowIndex].Value) - Convert.ToDouble(dgv[11, e.RowIndex].Value)).ToString();
                    }
                }
                if (sender.Equals(dgv2) == true)
                {
                    if (Convert.ToInt32(dgv2[e.ColumnIndex, e.RowIndex].Value) == 0)
                    {
                        maxm2 = maxm2 + 1;
                        dgv2.Rows[e.RowIndex].Cells[0].Value = maxm2;
                    }
                    if (acc_journ_sett.Default.dynam_jrn == true)
                    {
                        th_sum2_strt();
                    }
                    if (e.ColumnIndex == 11 || e.ColumnIndex == 12)
                    {
                        dgv2[13, e.RowIndex].Value = (Convert.ToDouble(dgv2[12, e.RowIndex].Value) - Convert.ToDouble(dgv2[11, e.RowIndex].Value)).ToString();
                    
                        //dgv2[13, e.RowIndex].Value = (Convert.ToDouble(dgv2[11, e.RowIndex].Value) - Convert.ToDouble(dgv2[12, e.RowIndex].Value)).ToString();
                    }
                }
                if (sender.Equals(dgv3) == true)
                {
                    if (Convert.ToInt32(dgv3[e.ColumnIndex, e.RowIndex].Value) == 0)
                    {
                        maxm3 = maxm3 + 1;
                        dgv3.Rows[e.RowIndex].Cells[0].Value = maxm3;
                    }
                    if (acc_journ_sett.Default.dynam_jrn == true)
                    {
                        th_sum3_strt();
                    }
                    if (e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 10)
                    {
                        if (dgv3[9, e.RowIndex].Value == DBNull.Value || dgv3[10, e.RowIndex].Value == DBNull.Value || Convert.ToInt32(dgv3[9, e.RowIndex].Value) == 0 || Convert.ToInt32(dgv3[10, e.RowIndex].Value) == 0)
                        {
                            dgv3[11, e.RowIndex].Value = (Convert.ToDouble(Convert.ToDouble(dgv3[8, e.RowIndex].Value)) - Convert.ToDouble(dgv3[7, e.RowIndex].Value)).ToString();
                            //dgv3[11, e.RowIndex].Value = (Convert.ToDouble(dgv3[7, e.RowIndex].Value) - Convert.ToDouble(dgv3[8, e.RowIndex].Value)).ToString();
                        }
                    }
                }
                if (sender.Equals(dgv4) == true)
                {
                    if (Convert.ToInt32(dgv4[e.ColumnIndex, e.RowIndex].Value) == 0)
                    {
                        maxm4 = maxm4 + 1;
                        dgv4.Rows[e.RowIndex].Cells[0].Value = maxm4;
                    }
                    if (acc_journ_sett.Default.dynam_jrn == true)
                    {
                        thsum4_strt();
                    }
                    if (e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 10)
                    {
                        if (dgv4[9, e.RowIndex].Value == DBNull.Value || dgv4[10, e.RowIndex].Value == DBNull.Value || Convert.ToInt32(dgv4[9, e.RowIndex].Value) == 0 || Convert.ToInt32(dgv4[10, e.RowIndex].Value) == 0)
                        {
                            dgv4[11, e.RowIndex].Value = (dgv4[8, e.RowIndex].Value).ToString();
                        }
                    }
                }
            }
            catch (Exception erty) { }
        }

        private Thread thsum4;
        private delegate void del_sum_4();

        private void thsum4_strt()
        {
            try
            {
                thsum4 = new Thread(new ThreadStart(del_sum4_strt));
                thsum4.IsBackground = true;
                thsum4.Start();
            }
            catch (Exception erty) { SUMS4(); }
        }

        private void del_sum4_strt()
        {
            try
            {
                this.Invoke(new del_sum_4(SUMS4));
            }
            catch (Exception erty) { SUMS4(); }
        }

        private void SUMS4()
        {
            if (Main.Amatrix.acc == "")
            {
                try
                {
                    smm1 = Convert.ToDouble(salesBook_dtst.SalesBook.Compute("sum(Credit)", ""));
                }
                catch (Exception erty) { }

                try
                {
                    smm2 = Convert.ToDouble(salesBook_dtst.SalesBook.Compute("sum(Debit)", ""));
                }
                catch (Exception erty2) { }
            }
            else
            {
                try
                {
                    smm1 = Convert.ToDouble(Datatb4.Compute("sum(Credit)", ""));
                }
                catch (Exception erty) { }
                try
                {
                    smm2 = Convert.ToDouble(Datatb4.Compute("sum(Debit)", ""));
                }
                catch (Exception erty1) { }
            }

            try
            {
                ttlcrd4.Text = smm1.ToString();
                ttldbt4.Text = smm2.ToString();
                resjrn = smm2 - smm1;
                ttl4.Text = resjrn.ToString();
            }
            catch (Exception erty3) { }
        }

        private Thread thsum1;
        private delegate void delsum1();

        private void thsum1strt()
        {
            thsum1 = new Thread(new ThreadStart(delsum1strt));
            thsum1.IsBackground = true;
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

        private void cshshwbycurrmnth_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void shw_ALLToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void show_AllToolStripButton_Click(object sender, EventArgs e)
        {
            oper_save();
            if (Main.Amatrix.acc == "")
            {
                dgv.DataSource = journ_dtst.journal;
                try
                {
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            else
            {
                dgv.DataSource = Datatb;
            }
        }

        private void dbtsumqry_Click(object sender, EventArgs e)
        {
            oper_save();
            if (Main.Amatrix.acc == "")
            {
                if (sender.Equals(dbtsumqry) == true)
                {
                    journ_dtst.Clear();
                    Last_Query_Used = "Select sum(Debit) Debit, sum(Credit) Credit, sum([Serial Number]) [Serial Number] FROM journal";
                    string ConnString = journalTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                dgv.DataSource = journ_dtst.journal;
                            }
                        }
                    }
                }
                if (sender.Equals(toolStripButton4) == true)
                {
                    journ_dtst.Clear();
                    string ConnString = journalTableAdapter.Connection.ConnectionString;
                    Last_Query_Used = "Select sum(Debit) Debit, sum(Credit) Credit, sum([Serial Number]) [Serial Number] FROM journal WHERE [Date of Transaction] > '" + toolStripTextBox195.Text + "' AND [Date of Transaction] < '" + toolStripTextBox196.Text + "'";
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                dgv.DataSource = journ_dtst.journal;
                            }
                        }
                    }
                }
            }
            else
            {
                if (sender.Equals(dbtsumqry) == true)
                {
                    Datatb.Clear();
                    string ConnString = Main.Amatrix.acc;
                    Last_Query_Used = "Select sum(Debit) Debit, sum(Credit) Credit, sum([Serial Number]) [Serial Number] FROM journal";
                    using (SqlConnection conn = new SqlConnection(ConnString))
                    {
                        using (SqlCommand cmd = new SqlCommand(Last_Query_Used, conn))
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                Datatb.Load(reader);
                                dgv.DataSource = Datatb;
                            }
                        }
                    }
                }
                if (sender.Equals(toolStripButton4) == true)
                {
                    Datatb.Clear();
                    string ConnString = Main.Amatrix.acc;
                    Last_Query_Used = "Select sum(Debit) Debit, sum(Credit) Credit, sum([Serial Number]) [Serial Number] FROM journal WHERE [Date of Transaction] > '" + toolStripTextBox195.Text + "' AND [Date of Transaction] < '" + toolStripTextBox196.Text + "'";
                    using (SqlConnection conn = new SqlConnection(ConnString))
                    {
                        using (SqlCommand cmd = new SqlCommand(Last_Query_Used, conn))
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                Datatb.Load(reader);
                                dgv.DataSource = Datatb;
                            }
                        }
                    }
                }
            }
        }

        private void avgqryjourn_Click(object sender, EventArgs e)
        {
            oper_save();
            if (Main.Amatrix.acc == "")
            {
                journ_dtst.Clear();
                if (sender.Equals(avgqryjourn) == true)
                {
                    string ConnString = journalTableAdapter.Connection.ConnectionString;
                    Last_Query_Used = "Select avg(Debit) Debit, avg(Credit) Credit, avg([Serial Number]) [Serial Number] FROM journal";
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();//)

                            using (reader)
                            {
                                journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                dgv.DataSource = journ_dtst.journal;
                            }
                        }
                    }
                }
                if (sender.Equals(toolStripButton267) == true)
                {
                    string ConnString = journalTableAdapter.Connection.ConnectionString;
                    Last_Query_Used = "Select avg(Debit) Debit, avg(Credit) Credit, avg([Serial Number]) [Serial Number] FROM journal WHERE [Date of Transaction] > '" + toolStripTextBox197.Text + "' AND [Date of Transaction] < '" + toolStripTextBox198.Text + "'";
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();//)

                            using (reader)
                            {
                                journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                dgv.DataSource = journ_dtst.journal;
                            }
                        }
                    }
                }
            }
            else
            {
                Datatb.Clear();
                if (sender.Equals(avgqryjourn) == true)
                {
                    string ConnString = Main.Amatrix.acc;
                    Last_Query_Used = "Select avg(Debit) Debit, avg(Credit) Credit, avg([Serial Number]) [Serial Number] FROM journal";
                    using (SqlConnection conn = new SqlConnection(ConnString))
                    {
                        using (SqlCommand cmd = new SqlCommand(Last_Query_Used, conn))
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                Datatb.Load(reader);
                                dgv.DataSource = Datatb;
                            }
                        }
                    }
                }
                if (sender.Equals(toolStripButton267) == true)
                {
                    string ConnString = Main.Amatrix.acc;
                    Last_Query_Used = "Select avg(Debit) Debit, avg(Credit) Credit, avg([Serial Number]) [Serial Number] FROM journal WHERE [Date of Transaction] > '" + toolStripTextBox197.Text + "' AND [Date of Transaction] < '" + toolStripTextBox198.Text + "'";
                    using (SqlConnection conn = new SqlConnection(ConnString))
                    {
                        using (SqlCommand cmd = new SqlCommand(Last_Query_Used, conn))
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                Datatb.Load(reader);
                                dgv.DataSource = Datatb;
                            }
                        }
                    }
                }
            }
        }

        private void oper_save()
        {
            if (acc_journ_sett.Default.dynam_jrn == true)
            {
                if (Main.Amatrix.acc == "")
                {
                    if (winstatus == 3)
                    {
                        journalTableAdapter.Update(journ_dtst);
                    }
                    if (winstatus == 2)
                    {
                        cashBookTableAdapter.Update(cashbook_dtst);
                    }
                    if (winstatus == 4)
                    {
                        purchaseBookTableAdapter.Update(purchaseBook_dtst);
                    }
                    if (winstatus == 5)
                    {
                        salesBookTableAdapter.Update(salesBook_dtst);
                    }
                }
                else
                {
                    EventArgs e = new EventArgs();
                    svebtn_ButtonClick(svebtn, e);
                }
            }
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
                    dgv[x, y].Value = aund[aund.Count - 1];
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
            if (winstatus == 2)
            {
                try
                {
                    int x, y;
                    x = Convert.ToInt32(aundC2[aundC2.Count - 1]);
                    y = Convert.ToInt32(aundR2[aundR2.Count - 1]);
                    dgv2[x, y].Value = aund2[aund2.Count - 1];
                    aund2.RemoveAt(aund2.Count - 1);
                    aundC2.RemoveAt(aundC2.Count - 1);
                    aundR2.RemoveAt(aundR2.Count - 1);
                }
                catch (Exception erty)
                {
                    try
                    {
                        aund2.RemoveAt(aund2.Count - 1);
                        aundC2.RemoveAt(aundC2.Count - 1);
                        aundR2.RemoveAt(aundR2.Count - 1);
                    }
                    catch (Exception ett) { }
                }
            }
            if (winstatus == 4)
            {
                try
                {
                    int x, y;
                    x = Convert.ToInt32(aundC3[aundC3.Count - 1]);
                    y = Convert.ToInt32(aundR3[aundR3.Count - 1]);
                    dgv3[x, y].Value = aund3[aund3.Count - 1];
                    aund3.RemoveAt(aund3.Count - 1);
                    aundC3.RemoveAt(aundC3.Count - 1);
                    aundR3.RemoveAt(aundR3.Count - 1);
                }
                catch (Exception erty)
                {
                    try
                    {
                        aund3.RemoveAt(aund3.Count - 1);
                        aundC3.RemoveAt(aundC3.Count - 1);
                        aundR3.RemoveAt(aundR3.Count - 1);
                    }
                    catch (Exception ett) { }
                }
            }
            if (winstatus == 5)
            {
                try
                {
                    int x, y;
                    x = Convert.ToInt32(aundC4[aundC4.Count - 1]);
                    y = Convert.ToInt32(aundR4[aundR4.Count - 1]);
                    dgv4[x, y].Value = aund4[aund4.Count - 1];
                    aund4.RemoveAt(aund4.Count - 1);
                    aundC4.RemoveAt(aundC4.Count - 1);
                    aundR4.RemoveAt(aundR4.Count - 1);
                }
                catch (Exception erty)
                {
                    try
                    {
                        aund4.RemoveAt(aund4.Count - 1);
                        aundC4.RemoveAt(aundC4.Count - 1);
                        aundR4.RemoveAt(aundR4.Count - 1);
                    }
                    catch (Exception ett) { }
                }
            }
        }

        private void redoall_Click(object sender, EventArgs e)
        {
            if (winstatus == 3)
            {
                try
                {
                    aund.Add(00);
                    aundC.Add(x);
                    aundR.Add(y);
                }
                catch (Exception erty)
                {
                }
            }
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (winstatus == 3)
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
            if (winstatus == 2)
            {
                //Arraylist addition
                aund2.Add(dgv2[e.ColumnIndex, e.RowIndex].Value);
                aundC2.Add(e.ColumnIndex);
                aundR2.Add(e.RowIndex);

                if (dgv2.Rows[e.RowIndex].Cells[0].Value == DBNull.Value || dgv2.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    maxm2 = maxm2 + 1;
                    if (maxm == 1 && dgv2[0, 0].Value.ToString() == "1")
                    {
                        maxm2 = maxm2 + 1;
                        dgv2.Rows[e.RowIndex].Cells[0].Value = maxm2;
                    }
                    else
                    {
                        dgv2.Rows[e.RowIndex].Cells[0].Value = maxm2;
                    }
                    howmany2 = howmany2 + 1;
                }
            }
            if (winstatus == 4)
            {                
                //Arraylist addition
                aund3.Add(dgv3[e.ColumnIndex, e.RowIndex].Value);
                aundC3.Add(e.ColumnIndex);
                aundR3.Add(e.RowIndex);

                if (dgv3.Rows[e.RowIndex].Cells[0].Value == DBNull.Value || dgv3.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    maxm3 = maxm3 + 1;
                    if (maxm == 1 && dgv3[0, 0].Value.ToString() == "1")
                    {
                        maxm3 = maxm3 + 1;
                        dgv3.Rows[e.RowIndex].Cells[0].Value = maxm3;
                    }
                    else
                    {
                        dgv3.Rows[e.RowIndex].Cells[0].Value = maxm3;
                    }
                    howmany3 = howmany3 + 1;
                }
            }
            if (winstatus == 5)
            {                //Arraylist addition
                aund4.Add(dgv4[e.ColumnIndex, e.RowIndex].Value);
                aundC4.Add(e.ColumnIndex);
                aundR4.Add(e.RowIndex);

                if (dgv4.Rows[e.RowIndex].Cells[0].Value == DBNull.Value || dgv4.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    maxm4 = maxm4 + 1;
                    if (maxm == 1 && dgv4[0, 0].Value.ToString() == "1")
                    {
                        maxm4 = maxm4 + 1;
                        dgv4.Rows[e.RowIndex].Cells[0].Value = maxm4;
                    }
                    else
                    {
                        dgv4.Rows[e.RowIndex].Cells[0].Value = maxm4;
                    }
                    howmany4 = howmany4 + 1;
                }
            }
        }

        private void currmnth_Click(object sender, EventArgs e)
        {
            try
            {
                oper_save();
                if (Main.Amatrix.acc == "")
                {
                    journ_dtst.Clear();
                    string ConnString = journalTableAdapter.Connection.ConnectionString;
                    Last_Query_Used = "Select * FROM Journal WHERE DATEPART(mm, [Date of Transaction]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Date of Transaction]) = DATEPART(yy, GETDATE())";
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                dgv.DataSource = journ_dtst.journal;
                            }
                        }
                    }
                }
                else
                {
                    Datatb.Clear();
                    string ConnString = Main.Amatrix.acc;
                    Last_Query_Used = "Select * FROM Journal WHERE DATEPART(mm, [Date of Transaction]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Date of Transaction]) = DATEPART(yy, GETDATE())";
                    using (SqlConnection conn = new SqlConnection(ConnString))
                    {
                        using (SqlCommand cmd = new SqlCommand(Last_Query_Used, conn))
                        {

                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                Datatb.Load(reader);
                                dgv.DataSource = Datatb;
                            }
                        }
                    }
                }
            }
            catch (Exception erty) { }
        }

        private void tvvbtxt13_Click(object sender, EventArgs e)
        {
            try
            {
                oper_save();
                if (Main.Amatrix.acc == "")
                {
                    journ_dtst.Clear();
                    string ConnString = journalTableAdapter.Connection.ConnectionString;
                    Last_Query_Used = "Select * FROM Journal WHERE DATEPART(dd, [Date of Transaction]) = " + tvtxt15.Text;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                dgv.DataSource = journ_dtst.journal;
                            }
                        }
                    }
                }
                else
                {
                    Datatb.Clear();
                    string ConnString = Main.Amatrix.acc;
                    Last_Query_Used = "Select * FROM Journal WHERE DATEPART(dd, [Date of Transaction]) = " + tvtxt15.Text;
                    using (SqlConnection conn = new SqlConnection(ConnString))
                    {
                        using (SqlCommand cmd = new SqlCommand(Last_Query_Used, conn))
                        {

                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                Datatb.Load(reader);
                                dgv.DataSource = Datatb;
                            }
                        }
                    }
                }
            }
            catch (Exception ertyyu) { }
        }

        private void dgv_Sorted(object sender, EventArgs e)
        {
            aund.Clear();
            aundC.Clear();
            aundR.Clear();
        }

        private void tvvbtxt14_Click(object sender, EventArgs e)
        {
            try
            {
                oper_save();
                if (Main.Amatrix.acc == "")
                {
                    journ_dtst.Clear();//may be without clear()
                    string ConnString = journalTableAdapter.Connection.ConnectionString;
                    Last_Query_Used = "Select * FROM Journal WHERE DATEPART(mm, [Date of Transaction]) = " + tvtxt16.Text;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                dgv.DataSource = journ_dtst.journal;
                            }
                        }
                    }
                }
                else
                {
                    Datatb.Clear();  //may be without Clear();
                    string ConnString = Main.Amatrix.acc;
                    Last_Query_Used = "Select * FROM Journal WHERE DATEPART(mm, [Date of Transaction]) = " + tvtxt16.Text;
                    using (SqlConnection conn = new SqlConnection(ConnString))
                    {
                        using (SqlCommand cmd = new SqlCommand(Last_Query_Used, conn))
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                Datatb.Load(reader);
                                dgv.DataSource = Datatb;
                            }
                        }
                    }
                }
            }
            catch (Exception erty) { }
        }

        private void tvvbtxt15_Click(object sender, EventArgs e)
        {
            try
            {
                oper_save();
                if (Main.Amatrix.acc == "")
                {
                    journ_dtst.Clear();
                    string ConnString = journalTableAdapter.Connection.ConnectionString;
                    Last_Query_Used = "Select * FROM Journal WHERE DATEPART(yy, [Date of Transaction]) = " + tvtxt17.Text;
                    if (sender.Equals(tvvbtxt16) == true)
                    {
                        Last_Query_Used = "Select * FROM journal WHERE [Date of Transaction] > '" + tvtxt18.Text + "' AND [Date of Transaction] < '" + tvtxt19.Text + "'";
                    }
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                dgv.DataSource = journ_dtst.journal;
                            }
                        }
                    }
                }
                else
                {
                    Datatb.Clear();
                    string ConnString = Main.Amatrix.acc;
                    Last_Query_Used = "Select * FROM Journal WHERE DATEPART(yy, [Date of Transaction]) = " + tvtxt17.Text;
                    if (sender.Equals(tvvbtxt16) == true)
                    {
                        Last_Query_Used = "Select * FROM journal WHERE [Date of Transaction] > '" + tvtxt18.Text + "' AND [Date of Transaction] < '" + tvtxt19.Text + "'";
                    }
                    using (SqlConnection conn = new SqlConnection(ConnString))
                    {
                        using (SqlCommand cmd = new SqlCommand(Last_Query_Used, conn))
                        {
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                Datatb.Load(reader);
                                dgv.DataSource = Datatb;
                            }
                        }
                    }
                }
            }
            catch (Exception erty) { }
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
        private void tvtxt1_MouseEnter(object sender, EventArgs e)
        {
            tstt = (ToolStripTextBox)sender;
            tstt.BackColor = Color.Lavender;
        }

        private void tvtxt1_MouseLeave(object sender, EventArgs e)
        {
            tstt = (ToolStripTextBox)sender;
            tstt.BackColor = Color.White;
        }

        private void tvtxt20_Click(object sender, EventArgs e)
        {
            try
            {
                tstt = (ToolStripTextBox)sender;
                if (tstt.Text == "Enter a Date (eg. 01-jan-91)" || tstt.Text == "Enter First Date (eg. 01-Jan-91)" || tstt.Text == "Enter a Year (eg. 1991)" || tstt.Text == "Enter Last Date (eg. 02-Feb-92)" || tstt.Text == "Enter a Value" || tstt.Text == "Enter a Value(Piece)" || tstt.Text == "Enter Lower Value" || tstt.Text == "Enter Greater Value" || tstt.Text == "Enter First Value" || tstt.Text == "Enter Last Value")
                {
                    tstt.SelectAll();
                }
            }
            catch (Exception erty) { }
        }

        private ArrayList copycutpaste = new ArrayList();
        private void cpy_Click(object sender, EventArgs e)
        {
            copycutpaste.Clear();
            try
            {
                if (winstatus == 3)
                {
                    copycutpaste.Add(dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value);
                }

                if (winstatus == 2)
                {
                    copycutpaste.Add(dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex].Value);
                }

                if (winstatus == 4)
                {
                    copycutpaste.Add(dgv3.Rows[dgv3.CurrentCell.RowIndex].Cells[dgv3.CurrentCell.ColumnIndex].Value);
                }

                if (winstatus == 5)
                {
                    copycutpaste.Add(dgv4.Rows[dgv4.CurrentCell.RowIndex].Cells[dgv4.CurrentCell.ColumnIndex].Value);
                }
            }
            catch (Exception erty) { }
        }

        private void pster_Click(object sender, EventArgs e)
        {
            try
            {
                if (winstatus == 3)
                {
                    dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value.ToString() + copycutpaste[0].ToString();
                }
                if (winstatus == 2)
                {
                    dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex].Value = dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex].Value.ToString() + copycutpaste[0].ToString();
                }
                if (winstatus == 4)
                {
                    dgv3.Rows[dgv3.CurrentCell.RowIndex].Cells[dgv3.CurrentCell.ColumnIndex].Value = dgv3.Rows[dgv3.CurrentCell.RowIndex].Cells[dgv3.CurrentCell.ColumnIndex].Value.ToString() + copycutpaste[0].ToString();
                }
                if (winstatus == 5)
                {
                    dgv4.Rows[dgv4.CurrentCell.RowIndex].Cells[dgv4.CurrentCell.ColumnIndex].Value = dgv4.Rows[dgv4.CurrentCell.RowIndex].Cells[dgv4.CurrentCell.ColumnIndex].Value.ToString() + copycutpaste[0].ToString();
                }
            }
            catch (Exception erty) { Am_err mer = new Am_err(); mer.tx(erty.Message); } 
        }

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

            if (sender.Equals(dgv) == true)
            {
                err_inf_1.Text = "Row : [" + e.RowIndex.ToString() + "] Cell : [" + e.ColumnIndex.ToString() + "], Serial Number : " + dgv[0, e.RowIndex].Value.ToString();
            }
            if (sender.Equals(dgv2) == true)
            {
                err_inf_1.Text = "Row : [" + e.RowIndex.ToString() + "] Cell : [" + e.ColumnIndex.ToString() + "], Serial Number : " + dgv2[0, e.RowIndex].Value.ToString();
            }
            err_inf_2.Text = mssge_ttp;
            try
            {
                ttp2.Show(mssge_ttp, this, this.Size.Width - 94, ts2.Location.Y - 34, 5000);
            }
            catch (Exception erty) { }
            ttp_del.Start();
        }

        private void dgv1_RowErrorTextChanged(object sender, DataGridViewRowEventArgs e)
        {
            Am_err mer = new Am_err(); mer.tx(e.Row.ErrorText);
        }

        private void dgv3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Am_err mer = new Am_err(); mer.tx(e.Exception.Message);
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

        private void ct_Click(object sender, EventArgs e)
        {
            copycutpaste.Clear();
            try
            {
                if (winstatus == 3)
                {
                    copycutpaste.Add(dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value);
                    dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value = null;
                }
                if (winstatus == 2)
                {
                    copycutpaste.Add(dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex].Value);
                    dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex].Value = null;
                }
                if (winstatus == 4)
                {
                    copycutpaste.Add(dgv3.Rows[dgv3.CurrentCell.RowIndex].Cells[dgv3.CurrentCell.ColumnIndex].Value);
                    dgv3.Rows[dgv3.CurrentCell.RowIndex].Cells[dgv3.CurrentCell.ColumnIndex].Value = null;
                }
                if (winstatus == 5)
                {
                    copycutpaste.Add(dgv4.Rows[dgv4.CurrentCell.RowIndex].Cells[dgv4.CurrentCell.ColumnIndex].Value);
                    dgv4.Rows[dgv4.CurrentCell.RowIndex].Cells[dgv4.CurrentCell.ColumnIndex].Value = null;
                }
            }
            catch (Exception erty) { }
        }

        //Query Quik Access
        //Equal to
        private void eqlmnuopn_Click(object sender, EventArgs e)
        {
            try
            {
                if (winstatus == 3)
                {
                    //ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    //ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    //tss2.ShowDropDown();
                }
                if (winstatus == 2)
                {
                    //ToolStripMenuItem tss = (ToolStripMenuItem)toolStripMenuItem114.DropDown.Items[dgv2.CurrentCell.ColumnIndex + 1];
                    //ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    //tss2.ShowDropDown();
                }
                if (winstatus == 4)
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)toolStripDropDownButton3.DropDown.Items[dgv3.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    tss2.ShowDropDown();
                }
                if (winstatus == 5)
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)toolStripDropDownButton5.DropDown.Items[dgv4.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    tss2.ShowDropDown();
                }
            }
            catch (Exception erty) { }
        }

        //Not Equal to
        private ToolStripItem tsmi_temp;
        private void nteqlshrt_Click(object sender, EventArgs e)
        {
            try
            {
                if (winstatus == 3)
                {
                    //ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    //ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[2];
                    //tss2.ShowDropDown();
                }
                if (winstatus == 2)
                {
                    //ToolStripMenuItem tss = (ToolStripMenuItem)toolStripMenuItem114.DropDown.Items[dgv2.CurrentCell.ColumnIndex + 1];
                    //ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    //tss2.ShowDropDown();
                }
                if (winstatus == 4)
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)toolStripDropDownButton3.DropDown.Items[dgv3.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    tss2.ShowDropDown();
                }
                if (winstatus == 5)
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)toolStripDropDownButton5.DropDown.Items[dgv4.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    tss2.ShowDropDown();
                }
            }
            catch (Exception erty) { }
        }

        //greater than
        private void grtrthn_Click(object sender, EventArgs e)
        {
            try
            {
                if (winstatus == 3)
                {
                    //ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    //ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[4];
                    //tss2.ShowDropDown();
                }
                if (winstatus == 2)
                {
                    //ToolStripMenuItem tss = (ToolStripMenuItem)toolStripMenuItem114.DropDown.Items[dgv2.CurrentCell.ColumnIndex + 1];
                    //ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    //tss2.ShowDropDown();
                }
                if (winstatus == 4)
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)toolStripDropDownButton3.DropDown.Items[dgv3.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    tss2.ShowDropDown();
                }
                if (winstatus == 5)
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)toolStripDropDownButton5.DropDown.Items[dgv4.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    tss2.ShowDropDown();
                }
            }
            catch (Exception erty) { }
        }

        //less than
        private void lessthn_Click(object sender, EventArgs e)
        {
            try
            {
                if (winstatus == 3)
                {
                    //ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    //ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[5];
                    //tss2.ShowDropDown();
                }
                if (winstatus == 2)
                {
                    //ToolStripMenuItem tss = (ToolStripMenuItem)toolStripMenuItem114.DropDown.Items[dgv2.CurrentCell.ColumnIndex + 1];
                    //ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    //tss2.ShowDropDown();
                }
                if (winstatus == 4)
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)toolStripDropDownButton3.DropDown.Items[dgv3.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    tss2.ShowDropDown();
                }
                if (winstatus == 5)
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)toolStripDropDownButton5.DropDown.Items[dgv4.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    tss2.ShowDropDown();
                }
            }
            catch (Exception erty) { }
        }

        //Between
        private void btween_Click(object sender, EventArgs e)
        {
            try
            {
                if (winstatus == 3)
                {
                    //ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    //ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[3];
                    //tss2.ShowDropDown();
                }
                if (winstatus == 2)
                {
                    //ToolStripMenuItem tss = (ToolStripMenuItem)toolStripMenuItem114.DropDown.Items[dgv2.CurrentCell.ColumnIndex + 1];
                    //ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    //tss2.ShowDropDown();
                }
                if (winstatus == 4)
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)toolStripDropDownButton3.DropDown.Items[dgv3.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    tss2.ShowDropDown();
                }
                if (winstatus == 5)
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)toolStripDropDownButton5.DropDown.Items[dgv4.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    tss2.ShowDropDown();
                }
            }
            catch (Exception erty) { }
        }

        //Starting With
        private void startwid_Click(object sender, EventArgs e)
        {
            try
            {
                if (winstatus == 3)
                {
                    //ToolStripMenuItem tss = (ToolStripMenuItem)shw_where2.DropDown.Items[dgv.CurrentCell.ColumnIndex + 1];
                    //ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[3];
                    //tss2.ShowDropDown();
                }
                if (winstatus == 2)
                {
                    //ToolStripMenuItem tss = (ToolStripMenuItem)toolStripMenuItem114.DropDown.Items[dgv2.CurrentCell.ColumnIndex + 1];
                    //ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    //tss2.ShowDropDown();
                }
                if (winstatus == 4)
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)toolStripDropDownButton3.DropDown.Items[dgv3.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    tss2.ShowDropDown();
                }
                if (winstatus == 5)
                {
                    ToolStripMenuItem tss = (ToolStripMenuItem)toolStripDropDownButton5.DropDown.Items[dgv4.CurrentCell.ColumnIndex + 1];
                    ToolStripMenuItem tss2 = (ToolStripMenuItem)tss.DropDown.Items[1];
                    tss2.ShowDropDown();
                }
            }
            catch (Exception erty) { }
        }

        private void deletecell_Click(object sender, EventArgs e)
        {
            if (winstatus == 3)
            {
                try
                {
                    dgv.CurrentCell.Value = DBNull.Value;
                }
                catch (Exception erty)
                {
                    try
                    {
                        dgv.CurrentCell.Value = null;
                    }
                    catch (Exception erty1) { }
                }
            }
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
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex-1];
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

            //dgv2

            if (sender.Equals(upall2) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[0].Cells[dgv2.CurrentCell.ColumnIndex];
                }
                catch (Exception erty12) { }
            }
            else if (sender.Equals(upone2) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentCell.RowIndex - 1].Cells[dgv2.CurrentCell.ColumnIndex];
                }
                catch (Exception erty22) { }
            }
            else if (sender.Equals(leftall2) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[0];
                }
                catch (Exception erty33) { }
            }
            else if (sender.Equals(leftone2) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex - 1];
                }
                catch (Exception erty42) { }
            }
            else if (sender.Equals(rightone2) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex + 1];
                }
                catch (Exception erty52) { }
            }
            else if (sender.Equals(rightall2) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.ColumnCount - 1];
                }
                catch (Exception erty62) { }
            }
            else if (sender.Equals(downone2) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentRow.Index + 1].Cells[dgv2.CurrentCell.ColumnIndex];
                }
                catch (Exception erty72) { }
            }
            else if (sender.Equals(downalltwo) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.RowCount - 1].Cells[dgv2.CurrentCell.ColumnIndex];
                }
                catch (Exception erty82) { }
            }
            //dgv3

            if (sender.Equals(upall3) == true)
            {
                try
                {
                    dgv3.CurrentCell = dgv3.Rows[0].Cells[dgv3.CurrentCell.ColumnIndex];
                }
                catch (Exception erty12) { }
            }
            else if (sender.Equals(upone3) == true)
            {
                try
                {
                    dgv3.CurrentCell = dgv3.Rows[dgv3.CurrentCell.RowIndex - 1].Cells[dgv3.CurrentCell.ColumnIndex];
                }
                catch (Exception erty22) { }
            }
            else if (sender.Equals(leftall3) == true)
            {
                try
                {
                    dgv3.CurrentCell = dgv3.Rows[dgv3.CurrentCell.RowIndex].Cells[0];
                }
                catch (Exception erty33) { }
            }
            else if (sender.Equals(leftone3) == true)
            {
                try
                {
                    dgv3.CurrentCell = dgv3.Rows[dgv3.CurrentCell.RowIndex].Cells[dgv3.CurrentCell.ColumnIndex - 1];
                }
                catch (Exception erty42) { }
            }
            else if (sender.Equals(rightone3) == true)
            {
                try
                {
                    dgv3.CurrentCell = dgv3.Rows[dgv3.CurrentCell.RowIndex].Cells[dgv3.CurrentCell.ColumnIndex + 1];
                }
                catch (Exception erty52) { }
            }
            else if (sender.Equals(rightall3) == true)
            {
                try
                {
                    dgv3.CurrentCell = dgv3.Rows[dgv3.CurrentCell.RowIndex].Cells[dgv3.ColumnCount - 1];
                }
                catch (Exception erty62) { }
            }
            else if (sender.Equals(downone3) == true)
            {
                try
                {
                    dgv3.CurrentCell = dgv3.Rows[dgv3.CurrentRow.Index + 1].Cells[dgv3.CurrentCell.ColumnIndex];
                }
                catch (Exception erty72) { }
            }
            else if (sender.Equals(downallthree) == true)
            {
                try
                {
                    dgv3.CurrentCell = dgv3.Rows[dgv3.RowCount - 1].Cells[dgv3.CurrentCell.ColumnIndex];
                }
                catch (Exception erty82) { }
            }
            //dgv4

            if (sender.Equals(toolStripButton173) == true)
            {
                try
                {
                    dgv4.CurrentCell = dgv4.Rows[0].Cells[dgv4.CurrentCell.ColumnIndex];
                }
                catch (Exception erty12) { }
            }
            else if (sender.Equals(toolStripSplitButton11) == true)
            {
                try
                {
                    dgv4.CurrentCell = dgv4.Rows[dgv4.CurrentCell.RowIndex - 1].Cells[dgv4.CurrentCell.ColumnIndex];
                }
                catch (Exception erty22) { }
            }
            else if (sender.Equals(toolStripButton174) == true)
            {
                try
                {
                    dgv4.CurrentCell = dgv4.Rows[dgv4.CurrentCell.RowIndex].Cells[0];
                }
                catch (Exception erty33) { }
            }
            else if (sender.Equals(toolStripSplitButton12) == true)
            {
                try
                {
                    dgv4.CurrentCell = dgv4.Rows[dgv4.CurrentCell.RowIndex].Cells[dgv4.CurrentCell.ColumnIndex - 1];
                }
                catch (Exception erty42) { }
            }
            else if (sender.Equals(toolStripSplitButton13) == true)
            {
                try
                {
                    dgv4.CurrentCell = dgv4.Rows[dgv4.CurrentCell.RowIndex].Cells[dgv4.CurrentCell.ColumnIndex + 1];
                }
                catch (Exception erty52) { }
            }
            else if (sender.Equals(toolStripButton175) == true)
            {
                try
                {
                    dgv4.CurrentCell = dgv4.Rows[dgv4.CurrentCell.RowIndex].Cells[dgv4.ColumnCount - 1];
                }
                catch (Exception erty62) { }
            }
            else if (sender.Equals(toolStripSplitButton14) == true)
            {
                try
                {
                    dgv4.CurrentCell = dgv4.Rows[dgv4.CurrentRow.Index + 1].Cells[dgv4.CurrentCell.ColumnIndex];
                }
                catch (Exception erty72) { }
            }
            else if (sender.Equals(toolStripButton176) == true)
            {
                try
                {
                    dgv4.CurrentCell = dgv4.Rows[dgv4.RowCount - 1].Cells[dgv4.CurrentCell.ColumnIndex];
                }
                catch (Exception erty82) { }
            }
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
                //dgv2
                if (sender.Equals(left2txt) == true)
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentRow.Index].Cells[Convert.ToInt32(left2txt.Text)];
                }
                else if (sender.Equals(right2txt))
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentRow.Index].Cells[Convert.ToInt32(right2txt.Text)];
                }
                //dgv3
                if (sender.Equals(toolStripTextBox79) == true)
                {
                    dgv3.CurrentCell = dgv3.Rows[dgv3.CurrentRow.Index].Cells[Convert.ToInt32(toolStripTextBox79.Text)];
                }
                else if (sender.Equals(toolStripTextBox80))
                {
                    dgv3.CurrentCell = dgv3.Rows[dgv3.CurrentRow.Index].Cells[Convert.ToInt32(toolStripTextBox80.Text)];
                }
                //dgv4
                if (sender.Equals(toolStripTextBox118) == true)
                {
                    dgv4.CurrentCell = dgv4.Rows[dgv4.CurrentRow.Index].Cells[Convert.ToInt32(toolStripTextBox118.Text)];
                }
                else if (sender.Equals(toolStripTextBox117))
                {
                    dgv4.CurrentCell = dgv4.Rows[dgv4.CurrentRow.Index].Cells[Convert.ToInt32(toolStripTextBox117.Text)];
                }
            }
            catch (Exception erty) { }
        }

        private DataTable dtp_cst_box = new DataTable();
        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (sender.Equals(dgv) == true)
            {
                toolStripButton10.Enabled = false;
                try
                {
                    dgvwintic3.Stop();
                    tmex3.Stop();
                }
                catch (Exception erty) { }

                if (winstatus != 3)
                {
                    winstatus = 3;
                }
            }
            if (sender.Equals(dgv2) == true)
            {
                if (dgv2.CurrentCell.ColumnIndex == 3)
                {
                    toolStripButton10.Enabled = true;
                    label12.Text = "Pick a Product";
                    dataGridView2.DataSource = null;
                    dtp_cst_box = new DataTable();
                    dataGridView2.BringToFront();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp_cst_box.Load(dr);
                        dataGridView2.DataSource = dtp_cst_box;
                        conn.Close();
                    }
                    else
                    {
                        SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                        SqlCommand cmd = new SqlCommand("SELECT * FROM Prod_mgmt", conn);
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        dtp_cst_box.Load(dr);
                        dataGridView2.DataSource = dtp_cst_box;
                        conn.Close();
                    }
                    cst_box.Location = new Point((Cursor.Position.X - this.Location.X) - cst_box.Size.Width / 2, Cursor.Position.Y - this.Location.Y);
                    cst_box.Visible = true;
                    dataGridView2.Refresh();
                }
                else { cst_box.Visible = false; toolStripButton10.Enabled = false; }
                try
                {
                    dgvwintic.Stop();
                    tmex.Stop();
                }
                catch (Exception erty) { }

                if (winstatus != 2)
                {
                    winstatus = 2;
                }
            }
            if (sender.Equals(dgv3) == true)
            {
                if (dgv3.CurrentCell.ColumnIndex == 3)
                {
                    toolStripButton10.Enabled = true;
                }
                if (dgv3.CurrentCell.ColumnIndex != 3) { cst_box.Visible = false; toolStripButton10.Enabled = false; }
                try
                {
                    dgvwintic.Stop();
                    tmex.Stop();
                }
                catch (Exception erty) { }

                if (winstatus != 4)
                {
                    winstatus = 4;
                }
            }
            if (sender.Equals(dgv4) == true)
            {
                if (dgv4.CurrentCell.ColumnIndex == 4)
                { toolStripButton10.Enabled = true; }
                else if (dgv4.CurrentCell.ColumnIndex == 1)
                {
                    toolStripButton10.Enabled = true;
                    label12.Text = "Pick a Customer";
                    dataGridView2.DataSource = null;
                    dtp_cst_box = new DataTable();
                    dataGridView2.BringToFront();
                    if (Main.Amatrix.mgt == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Customers", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp_cst_box.Load(dr);
                        dataGridView2.DataSource = dtp_cst_box;
                        conn.Close();
                    }
                    else
                    {
                        SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                        SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", conn);
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        dtp_cst_box.Load(dr);
                        dataGridView2.DataSource = dtp_cst_box;
                        conn.Close();
                    }
                    cst_box.Location = new Point((Cursor.Position.X - this.Location.X) - cst_box.Size.Width / 2, Cursor.Position.Y - this.Location.Y);
                    cst_box.Visible = true;

                }
                else { cst_box.Visible = false; toolStripButton10.Enabled = false; }
                try
                {
                    dgvwintic.Stop();
                    tmex.Stop();
                }
                catch (Exception erty) { }
                if (winstatus != 5)
                {
                    winstatus = 5;
                }
            }
            zz_yn(true);
        }

        private void initializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                initconn1.RunWorkerAsync();
                init_conn2.RunWorkerAsync();
                init_conn3.RunWorkerAsync();
                init_conn4.RunWorkerAsync();
                bkkinit.RunWorkerAsync();
                bkk_init2.RunWorkerAsync();
                bkk_init3.RunWorkerAsync();
                bkk_init4.RunWorkerAsync();
            }
            catch (Exception erty) { }
        }

        private void dynayes_Click(object sender, EventArgs e)
        {
            if (sender.Equals(dynayes) == true)
            {
                acc_journ_sett.Default.dynam_jrn = true;
                acc_journ_sett.Default.Save();
            }
            else
            {
                acc_journ_sett.Default.dynam_jrn = false;
                acc_journ_sett.Default.Save();
            }
        }

        private void refrjrn_Click(object sender, EventArgs e)
        {
            thsum1strt();
            th_sum2_strt();
        }

        private void zz_MouseEnter(object sender, EventArgs e)
        {
            zz.BackColor = Color.GhostWhite;
            toolStrip5.BackColor = zz.BackColor;
            tmr.Interval = 13000;
            tmr.Start();
        }

        private void zz_MouseLeave(object sender, EventArgs e)
        {
            zz.BackColor = Color.WhiteSmoke;
            toolStrip5.BackColor = zz.BackColor;
            tmr.Interval = 13000;
            tmr.Start();
        }

        private void zz_yn(bool shw)
        {
            if (extern_opn == 0)
            {
                try
                {
                    if (shw == true)
                    {
                        try
                        {
                            tmr.Stop();
                        }
                        catch (Exception erty) { }
                        set_quikbox();
                        cst_box.BringToFront();
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
                catch (Exception erty) { }
            }
            else
            {
                general_mssg("Some Quick Access Functions Crashed and are Temporarily Unavailable", "Opened from External Application");
            }
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

        private void newitm_Click(object sender, EventArgs e)
        {
            if (winstatus == 3)
            {
                foreach (DataGridViewCell dgvc in dgv.SelectedCells)
                {
                    if (dgvc.ColumnIndex == 0)
                    {
                    }
                    else
                    {
                        if (dgvc.ReadOnly == false) { dgvc.Value = DBNull.Value; }
                    }
                }
            }
            if (winstatus == 2)
            {
                foreach (DataGridViewCell dgvc in dgv2.SelectedCells)
                {
                    if (dgvc.ColumnIndex == 0)
                    {
                    }
                    else
                    {
                        if (dgvc.ReadOnly == false)
                        { dgvc.Value = DBNull.Value; }
                    }
                }
            }
            if (winstatus == 4)
            {
                foreach (DataGridViewCell dgvc in dgv3.SelectedCells)
                {
                    if (dgvc.ColumnIndex == 0)
                    {
                    }
                    else
                    {
                        if
                            (dgvc.ReadOnly == false) { dgvc.Value = DBNull.Value; }
                    }
                }
            }
            if (winstatus == 5)
            {
                foreach (DataGridViewCell dgvc in dgv4.SelectedCells)
                {
                    if (dgvc.ColumnIndex == 0)
                    {
                    }
                    else
                    {
                        if (dgvc.ReadOnly == false) { dgvc.Value = DBNull.Value; }
                    }
                }
            }
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
                    if (dgv.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow dgvr in dgv.SelectedRows)
                        {
                            if (Convert.ToInt32(dgvr.Cells[0].Value) == 1) { }
                            else
                            {
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
                    string ConnString = journalTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand("DELETE FROM " + journ_dtst.journal.TableName, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                journ_dtst.Load(reader, LoadOption.OverwriteChanges, "journal");
                                dgv.DataSource = null;
                                dgv.DataSource = journ_dtst.journal;
                            }
                        }
                    }
                    journ_dtst.journal.Rows.Clear();
                    journ_dtst.journal.AcceptChanges();
                    maxm = 0; howmany = 0;
                }
                else
                {
                    dgv.DataSource = null;
                    Datatb.Clear();
                    Datatb = basql.Execute(Main.Amatrix.acc, "DELETE FROM " + journ_dtst.journal.TableName, "journal", Datatb);
                    dgv.DataSource = Datatb;
                    maxm = 0; howmany = 0;
                }
            }

            //dgv2
            if (sender.Equals(delete2) == true)
            {
                if (Convert.ToInt32(dgv2.CurrentRow.Cells[0].Value) == 1)
                {}
                else
                {
                    if (dgv2.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow dgvr in dgv2.SelectedRows)
                        {
                            if (Convert.ToInt32(dgvr.Cells[0].Value) == 1) { }
                            else
                            {
                                dgv2.Rows.Remove(dgvr);
                            }
                        }
                    }
                    else
                    {
                        if (dgv2.SelectedCells.Count > 1)
                        {
                            foreach (DataGridViewCell dgvc in dgv2.SelectedCells)
                            {
                                if (Convert.ToInt32(dgvc.OwningRow.Cells[0].Value) == 1)
                                { }
                                else
                                {
                                    try
                                    {
                                        dgv2.Rows.RemoveAt(dgvc.RowIndex);
                                    }
                                    catch (Exception erty) { }
                                }
                            }
                        }
                        else
                        {
                            dgv2.Rows.RemoveAt(dgv2.CurrentRow.Index);
                            howmany2 = howmany2 - 1;
                            numbrw2.Text = howmany2.ToString();
                        }
                    }
                }
            }

            if (sender.Equals(del_all2) == true)
            {
                if (Main.Amatrix.acc == "")
                {
                    string ConnString = cashBookTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand("DELETE FROM " + cashbook_dtst.CashBook.TableName, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                cashbook_dtst.Load(reader, LoadOption.OverwriteChanges, "CashBook");
                                dgv2.DataSource = null;
                                dgv2.DataSource = cashbook_dtst.CashBook;
                            }
                        }
                    }
                    cashbook_dtst.CashBook.Rows.Clear();
                    cashbook_dtst.CashBook.AcceptChanges();
                    maxm2 = 0; howmany2 = 0;
                }
                else
                {
                    dgv2.DataSource = null;
                    Datatb2.Clear();
                    Datatb2 = basql.Execute(Main.Amatrix.acc, "DELETE FROM " + cashbook_dtst.CashBook.TableName, "CashBook", Datatb2);
                    dgv2.DataSource = Datatb2;
                    maxm2 = 0; maxm2 = 0;
                }
            }

            //dgv3

            if (sender.Equals(delete3) == true)
            {
                if (Convert.ToInt32(dgv3.CurrentRow.Cells[0].Value) == 1)
                {
                }
                else
                {
                    if (dgv3.SelectedRows.Count > 0)
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("", conn);
                        conn.Open();
                        foreach (DataGridViewRow dgvr in dgv3.SelectedRows)
                        {
                            if (Convert.ToInt32(dgvr.Cells[0].Value) == 1) { }
                            else
                            {
                                if (Main.Amatrix.acc == "")
                                {
                                    cmd.CommandText = "DELETE FROM acc_linking WHERE [For Row] = '" + dgvr.Cells[0].Value.ToString() + "' AND [For Book] = 'purchasebook'";
                                    SqlCeDataReader dr = cmd.ExecuteReader();
                                    SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                                    SqlCeCommand cmd2 = new SqlCeCommand("UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'purchasebook' AND [Invoice Reference Number (ID)] = '" + dgvr.Cells[3].Value.ToString() + "'", conn2);
                                    conn2.Open();
                                    SqlCeDataReader dr2 = cmd2.ExecuteReader();
                                    conn2.Close();
                                }
                                else
                                {
                                    DataTable dty = new DataTable();
                                    basql.Execute(Main.Amatrix.acc, "DELETE FROM acc_linking WHERE [For Row] = '" + dgvr.Cells[0].Value.ToString() + "' AND [For Book] = 'purchasebook'", "purchasebook", dty);
                                    basql.Execute(Main.Amatrix.acc, "UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'purchasebook' AND [Invoice Reference Number (ID)] = '" + dgvr.Cells[3].Value.ToString() + "'", "invoice", dty);
                                    dty.Clear();
                                    dty.Dispose();
                                }
                                dgv3.Rows.Remove(dgvr);
                            }
                        }
                        conn.Close();
                    }
                    else
                    {
                        if (dgv3.SelectedCells.Count > 1)
                        {
                            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                            SqlCeCommand cmd = new SqlCeCommand("", conn);
                            conn.Open();
                            foreach (DataGridViewCell dgvc in dgv3.SelectedCells)
                            {
                                if (Convert.ToInt32(dgvc.OwningRow.Cells[0].Value) == 1)
                                { }
                                else
                                {
                                    try
                                    {
                                        if (Main.Amatrix.acc == "")
                                        {
                                            cmd.CommandText = "DELETE FROM acc_linking WHERE [For Row] = '" + dgvc.OwningRow.Cells[0].Value.ToString() + "' AND [For Book] = 'purchasebook'";
                                            SqlCeDataReader dr = cmd.ExecuteReader();

                                            SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                                            SqlCeCommand cmd2 = new SqlCeCommand("UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'purchasebook' AND [Invoice Reference Number (ID)] = '" + dgvc.OwningRow.Cells[3].Value.ToString() + "'", conn2);
                                            conn2.Open();
                                            SqlCeDataReader dr2 = cmd2.ExecuteReader();
                                            conn2.Close();
                                        }
                                        else 
                                        {
                                            DataTable dty = new DataTable();
                                            basql.Execute(Main.Amatrix.acc, "DELETE FROM acc_linking WHERE [For Row] = '" + dgvc.OwningRow.Cells[0].Value.ToString() + "' AND [For Book] = 'purchasebook'", "purchasebook", dty);
                                            basql.Execute(Main.Amatrix.acc, "UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'purchasebook' AND [Invoice Reference Number (ID)] = '" + dgvc.OwningRow.Cells[3].Value.ToString() + "'", "invoice", dty);
                                            dty.Clear();
                                            dty.Dispose();
                                        }
                                        dgv3.Rows.RemoveAt(dgvc.RowIndex);
                                    }
                                    catch (Exception erty) { }
                                }
                            }
                            conn.Close();
                        }
                        else
                        {
                            if (Main.Amatrix.acc == "")
                            {
                                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                                SqlCeCommand cmd = new SqlCeCommand("DELETE FROM acc_linking WHERE [For Row] = '" + dgv3.CurrentRow.Cells[0].Value.ToString() + "' AND [For Book] = 'purchasebook'", conn);
                                conn.Open();
                                SqlCeDataReader dr = cmd.ExecuteReader();
                                conn.Close();

                                SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                                SqlCeCommand cmd2 = new SqlCeCommand("UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'purchasebook' AND [Invoice Reference Number (ID)] = '" + dgv3.CurrentRow.Cells[3].Value.ToString() + "'", conn2);
                                conn2.Open();
                                SqlCeDataReader dr2 = cmd2.ExecuteReader();
                                conn2.Close();
                            }
                            else
                            {
                                DataTable dty = new DataTable();
                                basql.Execute(Main.Amatrix.acc, "DELETE FROM acc_linking WHERE [For Row] = '" + dgv3.CurrentRow.Cells[0].Value.ToString() + "' AND [For Book] = 'purchasebook'", "purchasebook", dty);
                                basql.Execute(Main.Amatrix.acc, "UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'purchasebook' AND [Invoice Reference Number (ID)] = '" + dgv3.CurrentRow.Cells[3].Value.ToString() + "'", "invoice", dty);
                                dty.Clear();
                                dty.Dispose();
                            }
                            dgv3.Rows.RemoveAt(dgv3.CurrentRow.Index);
                            howmany3 = howmany3 - 1;
                            numbrw3.Text = howmany3.ToString();
                        }
                    }
                }
            }

            if (sender.Equals(del_all3) == true)
            {
                if (Main.Amatrix.acc == "")
                {
                    string ConnString = purchaseBookTableAdapter.Connection.ConnectionString;
                    string SqlString = "DELETE FROM " + purchaseBook_dtst.PurchaseBook.TableName;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();
                            conn.Close();

                            using (reader)
                            {
                                purchaseBook_dtst.Load(reader, LoadOption.OverwriteChanges, "PurchaseBook");
                                dgv3.DataSource = null;
                                dgv3.DataSource = purchaseBook_dtst.PurchaseBook;
                            }
                        }
                    }
                    purchaseBook_dtst.PurchaseBook.Rows.Clear();
                    purchaseBook_dtst.PurchaseBook.AcceptChanges();
                    maxm3 = 0; howmany3 = 0;

                    SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                    SqlCeCommand cmd2 = new SqlCeCommand("DELETE FROM acc_linking WHERE [For Book] = 'purchasebook'", conn2);
                    conn2.Open();
                    SqlCeDataReader dr = cmd2.ExecuteReader();
                    conn2.Close();

                    SqlCeConnection conn3 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                    SqlCeCommand cmd3 = new SqlCeCommand("UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'purchasebook'", conn3);
                    conn3.Open();
                    SqlCeDataReader dr3 = cmd3.ExecuteReader();
                    conn3.Close();
                }
                else
                {
                    dgv3.DataSource = null;
                    Datatb3.Clear();
                    Datatb3 = basql.Execute(Main.Amatrix.acc, "DELETE FROM PurchaseBook", "PurchaseBook", Datatb3);
                    dgv3.DataSource = Datatb3;
                    maxm3 = 0; howmany3 = 0;

                    DataTable dty = new DataTable();
                    basql.Execute(Main.Amatrix.acc, "DELETE FROM acc_linking WHERE [For Book] = 'purchasebook'", "acc_linking", dty);
                    basql.Execute(Main.Amatrix.acc, "UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'purchasebook'", "invoice", dty);
                    dty.Clear();
                    dty.Dispose();
                }
            }
            //dgv4
            if (sender.Equals(toolStripSplitButton15) == true)
            {
                if (Convert.ToInt32(dgv4.CurrentRow.Cells[0].Value) == 1)
                {
                }
                else
                {
                    if (dgv4.SelectedRows.Count > 0)
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("", conn);
                        foreach (DataGridViewRow dgvr in dgv4.SelectedRows)
                        {
                            if (Convert.ToInt32(dgvr.Cells[0].Value) == 1) { }
                            else
                            {
                                if (Main.Amatrix.acc == "")
                                {
                                    cmd.CommandText = "DELETE FROM acc_linking WHERE [For Row] = '" + dgvr.Cells[0].Value.ToString() + "' AND [For Book] = 'salesbook'";
                                    SqlCeDataReader dr = cmd.ExecuteReader();

                                    SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                                    SqlCeCommand cmd2 = new SqlCeCommand("UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'salesbook' AND [Invoice Reference Number (ID)] = '" + dgvr.Cells[4].Value.ToString() + "'", conn2);
                                    conn2.Open();
                                    SqlCeDataReader dr2 = cmd2.ExecuteReader();
                                    conn2.Close();
                                }
                                else
                                {
                                    DataTable dty = new DataTable();
                                    basql.Execute(Main.Amatrix.acc, "DELETE FROM acc_linking WHERE [For Row] = '" + dgvr.Cells[0].Value.ToString() + "' AND [For Book] = 'salesbook'", "acc_linker", dty);
                                    basql.Execute(Main.Amatrix.acc, "UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'salesbook' AND [Invoice Reference Number (ID)] = '" + dgvr.Cells[4].Value.ToString() + "'", "invoice", dty);
                                    dty.Clear();
                                    dty.Dispose();
                                }
                                dgv4.Rows.Remove(dgvr);
                            }
                        }
                        conn.Close();
                    }
                    else
                    {
                        if (dgv4.SelectedCells.Count > 1)
                        {
                            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                            SqlCeCommand cmd = new SqlCeCommand("", conn);
                            conn.Open();
                            foreach (DataGridViewCell dgvc in dgv4.SelectedCells)
                            {
                                if (Convert.ToInt32(dgvc.OwningRow.Cells[0].Value) == 1)
                                { }
                                else
                                {
                                    try
                                    {
                                        if (Main.Amatrix.acc == "")
                                        {
                                            cmd.CommandText = "DELETE FROM acc_linking WHERE [For Row] = '" + dgvc.OwningRow.Cells[0].Value.ToString() + "' AND [For Book] = 'salesbook'";
                                            SqlCeDataReader dr = cmd.ExecuteReader();

                                            SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                                            SqlCeCommand cmd2 = new SqlCeCommand("UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'salesbook' AND [Invoice Reference Number (ID)] = '" + dgvc.OwningRow.Cells[4].Value.ToString() + "'", conn2);
                                            conn2.Open();
                                            SqlCeDataReader dr2 = cmd2.ExecuteReader();
                                            conn2.Close();
                                        }
                                        else
                                        {
                                            DataTable dty = new DataTable();
                                            basql.Execute(Main.Amatrix.acc, "DELETE FROM acc_linking WHERE [For Row] = '" + dgvc.OwningRow.Cells[0].Value.ToString() + "' AND [For Book] = 'salesbook'", "salesbook", dty);
                                            basql.Execute(Main.Amatrix.acc, "UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'salesbook' AND [Invoice Reference Number (ID)] = '" + dgvc.OwningRow.Cells[4].Value.ToString() + "'", "invoice", dty);
                                            dty.Clear();
                                            dty.Dispose();
                                        }
                                    }
                                    catch (Exception erty) { }
                                    dgv4.Rows.RemoveAt(dgvc.RowIndex);
                                }
                            }
                            conn.Close();
                        }
                        else
                        {
                            if (Main.Amatrix.acc == "")
                            {
                                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                                SqlCeCommand cmd = new SqlCeCommand("DELETE FROM acc_linking WHERE [For Row] = '" + dgv4.CurrentRow.Cells[0].Value.ToString() + "' AND [For Book] = 'salesbook'", conn);
                                conn.Open();
                                SqlCeDataReader dr = cmd.ExecuteReader();
                                conn.Close();

                                SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                                SqlCeCommand cmd2 = new SqlCeCommand("UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'salesbook' AND [Invoice Reference Number (ID)] = '" + dgv4.CurrentRow.Cells[4].Value.ToString() + "'", conn2);
                                conn2.Open();
                                SqlCeDataReader dr2 = cmd2.ExecuteReader();
                                conn2.Close();
                            }
                            else
                            {
                                DataTable dty = new DataTable();
                                basql.Execute(Main.Amatrix.acc, "DELETE FROM acc_linking WHERE [For Row] = '" + dgv4.CurrentRow.Cells[0].Value.ToString() + "' AND [For Book] = 'salesbook'", "acc_linking", dty);
                                basql.Execute(Main.Amatrix.acc, "UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'salesbook' AND [Invoice Reference Number (ID)] = '" + dgv4.CurrentRow.Cells[4].Value.ToString() + "'", "invoice", dty);
                                dty.Clear();
                                dty.Dispose();
                            }
                            dgv4.Rows.RemoveAt(dgv4.CurrentRow.Index);
                            howmany4 = howmany4 - 1;
                            numbrw4.Text = howmany4.ToString();
                        }
                    }
                }
            }

            if (sender.Equals(toolStripMenuItem514) == true)
            {
                if (Main.Amatrix.acc == "")
                {
                    string ConnString = salesBookTableAdapter.Connection.ConnectionString;
                    string SqlString = "DELETE FROM " + salesBook_dtst.SalesBook.TableName;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                salesBook_dtst.Load(reader, LoadOption.OverwriteChanges, "SalesBook");
                                dgv4.DataSource = null;
                                dgv4.DataSource = salesBook_dtst.SalesBook;
                            }
                        }
                    }
                    salesBook_dtst.SalesBook.Rows.Clear();
                    salesBook_dtst.SalesBook.AcceptChanges();
                    maxm4 = 0; howmany4 = 0;

                    SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Misc_DBConnectionString);
                    SqlCeCommand cmd2 = new SqlCeCommand("DELETE FROM acc_linking WHERE [For Book] = 'salesbook'", conn2);
                    conn2.Open();
                    SqlCeDataReader dr = cmd2.ExecuteReader();
                    conn2.Close();

                    SqlCeConnection conn3 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                    SqlCeCommand cmd3 = new SqlCeCommand("UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'salesbook'", conn3);
                    conn3.Open();
                    SqlCeDataReader dr3 = cmd3.ExecuteReader();
                    conn3.Close();
                }
                else
                {
                    dgv4.DataSource = null;
                    Datatb4.Clear();
                    Datatb4 = basql.Execute(Main.Amatrix.acc, "DELETE FROM SalesBook", "SalesBook", Datatb4);
                    dgv4.DataSource = Datatb4;
                    maxm4 = 0; howmany4 = 0;

                    DataTable dty = new DataTable();
                    basql.Execute(Main.Amatrix.acc, "DELETE FROM acc_linking WHERE [For Book] = 'salesbook'", "acc_linking", dty);
                    basql.Execute(Main.Amatrix.acc, "UPDATE invoice SET [Binded to Journal] = NULL, [Binded to Journal Entry] = NULL WHERE [Binded to Journal] = 'salesbook'", "invoice", dty);
                    dty.Clear();
                    dty.Dispose();
                }
            }
            try
            {
                oper_save();
            }
            catch (Exception erty) { }
        }

        private void nxtst_Click(object sender, EventArgs e)
        {
            sender_obj = sender;
            th_nxt_strt();
        }

        private Thread th_nxt;
        private delegate void del_nxt();
        private object sender_obj;
        private void th_nxt_strt()
        {
            th_nxt = new Thread(new ThreadStart(del_nxt_strt));
            th_nxt.IsBackground = true;
            th_nxt.Start();
        }

        private void del_nxt_strt()
        {
            this.Invoke(new del_nxt(next));
        }

        private void next()
        {
            oper_save();
            if (sender_obj.Equals(nxtst) == true)
            {
                try
                {
                    smallest = biggest;
                    biggest = biggest + 100; 
                    Last_Query_Used = "Select * From journal Where [Serial Number] >= '" + smallest + "' AND [Serial Number] <= '" + biggest + "'";
                    if (Main.Amatrix.acc == "")
                    {
                        journ_dtst.Clear();
                        string ConnString = journalTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                    dgv.DataSource = journ_dtst.journal;
                                }
                            }
                        }
                    }
                    else
                    {
                        Datatb = asql.Go_To_Position(Main.Amatrix.acc, Datatb, "journal", true, smallest, biggest);
                        dgv.DataSource = Datatb;
                    }
                    try
                    {
                        huntme.Text = dgv.Rows[0].Cells[0].Value.ToString() + "-" + dgv.Rows[dgv.RowCount - 2].Cells[0].Value.ToString();
                    }
                    catch (Exception erty) { huntme.Text = smallest.ToString() + "-" + biggest.ToString(); }
                }
                catch (Exception erty7889) { }
            }
            //dgv2
            if (sender_obj.Equals(nxtst2) == true)
            {
                try
                {
                    smallest2 = biggest2;
                    biggest2 = biggest2 + 100; 
                    Last_Query_Used_cb = "Select * From CashBook Where [Serial Number] >= '" + smallest2 + "' AND [Serial Number] <= '" + biggest2 + "'";  
                    if (Main.Amatrix.acc == "")
                    {
                        cashbook_dtst.Clear();
                        string ConnString = cashBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_cb, conn))
                            {
                                cmd.CommandType = CommandType.Text;
                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();
                                using (reader)
                                {
                                    cashbook_dtst.Load(reader, LoadOption.PreserveChanges, "CashBook");
                                    dgv2.DataSource = cashbook_dtst.CashBook;
                                }
                            }
                        }
                    }
                    else
                    {
                        Datatb2 = asql.Go_To_Position(Main.Amatrix.acc, Datatb2, "CashBook", true, smallest2, biggest2);
                        dgv2.DataSource = Datatb2;
                    }
                    try
                    {
                        huntme2.Text = dgv2.Rows[0].Cells[0].Value.ToString() + "-" + dgv2.Rows[dgv.RowCount - 2].Cells[0].Value.ToString();
                    }
                    catch (Exception erty) { huntme2.Text = smallest2.ToString() + "-" + biggest2.ToString(); }
                }
                catch (Exception erty) { }
            }
            //dgv3
            if (sender_obj.Equals(nxtst3) == true)
            {
                try
                {
                    smallest3 = biggest3;
                    biggest3 = biggest3 + 100; 
                    Last_Query_Used_pb = "Select * From PurchaseBook Where [Serial Number] >= '" + smallest3 + "' AND [Serial Number] <= '" + biggest3 + "'";
                        
                    if (Main.Amatrix.acc == "")
                    {
                        purchaseBook_dtst.Clear();
                        string ConnString = purchaseBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_pb, conn))
                            {
                                cmd.CommandType = CommandType.Text;
                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();
                                using (reader)
                                {
                                    purchaseBook_dtst.Load(reader, LoadOption.PreserveChanges, "PurchaseBook");
                                    dgv3.DataSource = purchaseBook_dtst.PurchaseBook;
                                }
                            }
                        }
                    }
                    else
                    {
                        Datatb3 = asql.Go_To_Position(Main.Amatrix.acc, Datatb3, "PurchaseBook", true, smallest3, biggest3);
                        dgv3.DataSource = Datatb3;
                    }
                    try
                    {
                        huntme3.Text = dgv3.Rows[0].Cells[0].Value.ToString() + "-" + dgv3.Rows[dgv3.RowCount - 2].Cells[0].Value.ToString();
                    }
                    catch (Exception erty) { huntme3.Text = smallest3.ToString() + "-" + biggest3.ToString(); }
                }
                catch (Exception erty) { }
            }
            //dgv4
            if (sender_obj.Equals(nxtst4) == true)
            {
                try
                {
                    smallest4 = biggest4;
                    biggest4 = biggest4 + 100; Last_Query_Used_sb = "Select * From SalesBook Where [Serial Number] >= '" + smallest4 + "' AND [Serial Number] <= '" + biggest4 + "'";
                        
                    if (Main.Amatrix.acc == "")
                    {
                        salesBook_dtst.Clear();
                        string ConnString = salesBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_sb, conn))
                            {
                                cmd.CommandType = CommandType.Text;
                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();
                                using (reader)
                                {
                                    salesBook_dtst.Load(reader, LoadOption.PreserveChanges, "SalesBook");
                                    dgv4.DataSource = salesBook_dtst.SalesBook;
                                }
                            }
                        }
                    }
                    else
                    {
                        Datatb4 = asql.Go_To_Position(Main.Amatrix.acc, Datatb4, "SalesBook", true, smallest4, biggest4);
                        dgv4.DataSource = Datatb4;
                    }
                    try
                    {
                        huntme4.Text = dgv4.Rows[0].Cells[0].Value.ToString() + "-" + dgv4.Rows[dgv4.RowCount - 2].Cells[0].Value.ToString();
                    }
                    catch (Exception erty) { huntme4.Text = smallest4.ToString() + "-" + biggest4.ToString(); }
                }
                catch (Exception erty) { }
            }
        }

        private void prev_ButtonClick(object sender, EventArgs e)
        {
            sender_obj = sender;
            th_back_strt();
        }

        private Thread th_back;
        private delegate void del_back();
        private void th_back_strt()
        {
            try
            {
                th_back = new Thread(new ThreadStart(del_back_strt));
                th_back.IsBackground = true;
                th_back.Start();
            }
            catch (Exception erty) { }
        }

        private void del_back_strt()
        {
            try
            {
                this.Invoke(new del_back(back));
            }
            catch (Exception erty) { back(); }
        }

        private void back()
        {
            oper_save();
            if (sender_obj.Equals(prev) == true)
            {
                try
                {
                    if (Convert.ToInt32(dgv.Rows[0].Cells[0].Value) == 1)
                    {
                    }
                    else
                    {
                        biggest = (int)dgv.Rows[0].Cells[0].Value;
                        smallest = biggest - 100; Last_Query_Used = "Select * From journal Where [Serial Number] >= '" + smallest + "' AND [Serial Number] <= '" + biggest + "'";
                            
                        if (Main.Amatrix.acc == "")
                        {
                            journ_dtst.Clear();
                            string ConnString = journalTableAdapter.Connection.ConnectionString;
                            using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                            {
                                using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                                {
                                    cmd.CommandType = CommandType.Text;

                                    conn.Open();
                                    SqlCeDataReader reader = cmd.ExecuteReader();

                                    using (reader)
                                    {
                                        journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                        dgv2.DataSource = journ_dtst.journal;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Datatb = asql.Go_To_Position(Main.Amatrix.acc, Datatb, "journal", true, smallest, biggest);
                            dgv.DataSource = Datatb;
                        }
                    }
                }
                catch (Exception erty)
                {
                    smallest = smallest - 100;
                    biggest = biggest - 100; Last_Query_Used = "Select * From journal Where [Serial Number] >= '" + smallest + "' AND [Serial Number] <= '" + biggest + "'";
                       
                    if (Main.Amatrix.acc == "")
                    {
                        journ_dtst.Clear();
                        string ConnString = journalTableAdapter.Connection.ConnectionString;
                         using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                    dgv.DataSource = journ_dtst.journal;
                                }
                            }
                        }
                    }
                    else
                    {
                        Datatb = asql.Go_To_Position(Main.Amatrix.acc, Datatb, "journal", true, smallest, biggest);
                        dgv.DataSource = Datatb;
                    }
                }
                try
                {
                    huntme.Text = dgv.Rows[0].Cells[0].Value.ToString() + "-" + dgv.Rows[dgv.RowCount - 2].Cells[0].Value.ToString();
                }
                catch (Exception erty) { huntme.Text = smallest.ToString() + "-" + biggest.ToString(); }
            }
            //dgv2
            if (sender_obj.Equals(prev2) == true)
            {
                try
                {
                    if (Convert.ToInt32(dgv2.Rows[0].Cells[0].Value) == 1)
                    {
                    }
                    else
                    {
                        biggest2 = (int)dgv2.Rows[0].Cells[0].Value;
                        smallest2 = biggest2 - 100; Last_Query_Used_cb = "Select * From CashBook Where [Serial Number] >= '" + smallest2 + "' AND [Serial Number] <= '" + biggest2 + "'";
                            
                        if (Main.Amatrix.acc == "")
                        {
                            cashbook_dtst.Clear();
                            string ConnString = cashBookTableAdapter.Connection.ConnectionString;
                            using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                            {
                                using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_cb, conn))
                                {
                                    cmd.CommandType = CommandType.Text;

                                    conn.Open();
                                    SqlCeDataReader reader = cmd.ExecuteReader();

                                    using (reader)
                                    {
                                        cashbook_dtst.Load(reader, LoadOption.PreserveChanges, "CashBook");
                                        dgv2.DataSource = cashbook_dtst.CashBook;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Datatb2 = asql.Go_To_Position(Main.Amatrix.acc, Datatb2, "CashBook", true, smallest2, biggest2);
                            dgv2.DataSource = Datatb2;
                        }
                    }
                }
                catch (Exception erty)
                {
                    smallest2 = smallest2 - 100;
                    biggest2 = biggest2 - 100; Last_Query_Used_cb = "Select * From CashBook Where [Serial Number] >= '" + smallest2 + "' AND [Serial Number] <= '" + biggest2 + "'";
                        
                    if (Main.Amatrix.acc == "")
                    {
                        cashbook_dtst.Clear();
                        string ConnString = cashBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_cb, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    cashbook_dtst.Load(reader, LoadOption.PreserveChanges, "CashBook");
                                    dgv2.DataSource = cashbook_dtst.CashBook;
                                }
                            }
                        }
                    }
                    else
                    {
                        Datatb2 = asql.Go_To_Position(Main.Amatrix.acc, Datatb2, "CashBook", true, smallest2, biggest2);
                        dgv2.DataSource = Datatb2;
                    }
                }
                try
                {
                    huntme2.Text = dgv2.Rows[0].Cells[0].Value.ToString() + "-" + dgv2.Rows[dgv2.RowCount - 2].Cells[0].Value.ToString();
                }
                catch (Exception erty) { huntme2.Text = smallest2.ToString() + "-" + biggest2.ToString(); }
            }
            //dgv3
            if (sender_obj.Equals(prev3) == true)
            {
                try
                {
                    if (Convert.ToInt32(dgv3.Rows[0].Cells[0].Value) == 1)
                    {
                    }
                    else
                    {
                        biggest3 = (int)dgv3.Rows[0].Cells[0].Value;
                        smallest3 = biggest3 - 100; Last_Query_Used_pb = "Select * From PurchaseBook Where [Serial Number] >= '" + smallest3 + "' AND [Serial Number] <= '" + biggest3 + "'";
                            
                        if (Main.Amatrix.acc == "")
                        {
                            purchaseBook_dtst.Clear();
                            string ConnString = purchaseBookTableAdapter.Connection.ConnectionString;
                            using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                            {
                                using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_pb, conn))
                                {
                                    cmd.CommandType = CommandType.Text;

                                    conn.Open();
                                    SqlCeDataReader reader = cmd.ExecuteReader();

                                    using (reader)
                                    {
                                        purchaseBook_dtst.Load(reader, LoadOption.PreserveChanges, "PurchaseBook");
                                        dgv3.DataSource = purchaseBook_dtst.PurchaseBook;
                                    }
                                }
                            }
                        }
                        else
                        {
                                Datatb3 = asql.Go_To_Position(Main.Amatrix.acc, Datatb3, "PurchaseBook", true, smallest3, biggest3);
                                dgv3.DataSource = Datatb3;
                        }
                    }
                }
                catch (Exception erty)
                {
                    smallest3 = smallest3 - 100;
                    biggest3 = biggest3 - 100; Last_Query_Used_pb = "Select * From PurchaseBook Where [Serial Number] >= '" + smallest3 + "' AND [Serial Number] <= '" + biggest3 + "'";
                        
                    if (Main.Amatrix.acc == "")
                    {
                        purchaseBook_dtst.Clear();
                        string ConnString = purchaseBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_pb, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    purchaseBook_dtst.Load(reader, LoadOption.PreserveChanges, "PurchaseBook");
                                    dgv3.DataSource = purchaseBook_dtst.PurchaseBook;
                                }
                            }
                        }
                    }
                    else
                    {
                            Datatb3 = asql.Go_To_Position(Main.Amatrix.acc, Datatb3, "PurchaseBook", true, smallest3, biggest3);
                            dgv3.DataSource = Datatb3;
                    }
                }
                try
                {
                    huntme3.Text = dgv3.Rows[0].Cells[0].Value.ToString() + "-" + dgv3.Rows[dgv3.RowCount - 2].Cells[0].Value.ToString();
                }
                catch (Exception erty) { huntme3.Text = smallest3.ToString() + "-" + biggest3.ToString(); }
            }
            //dgv4
            if (sender_obj.Equals(prev4) == true)
            {
                try
                {
                    if (Convert.ToInt32(dgv4.Rows[0].Cells[0].Value) == 1)
                    {
                    }
                    else
                    {
                        biggest4 = (int)dgv4.Rows[0].Cells[0].Value;
                        smallest4 = biggest4 - 100; Last_Query_Used_sb = "Select * From SalesBook Where [Serial Number] >= '" + smallest4 + "' AND [Serial Number] <= '" + biggest4 + "'";
                            
                        if (Main.Amatrix.acc == "")
                        {
                            salesBook_dtst.Clear();
                            string ConnString = salesBookTableAdapter.Connection.ConnectionString;
                            using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                            {
                                using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_sb, conn))
                                {
                                    cmd.CommandType = CommandType.Text;

                                    conn.Open();
                                    SqlCeDataReader reader = cmd.ExecuteReader();

                                    using (reader)
                                    {
                                        salesBook_dtst.Load(reader, LoadOption.PreserveChanges, "SalesBook");
                                        dgv4.DataSource = salesBook_dtst.SalesBook;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Datatb4 = asql.Go_To_Position(Main.Amatrix.acc, Datatb4, "SalesBook", true, smallest4, biggest4);
                            dgv4.DataSource = Datatb4;
                        }
                    }
                }
                catch (Exception erty)
                {
                    smallest4 = smallest4 - 100;
                    biggest4 = biggest4 - 100; Last_Query_Used_sb = "Select * From SalesBook Where [Serial Number] >= '" + smallest4 + "' AND [Serial Number] <= '" + biggest4 + "'";
                        
                    if (Main.Amatrix.acc == "")
                    {
                        salesBook_dtst.Clear();
                        string ConnString = salesBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_sb, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    salesBook_dtst.Load(reader, LoadOption.PreserveChanges, "SalesBook");
                                    dgv4.DataSource = salesBook_dtst.SalesBook;
                                }
                            }
                        }
                    }
                    else
                    {
                        Datatb4 = asql.Go_To_Position(Main.Amatrix.acc, Datatb4, "SalesBook", true, smallest4, biggest4);
                        dgv4.DataSource = Datatb4;
                    }
                }
                try
                {
                    huntme4.Text = dgv4.Rows[0].Cells[0].Value.ToString() + "-" + dgv4.Rows[dgv3.RowCount - 2].Cells[0].Value.ToString();
                }
                catch (Exception erty) { huntme4.Text = smallest4.ToString() + "-" + biggest4.ToString(); }
            }
        }

        private Color cl_tmp;
        private void dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (visual.Default.Basic == false)
            {
                if (sender.Equals(dgv) == true)
                {
                    try
                    {
                        cl_tmp = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.AliceBlue;
                    }
                    catch (Exception erty) { }
                }
                if (sender.Equals(dgv2) == true)
                {
                    try
                    {
                        cl_tmp = dgv2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                        dgv2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.AliceBlue;
                    }
                    catch (Exception erty) { }
                }
                if (sender.Equals(dgv3) == true)
                {
                    try
                    {
                        cl_tmp = dgv3.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                        dgv3.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.AliceBlue;
                    }
                    catch (Exception erty) { }
                }
                if (sender.Equals(dgv4) == true)
                {
                    try
                    {
                        cl_tmp = dgv4.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                        dgv4.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.AliceBlue;
                    }
                    catch (Exception erty) { }
                }
            }
            else { }
        }

        private void dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (visual.Default.Basic == false)
            {
                if (sender.Equals(dgv) == true)
                {
                    try
                    {
                        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = cl_tmp;
                    }
                    catch (Exception erty) { }
                }
                if (sender.Equals(dgv2) == true)
                {
                    try
                    {
                        dgv2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = cl_tmp;
                    }
                    catch (Exception erty) { }
                }
                if (sender.Equals(dgv3) == true)
                {
                    try
                    {
                        dgv3.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = cl_tmp;
                    }
                    catch (Exception erty) { }
                }
                if (sender.Equals(dgv4) == true)
                {
                    try
                    {
                        dgv4.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = cl_tmp;
                    }
                    catch (Exception erty) { }
                }
            }
            else { }
        }

        private void remv_zz_Click(object sender, EventArgs e)
        {
            zz_yn(false);
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
                        
                        dgv2.Font = fnt;
                        dgv2.AutoResizeRows();
                        dgv2.AutoResizeColumns();

                        dgv3.Font = fnt;
                        dgv3.AutoResizeRows();
                        dgv3.AutoResizeColumns();

                        dgv4.Font = fnt;
                        dgv4.AutoResizeRows();
                        dgv4.AutoResizeColumns();
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
                    
                    dgv2.Font = fnt;
                    dgv2.AutoResizeRows();
                    dgv2.AutoResizeColumns();

                    dgv3.Font = fnt;
                    dgv3.AutoResizeRows();
                    dgv3.AutoResizeColumns();

                    dgv4.Font = fnt;
                    dgv4.AutoResizeRows();
                    dgv4.AutoResizeColumns();
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
                        dgv2.Font = fnt;
                        dgv2.AutoResizeRows();
                        dgv2.AutoResizeColumns();

                        dgv3.Font = fnt;
                        dgv3.AutoResizeRows();
                        dgv3.AutoResizeColumns();

                        dgv4.Font = fnt;
                        dgv4.AutoResizeRows();
                        dgv4.AutoResizeColumns();
                    }
                    catch (Exception erty) { }
                }
                else
                {
                    Font fnt = new Font("Microsoft Sans Serif", dgv.Font.Size, FontStyle.Regular);
                    dgv.Font = fnt;
                    dgv.AutoResizeRows();
                    dgv.AutoResizeColumns();
                    dgv2.Font = fnt;
                    dgv2.AutoResizeRows();
                    dgv2.AutoResizeColumns();

                    dgv3.Font = fnt;
                    dgv3.AutoResizeRows();
                    dgv3.AutoResizeColumns();

                    dgv4.Font = fnt;
                    dgv4.AutoResizeRows();
                    dgv4.AutoResizeColumns();
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

        private void gotoitm_Click(object sender, EventArgs e)
        {
            th_search_strt();
        }

        private Thread th_search;
        private delegate void del_search();

        private void th_search_strt()
        {
            try
            {
                th_search = new Thread(new ThreadStart(del_search_strt));
                th_search.IsBackground = true;
                th_search.Start();
            }
            catch (Exception erty) { search(); }
        }

        private void del_search_strt()
        {
            try
            {
                this.Invoke(new del_search(search));
            }
            catch (Exception erty) { search(); };
        }

        private int who_srch = 0;
        private void search()
        {
            oper_save();
            if (who_srch == 0 || who_srch == 1)
            {
                try
                {
                    Last_Query_Used = "Select * FROM journal WHERE [Serial Number] LIKE '%" + tbxfned.Text + "%' OR [Particulars] LIKE '%" + tbxfned.Text + "%' OR [Date of Transaction] LIKE '%" + tbxfned.Text + "%' OR [On Credit] LIKE '%" + tbxfned.Text + "%' OR [Credit] LIKE '%" + tbxfned.Text + "%' OR [Creditor Information] LIKE '%" + tbxfned.Text + "%' OR [Debit] LIKE '%" + tbxfned.Text + "%' OR [Debitor Information] LIKE '%" + tbxfned.Text + "%'";
                    if (Main.Amatrix.acc == "")
                    {
                        journ_dtst.Clear();
                        string ConnString = journalTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                            {
                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                    dgv.DataSource = journ_dtst.journal;
                                }
                            }
                        }
                    }
                    else
                    {
                        Datatb.Clear();
                        Datatb = basql.Execute(Main.Amatrix.acc, Last_Query_Used, "journal", Datatb);
                        dgv.DataSource = Datatb;
                    }
                }
                catch (Exception erty) { general_mssg("Search Has Encountered an Error (JRN) and was Forced to Stop", "Search (Journal)"); }
            }
            //cash
            if (who_srch == 0 || who_srch == 2)
            {
                try
                {
                    Last_Query_Used_cb = "Select * FROM CashBook WHERE [Serial Number] LIKE '%" + tbxfned.Text + "%' OR [Particulars] LIKE '%" + tbxfned.Text + "%' OR [Date] LIKE '%" + tbxfned.Text + "%' OR [Accounts Recievable] LIKE '%" + tbxfned.Text + "%' OR [Accounts Payable] LIKE '%" + tbxfned.Text + "%' OR [Balance] LIKE '%" + tbxfned.Text + "%' OR [Credit] LIKE '%" + tbxfned.Text + "%' OR [Debit] LIKE '%" + tbxfned.Text + "%' OR [Notes] LIKE '%" + tbxfned.Text + "%' OR [Items] LIKE '%" + tbxfned.Text + "%' OR [Number of Items] LIKE '%" + tbxfned.Text + "%' OR [Items (Accounts Recievable)] LIKE '%" + tbxfned.Text + "%' OR [Items (Accounts Payable)] LIKE '%" + tbxfned.Text + "%' OR [Payment Serial Number (if any)] LIKE '%" + tbxfned.Text + "%'";
                    if (Main.Amatrix.acc == "")
                    {
                        cashbook_dtst.Clear();
                        string ConnString = cashBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_cb, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    cashbook_dtst.Load(reader, LoadOption.PreserveChanges, "CashBook");
                                    dgv2.DataSource = cashbook_dtst.CashBook;
                                }
                            }
                        }
                    }
                    else
                    {
                        Datatb2.Clear();
                        Datatb2 = basql.Execute(Main.Amatrix.acc, Last_Query_Used_cb, "CashBook", Datatb2);
                        dgv2.DataSource = Datatb2;
                    }
                }
                catch (Exception erty) { MessageBox.Show(erty.Message); general_mssg("Search Has Encountered an Error (CB) and was Forced to Stop", "Search (CashBook)"); }
            }
            //Purchase book
            if (who_srch == 0 || who_srch == 3)
            {
                try
                {
                    Last_Query_Used_pb = "Select * FROM PurchaseBook WHERE [Serial Number] LIKE '%" + tbxfned.Text + "%' OR [Particulars] LIKE '%" + tbxfned.Text + "%' OR [Date of Purchase] LIKE '%" + tbxfned.Text + "%' OR [Supplier] LIKE '%" + tbxfned.Text + "%' OR [Invoice Number] LIKE '%" + tbxfned.Text + "%' OR [CST (Money Value)] LIKE '%" + tbxfned.Text + "%' OR [Credit] LIKE '%" + tbxfned.Text + "%' OR [Debit] LIKE '%" + tbxfned.Text + "%' OR [Notes] LIKE '%" + tbxfned.Text + "%' OR [VAT (Money Value)] LIKE '%" + tbxfned.Text + "%' OR [Value after VAT] LIKE '%" + tbxfned.Text + "%' OR [Value after CST] LIKE '%" + tbxfned.Text + "%' OR [Balanced Total] LIKE '%" + tbxfned.Text + "%'"; //OR [Payment Serial Number] LIKE '%" + tbxfned.Text + "%'";
                        
                    if (Main.Amatrix.acc == "")
                    {
                        purchaseBook_dtst.Clear();
                        string ConnString = cashBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_pb, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    purchaseBook_dtst.Load(reader, LoadOption.PreserveChanges, "PurchaseBook");
                                    dgv3.DataSource = purchaseBook_dtst.PurchaseBook;
                                }
                            }
                        }
                    }
                    else
                    {
                        Datatb3.Clear();
                        Datatb3 = basql.Execute(Main.Amatrix.acc, Last_Query_Used_pb, "PurchaseBook", Datatb3);
                        dgv3.DataSource = Datatb3;
                    }
                }
                catch (Exception erty) { general_mssg("Search Has Encountered an Error (PB) and was Forced to Stop", "Search (PurchaseBook)"); }
            }
            //SalesBook            
            if (who_srch == 0 || who_srch == 4)
            {
                try
                {
                    Last_Query_Used_sb = "Select * FROM SalesBook WHERE [Serial Number] LIKE '%" + tbxfned.Text + "%' OR [Particulars] LIKE '%" + tbxfned.Text + "%' OR [Date of Sale] LIKE '%" + tbxfned.Text + "%' OR [Due Date] LIKE '%" + tbxfned.Text + "%' OR [Invoice Number] LIKE '%" + tbxfned.Text + "%' OR [CST (Money Value)] LIKE '%" + tbxfned.Text + "%' OR [Credit] LIKE '%" + tbxfned.Text + "%' OR [Notes] LIKE '%" + tbxfned.Text + "%' OR [VAT (Money Value)] LIKE '%" + tbxfned.Text + "%' OR [Value after VAT] LIKE '%" + tbxfned.Text + "%' OR [Value after CST] LIKE '%" + tbxfned.Text + "%' OR [Balanced Total] LIKE '%" + tbxfned.Text + "%'"; //OR [Payment Serial Number] LIKE '%" + tbxfned.Text + "%'";
                        
                    if (Main.Amatrix.acc == "")
                    {
                        salesBook_dtst.Clear();
                        string ConnString = salesBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_sb, conn))
                            {

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    salesBook_dtst.Load(reader, LoadOption.PreserveChanges, "SalesBook");
                                    dgv4.DataSource = salesBook_dtst.SalesBook;
                                }
                            }
                        }
                    }
                    else
                    {
                        Datatb4.Clear();
                        Datatb4 = basql.Execute(Main.Amatrix.acc, Last_Query_Used_sb, "SalesBook", Datatb4);
                        dgv4.DataSource = Datatb4;
                    }
                }
                catch (Exception erty) {general_mssg("Search Has Encountered an Error (SB) and was Forced to Stop", "Search (SalesBook)"); }
            }
        }

        private void IO_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(IO) == true)
                {
                    acc_journ_sett.Default.IO = false;
                    dgv.ReadOnly = false;
                    dgv2.ReadOnly = false;
                    dgv3.ReadOnly = false;
                    dgv4.ReadOnly = false;
                    re_only.Visible = false;
                }
                if (sender.Equals(O) == true)
                {
                    acc_journ_sett.Default.IO = true;
                    dgv.ReadOnly = true;
                    dgv2.ReadOnly = true;
                    dgv3.ReadOnly = true;
                    dgv4.ReadOnly = true;
                    re_only.Visible = true;
                }
                acc_journ_sett.Default.Save();
            }
            catch (Exception erty) { }
        }

        private void nw_rw_Click(object sender, EventArgs e)
        {
            if (winstatus == 3)
            {
                DataRow row;
                DataTable tb = new DataTable();
                row = journ_dtst.journal.NewRow();
                row[1] = maxm;
                tb = journ_dtst.journal;
                tb.Rows.Add(row);
                dgv.DataSource = tb;
            }
            if (winstatus == 2)
            {
                DataRow row;
                DataTable tb = new DataTable();
                row = cashbook_dtst.CashBook.NewRow();
                row[1] = maxm2;
                tb = cashbook_dtst.CashBook;
                tb.Rows.Add(row);
                dgv2.DataSource = tb;
            }
            if (winstatus == 4)
            {
                DataRow row;
                DataTable tb = new DataTable();
                row = purchaseBook_dtst.PurchaseBook.NewRow();
                row[1] = maxm3;
                tb = purchaseBook_dtst.PurchaseBook;
                tb.Rows.Add(row);
                dgv3.DataSource = tb;
            }
        }

        private void ldb_Click(object sender, EventArgs e)
        {
            Wizard_sdf wsdf = new Wizard_sdf();
            wsdf.Show();
        }

        //winstatus element cash book
        private void bindingNavigator1_Click(object sender, EventArgs e)
        {
            try
            {
                winstatus = 2;
                winwin = 2;
                dgvwintic.Stop();
                dgvwintic3.Stop();
                tmex.Stop();
                tmex3.Stop();
                calculateValueAfterVatAndCSTToolStripMenuItem.Enabled = false;
            }
            catch (Exception exct)
            { }
            toolStripButton13.Enabled = false;
            summpnl.BringToFront();
            if (zz.Visible == true) { zz.BringToFront(); }
        }

        private void svebtn_alone_ButtonClick(object sender, EventArgs e)
        {
            if (sender.Equals(svewin3) == true)
            {
                if (Main.Amatrix.acc == "")
                {
                    journalTableAdapter.Update(journ_dtst);
                }
                else
                {
                    asql.Save(Datatb, "journal", Main.Amatrix.acc);
                }
            }
            if (sender.Equals(svewin2) == true)
            {
                cashBookTableAdapter.Update(cashbook_dtst);
                purchaseBookTableAdapter.Update(purchaseBook_dtst);
                salesBookTableAdapter.Update(salesBook_dtst);
                asql.Save(Datatb2, "CashBook", Main.Amatrix.acc);
                asql.Save(Datatb3, "PurchaseBook", Main.Amatrix.acc);
                asql.Save(Datatb4, "SalesBook", Main.Amatrix.acc);
            }
        }

        private string SqlString = "";
        private void bydates_Click(object sender, EventArgs e)
        {
            try
            {
                Last_Query_Used_cb = "";
                oper_save();
                if (sender.Equals(toolStripMenuItem243) == true)
                {
                    Last_Query_Used_cb = "Select * FROM CashBook WHERE DATEPART(mm, [Date]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Date]) = DATEPART(yy, GETDATE())";
                }
                if (sender.Equals(toolStripButton64) == true)
                {
                    Last_Query_Used_cb = "Select * FROM CashBook WHERE DATEPART(dd, [Date]) = " + toolStripComboBox18.Text;
                }
                if (sender.Equals(toolStripButton65) == true)
                {
                    Last_Query_Used_cb = "Select * FROM CashBook WHERE DATEPART(mm, [Date]) = " + toolStripComboBox19.Text;
                }
                if (sender.Equals(toolStripButton66) == true)
                {
                    Last_Query_Used_cb = "Select * FROM CashBook WHERE DATEPART(yy, [Date]) = " + toolStripTextBox37.Text;
                }
                if (sender.Equals(toolStripButton67) == true)
                {
                    Last_Query_Used_cb = "Select * FROM CashBook WHERE [Date] > '" + toolStripTextBox38.Text + "' AND [Date] < '" + toolStripTextBox39.Text + "'";
                }
                try
                {
                    if (Main.Amatrix.acc == "")
                    {
                        cashbook_dtst.Clear();
                        string ConnString = cashBookTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_cb, conn))
                            {
                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    cashbook_dtst.Load(reader, LoadOption.PreserveChanges, "CashBook");
                                    dgv2.DataSource = cashbook_dtst.CashBook;
                                }
                            }
                        }
                    }
                    else
                    {
                        Datatb2.Clear();
                        Datatb2 = basql.Execute(Main.Amatrix.acc, Last_Query_Used_cb, "CashBook", Datatb2);
                        dgv2.DataSource = Datatb2;
                    }
                }
                catch (Exception errty)
                {
                    Am_err mer = new Am_err(); mer.tx(errty.Message);
                }
            }
            catch (Exception erty) { general_mssg("Amatrix was Unable to Execute the Specified Query", erty.Source); }
        }


        private void cshsumqry_Click(object sender, EventArgs e)
        {
            try
            {
                oper_save(); Last_Query_Used_cb = "";
                if (sender.Equals(toolStripMenuItem239) == true)
                {
                    Last_Query_Used_cb = "Select sum(Debit) Debit, sum(Credit) Credit, sum([Serial Number]) [Serial Number], sum([Accounts Recievable]) [Accounts Recievable], sum([Accounts Payable]) [Accounts Payable], sum(Balance) Balance FROM CashBook";
                }
                else if (sender.Equals(toolStripButton261) == true)
                {
                    Last_Query_Used_cb = "Select sum(Debit) Debit, sum(Credit) Credit, sum([Serial Number]) [Serial Number], sum([Accounts Recievable]) [Accounts Recievable], sum([Accounts Payable]) [Accounts Payable], sum(Balance) Balance FROM CashBook WHERE [Date] > '" + toolStripTextBox183.Text + "' AND [Date] < '" + toolStripTextBox184.Text + "'";
                }

                if (Main.Amatrix.acc == "")
                {
                    cashbook_dtst.Clear();
                    string ConnString = cashBookTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_cb, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                cashbook_dtst.Load(reader, LoadOption.PreserveChanges, "CashBook");
                                dgv2.DataSource = cashbook_dtst.CashBook;
                            }
                        }
                    }
                }
                else
                {
                    Datatb2.Clear();
                    Datatb2 = basql.Execute(Main.Amatrix.acc, Last_Query_Used_cb, "CashBook", Datatb2);
                    dgv2.DataSource = Datatb2;
                }
            }
            catch (Exception erty) { MessageBox.Show(erty.Message); general_mssg("Amatrix was Unable to Execute the Specified Query", erty.Source); }
        }

        private void cshavgqry_Click(object sender, EventArgs e)
        {
            try
            {
                oper_save(); Last_Query_Used_cb = "";
                if (sender.Equals(toolStripMenuItem240) == true)
                {
                    Last_Query_Used_cb = "Select avg(Debit) Debit, avg(Credit) Credit, avg([Serial Number]) [Serial Number], avg([Accounts Recievable]) [Accounts Recievable], avg([Accounts Payable]) [Accounts Payable], avg(Balance) Balance FROM CashBook";
                }
                else if (sender.Equals(toolStripButton262) == true)
                {
                    Last_Query_Used_cb = "Select avg(Debit) Debit, avg(Credit) Credit, avg([Serial Number]) [Serial Number], avg([Accounts Recievable]) [Accounts Recievable], avg([Accounts Payable]) [Accounts Payable], avg(Balance) Balance FROM CashBook WHERE [Date] > '" + toolStripTextBox185.Text + "' AND [Date] < '" + toolStripTextBox186.Text + "'";
                }
                if (Main.Amatrix.acc == "")
                {
                    cashbook_dtst.Clear();
                    string ConnString = cashBookTableAdapter.Connection.ConnectionString; using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_cb, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                cashbook_dtst.Load(reader, LoadOption.PreserveChanges, "CashBook");
                                dgv2.DataSource = cashbook_dtst.CashBook;
                            }
                        }
                    }
                }
                else
                {
                    Datatb2.Clear();
                    Datatb2 = basql.Execute(Main.Amatrix.acc, Last_Query_Used_cb, "CashBook", Datatb2);
                    dgv2.DataSource = Datatb2;
                }
            }
            catch (Exception erty) { general_mssg("Amatrix was Unable to Execute the Specified Query", erty.Source); }
        }

        private void pb_click(object sender, EventArgs e)
        {
            try
            {
                winstatus = 4;
                winwin = 4;
                dgvwintic.Stop();
                dgvwintic3.Stop();
                tmex.Stop();
                tmex3.Stop();
            }
            catch (Exception exct)
            { }
            calculateValueAfterVatAndCSTToolStripMenuItem.Enabled = true;
            toolStripButton13.Enabled = true;
            summpnl.BringToFront();
            if (zz.Visible == true) { zz.BringToFront(); }
        }

        private void choose_src(object sender, EventArgs e)
        {
            if (sender.Equals(allToolStripMenuItem) == true)
            {
                who_srch = 0;
            }

            if (sender.Equals(generalJournalToolStripMenuItem) == true)
            {
                who_srch = 1;
            }

            if (sender.Equals(cashBookToolStripMenuItem) == true)
            {
                who_srch = 2;
            }

            if (sender.Equals(purchaseBookToolStripMenuItem) == true)
            {
                who_srch = 3;
            }

            if (sender.Equals(salesBookToolStripMenuItem1) == true)
            {
                who_srch = 4;
            }
            who_is_srch();
        }

        private void who_is_srch()
        {
            if (who_srch == 0)
            {
                allToolStripMenuItem.Image = Properties.Resources.tick;
                generalJournalToolStripMenuItem.Image = null;
                cashBookToolStripMenuItem.Image = null;
                purchaseBookToolStripMenuItem.Image = null;
                salesBookToolStripMenuItem1.Image = null;

            }
            else if (who_srch == 1)
            {
                allToolStripMenuItem.Image = null;
                generalJournalToolStripMenuItem.Image = Properties.Resources.tick;
                cashBookToolStripMenuItem.Image = null;
                purchaseBookToolStripMenuItem.Image = null;
                salesBookToolStripMenuItem1.Image = null;
            }
            else if (who_srch == 2)
            {
                allToolStripMenuItem.Image = null;
                generalJournalToolStripMenuItem.Image = null;
                cashBookToolStripMenuItem.Image = Properties.Resources.tick;
                purchaseBookToolStripMenuItem.Image = null;
                salesBookToolStripMenuItem1.Image = null;
            }
            else if (who_srch == 3)
            {
                allToolStripMenuItem.Image = null;
                generalJournalToolStripMenuItem.Image = null;
                cashBookToolStripMenuItem.Image = null;
                purchaseBookToolStripMenuItem.Image = Properties.Resources.tick;
                salesBookToolStripMenuItem1.Image = null;
            }
            else if (who_srch == 4)
            {
                allToolStripMenuItem.Image = null;
                generalJournalToolStripMenuItem.Image = null;
                cashBookToolStripMenuItem.Image = null;
                purchaseBookToolStripMenuItem.Image = null;
                salesBookToolStripMenuItem1.Image = Properties.Resources.tick;
            }
        }

        private void pb_bydateqry(object sender, EventArgs e)
        {
            try
            {
                oper_save();
                if (sender.Equals(toolStripMenuItem369) == true)
                {
                    Last_Query_Used_pb = "Select * FROM PurchaseBook WHERE DATEPART(mm, [Date of Purchase]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Date of Purchase]) = DATEPART(yy, GETDATE())";
                }
                if (sender.Equals(toolStripButton116) == true)
                {
                    Last_Query_Used_pb = "Select * FROM PurchaseBook WHERE DATEPART(dd, [Date of Purchase]) = " + toolStripComboBox32.Text;
                }
                if (sender.Equals(toolStripButton117) == true)
                {
                    Last_Query_Used_pb = "Select * FROM PurchaseBook WHERE DATEPART(mm, [Date of Purchase]) = " + toolStripComboBox33.Text;
                }
                if (sender.Equals(toolStripButton118) == true)
                {
                    Last_Query_Used_pb = "Select * FROM PurchaseBook WHERE DATEPART(yy, [Date of Purchase]) = " + toolStripTextBox75.Text;
                }
                if (sender.Equals(toolStripButton119) == true)
                {
                    Last_Query_Used_pb = "Select * FROM PurchaseBook WHERE [Date of Purchase] > '" + toolStripTextBox76.Text + "' AND [Date of Purchase] < '" + toolStripTextBox77.Text + "'";
                }
                if (Main.Amatrix.acc == "")
                {
                    purchaseBook_dtst.Clear();
                    string ConnString = purchaseBookTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_pb, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                purchaseBook_dtst.Load(reader, LoadOption.PreserveChanges, "PurchaseBook");
                                dgv3.DataSource = purchaseBook_dtst.PurchaseBook;
                            }
                        }
                    }
                }
                else
                {
                    Datatb3.Clear();
                    Datatb3 = basql.Execute(Main.Amatrix.acc, Last_Query_Used_pb, "PurchaseBook", Datatb3);
                    dgv3.DataSource = Datatb3;
                }
            }
            catch (Exception erty) { general_mssg("Amatrix was Unable to Execute the Specified Query", erty.Source); }
        }

        private void agreg_purqry(object sender, EventArgs e)
        {
            try
            {
                oper_save();
                if (sender.Equals(toolStripMenuItem365) == true)
                {
                    Last_Query_Used_pb = "Select sum([CST (Money Value)]) [CST (Money Value)], sum([VAT (Money Value)]) [VAT (Money Value)], sum(Debit) Debit, sum(Credit) Credit, sum([Serial Number]) [Serial Number], sum([Balanced Total]) [Balanced Total], sum([Value after CST]) [Value after CST], sum([Value after VAT]) [Value after VAT] FROM PurchaseBook";
                }
                if (sender.Equals(toolStripMenuItem366) == true)
                {
                    Last_Query_Used_pb = "Select avg([CST (Money Value)]) [CST (Money Value)], avg([VAT (Money Value)]) [VAT (Money Value)], avg(Debit) Debit, avg(Credit) Credit, avg([Serial Number]) [Serial Number], avg([Balanced Total]) [Balanced Total], avg([Value after CST]) [Value after CST], avg([Value after VAT]) [Value after VAT] FROM PurchaseBook";
                }
                //sum
                if (sender.Equals(toolStripButton263) == true)
                {
                    Last_Query_Used_pb = "Select sum([CST (Money Value)]) [CST (Money Value)], sum([VAT (Money Value)]) [VAT (Money Value)], sum(Debit) Debit, sum(Credit) Credit, sum([Serial Number]) [Serial Number], sum([Balanced Total]) [Balanced Total], sum([Value after CST]) [Value after CST], sum([Value after VAT]) [Value after VAT] FROM PurchaseBook WHERE [Date of Purchase] > '" + toolStripTextBox187.Text + "' AND [Date of Purchase] < '" + toolStripTextBox188.Text + "'";
                }
                //avg
                if (sender.Equals(toolStripButton264) == true)
                {
                    Last_Query_Used_pb = "Select avg([CST (Money Value)]) [CST (Money Value)], avg([VAT (Money Value)]) [VAT (Money Value)], avg(Debit) Debit, avg(Credit) Credit, avg([Serial Number]) [Serial Number], avg([Balanced Total]) [Balanced Total], avg([Value after CST]) [Value after CST], avg([Value after VAT]) [Value after VAT] FROM PurchaseBook WHERE [Date of Purchase] > '" + toolStripTextBox189.Text + "' AND [Date of Purchase] < '" + toolStripTextBox190.Text + "'";
                }
                if (Main.Amatrix.acc == "")
                {
                    purchaseBook_dtst.Clear();
                    string ConnString = purchaseBookTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_pb, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                purchaseBook_dtst.Load(reader, LoadOption.PreserveChanges, "PurchaseBook");
                                dgv3.DataSource = purchaseBook_dtst.PurchaseBook;
                            }
                        }
                    }
                }
                else
                {
                    Datatb3.Clear();
                    Datatb3 = basql.Execute(Main.Amatrix.acc, Last_Query_Used_pb, "PurchaseBook", Datatb3);
                    dgv3.DataSource = Datatb3;
                }
            }
            catch (Exception ertyy)
            {
                general_mssg("Amatrix was Unable to Execute the Specified Query", ertyy.Source);
            }
        }

        private void dgv4_click(object sender, EventArgs e)
        {
            try
            {
                winstatus = 5;
                winwin = 5;
                dgvwintic.Stop();
                dgvwintic3.Stop();
                tmex.Stop();
                tmex3.Stop();
            }
            catch (Exception exct){ }
            calculateValueAfterVatAndCSTToolStripMenuItem.Enabled = true;
            toolStripButton13.Enabled = true;
            summpnl.BringToFront();
            if (zz.Visible == true) { zz.BringToFront(); }
        }

        private void smmavg_sls(object sender, EventArgs e)
        {
            try
            {
                oper_save();
                if (sender.Equals(toolStripMenuItem496) == true)
                {
                    Last_Query_Used_sb = "Select sum([CST (Money Value)]) [CST (Money Value)], sum([VAT (Money Value)]) [VAT (Money Value)], sum(Credit) Credit, sum([Serial Number]) [Serial Number], sum([Balanced Total]) [Balanced Total], sum([Value after CST]) [Value after CST], sum([Value after VAT]) [Value after VAT] FROM SalesBook";
                }

                if (sender.Equals(toolStripMenuItem497) == true)
                {
                    Last_Query_Used_sb = "Select avg([CST (Money Value)]) [CST (Money Value)], avg([VAT (Money Value)]) [VAT (Money Value)], avg(Credit) Credit, avg([Serial Number]) [Serial Number], avg([Balanced Total]) [Balanced Total], avg([Value after CST]) [Value after CST], avg([Value after VAT]) [Value after VAT] FROM SalesBook";
                }
                //sum
                if (sender.Equals(toolStripButton265) == true)
                {
                    Last_Query_Used_sb = "Select sum([CST (Money Value)]) [CST (Money Value)], sum([VAT (Money Value)]) [VAT (Money Value)], sum(Credit) Credit, sum([Serial Number]) [Serial Number], sum([Balanced Total]) [Balanced Total], sum([Value after CST]) [Value after CST], sum([Value after VAT]) [Value after VAT] FROM SalesBook WHERE [Date of Sale] > '" + toolStripTextBox191.Text + "' AND [Date of Sale] < '" + toolStripTextBox192.Text + "'";
                }
                //avg
                if (sender.Equals(toolStripButton266) == true)
                {
                    Last_Query_Used_sb = "Select avg([CST (Money Value)]) [CST (Money Value)], avg([VAT (Money Value)]) [VAT (Money Value)], avg(Credit) Credit, avg([Serial Number]) [Serial Number], avg([Balanced Total]) [Balanced Total], avg([Value after CST]) [Value after CST], avg([Value after VAT]) [Value after VAT] FROM SalesBook WHERE [Date of Sale] > '" + toolStripTextBox193.Text + "' AND [Date of Sale] < '" + toolStripTextBox194.Text + "'";
                }
                if (Main.Amatrix.acc == "")
                {
                    salesBook_dtst.Clear();
                    string ConnString = salesBookTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_sb, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();//)

                            using (reader)
                            {
                                salesBook_dtst.Load(reader, LoadOption.PreserveChanges, "SalesBook");
                                dgv4.DataSource = salesBook_dtst.SalesBook;
                            }
                        }
                    }
                }
                else
                {
                    Datatb4.Clear();
                    Datatb4 = basql.Execute(Main.Amatrix.acc, Last_Query_Used_sb, "SalesBook", Datatb4);
                    dgv4.DataSource = Datatb4;
                }
            }
            catch (Exception ertyy)
            {
                general_mssg("Amatrix was Unable to Execute the Specified Query", ertyy.Source);
            }
        }

        private void dtes_slsqry(object sender, EventArgs e)
        {
            try
            {
                oper_save();
                if (sender.Equals(toolStripMenuItem500) == true)
                {
                    Last_Query_Used_sb = "Select * FROM SalesBook WHERE DATEPART(mm, [Date of Sale]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Date of Sale]) = DATEPART(yy, GETDATE())";
                }
                if (sender.Equals(toolStripButton168) == true)
                {
                    Last_Query_Used_sb = "Select * FROM SalesBook WHERE DATEPART(dd, [Date of Sale]) = " + toolStripComboBox46.Text;
                }
                if (sender.Equals(toolStripButton169) == true)
                {
                    Last_Query_Used_sb = "Select * FROM SalesBook WHERE DATEPART(mm, [Date of Sale]) = " + toolStripComboBox47.Text;
                }
                if (sender.Equals(toolStripButton170) == true)
                {
                    Last_Query_Used_sb = "Select * FROM SalesBook WHERE DATEPART(yy, [Date of Sale]) = " + toolStripTextBox113.Text;
                }
                if (sender.Equals(toolStripButton171) == true)
                {
                    Last_Query_Used_sb = "Select * FROM SalesBook WHERE [Date of Sale] > '" + toolStripTextBox114.Text + "' AND [Date of Sale] < '" + toolStripTextBox115.Text + "'";
                }
                if (Main.Amatrix.acc == "")
                {
                    salesBook_dtst.Clear();
                    string ConnString = salesBookTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_sb, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                salesBook_dtst.Load(reader, LoadOption.PreserveChanges, "SalesBook");
                                dgv4.DataSource = salesBook_dtst.SalesBook;
                            }
                        }
                    }
                }
                else
                {
                    Datatb4.Clear();
                    Datatb4 = basql.Execute(Main.Amatrix.acc, Last_Query_Used_sb, "SalesBook", Datatb4);
                    dgv4.DataSource = Datatb4;
                }
            }
            catch (Exception ertyy)
            {
                Am_err ner = new Am_err();
                ner.tx(ertyy.Message);
                general_mssg("Amatrix was Unable to Execute the Specified Query", ertyy.Source);
            }
        }

        private void dudte_currsls(object sender, EventArgs e)
        {
            try
            {
                oper_save();
                if (sender.Equals(toolStripMenuItem721) == true)
                {
                    Last_Query_Used_sb = "Select * FROM SalesBook WHERE DATEPART(mm, [Due Date]) = DATEPART(mm, GETDATE()) AND DATEPART(yy, [Due Date]) = DATEPART(yy, GETDATE())";
                }
                if (sender.Equals(toolStripButton254) == true)
                {
                    Last_Query_Used_sb = "Select * FROM SalesBook WHERE DATEPART(dd, [Due Date]) = " + toolStripComboBox90.Text;
                }
                if (sender.Equals(toolStripButton255) == true)
                {
                    Last_Query_Used_sb = "Select * FROM SalesBook WHERE DATEPART(mm, [Due Date]) = " + toolStripComboBox91.Text;
                }
                if (sender.Equals(toolStripButton256) == true)
                {
                    Last_Query_Used_sb = "Select * FROM SalesBook WHERE DATEPART(yy, [Due Date]) = " + toolStripTextBox180.Text;
                }
                if (sender.Equals(toolStripButton257) == true)
                {
                    Last_Query_Used_sb = "Select * FROM SalesBook WHERE [Due Date] > '" + toolStripTextBox181.Text + "' AND [Due Date] < '" + toolStripTextBox182.Text + "'";
                }
                if (Main.Amatrix.acc == "")
                {
                    salesBook_dtst.Clear();
                    string ConnString = salesBookTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_sb, conn))
                        {

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                salesBook_dtst.Load(reader, LoadOption.PreserveChanges, "SalesBook");
                                dgv4.DataSource = salesBook_dtst.SalesBook;
                            }
                        }
                    }
                }
                else
                {
                    Datatb4.Clear();
                    Datatb4 = basql.Execute(Main.Amatrix.acc, Last_Query_Used_sb, "SalesBook", Datatb4);
                    dgv4.DataSource = Datatb4;
                }
            }
            catch (Exception ertyy)
            {
                general_mssg("Amatrix was Unable to Execute the Specified Query", ertyy.Source);
            }
        }

        private int extern_opn = 0;
        public void Extern(string Query, string Table_Name)
        {
            try
            {
                oper_save();
                extern_opn = 1;
                if (Table_Name.ToLower() == "cashbook")
                {
                    string ConnString = cashBookTableAdapter.Connection.ConnectionString;
                    string Last_Query_Used_cb = Query;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_cb, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                cashbook_dtst.Load(reader, LoadOption.PreserveChanges, "CashBook");
                                dgv2.DataSource = cashbook_dtst.CashBook;
                            }
                        }
                    }
                    tbcol.SelectTab(1);
                }
                if (Table_Name.ToLower() == "purchasebook")
                {
                    string ConnString = purchaseBookTableAdapter.Connection.ConnectionString;
                    string Last_Query_Used_pb = Query;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_pb, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                purchaseBook_dtst.Load(reader, LoadOption.PreserveChanges, "PurchaseBook");
                                dgv3.DataSource = purchaseBook_dtst.PurchaseBook;
                            }
                        }
                    }
                    tbcol.SelectTab(2);
                }
                if (Table_Name.ToLower() == "salesbook")
                {
                    salesBook_dtst.Clear();
                    string ConnString = salesBookTableAdapter.Connection.ConnectionString;
                    string Last_Query_Used_sb = Query;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_sb, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();
                            using (reader)
                            {
                                salesBook_dtst.Load(reader, LoadOption.PreserveChanges, "SalesBook");
                                dgv4.DataSource = salesBook_dtst.SalesBook;
                            }
                        }
                    }
                    tbcol.SelectTab(3);
                }
            }
            catch (Exception erty)
            {
                general_mssg("Amatrix was Unable to Execute the Specified External Query", erty.Source);
            }
        }

        int bubu = 2;
        private void journaddrwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 50000; i++)
            {
                bubu = bubu + 1;
                DataRow row;

                DataTable tb = new DataTable();
                row = journ_dtst.journal.NewRow();
                row[0] = bubu;
                if (i < 5000)
                {
                    row[1] = "09-Oct-91";
                }
                else
                {
                    row[1] = "01-Dec-11";
                }
                row[7] = 12;
                row[9] = 9;
                tb = journ_dtst.journal;
                tb.Rows.Add(row);
                dgv.DataSource = tb;
            }
        }

        private void ocb_Click(object sender, EventArgs e)
        {
            gadg_custombook cb = new gadg_custombook();
            pnl_journvw.Controls.Add(cb);
            cb.BringToFront();
        }

        private void gtb_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(dgv.Rows[0].Cells[0].Value) == 1)
            {
            }
            else
            {
                Last_Query_Used = "Select * From journal Where [Serial Number] >= 1 AND [Serial Number] <= 100";
                if (Main.Amatrix.acc == "")
                {
                    biggest = 100;
                    smallest = 1;
                    journ_dtst.Clear();
                    string ConnString = journalTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                journ_dtst.Load(reader, LoadOption.PreserveChanges, "journal");
                                dgv.DataSource = journ_dtst.journal;
                            }
                        }
                    }
                }
                else
                {
                    Datatb.Clear();
                    Datatb = asql.Go_To_Position(Main.Amatrix.acc, Datatb, "journal", true, 0, 101);
                    dgv.DataSource = Datatb;
                }
            }
        }

        Control contmp = new Control();
        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // betweenToolStripMenuItem1.DropDown.Show(tsjrnstat, 0, 0);
        }

        private void toolStripDropDownButton9_DropDownOpening(object sender, EventArgs e)
        {
            winstatus = 3;
        }

        private void journalToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (sender.Equals(journalToolStripMenuItem4) == true)
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dgv);
            }
            else if (sender.Equals(cashBookToolStripMenuItem1) == true)
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dgv2);
            }
            else if (sender.Equals(purchaseBookToolStripMenuItem1) == true)
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dgv3);
            }
            else if (sender.Equals(salesBookToolStripMenuItem) == true)
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dgv4);
            }
        }

        private void toolStripButton259_Click(object sender, EventArgs e)
        {
            Calculator_1 calc = new Calculator_1();
        }

        private double vat, cst;
        private void f_of_x(object sender, EventArgs e)
        {
            if (sender.Equals(calculateValueAfterVatAndCSTToolStripMenuItem) == true)
            {
                if (winstatus == 4)
                {
                    try
                    {
                        double d = Convert.ToDouble(dgv3[7, dgv3.CurrentRow.Index].Value) - Convert.ToDouble(dgv3[8, dgv3.CurrentRow.Index].Value);
                        try
                        {
                            vat = d + Convert.ToDouble(dgv3[6, dgv3.CurrentRow.Index].Value);
                            cst = d + Convert.ToDouble(dgv3[5, dgv3.CurrentRow.Index].Value);
                            //set
                            dgv3[10, dgv3.CurrentRow.Index].Value = cst;
                            dgv3[9, dgv3.CurrentRow.Index].Value = vat;
                        }
                        catch (Exception ertyyy) { general_mssg("fx Could not Use your VAT and CST Variables so only Your Credit and Debit Values have been Used.", ""); }
                        try
                        {
                            dgv3[11, dgv3.CurrentRow.Index].Value = d + Convert.ToDouble(dgv3[5, dgv3.CurrentRow.Index].Value) + Convert.ToDouble(dgv3[6, dgv3.CurrentRow.Index].Value);
                        }
                        catch (Exception erty) { dgv3[11, dgv3.CurrentRow.Index].Value = d; general_mssg("Balanced Total Could not use VAT and CST Values, Balanced total set to 'Credit' - 'Debit'.", ""); }
                    }
                    catch (Exception ertt) { general_mssg("The Folowing Function could not Be Executed due to Missing Values in Debit and Credit.", ""); }
                }
                if (winstatus == 5)
                {
                    try
                    {
                        double d = Convert.ToDouble(dgv4[8, dgv4.CurrentRow.Index].Value);
                        try
                        {
                            vat = d + Convert.ToDouble(dgv4[7, dgv4.CurrentRow.Index].Value);
                            cst = d + Convert.ToDouble(dgv4[6, dgv4.CurrentRow.Index].Value);
                            //set
                            dgv4[10, dgv4.CurrentRow.Index].Value = cst;
                            dgv4[9, dgv4.CurrentRow.Index].Value = vat;
                        }
                        catch (Exception erty) { general_mssg("fx Could not Use your VAT and CST Variables so only Your Credit and Debit Values have been Used.", ""); }
                        try
                        {
                            dgv4[11, dgv4.CurrentRow.Index].Value = d + Convert.ToDouble(dgv4[7, dgv4.CurrentRow.Index].Value) + Convert.ToDouble(dgv4[6, dgv4.CurrentRow.Index].Value);
                        }
                        catch (Exception erty) { dgv4[11, dgv4.CurrentRow.Index].Value = d; general_mssg("Balanced Total Could not use VAT and CST Values, Balanced total set to 'Credit'.", ""); }
                    }
                    catch (Exception ertt) { general_mssg("The Following Function could not Be Executed due to Missing Values in Debit and Credit.", ""); }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (sender.Equals(button4) == true)
            {
                if (winstatus == 2)
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        dtp_cst_box.Clear();
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp_cst_box.Load(dr);
                        dataGridView1.DataSource = dtp_cst_box;
                        conn.Close();//2
                    }
                    else
                    {
                        dtp_cst_box.Clear();
                        dtp_cst_box = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Prod_mgmt", "Prod_mgmt", dtp_cst_box);
                        dataGridView1.DataSource = dtp_cst_box;
                    }
                }
                else if (winstatus == 5 && dgv4.CurrentCell.ColumnIndex != 4)
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        dtp_cst_box.Clear();
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Customers", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp_cst_box.Load(dr);
                        dataGridView1.DataSource = dtp_cst_box;
                        conn.Close();//2
                    }
                    else
                    {
                        dtp_cst_box.Clear();
                        dtp_cst_box = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Customers", "Customers", dtp_cst_box);
                        dataGridView1.DataSource = dtp_cst_box;
                    }
                }
                else
                {
                    if (Main.Amatrix.acc == "")
                    {
                        invoice_dataset.Clear();
                        SqlCeConnection conn = new SqlCeConnection(invoiceTableAdapter.Connection.ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM invoice", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        invoice_dataset.invoice.Load(dr);
                        dataGridView1.DataSource = invoice_dataset.invoice;
                        conn.Close();
                    }
                    else
                    {
                        dtp_cst_box.Clear();
                        dtp_cst_box = basql.Execute(Main.Amatrix.acc, "SELECT * FROM invoice", "invoice", dtp_cst_box);
                        dataGridView1.DataSource = dtp_cst_box;
                    }
                }
            }
            if (sender.Equals(button5) == true)
            {
                if (winstatus == 2)
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        dtp_cst_box.Clear();
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Prod_mgmt WHERE [Product Name] LIKE '%" + textBox11.Text + "%'", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp_cst_box.Load(dr);
                        dataGridView1.DataSource = dtp_cst_box;
                        conn.Close();//2
                    }
                    else
                    {
                        dtp_cst_box.Clear();
                        dtp_cst_box = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Prod_mgmt WHERE [Product Name] LIKE '%" + textBox11.Text + "%'", "Prod_mgmt", dtp_cst_box);
                        dataGridView1.DataSource = dtp_cst_box;
                    }
                }
                else if (winstatus == 5 && dgv4.CurrentCell.ColumnIndex != 4)
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        dtp_cst_box.Clear();
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Customers WHERE [Corporate Name] LIKE '%" + textBox11.Text + "%'", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        dtp_cst_box.Load(dr);
                        dataGridView1.DataSource = dtp_cst_box;
                        conn.Close();
                    }
                    else
                    {
                        dtp_cst_box.Clear();
                        dtp_cst_box = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Customers WHERE [Corporate Name] LIKE '%" + textBox11.Text + "%'", "Customers", dtp_cst_box);
                        dataGridView1.DataSource = dtp_cst_box;//2
                    }
                }
                else
                {
                    if (Main.Amatrix.acc == "")
                    {
                        invoice_dataset.Clear();
                        SqlCeConnection conn = new SqlCeConnection(invoiceTableAdapter.Connection.ConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM invoice WHERE [Invoice Reference Number (ID)] LIKE '%" + textBox11.Text + "%'", conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        invoice_dataset.invoice.Load(dr);
                        dataGridView1.DataSource = invoice_dataset.invoice;
                        conn.Close();
                    }
                    else
                    {
                        dtp_cst_box.Clear();
                        dtp_cst_box = basql.Execute(Main.Amatrix.acc, "SELECT * FROM invoice WHERE [Invoice Reference Number (ID)] LIKE '%" + textBox11.Text + "%'", "invoice", dtp_cst_box);
                        dataGridView1.DataSource = dtp_cst_box;
                    }
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sender.Equals(dataGridView2) == true)
            {
                if (winstatus == 2)
                {
                    dgv2[3, dgv2.CurrentRow.Index].Value = dataGridView2[0, e.RowIndex].Value;
                }
                if (winstatus == 5)
                {
                    dgv4[1, dgv4.CurrentRow.Index].Value = dataGridView2[0, e.RowIndex].Value;
                }
            }
            else
            {
                if (winstatus == 4)
                {
                    dgv3[3, dgv3.CurrentRow.Index].Value = dataGridView1[1, e.RowIndex].Value;
                }
                if (winstatus == 5)
                {
                    dgv4[4, dgv4.CurrentRow.Index].Value = dataGridView1[1, e.RowIndex].Value;
                }
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (Main.Amatrix.acc == "")
            {
                DataTable table = new DataTable();
                table = (DataTable)dataGridView2.DataSource;
                DataTable table2 = new DataTable();
                using (var con = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString))
                using (var adapter = new SqlCeDataAdapter("SELECT * FROM" + " Customers", con))
                using (new SqlCeCommandBuilder(adapter))
                {
                    adapter.Fill(table2);
                    con.Open();
                    adapter.Update(table);
                }
            }
            else
            {
                DataTable table = new DataTable();
                table = (DataTable)dataGridView2.DataSource;
                DataTable table2 = new DataTable();
                using (var con = new SqlConnection(Main.Amatrix.acc))
                using (var adapter = new SqlDataAdapter("SELECT * FROM" + " Customers", con))
                using (new SqlCommandBuilder(adapter))
                {
                    adapter.Fill(table2);
                    con.Open();
                    adapter.Update(table);
                }
            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            if (label12.Text.ToLower() == "pick an invoice")
            {
                acc_invce aci = new acc_invce();
                aci.Show();
            }
            else if (label12.Text.ToLower() == "pick a product")
            {
                mgmt_supch sh = new mgmt_supch();
                sh.Show();
            }
            else if (label12.Text.ToLower() == "pick a customer")
            {
                mgmt_pr prr = new mgmt_pr();
                prr.Show();
            }
        }

        private void switchDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy_adv adv = new loggy_adv();
            adv.Show();
        }

        private void rePartitionDataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reparttn prtn = new reparttn();
            prtn.Show();
        }

        //DISPOSAL 
        void acc_journ_Disposed(object sender, EventArgs e)
        {
            journalTableAdapter.Connection.Close();
            journ_dtst.Clear();
            journ_dtst.Dispose();
            journalBindingSource.Dispose();
            journalTableAdapter.Dispose();

            cashBookTableAdapter.Connection.Close();
            cashbook_dtst.Clear();
            cashbook_dtst.Dispose();
            cashBookBindingSource.Dispose();
            cashBookTableAdapter.Dispose();

            purchaseBookTableAdapter.Connection.Close();
            purchaseBook_dtst.Clear();
            purchaseBook_dtst.Dispose();
            purchaseBookBindingSource.Dispose();
            purchaseBookTableAdapter.Dispose();

            salesBookTableAdapter.Connection.Close();
            salesBook_dtst.Clear();
            salesBook_dtst.Dispose();
            salesBookTableAdapter.Dispose();
            salesBookBindingSource.Dispose();

            invoiceTableAdapter.Connection.Close();
            invoice_dataset.Clear();
            invoice_dataset.Dispose();
            invoiceBindingSource.Dispose();
            invoiceTableAdapter.Dispose();
            invoicedatasetBindingSource.Dispose();

            dtp_cst_box.Clear();
            dgv.DataSource = null;
            dgv2.DataSource = null;
            dgv3.DataSource = null;
            dgv4.DataSource = null;
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;

            Datatb.Dispose(); 
            Datatb2.Dispose();
            Datatb3.Dispose(); 
            Datatb4.Dispose();

            initconn1.DoWork -= initconn1_DoWork;
            initconn1.RunWorkerCompleted -= initconn1_RunWorkerCompleted;
            init_conn2.DoWork -= init_conn2_DoWork;
            init_conn2.RunWorkerCompleted -= init_conn2_RunWorkerCompleted;
            init_conn3.DoWork -= init_conn3_DoWork;
            init_conn3.RunWorkerCompleted -= init_conn3_RunWorkerCompleted;
            init_conn4.DoWork -= init_conn4_DoWork;
            init_conn4.RunWorkerCompleted -= init_conn4_RunWorkerCompleted;

            bkkinit.RunWorkerCompleted -= bkkinit_RunWorkerCompleted;
            bkk_init2.DoWork -= bkk_init2_DoWork;
            bkk_init3.DoWork -= bkk_init3_DoWork;
            bkk_init4.DoWork -= bkk_init4_DoWork;

            tbcol.SelectedIndexChanged -= tbcol_SelectedIndexChanged;
            rfrsh_dta.Click -= rfrsh_dta_Click;
            toolStripButton13.Click -= toolStripButton13_Click;
            toolStripButton10.Click -= toolStripButton10_Click;
            toolStripSplitButton11.ButtonClick -= dgvupall_Click;
            toolStripSplitButton14.ButtonClick -= dgvupall_Click;
            right2txt.TextChanged -= uptxtdgv2_TextChanged;
            toolStripTextBox80.TextChanged -= uptxtdgv2_TextChanged;
            toolStripTextBox117.TextChanged -= uptxtdgv2_TextChanged;
            toolStripTextBox118.TextChanged -= uptxtdgv2_TextChanged;
            tsjrnstat.Click -= tswin_Click;
            bn1jn.Click -= tswin_Click;
            gadg_resz1.Click -= xuy;
            gadg_resz1.Dispose();
            toolStripTextBox193.Click -= tvtxt20_Click;
            toolStripTextBox194.Click -= tvtxt20_Click;

            tvtxt17.Click -= tvtxt20_Click;
            onlyHeightToolStripMenuItem.Click -= simjrnhgt_Click;
            onlyWidthToolStripMenuItem.Click -= simjrnwth_Click;
            toolStripButton258.Click -= toolStripButton258_Click;
            toolStripMenuItem514.Click -= delete_itms;
            tbxfned.Enter -= tbxfned_Enter;
            tbxfned.Leave -= tbxfned_Leave;
            tbxfned.MouseEnter -= tvtxt1_MouseEnter;
            tbxfned.MouseLeave -= tvtxt1_MouseLeave;
            toolStripButton127.Click -= show_AllToolStripButton_Click_1;
            toolStripButton168.Click -= dtes_slsqry;
            toolStripButton169.Click -= dtes_slsqry;
            toolStripButton170.Click -= dtes_slsqry;
            toolStripButton171.Click -= dtes_slsqry;
            toolStripSplitButton15.ButtonClick -= delete_itms;
            switchDatabaseToolStripMenuItem.Click -= switchDatabaseToolStripMenuItem_Click;
            connectToToolStripMenuItem.Click -= connectToToolStripMenuItem_Click;
            helpToolStripMenuItem1.Click -= contentsToolStripMenuItem_Click;
            contentsToolStripMenuItem.Click -= contentsToolStripMenuItem_Click;
            toolStripTextBox195.Click -= tvtxt20_Click;
            toolStripTextBox195.KeyUp -= tvtxt20_KeyUp;
            toolStripTextBox195.MouseEnter -= tvtxt1_MouseEnter;
            toolStripTextBox195.MouseLeave -= tvtxt1_MouseLeave;
            toolStripTextBox196.Click -= tvtxt20_Click;
            toolStripTextBox196.KeyUp -= tvtxt20_KeyUp;
            toolStripTextBox196.MouseEnter -= tvtxt1_MouseEnter;
            toolStripTextBox196.MouseLeave -= tvtxt1_MouseLeave;
            tvvbtxt16.Click -= tvvbtxt15_Click;
            toolStripTextBox197.Click -= tvtxt20_Click;
            toolStripTextBox197.KeyUp -= tvtxt20_KeyUp;
            toolStripTextBox197.MouseEnter -= tvtxt1_MouseEnter;
            toolStripTextBox197.MouseLeave -= tvtxt1_MouseLeave;
            toolStripTextBox198.Click -= tvtxt20_Click;
            toolStripTextBox198.KeyUp -= tvtxt20_KeyUp;
            toolStripTextBox198.MouseEnter -= tvtxt1_MouseEnter;
            toolStripTextBox198.MouseLeave -= tvtxt1_MouseLeave;
            toolStripButton179.Click -= toolStripButton179_Click;

            cst_box.Leave -= cst_box_Leave;
            this.Deactivate -= this.acc_journ_dec;
            this.Load -= this.acc_journ_Load;
            this.Activated -= this.acc_journ_act;
            this.svebtn.Click -= this.svebtn_ButtonClick;
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
            this.tbcol.Click -= this.tbcol_Click;
            this.tbpges.Click -= this.tbx1_Click;
            this.dgv2.CellBeginEdit -= this.dgv_CellBeginEdit;
            this.dgv2.CellMouseLeave -= this.dgv_CellMouseLeave;
            this.dgv2.CellMouseEnter -= this.dgv_CellMouseEnter;
            this.dgv2.CellEndEdit -= this.dgv_CellEndEdit;
            this.dgv2.DataError -= this.dgv_DataError;
            this.dgv2.CellEnter -= this.dgv_CellEnter;
            this.dgv2.Click -= this.bindingNavigator1_Click;
            this.toolStrip1.Click -= this.bindingNavigator1_Click;
            this.nxtst2.Click -= this.nxtst_Click;
            this.shwhere2.Click -= this.show_AllToolStripButton_Click_1;
            this.prev2.Click -= this.prev_ButtonClick;
            this.bindingNavigator1.Click -= this.bindingNavigator1_Click;
            this.toolStripMenuItem112.Click -= this.show_AllToolStripButton_Click_1;
            this.showall2.Click -= this.show_AllToolStripButton_Click_1;
            this.toolStripMenuItem239.Click -= this.cshsumqry_Click;
            this.toolStripMenuItem240.Click -= this.cshavgqry_Click;
            this.toolStripTextBox183.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox183.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox183.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox183.Click -= this.tvtxt20_Click;
            this.toolStripTextBox184.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox184.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox184.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox184.Click -= this.tvtxt20_Click;
            this.toolStripButton261.Click -= this.cshsumqry_Click;
            this.toolStripTextBox185.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox185.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox185.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox185.Click -= this.tvtxt20_Click;
            this.toolStripTextBox186.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox186.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox186.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox186.Click -= this.tvtxt20_Click;
            this.toolStripButton262.Click -= this.cshavgqry_Click;
            this.toolStripMenuItem243.Click -= this.bydates_Click;
            this.toolStripButton64.Click -= this.bydates_Click;
            this.toolStripButton65.Click -= this.bydates_Click;
            this.toolStripTextBox37.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox37.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox37.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox37.Click -= this.tvtxt20_Click;
            this.toolStripButton66.Click -= this.bydates_Click;
            this.toolStripTextBox38.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox38.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox38.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox38.Click -= this.tvtxt20_Click;
            this.toolStripTextBox39.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox39.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox39.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox39.Click -= this.tvtxt20_Click;
            this.toolStripButton67.Click -= this.bydates_Click;
            this.upall2.Click -= this.dgvupall_Click;
            this.upone2.ButtonClick -= this.dgvupall_Click;
            this.leftall2.Click -= this.dgvupall_Click;
            this.leftone2.ButtonClick -= this.dgvupall_Click;
            this.left2txt.TextChanged -= this.uptxtdgv2_TextChanged;
            this.rightone2.ButtonClick -= this.dgvupall_Click;
            this.right2txt.Click -= this.uptxtdgv2_TextChanged;
            this.rightall2.Click -= this.dgvupall_Click;
            this.downone2.ButtonClick -= this.dgvupall_Click;
            this.downalltwo.Click -= this.dgvupall_Click;
            this.delete2.ButtonClick -= this.delete_itms;
            this.del_all2.Click -= this.delete_itms;
            this.toolStripButton73.Click -= this.newitm_Click;
            this.tabPage1.Click -= this.tbx1_Click;
            this.dgv3.CellBeginEdit -= this.dgv_CellBeginEdit;
            this.dgv3.CellMouseLeave -= this.dgv_CellMouseLeave;
            this.dgv3.CellMouseEnter -= this.dgv_CellMouseEnter;
            this.dgv3.CellEndEdit -= this.dgv_CellEndEdit;
            this.dgv3.DataError -= this.dgv_DataError;
            this.dgv3.CellEnter -= this.dgv_CellEnter;
            this.dgv3.Click -= this.pb_click;
            this.toolStrip2.Click -= this.pb_click;
            this.nxtst3.Click -= this.nxtst_Click;
            this.shwhere3.Click -= this.show_AllToolStripButton_Click_1;
            this.prev3.Click -= this.prev_ButtonClick;
            this.bindingNavigator2.Click -= this.pb_click;
            this.toolStripMenuItem259.Click -= this.show_AllToolStripButton_Click_1;
            this.showall3.Click -= this.show_AllToolStripButton_Click_1;
            this.toolStripMenuItem365.Click -= this.agreg_purqry;
            this.toolStripMenuItem366.Click -= this.agreg_purqry;
            this.toolStripTextBox187.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox187.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox187.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox187.Click -= this.tvtxt20_Click;
            this.toolStripTextBox188.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox188.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox188.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox188.Click -= this.tvtxt20_Click;
            this.toolStripButton263.Click -= this.agreg_purqry;
            this.toolStripTextBox189.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox189.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox189.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox189.Click -= this.tvtxt20_Click;
            this.toolStripTextBox190.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox190.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox190.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox190.Click -= this.tvtxt20_Click;
            this.toolStripButton264.Click -= this.agreg_purqry;
            this.toolStripMenuItem369.Click -= this.pb_bydateqry;
            this.toolStripButton116.Click -= this.pb_bydateqry;
            this.toolStripButton117.Click -= this.pb_bydateqry;
            this.toolStripTextBox75.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox75.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox75.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox75.Click -= this.tvtxt20_Click;
            this.toolStripButton118.Click -= this.pb_bydateqry;
            this.toolStripTextBox76.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox76.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox76.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox76.Click -= this.tvtxt20_Click;
            this.toolStripTextBox77.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox77.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox77.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox77.Click -= this.tvtxt20_Click;
            this.toolStripButton119.Click -= this.pb_bydateqry;
            this.upall3.Click -= this.dgvupall_Click;
            this.upone3.ButtonClick -= this.dgvupall_Click;
            this.leftall3.Click -= this.dgvupall_Click;
            this.leftone3.ButtonClick -= this.dgvupall_Click;
            this.toolStripTextBox79.TextChanged -= this.uptxtdgv2_TextChanged;
            this.rightone3.ButtonClick -= this.dgvupall_Click;
            this.rightall3.Click -= this.dgvupall_Click;
            this.downone3.ButtonClick -= this.dgvupall_Click;
            this.downallthree.Click -= this.dgvupall_Click;
            this.delete3.ButtonClick -= this.delete_itms;
            this.del_all3.Click -= this.delete_itms;
            this.toolStripButton125.Click -= this.newitm_Click;
            this.dgv4.CellBeginEdit -= this.dgv_CellBeginEdit;
            this.dgv4.CellMouseLeave -= this.dgv_CellMouseLeave;
            this.dgv4.CellMouseEnter -= this.dgv_CellMouseEnter;
            this.dgv4.CellEndEdit -= this.dgv_CellEndEdit;
            this.dgv4.DataError -= this.dgv_DataError;
            this.dgv4.CellEnter -= this.dgv_CellEnter;
            this.dgv4.Click -= this.dgv4_click;
            this.toolStrip3.Click -= this.dgv4_click;
            this.nxtst4.Click -= this.nxtst_Click;
            this.prev4.Click -= this.prev_ButtonClick;
            this.bindingNavigator3.Click -= this.dgv4_click;
            this.toolStripMenuItem385.Click -= this.show_AllToolStripButton_Click_1;
            this.toolStripMenuItem386.Click -= this.show_AllToolStripButton_Click_1;
            this.toolStripMenuItem496.Click -= this.smmavg_sls;
            this.toolStripMenuItem497.Click -= this.smmavg_sls;
            this.toolStripTextBox191.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox191.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox191.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox191.Click -= this.tvtxt20_Click;
            this.toolStripTextBox192.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox192.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox192.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox192.Click -= this.tvtxt20_Click;
            this.toolStripButton265.Click -= this.smmavg_sls;
            this.toolStripTextBox193.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox193.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox193.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox194.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox194.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox194.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripButton266.Click -= this.smmavg_sls;
            this.toolStripMenuItem500.Click -= this.dtes_slsqry;
            this.toolStripTextBox113.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox113.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox113.Click -= this.tvtxt20_Click;
            this.toolStripTextBox114.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox114.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox114.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox114.Click -= this.tvtxt20_Click;
            this.toolStripTextBox115.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox115.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox115.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox115.Click -= this.tvtxt20_Click;
            this.toolStripMenuItem721.Click -= this.dudte_currsls;
            this.toolStripButton254.Click -= this.dudte_currsls;
            this.toolStripButton255.Click -= this.dudte_currsls;
            this.toolStripTextBox180.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox180.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox180.Click -= this.tvtxt20_Click;
            this.toolStripButton256.Click -= this.dudte_currsls;
            this.toolStripTextBox181.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox181.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox181.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox181.Click -= this.tvtxt20_Click;
            this.toolStripTextBox182.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox182.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox182.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox182.Click -= this.tvtxt20_Click;
            this.toolStripButton257.Click -= this.dudte_currsls;
            this.toolStripButton173.Click -= this.dgvupall_Click;
            this.toolStripSplitButton11.Click -= this.dgvupall_Click;
            this.toolStripButton174.Click -= this.dgvupall_Click;
            this.toolStripSplitButton12.ButtonClick -= this.dgvupall_Click;
            this.toolStripSplitButton13.ButtonClick -= this.dgvupall_Click;
            this.toolStripButton175.Click -= this.dgvupall_Click;
            this.toolStripSplitButton14.Click -= this.dgvupall_Click;
            this.toolStripButton176.Click -= this.dgvupall_Click;
            this.toolStripButton177.Click -= this.newitm_Click;
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
            this.svewin2.Click -= this.svebtn_alone_ButtonClick;
            this.toolStripButton178.Click -= this.svebtn_ButtonClick;
            this.dgvwin.Click -= this.dgvwin_Click;
            this.dgv.CellBeginEdit -= this.dgv_CellBeginEdit;
            this.dgv.Sorted -= this.dgv_Sorted;
            this.dgv.CellMouseLeave -= this.dgv_CellMouseLeave;
            this.dgv.CellMouseEnter -= this.dgv_CellMouseEnter;
            this.dgv.CellEndEdit -= this.dgv_CellEndEdit;
            this.dgv.DataError -= this.dgv_DataError;
            this.dgv.CellEnter -= this.dgv_CellEnter;
            this.dgv.Click -= this.tswin_Click;
            this.nxtst.Click -= this.nxtst_Click;
            this.shw_here2.Click -= this.show_AllToolStripButton_Click_1;
            this.prev.Click -= this.prev_ButtonClick;
            this.toolStripDropDownButton9.DropDownOpening -= this.toolStripDropDownButton9_DropDownOpening;
            this.shw_here.Click -= this.show_AllToolStripButton_Click_1;
            this.showAllToolStripMenuItem1.Click -= this.show_AllToolStripButton_Click_1;
            this.dbtsumqry.Click -= this.dbtsumqry_Click;
            this.avgqryjourn.Click -= this.avgqryjourn_Click;
            this.toolStripButton4.Click -= this.dbtsumqry_Click;
            this.toolStripButton267.Click -= this.avgqryjourn_Click;
            this.currmnth.Click -= this.currmnth_Click;
            this.tvvbtxt13.Click -= this.tvvbtxt13_Click;
            this.tvvbtxt14.Click -= this.tvvbtxt14_Click;
            this.tvtxt17.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tvtxt17.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tvtxt17.KeyUp -= this.tvtxt20_KeyUp;
            this.tvvbtxt15.Click -= this.tvvbtxt15_Click;
            this.tvtxt18.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tvtxt18.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tvtxt18.KeyUp -= this.tvtxt20_KeyUp;
            this.tvtxt18.Click -= this.tvtxt20_Click;
            this.tvtxt19.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tvtxt19.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tvtxt19.KeyUp -= this.tvtxt20_KeyUp;
            this.tvtxt19.Click -= this.tvtxt20_Click;
            this.dgvupall.Click -= this.dgvupall_Click;
            this.dgvupone.ButtonClick -= this.dgvupall_Click;
            this.dgvleftall.Click -= this.dgvupall_Click;
            this.dgvleftone.ButtonClick -= this.dgvupall_Click;
            this.leftxtdgv.TextChanged -= this.uptxtdgv2_TextChanged;
            this.dgvrightone.ButtonClick -= this.dgvupall_Click;
            this.dgvtxtright.TextChanged -= this.uptxtdgv2_TextChanged;
            this.dgvrightall.Click -= this.dgvupall_Click;
            this.dgvdownone.ButtonClick -= this.dgvupall_Click;
            this.dgvdownall.Click -= this.dgvupall_Click;
            this.delete.ButtonClick -= this.delete_itms;
            this.del_all.Click -= this.delete_itms;
            this.new_rw.Click -= this.newitm_Click;
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
            this.svewin3.Click -= this.svebtn_alone_ButtonClick;
            this.toolStripButton8.Click -= this.svebtn_ButtonClick;
            this.refrjrn.Click -= this.refrjrn_Click;
            this.zz.MouseLeave -= this.zz_MouseLeave;
            this.zz.MouseEnter -= this.zz_MouseEnter;
            this.toolStrip5.MouseEnter -= this.zz_MouseEnter;
            this.toolStrip5.MouseLeave -= this.zz_MouseLeave;
            this.calculateValueAfterVatAndCSTToolStripMenuItem.Click -= this.f_of_x;
            this.calculateTotalToolStripMenuItem.Click -= this.f_of_x;
            this.toolStripButton259.Click -= this.toolStripButton259_Click;
            this.toolStripButton2.Click -= this.undoall_Click;
            this.toolStripButton26.Click -= this.cpy_Click;
            this.toolStripButton5.Click -= this.ct_Click;
            this.toolStripButton6.Click -= this.pster_Click;
            this.toolStripButton9.Click -= this.sall_Click;
            this.toolStripButton3.Click -= this.newitm_Click;
            this.remv_zz.Click -= this.remv_zz_Click;
            this.autsve.Tick -= this.autsvetck;
            this.dgvwintic.Tick -= this.dgvwintc;
            this.saveas.FileOk -= this.saveasfok;
            this.tmex.Tick -= this.tmex_Tick;
            this.saveToolStripMenuItem.Click -= this.svebtn_ButtonClick;
            this.restr.Click -= this.restr_Click;
            this.clsemn.Click -= this.clsejournclc;
            this.undoall.Click -= this.undoall_Click;
            this.cpy.Click -= this.cpy_Click;
            this.ct.Click -= this.ct_Click;
            this.pster.Click -= this.pster_Click;
            this.deletecell.Click -= this.deletecell_Click;
            this.sall.Click -= this.sall_Click;
            this.initializeToolStripMenuItem.Click -= this.initializeToolStripMenuItem_Click;
            this.dynayes.Click -= this.dynayes_Click;
            this.dynano.Click -= this.dynayes_Click;
            this.rePartitionDataBaseToolStripMenuItem.Click -= this.rePartitionDataBaseToolStripMenuItem_Click;
            this.abtmnu.Click -= this.abtmnu_Click;
            this.ocb.Click -= this.ocb_Click;
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
            this.gotoitm.ButtonClick -= this.gotoitm_Click;
            this.allToolStripMenuItem.Click -= this.choose_src;
            this.generalJournalToolStripMenuItem.Click -= this.choose_src;
            this.cashBookToolStripMenuItem.Click -= this.choose_src;
            this.purchaseBookToolStripMenuItem.Click -= this.choose_src;
            this.salesBookToolStripMenuItem1.Click -= this.choose_src;
            this.rsdbord.Click -= this.rsdbord_Click;
            this.dflt_dgvbord.Click -= this.dflt_dgvbord_Click;
            this.O.Click -= this.IO_Click;
            this.IO.Click -= this.IO_Click;
            this.colreordr.Click -= this.colreordr_Click;
            this.colreordflse.Click -= this.colreordflse_Click;
            this.journalToolStripMenuItem4.Click -= this.journalToolStripMenuItem4_Click;
            this.cashBookToolStripMenuItem1.Click -= this.journalToolStripMenuItem4_Click;
            this.purchaseBookToolStripMenuItem1.Click -= this.journalToolStripMenuItem4_Click;
            this.salesBookToolStripMenuItem.Click -= this.journalToolStripMenuItem4_Click;
            this.ascvw.Click -= this.ascvw_Click;
            this.descvw.Click -= this.descvw_Click;
            this.nopeascdesc.Click -= this.show_AllToolStripButton_Click_1;
            this.slctrgb.Click -= this.slctrgb_Click;
            this.fnt_mnu.MouseEnter -= this.Virtual_menu;
            this.tbxfntlv.SelectedIndexChanged -= this.Virtual_Combo;
            this.tsc_fnt_sze.SelectedIndexChanged -= this.Virtual_Combo;
            this.enblhc.Click -= this.enblhc_Click_1;
            this.ewv.Click -= this.ewv_Click;
            this.dwv.Click -= this.dwv_Click;
            this.col.Tick -= this.col_Tick;
            this.tmeinit.Tick -= this.tmeinit_Tick;
            this.ttp_del.Tick -= this.ttp_del_Tick;
            this.bkkinit.DoWork -=  this.bkkinit_DoWork;
            this.button1.Click -= this.button1_Click_3;
            this.button5.Click -= this.button4_Click;
            this.button4.Click -= this.button4_Click;
            this.dataGridView2.CellMouseClick -= this.dataGridView1_CellMouseClick;
            this.dataGridView1.CellMouseClick -= this.dataGridView1_CellMouseClick;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose();
            GC.Collect();
        }

        private void cst_box_Leave(object sender, EventArgs e)
        {
            cst_box.Visible = false;
        }

        private void toolStripButton268_Click(object sender, EventArgs e)
        {
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.tx(this.Name);
        }

        private void connectToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy lg = new loggy();
            lg.Show();
        }

        private void toolStripButton179_Click(object sender, EventArgs e)
        {
            thsum4_strt();
            th_sum2_strt();
            th_sum3_strt();

            try
            {
                bkkinit.RunWorkerAsync();
            }
            catch (Exception erty) { }
            try
            {
                bkk_init2.RunWorkerAsync();
            }
            catch (Exception ertty) { }
            try
            {
                bkk_init3.RunWorkerAsync();
            }
            catch (Exception erty) { }
            try
            {
                bkk_init4.RunWorkerAsync();
            }
            catch (Exception erty) { }
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

        private void toolStripButton258_Click(object sender, EventArgs e)
        {
            gadg_grph grph = new gadg_grph();
            grph.in_form();
        }

        private void bkkinit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            conn();
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

        //new
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            try
            {
                if (winstatus == 5)
                {
                    acc_linker lnk = new acc_linker();
                    lnk.tx(0, dgv4.CurrentRow, "salesbook", this); lnk.ShowDialog();
                }
                else if (winstatus == 4)
                {
                    acc_linker lnk = new acc_linker();
                    lnk.tx(0, dgv3.CurrentRow, "purchasebook", this); lnk.ShowDialog();
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Invalid Operation No Rows Selected."); }
        }

        public void set_linked(int Type, string ofbook, string value)
        {
            if (Type == 0)
            {
                if (ofbook == "salesbook")
                {
                    dgv4.CurrentRow.Cells[4].Value = value;
                }
                else if (ofbook == "purchasebook")
                {
                    dgv3.CurrentRow.Cells[3].Value = value;
                }
            }
            oper_save();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (winstatus == 4)
            {
                if (dgv3.CurrentCell.ColumnIndex == 3)
                {
                    acc_linker lnk = new acc_linker();
                    lnk.tx(0, dgv3.CurrentRow, "purchasebook", this); lnk.ShowDialog();
                }
                else { general_mssg("No Operation Available for the Selected Cell", "Column Index Did Not Verify with Any Environment Variables."); }
            }
            else if (winstatus == 5)
            {
                if (dgv4.CurrentCell.ColumnIndex == 1)
                {
                    mgmt_pr prr = new mgmt_pr();
                    prr.tx(dgv4.CurrentRow.Cells[1].Value.ToString());
                }
                else if (dgv4.CurrentCell.ColumnIndex == 4)
                {
                    acc_linker lnk = new acc_linker();
                    lnk.tx(0, dgv4.CurrentRow, "salesbook", this); lnk.ShowDialog();
                }
                else { general_mssg("No Operation Available for the Selected Cell", "Column Index Did Not Verify with Any Environment Variables."); }
            }
            else if (winstatus == 2)
            {
                if (dgv2.CurrentCell.ColumnIndex == 3)
                {
                    mgmt_supch prd = new mgmt_supch();
                    prd.tx(dgv2.CurrentRow.Cells[3].Value.ToString());
                }
                else { general_mssg("No Operation Available for the Selected Cell", "Column Index Did Not Verify with Any Environment Variables."); }
            }
        }

        Isync.isync_start asnc; string Last_Query_Used_cb, Last_Query_Used_pb, Last_Query_Used_sb;
        bool cancel = false;
        private void rfrsh_dta_Click(object sender, EventArgs e)
        {
            //save_all();
            rfrsh_dta.DisplayStyle = ToolStripItemDisplayStyle.Image;

            dgv.Enabled = false;
            dgv2.Enabled = false;
            dgv3.Enabled = false;
            dgv4.Enabled = false;

            int x, y;
            oper_save();
            if (winstatus == 3)
            {
                x = dgv.CurrentCell.ColumnIndex;
                y = dgv.CurrentRow.Index;
                if (Main.Amatrix.acc == "")
                {
                    journ_dtst.journal.Clear();
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    journ_dtst.journal.Load(dr);
                    conn.Close();
                }
                else
                {
                    Datatb.Clear();
                    SqlConnection conn = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand cmd = new SqlCommand(Last_Query_Used, conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    Datatb.Load(dr);
                    conn.Close();
                }
                dgv.CurrentCell = dgv[x, y];
            }
            if (winstatus == 2)
            {
                x = dgv2.CurrentCell.ColumnIndex;
                y = dgv2.CurrentRow.Index;
                if (Main.Amatrix.acc == "")
                {
                    Datatb2.Clear();
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_cb, conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    Datatb2.Load(dr);
                    conn.Close();
                }
                else
                {
                    cashbook_dtst.Clear();
                    SqlConnection conn = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand cmd = new SqlCommand(Last_Query_Used_cb, conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    cashbook_dtst.CashBook.Load(dr);
                    conn.Close();
                }
                dgv2.CurrentCell = dgv2[x, y];
            }
            if (winstatus == 4)
            {
                x = dgv3.CurrentCell.ColumnIndex;
                y = dgv3.CurrentRow.Index;
                if (Main.Amatrix.acc == "")
                {
                    Datatb3.Clear();
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_pb, conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    Datatb3.Load(dr);
                    conn.Close();
                }
                else
                {
                    purchaseBook_dtst.Clear();
                    SqlConnection conn = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand cmd = new SqlCommand(Last_Query_Used_pb, conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    purchaseBook_dtst.PurchaseBook.Load(dr);
                    conn.Close();
                }
                dgv3.CurrentCell = dgv3[x, y];
            }
            if (winstatus == 5)
            {
                x = dgv4.CurrentCell.ColumnIndex;
                y = dgv4.CurrentRow.Index;
                if (Main.Amatrix.acc == "")
                {
                    Datatb4.Clear();
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used_sb, conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    Datatb4.Load(dr);
                    conn.Close();
                }
                else
                {
                    salesBook_dtst.Clear();
                    SqlConnection conn = new SqlConnection(Main.Amatrix.acc);
                    SqlCommand cmd = new SqlCommand(Last_Query_Used_sb, conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    salesBook_dtst.SalesBook.Load(dr);
                    conn.Close();
                }
                dgv4.CurrentCell = dgv4[x, y];
            }
            dgv.Enabled = true;
            dgv2.Enabled = true;
            dgv3.Enabled = true;
            dgv4.Enabled = true;
        }

        private void tbcol_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
     }
}