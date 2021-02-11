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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            identifyingAreas r = new identifyingAreas();
            this.Hide();
            r.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            findingCallnumbers r = new findingCallnumbers();
            this.Hide();
            r.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            replacingBooks r = new replacingBooks();
            this.Hide();
            r.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            results r = new results();
            this.Hide();
            r.Show();
        }
    }
}
