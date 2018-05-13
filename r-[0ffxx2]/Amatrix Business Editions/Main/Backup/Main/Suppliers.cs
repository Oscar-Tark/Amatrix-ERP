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
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Suppliers : Form
    {
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        SQLCEBasql.SQLCEBSQ scsql = new SQLCEBasql.SQLCEBSQ();
        public Suppliers()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.amdsicnico;
            this.Disposed += new EventHandler(Suppliers_Disposed);
            this.Show();
        }

        void Suppliers_Disposed(object sender, EventArgs e)
        {
            amdtbse_6DataSet1.Clear();
            amdtbse_6DataSet1.Dispose();

            suppliersBindingSource.Clear();
            suppliersBindingSource.Dispose();

            suppliersTableAdapter.Dispose();

            this.button1.Click -= button1_Click;
            this.button2.Click -= button2_Click;
            this.button3.Click -= button3_Click;
            this.Load -= Suppliers_Load;
            this.Disposed -= Suppliers_Disposed;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void Suppliers_Load(object sender, EventArgs e)
        {
            if (Amatrix.mgt == "")
            {
                // TODO: This line of code loads data into the 'amdtbse_6DataSet1.Suppliers' table. You can move, or remove it, as needed.
                this.suppliersTableAdapter.Fill(this.amdtbse_6DataSet1.Suppliers);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Suppliers", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                amdtbse_6DataSet1.Suppliers.Clear();
                amdtbse_6DataSet1.Suppliers.Load(dr);
                conn.Close();
            }
        }

        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        private void button1_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                suppliersTableAdapter.Update(amdtbse_6DataSet1);
            }
            else
            {
                asql.Save(amdtbse_6DataSet1.Suppliers, "Suppliers", Amatrix.mgt);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgv.SelectedRows)
            {
                try
                {
                    if (Amatrix.mgt == "")
                    {
                        scsql.Execute_Insert_Update(Properties.Settings.Default.Amdtbse_6ConnectionString, "DELETE FROM Suppliers WHERE [Supplier Name] = '" + dgvr.Cells[0].Value.ToString() + "'");
                    }
                    else
                    {
                        basql.Execute(Amatrix.mgt, "DELETE FROM Suppliers WHERE [Supplier Name] = '" + dgvr.Cells[0].Value.ToString() + "'", "", amdtbse_6DataSet1.Suppliers);
                    }
                    dgv.Rows.Remove(dgvr); dgvr.Dispose();
                }
                catch (Exception erty) { MessageBox.Show(erty.Message); }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
