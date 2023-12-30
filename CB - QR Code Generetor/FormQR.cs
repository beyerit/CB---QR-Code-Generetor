using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;
using CB___QR_Code_Generator;
using QRCoder;

namespace CB___QR_Code_Generetor
{
    public partial class FormQR : Form
    {
        private string url;
        private char eccMode;
        private int quality;
        QRCodeGenerator.ECCLevel eccLevel = new QRCodeGenerator.ECCLevel();

        public FormQR(string url, int quality, char eccMode)
        {
            this.url = url;
            this.quality = quality;
            this.eccMode = eccMode;

            switch (eccMode)
            {
                case 'L':
                    eccLevel = QRCodeGenerator.ECCLevel.L;
                    break;
                case 'M':
                    eccLevel = QRCodeGenerator.ECCLevel.M;
                    break;
                case 'H':
                    eccLevel = QRCodeGenerator.ECCLevel.H;
                    break;
                default:
                    eccLevel = QRCodeGenerator.ECCLevel.Q;
                    break;
            }

            InitializeComponent();
        }

        //Error msg
        private void ERROR(string message)
        {
            MessageBox.Show("An error occurred: " + message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FormQR_Load(object sender, EventArgs e)
        {
            if (url != string.Empty) this.Text = "QR Code: " + url;
            pbQR.Image = generateQRcode();
        }

        //QR Code generator
        private Bitmap generateQRcode()
        {
            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, eccLevel);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(quality);
                return qrCodeImage;
            }

            catch (Exception ex)
            {
                ERROR(ex.Message);
                this.Close();
                return null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //save qr code as picture

            //filename
            string modifiedURL = url.Replace(":", "").Replace("/", "");
            string filepath = "QRCode_" + modifiedURL + ".jpeg";
            if (url.Length > 20) filepath = "QRCode_" + modifiedURL.Substring(0, 20) + ".jpeg";
            
            saveFileDialog1.FileName = filepath;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) pbQR.Image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
        }

        private void saveAsPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSave.PerformClick();
        }

        private void openInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //open url in browser
            try
            {
                Process.Start(url);
            }

            catch (Exception ex)
            {
                ERROR("URL could not be openend.\n\nMaybe '" + url + "' is not a valid URL?\n\nSystem answer: " + ex.Message);
            }
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //copy url to clipboard
            Clipboard.SetText(url);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show properties form
            FormProperties frmPrp = new FormProperties(pbQR.Image.Height + " x " + pbQR.Image.Width, quality, eccMode, url);
            frmPrp.Show();
        }

        private void copyImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //copy image to clipboard
            Clipboard.SetImage(pbQR.Image);
        }
    }
}
