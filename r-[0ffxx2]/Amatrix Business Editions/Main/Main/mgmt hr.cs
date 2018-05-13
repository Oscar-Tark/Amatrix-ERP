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
using System.IO;
using System.Collections;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Windows.Forms;
using Base_ASQL;
using Extern_ASQL;

namespace Main
{
    public partial class mgmt_hr : Form
    {
        //Cross Threading Objects
        //private Thread thinit;
        BASQL basql = new BASQL();
        Extern_Sql asql = new Extern_Sql();

        private delegate void delinit();
        private int howmany;
        private int maxm;

        private ArrayList aund = new ArrayList();
        private ArrayList aundC = new ArrayList();
        private ArrayList aundR = new ArrayList();

        public mgmt_hr()
        {
            mps.Visible = false;
            this.Controls.Add(mps);
            this.Icon = Properties.Resources.amdsicnico;
            InitializeComponent();
            this.Disposed += new EventHandler(mgmt_hr_Disposed);
            Init();
            if (Main.Amatrix.mgt != "")
            {
                /*try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.mgt); pwd.Owner = this; }
                catch { }*/
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
            dgv.AutoResizeColumns();
            clse.Image = Properties.Resources.extfin;
            if (acc_journ_sett.Default.IO == false)
            {
                dgv.ReadOnly = false;
                re_only.Visible = false;
            }
            if (acc_journ_sett.Default.IO == true)
            {
                dgv.ReadOnly = true;
                re_only.Visible = true;
            }

            if (acc_journ_sett.Default.dgvborder == 0)
            {
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgv2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv2.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            }

            else if (acc_journ_sett.Default.dgvborder == 1)
            {
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
                dgv2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgv2.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            }
            else { acc_journ_sett.Default.dgvborder = 0; acc_journ_sett.Default.Save(); }

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
                    visual.Default.font = ftmp;
                    visual.Default.Save();
                }
            }
            catch (Exception erty) { }

            /*if (Main.Amatrix.mgt != "")
            {
                bt_cv.Enabled = false;
            }*/
            dataGridView4.DataError += new DataGridViewDataErrorEventHandler(dataGridView4_DataError);
            th_ult_strt();
        }

        void dataGridView4_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private Thread th_db;
        private Thread thdb_ult;
        private Thread th2_db;
        private delegate void del_ult();
        private delegate void del_db();
        private delegate void del2_db();

        private void th_ult_strt()
        {
            try
            {
                thdb_ult = new Thread(new ThreadStart(del_ult_strt));
                thdb_ult.IsBackground = true;
                thdb_ult.Start();
            }
            catch (Exception erty) { init_dbult(); }
        }

        private void del_ult_strt()
        {
            try
            {
                this.Invoke(new del_ult(init_dbult));
            }
            catch (Exception erty) { init_dbult(); }
        }

