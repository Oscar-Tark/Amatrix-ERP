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
    public partial class mgmt_purchase_information : Form
    {
        public mgmt_purchase_information()
        {
            InitializeComponent();
        }

        private void mgmt_purchase_information_Load(object sender, EventArgs e)
        {
        }

        public void tx(string product_id)
        {
            if (Main.Amatrix.mgt == "")
            {
                // TODO: This line of code loads data into the 'purch_info_dtst.Purchase_Information' table. You can move, or remove it, as needed.
                this.purchase_InformationTableAdapter.Fill(this.purch_info_dtst.Purchase_Information);
            }
            else
            {
                purch_info_dtst.Purchase_Information.Clear();
                SqlConnection conn = new SqlConnection(Main.Amatrix.mgt);
                SqlCommand cmd = new SqlCommand("SELECT * FROM Purchase_Information WHERE [For Product Serial Number] = '" + product_id + "'", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                purch_info_dtst.Purchase_Information.Load(dr);
                conn.Close();
            }
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
