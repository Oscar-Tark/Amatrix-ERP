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
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Microsoft.PointOfService;

namespace Main
{
    public partial class Inventory_Searcher : Form
    {
        private PosExplorer explorer;
        private ArrayList scannerList;
        private Scanner activeScanner;
        Base_ASQL.BASQL asql = new Base_ASQL.BASQL();

        public Inventory_Searcher()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.amdsicnico;
            if (Main.Amatrix.mgt != "")
            {
                try
                { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text, Main.Amatrix.mgt); pwd.Owner = this; }
                catch (Exception erty) { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable dtp1 = new DataTable(); private DataTable dtp2 = new DataTable(); private DataTable dtp3 = new DataTable();
        private void button2_Click(object sender, EventArgs e)
        {
            dtp1.Clear();
            dtp3.Clear();
            String SQL_String = "SELECT * FROM prod_bulk WHERE [Reference Number] = '" + textBox1.Text + "' OR [Bar Code Extension] = '" + textBox1.Text + "'";
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand(SQL_String, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp1.Load(dr);
                conn.Close();
            }
            else
            {
                dtp1 = asql.Execute(Main.Amatrix.mgt, SQL_String, "prod_bulk", dtp1);
            }
            //Customers
            String SQL_String2 = "SELECT [Sold To], [Bar Code Extension], [Reference Number] FROM prod_bulk WHERE [Sold To] <> NULL AND [Bar Code Extension] = '" + textBox1.Text + "' OR [Reference Number] = '" + textBox1.Text + "'"; 
            if (Main.Amatrix.mgt == "")
            {
                SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                SqlCeCommand cmd = new SqlCeCommand(SQL_String2, conn);
                conn.Open();
                SqlCeDataReader dr = cmd.ExecuteReader();
                dtp2.Load(dr);
                conn.Close();
            }
            else
            {
                dtp2 = asql.Execute(Main.Amatrix.mgt, SQL_String2, "prod_bulk", dtp2);
            }
            dataGridView1.DataSource = dtp1;
            dataGridView3.DataSource = dtp2;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Products
            try
            {
                dtp3.Clear();
                String SQL_String3 = "SELECT * FROM Prod_mgmt WHERE [Product ID Number] = '" + dataGridView1[2, e.RowIndex].Value.ToString() + "'";
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand(SQL_String3, conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    dtp3.Load(dr);
                    conn.Close();
                }
                else
                {
                    dtp3 = asql.Execute(Main.Amatrix.mgt, SQL_String3, "prod_bulk", dtp3);
                }
                dataGridView2.DataSource = dtp3;
            }
            catch (Exception erty) { }
        }

        private void Inventory_Searcher_Load(object sender, EventArgs e)
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
                textBox1.Text = encoder.GetString(activeScanner.ScanDataLabel);
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
    }
}
