using System; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CB___QR_Code_Generator;
using QRCoder;

namespace CB___QR_Code_Generetor
{
    /*
            ### CB - QR Code Generator ###

            Copyright © 2020 - 2022 Christoph Beyer
    */
    public partial class FormMain : Form
    {
        double calcquality = 20;
        char eccMode = 'Q';
        int eccIndex = 2;

        public FormMain()
        {
            InitializeComponent();
            pbPreview.Image = generateQRcode();

            //transparent label
            llblAbout.Parent = pictureBox1;
            llblAbout.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Open new window with qr code
            if (tbURL.TextLength <= 1663)
            {
                FormQR frmQR = new FormQR(tbURL.Text, Convert.ToInt32(calcquality), eccMode);
                frmQR.Show();
            }

            else
                MessageBox.Show("The maximum size of QR Code is 1663 bytes, however your string is " + tbURL.Text.Length + " bytes in size. Please shorten your string and try again.", "Error: string too long", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tbURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnGenerate.PerformClick();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //avout form
        {
            FormAbout frmAbt = new FormAbout();
            frmAbt.ShowDialog();
        }

        private void btnPaste_Click_1(object sender, EventArgs e)
        {
            if (tbURL.Text == string.Empty)
            {
                tbURL.Text = Clipboard.GetText(); //paste from clipboard
            }

            else
            {
                tbURL.Text = "";
            }
        }

        private void button1_Click_1(object sender, EventArgs e) //more form
        {
            FormText frmtxt = new FormText(tbURL.Text);
            frmtxt.ShowDialog();

            if (frmtxt.DialogResult == DialogResult.OK) tbURL.Text = frmtxt.getTheText();
        }

        private Bitmap generateQRcode()
        {
            //generate bitmap qr code
            string useURL = tbURL.Text;
            if (useURL.Length > 1663) useURL = useURL.Substring(0, 1663);

            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(useURL, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(3);
                return qrCodeImage;
            }

            catch
            {
                return null;
            }
        }

        private void tbURL_TextChanged(object sender, EventArgs e)
        {
            if (pbPreview.Visible) pbPreview.Image = generateQRcode();
        }

        private void pbPreview_Click(object sender, EventArgs e)
        {
            //hide preview qr code
            pbPreview.Visible = false;
            pbPreview.Image = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //options form
            FormOptions frmOpt = new FormOptions(Convert.ToInt32(calcquality), eccIndex);
            frmOpt.ShowDialog();

            if (frmOpt.DialogResult == DialogResult.OK)
            {
                calcquality = frmOpt.getQuality();
                eccMode = frmOpt.getEccMode();
                eccIndex = frmOpt.getEccIndex();

                toolTip1.SetToolTip(btnGenerate, "Pixels per module: " + calcquality + "\nECC mode: " + eccMode);
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            //bring back preview qr code 
            pbPreview.Visible = true;
            pbPreview.Image = generateQRcode();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnGenerate, "Pixels per module: " + calcquality + "\nECC mode: " + eccMode);
        }
    }
}
