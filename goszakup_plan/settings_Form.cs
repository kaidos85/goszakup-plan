using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace goszakup_plan
{
    public partial class settings_Form : Form
    {
        public settings_Form()
        {
            InitializeComponent();
        }

        private void settings_Form_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.koeficent;
            comboBox1.DataSource = Properties.Settings.Default.gody.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            comboBox1.Text = Properties.Settings.Default.god;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.koeficent = textBox1.Text;
            Properties.Settings.Default.god = comboBox1.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
