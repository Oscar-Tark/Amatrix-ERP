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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Contacts : Form
    {
        Extern_ASQL.Extern_Sql asql = new Extern_ASQL.Extern_Sql();
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();

        public Contacts()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = Properties.Resources.amdsicnico;
            InitializeComponent();
            this.Disposed += new EventHandler(Contacts_Disposed);
            /*try
            { Security_PWD pwd = new Security_PWD(); pwd.tx(true, this.Name, this.Text); pwd.Owner = this; }
            catch (Exception erty) { }*/
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

        void Contacts_Disposed(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = null;

            amdtbse5_dtst.Clear();
            contactsTableAdapter.Connection.Close();
            contactsBindingSource.EndEdit();

            amdtbse5_dtst.Dispose();
            contactsTableAdapter.Dispose();
            contactsBindingSource.Dispose();

            bindingNavigatorAddNewItem.Click -= bindingNavigatorAddNewItem_Click;
            dectmeabt.Tick -= dectmeabt_Tick;
            this.Activated -= Contacts_Activated;
            this.Deactivate -= Contacts_Deactivate;
            textBox1.Enter -= textBox1_Enter;
            textBox1.Leave -= textBox1_Leave;
            radioButton1.CheckedChanged -= Chnge_From_DTA;
            radioButton2.CheckedChanged -= Chnge_From_DTA;
            this.button1.Click -= this.button1_Click;
            //this.dataGridView1.CellBeginEdit -= this.dataGridView1_CellBeginEdit;
            this.button3.Click -= this.button3_Click;
            this.button4.Click -= this.button4_Click;
            this.Load -= this.Contacts_Load;

            foreach (Control C in this.Controls)
            {
                C.Dispose();
            }

            this.components.Dispose();
            this.BindingContext = null;

            this.Dispose(true);
            GC.Collect();
        }

        eml_send eml_;
        public void tx(bool OK_button, eml_send eml)
        {
            eml_ = eml;
            button1.Enabled = OK_button;
        }

        public void tx(bool OK_button)
        {
            button1.Enabled = OK_button;
        }

        private void Contacts_Load(object sender, EventArgs e)
        {
            init_db();
        }

        private void init_db()
        {
            amdtbse5_dtst.Clear();
            if (radioButton1.Checked == true)
            {
                // TODO: This line of code loads data into the 'amdtbse5_dtst.Contacts' table. You can move, or remove it, as needed.
                this.contactsTableAdapter.Fill(this.amdtbse5_dtst.Contacts);
            }
            else
            {
                try
                {
                    dr_univ = Quer("SELECT * FROM Contacts");
                    amdtbse5_dtst.Contacts.Load(dr_univ);
                }
                catch (Exception erty) { radioButton1.Checked = true; radioButton2.Enabled = false; Am_err ner = new Am_err(); ner.tx("Could Not Contact the Server."); }
            }
        }

        private void oper_save()
        {
            try
            {
                contactsBindingSource.EndEdit();
                if (radioButton1.Checked == true)
                {
                    contactsTableAdapter.Update(amdtbse5_dtst);
                }
                else
                {
                    asql.Save(amdtbse5_dtst.Contacts, "Contacts", Main.Amatrix.doc);
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("An Error Occured While Saving Your Contacts, Please Check Your New Entries and Try Again."); }
        }

        SqlDataReader dr_univ;
        private SqlDataReader Quer(string Query)
        {
            oper_save();
            SqlConnection conn = new SqlConnection(Main.Amatrix.doc);
            SqlCommand cmd = new SqlCommand(Query, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            eml_.tx(textBox4.Text);
            this.Close();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            /*if (dataGridView1[0, e.RowIndex].Value == DBNull.Value || dataGridView1[0, e.RowIndex].Value == "")
            {
                dataGridView1[0, e.RowIndex].Value = DateTime.Now.ToString();
            }*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            oper_save();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                amdtbse5_dtst.Contacts.Clear(); string Query = "SELECT * FROM Contacts WHERE [First Name] LIKE '%" + textBox1.Text + "%' OR [Last Name] LIKE '%" + textBox1.Text + "%' OR [First Name] + ' ' + [Last Name] LIKE '%" + textBox1.Text + "%' OR [First Name] + [Last Name] LIKE '%" + textBox1.Text + "%'";
                if (radioButton1.Checked == true)
                {
                    SqlCeConnection conn = new SqlCeConnection(contactsTableAdapter.Connection.ConnectionString);
                    SqlCeCommand cmd = new SqlCeCommand(Query, conn);
                    conn.Open();
                    SqlCeDataReader dr = cmd.ExecuteReader();
                    amdtbse5_dtst.Contacts.Load(dr);
                    conn.Close();
                }
                else
                {
                    dr_univ = Quer(Query);
                    amdtbse5_dtst.Contacts.Load(dr_univ);
                }
            }
            catch (Exception erty) { Am_err ner = new Am_err(); ner.tx("Could not Connect To Server."); }
        }

        private void Chnge_From_DTA(object sender, EventArgs e)
        {
            init_db();
        }
        //new
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                textBox9.Text = "CNT - " + DateTime.Now.ToString() + DateTime.Now.Ticks.ToString();
            }
        }

        Font fi;
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter a First or Last Name or Both (ex. John Marx)")
            {
                textBox1.Text = ""; fi = textBox1.Font;
                textBox1.ForeColor = Color.Black;
                textBox1.Font = textBox4.Font;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Enter a First or Last Name or Both (ex. John Marx)";
                textBox1.ForeColor = Color.DimGray;
                textBox1.Font = fi;
            }
        }

        private void dectmeabt_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.70)
            {
                dectmeabt.Stop();
            }
            else
            {
                this.Opacity = this.Opacity - 0.03;
            }
        }

        private void Contacts_Activated(object sender, EventArgs e)
        {
            try
            {
                dectmeabt.Stop();
            }
            catch (Exception erty) { }
            this.Opacity = Properties.Settings.Default.opacity;
        }

        private void Contacts_Deactivate(object sender, EventArgs e)
        {
            dectmeabt.Start();
        }
    }
}
