using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class results : Form
    {
        public static int completedTasks = 0;
        public results()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void results_Load(object sender, EventArgs e)
        {
            label1.Text = "You have completed "+completedTasks+" task(s) out of 3!";
            progressBar1.Maximum = 3;

            progressBar1.Value = completedTasks;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }
    }
}
