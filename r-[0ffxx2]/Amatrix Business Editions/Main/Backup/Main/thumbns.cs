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
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class Thumbnail : Form
    {
        private IntPtr m_hThumbnail;

        public Thumbnail() { InitializeComponent(); this.TransparencyKey = Color.AliceBlue; this.BackColor = Color.AliceBlue; this.Icon = Properties.Resources.bt37endx64; } //Thumbnail/*this.Size = new Size(200, 150);*/ }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Thumbnail
            // 
            this.ClientSize = new System.Drawing.Size(532, 510);
            this.Name = "Thumbnail";
            this.Load += new System.EventHandler(this.Thumbnail_Load);
            this.ResumeLayout(false);
            try
            {
                //DwmApi.MARGINS m = new DwmApi.MARGINS(1500, 1500, 1500, 1500);
                //DwmApi.DwmExtendFrameIntoClientArea(Handle, m);
            }
            catch (Exception enoaerothmp)
            {
                }
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);

            // when you are done with the thumbnail, unregister it
            if (m_hThumbnail != IntPtr.Zero)
            {
                if (DwmApi.DwmIsCompositionEnabled()) DwmApi.DwmUnregisterThumbnail(m_hThumbnail);
                try
                {
                    m_hThumbnail = IntPtr.Zero;
                }
                catch (Exception excnorg) { this.Dispose(); }
            }
        }

        public void CreateAndShow(IntPtr sourceWindow)
        {
            // register the thumbnail to associate this form's hwnd (where we
            // want the thumbnail rendered to) with the parent hwnd
            // (which is the window we want the thumbnail of) and put the info 
            // in the thumbnail handle
            m_hThumbnail = DwmApi.DwmRegisterThumbnail(this.Handle, sourceWindow);

            // create and set a thumbnail properties to tell the DWM
            // how we want the thumbnail to be rendered.
            DwmApi.DWM_THUMBNAIL_PROPERTIES m_ThumbnailProperties = new DwmApi.DWM_THUMBNAIL_PROPERTIES();

            m_ThumbnailProperties.dwFlags = DwmApi.DWM_THUMBNAIL_PROPERTIES.DWM_TNP_VISIBLE
                + DwmApi.DWM_THUMBNAIL_PROPERTIES.DWM_TNP_OPACITY
                + DwmApi.DWM_THUMBNAIL_PROPERTIES.DWM_TNP_RECTDESTINATION
                + DwmApi.DWM_THUMBNAIL_PROPERTIES.DWM_TNP_SOURCECLIENTAREAONLY;
            m_ThumbnailProperties.opacity = 255;
            m_ThumbnailProperties.fVisible = true;
            m_ThumbnailProperties.rcSource = m_ThumbnailProperties.rcDestination =
                new DwmApi.RECT(0, 0, ClientRectangle.Right, ClientRectangle.Bottom);



            m_ThumbnailProperties.fSourceClientAreaOnly = true;

            // now update the thumbnail properties - this actually starts the live rendering
            // in the thumbnail
            DwmApi.DwmUpdateThumbnailProperties(m_hThumbnail, m_ThumbnailProperties);

            this.Show();
            object j = (object)this.ClientRectangle;
            //this.DrawToBitmap(Bitmap.FromFile());
        }

        private void Thumbnail_Load(object sender, EventArgs e)
        {

        }
    }
}