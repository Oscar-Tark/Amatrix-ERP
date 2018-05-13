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
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class mgmt_pr_payrolls : Form
    {
        string for_hr_ = ""; string Fulln = ""; double TOTAL = 0;
        double payment_ = 0; double benefits_ = 0; double bonuses_ = 0; string overhours = ""; string tax_ = "0"; double paypd = 0;
        private DataTable dtp = new DataTable();
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();

        public mgmt_pr_payrolls()
        {
            InitializeComponent();
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

        private void mgmt_pr_payrolls_Load(object sender, EventArgs e)
        {

        }

        public void tx(string for_hr, string FullName, string payment, string benefits, string bonuses, string overhrs, string tax, string payment_type, string payperday, string basis, string payno)
        {
            try
            {
                for_hr_ = for_hr; Fulln = FullName; overhours = overhrs; tax_ = tax;
            }
            catch (Exception erty) { }
            try
            {
                payment_ = Convert.ToDouble(payment);
            }
            catch (Exception erty) { }
            try
            {
                benefits_ = Convert.ToDouble(benefits);
            }
            catch (Exception erty) { }
            try
            {
                bonuses_ = Convert.ToDouble(bonuses);
            }
            catch (Exception erty) { }
            try
            {
                paypd = Convert.ToDouble(payperday);
            }
            catch (Exception erty) { }
            try
            {
                TOTAL = (payment_ + benefits_ + bonuses_) - Convert.ToDouble(tax_);
            }
            catch (Exception erty)
            {
                TOTAL = (payment_ + benefits_ + bonuses_) - 0;
            }

            //Evaluate Sick Days

            if (basis == "Daily Basis" || basis == "Monthly Basis")
            {
                if (DialogResult.Yes == MessageBox.Show("Automate Payrolls to Comply with Attendances (Attendance Managment)? ACTION REQUIRES AN ENTERED VALUE FOR 'Basic Employee Salary [Per Day]', IF NOT ENTERED WE ENCOURAGE YOU TO CLICK ON No.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    try
                    {
                        int count = 0;
                        DataTable tp = new DataTable();
                        string SQL1 = "SELECT * FROM Signs WHERE datepart(MM,[Employee Time In]) = datepart(MM, getdate()) AND datepart(YYYY, [Employee Time In]) = datepart(YYYY, getdate())";
                        if (Main.Amatrix.mgt == "")
                        {
                            SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.BSAMConnectionString);
                            SqlCeCommand cmd = new SqlCeCommand(SQL1, conn);
                            conn.Open();
                            SqlCeDataReader dr = cmd.ExecuteReader();
                            tp.Load(dr);
                            count = tp.Rows.Count;
                            conn.Close();
                        }
                        else
                        {
                            tp = basql.Execute(Main.Amatrix.mgt, SQL1, "Signs", tp);
                            count = tp.Rows.Count;
                        }

                        tp.Clear(); tp.Dispose();
                        if (count != 0 && paypd != 0)
                        {
                            double d1 = count * paypd;
                            payment_ = d1;
                            TOTAL = (payment_ + benefits_ + bonuses_) - Convert.ToDouble(tax_);
                        }
                        else { MessageBox.Show("Unable to Complete the Operation as one of the Following Conditions was Raised: the Field 'Basic Employee Salary [Per Day]' was Valued at 0 or, no Employee Time In/Out's were Recorded.", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error); }
                    }
                    catch (Exception erty) { }
                }
            }

            //Evaluate Sick Days END

            string SQL = "INSERT INTO Payrolls VALUES('PAYR-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString() + "', '" + for_hr_ + "', '" + Fulln + "', '" + payment_.ToString() + "','','','" + tax + "','" + benefits_ + "','" + bonuses + "','" + payment_type + "','','" + DateTime.Now.ToString() + "','" + TOTAL.ToString() + "')";
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand(SQL, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                conn.Close();
            }
            else
            {
                DataTable dtpy = new DataTable();
                basql.Execute(Main.Amatrix.mgt, SQL, "Payrolls", dtpy);
                dtpy.Clear(); dtpy.Dispose();
            }

            tx(for_hr_);
            if (DialogResult.Yes == MessageBox.Show("Insert a Journal Value For the Salary Payment?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                mgmt_Linkto_acc loacc = new mgmt_Linkto_acc();
                loacc.tx("Salary Payment For(" + FullName + " [" + for_hr + "]" + ")[Pay." + payno + "]", "", FullName + "[" + for_hr + "]", "managment hr", TOTAL, 0); 
            }
        }

        public void tx(string HR_ID)
        {
            dtp.Clear();
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Payrolls WHERE [For Employee] = '" + HR_ID + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp.Load(dr);
                conn.Close();
            }
            else
            {
                dtp = basql.Execute(Main.Amatrix.mgt, "SELECT * FROM Payrolls WHERE [For Employee] = '" + HR_ID + "'", "Payrolls", dtp);
            }
            dataGridView1.DataSource = dtp;
            dataGridView1.Columns[0].ReadOnly = true;
            this.Show();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1[0, dataGridView1.CurrentRow.Index].Value == DBNull.Value) { dataGridView1[0, dataGridView1.CurrentRow.Index].Value = "PAYR-" + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                DataTable table = new DataTable();
                table = dtp;
                DataTable table2 = new DataTable();

                using (var con = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString))
                using (var adapter = new SqlCeDataAdapter("SELECT * FROM Payrolls", con))
                using (new SqlCeCommandBuilder(adapter))
                {
                    adapter.Fill(table2);
                    con.Open();
                    adapter.Update(table);
                }
            }
            else
            {
                asql.Save(dtp, "Payrolls", Main.Amatrix.mgt);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_3ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand("DELETE FROM Payrolls WHERE [Serial] = '" + dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString() + "'", conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                conn.Close();
            }
            else
            {
                DataTable dtpy = new DataTable();
                basql.Execute(Main.Amatrix.mgt, "DELETE FROM Payrolls WHERE [Serial] = '" + dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString() + "'", "prod_warranties", dtpy);
                dtpy.Clear(); dtpy.Dispose();
            }
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7 || e.ColumnIndex == 8)
            {
                try
                {
                    dataGridView1[12, dataGridView1.CurrentRow.Index].Value = ((Convert.ToDouble(dataGridView1[3, dataGridView1.CurrentRow.Index].Value) + Convert.ToDouble(dataGridView1[7, dataGridView1.CurrentRow.Index].Value) + Convert.ToDouble(dataGridView1[8, dataGridView1.CurrentRow.Index].Value)) - Convert.ToDouble(dataGridView1[6, dataGridView1.CurrentRow.Index].Value));
                }
                catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Unable to Update the Total Value, as a Failsafe This may be Done Manually"); }
            }
        }
    }
}
