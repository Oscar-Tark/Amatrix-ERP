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
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Main
{
    public partial class Product_Type : Form
    {
        public Product_Type()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.amdsicnico;
            this.Disposed += new EventHandler(Product_Type_Disposed);
            this.Show();
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

        void Product_Type_Disposed(object sender, EventArgs e)
        {
            prod_typ_dtst.Clear();
            prod_typ_dtst.Clear();

            product_TypesTableAdapter.Dispose();

            productTypesBindingSource.Clear();
            productTypesBindingSource.Dispose();

            button2.Click -= button2_Click;
            this.Load -= Product_Type_Load;
            this.button1.Click -= button1_Click;
            this.Load -= this.Product_Type_Load;
            this.Disposed -= Product_Type_Disposed;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        private void Product_Type_Load(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                // TODO: This line of code loads data into the 'prod_typ_dtst.Product_Types' table. You can move, or remove it, as needed.
                this.product_TypesTableAdapter.Fill(this.prod_typ_dtst.Product_Types);
            }
            else
            {
                prod_typ_dtst.Clear();
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Product_Types", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                prod_typ_dtst.Product_Types.Load(dr);
                conn.Close();
            }
        }

        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        private void button1_Click(object sender, EventArgs e)
        {
            productTypesBindingSource.EndEdit();
            dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);

            if (Amatrix.mgt == "")
            {
                product_TypesTableAdapter.Update(prod_typ_dtst);
            }
            else
            {
                asql.Save(prod_typ_dtst.Product_Types, "Product_Types", Main.Amatrix.mgt);
            }

            this.Close();
        }

        SQLCEBasql.SQLCEBSQ scsql = new SQLCEBasql.SQLCEBSQ();
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        private void button2_Click(object sender, EventArgs e)
        {
            if (Main.Amatrix.mgt == "")
            {
                SqlCeDataReader dr = scsql.Execute_DataReader(Properties.Settings.Default.Amdtbse_6ConnectionString, "DELETE FROM Product_Types WHERE [Type] = '" + dgv.CurrentCell.Value.ToString() + "'");
                prod_typ_dtst.Product_Types.Rows.RemoveAt(dgv.CurrentRow.Index);
            }
            else
            {
                basql.Execute(Main.Amatrix.mgt, "DELETE FROM Product_Types WHERE [Type] = '" + dgv.CurrentCell.Value.ToString() + "'", "", prod_typ_dtst.Product_Types);
                prod_typ_dtst.Product_Types.Rows.RemoveAt(dgv.CurrentRow.Index);
            }
            dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }
}
