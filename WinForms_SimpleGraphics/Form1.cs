using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForms_SimpleGraphics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document im = new Document();
            im.MdiParent = this;

            im.Show();
        }

        private void closeCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure about that?", "Closing file", MessageBoxButtons.OKCancel);
            if (d == DialogResult.OK)
            {
                ActiveMdiChild.Close();
            }
        }

        private void closeThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure about that?", "Closing file", MessageBoxButtons.OKCancel);
            if (d == DialogResult.OK)
            {
                foreach (var item in MdiChildren)
                {
                    item.Close();
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void saveCurrentAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

}
