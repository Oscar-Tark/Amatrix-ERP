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
    public partial class mgmt_supch_resent : Form
    {
        private DataTable dtp = new DataTable();
        string mainprod = ""; string subprod = "";
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();

        public mgmt_supch_resent()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            try
            {
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[8].ReadOnly = true;
                dataGridView1.Columns[9].ReadOnly = true;
            }
            catch (Exception erty) { }
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

        private void init_db()
        {
            try
            {
                dtp.Clear();
                string SQL = "SELECT * FROM prod_resent WHERE [For Product] = '" + mainprod + "' AND [For Bulk] = '" + subprod + "'";
                if (Main.Amatrix.mgt == "")
                {
                    SqlCeConnection conn = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand(SQL, conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    dtp.Load(dr);
                    conn.Close();
                }
                else
                {
                    dtp = basql.Execute(Main.Amatrix.mgt, SQL, "prod_resent", dtp);
                }
                dataGridView1.DataSource = dtp;
            }
            catch (Exception erty) { }
        }

        private void mgmt_supch_resent_Load(object sender, EventArgs e)
        {

        }

        public void tx(string Main_PROD, string SUB_PROD)
        {
            mainprod = Main_PROD; subprod = SUB_PROD;
            init_db();
            this.ShowDialog();
            this.Text = "Resent Items For (" + mainprod.Replace(" ", "") + "/" + subprod.Replace(" ", "") + ")";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                DataTable table = new DataTable();
                table = dtp;
                DataTable table2 = new DataTable();

                using (var con = new SqlCeConnection(Properties.Settings.Default.Amdtbse_4ConnectionString))
                using (var adapter = new SqlCeDataAdapter("SELECT * FROM prod_resent", con))
                using (new SqlCeCommandBuilder(adapter))
                {
                    adapter.Fill(table2);
                    con.Open();
                    adapter.Update(table);
                }
            }
            else
            {
                asql.Save(dtp, "prod_resent", Main.Amatrix.mgt);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
