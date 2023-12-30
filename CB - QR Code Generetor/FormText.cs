using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CB___QR_Code_Generator
{
    public partial class FormText : Form
    {
        public FormText(string lastText)
        {
            InitializeComponent();
            tbInput.Text = lastText;

            //move cursor
            tbInput.SelectionStart = tbInput.Text.Length;
            tbInput.SelectionLength = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string getTheText()
        {
            return tbInput.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            //characters count
            tslblLength.Text = "Length: " + tbInput.Text.Length;
            if (tbInput.Text.Length > 1663) tslblLength.ForeColor = Color.Red;
            else tslblLength.ForeColor = Color.Black;
        }

        private void FormText_Load(object sender, EventArgs e)
        {

        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //wordwrap
            wordWrapToolStripMenuItem.Checked = !wordWrapToolStripMenuItem.Checked;
            tbInput.WordWrap = wordWrapToolStripMenuItem.Checked;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //textbox clear
            tbInput.Clear();
        }

        private void applyAndCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void shortenStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //shorten string
            if (tbInput.Text.Length > 1663) tbInput.Text = tbInput.Text.Substring(0, 1663);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //import file
            if (openFileDialog1.ShowDialog () == DialogResult.OK)
            {
                try
                {
                    using (StreamReader streamReader = new StreamReader(openFileDialog1.FileName, Encoding.UTF8))
                    {
                        tbInput.Text = streamReader.ReadToEnd();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: File '"+ openFileDialog1.SafeFileName + "' could not be loaded.\n\nSystem message: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
