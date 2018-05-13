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
using System.Drawing;
using System.Data;
using System.Collections;
using System.Data.SqlServerCe;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class gadg_grph : UserControl
    {
        //objects
        private DataGridView dgv_temp;
        private DataTable dtp;
        private Font Text_FONT;
        private ArrayList al_Data = new ArrayList();
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        string Graph_Mode = "box";

        public gadg_grph()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(gadg_grph_Disposed);
            Init();
        }

        void gadg_grph_Disposed(object sender, EventArgs e)
        {
            pnl_drw1.Invalidate();
            try
            {
                dgv_temp.Dispose();
            }
            catch (Exception ertyt) { }
            try
            {
                dtp.Clear();
                dtp.Dispose();
            }
            catch (Exception erty) { }
            try
            {
                dtp_.Clear();
                dtp_.Dispose();
            }
            catch (Exception erty) { }
            try
            {
                Text_FONT.Dispose();
            }
            catch (Exception erty) { }
            al_Data.Clear();
            try
            {
                bmp_genr.Dispose();
            }
            catch (Exception erty) { }
            VAL.Clear(); VAL_Sort.Clear();

            this.clse.Click -= this.clse_Click;
            this.toolStripButton10.Click -= this.toolStripButton10_Click;
            this.tv.NodeMouseClick -= this.tv_NodeMouseClick;
            this.sfd.FileOk -= this.sfd_FileOk;
            this.Load -= this.gadg_grph_Load;

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
            tv.ExpandAll();
            Set_COLOR();
        }

        public void in_form()
        {
            Form ff = new Form();
            ff.Text = "Amatrix Grapher (Hosted Process)";
            ff.Icon = Properties.Resources.amdsicnico;
            ff.StartPosition = FormStartPosition.CenterScreen;
            this.Dock = DockStyle.Fill;
            ff.Size = new Size(this.Size.Width + 80, this.Size.Height + 80);
            ff.Controls.Add(this);
            ff.Show();
            if (Main.Amatrix.acc != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.acc); pwd.Owner = ff; }
                catch (Exception erty) { }
            }
        }

        private Bitmap bmp_genr;
        private void gadg_grph_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            sfd.ShowDialog();
        }

        private void sfd_FileOk(object sender, CancelEventArgs e)
        {
            Bitmap bmp = new Bitmap(pnl_drw1.Size.Width, pnl_drw1.Size.Height); Rectangle r = new Rectangle();
            r.Height = pnl_drw1.Size.Height; r.X = pnl_drw1.Size.Width;
            r.Width = pnl_drw1.Size.Width; r.Y = pnl_drw1.Size.Height;
            pnl_drw1.DrawToBitmap(bmp, r);
            try
            {
                bmp.Save(sfd.FileName + ".jpg");
            }
            catch (Exception erty) { MessageBox.Show(sfd.FileName + sfd.Filter); }
        }

        private void clse_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        string Node_ = ""; TreeNodeMouseClickEventArgs e;
        private void tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e2)
        {
            if (bkk.IsBusy == false)
            {
                e = e2;
                try
                {
                    bkk.RunWorkerAsync();
                }
                catch (Exception erty) { }
            }
        }

        private void bkk_DoWork(object sender, DoWorkEventArgs e)
        {
            th_init_gr();
        }

        Thread init_gr;
        delegate void del_gr();
        private void th_init_gr()
        {
            init_gr = new Thread(new ThreadStart(del_init_gr));
            init_gr.IsBackground = true;
            init_gr.Start();
        }

        private void del_init_gr()
        {
            this.Invoke(new del_gr(initialize_grapher));
        }

        private void initialize_grapher()
        {
            VAL.Clear();
            if (e.Node.Parent.Text == "General Journal")
            {
                if (e.Node.Text == "Debit Comparison Per Month")
                {
                    Node_ = "General Journal Debit Comparison Per Month For (" + DateTime.Now.Year.ToString() + ")";
                    Process("General Journal", "Debit");
                }
                else if (e.Node.Text == "Credit Comparison Per Month")
                {
                    Node_ = "General Journal Credit Comparison Per Month For (" + DateTime.Now.Year.ToString() + ")";
                    Process("General Journal", "Credit");
                }
                else if (e.Node.Text == "Debit And Credit Totaled Comparison Per Month")
                {
                    Node_ = "General Journal Debit And Credit Totaled Comparison Per Month For (" + DateTime.Now.Year.ToString() + ")";
                    Process("General Journal", "Final");
                }
            }
            else if (e.Node.Parent.Text == "Cash Book")
            {
                if (e.Node.Text == "Debit Comparison Per Month")
                {
                    Node_ = "Cash Book Debit Comparison Per Month For (" + DateTime.Now.Year.ToString() + ")";
                    Process("Cash Book", "Debit");
                }
                else if (e.Node.Text == "Credit Comparison Per Month")
                {
                    Node_ = "Cash Book Credit Comparison Per Month For (" + DateTime.Now.Year.ToString() + ")";
                    Process("Cash Book", "Credit");
                }
                else if (e.Node.Text == "Debit And Credit Totaled Comparison Per Month")
                {
                    Node_ = "Cash Book Debit And Credit Totaled Comparison Per Month For (" + DateTime.Now.Year.ToString() + ")";
                    Process("Cash Book", "Final");
                }
            }
            else if (e.Node.Parent.Text == "Purchase Book")
            {
                if (e.Node.Text == "Debit Comparison Per Month")
                {
                    Node_ = "Purchase Book Debit Comparison Per Month For (" + DateTime.Now.Year.ToString() + ")";
                    Process("Purchase Book", "Debit");
                }
                else if (e.Node.Text == "Credit Comparison Per Month")
                {
                    Node_ = "Purchase Book Credit Comparison Per Month For (" + DateTime.Now.Year.ToString() + ")";
                    Process("Purchase Book", "Credit");
                }
                else if (e.Node.Text == "Debit And Credit Totaled Comparison Per Month")
                {
                    Node_ = "Purchase Book Debit And Credit Totaled Comparison Per Month For (" + DateTime.Now.Year.ToString() + ")";
                    Process("Purchase Book", "Final");
                }
            }
            else if (e.Node.Parent.Text == "Sales Book")
            {
                if (e.Node.Text == "Credit Comparison Per Month")
                {
                    Node_ = "Sales Book Credit Comparison Per Month For (" + DateTime.Now.Year.ToString() + ")";
                    Process("Sales Book", "Credit");
                }
            }
            //invoices
            else if (e.Node.Parent.Text == "Invoices")
            {
                if (e.Node.Text == "Debit Comparison Per Month")
                {
                    Node_ = "Invoices Debit Comparison Per Month For (" + DateTime.Now.Year.ToString() + ")";
                    Process("Invoices", "Debit");
                }
                else if (e.Node.Text == "Credit Comparison Per Month")
                {
                    Node_ = "Invoices Credit Comparison Per Month For (" + DateTime.Now.Year.ToString() + ")";
                    Process("Invoices", "Credit");
                }
            }
        }

        private void Process(String Type, String Debit_Credit)
        {
            string Connection = ""; string Query = ""; string Date_Column = ""; string Predefined = "";
            if (Type == "General Journal")
            {
                if (Main.Amatrix.acc == "")
                {
                    Connection = Properties.Settings.Default.AmdtbseConnectionString;
                }
                else
                {
                    Connection = Main.Amatrix.acc;
                }

                if (Debit_Credit == "Debit")
                {
                    Query = "SELECT sum(Debit) FROM journal WHERE datepart(mm, ["; Date_Column = "Date of Transaction";
                }
                else if (Debit_Credit == "Credit")
                {
                    Query = "SELECT sum(Credit) FROM journal WHERE datepart(mm, ["; Date_Column = "Date of Transaction";
                }
                else
                {
                    Query = "SELECT sum(Debit) - sum(Credit) FROM journal WHERE datepart(mm, ["; Date_Column = "Date of Transaction";
                }
            }
            else if (Type == "Cash Book")
            {
                if (Main.Amatrix.acc == "")
                {
                    Connection = Properties.Settings.Default.AmdtbseConnectionString;
                }
                else
                {
                    Connection = Main.Amatrix.acc;
                }

                if (Debit_Credit == "Debit")
                {
                    Query = "SELECT sum(Debit) FROM [CashBook] WHERE datepart(mm, ["; Date_Column = "Date";
                }
                else if (Debit_Credit == "Credit")
                {
                    Query = "SELECT sum(Credit) FROM [CashBook] WHERE datepart(mm, ["; Date_Column = "Date";
                }
                else { Query = "SELECT  sum(Debit) - sum(Credit) FROM [CashBook] WHERE datepart(mm, ["; Date_Column = "Date"; }
            }
            else if (Type == "Purchase Book")
            {
                if (Main.Amatrix.acc == "")
                {
                    Connection = Properties.Settings.Default.AmdtbseConnectionString;
                }
                else
                {
                    Connection = Main.Amatrix.acc;
                }

                if (Debit_Credit == "Debit")
                {
                    Query = "SELECT sum(Debit) FROM [PurchaseBook] WHERE datepart(mm, ["; Date_Column = "Date of Purchase";
                }
                else if (Debit_Credit == "Credit")
                {
                    Query = "SELECT sum(Credit) FROM [PurchaseBook] WHERE datepart(mm, ["; Date_Column = "Date of Purchase";
                }
                else
                {
                    Query = "SELECT sum(Debit) - sum(Credit) FROM [PurchaseBook] WHERE datepart(mm, ["; Date_Column = "Date of Purchase";
                }
            }
            else if (Type == "Sales Book")
            {
                if (Main.Amatrix.acc == "")
                {
                    Connection = Properties.Settings.Default.AmdtbseConnectionString;
                }
                else
                {
                    Connection = Main.Amatrix.acc;
                }

                Query = "SELECT sum(Credit) FROM [SalesBook] WHERE datepart(mm, ["; Date_Column = "Date of Sale";
            }
            else if (Type == "Invoices")
            {
                if (Main.Amatrix.acc == "")
                {
                    Connection = Properties.Settings.Default.AmdtbseConnectionString;
                }
                else
                {
                    Connection = Main.Amatrix.acc;
                }

                if (Debit_Credit == "Debit")
                {
                    Query = "SELECT sum(Profit) FROM [invoice] WHERE datepart(mm, ["; Date_Column = "Date";
                }
                else
                {
                    Query = "SELECT sum(Cost) FROM [invoice] WHERE datepart(mm, ["; Date_Column = "Date";
                }
            }

            Predefined = Query;
            for (int i = 1; i <= 12; i++)
            {
                Query = Query + Date_Column + "]) = " + i + " AND datepart(yy, [" + Date_Column + "]) = datepart(yy, getdate())";
                Execute(Connection, Query);
                Query = Predefined;
            }

            draw_LINES(Debit_Credit);
        }

        DataTable dtp_ = new DataTable(); ArrayList VAL = new ArrayList(); ArrayList VAL_Sort = new ArrayList();
        private void Execute(String Connection_String, String Query)
        {
            dtp_.Clear();
            SqlCeConnection conn = new SqlCeConnection(Connection_String);
            SqlCeCommand cmd = new SqlCeCommand(Query, conn);
            conn.Open();
            SqlCeDataReader dr = cmd.ExecuteReader();
            dtp_.Load(dr);
            conn.Close();
            try
            {
                VAL.Add(Convert.ToInt32(dtp_.Rows[0].ItemArray[0]));
            }
            catch (Exception erty) { VAL.Add(0); }
            try
            {
                //VAL.Sort();
            }
            catch (Exception erty) { }
        }

        double biggest;
        private void draw_BARS(String C_OR_D)
        {
            double d_DATA = 0; float Y_ = 58f; string s; int ndx, ndx2; float X_ = 110;
            pnl_drw1.Refresh();
            using (Graphics g = pnl_drw1.CreateGraphics())
            {
                Font f = new Font(tv.Font.FontFamily, 12, FontStyle.Regular);
                g.DrawString(Node_, f, Brushes.DimGray, 10, 10);
                g.DrawString("JAN", tv.Font, Brushes.SlateGray, 110, 435);
                g.DrawString("FEB", tv.Font, Brushes.SlateGray, 160, 435);
                g.DrawString("MAR", tv.Font, Brushes.SlateGray, 210, 435);
                g.DrawString("APR", tv.Font, Brushes.SlateGray, 260, 435);
                g.DrawString("MAY", tv.Font, Brushes.SlateGray, 310, 435);
                g.DrawString("JUN", tv.Font, Brushes.SlateGray, 360, 435);
                g.DrawString("JUL", tv.Font, Brushes.SlateGray, 410, 435);
                g.DrawString("AUG", tv.Font, Brushes.SlateGray, 460, 435);
                g.DrawString("SEP", tv.Font, Brushes.SlateGray, 510, 435);
                g.DrawString("OCT", tv.Font, Brushes.SlateGray, 560, 435);
                g.DrawString("NOV", tv.Font, Brushes.SlateGray, 610, 435);
                g.DrawString("DEC", tv.Font, Brushes.SlateGray, 660, 435);
                g.DrawLine(Pens.DimGray, 100, 55, 100, 430);
                g.DrawLine(Pens.DimGray, 100, 430, 700, 430);

                try
                {
                    VAL_Sort = (ArrayList)VAL.Clone();
                    //VAL_Sort = VAL_Sort.Sort();
                }
                catch (Exception erty) { }

                int nxo = VAL_Sort.Count - 1;
                foreach (object o in VAL_Sort)
                {
                    ndx = nxo;//VAL_Sort.IndexOf(VAL_Sort[);
                    try
                    {
                        d_DATA = Convert.ToDouble(VAL_Sort[nxo]);
                        s = d_DATA.ToString();
                        //g.DrawString(s, tv.Font, Brushes.DimGray, 55, Y_);
                        g.DrawString(s, tv.Font, Brushes.SlateGray, 55, 58 + (30*ndx));
                    }
                    catch (Exception erty)
                    {
                        g.DrawString("N.A.", tv.Font, Brushes.SlateGray, 55, Y_);
                    }

                    nxo = nxo - 1;
                   // Y_ = Y_ - 30f;
                    Y_ = Y_ + 30f;
                }

                foreach (object o in VAL)
                {
                    ndx = VAL.IndexOf(o);
                    ndx2 = VAL_Sort.IndexOf(o);
                    if (ndx != -1)
                    {
                        g.FillRectangle(Brushes.SlateGray, X_, 20 + (ndx2 * 30 + 40), 30, 10);
                    }
                    X_ = X_ + 50;
                }
                pnl_drw1.CreateGraphics();
            }
        }

        float X_Line = 110; float Y_Line = 58f; bool done = false; ArrayList reoccur = new ArrayList();
        private void draw_LINES(String C_OR_D)
        {
            double d_DATA = 0; float Y_ = 58f; string s; int ndx, ndx2; float X_ = 110;
            pnl_drw1.Refresh();
            using (Graphics g = pnl_drw1.CreateGraphics())
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Font f = new Font(tv.Font.FontFamily, 12, FontStyle.Regular);
                g.DrawString(Node_, f, Brushes.DimGray, 10, 10);
                g.DrawString("JAN", tv.Font, Brushes.SlateGray, 110, 435);
                g.DrawString("FEB", tv.Font, Brushes.SlateGray, 160, 435);
                g.DrawString("MAR", tv.Font, Brushes.SlateGray, 210, 435);
                g.DrawString("APR", tv.Font, Brushes.SlateGray, 260, 435);
                g.DrawString("MAY", tv.Font, Brushes.SlateGray, 310, 435);
                g.DrawString("JUN", tv.Font, Brushes.SlateGray, 360, 435);
                g.DrawString("JUL", tv.Font, Brushes.SlateGray, 410, 435);
                g.DrawString("AUG", tv.Font, Brushes.SlateGray, 460, 435);
                g.DrawString("SEP", tv.Font, Brushes.SlateGray, 510, 435);
                g.DrawString("OCT", tv.Font, Brushes.SlateGray, 560, 435);
                g.DrawString("NOV", tv.Font, Brushes.SlateGray, 610, 435);
                g.DrawString("DEC", tv.Font, Brushes.SlateGray, 660, 435);
                g.DrawLine(Pens.DimGray, 100, 55, 100, 430);
                g.DrawLine(Pens.DimGray, 100, 430, 700, 430);

                try
                {
                    VAL_Sort = (ArrayList)VAL.Clone();
                    VAL_Sort.Sort();
                }
                catch (Exception erty) { }

                foreach (object o in VAL_Sort)
                {
                    ndx = VAL_Sort.IndexOf(o);
                    try
                    {
                        d_DATA = Convert.ToDouble(o);
                        s = d_DATA.ToString();
                        if (invert.Checked == false)
                        {
                            g.DrawString(s, tv.Font, Brushes.DimGray, 55, 58 + (30 * ndx));
                        }
                        else
                        {
                            g.DrawString(s, tv.Font, Brushes.DimGray, 55, 384 - (30 * ndx));
                        }
                    }
                    catch (Exception erty)
                    {
                        g.DrawString("N.A.", tv.Font, Brushes.SlateGray, 55, Y_);
                    }

                    // Y_ = Y_ - 30f;
                    if (invert.Checked == false)
                    {
                        Y_ = Y_ + 30f;
                    }
                    else { Y_ = Y_ - 30f; }
                }

                foreach (object o in VAL)
                {
                    ndx = VAL.IndexOf(o);
                    ndx2 = VAL_Sort.IndexOf(o);
                    reoccur.Add(o);
                    if (ndx != -1)
                    {
                        try
                        {
                            if (done != false)
                            {
                                X_Line = X_Line;
                                if (invert.Checked == false)
                                {
                                    g.DrawLine(Pens.Magenta, X_, 20 + (ndx2 * 30 + 40), X_Line, Y_Line);
                                    g.FillEllipse(Brushes.Orange, X_  - 2, (20 + (ndx2 * 30 + 40)) - 2, 4, 4);
                                }
                                else
                                {
                                    g.DrawLine(Pens.Magenta, X_, 430 - (ndx2 * 30 + 40), X_Line, Y_Line);
                                    g.FillEllipse(Brushes.Orange, X_ - 2, (430 - (ndx2 * 30 + 40)) - 2, 4, 4);
                                }
                                g.FillEllipse(Brushes.Orange, X_Line - 2, Y_Line - 2, 4, 4);
                            }
                        }
                        catch (Exception erty) { }
                        done = true;
                        X_Line = X_;
                        if (invert.Checked == false)
                        {
                            Y_Line = 20 + (ndx2 * 30 + 40);
                        }
                        else
                        {
                            Y_Line = 430 - (ndx2 * 30 + 40);
                        }
                    }
                    X_ = X_ + 50;
                }
                pnl_drw1.CreateGraphics();
                done = false;
            }
        }

        int col_ndx = 0; ArrayList al_col = new ArrayList(); Pen Line_PEN;
        private Pen Get_COLOR()
        {
            Line_PEN = (Pen)al_col[col_ndx];
            if (col_ndx != al_col.Count - 1)
            {
                col_ndx++;
            }
            else
            { col_ndx = 0; }
            return (Line_PEN);
        }

        private void Set_COLOR()
        {
            al_col.Add(Pens.SteelBlue);
            al_col.Add(Pens.CornflowerBlue);
            al_col.Add(Pens.LightSteelBlue);
            al_col.Add(Pens.MidnightBlue);
            al_col.Add(Pens.RoyalBlue);
            al_col.Add(Pens.Blue);
            al_col.Add(Pens.Orange);
            al_col.Add(Pens.Green);
            al_col.Add(Pens.Yellow);
        }

        private void Change_Style(object sender, EventArgs e)
        {
            if (sender.Equals(boxChartToolStripMenuItem) == true)
            {
                boxChartToolStripMenuItem.Checked = true;
                lineChartToolStripMenuItem.Checked = false;
                Graph_Mode = "box";
            }
            else
            {
                boxChartToolStripMenuItem.Checked = false;
                lineChartToolStripMenuItem.Checked = true;
                Graph_Mode = "line";
            }
        }

        private void invert_Click(object sender, EventArgs e)
        {
            tv_NodeMouseClick(tv, this.e);
        }
    }
}
