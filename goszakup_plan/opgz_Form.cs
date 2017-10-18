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
    public partial class opgz_Form : Form
    {
        //dal database = new dal();
        BindingSource bs = new BindingSource();

        public opgz_Form()
        {
            InitializeComponent();
        }

        private void opgz_Form_Load(object sender, EventArgs e)
        {
            //bs = new workDB().opgz().tableLoadBS();
            dataGridViewX1.DataSource = bs;
            bindingNavigatorEx1.BindingSource = bs;
            //dataGridViewX1.Columns["id"].Visible = false;
            //dataGridViewX1.Columns["Название_каз"].Width = 220;
            //dataGridViewX1.Columns["Название_рус"].Width = 220;
        }
    }
}
