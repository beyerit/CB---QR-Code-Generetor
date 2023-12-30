using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace CB___QR_Code_Generator
{
    public partial class FormProperties : Form
    {
        public FormProperties(string resolution, int pxQuality, char eccMode, string url)
        {
            InitializeComponent();

            rtbText.Text = "Resolution: " + resolution + " pixel\n\nPixels per module: " + pxQuality +"\n\nECC mode: " + eccMode + "\n\n\n\nURL:\n\n" + url;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rtbText_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            //open url when ctrl is pressed
            if (Keyboard.IsKeyDown(Key.LeftCtrl)) Process.Start(e.LinkText);
        }
    }
}