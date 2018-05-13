using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Currencies : Form
    {
        public Currencies()
        {
            InitializeComponent();
        }

        private void Currencies_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'currencies_dtst.Currencies' table. You can move, or remove it, as needed.
            this.currenciesTableAdapter.Fill(this.currencies_dtst.Currencies);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            currenciesTableAdapter.Update(currencies_dtst);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgv.SelectedRows)
            {

            }
        }
    }
}
