using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dal_sqlce;
using DomainClasses;
using dal_sqlce.Context;
using System.Linq;

namespace goszakup_plan
{
    public partial class ekrb_Form : Form
    {
        IRepozitory<r_specific> rep = new EFRepozitory<r_specific>();
        BindingSource bs = new BindingSource();

        public ekrb_Form()
        {
            InitializeComponent();
        }

        private void ekrb_Form_Load(object sender, EventArgs e)
        {
            bs.DataSource = rep.GetList().ToList();
            dataGridViewX1.DataSource = bs;
            bindingNavigatorEx1.BindingSource = bs;
            dataGridViewX1.Columns["id"].Visible = false;
        }
    }
}
