using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dal_sqlce;

namespace goszakup_plan
{
    public partial class ktru_rabot_Form : Form
    {
        //dal database = new dal();
        BindingSource bs = new BindingSource();

        public ktru_rabot_Form()
        {
            InitializeComponent();
            comboBoxItem5.SelectedIndex = 0;
        }

        private void ktru_rabot_Form_Load(object sender, EventArgs e)
        {
            //bs = new workDB().ktru_rabot().tableLoadBS();
            dataGridViewX1.DataSource = bs;
            bindingNavigatorEx1.BindingSource = bs;
            dataGridViewX1.Columns["id"].Visible = false;
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            bs.Filter = String.Empty;
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedCells != null)
            {
                Clipboard.SetText(dataGridViewX1.SelectedCells[0].Value.ToString());
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            bs.Filter = comboBoxItem5.SelectedItem.ToString() + " LIKE '%" + textBoxItem1.Text + "%'";
        }
    }
}
