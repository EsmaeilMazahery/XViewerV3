using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XViewerV3
{
    public partial class frm_Start : Form
    {
        public frm_Start()
        {
            InitializeComponent();
        }
        public static frm_Main_Dr mainform1;
        public static frm_Main_Cl mainform2;
        private void button1_Click(object sender, EventArgs e)
        {
            mainform1 = new frm_Main_Dr();
            mainform1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainform2 = new frm_Main_Cl();
            mainform2.Show();
            this.Hide();
        }
    }
}