        private void init_dbult()
        {
            emply_payr_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                string ConnString2 = employ_payrllTableAdapter.Connection.ConnectionString;
                string SqlString2 = "Select * From Employ_payrll";
                Last_Query_Used = SqlString2;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString2))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString2, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            emply_payr_dtst.Load(reader, LoadOption.PreserveChanges, "Employ_payrll");
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer("Select * From Employ_payrll");
                emply_payr_dtst.Employ_payrll.Load(dr_univ);
                //image
                if (Main.Amatrix.mgt != "")
                {
                    try
                    {
                        if (dgv4[80, 0].Value != DBNull.Value)
                        {
                            byte[] res1 = (byte[])dgv4[80, 0].Value;
                            Image newImage;
                            using (MemoryStream ms = new MemoryStream(res1, 0, res1.Length))
                            {
                                newImage = Bitmap.FromStream(ms, true);
                                ms.Flush();
                                ms.Close();
                                ms.Dispose();
                            }
                            pbx.BackgroundImage = newImage;
                        }
                        else
                        {
                            pbx.BackgroundImage = Properties.Resources.person;
                        }
                    }
                    catch (Exception erty) { }
                }
            }
            conn2();
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

        private void oper_save()
        {
            if (acc_journ_sett.Default.autosave == true)
            {
                try
                {
                    if (Main.Amatrix.mgt == "")
                    {
                        employ_payrllTableAdapter.Update(emply_payr_dtst);
                        empl_selecTableAdapter.Update(empl_selc_dtst);
                        outsrceTableAdapter.Update(outsrce_dtst);
                        payedonTableAdapter.Update(peyed_dtst);
                    }
                    else
                    {
                        asql.Save(emply_payr_dtst.Employ_payrll, emply_payr_dtst.Employ_payrll.TableName, Main.Amatrix.mgt);
                        asql.Save(empl_selc_dtst.Empl_selec, empl_selc_dtst.Empl_selec.TableName, Main.Amatrix.mgt);
                        asql.Save(outsrce_dtst.outsrce, outsrce_dtst.outsrce.TableName, Main.Amatrix.mgt);
                        asql.Save(peyed_dtst.payedon, peyed_dtst.payedon.TableName, Main.Amatrix.mgt);
                    }
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A Fatal Error Occured While Auto-Saving Your Information. The Operation Was Halted and Your Data Was not Saved."); }
            }
        }

        private void th_initdb()
        {
            th_db = new Thread(new ThreadStart(del_initdb));
            th_db.IsBackground = true;
            th_db.Start();
        }

        private void del_initdb()
        {
            try
            {
                this.Invoke(new del_db(init_db));
            }
            catch (Exception erty) { init_db(); }
        }

        //access by thread

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
            try
            {
                if (Main.Amatrix.mgt == "")
                {
                    if (tb.SelectedIndex == 0)
                    {
                        employ_payrllTableAdapter.Connection.Open();
                        f = true;
                        employ_payrllTableAdapter.Connection.Close();
                    }
                    else if (tb.SelectedIndex == 1)
                    {
                        outsrceTableAdapter.Connection.Open();
                        f = true;
                        outsrceTableAdapter.Connection.Close();
                    }
                    else if (tb.SelectedIndex == 2)
                    {
                        empl_selecTableAdapter.Connection.Open();
                        f = true;
                        empl_selecTableAdapter.Connection.Close();
                    }
                }
                else
                {
                    try
                    {
                        SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                        conn.Open();
                        connlbl.Image = Properties.Resources.conncted;
                        connlbl.Text = "HR Managment is Connected";
                    }
                    catch (Exception erty)
                    {
                        connlbl.Text = "HR Managment is Not Connected";
                        connlbl.Image = Properties.Resources.connctno;
                    }
                }
            }
            catch (Exception errtt) { f = false; }
            if (Main.Amatrix.mgt == "")
            {
                if (tb.SelectedIndex == 0)
                {
                    if (f == true)
                    {
                        connlbl.Image = Properties.Resources.conncted;
                        connlbl.Text = "Employee/Payroll Table is Connected";
                    }
                    else if (f == false)
                    {
                        connlbl.Text = "Employee/Payroll Table is Not Connected";
                        connlbl.Image = Properties.Resources.connctno;
                    }
                    db_info.Text = employ_payrllTableAdapter.Connection.Database; srv_inf.Text = employ_payrllTableAdapter.Connection.ServerVersion;
                    nds_.Text = employ_payrllTableAdapter.Connection.DataSource;
                }
                if (tb.SelectedIndex == 1)
                {
                    if (f == true)
                    {
                        connlbl.Image = Properties.Resources.conncted;
                        connlbl.Text = "Outsourcing Table is Connected";
                    }
                    else if (f == false)
                    {
                        connlbl.Text = "Outsourcing Table is Not Connected";
                        connlbl.Image = Properties.Resources.connctno;
                    }
                    db_info.Text = outsrceTableAdapter.Connection.Database; srv_inf.Text = outsrceTableAdapter.Connection.ServerVersion;
                    nds_.Text = outsrceTableAdapter.Connection.DataSource;
                }

                if (tb.SelectedIndex == 2)
                {
                    if (f == true)
                    {
                        connlbl.Image = Properties.Resources.conncted;
                        connlbl.Text = "Payroll Table is Connected";
                    }
                    else if (f == false)
                    {
                        connlbl.Text = "Payroll Table is Not Connected";
                        connlbl.Image = Properties.Resources.connctno;
                    }
                    db_info.Text = empl_selecTableAdapter.Connection.Database; srv_inf.Text = empl_selecTableAdapter.Connection.ServerVersion;
                    nds_.Text = empl_selecTableAdapter.Connection.DataSource;
                }
            }
        }

        private void mgmt_hr_Load(object sender, EventArgs e)
        {
        }

        public void tx(string First_Name, string Last_Name)
        {
            oper_save();
            emply_payr_dtst.Clear();
            string SqlString = "SELECT * FROM Employ_payrll WHERE [Employee First Name] LIKE '%" + First_Name + "%'";// OR [Employee Last Name] LIKE '%" + Last_Name + "%'";

            if (Main.Amatrix.mgt == "")
            {
                string ConnString = employ_payrllTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            emply_payr_dtst.Load(reader, LoadOption.PreserveChanges, "Employ_payrll");
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                emply_payr_dtst.Employ_payrll.Load(dr_univ);
            }
            this.Show();
        }

        private void selcoll_Click(object sender, EventArgs e)
        {
            cols cll = new cols();
            cll.Show();
        }

        private void tbsel1_Click(object sender, EventArgs e)
        {
            tb.SelectTab(0);
        }

        private void tbsel2_Click(object sender, EventArgs e)
        {
            tb.SelectTab(1);
        }

        private void tbsel3_Click(object sender, EventArgs e)
        {
            tb.SelectTab(2);
        }

        private void tbsel4_Click(object sender, EventArgs e)
        {
            tb.SelectTab(3);
        }

        private void connlbl_MouseEnter(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = Properties.Resources.bannrrageconv;
        }

        private void connlbl_MouseLeave(object sender, EventArgs e)
        {
            connlbl.BackgroundImage = null;
        }

        private void dechr_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dechr.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void mgmt_hr_Activated(object sender, EventArgs e)
        {

        }

        private void mgmt_hr_Deactivate(object sender, EventArgs e)
        {

        }

        private void rstrt_Click(object sender, EventArgs e)
        {
            mgmt_hr hr = new mgmt_hr();
            hr.Show();
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

        private void clse_MouseEnter(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        }

        private void clse_MouseLeave(object sender, EventArgs e)
        {
            clse.DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        private void svebtn_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (Main.Amatrix.mgt == "")
                {
                    employpayrllBindingSource.EndEdit();
                    employ_payrllTableAdapter.Update(emply_payr_dtst);
                    outsrceTableAdapter.Update(outsrce_dtst);
                    empl_selecTableAdapter.Update(empl_selc_dtst);
                    payedonTableAdapter.Update(peyed_dtst);
                }
                else
                {
                    employpayrllBindingSource.EndEdit();
                    asql.Save(emply_payr_dtst.Employ_payrll, emply_payr_dtst.Employ_payrll.TableName, Main.Amatrix.mgt);
                    asql.Save(outsrce_dtst.outsrce, outsrce_dtst.outsrce.TableName, Main.Amatrix.mgt);
                    asql.Save(empl_selc_dtst.Empl_selec, empl_selc_dtst.Empl_selec.TableName, Main.Amatrix.mgt);
                    asql.Save(peyed_dtst.payedon, peyed_dtst.payedon.TableName, Main.Amatrix.mgt);

                    try
                    {
                        Main.Amatrix.ascl.broadcast("<ip>" + Properties.Settings.Default.IP + "</ip><typ>w</typ><val>0</val><app>" + this.Name + "</app><par>[" + toolStrip1.Name + "]</par><con>bttn_sync</con>");
                    }
                    catch (Exception erty) { general_mssg("Syncronization is not Set Up", "Sync Error"); }
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A Fatal Error Occured While Saving Your Information. The Operation Was Halted and Your Data Was not Saved."); }
        }

        private void pbx_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
            if (tb.SelectedIndex == 0)
            {
                try
                {
                    pbx.BackgroundImage = Image.FromFile(ofd.FileName);
                    pbx.BackgroundImageLayout = ImageLayout.Zoom;
                }
                catch (Exception erty) { general_mssg("Could Not Set The Specified Logo", erty.Message); }
                if (Main.Amatrix.mgt == "")
                {
                    textBox67.Text = ofd.FileName;
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
                        dgv4[80, dgv4.CurrentRow.Index].Value = b;
                    }
                    catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
                }
            }
            else
            {
                /*Image i = Image.FromFile(ofd.FileName);
                byte[] b;
                try
                {
                    ImageConverter converter = new ImageConverter();
                    Bitmap bmp = new Bitmap(i);
                    b = (byte[])converter.ConvertTo(bmp, typeof(byte[]));
                    dgv3[13, dgv3.CurrentRow.Index].Value = b;
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }*/
            }
        }

        private void remv_pic_Click(object sender, EventArgs e)
        {
            textBox67.Text = "";
            pbx.BackgroundImage = Properties.Resources.person;
            if (Main.Amatrix.mgt != "")
            {
                dgv4[80, dgv4.CurrentRow.Index].Value = null;
            }
        }

        private void general_mssg(string text, string cause1)
        {
            err_inf_1.Text = cause1;
            err_inf_2.Text = text;
            err.Visible = true;

            try
            {
                toolTip1.Show(text, this, this.Size.Width - 114, ts2.Location.Y - 10, 5000);
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
                toolTip1.ToolTipTitle = "Error";
                toolTip1.Show(text, this, this.Size.Width - 114, ts2.Location.Y - 10, Interval);
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

        private void bt_cv_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
            /*if (bt_cv.Text == "View C.V.")
            {
                try
                {
                    System.Diagnostics.Process.Start(textBox66.Text);
                }
                catch (Exception erty) { general_mssg("Unable To Load C.V", "C.V Address is Wrong, The Image may be deleted or the Address may be tampered with."); }
            }
            else
            {
                cv_ofd.ShowDialog();
            }*/
        }

        private void cv_ofd_FileOk(object sender, CancelEventArgs e)
        {
            //textBox66.Text = cv_ofd.FileName;
            //b = (byte[])converter.ConvertTo(bmp, typeof(byte[]));
            try{
                FileStream fs = new FileStream(cv_ofd.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                byte[] b = new byte[fs.Length];
                b = br.ReadBytes(Convert.ToInt32(fs.Length));
                dgv4[dgv.ColumnCount-1, dgv.CurrentRow.Index].Value = b;
            }
            catch (Exception ertyty) { MessageBox.Show(ertyty.Message); }
        }

        private void connlbl_Click(object sender, EventArgs e)
        {
            Point ptt = new Point();
            ptt.X = Cursor.Position.X - this.Location.X + 50;
            ptt.Y = Cursor.Position.Y - this.Location.Y;
            cmslv.Show(ptt);
        }

        private void connct_Click(object sender, EventArgs e)
        {
            connctyes();
        }

        private void connctyes()
        {
            conn2();
        }

        private void clseconn_Click(object sender, EventArgs e)
        {
            connect_no();
        }

        private void connect_no()
        {
            conn2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(bt_cv, 0, 0);
        }

        private void addReplaceCVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.Equals(addReplaceCVToolStripMenuItem) == true)
            {
                cv_ofd.ShowDialog();
            }
            else
            {
                textBox66.Text = "";
            }
        }

        gadg_pics pics;
        private void pbx_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (pbx.BackgroundImage != Properties.Resources.person)
                {
                    try
                    {
                        pics = new gadg_pics();
                        pics.Size = new Size(400, 400);
                        pics.fromfle(pbx.BackgroundImage);
                        pics.Location = new Point(287, 6);
                        panel7.Controls.Add(pics);
                        pics.BringToFront();
                    }
                    catch (Exception erty) { }
                }
            }
            catch (Exception erty) { }
        }

        private void pbx_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                pics.Dispose();
            }
            catch (Exception erty) { }
        }

        private void gotoitm_Click(object sender, EventArgs e)
        {
            oper_save();
            try
            {
                if (search_who_sel == 1 || search_who_sel == 2)
                {
                    int start = 0; int count = 0;
                    string SqlString = "Select * From Employ_payrll Where";
                    Last_Query_Used = SqlString;
                    foreach (DataColumn dgvc in emply_payr_dtst.Employ_payrll.Columns)
                    {
                        try
                        {
                            if (count <= 77)
                            {
                                if (start == 0)
                                {
                                    if (tbxfned.Text.Contains(' ') == true)
                                    {
                                        SqlString = SqlString + " [" + dgvc.ColumnName + "] + ' ' + [" + emply_payr_dtst.Employ_payrll.Columns[1].ColumnName + "] LIKE '%" + tbxfned.Text + "%' ";
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
                            count++;
                        }
                        catch (Exception ertty) { Am_err ner = new Am_err(); ner.tx(ertty.Message);}
                    }
                    emply_payr_dtst.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        try
                        {
                            string ConnString = employ_payrllTableAdapter.Connection.ConnectionString;
                            using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                            {
                                using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    conn.Open();
                                    SqlCeDataReader reader = cmd.ExecuteReader();

                                    using (reader)
                                    {
                                        emply_payr_dtst.Load(reader, LoadOption.PreserveChanges, "Employ_payrll");
                                        dgv2.DataSource = emply_payr_dtst.Employ_payrll;
                                    }
                                }
                            }
                        }
                        catch (Exception erttyY) { Am_err er = new Am_err(); er.tx(erttyY.Message); }
                    }
                    else
                    {
                        try
                        {
                            dr_univ = Quer(SqlString);
                            emply_payr_dtst.Employ_payrll.Load(dr_univ);
                            dgv2.DataSource = emply_payr_dtst.Employ_payrll;
                        }
                        catch (Exception erty) { Am_err ner = new Am_err(); ner.tx(erty.Message); }
                    }
                }

                if (search_who_sel == 1 || search_who_sel == 3)
                {
                    string SqlString = "Select * From outsrce Where";
                    Last_Query_Used2 = SqlString;
                    foreach (DataGridViewColumn dgvc in dgv.Columns)
                    {
                        if (dgvc.Index == dgv.ColumnCount - 1)
                        {
                            SqlString = SqlString + " [" + dgvc.HeaderText + "] LIKE '%" + tbxfned.Text + "%'";
                        }
                        else
                        {
                            SqlString = SqlString + " [" + dgvc.HeaderText + "] LIKE '%" + tbxfned.Text + "%' OR";
                        }
                    }

                    outsrce_dtst.Clear();
                    if (Main.Amatrix.mgt == "")
                    {
                        string ConnString = outsrceTableAdapter.Connection.ConnectionString;
                        using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                        {
                            using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                            {
                                cmd.CommandType = CommandType.Text;

                                conn.Open();
                                SqlCeDataReader reader = cmd.ExecuteReader();

                                using (reader)
                                {
                                    outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                    dgv.DataSource = outsrce_dtst.outsrce;
                                }
                            }
                        }
                    }
                    else
                    {
                        dr_univ = Quer(SqlString);
                        outsrce_dtst.outsrce.Load(dr_univ);
                        dgv.DataSource = outsrce_dtst.outsrce;
                    }
                }
            }
            catch (Exception erty) { }
        }

        private void dgv2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void sve_po_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (Main.Amatrix.mgt == "")
                {
                    payedonTableAdapter.Update(peyed_dtst);
                }
                else
                {
                    asql.Save(peyed_dtst.payedon, peyed_dtst.payedon.TableName, Main.Amatrix.mgt);
                }
            //}
            //catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("A Fatal Error Occured While Saving Your Information. The Operation Was Halted and Your Data Was not Saved."); }
        }

        private void rsdbord_Click(object sender, EventArgs e)
        {
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dgv2.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            acc_journ_sett.Default.dgvborder = 1;
            acc_journ_sett.Default.Save();
        }

        private void dflt_dgvbord_Click(object sender, EventArgs e)
        {
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgv2.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            acc_journ_sett.Default.dgvborder = 0;
            acc_journ_sett.Default.Save();
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

        private void colreordr_Click(object sender, EventArgs e)
        {
            dgv.AllowUserToOrderColumns = true;
            dgv2.AllowUserToOrderColumns = true;
        }

        private void colreordflse_Click(object sender, EventArgs e)
        {
            dgv.AllowUserToOrderColumns = false;
            dgv2.AllowUserToOrderColumns = false;
        }

        private void ascvw_Click(object sender, EventArgs e)
        {
            try
            {
                dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);
                dgv2.Sort(dgv2.Columns[0], ListSortDirection.Ascending);
            }
            catch (Exception erty) { }
        }

        private void descvw_Click(object sender, EventArgs e)
        {
            try
            {
                dgv.Sort(dgv.Columns[0], ListSortDirection.Descending);
                dgv2.Sort(dgv2.Columns[0], ListSortDirection.Descending);
            }
            catch (Exception erty) { }
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

                        dgv2.Font = fnt;
                        dgv2.AutoResizeRows();
                        dgv2.AutoResizeColumns();
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
                }
            }
            Properties.Settings.Default.Save();
        }

        //menu

        private int dgv_who = 1;
        private void undoall_Click(object sender, EventArgs e)
        {
            if (tb.SelectedIndex == 1 && dgv_who == 1)
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
        }

        private void initializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            emply_payr_dtst.Clear();
            th_ult_strt();
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

        private TextBox tbx_temp; private ToolStripTextBox tbx_tstemp;
        private void tbxfned_Enter(object sender, EventArgs e)
        {
            try
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
            catch (Exception erty) { }
        }

        private void tbxfned_Leave(object sender, EventArgs e)
        {
            try
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
            catch (Exception erty) { }
        }

        private int search_who_sel = 1;
        private void search_who(object sender, EventArgs e)
        {
            src_all.Checked = false;
            src_ev.Checked = false;
            src_py.Checked = false;

            if (sender.Equals(src_all) == true)
            {
                search_who_sel = 1;
                src_all.Checked = true;
            }
            if (sender.Equals(src_ev) == true)
            {
                search_who_sel = 2;
                src_ev.Checked = true;
            }
            if (sender.Equals(src_py) == true)
            {
                search_who_sel = 3;
                src_py.Checked = true;
            }
        }

        //error handling
        private string mssge_ttp;
        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            general_mssg(e.Exception.Message, e.Exception.Source);
        }

        private void qa2_shw(bool Show)
        {
            /*if (Show == true)
            {
                qa2.Visible = true;
                qa2.Location = new Point(Cursor.Position.X - (this.Location.X + 10), Cursor.Position.Y - (this.Location.Y + 60));
            }
            else
            { qa2.Visible = false; }*/
        }

        private ComboBox bx; string s_bx;
        private void currency_changed(object sender, EventArgs e)
        {
            bx = (ComboBox)sender;
            s_bx = bx.Text;

            comboBox13.Text = s_bx;
            comboBox14.Text = s_bx;
            comboBox15.Text = s_bx;
            comboBox16.Text = s_bx;
            comboBox17.Text = s_bx;
            comboBox19.Text = s_bx;
            comboBox20.Text = s_bx;
            //comboBox2.Text = s_bx;
        }

        private void go_emp_ButtonClick(object sender, EventArgs e)
        {
            oper_save();
            emply_payr_dtst.Clear();
            string ConnString = employ_payrllTableAdapter.Connection.ConnectionString;
            string SqlString = "";
            if (toolStripTextBox2.Text.Contains(' ') == true)
            {
                SqlString = "Select * FROM Employ_payrll WHERE [Employee First Name] + ' ' + [Employee Last Name] LIKE '%" + toolStripTextBox2.Text + "%'";
            }
            else
            {
                SqlString = "Select * FROM Employ_payrll WHERE [Employee First Name] LIKE '%" + toolStripTextBox2.Text + "%' OR [Employee Last Name] LIKE '%" + toolStripTextBox2.Text + "%'";
            }
            if (Main.Amatrix.mgt == "")
            {
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();//)

                        using (reader)
                        {
                            emply_payr_dtst.Load(reader, LoadOption.PreserveChanges, "Employ_payrll");
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                emply_payr_dtst.Employ_payrll.Load(dr_univ);
            }
        }

        private Control cnt_temp; double tot; double dinky = 0;

        private double gethours(double start, double end, string am_pm1, string am_pm2)
        {
            double n;
            if (am_pm1 != am_pm2)
            {
                end = end + 12;
                n = end - start;
                return n;
            }
            else
            { n = end - start; return n; }
        }

        private double hour_gap, time_hours; int timehours2; string append_time = "";
        private void button23_Click(object sender, EventArgs e)
        {
            if (comboBox7.Text != "Hour" && comboBox12.Text != "Hour")
            {
                //fromdata_or_cnt = true;
                append_time = "";
                tbx_time.Text = tbx_time.Text + "\n";
                tbx_time.Text = tbx_time.Text + comboBox7.Text + ':' + comboBox9.Text + " to " + comboBox12.Text + ':' + comboBox10.Text;
                hour_gap = gethours(Convert.ToDouble(comboBox7.Text), Convert.ToDouble(comboBox12.Text), comboBox9.Text, comboBox10.Text);
               // tbx_time.Text = tbx_time.Text + "  [" + hour_gap.ToString() + " Hours]";

                /*foreach (ListViewItem lvt in lv_shifts.Items)
                {
                    time_hours = time_hours + Convert.ToDouble(lvt.SubItems[1].Text);
                    timehours2 = (int)time_hours;
                    if (lvt.Index != lv_shifts.Items.Count - 1)
                    {
                        append_time = append_time + lvt.Text + '[' + lvt.SubItems[1].Text + ']' + ',';
                    }
                    else
                    {
                        append_time = append_time + lvt.Text + '[' + lvt.SubItems[1].Text + ']';
                    }
                }
                tbx_time.Text = append_time;*/
            }
        }

        private void elim_shift_Click(object sender, EventArgs e)
        {
            int ndx, ndx2, ndx3;
            ndx = tbx_time.GetLineFromCharIndex(tbx_time.SelectionStart);
            ndx2 = tbx_time.GetFirstCharIndexFromLine(ndx);
            ndx3 = tbx_time.Lines[ndx].Length;
            try
            {
                tbx_time.Text = tbx_time.Text.Remove(ndx2 - 1, ndx3 + 1);
            }
            catch (Exception erty)
            {
                tbx_time.Text = tbx_time.Text.Remove(ndx2, ndx3);
            }
        }

        double d = 0;
        private void tax_changed(object sender, EventArgs e)
        {
            if (bindingNavigatorPositionItem.Text == "0")
            {
                general_mssg("No Values are Within the Employee/Payroll Table Press the + Button to Add One The Button may be Found in the Tool-bar Above.", "No Entries Within Said Table");
            }
            d = 0;
            try
            {
                d = d + Convert.ToDouble(textBox54.Text);
            }
            catch (Exception erty) { }
            try
            {
                d = d + Convert.ToDouble(textBox55.Text);
            }
            catch (Exception erty2) { }
            try
            {
                d = d + Convert.ToDouble(textBox56.Text);
            }
            catch (Exception erty3) { }
            try
            {
                d = d + Convert.ToDouble(textBox57.Text);
            }
            catch (Exception erty4) { }
            try
            {
                d = d + Convert.ToDouble(textBox59.Text);
            }
            catch (Exception erty5) { }
            try
            {
                d = d + Convert.ToDouble(textBox60.Text);
            }
            catch (Exception erty6) { }
            textBox53.Text = d.ToString();
        }

        int wooo = 0;
        private void project_totalz(object sender, EventArgs e)
        {
            /*wooo = 0;
            try
            {
                //wooo = wooo + Convert.ToInt32(textBox33.Text);
            }
            catch (Exception erty) { }
            try
            {
                //wooo = wooo + Convert.ToInt32(textBox34.Text);
            }
            catch (Exception erty) { }
            try
            {
                wooo = wooo + Convert.ToInt32(textBox35.Text);
            }
            catch (Exception erty) { }

            textBox58.Text = wooo.ToString();*/
        }

        private double temp_work_double = 0;
        private void OverhourPay_tot(object sender, EventArgs e)
        {
            try
            {
                temp_work_double = Convert.ToDouble(textBox49.Text) * Convert.ToInt32(comboBox18.Text);
                textBox50.Text = temp_work_double.ToString();
            }
            catch (Exception erty) { textBox50.Text = ""; }
        }

        private int work_int = 0;
        private void holidays_total(object sender, EventArgs e)
        {
            /*if (textBox40.Text != null && textBox40.Text != "")
            {
                work_int = 0;
                try
                {
                    work_int = work_int + Convert.ToInt32(textBox37.Text);
                }
                catch (Exception erty2) { }
                try
                {
                    work_int = work_int + Convert.ToInt32(textBox38.Text);
                }
                catch (Exception erty3) { }
                try
                {
                    work_int = work_int + Convert.ToInt32(textBox39.Text);
                }
                catch (Exception erty4) { }

                try
                {
                    work_int = Convert.ToInt32(textBox40.Text) - work_int;
                }
                catch (Exception erty) { }
                textBox4.Text = work_int.ToString();
            }*/
        }

        //outsourcing...

        private void init_db()
        {
            try
            {
                oper_save();
                outsrce_dtst.Clear();
                Last_Query_Used2 = "Select * From outsrce";
                if (Main.Amatrix.mgt == "")
                {
                    string ConnString = outsrceTableAdapter.Connection.ConnectionString;
                    string SqlString = "Select * From outsrce";
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                dgv.DataSource = outsrce_dtst.outsrce;
                            }
                        }
                    }
                }
                else
                {
                    dr_univ = Quer("Select * From outsrce");
                    outsrce_dtst.outsrce.Load(dr_univ);
                    dgv.DataSource = outsrce_dtst.outsrce;
                }
            }
            catch (Exception erty) { }
        }


        private void th2_db_strt()
        {
            th2_db = new Thread(new ThreadStart(del2_db_strt));
            th2_db.IsBackground = true;
            th2_db.Start();
        }

        private void del2_db_strt()
        {
            try
            {
                this.Invoke(new del2_db(init_db2));
            }
            catch (Exception erty) { general_mssg(erty.Message, ""); }
        }

        DataSet dgv_dtst = new DataSet();
        private void init_db2()
        {
            try
            {
                oper_save();
                emply_payr_dtst.Clear();
                string SqlString = "Select * From Employ_payrll Where [outsourced to office] = '" + dgv.CurrentRow.Cells[1].Value.ToString() + "'";
                Last_Query_Used = SqlString;
                if (Main.Amatrix.mgt == "")
                {
                    string ConnString = employ_payrllTableAdapter.Connection.ConnectionString; using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                emply_payr_dtst.Load(reader, LoadOption.PreserveChanges, "Employ_payrll");
                                dgv2.DataSource = emply_payr_dtst.Employ_payrll;
                            }
                        }
                    }
                }
                else
                {
                    dr_univ = Quer(SqlString);
                    emply_payr_dtst.Employ_payrll.Load(dr_univ);
                    dgv2.DataSource = emply_payr_dtst.Employ_payrll;
                }
            }
            catch (Exception erty) { general_mssg(erty.Message, ""); }
        }

        //init outsourcing -END-

        private void dgv_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (tb.SelectedIndex == 1)
                {
                    th2_db_strt();
                }
            }
            catch (Exception erty) { }
        }

        private void set_of_info(bool Show)
        {
            if (Show == true)
            {
                pnl_info.Visible = true;
                load_office_info();
            }
            else if (Show == false)
            {
                pnl_info.Visible = false;
            }
        }

        private void load_office_info()
        {
            richTextBox4.Text = dgv[11, dgv.CurrentRow.Index].Value.ToString();
            richTextBox4.Text = richTextBox4.Text.Replace(';','\n');

            for (int i = 0; i <= 10; i++)
            {
                try
                {
                    listView5.Items[i].SubItems.Add(richTextBox4.Lines[i]);
                }
                catch (Exception erty) { listView5.Items[i].SubItems.Add("None"); }
            }
            zz_yn(false);
        }

        private DataSet dtst2_temp = new DataSet();
        private void set_outsource_office_DropDown(object sender, EventArgs e)
        {
            try
            {
                oper_save();
                dtst2_temp.Clear();
                string SqlString = "Select DISTINCT [Outsourced Office] From outsrce";
                if (Main.Amatrix.mgt == "")
                {
                    string ConnString = outsrceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                dtst2_temp.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                dgv_temp.DataSource = dtst2_temp.Tables[0];
                            }
                        }
                    }
                }
                else
                {
                    dr_univ = Quer(SqlString);
                    dtst2_temp.Load(dr_univ, LoadOption.PreserveChanges, "outsrce");
                    dgv_temp.DataSource = dtst2_temp.Tables[0];
                }
                set_outsource_office.Items.Clear();
                foreach (DataGridViewRow dgvr in dgv_temp.Rows)
                {
                    try
                    {
                        set_outsource_office.Items.Add(dgvr.Cells[0].Value.ToString());
                    }
                    catch (Exception erty24) { }
                }
            }
            catch (Exception erty) { }
        }

        TextBox tstt = new TextBox();
        int sel;
        private void date_helper(object sender, KeyEventArgs e)
        {
            tstt = (TextBox)sender;
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

        //dgv's tab2
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
                else if (sender.Equals(dgv2) == true)
                {
                    try
                    {
                        cl_tmp = dgv2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                        dgv2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.AliceBlue;
                    }
                    catch (Exception erty) { }
                }
            }
            else { }
        }

        //gadg_pics pics;
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
                else if (sender.Equals(dgv2) == true)
                {

                    try
                    {
                        dgv2.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = cl_tmp;
                    }
                    catch (Exception erty) { }
                }
            }
            else { }
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

            //dgv2
            if (sender.Equals(dgv2dwnall) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.RowCount - 1].Cells[dgv2.CurrentCell.ColumnIndex];
                }
                catch (Exception erty) { }
            }
            if (sender.Equals(dgv2dwnone) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentRow.Index + 1].Cells[dgv2.CurrentCell.ColumnIndex];
                }
                catch (Exception erty) { }
            }
            if (sender.Equals(dgv2rightall) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.ColumnCount - 1];
                }
                catch (Exception erty6) { }
            }
            if (sender.Equals(dgv2rightone) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex + 1];
                }
                catch (Exception erty5) { }
            }
            if (sender.Equals(dgv2leftone) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex - 1];
                }
                catch (Exception erty4) { }
            }
            if (sender.Equals(dgv2leftall) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[0];
                }
                catch (Exception erty3) { }
            }
            if (sender.Equals(dgv2upone) == true)
            {
                try
                {
                    dgv2.CurrentCell = dgv2.Rows[dgv2.CurrentCell.RowIndex - 1].Cells[dgv2.CurrentCell.ColumnIndex];
                }
                catch (Exception erty2) { }
            }
            if (sender.Equals(dgv2upall) == true)
            {
                try
                {
                    try
                    {
                        dgv2.CurrentCell = dgv2.Rows[0].Cells[dgv2.CurrentCell.ColumnIndex];
                    }
                    catch (Exception erty) { }
                }
                catch (Exception erty) { }
            }
        }

        //queries
        //dynam query
        ToolStripButton b_temp;
        ToolStripMenuItem mt, mt2, mt3;
        ToolStripTextBox tbxx, tbxx2; ToolStripComboBox tcxx;
        string who, what1, what2;
        private void eqls(ToolStripButton sender, int ndx)
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

            string SqlString = "";
            string ConnString = "";
            if (ndx == 1)
            {
                outsrce_dtst.Clear();
                ConnString = outsrceTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM outsrce WHERE [" + who + "] = '" + what2 + "'";
            }
            else if (ndx == 2)
            {
                empl_selc_dtst.Clear();
                ConnString = empl_selecTableAdapter.Connection.ConnectionString;
                SqlString = "Select * From Empl_selec Where [" + who + "] = '" + what2 + "'";
            }
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
                            if (ndx == 1)
                            {
                                outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                dgv.DataSource = outsrce_dtst.outsrce;
                            }
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                if (ndx == 1)
                {
                    outsrce_dtst.outsrce.Load(dr_univ);
                    dgv.DataSource = outsrce_dtst.outsrce;
                }
            }
        }

        private void nt_eql(ToolStripButton sender, int ndx)
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

            string SqlString = "";
            string ConnString = "";
            if (ndx == 1)
            {
                outsrce_dtst.Clear();
                ConnString = outsrceTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM outsrce WHERE [" + who + "] != '" + what2 + "'";
            }
            else if (ndx == 2)
            {
                empl_selc_dtst.Clear();
                ConnString = empl_selecTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM Empl_selec WHERE [" + who + "] != '" + what2 + "'";
            }
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
                            if (ndx == 1)
                            {
                                outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                dgv.DataSource = outsrce_dtst.outsrce;
                            }
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                if (ndx == 1)
                {
                    outsrce_dtst.outsrce.Load(dr_univ);
                    dgv.DataSource = outsrce_dtst.outsrce;
                }
            }
        }

        //between/between dates
        private void btw(ToolStripButton sender, int ndx)
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

            string SqlString = "";
            string ConnString = "";
            if (ndx == 1)
            {
                outsrce_dtst.Clear();
                ConnString = outsrceTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM outsrce WHERE [" + who + "] > '" + what2 + "' AND [" + who + "] < '" + tbxx2.Text + "'";
            }
            else if (ndx == 2)
            {
                empl_selc_dtst.Clear();
                ConnString = empl_selecTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM Empl_selec WHERE [" + who + "] > '" + what2 + "' AND [" + who + "] < '" + tbxx2.Text + "'";
            }
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
                            if (ndx == 1)
                            {
                                outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                dgv.DataSource = outsrce_dtst.outsrce;
                            }
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                if (ndx == 1)
                {
                    outsrce_dtst.outsrce.Load(dr_univ);
                    dgv.DataSource = outsrce_dtst.outsrce;
                }
            }
        }

        //less/before than
        private void lss_thn(ToolStripButton sender, int ndx)
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

            string SqlString = "";
            string ConnString = "";
            if (ndx == 1)
            {
                outsrce_dtst.Clear();
                ConnString = outsrceTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM outsrce WHERE [" + who + "] < '" + what2 + "'";
            }
            else if (ndx == 2)
            {
                empl_selc_dtst.Clear();
                ConnString = empl_selecTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM Empl_selec WHERE [" + who + "] < '" + what2 + "'";
            }
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
                            if (tb.SelectedIndex == 1)
                            {
                                outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                dgv.DataSource = outsrce_dtst.outsrce;
                            }
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                if (ndx == 1)
                {
                    outsrce_dtst.outsrce.Load(dr_univ);
                    dgv.DataSource = outsrce_dtst.outsrce;
                }
            }
        }

        //greater/after than
        private void grt_thn(ToolStripButton sender, int ndx)
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

            string SqlString = "";
            string ConnString = "";
            if (ndx == 1)
            {
                outsrce_dtst.Clear();
                ConnString = outsrceTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM outsrce WHERE [" + who + "] > '" + what2 + "'";
            }
            else if (ndx == 2)
            {
                empl_selc_dtst.Clear();
                ConnString = empl_selecTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM Empl_selec WHERE [" + who + "] > '" + what2 + "'";
            }
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
                            if (ndx == 1)
                            {
                                outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                dgv.DataSource = outsrce_dtst.outsrce;
                            }
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                if (ndx == 1)
                {
                    outsrce_dtst.outsrce.Load(dr_univ);
                    dgv.DataSource = outsrce_dtst.outsrce;
                }
            }
        }

        //starting with
        private void str_wth(ToolStripButton sender, int ndx)
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

            string SqlString = "";
            string ConnString = "";
            if (ndx == 1)
            {
                ConnString = outsrceTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM outsrce WHERE [" + who + "] LIKE '" + what2 + "%'";
            }
            else if (ndx == 2)
            {
                empl_selc_dtst.Clear();
                ConnString = empl_selecTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM Empl_selec WHERE [" + who + "] LIKE '" + what2 + "%'";
            }
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
                            if (ndx == 1)
                            {
                                outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                dgv.DataSource = outsrce_dtst.outsrce;
                            }
                        }
                    }
                }
            }
            else
            {
                try
                {
                    dr_univ = Quer(SqlString);
                    if (ndx == 1)
                    {
                        outsrce_dtst.Clear();
                        outsrce_dtst.outsrce.Load(dr_univ);
                        dgv.DataSource = outsrce_dtst.outsrce;
                    }
                }
                catch (Exception ertt) { }
            }
        }

        //ending with
        private void end_wth(ToolStripButton sender, int ndx)
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
            string SqlString = "";
            string ConnString = "";

            if (ndx == 1)
            {
                outsrce_dtst.Clear();
                ConnString = outsrceTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM outsrce WHERE [" + who + "] LIKE '%" + what2 + "'";
            }
            else if (ndx == 2)
            {
                empl_selc_dtst.Clear();
                ConnString = empl_selecTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM Empl_selec WHERE [" + who + "] LIKE '%" + what2 + "'";
            }
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
                            if (ndx == 1)
                            {
                                outsrce_dtst.Clear();
                                outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                dgv.DataSource = outsrce_dtst.outsrce;
                            }
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                if (ndx == 1)
                {
                    outsrce_dtst.outsrce.Load(dr_univ);
                    dgv.DataSource = outsrce_dtst.outsrce;
                }
            }
        }

        //find with pieces
        private void piece(ToolStripButton sender, int ndx)
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

            string SqlString = "";
            string ConnString = "";
            if (ndx == 1)
            {
                outsrce_dtst.Clear();
                ConnString = outsrceTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM outsrce WHERE [" + who + "] LIKE '%" + what2 + "%'";
            }
            else if (ndx == 2)
            {
                empl_selc_dtst.Clear();
                ConnString = empl_selecTableAdapter.Connection.ConnectionString;
                SqlString = "Select * FROM Empl_selec WHERE [" + who + "] LIKE '%" + what2 + "%'";
            }
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
                            if (ndx == 1)
                            {
                                outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                dgv.DataSource = outsrce_dtst.outsrce;
                            }
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                if (ndx == 1)
                {
                    outsrce_dtst.outsrce.Load(dr_univ);
                    dgv.DataSource = outsrce_dtst.outsrce;
                }
            }
        }

        //dynam query -END-

        //Query Events

        //equal
        private void new_q(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            eqls(b_temp, tb.SelectedIndex);
        }

        //not equal
        private void tvvbxtnt1_Click(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            nt_eql(b_temp, tb.SelectedIndex);
        }

        private void btw_event(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            btw(b_temp, tb.SelectedIndex);
        }

        private void gt_event(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            grt_thn(b_temp, tb.SelectedIndex);
        }

        private void lss_event(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            lss_thn(b_temp, tb.SelectedIndex);
        }

        private void stwth_event(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            str_wth(b_temp, tb.SelectedIndex);
        }

        private void ew_event(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            end_wth(b_temp, tb.SelectedIndex);
        }

        private void piece_event(object sender, EventArgs e)
        {
            b_temp = (ToolStripButton)sender;
            piece(b_temp, tb.SelectedIndex);
        }

        private void showAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            oper_save();
            outsrce_dtst.Clear();
            string SqlString = "Select * FROM outsrce";
            Last_Query_Used2 = SqlString;
            if (Main.Amatrix.mgt == "")
            {
                string ConnString = outsrceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                            dgv.DataSource = outsrce_dtst.outsrce;
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                outsrce_dtst.outsrce.Load(dr_univ);
                dgv.DataSource = outsrce_dtst.outsrce;
            }
        }

        private void delete_itms(object sender, EventArgs e)
        {
            if (sender.Equals(delete) == true)
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in dgv.SelectedRows)
                    {
                        if (Convert.ToInt32(dgvr.Cells[0].Value) == 1) { }
                        else
                        {
                            howmany = howmany - 1;
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
                    }
                }
            }

            if (sender.Equals(del_all) == true)
            {
                oper_save();
                string SqlString = "DELETE FROM " + outsrce_dtst.outsrce.TableName;
                if (Main.Amatrix.mgt == "")
                {
                    string ConnString = outsrceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                outsrce_dtst.Load(reader, LoadOption.OverwriteChanges, "outsrce");
                                dgv.DataSource = null;
                                dgv.DataSource = outsrce_dtst.outsrce;
                            }
                        }
                    }
                }
                else
                {
                    dr_univ = Quer(SqlString);
                    outsrce_dtst.outsrce.Load(dr_univ);
                    dgv.DataSource = null;
                    dgv.DataSource = outsrce_dtst.outsrce;
                }
                outsrce_dtst.outsrce.Rows.Clear();
                outsrce_dtst.AcceptChanges();

                maxm = 0;
            }
        }

        private void deletecell_Click(object sender, EventArgs e)
        {
            /*if (sender.Equals(new_rw) == true)
            {*/
                if (tb.SelectedIndex == 1)
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
                    if (dgv.SelectedRows.Count > 1)
                    {
                        foreach (DataGridViewRow dgvr in dgv.SelectedRows)
                        {
                            dgv.Rows.Remove(dgvr);
                        }
                    }
                }
                else
                {
                    try
                    {
                        this.ActiveControl.Text = "";
                    }
                    catch (Exception erty) { }
                }
           // }
        }

        private void aggregates(object sender, EventArgs e)
        {
            oper_save();
            if (sender.Equals(dbtsumqry) == true)
            {
                outsrce_dtst.Clear(); string SqlString = "Select sum(Revenue) Revenue, sum(Loss) Loss, sum([Number of Employees]) [Number of Employees] FROM outsrce";
                    
                if (Main.Amatrix.mgt == "")
                {
                    string ConnString = outsrceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                dgv.DataSource = outsrce_dtst.outsrce;
                            }
                        }
                    }
                }
                else
                {
                    dr_univ = Quer(SqlString);
                    outsrce_dtst.outsrce.Load(dr_univ);
                    dgv.DataSource = outsrce_dtst.outsrce;
                }
            }
            else if (sender.Equals(avgqryjourn) == true)
            {
                outsrce_dtst.Clear(); string SqlString = "Select avg(Revenue) Revenue, avg(Loss) Loss, avg([Number of Employees]) [Number of Employees] FROM outsrce";
                if (Main.Amatrix.mgt == "")
                {
                    string ConnString = outsrceTableAdapter.Connection.ConnectionString;
                    using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                    {
                        using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                        {
                            conn.Open();
                            SqlCeDataReader reader = cmd.ExecuteReader();

                            using (reader)
                            {
                                outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                                dgv.DataSource = outsrce_dtst.outsrce;
                            }
                        }
                    }
                }
                else
                {
                    dr_univ = Quer(SqlString);
                    outsrce_dtst.outsrce.Load(dr_univ);
                    dgv.DataSource = outsrce_dtst.outsrce;
                }
            }
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            th_shwall_strt();
        }

        private Thread th_shwall;
        private delegate void del_shwall();

        private void th_shwall_strt()
        {
            th_shwall = new Thread(new ThreadStart(del_shhwall_strt));
            th_shwall.IsBackground = true;
            th_shwall.Start();
        }

        private void del_shhwall_strt()
        {
            try
            {
                this.Invoke(new del_shwall(showall_GUI));
            }
            catch (Exception erty) { general_mssg("Amatrix was Unable to Process the Request", "Process Interrupted"); }
        }

        string Last_Query_Used;
        string Last_Query_Used2;
        private void showall_GUI()
        {
            oper_save();
            emply_payr_dtst.Clear();
            string SqlString = "Select * From Employ_payrll";
            Last_Query_Used = SqlString;
            if (Main.Amatrix.mgt == "")
            {
                string ConnString = employ_payrllTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            emply_payr_dtst.Load(reader, LoadOption.PreserveChanges, "Employ_payrll");
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                emply_payr_dtst.Employ_payrll.Load(dr_univ);
                //image
                try
                {
                    if (dgv4[80, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value != DBNull.Value)
                    {
                        try
                        {
                            byte[] res1 = (byte[])dgv4[80, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value;
                            Image newImage;
                            using (MemoryStream ms = new MemoryStream(res1, 0, res1.Length))
                            {
                                newImage = Bitmap.FromStream(ms, true);
                                ms.Flush();
                                ms.Close();
                                ms.Dispose();
                            }
                            pbx.BackgroundImage = newImage;
                        }
                        catch (Exception erty)
                        {
                            pbx.BackgroundImage = (Image)dgv4[80, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value;
                        }
                    }
                    else
                    {
                        pbx.BackgroundImage = Properties.Resources.person;
                    }
                }
                catch (Exception erty) { }
            }
        }


        private void text_pic_changed(object sender, EventArgs e)
        {
            if (textBox67.Text != "")
            {
                try
                {
                    pbx.BackgroundImage = Image.FromFile(textBox67.Text);
                }
                catch (Exception erty) { }
            }
            else { pbx.BackgroundImage = Properties.Resources.person; }
        }

        private ArrayList copycutpaste = new ArrayList();
        string s_;
        private void cpy_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb.SelectedIndex == 1)
                {
                    copycutpaste.Clear();
                    if (dgv_who == 1)
                    {
                        copycutpaste.Add(dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value);
                    }
                    else if (dgv_who == 2)
                    {
                        copycutpaste.Add(dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex].Value);
                    }
                }
                else
                {
                    try
                    {
                        s_ = this.ActiveControl.Text;
                    }
                    catch (Exception erty) { }
                }
            }
            catch (Exception erty) { }
        }

        private void pster_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb.SelectedIndex == 1)
                {
                    if (dgv_who == 1)
                    {
                        dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value.ToString() + copycutpaste[0].ToString();
                    }
                    else if (dgv_who == 2)
                    {
                        dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex].Value = dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex].Value.ToString() + copycutpaste[0].ToString();
                    }
                }
                else
                {
                    try
                    {
                        this.ActiveControl.Text = this.ActiveControl.Text + s_;
                    }
                    catch (Exception erty) { }
                }
            }
            catch (Exception erty) { Am_err mer = new Am_err(); mer.tx(erty.Message); }
        }

        private void ct_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb.SelectedIndex == 1)
                {
                    copycutpaste.Clear();
                    if (dgv_who == 1)
                    {
                        copycutpaste.Add(dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value);
                        dgv.Rows[dgv.CurrentCell.RowIndex].Cells[dgv.CurrentCell.ColumnIndex].Value = null;
                    }
                    else if (dgv_who == 2)
                    {
                        copycutpaste.Add(dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex].Value);
                        dgv2.Rows[dgv2.CurrentCell.RowIndex].Cells[dgv2.CurrentCell.ColumnIndex].Value = null;
                    }
                }
                else
                {
                    try
                    {
                        s_ = this.ActiveControl.Text;
                        this.ActiveControl.Text = "";
                    }
                    catch (Exception erty) { }
                }
            }
            catch (Exception erty) { }
        }

        private void sall_Click(object sender, EventArgs e)
        {
            if (tb.SelectedIndex == 1)
            {
                if (dgv_who == 1)
                {
                    dgv.SelectAll();
                }
                else if (dgv_who == 2)
                {
                    dgv2.SelectAll();
                }
            }
        }

        private void cv_text(object sender, EventArgs e)
        {
            if (textBox66.Text == "")
            {
                bt_cv.Text = "Add C.V.";
            }
            else
            {
                bt_cv.Text = "View C.V.";
            }
        }

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.CurrentCell.ColumnIndex == 11)
                {
                    set_of_info(true);
                }
                else { set_of_info(false); }
            }
            catch (Exception erty) { }

            try
            {
                tmr.Stop();
                dgvwintic.Stop();
                tmex.Stop();
            }
            catch (Exception erty) { }
            zz_yn(true);
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

        private void zz_MouseEnter(object sender, EventArgs e)
        {
            zz.BackColor = Color.GhostWhite;
            tmr.Interval = 13000;
            tmr.Start();
        }

        private void zz_MouseLeave(object sender, EventArgs e)
        {
            zz.BackColor = Color.WhiteSmoke;
            tmr.Interval = 13000;
            tmr.Start();
        }

        private void set_quikbox()
        {
            if (Cursor.Position.X - this.Location.X <= this.Size.Width && Cursor.Position.Y - this.Location.Y <= this.Size.Height)
            {
                zz.Location = new Point((Cursor.Position.X - this.Location.X) - zz.Size.Width / 2, (Cursor.Position.Y - this.Location.Y) - 30);
            }
            else { }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            zz_yn(false);
            tmr.Stop();
        }

        private void dgv_Enter(object sender, EventArgs e)
        {
            dgv_who = 1;
        }

        private void dataGridView2_Enter(object sender, EventArgs e)
        {
            dgv_who = 2;
            editToolStripMenuItem1.Enabled = false;
            zz_yn(false);
        }

        private string search_temp;
        private void dgv2_src_txtch(object sender, EventArgs e)
        {
            oper_save();
            int start = 0; int count = 0;
            string SqlString = "Select * From Employ_payrll Where";
            Last_Query_Used = SqlString;
            foreach (DataColumn dgvc in emply_payr_dtst.Employ_payrll.Columns)
            {
                try
                {
                    if (dgvc.ColumnName.Contains("pic") == false)
                    {
                        if (start == 0)
                        {
                            if (tbxfned.Text.Contains(' ') == true)
                            {
                                SqlString = SqlString + " [" + dgvc.ColumnName + "] + ' ' + [" + emply_payr_dtst.Employ_payrll.Columns[1].ColumnName + "] LIKE '%" + toolStripTextBox14.Text + "%' AND [outsourced to office] LIKE '%" + dgv.CurrentRow.Cells[1].Value.ToString() + "%'"; ;
                            }
                            else
                            {
                                SqlString = SqlString + " [" + dgvc.ColumnName + "] LIKE '%" + toolStripTextBox14.Text + "%' AND [outsourced to office] LIKE '%" + dgv.CurrentRow.Cells[1].Value.ToString() + "%'";
                            }
                            start = 1;
                        }
                        else
                        {
                            SqlString = SqlString + " OR [" + dgvc.ColumnName + "] LIKE '%" + toolStripTextBox14.Text + "%' AND [outsourced to office] LIKE '%" + dgv.CurrentRow.Cells[1].Value.ToString() + "%'"; ;
                        }
                    }
                }
                catch (Exception ertty) { }
            }
            emply_payr_dtst.Clear();
            if (Main.Amatrix.mgt == "")
            {
                string ConnString = employ_payrllTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            emply_payr_dtst.Load(reader, LoadOption.PreserveChanges, "Employ_payrll");
                            dgv2.DataSource = emply_payr_dtst.Employ_payrll;
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                emply_payr_dtst.Employ_payrll.Load(dr_univ);
                dgv2.DataSource = emply_payr_dtst.Employ_payrll;
            }
        }

        private void remv_zz_Click(object sender, EventArgs e)
        {
            zz_yn(false);
        }

        private void clc_opn_Click(object sender, EventArgs e)
        {
            try
            {
                Calculator_1 clc = new Calculator_1();
                clc.Show();
            }
            catch (Exception erty) { }
        }

        private void ext_io_Click(object sender, EventArgs e)
        {
            set_of_info(false);
        }

        private void lio_selndx(object sender, EventArgs e)
        {
            try
            {
                txt_oi_edt.Text = listView5.FocusedItem.SubItems[1].Text;
            }
            catch (Exception erty) { }
        }

        private void sve_oi_Click(object sender, EventArgs e)
        {
            richTextBox4.Clear();
            foreach (ListViewItem itm in listView5.Items)
            {
                richTextBox4.Text = richTextBox4.Text + itm.SubItems[1].Text + '\n';
            }
            dgv[11, dgv.CurrentRow.Index].Value = richTextBox4.Text.Replace('\n', ';');
        }

        private void txt_oi_edt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                listView5.FocusedItem.SubItems[1].Text = txt_oi_edt.Text;
            }
            catch (Exception erty) {  }
        }

        private void remve_oi_Click(object sender, EventArgs e)
        {
            try
            {
                listView5.FocusedItem.SubItems[1].Text = "None";
            }
            catch (Exception erty) { }
        }
        //Query Quik Access

        //Equal to
        private void eqlmnuopn_Click(object sender, EventArgs e)
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
        private ToolStripItem tsmi_temp;
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
        }

        //QA open

        private void QA_cmd_DropDownOpened(object sender, EventArgs e)
        {
            if (dgv.CurrentCell.ColumnIndex == 3 || dgv.CurrentCell.ColumnIndex == 5)// || dgv.CurrentCell.ColumnIndex == 4 || dgv.CurrentCell.ColumnIndex >= 25 && dgv.CurrentCell.ColumnIndex <= 31)
            {//numerical
                QA_grth.Enabled = true;
                QA_lssth.Enabled = true;
                QA_btw.Enabled = true;

                QA_endwth.Enabled = false;
                QA_stwth.Enabled = false;
                QA_ad.Enabled = false;
                QA_bd.Enabled = false;
            }
            else if (dgv.CurrentCell.ColumnIndex == 7 || dgv.CurrentCell.ColumnIndex == 8 || dgv.CurrentCell.ColumnIndex == 4)
            {//date
                QA_grth.Enabled = false;
                QA_lssth.Enabled = false;
                QA_btw.Enabled = true;

                QA_endwth.Enabled = false;
                QA_stwth.Enabled = false;
                QA_ad.Enabled = true;
                QA_bd.Enabled = true;
            }
            else if (dgv.CurrentCell.ColumnIndex == 11 || dgv.CurrentCell.ColumnIndex >= 0 && dgv.CurrentCell.ColumnIndex <= 2 || dgv.CurrentCell.ColumnIndex == 12)
            {//alpha
                QA_grth.Enabled = false;
                QA_lssth.Enabled = false;
                QA_btw.Enabled = false;

                QA_endwth.Enabled = true;
                QA_stwth.Enabled = true;
                QA_ad.Enabled = false;
                QA_bd.Enabled = false;
            }
        }

        private void edt_empl_Click(object sender, EventArgs e)
        {
            tb.SelectTab(0);
        }

        private ToolStripTextBox txtb_bycell;
        private void dgv_cellbyndx(object sender, EventArgs e)
        {
            txtb_bycell = (ToolStripTextBox)sender; th_src_strt();
        }

        private Thread th_src;
        private delegate void del_src();

        private void th_src_strt()
        {
            try
            {
                th_src = new Thread(new ThreadStart(del_src_strt));
                th_src.IsBackground = true;
                th_src.Start();
            }
            catch (Exception erty) { }
        }

        private void del_src_strt()
        {
            try
            {
                this.Invoke(new del_src(dgv_src));
            }
            catch (Exception erty) { }
        }

        private void dgv_src()
        {
            oper_save();
            outsrce_dtst.Clear();
            string SqlString = "Select * FROM outsrce WHERE [Outsourced Office] LIKE '%" + txtb_bycell.Text + "%'";
            if (Main.Amatrix.mgt == "")
            {
                string ConnString = outsrceTableAdapter.Connection.ConnectionString;
                using (SqlCeConnection conn = new SqlCeConnection(ConnString))
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(SqlString, conn))
                    {
                        conn.Open();
                        SqlCeDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            outsrce_dtst.Load(reader, LoadOption.PreserveChanges, "outsrce");
                            dgv.DataSource = outsrce_dtst.outsrce;
                        }
                    }
                }
            }
            else
            {
                dr_univ = Quer(SqlString);
                outsrce_dtst.outsrce.Load(dr_univ);
                dgv.DataSource = outsrce_dtst.outsrce;
            }
        }

        private void x(object sender, EventArgs e)
        {
            txtb_bycell = (ToolStripTextBox)sender;
            try
            {
                dgv.CurrentCell = dgv[Convert.ToInt32(txtb_bycell.Text), dgv.CurrentRow.Index];
            }
            catch (Exception erty) { }
        }

        private void bindingNavigatorPositionItem1_TextChanged(object sender, EventArgs e)
        {
            sum_all();
        }

        private void sum_all()
        {
            double tot = 0;

            listView3.Items.Clear();
            listView3.Items.Add("Salary", 10);
            listView3.Items.Add("Over-Hours", 10);
            listView3.Items[0].SubItems.Add(textBox46.Text);
            listView3.Items[1].SubItems.Add(textBox50.Text);
            try
            {
                tot = Convert.ToDouble(textBox46.Text);
            }
            catch (Exception erty) { }
            try
            {
                tot = tot + Convert.ToDouble(textBox50.Text);
            }
            catch (Exception erty2) { }
            listView3.Items.Add("Benefits", 10);
            listView3.Items[2].SubItems.Add(textBox51.Text);
            try { tot = tot + Convert.ToDouble(textBox51.Text); }
            catch (Exception ty) { }
            listView3.Items.Add("Bonuses", 10);
            listView3.Items[3].SubItems.Add(textBox52.Text);
            try { tot = tot + Convert.ToDouble(textBox52.Text); }
            catch (Exception erty1223) { }
            listView3.Items.Add("Tax", 10);
            listView3.Items[4].SubItems.Add(textBox53.Text);
            try { tot = tot + Convert.ToDouble(textBox53.Text); }
            catch (Exception ertt66) { }
            listView3.Items.Add("Insurance", 10);
            double dbtemp = 0;
            double dbtemp2 = 0;

            try
            {
                dbtemp = Convert.ToDouble(textBox61.Text);
            }
            catch (Exception ertysex) { }
            try
            {
                dbtemp2 = Convert.ToDouble(textBox62.Text);
            }
            catch (Exception ertysex2) { }
            listView3.Items[5].SubItems.Add((dbtemp + dbtemp2).ToString());
            try { tot = tot + (dbtemp2 + dbtemp); }
            catch (Exception ertyuio) { }
            ListViewItem lvt = new ListViewItem("Grand Total", 10);
            lvt.BackColor = Color.Gainsboro;
            Font fnt = new Font(lvt.Font.FontFamily.GetName(0), lvt.Font.Size, FontStyle.Bold);
            lvt.Font = fnt;
            listView3.Items.Add(lvt);
            listView3.Items[6].SubItems.Add(tot.ToString());
        }

        private void efrsums_Click(object sender, EventArgs e)
        {
            sum_all();
        }

        void mgmt_hr_Disposed(object sender, EventArgs e)
        {
            dgv.DataSource = null;
            dgv2.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView4.DataSource = null;

            attndnce_dtst.Clear();
            empl_selc_dtst.Clear();
            emply_payr_dtst.Clear();
            outsrce_dtst.Clear();
            peyed_dtst.Clear();
            prjmgmt_dtst.Clear();

            attendance_ManagmentTableAdapter1.Connection.Close();
            empl_selecTableAdapter.Connection.Close();
            employ_payrllTableAdapter.Connection.Close();
            outsrceTableAdapter.Connection.Close();
            payedonTableAdapter.Connection.Close();
            prj_mgmtTableAdapter.Connection.Close();

            attendanceManagmentBindingSource1.EndEdit();
            emplselecBindingSource.EndEdit();
            employpayrllBindingSource.EndEdit();
            outsrceBindingSource.EndEdit();
            payedonBindingSource.EndEdit();
            prjmgmtBindingSource.EndEdit();

            attndnce_dtst.Dispose();
            empl_selc_dtst.Dispose();
            emply_payr_dtst.Dispose();
            outsrce_dtst.Dispose();
            peyed_dtst.Dispose();
            prjmgmt_dtst.Dispose();

            attendance_ManagmentTableAdapter1.Dispose();
            empl_selecTableAdapter.Dispose();
            employ_payrllTableAdapter.Dispose();
            outsrceTableAdapter.Dispose();
            payedonTableAdapter.Dispose();
            prj_mgmtTableAdapter.Dispose();

            attendanceManagmentBindingSource1.Dispose();
            emplselecBindingSource.Dispose();
            employpayrllBindingSource.Dispose();
            outsrceBindingSource.Dispose();
            payedonBindingSource.Dispose();
            prjmgmtBindingSource.Dispose();

            bttn_sync.Click -= bttn_sync_Click;
            bkk_sync.DoWork -= bkk_sync_DoWork;
            dataGridView2.DataError -= dataGridView2_DataError;
            toolStripButton13.Click -= toolStripButton13_Click;
            toolStripButton5.Click -= bindingNavigatorDeleteItem2_Click;
            button6.Click -= button6_Click_1;
            dgv2.Leave -= dgv2_Leave;
            dgv.CellBeginEdit -= dgv_CellBeginEdit_1;
            tbx_time.SelectionChanged -= tbx_time_SelectionChanged;
            button8.Click -= print_;
            fx_sum.Click -= OverhourPay_tot;
            dataGridView4.DataError -= dataGridView4_DataError;
            dataGridView4.CellBeginEdit -= dataGridView4_CellBeginEdit;
            toolStripButton4.Click -= toolStripButton4_Click;
            toolStripButton8.Click -= toolStripButton8_Click;
           // button2.Click -= button2_Click_1;
            pbx.BackgroundImageChanged -= pbx_BackgroundImageChanged;
            button11.Click -= button11_Click;
            eml_snd.Click -= eml_snd_Click;
            bindingNavigatorPositionItem1.MouseEnter -= tvtxt1_MouseEnter;
            bindingNavigatorPositionItem1.MouseLeave -= tvtxt1_MouseLeave;
            saveToolStripMenuItem.Click -= svebtn_ButtonClick;
            //dgv3.CellEnter -= dgv3_CellEnter;
            //toolStripButton5.Click -= toolStripButton5_Click;
            bindingNavigatorPositionItem.TextChanged -= bindingNavigatorPositionItem_TextChanged;
            this.Deactivate -= this.mgmt_hr_Deactivate;
            this.Disposed -= mgmt_hr_Disposed;
            this.Load -= this.mgmt_hr_Load;
            this.Activated -= this.mgmt_hr_Activated;
            this.tbxfned.Leave -= this.tbxfned_Leave;
            this.tbxfned.Enter -= this.tbxfned_Enter;
            this.tbxfned.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tbxfned.MouseLeave -= this.tvtxt1_MouseLeave;
            this.gotoitm.ButtonClick -= this.gotoitm_Click;
            this.src_all.Click -= this.search_who;
            this.src_ev.Click -= this.search_who;
            this.src_py.Click -= this.search_who;
            //this.toolStripButton54.Click -= this.edt_Click;
            this.pnt.ButtonClick -= this.print_;
            this.printOutSourcedEmployeesToolStripMenuItem.Click -= this.print_;
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
            this.clc_opn.Click -= this.clc_opn_Click;
            this.svebtn.Click -= this.svebtn_ButtonClick;
            this.clse.MouseLeave -= this.clse_MouseLeave;
            this.clse.ButtonClick -= this.clse_ButtonClick;
            this.clse.MouseEnter -= this.clse_MouseEnter;
            this.rstrt.Click -= this.rstrt_Click;
            this.connlbl.MouseEnter -= this.connlbl_MouseEnter;
            this.connlbl.MouseLeave -= this.connlbl_MouseLeave;
            this.connlbl.Click -= this.connlbl_Click;
            this.tb.SelectedIndexChanged -= this.tb_SelectedIndexChanged;
            this.pbx.MouseLeave -= this.pbx_MouseLeave;
            this.pbx.Click -= this.pbx_Click;
            this.pbx.MouseEnter -= this.pbx_MouseEnter;
            this.textBox67.TextChanged -= this.text_pic_changed;
            this.button4.Click -= this.button4_Click;
            this.textBox66.TextChanged -= this.cv_text;
            //this.button8.Click -= this.edt_Click;
            //this.button2.Click -= this.button2_Click;
            this.remv_pic.Click -= this.remv_pic_Click;
            this.bt_cv.Click -= this.bt_cv_Click;
            /*this.textBox4.TextChanged -= this.Gen;
            this.textBox4.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox4.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox36.TextChanged -= this.Gen;
            this.textBox36.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox36.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox37.TextChanged -= this.Gen;
            this.textBox37.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox37.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox41.TextChanged -= this.Gen;
            this.textBox41.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox41.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox38.TextChanged -= this.Gen;
            this.textBox38.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox38.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox40.TextChanged -= this.Gen;
            this.textBox40.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox40.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox39.TextChanged -= this.Gen;
            this.textBox39.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox39.MouseEnter -= this.tvtxt1_MouseEnter;*/
            this.comboBox6.DropDown -= this.Gen;
            this.comboBox6.TextChanged -= this.Gen;
            this.textBox44.TextChanged -= this.Gen;
            this.textBox44.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox44.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox47.TextChanged -= this.Gen;
            this.textBox47.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox47.MouseEnter -= this.tvtxt1_MouseEnter;
            this.set_outsource_office.DropDown -= this.Gen;
            this.set_outsource_office.TextChanged -= this.Gen;
            this.comboBox11.DropDown -= this.Gen;
            this.comboBox11.TextChanged -= this.Gen;
            //this.textBox32.TextChanged -= this.Gen;
            //this.textBox32.MouseLeave -= this.tvtxt1_MouseLeave;
            //this.textBox32.MouseEnter -= this.tvtxt1_MouseEnter;
            this.comboBox4.DropDown -= this.Gen;
            this.comboBox4.TextChanged -= this.Gen;
            this.textBox31.TextChanged -= this.Gen;
            this.textBox31.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox31.MouseEnter -= this.tvtxt1_MouseEnter;
            //this.textBox58.TextChanged -= this.Gen;
            //this.textBox58.MouseLeave -= this.tvtxt1_MouseLeave;
            //this.textBox58.MouseEnter -= this.tvtxt1_MouseEnter;
            //this.textBox35.TextChanged -= this.Gen;
            //this.textBox35.MouseLeave -= this.tvtxt1_MouseLeave;
            //this.textBox35.MouseEnter -= this.tvtxt1_MouseEnter;
            //this.textBox34.TextChanged -= this.Gen;
            //this.textBox34.MouseLeave -= this.tvtxt1_MouseLeave;
            //this.textBox34.MouseEnter -= this.tvtxt1_MouseEnter;
            //this.textBox33.TextChanged -= this.Gen;
            //this.textBox33.MouseLeave -= this.tvtxt1_MouseLeave;
            //this.textBox33.MouseEnter -= this.tvtxt1_MouseEnter;
            this.elim_shift.Click -= this.elim_shift_Click;
            this.button23.Click -= this.button23_Click;
            this.comboBox10.DropDown -= this.Gen;
            this.comboBox10.TextChanged -= this.Gen;
            this.comboBox12.DropDown -= this.Gen;
            this.comboBox12.TextChanged -= this.Gen;
            this.comboBox9.DropDown -= this.Gen;
            this.comboBox9.TextChanged -= this.Gen;
            this.comboBox7.DropDown -= this.Gen;
            this.comboBox7.TextChanged -= this.Gen;
            this.comboBox24.DropDown -= this.Gen;
            this.comboBox23.DropDown -= this.Gen;
            this.button21.Click -= this.button4_Click;
            this.textBox30.TextChanged -= this.Gen;
            this.textBox30.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox30.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox28.TextChanged -= this.Gen;
            this.textBox28.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox28.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox29.TextChanged -= this.Gen;
            this.textBox29.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox29.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox27.TextChanged -= this.Gen;
            this.textBox27.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox27.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox26.TextChanged -= this.Gen;
            this.textBox26.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox26.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox5.TextChanged -= this.Gen;
            this.textBox5.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox5.MouseEnter -= this.tvtxt1_MouseEnter;
            /*this.toolStripButton14.Click -= this.button3_Click;
            this.comboBox3.DropDown -= this.Gen;
            this.comboBox3.TextChanged -= this.Gen;
            this.gp.MouseLeave -= this.tvtxt1_MouseLeave;
            this.gp.Enter -= this.gp_Enter;
            this.gp.MouseEnter -= this.tvtxt1_MouseEnter;
            this.comboBox2.SelectedIndexChanged -= this.currency_changed;
            this.comboBox2.DropDown -= this.Gen;*/
            this.textBox23.TextChanged -= this.Gen;
            this.textBox23.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox23.MouseEnter -= this.tvtxt1_MouseEnter;
            //this.textBox20.TextChanged -= this.Gen;
            //this.textBox20.MouseLeave -= this.tvtxt1_MouseLeave;
            //this.textBox20.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox21.TextChanged -= this.Gen;
            this.textBox21.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox21.MouseEnter -= this.tvtxt1_MouseEnter;
            /*this.textBox18.TextChanged -= this.Gen;
            this.textBox18.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox18.MouseEnter -= this.tvtxt1_MouseEnter;*/
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
            this.comboBox5.DropDown -= this.Gen;
            this.textBox7.TextChanged -= this.Gen;
            this.textBox7.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox7.MouseEnter -= this.tvtxt1_MouseEnter;
            this.checkBox1.CheckedChanged -= this.Gen;
            this.comboBox1.DropDown -= this.Gen;
            this.dateTimePicker2.ValueChanged -= this.Gen;
            this.dateTimePicker2.DropDown -= this.Gen;
            this.textBox6.TextChanged -= this.Gen;
            this.textBox6.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox6.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox3.TextChanged -= this.Gen;
            this.textBox3.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox3.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox1.TextChanged -= this.Gen;
            this.textBox1.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox1.MouseEnter -= this.tvtxt1_MouseEnter;
            this.button7.Click -= this.button4_Click;
            this.button10.Click -= this.button4_Click;
            this.textBox10.TextChanged -= this.Gen;
            this.textBox10.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox10.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox11.TextChanged -= this.Gen;
            this.textBox11.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox11.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox8.TextChanged -= this.Gen;
            this.textBox8.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox8.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox9.TextChanged -= this.Gen;
            this.textBox9.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox9.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox12.TextChanged -= this.Gen;
            this.textBox12.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox12.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox13.TextChanged -= this.Gen;
            this.textBox13.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox13.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripButton35.Click -= this.toolStripButton35_Click;
            this.toolStripButton6.Click -= this.toolStripButton6_Click;
            this.bindingNavigatorPositionItem.MouseEnter -= this.tvtxt1_MouseEnter;
            this.bindingNavigatorPositionItem.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox2.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox2.MouseLeave -= this.tvtxt1_MouseLeave;
            this.efrsums.Click -= this.efrsums_Click;
            this.textBox63.TextChanged -= this.Gen;
            this.textBox63.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox63.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox62.TextChanged -= this.Gen;
            this.textBox62.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox62.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox61.TextChanged -= this.Gen;
            this.textBox61.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox61.MouseEnter -= this.tvtxt1_MouseEnter;
            this.button25.Click -= this.tax_changed;
            this.textBox60.TextChanged -= this.tax_changed;
            this.textBox60.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox60.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox59.TextChanged -= this.tax_changed;
            this.textBox59.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox59.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox57.TextChanged -= this.tax_changed;
            this.textBox57.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox57.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox56.TextChanged -= this.tax_changed;
            this.textBox56.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox56.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox55.TextChanged -= this.tax_changed;
            this.textBox55.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox55.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox54.TextChanged -= this.tax_changed;
            this.textBox54.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox54.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox53.TextChanged -= this.Gen;
            this.textBox53.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox53.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tabControl6.SelectedIndexChanged -= this.tabControl6_SelectedIndexChanged;
            this.textBox51.TextChanged -= this.Gen;
            this.textBox51.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox51.MouseEnter -= this.tvtxt1_MouseEnter;
            this.comboBox19.SelectedIndexChanged -= this.currency_changed;
            this.comboBox19.DropDown -= this.Gen;
            this.textBox52.TextChanged -= this.Gen;
            this.textBox52.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox52.MouseEnter -= this.tvtxt1_MouseEnter;
            this.comboBox20.SelectedIndexChanged -= this.currency_changed;
            this.comboBox20.DropDown -= this.Gen;
            this.textBox50.TextChanged -= this.Gen;
            this.textBox50.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox50.MouseEnter -= this.tvtxt1_MouseEnter;
            this.comboBox18.SelectedIndexChanged -= this.OverhourPay_tot;
            this.comboBox18.DropDown -= this.Gen;
            this.comboBox18.TextChanged -= this.Gen;
            this.textBox49.TextChanged -= this.Gen;
            this.textBox49.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox49.MouseEnter -= this.tvtxt1_MouseEnter;
            this.comboBox17.SelectedIndexChanged -= this.currency_changed;
            this.comboBox17.DropDown -= this.Gen;
            this.button3.Click -= this.button3_Click;
            this.button1.Click -= this.button1_Click;
            this.comboBox16.SelectedIndexChanged -= this.currency_changed;
            this.comboBox16.DropDown -= this.Gen;
            this.textBox48.TextChanged -= this.Gen;
            this.textBox48.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox48.MouseEnter -= this.tvtxt1_MouseEnter;
            this.whereFebuaryHas28DaysToolStripMenuItem.Click -= this.button1_Click;
            this.whereFebuaryHas29DaysToolStripMenuItem.Click -= this.button3_Click;
            this.textBox42.TextChanged -= this.Gen;
            this.textBox42.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox42.MouseEnter -= this.tvtxt1_MouseEnter;
            this.textBox46.TextChanged -= this.Gen;
            this.textBox46.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox46.MouseEnter -= this.tvtxt1_MouseEnter;
            this.comboBox13.SelectedIndexChanged -= this.currency_changed;
            this.comboBox13.DropDown -= this.Gen;
            this.textBox43.TextChanged -= this.Gen;
            this.textBox43.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox43.MouseEnter -= this.tvtxt1_MouseEnter;
            this.comboBox15.SelectedIndexChanged -= this.currency_changed;
            this.comboBox15.DropDown -= this.Gen;
            this.comboBox14.SelectedIndexChanged -= this.currency_changed;
            this.comboBox14.DropDown -= this.Gen;
            this.textBox45.TextChanged -= this.Gen;
            this.textBox45.MouseLeave -= this.tvtxt1_MouseLeave;
            this.textBox45.MouseEnter -= this.tvtxt1_MouseEnter;
            this.dataGridView2.CellBeginEdit -= this.dataGridView2_CellBeginEdit;
            this.dataGridView2.CellEnter -= this.dataGridView2_CellEnter;
            this.sve_po.Click -= this.sve_po_Click;
            this.toolStripButton3.Click -= this.toolStripButton35_Click;
            this.listView5.SelectedIndexChanged -= this.lio_selndx;
            this.txt_oi_edt.MouseEnter -= this.tvtxt1_MouseEnter;
            this.txt_oi_edt.MouseLeave -= this.tvtxt1_MouseLeave;
            this.txt_oi_edt.TextChanged -= this.txt_oi_edt_TextChanged;
            this.sve_oi.Click -= this.sve_oi_Click;
            this.remve_oi.Click -= this.remve_oi_Click;
            this.ext_io.Click -= this.ext_io_Click;
            this.zz.MouseLeave -= this.zz_MouseLeave;
            this.zz.MouseEnter -= this.zz_MouseEnter;
            this.toolStrip25.MouseEnter -= this.zz_MouseEnter;
            this.toolStrip25.MouseLeave -= this.zz_MouseLeave;
            this.QA_cmd.DropDownOpened -= this.QA_cmd_DropDownOpened;
            this.equalToToolStripMenuItem2.Click -= this.eqlmnuopn_Click;
            this.notEqualToToolStripMenuItem.Click -= this.nteqlshrt_Click;
            this.QA_grth.Click -= this.grtrthn_Click;
            this.QA_lssth.Click -= this.lessthn_Click;
            this.QA_btw.Click -= this.btween_Click;
            this.QA_stwth.Click -= this.startwid_Click;
            this.QA_endwth.Click -= this.QA_endwth_Click;
            this.QA_bd.Click -= this.QA_bd_Click;
            this.QA_ad.Click -= this.QA_ad_Click;
            this.toolStripButton37.Click -= this.undoall_Click;
            this.toolStripButton38.Click -= this.cpy_Click;
            this.toolStripButton39.Click -= this.ct_Click;
            this.toolStripButton51.Click -= this.pster_Click;
            this.toolStripButton52.Click -= this.sall_Click;
            this.toolStripButton53.Click -= this.deletecell_Click;
            this.remv_zz.Click -= this.remv_zz_Click;
            this.dgv.CellMouseLeave -= this.dgv_CellMouseLeave;
            this.dgv.Enter -= this.dgv_Enter;
            this.dgv.RowEnter -= this.dgv_RowEnter_1;
            this.dgv.CellMouseEnter -= this.dgv_CellMouseEnter;
            this.dgv.CellEnter -= this.dgv_CellEnter;
            this.showAllToolStripMenuItem1.Click -= this.showAllToolStripMenuItem1_Click;
            this.toolStripTextBox9.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox9.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox9.Click -= this.tvtxt20_Click;
            this.toolStripButton22.Click -= this.new_q;
            this.toolStripTextBox10.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox10.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox10.Click -= this.tvtxt20_Click;
            this.toolStripButton26.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox11.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox11.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox11.Click -= this.tvtxt20_Click;
            this.toolStripButton27.Click -= this.stwth_event;
            this.toolStripTextBox12.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox12.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox12.Click -= this.tvtxt20_Click;
            this.toolStripButton28.Click -= this.ew_event;
            this.toolStripTextBox13.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox13.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox13.Click -= this.tvtxt20_Click;
            this.toolStripButton29.Click -= this.piece_event;
            this.toolStripTextBox24.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox24.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox24.Click -= this.tvtxt20_Click;
            this.toolStripButton45.Click -= this.new_q;
            this.toolStripTextBox25.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox25.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox25.Click -= this.tvtxt20_Click;
            this.toolStripButton46.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox26.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox26.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox26.Click -= this.tvtxt20_Click;
            this.toolStripButton47.Click -= this.stwth_event;
            this.toolStripTextBox27.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox27.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox27.Click -= this.tvtxt20_Click;
            this.toolStripButton48.Click -= this.ew_event;
            this.toolStripTextBox28.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox28.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox28.Click -= this.tvtxt20_Click;
            this.toolStripButton49.Click -= this.piece_event;
            this.toolStripButton79.Click -= this.new_q;
            this.toolStripButton80.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox57.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox57.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox57.Click -= this.tvtxt20_Click;
            this.toolStripTextBox58.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox58.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox58.Click -= this.tvtxt20_Click;
            this.toolStripButton81.Click -= this.btw_event;
            this.toolStripButton82.Click -= this.gt_event;
            this.toolStripButton83.Click -= this.lss_event;
            this.tvtxt20.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tvtxt20.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tvtxt20.Click -= this.tvtxt20_Click;
            this.tvvbtxt17.Click -= this.new_q;
            this.tvtxtnt2.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tvtxtnt2.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tvtxtnt2.Click -= this.tvtxt20_Click;
            this.tvvbtxtnt2.Click -= this.tvvbxtnt1_Click;
            this.tvtxtlate3.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tvtxtlate3.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tvtxtlate3.Click -= this.tvtxt20_Click;
            this.tvtxtlate4.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tvtxtlate4.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tvtxtlate4.Click -= this.tvtxt20_Click;
            this.tvvtxtlate3.Click -= this.btw_event;
            this.tvtxt21.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tvtxt21.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tvtxt21.Click -= this.tvtxt20_Click;
            this.tvvbtxt18.Click -= this.lss_event;
            this.tvtxt22.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tvtxt22.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tvtxt22.Click -= this.tvtxt20_Click;
            this.tvvbtxt19.Click -= this.gt_event;
            this.toolStripButton84.Click -= this.new_q;
            this.toolStripButton85.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox59.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox59.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox59.Click -= this.tvtxt20_Click;
            this.toolStripTextBox60.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox60.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox60.Click -= this.tvtxt20_Click;
            this.toolStripButton86.Click -= this.btw_event;
            this.toolStripButton87.Click -= this.gt_event;
            this.toolStripButton88.Click -= this.lss_event;
            this.toolStripButton89.Click -= this.new_q;
            this.toolStripButton90.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox61.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox61.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox61.Click -= this.tvtxt20_Click;
            this.toolStripTextBox62.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox62.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox62.Click -= this.tvtxt20_Click;
            this.toolStripButton91.Click -= this.btw_event;
            this.toolStripButton92.Click -= this.gt_event;
            this.toolStripButton93.Click -= this.lss_event;
            this.toolStripTextBox3.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox3.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox3.KeyUp -= this.date_helper;
            this.toolStripTextBox3.Click -= this.tvtxt20_Click;
            this.toolStripButton30.Click -= this.new_q;
            this.toolStripTextBox4.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox4.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox4.KeyUp -= this.date_helper;
            this.toolStripTextBox4.Click -= this.tvtxt20_Click;
            this.toolStripButton31.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox5.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox5.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox5.KeyUp -= this.date_helper;
            this.toolStripTextBox5.Click -= this.tvtxt20_Click;
            this.toolStripTextBox6.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox6.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox6.KeyUp -= this.date_helper;
            this.toolStripTextBox6.Click -= this.tvtxt20_Click;
            this.toolStripButton32.Click -= this.btw_event;
            this.toolStripTextBox7.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox7.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox7.KeyUp -= this.date_helper;
            this.toolStripTextBox7.Click -= this.tvtxt20_Click;
            this.toolStripButton33.Click -= this.lss_event;
            this.toolStripTextBox8.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox8.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox8.KeyUp -= this.date_helper;
            this.toolStripTextBox8.Click -= this.tvtxt20_Click;
            this.toolStripButton34.Click -= this.gt_event;
            this.toolStripTextBox29.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox29.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox29.KeyUp -= this.date_helper;
            this.toolStripTextBox29.Click -= this.tvtxt20_Click;
            this.toolStripButton23.Click -= this.new_q;
            this.toolStripTextBox63.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox63.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox63.KeyUp -= this.date_helper;
            this.toolStripTextBox63.Click -= this.tvtxt20_Click;
            this.toolStripButton24.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox64.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox64.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox64.KeyUp -= this.date_helper;
            this.toolStripTextBox64.Click -= this.tvtxt20_Click;
            this.toolStripTextBox65.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox65.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox65.KeyUp -= this.date_helper;
            this.toolStripTextBox65.Click -= this.tvtxt20_Click;
            this.toolStripButton50.Click -= this.btw_event;
            this.toolStripTextBox66.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox66.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox66.KeyUp -= this.date_helper;
            this.toolStripTextBox66.Click -= this.tvtxt20_Click;
            this.toolStripButton94.Click -= this.lss_event;
            this.toolStripTextBox67.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox67.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox67.KeyUp -= this.date_helper;
            this.toolStripTextBox67.Click -= this.tvtxt20_Click;
            this.toolStripButton95.Click -= this.gt_event;
            this.toolStripTextBox19.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox19.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox19.Click -= this.tvtxt20_Click;
            this.toolStripButton40.Click -= this.new_q;
            this.toolStripTextBox20.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox20.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox20.Click -= this.tvtxt20_Click;
            this.toolStripButton41.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox21.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox21.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox21.Click -= this.tvtxt20_Click;
            this.toolStripButton42.Click -= this.stwth_event;
            this.toolStripTextBox22.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox22.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox22.Click -= this.tvtxt20_Click;
            this.toolStripButton43.Click -= this.ew_event;
            this.toolStripTextBox23.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox23.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox23.Click -= this.tvtxt20_Click;
            this.toolStripButton44.Click -= this.piece_event;
            this.dbtsumqry.Click -= this.aggregates;
            this.avgqryjourn.Click -= this.aggregates;
            this.delete.ButtonClick -= this.delete_itms;
            this.del_all.Click -= this.delete_itms;
            this.new_rw.Click -= this.deletecell_Click;
            this.dgvupall.Click -= this.dgvupall_Click;
            this.dgvupone.ButtonClick -= this.dgvupall_Click;
            this.uptxtdgv.TextChanged -= this.dgv_cellbyndx;
            this.dgvleftall.Click -= this.dgvupall_Click;
            this.dgvleftone.ButtonClick -= this.dgvupall_Click;
            this.leftxtdgv.TextChanged -= this.x;
            this.dgvrightone.ButtonClick -= this.dgvupall_Click;
            this.dgvtxtright.TextChanged -= this.x;
            this.dgvrightall.Click -= this.dgvupall_Click;
            this.dgvdownone.ButtonClick -= this.dgvupall_Click;
            this.dgvtxtleft.TextChanged -= this.dgv_cellbyndx;
            this.dgvdownall.Click -= this.dgvupall_Click;
            this.dgv2.CellMouseLeave -= this.dgv_CellMouseLeave;
            this.dgv2.Enter -= this.dataGridView2_Enter;
            this.dgv2.CellMouseEnter -= this.dgv_CellMouseEnter;
            this.dgv2upall.Click -= this.dgvupall_Click;
            this.dgv2upone.Click -= this.dgvupall_Click;
            this.dgv2leftall.Click -= this.dgvupall_Click;
            this.dgv2leftone.Click -= this.dgvupall_Click;
            this.dgv2rightone.Click -= this.dgvupall_Click;
            this.dgv2rightall.Click -= this.dgvupall_Click;
            this.dgv2dwnone.Click -= this.dgvupall_Click;
            this.dgv2dwnall.Click -= this.dgvupall_Click;
            this.toolStripSplitButton6.ButtonClick -= this.dgv2_src_txtch;
            this.toolStripTextBox14.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox14.MouseLeave -= this.tvtxt1_MouseLeave;
            this.edt_empl.Click -= this.edt_empl_Click;
            this.toolStripButton2.Click -= this.print_;
            this.toolStripButton25.Click -= this.svebtn_ButtonClick;
            /*this.dgv3.CellMouseLeave -= this.dgv_CellMouseLeave;
            this.dgv3.CellMouseEnter -= this.dgv_CellMouseEnter;
            this.dgv3.CellEndEdit -= this.dgv2_CellEndEdit;
            this.dgv3.DataError -= this.dgv_DataError;
            this.fst_sel.Click -= this.choose_select;
            this.sec_sel.Click -= this.choose_select;
            this.thi_sel.Click -= this.choose_select;
            this.fin_sel.Click -= this.choose_select;
            this.guu_dgv3.Click -= this.dgv3_src;
            this.tscb2.Leave -= this.tbxfned_Leave;
            this.tscb2.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tscb2.Enter -= this.tbxfned_Enter;
            this.tscb2.MouseEnter -= this.tvtxt1_MouseEnter;
            this.tscb.Leave -= this.tbxfned_Leave;
            this.tscb.MouseLeave -= this.tvtxt1_MouseLeave;
            this.tscb.Enter -= this.tbxfned_Enter;
            this.tscb.DropDown -= this.Virtual_Combo;
            this.tscb.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripMenuItem18.Click -= this.shw_all_recruit;
            this.toolStripTextBox15.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox15.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox15.Click -= this.tvtxt20_Click;
            this.toolStripButton55.Click -= this.new_q;
            this.toolStripTextBox16.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox16.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox16.Click -= this.tvtxt20_Click;
            this.toolStripButton56.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox17.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox17.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox17.Click -= this.tvtxt20_Click;
            this.toolStripButton57.Click -= this.stwth_event;
            this.toolStripTextBox18.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox18.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox18.Click -= this.tvtxt20_Click;
            this.toolStripButton58.Click -= this.ew_event;
            this.toolStripTextBox30.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox30.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox30.Click -= this.tvtxt20_Click;
            this.toolStripButton59.Click -= this.piece_event;
            this.toolStripTextBox31.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox31.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox31.Click -= this.tvtxt20_Click;
            this.toolStripButton63.Click -= this.new_q;
            this.toolStripTextBox32.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox32.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox32.Click -= this.tvtxt20_Click;
            this.toolStripButton64.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox33.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox33.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox33.Click -= this.tvtxt20_Click;
            this.toolStripButton65.Click -= this.stwth_event;
            this.toolStripTextBox34.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox34.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox34.Click -= this.tvtxt20_Click;
            this.toolStripButton66.Click -= this.ew_event;
            this.toolStripTextBox35.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox35.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox35.Click -= this.tvtxt20_Click;
            this.toolStripButton67.Click -= this.piece_event;
            this.toolStripButton68.Click -= this.new_q;
            this.toolStripButton69.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox36.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox36.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox36.Click -= this.tvtxt20_Click;
            this.toolStripTextBox37.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox37.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox37.Click -= this.tvtxt20_Click;
            this.toolStripButton70.Click -= this.btw_event;
            this.toolStripButton71.Click -= this.gt_event;
            this.toolStripButton72.Click -= this.lss_event;
            this.toolStripTextBox73.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox73.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox73.Click -= this.tvtxt20_Click;
            this.toolStripButton115.Click -= this.new_q;
            this.toolStripTextBox74.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox74.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox74.Click -= this.tvtxt20_Click;
            this.toolStripButton116.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox75.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox75.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox75.Click -= this.tvtxt20_Click;
            this.toolStripButton117.Click -= this.stwth_event;
            this.toolStripTextBox76.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox76.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox76.Click -= this.tvtxt20_Click;
            this.toolStripButton118.Click -= this.ew_event;
            this.toolStripTextBox77.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox77.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox77.Click -= this.tvtxt20_Click;
            this.toolStripButton119.Click -= this.piece_event;
            this.toolStripTextBox38.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox38.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox38.Click -= this.tvtxt20_Click;
            this.toolStripButton73.Click -= this.new_q;
            this.toolStripTextBox39.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox39.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox39.Click -= this.tvtxt20_Click;
            this.toolStripButton74.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox40.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox40.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox40.Click -= this.tvtxt20_Click;
            this.toolStripButton75.Click -= this.stwth_event;
            this.toolStripTextBox41.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox41.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox41.Click -= this.tvtxt20_Click;
            this.toolStripButton76.Click -= this.ew_event;
            this.toolStripTextBox42.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox42.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox42.Click -= this.tvtxt20_Click;
            this.toolStripButton77.Click -= this.piece_event;
            this.toolStripTextBox43.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox43.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox43.Click -= this.tvtxt20_Click;
            this.toolStripButton78.Click -= this.new_q;
            this.toolStripTextBox44.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox44.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox44.Click -= this.tvtxt20_Click;
            this.toolStripButton96.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox47.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox47.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox47.Click -= this.tvtxt20_Click;
            this.toolStripButton97.Click -= this.stwth_event;
            this.toolStripTextBox48.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox48.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox48.Click -= this.tvtxt20_Click;
            this.toolStripButton98.Click -= this.ew_event;
            this.toolStripTextBox49.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox49.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox49.Click -= this.tvtxt20_Click;
            this.toolStripButton99.Click -= this.piece_event;
            this.toolStripTextBox50.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox50.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox50.Click -= this.tvtxt20_Click;
            this.toolStripButton100.Click -= this.new_q;
            this.toolStripTextBox51.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox51.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox51.Click -= this.tvtxt20_Click;
            this.toolStripButton101.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox52.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox52.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox52.Click -= this.tvtxt20_Click;
            this.toolStripButton102.Click -= this.stwth_event;
            this.toolStripTextBox53.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox53.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox53.Click -= this.tvtxt20_Click;
            this.toolStripButton103.Click -= this.ew_event;
            this.toolStripTextBox54.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox54.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox54.Click -= this.tvtxt20_Click;
            this.toolStripButton104.Click -= this.piece_event;
            this.toolStripTextBox55.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox55.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox55.Click -= this.tvtxt20_Click;
            this.toolStripButton105.Click -= this.new_q;
            this.toolStripTextBox56.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox56.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox56.Click -= this.tvtxt20_Click;
            this.toolStripButton106.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox68.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox68.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox68.Click -= this.tvtxt20_Click;
            this.toolStripButton107.Click -= this.new_q;
            this.toolStripButton108.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox70.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox70.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox70.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox70.Click -= this.tvtxt20_Click;
            this.toolStripButton109.Click -= this.new_q;
            this.toolStripTextBox71.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox71.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox71.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox71.Click -= this.tvtxt20_Click;
            this.toolStripButton110.Click -= this.tvvbxtnt1_Click;
            this.toolStripTextBox72.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox72.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox72.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox72.Click -= this.tvtxt20_Click;
            this.toolStripButton111.Click -= this.btw_event;
            this.toolStripTextBox83.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox83.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox83.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox83.Click -= this.tvtxt20_Click;
            this.toolStripButton112.Click -= this.lss_event;
            this.toolStripTextBox84.MouseEnter -= this.tvtxt1_MouseEnter;
            this.toolStripTextBox84.MouseLeave -= this.tvtxt1_MouseLeave;
            this.toolStripTextBox84.KeyUp -= this.tvtxt20_KeyUp;
            this.toolStripTextBox84.Click -= this.tvtxt20_Click;
            this.toolStripButton113.Click -= this.gt_event;
            this.delete2.ButtonClick -= this.delete_itms;
            this.del_all2.Click -= this.delete_itms;
            this.delcel2.Click -= this.deletecell_Click;*/
            this.undoToolStripMenuItem.Click -= this.undoall_Click;
            this.dechr.Tick -= this.dechr_Tick;
            this.tmeclse.Tick -= this.tmeclse_Tick;
            this.ofd.FileOk -= this.ofd_FileOk;
            this.ttp_del.Tick -= this.ttp_del_Tick;
            this.cv_ofd.FileOk -= this.cv_ofd_FileOk;
            this.addReplaceCVToolStripMenuItem.Click -= this.addReplaceCVToolStripMenuItem_Click;
            this.removeLinkToCVToolStripMenuItem.Click -= this.addReplaceCVToolStripMenuItem_Click;
            this.connectToToolStripMenuItem.Click -= this.connectToToolStripMenuItem_Click;
            this.helpToolStripMenuItem1.Click -= this.helpToolStripMenuItem1_Click;
            this.tmr.Tick -= this.tmr_Tick;
            this.button5.Click -= this.dateTimePicker1_ValueChanged;
            this.tbsel1.Click -= this.tbsel1_Click;
            this.tbsel2.Click -= this.tbsel2_Click;
            //this.tbsel3.Click -= this.tbsel3_Click;
            this.save_inv.Click -= this.svebtn_ButtonClick;
            this.restr.Click -= this.rstrt_Click;
            this.clsemn.Click -= this.clse_ButtonClick;
            this.undoall.Click -= this.undoall_Click;
            this.cpy.Click -= this.cpy_Click;
            this.ct.Click -= this.ct_Click;
            this.pster.Click -= this.pster_Click;
            this.deletecell.Click -= this.deletecell_Click;
            this.sall.Click -= this.sall_Click;
            this.initializeToolStripMenuItem.Click -= this.initializeToolStripMenuItem_Click;
            this.switchDatabaseToolStripMenuItem.Click -= this.switchDatabaseToolStripMenuItem_Click;
            this.rePartitionDataBaseToolStripMenuItem.Click -= this.rePartitionDataBaseToolStripMenuItem_Click;
            this.contentsToolStripMenuItem.Click -= this.helpToolStripMenuItem1_Click;
            this.abtmnu.Click -= this.abtmnu_Click;
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

        private bool bl_out = true;
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            showall_GUI();
            bindingNavigator1.BindingSource.Position = where - 1;
            bl_out = true;
        }

        private int uuu2 = 0;
        private int uuu = 0;
        private int where;
        private void tb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tb.SelectedIndex == 1 && uuu == 0)
            {
                th_initdb();
                general_mssg("Payroll and Employee Viewer Has Changed its View to Show Only Outsourced Employees of the Selected Outsourced Office Row, Click on Show All in the Employee/Payroll Viewer Tab to View All Employees again.", "", 20000);
                uuu = 1;
            }
            if (tb.SelectedIndex == 2 && uuu2 == 0)
            {
                //th3_db_strt();
                uuu2 = 1;
                tbxfned.Enabled = false;
                gotoitm.Enabled = false;
            }
            else
            {
                tbxfned.Enabled = true;
                gotoitm.Enabled = true;
            }
            if (bl_out == true)
            {
                where = Convert.ToInt32(bindingNavigatorPositionItem.Text);
                bl_out = false;
            }
            if (tb.SelectedIndex == 0)
            {
                undoall.Enabled = false;
                sall.Enabled = false;
            }
            else
            {
                undoall.Enabled = true;
                sall.Enabled = true;
            }
            conn2();
        }

        private DataTable dtp_temp = new DataTable(); String Temp_gp; bool shown_ = false;
        private void Gen(object sender, EventArgs e)
        {
            if (bindingNavigatorPositionItem.Text == "0" && tb.SelectedIndex == 0)
            {
                general_mssg("No Values are Within the Employee/Payroll Table Press the + Button to Add One The Button may be Found in the Tool-bar Above.", "No Entries Within Said Table");
            }

            if (sender.Equals(textBox48) == true || sender.Equals(textBox42) == true)
            {
                try
                {
                    int n, n2;
                    n = Convert.ToInt32(textBox42.Text);
                    n2 = Convert.ToInt32(textBox48.Text);
                    if (n < n2)
                    {
                        textBox48.BackColor = Color.DarkOrange;
                        textBox42.BackColor = Color.DarkOrange;
                        MessageBox.Show("Per Hour Payment is Under the Current Minimum Wage Limit.", "Informator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        textBox48.BackColor = Color.Lavender;
                        textBox42.BackColor = Color.Lavender;
                    }
                }
                catch (Exception erty) { }
            }

            if (sender.Equals(textBox1) == true || sender.Equals(textBox3) == true)
            {
                try
                {
                    tabControl6.SelectTab(0);
                }
                catch (Exception erty) { }
                try
                {
                    if (textBox3.Text == null || textBox3.Text == "")
                    {
                        textBox6.Text = (textBox1.Text[0]).ToString();
                    }
                    else if (textBox1.Text == null || textBox1.Text == "")
                    {
                        textBox6.Text = (textBox3.Text[0]).ToString();
                    }
                    else if (textBox1.Text == null || textBox1.Text == "" && textBox3.Text == null || textBox3.Text == "")
                    {
                        textBox6.Text = "";
                    }
                    else
                    {
                        textBox6.Text = (textBox1.Text[0] + "." + textBox3.Text[0]).ToString();
                    }
                }
                catch (Exception erty) { }
            }

            if (sender.Equals(textBox49) == true)
            {
                Object o = new object(); EventArgs err = new EventArgs();
                OverhourPay_tot(o, err);
            }
            if(sender.Equals(dateTimePicker2)==true)
            {
                calc_age();
            }
        }

        private void year_count()
        {
            try
            {
                double total = 0; int days; double meantime = 0;
                for (int i = 1; i <= 12; i++)
                {
                    meantime = Convert.ToDouble(textBox45.Text);
                    days = DateTime.DaysInMonth(DateTime.Now.Year, i);
                    meantime = meantime * days;
                    total = total + meantime;
                }
                textBox43.Text = (total / 12).ToString();
            }
            catch (Exception ert) { }
        }

        private void connectToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggy lg = new loggy();
            lg.Show();
        }

        private void print_(object sender, EventArgs e)
        {
            if (sender.Equals(toolStripButton2) == true || sender.Equals(printOutSourcedEmployeesToolStripMenuItem) == true)
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dgv2);
            }
            else if (tb.SelectedIndex == 1)
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dgv);
            }
            else if (tb.SelectedIndex == 0)
            {
                PrintDataGrid.PrintDGV.Print_DataGridView(dgv4);
            }
        }

        private void toolStripButton120_Click(object sender, EventArgs e)
        {
            gadg_grph gr = new gadg_grph();
            gr.in_form();
        }

        private Button btn_mp;
        Gadg_maps mps = new Gadg_maps();
        private void button4_Click(object sender, EventArgs e)
        {
            btn_mp = (Button)sender;
            mps.Visible = true;
            mps.webBrowser1.Navigate("http://maps.google.co.in/maps?hl=en&tab=wl");
            mps.Dock = DockStyle.Fill;
            mps.BringToFront();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            year_count();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            year_count();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            load_payedon();
            double totl = 0;
            foreach (DataGridViewRow dgvr in dataGridView2.Rows)
            {
                if (dgvr.Cells[0].Value != DBNull.Value && dgvr.Index != dataGridView2.Rows.Count - 1)
                {
                    totl = totl + Convert.ToDouble(textBox43.Text);
                }
            }
            textBox46.Text = totl.ToString();
        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2[0, 0].Value == DBNull.Value)
            {
                toolStripButton5.Enabled = false;
            }
            else { toolStripButton5.Enabled = true; }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2[0, dataGridView2.CurrentRow.Index].Value == DBNull.Value)
                {
                    general_mssg("Enter a Unique Serial Number", "No Unique Serial Number Entered");
                }
                else
                {
                    dataGridView2[1, dataGridView2.CurrentRow.Index].Value = dateTimePicker1.Value.ToString();
                }
            }
            catch (Exception erty) 
            {
            }
        }

        private void dataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void tabControl6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl6.SelectedIndex == 0)
            {
                panel2.Visible = false;
            }
            oper_save();
            load_payedon();
        }

        private void load_payedon()
        {
            peyed_dtst.Clear();
            string SQL_STR = "SELECT * FROM payedon WHERE [Pay To] = '" + textBox1.Text + " " + textBox3.Text + " [" + textBox7.Text + "]" + "'";
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand(SQL_STR, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                peyed_dtst.payedon.Load(dr);
                conn.Close();
            }
            else
            {
                dr_univ = Quer(SQL_STR);
                peyed_dtst.payedon.Load(dr_univ);
            }

            if (dataGridView2[0, 0].Value == DBNull.Value)
            {
                toolStripButton5.Enabled = false;
            }
            else
            {
                toolStripButton5.Enabled = false;
            }
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

        private void abtmnu_Click(object sender, EventArgs e)
        {
            app_abt abtat = new app_abt();
            abtat.descr(this.Text);
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Helper hlp = new Helper();
            hlp.tx(this.Name);
        }

        //sexyness
        //private int sel;
        private ToolStripTextBox tstt2;
        private void tvtxt20_KeyUp(object sender, KeyEventArgs e)
        {
            tstt2 = (ToolStripTextBox)sender;
            if (e.KeyValue == 8) { }
            else if (tstt2.Text.Length == 2 || tstt2.Text.Length == 6 && e.KeyValue != 8)
            {
                sel = tstt2.SelectionStart + 1;
                tstt2.Text = tstt2.Text + "-";
                tstt2.SelectionStart = sel;
            }

            if (tstt2.Text.Length > 9)
            {
                tstt2.BackColor = Color.Orange;
            }
            else if (tstt2.Text.Length <= 9)
            {
                tstt2.BackColor = Color.White;
            }
        }
        

        private void tvtxt20_Click(object sender, EventArgs e)
        {
            tstt2 = (ToolStripTextBox)sender;
            if (tstt2.Text == "Enter a Date (eg. 01-jan-91)" || tstt2.Text == "Enter First Date (eg. 01-Jan-91)" || tstt2.Text == "Show Invoice Of" || tstt2.Text == "Enter Last Date (eg. 02-Feb-92)" || tstt2.Text == "Enter a Value" || tstt2.Text == "Enter Value(Piece)" || tstt2.Text == "Enter First Value" || tstt2.Text == "Enter Last Value")
            {
                tstt2.SelectAll();
            }
        }

        //enter textbox
        Color cl = new Color(); ComboBox cbx_tempcol;
        private void tvtxt1_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                tbx_tstemp = (ToolStripTextBox)sender;
                cl = tbx_tstemp.BackColor;
                tbx_tstemp.BackColor = Color.Lavender;
            }
            catch (Exception erty)
            {
                try
                {
                    tbx_temp = (TextBox)sender; cl = tbx_temp.BackColor; tbx_temp.BackColor = Color.Lavender;
                }
                catch (Exception er2ty)
                {
                    try
                    {
                        cbx_tempcol = (ComboBox)sender; cl = cbx_tempcol.BackColor; cbx_tempcol.BackColor = Color.Lavender;
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
                catch
                {
                    try
                    {
                        cbx_tempcol = (ComboBox)sender;
                        cbx_tempcol.BackColor = cl;
                    }
                    catch { }
                }
            }
        }


        bool fst = true;
        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            //image
            if (bindingNavigatorPositionItem.Text == "0")
            {
                panel7.Enabled = false;
                panel12.Enabled = false;
            }
            else
            {
                panel7.Enabled = true;
                panel12.Enabled = true;
            }
            try
            {
                if (dgv4[80, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value != DBNull.Value)
                {
                    try
                    {
                        byte[] res1 = (byte[])dgv4[80, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value;
                        Image newImage;
                        using (MemoryStream ms = new MemoryStream(res1, 0, res1.Length))
                        {
                            newImage = Bitmap.FromStream(ms, true);
                            ms.Flush();
                            ms.Close();
                            ms.Dispose();
                        }
                        pbx.BackgroundImage = newImage;
                    }
                    catch (Exception erty)
                    {
                        pbx.BackgroundImage = (Image)dgv4[80, Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1].Value;
                    }
                }
                else
                {
                    pbx.BackgroundImage = Properties.Resources.person;
                }
            }
            catch (Exception erty) { }
            calc_age();
        }

        private void calc_age()
        {
            int date_1 = DateTime.Now.Year - dateTimePicker2.Value.Year;
            comboBox1.Text = date_1.ToString();

            if (date_1 < 18)
            {
                checkBox1.Checked = true;
            }
            else { checkBox1.Checked = false; }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mgmt mgt = new mgmt();
            mgt.Show();
        }

        private void eml_snd_Click(object sender, EventArgs e)
        {
            eml_send snd = new eml_send();
            snd.Send_to(textBox23.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form1 fm1 = new Form1();
            fm1.Show();
        }

        private void pbx_BackgroundImageChanged(object sender, EventArgs e)
        {
            if (pbx.BackgroundImage.Size.Height < pbx.Size.Height || pbx.BackgroundImage.Size.Width < pbx.Size.Width)
            {
                pbx.BackgroundImageLayout = ImageLayout.Center;
            }
            else
            {
                pbx.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 attnce = new Form1();
            //attnce.tx(textBox1.Text, textBox3.Text, textBox7.Text);
            attnce.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            attndnce_dtst.Attendance_Managment.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(attendance_ManagmentTableAdapter1.Connection.ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Attendance_Managment WHERE [First Name] = '" + textBox1.Text + "' AND [Last Name] = '" + textBox3.Text + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                attndnce_dtst.Attendance_Managment.Load(dr);
                dataGridView4.DataSource = attndnce_dtst.Attendance_Managment;
            }
            else
            {
                dr_univ = Quer("SELECT * FROM Attendance_Managment WHERE [First Name] = '" + textBox1.Text + "' AND [Last Name] = '" + textBox3.Text + "'");
                attndnce_dtst.Attendance_Managment.Load(dr_univ);
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                attendance_ManagmentTableAdapter1.Update(attndnce_dtst);
            }
            else
            {
                asql.Save(attndnce_dtst.Attendance_Managment, attndnce_dtst.Attendance_Managment.TableName, Main.Amatrix.mgt);
            }
        }

        private void dataGridView4_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView4[10, e.RowIndex].Value == DBNull.Value)
            {
                dataGridView4[10, e.RowIndex].Value = DateTime.Now.ToString() + DateTime.Now.Millisecond.ToString() + DateTime.Now.Ticks.ToString();
                dataGridView4[0, e.RowIndex].Value = textBox1.Text;
                dataGridView4[1, e.RowIndex].Value = textBox3.Text;
            }
        }

        private void tbx_time_SelectionChanged(object sender, EventArgs e)
        {
            int ndx, ndx2, ndx3;
            ndx = tbx_time.GetLineFromCharIndex(tbx_time.SelectionStart);
            ndx2 = tbx_time.GetFirstCharIndexFromLine(ndx);
            ndx3 = tbx_time.Lines[ndx].Length;
            tbx_time.SelectionStart = ndx2;
            tbx_time.SelectionLength = ndx3;
        }

        private void dgv_CellBeginEdit_1(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Arraylist addition
            aund.Add(dgv[e.ColumnIndex, e.RowIndex].Value);
            aundC.Add(e.ColumnIndex);
            aundR.Add(e.RowIndex);
        }

        private void dgv2_Leave(object sender, EventArgs e)
        {
            editToolStripMenuItem1.Enabled = true;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            double d = 0; bool do_ = false; bool mspec = false;
            try
            {
                if (comboBox24.Text == "Hourly Basis") { d = Convert.ToDouble(textBox42.Text); mspec = true; }
                else if (comboBox24.Text == "Monthly Basis") { d = Convert.ToDouble(textBox43.Text); mspec = true; }
                else if (comboBox24.Text == "Daily Basis") { d = Convert.ToDouble(textBox45.Text); mspec = true; }
                else if (comboBox24.Text == "") { Am_err ner = new Am_err(); ner.tx("Must be Payed on not Specified"); tabControl10.SelectTab(0); comboBox24.Select(); /*comboBox24.DroppedDown = true;*/ do_ = true; mspec = false; }
                if (mspec == true)
                {
                    if (do_ == false)
                    {
                        DataRow dr = peyed_dtst.payedon.NewRow();
                        dr[0] = "PAY-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString();
                        if (textBox15.Text != "Payed On (dd/mm/yy)")
                        {
                            dr[1] = textBox15.Text;
                        }
                        else { dr[1] = DateTime.Now.ToShortDateString(); }
                        if (textBox18.Text != "")
                        {
                            dr[3] = textBox18.Text;
                        }
                        else { dr[3] = "PAY-" + DateTime.Now.ToShortDateString() + DateTime.Now.Ticks.ToString(); }
                        if (comboBox23.Text != "")
                        {
                            dr[2] = comboBox23.Text;
                        }
                        else { general_mssg("Unable to Set Payed By Field 'Payment Type' in the Employer Editor Tab was left Empty.", "Field Empty"); }
                        dr[4] = textBox1.Text + " " + textBox3.Text + " [" + textBox7.Text + "]";
                        peyed_dtst.payedon.Rows.Add(dr);

                        if (Main.Amatrix.mgt == "")
                        {
                            payedonTableAdapter.Update(peyed_dtst);
                        }
                        else
                        {
                            asql.Save(peyed_dtst.payedon, peyed_dtst.payedon.TableName, Main.Amatrix.mgt);
                        }
                        //mgmt_Linkto_acc loacc = new mgmt_Linkto_acc();
                        //loacc.tx("Salary Payment For(" + textBox1.Text + " " + textBox3.Text + " [" + textBox7.Text + "]" + ")[Pay." + textBox18.Text + "]", "", textBox1.Text + " " + textBox3.Text + "[" + textBox7.Text + "]", "managment hr", d, 0);
                    }

                    if (DialogResult.Yes == MessageBox.Show("Create a new Payroll Entry?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        mgmt_pr_payrolls prr = new mgmt_pr_payrolls();

                        string s, s2, s3; double d_tax;
                        try
                        {
                            d_tax = Convert.ToDouble(textBox53.Text);
                            s3 = textBox53.Text;
                        }
                        catch (Exception erty) { s3 = "0"; }
                        if (comboBox24.Text == "Hourly Basis")
                        {
                            s = textBox42.Text; s2 = comboBox23.Text;
                            prr.tx(textBox7.Text, textBox1.Text + " " + textBox3.Text, s, textBox51.Text, textBox52.Text, textBox49.Text, s3, s2, textBox45.Text, comboBox24.Text, textBox18.Text);
                        }
                        else if (comboBox24.Text == "Monthly Basis")
                        {
                            s = textBox43.Text; s2 = comboBox23.Text;
                            prr.tx(textBox7.Text, textBox1.Text + " " + textBox3.Text, s, textBox51.Text, textBox52.Text, textBox49.Text, s3, s2, textBox45.Text, comboBox24.Text, textBox18.Text);
                        }
                        else if (comboBox24.Text == "Daily Basis")
                        {
                            s = textBox45.Text; s2 = comboBox23.Text;
                            prr.tx(textBox7.Text, textBox1.Text + " " + textBox3.Text, s, textBox51.Text, textBox52.Text, textBox49.Text, s3, s2, textBox45.Text, comboBox24.Text, textBox18.Text);
                        }
                        else { Am_err ner = new Am_err(); ner.tx("Unable to Continue, 'Must be Payed(On) value not Set'"); }
                    }
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Unable to Continue Operation as the Required Employee Salary Payment Fields Where Rendered Empty."); }
        }

        private void bindingNavigatorDeleteItem2_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Would you Like to Create a Journal Entry for Salary Payment as Removed?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                int maxm = 0;
                try
                {
                    if (Main.Amatrix.acc == "")
                    {
                        SqlCeConnection mySqlConnection3 = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                        SqlCeCommand mySqlCommand3 = new SqlCeCommand("SELECT MAX([Serial Number]) FROM journal", mySqlConnection3);
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
                        DataTable dtpy = new DataTable(); basql.Execute(Main.Amatrix.acc, "SELECT MAX([Serial Number]) FROM journal", "journal", dtpy);
                        maxm = Convert.ToInt32(dtpy.Rows[0].ItemArray[0]);
                        maxm = maxm + 1;
                        dtpy.Clear(); dtpy.Dispose();
                    }
                }
                catch (Exception erty) { maxm = 1; }
                //getprod

                //SELECT product cost
                double d = 0; bool do_ = false;
                if (comboBox24.Text == "Hourly Basis") { d = Convert.ToDouble(textBox42.Text); }
                else if (comboBox24.Text == "Monthly Basis") { d = Convert.ToDouble(textBox43.Text); }
                else if (comboBox24.Text == "Daily Basis") { d = Convert.ToDouble(textBox45.Text); }
                else if (comboBox24.Text == "") { Am_err ner = new Am_err(); ner.tx("Must be Payed on not Specified"); tabControl10.SelectTab(0); comboBox24.Select(); comboBox24.DroppedDown = true; do_ = true; }
                if (do_ == false)
                {
                    string sql = "INSERT INTO journal VALUES('" + maxm.ToString() + "', '" + DateTime.Now.ToShortDateString() + "', '" + "Payment Revokation For(" + textBox1.Text + " " + textBox3.Text + " [" + textBox7.Text + "]" + ")[Pay." + dataGridView2[3, dataGridView2.CurrentRow.Index].Value.ToString() + "]" + "', 'Payment Revokation','','','No','0','','" + d.ToString() + "','')";
                    if (Main.Amatrix.acc == "")
                    {
                        SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.AmdtbseConnectionString);
                        SqlCeCommand cmd = new SqlCeCommand(sql, conn);
                        conn.Open();
                        SqlCeDataReader dr = cmd.ExecuteReader();
                        conn.Close();

                        SqlCeConnection conn2 = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                        SqlCeCommand cmd2 = new SqlCeCommand("DELETE FROM payedon WHERE [Serial] = '" + dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString() + "'", conn2);
                        conn2.Open();
                        SqlCeDataReader dr2 = cmd2.ExecuteReader();
                        conn2.Close();
                    }
                    else
                    {
                        DataTable dtpy = new DataTable();
                        basql.Execute(Main.Amatrix.acc, sql, "journal", dtpy);
                        basql.Execute(Main.Amatrix.mgt, "DELETE FROM payedon WHERE [Serial] = '" + dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString() + "'", "payedon", dtpy);
                        dtpy.Clear(); dtpy.Dispose();
                    }
                    load_payedon();
                }
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            mgmt_pr_payrolls prr = new mgmt_pr_payrolls();
            prr.tx(textBox7.Text);
            prr.Show();
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        int whereami = 0; int xx = 0; int y = 0;
        Thread Sync_th;
        delegate void Sync_del();
        private void bttn_sync_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            bttn_sync.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.Text = this.Text + " [Synchronizing]";
            if (tb.SelectedIndex == 0)
            {
                whereami = Convert.ToInt32(bindingNavigatorPositionItem.Text);
            }
            else
            {
                xx = dgv.CurrentCell.RowIndex;
                y = dgv.CurrentCell.ColumnIndex;
            }
            try
            {
                bkk_sync.RunWorkerAsync();
            }
            catch { bttn_sync.BackColor = Color.DarkOrange; this.Enabled = true; this.Text = this.Text.Replace(" [Synchronizing]", ""); }
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
            if (tb.SelectedIndex == 0)
            {
                emply_payr_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used, conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    emply_payr_dtst.Employ_payrll.Load(dr);
                    conn.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    SqlCommand cmd = new SqlCommand(Last_Query_Used, conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    emply_payr_dtst.Employ_payrll.Load(dr);
                    conn.Close();
                }
                load_payedon();
                employpayrllBindingSource.Position = whereami - 1;
                this.Text = this.Text.Replace(" [Synchronizing]", "");
                this.Enabled = true;
                bttn_sync.BackColor = Color.Transparent;
            }
            else
            {
                outsrce_dtst.Clear();
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand(Last_Query_Used2, conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    outsrce_dtst.outsrce.Load(dr);
                    conn.Close();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                    SqlCommand cmd = new SqlCommand(Last_Query_Used2, conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    outsrce_dtst.outsrce.Load(dr);
                    conn.Close();
                }
                dgv.CurrentCell = dgv[y, xx];
                this.Text = this.Text.Replace(" [Synchronizing]", "");
                this.Enabled = true;
                bttn_sync.BackColor = Color.Transparent;
            }
        }
    }
}
