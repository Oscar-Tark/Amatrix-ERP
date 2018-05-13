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
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
using System.ComponentModel;

namespace Isync
{
    class isync_start
    {
        public DataTable dtp_main = new DataTable();
        private DataTable dtp_check = new DataTable();

        string Connection_String, Query;
        Base_ASQL.BASQL basql = new Base_ASQL.BASQL();

        public BackgroundWorker bkk = new BackgroundWorker();

        public isync_start(string Conn, DataTable dtp_port, string LQU)
        {
            dtp_main = dtp_port;
            dtp_main.PrimaryKey = dtp_port.PrimaryKey;
            Query = LQU;
            Connection_String = Conn;
            bkk.WorkerSupportsCancellation = true;
            bkk.DoWork += new DoWorkEventHandler(bkk_DoWork);
            //bkk.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkk_RunWorkerCompleted);
        }

        /*void bkk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GC.Collect();
        }*/

        void bkk_DoWork(object sender, DoWorkEventArgs e)
        {
            Run_SQL();
            Check();
        }

        private void Run_SQL()
        {
            //try
            //{
            dtp_check.Clear();
            SqlConnection conn = new SqlConnection(Connection_String);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtp_check);

            cmd.Dispose();
            da.Dispose();
            conn.Close();
            conn.Dispose();
            //dtp_check = basql.Execute(Connection_String, Query, "", dtp_check);

            DataColumn[] dtpcll = new DataColumn[1];
            dtpcll[0] = dtp_check.Columns[0];
            dtp_check.PrimaryKey = dtpcll;
            //}
            //catch (System.Exception erty) { }
        }

        ArrayList al_remv = new ArrayList();
        private void Check()
        {
            //try
            //{
            if (dtp_main.Rows.Count < dtp_check.Rows.Count)
            {
                dtp_main.Merge(dtp_check);
            }
            else
            {
                al_remv.Clear();
                foreach (DataRow dr in dtp_main.Rows)
                {
                    if (dtp_check.Rows.Contains(dr.ItemArray[0]) == false)
                    {
                        al_remv.Add(dr);
                    }
                }

                foreach (DataRow dr in al_remv)
                {
                    row_Del(dr);
                }
                al_remv.Clear();
            }
            //}
            //catch (System.Exception) { }
        }

        private void row_Add(DataRow dr)
        {
            DataRow dr1 = dtp_main.NewRow();
            dr1.ItemArray = dr.ItemArray;
            try
            {
                dtp_main.Rows.Add(dr1);
            }
            catch (System.Exception erty) { dr1.Delete(); }
        }

        private void row_Del(DataRow dr)
        {
            try
            {
                dtp_main.Rows.Remove(dr);
            }
            catch (System.Exception erty) { }
        }

        public void call()
        {
            try
            {
                bkk.RunWorkerAsync();
            }
            catch (System.Exception erty) { }
        }
    }
}