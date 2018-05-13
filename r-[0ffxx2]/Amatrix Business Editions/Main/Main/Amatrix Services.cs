using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Text;
using am_main.basics;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
//                                                                                                                                                                         0. END
//____________________________________________________________________________________________________________________________

namespace Main
{
    // Main class ~
    public partial class Amatrix
    {
        public static ArrayList AL_CLOCKS = new ArrayList();
        public string TIME = "N.A.";

        public void Time()
        {
            if (Properties.Settings.Default.hrfrmt == 0 || Properties.Settings.Default.hrfrmt == null || Properties.Settings.Default.hrfrmt == 1)
            {
                TIME = DateTime.Now.ToString();
            }
            else if (Properties.Settings.Default.hrfrmt == 2)
            {
                TIME = DateTime.Now.ToUniversalTime().ToString();
            }
            else if (Properties.Settings.Default.hrfrmt == 3)
            {
                TIME = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            }
            else if (Properties.Settings.Default.hrfrmt == 4)
            {
                TIME = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " - " + DateTime.Now.ToShortTimeString();
            }
            else if (Properties.Settings.Default.hrfrmt == 5)
            {
                TIME = DateTime.Now.ToLongDateString();
            }
            else if (Properties.Settings.Default.hrfrmt == 6)
            {
                TIME = DateTime.Now.ToLongTimeString();
            }

            foreach (object clock_ in AL_CLOCKS)
            {
                if (clock_ is ToolStripItem)
                {
                    ((ToolStripItem)clock_).Text = TIME;
                }
            }
        }

        public void Login()
        {
            loggy lg = new loggy();
            return;
        }
    }
}