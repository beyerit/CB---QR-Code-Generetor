using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CB___QR_Code_Generator
{
    public partial class FormOptions : Form
    {
        private double cQuality = 20;

        public FormOptions(int quality, int eccIndex)
        {
            InitializeComponent();
            if (quality < 51) trbQuality.Value = (quality * 2) / 10;
            else
            {
                trbQuality.Value = 10;
                nudQuality.Value = quality;
            }
            cmbECC.SelectedIndex = eccIndex;
        }

        //getter
        public int getQuality()
        {
            try
            {
                return Convert.ToInt32(nudQuality.Value);
            }

            catch
            {
                return 20;
            }
        }

        public int getEccIndex()
        {
            return cmbECC.SelectedIndex;
        }

        public char getEccMode()
        {
            switch (cmbECC.SelectedIndex)
            {
                case 0: 
                    return 'L';
                case 1:
                    return 'M';
                case 2:
                    return 'Q';
                case 3:
                    return 'H';
                default:
                    return 'Q';
            }
        }

        private void trbQuality_ValueChanged(object sender, EventArgs e)
        {
            cQuality = (trbQuality.Value / 2.0) * 10;

            nudQuality.Value = Convert.ToInt32(cQuality);

            if (trbQuality.Value == 1) lblQuality.Text = trbQuality.Value + " (Very low)";
            else if (trbQuality.Value <= 3) lblQuality.Text = trbQuality.Value + " (Low)";
            else if (trbQuality.Value == 4) lblQuality.Text = trbQuality.Value + " (Normal)";
            else if (trbQuality.Value > 4 && trbQuality.Value <= 6) lblQuality.Text = trbQuality.Value + " (Better)";
            else if (trbQuality.Value > 6 && trbQuality.Value <= 9) lblQuality.Text = trbQuality.Value + " (High)";
            else if (trbQuality.Value == 10) lblQuality.Text = trbQuality.Value + " (Very high)";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void nudQuality_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnApply.PerformClick();
        }
    }
}