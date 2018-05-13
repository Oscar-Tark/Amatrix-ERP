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
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class mgmt_supch_units : Form
    {
        SQLCEBasql.SQLCEBSQ scsql = new SQLCEBasql.SQLCEBSQ();
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();
        string ID, Name;
        public mgmt_supch_units(string Product_ID, string Product_Name)
        {
            Name = Product_Name;
            ID = Product_ID;
            this.Icon = Properties.Resources.amdsicnico;
            InitializeComponent();
            this.Disposed += new EventHandler(mgmt_supch_units_Disposed);
            init_db();
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

        void mgmt_supch_units_Disposed(object sender, EventArgs e)
        {
            this.button1.Click -= button1_Click;
            this.Load -= mgmt_supch_units_Load;
            this.Disposed -= mgmt_supch_units_Disposed;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            //this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        bool exists = false;
        private void init_db()
        {
            DataTable dtp = new DataTable();
            if (Amatrix.mgt == "")
            {
                dtp = scsql.Execute(Properties.Settings.Default.Amdtbse_6ConnectionString, "SELECT * FROM Product_Units WHERE [Product ID] = '" + ID + "'");
            }
            else
            {
                dtp = basql.Execute(Amatrix.mgt, "SELECT * FROM Product_Units WHERE [Product ID] = '" + ID + "'", "", dtp);
            }

            //load to GUI

            try
            {
                //cn.Text = dtp.Rows[0].ItemArray[1].ToString();
                comboBox3.Text = dtp.Rows[0].ItemArray[2].ToString();
                textBox1.Text = dtp.Rows[0].ItemArray[3].ToString();
                exists = true;
            }
            catch (Exception erty) { }
        }

        private void mgmt_supch_units_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql_string = "";
            if (exists == true)
            {
                sql_string = "UPDATE Product_Units SET [Unit] = '" + comboBox3.Text + "', [Value] = '" + textBox1.Text + "' WHERE [Product ID] = '" + ID + "'";
            }
            else
            {
                sql_string = "INSERT INTO Product_Units VALUES('" + Name + "', '" + ID + "', '" + comboBox3.Text + "', '" + textBox1.Text + "')";
            }

            if (Amatrix.mgt == "")
            {
                scsql.Execute_Insert_Update(Properties.Settings.Default.Amdtbse_6ConnectionString, sql_string);
            }
            else
            {
                DataTable dtp = new DataTable();
                basql.Execute(Amatrix.mgt, sql_string, "", dtp);
                dtp.Clear(); dtp.Dispose();
            }
            this.Close();
        }
    }
}
