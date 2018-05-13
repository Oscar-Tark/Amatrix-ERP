/*  <Scorpion IEE(Intelligent Execution Environment). A Program To Run Scorpion Built Applications Using the Scorpion IDE>
    Copyright (C) <2013>  <Oscar Arjun Singh Tark>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Drawing;
using System.ComponentModel;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
        private delegate void del_do(object Scorp_Line);
        private Thread th_do; public bool pointered = false;
        string Temp_str = ""; bool iscontrol = false; bool isitem = false; public bool cuimode = false;
        bool Continue_SCR = true; Form fm_temp; public bool resend = false; Control cntr_tmp; ToolStripItem tsi_tmp; public string strng_tmp;
        public ArrayList AL_Ref_EVT = new ArrayList(); string current_path; int var;
        //threads for reading

        Thread th_lib;
        Thread th_gui;
        delegate void del_lib();
        delegate void del_gui();

        public bool Decider(string Method)
        {
 //           rdd = rd_send;
            Temp_str = Method;
            do_(Method);
            return Continue_SCR;
        }

        public void do_(string Scorp_Line)
        {
            try
            {
                Thread th_doo = new Thread(new ParameterizedThreadStart(do2_));
                th_doo.IsBackground = true;
                th_doo.Start((object)Scorp_Line);
            }
            catch (Exception erty) { }
        }

        private void do2_(object Scorp_Line)
        {
            try
            {
                del_do ddo = new del_do(work_);
                ddo.Invoke(Scorp_Line);
            }
            catch (Exception erty) { MessageBox.Show(erty.Message + " (" + erty.StackTrace + ")"); }
        }

        public void work_(object Scorp_Line)
        {
            MessageBox.Show("entered work");
            string Scorp_Line_Exec = Scorp_Line.ToString();
            if (Scorp_Line_Exec.StartsWith("add(") == true || Scorp_Line_Exec.StartsWith("create(") == true)
            {
                add(Scorp_Line_Exec);
            }
            if (Scorp_Line_Exec.StartsWith("remove(") == true || Scorp_Line_Exec.StartsWith("delete(") == true)
            {
                remove(Scorp_Line_Exec);
            }
            if (Scorp_Line_Exec.StartsWith("change(") == true || Scorp_Line_Exec.StartsWith("modify(") == true)
            {
                modify(Scorp_Line_Exec);
            }
        }

        private void add(string Scorp_Line_Exec)
        {
            MessageBox.Show("add");
            int ndx = Scorp_Line_Exec.IndexOf("add(", 0);
            if (ndx == -1)
            {
                ndx = Scorp_Line_Exec.IndexOf("create(", 0);
            }
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);
            //create(textbox tbx location[], size[], text[])
        }

        private void modify(string Scorp_Line_Exec)
        {
        }

        private void remove(string Scorp_Line_Exec)
        {
        }
    }
}