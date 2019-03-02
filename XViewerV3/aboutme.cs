using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XViewerV3
{
    public partial class aboutme : Form
    {
        public aboutme()
        {
            InitializeComponent();
        }

        private void aboutme_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.memar-net.ir");
        }
    }
}
